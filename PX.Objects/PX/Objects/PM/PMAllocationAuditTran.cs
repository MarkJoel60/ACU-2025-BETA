// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAllocationAuditTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("PM Allocation Audit Transaction")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMAllocationAuditTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _AllocationID;
  protected long? _TranID;
  protected long? _SourceTranID;

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDefault]
  public virtual string AllocationID
  {
    get => this._AllocationID;
    set => this._AllocationID = value;
  }

  [PXDBLong(IsKey = true)]
  [PXDBChildIdentity(typeof (PMTran.tranID))]
  public virtual long? TranID
  {
    get => this._TranID;
    set => this._TranID = value;
  }

  [PXDBLong(IsKey = true)]
  [PXDBChildIdentity(typeof (PMTran.tranID))]
  public virtual long? SourceTranID
  {
    get => this._SourceTranID;
    set => this._SourceTranID = value;
  }

  public abstract class allocationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationAuditTran.allocationID>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMAllocationAuditTran.tranID>
  {
  }

  public abstract class sourceTranID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    PMAllocationAuditTran.sourceTranID>
  {
  }
}
