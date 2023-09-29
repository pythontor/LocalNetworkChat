using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Sockets;

namespace LocalNetworkChat
{
    public partial class ChatWindow : Window
    {
        private TcpServer server;
        private TcpClient client;
        private string userName;
        private bool isServer;

        public ChatWindow(string userName, bool isServer)
        {
            InitializeComponent();
            this.userName = userName;
            this.isServer = isServer;

            if (isServer)
            {
                // Инициализируйте и запустите сервер здесь.
                server = new TcpServer();
                server.MessageReceived += OnMessageReceived;
                server.UserConnected += OnUserConnected;
                server.UserDisconnected += OnUserDisconnected;
                // Запустите сервер с заданными параметрами.
                // server.StartServerAsync(ipAddress, port);
            }
            else
            {
                // Инициализируйте и запустите клиента здесь.
                client = new TcpClient();
                client.MessageReceived += OnMessageReceived;
                // Подключитесь к серверу с заданными параметрами.
                // client.ConnectToServerAsync(ipAddress, port);
            }
        }

        private void OnMessageReceived(string message)
        {
            // Обработка полученных сообщений и отображение их в ChatListBox.
            Dispatcher.Invoke(() =>
            {
                ChatListBox.Items.Add(message);
            });
        }

        private void OnUserConnected(string userName)
        {
            // Обработка подключения нового пользователя.
            Dispatcher.Invoke(() =>
            {
                ChatListBox.Items.Add($"{userName} присоединился к чату.");
            });
        }

        private void OnUserDisconnected(string userName)
        {
            // Обработка отключения пользователя.
            Dispatcher.Invoke(() =>
            {
                ChatListBox.Items.Add($"{userName} покинул чат.");
            });
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            // Отправка сообщения серверу или клиенту.
            string message = MessageTextBox.Text;
            if (!string.IsNullOrWhiteSpace(message))
            {
                if (isServer)
                {
                    // Отправка сообщения всем клиентам (если вы сервер).
                    // server.SendMessageToAllAsync($"{userName}: {message}");
                }
                else
                {
                    // Отправка сообщения серверу (если вы клиент).
                    // client.SendMessageToServerAsync($"{userName}: {message}");
                }

                // Очистка поля ввода сообщения.
                MessageTextBox.Text = string.Empty;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие окна и отключение от сервера (если необходимо).
            if (isServer)
            {
                // Остановка сервера.
                // server.StopServerAsync();
            }
            else
            {
                // Отключение от сервера (если вы клиент).
                // client.DisconnectFromServerAsync();
            }

            Close();
        }
    }
}