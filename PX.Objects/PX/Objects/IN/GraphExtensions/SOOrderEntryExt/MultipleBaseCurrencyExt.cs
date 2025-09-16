// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.SOOrderEntryExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.SO;
using System;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.SOOrderEntryExt;

public class MultipleBaseCurrencyExt : 
  MultipleBaseCurrencyExtBase<
  #nullable disable
  SOOrderEntry, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder.branchID, PX.Objects.SO.SOLine.branchID, PX.Objects.SO.SOLine.siteID>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.SO.SOOrder.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLineSplit.siteID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.SO.SOOrder.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.siteID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.SO.SOOrder.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.pOSiteID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.SO.SOOrder.branchID), typeof (Where<Current2<PX.Objects.SO.SOOrder.branchID>, IsNull, Or<Current2<PX.Objects.SO.SOOrder.behavior>, NotEqual<SOBehavior.tR>, Or<Current2<PX.Objects.SO.SOOrder.aRDocType>, NotEqual<ARDocType.noUpdate>, Or<PX.Objects.IN.INSite.baseCuryID, EqualBaseCuryID<Current2<PX.Objects.SO.SOOrder.branchID>>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrder.destinationSiteID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.SO.SOOrder.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<EntryHeader.siteID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.destinationSiteID> e)
  {
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.destinationSiteID>>) e).Cache.GetAttributesReadonly<PX.Objects.SO.SOOrder.destinationSiteID>().OfType<PXRestrictorAttribute>().ForEach<PXRestrictorAttribute>((Action<PXRestrictorAttribute, int>) ((r, i) => r.FieldVerifying(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.destinationSiteID>>) e).Cache, ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.destinationSiteID>>) e).Args)));
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<PX.Objects.SO.SOOrder.branchID>, IsNull, Or2<Where<Current2<PX.Objects.SO.SOOrder.behavior>, Equal<SOBehavior.tR>, And<Current2<PX.Objects.SO.SOOrder.aRDocType>, Equal<ARDocType.noUpdate>>>, Or<PX.Objects.AR.Customer.baseCuryID, EqualBaseCuryID<Current2<PX.Objects.SO.SOOrder.branchID>>, Or<PX.Objects.AR.Customer.baseCuryID, IsNull>>>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new Type[] {typeof (PX.Objects.AR.Customer.cOrgBAccountID), typeof (PX.Objects.AR.Customer.acctCD)})]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrder.customerID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<PX.Objects.SO.SOOrder.branchID>, IsNull, Or<PX.Objects.AP.Vendor.baseCuryID, EqualBaseCuryID<Current2<PX.Objects.SO.SOOrder.branchID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new Type[] {typeof (PX.Objects.AP.Vendor.vOrgBAccountID), typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.vendorID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.SO.SOOrder.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<SOSiteStatusFilter.siteID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    PXUIFieldAttribute.SetEnabled<MultipleBaseCurrencyExt.SOOrderMultipleBaseCurrenciesRestriction.branchBaseCuryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache, (object) null, false);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.branchID> e)
  {
    int num;
    if (!((PXGraph) this.Base).IsCopyPasteContext)
    {
      PX.Objects.SO.SOOrder row1 = e.Row;
      if ((row1 != null ? (!row1.CustomerID.HasValue ? 1 : 0) : 1) == 0 || !((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.branchID>>) e).ExternalCall)
      {
        PX.Objects.SO.SOOrder row2 = e.Row;
        num = row2 != null ? (row2.IsTransferOrder.GetValueOrDefault() ? 1 : 0) : 0;
      }
      else
        num = 1;
    }
    else
      num = 0;
    bool resetCuryID = num != 0;
    this.SetDefaultBaseCurrency<PX.Objects.SO.SOOrder.curyID, PX.Objects.SO.SOOrder.curyInfoID, PX.Objects.SO.SOOrder.orderDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.branchID>>) e).Cache, e.Row, resetCuryID);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID> e,
    PXFieldDefaulting baseMethod)
  {
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    if (current != null && current.IsTransferOrder.GetValueOrDefault() && !string.IsNullOrEmpty(e.Row?.BaseCuryID))
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID>, PX.Objects.CM.CurrencyInfo, object>) e).NewValue = (object) e.Row.BaseCuryID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID>>) e).Cancel = true;
    }
    else
    {
      if (current != null && current.BranchID.HasValue && !this.Base.IsCopyOrder && (!current.CustomerID.HasValue || PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, current.CustomerID)?.CuryID == null))
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID>, PX.Objects.CM.CurrencyInfo, object>) e).NewValue = (object) PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, current.BranchID)?.BaseCuryID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID>>) e).Cancel = ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID>, PX.Objects.CM.CurrencyInfo, object>) e).NewValue != null;
      }
      baseMethod?.Invoke(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID>>) e).Cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID>>) e).Args);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder>>) e).Cache.VerifyFieldAndRaiseException<PX.Objects.SO.SOOrder.branchID>((object) e.Row);
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder>>) e).Cache.VerifyFieldAndRaiseException<PX.Objects.SO.SOOrder.destinationSiteID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOLineSplit> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLineSplit>>) e).Cache.VerifyFieldAndRaiseException<PX.Objects.SO.SOLineSplit.siteID>((object) e.Row);
  }

  protected override void OnDocumentBaseCuryChanged(PXCache cache, PX.Objects.SO.SOOrder row)
  {
    base.OnDocumentBaseCuryChanged(cache, row);
    cache.VerifyFieldAndRaiseException<PX.Objects.SO.SOOrder.customerID>((object) row);
    cache.VerifyFieldAndRaiseException<PX.Objects.SO.SOOrder.destinationSiteID>((object) row);
    foreach (PXResult<PX.Objects.SO.SOLineSplit> pxResult in ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Select(Array.Empty<object>()))
    {
      PX.Objects.SO.SOLineSplit row1 = PXResult<PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult);
      PXCache cache1 = ((PXSelectBase) this.Base.splits).Cache;
      GraphHelper.MarkUpdated(cache1, (object) row1, true);
      cache1.VerifyFieldAndRaiseException<PX.Objects.SO.SOLineSplit.siteID>((object) row1);
    }
  }

  protected override void OnLineBaseCuryChanged(PXCache cache, PX.Objects.SO.SOLine row)
  {
    base.OnLineBaseCuryChanged(cache, row);
    cache.SetDefaultExt<PX.Objects.SO.SOLine.unitCost>((object) row);
    cache.VerifyFieldAndRaiseException<PX.Objects.SO.SOLine.vendorID>((object) row);
    cache.VerifyFieldAndRaiseException<PX.Objects.SO.SOLine.pOSiteID>((object) row);
  }

  protected override PXSelectBase<PX.Objects.SO.SOLine> GetTransactionView()
  {
    return (PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions;
  }

  protected override void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine> e)
  {
    base._(e);
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine>>) e).Cache.VerifyFieldAndRaiseException<PX.Objects.SO.SOLine.vendorID>((object) e.Row);
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine>>) e).Cache.VerifyFieldAndRaiseException<PX.Objects.SO.SOLine.pOSiteID>((object) e.Row);
  }

  public sealed class SOOrderMultipleBaseCurrenciesRestriction : 
    PXCacheExtension<SOOrderVisibilityRestriction, PX.Objects.SO.SOOrder>
  {
    public static bool IsActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
    }

    [PXString(5, IsUnicode = true)]
    [PXFormula(typeof (Selector<PX.Objects.SO.SOOrder.branchID, PX.Objects.GL.Branch.baseCuryID>))]
    [PXUIVisible(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.behavior, Equal<SOBehavior.tR>>>>>.And<BqlOperand<PX.Objects.SO.SOOrder.aRDocType, IBqlString>.IsEqual<ARDocType.noUpdate>>>))]
    [PXUIField(DisplayName = "Base Currency", Enabled = false, FieldClass = "MultipleBaseCurrencies")]
    public string BranchBaseCuryID { get; set; }

    public abstract class branchBaseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      MultipleBaseCurrencyExt.SOOrderMultipleBaseCurrenciesRestriction.branchBaseCuryID>
    {
    }
  }
}
