// Decompiled with JetBrains decompiler
// Type: Launcher_v2.Program
// Assembly: RustyHeartsLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b4b86969018abd94
// MVID: 4B0ED571-701E-464E-AF26-7DF82DDA673B
// Assembly location: F:\Temp\RustyHearts_Devourment_Beta\RustyHeartsLauncher.exe

using System;
using System.Windows.Forms;

namespace Launcher_v2
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}
