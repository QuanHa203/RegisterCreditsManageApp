using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RegisterCreditsManageApp.Models;

public partial class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private static AppDbContext context = null;
    public static AppDbContext _Context
    {
        get
        {
            if (context == null)
            {
                context = new AppDbContext();
            }

            return context;
        }
    }
    private AppDbContext()
    {
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.AddJsonFile($"{Directory.GetCurrentDirectory()}/appsettings.json");
        _configuration = builder.Build();
    }

    private AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = _configuration.GetConnectionString("ServerDb_01");
        optionsBuilder.UseSqlServer(connectionString);
    }

    public virtual DbSet<ClassRoom> ClassRooms { get; set; }

    public virtual DbSet<MainClass> MainClasses { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<RegisterCredit> RegisterCredits { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassRoom>(entity =>
        {
            entity.HasKey(e => e.IdClassRoom);

            entity.ToTable("ClassRoom");

            entity.Property(e => e.IdClassRoom)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Schedule).HasMaxLength(255);
        });

        modelBuilder.Entity<MainClass>(entity =>
        {
            entity.HasKey(e => e.IdMainClass).HasName("PK_Class");

            entity.ToTable("MainClass");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.IdCurrentRegisterSemesterNavigation).WithMany(p => p.MainClasses)
                .HasForeignKey(d => d.IdCurrentRegisterSemester)
                .HasConstraintName("FK_MainClass_Semester");

            entity.HasOne(d => d.IdMajorsNavigation).WithMany(p => p.MainClasses)
                .HasForeignKey(d => d.IdMajors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassName_Majors");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.IdMajors);

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<RegisterCredit>(entity =>
        {
            entity.HasKey(e => e.IdRegisterCredits);

            entity.Property(e => e.IdClassRoom)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdClassRoomNavigation).WithMany(p => p.RegisterCredits)
                .HasForeignKey(d => d.IdClassRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisterCredits_ClassRoom");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.ToTable("Role");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.IdSemester);

            entity.ToTable("Semester");

            entity.Property(e => e.Name).HasMaxLength(50);
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

            entity.HasOne(d => d.IdMainClassNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdMainClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_ClassName");

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

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.IdMajorsNavigation).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.IdMajors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subject_Majors");

            entity.HasOne(d => d.IdSemesterNavigation).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.IdSemester)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subject_Semester");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.IdTeacher);

            entity.ToTable("Teacher");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.Property(e => e.IdUser).ValueGeneratedNever();
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
