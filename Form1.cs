// Decompiled with JetBrains decompiler
// Type: Launcher_v2.Form1
// Assembly: RustyHeartsLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b4b86969018abd94
// MVID: 4B0ED571-701E-464E-AF26-7DF82DDA673B
// Assembly location: F:\Temp\RustyHearts_Devourment_Beta\RustyHeartsLauncher.exe

using Ionic.Zip;
using Launcher_v2.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Launcher_v2
{
  public class Form1 : Form
  {
    public const int WM_NCLBUTTONDOWN = 161;
    public const int HT_CAPTION = 2;
    private IContainer components;
    private ProgressBar progressBar1;
    private BackgroundWorker backgroundWorker1;
    private Button strtGameBtn;
    private Label downloadLbl;
    private Button closeBtn;
    private Button minimizeBtn;
    private BackgroundWorker backgroundWorker2;
    private Button button2;
    private Button button3;
    private Button button4;
    private Button button5;
    private Button button1;
    private Button button6;
    private Button button7;
    private Button button8;

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    public Form1()
    {
      this.InitializeComponent();
      this.backgroundWorker1.RunWorkerAsync();
      this.strtGameBtn.Enabled = false;
    }

    private void Form1_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      Form1.ReleaseCapture();
      Form1.SendMessage(this.Handle, 161, 2, 0);
    }

    private void closeBtn_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void closeBtn_MouseEnter(object sender, EventArgs e)
    {
      this.closeBtn.BackgroundImage = (Image) Resources.close2;
    }

    private void closeBtn_MouseLeave(object sender, EventArgs e)
    {
      this.closeBtn.BackgroundImage = (Image) Resources.close1;
    }

    private void minimizeBtn_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void minimizeBtn_MouseEnter(object sender, EventArgs e)
    {
      this.minimizeBtn.BackgroundImage = (Image) Resources.minimize2;
    }

    private void minimizeBtn_MouseLeave(object sender, EventArgs e)
    {
      this.minimizeBtn.BackgroundImage = (Image) Resources.minimize1;
    }

    private static bool deleteFile(string f)
    {
      try
      {
        System.IO.File.Delete(f);
        return true;
      }
      catch (IOException ex)
      {
        return false;
      }
    }

    private void backgroundWorker1_DoWork(object sender, EventArgs e)
    {
      string str1 = "http://104.238.174.204/Updates/";
      string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      if (!System.IO.File.Exists("version.txt"))
      {
        using (System.IO.File.Create("version.txt"))
          ;
        using (StreamWriter streamWriter = new StreamWriter("version.txt"))
          streamWriter.Write("1.0");
      }
      string s1;
      using (StreamReader streamReader = new StreamReader("version.txt"))
        s1 = streamReader.ReadLine();
      Decimal num1 = Decimal.Parse(s1);
      foreach (XElement descendant in XDocument.Load("http://104.238.174.204/Updates.xml").Descendants((XName) "update"))
      {
        string s2 = descendant.Element((XName) "version").Value;
        string str2 = descendant.Element((XName) "file").Value;
        Decimal num2 = Decimal.Parse(s2);
        string uriString = str1 + str2;
        string path = baseDirectory + str2;
        if (num2 > num1)
        {
          HttpWebResponse response = (HttpWebResponse) WebRequest.Create(new Uri(uriString)).GetResponse();
          response.Close();
          long contentLength = response.ContentLength;
          long num3 = 0;
          using (WebClient webClient = new WebClient())
          {
            using (Stream stream1 = webClient.OpenRead(new Uri(uriString)))
            {
              using (Stream stream2 = (Stream) new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
              {
                byte[] buffer = new byte[contentLength];
                int count;
                while ((count = stream1.Read(buffer, 0, buffer.Length)) > 0)
                {
                  stream2.Write(buffer, 0, count);
                  num3 += (long) count;
                  this.backgroundWorker1.ReportProgress((int) ((double) num3 / (double) buffer.Length * 100.0));
                }
                stream2.Close();
              }
              stream1.Close();
            }
          }
          using (ZipFile zipFile = ZipFile.Read(str2))
          {
            foreach (ZipEntry zipEntry in zipFile)
              zipEntry.Extract(baseDirectory, true);
          }
          new WebClient().DownloadFile(str1 + "version.txt", baseDirectory + "version.txt");
          Form1.deleteFile(str2);
          Process.Start("mpatcher.exe");
        }
      }
    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.progressBar1.Value = e.ProgressPercentage;
      this.downloadLbl.ForeColor = Color.Silver;
      this.downloadLbl.Text = "Downloading Updates";
    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.strtGameBtn.Enabled = true;
      this.downloadLbl.ForeColor = Color.FromArgb(10, 90, 23);
      this.downloadLbl.Text = "Client is up to date";
    }

    private void strtGameBtn_Click(object sender, EventArgs e)
    {
      Process.Start("RustyHearts.exe", "server=http://104.238.174.204/rustyhearts.xml");
      this.Close();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void manualPatchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num1 = (int) MessageBox.Show("Press Ok To Begin Manual Patch be Patient While this Process is underway please do not click the launcher");
      string str1 = "http://104.238.174.204/Updates/";
      string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      foreach (XContainer descendant in XDocument.Load("http://104.238.174.204/Updates.xml").Descendants((XName) "update"))
      {
        string str2 = descendant.Element((XName) "file").Value;
        string uriString = str1 + str2;
        string path = baseDirectory + str2;
        HttpWebResponse response = (HttpWebResponse) WebRequest.Create(new Uri(uriString)).GetResponse();
        response.Close();
        long contentLength = response.ContentLength;
        long num2 = 0;
        using (WebClient webClient = new WebClient())
        {
          using (Stream stream1 = webClient.OpenRead(new Uri(uriString)))
          {
            using (Stream stream2 = (Stream) new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
              byte[] buffer = new byte[contentLength];
              int count;
              while ((count = stream1.Read(buffer, 0, buffer.Length)) > 0)
              {
                stream2.Write(buffer, 0, count);
                num2 += (long) count;
              }
              stream2.Close();
            }
            stream1.Close();
          }
        }
        using (ZipFile zipFile = ZipFile.Read(str2))
        {
          foreach (ZipEntry zipEntry in zipFile)
            zipEntry.Extract(baseDirectory, true);
        }
        new WebClient().DownloadFile(str1 + "version.txt", baseDirectory + "version.txt");
        Form1.deleteFile(str2);
        Process.Start("mpatcher.exe");
      }
    }

    private void versionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("Version.txt");
    }

    private void button3_Click(object sender, EventArgs e)
    {
      int num1 = (int) MessageBox.Show("Press Ok To Begin Manual Patch be Patient While this Process is underway please do not click the launcher");
      string str1 = "http://104.238.174.204/Updates/";
      string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      foreach (XContainer descendant in XDocument.Load("http://104.238.174.204/Updates.xml").Descendants((XName) "update"))
      {
        string str2 = descendant.Element((XName) "file").Value;
        string uriString = str1 + str2;
        string path = baseDirectory + str2;
        HttpWebResponse response = (HttpWebResponse) WebRequest.Create(new Uri(uriString)).GetResponse();
        response.Close();
        long contentLength = response.ContentLength;
        long num2 = 0;
        using (WebClient webClient = new WebClient())
        {
          using (Stream stream1 = webClient.OpenRead(new Uri(uriString)))
          {
            using (Stream stream2 = (Stream) new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
              byte[] buffer = new byte[contentLength];
              int count;
              while ((count = stream1.Read(buffer, 0, buffer.Length)) > 0)
              {
                stream2.Write(buffer, 0, count);
                num2 += (long) count;
              }
              stream2.Close();
            }
            stream1.Close();
          }
        }
        using (ZipFile zipFile = ZipFile.Read(str2))
        {
          foreach (ZipEntry zipEntry in zipFile)
            zipEntry.Extract(baseDirectory, true);
        }
        new WebClient().DownloadFile(str1 + "version.txt", baseDirectory + "version.txt");
        Form1.deleteFile(str2);
        Process.Start("mpatcher.exe");
      }
    }

    private void button4_Click(object sender, EventArgs e)
    {
      Process.Start("Version.txt");
    }

    private void button2_Click(object sender, EventArgs e)
    {
      Process.Start("gamepadconfig.exe");
    }

    private void button5_Click(object sender, EventArgs e)
    {
      Process.Start("rustyheartsConfig.exe");
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Process.Start("www.devourment.wixsite.com/rhdevourment/about-us");
    }

    private void button6_Click(object sender, EventArgs e)
    {
      Process.Start("www.discord.gg/aUXD2gn");
    }

    private void button8_Click(object sender, EventArgs e)
    {
      Process.Start("www.paypal.me/RHDevourement");
    }

    private void button7_Click(object sender, EventArgs e)
    {
      Process.Start("www.rustyheartsdevourment.com");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.progressBar1 = new ProgressBar();
      this.backgroundWorker1 = new BackgroundWorker();
      this.strtGameBtn = new Button();
      this.downloadLbl = new Label();
      this.closeBtn = new Button();
      this.minimizeBtn = new Button();
      this.backgroundWorker2 = new BackgroundWorker();
      this.button2 = new Button();
      this.button3 = new Button();
      this.button4 = new Button();
      this.button5 = new Button();
      this.button1 = new Button();
      this.button6 = new Button();
      this.button7 = new Button();
      this.button8 = new Button();
      this.SuspendLayout();
      this.progressBar1.BackColor = SystemColors.AppWorkspace;
      this.progressBar1.ForeColor = Color.DarkRed;
      this.progressBar1.Location = new Point(168, 355);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(666, 68);
      this.progressBar1.TabIndex = 0;
      this.backgroundWorker1.WorkerReportsProgress = true;
      this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
      this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
      this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
      this.strtGameBtn.BackColor = Color.Transparent;
      this.strtGameBtn.BackgroundImage = (Image) componentResourceManager.GetObject("strtGameBtn.BackgroundImage");
      this.strtGameBtn.BackgroundImageLayout = ImageLayout.Zoom;
      this.strtGameBtn.FlatAppearance.BorderColor = SystemColors.Control;
      this.strtGameBtn.FlatAppearance.BorderSize = 0;
      this.strtGameBtn.FlatStyle = FlatStyle.Popup;
      this.strtGameBtn.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.strtGameBtn.ForeColor = Color.Transparent;
      this.strtGameBtn.Location = new Point(12, 355);
      this.strtGameBtn.Name = "strtGameBtn";
      this.strtGameBtn.Size = new Size(142, 68);
      this.strtGameBtn.TabIndex = 1;
      this.strtGameBtn.TextImageRelation = TextImageRelation.ImageAboveText;
      this.strtGameBtn.UseVisualStyleBackColor = false;
      this.strtGameBtn.Click += new EventHandler(this.strtGameBtn_Click);
      this.downloadLbl.AutoSize = true;
      this.downloadLbl.BackColor = Color.FromArgb(17, 17, 17);
      this.downloadLbl.ForeColor = Color.FromArgb(0, 121, 203);
      this.downloadLbl.ImageAlign = ContentAlignment.MiddleRight;
      this.downloadLbl.Location = new Point(605, 402);
      this.downloadLbl.Name = "downloadLbl";
      this.downloadLbl.RightToLeft = RightToLeft.No;
      this.downloadLbl.Size = new Size(0, 13);
      this.downloadLbl.TabIndex = 2;
      this.closeBtn.BackColor = Color.Transparent;
      this.closeBtn.BackgroundImage = (Image) componentResourceManager.GetObject("closeBtn.BackgroundImage");
      this.closeBtn.BackgroundImageLayout = ImageLayout.Stretch;
      this.closeBtn.FlatAppearance.BorderSize = 0;
      this.closeBtn.FlatStyle = FlatStyle.Flat;
      this.closeBtn.Location = new Point(810, 10);
      this.closeBtn.Name = "closeBtn";
      this.closeBtn.Size = new Size(24, 18);
      this.closeBtn.TabIndex = 3;
      this.closeBtn.UseVisualStyleBackColor = false;
      this.closeBtn.Click += new EventHandler(this.closeBtn_Click);
      this.closeBtn.MouseEnter += new EventHandler(this.closeBtn_MouseEnter);
      this.closeBtn.MouseLeave += new EventHandler(this.closeBtn_MouseLeave);
      this.minimizeBtn.BackColor = Color.Transparent;
      this.minimizeBtn.BackgroundImage = (Image) componentResourceManager.GetObject("minimizeBtn.BackgroundImage");
      this.minimizeBtn.BackgroundImageLayout = ImageLayout.Stretch;
      this.minimizeBtn.FlatAppearance.BorderSize = 0;
      this.minimizeBtn.FlatStyle = FlatStyle.Flat;
      this.minimizeBtn.Location = new Point(780, 10);
      this.minimizeBtn.Name = "minimizeBtn";
      this.minimizeBtn.Size = new Size(24, 18);
      this.minimizeBtn.TabIndex = 4;
      this.minimizeBtn.UseVisualStyleBackColor = false;
      this.minimizeBtn.Click += new EventHandler(this.minimizeBtn_Click);
      this.minimizeBtn.MouseEnter += new EventHandler(this.minimizeBtn_MouseEnter);
      this.minimizeBtn.MouseLeave += new EventHandler(this.minimizeBtn_MouseLeave);
      this.backgroundWorker2.WorkerReportsProgress = true;
      this.button2.BackColor = Color.DarkRed;
      this.button2.BackgroundImage = (Image) componentResourceManager.GetObject("button2.BackgroundImage");
      this.button2.BackgroundImageLayout = ImageLayout.Center;
      this.button2.FlatAppearance.BorderColor = Color.FromArgb(0, 121, 203);
      this.button2.FlatAppearance.BorderSize = 0;
      this.button2.FlatStyle = FlatStyle.Popup;
      this.button2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.button2.ForeColor = Color.Transparent;
      this.button2.Location = new Point(459, 133);
      this.button2.Name = "button2";
      this.button2.Size = new Size(151, 48);
      this.button2.TabIndex = 12;
      this.button2.TextImageRelation = TextImageRelation.ImageAboveText;
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button3.BackColor = Color.DarkRed;
      this.button3.BackgroundImage = (Image) componentResourceManager.GetObject("button3.BackgroundImage");
      this.button3.BackgroundImageLayout = ImageLayout.Center;
      this.button3.FlatAppearance.BorderColor = Color.FromArgb(0, 121, 203);
      this.button3.FlatAppearance.BorderSize = 0;
      this.button3.FlatStyle = FlatStyle.Popup;
      this.button3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.button3.ForeColor = Color.Transparent;
      this.button3.Location = new Point(459, 70);
      this.button3.Name = "button3";
      this.button3.Size = new Size(151, 48);
      this.button3.TabIndex = 13;
      this.button3.TextImageRelation = TextImageRelation.ImageAboveText;
      this.button3.UseVisualStyleBackColor = false;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button4.BackColor = Color.DarkRed;
      this.button4.BackgroundImage = (Image) componentResourceManager.GetObject("button4.BackgroundImage");
      this.button4.BackgroundImageLayout = ImageLayout.Center;
      this.button4.FlatAppearance.BorderColor = Color.FromArgb(0, 121, 203);
      this.button4.FlatAppearance.BorderSize = 0;
      this.button4.FlatStyle = FlatStyle.Popup;
      this.button4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.button4.ForeColor = Color.Transparent;
      this.button4.Location = new Point(645, 70);
      this.button4.Name = "button4";
      this.button4.Size = new Size(151, 48);
      this.button4.TabIndex = 14;
      this.button4.TextImageRelation = TextImageRelation.ImageAboveText;
      this.button4.UseVisualStyleBackColor = false;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.button5.BackColor = Color.DarkRed;
      this.button5.BackgroundImage = (Image) componentResourceManager.GetObject("button5.BackgroundImage");
      this.button5.BackgroundImageLayout = ImageLayout.Center;
      this.button5.FlatAppearance.BorderColor = Color.FromArgb(0, 121, 203);
      this.button5.FlatAppearance.BorderSize = 0;
      this.button5.FlatStyle = FlatStyle.Popup;
      this.button5.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.button5.ForeColor = Color.Transparent;
      this.button5.Location = new Point(459, 197);
      this.button5.Name = "button5";
      this.button5.Size = new Size(151, 48);
      this.button5.TabIndex = 15;
      this.button5.TextImageRelation = TextImageRelation.ImageAboveText;
      this.button5.UseVisualStyleBackColor = false;
      this.button5.Click += new EventHandler(this.button5_Click);
      this.button1.BackColor = Color.DarkRed;
      this.button1.BackgroundImageLayout = ImageLayout.Zoom;
      this.button1.FlatStyle = FlatStyle.Popup;
      this.button1.Image = (Image) componentResourceManager.GetObject("button1.Image");
      this.button1.Location = new Point(645, 133);
      this.button1.Name = "button1";
      this.button1.Size = new Size(152, 48);
      this.button1.TabIndex = 16;
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button6.BackColor = Color.DarkRed;
      this.button6.BackgroundImageLayout = ImageLayout.Zoom;
      this.button6.FlatStyle = FlatStyle.Popup;
      this.button6.Image = (Image) componentResourceManager.GetObject("button6.Image");
      this.button6.Location = new Point(645, 197);
      this.button6.Name = "button6";
      this.button6.Size = new Size(150, 49);
      this.button6.TabIndex = 17;
      this.button6.UseVisualStyleBackColor = false;
      this.button6.Click += new EventHandler(this.button6_Click);
      this.button7.BackColor = Color.DarkRed;
      this.button7.BackgroundImageLayout = ImageLayout.Zoom;
      this.button7.FlatAppearance.BorderSize = 0;
      this.button7.FlatStyle = FlatStyle.Popup;
      this.button7.Image = (Image) componentResourceManager.GetObject("button7.Image");
      this.button7.Location = new Point(459, 261);
      this.button7.Name = "button7";
      this.button7.Size = new Size(151, 47);
      this.button7.TabIndex = 18;
      this.button7.UseVisualStyleBackColor = false;
      this.button7.Click += new EventHandler(this.button7_Click);
      this.button8.BackColor = Color.DarkRed;
      this.button8.BackgroundImageLayout = ImageLayout.Zoom;
      this.button8.FlatAppearance.BorderSize = 0;
      this.button8.FlatAppearance.MouseDownBackColor = Color.White;
      this.button8.FlatAppearance.MouseOverBackColor = Color.White;
      this.button8.FlatStyle = FlatStyle.Popup;
      this.button8.Image = (Image) componentResourceManager.GetObject("button8.Image");
      this.button8.Location = new Point(645, 259);
      this.button8.Name = "button8";
      this.button8.Size = new Size(150, 48);
      this.button8.TabIndex = 19;
      this.button8.UseVisualStyleBackColor = false;
      this.button8.Click += new EventHandler(this.button8_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Black;
      this.BackgroundImage = (Image) componentResourceManager.GetObject("$this.BackgroundImage");
      this.BackgroundImageLayout = ImageLayout.None;
      this.ClientSize = new Size(844, 431);
      this.Controls.Add((Control) this.button8);
      this.Controls.Add((Control) this.button7);
      this.Controls.Add((Control) this.button6);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.button5);
      this.Controls.Add((Control) this.button4);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.minimizeBtn);
      this.Controls.Add((Control) this.closeBtn);
      this.Controls.Add((Control) this.downloadLbl);
      this.Controls.Add((Control) this.strtGameBtn);
      this.Controls.Add((Control) this.progressBar1);
      this.DoubleBuffered = true;
      this.ForeColor = Color.Transparent;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Form1);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "RustyHeartsLauncher";
      this.Load += new EventHandler(this.Form1_Load);
      this.MouseDown += new MouseEventHandler(this.Form1_MouseDown);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
