using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_patient
{
    public interface IConcierge
    {
        public void InsertConciegegeData(ConciegeViewModel model,string AspId);
        public void InsertConciegegeData(ConciegeViewModel model);
    }
}
