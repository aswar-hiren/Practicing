using DataLayer.DataContext;
using DataLayer.Models;
using LogicLayer.Interface_patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LogicLayer.Repositary_patient
{
    public class ReqWiseFileClass : IReqWiseFiles
    {
        private readonly HellodocPrjContext _context;
        public ReqWiseFileClass(HellodocPrjContext context)
        {
                _context = context;
        }
        public List<Requestwisefile> getReqwisefile(int requestid)
        {
            return _context.Requestwisefiles.Where(u => u.Requestid == requestid).ToList();
        }
    }
}
