// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Mappers.AmountLineFields
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.Extensions.Discount;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.Common.Discount.Mappers;

public abstract class AmountLineFields(
#nullable disable
PXCache cache, object row) : DiscountedLineMapperBase(cache, row)
{
  public bool HaveBaseQuantity { get; set; }

  public virtual Decimal? Quantity { get; set; }

  public virtual Decimal? CuryUnitPrice { get; set; }

  public virtual Decimal? CuryExtPrice { get; set; }

  public virtual Decimal? CuryLineAmount { get; set; }

  public virtual string UOM { get; set; }

  public virtual Decimal? OrigGroupDiscountRate { get; set; }

  public virtual Decimal? OrigDocumentDiscountRate { get; set; }

  public virtual Decimal? GroupDiscountRate { get; set; }

  public virtual Decimal? DocumentDiscountRate { get; set; }

  public virtual string TaxCategoryID { get; set; }

  public virtual bool? FreezeManualDisc { get; set; }

  public string ExtPriceDisplayName => this.GetDisplayName<AmountLineFields.curyExtPrice>();

  private object GetState<T>() where T : IBqlField
  {
    return this.Cache.GetStateExt(this.MappedLine, this.Cache.GetField(this.GetField<T>()));
  }

  private string GetDisplayName<T>() where T : IBqlField
  {
    return ((PXFieldState) this.GetState<T>()).DisplayName;
  }

  /// <summary>Get map to amount line fields</summary>
  /// <remarks>
  ///  QuantityField: Quantity
  ///  CuryUnitPriceField: Cury Unit Price
  ///  CuryExtPriceField: Quantity * Cury Unit Price field
  ///  CuryLineAmountField: (Quantity * Cury Unit Price field) - Cury Discount Amount field
  /// </remarks>
  public static AmountLineFields GetMapFor<TLine>(TLine line, PXCache cache) where TLine : class, IBqlTable
  {
    return AmountLineFields.GetMapFor<TLine>(line, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?());
  }

  internal static AmountLineFields GetMapFor<TLine>(
    TLine line,
    PXCache cache,
    bool applyQuantityDiscountByBaseUOM)
    where TLine : class, IBqlTable
  {
    return AmountLineFields.GetMapFor<TLine>(line, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption(applyQuantityDiscountByBaseUOM, applyQuantityDiscountByBaseUOM)));
  }

  internal static AmountLineFields GetMapFor<TLine>(
    TLine line,
    PXCache cache,
    DiscountEngine.ApplyQuantityDiscountByBaseUOMOption? applyQuantityDiscountByBaseUOM)
    where TLine : class, IBqlTable
  {
    System.Type type = line?.GetType();
    if ((object) type == null)
      type = typeof (TLine);
    System.Type lineType = type;
    return AmountLineFields.GetMapFor((object) line, cache, lineType, applyQuantityDiscountByBaseUOM);
  }

  private static AmountLineFields GetMapFor(
    object line,
    PXCache cache,
    System.Type lineType,
    DiscountEngine.ApplyQuantityDiscountByBaseUOMOption? applyQuantityDiscountByBaseUOM)
  {
    if (typeof (ARTran).IsAssignableFrom(lineType))
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>() && (line is ARTran arTran ? arTran.TranType : (string) null) == "PPI")
      {
        ARTranVATRecognitionOnPrepayments extension = cache.GetExtension<ARTranVATRecognitionOnPrepayments>(line);
        if ((applyQuantityDiscountByBaseUOM.HasValue ? (applyQuantityDiscountByBaseUOM.GetValueOrDefault().ForAR ? 1 : 0) : (DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR(cache.Graph) ? 1 : 0)) == 0)
          return (AmountLineFields) new PrepaymentAmountLineFields<ARTran.qty, ARTran.curyUnitPrice, ARTran.curyExtPrice, ARTran.curyTranAmt, ARTran.curyRetainageAmt, ARTran.uOM, ARTran.origGroupDiscountRate, ARTran.origDocumentDiscountRate, ARTran.groupDiscountRate, ARTran.documentDiscountRate, ARTran.freezeManualDisc>(cache, line, (object) extension);
        PrepaymentAmountLineFields<ARTran.baseQty, ARTran.curyUnitPrice, ARTran.curyExtPrice, ARTran.curyTranAmt, ARTran.curyRetainageAmt, ARTran.uOM, ARTran.origGroupDiscountRate, ARTran.origDocumentDiscountRate, ARTran.groupDiscountRate, ARTran.documentDiscountRate, ARTran.freezeManualDisc> mapFor = new PrepaymentAmountLineFields<ARTran.baseQty, ARTran.curyUnitPrice, ARTran.curyExtPrice, ARTran.curyTranAmt, ARTran.curyRetainageAmt, ARTran.uOM, ARTran.origGroupDiscountRate, ARTran.origDocumentDiscountRate, ARTran.groupDiscountRate, ARTran.documentDiscountRate, ARTran.freezeManualDisc>(cache, line, (object) extension);
        mapFor.HaveBaseQuantity = true;
        return (AmountLineFields) mapFor;
      }
      if ((applyQuantityDiscountByBaseUOM.HasValue ? (applyQuantityDiscountByBaseUOM.GetValueOrDefault().ForAR ? 1 : 0) : (DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR(cache.Graph) ? 1 : 0)) == 0)
        return (AmountLineFields) new RetainedAmountLineFields<ARTran.qty, ARTran.curyUnitPrice, ARTran.curyExtPrice, ARTran.curyTranAmt, ARTran.curyRetainageAmt, ARTran.uOM, ARTran.origGroupDiscountRate, ARTran.origDocumentDiscountRate, ARTran.groupDiscountRate, ARTran.documentDiscountRate, ARTran.freezeManualDisc>(cache, line);
      RetainedAmountLineFields<ARTran.baseQty, ARTran.curyUnitPrice, ARTran.curyExtPrice, ARTran.curyTranAmt, ARTran.curyRetainageAmt, ARTran.uOM, ARTran.origGroupDiscountRate, ARTran.origDocumentDiscountRate, ARTran.groupDiscountRate, ARTran.documentDiscountRate, ARTran.freezeManualDisc> mapFor1 = new RetainedAmountLineFields<ARTran.baseQty, ARTran.curyUnitPrice, ARTran.curyExtPrice, ARTran.curyTranAmt, ARTran.curyRetainageAmt, ARTran.uOM, ARTran.origGroupDiscountRate, ARTran.origDocumentDiscountRate, ARTran.groupDiscountRate, ARTran.documentDiscountRate, ARTran.freezeManualDisc>(cache, line);
      mapFor1.HaveBaseQuantity = true;
      return (AmountLineFields) mapFor1;
    }
    if (typeof (PX.Objects.SO.SOLine).IsAssignableFrom(lineType))
    {
      if ((applyQuantityDiscountByBaseUOM.HasValue ? (applyQuantityDiscountByBaseUOM.GetValueOrDefault().ForAR ? 1 : 0) : (DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR(cache.Graph) ? 1 : 0)) == 0)
        return (AmountLineFields) new AmountLineFields<PX.Objects.SO.SOLine.orderQty, PX.Objects.SO.SOLine.curyUnitPrice, PX.Objects.SO.SOLine.curyExtPrice, PX.Objects.SO.SOLine.curyLineAmt, PX.Objects.SO.SOLine.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, PX.Objects.SO.SOLine.groupDiscountRate, PX.Objects.SO.SOLine.documentDiscountRate, PX.Objects.SO.SOLine.freezeManualDisc>(cache, line);
      AmountLineFields<PX.Objects.SO.SOLine.baseOrderQty, PX.Objects.SO.SOLine.curyUnitPrice, PX.Objects.SO.SOLine.curyExtPrice, PX.Objects.SO.SOLine.curyLineAmt, PX.Objects.SO.SOLine.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, PX.Objects.SO.SOLine.groupDiscountRate, PX.Objects.SO.SOLine.documentDiscountRate, PX.Objects.SO.SOLine.freezeManualDisc> mapFor = new AmountLineFields<PX.Objects.SO.SOLine.baseOrderQty, PX.Objects.SO.SOLine.curyUnitPrice, PX.Objects.SO.SOLine.curyExtPrice, PX.Objects.SO.SOLine.curyLineAmt, PX.Objects.SO.SOLine.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, PX.Objects.SO.SOLine.groupDiscountRate, PX.Objects.SO.SOLine.documentDiscountRate, PX.Objects.SO.SOLine.freezeManualDisc>(cache, line);
      mapFor.HaveBaseQuantity = true;
      return (AmountLineFields) mapFor;
    }
    if (typeof (SOShipLine).IsAssignableFrom(lineType))
    {
      if ((applyQuantityDiscountByBaseUOM.HasValue ? (applyQuantityDiscountByBaseUOM.GetValueOrDefault().ForAR ? 1 : 0) : (DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR(cache.Graph) ? 1 : 0)) == 0)
        return (AmountLineFields) new AmountLineFields<SOShipLine.shippedQty, AmountLineFields.curyUnitCost, AmountLineFields.curyLineAmt, AmountLineFields.curyExtCost, SOShipLine.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, AmountLineFields.groupDiscountRate, AmountLineFields.documentDiscountRate>(cache, line);
      AmountLineFields<SOShipLine.baseShippedQty, AmountLineFields.curyUnitCost, AmountLineFields.curyLineAmt, AmountLineFields.curyExtCost, SOShipLine.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, AmountLineFields.groupDiscountRate, AmountLineFields.documentDiscountRate> mapFor = new AmountLineFields<SOShipLine.baseShippedQty, AmountLineFields.curyUnitCost, AmountLineFields.curyLineAmt, AmountLineFields.curyExtCost, SOShipLine.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, AmountLineFields.groupDiscountRate, AmountLineFields.documentDiscountRate>(cache, line);
      mapFor.HaveBaseQuantity = true;
      return (AmountLineFields) mapFor;
    }
    if (typeof (PX.Objects.AP.APTran).IsAssignableFrom(lineType))
    {
      if ((applyQuantityDiscountByBaseUOM.HasValue ? (applyQuantityDiscountByBaseUOM.GetValueOrDefault().ForAP ? 1 : 0) : (DiscountEngine.ApplyQuantityDiscountByBaseUOMForAP(cache.Graph) ? 1 : 0)) == 0)
        return (AmountLineFields) new RetainedAmountLineFields<PX.Objects.AP.APTran.qty, PX.Objects.AP.APTran.curyUnitCost, PX.Objects.AP.APTran.curyLineAmt, PX.Objects.AP.APTran.curyTranAmt, PX.Objects.AP.APTran.curyRetainageAmt, PX.Objects.AP.APTran.uOM, PX.Objects.AP.APTran.origGroupDiscountRate, PX.Objects.AP.APTran.origDocumentDiscountRate, PX.Objects.AP.APTran.groupDiscountRate, PX.Objects.AP.APTran.documentDiscountRate, PX.Objects.AP.APTran.freezeManualDisc>(cache, line);
      RetainedAmountLineFields<PX.Objects.AP.APTran.baseQty, PX.Objects.AP.APTran.curyUnitCost, PX.Objects.AP.APTran.curyLineAmt, PX.Objects.AP.APTran.curyTranAmt, PX.Objects.AP.APTran.curyRetainageAmt, PX.Objects.AP.APTran.uOM, PX.Objects.AP.APTran.origGroupDiscountRate, PX.Objects.AP.APTran.origDocumentDiscountRate, PX.Objects.AP.APTran.groupDiscountRate, PX.Objects.AP.APTran.documentDiscountRate, PX.Objects.AP.APTran.freezeManualDisc> mapFor = new RetainedAmountLineFields<PX.Objects.AP.APTran.baseQty, PX.Objects.AP.APTran.curyUnitCost, PX.Objects.AP.APTran.curyLineAmt, PX.Objects.AP.APTran.curyTranAmt, PX.Objects.AP.APTran.curyRetainageAmt, PX.Objects.AP.APTran.uOM, PX.Objects.AP.APTran.origGroupDiscountRate, PX.Objects.AP.APTran.origDocumentDiscountRate, PX.Objects.AP.APTran.groupDiscountRate, PX.Objects.AP.APTran.documentDiscountRate, PX.Objects.AP.APTran.freezeManualDisc>(cache, line);
      mapFor.HaveBaseQuantity = true;
      return (AmountLineFields) mapFor;
    }
    if (typeof (PX.Objects.PO.POLine).IsAssignableFrom(lineType))
    {
      if ((applyQuantityDiscountByBaseUOM.HasValue ? (applyQuantityDiscountByBaseUOM.GetValueOrDefault().ForAP ? 1 : 0) : (DiscountEngine.ApplyQuantityDiscountByBaseUOMForAP(cache.Graph) ? 1 : 0)) == 0)
        return (AmountLineFields) new RetainedAmountLineFields<PX.Objects.PO.POLine.orderQty, PX.Objects.PO.POLine.curyUnitCost, PX.Objects.PO.POLine.curyLineAmt, PX.Objects.PO.POLine.curyExtCost, PX.Objects.PO.POLine.curyRetainageAmt, PX.Objects.PO.POLine.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, PX.Objects.PO.POLine.groupDiscountRate, PX.Objects.PO.POLine.documentDiscountRate>(cache, line);
      RetainedAmountLineFields<PX.Objects.PO.POLine.baseOrderQty, PX.Objects.PO.POLine.curyUnitCost, PX.Objects.PO.POLine.curyLineAmt, PX.Objects.PO.POLine.curyExtCost, PX.Objects.PO.POLine.curyRetainageAmt, PX.Objects.PO.POLine.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, PX.Objects.PO.POLine.groupDiscountRate, PX.Objects.PO.POLine.documentDiscountRate> mapFor = new RetainedAmountLineFields<PX.Objects.PO.POLine.baseOrderQty, PX.Objects.PO.POLine.curyUnitCost, PX.Objects.PO.POLine.curyLineAmt, PX.Objects.PO.POLine.curyExtCost, PX.Objects.PO.POLine.curyRetainageAmt, PX.Objects.PO.POLine.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, PX.Objects.PO.POLine.groupDiscountRate, PX.Objects.PO.POLine.documentDiscountRate>(cache, line);
      mapFor.HaveBaseQuantity = true;
      return (AmountLineFields) mapFor;
    }
    if (typeof (CROpportunityProducts).IsAssignableFrom(lineType))
      return (AmountLineFields) new AmountLineFields<CROpportunityProducts.quantity, CROpportunityProducts.curyUnitPrice, CROpportunityProducts.curyExtPrice, CROpportunityProducts.curyAmount, CROpportunityProducts.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, CROpportunityProducts.groupDiscountRate, CROpportunityProducts.documentDiscountRate, AmountLineFields.freezeManualDisc>(cache, line);
    if (typeof (Detail).IsAssignableFrom(lineType))
      return (AmountLineFields) new AmountLineFields<Detail.quantity, Detail.curyUnitPrice, Detail.curyExtPrice, Detail.curyLineAmount, Detail.uOM, Detail.origGroupDiscountRate, Detail.origDocumentDiscountRate, Detail.groupDiscountRate, Detail.documentDiscountRate, Detail.freezeManualDisc>(cache, line);
    if ((applyQuantityDiscountByBaseUOM.HasValue ? (applyQuantityDiscountByBaseUOM.GetValueOrDefault().ForAR ? 1 : 0) : (DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR(cache.Graph) ? 1 : 0)) == 0)
      return (AmountLineFields) new AmountLineFields<AmountLineFields.orderQty, AmountLineFields.curyUnitPrice, AmountLineFields.curyExtPrice, AmountLineFields.curyLineAmt, AmountLineFields.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, AmountLineFields.groupDiscountRate, AmountLineFields.documentDiscountRate, AmountLineFields.freezeManualDisc>(cache, line);
    AmountLineFields<AmountLineFields.baseOrderQty, AmountLineFields.curyUnitPrice, AmountLineFields.curyExtPrice, AmountLineFields.curyLineAmt, AmountLineFields.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, AmountLineFields.groupDiscountRate, AmountLineFields.documentDiscountRate, AmountLineFields.freezeManualDisc> mapFor2 = new AmountLineFields<AmountLineFields.baseOrderQty, AmountLineFields.curyUnitPrice, AmountLineFields.curyExtPrice, AmountLineFields.curyLineAmt, AmountLineFields.uOM, AmountLineFields.origGroupDiscountRate, AmountLineFields.origDocumentDiscountRate, AmountLineFields.groupDiscountRate, AmountLineFields.documentDiscountRate, AmountLineFields.freezeManualDisc>(cache, line);
    mapFor2.HaveBaseQuantity = true;
    return (AmountLineFields) mapFor2;
  }

  public abstract class quantity : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AmountLineFields.quantity>
  {
  }

  private abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AmountLineFields.orderQty>
  {
  }

  private abstract class baseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.baseOrderQty>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.curyUnitPrice>
  {
  }

  private abstract class curyUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.curyUnitCost>
  {
  }

  public abstract class curyExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.curyExtPrice>
  {
  }

  private abstract class curyExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.curyExtCost>
  {
  }

  public abstract class curyLineAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.curyLineAmount>
  {
  }

  private abstract class curyLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.curyLineAmt>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AmountLineFields.uOM>
  {
  }

  public abstract class origGroupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.origGroupDiscountRate>
  {
  }

  public abstract class origDocumentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.origDocumentDiscountRate>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AmountLineFields.documentDiscountRate>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AmountLineFields.taxCategoryID>
  {
  }

  public abstract class freezeManualDisc : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AmountLineFields.freezeManualDisc>
  {
  }
}
