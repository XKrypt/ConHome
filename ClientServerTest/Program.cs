using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientServerTest
{
    class Program
    {
         const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        static void Main(string[] args)
        {
            //---data to send to the server---

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            NetworkStream nwStream = client.GetStream();
            
            string textToSend = Console.ReadLine();
            byte[] bytesToSend = new PacketReader(textToSend).writeData();

            //---send the text---
            Console.WriteLine("Sending : " + textToSend);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            Console.ReadLine();
            client.Close();
        }
    }
}
