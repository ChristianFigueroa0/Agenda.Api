using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Api.Data
{
    public class Context : DbContext
    {
        public DbSet<Models.Agenda> Agendas { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
    }
}
