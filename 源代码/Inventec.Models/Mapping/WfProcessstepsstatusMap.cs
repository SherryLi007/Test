using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfProcessstepsstatusMap : EntityTypeConfiguration<WfProcessstepsstatus>
    {
        public WfProcessstepsstatusMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.statuscn)
                .HasMaxLength(50);

            this.Property(t => t.statusen)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WF_PROCESSSTEPSSTATUS");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.status).HasColumnName("Status");
            this.Property(t => t.statuscn).HasColumnName("Statuscn");
            this.Property(t => t.statusen).HasColumnName("Statusen");
        }
    }
}
