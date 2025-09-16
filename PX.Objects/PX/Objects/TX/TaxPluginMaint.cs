// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxPluginMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.TX;

public class TaxPluginMaint : PXGraph<TaxPluginMaint, TaxPlugin>
{
  public PXSelect<TaxPlugin> Plugin;
  public PXSelectOrderBy<TaxPluginDetail, OrderBy<Asc<TaxPluginDetail.sortOrder>>> Details;
  public PXSelect<TaxPluginDetail, Where<TaxPluginDetail.taxPluginID, Equal<Current<TaxPlugin.taxPluginID>>>, OrderBy<Asc<TaxPluginDetail.sortOrder>>> SelectDetails;
  public PXSelect<TaxPluginMapping, Where<TaxPluginMapping.taxPluginID, Equal<Current<TaxPlugin.taxPluginID>>>> Mapping;
  public PXAction<TaxPlugin> test;

  public TaxPluginMaint() => ((PXAction) this.CopyPaste).SetVisible(false);

  protected IEnumerable details()
  {
    this.ImportSettings();
    return (IEnumerable) ((PXSelectBase<TaxPluginDetail>) this.SelectDetails).Select(Array.Empty<object>());
  }

  public virtual void ImportSettings()
  {
    if (((PXSelectBase<TaxPlugin>) this.Plugin).Current == null)
      return;
    ITaxProvider taxProvider = TaxPluginMaint.CreateTaxProvider((PXGraph) this, ((PXSelectBase<TaxPlugin>) this.Plugin).Current);
    if (taxProvider == null)
      return;
    this.InsertDetails((IList<ITaxProviderSetting>) taxProvider.DefaultSettings);
  }

