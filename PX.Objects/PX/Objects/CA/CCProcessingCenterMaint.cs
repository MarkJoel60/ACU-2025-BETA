// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCProcessingCenterMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CCProcessingCenterMaint : 
  PXGraph<CCProcessingCenterMaint, CCProcessingCenter>,
  IProcessingCenterSettingsStorage
{
  private Dictionary<string, string> _displayNamesAndTypes;
  public PXSelect<CCProcessingCenter> ProcessingCenter;
  public PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<CCProcessingCenter.processingCenterID>>>> CurrentProcessingCenter;
  public PXSelect<CCProcessingCenterDetail, Where<CCProcessingCenterDetail.processingCenterID, Equal<Current<CCProcessingCenter.processingCenterID>>>> Details;
  public PXSelect<CCProcessingCenterPmntMethod, Where<CCProcessingCenterPmntMethod.processingCenterID, Equal<Current<CCProcessingCenter.processingCenterID>>>> PaymentMethods;
  public PXSelect<CCProcessingCenterPmntMethodBranch, Where<CCProcessingCenterPmntMethodBranch.processingCenterID, Equal<Current<CCProcessingCenter.processingCenterID>>>> BranchProcessingCenters;
  public PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<CCProcessingCenter.cashAccountID>>>> CashAccount;
  public PXSelect<CCProcessingCenterFeeType, Where<CCProcessingCenterFeeType.processingCenterID, Equal<Current<CCProcessingCenter.processingCenterID>>>> FeeTypes;
  public PXAction<CCProcessingCenter> testCredentials;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  private bool errorKey;
  private bool isExportingSettings;

  public virtual Dictionary<string, string> DisplayNamesAndTypes => this._displayNamesAndTypes;

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable TestCredentials(PXAdapter adapter)
  {
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Current;
    if (string.IsNullOrEmpty(current.ProcessingCenterID))
      throw new PXException("Processing Center is not selected.");
    if (string.IsNullOrEmpty(current.ProcessingTypeName))
      throw new PXException("Processing plug-in is not selected.");
    if (PXGraph.CreateInstance<CCPaymentProcessingGraph>().TestCredentials((PXGraph) this, current.ProcessingCenterID))
      ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Ask("Result", "The credentials were accepted by the processing center.", (MessageButtons) 0);
    return adapter.Get();
  }

  public CCProcessingCenterMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    ((PXSelectBase) this.ProcessingCenter).Cache.AllowDelete = false;
    ((PXSelectBase) this.PaymentMethods).Cache.AllowInsert = ((PXSelectBase) this.PaymentMethods).Cache.AllowUpdate = ((PXSelectBase) this.PaymentMethods).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled<CCProcessingCenterPmntMethod.isDefault>(((PXSelectBase) this.PaymentMethods).Cache, (object) null, false);
    this._displayNamesAndTypes = new Dictionary<string, string>();
    this.SetDisplayNamesAndTypes();
  }

  protected virtual void SetDisplayNamesAndTypes()
  {
    foreach (PXProviderTypeSelectorAttribute.ProviderRec pluginRec in PXCCPluginTypeSelectorAttribute.GetPluginRecs())
    {
      if (pluginRec.TypeName != pluginRec.DisplayTypeName)
        this._displayNamesAndTypes.Add(pluginRec.DisplayTypeName, pluginRec.TypeName);
    }
  }

  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void CCProcessingCenterPmntMethod_ProcessingCenterID_CacheAttached(
    PXCache sender)
  {
  }

  protected virtual void CCProcessingCenter_ProcessingTypeName_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is CCProcessingCenter row) || row.ProcessingTypeName == null || ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Ask("Changing the Plug-In Type will reset the details to default values. Continue?", (MessageButtons) 1) != 2)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CCProcessingCenter_ProcessingTypeName_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is CCProcessingCenter row) || row.ProcessingTypeName == null)
      return;
    foreach (KeyValuePair<string, string> displayNamesAndType in this.DisplayNamesAndTypes)
    {
      if (displayNamesAndType.Value.Equals(row.ProcessingTypeName))
      {
        e.ReturnValue = (object) displayNamesAndType.Key;
        break;
      }
    }
  }

  protected virtual void CCProcessingCenter_ProcessingTypeName_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is CCProcessingCenter) || e.NewValue == null)
      return;
    if (this.DisplayNamesAndTypes.ContainsKey(e.NewValue.ToString()))
      e.NewValue = (object) this.DisplayNamesAndTypes[e.NewValue.ToString()];
    CCPluginTypeHelper.ThrowIfProcCenterFeatureDisabled((string) e.NewValue);
  }

  protected virtual void CCProcessingCenter_CreateAdditionalCustomerProfile_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CCProcessingCenter row))
      return;
    if (!CCProcessingFeatureHelper.IsFeatureSupported(row, CCProcessingFeature.ExtendedProfileManagement, true))
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} feature is not supported by processing", new object[1]
      {
        (object) CCProcessingFeature.ExtendedProfileManagement
      }));
    if (!row.CreateAdditionalCustomerProfiles.GetValueOrDefault() || row.CreditCardLimit.HasValue)
      return;
    cache.SetDefaultExt<CCProcessingCenter.creditCardLimit>((object) row);
  }

  protected virtual void CCProcessingCenter_ProcessingTypeName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CCProcessingCenter row))
      return;
    foreach (PXResult<CCProcessingCenterDetail> pxResult in ((PXSelectBase<CCProcessingCenterDetail>) this.Details).Select(Array.Empty<object>()))
      ((PXSelectBase<CCProcessingCenterDetail>) this.Details).Delete(PXResult<CCProcessingCenterDetail>.op_Implicit(pxResult));
    this.ImportSettings();
    if (CCProcessingFeatureHelper.IsPaymentHostedFormSupported(row))
    {
      sender.RaiseExceptionHandling<CCProcessingCenter.useAcceptPaymentForm>((object) ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Current, (object) null, (Exception) new PXSetPropertyException("The check box was selected automatically because this processing center allows accepting payments from new cards. Clear this check box if new cards should be registered on the Customer Payment Methods (AR303010) form only.", (PXErrorLevel) 2));
      row.UseAcceptPaymentForm = new bool?(true);
    }
    if (row.ProcessingTypeName == "PX.CCProcessing.V2.AuthnetProcessingPlugin")
      row.IsExternalAuthorizationOnly = new bool?(true);
    sender.SetDefaultExt<CCProcessingCenter.allowAuthorizedIncrement>((object) row);
    sender.SetDefaultExt<CCProcessingCenter.acceptPOSPayments>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CCProcessingCenter, CCProcessingCenter.cashAccountID> e)
  {
    if (!(PX.Objects.CA.CashAccount.PK.Find((PXGraph) this, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CCProcessingCenter, CCProcessingCenter.cashAccountID>, CCProcessingCenter, object>) e).OldValue as int?)?.CuryID != PX.Objects.CA.CashAccount.PK.Find((PXGraph) this, e.NewValue as int?)?.CuryID))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenter, CCProcessingCenter.cashAccountID>>) e).Cache.SetValueExt<CCProcessingCenter.depositAccountID>((object) e.Row, (object) null);
    UIState.RaiseOrHideErrorByErrorLevelPriority<CCProcessingCenter.depositAccountID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenter, CCProcessingCenter.cashAccountID>>) e).Cache, (object) e.Row, e.Row.ImportSettlementBatches.GetValueOrDefault(), "Select an account with the same currency as the cash account.", (PXErrorLevel) 2);
  }

  protected virtual void CCProcessingCenter_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is CCProcessingCenter row))
      return;
    using (new PXConnectionScope())
      row.NeedsExpDateUpdate = CCProcessingHelper.CCProcessingCenterNeedsExpDateUpdate((PXGraph) this, row);
  }

  protected virtual void CCProcessingCenter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CCProcessingCenter row))
      return;
    PXCache pxCache1 = sender;
    CCProcessingCenter processingCenter1 = row;
    bool? nullable = row.IsActive;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CCProcessingCenter.processingTypeName>(pxCache1, (object) processingCenter1, num1 != 0);
    PXCache pxCache2 = sender;
    nullable = row.IsActive;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetRequired<CCProcessingCenter.processingTypeName>(pxCache2, num2 != 0);
    bool flag1 = CCProcessingFeatureHelper.IsFeatureSupported(row, CCProcessingFeature.HostedForm) || CCProcessingFeatureHelper.IsFeatureSupported(row, CCProcessingFeature.ProfileForm);
    bool flag2 = CCProcessingFeatureHelper.IsFeatureSupported(row, CCProcessingFeature.ExtendedProfileManagement);
    bool flag3 = row.ProcessingTypeName != null;
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.allowDirectInput>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.syncronizeDeletion>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.syncRetryAttemptsNo>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.syncRetryDelayMs>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.allowSaveProfile>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.allowUnlinkedRefund>(sender, (object) row, flag3);
    bool flag4 = flag1 && CCProcessingFeatureHelper.IsFeatureSupported(row, CCProcessingFeature.ExtendedProfileManagement);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.createAdditionalCustomerProfiles>(sender, (object) row, flag4);
    PXCache pxCache3 = sender;
    CCProcessingCenter processingCenter2 = row;
    int num3;
    if (flag4)
    {
      nullable = row.CreateAdditionalCustomerProfiles;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.creditCardLimit>(pxCache3, (object) processingCenter2, num3 != 0);
    nullable = row.ImportSettlementBatches;
    bool valueOrDefault1 = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<CCProcessingCenter.importStartDate>(sender, (object) row, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenter.depositAccountID>(sender, (object) row, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenter.autoCreateBankDeposit>(sender, (object) row, valueOrDefault1);
    PXDefaultAttribute.SetPersistingCheck<CCProcessingCenter.importStartDate>(sender, e.Row, valueOrDefault1 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CCProcessingCenter.depositAccountID>(sender, e.Row, valueOrDefault1 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    bool flag5 = CCProcessingFeatureHelper.IsFeatureSupported(row, CCProcessingFeature.AuthorizeIncrement);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.allowAuthorizedIncrement>(sender, (object) row, flag5);
    ((PXAction) this.testCredentials).SetEnabled(!string.IsNullOrEmpty(row.ProcessingCenterID) && !string.IsNullOrEmpty(row.ProcessingTypeName));
    ((PXSelectBase) this.ProcessingCenter).Cache.AllowDelete = !this.HasTransactions(row);
    ((PXSelectBase) this.PaymentMethods).AllowInsert = false;
    ((PXSelectBase) this.PaymentMethods).AllowDelete = false;
    this.SetAllowAcceptFormCheckbox(sender, row);
    this.CheckUsingDirectInputMode(sender, e);
    this.CheckBatchImportSettings(sender, row);
    this.ShowAllowSaveCardCheckboxWarnIfNeeded(sender, row);
    this.ShowAllowUnlinkedRefundWarnIfNeeded(sender, row);
    nullable = row.IsExternalAuthorizationOnly;
    bool valueOrDefault2 = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.reauthRetryNbr>(sender, (object) row, !valueOrDefault2);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.reauthRetryDelay>(sender, (object) row, !valueOrDefault2);
    PXUIFieldAttribute.SetVisible<CCProcessingCenterPmntMethod.reauthDelay>(((PXSelectBase) this.PaymentMethods).Cache, (object) null, !valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenter.useAcceptPaymentForm>(sender, (object) row, !valueOrDefault2);
    UIState.RaiseOrHideErrorByErrorLevelPriority<CCProcessingCenter.processingTypeName>(sender, (object) row, (valueOrDefault2 ? 1 : 0) != 0, "The {0} processing center does not support the Authorize action. The Capture action is supported only for payments that were pre-authorized externally.", (PXErrorLevel) 2, (object) row?.ProcessingCenterID);
  }

  protected virtual void CCProcessingCenter_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if (!(e.Row is CCProcessingCenter row))
      return;
    bool? nullable;
    if (e.Operation == 2)
    {
      nullable = !CCProcessingCenterMaint.CheckUseAimPlugin(row) ? row.AllowDirectInput : throw new PXRowPersistingException(typeof (CCProcessingCenter.processingTypeName).Name, (object) null, "The Authorize.Net AIM plug-in cannot be used for new processing center configurations. Use another plug-in.");
      if (nullable.GetValueOrDefault())
        throw new PXRowPersistingException("allowDirectInput", (object) null, "The processing center configuration cannot be saved with the Allow Direct Input check box selected because the direct input mode is no longer supported. Please configure processing centers to use processing plug-ins that support hosted forms.");
    }
    if (e.Operation == 1)
    {
      if (((PXSelectBase<CCProcessingCenter>) new PXSelectReadonly<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>((PXGraph) this)).SelectSingle(new object[1]
      {
        (object) row.ProcessingCenterID
      })?.ProcessingTypeName != row.ProcessingTypeName && CCProcessingCenterMaint.CheckUseAimPlugin(row))
        throw new PXRowPersistingException(typeof (CCProcessingCenter.processingTypeName).Name, (object) null, "The Authorize.Net AIM plug-in cannot be used for new processing center configurations. Use another plug-in.");
      nullable = row.AllowDirectInput;
      if (nullable.GetValueOrDefault())
        throw new PXRowPersistingException("allowDirectInput", (object) null, "The processing center configuration cannot be saved with the Allow Direct Input check box selected because the direct input mode is no longer supported. Please configure processing centers to use processing plug-ins that support hosted forms.");
    }
    if ((e.Operation & 3) == 3)
      return;
    nullable = row.IsActive;
    if (!nullable.GetValueOrDefault() || !string.IsNullOrEmpty(row.ProcessingTypeName))
      return;
    if (sender.RaiseExceptionHandling<CCProcessingCenter.processingTypeName>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) typeof (CCProcessingCenter.processingTypeName).Name
    })))
      throw new PXRowPersistingException(typeof (CCProcessingCenter.processingTypeName).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (CCProcessingCenter.processingTypeName).Name
      });
  }

  private static bool CheckUseAimPlugin(CCProcessingCenter processingCenter)
  {
    bool flag = false;
    if (processingCenter.ProcessingTypeName == "PX.CCProcessing.AuthorizeNetProcessing")
      flag = true;
    return flag;
  }

  protected virtual void CCProcessingCenterDetail_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<CCProcessingCenterDetail.detailID>(cache, e.Row, false);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenterDetail.descr>(cache, e.Row, false);
    if (!(e.Row is CCProcessingCenterDetail row) || !row.IsEncryptionRequired.GetValueOrDefault() || row.IsEncrypted.GetValueOrDefault() || ((PXSelectBase) this.Details).Cache.GetStatus((object) row) != null)
      return;
    ((PXSelectBase) this.Details).Cache.SetStatus((object) row, (PXEntryStatus) 1);
    ((PXSelectBase) this.Details).Cache.IsDirty = true;
    PXUIFieldAttribute.SetWarning<CCProcessingCenterDetail.value>(((PXSelectBase) this.Details).Cache, (object) row, "Encryption settings were changed during last system update. To finalize changes please press save button manually.");
  }

  protected virtual void CCProcessingCenterDetail_RowInserting(
    PXCache cache,
    PXRowInsertingEventArgs e)
  {
    if (this.errorKey)
    {
      this.errorKey = false;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      string detailId = ((CCProcessingCenterDetail) e.Row).DetailID;
      bool flag = false;
      foreach (PXResult<CCProcessingCenterDetail> pxResult in ((PXSelectBase<CCProcessingCenterDetail>) this.Details).Select(Array.Empty<object>()))
      {
        if (PXResult<CCProcessingCenterDetail>.op_Implicit(pxResult).DetailID == detailId)
          flag = true;
      }
      if (!flag)
        return;
      cache.RaiseExceptionHandling<CCProcessingCenterDetail.detailID>(e.Row, (object) detailId, (Exception) new PXException("Row is duplicated"));
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CCProcessingCenterDetail_DetailID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if ((e.Row as CCProcessingCenterDetail).DetailID == null)
      return;
    this.errorKey = true;
  }

  protected virtual void CCProcessingCenterPmntMethod_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CCProcessingCenterPmntMethod))
      return;
    PXUIFieldAttribute.SetEnabled<CCProcessingCenterPmntMethod.paymentMethodID>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenterPmntMethod.isActive>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenterPmntMethod.isDefault>(sender, (object) null, false);
  }

  protected virtual void CCProcessingCenterPmntMethod_RowInserting(
    PXCache cache,
    PXRowInsertingEventArgs e)
  {
    if (this.errorKey)
    {
      this.errorKey = false;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      CCProcessingCenterPmntMethod row = e.Row as CCProcessingCenterPmntMethod;
      string processingCenterId = row.ProcessingCenterID;
      bool flag = false;
      foreach (PXResult<CCProcessingCenterPmntMethod> pxResult in ((PXSelectBase<CCProcessingCenterPmntMethod>) this.PaymentMethods).Select(Array.Empty<object>()))
      {
        CCProcessingCenterPmntMethod centerPmntMethod = PXResult<CCProcessingCenterPmntMethod>.op_Implicit(pxResult);
        if (centerPmntMethod != row && centerPmntMethod.PaymentMethodID == row.PaymentMethodID && centerPmntMethod.ProcessingCenterID == centerPmntMethod.ProcessingCenterID)
          flag = true;
      }
      if (!flag)
        return;
      cache.RaiseExceptionHandling<CCProcessingCenterPmntMethod.paymentMethodID>(e.Row, (object) processingCenterId, (Exception) new PXException("This Payment Method is already assigned to the Processing Center"));
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void _DepositAccountFieldUpdating(
    PX.Data.Events.FieldUpdating<CCProcessingCenter.depositAccountID> e)
  {
    if (!((PXGraph) this).IsCopyPasteContext)
      return;
    ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).SelectSingle(Array.Empty<object>());
  }

  protected virtual void CCProcessingCenterPmntMethod_ProcessingCenterID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if ((e.Row as CCProcessingCenterPmntMethod).PaymentMethodID == null)
      return;
    this.errorKey = true;
  }

  protected virtual bool HasTransactions(CCProcessingCenter aRow)
  {
    return PXResultset<CCProcTran>.op_Implicit(PXSelectBase<CCProcTran, PXSelect<CCProcTran, Where<CCProcTran.processingCenterID, Equal<Required<CCProcTran.processingCenterID>>>, OrderBy<Desc<CCProcTran.tranNbr>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) aRow.ProcessingCenterID
    })) != null;
  }

  private void SetAllowAcceptFormCheckbox(PXCache cache, CCProcessingCenter processingCenter)
  {
    bool flag = CCProcessingFeatureHelper.IsPaymentHostedFormSupported(processingCenter);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenter.useAcceptPaymentForm>(cache, (object) processingCenter, flag);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.useAcceptPaymentForm>(cache, (object) processingCenter, flag);
    if (flag)
      return;
    cache.SetValueExt<CCProcessingCenter.useAcceptPaymentForm>((object) processingCenter, (object) flag);
  }

  private void CheckUsingDirectInputMode(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CCProcessingCenter row))
      return;
    bool valueOrDefault = row.AllowDirectInput.GetValueOrDefault();
    if (valueOrDefault)
      cache.RaiseExceptionHandling<CCProcessingCenter.allowDirectInput>(e.Row, (object) valueOrDefault, (Exception) new PXSetPropertyException("The processing center uses the direct input mode, which is no longer supported.", (PXErrorLevel) 2));
    else
      cache.RaiseExceptionHandling<CCProcessingCenter.allowDirectInput>(e.Row, (object) valueOrDefault, (Exception) null);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenter.allowDirectInput>(cache, e.Row, valueOrDefault);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.allowDirectInput>(cache, e.Row, valueOrDefault);
  }

  private void CheckBatchImportSettings(PXCache cache, CCProcessingCenter row)
  {
    int num;
    if (row.ImportSettlementBatches.GetValueOrDefault())
    {
      DateTime? importStartDate = row.ImportStartDate;
      DateTime? lastSettlementDate = row.LastSettlementDate;
      if ((importStartDate.HasValue & lastSettlementDate.HasValue ? (importStartDate.GetValueOrDefault() < lastSettlementDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        DateTime today = DateTime.Today;
        lastSettlementDate = row.LastSettlementDate;
        num = (lastSettlementDate.HasValue ? new TimeSpan?(today - lastSettlementDate.GetValueOrDefault()) : new TimeSpan?()).Value.Days > 31 /*0x1F*/ ? 1 : 0;
        goto label_4;
      }
    }
    num = 0;
label_4:
    bool flag = num != 0;
    UIState.RaiseOrHideErrorByErrorLevelPriority<CCProcessingCenter.importStartDate>(cache, (object) row, (flag ? 1 : 0) != 0, "Settlement batches starting from {0} will be imported during the next import. If you want to import batches from a later date, specify the Import Start Date.", (PXErrorLevel) 2, (object) row.LastSettlementDate);
  }

  private void ShowAllowUnlinkedRefundWarnIfNeeded(
    PXCache cache,
    CCProcessingCenter processingCenter)
  {
    if (processingCenter != null)
    {
      bool? allowUnlinkedRefund = processingCenter.AllowUnlinkedRefund;
      bool flag = false;
      if (allowUnlinkedRefund.GetValueOrDefault() == flag & allowUnlinkedRefund.HasValue)
        return;
    }
    if (processingCenter == null || processingCenter.ProcessingTypeName == null)
      return;
    cache.RaiseExceptionHandling<CCProcessingCenter.allowUnlinkedRefund>((object) ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Current, (object) null, (Exception) new PXSetPropertyException("The processing center may require you to sign an additional agreement to be able to process unlinked refunds.", (PXErrorLevel) 2));
  }

  private void ShowAllowSaveCardCheckboxWarnIfNeeded(
    PXCache cache,
    CCProcessingCenter processingCenter)
  {
    if (string.IsNullOrEmpty(processingCenter?.ProcessingTypeName))
      return;
    string str = (string) null;
    try
    {
      if (!CCProcessingFeatureHelper.IsFeatureSupported(processingCenter, CCProcessingFeature.TransactionGetter, true))
        str = "Customer payment methods cannot be created for imported transactions with saved cards. The processing center plug-in does not support getting a customer payment profile for a transaction.";
    }
    catch (PXException ex)
    {
      str = ((Exception) ex).Message;
    }
    if (str == null)
      return;
    cache.RaiseExceptionHandling<CCProcessingCenter.allowSaveProfile>((object) ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Current, (object) null, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 2));
  }

  public virtual void ReadSettings(
    Dictionary<string, CCProcessingCenterDetail> aSettings)
  {
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Current;
    foreach (PXResult<CCProcessingCenterDetail> pxResult in ((PXSelectBase<CCProcessingCenterDetail>) this.Details).Select(Array.Empty<object>()))
    {
      CCProcessingCenterDetail processingCenterDetail = PXResult<CCProcessingCenterDetail>.op_Implicit(pxResult);
      aSettings[processingCenterDetail.DetailID] = processingCenterDetail;
    }
  }

  public virtual void ReadSettings(Dictionary<string, string> aSettings)
  {
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Current;
    foreach (PXResult<CCProcessingCenterDetail> pxResult in ((PXSelectBase<CCProcessingCenterDetail>) this.Details).Select(Array.Empty<object>()))
    {
      CCProcessingCenterDetail processingCenterDetail = PXResult<CCProcessingCenterDetail>.op_Implicit(pxResult);
      aSettings[processingCenterDetail.DetailID] = processingCenterDetail.Value;
    }
  }

  protected virtual void ImportSettings()
  {
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Current;
    if (string.IsNullOrEmpty(current.ProcessingCenterID))
      throw new PXException("Processing CenterID is required for this operation");
    if (string.IsNullOrEmpty(current.ProcessingTypeName))
      throw new PXException("Type of the object for the Credit Card processing is not specified");
    this.ImportSettingsFromPC();
  }

  private void ImportSettingsFromPC()
  {
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Current;
    Dictionary<string, CCProcessingCenterDetail> aSettings = new Dictionary<string, CCProcessingCenterDetail>();
    this.ReadSettings(aSettings);
    IList<PluginSettingDetail> pluginSettingDetailList = PXGraph.CreateInstance<CCPaymentProcessingGraph>().ExportSettings((PXGraph) this, current.ProcessingCenterID);
    this.isExportingSettings = true;
    foreach (PluginSettingDetail src in (IEnumerable<PluginSettingDetail>) pluginSettingDetailList)
    {
      if (!aSettings.ContainsKey(src.DetailID))
      {
        CCProcessingCenterDetail dst = new CCProcessingCenterDetail();
        CCProcessingCenterDetail.Copy(src, dst);
        ((PXSelectBase<CCProcessingCenterDetail>) this.Details).Insert(dst);
      }
      else
      {
        CCProcessingCenterDetail dst = aSettings[src.DetailID];
        CCProcessingCenterDetail.Copy(src, dst);
        ((PXSelectBase<CCProcessingCenterDetail>) this.Details).Update(dst);
      }
    }
    this.isExportingSettings = false;
  }

  protected virtual void CCProcessingCenterDetail_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is CCProcessingCenterDetail row))
      return;
    string name = typeof (CCProcessingCenterDetail.value).Name;
    int? controlType = row.ControlType;
    if (!controlType.HasValue)
      return;
    switch (controlType.GetValueOrDefault())
    {
      case 2:
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        foreach (KeyValuePair<string, string> comboValues in (IEnumerable<KeyValuePair<string, string>>) row.ComboValuesCollection)
        {
          stringList2.Add(comboValues.Key);
          stringList1.Add(comboValues.Value);
        }
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1024 /*0x0400*/), new bool?(), name, new bool?(false), new int?(1), (string) null, stringList2.ToArray(), stringList1.ToArray(), new bool?(true), (string) null, (string[]) null);
        break;
      case 3:
        e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (bool), new bool?(false), new bool?(), new int?(-1), new int?(), new int?(), (object) null, name, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
        break;
    }
  }

  protected virtual void CCProcessingCenterDetail_Value_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (this.isExportingSettings)
      return;
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).Current;
    CCProcessingCenterDetail row = (CCProcessingCenterDetail) e.Row;
    if (((IEnumerable<string>) InterfaceConstants.SpecialDetailIDs).Contains<string>(row.DetailID))
      return;
    CCPaymentProcessingGraph instance = PXGraph.CreateInstance<CCPaymentProcessingGraph>();
    PluginSettingDetail pluginSettingDetail = new PluginSettingDetail()
    {
      DetailID = row.DetailID,
      Value = (string) e.NewValue
    };
    string processingCenterId = current.ProcessingCenterID;
    PluginSettingDetail settingDetail = pluginSettingDetail;
    instance.ValidateSettings((PXGraph) this, processingCenterId, settingDetail);
  }
}
