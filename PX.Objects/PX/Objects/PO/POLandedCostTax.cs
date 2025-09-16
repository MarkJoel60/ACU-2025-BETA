// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Landed Costs Tax Detail")]
[Serializable]
public class POLandedCostTax : 
  TaxDetail,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ITaxDetailWithLineNbr,
  ITaxDetail
{
  /// <summary>The type of the landed cost receipt line.</summary>
  /// <value>
  /// The field is determined by the type of the parent <see cref="T:PX.Objects.PO.POLandedCostDoc">document</see>.
  /// For the list of possible values see <see cref="P:PX.Objects.PO.POLandedCostDoc.DocType" />.
  /// </value>
  [POLandedCostDocType.List]
  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (POLandedCostDoc.docType))]
  [PXUIField]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (POLandedCostDoc.refNbr))]
  [PXUIField]
  [PXParent(typeof (POLandedCostTax.FK.LandedCostDocument))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (POLandedCostTax.FK.LandedCostDetail))]
  public virtual int? LineNbr { get; set; }

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
  [CurrencyInfo(typeof (POLandedCostDoc.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (POLandedCostTax.curyInfoID), typeof (POLandedCostTax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt { get; set; }

  [PXDBCurrency(typeof (POLandedCostTax.curyInfoID), typeof (POLandedCostTax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt { get; set; }

  public class PK : 
    PrimaryKeyOf<POLandedCostTax>.By<POLandedCostTax.docType, POLandedCostTax.refNbr, POLandedCostTax.lineNbr, POLandedCostTax.taxID>
  {
    public static POLandedCostTax Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      string taxID,
      PKFindOptions options = 0)
    {
      return (POLandedCostTax) PrimaryKeyOf<POLandedCostTax>.By<POLandedCostTax.docType, POLandedCostTax.refNbr, POLandedCostTax.lineNbr, POLandedCostTax.taxID>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class LandedCostDocument : 
      PrimaryKeyOf<POLandedCostDoc>.By<POLandedCostDoc.docType, POLandedCostDoc.refNbr>.ForeignKeyOf<POLandedCostTax>.By<POLandedCostTax.docType, POLandedCostTax.refNbr>
    {
    }

    public class LandedCostDetail : 
      PrimaryKeyOf<POLandedCostDetail>.By<POLandedCostDetail.docType, POLandedCostDetail.refNbr, POLandedCostDetail.lineNbr>.ForeignKeyOf<POLandedCostTax>.By<POLandedCostTax.docType, POLandedCostTax.refNbr, POLandedCostTax.lineNbr>
    {
    }

    public class Tax : 
      PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<POLandedCostTax>.By<POLandedCostTax.taxID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POLandedCostTax>.By<POLandedCostTax.curyInfoID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostTax.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostTax.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostTax.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostTax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostTax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLandedCostTax.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostTax.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostTax.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostTax.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostTax.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostTax.curyExpenseAmt>
  {
  }
}
