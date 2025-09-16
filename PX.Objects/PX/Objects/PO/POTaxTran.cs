// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("PO Tax")]
[Serializable]
public class POTaxTran : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected int? _RecordID;
  protected string _JurisType;
  protected string _JurisName;
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;
  public const int LineNbrValue = 2147483647 /*0x7FFFFFFF*/;

  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDBDefault(typeof (POOrder.orderType), DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Order Type", Enabled = false, Visible = false)]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXDBDefault(typeof (POOrder.orderNbr), DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false, Visible = false)]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(2147483647 /*0x7FFFFFFF*/)]
  [PXUIField]
  [PXParent(typeof (POTaxTran.FK.Order))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PX.Objects.TX.TaxID]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), IsDirty = true, DirtyRead = true)]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
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
  [CurrencyInfo(typeof (POOrder.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (POTaxTran.curyInfoID), typeof (POTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Switch<Case<Where<WhereExempt<POTaxTran.taxID>>, POTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<POOrder.curyVatExemptTotal>), ValidateAggregateCalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<WhereTaxable<POTaxTran.taxID>>, POTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<POOrder.curyVatTaxableTotal>), ValidateAggregateCalculation = true)]
  public virtual Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  [PXDBCurrency(typeof (POTaxTran.curyInfoID), typeof (POTaxTran.unbilledTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryUnbilledTaxableAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledTaxableAmt { get; set; }

  /// <summary>The unbilled taxable quantity for per unit taxes.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Taxable Qty.", Enabled = false)]
  public virtual Decimal? UnbilledTaxableQty { get; set; }

  [PXDBCurrency(typeof (POTaxTran.curyInfoID), typeof (POTaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBCurrency(typeof (POTaxTran.curyInfoID), typeof (POTaxTran.unbilledTaxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryUnbilledTaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt
  {
    get => this._TaxAmt;
    set => this._TaxAmt = value;
  }

  [PXDBCurrency(typeof (POTaxTran.curyInfoID), typeof (POTaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledTaxAmt { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string TaxZoneID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (TaxTran.curyInfoID), typeof (TaxTran.retainedTaxableAmt))]
  [PXUIField(DisplayName = "Retained Taxable", FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainedTaxableAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Retained Taxable", FieldClass = "Retainage")]
  public virtual Decimal? RetainedTaxableAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POTaxTran.curyInfoID), typeof (POTaxTran.retainedTaxAmt))]
  [PXUIField(DisplayName = "Retained Tax", FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainedTaxAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Retained Tax", FieldClass = "Retainage")]
  public virtual Decimal? RetainedTaxAmt { get; set; }

  [PXDBTimestamp(RecordComesFirst = true)]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// A Boolean value that specifies that the tax transaction is tax inclusive or not
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Inclusive")]
  public virtual bool? IsTaxInclusive { get; set; }

  public class PK : 
    PrimaryKeyOf<POTaxTran>.By<POTaxTran.recordID, POTaxTran.orderType, POTaxTran.orderNbr, POTaxTran.lineNbr, POTaxTran.taxID>
  {
    public static POTaxTran Find(
      PXGraph graph,
      int? recordID,
      string orderType,
      string orderNbr,
      int? lineNbr,
      string taxID)
    {
      return POTaxTran.PK.Find(graph, recordID, orderType, orderNbr, lineNbr, taxID);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POTaxTran>.By<POTaxTran.orderType, POTaxTran.orderNbr>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<POLine>.By<POLine.orderType, POLine.orderNbr, POLine.lineNbr>.ForeignKeyOf<POTaxTran>.By<POTaxTran.orderType, POTaxTran.orderNbr, POTaxTran.lineNbr>
    {
    }

    public class POTax : 
      PrimaryKeyOf<POTax>.By<POTax.orderType, POTax.orderNbr, POTax.lineNbr, POTax.taxID>.ForeignKeyOf<POTaxTran>.By<POTaxTran.orderType, POTaxTran.orderNbr, POTaxTran.lineNbr, POTaxTran.taxID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<POTaxTran>.By<POTaxTran.taxID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POTaxTran>.By<POTaxTran.curyInfoID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<POTaxTran>.By<POTaxTran.taxZoneID>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTaxTran.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTaxTran.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POTaxTran.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTaxTran.taxID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POTaxTran.recordID>
  {
  }

  public abstract class jurisType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTaxTran.jurisType>
  {
  }

  public abstract class jurisName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTaxTran.jurisName>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTaxTran.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POTaxTran.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.curyTaxableAmt>
  {
  }

  public abstract class curyUnbilledTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.curyUnbilledTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTaxTran.taxableAmt>
  {
  }

  public abstract class unbilledTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.unbilledTaxableAmt>
  {
  }

  /// <summary>The unbilled taxable quantity for per unit taxes.</summary>
  public abstract class unbilledTaxableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.unbilledTaxableQty>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTaxTran.curyTaxAmt>
  {
  }

  public abstract class curyUnbilledTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.curyUnbilledTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTaxTran.taxAmt>
  {
  }

  public abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.nonDeductibleTaxRate>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTaxTran.expenseAmt>
  {
  }

  public abstract class unbilledTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.unbilledTaxAmt>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTaxTran.taxZoneID>
  {
  }

  public abstract class curyRetainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.curyRetainedTaxableAmt>
  {
  }

  public abstract class retainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.retainedTaxableAmt>
  {
  }

  public abstract class curyRetainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.curyRetainedTaxAmt>
  {
  }

  public abstract class retainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTran.retainedTaxAmt>
  {
  }

  public class lineNbrValue : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  POTaxTran.lineNbrValue>
  {
    public lineNbrValue()
      : base(int.MaxValue)
    {
    }
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POTaxTran.Tstamp>
  {
  }

  public abstract class isTaxInclusive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POTaxTran.isTaxInclusive>
  {
  }
}
