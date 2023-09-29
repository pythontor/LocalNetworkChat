using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

public class TcpClient
{
    private TcpClient _client;
    private NetworkStream _stream;
    private string _serverAddress;
    private int _serverPort;

    public Action<string> MessageReceived { get; internal set; }

    public async Task ConnectToServerAsync(string serverAddress, int serverPort)
    {
        _client = new TcpClient();
        _serverAddress = serverAddress;
        _serverPort = serverPort;

        await _client.ConnectAsync(serverAddress, serverPort);
        _stream = _client.GetStream();

        // Handle the connection (e.g., start listening for messages)
        // You can use a separate method or thread for this.
    }

    public NetworkStream GetStream()
    {
        throw new NotImplementedException();
    }

    public async Task SendMessageToServerAsync(string message)
    {
        if (_client != null && _client.Connected)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await _stream.WriteAsync(buffer, 0, buffer.Length);
        }
        else
        {
            // Handle not connected state or provide feedback to the user.
        }
    }

    public void DisconnectFromServer()
    {
        if (_client != null)
        {
            _stream.Close();
            _client.Close();
        }
    }

    public void Close()
    {
        throw new NotImplementedException();
    }

    public static implicit operator TcpClient(System.Net.Sockets.TcpClient v)
    {
        throw new NotImplementedException();
    }
}