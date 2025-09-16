// Decompiled with JetBrains decompiler
// Type: PX.SM.SFSyncRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <summary>Dummy DAC for PX.Salesforce.SFSyncRecord</summary>
[PXHidden]
[Serializable]
public class SFSyncRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  public virtual int? SyncRecordID { get; set; }

  public abstract class syncRecordID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  SFSyncRecord.syncRecordID>
  {
  }
}
