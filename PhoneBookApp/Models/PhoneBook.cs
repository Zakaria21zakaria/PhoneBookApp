using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneBookApp.Models
{
    public class PhoneBook
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Book Name")]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<Contact> contacts { get; set;}

    }

    public class PhoneBookDBContext : DbContext
    {
        public virtual DbSet<PhoneBook> PhoneBook { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<PhoneBookDBContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}