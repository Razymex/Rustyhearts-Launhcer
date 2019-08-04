// Decompiled with JetBrains decompiler
// Type: Launcher_v2.Properties.Resources
// Assembly: RustyHeartsLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b4b86969018abd94
// MVID: 4B0ED571-701E-464E-AF26-7DF82DDA673B
// Assembly location: F:\Temp\RustyHearts_Devourment_Beta\RustyHeartsLauncher.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Launcher_v2.Properties
{
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [CompilerGenerated]
  public class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Launcher_v2.Properties.Resources.resourceMan, (object) null))
          Launcher_v2.Properties.Resources.resourceMan = new ResourceManager("Launcher_v2.Properties.Resources", typeof (Launcher_v2.Properties.Resources).Assembly);
        return Launcher_v2.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
      get
      {
        return Launcher_v2.Properties.Resources.resourceCulture;
      }
      set
      {
        Launcher_v2.Properties.Resources.resourceCulture = value;
      }
    }

    public static Bitmap close1
    {
      get
      {
        return (Bitmap) Launcher_v2.Properties.Resources.ResourceManager.GetObject(nameof (close1), Launcher_v2.Properties.Resources.resourceCulture);
      }
    }

    public static Bitmap close2
    {
      get
      {
        return (Bitmap) Launcher_v2.Properties.Resources.ResourceManager.GetObject(nameof (close2), Launcher_v2.Properties.Resources.resourceCulture);
      }
    }

    public static Bitmap Config_Hover
    {
      get
      {
        return (Bitmap) Launcher_v2.Properties.Resources.ResourceManager.GetObject("Config Hover", Launcher_v2.Properties.Resources.resourceCulture);
      }
    }

    public static Bitmap dfgdfg
    {
      get
      {
        return (Bitmap) Launcher_v2.Properties.Resources.ResourceManager.GetObject(nameof (dfgdfg), Launcher_v2.Properties.Resources.resourceCulture);
      }
    }

    public static Icon favicon__1_
    {
      get
      {
        return (Icon) Launcher_v2.Properties.Resources.ResourceManager.GetObject(nameof (favicon__1_), Launcher_v2.Properties.Resources.resourceCulture);
      }
    }

    public static byte[] Ionic_Zip
    {
      get
      {
        return (byte[]) Launcher_v2.Properties.Resources.ResourceManager.GetObject(nameof (Ionic_Zip), Launcher_v2.Properties.Resources.resourceCulture);
      }
    }

    public static Bitmap minimize1
    {
      get
      {
        return (Bitmap) Launcher_v2.Properties.Resources.ResourceManager.GetObject(nameof (minimize1), Launcher_v2.Properties.Resources.resourceCulture);
      }
    }

    public static Bitmap minimize2
    {
      get
      {
        return (Bitmap) Launcher_v2.Properties.Resources.ResourceManager.GetObject(nameof (minimize2), Launcher_v2.Properties.Resources.resourceCulture);
      }
    }

    public static Bitmap start1
    {
      get
      {
        return (Bitmap) Launcher_v2.Properties.Resources.ResourceManager.GetObject(nameof (start1), Launcher_v2.Properties.Resources.resourceCulture);
      }
    }
  }
}
