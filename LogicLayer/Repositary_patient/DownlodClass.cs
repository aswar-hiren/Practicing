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
    public class DownlodClass : IDownlod
    {
        private readonly HellodocPrjContext _context;
        public DownlodClass(HellodocPrjContext context)
        {
                _context = context;
        }
        public Requestwisefile GetFile(int documentid)
        {
            return _context.Requestwisefiles.FirstOrDefault(u => u.Requestwisefileid == documentid);
        }
    }
}
