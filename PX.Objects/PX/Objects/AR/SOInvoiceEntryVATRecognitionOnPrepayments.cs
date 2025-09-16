// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SOInvoiceEntryVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.AR;

public class SOInvoiceEntryVATRecognitionOnPrepayments : PXGraphExtension<SOInvoiceEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>();
  }

  public virtual void Initialize() => ((PXGraphExtension) this).Initialize();

  protected virtual void _(PX.Data.Events.RowSelected<ARInvoice> e)
  {
    PXUIFieldAttribute.SetVisible<ARTran.sOOrderNbr>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<ARTran.sOOrderType>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, true);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrderShipment> eventArgs)
  {
    PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrderShipment>>) eventArgs).Cache, (object) eventArgs.Row, false);
    PX.Objects.SO.SOOrderShipment row = eventArgs.Row;
    if (row != null)
    {
      SOAdjust soAdjust = PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXSelectJoin<SOAdjust, InnerJoin<ARPayment, On<ARPayment.docType, Equal<SOAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<SOAdjust.adjgRefNbr>>>>, Where<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>, And<ARPayment.docType, Equal<ARDocType.prepaymentInvoice>, And<ARPayment.status, Equal<ARDocStatus.pendingPayment>, And<SOAdjust.curyAdjdAmt, NotEqual<decimal0>>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object[]) new string[2]
      {
        row.OrderType,
        row.OrderNbr
      }));
      if (soAdjust != null)
      {
        PXUIFieldAttribute.SetWarning<PX.Objects.SO.SOOrderShipment.orderNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrderShipment>>) eventArgs).Cache, (object) eventArgs.Row, PXMessages.LocalizeFormatNoPrefix("An invoice cannot be created because the sales order is associated with the following unpaid prepayment invoice: {0}.", new object[1]
        {
          (object) soAdjust.AdjgRefNbr
        }));
        PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOOrderShipment.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrderShipment>>) eventArgs).Cache, (object) eventArgs.Row, false);
      }
      else
        PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOOrderShipment.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrderShipment>>) eventArgs).Cache, (object) eventArgs.Row, true);
    }
    else
      PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOOrderShipment.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrderShipment>>) eventArgs).Cache, (object) eventArgs.Row, true);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  public virtual void InvoicePreProcessingValidations(InvoiceOrderArgs args)
  {
  }

  [PXOverride]
  public virtual void AfterInsertApplication(PX.Objects.SO.SOOrderShipment orderShipment)
  {
    Decimal? curyUnpaidBalance = ((PXSelectBase<ARInvoice>) this.Base.Document).Current.CuryUnpaidBalance;
    Decimal num = 0M;
    if (!(curyUnpaidBalance.GetValueOrDefault() > num & curyUnpaidBalance.HasValue))
      return;
    SOAdjust soAdjust = PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXViewOf<SOAdjust>.BasedOn<SelectFromBase<SOAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<SOAdjust.adjgDocType>>>>>.And<BqlOperand<ARRegister.refNbr, IBqlString>.IsEqual<SOAdjust.adjgRefNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.adjdOrderType, Equal<P.AsString.ASCII>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.adjdOrderNbr, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.openDoc, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.released, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.pendingPayment, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.voided, Equal<False>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.curyOrigDocAmt, NotEqual<ARRegister.curyDocBal>>>>>.And<BqlOperand<SOAdjust.curyAdjdAmt, IBqlDecimal>.IsGreater<decimal0>>>>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) orderShipment.OrderType,
      (object) orderShipment.OrderNbr
    }));
    if (soAdjust != null)
      throw new PXException("An invoice cannot be created because the sales order is associated with the following unpaid prepayment invoice: {0}.", new object[1]
      {
        (object) soAdjust.AdjgRefNbr
      });
  }
}
