// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProformaRevision
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>The projection of the <see cref="T:PX.Objects.PM.PMProforma" /> that contains the pro forma invoices that have been corrected.</summary>
[PXCacheName("Pro Forma Invoice Revision")]
[PXProjection(typeof (Select<PMProforma, Where<PMProforma.corrected, Equal<True>>>), Persistent = true)]
public class PMProformaRevision : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.RefNbr" />
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PMProforma.refNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.RevisionID" />
  [PXUIField(DisplayName = "Revision")]
  [PXDBInt(IsKey = true, BqlField = typeof (PMProforma.revisionID))]
  public virtual int? RevisionID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.Description" />
  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PMProforma.description))]
  [PXUIField(DisplayName = "Description")]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.CuryInfoID" />
  [PXDBLong(BqlField = typeof (PMProforma.curyInfoID))]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.CuryDocTotal" />
  [PXDBCurrency(typeof (PMProformaRevision.curyInfoID), typeof (PMProformaRevision.docTotal), BqlField = typeof (PMProforma.curyDocTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Invoice Total")]
  public virtual Decimal? CuryDocTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.DocTotal" />
  [PXDBBaseCury(BqlField = typeof (PMProforma.docTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Invoice Total in Base Currency")]
  public virtual Decimal? DocTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.CuryRetainageTotal" />
  [PXCurrency(typeof (PMProformaRevision.curyInfoID), typeof (PMProformaRevision.retainageTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Total", FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainageTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMProformaRevision.curyRetainageDetailTotal), typeof (PMProformaRevision.curyRetainageTaxTotal), typeof (PMProformaRevision.curyRetainageTaxInclTotal)})] get
    {
      Decimal? retainageDetailTotal = this.CuryRetainageDetailTotal;
      Decimal? curyRetainageTotal = this.CuryRetainageTaxTotal;
      Decimal? nullable = retainageDetailTotal.HasValue & curyRetainageTotal.HasValue ? new Decimal?(retainageDetailTotal.GetValueOrDefault() + curyRetainageTotal.GetValueOrDefault()) : new Decimal?();
      Decimal? retainageTaxInclTotal = this.CuryRetainageTaxInclTotal;
      if (nullable.HasValue & retainageTaxInclTotal.HasValue)
        return new Decimal?(nullable.GetValueOrDefault() - retainageTaxInclTotal.GetValueOrDefault());
      curyRetainageTotal = new Decimal?();
      return curyRetainageTotal;
    }
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.RetainageTotal" />
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Total in Base Currency", FieldClass = "Retainage")]
  public virtual Decimal? RetainageTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMProformaRevision.retainageDetailTotal), typeof (PMProformaRevision.retainageTaxTotal), typeof (PMProformaRevision.retainageTaxInclTotal)})] get
    {
      Decimal? retainageDetailTotal = this.RetainageDetailTotal;
      Decimal? retainageTotal = this.RetainageTaxTotal;
      Decimal? nullable = retainageDetailTotal.HasValue & retainageTotal.HasValue ? new Decimal?(retainageDetailTotal.GetValueOrDefault() + retainageTotal.GetValueOrDefault()) : new Decimal?();
      Decimal? retainageTaxInclTotal = this.RetainageTaxInclTotal;
      if (nullable.HasValue & retainageTaxInclTotal.HasValue)
        return new Decimal?(nullable.GetValueOrDefault() - retainageTaxInclTotal.GetValueOrDefault());
      retainageTotal = new Decimal?();
      return retainageTotal;
    }
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.CuryRetainageDetailTotal" />
  [PXDBCurrency(typeof (PMProformaRevision.curyInfoID), typeof (PMProformaRevision.retainageDetailTotal), BqlField = typeof (PMProforma.curyRetainageDetailTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Detail Total", FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainageDetailTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.RetainageDetailTotal" />
  [PXDBBaseCury(BqlField = typeof (PMProforma.retainageDetailTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Detail Total in Base Currency", FieldClass = "Retainage")]
  public virtual Decimal? RetainageDetailTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.CuryRetainageTaxTotal" />
  [PXDBCurrency(typeof (PMProformaRevision.curyInfoID), typeof (PMProformaRevision.retainageTaxTotal), BqlField = typeof (PMProforma.curyRetainageTaxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retained Tax Total", FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainageTaxTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.RetainageTaxTotal" />
  [PXDBBaseCury(BqlField = typeof (PMProforma.retainageTaxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Tax Total in Base Currency", FieldClass = "Retainage")]
  public virtual Decimal? RetainageTaxTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.CuryRetainageTaxInclTotal" />
  [PXDBCurrency(typeof (PMProformaRevision.curyInfoID), typeof (PMProformaRevision.retainageTaxInclTotal), BqlField = typeof (PMProforma.curyRetainageTaxInclTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retained Inclusive Tax Total", Visible = false, Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainageTaxInclTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.RetainageTaxInclTotal" />
  [PXDBBaseCury(BqlField = typeof (PMProforma.retainageTaxInclTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Inclusive Tax in Base Currency", Visible = false, Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? RetainageTaxInclTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.CuryTaxTotal" />
  [PXDBCurrency(typeof (PMProformaRevision.curyInfoID), typeof (PMProformaRevision.taxTotal), BqlField = typeof (PMProforma.curyTaxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Total")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.TaxTotal" />
  [PXDBBaseCury(BqlField = typeof (PMProforma.taxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Total in Base Currency")]
  public virtual Decimal? TaxTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.ARInvoiceDocType" />
  [ARInvoiceType.List]
  [PXUIField]
  [PXDBString(3, BqlField = typeof (PMProforma.aRInvoiceDocType))]
  public virtual string ARInvoiceDocType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.ARInvoiceRefNbr" />
  [PXUIField]
  [PXSelector(typeof (Search<ARInvoice.refNbr, Where<ARInvoice.docType, Equal<Current<PMProformaRevision.aRInvoiceDocType>>>>))]
  [PXDBString(15, IsUnicode = true, BqlField = typeof (PMProforma.aRInvoiceRefNbr))]
  public virtual string ARInvoiceRefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.ReversedARInvoiceDocType" />
  [ARInvoiceType.List]
  [PXUIField]
  [PXDBString(3, BqlField = typeof (PMProforma.reversedARInvoiceDocType))]
  public virtual string ReversedARInvoiceDocType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProforma.ReversedARInvoiceRefNbr" />
  [PXUIField]
  [PXSelector(typeof (Search<ARInvoice.refNbr, Where<ARInvoice.docType, Equal<Current<PMProformaRevision.reversedARInvoiceDocType>>>>))]
  [PXDBString(15, IsUnicode = true, BqlField = typeof (PMProforma.reversedARInvoiceRefNbr))]
  public virtual string ReversedARInvoiceRefNbr { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaRevision.refNbr>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaRevision.revisionID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaRevision.description>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMProformaRevision.curyInfoID>
  {
  }

  public abstract class curyDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaRevision.curyDocTotal>
  {
  }

  public abstract class docTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaRevision.docTotal>
  {
  }

  public abstract class curyRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaRevision.curyRetainageTotal>
  {
  }

  public abstract class retainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaRevision.retainageTotal>
  {
  }

  public abstract class curyRetainageDetailTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaRevision.curyRetainageDetailTotal>
  {
  }

  public abstract class retainageDetailTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaRevision.retainageDetailTotal>
  {
  }

  public abstract class curyRetainageTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaRevision.curyRetainageTaxTotal>
  {
  }

  public abstract class retainageTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaRevision.retainageTaxTotal>
  {
  }

  public abstract class curyRetainageTaxInclTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaRevision.curyRetainageTaxInclTotal>
  {
  }

  public abstract class retainageTaxInclTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaRevision.retainageTaxInclTotal>
  {
  }

  public abstract class curyTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaRevision.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProformaRevision.taxTotal>
  {
  }

  public abstract class aRInvoiceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaRevision.aRInvoiceDocType>
  {
  }

  public abstract class aRInvoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaRevision.aRInvoiceRefNbr>
  {
  }

  public abstract class reversedARInvoiceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaRevision.reversedARInvoiceDocType>
  {
  }

  public abstract class reversedARInvoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaRevision.reversedARInvoiceRefNbr>
  {
  }
}
