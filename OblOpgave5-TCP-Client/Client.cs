using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OblOpgave5_TCP_Client
{
    class Client
    {
        private const int Server_Port = 2121;

        public Client()
        {
            
        }
        public void Start()
        {
            TcpClient socket = new TcpClient("localhost", Server_Port);

            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                sw.AutoFlush = true;

                string message1 = Console.ReadLine();
                sw.WriteLine(message1);

                string message2 = Console.ReadLine();
                sw.WriteLine(message2);

                string line1 = sr.ReadLine();
                Console.WriteLine(line1);

                string line2 = sr.ReadLine();
                Console.WriteLine(line2);

            }
            
                
            
        }
    }
}
