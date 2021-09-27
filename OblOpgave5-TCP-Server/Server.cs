using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassLibrary_FootballPlayer;

namespace OblOpgave5_TCP_Server
{
    class Server
    {
        private const int PORT = 2121;
        public Server()
        {
            
        }

        public static List<FootballPlayer> players = new List<FootballPlayer>()
        {
            new FootballPlayer(1, "Player One", 100, 10),
            new FootballPlayer(2, "Player Two", 200, 12),
            new FootballPlayer(3, "Player Tree", 300, 14),
            new FootballPlayer(4, "Player Four", 400, 9),
            new FootballPlayer(5, "Player Five", 500, 11),
            
            
        };
        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, PORT);
            listener.Start();

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Task.Run(
                    () =>
                    {
                        TcpClient tmpSocket = socket;
                        DoClient(tmpSocket);
                    }
                );

            }
        }

        public void DoClient(TcpClient socket)
        {
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                sw.AutoFlush = true;

                string readLineOne = sr.ReadLine();
                string readLineTwo = sr.ReadLine();

                if (readLineOne == "hentalle")
                {
                    string message = GetAll();
                    sw.WriteLine(message);
                    Console.WriteLine(message);
                }

                else if (readLineOne == "hent")
                {
                    string message = Get(readLineTwo);
                    sw.WriteLine(message);
                    Console.WriteLine(message);
                }


                else if (readLineOne == "save")
                {
                    string message = Save(readLineTwo);
                    sw.WriteLine(message);
                    Console.WriteLine(message);
                }


            }
            socket?.Close();
            
        }

        private string GetAll()
        {
            string allPlayers = JsonSerializer.Serialize(players);

            return allPlayers;
        }

        private string Get(string id_string)
        {
            int id;
            string eMessage = "\n player id not found";

            try
            {
                id = Int32.Parse(id_string);
                return JsonSerializer.Serialize(players.Find(p => p.Id == id));
            }
            catch
            {
                return eMessage;
            }

        }

        private string Save(string readLineTwo)
        {
            string eMessage = "\n forkert indtastning af PLayer";

            try
            {
                players.Add(JsonSerializer.Deserialize<FootballPlayer>(readLineTwo));
                return "Player added";
            }
            catch (Exception e)
            {
                return eMessage;
            }
        }
    }
}
