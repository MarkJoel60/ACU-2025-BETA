// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.CAAdj
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA.Light;

[PXHidden]
[Serializable]
public class CAAdj : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _tstamp;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public virtual string AdjRefNbr { get; set; }

  [PXDBString(3, IsFixed = true)]
  public virtual string AdjTranType { get; set; }

  [PXDBInt]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(1, IsFixed = true)]
  public virtual string DrCr { get; set; }

  [PXDBBool]
  public virtual bool? Released { get; set; }

  [PXDBString(40, IsUnicode = true)]
  public virtual string ExtRefNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  public virtual string Status { get; set; }

  [PXDBLong]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  public virtual string CuryID { get; set; }

  [PXDBDecimal(4)]
  public virtual Decimal? TranAmt { get; set; }

  [PXDBDecimal(4)]
  public virtual Decimal? CuryTranAmt { get; set; }

  [PXDBDate]
  public virtual DateTime? TranDate { get; set; }

  [PXDBBool]
  public virtual bool? DepositAsBatch { get; set; }

  [PXDBDate]
  public virtual DateTime? DepositAfter { get; set; }

  [PXDBBool]
  public virtual bool? Deposited { get; set; }

  [PXDBString(3, IsFixed = true)]
  public virtual string DepositType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string DepositNbr { get; set; }

  [PXDBTimestamp(RecordComesFirst = true)]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class adjRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.adjRefNbr>
  {
  }

  public abstract class adjTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.adjTranType>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAAdj.cashAccountID>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.drCr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.released>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.extRefNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.status>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CAAdj.curyInfoID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.curyID>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.tranAmt>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAAdj.curyTranAmt>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAAdj.tranDate>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.depositAsBatch>
  {
  }

  public abstract class depositAfter : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAAdj.depositAfter>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAAdj.deposited>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAAdj.depositNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CAAdj.Tstamp>
  {
  }
}
