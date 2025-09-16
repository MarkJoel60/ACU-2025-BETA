// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Reclassification.UI.ReclassifyTransactionsProcessMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Reclassification.UI;

public class ReclassifyTransactionsProcessMultipleBaseCurrencies : 
  PXGraphExtension<ReclassifyTransactionsProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected void _(
    Events.FieldUpdated<ReclassifyTransactionsProcess.ReplaceOptions, ReclassifyTransactionsProcess.ReplaceOptions.withBranchID> e)
  {
    ReclassifyTransactionsProcess.ReplaceOptions row = e.Row;
    if (((IQueryable<PXResult<PX.Objects.GL.Branch>>) PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.baseCuryID, EqualBaseCuryID<BqlField<ReclassifyTransactionsProcess.ReplaceOptions.withBranchID, IBqlInt>.FromCurrent>, And<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<BqlField<ReclassifyTransactionsProcess.ReplaceOptions.newBranchID, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, Array.Empty<object>())).Any<PXResult<PX.Objects.GL.Branch>>())
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<ReclassifyTransactionsProcess.ReplaceOptions, ReclassifyTransactionsProcess.ReplaceOptions.withBranchID>>) e).Cache.SetValueExt<ReclassifyTransactionsProcess.ReplaceOptions.newBranchID>((object) row, (object) null);
  }
}
