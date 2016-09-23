using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfProcessfieldMap : EntityTypeConfiguration<WfProcessfield>
    {
        public WfProcessfieldMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.processname)
                .HasMaxLength(50);

            this.Property(t => t.fieldname)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WF_PROCESSFIELD");
            this.Property(t => t.id).HasColumnName("Id");
            this.Property(t => t.processid).HasColumnName("ProcessID");
            this.Property(t => t.processname).HasColumnName("ProcessName");
            this.Property(t => t.fieldname).HasColumnName("FieldName");
            this.Property(t => t.isqueryshow).HasColumnName("IsQueryShow");
        }
    }
}
