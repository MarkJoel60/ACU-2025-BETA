// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.ACHPlugInBase;
using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Description;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Metadata;
using PX.Objects.AP;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.CA.Descriptor;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class PaymentMethodMaint : PXGraph<PaymentMethodMaint, PX.Objects.CA.PaymentMethod>
{
  public PXSelect<PX.Objects.CA.PaymentMethod> PaymentMethod;
  public PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>>> PaymentMethodCurrent;
  [PXCopyPasteHiddenFields(new Type[] {typeof (PaymentMethodDetail.paymentMethodID)})]
  public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>>> Details;
  [PXCopyPasteHiddenFields(new Type[] {typeof (PaymentMethodDetail.paymentMethodID)})]
  public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>, OrderBy<Asc<PaymentMethodDetail.orderIndex>>> DetailsForReceivable;
  [PXCopyPasteHiddenFields(new Type[] {typeof (PaymentMethodDetail.paymentMethodID)})]
  public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>, OrderBy<Asc<PaymentMethodDetail.orderIndex>>> DetailsForCashAccount;
  [PXCopyPasteHiddenFields(new Type[] {typeof (PaymentMethodDetail.paymentMethodID)})]
  public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>, OrderBy<Asc<PaymentMethodDetail.orderIndex>>> DetailsForVendor;
  public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Required<PaymentMethodDetail.detailID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>> RemmittanceSettings;
  public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Required<PaymentMethodDetail.detailID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>, OrderBy<Asc<PaymentMethodDetail.orderIndex>>> VendorInstructions;
  public PXSelect<VendorPaymentMethodDetail> dummy_VendorPaymentMethodDetail;
  public PXSelectJoin<PaymentMethodAccount, InnerJoin<CashAccount, On<PaymentMethodAccount.cashAccountID, Equal<CashAccount.cashAccountID>>>, Where<PaymentMethodAccount.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>>> CashAccounts;
  public PXSelect<CCProcessingCenterPmntMethod, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>>> ProcessingCenters;
  public PXSelect<CCProcessingCenterPmntMethodBranch, Where<CCProcessingCenterPmntMethodBranch.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>>> BranchProcessingCenters;
  public PXSelect<CCProcessingCenterPmntMethod, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isDefault, Equal<True>>>> DefaultProcCenter;
  public PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>>> PaymentMethodCurrentForPlugIn;
  public PXSelect<PX.Objects.CA.PlugInFilter, Where<PX.Objects.CA.PlugInFilter.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<PX.Objects.CA.PlugInFilter.plugInTypeName, Equal<Current<PX.Objects.CA.PaymentMethod.aPBatchExportPlugInTypeName>>>>> plugInFilter;
  [PXCopyPasteHiddenView]
  public PXSelect<ACHPlugInParameter, Where<ACHPlugInParameter.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<ACHPlugInParameter.plugInTypeName, Equal<Current<PX.Objects.CA.PaymentMethod.aPBatchExportPlugInTypeName>>>>, OrderBy<Asc<ACHPlugInParameter.order>>> aCHPlugInParameters;
  public PXSelect<ACHPlugInParameter2, Where<ACHPlugInParameter2.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<ACHPlugInParameter2.plugInTypeName, Equal<Current<PX.Objects.CA.PaymentMethod.aPBatchExportPlugInTypeName>>>>> PlugInParameters;
  public PXSelect<ACHPlugInParameter, Where<ACHPlugInParameter.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<ACHPlugInParameter.plugInTypeName, Equal<Current<PX.Objects.CA.PaymentMethod.aPBatchExportPlugInTypeName>>, And<ACHPlugInParameter.value, Equal<Required<PaymentMethodDetail.detailID>>, And<ACHPlugInParameter.type, Equal<Required<ACHPlugInParameter.type>>>>>>> aCHPlugInParametersByID;
  public PXSelect<ACHPlugInParameter, Where<ACHPlugInParameter.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<ACHPlugInParameter.plugInTypeName, Equal<Current<PX.Objects.CA.PaymentMethod.aPBatchExportPlugInTypeName>>, And<ACHPlugInParameter.parameterID, Equal<Required<ACHPlugInParameter.parameterID>>>>>> aCHPlugInParametersByParameter;
  public PXSelect<VendorPaymentMethodDetail, Where<VendorPaymentMethodDetail.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>>> vendorPaymentMethodDetail;
  public PXSelectJoin<VendorPaymentMethodDetail, RightJoin<VendorPaymentMethodDetailAlias, On<VendorPaymentMethodDetail.paymentMethodID, Equal<VendorPaymentMethodDetailAlias.paymentMethodID>, And<VendorPaymentMethodDetail.bAccountID, Equal<VendorPaymentMethodDetailAlias.bAccountID>, And<VendorPaymentMethodDetail.locationID, Equal<VendorPaymentMethodDetailAlias.locationID>>>>>, Where<VendorPaymentMethodDetailAlias.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<VendorPaymentMethodDetail.detailID, Equal<Required<PaymentMethodDetail.detailID>>, And<VendorPaymentMethodDetailAlias.detailID, Equal<Required<PaymentMethodDetail.detailID>>>>>> vendorPaymentMethodDetailByID;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  private bool errorKey;

  [InjectDependency]
  public DirectDepositTypeService DirectDepositService { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [CCProcessingCenterPaymentMethodFilter(typeof (Search<CCProcessingCenter.processingCenterID, Where<CCProcessingCenter.isActive, Equal<True>>>))]
  [PXParent(typeof (Select<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<CCProcessingCenterPmntMethod.processingCenterID>>>>))]
  [PXUIField(DisplayName = "Proc. Center ID")]
  [DeprecatedProcessing(ChckVal = DeprecatedProcessingAttribute.CheckVal.ProcessingCenterId)]
  [DisabledProcCenter(CheckFieldValue = DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CCProcessingCenterPmntMethod.processingCenterID> e)
  {
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.CA.PaymentMethod.paymentMethodID))]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID>))]
  [PXUIField(DisplayName = "Payment Method")]
  [PXParent(typeof (Select<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<CCProcessingCenterPmntMethod.paymentMethodID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CCProcessingCenterPmntMethod.paymentMethodID> e)
  {
  }

  [PXMergeAttributes]
  [CashAccount(true, null, null)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PaymentMethodAccount.cashAccountID> e)
  {
  }

  public string[] GetAddendaInfoFields()
  {
    string str1 = "AP305000";
    string addendaMember = "AddendaInfo";
    if (string.IsNullOrEmpty(str1))
      return (string[]) null;
    PXSiteMap.ScreenInfo info = IScreenInfoProviderExtensions.TryGet(ScreenUtils.ScreenInfo, str1);
    return info == null ? (string[]) null : info.Containers.Where<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (m => m.Key == addendaMember)).Select(c => new
    {
      container = c,
      viewName = c.Key.Split(new string[1]{ ": " }, StringSplitOptions.None)[0]
    }).SelectMany(t => (IEnumerable<FieldInfo>) info.Containers[t.container.Key].Fields, (t, field) =>
    {
      string empty = string.Empty;
      string fieldName = field.FieldName;
      string key;
      if (field.FieldName.Contains("__"))
      {
        string[] strArray = field.FieldName.Replace("__", "_").Split('_');
        key = strArray[0];
        fieldName = strArray[1];
      }
      else
        key = t.viewName.Replace(addendaMember, "APPayment");
      if (fieldName == "NoteText")
        return string.Empty;
      string str2;
      if (AddendaAliases.Direct.TryGetValue(key, out str2))
        key = str2;
      return $"[{key}.{fieldName}]";
    }).Distinct<string>().ToArray<string>();
  }

  protected virtual IEnumerable PlugInFilter()
  {
    PX.Objects.CA.PlugInFilter current = ((PXSelectBase<PX.Objects.CA.PlugInFilter>) this.plugInFilter).Current;
    if (current == null)
    {
      IEnumerator enumerator = GraphHelper.QuickSelect(((PXSelectBase) this.plugInFilter).View).GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
          current = (PX.Objects.CA.PlugInFilter) enumerator.Current;
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
    }
    if (current == null)
      return (IEnumerable) new List<PX.Objects.CA.PlugInFilter>()
      {
        ((PXSelectBase<PX.Objects.CA.PlugInFilter>) this.plugInFilter).Current
      };
    bool result;
    bool.TryParse(((PXSelectBase<ACHPlugInParameter>) this.aCHPlugInParametersByParameter).SelectSingle(new object[1]
    {
      (object) "IncludeOffsetRecord".ToUpper()
    })?.Value, out result);
    current.ShowOffsetSettings = new bool?(result);
    return (IEnumerable) new List<PX.Objects.CA.PlugInFilter>()
    {
      current
    };
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.CA.PlugInFilter.showAllSettings> e)
  {
    if (e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CA.PlugInFilter.showAllSettings>, object, object>) e).OldValue)
      return;
    ((PXSelectBase) this.aCHPlugInParameters).View.RequestRefresh();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.CA.PlugInFilter.showOffsetSettings> e)
  {
    if (e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CA.PlugInFilter.showOffsetSettings>, object, object>) e).OldValue)
      return;
    ((PXSelectBase) this.aCHPlugInParameters).View.RequestRefresh();
  }

  protected virtual IEnumerable ACHPlugInParameters() => this.GetACHPlugInParameters();

  private IEnumerable SelectParameters()
  {
    return GraphHelper.QuickSelect(((PXSelectBase) this.aCHPlugInParameters).View);
  }

  protected virtual IEnumerable GetACHPlugInParameters()
  {
    PaymentMethodMaint graph = this;
    IEnumerable<IACHPlugInParameter> ofSelectedPlugIn = graph.GetParametersOfSelectedPlugIn();
    List<ACHPlugInParameter> aCHPlugInParameters = new List<ACHPlugInParameter>();
    foreach (ACHPlugInParameter selectParameter in graph.SelectParameters())
    {
      foreach (IACHPlugInParameter iachPlugInParameter in ofSelectedPlugIn)
      {
        PX.Objects.CA.PlugInFilter current = ((PXSelectBase<PX.Objects.CA.PlugInFilter>) graph.plugInFilter).Current;
        if (((current != null ? (!current.ShowAllSettings.GetValueOrDefault() ? 1 : 0) : 1) == 0 || selectParameter.IsAvailableInShortForm.GetValueOrDefault()) && selectParameter.ParameterID.ToUpper() == iachPlugInParameter.ParameterID.ToUpper())
          aCHPlugInParameters.Add(selectParameter);
      }
    }
    foreach (ACHPlugInParameter applyFilter in graph.ApplyFilters((IEnumerable<ACHPlugInParameter>) aCHPlugInParameters))
    {
      if (applyFilter.Visible.GetValueOrDefault())
        yield return (object) applyFilter;
    }
  }

  protected virtual IEnumerable<ACHPlugInParameter> ApplyFilters(
    IEnumerable<ACHPlugInParameter> aCHPlugInParameters)
  {
    return aCHPlugInParameters;
  }

  public PaymentMethodMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
  }

  public virtual int Persist(Type cacheType, PXDBOperation operation)
  {
    try
    {
      int num = cacheType == typeof (PaymentMethodAccount) ? 1 : 0;
      return ((PXGraph) this).Persist(cacheType, operation);
    }
    catch (PXDatabaseException ex)
    {
      if (cacheType == typeof (PaymentMethodAccount) && (operation == 3 || operation == 3) && (ex.ErrorCode == null || ex.ErrorCode == 1))
      {
        string cashAccountCd = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          ex.Keys[1]
        })).CashAccountCD;
        throw new PXException("The combination of Payment Method, Cash Account '{0}, {1}' cannot be deleted because it is already used in payments.", new object[2]
        {
          ex.Keys[0],
          (object) cashAccountCd
        });
      }
      throw;
    }
  }

  protected virtual void PaymentMethod_PaymentType_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.CA.PaymentMethod row = (PX.Objects.CA.PaymentMethod) e.Row;
    if (!((IQueryable<PXResult<PaymentMethodDetail>>) ((PXSelectBase<PaymentMethodDetail>) this.Details).Select(Array.Empty<object>())).Any<PXResult<PaymentMethodDetail>>() || ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethod).Ask("Confirmation", "The details for the payment method will be reset. Continue?", (MessageButtons) 4) == 6)
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) row.PaymentType;
  }

  protected virtual void PaymentMethod_DirectDepositFileFormat_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.CA.PaymentMethod row = (PX.Objects.CA.PaymentMethod) e.Row;
    if (!((IQueryable<PXResult<PaymentMethodDetail>>) ((PXSelectBase<PaymentMethodDetail>) this.Details).Select(Array.Empty<object>())).Any<PXResult<PaymentMethodDetail>>() || ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethod).Ask("Confirmation", "The details for the payment method will be reset. Continue?", (MessageButtons) 4) == 6)
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) row.DirectDepositFileFormat;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.CA.PaymentMethod, PX.Objects.CA.PaymentMethod.externalPaymentProcessorID> e)
  {
    PX.Objects.CA.PaymentMethod row = e.Row;
    if (!(row.PaymentType == "EPP") || row.ExternalPaymentProcessorID == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.CA.PaymentMethod, PX.Objects.CA.PaymentMethod.externalPaymentProcessorID>, PX.Objects.CA.PaymentMethod, object>) e).NewValue != null)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.CA.PaymentMethod, PX.Objects.CA.PaymentMethod.externalPaymentProcessorID>>) e).Cache.RaiseExceptionHandling<PX.Objects.CA.PaymentMethod.externalPaymentProcessorID>((object) row, (object) null, (Exception) new PXSetPropertyException((IBqlTable) row, "The External Payment Processor box cannot be empty.", (PXErrorLevel) 4));
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.CA.PaymentMethod, PX.Objects.CA.PaymentMethod.externalPaymentProcessorID>>) e).Cancel = true;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.CA.PaymentMethod, PX.Objects.CA.PaymentMethod.externalPaymentProcessorID>, PX.Objects.CA.PaymentMethod, object>) e).NewValue = (object) row.ExternalPaymentProcessorID;
  }

  protected virtual void PaymentMethod_PaymentType_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    foreach (PXResult<CCProcessingCenterPmntMethod> pxResult in ((PXSelectBase<CCProcessingCenterPmntMethod>) this.ProcessingCenters).Select(Array.Empty<object>()))
      ((PXSelectBase<CCProcessingCenterPmntMethod>) this.ProcessingCenters).Delete(PXResult<CCProcessingCenterPmntMethod>.op_Implicit(pxResult));
    PX.Objects.CA.PaymentMethod row = (PX.Objects.CA.PaymentMethod) e.Row;
    cache.SetDefaultExt<PX.Objects.CA.PaymentMethod.aRHasBillingInfo>((object) row);
    cache.SetDefaultExt<PX.Objects.CA.PaymentMethod.useForAP>((object) row);
    cache.SetDefaultExt<PX.Objects.CA.PaymentMethod.aRVoidOnDepositAccount>((object) row);
    if (!(row.PaymentType != "EPP"))
      return;
    ((PXSelectBase) this.PaymentMethod).Cache.SetValueExt<PX.Objects.CA.PaymentMethod.externalPaymentProcessorID>(e.Row, (object) null);
  }

  protected virtual void PaymentMethod_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PX.Objects.CA.PaymentMethod row = (PX.Objects.CA.PaymentMethod) e.Row;
    bool flag1 = row.PaymentType == "CCD";
    bool flag2 = row.PaymentType == "EFT";
    bool flag3 = row.PaymentType == "POS";
    bool flag4 = row.PaymentType == "EPP";
    bool? nullable = row.APPrintChecks;
    bool valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.APCreateBatchPayment;
    bool valueOrDefault2 = nullable.GetValueOrDefault();
    bool flag5 = row.APBatchExportMethod == "P" && !string.IsNullOrEmpty(row.APBatchExportPlugInTypeName);
    nullable = row.ARIsProcessingRequired;
    bool valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = row.UseForAP;
    bool flag6 = nullable ?? false;
    nullable = row.UseForAR;
    bool flag7 = nullable ?? false;
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.aPCheckReportID>(cache, (object) row, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.aPStubLines>(cache, (object) row, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.aPPrintRemittance>(cache, (object) row, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.aPRemittanceReportID>(cache, (object) row, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aPPrintRemittance>(cache, (object) row, valueOrDefault1);
    PXCache pxCache1 = cache;
    PX.Objects.CA.PaymentMethod paymentMethod = row;
    int num1;
    if (valueOrDefault1)
    {
      nullable = row.APPrintRemittance;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aPRemittanceReportID>(pxCache1, (object) paymentMethod, num1 != 0);
    PXCache pxCache2 = cache;
    int num2;
    if (valueOrDefault1)
    {
      nullable = row.APPrintRemittance;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetRequired<PX.Objects.CA.PaymentMethod.aPRemittanceReportID>(pxCache2, num2 != 0);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aPCheckReportID>(cache, (object) row, valueOrDefault1);
    PXUIFieldAttribute.SetRequired<PX.Objects.CA.PaymentMethod.aPCheckReportID>(cache, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aPPrintChecks>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aPStubLines>(cache, (object) row, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aPCreateBatchPayment>(cache, (object) row, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.aPBatchExportSYMappingID>(cache, (object) row, valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aPBatchExportSYMappingID>(cache, (object) row, valueOrDefault2);
    PXUIFieldAttribute.SetRequired<PX.Objects.CA.PaymentMethod.aPBatchExportSYMappingID>(cache, valueOrDefault2);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.skipPaymentsWithZeroAmt>(cache, (object) row, valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.skipPaymentsWithZeroAmt>(cache, (object) row, valueOrDefault2);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.aPRequirePaymentRef>(cache, (object) row, !valueOrDefault1 && !valueOrDefault2 && !flag4);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.aPAdditionalProcessing>(cache, (object) row, !flag4);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.useForAR>(cache, (object) row, !flag4);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.useForCA>(cache, (object) row, !flag4);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.useForAP>(cache, (object) row, !flag3 && !flag2);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.externalPaymentProcessorID>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aRVoidOnDepositAccount>(cache, (object) row, !flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired>(cache, (object) row, !flag3 && !flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aRHasBillingInfo>(cache, (object) row, !flag3);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.aRHasBillingInfo>(cache, (object) row, !flag3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.isAccountNumberRequired>(cache, (object) row, !flag3);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.isAccountNumberRequired>(cache, (object) row, !flag3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.paymentDateToBankDate>(cache, (object) row, !valueOrDefault3 && !flag4);
    PXUIFieldAttribute.SetVisible<PaymentMethodDetail.isExpirationDate>(((PXSelectBase) this.Details).Cache, (object) null, flag1 | flag3 | flag2);
    PXUIFieldAttribute.SetVisible<PaymentMethodDetail.isIdentifier>(((PXSelectBase) this.Details).Cache, (object) null, flag1 | flag3 | flag2);
    PXUIFieldAttribute.SetVisible<PaymentMethodDetail.isOwnerName>(((PXSelectBase) this.Details).Cache, (object) null, flag1 | flag3 | flag2);
    PXUIFieldAttribute.SetVisible<PaymentMethodDetail.displayMask>(((PXSelectBase) this.Details).Cache, (object) null, flag1 | flag3 | flag2);
    PXUIFieldAttribute.SetVisible<PaymentMethodDetail.isCCProcessingID>(((PXSelectBase) this.Details).Cache, (object) null, flag1 | flag2);
    PXUIFieldAttribute.SetVisible<PaymentMethodDetail.isCVV>(((PXSelectBase) this.Details).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PaymentMethodDetail.isExpirationDate>(((PXSelectBase) this.Details).Cache, (object) null, flag1 | flag3 | flag2);
    PXUIFieldAttribute.SetEnabled<PaymentMethodDetail.isIdentifier>(((PXSelectBase) this.Details).Cache, (object) null, flag1 | flag3 | flag2);
    PXUIFieldAttribute.SetEnabled<PaymentMethodDetail.isIdentifier>(((PXSelectBase) this.Details).Cache, (object) null, flag1 | flag3 | flag2);
    PXUIFieldAttribute.SetEnabled<PaymentMethodDetail.displayMask>(((PXSelectBase) this.Details).Cache, (object) null, flag1 | flag3 | flag2);
    PXUIFieldAttribute.SetEnabled<PaymentMethodDetail.isCCProcessingID>(((PXSelectBase) this.Details).Cache, (object) null, flag1 | flag3 | flag2);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.aRDepositAsBatch>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.aRDepositAsBatch>(cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.useForAP>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag6);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.aPIsDefault>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag6);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.aPAutoNextNbr>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag6 && !flag4);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.aPLastRefNbr>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag6 && !flag4);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.aPBatchLastRefNbr>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag6 && !flag4);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPQuickBatchGeneration>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag6 & valueOrDefault2);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.aPQuickBatchGeneration>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag6 & valueOrDefault2);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.useForAR>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag7);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.aRIsDefault>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag7);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.aRAutoNextNbr>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag7);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.aRLastRefNbr>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag7);
    PXUIFieldAttribute.SetVisible<PaymentMethodAccount.aRIsDefaultForRefund>(((PXSelectBase) this.CashAccounts).Cache, (object) null, flag7);
    PXUIFieldAttribute.SetVisible<CCProcessingCenterPmntMethod.fundHoldPeriod>(((PXSelectBase) this.ProcessingCenters).Cache, (object) null, flag1 | flag3);
    PXUIFieldAttribute.SetVisible<CCProcessingCenterPmntMethod.reauthDelay>(((PXSelectBase) this.ProcessingCenters).Cache, (object) null, flag1 | flag3);
    PXUIFieldAttribute.SetEnabled<PaymentMethodDetail.controlType>(((PXSelectBase) this.Details).Cache, (object) null, flag5);
    PXUIFieldAttribute.SetEnabled<PaymentMethodDetail.defaultValue>(((PXSelectBase) this.Details).Cache, (object) null, flag5);
    PXUIFieldAttribute.SetVisible<PaymentMethodDetail.controlType>(((PXSelectBase) this.Details).Cache, (object) null, flag5);
    PXUIFieldAttribute.SetVisible<PaymentMethodDetail.defaultValue>(((PXSelectBase) this.Details).Cache, (object) null, flag5);
    ((PXSelectBase) this.aCHPlugInParameters).AllowSelect = row.APBatchExportMethod == "P";
    ((PXSelectBase) this.PlugInParameters).AllowSelect = ((PXGraph) this).IsCopyPasteContext;
    ((PXSelectBase) this.PlugInParameters).AllowInsert = ((PXGraph) this).IsCopyPasteContext;
    ((PXSelectBase) this.PlugInParameters).AllowUpdate = ((PXGraph) this).IsCopyPasteContext;
    ((PXSelectBase) this.PlugInParameters).AllowDelete = ((PXGraph) this).IsCopyPasteContext;
    bool flag8 = false;
    if (row.PaymentType == "DDT" && this.DirectDepositService.GetDirectDepositTypes().Count<DirectDepositType>() > 0)
      flag8 = true;
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.directDepositFileFormat>(cache, (object) row, flag8);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.directDepositFileFormat>(cache, (object) row, flag8);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.PaymentMethod.requireBatchSeqNum>(cache, (object) row, valueOrDefault2 & flag8);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.PaymentMethod.requireBatchSeqNum>(cache, (object) row, false);
    ((PXSelectBase) this.DetailsForReceivable).AllowSelect = !flag3;
    nullable = row.ARIsProcessingRequired;
    bool valueOrDefault4 = nullable.GetValueOrDefault();
    ((PXSelectBase) this.ProcessingCenters).Cache.AllowDelete = valueOrDefault4;
    ((PXSelectBase) this.ProcessingCenters).Cache.AllowUpdate = valueOrDefault4;
    ((PXSelectBase) this.ProcessingCenters).Cache.AllowInsert = valueOrDefault4;
    PXResultset<CCProcessingCenterPmntMethod> pxResultset = ((PXSelectBase<CCProcessingCenterPmntMethod>) this.DefaultProcCenter).Select(Array.Empty<object>());
    int num3;
    if (CCProcessingHelper.PaymentMethodSupportsIntegratedProcessing(row))
    {
      nullable = row.UseForAR;
      if (nullable.GetValueOrDefault())
      {
        nullable = row.IsAccountNumberRequired;
        bool flag9 = false;
        if (nullable.GetValueOrDefault() == flag9 & nullable.HasValue)
        {
          num3 = row.PaymentType != "POS" ? 1 : 0;
          goto label_15;
        }
      }
    }
    num3 = 0;
label_15:
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (num3 != 0)
      propertyException = new PXSetPropertyException("The Require Card/Account Number check box must be selected if the means of payment is Credit Card and the Integrated Processing check box is selected.");
    ((PXSelectBase) this.PaymentMethodCurrent).Cache.RaiseExceptionHandling<PX.Objects.CA.PaymentMethod.isAccountNumberRequired>((object) row, (object) row.IsAccountNumberRequired, (Exception) propertyException);
    ((PXSelectBase) this.PaymentMethodCurrent).Cache.RaiseExceptionHandling<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired>((object) row, (object) row.ARIsProcessingRequired, (Exception) propertyException);
    if (PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>())
    {
      nullable = row.ARIsProcessingRequired;
      if (nullable.GetValueOrDefault() && pxResultset.Count == 0)
      {
        ((PXSelectBase) this.PaymentMethod).Cache.RaiseExceptionHandling<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired>((object) row, (object) row.ARIsProcessingRequired, (Exception) new PXSetPropertyException("No processing center was set as default", (PXErrorLevel) 2));
        goto label_22;
      }
    }
    PXFieldState stateExt = (PXFieldState) cache.GetStateExt<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired>((object) row);
    if (stateExt.IsWarning && string.Equals(stateExt.Error, "No processing center was set as default"))
      ((PXSelectBase) this.PaymentMethod).Cache.RaiseExceptionHandling<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired>((object) row, (object) null, (Exception) null);
label_22:
    foreach (PXResult<CCProcessingCenterPmntMethod> pxResult in ((PXSelectBase<CCProcessingCenterPmntMethod>) this.ProcessingCenters).Select(Array.Empty<object>()))
      ((PXSelectBase) this.ProcessingCenters).Cache.RaiseRowSelected((object) PXResult<CCProcessingCenterPmntMethod>.op_Implicit(pxResult));
  }

  protected virtual void PaymentMethod_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    PX.Objects.CA.PaymentMethod row = (PX.Objects.CA.PaymentMethod) e.Row;
    PX.Objects.CA.PaymentMethod oldRow = (PX.Objects.CA.PaymentMethod) e.OldRow;
    if (oldRow.PaymentType != row.PaymentType)
    {
      foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) this.Details).Select(Array.Empty<object>()))
        ((PXSelectBase) this.Details).Cache.Delete((object) PXResult<PaymentMethodDetail>.op_Implicit(pxResult));
      if (row.PaymentType == "CCD" || row.PaymentType == "EFT")
        this.fillCreditCardDefaults();
      row.ARIsOnePerCustomer = new bool?(row.PaymentType == "CHC" || row.PaymentType == "POS");
      row.ARIsProcessingRequired = new bool?(PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() && (row.PaymentType == "CCD" || row.PaymentType == "POS" || row.PaymentType == "EFT"));
    }
    bool? useForAr = oldRow.UseForAR;
    bool? nullable1 = row.UseForAR;
    if (!(useForAr.GetValueOrDefault() == nullable1.GetValueOrDefault() & useForAr.HasValue == nullable1.HasValue))
    {
      nullable1 = row.UseForAR;
      if (!(nullable1 ?? false))
      {
        row.ARIsProcessingRequired = new bool?(false);
        foreach (PXResult<PaymentMethodAccount> pxResult in ((PXSelectBase<PaymentMethodAccount>) this.CashAccounts).Select(Array.Empty<object>()))
        {
          PaymentMethodAccount paymentMethodAccount1 = PXResult<PaymentMethodAccount>.op_Implicit(pxResult);
          PaymentMethodAccount paymentMethodAccount2 = paymentMethodAccount1;
          paymentMethodAccount1.ARIsDefault = nullable1 = paymentMethodAccount1.ARIsDefaultForRefund = new bool?(false);
          bool? nullable2 = nullable1;
          paymentMethodAccount2.UseForAR = nullable2;
          ((PXSelectBase<PaymentMethodAccount>) this.CashAccounts).Update(paymentMethodAccount1);
        }
      }
    }
    nullable1 = oldRow.UseForAR;
    bool? nullable3 = row.UseForAR;
    if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
    {
      nullable3 = row.UseForAR;
      if ((nullable3 ?? true) && (row.PaymentType == "POS" || row.PaymentType == "EFT"))
        row.ARIsProcessingRequired = new bool?(true);
    }
    nullable3 = oldRow.UseForAP;
    nullable1 = row.UseForAP;
    if (!(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue))
    {
      nullable1 = row.UseForAP;
      if (!(nullable1 ?? false))
      {
        foreach (PXResult<PaymentMethodAccount> pxResult in ((PXSelectBase<PaymentMethodAccount>) this.CashAccounts).Select(Array.Empty<object>()))
        {
          PaymentMethodAccount paymentMethodAccount3 = PXResult<PaymentMethodAccount>.op_Implicit(pxResult);
          PaymentMethodAccount paymentMethodAccount4 = paymentMethodAccount3;
          paymentMethodAccount3.APIsDefault = nullable1 = new bool?(false);
          bool? nullable4 = nullable1;
          paymentMethodAccount4.UseForAP = nullable4;
          ((PXSelectBase<PaymentMethodAccount>) this.CashAccounts).Update(paymentMethodAccount3);
        }
      }
    }
    nullable1 = oldRow.APCreateBatchPayment;
    nullable3 = row.APCreateBatchPayment;
    if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
    {
      foreach (PXResult<PaymentMethodAccount> pxResult in ((PXSelectBase<PaymentMethodAccount>) this.CashAccounts).Select(Array.Empty<object>()))
      {
        PaymentMethodAccount paymentMethodAccount = PXResult<PaymentMethodAccount>.op_Implicit(pxResult);
        nullable3 = paymentMethodAccount.APQuickBatchGeneration;
        if (nullable3.GetValueOrDefault())
        {
          paymentMethodAccount.APQuickBatchGeneration = new bool?(false);
          ((PXSelectBase<PaymentMethodAccount>) this.CashAccounts).Update(paymentMethodAccount);
        }
      }
    }
    this.UpdatePlugInSettings(row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.CA.PaymentMethod, PX.Objects.CA.PaymentMethod.aPBatchExportPlugInTypeName> e)
  {
    this.UpdatePlugInSettings(e.Row);
  }

  protected virtual void UpdatePlugInSettings(PX.Objects.CA.PaymentMethod pm)
  {
  }

  protected virtual void ResetPlugInSettings(PX.Objects.CA.PaymentMethod pm)
  {
    pm.APBatchExportPlugInTypeName = (string) null;
    this.UpdatePlugInSettings(pm);
  }

  protected virtual void PaymentMethod_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    PX.Objects.CA.PaymentMethod row = (PX.Objects.CA.PaymentMethod) e.Row;
    foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) this.Details).Select(Array.Empty<object>()))
      ((PXSelectBase) this.Details).Cache.Delete((object) PXResult<PaymentMethodDetail>.op_Implicit(pxResult));
    if (row.PaymentType == "CCD" || row.PaymentType == "EFT")
    {
      this.fillCreditCardDefaults();
      row.ARIsOnePerCustomer = new bool?(false);
    }
    row.ARIsOnePerCustomer = new bool?(row.PaymentType == "CHC");
  }

  protected virtual void PaymentMethod_aRVoidOnDepositAccount_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    this.DefaultPMSettings(e);
  }

  protected virtual void PaymentMethod_UseForAP_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    this.DefaultPMSettings(e);
  }

  private void DefaultPMSettings(PXFieldDefaultingEventArgs e)
  {
    PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethod).Current;
    if (current == null || !(current.PaymentType == "POS") && !(current.PaymentType == "EFT"))
      return;
    e.NewValue = (object) false;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.CA.PaymentMethod> e)
  {
    if (e.Operation != 3)
    {
      PX.Objects.CA.PaymentMethod row = e.Row;
      this.VerifyAPRequirePaymentRefAndAPAdditionalProcessing(e.Row.APRequirePaymentRef, e.Row.APAdditionalProcessing);
      this.CheckPaymentMethodDetailsIfProcessingReq(row);
      foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) this.Details).Select(Array.Empty<object>()))
        this.CheckPaymentMethodDetailConsistency(PXResult<PaymentMethodDetail>.op_Implicit(pxResult));
      if (row.PaymentType == "EPP" && row.ExternalPaymentProcessorID == null)
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.CA.PaymentMethod>>) e).Cache.RaiseExceptionHandling<PX.Objects.CA.PaymentMethod.externalPaymentProcessorID>((object) row, (object) null, (Exception) new PXSetPropertyException((IBqlTable) row, "The External Payment Processor box cannot be empty.", (PXErrorLevel) 4));
    }
    if (e.Operation != 3)
      return;
    foreach (PXResult<PaymentMethodAccount, CashAccount> pxResult in ((PXSelectBase<PaymentMethodAccount>) this.CashAccounts).Select(Array.Empty<object>()))
      this.VerifyCashAccountLinkOrMethodCanBeDeleted(PXResult<PaymentMethodAccount, CashAccount>.op_Implicit(pxResult));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PaymentMethodDetail.isCCProcessingID> e)
  {
    PaymentMethodDetail row = (PaymentMethodDetail) e.Row;
    if (!row.IsCCProcessingID.GetValueOrDefault())
      return;
    row.IsCVV = new bool?(false);
    row.IsExpirationDate = new bool?(false);
    row.IsIdentifier = new bool?(false);
    row.IsOwnerName = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PaymentMethodDetail.isCVV> e)
  {
    PaymentMethodDetail row = (PaymentMethodDetail) e.Row;
    if (!row.IsCVV.GetValueOrDefault())
      return;
    row.IsCCProcessingID = new bool?(false);
    row.IsExpirationDate = new bool?(false);
    row.IsIdentifier = new bool?(false);
    row.IsOwnerName = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PaymentMethodDetail.isExpirationDate> e)
  {
    PaymentMethodDetail row = (PaymentMethodDetail) e.Row;
    if (!row.IsExpirationDate.GetValueOrDefault())
      return;
    row.IsCCProcessingID = new bool?(false);
    row.IsCVV = new bool?(false);
    row.IsIdentifier = new bool?(false);
    row.IsOwnerName = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PaymentMethodDetail.isIdentifier> e)
  {
    PaymentMethodDetail row = (PaymentMethodDetail) e.Row;
    if (!row.IsIdentifier.GetValueOrDefault())
      return;
    row.IsCCProcessingID = new bool?(false);
    row.IsCVV = new bool?(false);
    row.IsExpirationDate = new bool?(false);
    row.IsOwnerName = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PaymentMethodDetail.isOwnerName> e)
  {
    PaymentMethodDetail row = (PaymentMethodDetail) e.Row;
    if (!row.IsOwnerName.GetValueOrDefault())
      return;
    row.IsCCProcessingID = new bool?(false);
    row.IsCVV = new bool?(false);
    row.IsExpirationDate = new bool?(false);
    row.IsIdentifier = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CA.PaymentMethod.paymentType> e)
  {
    if (e.NewValue.ToString() != "DDT")
      ((PXSelectBase) this.PaymentMethod).Cache.SetValueExt<PX.Objects.CA.PaymentMethod.directDepositFileFormat>(e.Row, (object) null);
    if (!(e.NewValue.ToString() == "EPP"))
      return;
    ((PXSelectBase) this.PaymentMethod).Cache.SetValueExt<PX.Objects.CA.PaymentMethod.useForAR>(e.Row, (object) false);
    ((PXSelectBase) this.PaymentMethod).Cache.SetValueExt<PX.Objects.CA.PaymentMethod.useForCA>(e.Row, (object) false);
    ((PXSelectBase) this.PaymentMethod).Cache.SetValueExt<PX.Objects.CA.PaymentMethod.paymentDateToBankDate>(e.Row, (object) false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.CA.PaymentMethod.directDepositFileFormat> e)
  {
    PX.Objects.CA.PaymentMethod row = (PX.Objects.CA.PaymentMethod) e.Row;
    if (e.NewValue != null && !string.IsNullOrEmpty(row.DirectDepositFileFormat))
    {
      this.DirectDepositService?.SetPaymentMethodDefaults(((PXSelectBase) this.PaymentMethod).Cache);
      foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) this.Details).Select(Array.Empty<object>()))
        ((PXSelectBase) this.Details).Cache.Delete((object) PXResult<PaymentMethodDetail>.op_Implicit(pxResult));
      IEnumerable<PaymentMethodDetail> defaults = this.DirectDepositService?.GetDefaults(e.NewValue.ToString());
      if (defaults != null)
      {
        foreach (object obj in defaults)
          ((PXSelectBase) this.Details).Cache.Insert(obj);
      }
    }
    if (!(row.APBatchExportMethod == "P"))
      return;
    this.ResetPlugInSettings(row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PaymentMethodDetail> e)
  {
    PaymentMethodDetail row = e.Row;
    if (e.Operation == 3)
    {
      SelectorType selectorType = (SelectorType) 0;
      if (row.UseFor == "V")
        selectorType = (SelectorType) 1;
      if (row.UseFor == "C")
        selectorType = (SelectorType) 0;
      ACHPlugInParameter achPlugInParameter = ((PXSelectBase<ACHPlugInParameter>) this.aCHPlugInParametersByID).SelectSingle(new object[2]
      {
        (object) row?.DetailID,
        (object) (int) selectorType
      });
      if (!string.IsNullOrEmpty(achPlugInParameter?.ParameterID))
      {
        if (row.UseFor == "V")
          throw new PXException("The {0} payment method detail that is mapped with the {1} plug-in setting is missing on the Settings for Use in AP tab.", new object[2]
          {
            (object) row.Descr,
            (object) achPlugInParameter.ParameterCode
          });
        if (row.UseFor == "C")
          throw new PXException("The {0} remittance setting that is mapped with the {1} plug-in setting is missing on the Remittance Settings tab.", new object[2]
          {
            (object) row.Descr,
            (object) achPlugInParameter.ParameterCode
          });
      }
    }
    if ((e.Operation == 2 || e.Operation == 1) && row.ControlType.GetValueOrDefault() == 1 && !string.IsNullOrEmpty(row.DefaultValue))
    {
      PaymentMethodDetail firstDetail = ((PXSelectBase<PaymentMethodDetail>) this.DetailsForVendor).SelectSingle(Array.Empty<object>());
      if (!string.IsNullOrEmpty(firstDetail.DetailID) && !string.IsNullOrEmpty(((PXSelectBase<VendorPaymentMethodDetail>) this.vendorPaymentMethodDetail).SelectSingle(Array.Empty<object>())?.DetailID))
        this.UpdateVendorDetails(row, firstDetail);
    }
    if (e.Operation == 3 || row == null)
      return;
    PXDefaultAttribute.SetPersistingCheck<PaymentMethodDetail.displayMask>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PaymentMethodDetail>>) e).Cache, (object) row, row.IsIdentifier.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  private void UpdateVendorDetails(PaymentMethodDetail detail, PaymentMethodDetail firstDetail)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new PaymentMethodMaint.\u003C\u003Ec__DisplayClass61_0()
    {
      \u003C\u003E4__this = this,
      detail = detail,
      firstDetail = firstDetail
    }, __methodptr(\u003CUpdateVendorDetails\u003Eb__0)));
  }

  protected virtual void PaymentMethod_ARHasBillingInfo_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    PX.Objects.CA.PaymentMethod row = (PX.Objects.CA.PaymentMethod) e.Row;
    if (!(row.PaymentType == "CCD") && !(row.PaymentType == "EFT"))
      return;
    e.NewValue = (object) true;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.CA.PaymentMethod.aPAdditionalProcessing> e)
  {
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.CA.PaymentMethod.aPAdditionalProcessing>, object, object>) e).NewValue;
    if (newValue == null)
      return;
    this.VerifyAPRequirePaymentRefAndAPAdditionalProcessing(new bool?(), newValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.CA.PaymentMethod.aPRequirePaymentRef> e)
  {
    bool? newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.CA.PaymentMethod.aPRequirePaymentRef>, object, object>) e).NewValue;
    if (!newValue.HasValue)
      return;
    this.VerifyAPRequirePaymentRefAndAPAdditionalProcessing(newValue, (string) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.CA.PaymentMethod, PX.Objects.CA.PaymentMethod.aRIsProcessingRequired> e)
  {
    PX.Objects.CA.PaymentMethod row = e.Row;
    if (!row.ARIsProcessingRequired.HasValue || !row.ARIsProcessingRequired.Value || !row.PaymentDateToBankDate.HasValue || !row.PaymentDateToBankDate.Value)
      return;
    row.PaymentDateToBankDate = new bool?(false);
    ((PXSelectBase) this.PaymentMethod).Cache.RaiseExceptionHandling<PX.Objects.CA.PaymentMethod.paymentDateToBankDate>((object) row, (object) row.PaymentDateToBankDate, (Exception) new PXSetPropertyException("The Set Payment Date to Bank Transaction Date check box cannot be selected for payment methods with the Integrated Processing check box selected.", (PXErrorLevel) 2));
  }

  protected virtual void PaymentMethod_APAdditionalProcessing_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CA.PaymentMethod row = (PX.Objects.CA.PaymentMethod) e.Row;
    switch (row.APAdditionalProcessing)
    {
      case "P":
        row.APPrintChecks = new bool?(true);
        row.APCreateBatchPayment = new bool?(false);
        break;
      case "B":
        row.APCreateBatchPayment = new bool?(true);
        row.APPrintChecks = new bool?(false);
        break;
      default:
        row.APPrintChecks = new bool?(false);
        row.APCreateBatchPayment = new bool?(false);
        break;
    }
    bool? nullable = row.APPrintChecks;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.APCreateBatchPayment;
      if (!nullable.GetValueOrDefault())
        goto label_8;
    }
    sender.SetValuePending<PX.Objects.CA.PaymentMethod.aPRequirePaymentRef>((object) row, (object) true);
