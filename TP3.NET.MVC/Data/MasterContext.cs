﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TP3.NET.MVC.Models;

namespace TP3.NET.MVC.Data
{
    public partial class MasterContext : DbContext
    {
        public MasterContext()
        {
        }

        public MasterContext(DbContextOptions<MasterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aluno> Alunos { get; set; } = null!;
        public virtual DbSet<Curso> Cursos { get; set; } = null!;
        public virtual DbSet<Disciplina> Disciplinas { get; set; } = null!;
        public virtual DbSet<Professor> Professors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Master;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.ToTable("Aluno");

                entity.HasIndex(e => e.Email, "UQ__Aluno__A9D10534D909EC1D")
                    .IsUnique();

                entity.Property(e => e.AlunoId).HasColumnName("AlunoID");

                entity.Property(e => e.DataNascimento).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Nome).HasMaxLength(255);
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.ToTable("Curso");

                entity.Property(e => e.CursoId).HasColumnName("CursoID");

                entity.Property(e => e.AlunoId).HasColumnName("AlunoID");

                entity.Property(e => e.DataFim).HasColumnType("date");

                entity.Property(e => e.DataInicio).HasColumnType("date");

                entity.Property(e => e.DisciplinaId).HasColumnName("DisciplinaID");

                entity.Property(e => e.Nome).HasMaxLength(255);

                entity.HasOne(d => d.Aluno)
                    .WithMany(p => p.Cursos)
                    .HasForeignKey(d => d.AlunoId)
                    .HasConstraintName("FK__Curso__AlunoID__1C1D2798");

                entity.HasOne(d => d.Disciplina)
                    .WithMany(p => p.Cursos)
                    .HasForeignKey(d => d.DisciplinaId)
                    .HasConstraintName("FK__Curso__Disciplin__1D114BD1");
            });

            modelBuilder.Entity<Disciplina>(entity =>
            {
                entity.ToTable("Disciplina");

                entity.Property(e => e.DisciplinaId).HasColumnName("DisciplinaID");

                entity.Property(e => e.Nome).HasMaxLength(255);

                entity.Property(e => e.ProfessorId).HasColumnName("ProfessorID");

                entity.HasOne(d => d.Professor)
                    .WithMany(p => p.Disciplinas)
                    .HasForeignKey(d => d.ProfessorId)
                    .HasConstraintName("FK__Disciplin__Profe__1940BAED");
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.ToTable("Professor");

                entity.HasIndex(e => e.Email, "UQ__Professo__A9D1053464587175")
                    .IsUnique();

                entity.Property(e => e.ProfessorId).HasColumnName("ProfessorID");

                entity.Property(e => e.DataContratacao).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Nome).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
