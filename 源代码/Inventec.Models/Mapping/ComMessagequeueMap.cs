using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ComMessagequeueMap : EntityTypeConfiguration<ComMessagequeue>
    {
        public ComMessagequeueMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.messageid)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.source)
                .HasMaxLength(50);

            this.Property(t => t.formid)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.fromuser)
                .HasMaxLength(50);

            this.Property(t => t.touser)
                .HasMaxLength(50);

            this.Property(t => t.subject)
                .HasMaxLength(200);

            this.Property(t => t.body)
                .HasMaxLength(4000);

            this.Property(t => t.bodytype)
                .HasMaxLength(50);

            this.Property(t => t.sendtype)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("COM_MESSAGEQUEUE");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.messageid).HasColumnName("MESSAGEID");
            this.Property(t => t.source).HasColumnName("SOURCE");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.fromuser).HasColumnName("FROMUSER");
            this.Property(t => t.touser).HasColumnName("TOUSER");
            this.Property(t => t.subject).HasColumnName("SUBJECT");
            this.Property(t => t.body).HasColumnName("BODY");
            this.Property(t => t.bodytype).HasColumnName("BODYTYPE");
            this.Property(t => t.sendtype).HasColumnName("SENDTYPE");
            this.Property(t => t.status).HasColumnName("STATUS");
            this.Property(t => t.retrytimes).HasColumnName("RETRYTIMES");
            this.Property(t => t.createdate).HasColumnName("CREATEDATE");
            this.Property(t => t.senddate).HasColumnName("SENDDATE");
            this.Property(t => t.isread).HasColumnName("ISREAD");
        }
    }
}
