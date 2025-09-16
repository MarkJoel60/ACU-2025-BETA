// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.AddSOLineToDirectInvoiceVATRecognitonOnPrepayment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

#nullable disable
namespace PX.Objects.AR;

public class AddSOLineToDirectInvoiceVATRecognitonOnPrepayment : 
  PXGraphExtension<AddSOLineToDirectInvoice, SOInvoiceEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>() && PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>();
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<SOLineForDirectInvoice> eventArgs)
  {
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOLineForDirectInvoice>>) eventArgs).Cache;
    SOLineForDirectInvoice row = eventArgs.Row;
    SOLineForDirectInvoice forDirectInvoice = row;
    if (forDirectInvoice != null)
    {
      SOAdjust soAdjust = PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXSelectJoin<SOAdjust, InnerJoin<ARPayment, On<ARPayment.docType, Equal<SOAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<SOAdjust.adjgRefNbr>>>>, Where<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>, And<ARPayment.docType, Equal<ARDocType.prepaymentInvoice>, And<ARPayment.status, Equal<ARDocStatus.pendingPayment>, And<SOAdjust.curyAdjdAmt, NotEqual<decimal0>>>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOInvoiceEntry>) this).Base, (object[]) null, (object[]) new string[2]
      {
        forDirectInvoice.OrderType,
        forDirectInvoice.OrderNbr
      }));
      if (soAdjust != null)
      {
        PXUIFieldAttribute.SetWarning<SOLineForDirectInvoice.orderNbr>(cache, (object) row, PXMessages.LocalizeFormatNoPrefix("An invoice cannot be created because the sales order is associated with the following unpaid prepayment invoice: {0}.", new object[1]
        {
          (object) soAdjust.AdjgRefNbr
        }));
        PXUIFieldAttribute.SetEnabled<SOLineForDirectInvoice.selected>(cache, (object) row, false);
      }
      else
        PXUIFieldAttribute.SetEnabled<SOLineForDirectInvoice.selected>(cache, (object) row, true);
    }
    else
      PXUIFieldAttribute.SetEnabled<SOLineForDirectInvoice.selected>(cache, (object) row, true);
  }
}
