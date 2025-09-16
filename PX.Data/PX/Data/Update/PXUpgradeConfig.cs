// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpgradeConfig
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics;

#nullable disable
namespace PX.Data.Update;

[Serializable]
public class PXUpgradeConfig : PXUpgradeSpec
{
  public bool IsErp { get; set; }

  public int IISVersion { get; set; }

  public Version ASPVersion { get; set; }

  public string RootFolder { get; set; }

  public string SiteUrl { get; set; }

  public Action<EventLogEntryType, string, Exception> MessageCallback { get; set; }
}
