using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Week36_Client
{
	class Program
	{
		static void Main(string[] args)
		{
			TcpClient server = new TcpClient("webservicedemo.datamatiker-skolen.dk", 80);
			NetworkStream stream = server.GetStream();
			StreamReader sr = new StreamReader(stream);
			StreamWriter sw = new StreamWriter(stream);
			int a = 254;
			int b = 6;
			sw.WriteLine("GET /RegneWcfService.svc/RESTjson/Add?a=" + a + "&b=" + b + " HTTP/1.1");
			sw.WriteLine("HOST: webservicedemo.datamatiker-skolen.dk");
			sw.WriteLine();
			sw.Flush();
			int messagelength = 0;
			string line = sr.ReadLine();
			while (line != null)
			{
				Console.WriteLine(line);
				if (line == "") break;
				string[] data = line.Split(':');
				if(data[0] == "Content-Length")
				{
					data[1] = data[1].Replace(" ", "");
					messagelength = int.Parse(data[1]);
				}
				line = sr.ReadLine();
			}
			int i = 0;
			
			while(i < messagelength)
			{
				Console.Write((char)sr.Read());
				i++;
			}
			
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Done...");
			server.Close();
			Console.ReadKey();
		}
	}
}
