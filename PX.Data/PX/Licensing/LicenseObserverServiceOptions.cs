// Decompiled with JetBrains decompiler
// Type: PX.Licensing.LicenseObserverServiceOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Licensing;

internal class LicenseObserverServiceOptions
{
  public string LicenseThreadPeriod { get; set; }

  public int? ParsedLicenseThreadPeriod
  {
    get
    {
      int result;
      return !int.TryParse(this.LicenseThreadPeriod, out result) ? new int?() : new int?(result);
    }
  }
}
