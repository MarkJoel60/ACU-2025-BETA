// Decompiled with JetBrains decompiler
// Type: PX.Common.PXInvariantScope
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

#nullable disable
namespace PX.Common;

[PXInternalUseOnly]
public class PXInvariantScope : PXInvariantCultureScope
{
  private readonly PXTimeZoneInfo \u0002;

  public PXInvariantScope()
  {
    this.\u0002 = LocaleInfo.GetTimeZone();
    LocaleInfo.SetTimeZone(PXTimeZoneInfo.Local);
  }

  public override void Dispose() => LocaleInfo.SetTimeZone(this.\u0002);
}
