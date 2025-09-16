// Decompiled with JetBrains decompiler
// Type: PX.Common.Config.TSScreenInfoOptions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

#nullable disable
namespace PX.Common.Config;

[PXInternalUseOnly]
public class TSScreenInfoOptions
{
  private string \u0002 = "App_Data/TSScreenInfo";
  private string \u000E = "App_Data/TSWrappers";

  public string ScreenInfoPath
  {
    get => this.\u0002;
    set => this.\u0002 = value;
  }

  public string WrappersInfoPath
  {
    get => this.\u000E;
    set => this.\u000E = value;
  }
}
