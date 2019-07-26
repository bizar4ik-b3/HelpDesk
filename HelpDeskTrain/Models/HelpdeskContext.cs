using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions; 
namespace HelpDeskTrain.Models
{
    public class HelpdeskContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Lifecycle> Lifecycles { get; set; }
        public DbSet<Hardware> Hardwares { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Orgtechnic> Orgtechnics { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Activ> Activs { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<NadpsuOrgState> NadpsuOrgStates { get; set; }
        public DbSet<Kadet> Kadets { get; set; }
        public DbSet<LessonList> LessonLists { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<Eval> Evals { get; set; }
        //public DbSet<JournalAccess> JournalAccess { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }

        
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {

       // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
        /*    modelBuilder.Entity<LessonList>().HasMany(l => l.Kadets)
            .WithMany(s => s.LessonLists)
            .Map(t => t.MapLeftKey("LessonlistId")
            .MapRightKey("KadetId")
            .ToTable("Evals"));

         modelBuilder.Entity<Journal>()
              .HasMany(l => l.LessonLists)
              .WithMany(s => s.Journals)
              .Map(t => t.MapLeftKey("JournalId")
              .MapRightKey("LessonListId")
              .ToTable("JournalLessonLists"));
      
           */
    }

    
    }
}