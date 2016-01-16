using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MicroM.Services
{
    public interface IAuditService
    {
        MicroManageContext Context { get; set; }
        Task AppendAuditEntryAsync(InventoryAudit auditEntry);
    }

    public class AuditService : IAuditService
    {
        public MicroManageContext Context { get; set; }

        public AuditService(MicroManageContext context) {
            this.Context = context;
        }

        public async Task AppendAuditEntryAsync(InventoryAudit auditEntry) {
            this.Context.InventoryAudits.Add(auditEntry);
            await this.Context.SaveChangesAsync();
        }
    }
}