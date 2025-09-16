// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.DeprecatedProcessingAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.CA;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

/// <summary>The attribute that informs a user that the processing center uses an unsupported plug-in.</summary>
public class DeprecatedProcessingAttribute : PXEventSubscriberAttribute, IPXRowSelectedSubscriber
{
  private bool errorRised;

  public DeprecatedProcessingAttribute.CheckVal ChckVal { get; set; }

  /// <summary>The DAC field name that is used to display the error message.</summary>
  public string ErrorMappedFieldName { get; set; }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    string fieldName = this.FieldName;
    string str1 = this.ErrorMappedFieldName ?? fieldName;
    object obj = sender.GetValue(e.Row, fieldName);
    if (this.ChckVal == DeprecatedProcessingAttribute.CheckVal.PmInstanceId)
    {
      int? pmInstanceID = obj as int?;
      int? nullable = pmInstanceID;
      int num = 0;
      CCPluginCheckResult result;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue && DeprecatedProcessingAttribute.IsProcessingCenterNotSupported(sender.Graph, pmInstanceID, out result))
      {
        CCProcessingCenter centerByPmInstance = DeprecatedProcessingAttribute.GetProcessingCenterByPMInstance(sender.Graph, pmInstanceID);
        string str2 = DeprecatedProcessingAttribute.InterpretPluginCheckResult(result);
        sender.RaiseExceptionHandling(str1, e.Row, (object) pmInstanceID, (Exception) new PXSetPropertyException(str2, (PXErrorLevel) 2, new object[1]
        {
          (object) centerByPmInstance?.ProcessingCenterID
        }));
        this.errorRised = true;
      }
      else if (this.errorRised)
      {
        sender.RaiseExceptionHandling(str1, e.Row, (object) null, (Exception) null);
        this.errorRised = false;
      }
    }
    if (this.ChckVal == DeprecatedProcessingAttribute.CheckVal.ProcessingCenterId)
    {
      CCPluginCheckResult result;
      if (obj is string procCenterId && DeprecatedProcessingAttribute.IsProcessingCenterNotSupported(sender.Graph, procCenterId, out result))
      {
        string str3 = DeprecatedProcessingAttribute.InterpretPluginCheckResult(result);
        sender.RaiseExceptionHandling(str1, e.Row, (object) procCenterId, (Exception) new PXSetPropertyException(str3, (PXErrorLevel) 2, new object[1]
        {
          (object) procCenterId
        }));
        this.errorRised = true;
      }
      else if (this.errorRised)
      {
        sender.RaiseExceptionHandling(str1, e.Row, (object) null, (Exception) null);
        this.errorRised = false;
      }
    }
    if (this.ChckVal != DeprecatedProcessingAttribute.CheckVal.ProcessingCenterType)
      return;
    CCPluginCheckResult result1;
    if (obj is string typeName && DeprecatedProcessingAttribute.IsProcessingCenterNotSupported(typeName, out result1))
    {
      string str4 = DeprecatedProcessingAttribute.InterpretPluginTypeCheckResult(result1);
      sender.RaiseExceptionHandling(str1, e.Row, (object) typeName, (Exception) new PXSetPropertyException(str4, (PXErrorLevel) 2, new object[1]
      {
        (object) string.Empty
      }));
      this.errorRised = true;
    }
    else
    {
      if (!this.errorRised)
        return;
      sender.RaiseExceptionHandling(str1, e.Row, (object) null, (Exception) null);
      this.errorRised = false;
    }
  }

  private static string InterpretPluginCheckResult(CCPluginCheckResult result)
  {
    switch (result)
    {
      case CCPluginCheckResult.Empty:
        return "Plug-in type is not selected for the {0} processing center.";
      case CCPluginCheckResult.Missing:
        return "The {0} processing center configured for the selected customer payment method references a missing plug-in.";
      case CCPluginCheckResult.Unsupported:
        return "The {0} processing center configured for the selected customer payment method uses an unsupported plug-in.";
      default:
        return string.Empty;
    }
  }

  private static string InterpretPluginTypeCheckResult(CCPluginCheckResult result)
  {
    switch (result)
    {
      case CCPluginCheckResult.Empty:
        return "Plug-in type is not selected for the {0} processing center.";
      case CCPluginCheckResult.Missing:
        return "The {0} processing center references a missing plug-in.";
      case CCPluginCheckResult.Unsupported:
        return "The {0} processing center uses an unsupported plug-in.";
      default:
        return string.Empty;
    }
  }

  private static bool IsProcessingCenterNotSupported(
    PXGraph graph,
    string procCenterId,
    out CCPluginCheckResult result)
  {
    result = CCPluginCheckResult.NotPerformed;
    if (procCenterId == null)
      return false;
    CCProcessingCenter processingCenter = PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select(graph, new object[1]
    {
      (object) procCenterId
    }));
    if (processingCenter == null)
      return false;
    result = CCPluginTypeHelper.TryGetPluginTypeWithCheck(processingCenter.ProcessingTypeName, out Type _);
    return result != 0;
  }

  private static bool IsProcessingCenterNotSupported(
    PXGraph graph,
    int? pmInstanceID,
    out CCPluginCheckResult result)
  {
    result = CCPluginCheckResult.NotPerformed;
    if (!pmInstanceID.HasValue)
      return false;
    CCProcessingCenter centerByPmInstance = DeprecatedProcessingAttribute.GetProcessingCenterByPMInstance(graph, pmInstanceID);
    if (centerByPmInstance == null)
      return false;
    result = CCPluginTypeHelper.TryGetPluginTypeWithCheck(centerByPmInstance.ProcessingTypeName, out Type _);
    return result != 0;
  }

  private static bool IsProcessingCenterNotSupported(
    string typeName,
    out CCPluginCheckResult result)
  {
    result = CCPluginCheckResult.NotPerformed;
    if (string.IsNullOrEmpty(typeName))
      return false;
    result = CCPluginTypeHelper.TryGetPluginTypeWithCheck(typeName, out Type _);
    return result != 0;
  }

  private static CCProcessingCenter GetProcessingCenterByPMInstance(
    PXGraph graph,
    int? pmInstanceID)
  {
    return PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelectJoin<CCProcessingCenter, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenter.processingCenterID>>>, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select(graph, new object[1]
    {
      (object) pmInstanceID
    }));
  }

  /// <summary>Defines the types of the values of the field that is annotated with the attribute.</summary>
  public enum CheckVal
  {
    PmInstanceId,
    ProcessingCenterId,
    ProcessingCenterType,
  }
}
