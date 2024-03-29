using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RegisterCreditsManageApp.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<CourseYear> CourseYears { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<RegisterCredit> RegisterCredits { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = DESKTOP-KQN4I3C; UID = sa; Password = 270603; Database = RegisterCreditsManageApp; TrustServerCertificate = true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.IdClass);

            entity.ToTable("Class");

            entity.Property(e => e.ClassName).HasMaxLength(100);
        });

        modelBuilder.Entity<CourseYear>(entity =>
        {
            entity.HasKey(e => e.IdCourseYear);

            entity.ToTable("CourseYear");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.IdMajors);

            entity.Property(e => e.MajorsName).HasMaxLength(100);
        });

        modelBuilder.Entity<RegisterCredit>(entity =>
        {
            entity.HasKey(e => e.IdRegisterCredits);

            entity.Property(e => e.ClassName).HasMaxLength(100);
            entity.Property(e => e.Schedule).HasMaxLength(255);

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.RegisterCredits)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisterCredits_Teacher");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.IdSemester);

            entity.ToTable("Semester");

            entity.Property(e => e.SemesterName).HasMaxLength(50);

            entity.HasMany(d => d.IdMajors).WithMany(p => p.IdSemesters)
                .UsingEntity<Dictionary<string, object>>(
                    "SemesterOfMajor",
                    r => r.HasOne<Major>().WithMany()
                        .HasForeignKey("IdMajors")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SemesterOfMajors_Majors"),
                    l => l.HasOne<Semester>().WithMany()
                        .HasForeignKey("IdSemester")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SemesterOfMajors_Semester"),
                    j =>
                    {
                        j.HasKey("IdSemester", "IdMajors");
                        j.ToTable("SemesterOfMajors");
                    });
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsFixedLength();

            entity.HasOne(d => d.IdClassNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Class");

            entity.HasOne(d => d.IdCourseYearNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdCourseYear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_CourseYear");

            entity.HasOne(d => d.IdMajorsNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdMajors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Majors");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.IdSubject);

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectName).HasMaxLength(100);

            entity.HasOne(d => d.IdRegisterCreditsNavigation).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.IdRegisterCredits)
                .HasConstraintName("FK_Subject_RegisterCredits");

            entity.HasOne(d => d.IdSemesterNavigation).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.IdSemester)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subject_Semester");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.IdTeacher);

            entity.ToTable("Teacher");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
