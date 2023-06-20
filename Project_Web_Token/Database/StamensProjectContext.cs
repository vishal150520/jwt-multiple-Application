using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project_Web_Token.Database;

public partial class StamensProjectContext : DbContext
{
    public StamensProjectContext()
    {
    }

    public StamensProjectContext(DbContextOptions<StamensProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbEmployee> TbEmployees { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=STAMENS-PC;Initial Catalog=stamensProject;Persist Security Info=False;Integrated Security=True;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbEmployee>(entity =>
        {
            entity.HasKey(e => e.Eid).HasName("PK__tb_Emplo__C1971B53DDF5F8CF");

            entity.ToTable("tb_Employee");

            entity.Property(e => e.EmailId).HasMaxLength(100);
            entity.Property(e => e.Ename).HasMaxLength(100);
            entity.Property(e => e.Passwords).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
