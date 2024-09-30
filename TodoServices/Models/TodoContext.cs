using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoServices.Models;

public partial class TodoContext : DbContext
{
    public TodoContext()
    {
    }

    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<LabelList> LabelLists { get; set; }

    public virtual DbSet<Subtask> Subtasks { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskLabel> TaskLabels { get; set; }

    public virtual DbSet<UserList> UserLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=TODO;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__Attachme__442C64DE19CCC4D9");

            entity.ToTable("Attachment", "todoschema");

            entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");
            entity.Property(e => e.AttachmentLink)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TaskId).HasColumnName("TaskID");

            entity.HasOne(d => d.Task).WithMany(p => p.Attachments).HasForeignKey(d => d.TaskId);
        });

        modelBuilder.Entity<LabelList>(entity =>
        {
            entity.HasKey(e => e.LabelId).HasName("PK__LabelLis__397E2BA3E93931E8");

            entity.ToTable("LabelList", "todoschema");

            entity.Property(e => e.LabelId).HasColumnName("LabelID");
            entity.Property(e => e.LabelColor)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LabelName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subtask>(entity =>
        {
            entity.HasKey(e => e.SubTaskId).HasName("PK_SubTaskID");

            entity.ToTable("Subtasks", "todoschema");

            entity.Property(e => e.SubTaskId).HasColumnName("SubTaskID");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Task).WithMany(p => p.Subtasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TaskTable_Tasks_TaskID");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949D1D9CCE41E");

            entity.ToTable("Tasks", "todoschema");

            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.AssignedUserId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("AssignedUserID");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.AssignedUser).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.AssignedUserId)
                .HasConstraintName("FK_Tasks_UserList_UserID");
        });

        modelBuilder.Entity<TaskLabel>(entity =>
        {
            entity.HasKey(e => e.TaskLabelIndex).HasName("PK__TaskLabe__E257CD0E59F4DA9C");

            entity.ToTable("TaskLabels", "todoschema");

            entity.HasIndex(e => new { e.TaskId, e.LabelId }, "DiffLabelsforTask").IsUnique();

            entity.Property(e => e.LabelId).HasColumnName("LabelID");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");

            entity.HasOne(d => d.Label).WithMany(p => p.TaskLabels)
                .HasForeignKey(d => d.LabelId)
                .HasConstraintName("FK_LabelListTaskLabel");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskLabels)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TasksandTaskLabel");
        });

        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserList__1788CCACB50BC84D");

            entity.ToTable("UserList", "todoschema");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
