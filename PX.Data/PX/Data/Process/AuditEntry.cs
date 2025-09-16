// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.AuditEntry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data.Process;

[Serializable]
public class AuditEntry : Dictionary<string, AuditValue>
{
  public AuditBatch Batch { get; private set; }

  public string Key { get; set; }

  public string Table { get; set; }

  public AuditOperation Operation { get; set; }

  public string OperationTitle { get; set; }

  public object Row { get; set; }

  public AuditEntry(AuditBatch batch) => this.Batch = batch;

  protected AuditEntry(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<AuditEntry>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<AuditEntry>(this, info);
    base.GetObjectData(info, context);
  }
}
