using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ComLogMap : EntityTypeConfiguration<ComLog>
    {
        public ComLogMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.thread)
                .HasMaxLength(255);

            this.Property(t => t.loglevel)
                .HasMaxLength(50);

            this.Property(t => t.logger)
                .HasMaxLength(255);

            this.Property(t => t.message)
                .HasMaxLength(2000);

            this.Property(t => t.exception)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("COM_LOG");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.logdate).HasColumnName("LogDate");
            this.Property(t => t.thread).HasColumnName("Thread");
            this.Property(t => t.loglevel).HasColumnName("LogLevel");
            this.Property(t => t.logger).HasColumnName("Logger");
            this.Property(t => t.message).HasColumnName("Message");
            this.Property(t => t.exception).HasColumnName("Exception");
        }
    }
}
