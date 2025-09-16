// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DeferredCodeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.DR;

public class DeferredCodeMaint : PXGraph<DeferredCodeMaint, DRDeferredCode>
{
  public PXSelect<DRDeferredCode> DeferredCode;
  public PXSetup<DRSetup> Setup;

  public DeferredCodeMaint()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
  }

  private void SetPeriodicallyControlsState(PXCache cache, DRDeferredCode s)
  {
    PXUIFieldAttribute.SetEnabled<DRDeferredCode.fixedDay>(cache, (object) s, s.ScheduleOption == "D");
  }

  protected virtual void DRDeferredCode_Method_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row is DRDeferredCode row && row.Method == "C")
      row.AccountType = "I";
    if (DeferredMethodType.RequiresTerms(row))
    {
      sender.SetValueExt<DRDeferredCode.accountType>((object) row, (object) "I");
      sender.SetDefaultExt<DRDeferredCode.startOffset>((object) row);
      sender.SetDefaultExt<DRDeferredCode.occurrences>((object) row);
    }
    else
      sender.SetDefaultExt<DRDeferredCode.recognizeInPastPeriods>((object) row);
    if (!(row.Method == "C"))
      return;
    sender.SetValueExt<DRDeferredCode.accountType>((object) row, (object) "I");
  }

  protected virtual void DRDeferredCode_MultiDeliverableArrangement_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRDeferredCode row) || !row.MultiDeliverableArrangement.GetValueOrDefault())
      return;
    Type[] typeArray = new Type[14]
    {
      typeof (DRDeferredCode.method),
      typeof (DRDeferredCode.reconNowPct),
      typeof (DRDeferredCode.startOffset),
      typeof (DRDeferredCode.occurrences),
      typeof (DRDeferredCode.accountType),
      typeof (DRDeferredCode.accountSource),
      typeof (DRDeferredCode.deferralSubMaskAR),
      typeof (DRDeferredCode.deferralSubMaskAP),
      typeof (DRDeferredCode.copySubFromSourceTran),
      typeof (DRDeferredCode.accountID),
      typeof (DRDeferredCode.subID),
      typeof (DRDeferredCode.frequency),
      typeof (DRDeferredCode.scheduleOption),
      typeof (DRDeferredCode.fixedDay)
    };
    foreach (Type type in typeArray)
      sender.SetDefaultExt((object) row, type.Name, (object) null);
  }

  protected virtual void DRDeferredCode_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DRDeferredCode row))
      return;
    PXUIFieldAttribute.SetVisible(sender, (object) row, (string) null, true);
    this.SetPeriodicallyControlsState(sender, row);
    bool? nullable = row.MultiDeliverableArrangement;
    int num1 = nullable.GetValueOrDefault() ? 1 : (row.AccountSource == "I" ? 1 : 0);
    nullable = row.MultiDeliverableArrangement;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.CopySubFromSourceTran;
      1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag = DeferredMethodType.RequiresTerms(row);
    PXUIFieldAttribute.SetEnabled<DRDeferredCode.accountType>(sender, (object) row, row.Method != "C");
    PXUIFieldAttribute.SetEnabled<DRDeferredCode.startOffset>(sender, (object) row, !flag);
    PXUIFieldAttribute.SetEnabled<DRDeferredCode.occurrences>(sender, (object) row, !flag);
    PXCache pxCache1 = sender;
    DRDeferredCode drDeferredCode1 = row;
    nullable = row.CopySubFromSourceTran;
    int num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<DRDeferredCode.deferralSubMaskAR>(pxCache1, (object) drDeferredCode1, num2 != 0);
    PXCache pxCache2 = sender;
    DRDeferredCode drDeferredCode2 = row;
    nullable = row.CopySubFromSourceTran;
    int num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<DRDeferredCode.deferralSubMaskAP>(pxCache2, (object) drDeferredCode2, num3 != 0);
    PXUIFieldAttribute.SetVisible<DRDeferredCode.deferralSubMaskAR>(sender, (object) row, row.AccountType == "I");
    PXUIFieldAttribute.SetVisible<DRDeferredCode.deferralSubMaskAP>(sender, (object) row, row.AccountType == "E");
    PXUIFieldAttribute.SetVisible<DRDeferredCode.recognizeInPastPeriods>(sender, (object) row, flag);
    nullable = row.MultiDeliverableArrangement;
    if (!nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetVisible(sender, (object) row, (string) null, false);
    PXUIFieldAttribute.SetVisible<DRDeferredCode.deferredCodeID>(sender, (object) row, true);
    PXUIFieldAttribute.SetVisible<DRDeferredCode.description>(sender, (object) row, true);
    PXUIFieldAttribute.SetVisible<DRDeferredCode.multiDeliverableArrangement>(sender, (object) row, true);
    PXUIFieldAttribute.SetVisible<DRDeferredCode.active>(sender, (object) row, true);
  }

  protected virtual void DRDeferredCode_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is DRDeferredCode))
      return;
    PX.Objects.IN.InventoryItem inventoryItem = ((PXSelectBase<PX.Objects.IN.InventoryItem>) new PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.deferredCode, Equal<Current<DRDeferredCode.deferredCodeID>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>());
    INComponent inComponent = ((PXSelectBase<INComponent>) new PXSelect<INComponent, Where<INComponent.deferredCode, Equal<Current<DRDeferredCode.deferredCodeID>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>());
    PX.Objects.AR.ARTran arTran = ((PXSelectBase<PX.Objects.AR.ARTran>) new PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.released, Equal<False>, And<PX.Objects.AR.ARTran.deferredCode, Equal<Current<DRDeferredCode.deferredCodeID>>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>());
    APTran apTran = ((PXSelectBase<APTran>) new PXSelect<APTran, Where<APTran.released, Equal<False>, And<APTran.deferredCode, Equal<Current<DRDeferredCode.deferredCodeID>>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>());
    foreach (Tuple<string, object> tuple in new List<Tuple<string, object>>()
    {
      new Tuple<string, object>("Inventory Item", (object) inventoryItem),
      new Tuple<string, object>("Component Part", (object) inComponent),
      new Tuple<string, object>("AR Transactions", (object) arTran),
      new Tuple<string, object>("AP Transactions", (object) apTran)
    })
    {
      if (tuple.Item2 != null)
        throw new PXException("Can't delete the Deferral Code because it is in use. There is at least one '{0}' record that refers it.", new object[1]
        {
          (object) PXMessages.LocalizeNoPrefix(tuple.Item1)
        });
    }
  }

  protected virtual void DRDeferredCode_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    DRDeferredCode row = e.Row as DRDeferredCode;
    PXDefaultAttribute.SetPersistingCheck<DRDeferredCode.accountID>(sender, (object) row, row.MultiDeliverableArrangement.GetValueOrDefault() || row.AccountSource == "I" ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXCache pxCache = sender;
    DRDeferredCode drDeferredCode = row;
    bool? nullable1;
    int num1;
    if (!row.MultiDeliverableArrangement.GetValueOrDefault())
    {
      nullable1 = row.CopySubFromSourceTran;
      if (!nullable1.GetValueOrDefault())
      {
        num1 = 1;
        goto label_4;
      }
    }
    num1 = 2;
label_4:
    PXDefaultAttribute.SetPersistingCheck<DRDeferredCode.subID>(pxCache, (object) drDeferredCode, (PXPersistingCheck) num1);
    if (DeferredMethodType.RequiresTerms(row))
      return;
    nullable1 = row.MultiDeliverableArrangement;
    if (nullable1.GetValueOrDefault())
      return;
    short? occurrences = row.Occurrences;
    int? nullable2 = occurrences.HasValue ? new int?((int) occurrences.GetValueOrDefault()) : new int?();
    int num2 = 0;
    if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
      return;
    sender.RaiseExceptionHandling<DRDeferredCode.occurrences>(e.Row, (object) row.Occurrences, (Exception) new PXSetPropertyException("The number of occurrences should not be equal to zero."));
  }

  private void CheckAccountType(PXCache sender, object Row, int? AccountID, string AccountType)
  {
    PX.Objects.GL.Account account = AccountID.HasValue ? (PX.Objects.GL.Account) PXSelectorAttribute.Select<DRDeferredCode.accountID>(sender, Row, (object) AccountID) : (PX.Objects.GL.Account) PXSelectorAttribute.Select<DRDeferredCode.accountID>(sender, Row);
    if (account != null && AccountType == "E" && account.Type != "A")
      sender.RaiseExceptionHandling<DRDeferredCode.accountID>(Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Account Type is not {0}.", (PXErrorLevel) 2, new object[1]
      {
        (object) "Asset"
      }));
    if (account == null || !(AccountType == "I") || !(account.Type != "L"))
      return;
    sender.RaiseExceptionHandling<DRDeferredCode.accountID>(Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Account Type is not {0}.", (PXErrorLevel) 2, new object[1]
    {
      (object) "Liability"
    }));
  }

  protected virtual void DRDeferredCode_AccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.CheckAccountType(sender, e.Row, (int?) e.NewValue, ((DRDeferredCode) e.Row).AccountType);
  }

  protected virtual void DRDeferredCode_AccountType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.CheckAccountType(sender, e.Row, new int?(), (string) e.NewValue);
  }

  protected virtual void DRDeferredCode_MultiDeliverableArrangement_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is DRDeferredCode row && !((bool?) e.NewValue).GetValueOrDefault() && row.MultiDeliverableArrangement.GetValueOrDefault() && ((PXSelectBase<PX.Objects.IN.InventoryItem>) new PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.deferredCode, Equal<Current<DRDeferredCode.deferredCodeID>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>()) != null)
      throw new PXSetPropertyException<DRDeferredCode.multiDeliverableArrangement>("The MDA code is used on some items - can't convert to common code.");
  }
}
