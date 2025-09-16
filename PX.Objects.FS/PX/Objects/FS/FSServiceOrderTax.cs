// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceOrderTax
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Service Order Tax")]
[Serializable]
public class FSServiceOrderTax : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;
  protected 
  #nullable disable
  byte[] _tstamp;

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSServiceOrder.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Service Order Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSServiceOrder.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSServiceOrderTax.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSServiceOrderTax.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (FSSODet.lineNbr))]
  [PXUIField]
  [PXParent(typeof (Select<FSSODet, Where<FSSODet.srvOrdType, Equal<Current<FSServiceOrderTax.srvOrdType>>, And<FSSODet.refNbr, Equal<Current<FSServiceOrderTax.refNbr>>, And<FSSODet.lineNbr, Equal<Current<FSServiceOrderTax.lineNbr>>>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true)]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (FSServiceOrder.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (FSServiceOrderTax.curyInfoID), typeof (FSServiceOrderTax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  [PXDBCurrency(typeof (FSServiceOrderTax.curyInfoID), typeof (FSServiceOrderTax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt
  {
    get => this._TaxAmt;
    set => this._TaxAmt = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<FSServiceOrderTax>.By<FSServiceOrderTax.srvOrdType, FSServiceOrderTax.refNbr, FSServiceOrderTax.lineNbr, FSServiceOrderTax.taxID>
  {
    public static FSServiceOrderTax Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      string taxID,
      PKFindOptions options = 0)
    {
      return (FSServiceOrderTax) PrimaryKeyOf<FSServiceOrderTax>.By<FSServiceOrderTax.srvOrdType, FSServiceOrderTax.refNbr, FSServiceOrderTax.lineNbr, FSServiceOrderTax.taxID>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentTax>.By<FSServiceOrderTax.srvOrdType>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSAppointmentTax>.By<FSServiceOrderTax.srvOrdType, FSServiceOrderTax.refNbr>
    {
    }

    public class ServiceOrderLine : 
      PrimaryKeyOf<FSSODet>.By<FSSODet.srvOrdType, FSSODet.refNbr, FSSODet.lineNbr>.ForeignKeyOf<FSAppointmentTax>.By<FSServiceOrderTax.srvOrdType, FSServiceOrderTax.refNbr, FSServiceOrderTax.lineNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSAppointmentTax>.By<FSServiceOrderTax.curyInfoID>
    {
    }

    public class Tax : 
      PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<FSAppointmentTax>.By<FSServiceOrderTax.taxID>
    {
    }
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrderTax.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrderTax.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrderTax.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrderTax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrderTax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSServiceOrderTax.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderTax.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderTax.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderTax.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrderTax.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderTax.curyExpenseAmt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSServiceOrderTax.Tstamp>
  {
  }
}
