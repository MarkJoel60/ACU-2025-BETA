// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAllocationSourceTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("PM Allocation Source Transaction")]
[DebuggerDisplay("Rate={Rate} Qty={Qty} Amount={Amount} Description={Description}")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMAllocationSourceTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _AllocationID;
  protected int? _StepID;
  protected long? _TranID;
  protected Decimal? _Rate;
  protected Decimal? _Qty;
  protected Decimal? _Amount;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Allocation ID")]
  public virtual string AllocationID
  {
    get => this._AllocationID;
    set => this._AllocationID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? StepID
  {
    get => this._StepID;
    set => this._StepID = value;
  }

  [PXDBLong(IsKey = true)]
  [PXDBChildIdentity(typeof (PMTran.tranID))]
  public virtual long? TranID
  {
    get => this._TranID;
    set => this._TranID = value;
  }

  [PXDBDecimal(19)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Rate
  {
    get => this._Rate;
    set => this._Rate = value;
  }

  [PXDBDecimal(19)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBDecimal(19)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Amount
  {
    get => this._Amount;
    set => this._Amount = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class allocationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationSourceTran.allocationID>
  {
  }

  public abstract class stepID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationSourceTran.stepID>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMAllocationSourceTran.tranID>
  {
  }

  public abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMAllocationSourceTran.rate>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMAllocationSourceTran.qty>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMAllocationSourceTran.amount>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMAllocationSourceTran.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMAllocationSourceTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationSourceTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMAllocationSourceTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMAllocationSourceTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationSourceTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMAllocationSourceTran.lastModifiedDateTime>
  {
  }
}
