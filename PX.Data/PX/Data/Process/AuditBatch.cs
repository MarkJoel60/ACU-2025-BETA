// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.AuditBatch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Process;

[Serializable]
public class AuditBatch : List<AuditEntry>
{
  public AuditInfo Info { get; private set; }

  public long Batch { get; set; }

  public string[] Screens { get; set; }

  public string Username { get; set; }

  public System.DateTime? Date { get; set; }

  public AuditBatch(AuditInfo info) => this.Info = info;

  public AuditEntry Add()
  {
    AuditEntry auditEntry = new AuditEntry(this);
    this.Add(auditEntry);
    return auditEntry;
  }
}
