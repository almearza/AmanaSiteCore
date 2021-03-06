using System.Diagnostics.CodeAnalysis;
using AmanaSite.Models;
using AmanaSite.Models.Survey;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AmanaSite.Data
{
    public class DContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>,
    AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<SlidersShow> SlidersShows { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<NewsType> NewsTypes { get; set; }
        public DbSet<Baladyat> Baladyat { get; set; }
        public DbSet<Info> Info { get; set; }
        public DbSet<AmanaDocs> AmanaDocs { get; set; }
      
      
        public DbSet<SurveyData> SurveyData { get; set; }
        public DbSet<SurveyAge> SurveyAge { get; set; }
        public DbSet<SurveyEducation> SurveyEducation { get; set; }
        public DbSet<SurveyEvaluateEmployee> SurveyEvaluateEmployee { get; set; }
        public DbSet<SurveyGender> SurveyGender { get; set; }
        public DbSet<SurveyNationalty> SurveyNationalty { get; set; }
        public DbSet<SurveyReferencesType> SurveyReferencesType { get; set; }
        public DbSet<SurveyTransactionCompletion> SurveyTransactionCompletion { get; set; }
        public DbSet<SurveyTransactionType> SurveyTransactionType { get; set; }
        public DbSet<SurveyVisitAvg> SurveyVisitAvg { get; set; }
        public DContext([NotNullAttribute] DbContextOptions options) : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<AppUser>()
            .HasMany(u => u.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

            builder.Entity<AppRole>()
            .HasMany(u => u.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId)
            .IsRequired();



        }

    }
}