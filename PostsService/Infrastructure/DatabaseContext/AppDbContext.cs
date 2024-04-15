using Microsoft.EntityFrameworkCore;
using PostsService.Features.Entities;

namespace PostsService.Infrastructure.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Init database
            Database.Migrate();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Technology", Description = "Posts related to technology" },
            new Category { Id = 2, Name = "Science", Description = "Posts related to science" }
                // Add more categories as needed
                );

            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "Programming" },
                new Tag { Id = 2, Name = "Machine Learning" }
            // Add more tags as needed
            );

            // Add 500 posts
            for (int i = 3; i <= 502; i++)
            {
                modelBuilder.Entity<Post>().HasData(
                    new Post
                    {
                        Id = i,
                        Title = $"Post {i}",
                        Content = $"This is the content of post {i}",
                        Author = "Anonymous",
                        AuthorId = 0,
                        Rating = 0,
                        CommentCount = 0,
                        SavedCount = 0,
                        IsDeleted = false,
                        IsPublished = true,
                        CategoryId = 1, // Technology category
                    }
                );
                modelBuilder.Entity("PostTag").HasData(
                new { PostsId = i, TagsId = 1 });

            }
        }


    }
}
