using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Blog.Models
{
    public partial class BlogContext : DbContext
    {
        public BlogContext()
        {
        }

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<DetailsSaved> DetailsSaveds { get; set; } = null!;
        public virtual DbSet<Follower> Followers { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Saved> Saveds { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Blog;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.Property(e => e.PostId).HasColumnName("post_id");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Decription).HasColumnName("decription");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_commet_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_commet_user");
            });

            modelBuilder.Entity<DetailsSaved>(entity =>
            {
                entity.ToTable("details_saved");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.SavedId).HasColumnName("saved_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.DetailsSaveds)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_details_saved_Post");

                entity.HasOne(d => d.Saved)
                    .WithMany(p => p.DetailsSaveds)
                    .HasForeignKey(d => d.SavedId)
                    .HasConstraintName("FK_details_saved_saved");
            });

            modelBuilder.Entity<Follower>(entity =>
            {
                entity.ToTable("follower");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChildId)
                    .HasMaxLength(450)
                    .HasColumnName("child_id");

                entity.Property(e => e.FatherId)
                    .HasMaxLength(450)
                    .HasColumnName("father_id");

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.FollowerChildren)
                    .HasForeignKey(d => d.ChildId)
                    .HasConstraintName("FK_follower_user1");

                entity.HasOne(d => d.Father)
                    .WithMany(p => p.FollowerFathers)
                    .HasForeignKey(d => d.FatherId)
                    .HasConstraintName("FK_follower_user");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("like");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_like_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_like_user");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(250)
                    .HasColumnName("category_name");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Header)
                    .HasMaxLength(450)
                    .HasColumnName("header");

                entity.Property(e => e.Liks).HasColumnName("liks");

                entity.Property(e => e.ListId).HasColumnName("list_id");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.Property(e => e.Views).HasColumnName("views");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Post_category");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Post_user");
            });

            modelBuilder.Entity<Saved>(entity =>
            {
                entity.ToTable("saved");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Saveds)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_saved_user");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Followers).HasColumnName("followers");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(256)
                    .HasColumnName("user_email");

                entity.Property(e => e.UserName)
                    .HasMaxLength(450)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_AspNetUsers1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
