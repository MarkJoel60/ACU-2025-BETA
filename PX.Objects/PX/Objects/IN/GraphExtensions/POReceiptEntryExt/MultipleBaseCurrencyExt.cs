// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.POReceiptEntryExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.POReceiptEntryExt;

public class MultipleBaseCurrencyExt : 
  MultipleBaseCurrencyExtBase<
  #nullable disable
  POReceiptEntry, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceipt.branchID, PX.Objects.PO.POReceiptLine.branchID, PX.Objects.PO.POReceiptLine.siteID>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.PO.POReceipt.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POReceiptLine.siteID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<PX.Objects.PO.POReceipt.branchID>, IsNull, Or<Current2<PX.Objects.PO.POReceipt.receiptType>, Equal<POReceiptType.transferreceipt>, Or<PX.Objects.AP.Vendor.baseCuryID, EqualBaseCuryID<Current2<PX.Objects.PO.POReceipt.branchID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new Type[] {typeof (PX.Objects.AP.Vendor.vOrgBAccountID), typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POReceipt.vendorID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.PO.POReceipt.branchID), typeof (Where<Current2<PX.Objects.PO.POReceipt.branchID>, IsNull, Or<Current2<PX.Objects.PO.POReceipt.receiptType>, NotEqual<POReceiptType.transferreceipt>, Or<INSite.baseCuryID, EqualBaseCuryID<Current2<PX.Objects.PO.POReceipt.branchID>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POReceipt.siteID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.PO.POReceipt.branchID), null)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POReceiptEntry.POOrderFilter.shipFromSiteID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt> e)
  {
    PXUIFieldAttribute.SetEnabled<MultipleBaseCurrencyExt.POReceiptMultipleBaseCurrenciesRestriction.branchBaseCuryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt>>) e).Cache, (object) null, false);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.PO.POReceipt> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POReceipt>>) e).Cache.VerifyFieldAndRaiseException<PX.Objects.PO.POReceipt.branchID>((object) e.Row);
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POReceipt>>) e).Cache.VerifyFieldAndRaiseException<PX.Objects.PO.POReceipt.siteID>((object) e.Row);
  }

  protected override void OnDocumentBaseCuryChanged(PXCache cache, PX.Objects.PO.POReceipt row)
  {
    base.OnDocumentBaseCuryChanged(cache, row);
    cache.VerifyFieldAndRaiseException<PX.Objects.PO.POReceipt.siteID>((object) row);
  }

  protected override PXSelectBase<PX.Objects.PO.POReceiptLine> GetTransactionView()
  {
    return (PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.InsertNewLandedCostDoc(PX.Objects.PO.POLandedCostDocEntry,PX.Objects.PO.POReceipt)" />
  /// </summary>
  [PXOverride]
  public virtual void InsertNewLandedCostDoc(
    POLandedCostDocEntry lcGraph,
    PX.Objects.PO.POReceipt receipt,
    Action<POLandedCostDocEntry, PX.Objects.PO.POReceipt> baseMethod)
  {
    baseMethod(lcGraph, receipt);
    ((PXSelectBase<POLandedCostDoc>) lcGraph.Document).Current.BranchID = receipt.BranchID;
    ((PXSelectBase<POLandedCostDoc>) lcGraph.Document).Current.CuryID = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, receipt.BranchID).BaseCuryID;
    ((PXSelectBase<POLandedCostDoc>) lcGraph.Document).UpdateCurrent();
  }

  public sealed class POReceiptMultipleBaseCurrenciesRestriction : 
    PXCacheExtension<POReceiptVisibilityRestriction, PX.Objects.PO.POReceipt>
  {
    public static bool IsActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
    }

    [PXString(5, IsUnicode = true)]
    [PXFormula(typeof (Selector<PX.Objects.PO.POReceipt.branchID, PX.Objects.GL.Branch.baseCuryID>))]
    [PXUIVisible(typeof (Where<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<POReceiptType.transferreceipt>>))]
    [PXUIField(DisplayName = "Base Currency", Enabled = false, FieldClass = "MultipleBaseCurrencies")]
    public string BranchBaseCuryID { get; set; }

    public abstract class branchBaseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      MultipleBaseCurrencyExt.POReceiptMultipleBaseCurrenciesRestriction.branchBaseCuryID>
    {
    }
  }
}
