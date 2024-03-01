using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsList.MVVM.MODEL
{
    public class Place
    {
		private Microsoft.Maui.Devices.Sensors.Location _location;

		public Microsoft.Maui.Devices.Sensors.Location Location
		{
			get { return _location; }
			set { _location = value; }
		}

		private string _address;

		public string Address
		{
			get { return _address; }
			set { _address = value; }
		}

		private string _description;

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}


	}
}
