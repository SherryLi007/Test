using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfApprovalhistoryMap : EntityTypeConfiguration<WfApprovalhistory>
    {
        public WfApprovalhistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.processname)
                .HasMaxLength(100);

            this.Property(t => t.stepname)
                .HasMaxLength(100);

            this.Property(t => t.displaystepname)
                .HasMaxLength(100);

            this.Property(t => t.approvername)
                .HasMaxLength(150);

            this.Property(t => t.approveraccount)
                .HasMaxLength(50);

            this.Property(t => t.action)
                .HasMaxLength(50);

            this.Property(t => t.comments)
                .HasMaxLength(2000);

            this.Property(t => t.taskid)
                .HasMaxLength(50);

            this.Property(t => t.taskuser)
                .HasMaxLength(50);

            this.Property(t => t.assigendtouser)
                .HasMaxLength(50);

            this.Property(t => t.childprocessname)
                .HasMaxLength(100);

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
            this.ToTable("WF_APPROVALHISTORY");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.processname).HasColumnName("PROCESSNAME");
            this.Property(t => t.incident).HasColumnName("INCIDENT");
            this.Property(t => t.stepname).HasColumnName("STEPNAME");
            this.Property(t => t.displaystepname).HasColumnName("DISPLAYSTEPNAME");
            this.Property(t => t.approvername).HasColumnName("APPROVERNAME");
            this.Property(t => t.approveraccount).HasColumnName("APPROVERACCOUNT");
            this.Property(t => t.action).HasColumnName("ACTION");
            this.Property(t => t.comments).HasColumnName("COMMENTS");
            this.Property(t => t.createdate).HasColumnName("CREATEDATE");
            this.Property(t => t.enddate).HasColumnName("ENDDATE");
            this.Property(t => t.taskid).HasColumnName("TASKID");
            this.Property(t => t.taskuser).HasColumnName("TASKUSER");
            this.Property(t => t.assigendtouser).HasColumnName("ASSIGENDTOUSER");
            this.Property(t => t.status).HasColumnName("STATUS");
            this.Property(t => t.childprocessname).HasColumnName("CHILDPROCESSNAME");
            this.Property(t => t.childincident).HasColumnName("CHILDINCIDENT");
            this.Property(t => t.ext01).HasColumnName("EXT01");
            this.Property(t => t.ext02).HasColumnName("EXT02");
            this.Property(t => t.ext03).HasColumnName("EXT03");
            this.Property(t => t.ext04).HasColumnName("EXT04");
            this.Property(t => t.ext05).HasColumnName("EXT05");
        }
    }
}
