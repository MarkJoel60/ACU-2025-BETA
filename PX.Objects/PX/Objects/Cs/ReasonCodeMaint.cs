// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ReasonCodeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.CS;

public class ReasonCodeMaint : PXGraph<ReasonCodeMaint, ReasonCode>
{
  [PXCopyPasteHiddenFields(new Type[] {typeof (ReasonCode.subMaskFinance), typeof (ReasonCode.subMaskInventory)})]
  public PXSelect<ReasonCode> reasoncode;
  public PXSelect<PX.Objects.IN.INSetup> INSetup;

  protected virtual void ReasonCode_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ReasonCode row))
      return;
    PXUIFieldAttribute.SetVisible<ReasonCode.accountID>(sender, (object) row, !EnumerableExtensions.IsIn<string>(row.Usage, "S", "N", "T"));
    PXUIFieldAttribute.SetVisible<ReasonCode.subID>(sender, (object) row, !EnumerableExtensions.IsIn<string>(row.Usage, "S", "N", "T"));
    PXUIFieldAttribute.SetVisible<ReasonCode.salesAcctID>(sender, (object) row, EnumerableExtensions.IsIn<string>(row.Usage, "S", "I"));
    PXUIFieldAttribute.SetVisible<ReasonCode.salesSubID>(sender, (object) row, EnumerableExtensions.IsIn<string>(row.Usage, "S", "I"));
    bool flag1;
    if (EnumerableExtensions.IsIn<string>(row.Usage, "S", "N", "T"))
    {
      flag1 = false;
      PXUIFieldAttribute.SetVisible<ReasonCode.subMaskFinance>(sender, (object) row, false);
    }
    else if (row.Usage == "C" || row.Usage == "B")
    {
      flag1 = false;
      PXUIFieldAttribute.SetVisible<ReasonCode.subMaskFinance>(sender, (object) row, true);
    }
    else
    {
      flag1 = true;
      PXUIFieldAttribute.SetVisible<ReasonCode.subMaskFinance>(sender, (object) row, false);
    }
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && EnumerableExtensions.IsIn<string>(row.Usage, "I", "A");
    PXUIFieldAttribute.SetVisible<ReasonCode.subMaskInventory>(sender, (object) row, flag1 & flag2);
    PXUIFieldAttribute.SetVisible<ReasonCode.subMaskInventoryShort>(sender, (object) row, flag1 && !flag2);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ReasonCode, ReasonCode.usage> e)
  {
    ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(PXSetup<ARSetup>.Select((PXGraph) this, Array.Empty<object>()));
    ReasonCode row = e.Row;
    if (row.ReasonCodeID != null && row.ReasonCodeID == arSetup?.BalanceWriteOff)
      throw new PXSetPropertyException("The reason code must have the {0} usage because it is selected in the Balance Write-Off Reason Code box on the Accounts Receivable Preferences (AR101000) form.", new object[1]
      {
        (object) row.ReasonCodeID
      });
    if (row.ReasonCodeID != null && row.ReasonCodeID == arSetup?.CreditWriteOff)
      throw new PXSetPropertyException("The reason code must have the {0} usage because it is selected in the Credit Write-Off Reason Code box on the Accounts Receivable Preferences (AR101000) form.", new object[1]
      {
        (object) row.ReasonCodeID
      });
  }

  protected virtual void ReasonCode_Usage_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ReasonCode row))
      return;
    if (!(row.Usage == "S"))
    {
      if (row.Usage == "C" || row.Usage == "B")
      {
        sender.SetDefaultExt<ReasonCode.subMaskFinance>(e.Row);
      }
      else
      {
        sender.SetDefaultExt<ReasonCode.subMaskInventory>(e.Row);
        sender.SetDefaultExt<ReasonCode.subMaskInventoryShort>(e.Row);
      }
    }
    if (e.OldValue == null || !EnumerableExtensions.IsIn<string>(e.OldValue.ToString(), "I", "A") || !EnumerableExtensions.IsNotIn<string>(row.Usage, "I", "A"))
      return;
    sender.SetDefaultExt<ReasonCode.subMaskInventory>(e.Row);
    sender.SetDefaultExt<ReasonCode.subMaskInventoryShort>(e.Row);
  }

  protected virtual void ReasonCode_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    PX.Objects.IN.INSetup inSetup = PXResultset<PX.Objects.IN.INSetup>.op_Implicit(((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Select(Array.Empty<object>()));
    ReasonCode row = (ReasonCode) e.Row;
    if (row != null && inSetup != null && (inSetup.ReceiptReasonCode == row.ReasonCodeID || inSetup.PIReasonCode == row.ReasonCodeID || inSetup.IssuesReasonCode == row.ReasonCodeID || inSetup.AdjustmentReasonCode == row.ReasonCodeID))
      throw new PXException("This reason code is specified as the default reason code on the Inventory Preferences form and cannot be deleted.");
  }

  protected virtual void ReasonCode_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    bool flag = EnumerableExtensions.IsIn<string>(((ReasonCode) e.Row).Usage, "S", "N", "T");
    PXDefaultAttribute.SetPersistingCheck<ReasonCode.accountID>(sender, e.Row, flag ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<ReasonCode.subID>(sender, e.Row, flag ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
  }
}
