using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.IO;
using UnityEngine;
using PacketPackage;
using ProtoBuf;
using System.Threading;

public class PacketManager
{
    private const int serverPort = 1001;
    private const string serverIP = "172.30.1.16";
    private static PacketManager instance;
    private TcpClient server;

    private string id;
    public string ID { get; set; }

    private PacketManager()
    {
        server = null;
    }
    public static PacketManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PacketManager();
            }
            return instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Connect()
    {
        PacketManager.Instance.ConnectServer();
    }

    public void ConnectServer()
    {
        try
        {
            server = new TcpClient(serverIP, serverPort);

            var t = new Thread(Receive);
            t.Start();
        }
        catch(Exception e)
        {
            //연결 불가능하면 Debug.Log 문구와 UI로 표기해주기
            Debug.Log(e.Message);
            return;
        }
    }
    public void Receive()
    {
        while (true)
        {
            ReceivePacket();
        }
    }

    public void SendPacket(Packet packet)
    {
        if(isConnected() == false)
        {
            Debug.LogError("서버에 연결되어 있지 않아 패킷을 전송할 수 없습니다.");
        }

        SendHeadPacket(packet);
        SendBodyPacket(packet);
    }

    private void SendHeadPacket(Packet packet)
    {
        byte[] headBuffer = IntToBytes(packet.Head);

        NetworkStream networkStream = server.GetStream();
        networkStream.Write(headBuffer, 0, headBuffer.Length);
    }
    private void SendBodyPacket(Packet packet)
    {
        int bodyLength;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            Serializer.Serialize(memoryStream, packet);
            var buffer = memoryStream.ToArray();
            bodyLength = buffer.Length;
        }

        byte[] bodyLengthBuffer = IntToBytes(bodyLength);
        NetworkStream networkStream = server.GetStream();

        networkStream.Write(bodyLengthBuffer, 0, bodyLengthBuffer.Length);

        using (MemoryStream memoryStream = new MemoryStream())
        {
            Serializer.Serialize(memoryStream, packet);
            var buffer = memoryStream.ToArray();
            networkStream.Write(buffer, 0, buffer.Length);
        }
    }

    public void ReceivePacket()
    {
        if (isConnected() == false)
        {
            Debug.LogError("서버에 연결되어 있지 않아 패킷을 전송할 수 없습니다.");
        }
        NetworkStream networkStream = server.GetStream();

        byte[] headBuffer = new byte[4];
        networkStream.Read(headBuffer, 0, headBuffer.Length);

        int head = BytesToInt(headBuffer);

        byte[] bodyLengthBuffer = new byte[4];

        networkStream.Read(bodyLengthBuffer, 0, bodyLengthBuffer.Length);

        int bodyLength = BytesToInt(bodyLengthBuffer);

        byte[] bodyBuffer = new byte[bodyLength];
        networkStream.Read(bodyBuffer, 0, bodyBuffer.Length);
        Packet packet;

        using (MemoryStream memoryStream = new MemoryStream(bodyBuffer))
        {
            packet = Serializer.Deserialize<Packet>(memoryStream);
        }

        Dispatcher.Register(head, packet);
    }

    static byte[] IntToBytes(int n)
    {
        byte[] arr = new byte[4];
        arr[0] = (byte)(n & 0xff);
        arr[1] = (byte)((n >> 8) & 0xff);
        arr[2] = (byte)((n >> 16) & 0xff);
        arr[3] = (byte)((n >> 24) & 0xff);
        return arr;
    }
    static int BytesToInt(byte[] buf)
    {
        int num = (buf[0] & 0xff) | ((buf[1] & 0xff) << 8) | ((buf[2] & 0xff) << 16) | ((buf[3] & 0xff) << 24);
        return num;
    }

    private bool isConnected()
    {
        if (server != null)
        {
            return true;
        }

        return false;
    }
}