label_8:
    nullable = row.APPrintChecks;
    if (!nullable.GetValueOrDefault())
    {
      sender.SetDefaultExt<PX.Objects.CA.PaymentMethod.aPCheckReportID>((object) row);
      sender.SetDefaultExt<PX.Objects.CA.PaymentMethod.aPStubLines>((object) row);
      sender.SetDefaultExt<PX.Objects.CA.PaymentMethod.aPPrintRemittance>((object) row);
      sender.SetDefaultExt<PX.Objects.CA.PaymentMethod.aPRemittanceReportID>((object) row);
    }
    nullable = row.APCreateBatchPayment;
    if (nullable.GetValueOrDefault())
      return;
    sender.SetDefaultExt<PX.Objects.CA.PaymentMethod.aPBatchExportSYMappingID>((object) row);
  }

  protected virtual void PaymentMethod_APPrintChecks_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CA.PaymentMethod row = (PX.Objects.CA.PaymentMethod) e.Row;
    if (row.APPrintChecks.Value)
    {
      row.APCreateBatchPayment = new bool?(false);
      row.APCheckReportID = (string) null;
    }
    else
      sender.SetDefaultExt<PX.Objects.CA.PaymentMethod.aPCreateBatchPayment>((object) row);
  }

  public virtual int ExecuteInsert(string viewName, IDictionary values, params object[] parameters)
  {
    switch (viewName)
    {
      case "DetailsForCashAccount":
        values[(object) PXDataUtils.FieldName<PaymentMethodDetail.useFor>()] = (object) "C";
        break;
      case "DetailsForVendor":
        values[(object) PXDataUtils.FieldName<PaymentMethodDetail.useFor>()] = (object) "V";
        break;
      case "DetailsForReceivable":
        values[(object) PXDataUtils.FieldName<PaymentMethodDetail.useFor>()] = (object) "R";
        break;
      case "ProcessingCenters":
        this.ThrowIfProcessingCenterDoesNotExist((string) values[(object) PXDataUtils.FieldName<CCProcessingCenterPmntMethod.processingCenterID>()]);
        break;
    }
    return ((PXGraph) this).ExecuteInsert(viewName, values, parameters);
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    string str = (string) values[(object) PXDataUtils.FieldName<PaymentMethodDetail.useFor>()];
    if (string.IsNullOrEmpty(str) || str == "A")
    {
      switch (viewName)
      {
        case "DetailsForCashAccount":
          keys[(object) PXDataUtils.FieldName<PaymentMethodDetail.useFor>()] = (object) "C";
          values[(object) PXDataUtils.FieldName<PaymentMethodDetail.useFor>()] = (object) "C";
          break;
        case "DetailsForVendor":
          keys[(object) PXDataUtils.FieldName<PaymentMethodDetail.useFor>()] = (object) "V";
          values[(object) PXDataUtils.FieldName<PaymentMethodDetail.useFor>()] = (object) "V";
          break;
        case "DetailsForReceivable":
          keys[(object) PXDataUtils.FieldName<PaymentMethodDetail.useFor>()] = (object) "R";
          values[(object) PXDataUtils.FieldName<PaymentMethodDetail.useFor>()] = (object) "R";
          break;
        case "ProcessingCenters":
          if (keys.Contains((object) PXDataUtils.FieldName<CCProcessingCenterPmntMethod.processingCenterID>()))
          {
            this.ThrowIfProcessingCenterDoesNotExist((string) values[(object) PXDataUtils.FieldName<CCProcessingCenterPmntMethod.processingCenterID>()]);
            break;
          }
          break;
      }
    }
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  private void ThrowIfProcessingCenterDoesNotExist(string newId)
  {
    if (newId == null)
      return;
    if (PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXViewOf<CCProcessingCenter>.BasedOn<SelectFromBase<CCProcessingCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CCProcessingCenter.processingCenterID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newId
    })) == null)
      throw new PXSetPropertyException("The {0} processing center cannot be found in the system.", new object[1]
      {
        (object) newId
      });
  }

  public virtual Dictionary<string, string> GetPlugInSettings() => new Dictionary<string, string>();

  public virtual Dictionary<SelectorType?, string> GetPlugInSelectorTypes()
  {
    return new Dictionary<SelectorType?, string>();
  }

  protected virtual void PaymentMethodDetail_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    bool flag1 = false;
    if (!(e.Row is PaymentMethodDetail row) || row != null && string.IsNullOrEmpty(row.DetailID))
      flag1 = true;
    PXUIFieldAttribute.SetEnabled<PaymentMethodDetail.detailID>(cache, e.Row, flag1);
    bool flag2 = row != null && row.IsIdentifier.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<PaymentMethodDetail.displayMask>(cache, e.Row, flag2);
    int? controlType = (int?) row?.ControlType;
    PaymentMethodDetailType? nullable = controlType.HasValue ? new PaymentMethodDetailType?((PaymentMethodDetailType) controlType.GetValueOrDefault()) : new PaymentMethodDetailType?();
    PaymentMethodDetailType methodDetailType = PaymentMethodDetailType.Text;
    bool flag3 = !(nullable.GetValueOrDefault() == methodDetailType & nullable.HasValue);
    PXUIFieldAttribute.SetEnabled<PaymentMethodDetail.defaultValue>(cache, e.Row, flag3);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PaymentMethodDetail.controlType> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentMethodDetail.controlType>>) e).Cache.SetDefaultExt<PaymentMethodDetail.defaultValue>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PaymentMethodDetail, PaymentMethodDetail.defaultValue> e)
  {
    PaymentMethodDetailHelper.PaymentMethodDetailValueFieldSelecting((PXGraph) this, e);
  }

  protected virtual void CCProcessingCenterPmntMethod_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    CCProcessingCenterPmntMethod row = e.Row as CCProcessingCenterPmntMethod;
    CCProcessingCenter processingCenter = PXParentAttribute.SelectParent<CCProcessingCenter>(cache, (object) row);
    UIState.RaiseOrHideErrorByErrorLevelPriority<CCProcessingCenterPmntMethod.processingCenterID>(cache, e.Row, (processingCenter != null ? (processingCenter.IsExternalAuthorizationOnly.GetValueOrDefault() ? 1 : 0) : 0) != 0, "The {0} processing center does not support reauthorization.", (PXErrorLevel) 3, (object) row.ProcessingCenterID);
  }

  protected virtual void PaymentMethodDetail_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (this.errorKey)
    {
      this.errorKey = false;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PaymentMethodDetail row = (PaymentMethodDetail) e.Row;
      string detailId = row.DetailID;
      string useFor = row.UseFor;
      bool flag = false;
      foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) this.Details).Select(Array.Empty<object>()))
      {
        PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
        if (paymentMethodDetail.DetailID == detailId && useFor == paymentMethodDetail.UseFor)
          flag = true;
      }
      if (!flag)
        return;
      cache.RaiseExceptionHandling<PaymentMethodDetail.detailID>(e.Row, (object) detailId, (Exception) new PXException("Record already exists."));
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void PaymentMethodDetail_DetailID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if ((e.Row as PaymentMethodDetail).DetailID == null)
      return;
    this.errorKey = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CCProcessingCenterPmntMethodBranch.processingCenterID> e)
  {
    this.ValidateProcCenter((CCProcessingCenterPmntMethodBranch) e.Row, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenterPmntMethodBranch.processingCenterID>>) e).Cache);
  }

  protected virtual void _(
    PX.Data.Events.RowInserting<CCProcessingCenterPmntMethodBranch> e)
  {
    if (this.errorKey)
    {
      this.errorKey = false;
      e.Cancel = true;
    }
    else
    {
      CCProcessingCenterPmntMethodBranch row = e.Row;
      foreach (PXResult<CCProcessingCenterPmntMethodBranch> pxResult in ((PXSelectBase<CCProcessingCenterPmntMethodBranch>) this.BranchProcessingCenters).Select(Array.Empty<object>()))
      {
        CCProcessingCenterPmntMethodBranch pmntMethodBranch = PXResult<CCProcessingCenterPmntMethodBranch>.op_Implicit(pxResult);
        if (row != pmntMethodBranch)
        {
          int? branchId1 = row.BranchID;
          int? branchId2 = pmntMethodBranch.BranchID;
          if (branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue)
          {
            PX.Objects.GL.Branch parent = (PX.Objects.GL.Branch) PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CCProcessingCenterPmntMethodBranch>.By<CCProcessingCenterPmntMethodBranch.branchID>.FindParent((PXGraph) this, (CCProcessingCenterPmntMethodBranch.branchID) pmntMethodBranch, (PKFindOptions) 0);
            ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<CCProcessingCenterPmntMethodBranch>>) e).Cache.RaiseExceptionHandling<CCProcessingCenterPmntMethodBranch.branchID>((object) row, (object) parent.BranchCD, (Exception) new PXException("The {0} processing center has been selected as default for the {1} branch. Only one processing center can be defined as default for a single branch and payment method.", new object[2]
            {
              (object) pmntMethodBranch.ProcessingCenterID,
              (object) parent.BranchCD
            }));
            e.Cancel = true;
            break;
          }
        }
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.RowPersisting<CCProcessingCenterPmntMethodBranch> e)
  {
    this.ValidateProcCenter(e.Row, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CCProcessingCenterPmntMethodBranch>>) e).Cache);
  }

  private void ValidateProcCenter(CCProcessingCenterPmntMethodBranch currentSetting, PXCache cache)
  {
    if (string.IsNullOrEmpty(currentSetting.ProcessingCenterID) || ((IEnumerable<CCProcessingCenterPmntMethod>) ((PXSelectBase<CCProcessingCenterPmntMethod>) this.ProcessingCenters).Select<CCProcessingCenterPmntMethod>(Array.Empty<object>())).Where<CCProcessingCenterPmntMethod>((Func<CCProcessingCenterPmntMethod, bool>) (e => e.IsActive.GetValueOrDefault())).Select<CCProcessingCenterPmntMethod, string>((Func<CCProcessingCenterPmntMethod, string>) (e => e.ProcessingCenterID)).ToHashSet<string>().Contains(currentSetting.ProcessingCenterID))
      return;
    cache.RaiseExceptionHandling<CCProcessingCenterPmntMethodBranch.processingCenterID>((object) currentSetting, (object) currentSetting.ProcessingCenterID, (Exception) new PXException("The {0} processing center is not an active processing center configured for the current payment method.", new object[1]
    {
      (object) currentSetting.ProcessingCenterID
    }));
  }

  protected virtual void PaymentMethodAccount_PaymentMethodID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentMethodAccount_CashAccountID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    int? newValue = e.NewValue as int?;
    if (!newValue.HasValue)
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelectReadonly<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newValue
    }));
    if (cashAccount == null || cashAccount.Active.GetValueOrDefault())
      return;
    string str = $"The cash account {cashAccount.CashAccountCD} is deactivated on the Cash Accounts (CA202000) form.";
    cache.RaiseExceptionHandling<PaymentMethodAccount.cashAccountID>(e.Row, (object) cashAccount.CashAccountCD, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 4));
  }

  protected virtual void PaymentMethodAccount_APQuickBatchGeneration_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    PaymentMethodAccountHelper.APQuickBatchGenerationFieldVerifying(cache, e);
  }

  protected virtual void PaymentMethodAccount_CashAccountID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    cache.SetDefaultExt<PaymentMethodAccount.useForAP>((object) row);
  }

  protected virtual void PaymentMethodAccount_UseForAP_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethod).Current;
    if (row == null || current == null)
      return;
    e.NewValue = (object) current.UseForAP.GetValueOrDefault();
    if (current.UseForAP.GetValueOrDefault() && row.CashAccountID.HasValue)
    {
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelectReadonly<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.CashAccountID
      }));
      e.NewValue = (object) (cashAccount != null);
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentMethodAccount_UseForAR_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethod).Current;
    e.NewValue = (object) (bool) (current == null ? 0 : (current.UseForAR.GetValueOrDefault() ? 1 : 0));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentMethodAccount_APQuickBatchGeneration_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    int num;
    if (row != null)
    {
      bool? nullable = row.UseForAP;
      if (nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethod).Current.APCreateBatchPayment;
        num = nullable.GetValueOrDefault() ? 1 : 0;
        goto label_4;
      }
    }
    num = 0;
