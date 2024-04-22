using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
	public class SearchRecordvm
	{
		public string PatientName { get; set; }

		public string RequestorName { get; set; }

		public string Email { get; set; }

		public string Phonenumber { get; set; }

		public string Address { get; set; }

		public string Zip { get; set; }

		public string RequestStatus { get; set; }

		public string PhysicianName { get; set; }

		public string PhysicianNotes { get; set; }


		public string AdminNotes { get; set; }

		public string PatientNotes { get; set; }

		public string DateofService { get; set; }

		public DateOnly fromDateofService { get; set; }

		public DateOnly toDateofService { get; set; }

		public int Requesttype { get; set; }

		public int requestid { get; set; }

		public string Providername { get; set; }
		public int CurrentPage { get; set; }

		public double TotalPages { get; set; }
		public int PageSize { get; set; }
		public int total { get; set; }
	}
}
