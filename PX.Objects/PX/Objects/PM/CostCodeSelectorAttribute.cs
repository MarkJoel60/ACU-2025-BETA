// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CostCodeSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.PM;

public class CostCodeSelectorAttribute : PXCustomSelectorAttribute
{
  public string BudgetType { get; set; }

  public Type TaskID { get; set; }

  public Type AccountID { get; set; }

  public Type AccountGroup { get; set; }

  public bool DisableProjectSpecific { get; set; }

  public CostCodeSelectorAttribute()
    : base(typeof (PMCostCode.costCodeID))
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (PMCostCode.costCodeCD);
    ((PXSelectorAttribute) this).DescriptionField = typeof (PMCostCode.description);
    ((PXSelectorAttribute) this).Filterable = true;
    ((PXSelectorAttribute) this).FilterEntity = typeof (PMCostCode);
  }

  protected virtual IEnumerable GetRecords()
  {
    PXResultset<PMCostCode> pxResultset = ((PXSelectBase<PMCostCode>) new PXSelect<PMCostCode>(this._Graph)).Select(Array.Empty<object>());
    Dictionary<int, PMCostCode> dictionary1 = new Dictionary<int, PMCostCode>(pxResultset.Count);
    foreach (PXResult<PMCostCode> pxResult in pxResultset)
    {
      PMCostCode pmCostCode1 = PXResult<PMCostCode>.op_Implicit(pxResult);
      pmCostCode1.IsProjectOverride = new bool?(false);
      Dictionary<int, PMCostCode> dictionary2 = dictionary1;
      int? costCodeId = pmCostCode1.CostCodeID;
      int key1 = costCodeId.Value;
      if (!dictionary2.ContainsKey(key1) && pmCostCode1.IsActive.GetValueOrDefault())
      {
        Dictionary<int, PMCostCode> dictionary3 = dictionary1;
        costCodeId = pmCostCode1.CostCodeID;
        int key2 = costCodeId.Value;
        PMCostCode pmCostCode2 = pmCostCode1;
        dictionary3.Add(key2, pmCostCode2);
      }
    }
    if (!this.DisableProjectSpecific)
    {
      foreach (PMBudgetedCostCode budgetedCostCode in this.GetProjectSpecificRecords().Values)
      {
        Dictionary<int, PMCostCode> dictionary4 = dictionary1;
        int? costCodeId = budgetedCostCode.CostCodeID;
        int key3 = costCodeId.Value;
        PMCostCode pmCostCode3;
        ref PMCostCode local = ref pmCostCode3;
        if (dictionary4.TryGetValue(key3, out local))
        {
          PMCostCode pmCostCode4 = new PMCostCode()
          {
            CostCodeID = pmCostCode3.CostCodeID,
            CostCodeCD = pmCostCode3.CostCodeCD,
            NoteID = pmCostCode3.NoteID,
            IsDefault = new bool?(false),
            Description = budgetedCostCode.Description,
            IsProjectOverride = new bool?(true)
          };
          Dictionary<int, PMCostCode> dictionary5 = dictionary1;
          costCodeId = budgetedCostCode.CostCodeID;
          int key4 = costCodeId.Value;
          PMCostCode pmCostCode5 = pmCostCode4;
          dictionary5[key4] = pmCostCode5;
        }
      }
    }
    return (IEnumerable) dictionary1.Values;
  }

  public virtual void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    PMCostCode pmCostCode = (PMCostCode) null;
    if (e.NewValue == null)
      return;
    if (e.NewValue.GetType() == typeof (string))
    {
      pmCostCode = PMCostCode.UK.Find(sender.Graph, (string) e.NewValue);
      if (pmCostCode != null)
      {
        e.NewValue = (object) pmCostCode.CostCodeID;
        ((CancelEventArgs) e).Cancel = true;
      }
    }
    else if (e.NewValue.GetType() == typeof (int))
      pmCostCode = PMCostCode.PK.Find(sender.Graph, (int?) e.NewValue);
    if (pmCostCode == null)
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("{0} '{1}' cannot be found in the system.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName(sender, ((PXEventSubscriberAttribute) this)._FieldName),
        (object) CostCodeSelectorAttribute.FormatValueByMask(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (string) e.NewValue)
      }));
  }

  public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object returnValue = e.ReturnValue;
    e.ReturnValue = (object) null;
    ((PXSelectorAttribute) this).FieldSelecting(sender, e);
    PMCostCode pmCostCode = PMCostCode.PK.Find(sender.Graph, (int?) returnValue);
    if (pmCostCode != null)
    {
      e.ReturnValue = (object) pmCostCode.CostCodeCD;
    }
    else
    {
      if (e.Row == null)
        return;
      e.ReturnValue = (object) null;
    }
  }

  protected virtual int? GetIDByType(Type fieldType)
  {
    if (fieldType == (Type) null)
      return new int?();
    PXCache cach = this._Graph.Caches[((PXSelectorAttribute) this)._CacheType];
    object[] currents = PXView.Currents;
    object obj = (currents != null ? (currents.Length != 0 ? 1 : 0) : 0) != 0 ? PXView.Currents[0] : cach.Current;
    return (int?) cach.GetValue(obj, fieldType.Name);
  }

  protected virtual int? GetTaskID() => this.GetIDByType(this.TaskID);

  protected virtual int? GetAccountGroupID() => this.GetIDByType(this.AccountGroup);

  protected virtual int? GetAccountID() => this.GetIDByType(this.AccountID);

  public static PXResultset<PMBudgetedCostCode> GetProjectSpecificRecords(
    PXGraph graph,
    int? taskID,
    int? accountGroupID,
    string budgetType,
    int? costCodeID)
  {
    return CostCodeSelectorAttribute.GetProjectSpecificRecords(graph, taskID, new int?(), accountGroupID, budgetType, costCodeID);
  }

  public static PXResultset<PMBudgetedCostCode> GetProjectSpecificRecords(
    PXGraph graph,
    int? taskID,
    int? accountID,
    int? accountGroupID,
    string budgetType)
  {
    return CostCodeSelectorAttribute.GetProjectSpecificRecords(graph, taskID, accountID, accountGroupID, budgetType, new int?());
  }

  public static PXResultset<PMBudgetedCostCode> GetProjectSpecificRecords(
    PXGraph graph,
    int? taskID,
    int? accountID,
    int? accountGroupID,
    string budgetType,
    int? costCodeID)
  {
    if (!taskID.HasValue)
      return new PXResultset<PMBudgetedCostCode>();
    return accountID.HasValue ? ((PXSelectBase<PMBudgetedCostCode>) new PXSelectJoin<PMBudgetedCostCode, InnerJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<PMBudgetedCostCode.accountGroupID>>, InnerJoin<Account, On<Account.accountGroupID, Equal<PMAccountGroup.groupID>>>>, Where<PMBudgetedCostCode.projectTaskID, Equal<Required<PMBudgetedCostCode.projectTaskID>>, And<Account.accountID, Equal<Required<Account.accountID>>, And<Where<Required<PMBudgetedCostCode.costCodeID>, IsNull, Or<PMBudgetedCostCode.costCodeID, Equal<Required<PMBudgetedCostCode.costCodeID>>>>>>>>(graph)).Select(new object[4]
    {
      (object) taskID,
      (object) accountID,
      (object) costCodeID,
      (object) costCodeID
    }) : (accountGroupID.HasValue ? ((PXSelectBase<PMBudgetedCostCode>) new PXSelect<PMBudgetedCostCode, Where<PMBudgetedCostCode.projectTaskID, Equal<Required<PMBudgetedCostCode.projectTaskID>>, And<PMBudgetedCostCode.accountGroupID, Equal<Required<PMBudgetedCostCode.accountGroupID>>, And<Where<Required<PMBudgetedCostCode.costCodeID>, IsNull, Or<PMBudgetedCostCode.costCodeID, Equal<Required<PMBudgetedCostCode.costCodeID>>>>>>>>(graph)).Select(new object[4]
    {
      (object) taskID,
      (object) accountGroupID,
      (object) costCodeID,
      (object) costCodeID
    }) : (!string.IsNullOrEmpty(budgetType) ? ((PXSelectBase<PMBudgetedCostCode>) new PXSelect<PMBudgetedCostCode, Where<PMBudgetedCostCode.projectTaskID, Equal<Required<PMBudgetedCostCode.projectTaskID>>, And<PMBudgetedCostCode.type, Equal<Required<PMBudgetedCostCode.type>>, And<Where<Required<PMBudgetedCostCode.costCodeID>, IsNull, Or<PMBudgetedCostCode.costCodeID, Equal<Required<PMBudgetedCostCode.costCodeID>>>>>>>>(graph)).Select(new object[4]
    {
      (object) taskID,
      (object) budgetType,
      (object) costCodeID,
      (object) costCodeID
    }) : ((PXSelectBase<PMBudgetedCostCode>) new PXSelect<PMBudgetedCostCode, Where<PMBudgetedCostCode.projectTaskID, Equal<Required<PMBudgetedCostCode.projectTaskID>>, And<Where<Required<PMBudgetedCostCode.costCodeID>, IsNull, Or<PMBudgetedCostCode.costCodeID, Equal<Required<PMBudgetedCostCode.costCodeID>>>>>>>(graph)).Select(new object[3]
    {
      (object) taskID,
      (object) costCodeID,
      (object) costCodeID
    })));
  }

  protected virtual Dictionary<int?, PMBudgetedCostCode> GetProjectSpecificRecords()
  {
    PXResultset<PMBudgetedCostCode> projectSpecificRecords1 = CostCodeSelectorAttribute.GetProjectSpecificRecords(this._Graph, this.GetTaskID(), this.GetAccountID(), this.GetAccountGroupID(), this.BudgetType);
    Dictionary<int?, PMBudgetedCostCode> projectSpecificRecords2 = new Dictionary<int?, PMBudgetedCostCode>(projectSpecificRecords1.Count);
    foreach (PXResult<PMBudgetedCostCode> pxResult in projectSpecificRecords1)
    {
      PMBudgetedCostCode budgetedCostCode1 = PXResult<PMBudgetedCostCode>.op_Implicit(pxResult);
      Dictionary<int?, PMBudgetedCostCode> dictionary1 = projectSpecificRecords2;
      int? costCodeId = budgetedCostCode1.CostCodeID;
      int? key1 = new int?(costCodeId.Value);
      if (!dictionary1.ContainsKey(key1))
      {
        Dictionary<int?, PMBudgetedCostCode> dictionary2 = projectSpecificRecords2;
        costCodeId = budgetedCostCode1.CostCodeID;
        int? key2 = new int?(costCodeId.Value);
        PMBudgetedCostCode budgetedCostCode2 = budgetedCostCode1;
        dictionary2.Add(key2, budgetedCostCode2);
      }
    }
    return projectSpecificRecords2;
  }

  public bool IsProjectSpecific(int costCode)
  {
    return !this.GetTaskID().HasValue || !this.GetAccountGroupID().HasValue && !this.GetAccountID().HasValue || this.GetProjectSpecificRecords().ContainsKey(new int?(costCode));
  }

  public static string FormatValueByMask(
    PXCache cache,
    object row,
    string fieldName,
    string value)
  {
    return !(cache.GetStateExt(row, fieldName) is PXSegmentedState stateExt) ? value : Mask.Format(((PXStringState) stateExt).InputMask, value);
  }

  public static string FormatValueByMask<TField>(PXCache cache, object row, string value) where TField : IBqlField
  {
    return CostCodeSelectorAttribute.FormatValueByMask(cache, row, typeof (TField).Name, value);
  }
}
