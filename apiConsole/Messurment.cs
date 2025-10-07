using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiConsole
{
	public class Messurment
	{
		public double Time { get; set; }
		public double Speed { get; set; }

		public Messurment(double time, double speed) 
		{
			Time = time;
			Speed = speed;
		}

		public override string ToString()
		{
			return $"{Time} s - {Speed} m/s";
		}

		public string ToFileString()
		{
			return $"{Time};{Speed}";
		}
	}
}
