using System.Net.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;


class ConServer
{

    TcpListener serverTcpListener;

    bool alreadyHaveClientControll;
    public bool isRuning;

    ClientController clientController;

    public ConServer(int port)
    {
        serverTcpListener = new TcpListener(IPAddress.Any, port);
        serverTcpListener.Start();
        serverTcpListener.BeginAcceptSocket(OnClientConnect, null);
        isRuning = true;
    }


    //When client connects    
    private void OnClientConnect(IAsyncResult _result)
    {
        try
        {
            TcpClient _client = serverTcpListener.EndAcceptTcpClient(_result);
            serverTcpListener.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), null);
            if (clientController == null)
            {
                clientController = new ClientController();
                clientController.Connect(_client);
                alreadyHaveClientControll = true;
            }
        }
        catch (System.Exception e)
        {

            Console.WriteLine(e);
        }

    }
}
