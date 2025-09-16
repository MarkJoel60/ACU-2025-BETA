// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.InstallationPolicy
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;

#nullable disable
namespace PX.Data.Update;

public static class InstallationPolicy
{
  private const string SlotName = "InstallationPolicy";

  public static bool UpdateEnabled => InstallationPolicy.Definitions.updateEnabled;

  public static string UpdateServer => InstallationPolicy.Definitions.updateServer;

  public static string UpdateServerAlternative
  {
    get => InstallationPolicy.Definitions.updateServerAlternative;
  }

  public static bool UpdateAlternativeEnabled
  {
    get => InstallationPolicy.Definitions.updateAlternativeEnabled;
  }

  public static bool UpdateNotification => InstallationPolicy.Definitions.updateNotification;

  public static string LicensingServer => InstallationPolicy.Definitions.licensingServer;

  public static string ISVUpdateEndpoint => InstallationPolicy.Definitions.iSVUpdateEndpoint;

  private static InstallationPolicy.Definition Definitions
  {
    get
    {
      return PXDatabase.GetSlot<InstallationPolicy.Definition>(nameof (InstallationPolicy), typeof (UPSetup), typeof (UPStorageParameters)) ?? InstallationPolicy.Definition.Default;
    }
  }

  public static void Clear()
  {
    PXDatabase.ResetSlot<InstallationPolicy.Definition>(nameof (InstallationPolicy), typeof (UPSetup), typeof (UPStorageParameters));
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    public static readonly InstallationPolicy.Definition Default = new InstallationPolicy.Definition();
    public bool updateEnabled;
    public string updateServer;
    public string updateServerAlternative;
    public bool updateAlternativeEnabled;
    public bool updateNotification;
    public string licensingServer;
    public string iSVUpdateEndpoint;

    public void Prefetch()
    {
      using (new PXConnectionScope())
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<UPSetup>(new PXDataField("UpdateEnabled"), new PXDataField("UpdateServer"), new PXDataField("UpdateServerAlternative"), new PXDataField("UpdateAlternativeEnabled"), new PXDataField("UpdateNotification"), new PXDataField("UpdateElevated"), new PXDataField("LicensingServer"), new PXDataField("ISVUpdateEndpoint")))
        {
          if (pxDataRecord == null)
            return;
          this.updateEnabled = pxDataRecord.GetBoolean(0).GetValueOrDefault();
          this.updateServer = pxDataRecord.GetString(1);
          this.updateServerAlternative = pxDataRecord.GetString(2);
          bool? boolean = pxDataRecord.GetBoolean(3);
          this.updateAlternativeEnabled = boolean.GetValueOrDefault();
          boolean = pxDataRecord.GetBoolean(4);
          this.updateNotification = boolean.GetValueOrDefault();
          this.licensingServer = pxDataRecord.GetString(6);
          this.iSVUpdateEndpoint = pxDataRecord.GetString(7);
        }
      }
    }
  }
}
