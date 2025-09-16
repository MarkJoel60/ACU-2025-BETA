// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMSubAccountMaskBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Attribute for Subaccount field. Aggregates PXFieldAttribute, PXUIFieldAttribute and DimensionSelector without any restriction.
/// </summary>
[PXDBString(30, IsUnicode = true, InputMask = "")]
[PXUIField]
public abstract class PMSubAccountMaskBaseAttribute : PXEntityAttribute
{
  private const string _DimensionName = "SUBACCOUNT";

  protected PMSubAccountMaskBaseAttribute(
    string maskName,
    PMAcctSubDefault.CustomListAttribute valueList,
    string defaultValue)
  {
    PXDimensionMaskAttribute dimensionMaskAttribute = new PXDimensionMaskAttribute("SUBACCOUNT", maskName, defaultValue, valueList.AllowedValues, valueList.AllowedLabels);
    dimensionMaskAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  protected static void VerifyCommonMaskProperties(
    PMAllocationDetail step,
    string mask,
    PMProject project,
    PMTask task)
  {
    if (string.IsNullOrWhiteSpace(mask))
      throw new PXException("Subaccount Mask is not set in allocation step. Please correct your allocation step. Allocation Rule:{0} Step:{1}", new object[2]
      {
        (object) step.AllocationID,
        (object) step.StepID
      });
    int? nullable;
    if (mask.Contains("J"))
    {
      nullable = project.DefaultSalesSubID;
      if (!nullable.HasValue)
        throw new PXException("The allocation rule is configured to use the sales subaccount from the project, but the sales subaccount is not specified for the {0} project.", new object[1]
        {
          (object) project.ContractCD
        });
    }
    if (mask.Contains("T"))
    {
      nullable = task.DefaultSalesSubID;
      if (!nullable.HasValue)
        throw new PXException("The allocation rule is configured to use the sales subaccount from the project task, but the sales subaccount is not specified for the {0} task of the {1} project.", new object[2]
        {
          (object) task.TaskCD,
          (object) project.ContractCD
        });
    }
    if (mask.Contains("C"))
    {
      nullable = project.DefaultExpenseSubID;
      if (!nullable.HasValue)
        throw new PXException("The allocation rule is configured to use the cost subaccount from the project, but the cost subaccount is not specified for the {0} project.", new object[1]
        {
          (object) project.ContractCD
        });
    }
    if (!mask.Contains("D"))
      return;
    nullable = task.DefaultExpenseSubID;
    if (!nullable.HasValue)
      throw new PXException("The allocation rule is configured to use the cost subaccount from the project task, but the cost subaccount is not specified for the {0} task of the {1} project.", new object[2]
      {
        (object) task.TaskCD,
        (object) project.ContractCD
      });
  }

  protected static string MakeSub<Field>(
    PXGraph graph,
    string mask,
    object[] sources,
    Type[] fields)
    where Field : IBqlField
  {
    return PMSubAccountMaskBaseAttribute.MakeSub<Field, PMAcctSubDefault.SubListAttribute>(graph, mask, sources, fields);
  }

  protected static string MakeSub<Field, TValueList>(
    PXGraph graph,
    string mask,
    object[] sources,
    Type[] fields)
    where Field : IBqlField
    where TValueList : PMAcctSubDefault.CustomListAttribute, new()
  {
    TValueList valueList = new TValueList();
    try
    {
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, valueList.AllowedValues, 0, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      PXCache cach = graph.Caches[BqlCommand.GetItemType(fields[ex.SourceIdx])];
      string name = fields[ex.SourceIdx].Name;
      throw new PXMaskArgumentException(new object[2]
      {
        (object) valueList.AllowedLabels[ex.SourceIdx],
        (object) PXUIFieldAttribute.GetDisplayName(cach, name)
      });
    }
  }
}
