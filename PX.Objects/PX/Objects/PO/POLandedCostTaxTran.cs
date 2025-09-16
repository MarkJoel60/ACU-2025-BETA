// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Landed Costs Tax")]
[Serializable]
public class POLandedCostTaxTran : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected 
  #nullable disable
  string _JurisType;
  protected string _JurisName;

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDBDefault(typeof (POLandedCostDoc.docType))]
  [PXUIField(DisplayName = "Document Type", Enabled = false, Visible = false)]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXDBDefault(typeof (POLandedCostDoc.refNbr))]
  [PXUIField(DisplayName = "Document Nbr.", Enabled = false, Visible = false)]
  [PXParent(typeof (POLandedCostTaxTran.FK.LandedCostDocument))]
  public virtual string RefNbr { get; set; }

  [PX.Objects.TX.TaxID]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true)]
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
  [CurrencyInfo(typeof (POLandedCostDoc.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (POLandedCostTaxTran.curyInfoID), typeof (POLandedCostTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Switch<Case<WhereExempt<POLandedCostTaxTran.taxID>, POLandedCostTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<POLandedCostDoc.curyVatExemptTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<WhereTaxable<POLandedCostTaxTran.taxID>, POLandedCostTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<POLandedCostDoc.curyVatTaxableTotal>))]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt { get; set; }

  [PXDBCurrency(typeof (POLandedCostTaxTran.curyInfoID), typeof (POLandedCostTaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt { get; set; }

  [PXDBCurrency(typeof (POLandedCostTaxTran.curyInfoID), typeof (POLandedCostTaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string TaxZoneID { get; set; }

  /// <summary>
  /// A Boolean value that specifies that the tax transaction is tax inclusive or not
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Inclusive")]
  public virtual bool? IsTaxInclusive { get; set; }

  [PXDBTimestamp(RecordComesFirst = true)]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<POLandedCostTaxTran>.By<POLandedCostTaxTran.docType, POLandedCostTaxTran.refNbr, POLandedCostTaxTran.taxID, POLandedCostTaxTran.recordID>
  {
    public static POLandedCostTaxTran Find(
      PXGraph graph,
      string docType,
      string refNbr,
      string taxID,
      int? recordID)
    {
      return POLandedCostTaxTran.PK.Find(graph, docType, refNbr, taxID, recordID);
    }
  }

  public static class FK
  {
    public class LandedCostDocument : 
      PrimaryKeyOf<POLandedCostDoc>.By<POLandedCostDoc.docType, POLandedCostDoc.refNbr>.ForeignKeyOf<POLandedCostTaxTran>.By<POLandedCostTaxTran.docType, POLandedCostTaxTran.refNbr>
    {
    }

    public class Tax : 
      PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<POLandedCostTaxTran>.By<POLandedCostTaxTran.taxID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POLandedCostTaxTran>.By<POLandedCostTaxTran.curyInfoID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<POLandedCostTaxTran>.By<POLandedCostTaxTran.taxZoneID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostTaxTran.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostTaxTran.refNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostTaxTran.taxID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostTaxTran.recordID>
  {
  }

  public abstract class jurisType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostTaxTran.jurisType>
  {
  }

  public abstract class jurisName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostTaxTran.jurisName>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostTaxTran.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLandedCostTaxTran.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostTaxTran.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostTaxTran.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostTaxTran.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostTaxTran.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostTaxTran.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostTaxTran.expenseAmt>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostTaxTran.taxZoneID>
  {
  }

  public abstract class isTaxInclusive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POLandedCostTaxTran.isTaxInclusive>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POLandedCostTaxTran.Tstamp>
  {
  }
}
