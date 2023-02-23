using ResearchData.Models.Interfaces;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using ResearchData.Models.EF;

namespace ResearchData.Models
{
    public class AppDbContext : DbContext
    {
        //Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
     
            //Propeties - [T A B L E S]
            public DbSet<Account> Account { get; set; }
            public DbSet<Address> Address { get; set; }
            public DbSet<Answer> Answer { get; set;}    
            
            public DbSet<Question> Question { get; set; }
            public DbSet<QuestionOption> QuestionOption { get; set; }
            public DbSet<QuestionSubsection> QuestionSubsection { get; set;}

            public DbSet<QuestionType> QuestionType { get; set; }
            public DbSet<Section> Section { get; set; }
            public DbSet<Subsection> Subsection { get; set;}   

            public DbSet<Subunit> Subunit { get; set; }
            public DbSet<Survey> Survey { get; set; }
            public DbSet<SurveyQuestion> SurveyQuestion { get; set; }
        
    }
}