label_4:
    // ISSUE: variable of a boxed type
    __Boxed<bool> local = (ValueType) (bool) num;
    defaultingEventArgs.NewValue = (object) local;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentMethodAccount_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    if (row == null)
      return;
    bool? nullable;
    if (!string.IsNullOrEmpty(row.PaymentMethodID))
    {
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.PaymentMethodID
      }));
      int num;
      if (paymentMethod != null)
      {
        nullable = paymentMethod.APCreateBatchPayment;
        num = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num = 0;
      bool flag = num != 0;
      PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPBatchLastRefNbr>(cache, (object) row, flag);
    }
    bool flag1 = true;
    if (row.CashAccountID.HasValue)
    {
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelectReadonly<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.CashAccountID
      }));
      PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.useForAP>(cache, (object) row, cashAccount != null);
      nullable = cashAccount.Active;
      flag1 = nullable ?? true;
    }
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.useForAP>(cache, e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.useForAR>(cache, e.Row, flag1);
    nullable = row.UseForAP;
    bool valueOrDefault1 = (flag1 ? nullable : new bool?(false)).GetValueOrDefault();
    nullable = row.UseForAR;
    bool valueOrDefault2 = (flag1 ? nullable : new bool?(false)).GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPIsDefault>(cache, e.Row, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPAutoNextNbr>(cache, e.Row, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPLastRefNbr>(cache, e.Row, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPBatchLastRefNbr>(cache, e.Row, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPQuickBatchGeneration>(cache, e.Row, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aRIsDefault>(cache, e.Row, valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aRIsDefaultForRefund>(cache, e.Row, valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aRAutoNextNbr>(cache, e.Row, valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aRLastRefNbr>(cache, e.Row, valueOrDefault2);
    PaymentMethodAccountHelper.VerifyAPAutoNextNbr(cache, row, ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodCurrent).Current);
  }

  protected virtual void PaymentMethodAccount_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    PaymentMethodAccount newRow = (PaymentMethodAccount) e.NewRow;
    if (newRow == null)
      return;
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    bool? nullable1 = newRow.UseForAP;
    bool? nullable2 = row.UseForAP;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      nullable2 = newRow.UseForAP;
      if (!(nullable2 ?? false))
        newRow.APIsDefault = new bool?(false);
    }
    nullable2 = newRow.UseForAR;
    nullable1 = row.UseForAR;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      return;
    nullable1 = newRow.UseForAR;
    if (nullable1 ?? false)
      return;
    newRow.ARIsDefault = new bool?(false);
  }

  protected virtual void PaymentMethodAccount_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    if (PaymentMethodAccountHelper.CheckIfErrorExists<PaymentMethodAccount.aPQuickBatchGeneration>(cache, row, (PXErrorLevel) 4))
      return;
    PXException error = (PXException) null;
    if (row.APQuickBatchGeneration.GetValueOrDefault() && !PaymentMethodAccountHelper.TryToVerifyAPLastReferenceNumber<PaymentMethodAccount.aPQuickBatchGeneration>(row.APAutoNextNbr, row.APLastRefNbr, (string) null, out error))
      cache.RaiseExceptionHandling<PaymentMethodAccount.aPQuickBatchGeneration>((object) row, (object) row.APQuickBatchGeneration, (Exception) error);
    else
      cache.RaiseExceptionHandling<PaymentMethodAccount.aPQuickBatchGeneration>((object) row, (object) row.APQuickBatchGeneration, (Exception) null);
  }

  protected virtual void PaymentMethodAccount_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    PXEntryStatus status = cache.GetStatus(e.Row);
    if (!row.CashAccountID.HasValue || status == 2 || status == 4)
      return;
    if (PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.cashAccountID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) row.PaymentMethodID,
      (object) row.CashAccountID
    })) != null)
      throw new PXException("This Cash Account is used  in one or more Customer Payment Methods and can not be deleted");
    this.VerifyCashAccountLinkOrMethodCanBeDeleted(CashAccount.PK.Find((PXGraph) this, row.CashAccountID));
  }

  protected virtual void PaymentMethodAccount_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    if (row == null || string.IsNullOrEmpty(row.PaymentMethodID))
      return;
    int? cashAccountId1 = row.CashAccountID;
    if (!cashAccountId1.HasValue)
      return;
    foreach (PXResult<PaymentMethodAccount, CashAccount> pxResult in ((PXSelectBase<PaymentMethodAccount>) this.CashAccounts).Select(Array.Empty<object>()))
    {
      PaymentMethodAccount paymentMethodAccount = PXResult<PaymentMethodAccount, CashAccount>.op_Implicit(pxResult);
      if (row != paymentMethodAccount && paymentMethodAccount.PaymentMethodID == row.PaymentMethodID)
      {
        cashAccountId1 = row.CashAccountID;
        int? cashAccountId2 = paymentMethodAccount.CashAccountID;
        if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
          throw new PXSetPropertyException("Cash Account '{0}' is already added to this Payment method", new object[1]
          {
            (object) PXResult<PaymentMethodAccount, CashAccount>.op_Implicit(pxResult).CashAccountCD
          });
      }
    }
  }

  protected virtual void PaymentMethodAccount_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    PXDefaultAttribute.SetPersistingCheck<PaymentMethodAccount.aPLastRefNbr>(sender, e.Row, row.APAutoNextNbr.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    bool? nullable = row.APAutoNextNbr;
    if (nullable.GetValueOrDefault() && row.APLastRefNbr == null)
      sender.RaiseExceptionHandling<PaymentMethodAccount.aPAutoNextNbr>((object) row, (object) row.APAutoNextNbr, (Exception) new PXSetPropertyException("To use the {0} - Suggest Next Number option you must specify the {0} Last Reference Number.", new object[1]
      {
        (object) "AP"
      }));
    nullable = row.ARAutoNextNbr;
    if (nullable.GetValueOrDefault() && row.ARLastRefNbr == null)
      sender.RaiseExceptionHandling<PaymentMethodAccount.aRAutoNextNbr>((object) row, (object) row.ARAutoNextNbr, (Exception) new PXSetPropertyException("To use the {0} - Suggest Next Number option you must specify the {0} Last Reference Number.", new object[1]
      {
        (object) "AR"
      }));
    PaymentMethodAccountHelper.VerifyQuickBatchGenerationOnRowPersisting(sender, row);
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelectReadonly<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.CashAccountID
    }));
    if (cashAccount == null)
      return;
    nullable = cashAccount.Active;
    if (nullable.GetValueOrDefault())
      return;
    if (e.Operation == 1)
    {
      int? cashAccountId1 = (int?) PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelectReadonly<PaymentMethodAccount, Where<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>, And<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.PaymentMethodID,
        (object) row.CashAccountID
      }))?.CashAccountID;
      int? cashAccountId2 = row.CashAccountID;
      if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
        return;
    }
    string str = $"The cash account {cashAccount.CashAccountCD.Trim()} is deactivated on the Cash Accounts (CA202000) form.";
    sender.RaiseExceptionHandling<PaymentMethodAccount.cashAccountID>(e.Row, (object) cashAccount.CashAccountCD, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 4));
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
      bool flag1 = false;
      foreach (PXResult<CCProcessingCenterPmntMethod> pxResult in ((PXSelectBase<CCProcessingCenterPmntMethod>) this.ProcessingCenters).Select(Array.Empty<object>()))
      {
        CCProcessingCenterPmntMethod centerPmntMethod = PXResult<CCProcessingCenterPmntMethod>.op_Implicit(pxResult);
        if (centerPmntMethod != row && centerPmntMethod.ProcessingCenterID == row.ProcessingCenterID)
          flag1 = true;
      }
      if (flag1)
      {
        cache.RaiseExceptionHandling<CCProcessingCenterPmntMethod.processingCenterID>(e.Row, (object) processingCenterId, (Exception) new PXException("This Processing Center is already assigned to the Payment Method"));
        ((CancelEventArgs) e).Cancel = true;
      }
      else
      {
        if (!CCProcessingFeatureHelper.IsPaymentHostedFormSupported(this.GetProcessingCenterById(row.ProcessingCenterID)))
          return;
        bool? isDefault = row.IsDefault;
        bool flag2 = false;
        if (!(isDefault.GetValueOrDefault() == flag2 & isDefault.HasValue) || ((PXSelectBase<CCProcessingCenterPmntMethod>) this.ProcessingCenters).Ask("Make this processing center default?", (MessageButtons) 4) != 6)
          return;
        row.IsDefault = new bool?(true);
      }
    }
  }

  protected virtual void CCProcessingCenterPmntMethod_IsDefault_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<CCProcessingCenterPmntMethod>) this.ProcessingCenters).Any<CCProcessingCenterPmntMethod>())
      return;
    e.NewValue = (object) true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CCProcessingCenterPmntMethod.isActive> e)
  {
    if (!(e.Row is CCProcessingCenterPmntMethod row) || row.IsActive.GetValueOrDefault())
      return;
    foreach (PXResult<CCProcessingCenterPmntMethodBranch> pxResult in ((PXSelectBase<CCProcessingCenterPmntMethodBranch>) this.BranchProcessingCenters).Select(Array.Empty<object>()))
    {
      CCProcessingCenterPmntMethodBranch pmntMethodBranch = PXResult<CCProcessingCenterPmntMethodBranch>.op_Implicit(pxResult);
      if (pmntMethodBranch.ProcessingCenterID == row.ProcessingCenterID)
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CCProcessingCenterPmntMethod.isActive>>) e).Cache.DisplayFieldWarning<CCProcessingCenterPmntMethod.isActive>((object) row, (object) row.IsActive, "The corresponding setting for the {0} processing center in the Overrides by Branch section will be removed once the changes are saved.", (object) pmntMethodBranch.ProcessingCenterID);
    }
  }

  protected virtual void _(
    PX.Data.Events.RowPersisting<CCProcessingCenterPmntMethod> e)
  {
    CCProcessingCenterPmntMethod row = e.Row;
    if (row.IsActive.GetValueOrDefault())
      return;
    foreach (PXResult<CCProcessingCenterPmntMethodBranch> pxResult in ((PXSelectBase<CCProcessingCenterPmntMethodBranch>) this.BranchProcessingCenters).Select(Array.Empty<object>()))
    {
      CCProcessingCenterPmntMethodBranch pmntMethodBranch = PXResult<CCProcessingCenterPmntMethodBranch>.op_Implicit(pxResult);
      if (pmntMethodBranch.ProcessingCenterID == row.ProcessingCenterID)
        ((PXSelectBase<CCProcessingCenterPmntMethodBranch>) this.BranchProcessingCenters).Delete(pmntMethodBranch);
    }
  }

  protected virtual void fillCreditCardDefaults()
  {
    PaymentMethodDetail paymentMethodDetail = (PaymentMethodDetail) ((PXSelectBase) this.Details).Cache.Insert((object) new PaymentMethodDetail()
    {
      DetailID = "CCPID",
      EntryMask = "",
      ValidRegexp = "",
      IsRequired = new bool?(true),
      IsCCProcessingID = new bool?(true),
      Descr = "Payment Profile ID",
      UseFor = "R",
      OrderIndex = new short?((short) 1)
    });
    if (!PXDBLocalizableStringAttribute.IsEnabled)
      return;
    PXDBLocalizableStringAttribute.DefaultTranslationsFromMessage(((PXSelectBase) this.Details).Cache, (object) paymentMethodDetail, "Descr", "Payment Profile ID");
  }

  private CCProcessingCenter GetProcessingCenterById(string id)
  {
    return PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) id
    }));
  }

  private void CheckPaymentMethodDetailsIfProcessingReq(PX.Objects.CA.PaymentMethod pm)
  {
    if (pm != null)
    {
      bool? processingRequired = pm.ARIsProcessingRequired;
      bool flag = false;
      if (processingRequired.GetValueOrDefault() == flag & processingRequired.HasValue)
        return;
    }
    foreach (PXResult<CCProcessingCenterPmntMethod> pxResult in ((PXSelectBase<CCProcessingCenterPmntMethod>) this.ProcessingCenters).Select(Array.Empty<object>()))
    {
      if (CCProcessingFeatureHelper.IsFeatureSupported(this.GetProcessingCenterById(PXResult<CCProcessingCenterPmntMethod>.op_Implicit(pxResult).ProcessingCenterID), CCProcessingFeature.ProfileManagement) && PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<PaymentMethodDetail.isCCProcessingID, Equal<True>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) == null && pm.PaymentType != "POS")
        throw new PXException("Payment Profile ID in 'Settings for Use in AR' has to be set up before tokenized processing centers can be used");
    }
  }

  private void CheckPaymentMethodDetailConsistency(PaymentMethodDetail detail)
  {
    bool valueOrDefault1 = detail.IsCVV.GetValueOrDefault();
    bool valueOrDefault2 = detail.IsCCProcessingID.GetValueOrDefault();
    bool valueOrDefault3 = detail.IsExpirationDate.GetValueOrDefault();
    bool valueOrDefault4 = detail.IsOwnerName.GetValueOrDefault();
    bool valueOrDefault5 = detail.IsIdentifier.GetValueOrDefault();
    if ((valueOrDefault1 ? 1 : 0) + (valueOrDefault2 ? 1 : 0) + (valueOrDefault3 ? 1 : 0) + (valueOrDefault4 ? 1 : 0) + (valueOrDefault5 ? 1 : 0) > 1)
    {
      PXRowPersistingException persistingException = new PXRowPersistingException("PaymentMethodDetail", (object) null, "Only one of the following check boxes can be selected in one detail line: Card/Account Nbr., Exp. Date, CVV Code, Name on Card, and Payment Profile ID.");
      if (valueOrDefault1)
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<PaymentMethodDetail.isCVV>((object) detail, (object) detail.IsCVV, (Exception) persistingException);
      if (valueOrDefault2)
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<PaymentMethodDetail.isCCProcessingID>((object) detail, (object) detail.IsCCProcessingID, (Exception) persistingException);
      if (valueOrDefault3)
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<PaymentMethodDetail.isExpirationDate>((object) detail, (object) detail.IsExpirationDate, (Exception) persistingException);
      if (valueOrDefault4)
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<PaymentMethodDetail.isOwnerName>((object) detail, (object) detail.IsOwnerName, (Exception) persistingException);
      if (valueOrDefault5)
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<PaymentMethodDetail.isIdentifier>((object) detail, (object) detail.IsIdentifier, (Exception) persistingException);
      throw persistingException;
    }
  }

  public virtual void VerifyAPRequirePaymentRefAndAPAdditionalProcessing(
    bool? apRequirePaymentRef,
    string apAdditionalProcessing)
  {
    if (!apRequirePaymentRef.GetValueOrDefault() && !(apAdditionalProcessing != "N"))
      return;
    foreach (PXResult<PaymentMethodAccount, CashAccount> pxResult in ((PXSelectBase<PaymentMethodAccount>) this.CashAccounts).Select(Array.Empty<object>()))
    {
      if (PXResult<PaymentMethodAccount, CashAccount>.op_Implicit(pxResult).UseForCorpCard.GetValueOrDefault())
        throw new PXSetPropertyException("The payment method has an associated cash account configured for corporate cards and should have the following settings specified on the Settings for Use in AP tab: the Require Unique Payment Ref. check box is cleared and the Not Required option is selected in the Additional Processing section.");
    }
  }

  public virtual void VerifyCashAccountLinkOrMethodCanBeDeleted(CashAccount cashAccount)
  {
    if (cashAccount.UseForCorpCard.GetValueOrDefault())
      throw new PXException("You cannot delete the associated cash account because it is configured for corporate cards. If you need to change the payment method for this cash account, please use the Cash Accounts (CA202000) form.");
  }

  [PXMergeAttributes]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PaymentMethodMaint.PXFormulaEditor_AddFields]
  public virtual void _(PX.Data.Events.CacheAttached<ACHPlugInParameter.value> e)
  {
  }

  public class PXFormulaEditor_AddFieldsAttribute : PXFormulaEditor.OptionsProviderAttribute
  {
    public virtual void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
    {
      PaymentMethodMaint paymentMethodMaint = (PaymentMethodMaint) graph;
      if (((PXSelectBase<PX.Objects.CA.PaymentMethod>) paymentMethodMaint.PaymentMethod).Current == null)
        return;
      foreach (string str in ((IEnumerable<string>) paymentMethodMaint.GetAddendaInfoFields()).Where<string>((Func<string, bool>) (a => !string.IsNullOrWhiteSpace(a))))
        options.Add(new FormulaOption()
        {
          Category = "Fields",
          Value = str
        });
    }
  }
}
