// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("PO Tax Detail")]
[Serializable]
public class POTax : 
  TaxDetail,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ITaxDetailWithAmounts,
  ITaxDetailWithLineNbr,
  ITaxDetail
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (POOrder.orderType), DefaultForUpdate = false)]
  [PXUIField]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (POOrder.orderNbr), DefaultForUpdate = false)]
  [PXUIField]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (POTax.FK.OrderLine))]
  [PXParent(typeof (POTax.FK.OrderLineR))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (POOrder.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (POTax.curyInfoID), typeof (POTax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  [PXDBCurrency(typeof (POTax.curyInfoID), typeof (POTax.unbilledTaxableAmt))]
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

  [PXDBCurrency(typeof (POTax.curyInfoID), typeof (POTax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBCurrency(typeof (POTax.curyInfoID), typeof (POTax.unbilledTaxAmt))]
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

  [PXDBCurrency(typeof (POTax.curyInfoID), typeof (POTax.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledTaxAmt { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string TaxZoneID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POTax.curyInfoID), typeof (POTax.retainedTaxableAmt))]
  [PXUIField]
  public virtual Decimal? CuryRetainedTaxableAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? RetainedTaxableAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POTax.curyInfoID), typeof (POTax.retainedTaxAmt))]
  [PXUIField]
  public virtual Decimal? CuryRetainedTaxAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? RetainedTaxAmt { get; set; }

  public class PK : 
    PrimaryKeyOf<POTax>.By<POTax.orderType, POTax.orderNbr, POTax.lineNbr, POTax.taxID>
  {
    public static POTax Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      string taxID,
      PKFindOptions options = 0)
    {
      return (POTax) PrimaryKeyOf<POTax>.By<POTax.orderType, POTax.orderNbr, POTax.lineNbr, POTax.taxID>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POTax>.By<POTax.orderType, POTax.orderNbr>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<POLine>.By<POLine.orderType, POLine.orderNbr, POLine.lineNbr>.ForeignKeyOf<POTax>.By<POTax.orderType, POTax.orderNbr, POTax.lineNbr>
    {
    }

    public class OrderLineR : 
      PrimaryKeyOf<POLineR>.By<POLineR.orderType, POLineR.orderNbr, POLineR.lineNbr>.ForeignKeyOf<POTax>.By<POTax.orderType, POTax.orderNbr, POTax.lineNbr>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<POTax>.By<POTax.taxID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POTax>.By<POTax.curyInfoID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<POTax>.By<POTax.taxZoneID>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTax.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTax.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POTax.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POTax.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTax.curyTaxableAmt>
  {
  }

  public abstract class curyUnbilledTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTax.curyUnbilledTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTax.taxableAmt>
  {
  }

  public abstract class unbilledTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTax.unbilledTaxableAmt>
  {
  }

  /// <summary>The unbilled taxable quantity for per unit taxes.</summary>
  public abstract class unbilledTaxableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTax.unbilledTaxableQty>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTax.curyTaxAmt>
  {
  }

  public abstract class curyUnbilledTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTax.curyUnbilledTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTax.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTax.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTax.expenseAmt>
  {
  }

  public abstract class unbilledTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTax.unbilledTaxAmt>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTax.taxZoneID>
  {
  }

  public abstract class curyRetainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTax.curyRetainedTaxableAmt>
  {
  }

  public abstract class retainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTax.retainedTaxableAmt>
  {
  }

  public abstract class curyRetainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTax.curyRetainedTaxAmt>
  {
  }

  public abstract class retainedTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POTax.retainedTaxAmt>
  {
  }
}
