// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.CCProcessingCenterMaintPayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Webhooks.DAC;
using PX.Api.Webhooks.Graph;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.CA;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class CCProcessingCenterMaintPayLink : 
  PXGraphExtension<CCProcessingCenterMaint>,
  PXImportAttribute.IPXPrepareItems
{
  [PXImport(typeof (CCProcessingCenter))]
  public PXSelect<CCProcessingCenterBranch, Where<CCProcessingCenterBranch.processingCenterID, Equal<Current<CCProcessingCenter.processingCenterID>>>, OrderBy<Asc<CCProcessingCenterBranch.branchID>>> ProcCenterBranch;
  public PXSelect<WebHook, Where<WebHook.webHookID, Equal<Current<CCProcessingCenter.webhookID>>>> Webhook;
  public PXAction<CCProcessingCenter> createWebhook;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable CreateWebhook(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCProcessingCenterMaintPayLink.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new CCProcessingCenterMaintPayLink.\u003C\u003Ec__DisplayClass4_0();
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.Base.ProcessingCenter).Current;
    if (current == null)
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.webHook = this.CreateLocalWebhook();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.copy = ((PXSelectBase) this.Base.ProcessingCenter).Cache.CreateCopy((object) current) as CCProcessingCenter;
    // ISSUE: method pointer
    PXLongOperation.StartOperation<CCProcessingCenterMaint>((PXGraphExtension<CCProcessingCenterMaint>) this, new PXToggleAsyncDelegate((object) cDisplayClass40, __methodptr(\u003CCreateWebhook\u003Eb__0)));
    return adapter.Get();
  }

  public void CreatePayLinkWebhook(WebHook webhook)
  {
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.Base.ProcessingCenter).Current;
    if (!current.WebhookID.HasValue)
      return;
    Guid? webHookId = webhook.WebHookID;
    Guid? webhookId = current.WebhookID;
    if ((webHookId.HasValue == webhookId.HasValue ? (webHookId.HasValue ? (webHookId.GetValueOrDefault() == webhookId.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      return;
    this.GetPayLinkProcessing().CreateWebhook(current, webhook);
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (viewName == "ProcCenterBranch")
    {
      this.ClearValueFromDescription(values, "cCPaymentMethodID");
      this.ClearValueFromDescription(values, "eFTPaymentMethodID");
      this.ClearValueFromDescription(values, "cCCashAccountID");
      this.ClearValueFromDescription(values, "eFTCashAccountID");
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => true;

  public bool RowImported(string viewName, object row, object oldRow) => true;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<CCProcessingCenter> e)
  {
    CCProcessingCenter row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CCProcessingCenter>>) e).Cache;
    if (row == null)
      return;
    bool flag = this.IsFeatureSupported(row);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenter.allowPayLink>(cache, (object) row, flag);
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.allowPayLink>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenter.webhookID>(cache, (object) row, false);
    ((PXAction) this.createWebhook).SetEnabled(row.AllowPayLink.GetValueOrDefault() & flag);
    ((PXAction) this.createWebhook).SetVisible(row.AllowPayLink.GetValueOrDefault() & flag);
    this.ShowMappingNotDefinedWarnIfNeeded(cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CCProcessingCenter, CCProcessingCenter.processingTypeName> e)
  {
    CCProcessingCenter row = e.Row;
    if (row == null || !row.AllowPayLink.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenter, CCProcessingCenter.processingTypeName>>) e).Cache.SetDefaultExt<CCProcessingCenter.allowPayLink>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CCProcessingCenter, CCProcessingCenter.allowPayLink> e)
  {
    if (e.Row == null)
      return;
    bool? newValue = (bool?) e.NewValue;
    if (!((bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CCProcessingCenter, CCProcessingCenter.allowPayLink>, CCProcessingCenter, object>) e).OldValue).GetValueOrDefault())
      return;
    bool? nullable = newValue;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    foreach (CCProcessingCenterBranch processingCenterBranch in GraphHelper.RowCast<CCProcessingCenterBranch>((IEnumerable) ((PXSelectBase<CCProcessingCenterBranch>) this.ProcCenterBranch).Select(Array.Empty<object>())))
    {
      if (((PXSelectBase) this.ProcCenterBranch).Cache.GetStatus((object) processingCenterBranch) == 2)
        ((PXSelectBase<CCProcessingCenterBranch>) this.ProcCenterBranch).Delete(processingCenterBranch);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CCProcessingCenterBranch, CCProcessingCenterBranch.branchID> e)
  {
    CCProcessingCenterBranch row = e.Row;
    if (row == null || ((PXGraph) this.Base).IsImportFromExcel || e.NewValue == null)
      return;
    CCProcessingCenterBranch processingCenterBranch = GraphHelper.RowCast<CCProcessingCenterBranch>(((PXSelectBase) this.ProcCenterBranch).Cache.Inserted).LastOrDefault<CCProcessingCenterBranch>() ?? GraphHelper.RowCast<CCProcessingCenterBranch>((IEnumerable) ((PXSelectBase<CCProcessingCenterBranch>) this.ProcCenterBranch).Select(Array.Empty<object>())).LastOrDefault<CCProcessingCenterBranch>();
    if (processingCenterBranch == null)
      return;
    if (row.CCPaymentMethodID == null)
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenterBranch, CCProcessingCenterBranch.branchID>>) e).Cache.SetValueExt<CCProcessingCenterBranch.cCPaymentMethodID>((object) row, (object) processingCenterBranch.CCPaymentMethodID);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenterBranch, CCProcessingCenterBranch.branchID>>) e).Cache.SetValueExt<CCProcessingCenterBranch.cCCashAccountID>((object) row, (object) processingCenterBranch.CCCashAccountID);
    }
    if (row.EFTPaymentMethodID != null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenterBranch, CCProcessingCenterBranch.branchID>>) e).Cache.SetValueExt<CCProcessingCenterBranch.eFTPaymentMethodID>((object) row, (object) processingCenterBranch.EFTPaymentMethodID);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenterBranch, CCProcessingCenterBranch.branchID>>) e).Cache.SetValueExt<CCProcessingCenterBranch.eFTCashAccountID>((object) row, (object) processingCenterBranch.EFTCashAccountID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CCProcessingCenterBranch, CCProcessingCenterBranch.cCPaymentMethodID> e)
  {
    CCProcessingCenterBranch row = e.Row;
    if (row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenterBranch, CCProcessingCenterBranch.cCPaymentMethodID>>) e).Cache.SetValueExt<CCProcessingCenterBranch.cCCashAccountID>((object) row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CCProcessingCenterBranch, CCProcessingCenterBranch.eFTPaymentMethodID> e)
  {
    CCProcessingCenterBranch row = e.Row;
    if (row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenterBranch, CCProcessingCenterBranch.eFTPaymentMethodID>>) e).Cache.SetValueExt<CCProcessingCenterBranch.eFTCashAccountID>((object) row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CCProcessingCenter, CCProcessingCenter.cashAccountID> e)
  {
    foreach (PXResult<CCProcessingCenterBranch> pxResult in ((PXSelectBase<CCProcessingCenterBranch>) this.ProcCenterBranch).Select(Array.Empty<object>()))
      ((PXSelectBase<CCProcessingCenterBranch>) this.ProcCenterBranch).Delete(PXResult<CCProcessingCenterBranch>.op_Implicit(pxResult));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CCProcessingCenterBranch.defaultForBranch> e)
  {
    CCProcessingCenterBranch row = e.Row as CCProcessingCenterBranch;
    bool? newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CCProcessingCenterBranch.defaultForBranch>, object, object>) e).NewValue;
    if (row == null || !newValue.HasValue || !newValue.GetValueOrDefault())
      return;
    CCProcessingCenterBranch procCenterForBranch = this.GetDefaultProcCenterForBranch(row);
    if (procCenterForBranch == null)
      return;
    PX.Objects.GL.Branch branchById = this.GetBranchById(procCenterForBranch.BranchID);
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<CCProcessingCenterBranch.defaultForBranch>>) e).Cache.RaiseExceptionHandling("defaultForBranch", (object) row, (object) true, (Exception) new PXSetPropertyException("The {0} processing center has been selected as default for the {1} branch. Only one processing center can be defined as default for a single branch-currency combination.", (PXErrorLevel) 4, new object[2]
    {
      (object) procCenterForBranch.ProcessingCenterID,
      (object) branchById.BranchCD
    }));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CCProcessingCenterBranch.cCCashAccountID> e)
  {
    CCProcessingCenterBranch row = e.Row as CCProcessingCenterBranch;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CCProcessingCenterBranch.cCCashAccountID>, object, object>) e).NewValue;
    if (row == null || !newValue.HasValue)
      return;
    CashAccount cashAccount = ((PXSelectBase<CashAccount>) this.Base.CashAccount).SelectSingle(Array.Empty<object>());
    if (cashAccount == null)
      return;
    CashAccount cashAccountById = this.GetCashAccountById(newValue);
    if (!(cashAccount.CuryID != cashAccountById.CuryID))
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<CCProcessingCenterBranch.cCCashAccountID>>) e).Cache.RaiseExceptionHandling("cCCashAccountID", (object) row, (object) cashAccountById.CashAccountCD, (Exception) new PXSetPropertyException("Select a cash account in the {0} currency.", (PXErrorLevel) 4, new object[1]
    {
      (object) cashAccount.CuryID
    }));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CCProcessingCenterBranch.eFTCashAccountID> e)
  {
    object row = e.Row;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CCProcessingCenterBranch.eFTCashAccountID>, object, object>) e).NewValue;
    if (row == null || !newValue.HasValue)
      return;
    CashAccount cashAccount = ((PXSelectBase<CashAccount>) this.Base.CashAccount).SelectSingle(Array.Empty<object>());
    if (cashAccount == null)
      return;
    CashAccount cashAccountById = this.GetCashAccountById(newValue);
    if (!(cashAccount.CuryID != cashAccountById.CuryID))
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<CCProcessingCenterBranch.eFTCashAccountID>>) e).Cache.RaiseExceptionHandling("eFTCashAccountID", row, (object) cashAccountById.CashAccountCD, (Exception) new PXSetPropertyException("Select a cash account in the {0} currency.", (PXErrorLevel) 4, new object[1]
    {
      (object) cashAccount.CuryID
    }));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CCProcessingCenter> e)
  {
    CCProcessingCenter row = e.Row;
    if ((row != null ? (!row.WebhookID.HasValue ? 1 : 0) : 1) != 0)
      return;
    if (e.Operation == 1)
    {
      ((PXSelectBase<WebHook>) this.Webhook).Current = ((PXSelectBase<WebHook>) this.Webhook).SelectSingle(Array.Empty<object>());
      CCProcessingCenter original = (CCProcessingCenter) ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CCProcessingCenter>>) e).Cache.GetOriginal((object) e.Row);
      bool? isActive = original.IsActive;
      bool? nullable1 = row.IsActive;
      if (isActive.GetValueOrDefault() == nullable1.GetValueOrDefault() & isActive.HasValue == nullable1.HasValue)
      {
        nullable1 = original.AllowPayLink;
        bool? allowPayLink = row.AllowPayLink;
        if (nullable1.GetValueOrDefault() == allowPayLink.GetValueOrDefault() & nullable1.HasValue == allowPayLink.HasValue)
          return;
      }
      WebHook current = ((PXSelectBase<WebHook>) this.Webhook).Current;
      bool? nullable2 = row.IsActive;
      int num;
      if (nullable2.GetValueOrDefault())
      {
        nullable2 = row.AllowPayLink;
        num = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      else
        num = 0;
      bool? nullable3 = new bool?(num != 0);
      current.IsActive = nullable3;
      ((PXSelectBase<WebHook>) this.Webhook).UpdateCurrent();
    }
    else
    {
      if (e.Operation != 3)
        return;
      ((PXSelectBase<WebHook>) this.Webhook).Current = WebHook.PK.Find((PXGraph) this.Base, row.WebhookID, (PKFindOptions) 0);
      ((PXSelectBase<WebHook>) this.Webhook).DeleteCurrent();
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CCProcessingCenterBranch> e)
  {
    CCProcessingCenterBranch row = e.Row;
    if (row == null || e.Operation != 2 && e.Operation != 1)
      return;
    bool flag1 = row.CCPaymentMethodID != null;
    bool flag2 = row.EFTPaymentMethodID != null;
    int? nullable = row.BranchID;
    if (nullable.HasValue && row.CCPaymentMethodID == null && row.EFTPaymentMethodID == null)
    {
      PX.Objects.GL.Branch branchById = this.GetBranchById(row.BranchID);
      throw new PXRowPersistingException("BranchID", (object) branchById.BranchCD, "Specify at least one payment method and cash account for the {0} branch.", new object[1]
      {
        (object) branchById.BranchCD.Trim()
      });
    }
    nullable = row.CCCashAccountID;
    if (!nullable.HasValue)
    {
      nullable = row.EFTCashAccountID;
      if (!nullable.HasValue)
        goto label_11;
    }
    CashAccount cashAccount = ((PXSelectBase<CashAccount>) this.Base.CashAccount).SelectSingle(Array.Empty<object>());
    if (cashAccount != null)
    {
      CashAccount cashAccountById1 = this.GetCashAccountById(row.CCCashAccountID);
      if (cashAccountById1 != null && cashAccount.CuryID != cashAccountById1.CuryID)
        throw new PXRowPersistingException("cCCashAccountID", (object) cashAccountById1.CashAccountCD, "Select a cash account in the {0} currency.", new object[1]
        {
          (object) cashAccount.CuryID
        });
      CashAccount cashAccountById2 = this.GetCashAccountById(row.EFTCashAccountID);
      if (cashAccountById2 != null && cashAccount.CuryID != cashAccountById2.CuryID)
        throw new PXRowPersistingException("eFTCashAccountID", (object) cashAccountById2.CashAccountCD, "Select a cash account in the {0} currency.", new object[1]
        {
          (object) cashAccount.CuryID
        });
    }
label_11:
    if (flag1)
    {
      nullable = row.CCCashAccountID;
      if (!nullable.HasValue)
        throw new PXRowPersistingException("cCCashAccountID", (object) row.CCCashAccountID, "'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<CCProcessingCenterBranch.cCCashAccountID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CCProcessingCenterBranch>>) e).Cache)
        });
    }
    if (flag2)
    {
      nullable = row.EFTCashAccountID;
      if (!nullable.HasValue)
        throw new PXRowPersistingException("eFTCashAccountID", (object) row.EFTCashAccountID, "'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<CCProcessingCenterBranch.eFTCashAccountID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CCProcessingCenterBranch>>) e).Cache)
        });
    }
    if (!row.DefaultForBranch.GetValueOrDefault())
      return;
    CCProcessingCenterBranch procCenterForBranch = this.GetDefaultProcCenterForBranch(row);
    if (procCenterForBranch != null)
    {
      PX.Objects.GL.Branch branchById = this.GetBranchById(procCenterForBranch.BranchID);
      throw new PXRowPersistingException("defaultForBranch", (object) true, "The {0} processing center has been selected as default for the {1} branch. Only one processing center can be defined as default for a single branch-currency combination.", new object[2]
      {
        (object) procCenterForBranch.ProcessingCenterID,
        (object) branchById.BranchCD
      });
    }
  }

  protected virtual WebHook CreateLocalWebhook()
  {
    WebhookMaint instance = PXGraph.CreateInstance<WebhookMaint>();
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.Base.ProcessingCenter).Current;
    WebHook localWebhook;
    if (!current.WebhookID.HasValue)
    {
      localWebhook = this.CreateLocalWebhook(instance);
    }
    else
    {
      ((PXSelectBase<WebHook>) instance.Webhook).Current = PXResultset<WebHook>.op_Implicit(((PXSelectBase<WebHook>) instance.Webhook).Search<WebHook.webHookID>((object) current.WebhookID, Array.Empty<object>()));
      localWebhook = ((PXSelectBase<WebHook>) instance.Webhook).Current;
      if (localWebhook == null)
        localWebhook = this.CreateLocalWebhook(instance);
      else if (!localWebhook.IsActive.GetValueOrDefault())
        localWebhook = this.ActivateLocalWebhook(instance);
    }
    return localWebhook;
  }

  protected virtual PX.Objects.CC.PaymentProcessing.PayLinkProcessing GetPayLinkProcessing()
  {
    return new PX.Objects.CC.PaymentProcessing.PayLinkProcessing((ICCPaymentProcessingRepository) new CCPaymentProcessingRepository((PXGraph) this.Base));
  }

  protected virtual CCProcessingCenterBranch GetDefaultProcCenterForBranch(
    CCProcessingCenterBranch row)
  {
    return PXResultset<CCProcessingCenterBranch>.op_Implicit(PXSelectBase<CCProcessingCenterBranch, PXSelectJoin<CCProcessingCenterBranch, InnerJoin<CCProcessingCenter, On<CCProcessingCenterBranch.processingCenterID, Equal<CCProcessingCenter.processingCenterID>>, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CCProcessingCenter.cashAccountID>>>>, Where<CashAccount.curyID, Equal<Required<CashAccount.curyID>>, And<CCProcessingCenter.allowPayLink, Equal<True>, And<CCProcessingCenter.processingCenterID, NotEqual<Required<CCProcessingCenter.processingCenterID>>, And<CCProcessingCenterBranch.branchID, Equal<Required<CCProcessingCenterBranch.branchID>>, And<CCProcessingCenterBranch.defaultForBranch, Equal<True>>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) ((PXSelectBase<CashAccount>) this.Base.CashAccount).SelectSingle(Array.Empty<object>()).CuryID,
      (object) row.ProcessingCenterID,
      (object) row.BranchID
    }));
  }

  protected virtual PX.Objects.GL.Branch GetBranchById(int? branchId)
  {
    return PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, branchId);
  }

  protected virtual CashAccount GetCashAccountById(int? cashAccountId)
  {
    return CashAccount.PK.Find((PXGraph) this.Base, cashAccountId);
  }

  private void ShowMappingNotDefinedWarnIfNeeded(PXCache cache, CCProcessingCenter row)
  {
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (row.AllowPayLink.GetValueOrDefault() && ((PXSelectBase<CCProcessingCenterBranch>) this.ProcCenterBranch).SelectSingle(Array.Empty<object>()) == null)
      propertyException = (PXSetPropertyException) new PXSetPropertyException<CCProcessingCenter.allowPayLink>("No settings for the payment link and for payment creation have been defined.", (PXErrorLevel) 2);
    cache.RaiseExceptionHandling<CCProcessingCenter.allowPayLink>((object) row, (object) row.AllowPayLink, (Exception) propertyException);
  }

  private WebHook CreateLocalWebhook(WebhookMaint webhookGraph)
  {
    CCProcessingCenter current = ((PXSelectBase<CCProcessingCenter>) this.Base.ProcessingCenter).Current;
    WebHook localWebhook = ((PXSelectBase<WebHook>) webhookGraph.Webhook).Insert(new WebHook()
    {
      Name = "PaymentProcessing - " + current.ProcessingCenterID,
      Handler = "PX.DataSync.PaymentProcessing.Webhooks.WebhookHandler",
      IsActive = new bool?(true),
      IsSystem = new bool?(true),
      RequestRetainCount = new short?((short) 100),
      RequestLogLevel = new byte?((byte) 1)
    });
    ((PXAction) ((PXGraph<WebhookMaint, WebHook>) webhookGraph).Save).Press();
    current.WebhookID = localWebhook.WebHookID;
    ((PXSelectBase<CCProcessingCenter>) this.Base.ProcessingCenter).UpdateCurrent();
    ((PXAction) this.Base.Save).Press();
    return localWebhook;
  }

  private bool IsFeatureSupported(CCProcessingCenter procCenter)
  {
    return CCProcessingFeatureHelper.IsFeatureSupported(procCenter, CCProcessingFeature.PayLink) && CCProcessingFeatureHelper.IsFeatureSupported(procCenter, CCProcessingFeature.WebhookManagement);
  }

  private WebHook ActivateLocalWebhook(WebhookMaint webhookGraph)
  {
    ((PXSelectBase<WebHook>) webhookGraph.Webhook).Current.IsActive = new bool?(true);
    WebHook webHook = ((PXSelectBase<WebHook>) webhookGraph.Webhook).UpdateCurrent();
    ((PXAction) ((PXGraph<WebhookMaint, WebHook>) webhookGraph).Save).Press();
    return webHook;
  }

  private void ClearValueFromDescription(IDictionary values, string key)
  {
    if (!values.Contains((object) key))
      return;
    string str = (string) values[(object) key];
    if (str == null)
      return;
    string[] strArray = str.Trim().Split(' ');
    if (strArray.Length == 0)
      return;
    values[(object) key] = (object) strArray[0].Trim();
  }
}
