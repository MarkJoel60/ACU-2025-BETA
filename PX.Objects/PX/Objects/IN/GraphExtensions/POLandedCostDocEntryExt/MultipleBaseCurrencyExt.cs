// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.POLandedCostDocEntryExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PO;
using PX.Objects.PO.LandedCosts;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.POLandedCostDocEntryExt;

public class MultipleBaseCurrencyExt : 
  MultipleBaseCurrencyExtBase<POLandedCostDocEntry, POLandedCostDoc, POLandedCostReceiptLine, POLandedCostDoc.branchID, POLandedCostReceiptLine.branchID, POLandedCostReceiptLine.siteID>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.poReceiptSelectionView).Join<InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.PO.POReceipt.FK.Branch>>>();
    ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.poReceiptSelectionView).WhereAnd<Where<PX.Objects.GL.Branch.baseCuryID, EqualBaseCuryID<Current2<POLandedCostDoc.branchID>>>>();
    ((PXSelectBase<POReceiptLineAdd>) this.Base.poReceiptLinesSelectionView).Join<InnerJoin<PX.Objects.GL.Branch, On<BqlOperand<POReceiptLineAdd.branchID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.branchID>>>>();
    ((PXSelectBase<POReceiptLineAdd>) this.Base.poReceiptLinesSelectionView).WhereAnd<Where<PX.Objects.GL.Branch.baseCuryID, EqualBaseCuryID<Current2<POLandedCostDoc.branchID>>>>();
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<POLandedCostDoc.branchID>, IsNull, Or<PX.Objects.AP.Vendor.baseCuryID, EqualBaseCuryID<Current2<POLandedCostDoc.branchID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vOrgBAccountID), typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(PX.Data.Events.CacheAttached<POLandedCostDoc.vendorID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.baseCuryID, EqualBaseCuryID<Current2<POLandedCostDoc.branchID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vOrgBAccountID), typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptFilter.vendorID> e)
  {
  }

  [PXMergeAttributes]
  [Branch(typeof (Coalesce<Search<PX.Objects.CR.Location.vBranchID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POLandedCostDoc.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<POLandedCostDoc.vendorLocationID>>>>>, Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current2<POLandedCostDoc.branchID>>>>, Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>), null, true, true, true, IsDetail = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<POLandedCostDoc.branchID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<POLandedCostDoc> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POLandedCostDoc>>) e).Cache.VerifyFieldAndRaiseException<POLandedCostDoc.branchID>((object) e.Row);
  }

  protected override PXSelectBase<POLandedCostReceiptLine> GetTransactionView()
  {
    return (PXSelectBase<POLandedCostReceiptLine>) this.Base.ReceiptLines;
  }
}
