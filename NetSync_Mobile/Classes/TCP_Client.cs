using Android.App;
using Android.Widget;
using Sockets.Plugin;
using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace NetSync_Mobile
{
    class TCP_Client
    {
        TcpClient tcpClient;
        X509CertificateCollection certificatesCollection;

        static string certificatePath = @"C:\Users\Admin\Desktop\123.pfx";
        static string certificatePassword = "vbhs456";

        Activity currActivity;

        public static SslStream SSLStream { get; private set; }

        public TCP_Client(Activity _currentActivity)
        {
            certificatesCollection = new X509CertificateCollection();
            //certificatesCollection.Add(new X509Certificate2(certificatePath, certificatePassword));
            tcpClient = new TcpClient();
            currActivity = _currentActivity;
        }
        public async void ConnectToServer(string ServerIP, int ServerPort, string hostName)
        {
            try
            {
                var address = "192.168.169.29";
                var port = 816;
                var r = new Random();

                //var client = new TcpSocketClient();
                //await client.ConnectAsync(address, port);

                IPAddress ipAddress = IPAddress.Parse("192.168.169.29");
                IPEndPoint IP_End = new IPEndPoint(IPAddress.Parse("192.168.169.29"), int.Parse("816"));
                tcpClient.Connect(IP_End);
                ////SSLStream = new SslStream(tcpClient.GetStream(), false);
                ////IPAddress ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
                //Toast.MakeText(currActivity, ipAddress.ToString(), ToastLength.Short).Show();
                //SSLStream.AuthenticateAsClient(hostName, certificatesCollection, SslProtocols.Tls12, true);
                Toast.MakeText(currActivity, "Successfully connected to server!", ToastLength.Short).Show();
            }
            catch (Exception exMessage)
            {
                Toast.MakeText(currActivity, exMessage.Message, ToastLength.Short).Show();
            }
        }

        public void DisconnectFromServer()
        {
            tcpClient.Close();
            Toast.MakeText(currActivity, "Disconnected from server!", ToastLength.Short).Show();
        }
    }
}