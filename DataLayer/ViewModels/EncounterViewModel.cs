using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class EncounterViewModel
    {  public int reqid {  get; set; }
        public string firstName {  get; set; }
        public string plan { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public DateTime? birthDate { get; set; }
        public DateTime? Date { get; set; }
        public string phoneNumber { get; set; }
        public string historyOfInjury { get; set; }

        public string  medicalHistory{ get; set; }
        public string medications {  get; set; }
        public string allergies { get; set; }

        public int temp {  get; set; }
        public int hr { get; set; }
        public int rr { get; set; }
        public int bps { get; set; }
        public int o2 { get; set; }
        public string pain {  get; set; }

        public int bpd { get; set; }

        public string hint { get; set; }
        public int  cv { get; set; }
        public int  chest { get; set; }
        public int abd {  get; set; }
        public int neuro { get; set; }
        public string other { get; set; }
        public string diagnosis {  get; set; }
        public string heent { get; set; }
     
        public string medicalDispensed { get; set; }
        public string procedure { get; set; }
        public string followUp { get; set; }

        public string skin { get; set; }
        public string  extr { get; set; }
    }
}
