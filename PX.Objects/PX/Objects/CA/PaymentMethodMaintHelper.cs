// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodMaintHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.ACHPlugInBase;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CA;

public static class PaymentMethodMaintHelper
{
  public static bool IsACHPlugIn(this PaymentMethodMaint graph)
  {
    return ((PXSelectBase<PaymentMethod>) graph.PaymentMethod).Current?.APBatchExportPlugInTypeName == ACHPlugInTypeAttribute.USACHPlugInType;
  }

  public static IEnumerable<IACHPlugInParameter> GetParametersOfSelectedPlugIn(
    this PaymentMethodMaint graph)
  {
    string exportPlugInTypeName = ((PXSelectBase<PaymentMethod>) graph.PaymentMethod).Current?.APBatchExportPlugInTypeName;
    return string.IsNullOrEmpty(exportPlugInTypeName) ? (IEnumerable<IACHPlugInParameter>) Enumerable.Empty<ACHPlugInParameter>() : (Activator.CreateInstance(PXBuildManager.GetType(exportPlugInTypeName, true)) as IACHPlugIn).GetACHPlugInParameters();
  }

  public static HashSet<string> GetRemittenceDetailsID(this PaymentMethodMaint graph)
  {
    HashSet<string> remittenceDetailsId = new HashSet<string>();
    foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) graph.DetailsForCashAccount).Select(Array.Empty<object>()))
    {
      string str = PXResult<PaymentMethodDetail>.op_Implicit(pxResult).DetailID.Trim();
      remittenceDetailsId.Add(str);
    }
    return remittenceDetailsId;
  }

  public static HashSet<string> GetVendorDetailsID(
    this PaymentMethodMaint graph,
    bool selectRemittanceOnly = false)
  {
    HashSet<string> vendorDetailsId = new HashSet<string>();
    foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) graph.DetailsForVendor).Select(Array.Empty<object>()))
    {
      string str = PXResult<PaymentMethodDetail>.op_Implicit(pxResult).DetailID.Trim();
      vendorDetailsId.Add(str);
    }
    return vendorDetailsId;
  }

  public static void AppendDefaultPlugInParameters(
    this PaymentMethodMaint graph,
    Dictionary<int, string> mapping,
    bool useExportScenarioMapping = false)
  {
    HashSet<string> remittenceDetailsId = graph.GetRemittenceDetailsID();
    HashSet<string> vendorDetailsId = graph.GetVendorDetailsID();
    Dictionary<string, IACHPlugInParameter> dictionary = ((PXSelectBase<ACHPlugInParameter2>) graph.PlugInParameters).Select(Array.Empty<object>()).FirstTableItems.ToDictionary<ACHPlugInParameter2, string, IACHPlugInParameter>((Func<ACHPlugInParameter2, string>) (x => x.ParameterID.ToUpper()), (Func<ACHPlugInParameter2, IACHPlugInParameter>) (x => (IACHPlugInParameter) x));
    foreach (IACHPlugInParameter sourceParameter in graph.GetParametersOfSelectedPlugIn())
    {
      IACHPlugInParameter targetParameter;
      int num = dictionary.TryGetValue(sourceParameter.ParameterID.ToUpper(), out targetParameter) ? 1 : 0;
      string parameterValue = PaymentMethodMaintHelper.GetParameterValue(mapping, useExportScenarioMapping, remittenceDetailsId, vendorDetailsId, sourceParameter);
      ACHPlugInParameter orCopyParameter = PaymentMethodMaintHelper.CreateOrCopyParameter(sourceParameter, targetParameter, parameterValue);
      if (num != 0)
        ((PXSelectBase<ACHPlugInParameter>) graph.aCHPlugInParameters).Update(orCopyParameter);
      else
        ((PXSelectBase<ACHPlugInParameter>) graph.aCHPlugInParameters).Insert(orCopyParameter);
    }
  }

  private static ACHPlugInParameter CreateOrCopyParameter(
    IACHPlugInParameter sourceParameter,
    IACHPlugInParameter targetParameter,
    string parameterValue)
  {
    if (targetParameter == null)
      targetParameter = (IACHPlugInParameter) new ACHPlugInParameter();
    targetParameter.ParameterID = sourceParameter.ParameterID.ToUpper();
    targetParameter.ParameterCode = sourceParameter.ParameterCode;
    targetParameter.Description = sourceParameter.Description;
    targetParameter.Order = sourceParameter.Order;
    targetParameter.Required = sourceParameter.Required;
    targetParameter.Type = sourceParameter.Type;
    targetParameter.UsedIn = sourceParameter.UsedIn;
    targetParameter.Visible = sourceParameter.Visible;
    targetParameter.IsGroupHeader = sourceParameter.IsGroupHeader;
    targetParameter.IsAvailableInShortForm = sourceParameter.IsAvailableInShortForm;
    targetParameter.DataElementSize = sourceParameter.DataElementSize;
    targetParameter.IsFormula = sourceParameter.IsFormula;
    targetParameter.Value = parameterValue;
    return targetParameter as ACHPlugInParameter;
  }

  private static string GetParameterValue(
    Dictionary<int, string> mapping,
    bool useExportScenarioMapping,
    HashSet<string> rsIDs,
    HashSet<string> viIDs,
    IACHPlugInParameter param)
  {
    int? type = param.Type;
    int num = 0;
    HashSet<string> stringSet1 = type.GetValueOrDefault() == num & type.HasValue ? rsIDs : new HashSet<string>();
    int? nullable = param.Type;
    HashSet<string> stringSet2 = nullable.GetValueOrDefault() == 1 ? viIDs : stringSet1;
    nullable = param.DetailMapping;
    if (nullable.HasValue)
    {
      Dictionary<int, string> dictionary1 = mapping;
      nullable = param.DetailMapping;
      int key1 = nullable.Value;
      string parameterValue1;
      ref string local1 = ref parameterValue1;
      if (dictionary1.TryGetValue(key1, out local1) && stringSet2.Contains(parameterValue1))
      {
        if (!useExportScenarioMapping)
          return parameterValue1;
        nullable = param.ExportScenarioMapping;
        if (nullable.HasValue)
        {
          Dictionary<int, string> dictionary2 = mapping;
          nullable = param.ExportScenarioMapping;
          int key2 = nullable.Value;
          string parameterValue2;
          ref string local2 = ref parameterValue2;
          if (dictionary2.TryGetValue(key2, out local2) && stringSet2.Contains(parameterValue2))
            return parameterValue2;
        }
        return parameterValue1;
      }
    }
    return param.Value;
  }
}
