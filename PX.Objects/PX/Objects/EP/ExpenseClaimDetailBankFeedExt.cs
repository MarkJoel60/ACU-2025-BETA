// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimDetailBankFeedExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP.DAC;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public class ExpenseClaimDetailBankFeedExt : PXGraphExtension<ExpenseClaimDetailEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankFeedIntegration>();

  protected virtual void _(Events.FieldUpdated<EPExpenseClaimDetails.hold> e)
  {
    if (!(e.Row is EPExpenseClaimDetails row))
      return;
    bool? oldValue = (bool?) ((Events.FieldUpdatedBase<Events.FieldUpdated<EPExpenseClaimDetails.hold>, object, object>) e).OldValue;
    ExpenseClaimDetailsBankFeedExt extension = PXCache<EPExpenseClaimDetails>.GetExtension<ExpenseClaimDetailsBankFeedExt>(row);
    if (!oldValue.GetValueOrDefault())
      return;
    bool? hold = row.Hold;
    bool flag = false;
    if (hold.GetValueOrDefault() == flag & hold.HasValue && extension.BankTranStatus == "P")
      throw new PXException("This receipt cannot be taken off hold because the bank transaction has the Pending status.");
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSubordinateAndWingmenSelectorAttribute))]
  [PXConfigureSubordinateAndWingmenSelector]
  protected virtual void _(
    Events.CacheAttached<EPExpenseClaimDetails.employeeID> e)
  {
  }

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    ExpenseClaimDetailBankFeedExt.CopyPasteGetScriptDelegate baseMethod)
  {
    baseMethod(isImportSimple, script, containers);
    int index = script.FindIndex((Predicate<Command>) (i => i.FieldName == "BankTranStatus"));
    if (index == -1)
      return;
    script.RemoveAt(index);
    containers.RemoveAt(index);
  }

  public delegate void CopyPasteGetScriptDelegate(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers);
}
