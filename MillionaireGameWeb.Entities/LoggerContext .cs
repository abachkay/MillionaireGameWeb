using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MillionaireGameWeb.Entities
{
    public class LoggerContext : DbContext
    {
        public LoggerContext() : base("name=MillionaireGameWebDbConnectioString")
        {
        }        
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<UserAnswerLog> UserAnswerLogs { get; set; }
    }

}
