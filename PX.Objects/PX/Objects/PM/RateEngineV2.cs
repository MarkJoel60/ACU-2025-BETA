// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RateEngineV2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.PM;

public class RateEngineV2
{
  protected PXGraph graph;
  protected Dictionary<string, List<PMRateDefinition>> definitions;
  protected StringBuilder trace;

  public RateEngineV2(PXGraph graph, IList<string> rateTables, IList<string> rateTypes)
  {
    this.graph = graph;
    this.definitions = new Dictionary<string, List<PMRateDefinition>>(rateTables.Count * rateTypes.Count);
    foreach (string rateTable in (IEnumerable<string>) rateTables)
    {
      foreach (PMRateDefinition rateDefinition in (IEnumerable<PMRateDefinition>) ((IRateTable) graph).GetRateDefinitions(rateTable))
      {
        if (rateTypes.Contains(rateDefinition.RateTypeID))
        {
          string definitionKey = this.GetDefinitionKey(rateTable, rateDefinition.RateTypeID);
          if (!this.definitions.ContainsKey(definitionKey))
            this.definitions.Add(definitionKey, new List<PMRateDefinition>());
          this.definitions[definitionKey].Add(rateDefinition);
        }
      }
    }
  }

  protected string GetDefinitionKey(string rateTableID, string rateTypeID)
  {
    return $"{rateTableID}.{rateTypeID}";
  }

  public Decimal? GetRate(string rateTableID, string rateTypeID, PMTran tran)
  {
    if (string.IsNullOrEmpty(rateTableID))
      throw new ArgumentNullException(nameof (rateTableID), PXMessages.LocalizeNoPrefix("Argument is null or an empty string."));
    this.trace = new StringBuilder();
    this.trace.AppendFormat("Calculating Rate. RateTable:{0}, RateType:{1}", (object) rateTableID, (object) rateTypeID);
    List<PMRateDefinition> pmRateDefinitionList;
    if (this.definitions.TryGetValue(this.GetDefinitionKey(rateTableID, rateTypeID), out pmRateDefinitionList))
    {
      foreach (PMRateDefinition rd in pmRateDefinitionList)
      {
        this.trace.AppendFormat("Start Processing Sequence:{0}", (object) rd.Description);
        Decimal? rate = this.GetRate(rd, tran);
        if (rate.HasValue)
        {
          this.trace.AppendFormat("End Processing Sequence. Rate Defined:{0}", (object) rate);
          return rate;
        }
        this.trace.AppendFormat("End Processing Sequence. Rate Not Defined");
      }
    }
    return new Decimal?();
  }

