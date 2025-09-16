// Decompiled with JetBrains decompiler
// Type: PX.Metadata.BaseScreenInfoCollector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Automation;
using PX.Data.Description;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Metadata;

public abstract class BaseScreenInfoCollector
{
  private readonly IWorkflowService _workflowService;

  protected BaseScreenInfoCollector(IWorkflowService workflowService)
  {
    this._workflowService = workflowService ?? throw new ArgumentNullException(nameof (workflowService));
  }

  protected void AddWorkflowContainer(
    string screenId,
    PXSiteMap.ScreenInfo screenInfo,
    PXGraph dataGraph)
  {
    PXFieldState[] screenFields = this._workflowService.GetScreenFields(screenId, dataGraph);
    if (screenFields != null && screenFields.Length > 0)
    {
      PXViewDescription pxViewDescription1 = new PXViewDescription("Transition Parameters", "FilterPreview", ((IEnumerable<PXFieldState>) screenFields).Select<PXFieldState, PX.Data.Description.FieldInfo>((Func<PXFieldState, PX.Data.Description.FieldInfo>) (wField =>
      {
        string name = wField.Name;
        string displayName = wField.DisplayName;
        int num1 = wField.PrimaryKey ? 1 : 0;
        System.Type dataType = wField.DataType;
        int num2 = wField.Enabled ? 1 : 0;
        bool? required = wField.Required;
        bool flag = true;
        int num3 = required.GetValueOrDefault() == flag & required.HasValue ? 1 : 0;
        int num4 = wField is PXStringState pxStringState4 ? (pxStringState4.IsUnicode ? 1 : 0) : 0;
        int num5 = !string.IsNullOrWhiteSpace(wField.ViewName) ? 1 : 0;
        string inputMask = wField is PXStringState pxStringState5 ? pxStringState5.InputMask : (string) null;
        string displayMask = wField is PXDateState pxDateState2 ? pxDateState2.DisplayMask : (string) null;
        object minValue = BaseScreenInfoCollector.GetMinValue((object) wField);
        object maxValue = BaseScreenInfoCollector.GetMaxValue((object) wField);
        int length = wField.Length;
        int precision = wField.Precision;
        string[] allowedLabels = (wField is PXStringState pxStringState6 ? pxStringState6.AllowedLabels : (string[]) null) ?? (wField is PXIntState pxIntState2 ? pxIntState2.AllowedLabels : (string[]) null);
        PX.Data.Description.FieldInfo fieldInfo1 = new PX.Data.Description.FieldInfo(name, displayName, (CallbackDescr) null, num1 != 0, dataType, num2 != 0, num3 != 0, num4 != 0, num5 != 0, (string) null, inputMask, displayMask, minValue, maxValue, length, precision, allowedLabels, false);
        if (!string.IsNullOrEmpty(wField.ViewName) && !string.IsNullOrEmpty(screenInfo.PrimaryView))
        {
          foreach (PXViewDescription pxViewDescription2 in screenInfo.Containers.Values)
          {
            PX.Data.Description.FieldInfo[] allFields = pxViewDescription2.AllFields;
            PX.Data.Description.FieldInfo fieldInfo2 = allFields != null ? ((IEnumerable<PX.Data.Description.FieldInfo>) allFields).FirstOrDefault<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (it => it.SelectorViewDescription?.ViewName == wField.ViewName)) : (PX.Data.Description.FieldInfo) null;
            if (fieldInfo2 != null)
            {
              fieldInfo1.SelectorViewDescription = fieldInfo2.SelectorViewDescription;
              break;
            }
          }
        }
        return fieldInfo1;
      })).ToArray<PX.Data.Description.FieldInfo>(), (ParsInfo[]) null, false);
      screenInfo.Containers["FilterPreview"] = pxViewDescription1;
    }
    if (!this._workflowService.IsWorkflowDefinitionDefined(dataGraph))
      return;
    screenInfo.HasWorkflow = true;
  }

  protected static object GetMinValue(object state)
  {
    object minValue;
    switch (state)
    {
      case PXDateState pxDateState:
        minValue = pxDateState.MinValue != System.DateTime.MinValue ? (object) pxDateState.MinValue : (object) null;
        break;
      case PXIntState pxIntState:
        minValue = pxIntState.MinValue != int.MinValue ? (object) pxIntState.MinValue : (object) null;
        break;
      case PXLongState pxLongState:
        minValue = pxLongState.MinValue != long.MinValue ? (object) pxLongState.MinValue : (object) null;
        break;
      case PXDoubleState pxDoubleState:
        minValue = pxDoubleState.MinValue != double.MinValue ? (object) pxDoubleState.MinValue : (object) null;
        break;
      case PXFloatState pxFloatState:
        minValue = (double) pxFloatState.MinValue != -3.4028234663852886E+38 ? (object) pxFloatState.MinValue : (object) null;
        break;
      case PXDecimalState pxDecimalState:
        minValue = pxDecimalState.MinValue != Decimal.MinValue ? (object) pxDecimalState.MinValue : (object) null;
        break;
      default:
        minValue = (object) null;
        break;
    }
    return minValue;
  }

  protected static object GetMaxValue(object state)
  {
    object maxValue;
    switch (state)
    {
      case PXDateState pxDateState:
        maxValue = pxDateState.MaxValue != System.DateTime.MaxValue ? (object) pxDateState.MaxValue : (object) null;
        break;
      case PXIntState pxIntState:
        maxValue = pxIntState.MaxValue != int.MaxValue ? (object) pxIntState.MaxValue : (object) null;
        break;
      case PXLongState pxLongState:
        maxValue = pxLongState.MaxValue != long.MaxValue ? (object) pxLongState.MaxValue : (object) null;
        break;
      case PXDoubleState pxDoubleState:
        maxValue = pxDoubleState.MaxValue != double.MaxValue ? (object) pxDoubleState.MaxValue : (object) null;
        break;
      case PXFloatState pxFloatState:
        maxValue = (double) pxFloatState.MaxValue != 3.4028234663852886E+38 ? (object) pxFloatState.MaxValue : (object) null;
        break;
      case PXDecimalState pxDecimalState:
        maxValue = pxDecimalState.MaxValue != Decimal.MaxValue ? (object) pxDecimalState.MaxValue : (object) null;
        break;
      default:
        maxValue = (object) null;
        break;
    }
    return maxValue;
  }

  protected static void PopulateFields(
    PXGraph graph,
    PXSiteMap.ScreenInfo info,
    string viewName,
    List<string> fields)
  {
    string[] strArray1;
    if (info.Views.TryGetValue(viewName, out strArray1))
    {
      foreach (string str in strArray1)
      {
        if (!fields.Contains(str))
          fields.Add(str);
      }
    }
    info.Views[viewName] = fields.ToArray();
    bool flag = true;
    foreach (System.Type table in graph.Views[viewName].BqlSelect.GetTables())
    {
      List<string> stringList = new List<string>();
      string[] strArray2;
      if (info.Caches.TryGetValue(table.FullName, out strArray2))
      {
        foreach (string str in strArray2)
          stringList.Add(str);
      }
      foreach (string field in fields)
      {
        if (flag)
        {
          if (!field.Contains("__") && !stringList.Contains(field))
            stringList.Add(field);
        }
        else if (field.Contains(table.Name + "__"))
        {
          string str = field.Substring(table.Name.Length + 2);
          if (!stringList.Contains(str))
            stringList.Add(str);
        }
      }
      info.Caches[table.FullName] = stringList.ToArray();
      flag = false;
    }
  }

  protected static bool IsKeyField(string dataField, string[] dataKeyNames)
  {
    if (dataKeyNames == null)
      return false;
    foreach (string dataKeyName in dataKeyNames)
    {
      if (string.Equals(dataField, dataKeyName, StringComparison.OrdinalIgnoreCase))
        return true;
    }
    return false;
  }
}
