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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocalNetworkChat
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Пожалуйста, введите ваше имя пользователя.");
                return;
            }

            if (CreateChatRadioButton.IsChecked == true)
            {
                // Здесь можно добавить код для создания нового чата (вы становитесь сервером).
                // Создание нового окна чата и переход к нему.
                ChatWindow chatWindow = new ChatWindow(userName, true);
                chatWindow.Show();
                this.Close();
            }
            else if (ConnectChatRadioButton.IsChecked == true)
            {
                // Здесь можно добавить код для подключения к существующему чату (вы становитесь клиентом).
                // Создание нового окна чата и переход к нему.
                ChatWindow chatWindow = new ChatWindow(userName, false);
                chatWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите один из вариантов: Создать новый чат или Подключиться к существующему чату.");
            }
        }
    }
}