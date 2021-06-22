using System.Net.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;





class ClientController
{


    //Tcp Connection
    public TcpClient clientSock;
    NetworkStream stream;

    //Buffer Of messages
    private static int dataBufferSize = 4096; //BufferSize

    //Received  message Buffer
    private byte[] receiveBuffer; //bufferReceived;




    public void Connect(TcpClient _socket)
    {

        //Connect client
        clientSock = _socket;
        //Buffer Configs
        clientSock.ReceiveBufferSize = dataBufferSize;
        clientSock.SendBufferSize = dataBufferSize;

        //save stream socket in the class
        stream = clientSock.GetStream();

        //receiveBufferSize;
        receiveBuffer = new byte[dataBufferSize];
        //callBack to read messages from clients.
        stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiverCallback, null);
    }


    //Receive client messages
    void ReceiverCallback(IAsyncResult _result){

        try
            {
                //Lenght of message;
                int _byteLength = stream.EndRead(_result);


                if (_byteLength <= 0)
                {
                    // TODO: disconnect
                    return;
                }
                byte[] copyBuffer = new byte[dataBufferSize];
                //Copy to receiver buff
                Array.Copy(receiveBuffer, copyBuffer, _byteLength);



                //Decode message
                PacketReader pacote = new PacketReader(copyBuffer);
                Console.WriteLine(pacote.GetData());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error receiving data: " + e);
            }
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiverCallback, null);

    }

}