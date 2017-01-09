using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CombineAudioAndVideo();
        }

        private void CombineAudioAndVideo()
        {
            String audioTextStr = audioText.Text;
            String videoTextStr = videoText.Text;
            String outputTextStr = outputText.Text + extension.Text;
            //String outputTextStr = "SECHSKIES - '컴백(COM' BACK)'   '세 단어(THREE WORDS)'   '커플(COUPLE)' in 2016 MELON MUSIC AWARDS.mp4";
            Process p = new Process();//创建进程对象 
            p.StartInfo.FileName = "cmd.exe";//设定需要执行的命令 
                                             // startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
            p.StartInfo.UseShellExecute = false;//不使用系统外壳程序启动 
            p.StartInfo.RedirectStandardInput = true;//可以重定向输入,接受来自调用程序的输入信息  
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不创建窗口
            //p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            //p.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            p.Start();//启动程序
            Console.WriteLine(p.StandardInput.Encoding.EncodingName);
            Console.WriteLine(Encoding.GetEncoding(949).EncodingName);
            p.StandardInput.WriteLine("D:");
            p.StandardInput.WriteLine(@"cd D:\\win-youtube-dl");
            //p.StandardInput.WriteLine(@"ping 192.168.123.11");
            
            p.StandardInput.WriteLine(@"ffmpegg -i """+audioTextStr+"\" -i \""+videoTextStr+"\" -vcodec copy -acodec copy \""+ outputTextStr+"\"");
            p.StandardInput.WriteLine(@"exit");
            //获取cmd窗口的输出信息
            //p.StandardInput.AutoFlush = true;

            //--------目前有阻塞问题，待解决 BEGIN----------
            string output = p.StandardOutput.ReadToEnd();
            //string error = p.StandardError.ReadToEnd();
            //p.WaitForExit();//等待程序执行完退出进程
            Console.WriteLine(output);
            //Console.WriteLine(error);
            //--------目前有阻塞问题，待解决 END----------
            if (p.HasExited)
            {
            p.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "音频文件(*.m4a)|*.m4a|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                audioText.Text = ofd.FileName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "视频文件(*.mp4,*.avi,*.mkv)|*.mp4;*.avi;*.mkv|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                videoText.Text = ofd.FileName;
                outputText.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
                extension.Text = Path.GetExtension(ofd.FileName);
            }
        }

        public static string get_uft8(string unicodeString)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }
    }
}
