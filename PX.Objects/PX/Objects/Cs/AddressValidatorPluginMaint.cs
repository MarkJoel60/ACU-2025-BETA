// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.AddressValidatorPluginMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.AddressValidator;
using PX.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CS;

public class AddressValidatorPluginMaint : 
  PXGraph<AddressValidatorPluginMaint, AddressValidatorPlugin>
{
  public PXSelect<AddressValidatorPlugin> Plugin;
  public PXSelectOrderBy<AddressValidatorPluginDetail, OrderBy<Asc<AddressValidatorPluginDetail.sortOrder>>> Details;
  public PXSelect<AddressValidatorPluginDetail, Where<AddressValidatorPluginDetail.addressValidatorPluginID, Equal<Current<AddressValidatorPlugin.addressValidatorPluginID>>>, OrderBy<Asc<AddressValidatorPluginDetail.sortOrder>>> SelectDetails;
  public PXAction<AddressValidatorPlugin> test;

  public AddressValidatorPluginMaint() => ((PXAction) this.CopyPaste).SetVisible(false);

  protected IEnumerable details()
  {
    this.ImportSettings();
    return (IEnumerable) ((PXSelectBase<AddressValidatorPluginDetail>) this.SelectDetails).Select(Array.Empty<object>());
  }

  public virtual void ImportSettings()
  {
    if (((PXSelectBase<AddressValidatorPlugin>) this.Plugin).Current == null)
      return;
    IAddressConnectedService addressValidator = AddressValidatorPluginMaint.CreateAddressValidator((PXGraph) this, ((PXSelectBase<AddressValidatorPlugin>) this.Plugin).Current);
    if (addressValidator == null)
      return;
    this.InsertDetails((IList<IAddressValidatorSetting>) addressValidator.DefaultSettings);
  }

  [PXUIField]
  [PXProcessButton]
  public virtual void Test()
  {
    if (((PXSelectBase<AddressValidatorPlugin>) this.Plugin).Current == null)
      return;
    ((PXAction) this.Save).Press();
    IAddressConnectedService addressValidator = AddressValidatorPluginMaint.CreateAddressValidator((PXGraph) this, ((PXSelectBase<AddressValidatorPlugin>) this.Plugin).Current);
    if (addressValidator == null)
      return;
    PingResult pingResult = addressValidator.Ping();
    if (((ResultBase) pingResult).IsSuccess)
    {
      ((PXSelectBase<AddressValidatorPlugin>) this.Plugin).Ask(((PXSelectBase<AddressValidatorPlugin>) this.Plugin).Current, "Connection", "The connection to the address provider was successful.", (MessageButtons) 0, (MessageIcon) 4);
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string message in ((ResultBase) pingResult).Messages)
        stringBuilder.AppendLine(message);
      if (stringBuilder.Length > 0)
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The test has failed. Details: {0}.", new object[1]
        {
          (object) stringBuilder.ToString().TrimEnd()
        }));
    }
  }

  private void InsertDetails(IList<IAddressValidatorSetting> list)
  {
    Dictionary<string, AddressValidatorPluginDetail> dictionary = new Dictionary<string, AddressValidatorPluginDetail>();
    foreach (PXResult<AddressValidatorPluginDetail> pxResult in ((PXSelectBase<AddressValidatorPluginDetail>) this.SelectDetails).Select(Array.Empty<object>()))
    {
      AddressValidatorPluginDetail validatorPluginDetail = PXResult<AddressValidatorPluginDetail>.op_Implicit(pxResult);
      dictionary.Add(validatorPluginDetail.SettingID.ToUpper(), validatorPluginDetail);
    }
    foreach (IAddressValidatorSetting validatorSetting in (IEnumerable<IAddressValidatorSetting>) list)
    {
      AddressValidatorPluginDetail validatorPluginDetail;
      if (dictionary.TryGetValue(validatorSetting.SettingID.ToUpper(), out validatorPluginDetail))
      {
        AddressValidatorPluginDetail copy = PXCache<AddressValidatorPluginDetail>.CreateCopy(validatorPluginDetail);
        if (!string.IsNullOrEmpty(validatorSetting.Description))
          copy.Description = validatorSetting.Description;
        copy.ControlType = validatorSetting.ControlType;
        copy.ComboValues = validatorSetting.ComboValues;
        copy.SortOrder = validatorSetting.SortOrder;
        if (!(validatorPluginDetail.Description != copy.Description))
        {
          int? nullable1 = validatorPluginDetail.ControlTypeValue;
          int? nullable2 = copy.ControlTypeValue;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && !(validatorPluginDetail.ComboValuesStr != copy.ComboValuesStr))
          {
            nullable2 = validatorPluginDetail.SortOrder;
            nullable1 = copy.SortOrder;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              continue;
          }
        }
        ((PXSelectBase<AddressValidatorPluginDetail>) this.SelectDetails).Update(copy);
      }
      else
        ((PXSelectBase<AddressValidatorPluginDetail>) this.SelectDetails).Insert(new AddressValidatorPluginDetail()
        {
          SettingID = validatorSetting.SettingID.ToUpper(),
          SortOrder = validatorSetting.SortOrder,
          Description = validatorSetting.Description,
          Value = validatorSetting.Value,
          ControlType = validatorSetting.ControlType,
          ComboValues = validatorSetting.ComboValues
        });
    }
  }

  public static IAddressConnectedService CreateAddressValidator(
    PXGraph graph,
    AddressValidatorPlugin plugin)
  {
    IAddressConnectedService addressValidator = (IAddressConnectedService) null;
    if (!string.IsNullOrEmpty(plugin.PluginTypeName))
    {
      try
      {
        addressValidator = (IAddressConnectedService) Activator.CreateInstance(PXBuildManager.GetType(plugin.PluginTypeName, true));
        PXResultset<AddressValidatorPluginDetail> pxResultset = ((PXSelectBase<AddressValidatorPluginDetail>) new PXSelect<AddressValidatorPluginDetail, Where<AddressValidatorPluginDetail.addressValidatorPluginID, Equal<Required<AddressValidatorPluginDetail.addressValidatorPluginID>>>>(graph)).Select(new object[1]
        {
          (object) plugin.AddressValidatorPluginID
        });
        List<IAddressValidatorSetting> validatorSettingList = new List<IAddressValidatorSetting>(pxResultset.Count);
        foreach (PXResult<AddressValidatorPluginDetail> pxResult in pxResultset)
        {
          AddressValidatorPluginDetail validatorPluginDetail = PXResult<AddressValidatorPluginDetail>.op_Implicit(pxResult);
          validatorSettingList.Add((IAddressValidatorSetting) validatorPluginDetail);
        }
        addressValidator.Initialize((IEnumerable<IAddressValidatorSetting>) validatorSettingList);
      }
      catch (Exception ex)
      {
        throw new PXException("Failed to create the address validator plug-in", new object[1]
        {
          (object) ex.Message
        });
      }
    }
    return addressValidator;
  }

  public static IReadOnlyList<string> GetAddressValidatorPluginAttributes(
    PXGraph graph,
    string addressValidatorPluginID)
  {
    AddressValidatorPlugin addressValidatorPlugin = PXResultset<AddressValidatorPlugin>.op_Implicit(PXSelectBase<AddressValidatorPlugin, PXSelect<AddressValidatorPlugin, Where<AddressValidatorPlugin.addressValidatorPluginID, Equal<Required<AddressValidatorPlugin.addressValidatorPluginID>>>>.Config>.Select(graph, new object[1]
    {
      (object) addressValidatorPluginID
    }));
    if (addressValidatorPlugin == null)
      throw new PXException("Failed to find the address validator plug-in", new object[1]
      {
        (object) addressValidatorPluginID
      });
    IAddressValidator iaddressValidator = (IAddressValidator) null;
    if (!string.IsNullOrEmpty(addressValidatorPlugin.PluginTypeName))
    {
      try
      {
        iaddressValidator = (IAddressValidator) Activator.CreateInstance(PXBuildManager.GetType(addressValidatorPlugin.PluginTypeName, true));
      }
      catch (Exception ex)
      {
        throw new PXException("Failed to create the address validator plug-in", new object[1]
        {
          (object) ex.Message
        });
      }
    }
    return iaddressValidator != null ? iaddressValidator.Attributes : (IReadOnlyList<string>) new List<string>().AsReadOnly();
  }

  public static IAddressValidator CreateAddressValidator(
    PXGraph graph,
    string addressValidatorPluginID)
  {
    return (IAddressValidator) AddressValidatorPluginMaint.CreateAddressValidator(graph, AddressValidatorPlugin.PK.Find(graph, addressValidatorPluginID) ?? throw new PXException("Failed to find the address validator plug-in", new object[1]
    {
      (object) addressValidatorPluginID
    }));
  }

  public static bool IsActive(PXGraph graph, string countryID)
  {
    Country country = Country.PK.Find(graph, countryID);
    return ((bool?) AddressValidatorPlugin.PK.Find(graph, country?.AddressValidatorPluginID)?.IsActive).GetValueOrDefault();
  }

  protected virtual void AddressValidatorPlugin_PluginTypeName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is AddressValidatorPlugin))
      return;
    foreach (PXResult<AddressValidatorPluginDetail> pxResult in ((PXSelectBase<AddressValidatorPluginDetail>) this.SelectDetails).Select(Array.Empty<object>()))
      ((PXSelectBase<AddressValidatorPluginDetail>) this.SelectDetails).Delete(PXResult<AddressValidatorPluginDetail>.op_Implicit(pxResult));
  }

  protected virtual bool IsCountryISOValid(string countryISOCode)
  {
    if (PXResultset<Country>.op_Implicit(PXSelectBase<Country, PXSelect<Country, Where<Country.countryID, Equal<Required<Country.countryID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) countryISOCode
    })) != null)
      return true;
    try
    {
      RegionInfo regionInfo = new RegionInfo(countryISOCode);
    }
    catch (ArgumentException ex)
    {
      throw new PXSetPropertyException("The {0} code is not a valid country code. Make sure that the country code complies with ISO 3166-1 alpha-2.", new object[1]
      {
        (object) countryISOCode
      });
    }
    return true;
  }

  /// <exclude />
  protected virtual void AddressValidatorPluginDetail_Value_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    AddressValidatorPluginDetail row = e.Row as AddressValidatorPluginDetail;
    string newValue = e.NewValue as string;
    if (row == null || string.IsNullOrEmpty(newValue))
      return;
    AddressValidatorPluginDetail copy = PXCache<AddressValidatorPluginDetail>.CreateCopy(row);
    copy.Value = newValue;
    ((IAddressConnectedService) Activator.CreateInstance(PXBuildManager.GetType(((PXSelectBase<AddressValidatorPlugin>) this.Plugin).Current.PluginTypeName, true))).Initialize((IEnumerable<IAddressValidatorSetting>) new List<AddressValidatorPluginDetail>()
    {
      copy
    });
    switch (row.ControlTypeValue.GetValueOrDefault())
    {
      case 5:
        using (List<string>.Enumerator enumerator = ((IEnumerable<string>) e.NewValue.ToString().Split(CountriesISOListHelper.DelimiterChars, StringSplitOptions.RemoveEmptyEntries)).ToList<string>().GetEnumerator())
        {
          while (enumerator.MoveNext())
            this.IsCountryISOValid(enumerator.Current);
          break;
        }
      case 6:
        this.IsCountryISOValid(e.NewValue.ToString());
        break;
    }
  }

  protected virtual void AddressValidatorPluginDetail_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is AddressValidatorPluginDetail row))
      return;
    string name = typeof (AddressValidatorPluginDetail.value).Name;
    int? controlTypeValue = row.ControlTypeValue;
    if (!controlTypeValue.HasValue)
      return;
    switch (controlTypeValue.GetValueOrDefault())
    {
      case 1:
        if (e.ReturnState == null || !(row.SettingID == "PATH") && !(row.SettingID == "URL"))
          break;
        e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(), new int?(-1), new int?(), new int?(), (object) null, name, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(false), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
        break;
      case 2:
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        foreach (KeyValuePair<string, string> comboValue in (IEnumerable<KeyValuePair<string, string>>) row.ComboValues)
        {
          stringList2.Add(comboValue.Key);
          stringList1.Add(comboValue.Value);
        }
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1024 /*0x0400*/), new bool?(), name, new bool?(false), new int?(1), (string) null, stringList2.ToArray(), stringList1.ToArray(), new bool?(true), (string) null, (string[]) null);
        break;
      case 3:
        e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (bool), new bool?(false), new bool?(), new int?(-1), new int?(), new int?(), (object) null, name, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
        break;
      case 4:
        if (e.ReturnState == null)
          break;
        string str = new string('*', e.ReturnState.ToString().Length);
        e.ReturnState = (object) PXFieldState.CreateInstance((object) str, typeof (string), new bool?(false), new bool?(), new int?(-1), new int?(), new int?(), (object) null, name, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
        break;
    }
  }

  protected virtual void AddressValidatorPlugin_RowDeleting(
    PXCache sender,
    PXRowDeletingEventArgs e)
  {
    if (!(e.Row is AddressValidatorPlugin))
      return;
    if (PXResultset<Country>.op_Implicit(PXSelectBase<Country, PXSelect<Country, Where<Country.addressValidatorPluginID, Equal<Current<AddressValidatorPlugin.addressValidatorPluginID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) != null)
      throw new PXException("Country cannot be deleted. One or more address validation providers are mapped to this country.");
    if (PXResultset<PreferencesGeneral>.op_Implicit(PXSelectBase<PreferencesGeneral, PXSelect<PreferencesGeneral, Where<PreferencesGeneral.addressLookupPluginID, Equal<Current<AddressValidatorPlugin.addressValidatorPluginID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) != null)
      throw new PXException("The address lookup plug-in cannot be deleted because it is selected on the Site Preferences (SM200505) form.");
    PXAddressLookup.PortalSetup portalSetup = PXResultset<PXAddressLookup.PortalSetup>.op_Implicit(PXSelectBase<PXAddressLookup.PortalSetup, PXSelect<PXAddressLookup.PortalSetup, Where<PXAddressLookup.PortalSetup.addressLookupPluginID, Equal<Current<AddressValidatorPlugin.addressValidatorPluginID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    PXAddressLookup.SPPortal spPortal = PXResultset<PXAddressLookup.SPPortal>.op_Implicit(PXSelectBase<PXAddressLookup.SPPortal, PXSelect<PXAddressLookup.SPPortal, Where<PXAddressLookup.SPPortal.addressLookupPluginID, Equal<Current<AddressValidatorPlugin.addressValidatorPluginID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (portalSetup != null || spPortal != null)
      throw new PXException("The address lookup plug-in cannot be deleted because it is selected on the Portal Preferences (SP800000) form.");
  }
}
