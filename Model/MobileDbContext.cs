using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace MobileBackend.Model
{
    public partial class MobileDbContext : DbContext
    {
        private IConfiguration _configuration;

        // public MobileDbContext(IConfiguration Configuration)
        // {
        //     _configuration = Configuration;
        // }

        public MobileDbContext(DbContextOptions<MobileDbContext> options,IConfiguration Configuration)
            : base(options)
        {
            this._configuration = Configuration;
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<FollowRelation> FollowRelation { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserLikePost> UserLikePost { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(this._configuration.GetConnectionString("mobiledb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.PostId, e.UserId });

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PostId)
                    .HasName("fk_comment_Post1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_comment_User1_idx");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comment)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comment_Post1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comment_User1");
            });

            modelBuilder.Entity<FollowRelation>(entity =>
            {
                entity.HasKey(e => new { e.From, e.To });

                entity.HasIndex(e => e.From)
                    .HasName("fk_User_has_User_User1_idx");

                entity.HasIndex(e => e.To)
                    .HasName("fk_User_has_User_User2_idx");

                entity.HasOne(d => d.FromNavigation)
                    .WithMany(p => p.FollowRelationFromNavigation)
                    .HasForeignKey(d => d.From)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_has_User_User1");

                entity.HasOne(d => d.ToNavigation)
                    .WithMany(p => p.FollowRelationToNavigation)
                    .HasForeignKey(d => d.To)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_has_User_User2");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.PostId, e.UserId });

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PostId)
                    .HasName("fk_Image_Post1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_Image_User1_idx");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Image)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Image_Post1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Image_User1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId });

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_Post_User1_idx");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Post_User1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();
            });

            modelBuilder.Entity<UserLikePost>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PostId, e.PostUserId });

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_User_has_Post_User1_idx");

                entity.HasIndex(e => new { e.PostId, e.PostUserId })
                    .HasName("fk_User_has_Post_Post1_idx");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLikePost)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_has_Post_User1");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.UserLikePost)
                    .HasForeignKey(d => new { d.PostId, d.PostUserId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_has_Post_Post1");
            });
        }
    }
}
