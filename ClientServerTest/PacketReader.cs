using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PacketReader : IDisposable
{
    byte[] package;
    List<byte> _package = new List<byte>();
    public PacketReader(byte[] data)
    {
        package = data;
    }

    public PacketReader(string data)
    {
        byte[] lenght = BitConverter.GetBytes(Encoding.ASCII.GetBytes(data).Length);

        List<byte> messageSender = new List<byte>();

        messageSender.AddRange(lenght);
        messageSender.AddRange(Encoding.UTF8.GetBytes(data));
        _package.AddRange(messageSender);
        Console.WriteLine($"Package lenght is { _package.Count }");
    }

    public int PackageLenght()
    {
        byte[] lenght = new byte[4];
        Array.Copy(package, lenght, 4);
        int _lenght = BitConverter.ToInt32(lenght,0);
        Console.WriteLine($"Package lenght is { _lenght }");
        return _lenght;
    }

    public string GetData()
    {
        byte[] data = new byte[PackageLenght()];
        Array.Copy(package, 4, data, 0, PackageLenght());
        string datareceived = Encoding.UTF8.GetString(data);

        return datareceived;
    }

    public byte[] writeData()
    {
        return _package.ToArray();
    }

    public void Dispose()
    {
        
        GC.SuppressFinalize(this);
    }
}