// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RateMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.PM;

[Serializable]
public class RateMaint : PXGraph<RateMaint, PMRateSequence>, ICaptionable
{
  [PXHidden]
  public PXSelect<EPEmployee> HiddenEmployee;
  public PXSelect<PMRateSequence> RateSequence;
  public PXSelect<PMRateDefinition, Where<PMRateDefinition.rateTableID, Equal<Current<PMRateSequence.rateTableID>>, And<PMRateDefinition.rateTypeID, Equal<Current<PMRateSequence.rateTypeID>>, And<PMRateDefinition.sequence, Equal<Current<PMRateSequence.sequence>>>>>> RateDefinition;
  public PXSelect<PMRate, Where<PMRate.rateDefinitionID, Equal<Current<PMRateSequence.rateDefinitionID>>, And<PMRate.rateCodeID, Equal<Current<PMRateSequence.rateCodeID>>>>> Rates;
  public PXSelectJoin<PMProjectRate, LeftJoin<PMProject, On<PMProjectRate.projectCD, Equal<PMProject.contractCD>>>, Where<PMProjectRate.rateDefinitionID, Equal<Current<PMRateSequence.rateDefinitionID>>, And<PMProjectRate.rateCodeID, Equal<Current<PMRateSequence.rateCodeID>>>>> Projects;
  public PXSelect<PMTaskRate, Where<PMTaskRate.rateDefinitionID, Equal<Current<PMRateSequence.rateDefinitionID>>, And<PMTaskRate.rateCodeID, Equal<Current<PMRateSequence.rateCodeID>>>>> Tasks;
  public PXSelectJoin<PMAccountGroupRate, InnerJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<PMAccountGroupRate.accountGroupID>>>, Where<PMAccountGroupRate.rateDefinitionID, Equal<Current<PMRateSequence.rateDefinitionID>>, And<PMAccountGroupRate.rateCodeID, Equal<Current<PMRateSequence.rateCodeID>>>>> AccountGroups;
  public PXSelectJoin<PMItemRate, InnerJoin<PX.Objects.IN.InventoryItem, On<PMItemRate.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>, Where<PMItemRate.rateDefinitionID, Equal<Current<PMRateSequence.rateDefinitionID>>, And<PMItemRate.rateCodeID, Equal<Current<PMRateSequence.rateCodeID>>>>> Items;
  public PXSelectJoin<PMEmployeeRate, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PMEmployeeRate.employeeID>>>, Where<PMEmployeeRate.rateDefinitionID, Equal<Current<PMRateSequence.rateDefinitionID>>, And<PMEmployeeRate.rateCodeID, Equal<Current<PMRateSequence.rateCodeID>>>>> Employees;

  public virtual void Persist()
  {
    ((PXAction) this.CopyPaste).SetVisible(false);
    this.OnValidateRate();
    this.OnValidateEntities();
    ((PXGraph) this).Persist();
  }

  public string Caption() => string.Empty;

  protected virtual void OnValidateRate()
  {
    List<PMRate> rates = new List<PMRate>();
    foreach (PXResult<PMRate> pxResult in ((PXSelectBase<PMRate>) this.Rates).Select(Array.Empty<object>()))
    {
      PMRate x = PXResult<PMRate>.op_Implicit(pxResult);
      if (rates.Count > 0)
      {
        if (this.IsOverlaping((IList<PMRate>) rates, x))
        {
          ((PXSelectBase) this.Rates).Cache.RaiseExceptionHandling<PMRate.startDate>((object) x, (object) x.StartDate, (Exception) new PXSetPropertyException("Overlapping time intervals are not allowed", (PXErrorLevel) 5));
          throw new PXException("Overlapping time intervals are not allowed");
        }
        rates.Add(x);
      }
      else
        rates.Add(x);
    }
  }

  private bool IsOverlaping(IList<PMRate> rates, PMRate x)
  {
    foreach (PMRate rate in (IEnumerable<PMRate>) rates)
    {
      if (this.IsOverlaping(x, rate))
        return true;
    }
    return false;
  }

