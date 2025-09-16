// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodMaintACHPlugInExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.ACHPlugInBase;
using PX.Common;
using PX.Data;
using PX.Objects.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class PaymentMethodMaintACHPlugInExt : PXGraphExtension<PaymentMethodMaint>
{
  public static bool IsActive() => true;

  [PXOverride]
  public virtual Dictionary<string, string> GetPlugInSettings(
    PaymentMethodMaintACHPlugInExt.GetPlugInSettingsDelegate baseMethod)
  {
    Dictionary<string, string> plugInSettings = baseMethod();
    if (this.Base.IsACHPlugIn())
      EnumerableExtensions.AddRange<string, string>((IDictionary<string, string>) plugInSettings, (IEnumerable<KeyValuePair<string, string>>) ACHPlugInSettingsMapping.Settings);
    return plugInSettings;
  }

  [PXOverride]
  public virtual Dictionary<SelectorType?, string> GetPlugInSelectorTypes(
    PaymentMethodMaintACHPlugInExt.GetPlugInSelectorTypesDelegate baseMethod)
  {
    Dictionary<SelectorType?, string> plugInSelectorTypes = baseMethod();
    if (this.Base.IsACHPlugIn())
      EnumerableExtensions.AddRange<SelectorType?, string>((IDictionary<SelectorType?, string>) plugInSelectorTypes, (IEnumerable<KeyValuePair<SelectorType?, string>>) ACHPlugInSettingsMapping.SelectorTypes);
    return plugInSelectorTypes;
  }

  [PXOverride]
  public virtual IEnumerable<ACHPlugInParameter> ApplyFilters(
    IEnumerable<ACHPlugInParameter> aCHPlugInParameters,
    PaymentMethodMaintACHPlugInExt.ApplyFiltersDelegate baseMethod)
  {
    aCHPlugInParameters = baseMethod(aCHPlugInParameters);
    if (!this.Base.IsACHPlugIn())
      return aCHPlugInParameters;
    bool result1 = false;
    bool result2 = false;
    foreach (ACHPlugInParameter chPlugInParameter in aCHPlugInParameters)
    {
      if (chPlugInParameter.ParameterID.ToUpper() == "IncludeOffsetRecord".ToUpper())
        bool.TryParse(chPlugInParameter.Value, out result1);
      if (chPlugInParameter.ParameterID.ToUpper() == "IncludeAddendaRecords".ToUpper())
        bool.TryParse(chPlugInParameter.Value, out result2);
    }
    List<ACHPlugInParameter> achPlugInParameterList = new List<ACHPlugInParameter>();
    foreach (ACHPlugInParameter chPlugInParameter in aCHPlugInParameters)
    {
      PlugInFilter current = ((PXSelectBase<PlugInFilter>) this.Base.plugInFilter).Current;
      if (((current != null ? (!current.ShowOffsetSettings.GetValueOrDefault() ? 1 : 0) : 1) == 0 || !(chPlugInParameter.ParameterID.ToUpper() == "OffsetDFIAccountNbr".ToUpper()) && !(chPlugInParameter.ParameterID.ToUpper() == "OffsetReceivingDEFIID".ToUpper()) && !(chPlugInParameter.ParameterID.ToUpper() == "OffsetReceivingID".ToUpper())) && (result2 || !(chPlugInParameter.ParameterID.ToUpper() == "AddendaRecordTemplate".ToUpper())))
        achPlugInParameterList.Add(chPlugInParameter);
    }
    return (IEnumerable<ACHPlugInParameter>) achPlugInParameterList;
  }

  /// <summary>
  /// Override of <see cref="M:PX.Objects.CA.PaymentMethodMaint.UpdatePlugInSettings(PX.Objects.CA.PaymentMethod)" />
  /// </summary>
  [PXOverride]
  public virtual void UpdatePlugInSettings(PaymentMethod pm)
  {
    if (!(pm.APBatchExportMethod == "P") || ((PXSelectBase<ACHPlugInParameter>) this.Base.aCHPlugInParameters).Any<ACHPlugInParameter>() || !string.IsNullOrEmpty(pm.DirectDepositFileFormat))
      return;
    if (string.IsNullOrEmpty(pm.APBatchExportPlugInTypeName))
    {
      PaymentMethod copy = (PaymentMethod) ((PXSelectBase) this.Base.PaymentMethod).Cache.CreateCopy((object) pm);
      copy.APBatchExportPlugInTypeName = ACHPlugInTypeAttribute.USACHPlugInType;
      ((PXSelectBase<PaymentMethod>) this.Base.PaymentMethod).Update(copy);
    }
    if (!this.Base.IsACHPlugIn() || !string.IsNullOrEmpty(pm.DirectDepositFileFormat) || ((PXGraph) this.Base).IsCopyPasteContext)
      return;
    if (!this.CheckIfACHSettingsExists())
    {
      this.AppendDefaultSettings();
      this.AppendDefaultPlugInParameters();
    }
    else if (this.CheckAcumaticaExportScenariosMapping())
    {
      this.UpdateDetailsAccordingToPlugIn();
      this.AppendDefaultPlugInParameters(true);
    }
    else
      this.AppendDefaultPlugInParameters();
  }

  private void AppendDefaultPlugInParameters(bool useExportScenarioMapping = false)
  {
    this.Base.AppendDefaultPlugInParameters(DefaultPaymentMethodDetailsHelper.ToIntKey(DefaultPaymentMethodDetailsHelper.Dictionary), useExportScenarioMapping);
  }

  private bool CheckIfACHSettingsExists()
  {
    return ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForCashAccount).Any<PaymentMethodDetail>() || ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForVendor).Any<PaymentMethodDetail>();
  }

  private bool CheckAcumaticaExportScenariosMapping()
  {
    Dictionary<string, string> dictionary1 = new Dictionary<string, string>()
    {
      {
        "1",
        "Beneficiary Account No:"
      },
      {
        "2",
        "Beneficiary Name:"
      },
      {
        "3",
        "Bank Routing Number (ABA):"
      },
      {
        "4",
        "Bank Name:"
      },
      {
        "5",
        "Company ID"
      },
      {
        "6",
        "Company ID Type"
      },
      {
        "7",
        "Offset ABA/Routing #"
      },
      {
        "8",
        "Offset Account #"
      },
      {
        "9",
        "Offset Description"
      }
    };
    Dictionary<string, string> dictionary2 = new Dictionary<string, string>()
    {
      {
        "1",
        "Beneficiary Account No:"
      },
      {
        "2",
        "Beneficiary Name:"
      },
      {
        "3",
        "Bank Routing Number (ABA):"
      },
      {
        "4",
        "Bank Name:"
      }
    };
    try
    {
      foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForCashAccount).Select(Array.Empty<object>()))
      {
        PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
        string key = paymentMethodDetail.DetailID.Trim();
        if (dictionary1[key] != paymentMethodDetail.Descr.Trim())
          return false;
      }
      foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForVendor).Select(Array.Empty<object>()))
      {
        PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
        string key = paymentMethodDetail.DetailID.Trim();
        if (dictionary2[key] != paymentMethodDetail.Descr.Trim())
          return false;
      }
    }
    catch
    {
      return false;
    }
    return true;
  }

  private void AppendDefaultSettings()
  {
    foreach (NewPaymentMethodDetail defaultSetting in this.GetDefaultSettings(DefaultDetails.DetailsToAddByDefault))
    {
      if (defaultSetting.UseFor == "C")
        ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForCashAccount).Insert(defaultSetting.ToPaymentMethodDetail(((PXSelectBase<PaymentMethod>) this.Base.PaymentMethod).Current));
      if (defaultSetting.UseFor == "V")
        ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForVendor).Insert(defaultSetting.ToPaymentMethodDetail(((PXSelectBase<PaymentMethod>) this.Base.PaymentMethod).Current));
    }
  }

  private void AppendTransactionCodeSetting()
  {
    foreach (NewPaymentMethodDetail defaultSetting in this.GetDefaultSettings(DefaultDetails.DetailsToAddTransactionCode))
    {
      if (defaultSetting.UseFor == "C")
        ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForCashAccount).Insert(defaultSetting.ToPaymentMethodDetail(((PXSelectBase<PaymentMethod>) this.Base.PaymentMethod).Current));
      if (defaultSetting.UseFor == "V")
        ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForVendor).Insert(defaultSetting.ToPaymentMethodDetail(((PXSelectBase<PaymentMethod>) this.Base.PaymentMethod).Current));
    }
  }

  private void UpdateDetailsAccordingToPlugIn()
  {
    foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForCashAccount).Select(Array.Empty<object>()))
    {
      PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
      if (paymentMethodDetail.DetailID == "5" && paymentMethodDetail.Descr == "Company ID")
      {
        paymentMethodDetail.ValidRegexp = "^([\\w]|\\s){0,10}$";
        ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForCashAccount).Update(paymentMethodDetail);
      }
      if (paymentMethodDetail.DetailID == "6" && paymentMethodDetail.Descr == "Company ID Type" && paymentMethodDetail.IsRequired.GetValueOrDefault())
      {
        paymentMethodDetail.IsRequired = new bool?(false);
        ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForCashAccount).Update(paymentMethodDetail);
      }
    }
    this.AppendTransactionCodeSetting();
  }

  private void AppendOffsetSettings()
  {
    Dictionary<DefaultPaymentMethodDetails, string> dictionary = new Dictionary<DefaultPaymentMethodDetails, string>();
    foreach (NewPaymentMethodDetail defaultSetting in this.GetDefaultSettings(DefaultDetails.DetailsToAddForOffset))
    {
      ((PXSelectBase<PaymentMethodDetail>) this.Base.DetailsForCashAccount).Insert(defaultSetting.ToPaymentMethodDetail(((PXSelectBase<PaymentMethod>) this.Base.PaymentMethod).Current));
      dictionary.Add((DefaultPaymentMethodDetails) defaultSetting.DetailIDInt.Value, defaultSetting.DetailID);
    }
    foreach (ACHPlugInParameter achPlugInParameter in GraphHelper.QuickSelect(((PXSelectBase) this.Base.aCHPlugInParameters).View))
    {
      if (achPlugInParameter.ParameterID == "OffsetDFIAccountNbr".ToUpper())
      {
        achPlugInParameter.Value = dictionary[(DefaultPaymentMethodDetails) 11];
        ((PXSelectBase<ACHPlugInParameter>) this.Base.aCHPlugInParameters).Update(achPlugInParameter);
      }
      if (achPlugInParameter.ParameterID == "OffsetReceivingDEFIID".ToUpper())
      {
        achPlugInParameter.Value = dictionary[(DefaultPaymentMethodDetails) 12];
        ((PXSelectBase<ACHPlugInParameter>) this.Base.aCHPlugInParameters).Update(achPlugInParameter);
      }
      if (achPlugInParameter.ParameterID == "OffsetReceivingID".ToUpper())
      {
        achPlugInParameter.Value = dictionary[(DefaultPaymentMethodDetails) 13];
        ((PXSelectBase<ACHPlugInParameter>) this.Base.aCHPlugInParameters).Update(achPlugInParameter);
      }
    }
  }

  private IEnumerable<NewPaymentMethodDetail> GetDefaultSettings(
    DefaultPaymentMethodDetails[] details)
  {
    DefaultPaymentMethodDetails[] paymentMethodDetailsArray = details;
    for (int index = 0; index < paymentMethodDetailsArray.Length; ++index)
    {
      DefaultPaymentMethodDetails key = (DefaultPaymentMethodDetails) (int) paymentMethodDetailsArray[index];
      string str;
      if (DefaultPaymentMethodDetailsHelper.Dictionary.TryGetValue(key, out str))
      {
        string empty1 = string.Empty;
        DefaultDetails.DefaultDetailDescription.TryGetValue(key, out empty1);
        string empty2 = string.Empty;
        DefaultDetails.DetailsUsedFor.TryGetValue(key, out empty2);
        string empty3 = string.Empty;
        DefaultDetails.DefaultDetailValidationRegexp.TryGetValue(key, out empty3);
        string empty4 = string.Empty;
        DefaultDetails.DefaultDetailEntryMask.TryGetValue(key, out empty4);
        bool flag = ((IEnumerable<DefaultPaymentMethodDetails>) DefaultDetails.RequiredDetailsByDefault).Contains<DefaultPaymentMethodDetails>(key);
        PaymentMethodDetailType methodDetailType = ((IEnumerable<DefaultPaymentMethodDetails>) DefaultDetails.AccountTypeFields).Contains<DefaultPaymentMethodDetails>(key) ? PaymentMethodDetailType.AccountType : PaymentMethodDetailType.Text;
        ((IEnumerable<DefaultPaymentMethodDetails>) DefaultDetails.NotSelectedFieldsByDefault).Contains<DefaultPaymentMethodDetails>(key);
        yield return new NewPaymentMethodDetail()
        {
          DetailIDInt = new int?((int) key),
          DetailID = str,
          Description = empty1,
          IsRequired = new bool?(flag),
          ControlType = new int?((int) methodDetailType),
          ValidRegexp = empty3,
          UseFor = empty2,
          EntryMask = empty4
        };
      }
    }
    paymentMethodDetailsArray = (DefaultPaymentMethodDetails[]) null;
  }

  private bool CheckIfOffsetSettingsExists()
  {
    return ((IQueryable<PXResult<PaymentMethodDetail>>) PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PaymentMethod.paymentMethodID>>, And<PaymentMethodDetail.descr, In<Required<PaymentMethodDetail.descr>>>>>.Config>.Select((PXGraph) this.Base, (object[]) new string[3]
    {
      "Offset Account #",
      "Offset ABA/Routing #",
      "Offset Description"
    })).Any<PXResult<PaymentMethodDetail>>();
  }

  protected virtual void _(Events.RowDeleting<ACHPlugInParameter> e) => e.Cancel = true;

  protected virtual void _(Events.RowPersisting<ACHPlugInParameter> e)
  {
    PaymentMethod current = ((PXSelectBase<PaymentMethod>) this.Base.PaymentMethod).Current;
    bool? nullable;
    int num;
    if (current == null)
    {
      num = 1;
    }
    else
    {
      nullable = current.IsUsingPlugin;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num != 0)
      return;
    if (NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Base.PlugInParameters).Cache.Inserted))
      ((PXSelectBase) this.Base.PlugInParameters).Cache.Clear();
    bool offsetFieldsRequired = false;
    bool addendaTemplateRequired = false;
    if (this.Base.IsACHPlugIn())
      this.GetAddendaAndOffsetRequirement(out offsetFieldsRequired, out addendaTemplateRequired);
    if (!string.IsNullOrEmpty(e.Row?.Value))
      return;
    nullable = e.Row.Required;
    if (nullable.GetValueOrDefault())
      ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<ACHPlugInParameter>>) e).Cache.RaiseExceptionHandling<ACHPlugInParameter.value>((object) e.Row, (object) e.Row?.Value, (Exception) new PXSetPropertyException<ACHPlugInParameter.value>("Cannot be empty."));
    if (!this.Base.IsACHPlugIn())
      return;
    this.ValidateACHSpecificFieldsOnRowPersisting(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<ACHPlugInParameter>>) e).Cache, e.Row, offsetFieldsRequired, addendaTemplateRequired);
  }

  private void GetAddendaAndOffsetRequirement(
    out bool offsetFieldsRequired,
    out bool addendaTemplateRequired)
  {
    bool.TryParse(((PXSelectBase<ACHPlugInParameter>) this.Base.aCHPlugInParametersByParameter).SelectSingle(new object[1]
    {
      (object) "IncludeOffsetRecord".ToUpper()
    })?.Value, out offsetFieldsRequired);
    bool.TryParse(((PXSelectBase<ACHPlugInParameter>) this.Base.aCHPlugInParametersByParameter).SelectSingle(new object[1]
    {
      (object) "IncludeAddendaRecords".ToUpper()
    })?.Value, out addendaTemplateRequired);
  }

  private void ValidateACHSpecificFieldsOnRowPersisting(
    PXCache cache,
    ACHPlugInParameter row,
    bool offsetFieldsRequired,
    bool addendaTemplateRequired)
  {
    if (offsetFieldsRequired && (row.ParameterID == "OffsetDFIAccountNbr".ToUpper() || row.ParameterID == "OffsetReceivingDEFIID".ToUpper() || row.ParameterID == "OffsetReceivingID".ToUpper()))
      cache.RaiseExceptionHandling<ACHPlugInParameter.value>((object) row, (object) row?.Value, (Exception) new PXSetPropertyException<ACHPlugInParameter.value>("Cannot be empty."));
    if (!addendaTemplateRequired || !(row.ParameterID == "AddendaRecordTemplate".ToUpper()))
      return;
    cache.RaiseExceptionHandling<ACHPlugInParameter.value>((object) row, (object) row?.Value, (Exception) new PXSetPropertyException<ACHPlugInParameter.value>("Cannot be empty."));
  }

  protected virtual void _(Events.RowInserted<ACHPlugInParameter2> e)
  {
    ACHPlugInParameter2 row = e.Row;
    Dictionary<string, IACHPlugInParameter> dictionary = this.Base.GetParametersOfSelectedPlugIn().ToDictionary<IACHPlugInParameter, string>((Func<IACHPlugInParameter, string>) (m => m.ParameterID));
    if (dictionary.ContainsKey(row?.ParameterID))
    {
      row.Description = dictionary[row.ParameterID].Description;
      row.DetailMapping = dictionary[row.ParameterID].DetailMapping;
      row.ExportScenarioMapping = dictionary[row.ParameterID].ExportScenarioMapping;
      row.Type = dictionary[row.ParameterID].Type;
      row.IsFormula = dictionary[row.ParameterID].IsFormula;
      row.DataElementSize = dictionary[row.ParameterID].DataElementSize;
    }
    ((PXSelectBase<ACHPlugInParameter>) this.Base.aCHPlugInParameters).Insert((ACHPlugInParameter) row);
  }

  protected virtual void _(
    Events.FieldUpdated<ACHPlugInParameter, ACHPlugInParameter.value> e)
  {
    if (e.Row?.ParameterID.ToUpper() == "IncludeOffsetRecord".ToUpper())
    {
      bool result = false;
      bool.TryParse(e.NewValue.ToString(), out result);
      if (result && !this.CheckIfOffsetSettingsExists() && ((PXSelectBase<PaymentMethod>) this.Base.PaymentMethod).Ask("Do you want to add remittance settings for the offset record?", (MessageButtons) 4) == 6)
        this.AppendOffsetSettings();
      ((PXSelectBase) this.Base.plugInFilter).Cache.SetValueExt<PlugInFilter.showOffsetSettings>((object) ((PXSelectBase<PlugInFilter>) this.Base.plugInFilter).Current, (object) result);
    }
    if (!(e.Row?.ParameterID.ToUpper() == "IncludeAddendaRecords".ToUpper()))
      return;
    ((PXSelectBase) this.Base.aCHPlugInParameters).View.RequestRefresh();
  }

  protected virtual void _(
    Events.ExceptionHandling<ACHPlugInParameter.value> e)
  {
    if (((Events.ExceptionHandlingBase<Events.ExceptionHandling<ACHPlugInParameter.value>>) e).Exception == null)
      return;
    ((PXSelectBase) this.Base.plugInFilter).Cache.SetValueExt<PlugInFilter.showAllSettings>((object) ((PXSelectBase<PlugInFilter>) this.Base.plugInFilter).Current, (object) true);
  }

  public delegate Dictionary<string, string> GetPlugInSettingsDelegate();

  public delegate Dictionary<SelectorType?, string> GetPlugInSelectorTypesDelegate();

  public delegate IEnumerable<ACHPlugInParameter> ApplyFiltersDelegate(
    IEnumerable<ACHPlugInParameter> aCHPlugInParameters);
}
