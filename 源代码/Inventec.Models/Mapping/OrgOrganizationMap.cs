using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class OrgOrganizationMap : EntityTypeConfiguration<OrgOrganization>
    {
        public OrgOrganizationMap()
        {
            // Primary Key
            this.HasKey(t => t.organization);

            // Properties
            this.Property(t => t.organization)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.companycode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.companyname)
                .HasMaxLength(50);

            this.Property(t => t.officialnameen)
                .HasMaxLength(200);

            this.Property(t => t.officialnamecn)
                .HasMaxLength(200);

            this.Property(t => t.legaladdressen)
                .HasMaxLength(500);

            this.Property(t => t.legaladdresscn)
                .HasMaxLength(500);

            this.Property(t => t.company_tax_code)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("ORG_ORGANIZATION");
            this.Property(t => t.organization).HasColumnName("Organization");
            this.Property(t => t.companycode).HasColumnName("CompanyCode");
            this.Property(t => t.companyname).HasColumnName("CompanyName");
            this.Property(t => t.officialnameen).HasColumnName("OfficialNameEN");
            this.Property(t => t.officialnamecn).HasColumnName("OfficialNameCN");
            this.Property(t => t.legaladdressen).HasColumnName("LegalAddressEN");
            this.Property(t => t.legaladdresscn).HasColumnName("LegalAddressCN");
            this.Property(t => t.currencyid).HasColumnName("CurrencyID");
            this.Property(t => t.effectfrom).HasColumnName("EFFECTFROM");
            this.Property(t => t.effectto).HasColumnName("EFFECTTO");
            this.Property(t => t.orderno).HasColumnName("ORDERNO");
            this.Property(t => t.company_tax_code).HasColumnName("company_tax_code");
            this.Property(t => t.is_valid).HasColumnName("is_valid");
            this.Property(t => t.last_modified).HasColumnName("last_modified");
            this.Property(t => t.isactive).HasColumnName("ISACTIVE");
        }
    }
}
