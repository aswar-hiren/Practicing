using DataLayer.DataContext;
using DataLayer.Models;
using LogicLayer.Interface_patient;


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
