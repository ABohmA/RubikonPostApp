﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RubikonZadatak.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class RubikonTaskDBEntities : DbContext
    {
        public RubikonTaskDBEntities()
            : base("name=RubikonTaskDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BlogPost> BlogPosts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
    
        public virtual ObjectResult<SPDB_GetPostsByTag_Result> SPDB_GetPostsByTag(string tag)
        {
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPDB_GetPostsByTag_Result>("SPDB_GetPostsByTag", tagParameter);
        }
    }
}