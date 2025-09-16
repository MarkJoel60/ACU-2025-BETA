// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.SVATTaxFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using PX.Objects.TX.Descriptor;
using System;

#nullable enable
namespace PX.Objects.TX;

[Serializable]
public class SVATTaxFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? Date { get; set; }

  [OrganizationTree(null, true)]
  [PXUIRequired(typeof (Where<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>))]
  public int? OrgBAccountID { get; set; }

  [TaxAgencyActive]
  public virtual int? TaxAgencyID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (Search<Tax.taxID, Where<Tax.isExternal, NotEqual<True>>>), DescriptionField = typeof (Tax.descr))]
  public virtual 
  #nullable disable
  string TaxID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [PXFormula(typeof (IsNull<Selector<SVATTaxFilter.taxAgencyID, PX.Objects.AP.Vendor.sVATReversalMethod>, SVATTaxReversalMethods.onDocuments>))]
  [SVATTaxReversalMethods.List]
  [PXUIField(DisplayName = "VAT Recognition Method")]
  public virtual string ReversalMethod { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Tax Amount", Enabled = false)]
  public virtual Decimal? TotalTaxAmount { get; set; }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SVATTaxFilter.date>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class taxAgencyID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATTaxFilter.taxAgencyID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATTaxFilter.taxID>
  {
  }

  public abstract class reversalMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATTaxFilter.reversalMethod>
  {
  }

  public abstract class totalTaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATTaxFilter.totalTaxAmount>
  {
  }
}
