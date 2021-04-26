using RubikonZadatak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RubikonZadatak.Controllers
{
    public class TagsController : ApiController
    {
        private RubikonTaskDBEntities db = new RubikonTaskDBEntities();

        [Route("api/Tags")]
        [HttpGet]
        public List<String> GetTags()
        {
            List<Tag> SviTagovi = new List<Tag>();
            SviTagovi = db.Tags.ToList();
            List<string> Tagovi = new List<string>();
            foreach (Tag item in SviTagovi)
            {
                Tagovi.Add(item.TName);
            }
            return Tagovi;
        }
    }
}
