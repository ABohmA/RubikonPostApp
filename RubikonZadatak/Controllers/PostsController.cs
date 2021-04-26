using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;
using RubikonZadatak.Models;
using RubikonZadatak.ViewModel;

namespace RubikonZadatak.Controllers
{
    public class PostsController : ApiController
    {
        private RubikonTaskDBEntities db = new RubikonTaskDBEntities();

        public string GenerateSlug(string naslov)
        {
            string Slug;

            //=== STRING GENERATOR ===
            //U slucaju da imamo vise istih naslova doslo bi do slucaja da imamo vise istih slugova.
            //Radi toga je napravljen generator za string od 8 karaktera koji ce biti
            //dodan na kraju sluga kako bi svaki slug bio jedinstven, a naslovi u sebi nece morati da nose brojeve po kojem su redosljedu napravljeni
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string Uq = null;
            for (int i = 0; i < 10; i++)
            {
                Uq += chars[random.Next(chars.Length)];
            }

            //Regex za pravljenje sluga
            string str = naslov.ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-");

            Slug = str + "-" + Uq;
            return Slug;
        }

        [Route("api/Posts/GetBySlug/{slug}")]
        [HttpGet]
        public PostTagsVM GetPostBySlug(string slug)
        {
            PostTagsVM Post = new PostTagsVM();
            BlogPost BP = db.BlogPosts.Where(x => x.Slug == slug).FirstOrDefault();

            Post.Slug = BP.Slug;
            Post.Title = BP.Title;
            Post.PostDescription = BP.PostDescription;
            Post.Body = BP.Body;

            List<Tag> TList = BP.Tags.ToList();
            Post.Tagovi = new List<string>();
            foreach (Tag T in TList)
                Post.Tagovi.Add(T.TName.ToString());

            Post.CreateAt = BP.CreateAt;
            Post.UpdatedAt = BP.UpdatedAt;

            return Post;
        }

        [Route("api/Posts/GetAll/{tag?}")]
        [HttpGet]
        public List<PostTagsVM> GetPosts(string tag = "")
        {
            List<SPDB_GetPostsByTag_Result> PostByTag = new List<SPDB_GetPostsByTag_Result>();
            PostByTag = db.SPDB_GetPostsByTag(tag).OrderByDescending(x => x.CreateAt).ToList();

            List<PostTagsVM> ListVM = new List<PostTagsVM>();

            foreach (SPDB_GetPostsByTag_Result BP in PostByTag)
            {
                PostTagsVM Post = new PostTagsVM();
                BlogPost BloGp = db.BlogPosts.Where(x => x.Slug == BP.Slug).FirstOrDefault();

                Post.Slug = BP.Slug;
                Post.Title = BP.Title;
                Post.PostDescription = BP.PostDescription;
                Post.Body = BP.Body;

                List<Tag> TList = BloGp.Tags.ToList();
                Post.Tagovi = new List<string>();
                foreach (Tag T in TList)
                    Post.Tagovi.Add(T.TName.ToString());

                Post.CreateAt = BP.CreateAt;
                Post.UpdatedAt = BP.UpdatedAt;

                ListVM.Add(Post);
            }
            return ListVM.OrderByDescending(x => x.CreateAt).ToList();
        }

        [Route("api/Posts/post")]
        [HttpPost]
        public void PostBlogPost(PostTagsVM Novi)
        {
            //Dodavanje Novih tagova u tabelu tagovi
            List<Tag> SviTagovi = db.Tags.ToList();

            foreach (string item in Novi.Tagovi)
            {
                if (SviTagovi.Where(x => x.TName == item).Count() == 0)
                {
                    Tag NoviT = new Tag();
                    NoviT.TName = item;
                    db.Tags.Add(NoviT);
                    db.SaveChanges();
                }
            }

            // Novi POST
            BlogPost NoviBP = new BlogPost();
            NoviBP.Title = Novi.Title;
            NoviBP.PostDescription = Novi.PostDescription;
            NoviBP.Body = Novi.Body;
            NoviBP.CreateAt = System.DateTime.Now;

            //Provjera da li slug vec postoji i u slucaju da postoji generise novi i pravi opet provjeru
            do
            {
                NoviBP.Slug = GenerateSlug(Novi.Title);
            } while (db.BlogPosts.Where(x => x.Slug == NoviBP.Slug).Count() > 0);

            //Dodavanje tagova u POST
            foreach (string item in Novi.Tagovi)
            {
                NoviBP.Tags.Add(db.Tags.Where(x => x.TName.ToLower() == item.ToLower()).FirstOrDefault());
            }

            db.BlogPosts.Add(NoviBP);
            db.SaveChanges();
        }

        [Route("api/Posts/Update")]
        [HttpPut]
        public void PostBlogUpdate(PostTagsVM Izmjena)
        {
            // Nije navedeno da je potrebno raditi izmjenu tagov pa iz istog razloga ona nece biti omogucena
            //Slug se ne updejtuje posto se on u ovome slucaju koristi kao primarni kljuc,
            //u suprotnom da sam imao ID takodjer bih mijenjao i slug zajedno sa ostalim izmjenama
            BlogPost ZaIzmjenu = db.BlogPosts.Where(x => x.Slug == Izmjena.Slug).FirstOrDefault();

            if (Izmjena.Title != null)
                ZaIzmjenu.Title = Izmjena.Title;

            if (Izmjena.PostDescription != null)
                ZaIzmjenu.PostDescription = Izmjena.PostDescription;

            if (Izmjena.Body != null)
                ZaIzmjenu.Body = Izmjena.Body;

            ZaIzmjenu.UpdatedAt = System.DateTime.Now;

            //brisanje starih tagova
            if (Izmjena.Tagovi != null)
            {
                ZaIzmjenu.Tags.Clear();

                //Provjera da li su se pojavili novi tagovi koje nemamo u tabeli tagova pri izmjeni posta
                //i u tome slucaju ih dodajemo u tabelu tagovi
                List<Tag> SviTagovi = db.Tags.ToList();
                foreach (string item in Izmjena.Tagovi)
                {
                    if (SviTagovi.Where(x => x.TName == item).Count() == 0)
                    {
                        Tag NoviT = new Tag();
                        NoviT.TName = item;
                        db.Tags.Add(NoviT);
                        db.SaveChanges();
                    }
                }

                //povezivanja potrebnih tagova sa postom
                foreach (string item in Izmjena.Tagovi)
                {
                    ZaIzmjenu.Tags.Add(db.Tags.Where(x => x.TName.ToLower() == item.ToLower()).FirstOrDefault());
                }
            }
            db.SaveChanges();
        }

        [Route("api/Posts/Del/{slug}")]
        [HttpDelete]
        public void PostDelete(string slug)
        {
            BlogPost bp = db.BlogPosts.Where(x => x.Slug == slug).FirstOrDefault();

            db.BlogPosts.Remove(bp);
            db.SaveChanges();


        }
    }
}
