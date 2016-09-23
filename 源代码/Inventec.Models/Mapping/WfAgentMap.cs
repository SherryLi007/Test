using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfAgentMap : EntityTypeConfiguration<WfAgent>
    {
        public WfAgentMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("WF_AGENT");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.userid).HasColumnName("UserId");
            this.Property(t => t.agentuserid).HasColumnName("AgentUserId");
            this.Property(t => t.agenttype).HasColumnName("AgentType");
            this.Property(t => t.startdate).HasColumnName("StartDate");
            this.Property(t => t.enddate).HasColumnName("EndDate");
            this.Property(t => t.createby).HasColumnName("CreateBy");
            this.Property(t => t.createdate).HasColumnName("CreateDate");
        }
    }
}
