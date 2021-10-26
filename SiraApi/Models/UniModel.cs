namespace SiraApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UniModel : DbContext
    {
        public UniModel()
            : base("name=UniDatabase")
        {
        }

        public virtual DbSet<CAMPUS> CAMPUS { get; set; }
        public virtual DbSet<CAREER> CAREER { get; set; }
        public virtual DbSet<COURSE> COURSE { get; set; }
        public virtual DbSet<FACULTY> FACULTY { get; set; }
        public virtual DbSet<ROLE> ROLE { get; set; }
        public virtual DbSet<STUDENT> STUDENT { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<STUDENT_COURSE> STUDENT_COURSE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CAMPUS>()
                .HasMany(e => e.CAREER)
                .WithOptional(e => e.CAMPUS1)
                .HasForeignKey(e => e.CAMPUS);

            modelBuilder.Entity<CAREER>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<CAREER>()
                .HasMany(e => e.COURSE)
                .WithRequired(e => e.CAREER)
                .HasForeignKey(e => e.ID_CAREER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAREER>()
                .HasMany(e => e.STUDENT)
                .WithRequired(e => e.CAREER1)
                .HasForeignKey(e => e.CAREER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<COURSE>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<COURSE>()
                .Property(e => e.ID_CAREER)
                .IsUnicode(false);

            modelBuilder.Entity<COURSE>()
                .HasMany(e => e.STUDENT_COURSE)
                .WithRequired(e => e.COURSE)
                .HasForeignKey(e => e.ID_COURSE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FACULTY>()
                .HasMany(e => e.CAREER)
                .WithOptional(e => e.FACULTY1)
                .HasForeignKey(e => e.FACULTY);

            modelBuilder.Entity<ROLE>()
                .HasMany(e => e.USERS)
                .WithOptional(e => e.ROLE1)
                .HasForeignKey(e => e.ROLE);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.CARNET)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .Property(e => e.CAREER)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT>()
                .HasMany(e => e.STUDENT_COURSE)
                .WithRequired(e => e.STUDENT)
                .HasForeignKey(e => e.ID_STUDENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<STUDENT_COURSE>()
                .Property(e => e.ID_STUDENT)
                .IsUnicode(false);

            modelBuilder.Entity<STUDENT_COURSE>()
                .Property(e => e.ID_COURSE)
                .IsUnicode(false);
        }
    }
}
