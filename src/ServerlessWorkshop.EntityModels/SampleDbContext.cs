using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace ServerlessWorkshop.EntityModels
{
    public class SampleDbContext : DbContext
    {
        static SampleDbContext()
        {
            Database.SetInitializer<SampleDbContext>(null);
        }

        public SampleDbContext()
            : base("Name=SampleDbContext")
        {
        }

        public SampleDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Configurations.Add(new UserMap());
        }
    }

    public class User
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset DateUpdated { get; set; }
    }

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(p => p.UserId);

            // Properties
            this.Property(p => p.UserId).IsRequired();
            this.Property(p => p.Username).IsRequired().HasMaxLength(50);
            this.Property(p => p.Password).IsRequired().HasMaxLength(50);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsOptional();

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(p => p.UserId).HasColumnName("UserId");
            this.Property(p => p.Username).HasColumnName("Username");
            this.Property(p => p.Password).HasColumnName("Password");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");
        }
    }
}
