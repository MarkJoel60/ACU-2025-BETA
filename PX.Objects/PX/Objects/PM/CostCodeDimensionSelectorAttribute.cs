// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CostCodeDimensionSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

[PXRestrictor(typeof (Where<PMCostCode.isActive, Equal<True>>), "The {0} cost code is inactive.", new Type[] {typeof (PMCostCode.costCodeCD)})]
public class CostCodeDimensionSelectorAttribute : PXDimensionSelectorAttribute
{
  protected readonly bool _disableProjectSpecific;
  protected readonly Type _taskID;
  protected readonly Type _accountID;
  protected readonly Type _accountGroupID;
  protected PXGraph _graph;
  protected Type _cacheType;

  public CostCodeDimensionSelectorAttribute(Type searchType)
    : base("COSTCODE", typeof (Search<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))
  {
    this.DescriptionField = typeof (PMCostCode.description);
    this._disableProjectSpecific = true;
    ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1] = (PXEventSubscriberAttribute) new PXSelectorAttribute(searchType)
    {
      SubstituteKey = typeof (PMCostCode.costCodeCD),
      DescriptionField = typeof (PMCostCode.description)
    };
  }

  public CostCodeDimensionSelectorAttribute(
    Type account,
    Type task,
    string budgetType,
    Type accountGroup,
    bool disableProjectSpecific)
    : base("COSTCODE", typeof (Search<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))
  {
    this._disableProjectSpecific = disableProjectSpecific;
    this._taskID = task;
    this._accountID = account;
    this._accountGroupID = accountGroup;
    this.DescriptionField = typeof (PMCostCode.description);
    if (disableProjectSpecific)
      ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1] = (PXEventSubscriberAttribute) new PXSelectorAttribute(typeof (PMCostCode.costCodeID))
      {
        SubstituteKey = typeof (PMCostCode.costCodeCD),
        DescriptionField = typeof (PMCostCode.description)
      };
    else
      ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1] = (PXEventSubscriberAttribute) new CostCodeSelectorAttribute()
      {
        TaskID = task,
        AccountID = account,
        BudgetType = budgetType,
        AccountGroup = accountGroup,
        DisableProjectSpecific = disableProjectSpecific
      };
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    foreach (PXRestrictorAttribute restrictorAttribute in ((IEnumerable) ((PXAggregateAttribute) this)._Attributes).OfType<PXRestrictorAttribute>())
      restrictorAttribute.FieldVerifying(sender, e);
    base.FieldVerifying(sender, e);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    this._cacheType = sender.GetItemType();
    this._graph = sender.Graph;
    base.CacheAttached(sender);
  }
}
