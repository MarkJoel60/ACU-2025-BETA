// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Caching;
using PX.Data;
using PX.Metadata;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class ARSetupMaint : PXGraph<ARSetupMaint>
{
  public PXSave<ARSetup> Save;
  public PXCancel<ARSetup> Cancel;
  public PXSelect<ARSetup> ARSetupRecord;
  public CRNotificationSetupList<ARNotification> Notifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<ARNotification.setupID>>>> Recipients;
  public PXSelect<ARDunningSetup> DunningSetup;
  public CMSetupSelect CMSetup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSelect<ARSetupApproval> SetupApproval;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXAction<ARSetup> viewAssignmentMap;

  public ARSetupMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
  }

  [PXUIField]
  public virtual IEnumerable ViewAssignmentMap(PXAdapter adapter)
  {
    if (((PXSelectBase<ARSetupApproval>) this.SetupApproval).Current != null)
    {
      EPAssignmentMap epAssignmentMap = PXResult<EPAssignmentMap>.op_Implicit(((IQueryable<PXResult<EPAssignmentMap>>) PXSelectBase<EPAssignmentMap, PXSelect<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<ARSetupApproval>) this.SetupApproval).Current.AssignmentMapID
      })).First<PXResult<EPAssignmentMap>>());
      int? nullable = epAssignmentMap.MapType;
      PXGraph instance;
      if (nullable.GetValueOrDefault() == 2)
      {
        instance = (PXGraph) PXGraph.CreateInstance<EPApprovalMapMaint>();
      }
      else
      {
        nullable = epAssignmentMap.MapType;
        if (nullable.GetValueOrDefault() == 1)
        {
          instance = (PXGraph) PXGraph.CreateInstance<EPAssignmentMapMaint>();
        }
        else
        {
          nullable = epAssignmentMap.MapType;
          int num1 = 0;
          if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
          {
            nullable = epAssignmentMap.AssignmentMapID;
            int num2 = 0;
            if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
            {
              instance = (PXGraph) PXGraph.CreateInstance<EPAssignmentMaint>();
              goto label_9;
            }
          }
          instance = (PXGraph) PXGraph.CreateInstance<EPAssignmentAndApprovalMapEnq>();
        }
      }
