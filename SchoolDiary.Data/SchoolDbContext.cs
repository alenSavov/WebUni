using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolDiary.Models;

namespace SchoolDiary.Data
{
    public class SchoolDbContext : IdentityDbContext<User>
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Course> Courses { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UsersCourses> UsersCourses { get; set; }

        public DbSet<UsersEvents> UsersEvents { get; set; }

        public DbSet<CoursesResources> CoursesResources { get; set; }

        public DbSet<EventsResources> EventsResources { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                  .Entity<UsersCourses>()
                  .HasKey(x => new { x.UserId, x.CourseId });

            builder
                .Entity<UsersCourses>()
                .HasOne(x => x.Course)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CourseId);

            builder
                .Entity<UsersCourses>()
                .HasOne(x => x.User)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.UserId);               
            
            builder
                .Entity<Category>()
                .HasMany(c => c.Courses)
                .WithOne(c => c.Category)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<UsersEvents>()
                .HasKey(x => new { x.UserId, x.EventId });

            builder
                .Entity<UsersEvents>()
                .HasOne(x => x.Event)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.EventId);

            builder
              .Entity<UsersEvents>()
              .HasOne(x => x.User)
              .WithMany(x => x.Events)
              .HasForeignKey(x => x.UserId);

            builder
                .Entity<CoursesResources>()
                .HasKey(x => new { x.CourseId, x.ResourceId });

            builder
                .Entity<EventsResources>()
                .HasKey(x => new { x.EventId, x.ResourceId });

            builder
                .Entity<Comment>()
                .HasOne(x => x.Article)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
