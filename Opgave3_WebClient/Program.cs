using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Opgave3_WebClient
{
	class Program
	{
		static void Main(string[] args)
		{
			WebClient client = new WebClient();
			string besked = client.DownloadString("http://bjoerks.net/test/test.txt");
			Console.WriteLine("Besked: "+ besked + ":Slut på besked");
			Console.ReadKey();
		}
	}
}