label_9:
      PXRedirectHelper.TryRedirect(instance, (object) epAssignmentMap, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXDBString(10)]
  [PXDefault]
  [CustomerContactType.ClassList]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationSetupRecipient.contactID)}, Where = typeof (Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>))]
  public virtual void NotificationSetupRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationSetupRecipient.contactType), typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<EPEmployee, On<EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<NotificationSetupRecipient.contactType>, Equal<NotificationContactType.employee>, And<EPEmployee.acctCD, IsNotNull>>>))]
  public virtual void NotificationSetupRecipient_ContactID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Prepare Statements", FieldClass = "COMPANYBRANCH")]
  public virtual void _(PX.Data.Events.CacheAttached<ARSetup.prepareStatements> e)
  {
  }

  protected virtual void ARSetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARSetup row))
      return;
    bool flag1 = this.ShowBranches();
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.dunningLetter>();
    PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
    bool flag3 = PXAccess.FeatureInstalled<FeaturesSet.branch>();
    bool flag4 = PXAccess.FeatureInstalled<FeaturesSet.multiCompany>();
    PXUIFieldAttribute.SetEnabled<ARSetup.invoicePrecision>(sender, (object) row, row.InvoiceRounding != "N");
    PXCache pxCache1 = sender;
    ARSetup arSetup1 = row;
    int? nullable = row.DunningLetterProcessType;
    int num1 = nullable.GetValueOrDefault() == 1 ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ARSetup.autoReleaseDunningLetter>(pxCache1, (object) arSetup1, num1 != 0);
    PXUIFieldAttribute.SetEnabled<ARSetup.numberOfMonths>(sender, (object) row, row.RetentionType == "F");
    PXCache pxCache2 = sender;
    ARSetup arSetup2 = row;
    int num2;
    if (!flag1)
    {
      nullable = row.StatementBranchID;
      num2 = nullable.HasValue ? 1 : 0;
    }
    else
      num2 = 1;
    PXUIFieldAttribute.SetVisible<ARSetup.prepareStatements>(pxCache2, (object) arSetup2, num2 != 0);
    PXCache pxCache3 = sender;
    ARSetup arSetup3 = row;
    int num3;
    if (!flag1)
    {
      nullable = row.StatementBranchID;
      num3 = nullable.HasValue ? 1 : 0;
    }
    else
      num3 = 1;
    PXUIFieldAttribute.SetVisible<ARSetup.statementBranchID>(pxCache3, (object) arSetup3, num3 != 0);
    PXUIFieldAttribute.SetEnabled<ARSetup.statementBranchID>(sender, (object) row, row.PrepareStatements == "A");
    PXUIFieldAttribute.SetVisible<ARSetup.prepareDunningLetters>(sender, (object) row, flag2 && flag3 | flag4);
    PXCache pxCache4 = sender;
    ARSetup arSetup4 = row;
    int num4;
    if (!flag1)
    {
      nullable = row.DunningLetterBranchID;
      num4 = nullable.HasValue ? 1 : 0;
    }
    else
      num4 = 1;
    PXUIFieldAttribute.SetVisible<ARSetup.dunningLetterBranchID>(pxCache4, (object) arSetup4, num4 != 0);
    PXUIFieldAttribute.SetEnabled<ARSetup.dunningLetterBranchID>(sender, (object) row, row.PrepareDunningLetters == "A");
    PXUIFieldAttribute.SetVisible<ARSetup.applyQuantityDiscountBy>(sender, (object) row, PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() && PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>());
    PXUIFieldAttribute.SetEnabled<ARSetup.numberOfMonths>(sender, (object) row, row.RetentionType == "F");
    this.VerifyInvoiceRounding(sender, row);
  }

  protected virtual void ARSetup_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is ARSetup row))
      return;
    bool flag1 = PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch>.Config>.Select((PXGraph) this, Array.Empty<object>()).Count > 0;
    PXDefaultAttribute.SetPersistingCheck<ARSetup.statementBranchID>(sender, (object) row, !flag1 || !(row.PrepareStatements == "A") ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<ARSetup.dunningLetterBranchID>(sender, (object) row, !flag1 || !(row.PrepareDunningLetters == "A") ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    bool flag2 = false;
    foreach (PXResult<ARDunningSetup> pxResult in ((PXSelectBase<ARDunningSetup>) this.DunningSetup).Select(Array.Empty<object>()))
    {
      ARDunningSetup arDunningSetup = PXResult<ARDunningSetup>.op_Implicit(pxResult);
      Decimal? dunningFee = arDunningSetup.DunningFee;
      if (dunningFee.HasValue)
      {
        dunningFee = arDunningSetup.DunningFee;
        Decimal num = 0M;
        if (!(dunningFee.GetValueOrDefault() == num & dunningFee.HasValue))
        {
          flag2 = true;
          break;
        }
      }
    }
    PXDefaultAttribute.SetPersistingCheck<ARSetup.dunningFeeInventoryID>(sender, (object) row, flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<ARSetup.prepaymentInvoiceNumberingID>(sender, (object) row, PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected virtual void ARSetup_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.Row is ARSetup row && !row.PrepareDunningLetters.Equals("A"))
      row.DunningLetterBranchID = new int?();
    if (row != null && !row.PrepareStatements.Equals("A"))
      row.StatementBranchID = new int?();
    if (row == null || sender.ObjectsEqual<ARSetup.retentionType>(e.Row, e.OldRow) && sender.ObjectsEqual<ARSetup.numberOfMonths>(e.Row, e.OldRow))
      return;
    if (row.RetentionType == "L")
      sender.RaiseExceptionHandling<ARSetup.retentionType>(e.Row, (object) ((ARSetup) e.Row).RetentionType, (Exception) new PXSetPropertyException("The system retains the last price and the current price for each item.", (PXErrorLevel) 2));
    if (!(row.RetentionType == "F"))
      return;
    int? numberOfMonths = row.NumberOfMonths;
    int num1 = 0;
    if (!(numberOfMonths.GetValueOrDefault() == num1 & numberOfMonths.HasValue))
      sender.RaiseExceptionHandling<ARSetup.retentionType>(e.Row, (object) ((ARSetup) e.Row).RetentionType, (Exception) new PXSetPropertyException("The system retains changes of the price records during {0} months.", (PXErrorLevel) 2, new object[1]
      {
        (object) row.NumberOfMonths
      }));
    numberOfMonths = row.NumberOfMonths;
    int num2 = 0;
    if (!(numberOfMonths.GetValueOrDefault() == num2 & numberOfMonths.HasValue))
      return;
    sender.RaiseExceptionHandling<ARSetup.retentionType>(e.Row, (object) ((ARSetup) e.Row).RetentionType, (Exception) new PXSetPropertyException("The system retains changes of the price records for an unlimited period.", (PXErrorLevel) 2, new object[1]
    {
      (object) row.NumberOfMonths
    }));
  }

  protected virtual void ARSetup_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row is ARSetup row && !row.PrepareDunningLetters.Equals("A"))
      row.DunningLetterBranchID = new int?();
    if (row == null || row.PrepareStatements.Equals("A"))
      return;
    row.StatementBranchID = new int?();
  }

  protected virtual void ARSetup_PrepareDunningLetters_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateDunningBranch(sender, e.Row as ARSetup);
  }

  protected virtual void ARSetup_PrepareStatements_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateStatementBranch(sender, e.Row as ARSetup);
  }

  protected virtual void ARSetup_ConsolidatedStatement_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateStatementBranch(sender, e.Row as ARSetup);
  }

  protected virtual void UpdateStatementBranch(PXCache sender, ARSetup setup)
  {
    if (setup.PrepareStatements == "A")
    {
      setup.ConsolidatedStatement = new bool?(true);
      sender.SetDefaultExt<ARSetup.statementBranchID>((object) setup);
    }
    else
    {
      setup.ConsolidatedStatement = new bool?(false);
      sender.SetValueExt<ARSetup.statementBranchID>((object) setup, (object) null);
      sender.SetValuePending<ARSetup.statementBranchID>((object) setup, (object) null);
    }
  }

  protected virtual void UpdateDunningBranch(PXCache sender, ARSetup setup)
  {
    if (setup.PrepareDunningLetters == "A")
    {
      sender.SetDefaultExt<ARSetup.dunningLetterBranchID>((object) setup);
    }
    else
    {
      sender.SetValueExt<ARSetup.dunningLetterBranchID>((object) setup, (object) null);
      sender.SetValuePending<ARSetup.dunningLetterBranchID>((object) setup, (object) null);
    }
  }

  protected virtual void ARSetup_DunningFeeInventoryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARSetup row) || e.NewValue == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<ARSetup.dunningFeeInventoryID>(sender, (object) row, e.NewValue);
    if (inventoryItem != null && !inventoryItem.SalesAcctID.HasValue)
    {
      e.NewValue = (object) inventoryItem.InventoryCD;
      throw new PXSetPropertyException("The non-stock item has no sales account specified, thus it cannot be used for recording the dunning fee. Select another item or specify a sales account for this item on Non-Stock Items (IN202000).");
    }
  }

  protected bool ShowBranches()
  {
    return this._currentUserInformationProvider.GetAllBranches().Select<BranchInfo, int>((Func<BranchInfo, int>) (b => b.Id)).Distinct<int>().Count<int>() > 1;
  }

  protected virtual void ARSetup_DunningLetterProcessType_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ARSetup row = (ARSetup) e.Row;
    int? oldValue = (int?) e.OldValue;
    if (row == null)
      return;
    int? letterProcessType = row.DunningLetterProcessType;
    int num = 0;
    if (!(letterProcessType.GetValueOrDefault() == num & letterProcessType.HasValue))
      return;
    row.AutoReleaseDunningLetter = new bool?(true);
    if (oldValue.GetValueOrDefault() != 1)
      return;
    cache.RaiseExceptionHandling<ARSetup.dunningLetterProcessType>((object) row, (object) row.DunningLetterProcessType, (Exception) new PXSetPropertyException("If you switch the mode to the By Customer option, the system will assign the highest dunning level found among customer documents to a customer account. The dunning letters will be prepared starting at this level, and the documents that have lower levels will be included in the first prepared letter.", (PXErrorLevel) 2));
  }

  protected virtual void ARSetup_FinChargeNumberingID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARSetup row) || e.NewValue == null)
      return;
    Numbering numbering = (Numbering) PXSelectorAttribute.Select<ARSetup.finChargeNumberingID>(sender, (object) row, e.NewValue);
    if (numbering != null && numbering.UserNumbering.GetValueOrDefault())
      throw new PXSetPropertyException("Manual numbering cannot be used for documents of the Overdue Charge type. Clear the Manual Numbering check box for the {0} numbering sequence on the Numbering Sequences (CS201010) form.", new object[1]
      {
        (object) numbering.NumberingID
      });
  }

  protected virtual void ARSetup_MigrationMode_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARSetup row))
      return;
    bool? oldValue = (bool?) e.OldValue;
    bool? migrationMode = row.MigrationMode;
    if (migrationMode.GetValueOrDefault() && !oldValue.GetValueOrDefault())
    {
      if (PXResultset<PX.Objects.GL.GLTran>.op_Implicit(PXSelectBase<PX.Objects.GL.GLTran, PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.module, Equal<BatchModule.moduleAR>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>())) == null)
        return;
      sender.RaiseExceptionHandling<ARSetup.migrationMode>((object) row, (object) row.MigrationMode, (Exception) new PXSetPropertyException("The General Ledger batches generated in the module exist in the system.", (PXErrorLevel) 2));
    }
    else
    {
      migrationMode = row.MigrationMode;
      if (migrationMode.GetValueOrDefault() || !oldValue.GetValueOrDefault() || PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.released, NotEqual<True>, And<ARRegister.isMigratedRecord, Equal<True>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>())) == null)
        return;
      sender.RaiseExceptionHandling<ARSetup.migrationMode>((object) row, (object) row.MigrationMode, (Exception) new PXSetPropertyException("Migrated documents that have not been released exist in the system.", (PXErrorLevel) 2));
    }
  }

  protected virtual void ARDunningSetup_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    int num1 = 0;
    foreach (PXResult<ARDunningSetup> pxResult in PXSelectBase<ARDunningSetup, PXSelect<ARDunningSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      int num2 = PXResult<ARDunningSetup>.op_Implicit(pxResult).DunningLetterLevel.Value;
      num1 = num1 < num2 ? num2 : num1;
    }
    if (e.Row is ARDunningSetup row && row.DunningLetterLevel.Value < num1)
      throw new PXException("Only last row can be deleted");
  }

  protected virtual void ARDunningSetup_DueDays_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARDunningSetup row))
      return;
    int? nullable = row.DunningLetterLevel;
    int num1 = nullable.Value;
    int int32 = Convert.ToInt32(e.NewValue);
    if (num1 == 1 && int32 <= 0)
      throw new PXSetPropertyException("This value MUST exceed {0}", (PXErrorLevel) 0);
    int num2 = 0;
    int num3 = 0;
    foreach (PXResult<ARDunningSetup> pxResult in ((PXSelectBase<ARDunningSetup>) this.DunningSetup).Select(Array.Empty<object>()))
    {
      ARDunningSetup arDunningSetup = PXResult<ARDunningSetup>.op_Implicit(pxResult);
      nullable = arDunningSetup.DunningLetterLevel;
      if (nullable.Value == num1 - 1)
      {
        nullable = arDunningSetup.DueDays;
        num3 = nullable.Value;
      }
      nullable = arDunningSetup.DunningLetterLevel;
      if (nullable.Value == num1 + 1)
      {
        nullable = arDunningSetup.DueDays;
        num2 = nullable.Value;
      }
    }
    if (int32 <= num3)
      throw new PXSetPropertyException("This value MUST exceed {0}", new object[1]
      {
        (object) num3
      });
    if (int32 >= num2 && num2 > 0)
      throw new PXSetPropertyException("This value can not exceed {0}", new object[1]
      {
        (object) num2
      });
  }

  protected virtual void ARDunningSetup_DueDays_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is ARDunningSetup row))
      return;
    int? nullable = row.DunningLetterLevel;
    if (!nullable.HasValue)
      return;
    nullable = row.DunningLetterLevel;
    int num1 = nullable.Value;
    if (num1 == 1)
    {
      e.NewValue = (object) 30;
    }
    else
    {
      int num2 = 0;
      foreach (PXResult<ARDunningSetup> pxResult in ((PXSelectBase<ARDunningSetup>) this.DunningSetup).Select(Array.Empty<object>()))
      {
        ARDunningSetup arDunningSetup = PXResult<ARDunningSetup>.op_Implicit(pxResult);
        nullable = arDunningSetup.DunningLetterLevel;
        if (nullable.Value == num1 - 1)
        {
          int num3 = num2;
          nullable = arDunningSetup.DueDays;
          int num4 = nullable.Value;
          num2 = num3 + num4;
        }
        nullable = arDunningSetup.DunningLetterLevel;
        if (nullable.Value == 1 && num1 > 1)
        {
          int num5 = num2;
          nullable = arDunningSetup.DueDays;
          int num6 = nullable.Value;
          num2 = num5 + num6;
        }
      }
      e.NewValue = (object) num2;
    }
  }

  protected virtual void ARDunningSetup_DunningLetterLevel_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    object row = e.Row;
    List<ARDunningSetup> list = GraphHelper.RowCast<ARDunningSetup>((IEnumerable) ((PXSelectBase<ARDunningSetup>) this.DunningSetup).Select(Array.Empty<object>())).ToList<ARDunningSetup>();
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    int? nullable;
    if (!list.Any<ARDunningSetup>())
    {
      nullable = new int?(1);
    }
    else
    {
      int? dunningLetterLevel = list.OrderByDescending<ARDunningSetup, int?>((Func<ARDunningSetup, int?>) (_ => _.DunningLetterLevel)).First<ARDunningSetup>().DunningLetterLevel;
      nullable = dunningLetterLevel.HasValue ? new int?(dunningLetterLevel.GetValueOrDefault() + 1) : new int?();
    }
    // ISSUE: variable of a boxed type
    __Boxed<int?> local = (ValueType) nullable;
    defaultingEventArgs.NewValue = (object) local;
  }

  protected virtual void ARDunningSetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARDunningSetup row))
      return;
    int? nullable = row.DunningLetterLevel;
    int num1 = nullable.Value;
    ARDunningSetup arDunningSetup = PXResultset<ARDunningSetup>.op_Implicit(PXSelectBase<ARDunningSetup, PXSelect<ARDunningSetup, Where<ARDunningSetup.dunningLetterLevel, Greater<Required<ARDunningSetup.dunningLetterLevel>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.DunningLetterLevel
    }));
    bool flag = true;
    if (arDunningSetup != null)
    {
      nullable = arDunningSetup.DueDays;
      if (nullable.HasValue)
      {
        nullable = row.DueDays;
        if (nullable.HasValue)
        {
          nullable = row.DaysToSettle;
          if (nullable.HasValue)
          {
            nullable = row.DueDays;
            int num2 = nullable.Value;
            nullable = row.DaysToSettle;
            int num3 = nullable.Value;
            int num4 = num2 + num3;
            nullable = arDunningSetup.DueDays;
            int valueOrDefault = nullable.GetValueOrDefault();
            if (num4 > valueOrDefault & nullable.HasValue)
            {
              string displayName1 = PXUIFieldAttribute.GetDisplayName<ARDunningSetup.dueDays>(sender);
              string displayName2 = PXUIFieldAttribute.GetDisplayName<ARDunningSetup.daysToSettle>(sender);
              sender.RaiseExceptionHandling<ARDunningSetup.daysToSettle>((object) row, (object) row.DaysToSettle, (Exception) new PXSetPropertyException("'{0}'+'{1}' should not exceed the '{0}' of the next level Dunning Letter.", (PXErrorLevel) 2, new object[2]
              {
                (object) displayName1,
                (object) displayName2
              }));
              flag = false;
            }
          }
        }
      }
    }
    if (!flag)
      return;
    sender.RaiseExceptionHandling<ARDunningSetup.daysToSettle>((object) row, (object) row.DaysToSettle, (Exception) null);
  }

  private void VerifyInvoiceRounding(PXCache sender, ARSetup row)
  {
    bool flag = false;
    if (row.InvoiceRounding != "N" && !PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
    {
      Decimal? roundingLimit = CurrencyCollection.GetCurrency(((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID).RoundingLimit;
      Decimal num = 0M;
      if (roundingLimit.GetValueOrDefault() == num & roundingLimit.HasValue)
      {
        flag = true;
        sender.RaiseExceptionHandling<ARSetup.invoiceRounding>((object) row, (object) null, (Exception) new PXSetPropertyException("To use this rounding rule, specify Rounding Limit for the base currency on the Currencies (CM202000) form.", (PXErrorLevel) 2));
      }
    }
    if (flag)
      return;
    sender.RaiseExceptionHandling<ARSetup.invoiceRounding>((object) row, (object) null, (Exception) null);
  }

  public virtual void Persist()
  {
    ((PXGraph) this).Persist();
    ((ICacheControl) this.PageCacheControl).InvalidateCache();
    ((ICacheControl) this.ScreenInfoCacheControl).InvalidateCache();
  }

  [InjectDependency]
  protected ICacheControl<PageCache> PageCacheControl { get; set; }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }
}
