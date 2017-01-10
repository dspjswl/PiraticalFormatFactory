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
        private Progress progressForm = new Progress();
        // 代理定义，可以在Invoke时传入相应的参数  
        private delegate void funHandle(int nValue);
        private funHandle myHandle = null;
        static int timeCount;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CombineAudioAndVideo2();
        }

        private void CombineAudioAndVideo()
        {
            // 启动线程  
            //System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadFun));
            //thread.Start();
            String audioTextStr = audioText.Text;
            String videoTextStr = videoText.Text;
            String outputTextStr = outputPath.Text + outputText.Text + extension.Text;
            //String outputTextStr = "SECHSKIES - '컴백(COM' BACK)'   '세 단어(THREE WORDS)'   '커플(COUPLE)' in 2016 MELON MUSIC AWARDS.mp4";
            Process p = new Process();//创建进程对象 
            p.StartInfo.FileName = "cmd.exe";//设定需要执行的命令 
                                             // startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
            p.StartInfo.UseShellExecute = false;//不使用系统外壳程序启动 
            p.StartInfo.RedirectStandardInput = true;//可以重定向输入,接受来自调用程序的输入信息  
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = false;//不创建窗口
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
            progressForm.Show();
            while (!p.HasExited)
            {
            }
            progressForm.Close();
            //if (p.HasExited)
            //{
            //p.Close();
            //}
            
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
                outputPath.Text = "D:\\win-youtube-dl\\";
            }
        }

        public static string get_uft8(string unicodeString)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }

        //-----用法太高深，以后再看2333------
        //private void ThreadFun()
        //{
        //    MethodInvoker mi = new MethodInvoker(ShowProgressBar);
        //    this.BeginInvoke(mi);

        //    System.Threading.Thread.Sleep(1000); // sleep to show window  

        //    for (int i = 0; i < 1000; ++i)
        //    {
        //        System.Threading.Thread.Sleep(5);
        //        // 这里直接调用代理  
        //        this.Invoke(this.myHandle, new object[] { (i / 10) });
        //    }
        //}
        //private void ShowProgressBar()
        //{
        //    myHandle = new funHandle(progressForm.SetProgressValue);
        //    progressForm.ShowDialog();
        //}

        private void CombineAudioAndVideo2()
        {

            String audioTextStr = audioText.Text;
            String videoTextStr = videoText.Text;
            String outputTextStr = outputPath.Text + outputText.Text + extension.Text;
            //String outputTextStr = "SECHSKIES - '컴백(COM' BACK)'   '세 단어(THREE WORDS)'   '커플(COUPLE)' in 2016 MELON MUSIC AWARDS.mp4";
            Process p = new Process();//创建进程对象 
            p.StartInfo.FileName = "cmd.exe";//设定需要执行的命令 
                                             // startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
            p.StartInfo.UseShellExecute = false;//不使用系统外壳程序启动 
            p.StartInfo.RedirectStandardInput = true;//可以重定向输入,接受来自调用程序的输入信息  
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不创建窗口
            p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            using (p)
            {
                p.Start();
                p.StandardInput.WriteLine("D:");
                p.StandardInput.WriteLine(@"cd D:\\win-youtube-dl");
                if (File.Exists(@outputTextStr))
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("文件已存在，是否覆盖?", "提示", messButton);
                    if (dr == DialogResult.OK)//如果点击“确定”按钮
                    {
                        p.StandardInput.WriteLine(@"echo y | ffmpeg -i """ + audioTextStr + "\" -i \"" + videoTextStr + "\" -vcodec copy -acodec copy \"" + outputTextStr + "\"");
                    }
                    else//如果点击“取消”按钮
                    {
                        p.StandardInput.WriteLine(@"echo N | ffmpeg -i """ + audioTextStr + "\" -i \"" + videoTextStr + "\" -vcodec copy -acodec copy \"" + outputTextStr + "\"");
                    }
                }
                else
                {
                    p.StandardInput.WriteLine(@"ffmpeg -i """ + audioTextStr + "\" -i \"" + videoTextStr + "\" -vcodec copy -acodec copy \"" + outputTextStr + "\"");
                }
                p.StandardInput.WriteLine(@"exit");
                p.BeginErrorReadLine();//开始异步读取
                if (p.HasExited)
                {
                    p.Close();//关闭进程
                }
            }

        }
        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
          
             var output = e.Data;
             if (output != null)
             {
                 if (output.Contains("Duration"))
                 {
                     Int32 indexOfDuration = output.IndexOf("Duration");
                     Int32 indexOfBegin = output.IndexOf(":", indexOfDuration);
                   Int32 indexOfEnd = output.IndexOf(",", indexOfBegin);
                     var duration = output.Substring(indexOfBegin + 1, indexOfEnd - indexOfBegin - 1);
                     timeCount =ConvertStringtoSecond(duration);
                 }
                 if (output.Contains("time="))
                 {
                     Int32 indexOfTime = output.IndexOf("time=");
                     Int32 indexOfBegin = output.IndexOf("=", indexOfTime);
                     Int32 indexOfEnd = output.IndexOf("bitrate", indexOfBegin);
                     string timeStr = output.Substring(indexOfBegin + 1, indexOfEnd - indexOfBegin - 2);
                     var time = (Double)ConvertStringtoSecond(timeStr);
                     var process = time / timeCount * 100;
                     process = Math.Round(process);
                     Console.Write("{0}% ", process);
                 }
                
                 Console.WriteLine(e.Data);
             }
         }
        private static int ConvertStringtoSecond(string input)
         {
             int totalSecond = 0;
             try
             {
                 string[] split = input.Split(new char[] { ':', '.' });
                 int hour = int.Parse(split[0]) * 3600;
                 int min = int.Parse(split[1]) * 60;
                 int second = int.Parse(split[2]);
                 int millisecond = int.Parse(split[3]) / 1000;
                 totalSecond = hour + min + second + millisecond;
             }
             catch (System.Exception ex)
             {
                 Console.Write(ex.Message);
 
             }
             return totalSecond;
         }
        private static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
         {
             Console.WriteLine(e.Data);
         }
}
}
