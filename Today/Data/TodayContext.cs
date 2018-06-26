using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodayList.Models;

namespace TodayList.Models
{
    public class TodayContext : DbContext
    {
        public TodayContext (DbContextOptions<TodayContext> options)
            : base(options)
        {
        }

        public DbSet<TodayList.Models.Today> Today { get; set; }
    }
}
