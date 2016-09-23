using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfFormMap : EntityTypeConfiguration<WfForm>
    {
        public WfFormMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.processname)
                .HasMaxLength(50);

            this.Property(t => t.formno)
                .HasMaxLength(50);

            this.Property(t => t.summary)
                .HasMaxLength(4000);

            this.Property(t => t.companycode)
                .HasMaxLength(50);

            this.Property(t => t.dept)
                .HasMaxLength(50);

            this.Property(t => t.costcenter)
                .HasMaxLength(50);

            this.Property(t => t.initiator)
                .HasMaxLength(50);

            this.Property(t => t.agent)
                .HasMaxLength(50);

            this.Property(t => t.approver)
                .HasMaxLength(50);

            this.Property(t => t.voucherno)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WF_FORM");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.processname).HasColumnName("PROCESSNAME");
            this.Property(t => t.formno).HasColumnName("FORMNO");
            this.Property(t => t.formamount).HasColumnName("FORMAMOUNT");
            this.Property(t => t.incident).HasColumnName("INCIDENT");
            this.Property(t => t.priority).HasColumnName("PRIORITY");
            this.Property(t => t.summary).HasColumnName("SUMMARY");
            this.Property(t => t.starttime).HasColumnName("STARTTIME");
            this.Property(t => t.endtime).HasColumnName("ENDTIME");
            this.Property(t => t.companycode).HasColumnName("COMPANYCODE");
            this.Property(t => t.dept).HasColumnName("DEPT");
            this.Property(t => t.costcenter).HasColumnName("COSTCENTER");
            this.Property(t => t.status).HasColumnName("STATUS");
            this.Property(t => t.initiator).HasColumnName("INITIATOR");
            this.Property(t => t.agent).HasColumnName("AGENT");
            this.Property(t => t.approver).HasColumnName("APPROVER");
            this.Property(t => t.isurgent).HasColumnName("ISURGENT");
            this.Property(t => t.paymentstatus).HasColumnName("PAYMENTSTATUS");
            this.Property(t => t.invoicedate).HasColumnName("InvoiceDate");
            this.Property(t => t.paymentdate).HasColumnName("PaymentDate");
            this.Property(t => t.voucherno).HasColumnName("VoucherNo");
            this.Property(t => t.isdownload).HasColumnName("IsDownload");
        }
    }
}
