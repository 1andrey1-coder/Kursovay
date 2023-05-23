using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace schedule;

public partial class ScheduleDbContext : DbContext
{
    public ScheduleDbContext()
    {
    }

    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pair1> Pair1s { get; set; }

    public virtual DbSet<TblCourse> TblCourses { get; set; }

    public virtual DbSet<TblDay> TblDays { get; set; }

    public virtual DbSet<TblGroup> TblGroups { get; set; }

    public virtual DbSet<TblPair> TblPairs { get; set; }

    public virtual DbSet<TblPredmet> TblPredmets { get; set; }

    public virtual DbSet<TblReplacement> TblReplacements { get; set; }

    public virtual DbSet<TblScheduleDb> TblScheduleDbs { get; set; }

    public virtual DbSet<TblSemester> TblSemesters { get; set; }

    public virtual DbSet<TblSemestr> TblSemestrs { get; set; }

    public virtual DbSet<TblSpeciality> TblSpecialities { get; set; }

    public virtual DbSet<TblWeekday> TblWeekdays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("host=192.168.200.13;userid=student;password=student;database=schedule_db", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Pair1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pair1");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<TblCourse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_course");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<TblDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_day");

            entity.HasIndex(e => e.PairId, "FK_tbl_day_tbl_pairs_Id");

            entity.Property(e => e.DaySid).HasColumnName("DaySId");

            entity.HasOne(d => d.Pair).WithMany(p => p.TblDays)
                .HasForeignKey(d => d.PairId)
                .HasConstraintName("FK_tbl_day_tbl_pairs_Id");
        });

        modelBuilder.Entity<TblGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PRIMARY");

            entity.ToTable("tbl_group");

            entity.HasIndex(e => e.SemestrNuberId, "FK_tbl_group");

            entity.HasIndex(e => e.CourseId, "FK_tbl_group_tbl_course_id");

            entity.HasIndex(e => e.SemestrWeekId, "FK_tbl_group_tbl_semestr_id");

            entity.HasIndex(e => e.SpecialityId, "FK_tbl_group_tbl_speciality_specialityID");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.CourseId).HasColumnName("courseID");
            entity.Property(e => e.SemestrNuberId).HasColumnName("semestrNuberID");
            entity.Property(e => e.SemestrWeekId).HasColumnName("semestrWeekID");
            entity.Property(e => e.SpecialityId).HasColumnName("specialityID");

            entity.HasOne(d => d.Course).WithMany(p => p.TblGroups)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_tbl_group_tbl_course_id");

            entity.HasOne(d => d.SemestrNuber).WithMany(p => p.TblGroupSemestrNubers)
                .HasForeignKey(d => d.SemestrNuberId)
                .HasConstraintName("FK_tbl_group");

            entity.HasOne(d => d.SemestrWeek).WithMany(p => p.TblGroupSemestrWeeks)
                .HasForeignKey(d => d.SemestrWeekId)
                .HasConstraintName("FK_tbl_group_tbl_semestr_id");

            entity.HasOne(d => d.Speciality).WithMany(p => p.TblGroups).HasForeignKey(d => d.SpecialityId);
        });

        modelBuilder.Entity<TblPair>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_pairs");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblPredmet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_predmet");

            entity.HasIndex(e => e.Factionid, "FK_tbl_predmet_tbl_faction_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Factionid).HasColumnName("factionid");
            entity.Property(e => e.Predmet)
                .HasMaxLength(255)
                .HasColumnName("predmet");
        });

        modelBuilder.Entity<TblReplacement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_replacement");

            entity.HasIndex(e => e.WeekdaysId, "FK_tbl_replacement");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Predmet)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.WeekdaysId).HasColumnName("weekdaysId");

            entity.HasOne(d => d.Weekdays).WithMany(p => p.TblReplacements)
                .HasForeignKey(d => d.WeekdaysId)
                .HasConstraintName("FK_tbl_replacement");
        });

        modelBuilder.Entity<TblScheduleDb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_schedule_db");

            entity.HasIndex(e => e.Groupid, "FK_tbl_schedule_db_tbl_group_GroupID");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Day)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Predmet)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");

            entity.HasOne(d => d.Group).WithMany(p => p.TblScheduleDbs)
                .HasForeignKey(d => d.Groupid)
                .HasConstraintName("FK_tbl_schedule_db_tbl_group_GroupID");
        });

        modelBuilder.Entity<TblSemester>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_semester");

            entity.HasIndex(e => e.SpecialityId, "FK_tbl_predmetid_tbl_speciality_specialityID");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.SemesterNumber).HasColumnName("semesterNumber");
            entity.Property(e => e.SemesterWeek).HasColumnName("semesterWeek");
            entity.Property(e => e.SpecialityId).HasColumnName("specialityID");
        });

        modelBuilder.Entity<TblSemestr>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_semestr");

            entity.HasIndex(e => e.CourseId, "FK_tbl_semestr_tbl_course_id");

            entity.HasIndex(e => e.SemesterNuber, "FK_tbl_semestr_tbl_group_GroupID");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.SemesterNuber).HasColumnName("semesterNuber");
            entity.Property(e => e.SemesterWeek).HasColumnName("semesterWeek");

            entity.HasOne(d => d.Course).WithMany(p => p.TblSemestrs)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_tbl_semestr_tbl_course_id");
        });

        modelBuilder.Entity<TblSpeciality>(entity =>
        {
            entity.HasKey(e => e.SpecialityId).HasName("PRIMARY");

            entity.ToTable("tbl_speciality");

            entity.Property(e => e.SpecialityId).HasColumnName("specialityID");
            entity.Property(e => e.Speciality)
                .HasMaxLength(255)
                .HasColumnName("speciality");
        });

        modelBuilder.Entity<TblWeekday>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_weekdays");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Day).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
