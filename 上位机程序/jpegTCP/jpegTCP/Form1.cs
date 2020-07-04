using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Drawing.Imaging;

namespace jpegTCP
{
    public partial class MainWindow : Form
    {

        IPAddress ip;
        IPEndPoint point;
        Socket aimSocket;
        Socket mySocket;
        string protocol;
        Thread thReceive;

        //小车控制
        bool Left = false;
        bool Head = false;
        bool Right = false;
        bool Behind = false;
        byte[] CtrBuf = new byte[2] { 0xA3, 0x20 };

        private bool ConnectOrNot = false;
        public MainWindow()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            //以下代码用于控件缩放 使窗口最大化
            int count = this.Controls.Count + 1;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is GroupBox)
                {
                    foreach (Control ctrl1 in ctrl.Controls)
                    {
                        count++;
                    }
                }

            }
            float[] factor = new float[count * 2];
            int i = 0;
            factor[i++] = Size.Width;
            factor[i++] = Size.Height;
            foreach (Control ctrl in this.Controls)
            {
                factor[i++] = ctrl.Location.X / (float)Size.Width;
                factor[i++] = ctrl.Location.Y / (float)Size.Height;
                ctrl.Tag = ctrl.Size;//!!!
                if (ctrl is GroupBox)
                {
                    foreach (Control ctrl1 in ctrl.Controls)
                    {
                        factor[i++] = ctrl1.Location.X / (float)Size.Width;
                        factor[i++] = ctrl1.Location.Y / (float)Size.Height;
                        ctrl1.Tag = ctrl1.Size;//!!!
                    }
                }

            }
            Tag = factor;
        }

        string GetAddressIP()
        {
            string AddressIP = "";
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    ip = _IPAddress;//设定全局的IP
                }
            }
            return AddressIP;
        }

        private void cobProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            protocol = cobProtocol.SelectedItem.ToString();
            if (protocol == "TCP Server")
            {
                labIP.Text = "本地IP：";
                labPort.Text = "本地端口：";
            }
            else if (protocol == "TCP Client")
            {
                labIP.Text = "服务器IP：";
                labPort.Text = "服务器端口：";
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            txtIP.Text = GetAddressIP(); //可以取消注释以获得本地代码 用于调试
            protocol = cobProtocol.Text;
            point = new IPEndPoint(ip, 8081); //端口号8081
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (protocol == "TCP Server")
            {
                mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));//绑定端口号
                mySocket.Bind(point);//绑定监听端口
                infoLabel.Text = "TCP Server绑定成功";
                mySocket.Listen(10);//等待连接是一个阻塞过程，创建线程来监听
                thReceive = new Thread(TSReceive);
                thReceive.IsBackground = true;
                thReceive.Start();
                ConnectOrNot = true;
            }
            else if (protocol == "TCP Client")
            {
                ip = IPAddress.Parse(txtIP.Text);//目标IP
                point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));//目标端口
                aimSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                aimSocket.Connect(point);//连接服务器
                infoLabel.Text = "连接成功";
                Thread thReceive = new Thread(TCReceive);
                thReceive.IsBackground = true;
                thReceive.Start();
            }
        }

        private void Network_DataFrameDecode(byte[] ReceiceBytes)
        {

        }

        //TCP Server接收线程
        void TSReceive()
        {
            aimSocket = mySocket.Accept();//服务端监听到的Socket为服务端发送数据的目标socket
            infoLabel.Text = "连接成功";
            byte[] buffer = new byte[4];
            while (true)
            {
                try
                {
                    aimSocket.Receive(buffer, buffer.Length, SocketFlags.None);
                    int contentLen = BitConverter.ToInt32(buffer, 0);
                    int size = 0;
                    MemoryStream ms = new MemoryStream();
                    while (size < contentLen)
                    {
                        //分多次接收,每次接收256个字节,
                        byte[] bits = new byte[256];
                        int r = aimSocket.Receive(bits, bits.Length, SocketFlags.None);//接收到监听的Socket的数据
                        if (r == 0)
                        {
                            MessageBox.Show("连接断开");
                            break;
                        }
                        ms.Write(bits, 0, r);
                        size += r;
                    }
                    Image img = Image.FromStream(ms);
                    picBox.Image = null;
                    picBox.Image = img;

                }
                catch
                { }
            }
        }
        //TCP Client接收线程
        void TCReceive()
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                try
                {
                    int r = aimSocket.Receive(buffer);
                    if (r == 0)
                    {
                        infoLabel.Text = "连接断开";
                        break;
                    }
                    Network_DataFrameDecode(buffer);
                }
                catch
                { }
            }
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            float[] scale = (float[])Tag;
            int i = 2;
            if (scale != null)
            {
                foreach (Control ctrl in this.Controls)
                {
                    ctrl.Left = (int)(Size.Width * scale[i++]);
                    ctrl.Top = (int)(Size.Height * scale[i++]);
                    ctrl.Width = (int)(Size.Width / (float)scale[0] * ((Size)ctrl.Tag).Width);//!!!
                    ctrl.Height = (int)(Size.Height / (float)scale[1] * ((Size)ctrl.Tag).Height);//!!!

                    //每次使用的都是最初始的控件大小，保证准确无误。
                    if (ctrl is GroupBox)
                    {
                        foreach (Control ctrl1 in ctrl.Controls)
                        {
                            ctrl1.Left = (int)(Size.Width * scale[i++]);
                            ctrl1.Top = (int)(Size.Height * scale[i++]);
                            ctrl1.Width = (int)(Size.Width / (float)scale[0] * ((Size)ctrl1.Tag).Width);//!!!
                            ctrl1.Height = (int)(Size.Height / (float)scale[1] * ((Size)ctrl1.Tag).Height);//!!!
                        }
                    }
                }

            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConnectOrNot)
                {
                    //mySocket.Shutdown(SocketShutdown.Both);
                    mySocket.Close();
                    thReceive.Abort();
                    infoLabel.Text = "连接断开";
                    ConnectOrNot = false;
                }
            }
            catch { }
        }

        private void btnHead_Click(object sender, EventArgs e)
        {
            CtrBuf[1] = 0x1A;
            aimSocket.Send(CtrBuf);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            CtrBuf[1] = 0x2A;
            aimSocket.Send(CtrBuf);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            CtrBuf[1] = 0x3A;
            aimSocket.Send(CtrBuf);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            CtrBuf[1] = 0x4A;
            aimSocket.Send(CtrBuf);
        }

    }
}
