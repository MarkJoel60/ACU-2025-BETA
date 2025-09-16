// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderShipmentProcessExt.Blanket
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO.DAC.Projections;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO.GraphExtensions.SOOrderShipmentProcessExt;

public class Blanket : PXGraphExtension<
#nullable disable
SOOrderShipmentProcess>
{
  public FbqlSelect<SelectFromBase<BlanketSOAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BlanketSOAdjust.adjdOrderType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  BlanketSOAdjust.adjdOrderType, IBqlString>.AsOptional>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  BlanketSOAdjust.adjdOrderNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  BlanketSOAdjust.adjdOrderNbr, IBqlString>.AsOptional>>>, 
  #nullable disable
  BlanketSOAdjust>.View BlanketAdjustments;
  public FbqlSelect<SelectFromBase<BlanketSOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BlanketSOOrder.completed, 
  #nullable disable
  Equal<True>>>>, And<BqlOperand<
  #nullable enable
  BlanketSOOrder.curyPaymentTotal, IBqlDecimal>.IsGreater<
  #nullable disable
  decimal0>>>>.And<Exists<Select<ARTran, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ARTran.blanketType, 
  #nullable disable
  Equal<BlanketSOOrder.orderType>>>>, And<BqlOperand<
  #nullable enable
  ARTran.blanketNbr, IBqlString>.IsEqual<
  #nullable disable
  BlanketSOOrder.orderNbr>>>, And<BqlOperand<
  #nullable enable
  ARTran.tranType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.AR.ARRegister.docType, IBqlString>.AsOptional>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  ARTran.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.AR.ARRegister.refNbr, IBqlString>.AsOptional>>>>>>>, 
  #nullable disable
  BlanketSOOrder>.View BlanketOrders;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  [PXOverride]
  public virtual List<PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder>> UpdateOrderShipments(
    PX.Objects.AR.ARRegister arDoc,
    HashSet<object> processed,
    Func<PX.Objects.AR.ARRegister, HashSet<object>, List<PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder>>> baseMethod)
  {
    List<PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder>> source = baseMethod(arDoc, processed);
    if (!arDoc.IsCancellation.GetValueOrDefault() && !arDoc.IsCorrection.GetValueOrDefault() && source.Any<PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder>>((Func<PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder>, bool>) (r =>
    {
      int? blanketLineCntr = PXResult.Unwrap<PX.Objects.SO.SOOrder>((object) r).BlanketLineCntr;
      int num = 0;
      return blanketLineCntr.GetValueOrDefault() > num & blanketLineCntr.HasValue;
    })))
    {
      foreach (PXResult<BlanketSOOrder> pxResult in ((PXSelectBase<BlanketSOOrder>) this.BlanketOrders).Select(new object[2]
      {
        (object) arDoc.DocType,
        (object) arDoc.RefNbr
      }))
        this.ResetPaymentAmount(PXResult<BlanketSOOrder>.op_Implicit(pxResult));
    }
    return source;
  }

  protected virtual void ResetPaymentAmount(BlanketSOOrder blanketOrder)
  {
    foreach (PXResult<BlanketSOAdjust> pxResult in ((PXSelectBase<BlanketSOAdjust>) this.BlanketAdjustments).Select(new object[2]
    {
      (object) blanketOrder.OrderType,
      (object) blanketOrder.OrderNbr
    }))
    {
      BlanketSOAdjust copy = PXCache<BlanketSOAdjust>.CreateCopy(PXResult<BlanketSOAdjust>.op_Implicit(pxResult));
      copy.CuryAdjdAmt = new Decimal?(0M);
      copy.CuryAdjgAmt = new Decimal?(0M);
      copy.AdjAmt = new Decimal?(0M);
      ((PXSelectBase<BlanketSOAdjust>) this.BlanketAdjustments).Update(copy);
    }
  }
}
