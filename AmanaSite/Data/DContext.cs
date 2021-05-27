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
        public DbSet<New> News { get; set; }
        public DbSet<NewsType> NewsTypes { get; set; }
        public DbSet<BaladyaDescr> Baladyat { get; set; }
        public DbSet<AmanaLink> AmanaLinks { get; set; }
        public DbSet<MAndF> MAndF { get; set; }
        public DbSet<MAndFType> MAndFType { get; set; }
        public DbSet<AmanaService> AmanaServices { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
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
        {
        }

    }
}