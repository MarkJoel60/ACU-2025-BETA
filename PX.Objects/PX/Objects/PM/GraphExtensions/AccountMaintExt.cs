// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.AccountMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class AccountMaintExt : PXGraphExtension<AccountMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.GL.Account> e)
  {
    if (!e.Row.AccountGroupID.HasValue)
      return;
    PMAccountGroup pmAccountGroup = PXResultset<PMAccountGroup>.op_Implicit(PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupID, Equal<Required<PMAccountGroup.groupID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) e.Row.AccountGroupID
    }));
    if (pmAccountGroup != null)
      throw new PXException("You cannot delete the account associated with an account group. Delete the {0} account from the {1} account group on the Account Groups (PM201000) form first.", new object[2]
      {
        (object) e.Row.AccountCD,
        (object) pmAccountGroup.GroupCD
      });
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.GL.Account> e)
  {
    if (e.NewRow == null)
      return;
    PX.Objects.GL.Account newRow = e.NewRow;
    PX.Objects.GL.Account row = e.Row;
    if (row == null)
      return;
    int? nullable = row.AccountGroupID;
    if (!nullable.HasValue)
      return;
    nullable = newRow.AccountGroupID;
    if (nullable.HasValue)
      return;
    PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (pmSetup == null)
      return;
    nullable = newRow.AccountID;
    int? remainderAccountId = pmSetup.UnbilledRemainderAccountID;
    if (nullable.GetValueOrDefault() == remainderAccountId.GetValueOrDefault() & nullable.HasValue == remainderAccountId.HasValue)
      throw new PXSetPropertyException("The account cannot be deleted from the account group because it is selected as the Debit Account in the Unbilled Remainders section on the Project Preferences (PM101000) form.", (PXErrorLevel) 4);
  }
}
