using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
	public static class LastResponseFormat
	{
		public static string DisplayFormat(this DateTime dateTime)
        {
            var then = dateTime;
			var now = DateTime.Now;
			var days = (int)(now - then).TotalDays;
			var hours = (int)(now - then).Hours;
			var minutes = (int)(now - then).Minutes;
			var seconds = (int)(now - then).Seconds;
			var value = "";
			if(days > 0) value += days + "D ";
			if(hours > 0) value += hours +"H ";
			if(minutes > 0) value += minutes +"M ";
			if(seconds > 0) value += seconds +"S ";
			return value;
        }
	}
}
