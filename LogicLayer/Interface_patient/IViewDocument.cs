using DataLayer.Models;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_patient
{
    public interface IViewDocument
    {
        public List<Requestwisefile> ShowDocument(viewdocumentmodel model,int reqid,Request requestdata);
        public Request Getrequest(int reqid);

        public User Getuser(int? userid );
        public Aspnetuser Getaspuser(String userid);
    }
}