  public string GetTrace(PMTran tran)
  {
    PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find(this.graph, tran.AccountGroupID);
    if (pmAccountGroup != null)
      this.trace.AppendFormat(" PMTran.AccountGroup={0} ", (object) pmAccountGroup.GroupCD);
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(this.graph, new object[1]
    {
      (object) tran.InventoryID
    }));
    if (inventoryItem != null)
      this.trace.AppendFormat(" PMTran.InventoryID={0} ", (object) inventoryItem.InventoryCD);
    return this.trace.ToString();
  }

  protected virtual Decimal? GetRate(PMRateDefinition rd, PMTran tran)
  {
    bool flag = true;
    List<string> first = (List<string>) null;
    if (rd.Project.GetValueOrDefault())
    {
      List<string> rateCodes;
      if (!this.IsProjectFit(rd.RateDefinitionID, tran.ProjectID, out rateCodes))
        flag = false;
      else
        first = rateCodes;
    }
    bool? nullable = rd.Task;
    if (nullable.GetValueOrDefault())
    {
      List<string> rateCodes;
      if (!this.IsTaskFit(rd.RateDefinitionID, tran.ProjectID, tran.TaskID, out rateCodes))
        flag = false;
      else if (first == null)
      {
        first = rateCodes;
      }
      else
      {
        first = new List<string>(first.Intersect<string>((IEnumerable<string>) rateCodes));
        if (first.Count == 0)
          flag = false;
      }
    }
    nullable = rd.AccountGroup;
    if (nullable.GetValueOrDefault())
    {
      List<string> rateCodes;
      if (!this.IsAccountGroupFit(rd.RateDefinitionID, tran.AccountGroupID, out rateCodes))
        flag = false;
      else if (first == null)
      {
        first = rateCodes;
      }
      else
      {
        first = new List<string>(first.Intersect<string>((IEnumerable<string>) rateCodes));
        if (first.Count == 0)
          flag = false;
      }
    }
    nullable = rd.RateItem;
    if (nullable.GetValueOrDefault())
    {
      List<string> rateCodes;
      if (!this.IsItemFit(rd.RateDefinitionID, tran.InventoryID, out rateCodes))
        flag = false;
      else if (first == null)
      {
        first = rateCodes;
      }
      else
      {
        first = new List<string>(first.Intersect<string>((IEnumerable<string>) rateCodes));
        if (first.Count == 0)
          flag = false;
      }
    }
    nullable = rd.Employee;
    if (nullable.GetValueOrDefault())
    {
      List<string> rateCodes;
      if (!this.IsEmployeeFit(rd.RateDefinitionID, tran.ResourceID, out rateCodes))
        flag = false;
      else if (first == null)
      {
        first = rateCodes;
      }
      else
      {
        first = new List<string>(first.Intersect<string>((IEnumerable<string>) rateCodes));
        if (first.Count == 0)
          flag = false;
      }
    }
    if (!flag)
      return new Decimal?();
    string str = (string) null;
    if (first != null && first.Count > 0)
      str = first[0];
    PMRate pmRate;
    if (!string.IsNullOrEmpty(str))
      pmRate = PXResultset<PMRate>.op_Implicit(((PXSelectBase<PMRate>) new PXSelect<PMRate, Where<PMRate.rateDefinitionID, Equal<Required<PMRate.rateDefinitionID>>, And<Where<PMRate.rateCodeID, Equal<Required<PMRate.rateCodeID>>, And<Where<PMRate.startDate, LessEqual<Required<PMRate.startDate>>, And2<Where<PMRate.endDate, GreaterEqual<Required<PMRate.endDate>>>, Or<PMRate.endDate, IsNull>>>>>>>>(this.graph)).Select(new object[4]
      {
        (object) rd.RateDefinitionID,
        (object) str,
        (object) tran.Date,
        (object) tran.Date
      }));
    else
      pmRate = PXResultset<PMRate>.op_Implicit(((PXSelectBase<PMRate>) new PXSelect<PMRate, Where<PMRate.rateDefinitionID, Equal<Required<PMRate.rateDefinitionID>>, And<Where<PMRate.startDate, LessEqual<Required<PMRate.startDate>>, And2<Where<PMRate.endDate, GreaterEqual<Required<PMRate.endDate>>>, Or<PMRate.endDate, IsNull>>>>>>(this.graph)).Select(new object[3]
      {
        (object) rd.RateDefinitionID,
        (object) tran.Date,
        (object) tran.Date
      }));
    this.trace.AppendFormat("\tSearching Rate for Date:{0}", (object) tran.Date);
    return pmRate?.Rate;
  }

  protected virtual bool IsProjectFit(
    int? rateDefinitionID,
    int? projectID,
    out List<string> rateCodes)
  {
    rateCodes = new List<string>();
    string contractCd = (PMProject.PK.Find(this.graph, projectID) ?? throw new PXException("The {0} project is not found.", new object[1]
    {
      (object) projectID
    })).ContractCD;
    PXSelect<PMProjectRate, Where<PMProjectRate.rateDefinitionID, Equal<Required<PMProjectRate.rateDefinitionID>>>> pxSelect = new PXSelect<PMProjectRate, Where<PMProjectRate.rateDefinitionID, Equal<Required<PMProjectRate.rateDefinitionID>>>>(this.graph);
    bool flag = false;
    object[] objArray = new object[1]
    {
      (object) rateDefinitionID
    };
    foreach (PXResult<PMProjectRate> pxResult in ((PXSelectBase<PMProjectRate>) pxSelect).Select(objArray))
    {
      PMProjectRate pmProjectRate = PXResult<PMProjectRate>.op_Implicit(pxResult);
      if (this.IsFit(pmProjectRate.ProjectCD.Trim(), contractCd.Trim()))
      {
        rateCodes.Add(pmProjectRate.RateCodeID);
        this.trace.AppendFormat("\tChecking Project {0}..Match found.", (object) contractCd.Trim());
        flag = true;
      }
    }
    if (!flag)
      this.trace.AppendFormat("\tChecking Project {0}..Match not found.", (object) contractCd.Trim());
    return flag;
  }

  protected virtual bool IsTaskFit(
    int? rateDefinitionID,
    int? projectID,
    int? taskID,
    out List<string> rateCodes)
  {
    rateCodes = new List<string>();
    string taskCd = (PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select(this.graph, new object[2]
    {
      (object) projectID,
      (object) taskID
    })) ?? throw new PXException("The {0} project task is not found in the {1} project.", new object[2]
    {
      (object) taskID,
      (object) projectID
    })).TaskCD;
    PXSelect<PMTaskRate, Where<PMTaskRate.rateDefinitionID, Equal<Required<PMTaskRate.rateDefinitionID>>>> pxSelect = new PXSelect<PMTaskRate, Where<PMTaskRate.rateDefinitionID, Equal<Required<PMTaskRate.rateDefinitionID>>>>(this.graph);
    bool flag = false;
    object[] objArray = new object[1]
    {
      (object) rateDefinitionID
    };
    foreach (PXResult<PMTaskRate> pxResult in ((PXSelectBase<PMTaskRate>) pxSelect).Select(objArray))
    {
      PMTaskRate pmTaskRate = PXResult<PMTaskRate>.op_Implicit(pxResult);
      if (this.IsFit(pmTaskRate.TaskCD.Trim(), taskCd.Trim()))
      {
        rateCodes.Add(pmTaskRate.RateCodeID);
        this.trace.AppendFormat("\tChecking Task {0}..Match found.", (object) taskCd.Trim());
        flag = true;
      }
    }
    if (!flag)
      this.trace.AppendFormat("\tChecking Task {0}..Match not found.", (object) taskCd.Trim());
    return flag;
  }

  protected virtual bool IsAccountGroupFit(
    int? rateDefinitionID,
    int? accountGroupID,
    out List<string> rateCodes)
  {
    rateCodes = new List<string>();
    PXSelect<PMAccountGroupRate, Where<PMAccountGroupRate.rateDefinitionID, Equal<Required<PMAccountGroupRate.rateDefinitionID>>, And<PMAccountGroupRate.accountGroupID, Equal<Required<PMAccountGroupRate.accountGroupID>>>>> pxSelect = new PXSelect<PMAccountGroupRate, Where<PMAccountGroupRate.rateDefinitionID, Equal<Required<PMAccountGroupRate.rateDefinitionID>>, And<PMAccountGroupRate.accountGroupID, Equal<Required<PMAccountGroupRate.accountGroupID>>>>>(this.graph);
    bool flag = false;
    object[] objArray = new object[2]
    {
      (object) rateDefinitionID,
      (object) accountGroupID
    };
    foreach (PXResult<PMAccountGroupRate> pxResult in ((PXSelectBase<PMAccountGroupRate>) pxSelect).Select(objArray))
    {
      PMAccountGroupRate accountGroupRate = PXResult<PMAccountGroupRate>.op_Implicit(pxResult);
      rateCodes.Add(accountGroupRate.RateCodeID);
      this.trace.AppendFormat("\tChecking Account Group {0}..Match found.", (object) accountGroupID);
      flag = true;
    }
    if (!flag)
      this.trace.AppendFormat("\tChecking Account Group {0}..Match not found.", (object) accountGroupID);
    return flag;
  }

  protected virtual bool IsItemFit(
    int? rateDefinitionID,
    int? inventoryID,
    out List<string> rateCodes)
  {
    rateCodes = new List<string>();
    PXSelect<PMItemRate, Where<PMItemRate.rateDefinitionID, Equal<Required<PMItemRate.rateDefinitionID>>, And<PMItemRate.inventoryID, Equal<Required<PMItemRate.inventoryID>>>>> pxSelect = new PXSelect<PMItemRate, Where<PMItemRate.rateDefinitionID, Equal<Required<PMItemRate.rateDefinitionID>>, And<PMItemRate.inventoryID, Equal<Required<PMItemRate.inventoryID>>>>>(this.graph);
    bool flag = false;
    object[] objArray = new object[2]
    {
      (object) rateDefinitionID,
      (object) inventoryID
    };
    foreach (PXResult<PMItemRate> pxResult in ((PXSelectBase<PMItemRate>) pxSelect).Select(objArray))
    {
      PMItemRate pmItemRate = PXResult<PMItemRate>.op_Implicit(pxResult);
      rateCodes.Add(pmItemRate.RateCodeID);
      this.trace.AppendFormat("\tChecking Inventory Item {0}..Match found.", (object) inventoryID);
      flag = true;
    }
    if (!flag)
      this.trace.AppendFormat("\tChecking Inventory Item {0}..Match not found.", (object) inventoryID);
    return flag;
  }

  protected virtual bool IsEmployeeFit(
    int? rateDefinitionID,
    int? employeeID,
    out List<string> rateCodes)
  {
    rateCodes = new List<string>();
    PXSelect<PMEmployeeRate, Where<PMEmployeeRate.rateDefinitionID, Equal<Required<PMEmployeeRate.rateDefinitionID>>, And<PMEmployeeRate.employeeID, Equal<Required<PMEmployeeRate.employeeID>>>>> pxSelect = new PXSelect<PMEmployeeRate, Where<PMEmployeeRate.rateDefinitionID, Equal<Required<PMEmployeeRate.rateDefinitionID>>, And<PMEmployeeRate.employeeID, Equal<Required<PMEmployeeRate.employeeID>>>>>(this.graph);
    bool flag = false;
    object[] objArray = new object[2]
    {
      (object) rateDefinitionID,
      (object) employeeID
    };
    foreach (PXResult<PMEmployeeRate> pxResult in ((PXSelectBase<PMEmployeeRate>) pxSelect).Select(objArray))
    {
      PMEmployeeRate pmEmployeeRate = PXResult<PMEmployeeRate>.op_Implicit(pxResult);
      rateCodes.Add(pmEmployeeRate.RateCodeID);
      this.trace.AppendFormat("\tChecking Employee {0}..Match found.", (object) employeeID);
      flag = true;
    }
    if (!flag)
      this.trace.AppendFormat("\tChecking Employee {0}..Match not found.", (object) employeeID);
    return flag;
  }

  protected virtual bool IsFit(string wildcard, string value)
  {
    if (value.Length < wildcard.Length)
      value += new string(' ', wildcard.Length - value.Length);
    else if (value.Length > wildcard.Length)
      return false;
    for (int index = 0; index < wildcard.Length; ++index)
    {
      if (wildcard[index] != '?' && (int) wildcard[index] != (int) value[index])
        return false;
    }
    return true;
  }
}
