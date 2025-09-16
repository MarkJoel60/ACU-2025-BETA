// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.IN;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("SO Tax Detail")]
[Serializable]
public class SOTax : 
  TaxDetail,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ITaxDetailWithLineNbr,
  ITaxDetail
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
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

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (SOOrder.orderType))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXParent(typeof (SOTax.FK.OrderLine), LeaveChildren = true)]
  [PXParent(typeof (SOTax.FK.Order))]
  [PXParent(typeof (Select<SOLine2, Where<SOLine2.orderType, Equal<Current<SOTax.orderType>>, And<SOLine2.orderNbr, Equal<Current<SOTax.orderNbr>>, And<SOLine2.lineNbr, Equal<Current<SOTax.lineNbr>>>>>>), LeaveChildren = true)]
  [PXParent(typeof (Select<SOLine4, Where<SOLine4.orderType, Equal<Current<SOTax.orderType>>, And<SOLine4.orderNbr, Equal<Current<SOTax.orderNbr>>, And<SOLine4.lineNbr, Equal<Current<SOTax.lineNbr>>>>>>), LeaveChildren = true)]
  [PXParent(typeof (Select<SOMiscLine2, Where<SOMiscLine2.orderType, Equal<Current<SOTax.orderType>>, And<SOMiscLine2.orderNbr, Equal<Current<SOTax.orderNbr>>, And<SOMiscLine2.lineNbr, Equal<Current<SOTax.lineNbr>>>>>>), LeaveChildren = true)]
  [PXParent(typeof (Select<BlanketSOLine, Where<BlanketSOLine.orderType, Equal<Current<SOTax.orderType>>, And<BlanketSOLine.orderNbr, Equal<Current<SOTax.orderNbr>>, And<BlanketSOLine.lineNbr, Equal<Current<SOTax.lineNbr>>>>>>), LeaveChildren = true)]
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
  [CurrencyInfo(typeof (SOOrder.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (SOTax.curyInfoID), typeof (SOTax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (SOTax.curyInfoID), typeof (SOTax.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryExemptedAmt { get; set; }

  [PXDBCurrency(typeof (SOTax.curyInfoID), typeof (SOTax.unshippedTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryUnshippedTaxableAmt
  {
    get => this._CuryUnshippedTaxableAmt;
    set => this._CuryUnshippedTaxableAmt = value;
  }

  [PXDBCurrency(typeof (SOTax.curyInfoID), typeof (SOTax.unbilledTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryUnbilledTaxableAmt
  {
    get => this._CuryUnbilledTaxableAmt;
    set => this._CuryUnbilledTaxableAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ExemptedAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnshippedTaxableAmt
  {
    get => this._UnshippedTaxableAmt;
    set => this._UnshippedTaxableAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledTaxableAmt
  {
    get => this._UnbilledTaxableAmt;
    set => this._UnbilledTaxableAmt = value;
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

  [PXDBCurrency(typeof (SOTax.curyInfoID), typeof (SOTax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBCurrency(typeof (SOTax.curyInfoID), typeof (SOTax.unshippedTaxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryUnshippedTaxAmt
  {
    get => this._CuryUnshippedTaxAmt;
    set => this._CuryUnshippedTaxAmt = value;
  }

  [PXDBCurrency(typeof (SOTax.curyInfoID), typeof (SOTax.unbilledTaxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryUnbilledTaxAmt
  {
    get => this._CuryUnbilledTaxAmt;
    set => this._CuryUnbilledTaxAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt
  {
    get => this._TaxAmt;
    set => this._TaxAmt = value;
  }

  [PXDBCurrency(typeof (SOTax.curyInfoID), typeof (SOTax.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnshippedTaxAmt
  {
    get => this._UnshippedTaxAmt;
    set => this._UnshippedTaxAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledTaxAmt
  {
    get => this._UnbilledTaxAmt;
    set => this._UnbilledTaxAmt = value;
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string TaxZoneID { get; set; }

  public class PK : 
    PrimaryKeyOf<SOTax>.By<SOTax.orderType, SOTax.orderNbr, SOTax.lineNbr, SOTax.taxID>
  {
    public static SOTax Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      string taxID,
      PKFindOptions options = 0)
    {
      return (SOTax) PrimaryKeyOf<SOTax>.By<SOTax.orderType, SOTax.orderNbr, SOTax.lineNbr, SOTax.taxID>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOTax>.By<SOTax.orderType>
    {
    }

    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOTax>.By<SOTax.orderType, SOTax.orderNbr>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<SOLine>.By<SOLine.orderType, SOLine.orderNbr, SOLine.lineNbr>.ForeignKeyOf<SOTax>.By<SOTax.orderType, SOTax.orderNbr, SOTax.lineNbr>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<SOTax>.By<SOTax.taxID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<SOTax>.By<SOTax.taxZoneID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOTax>.By<SOTax.curyInfoID>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOTax.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOTax.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOTax.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOTax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOTax.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTax.curyTaxableAmt>
  {
  }

  public abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyUnshippedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTax.curyUnshippedTaxableAmt>
  {
  }

  public abstract class curyUnbilledTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTax.curyUnbilledTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTax.taxableAmt>
  {
  }

  public abstract class exemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class unshippedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTax.unshippedTaxableAmt>
  {
  }

  public abstract class unbilledTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTax.unbilledTaxableAmt>
  {
  }

  /// <summary>The unshipped taxable quantity for per unit taxes.</summary>
  public abstract class unshippedTaxableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTax.unshippedTaxableQty>
  {
  }

  /// <summary>The unbilled taxable quantity for per unit taxes.</summary>
  public abstract class unbilledTaxableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTax.unbilledTaxableQty>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTax.curyTaxAmt>
  {
  }

  public abstract class curyUnshippedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTax.curyUnshippedTaxAmt>
  {
  }

  public abstract class curyUnbilledTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOTax.curyUnbilledTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTax.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTax.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTax.expenseAmt>
  {
  }

  public abstract class unshippedTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTax.unshippedTaxAmt>
  {
  }

  public abstract class unbilledTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOTax.unbilledTaxAmt>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOTax.taxZoneID>
  {
  }
}
