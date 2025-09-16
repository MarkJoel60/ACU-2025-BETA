// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.AuditInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Process;

[Serializable]
public class AuditInfo : List<AuditBatch>
{
  public string Table { get; private set; }

  public List<AuditValue> Keys { get; private set; }

  public AUAuditPanelInfo Panel { get; internal set; }

  public bool ChangesLimitReached { get; internal set; }

  public string ScreenId { get; internal set; }

  public AuditInfo(string table)
  {
    this.Table = table;
    this.Keys = new List<AuditValue>();
  }

  public AuditBatch Add()
  {
    AuditBatch auditBatch = new AuditBatch(this);
    this.Add(auditBatch);
    return auditBatch;
  }
}