  [PXUIField]
  [PXProcessButton]
  public virtual void Test()
  {
    if (((PXSelectBase<TaxPlugin>) this.Plugin).Current == null)
      return;
    ((PXAction) this.Save).Press();
    ITaxProvider taxProvider = TaxPluginMaint.CreateTaxProvider((PXGraph) this, ((PXSelectBase<TaxPlugin>) this.Plugin).Current);
    if (taxProvider == null)
      return;
    PingResult pingResult = taxProvider.Ping();
    if (((ResultBase) pingResult).IsSuccess)
    {
      ((PXSelectBase<TaxPlugin>) this.Plugin).Ask(((PXSelectBase<TaxPlugin>) this.Plugin).Current, "Connection", "The connection to the tax provider was successful.", (MessageButtons) 0, (MessageIcon) 4);
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

  private void InsertDetails(IList<ITaxProviderSetting> list)
  {
    Dictionary<string, TaxPluginDetail> dictionary = new Dictionary<string, TaxPluginDetail>();
    foreach (PXResult<TaxPluginDetail> pxResult in ((PXSelectBase<TaxPluginDetail>) this.SelectDetails).Select(Array.Empty<object>()))
    {
      TaxPluginDetail taxPluginDetail = PXResult<TaxPluginDetail>.op_Implicit(pxResult);
      dictionary.Add(taxPluginDetail.SettingID.ToUpper(), taxPluginDetail);
    }
    foreach (ITaxProviderSetting itaxProviderSetting in (IEnumerable<ITaxProviderSetting>) list)
    {
      TaxPluginDetail taxPluginDetail;
      if (dictionary.TryGetValue(itaxProviderSetting.SettingID.ToUpper(), out taxPluginDetail))
      {
        TaxPluginDetail copy = PXCache<TaxPluginDetail>.CreateCopy(taxPluginDetail);
        if (!string.IsNullOrEmpty(itaxProviderSetting.Description))
          copy.Description = itaxProviderSetting.Description;
        copy.ControlType = itaxProviderSetting.ControlType;
        copy.ComboValues = itaxProviderSetting.ComboValues;
        copy.SortOrder = itaxProviderSetting.SortOrder;
        if (!(taxPluginDetail.Description != copy.Description))
        {
          int? nullable1 = taxPluginDetail.ControlTypeValue;
          int? nullable2 = copy.ControlTypeValue;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && !(taxPluginDetail.ComboValuesStr != copy.ComboValuesStr))
          {
            nullable2 = taxPluginDetail.SortOrder;
            nullable1 = copy.SortOrder;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              continue;
          }
        }
        ((PXSelectBase<TaxPluginDetail>) this.SelectDetails).Update(copy);
      }
      else
        ((PXSelectBase<TaxPluginDetail>) this.SelectDetails).Insert(new TaxPluginDetail()
        {
          SettingID = itaxProviderSetting.SettingID.ToUpper(),
          Description = itaxProviderSetting.Description,
          Value = itaxProviderSetting.Value,
          SortOrder = itaxProviderSetting.SortOrder,
          ControlType = itaxProviderSetting.ControlType,
          ComboValues = itaxProviderSetting.ComboValues
        });
    }
  }

  public static ITaxProvider CreateTaxProvider(PXGraph graph, TaxPlugin plugin)
  {
    ITaxProvider taxProvider = (ITaxProvider) null;
    if (!string.IsNullOrEmpty(plugin.PluginTypeName))
    {
      try
      {
        taxProvider = (ITaxProvider) Activator.CreateInstance(PXBuildManager.GetType(plugin.PluginTypeName, true));
        PXResultset<TaxPluginDetail> pxResultset = ((PXSelectBase<TaxPluginDetail>) new PXSelect<TaxPluginDetail, Where<TaxPluginDetail.taxPluginID, Equal<Required<TaxPluginDetail.taxPluginID>>>>(graph)).Select(new object[1]
        {
          (object) plugin.TaxPluginID
        });
        List<ITaxProviderSetting> itaxProviderSettingList = new List<ITaxProviderSetting>(pxResultset.Count);
        foreach (PXResult<TaxPluginDetail> pxResult in pxResultset)
        {
          TaxPluginDetail taxPluginDetail = PXResult<TaxPluginDetail>.op_Implicit(pxResult);
          itaxProviderSettingList.Add((ITaxProviderSetting) taxPluginDetail);
        }
        taxProvider.Initialize((IEnumerable<ITaxProviderSetting>) itaxProviderSettingList);
      }
      catch (Exception ex)
      {
        throw new PXException("Failed to create the tax plug-in. Please check that Plug-In(Type) is valid Type Name {0}.", new object[1]
        {
          (object) ex.Message
        });
      }
    }
    return taxProvider;
  }

  public static IReadOnlyList<string> GetTaxPluginAttributes(PXGraph graph, string taxPluginID)
  {
    TaxPlugin taxPlugin = PXResultset<TaxPlugin>.op_Implicit(PXSelectBase<TaxPlugin, PXSelect<TaxPlugin, Where<TaxPlugin.taxPluginID, Equal<Required<TaxPlugin.taxPluginID>>>>.Config>.Select(graph, new object[1]
    {
      (object) taxPluginID
    }));
    if (taxPlugin == null)
      throw new PXException("Failed to find the tax plug-in with the specified ID {0}.", new object[1]
      {
        (object) taxPluginID
      });
    ITaxProvider itaxProvider = (ITaxProvider) null;
    if (!string.IsNullOrEmpty(taxPlugin.PluginTypeName))
    {
      try
      {
        itaxProvider = (ITaxProvider) Activator.CreateInstance(PXBuildManager.GetType(taxPlugin.PluginTypeName, true));
      }
      catch (Exception ex)
      {
        throw new PXException("Failed to create the tax plug-in. Please check that Plug-In(Type) is valid Type Name {0}.", new object[1]
        {
          (object) ex.Message
        });
      }
    }
    return itaxProvider != null ? itaxProvider.Attributes : (IReadOnlyList<string>) new List<string>().AsReadOnly();
  }

  public static ITaxProvider CreateTaxProvider(PXGraph graph, string taxPluginID)
  {
    return TaxPluginMaint.CreateTaxProvider(graph, PXResultset<TaxPlugin>.op_Implicit(PXSelectBase<TaxPlugin, PXSelect<TaxPlugin, Where<TaxPlugin.taxPluginID, Equal<Required<TaxPlugin.taxPluginID>>>>.Config>.Select(graph, new object[1]
    {
      (object) taxPluginID
    })) ?? throw new PXException("Failed to find the tax plug-in with the specified ID {0}.", new object[1]
    {
      (object) taxPluginID
    }));
  }

  public static IExemptionCertificateProvider CreateECMProvider(PXGraph graph, string taxPluginID)
  {
    ITaxProvider taxProvider = TaxPluginMaint.CreateTaxProvider(graph, taxPluginID);
    if (taxProvider != null)
    {
      TaxPlugin taxPlugin = PXResultset<TaxPlugin>.op_Implicit(PXSelectBase<TaxPlugin, PXSelect<TaxPlugin, Where<TaxPlugin.taxPluginID, Equal<Required<TaxPlugin.taxPluginID>>>>.Config>.Select(graph, new object[1]
      {
        (object) taxPluginID
      }));
      if (PXSelectBase<TaxPluginMapping, PXViewOf<TaxPluginMapping>.BasedOn<SelectFromBase<TaxPluginMapping, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<TaxPlugin>.On<BqlOperand<TaxPluginMapping.taxPluginID, IBqlString>.IsEqual<TaxPlugin.taxPluginID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxPluginMapping.taxPluginID, Equal<P.AsString>>>>>.And<BqlOperand<TaxPluginMapping.externalCompanyID, IBqlString>.IsNull>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
      {
        (object) taxPluginID
      }) != null)
      {
        GetCompaniesResult companies1 = ((IExemptionCertificateProvider) taxProvider).GetCompanies();
        if (((ResultBase) companies1).IsSuccess)
        {
          TaxPluginMaint instance = PXGraph.CreateInstance<TaxPluginMaint>();
          ((PXSelectBase<TaxPlugin>) instance.Plugin).Current = taxPlugin;
          PXView view = ((PXSelectBase) instance.Mapping).View;
          object[] objArray1 = new object[1]
          {
            (object) graph
          };
          object[] objArray2 = Array.Empty<object>();
          foreach (TaxPluginMapping taxPluginMapping in view.SelectMultiBound(objArray1, objArray2))
          {
            TaxPluginMapping res = taxPluginMapping;
            ECMCompanyDetail ecmCompanyDetail1;
            if (companies1 == null)
            {
              ecmCompanyDetail1 = (ECMCompanyDetail) null;
            }
            else
            {
              ECMCompanyDetail[] companies2 = companies1.Companies;
              ecmCompanyDetail1 = companies2 != null ? ((IEnumerable<ECMCompanyDetail>) companies2).FirstOrDefault<ECMCompanyDetail>((Func<ECMCompanyDetail, bool>) (comp => comp.CompanyCode.Trim() == res.CompanyCode.Trim())) : (ECMCompanyDetail) null;
            }
            ECMCompanyDetail ecmCompanyDetail2 = ecmCompanyDetail1;
            res.ExternalCompanyID = ecmCompanyDetail2?.CompanyID;
            ((PXSelectBase<TaxPluginMapping>) instance.Mapping).Update(res);
          }
          ((PXAction) instance.Save).Press();
        }
        else
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (string message in ((ResultBase) companies1).Messages)
            stringBuilder.AppendLine(message);
          throw new PXException(stringBuilder.ToString());
        }
      }
    }
    return (IExemptionCertificateProvider) taxProvider;
  }

  public static bool IsActive(PXGraph graph, string taxZoneID)
  {
    return ((bool?) PXResultset<TaxPlugin>.op_Implicit(PXSelectBase<TaxPlugin, PXSelectJoin<TaxPlugin, InnerJoin<TaxZone, On<TaxPlugin.taxPluginID, Equal<TaxZone.taxPluginID>>>, Where<TaxZone.taxZoneID, Equal<Required<TaxZone.taxZoneID>>>>.Config>.Select(graph, new object[1]
    {
      (object) taxZoneID
    }))?.IsActive).GetValueOrDefault();
  }

  protected virtual void TaxPlugin_PluginTypeName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TaxPlugin))
      return;
    foreach (PXResult<TaxPluginDetail> pxResult in ((PXSelectBase<TaxPluginDetail>) this.SelectDetails).Select(Array.Empty<object>()))
      ((PXSelectBase<TaxPluginDetail>) this.SelectDetails).Delete(PXResult<TaxPluginDetail>.op_Implicit(pxResult));
  }

  protected virtual void TaxPluginDetail_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is TaxPluginDetail row))
      return;
    string name = typeof (TaxPluginDetail.value).Name;
    int? controlTypeValue = row.ControlTypeValue;
    if (!controlTypeValue.HasValue)
      return;
    switch (controlTypeValue.GetValueOrDefault())
    {
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

  protected virtual void TaxPlugin_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (e.Row is TaxPlugin && PXResultset<TaxZone>.op_Implicit(PXSelectBase<TaxZone, PXSelect<TaxZone, Where<TaxZone.taxZoneID, Equal<Current<TaxPlugin.taxPluginID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) != null)
      throw new PXException("Tax plug-in cannot be deleted because one or more tax zones depends on this tax plug-in.");
  }

  protected virtual void _(Events.FieldVerifying<TaxPluginDetail.value> e)
  {
    TaxPluginDetail row = e.Row as TaxPluginDetail;
    string newValue = ((Events.FieldVerifyingBase<Events.FieldVerifying<TaxPluginDetail.value>, object, object>) e).NewValue as string;
    if (row == null || string.IsNullOrEmpty(newValue))
      return;
    TaxPluginDetail copy = PXCache<TaxPluginDetail>.CreateCopy(row);
    copy.Value = newValue;
    ((ITaxProvider) Activator.CreateInstance(PXBuildManager.GetType(((PXSelectBase<TaxPlugin>) this.Plugin).Current.PluginTypeName, true))).Initialize((IEnumerable<ITaxProviderSetting>) new List<TaxPluginDetail>()
    {
      copy
    });
  }

  protected virtual void _(Events.RowUpdated<TaxPluginMapping> e)
  {
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TaxPluginMapping>>) e).Cache.ObjectsEqual<TaxPluginMapping.companyCode>((object) e.Row, (object) e.OldRow))
      return;
    e.Row.ExternalCompanyID = (string) null;
  }
}
