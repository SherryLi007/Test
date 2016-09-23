using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Inventec.Models.Mapping;

namespace Inventec.Models
{
    public partial class InventecbizContext : DbContext
    {
        static InventecbizContext()
        {
            Database.SetInitializer<InventecbizContext>(null);
        }

        public InventecbizContext()
            : base("Name=InventecbizContext")
        {
        }

        public DbSet<BdLog> BdLog { get; set; }
        public DbSet<BdMasterdata> BdMasterdata { get; set; }
        public DbSet<ComLog> ComLog { get; set; }
        public DbSet<ComMessagequeue> ComMessagequeue { get; set; }
        public DbSet<ComSerialno> ComSerialno { get; set; }
        public DbSet<OrgDepartment> OrgDepartment { get; set; }
        public DbSet<OrgGroup> OrgGroup { get; set; }
        public DbSet<OrgGroupmember> OrgGroupmember { get; set; }
        public DbSet<OrgJob> OrgJob { get; set; }
        public DbSet<OrgJobgrade> OrgJobgrade { get; set; }
        public DbSet<OrgJobgroup> OrgJobgroup { get; set; }
        public DbSet<OrgOrganization> OrgOrganization { get; set; }
        public DbSet<OrgUser> OrgUser { get; set; }
        public DbSet<ProcInventecFeederlist> ProcInventecFeederlist { get; set; }
        public DbSet<ProcInventecFeederlistDt> ProcInventecFeederlistDt { get; set; }
        public DbSet<ProcInventecMfg> ProcInventecMfg { get; set; }
        public DbSet<ProcInventecMfgDt> ProcInventecMfgDt { get; set; }
        public DbSet<ProcInventecPcba> ProcInventecPcba { get; set; }
        public DbSet<ProcInventecPcbaDt> ProcInventecPcbaDt { get; set; }
        public DbSet<ProcInventecSmt> ProcInventecSmt { get; set; }
        public DbSet<ProcInventecSmtDt1> ProcInventecSmtDt1 { get; set; }
        public DbSet<ProcInventecSmtDt2> ProcInventecSmtDt2 { get; set; }
        public DbSet<ProcInventecSocket> ProcInventecSocket { get; set; }
        public DbSet<ProcInventecSocketDt> ProcInventecSocketDt { get; set; }
        public DbSet<SecMenu> SecMenu { get; set; }
        public DbSet<SecMenurights> SecMenurights { get; set; }
        public DbSet<SecMenurightsmember> SecMenurightsmember { get; set; }
        public DbSet<SecMenurightsobject> SecMenurightsobject { get; set; }
        public DbSet<WfAgent> WfAgent { get; set; }
        public DbSet<WfAgentProcess> WfAgentProcess { get; set; }
        public DbSet<WfApprovalhistory> WfApprovalhistory { get; set; }
        public DbSet<WfAttachment> WfAttachment { get; set; }
        public DbSet<WfDraft> WfDraft { get; set; }
        public DbSet<WfForm> WfForm { get; set; }
        public DbSet<WfProcess> WfProcess { get; set; }
        public DbSet<WfProcesscategory> WfProcesscategory { get; set; }
        public DbSet<WfProcessfield> WfProcessfield { get; set; }
        public DbSet<WfProcessstep> WfProcessstep { get; set; }
        public DbSet<WfProcessstepsstatus> WfProcessstepsstatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BdLogMap());
            modelBuilder.Configurations.Add(new BdMasterdataMap());
            modelBuilder.Configurations.Add(new ComLogMap());
            modelBuilder.Configurations.Add(new ComMessagequeueMap());
            modelBuilder.Configurations.Add(new ComSerialnoMap());
            modelBuilder.Configurations.Add(new OrgDepartmentMap());
            modelBuilder.Configurations.Add(new OrgGroupMap());
            modelBuilder.Configurations.Add(new OrgGroupmemberMap());
            modelBuilder.Configurations.Add(new OrgJobMap());
            modelBuilder.Configurations.Add(new OrgJobgradeMap());
            modelBuilder.Configurations.Add(new OrgJobgroupMap());
            modelBuilder.Configurations.Add(new OrgOrganizationMap());
            modelBuilder.Configurations.Add(new OrgUserMap());
            modelBuilder.Configurations.Add(new ProcInventecFeederlistMap());
            modelBuilder.Configurations.Add(new ProcInventecFeederlistDtMap());
            modelBuilder.Configurations.Add(new ProcInventecMfgMap());
            modelBuilder.Configurations.Add(new ProcInventecMfgDtMap());
            modelBuilder.Configurations.Add(new ProcInventecPcbaMap());
            modelBuilder.Configurations.Add(new ProcInventecPcbaDtMap());
            modelBuilder.Configurations.Add(new ProcInventecSmtMap());
            modelBuilder.Configurations.Add(new ProcInventecSmtDt1Map());
            modelBuilder.Configurations.Add(new ProcInventecSmtDt2Map());
            modelBuilder.Configurations.Add(new ProcInventecSocketMap());
            modelBuilder.Configurations.Add(new ProcInventecSocketDtMap());
            modelBuilder.Configurations.Add(new SecMenuMap());
            modelBuilder.Configurations.Add(new SecMenurightsMap());
            modelBuilder.Configurations.Add(new SecMenurightsmemberMap());
            modelBuilder.Configurations.Add(new SecMenurightsobjectMap());
            modelBuilder.Configurations.Add(new WfAgentMap());
            modelBuilder.Configurations.Add(new WfAgentProcessMap());
            modelBuilder.Configurations.Add(new WfApprovalhistoryMap());
            modelBuilder.Configurations.Add(new WfAttachmentMap());
            modelBuilder.Configurations.Add(new WfDraftMap());
            modelBuilder.Configurations.Add(new WfFormMap());
            modelBuilder.Configurations.Add(new WfProcessMap());
            modelBuilder.Configurations.Add(new WfProcesscategoryMap());
            modelBuilder.Configurations.Add(new WfProcessfieldMap());
            modelBuilder.Configurations.Add(new WfProcessstepMap());
            modelBuilder.Configurations.Add(new WfProcessstepsstatusMap());
        }
    }
}

