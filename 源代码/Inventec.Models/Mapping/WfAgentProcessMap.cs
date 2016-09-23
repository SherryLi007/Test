using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfAgentProcessMap : EntityTypeConfiguration<WfAgentProcess>
    {
        public WfAgentProcessMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("WF_AGENT_PROCESS");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.agentid).HasColumnName("AgentID");
            this.Property(t => t.processid).HasColumnName("ProcessID");
            this.Property(t => t.canapply).HasColumnName("CanApply");
            this.Property(t => t.canapprove).HasColumnName("CanApprove");
            this.Property(t => t.approveamount).HasColumnName("ApproveAmount");
        }
    }
}
