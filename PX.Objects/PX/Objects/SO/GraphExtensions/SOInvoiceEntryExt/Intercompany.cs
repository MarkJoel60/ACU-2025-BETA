// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.Intercompany
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class Intercompany : PXGraphExtension<SOInvoiceEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARTran> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    ARTran row = eventArgs.Row;
    if (!this.DisableIntercompanyOrderAmountsModification(row.SOOrderType, row.SOOrderNbr))
      return;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARTran>>) eventArgs).Cache, (object) eventArgs.Row).For<ARTran.curyUnitPrice>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    chained = chained.SameFor<ARTran.manualPrice>();
    chained = chained.SameFor<ARTran.curyExtPrice>();
    chained = chained.SameFor<ARTran.discPct>();
    chained = chained.SameFor<ARTran.curyDiscAmt>();
    chained = chained.SameFor<ARTran.manualDisc>();
    chained.SameFor<ARTran.discountID>();
  }

  protected virtual void _(
    PX.Data.Events.RowDeleting<ARInvoiceDiscountDetail> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    ARInvoiceDiscountDetail row = eventArgs.Row;
    if (eventArgs.ExternalCall && ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current != null && EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current), (PXEntryStatus) 3, (PXEntryStatus) 4) && this.DisableIntercompanyOrderAmountsModification(row.OrderType, row.OrderNbr))
      throw new PXSetPropertyException("The discount cannot be deleted because the Disable Editing Prices and Discounts check box is selected on the Sales Orders Preferences (SO101000) form.", (PXErrorLevel) 5);
  }

  protected virtual bool DisableIntercompanyOrderAmountsModification(
    string orderType,
    string orderNbr)
  {
    PX.Objects.AR.Customer current1 = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.IsBranch;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0 && orderNbr != null)
    {
      SOSetup current2 = ((PXSelectBase<SOSetup>) this.Base.sosetup).Current;
      int num2;
      if (current2 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current2.DisableEditingPricesDiscountsForIntercompany;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num2 != 0)
      {
        PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) orderType,
          (object) orderNbr
        }));
        if (soOrder != null && !string.IsNullOrEmpty(soOrder.IntercompanyPONbr) && EnumerableExtensions.IsIn<string>(soOrder.Behavior, "SO", "IN"))
          return true;
      }
    }
    return false;
  }
}
