using EntityFrameworkStudy.Data;
using EntityFrameworkStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkStudy {
    public  class DiTest {

        private readonly EntityFrameworkStudyContext _context;
        public DiTest(EntityFrameworkStudyContext context) {
            _context = context;
        }
        public void Ensyu() {
            var a=_context.Education;
        }
    }
}
