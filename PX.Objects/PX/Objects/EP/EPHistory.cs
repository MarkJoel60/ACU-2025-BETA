// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

[EPAccum]
[PXHidden]
[Serializable]
public class EPHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _EmployeeID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected Decimal? _FinPtdClaimed;
  protected Decimal? _TranPtdClaimed;
  protected Decimal? _FinYtdClaimed;
  protected Decimal? _TranYtdClaimed;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBString(6, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdClaimed
  {
    get => this._FinPtdClaimed;
    set => this._FinPtdClaimed = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdClaimed
  {
    get => this._TranPtdClaimed;
    set => this._TranPtdClaimed = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdClaimed
  {
    get => this._FinYtdClaimed;
    set => this._FinYtdClaimed = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdClaimed
  {
    get => this._TranYtdClaimed;
    set => this._TranYtdClaimed = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPHistory.employeeID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPHistory.finPeriodID>
  {
  }

  public abstract class finPtdClaimed : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPHistory.finPtdClaimed>
  {
  }

  public abstract class tranPtdClaimed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPHistory.tranPtdClaimed>
  {
  }

  public abstract class finYtdClaimed : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPHistory.finYtdClaimed>
  {
  }

  public abstract class tranYtdClaimed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPHistory.tranYtdClaimed>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPHistory.Tstamp>
  {
  }
}
