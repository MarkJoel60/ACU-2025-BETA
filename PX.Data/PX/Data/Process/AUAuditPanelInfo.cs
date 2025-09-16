// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.AUAuditPanelInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Process;

[Serializable]
public class AUAuditPanelInfo
{
  public string CreatedByID { get; set; }

  public string CreatedByScreenID { get; set; }

  public System.DateTime? CreatedDateTime { get; set; }

  public string LastModifiedByID { get; set; }

  public string LastModifiedByScreenID { get; set; }

  public System.DateTime? LastModifiedDateTime { get; set; }
}
