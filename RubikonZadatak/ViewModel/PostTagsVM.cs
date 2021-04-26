using RubikonZadatak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubikonZadatak.ViewModel
{
    public class PostTagsVM
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string PostDescription { get; set; }
        public string Body { get; set; }
        public List<string> Tagovi { get; set; }
        public System.DateTime CreateAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }

    }
}