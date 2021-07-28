using System;
using Blog.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Blog.Entities;
using Blog.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Blog.Data
{
    public class AppDataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDataContext(
            DbContextOptions<AppDataContext> options,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);


            if (optionsBuilder.IsConfigured)
                return;

            var connectionString = string.Empty;
            var companyId = _httpContextAccessor.HttpContext.User.CompanyId();

            // Só um exemplo, não usar em PROD!
            connectionString = _configuration.GetConnectionString(companyId == 0
                ? "DefaultConnection"
                : $"Company-{companyId}");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new TagMap());
            modelBuilder.ApplyConfiguration(new UserMap());




            // Query Filters
            modelBuilder.Entity<Post>().HasQueryFilter(x => EF.Property<int>(x, "CompanyId") == _httpContextAccessor.HttpContext.User.CompanyId());
        }
    }
}