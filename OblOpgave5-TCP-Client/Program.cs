using System;

namespace OblOpgave5_TCP_Client
{
    class Program
    {
        static void Main()
        {
            Client client = new Client();
            client.Start();

            Console.ReadLine();
        }
    }
}
