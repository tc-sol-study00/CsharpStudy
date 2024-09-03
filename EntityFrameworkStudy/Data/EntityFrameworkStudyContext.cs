using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkStudy.Models;
using System.Reflection.Metadata;

namespace EntityFrameworkStudy.Data
{
    public class EntityFrameworkStudyContext : DbContext 
    {
        public EntityFrameworkStudyContext (DbContextOptions<EntityFrameworkStudyContext> options)
            : base(options)
        {
        }

        public DbSet<EntityFrameworkStudy.Models.Education> Education { get; set; } = default!;
        public DbSet<EntityFrameworkStudy.Models.ClassAttr> ClassAttr { get; set; } = default!;
    }
}
