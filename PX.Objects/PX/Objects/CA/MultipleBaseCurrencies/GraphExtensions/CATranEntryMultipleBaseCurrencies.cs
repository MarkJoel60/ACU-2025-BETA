// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.MultipleBaseCurrencies.GraphExtensions.CATranEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA.MultipleBaseCurrencies.GraphExtensions;

public class CATranEntryMultipleBaseCurrencies : PXGraphExtension<CATranEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(PX.Data.Events.FieldVerifying<CAAdj.cashAccountID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CAAdj.cashAccountID>, object, object>) e).NewValue == null)
      return;
    CashAccount cashAccount = PXSelectorAttribute.Select<CAAdj.cashAccountID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<CAAdj.cashAccountID>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CAAdj.cashAccountID>, object, object>) e).NewValue) as CashAccount;
    PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (cashAccount != null && branch != null && cashAccount.BaseCuryID != branch.BaseCuryID)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CAAdj.cashAccountID>, object, object>) e).NewValue = (object) cashAccount.CashAccountCD;
      throw new PXSetPropertyException("The base currency of the {0} branch associated with the {1} cash account differs from the base currency of the current branch.", new object[2]
      {
        (object) PXAccess.GetBranchCD(cashAccount.BranchID),
        (object) cashAccount.CashAccountCD
      });
    }
  }
}