  private bool IsOverlaping(PMRate x, PMRate y)
  {
    if (!x.StartDate.HasValue && !x.EndDate.HasValue)
      return true;
    if (!x.StartDate.HasValue && x.EndDate.HasValue)
      return !y.StartDate.HasValue || y.StartDate.Value.Date < x.EndDate.Value.Date;
    if (x.StartDate.HasValue && !x.EndDate.HasValue)
      return !y.EndDate.HasValue || y.EndDate.Value.Date > x.StartDate.Value.Date;
    if (!y.StartDate.HasValue || !y.EndDate.HasValue)
      return true;
    DateTime date1 = x.StartDate.Value.Date;
    DateTime? nullable = y.StartDate;
    DateTime date2 = nullable.Value.Date;
    if (date1 <= date2)
    {
      nullable = y.StartDate;
      DateTime date3 = nullable.Value.Date;
      nullable = x.EndDate;
      DateTime date4 = nullable.Value.Date;
      return date3 < date4;
    }
    nullable = x.StartDate;
    DateTime date5 = nullable.Value.Date;
    nullable = y.EndDate;
    DateTime date6 = nullable.Value.Date;
    return date5 < date6;
  }

  protected virtual void OnValidateEntities()
  {
    if (((PXSelectBase<PMRateDefinition>) this.RateDefinition).Current == null || ((PXSelectBase<PMRateSequence>) this.RateSequence).Current == null)
      return;
    string str = this.RunEntryValidationAndReturnErrors(((PXSelectBase<PMRateDefinition>) this.RateDefinition).Current, ((PXSelectBase<PMRateSequence>) this.RateSequence).Current.RateCodeID);
    if (!string.IsNullOrEmpty(str))
      throw new PXException(PXMessages.LocalizeNoPrefix("The rate table sequence is not unique and cannot be saved. The following combinations of entities are already defined in the system:") + Environment.NewLine + str);
  }

