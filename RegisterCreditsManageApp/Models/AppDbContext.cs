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

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server = DINHQUAN1243\\SQLEXPRESS; UID = sa; Password = 87895424; Database = RegisterCreditsManageApp; TrustServerCertificate = true;");

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

            entity.Property(e => e.ClassRoom).HasMaxLength(100);
            entity.Property(e => e.Schedule).HasMaxLength(255);
            entity.Property(e => e.StartRegisterDate)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.RegisterCredits)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisterCredits_Teacher");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsFixedLength();
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
            entity.HasKey(e => e.IdStudent);

            entity.ToTable("Student");

            entity.Property(e => e.IdStudent).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
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

            entity.HasOne(d => d.IdStudentNavigation).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_User");
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
