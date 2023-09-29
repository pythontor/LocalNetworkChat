using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class TcpServer
{
    private TcpListener _listener;
    private List<TcpClient> _connectedClients = new List<TcpClient>();

    public Action<string> MessageReceived { get; internal set; }
    public Action<string> UserConnected { get; internal set; }
    public Action<string> UserDisconnected { get; internal set; }

    public async Task StartServerAsync(int port)
    {
        _listener = new TcpListener(IPAddress.Any, port);
        _listener.Start();

        while (true)
        {
            TcpClient client = await _listener.AcceptTcpClientAsync();
            _connectedClients.Add(client);

            // Handle client in a separate method or thread
            HandleClientAsync(client);
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        try
        {
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                // Handle received message (e.g., broadcast to other clients)
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., client disconnect)
        }
        finally
        {
            _connectedClients.Remove(client);
            client.Close();
        }
    }

    public async Task SendMessageToAllAsync(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        foreach (TcpClient client in _connectedClients)
        {
            await client.GetStream().WriteAsync(buffer, 0, buffer.Length);
        }
    }

    public async Task SendMessageToServerAsync(string message, IPAddress serverAddress, int serverPort)
    {
        TcpClient client = new TcpClient();
        await client.ConnectAsync(serverAddress, serverPort);

        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await client.GetStream().WriteAsync(buffer, 0, buffer.Length);

        client.Close();
    }

    public void StopServerAsync()
    {
        _listener.Stop();

        foreach (TcpClient client in _connectedClients)
        {
            client.Close();
        }

        _connectedClients.Clear();
    }

    public void DisconnectFromServerAsync(TcpClient client)
    {
        client.Close();
        _connectedClients.Remove(client);
    }
}
