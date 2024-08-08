using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Models;

namespace Persistence.AppContext
{
    public partial class ApplicationDBContext : DbContext
    {
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<AuditLogin> AuditLogins { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            // Get the modified entries
            var modifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added).ToList();


            var entries = modifiedEntries.FirstOrDefault();

            var tablename = "";

            var fieldvalue = "";

            if (entries != null)
            {
                var values = new List<string>();
                var entry = entries.Entity.GetType();

                tablename = entry.Name;
                foreach (var item in entry.GetProperties())
                {
                    values.Add(item.Name +"="+item.GetValue(entries.Entity));
                }

                fieldvalue = string.Join("||", values);
            }


            foreach (var entry in modifiedEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                    entry.Property("CreateId").CurrentValue = 1;
                    this.SaveToAudit(tablename, fieldvalue, "Added");
                }
                else if(entry.State == EntityState.Modified)
                {
                    entry.Property("LastUpdateDate").CurrentValue = DateTime.Now;
                    entry.Property("LastUpdateId").CurrentValue = 2;
                    this.SaveToAudit(tablename, fieldvalue, "Updated");
                }
                else if(entry.State == EntityState.Deleted)
                {
                    this.SaveToAudit(tablename, fieldvalue, "Deleted");
                }

                
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SaveToAudit(string TableName, string Value, string action)
        {
            this.Set<Audit>().Add(new Audit
            {
                InsertBy = 1,
                TransactionType = action,
                Table = TableName,
                NewValue = Value,
                DateModify = DateTime.Now,
            });
        }

    }
}
