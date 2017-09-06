using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Week36_Webserver
{
	class Program
	{
		static void Main(string[] args)
		{
			TcpListener listener = new TcpListener(IPAddress.Any, 80);
			listener.Start();
			while (true)
			{
				Console.WriteLine("Waiting for a connection...");
				Socket client = listener.AcceptSocket();
				//TcpClient client = listener.AcceptTcpClient();
				IPEndPoint remote = (IPEndPoint)client.RemoteEndPoint;
				Console.WriteLine("Client connected: IP: " + remote.Address + "; Port: " + remote.Port);
				NetworkStream stream = new NetworkStream(client);
				StreamReader sr = new StreamReader(stream);
				StreamWriter sw = new StreamWriter(stream);
				sw.AutoFlush = false;
				try
				{
					while (true)
					{
						string request = sr.ReadLine();
						Console.WriteLine(request);
						string[] data = request.Split(' ');
						if(data[0] == "GET")
						{
							switch (data[1])
							{
								case "/date":
									string date = DateTime.Today.ToString("dd/MM/yyyy");
									sw.WriteLine("HTTP/1.1 200 OK");
									sw.WriteLine("Content-Type: text/plain");
									sw.WriteLine("Content-Length: "+date.Length);
									sw.WriteLine();
									sw.WriteLine(date);
									sw.Flush();
									break;
								case "/time":
									string time = DateTime.Now.ToString("hh:mm:ss");
									sw.WriteLine("HTTP/1.1 200 OK");
									sw.WriteLine("Content-Type: text/plain");
									sw.WriteLine("Content-Length: " + time.Length);
									sw.WriteLine();
									sw.WriteLine(time);
									sw.Flush();
									break;
								default:
									string notfound = "404 Not Found";
									sw.WriteLine("HTTP/1.1 404 Not Found");
									sw.WriteLine("Content-Type: text/plain");
									sw.WriteLine("Content-Length: " + notfound.Length);
									sw.WriteLine();
									sw.WriteLine(notfound);
									sw.Flush();
									break;
							}
						}
						
					}
				}
				catch(Exception e)
				{
					sw.WriteLine("Error: "+e.ToString());
				}
				//client.Close();
			}
		}
	}
}