  protected virtual string RunEntryValidationAndReturnErrors(
    PMRateDefinition definition,
    string rateCodeID)
  {
    Dictionary<string, RateMaint.RateCodeData> dictionary = new Dictionary<string, RateMaint.RateCodeData>();
    if (definition.Project.GetValueOrDefault())
    {
      foreach (PXResult<PMProjectRate> pxResult in PXSelectBase<PMProjectRate, PXSelect<PMProjectRate, Where<PMProjectRate.rateDefinitionID, Equal<Required<PMProjectRate.rateDefinitionID>>, And<PMProjectRate.rateCodeID, NotEqual<Required<PMProjectRate.rateCodeID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) definition.RateDefinitionID,
        (object) rateCodeID
      }))
      {
        PMProjectRate pmProjectRate = PXResult<PMProjectRate>.op_Implicit(pxResult);
        if (!dictionary.ContainsKey(pmProjectRate.RateCodeID))
          dictionary.Add(pmProjectRate.RateCodeID, new RateMaint.RateCodeData(pmProjectRate.RateCodeID));
        dictionary[pmProjectRate.RateCodeID].Projects.Add((object) pmProjectRate.ProjectCD.ToUpper().Trim(), (object) pmProjectRate);
      }
    }
    if (definition.Task.GetValueOrDefault())
    {
      foreach (PXResult<PMTaskRate> pxResult in PXSelectBase<PMTaskRate, PXSelect<PMTaskRate, Where<PMTaskRate.rateDefinitionID, Equal<Required<PMTaskRate.rateDefinitionID>>, And<PMTaskRate.rateCodeID, NotEqual<Required<PMTaskRate.rateCodeID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) definition.RateDefinitionID,
        (object) rateCodeID
      }))
      {
        PMTaskRate pmTaskRate = PXResult<PMTaskRate>.op_Implicit(pxResult);
        if (!dictionary.ContainsKey(pmTaskRate.RateCodeID))
          dictionary.Add(pmTaskRate.RateCodeID, new RateMaint.RateCodeData(pmTaskRate.RateCodeID));
        dictionary[pmTaskRate.RateCodeID].Tasks.Add((object) pmTaskRate.TaskCD.ToUpper().Trim(), (object) pmTaskRate);
      }
    }
    if (definition.AccountGroup.GetValueOrDefault())
    {
      foreach (PXResult<PMAccountGroupRate> pxResult in PXSelectBase<PMAccountGroupRate, PXSelect<PMAccountGroupRate, Where<PMAccountGroupRate.rateDefinitionID, Equal<Required<PMAccountGroupRate.rateDefinitionID>>, And<PMAccountGroupRate.rateCodeID, NotEqual<Required<PMAccountGroupRate.rateCodeID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) definition.RateDefinitionID,
        (object) rateCodeID
      }))
      {
        PMAccountGroupRate accountGroupRate = PXResult<PMAccountGroupRate>.op_Implicit(pxResult);
        if (!dictionary.ContainsKey(accountGroupRate.RateCodeID))
          dictionary.Add(accountGroupRate.RateCodeID, new RateMaint.RateCodeData(accountGroupRate.RateCodeID));
        dictionary[accountGroupRate.RateCodeID].AccountGroups.Add((object) accountGroupRate.AccountGroupID.Value, (object) accountGroupRate);
      }
    }
    if (definition.RateItem.GetValueOrDefault())
    {
      foreach (PXResult<PMItemRate> pxResult in PXSelectBase<PMItemRate, PXSelect<PMItemRate, Where<PMItemRate.rateDefinitionID, Equal<Required<PMItemRate.rateDefinitionID>>, And<PMItemRate.rateCodeID, NotEqual<Required<PMItemRate.rateCodeID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) definition.RateDefinitionID,
        (object) rateCodeID
      }))
      {
        PMItemRate pmItemRate = PXResult<PMItemRate>.op_Implicit(pxResult);
        if (!dictionary.ContainsKey(pmItemRate.RateCodeID))
          dictionary.Add(pmItemRate.RateCodeID, new RateMaint.RateCodeData(pmItemRate.RateCodeID));
        dictionary[pmItemRate.RateCodeID].Inventory.Add((object) pmItemRate.InventoryID.Value, (object) pmItemRate);
      }
    }
    if (definition.Employee.GetValueOrDefault())
    {
      foreach (PXResult<PMEmployeeRate> pxResult in PXSelectBase<PMEmployeeRate, PXSelect<PMEmployeeRate, Where<PMEmployeeRate.rateDefinitionID, Equal<Required<PMEmployeeRate.rateDefinitionID>>, And<PMEmployeeRate.rateCodeID, NotEqual<Required<PMEmployeeRate.rateCodeID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) definition.RateDefinitionID,
        (object) rateCodeID
      }))
      {
        PMEmployeeRate pmEmployeeRate = PXResult<PMEmployeeRate>.op_Implicit(pxResult);
        if (!dictionary.ContainsKey(pmEmployeeRate.RateCodeID))
          dictionary.Add(pmEmployeeRate.RateCodeID, new RateMaint.RateCodeData(pmEmployeeRate.RateCodeID));
        dictionary[pmEmployeeRate.RateCodeID].Employees.Add((object) pmEmployeeRate.EmployeeID.Value, (object) pmEmployeeRate);
      }
    }
    string str1 = (string) null;
    List<RateMaint.Entity<string, PMProjectRate, PMProject>> data1 = new List<RateMaint.Entity<string, PMProjectRate, PMProject>>();
    List<RateMaint.Entity<string, PMTaskRate, PMTask>> data2 = new List<RateMaint.Entity<string, PMTaskRate, PMTask>>();
    List<RateMaint.Entity<int?, PMAccountGroupRate, PMAccountGroup>> data3 = new List<RateMaint.Entity<int?, PMAccountGroupRate, PMAccountGroup>>();
    List<RateMaint.Entity<int?, PMItemRate, PX.Objects.IN.InventoryItem>> data4 = new List<RateMaint.Entity<int?, PMItemRate, PX.Objects.IN.InventoryItem>>();
    List<RateMaint.Entity<int?, PMEmployeeRate, PX.Objects.CR.BAccount>> data5 = new List<RateMaint.Entity<int?, PMEmployeeRate, PX.Objects.CR.BAccount>>();
    if (definition.Project.GetValueOrDefault())
    {
      foreach (PXResult<PMProjectRate, PMProject> pxResult in ((PXSelectBase<PMProjectRate>) this.Projects).Select(Array.Empty<object>()))
      {
        if (((PXSelectBase) this.Projects).Cache.GetStatus((object) PXResult<PMProjectRate, PMProject>.op_Implicit(pxResult)) != 3)
          data1.Add(new RateMaint.Entity<string, PMProjectRate, PMProject>(PXResult<PMProjectRate, PMProject>.op_Implicit(pxResult).ProjectCD, PXResult<PMProjectRate, PMProject>.op_Implicit(pxResult), PXResult<PMProjectRate, PMProject>.op_Implicit(pxResult)));
      }
    }
    if (definition.Task.GetValueOrDefault())
    {
      foreach (PXResult<PMTaskRate> pxResult in ((PXSelectBase<PMTaskRate>) this.Tasks).Select(Array.Empty<object>()))
      {
        PMTaskRate pmTaskRate = PXResult<PMTaskRate>.op_Implicit(pxResult);
        if (((PXSelectBase) this.Tasks).Cache.GetStatus((object) pmTaskRate) != 3)
          data2.Add(new RateMaint.Entity<string, PMTaskRate, PMTask>(pmTaskRate.TaskCD, pmTaskRate, (PMTask) null));
      }
    }
    if (definition.AccountGroup.GetValueOrDefault())
    {
      foreach (PXResult<PMAccountGroupRate, PMAccountGroup> pxResult in ((PXSelectBase<PMAccountGroupRate>) this.AccountGroups).Select(Array.Empty<object>()))
      {
        if (((PXSelectBase) this.AccountGroups).Cache.GetStatus((object) PXResult<PMAccountGroupRate, PMAccountGroup>.op_Implicit(pxResult)) != 3)
          data3.Add(new RateMaint.Entity<int?, PMAccountGroupRate, PMAccountGroup>(PXResult<PMAccountGroupRate, PMAccountGroup>.op_Implicit(pxResult).AccountGroupID, PXResult<PMAccountGroupRate, PMAccountGroup>.op_Implicit(pxResult), PXResult<PMAccountGroupRate, PMAccountGroup>.op_Implicit(pxResult)));
      }
    }
    if (definition.RateItem.GetValueOrDefault())
    {
      foreach (PXResult<PMItemRate, PX.Objects.IN.InventoryItem> pxResult in ((PXSelectBase<PMItemRate>) this.Items).Select(Array.Empty<object>()))
      {
        if (((PXSelectBase) this.Items).Cache.GetStatus((object) PXResult<PMItemRate, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult)) != 3)
          data4.Add(new RateMaint.Entity<int?, PMItemRate, PX.Objects.IN.InventoryItem>(PXResult<PMItemRate, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult).InventoryID, PXResult<PMItemRate, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult), PXResult<PMItemRate, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult)));
      }
    }
    if (definition.Employee.GetValueOrDefault())
    {
      foreach (PXResult<PMEmployeeRate, PX.Objects.CR.BAccount> pxResult in ((PXSelectBase<PMEmployeeRate>) this.Employees).Select(Array.Empty<object>()))
      {
        if (((PXSelectBase) this.Employees).Cache.GetStatus((object) PXResult<PMEmployeeRate, PX.Objects.CR.BAccount>.op_Implicit(pxResult)) != 3)
          data5.Add(new RateMaint.Entity<int?, PMEmployeeRate, PX.Objects.CR.BAccount>(PXResult<PMEmployeeRate, PX.Objects.CR.BAccount>.op_Implicit(pxResult).EmployeeID, PXResult<PMEmployeeRate, PX.Objects.CR.BAccount>.op_Implicit(pxResult), PXResult<PMEmployeeRate, PX.Objects.CR.BAccount>.op_Implicit(pxResult)));
      }
    }
    if (data1.Count == 0)
      data1.Add(new RateMaint.Entity<string, PMProjectRate, PMProject>());
    if (data2.Count == 0)
      data2.Add(new RateMaint.Entity<string, PMTaskRate, PMTask>());
    if (data3.Count == 0)
      data3.Add(new RateMaint.Entity<int?, PMAccountGroupRate, PMAccountGroup>());
    if (data4.Count == 0)
      data4.Add(new RateMaint.Entity<int?, PMItemRate, PX.Objects.IN.InventoryItem>());
    if (data5.Count == 0)
      data5.Add(new RateMaint.Entity<int?, PMEmployeeRate, PX.Objects.CR.BAccount>());
    StringBuilder stringBuilder = new StringBuilder();
    foreach (RateMaint.RateCodeData rateCodeData in dictionary.Values)
    {
      string str2 = rateCodeData.Validate(data1, data2, data3, data4, data5);
      if (str2 != null)
        stringBuilder.AppendLine(str2);
    }
    if (stringBuilder.Length > 0)
      str1 = stringBuilder.ToString();
    return str1;
  }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    IEnumerable enumerable = adapter.Get();
    bool flag1 = false;
    IEnumerator enumerator1 = enumerable.GetEnumerator();
    try
    {
      if (enumerator1.MoveNext())
      {
        object current = enumerator1.Current;
        flag1 = true;
      }
    }
    finally
    {
      if (enumerator1 is IDisposable disposable)
        disposable.Dispose();
    }
    if (!flag1 && ((PXSelectBase<PMRateSequence>) this.RateSequence).Current != null)
    {
      int? nullable1 = new int?();
      string str1 = (string) null;
      bool flag2 = false;
      int? nullable2 = new int?();
      string str2 = (string) null;
      bool flag3 = false;
      int? nullable3 = new int?();
      int? nullable4 = new int?();
      bool flag4 = false;
      int? nullable5 = new int?();
      for (int index = 0; index <= adapter.SortColumns.Length - 1; ++index)
      {
        if (adapter.SortColumns[index].ToLower() == typeof (PMRateSequence.rateTableID).Name.ToLower())
        {
          nullable1 = new int?(index);
          str1 = adapter.Searches[index] as string;
          flag2 = PXLocalesProvider.CollationComparer.Compare(str1, ((PXSelectBase<PMRateSequence>) this.RateSequence).Current.RateTableID) != 0;
        }
        else if (adapter.SortColumns[index].ToLower() == typeof (PMRateSequence.rateTypeID).Name.ToLower())
        {
          nullable2 = new int?(index);
          str2 = adapter.Searches[index] as string;
          flag3 = PXLocalesProvider.CollationComparer.Compare(str2, ((PXSelectBase<PMRateSequence>) this.RateSequence).Current.RateTypeID) != 0;
        }
        else if (adapter.SortColumns[index].ToLower() == typeof (PMRateSequence.sequence).Name.ToLower())
        {
          nullable3 = new int?(index);
          nullable4 = adapter.Searches[index] as int?;
          int? nullable6 = nullable4;
          int? sequence = ((PXSelectBase<PMRateSequence>) this.RateSequence).Current.Sequence;
          flag4 = !(nullable6.GetValueOrDefault() == sequence.GetValueOrDefault() & nullable6.HasValue == sequence.HasValue);
        }
        else if (adapter.SortColumns[index].ToLower() == typeof (PMRateSequence.rateCodeID).Name.ToLower())
          nullable5 = new int?(index);
      }
      PMRateSequence pmRateSequence1 = (PMRateSequence) null;
      if (((str1 == null || str2 == null ? 0 : (nullable4.HasValue ? 1 : 0)) & (flag4 ? 1 : 0)) != 0)
      {
        pmRateSequence1 = GraphHelper.RowCast<PMRateSequence>((IEnumerable) PXSelectBase<PMRateSequence, PXSelect<PMRateSequence, Where<PMRateSequence.rateTableID, Equal<Required<PMRateSequence.rateTableID>>, And<PMRateSequence.rateTypeID, Equal<Required<PMRateSequence.rateTypeID>>, And<PMRateSequence.sequence, Equal<Required<PMRateSequence.sequence>>>>>, OrderBy<Asc<PMRateSequence.sequence, Asc<PMRateSequence.rateCodeID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
        {
          (object) str1,
          (object) str2,
          (object) nullable4
        })).FirstOrDefault<PMRateSequence>();
        if (pmRateSequence1 == null && GraphHelper.Caches<PMRateSequence>((PXGraph) this).GetStatus(((PXSelectBase<PMRateSequence>) this.RateSequence).Current) == null)
        {
          PMRateSequence pmRateSequence2 = new PMRateSequence();
          pmRateSequence2.RateTableID = str1;
          pmRateSequence2.RateTypeID = str2;
          pmRateSequence2.Sequence = nullable4;
          GraphHelper.Caches<PMRateSequence>((PXGraph) this).Insert(pmRateSequence2);
          ((PXCache) GraphHelper.Caches<PMRateSequence>((PXGraph) this)).IsDirty = false;
          return (IEnumerable) new object[1]
          {
            (object) pmRateSequence2
          };
        }
      }
      if (pmRateSequence1 == null && str1 != null && str2 != null && flag2 | flag3)
      {
        pmRateSequence1 = GraphHelper.RowCast<PMRateSequence>((IEnumerable) PXSelectBase<PMRateSequence, PXSelect<PMRateSequence, Where<PMRateSequence.rateTableID, Equal<Required<PMRateSequence.rateTableID>>, And<PMRateSequence.rateTypeID, Equal<Required<PMRateSequence.rateTypeID>>>>, OrderBy<Asc<PMRateSequence.sequence, Asc<PMRateSequence.rateCodeID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
        {
          (object) str1,
          (object) str2
        })).FirstOrDefault<PMRateSequence>();
        if (pmRateSequence1 == null && GraphHelper.Caches<PMRateSequence>((PXGraph) this).GetStatus(((PXSelectBase<PMRateSequence>) this.RateSequence).Current) == null)
        {
          PMRateSequence pmRateSequence3 = new PMRateSequence();
          pmRateSequence3.RateTableID = str1;
          pmRateSequence3.RateTypeID = str2;
          GraphHelper.Caches<PMRateSequence>((PXGraph) this).Insert(pmRateSequence3);
          ((PXCache) GraphHelper.Caches<PMRateSequence>((PXGraph) this)).IsDirty = false;
          return (IEnumerable) new object[1]
          {
            (object) pmRateSequence3
          };
        }
      }
      if (pmRateSequence1 != null)
      {
        if (nullable1.HasValue)
          adapter.Searches[nullable1.Value] = (object) pmRateSequence1.RateTableID;
        if (nullable2.HasValue)
          adapter.Searches[nullable2.Value] = (object) pmRateSequence1.RateTypeID;
        if (nullable3.HasValue)
          adapter.Searches[nullable3.Value] = (object) pmRateSequence1.Sequence;
        if (nullable5.HasValue)
          adapter.Searches[nullable5.Value] = (object) pmRateSequence1.RateCodeID;
      }
    }
    IEnumerator enumerator2 = ((PXAction) new PXCancel<PMRateSequence>((PXGraph) this, nameof (Cancel))).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator2.MoveNext())
        return (IEnumerable) new object[1]
        {
          (object) (PMRateSequence) enumerator2.Current
        };
    }
    finally
    {
      if (enumerator2 is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMRateSequence, PMRateSequence.sequence> e)
  {
    if (((PXGraph) this).IsImport)
      return;
    PXSelectBase<PMRateDefinition> pxSelectBase = (PXSelectBase<PMRateDefinition>) new PXSelect<PMRateDefinition, Where<PMRateDefinition.rateTableID, Equal<Required<PMRateSequence.rateTableID>>, And<PMRateDefinition.rateTypeID, Equal<Required<PMRateSequence.rateTypeID>>>>, OrderBy<Asc<PMRateDefinition.sequence>>>((PXGraph) this);
    List<string> stringList = new List<string>();
    List<int> intList = new List<int>();
    int? returnValue = (int?) ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMRateSequence, PMRateSequence.sequence>>) e).ReturnValue;
    bool flag = false;
    PMRateSequence pmRateSequence = ((PXSelectBase<PMRateSequence>) this.RateSequence).Current ?? e.Row;
    if (pmRateSequence != null)
    {
      foreach (PXResult<PMRateDefinition> pxResult in pxSelectBase.Select(new object[2]
      {
        (object) pmRateSequence.RateTableID,
        (object) pmRateSequence.RateTypeID
      }))
      {
        PMRateDefinition pmRateDefinition = PXResult<PMRateDefinition>.op_Implicit(pxResult);
        string str1 = $"{pmRateDefinition.Sequence} - [ ";
        bool? nullable = pmRateDefinition.Project;
        if (nullable.GetValueOrDefault())
          str1 = $"{str1}{PXMessages.LocalizeNoPrefix("Project")}  ";
        nullable = pmRateDefinition.Task;
        if (nullable.GetValueOrDefault())
          str1 = $"{str1}{PXMessages.LocalizeNoPrefix("Task")}  ";
        nullable = pmRateDefinition.AccountGroup;
        if (nullable.GetValueOrDefault())
          str1 = $"{str1}{PXMessages.LocalizeNoPrefix("Account Group")}  ";
        nullable = pmRateDefinition.RateItem;
        if (nullable.GetValueOrDefault())
          str1 = $"{str1}{PXMessages.LocalizeNoPrefix("Item")}  ";
        nullable = pmRateDefinition.Employee;
        if (nullable.GetValueOrDefault())
          str1 = $"{str1}{PXMessages.LocalizeNoPrefix("Employee")}  ";
        string str2 = $"{str1}] {pmRateDefinition.Description}";
        if (returnValue.HasValue && returnValue.Value == (int) pmRateDefinition.Sequence.Value)
          flag = true;
        stringList.Add(str2);
        intList.Add(Convert.ToInt32(pmRateDefinition.Sequence.Value));
      }
    }
    if (!flag)
    {
      if (intList.Count > 0)
        ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMRateSequence, PMRateSequence.sequence>>) e).ReturnValue = (object) intList[0];
      else
        ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMRateSequence, PMRateSequence.sequence>>) e).ReturnValue = (object) null;
    }
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMRateSequence, PMRateSequence.sequence>>) e).ReturnState = (object) PXIntState.CreateInstance(((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMRateSequence, PMRateSequence.sequence>>) e).ReturnState, "Sequence", new bool?(false), new int?(1), new int?(), new int?(), intList.ToArray(), stringList.ToArray(), (System.Type) null, new int?(), (string[]) null, new bool?());
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMRateSequence> e)
  {
    if (e.Row == null || e.Row.RateDefinitionID.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMRateSequence>>) e).Cache.SetDefaultExt<PMRateSequence.rateDefinitionID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMRate> e)
  {
    if (e.Row == null || e.Row.RateDefinitionID.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMRate>>) e).Cache.SetDefaultExt<PMRate.rateDefinitionID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMProjectRate> e)
  {
    if (e.Row == null || e.Row.RateDefinitionID.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProjectRate>>) e).Cache.SetDefaultExt<PMProjectRate.rateDefinitionID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMTaskRate> e)
  {
    if (e.Row == null || e.Row.RateDefinitionID.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMTaskRate>>) e).Cache.SetDefaultExt<PMTaskRate.rateDefinitionID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMAccountGroupRate> e)
  {
    if (e.Row == null || e.Row.RateDefinitionID.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMAccountGroupRate>>) e).Cache.SetDefaultExt<PMAccountGroupRate.rateDefinitionID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMItemRate> e)
  {
    if (e.Row == null || e.Row.RateDefinitionID.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMItemRate>>) e).Cache.SetDefaultExt<PMItemRate.rateDefinitionID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMEmployeeRate> e)
  {
    if (e.Row == null || e.Row.RateDefinitionID.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMEmployeeRate>>) e).Cache.SetDefaultExt<PMEmployeeRate.rateDefinitionID>((object) e.Row);
  }

  protected virtual void PMRate_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PMRate row = (PMRate) e.Row;
    if (!row.StartDate.HasValue)
      return;
    DateTime? nullable = row.EndDate;
    if (!nullable.HasValue)
      return;
    nullable = row.StartDate;
    DateTime? endDate = row.EndDate;
    if ((nullable.HasValue & endDate.HasValue ? (nullable.GetValueOrDefault() > endDate.GetValueOrDefault() ? 1 : 0) : 0) == 0 || e.Operation == 3)
      return;
    sender.RaiseExceptionHandling<PMRate.endDate>((object) row, (object) row.EndDate, (Exception) new PXSetPropertyException("End Date cannot be earlier then Start Date"));
  }

  protected class Entity<K, V, T>
    where V : IBqlTable
    where T : IBqlTable
  {
    public K Key { get; private set; }

    public V Value { get; private set; }

    public T Object { get; private set; }

    public Entity()
    {
    }

    public Entity(K key, V value, T entity)
    {
      this.Key = key;
      this.Value = value;
      this.Object = entity;
    }
  }

  protected class RateCodeData
  {
    public string RateCode { get; private set; }

    public Hashtable Projects { get; private set; }

    public Hashtable Tasks { get; private set; }

    public Hashtable AccountGroups { get; private set; }

    public Hashtable Inventory { get; private set; }

    public Hashtable Employees { get; private set; }

    public RateCodeData(string rateCode)
    {
      this.RateCode = rateCode;
      this.Projects = new Hashtable();
      this.Tasks = new Hashtable();
      this.AccountGroups = new Hashtable();
      this.Inventory = new Hashtable();
      this.Employees = new Hashtable();
    }

    public string Validate(
      List<RateMaint.Entity<string, PMProjectRate, PMProject>> data1,
      List<RateMaint.Entity<string, PMTaskRate, PMTask>> data2,
      List<RateMaint.Entity<int?, PMAccountGroupRate, PMAccountGroup>> data3,
      List<RateMaint.Entity<int?, PMItemRate, PX.Objects.IN.InventoryItem>> data4,
      List<RateMaint.Entity<int?, PMEmployeeRate, PX.Objects.CR.BAccount>> data5)
    {
      string str = (string) null;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (RateMaint.Entity<string, PMProjectRate, PMProject> entity1 in data1)
      {
        foreach (RateMaint.Entity<string, PMTaskRate, PMTask> entity2 in data2)
        {
          foreach (RateMaint.Entity<int?, PMAccountGroupRate, PMAccountGroup> entity3 in data3)
          {
            foreach (RateMaint.Entity<int?, PMItemRate, PX.Objects.IN.InventoryItem> entity4 in data4)
            {
              foreach (RateMaint.Entity<int?, PMEmployeeRate, PX.Objects.CR.BAccount> entity5 in data5)
              {
                bool flag1 = true;
                bool flag2;
                if (entity1.Key != null && !this.Projects.ContainsKey((object) entity1.Key.ToUpper().Trim()))
                  flag2 = false;
                else if (entity2.Key != null && !this.Tasks.ContainsKey((object) entity2.Key.ToUpper().Trim()))
                {
                  flag2 = false;
                }
                else
                {
                  int? key = entity3.Key;
                  if (key.HasValue && !this.AccountGroups.ContainsKey((object) entity3.Key))
                  {
                    flag2 = false;
                  }
                  else
                  {
                    key = entity4.Key;
                    if (key.HasValue && !this.Inventory.ContainsKey((object) entity4.Key))
                    {
                      flag2 = false;
                    }
                    else
                    {
                      key = entity5.Key;
                      if (key.HasValue && !this.Employees.ContainsKey((object) entity5.Key))
                        flag2 = false;
                      else if (flag1)
                      {
                        bool flag3 = false;
                        if (entity1.Key != null)
                        {
                          stringBuilder.AppendFormat("{0}:{1}, ", (object) PXMessages.LocalizeNoPrefix("Project"), (object) entity1.Key);
                          flag3 = true;
                        }
                        if (entity2.Key != null)
                        {
                          stringBuilder.AppendFormat("{0}:{1}, ", (object) PXMessages.LocalizeNoPrefix("Project Task"), (object) entity2.Key);
                          flag3 = true;
                        }
                        key = entity3.Key;
                        if (key.HasValue)
                        {
                          stringBuilder.AppendFormat("{0}:{1}, ", (object) PXMessages.LocalizeNoPrefix("Account Group"), (object) entity3.Object.GroupCD);
                          flag3 = true;
                        }
                        key = entity4.Key;
                        if (key.HasValue)
                        {
                          stringBuilder.AppendFormat("{0}:{1}, ", (object) PXMessages.LocalizeNoPrefix("Inventory Item"), (object) entity4.Object.InventoryCD);
                          flag3 = true;
                        }
                        key = entity5.Key;
                        if (key.HasValue)
                        {
                          stringBuilder.AppendFormat("{0}:{1}, ", (object) PXMessages.LocalizeNoPrefix("Employee"), (object) entity5.Object.AcctCD);
                          flag3 = true;
                        }
                        if (flag3)
                          stringBuilder.AppendLine("");
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      if (stringBuilder.Length > 0)
        str = $"{PXMessages.LocalizeNoPrefix("Rate Code")} = {this.RateCode} " + stringBuilder.ToString();
      return str;
    }
  }
}
