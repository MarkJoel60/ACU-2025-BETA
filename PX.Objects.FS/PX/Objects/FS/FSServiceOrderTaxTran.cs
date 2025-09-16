// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceOrderTaxTran
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Service Order Tax Detail")]
[Serializable]
public class FSServiceOrderTaxTran : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected 
  #nullable disable
  string _JurisType;
  protected string _JurisName;
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;
  protected byte[] _tstamp;

  [PXDBString(4, IsFixed = true, IsKey = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSServiceOrder.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXUIField(DisplayName = "Service Order Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSServiceOrder.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSServiceOrderTaxTran.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSServiceOrderTaxTran.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PX.Objects.TX.TaxID]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true)]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBString(9, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Type")]
  public virtual string JurisType
  {
    get => this._JurisType;
    set => this._JurisType = value;
  }

  [PXDBString(200, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Name")]
  public virtual string JurisName
  {
    get => this._JurisName;
    set => this._JurisName = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (FSServiceOrder.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (FSServiceOrderTaxTran.curyInfoID), typeof (FSServiceOrderTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Switch<Case<Where<WhereExempt<FSServiceOrderTaxTran.taxID>>, FSServiceOrderTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<FSServiceOrder.curyVatExemptTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<WhereTaxable<FSServiceOrderTaxTran.taxID>>, FSServiceOrderTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<FSServiceOrder.curyVatTaxableTotal>))]
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

  [PXDBCurrency(typeof (FSServiceOrderTaxTran.curyInfoID), typeof (FSServiceOrderTaxTran.taxAmt))]
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

  /// <summary>
  /// The reference to the tax zone (<see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" />).
  /// </summary>
  [PXString(10, IsUnicode = true)]
  public virtual string TaxZoneID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <summary>
  /// A Boolean value that specifies that the tax transaction is tax inclusive or not
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Inclusive")]
  public virtual bool? IsTaxInclusive { get; set; }

  public class PK : 
    PrimaryKeyOf<FSServiceOrderTaxTran>.By<FSServiceOrderTaxTran.srvOrdType, FSServiceOrderTaxTran.refNbr, FSServiceOrderTaxTran.recordID, FSServiceOrderTaxTran.taxID>
  {
    public static FSServiceOrderTaxTran Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? recordID,
      string taxID,
      PKFindOptions options = 0)
    {
      return (FSServiceOrderTaxTran) PrimaryKeyOf<FSServiceOrderTaxTran>.By<FSServiceOrderTaxTran.srvOrdType, FSServiceOrderTaxTran.refNbr, FSServiceOrderTaxTran.recordID, FSServiceOrderTaxTran.taxID>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) recordID, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentTax>.By<FSServiceOrderTaxTran.srvOrdType>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSAppointmentTax>.By<FSServiceOrderTaxTran.srvOrdType, FSServiceOrderTaxTran.refNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSAppointmentTax>.By<FSServiceOrderTaxTran.curyInfoID>
    {
    }

    public class Tax : 
      PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<FSAppointmentTax>.By<FSServiceOrderTaxTran.taxID>
    {
    }
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderTaxTran.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrderTaxTran.refNbr>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceOrderTaxTran.recordID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceOrderTaxTran.taxID>
  {
  }

  public abstract class jurisType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderTaxTran.jurisType>
  {
  }

  public abstract class jurisName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderTaxTran.jurisName>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrderTaxTran.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSServiceOrderTaxTran.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderTaxTran.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderTaxTran.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderTaxTran.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceOrderTaxTran.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSServiceOrderTaxTran.curyExpenseAmt>
  {
  }

  public abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceOrderTaxTran.taxZoneID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSServiceOrderTaxTran.Tstamp>
  {
  }

  public abstract class isTaxInclusive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSServiceOrderTaxTran.isTaxInclusive>
  {
  }
}
