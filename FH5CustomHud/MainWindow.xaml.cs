using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using FH5CustomHud.Core;
using FH5CustomHud.Gauges;

namespace FH5CustomHud
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            int receiverPort = 6977;
            UdpClient receiver = new UdpClient(receiverPort);
            receiver.BeginReceive(DataReceived, receiver);
            spawnWidgets();
        }
        private static void DataReceived(IAsyncResult ar)
        {
            int BufferOffset = 0;
            UdpClient c = (UdpClient)ar.AsyncState;
            IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Byte[] bytes = c.EndReceive(ar, ref receivedIpEndPoint);
            Globals.speed.Value = (int)Globals.GetSingle(bytes, 256  + BufferOffset);
            Globals.RPM_MAX.Value = (int)Globals.GetSingle(bytes, 8);
            Globals.RPM.Value = (int)Globals.GetSingle(bytes, 16);
            Globals.BOOST.Value = Globals.GetSingle(bytes, 284 + BufferOffset);

            c.BeginReceive(DataReceived, ar.AsyncState);
        }

        public void spawnWidgets()
        {
            new BoostGauge().Show();
        }
        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            Globals.AutoHide = true;
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            Globals.AutoHide = false;
        }

        private void SettingsWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}