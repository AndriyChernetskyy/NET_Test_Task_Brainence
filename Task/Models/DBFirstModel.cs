using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication8.Models
{
    public class DbFirstModel : DbContext
    {
        public DbFirstModel()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-L3DGQ0O\SQLEXPRESS;Database=Sentences;Trusted_Connection=True;");
        }

        public virtual DbSet<InputSentences> Sentences { get; set; }
    }

    public class InputSentences
    {
        [Key]
        public int Id { get; set; }
        public int NumberOfOccurrences { get; set; }
        public string Sentence { get; set; }
    }

}

