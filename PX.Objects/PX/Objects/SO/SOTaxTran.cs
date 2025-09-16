// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// Represents tax details at the sales order document level, that is, information on all individual taxes applied
/// to the document lines.
/// </summary>
/// <remarks>
/// It may be an aggregation of the appropriate SOTax records in case of internal tax calculation, or when data is
/// calculated externally.
/// The records of this type are created and edited on the <i>Sales Orders (SO301000)</i> form (corresponding to
/// the <see cref="T:PX.Objects.SO.SOOrderEntry" /> graph).
/// </remarks>
[PXCacheName("Sales Order Tax")]
[Serializable]
public class SOTaxTran : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
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
  protected Decimal? _CuryUnshippedTaxableAmt;
  protected Decimal? _CuryUnbilledTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _UnshippedTaxableAmt;
  protected Decimal? _UnbilledTaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _CuryUnshippedTaxAmt;
  protected Decimal? _CuryUnbilledTaxAmt;
  protected Decimal? _TaxAmt;
  protected Decimal? _UnshippedTaxAmt;
  protected Decimal? _UnbilledTaxAmt;
  protected string _TaxZoneID;

  /// <summary>
  /// The type of the sales order in which this tax item is listed.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOTaxTran.FK.Order" />, the field is a part of the identifier of the sales order
  /// <see cref="T:PX.Objects.SO.SOOrder" />.<see cref="T:PX.Objects.SO.SOOrder.orderType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOTaxTran.FK.OrderLine" />, the field is a part of the identifier of the sales order line
  /// <see cref="T:PX.Objects.SO.SOLine" />.<see cref="T:PX.Objects.SO.SOLine.orderType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOTaxTran.FK.SOTax" />, the field is a part of the identifier of the parent sales order's tax line
  /// <see cref="T:PX.Objects.SO.SOTax" />.<see cref="T:PX.Objects.SO.SOTax.orderType" /></item>
  /// </list>
  /// </remarks>
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDBDefault(typeof (SOOrder.orderType))]
  [PXUIField(DisplayName = "Order Type", Enabled = false, Visible = false)]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  /// <summary>
  /// The reference number of the sales order in which this tax item is listed.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOTaxTran.FK.Order" />, the field is a part of the identifier of the sales order
  /// <see cref="T:PX.Objects.SO.SOOrder" />.<see cref="T:PX.Objects.SO.SOOrder.orderNbr" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOTaxTran.FK.OrderLine" />, the field is a part of the identifier of the sales order line
  /// <see cref="T:PX.Objects.SO.SOLine" />.<see cref="T:PX.Objects.SO.SOLine.orderNbr" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOTaxTran.FK.SOTax" />, the field is a part of the identifier of the parent sales order's tax line
  /// <see cref="T:PX.Objects.SO.SOTax" />.<see cref="T:PX.Objects.SO.SOTax.orderNbr" /></item>
  /// </list>
  /// </remarks>
  [PXDBString(15, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false, Visible = false)]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  /// <summary>The line number of this tax item.</summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOTaxTran.FK.OrderLine" />, the field is a part of the identifier of the sales order line
  /// <see cref="T:PX.Objects.SO.SOLine" />.<see cref="T:PX.Objects.SO.SOLine.lineNbr" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOTaxTran.FK.SOTax" />, the field is a part of the identifier of the parent sales order's tax line
  /// <see cref="T:PX.Objects.SO.SOTax" />.<see cref="T:PX.Objects.SO.SOTax.lineNbr" /></item>
  /// </list>
  /// </remarks>
  [PXDBInt(IsKey = true)]
  [PXDefault(2147483647 /*0x7FFFFFFF*/)]
  [PXUIField]
  [PXParent(typeof (SOTaxTran.FK.Order))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>
  /// The identifier of the specific tax applied to the document.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOTaxTran.FK.SOTax" />, the field is a part of the identifier of the parent sales order's tax line
  /// <see cref="T:PX.Objects.SO.SOTax" />.<see cref="T:PX.Objects.SO.SOTax.taxID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOTaxTran.FK.Tax" />, the field is the identifier of the tax
  /// <see cref="T:PX.Objects.TX.Tax" />.<see cref="T:PX.Objects.TX.Tax.taxID" /></item>
  /// </list>
  /// </remarks>
  [PX.Objects.TX.TaxID]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true)]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  /// <summary>
  /// This is an auto-numbered field, which is a part of the primary key.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  /// <summary>
  /// The tax jurisdiction type. The field is used for the Avalara taxes.
  /// </summary>
  [PXDBString(9, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Type")]
  public virtual string JurisType
  {
    get => this._JurisType;
    set => this._JurisType = value;
  }

  /// <summary>
  /// The tax jurisdiction name. The field is used for the Avalara taxes.
  /// </summary>
  [PXDBString(200, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Name")]
  public virtual string JurisName
  {
    get => this._JurisName;
    set => this._JurisName = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">currency and exchange rate information</see>.
  /// The field is included in the foreign key <see cref="T:PX.Objects.SO.SOTaxTran.FK.CurrencyInfo" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CM.CurrencyInfo.curyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(typeof (SOOrder.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOTaxTran.taxableAmt">taxable amount</see> for the specific tax calculated through the document
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOTaxTran.curyInfoID), typeof (SOTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Switch<Case<Where<WhereExempt<SOTaxTran.taxID>>, SOTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<SOOrder.curyVatExemptTotal>), CancelParentUpdate = true, ValidateAggregateCalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<WhereTaxable<SOTaxTran.taxID>>, SOTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<SOOrder.curyVatTaxableTotal>), CancelParentUpdate = true, ValidateAggregateCalculation = true)]
  public virtual Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOTaxTran.exemptedAmt">exempt amount</see> for the specific tax calculated through the document
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOTaxTran.curyInfoID), typeof (SOTaxTran.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryExemptedAmt { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOTaxTran.unshippedTaxableAmt">taxable amount</see> for the specific tax calculated through the
  /// document lines with nonzero unshipped quantities of stock items (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOTaxTran.curyInfoID), typeof (SOTaxTran.unshippedTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryUnshippedTaxableAmt
  {
    get => this._CuryUnshippedTaxableAmt;
    set => this._CuryUnshippedTaxableAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOTaxTran.unbilledTaxableAmt">taxable amount</see> for the specific tax calculated through the
  /// unbilled document lines (in the currency of the document).
  /// The taxable amount for the specific tax calculated through the unbilled document lines.
  /// </summary>
  [PXDBCurrency(typeof (SOTaxTran.curyInfoID), typeof (SOTaxTran.unbilledTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryUnbilledTaxableAmt
  {
    get => this._CuryUnbilledTaxableAmt;
    set => this._CuryUnbilledTaxableAmt = value;
  }

  /// <summary>
  /// The taxable amount for the specific tax calculated through the document.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  /// <summary>
  /// The exempt amount for the specific tax calculated through the document.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? ExemptedAmt { get; set; }

  /// <summary>
  /// The taxable amount for the specific tax calculated through the document lines with nonzero unshipped
  /// quantities of stock items.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnshippedTaxableAmt
  {
    get => this._UnshippedTaxableAmt;
    set => this._UnshippedTaxableAmt = value;
  }

  /// <summary>
  /// The taxable amount for the specific tax calculated through the unbilled document lines.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledTaxableAmt
  {
    get => this._UnbilledTaxableAmt;
    set => this._UnbilledTaxableAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOTaxTran.taxAmt">tax amount</see> for the specific tax (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOTaxTran.curyInfoID), typeof (SOTaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOTaxTran.unshippedTaxAmt">taxable amount</see> for the specific tax calculated through the unshipped
  /// document lines (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOTaxTran.curyInfoID), typeof (SOTaxTran.unshippedTaxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryUnshippedTaxAmt
  {
    get => this._CuryUnshippedTaxAmt;
    set => this._CuryUnshippedTaxAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOTaxTran.unbilledTaxAmt">tax amount</see> for the specific tax calculated through the unbilled
  /// document lines (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOTaxTran.curyInfoID), typeof (SOTaxTran.unbilledTaxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryUnbilledTaxAmt
  {
    get => this._CuryUnbilledTaxAmt;
    set => this._CuryUnbilledTaxAmt = value;
  }

  /// <summary>The unshipped taxable quantity for per unit taxes.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unshipped Taxable Qty.", Enabled = false)]
  public virtual Decimal? UnshippedTaxableQty { get; set; }

  /// <summary>The unbilled taxable quantity for per unit taxes.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Taxable Qty.", Enabled = false)]
  public virtual Decimal? UnbilledTaxableQty { get; set; }

  /// <summary>
  /// The tax amount for the specific tax calculated through the document.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt
  {
    get => this._TaxAmt;
    set => this._TaxAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOTaxTran.expenseAmt">expense amount</see> of the tax line (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOTaxTran.curyInfoID), typeof (SOTaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  /// <summary>
  /// The taxable amount for the specific tax calculated through the unshipped document lines.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnshippedTaxAmt
  {
    get => this._UnshippedTaxAmt;
    set => this._UnshippedTaxAmt = value;
  }

  /// <summary>
  /// The tax amount for the specific tax calculated through the unbilled document lines.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledTaxAmt
  {
    get => this._UnbilledTaxAmt;
    set => this._UnbilledTaxAmt = value;
  }

  /// <summary>
  /// The tax zone to be used to process customer sales orders.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.TX.TaxZone.taxZoneID" /> field.
  /// </value>
  /// <remarks>
  ///  The field is the identifier of the <see cref="T:PX.Objects.TX.TaxZone">tax zone</see>. The field is included in the
  /// <see cref="T:PX.Objects.SO.SOTaxTran.FK.TaxZone" /> foreign key.
  /// </remarks>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

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
    PrimaryKeyOf<SOTaxTran>.By<SOTaxTran.orderType, SOTaxTran.orderNbr, SOTaxTran.lineNbr, SOTaxTran.taxID, SOTaxTran.recordID>
  {
    public static SOTaxTran Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      string taxID,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (SOTaxTran) PrimaryKeyOf<SOTaxTran>.By<SOTaxTran.orderType, SOTaxTran.orderNbr, SOTaxTran.lineNbr, SOTaxTran.taxID, SOTaxTran.recordID>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, (object) taxID, (object) recordID, options);
    }
  }

  public class UK
  {
    public class ByRecordID : PrimaryKeyOf<SOTaxTran>.By<SOTaxTran.recordID>
    {
      public static SOTaxTran Find(PXGraph graph, string recordID, PKFindOptions options = 0)
      {
        return (SOTaxTran) PrimaryKeyOf<SOTaxTran>.By<SOTaxTran.recordID>.FindBy(graph, (object) recordID, options);
      }
    }

    public class ByJurisNameAndType : 
      PrimaryKeyOf<SOTaxTran>.By<SOTaxTran.orderType, SOTaxTran.orderNbr, SOTaxTran.lineNbr, SOTaxTran.taxID, SOTaxTran.jurisName, SOTaxTran.jurisType>
    {
      public static SOTaxTran Find(
        PXGraph graph,
        string orderType,
        string orderNbr,
        int? lineNbr,
        string taxID,
        string jurisName,
        string jurisType,
        PKFindOptions options = 0)
      {
        return (SOTaxTran) PrimaryKeyOf<SOTaxTran>.By<SOTaxTran.orderType, SOTaxTran.orderNbr, SOTaxTran.lineNbr, SOTaxTran.taxID, SOTaxTran.jurisName, SOTaxTran.jurisType>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, (object) taxID, (object) jurisName, (object) jurisType, options);
      }
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOTaxTran>.By<SOTaxTran.orderType, SOTaxTran.orderNbr>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<SOLine>.By<SOLine.orderType, SOLine.orderNbr, SOLine.lineNbr>.ForeignKeyOf<SOTaxTran>.By<SOTaxTran.orderType, SOTaxTran.orderNbr, SOTaxTran.lineNbr>
    {
    }

    public class SOTax : 
      PrimaryKeyOf<SOTax>.By<SOTax.orderType, SOTax.orderNbr, SOTax.lineNbr, SOTax.taxID>.ForeignKeyOf<SOTaxTran>.By<SOTaxTran.orderType, SOTaxTran.orderNbr, SOTaxTran.lineNbr, SOTaxTran.taxID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<SOTaxTran>.By<SOTaxTran.taxID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<SOTaxTran>.By<SOTaxTran.taxZoneID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOTaxTran>.By<SOTaxTran.curyInfoID>
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.OrderType" />
  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOTaxTran.orderType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.OrderNbr" />
  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOTaxTran.orderNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.LineNbr" />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOTaxTran.lineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.TaxID" />
  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOTaxTran.taxID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.RecordID" />
  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOTaxTran.recordID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.JurisType" />
  public abstract class jurisType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOTaxTran.jurisType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.JurisName" />
  public abstract class jurisName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOTaxTran.jurisName>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.TX.TaxDetail.TaxRate" />
  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTaxTran.taxRate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.CuryInfoID" />
  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOTaxTran.curyInfoID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.CuryTaxableAmt" />
  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.curyTaxableAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.CuryExemptedAmt" />
  public abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.CuryUnshippedTaxableAmt" />
  public abstract class curyUnshippedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.curyUnshippedTaxableAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.CuryUnbilledTaxableAmt" />
  public abstract class curyUnbilledTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.curyUnbilledTaxableAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.TaxableAmt" />
  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTaxTran.taxableAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.ExemptedAmt" />
  public abstract class exemptedAmt : IBqlField, IBqlOperand
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.UnshippedTaxableAmt" />
  public abstract class unshippedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.unshippedTaxableAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.UnbilledTaxableAmt" />
  public abstract class unbilledTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.unbilledTaxableAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.CuryTaxAmt" />
  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTaxTran.curyTaxAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.CuryUnshippedTaxAmt" />
  public abstract class curyUnshippedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.curyUnshippedTaxAmt>
  {
  }

  public abstract class curyUnbilledTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.curyUnbilledTaxAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.UnshippedTaxableQty" />
  public abstract class unshippedTaxableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.unshippedTaxableQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.UnbilledTaxableQty" />
  public abstract class unbilledTaxableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.unbilledTaxableQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.TaxAmt" />
  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTaxTran.taxAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.CuryExpenseAmt" />
  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.curyExpenseAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.TX.TaxDetail.ExpenseAmt" />
  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTaxTran.expenseAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.UnshippedTaxAmt" />
  public abstract class unshippedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.unshippedTaxAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.UnbilledTaxAmt" />
  public abstract class unbilledTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTaxTran.unbilledTaxAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOTaxTran.TaxZoneID" />
  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOTaxTran.taxZoneID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOTaxTran.Tstamp>
  {
  }

  public abstract class isTaxInclusive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOTaxTran.isTaxInclusive>
  {
  }
}
