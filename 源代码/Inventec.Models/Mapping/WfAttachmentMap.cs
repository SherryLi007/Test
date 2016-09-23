using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfAttachmentMap : EntityTypeConfiguration<WfAttachment>
    {
        public WfAttachmentMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.formid)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.processname)
                .HasMaxLength(100);

            this.Property(t => t.stepname)
                .HasMaxLength(100);

            this.Property(t => t.filename)
                .HasMaxLength(100);

            this.Property(t => t.newname)
                .HasMaxLength(100);

            this.Property(t => t.filetype)
                .HasMaxLength(50);

            this.Property(t => t.status)
                .HasMaxLength(50);

            this.Property(t => t.taskid)
                .HasMaxLength(50);

            this.Property(t => t.type)
                .HasMaxLength(50);

            this.Property(t => t.comments)
                .HasMaxLength(50);

            this.Property(t => t.createby)
                .HasMaxLength(50);

            this.Property(t => t.ext01)
                .HasMaxLength(50);

            this.Property(t => t.ext02)
                .HasMaxLength(50);

            this.Property(t => t.ext03)
                .HasMaxLength(50);

            this.Property(t => t.ext04)
                .HasMaxLength(50);

            this.Property(t => t.ext05)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WF_ATTACHMENT");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.processname).HasColumnName("PROCESSNAME");
            this.Property(t => t.incident).HasColumnName("INCIDENT");
            this.Property(t => t.stepname).HasColumnName("STEPNAME");
            this.Property(t => t.filename).HasColumnName("FILENAME");
            this.Property(t => t.newname).HasColumnName("NEWNAME");
            this.Property(t => t.filetype).HasColumnName("FILETYPE");
            this.Property(t => t.filesize).HasColumnName("FILESIZE");
            this.Property(t => t.status).HasColumnName("STATUS");
            this.Property(t => t.taskid).HasColumnName("TASKID");
            this.Property(t => t.type).HasColumnName("TYPE");
            this.Property(t => t.comments).HasColumnName("COMMENTS");
            this.Property(t => t.createdate).HasColumnName("CREATEDATE");
            this.Property(t => t.createby).HasColumnName("CREATEBY");
            this.Property(t => t.ext01).HasColumnName("EXT01");
            this.Property(t => t.ext02).HasColumnName("EXT02");
            this.Property(t => t.ext03).HasColumnName("EXT03");
            this.Property(t => t.ext04).HasColumnName("EXT04");
            this.Property(t => t.ext05).HasColumnName("EXT05");
        }
    }
}
