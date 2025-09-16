// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentHelper`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Wiki.Parser;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP.DAC;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.EP;

/// <summary>Implements the approval process logic.</summary>
public class EPAssignmentHelper<Table> : PXGraph<EPAssignmentHelper<Table>> where Table : class, IBqlTable, new()
{
  private readonly PXGraph _Graph;
  private readonly List<Guid> path = new List<Guid>();
  private readonly List<PXResult> results = new List<PXResult>();
  private PXGraph processGraph;
  private System.Type processMapType;

  public EPAssignmentHelper(PXGraph graph)
    : this()
  {
    this._Graph = graph;
  }

  public EPAssignmentHelper()
  {
  }

  public virtual IEnumerable<ApproveInfo> Assign(
    Table item,
    EPAssignmentMap map,
    bool isApprove,
    int? currentStepSequence)
  {
    EPAssignmentHelper<Table> assignmentHelper1 = this;
    assignmentHelper1.path.Clear();
    assignmentHelper1.processMapType = GraphHelper.GetType(map.EntityType);
    System.Type type1 = item.GetType();
    PXSelectBase<EPRule> pxSelectBase = (PXSelectBase<EPRule>) new PXSelectReadonly<EPRule, Where<EPRule.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>>, OrderBy<Asc<EPAssignmentRoute.sequence>>>((PXGraph) assignmentHelper1);
    PXResultset<EPRule> rules;
    if (isApprove)
    {
      EPRule nextStep = PXResultset<EPRule>.op_Implicit(((PXSelectBase<EPRule>) new PXSelectReadonly<EPRule, Where<EPRule.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>, And<EPRule.isActive, Equal<boolTrue>, And<EPRule.sequence, Greater<Required<EPRule.sequence>>, And<EPRule.stepID, IsNull>>>>, OrderBy<Asc<EPAssignmentRoute.sequence>>>((PXGraph) assignmentHelper1)).Select(new object[2]
      {
        (object) map.AssignmentMapID,
        (object) (currentStepSequence ?? -1)
      }));
      if (nextStep == null)
        yield break;
      pxSelectBase.WhereAnd<Where<EPRule.stepID, Equal<Required<EPRule.stepID>>>>();
      rules = pxSelectBase.Select(new object[3]
      {
        (object) map.AssignmentMapID,
        (object) nextStep.RuleID,
        null
      });
      EnumerableExtensions.ForEach<PXResult<EPRule>>((IEnumerable<PXResult<EPRule>>) rules, (Action<PXResult<EPRule>>) (_ => PXResult<EPRule>.op_Implicit(_).StepName = nextStep.Name));
    }
    else
      rules = pxSelectBase.Select(new object[2]
      {
        (object) map.AssignmentMapID,
        null
      });
    System.Type type2 = GraphHelper.GetType(map.GraphType);
    if (type2 == (System.Type) null)
    {
      ((PXGraph) assignmentHelper1).Caches[type1].Current = (object) item;
      type2 = EntityHelper.GetPrimaryGraphType((PXGraph) assignmentHelper1, assignmentHelper1.processMapType);
    }
    if (assignmentHelper1._Graph != null && type2.IsAssignableFrom(assignmentHelper1._Graph.GetType()))
    {
      assignmentHelper1.processGraph = assignmentHelper1._Graph;
    }
    else
    {
      EPAssignmentHelper<Table> assignmentHelper2 = assignmentHelper1;
      System.Type type3 = GraphHelper.GetType(map.GraphType);
      if ((object) type3 == null)
        type3 = type2;
      PXGraph instance = PXGraph.CreateInstance(type3);
      assignmentHelper2.processGraph = instance;
    }
    if (assignmentHelper1.processGraph != null && assignmentHelper1.processMapType != (System.Type) null)
    {
      if (assignmentHelper1.processMapType.IsAssignableFrom(type1))
      {
        assignmentHelper1.processGraph.Caches[type1].Current = (object) item;
        assignmentHelper1.processGraph.Caches[assignmentHelper1.processMapType].Current = (object) item;
      }
      else
      {
        if (!type1.IsAssignableFrom(assignmentHelper1.processMapType))
          yield break;
        object instance = assignmentHelper1.processGraph.Caches[assignmentHelper1.processMapType].CreateInstance();
        ((PXCache) Activator.CreateInstance(typeof (PXCache<>).MakeGenericType(type1), (object) assignmentHelper1)).RestoreCopy(instance, (object) item);
        assignmentHelper1.processGraph.Caches[assignmentHelper1.processMapType].Current = instance;
      }
    }
    foreach (ApproveInfo approveInfo in assignmentHelper1.ProcessLevel(item, map.AssignmentMapID, rules))
      yield return approveInfo;
  }

  private IEnumerable<ApproveInfo> ProcessLevel(
    Table item,
    int? assignmentMap,
    PXResultset<EPRule> rules)
  {
    EPAssignmentHelper<Table> assignmentHelper = this;
    foreach (PXResult<EPRule> rule1 in rules)
    {
      EPRule rule = PXResult<EPRule>.op_Implicit(rule1);
      Guid? ruleId = rule.RuleID;
      if (ruleId.HasValue && rule.IsActive.GetValueOrDefault())
      {
        List<Guid> path1 = assignmentHelper.path;
        ruleId = rule.RuleID;
        Guid guid1 = ruleId.Value;
        path1.Add(guid1);
        bool isSuccessful = true;
        List<object> objectList = assignmentHelper.ExecuteRule(item, rule, ref isSuccessful);
        if (isSuccessful && (objectList == null || objectList.Count != 0))
        {
          int? OwnerID = new int?();
          int? WorkgroupID = new int?();
          System.Type viewType;
          switch (rule.RuleType)
          {
            case "D":
              if (!rule.OwnerID.HasValue)
              {
                PXResultset<EPRuleApprover> source = PXSelectBase<EPRuleApprover, PXSelect<EPRuleApprover, Where<EPRuleApprover.ruleID, Equal<Required<EPRule.ruleID>>>>.Config>.Select((PXGraph) assignmentHelper, new object[1]
                {
                  (object) rule.RuleID
                });
                if (((IQueryable<PXResult<EPRuleApprover>>) source).Any<PXResult<EPRuleApprover>>())
                {
                  foreach (PXResult<EPRuleApprover> pxResult in source)
                  {
                    OwnerID = PXResult<EPRuleApprover>.op_Implicit(pxResult).OwnerID;
                    WorkgroupID = rule.WorkgroupID;
                    ApproveInfo approveInfo = assignmentHelper.CreateApproveInfo(OwnerID, WorkgroupID, rule);
                    if (approveInfo != null)
                      yield return approveInfo;
                  }
                  break;
                }
              }
              OwnerID = rule.OwnerID;
              WorkgroupID = rule.WorkgroupID;
              ApproveInfo approveInfo1 = assignmentHelper.CreateApproveInfo(OwnerID, WorkgroupID, rule);
              if (approveInfo1 != null)
              {
                yield return approveInfo1;
                break;
              }
              continue;
            case "E":
              WorkgroupID = rule.WorkgroupID;
              if (rule.OwnerSource != null)
              {
                PXView pxView1 = (PXView) null;
                PXView pxView2 = (PXView) null;
                viewType = (System.Type) null;
                try
                {
                  string ownerSource = rule.OwnerSource;
                  string a = ownerSource.Substring(ownerSource.LastIndexOf("(") + 1, ownerSource.IndexOf(".") - (ownerSource.LastIndexOf("(") + 1));
                  if (string.Equals(a, assignmentHelper.processGraph.PrimaryView, StringComparison.InvariantCultureIgnoreCase))
                  {
                    pxView2 = assignmentHelper.processGraph.Views[a];
                    viewType = pxView2.GetItemType();
                  }
                  else
                  {
                    pxView1 = assignmentHelper.processGraph.Views[a];
                    viewType = pxView1.GetItemType();
                  }
                }
                catch (Exception ex)
                {
                }
                foreach (object obj1 in (objectList == null || objectList.Count <= 0 ? 0 : (((PXResult) objectList[0])[viewType] != null ? 1 : 0)) != 0 ? objectList : pxView1?.SelectMulti(Array.Empty<object>()) ?? pxView2.Cache.Current.SingleToList<object>() ?? objectList)
                {
                  if (obj1 is PXResult)
                  {
                    object obj2 = ((PXResult) obj1)[viewType];
                    assignmentHelper.processGraph.Caches[viewType].Current = obj2;
                  }
                  else
                    assignmentHelper.processGraph.Caches[viewType].Current = obj1;
                  int result;
                  // ISSUE: reference to a compiler-generated method
                  if (int.TryParse(PXTemplateContentParser.Instance.Process(rule.OwnerSource, assignmentHelper.processGraph, viewType, (object[]) null, new Func<PXCache, object, string, string>(assignmentHelper.\u003CProcessLevel\u003Eb__8_1)), out result))
                    OwnerID = new int?(result);
                  if (OwnerID.HasValue || WorkgroupID.HasValue)
                    yield return new ApproveInfo()
                    {
                      OwnerID = OwnerID,
                      WorkgroupID = WorkgroupID,
                      RuleID = rule.RuleID,
                      StepID = rule.StepID,
                      WaitTime = rule.WaitTime
                    };
                }
                break;
              }
              continue;
            case "F":
              List<EPRuleBaseCondition> list = ((IQueryable<PXResult<EPRuleEmployeeCondition>>) PXSelectBase<EPRuleEmployeeCondition, PXSelectReadonly<EPRuleEmployeeCondition, Where<EPRuleEmployeeCondition.ruleID, Equal<Required<EPRule.ruleID>>>>.Config>.Select((PXGraph) assignmentHelper, new object[1]
              {
                (object) rule.RuleID
              })).Select<PXResult<EPRuleEmployeeCondition>, EPRuleBaseCondition>((Expression<Func<PXResult<EPRuleEmployeeCondition>, EPRuleBaseCondition>>) (_ => (EPRuleEmployeeCondition) _)).ToList<EPRuleBaseCondition>();
              if (list.Count != 0)
              {
                foreach (ApproveInfo approveInfo2 in assignmentHelper.GetEmployeesByFilter(item, rule, list))
                  yield return approveInfo2;
                break;
              }
              break;
          }
          viewType = (System.Type) null;
          ruleId = rule.RuleID;
          if (ruleId.HasValue)
          {
            List<Guid> path2 = assignmentHelper.path;
            ruleId = rule.RuleID;
            Guid guid2 = ruleId.Value;
            if (!path2.Contains(guid2))
            {
              foreach (ApproveInfo approveInfo3 in assignmentHelper.ProcessLevel(item, assignmentMap, PXSelectBase<EPRule, PXSelectReadonly<EPRule>.Config>.Search<EPRule.ruleID>((PXGraph) assignmentHelper, (object) rule.RuleID, Array.Empty<object>())))
                yield return approveInfo3;
            }
            else
              continue;
          }
          PXResultset<EPRule> rules1 = PXSelectBase<EPRule, PXSelectReadonly<EPRule, Where<EPRule.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>>, OrderBy<Asc<EPRule.sequence>>>.Config>.Select((PXGraph) assignmentHelper, new object[1]
          {
            (object) assignmentMap
          });
          foreach (ApproveInfo approveInfo4 in assignmentHelper.ProcessLevel(item, assignmentMap, rules1))
            yield return approveInfo4;
          OwnerID = new int?();
          WorkgroupID = new int?();
          rule = (EPRule) null;
        }
      }
    }
  }

  private void FillOwnerWorkgroup(ref int? OwnerID, ref int? WorkgroupID)
  {
    if (WorkgroupID.HasValue && OwnerID.HasValue)
    {
      EPCompanyTreeMember companyTreeMember = PXResultset<EPCompanyTreeMember>.op_Implicit(PXSelectBase<EPCompanyTreeMember, PXSelectJoin<EPCompanyTreeMember, InnerJoin<EPCompanyTreeH, On<EPCompanyTreeH.workGroupID, Equal<EPCompanyTreeMember.workGroupID>>, InnerJoin<EPEmployee, On<EPEmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>>>, Where<EPCompanyTreeH.parentWGID, Equal<Required<EPCompanyTreeH.parentWGID>>, And<EPCompanyTreeMember.contactID, Equal<Required<EPCompanyTreeMember.contactID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
      {
        (object) WorkgroupID,
        (object) OwnerID
      }));
      if (companyTreeMember != null)
        WorkgroupID = companyTreeMember.WorkGroupID;
      else
        OwnerID = new int?();
    }
    if (!WorkgroupID.HasValue || OwnerID.HasValue)
      return;
    EPCompanyTreeMember companyTreeMember1 = PXResultset<EPCompanyTreeMember>.op_Implicit(PXSelectBase<EPCompanyTreeMember, PXSelectJoin<EPCompanyTreeMember, InnerJoin<EPEmployee, On<EPEmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>>, Where<EPCompanyTreeMember.workGroupID, Equal<Required<EPCompanyTreeMember.workGroupID>>, And<EPCompanyTreeMember.isOwner, Equal<boolTrue>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) WorkgroupID
    }));
    if (companyTreeMember1 == null)
      return;
    OwnerID = companyTreeMember1.ContactID;
  }

  private List<object> ExecuteRule(Table item, EPRule rule, ref bool isSuccessful)
  {
    try
    {
      List<EPRuleBaseCondition> list = ((IQueryable<PXResult<EPRuleCondition>>) PXSelectBase<EPRuleCondition, PXSelectReadonly<EPRuleCondition, Where<EPRuleCondition.ruleID, Equal<Required<EPRule.ruleID>>, And<EPRuleCondition.isActive, Equal<boolTrue>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) rule.RuleID
      })).Select<PXResult<EPRuleCondition>, EPRuleBaseCondition>((Expression<Func<PXResult<EPRuleCondition>, EPRuleBaseCondition>>) (_ => (EPRuleCondition) _)).ToList<EPRuleBaseCondition>();
      if (list.Count == 0)
        return (List<object>) null;
      // ISSUE: method pointer
      PXView itemView = new PXView(this.processGraph, false, BqlCommand.CreateInstance(new System.Type[1]
      {
        EPAssignmentHelper<Table>.CreateResultView(this.processGraph, list)
      }), (Delegate) new PXSelectDelegate((object) this, __methodptr(getItemRecord)));
      this.Select(item, itemView, ((IEnumerable<System.Type>) itemView.GetItemTypes()).ToList<System.Type>(), (List<object>) null);
      PXFilterRow[] array = this.GenerateFilters(item, list).ToArray<PXFilterRow>();
      int num1 = 0;
      int num2 = 0;
      List<object> objectList = itemView.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, array, ref num1, 0, ref num2);
      this.results.Clear();
      this.TraceResult(rule, list, objectList.Count);
      return objectList;
    }
    catch (Exception ex)
    {
      isSuccessful = false;
      PXTrace.WriteInformation(ex);
      return (List<object>) null;
    }
  }

  private IEnumerable<ApproveInfo> GetEmployeesByFilter(
    Table item,
    EPRule rule,
    List<EPRuleBaseCondition> conditions)
  {
    EPAssignmentHelper<Table> assignmentHelper = this;
    System.Type type = typeof (Select5<EPEmployee, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<EPEmployee.defAddressID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>>>, Aggregate<GroupBy<EPEmployee.bAccountID>>>);
    PXView pxView = new PXView((PXGraph) assignmentHelper, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      type
    }));
    PXFilterRow[] array = assignmentHelper.GenerateFilters(item, conditions).ToArray<PXFilterRow>();
    int num1 = 0;
    int num2 = 0;
    using (new PXFieldScope(pxView, Array.Empty<System.Type>()))
    {
      foreach (PXResult<EPEmployee, PX.Objects.CR.Address, PX.Objects.CR.Contact> pxResult in pxView.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, array, ref num1, 0, ref num2).Select<object, PXResult<EPEmployee, PX.Objects.CR.Address, PX.Objects.CR.Contact>>((Func<object, PXResult<EPEmployee, PX.Objects.CR.Address, PX.Objects.CR.Contact>>) (_ => (PXResult<EPEmployee, PX.Objects.CR.Address, PX.Objects.CR.Contact>) _)))
      {
        PX.Objects.CR.Contact contact = PXResult<EPEmployee, PX.Objects.CR.Address, PX.Objects.CR.Contact>.op_Implicit(pxResult);
        yield return new ApproveInfo()
        {
          OwnerID = contact.ContactID,
          WorkgroupID = rule.WorkgroupID,
          RuleID = rule.RuleID,
          StepID = rule.StepID,
          WaitTime = rule.WaitTime
        };
      }
    }
  }

  private IEnumerable GenerateFilters(Table item, List<EPRuleBaseCondition> conditions)
  {
    EPAssignmentHelper<Table> assignmentHelper = this;
    foreach (EPRuleBaseCondition condition in conditions)
    {
      System.Type type = GraphHelper.GetType(condition.Entity);
      PXFilterRow filter;
      if (condition.Entity.Equals(typeof (EPEmployee).FullName, StringComparison.InvariantCultureIgnoreCase) && condition.FieldName.Equals(typeof (BAccount.workgroupID).Name, StringComparison.InvariantCultureIgnoreCase))
      {
        object current = assignmentHelper._Graph.Caches[type].Current;
        filter = assignmentHelper.CheckEmployeeInWorkgroup((EPEmployee) current, condition);
      }
      else
      {
        bool flag = !string.IsNullOrWhiteSpace(condition.FieldName) && condition.FieldName.Contains("_Attributes");
        if (flag)
          PXDBAttributeAttribute.Activate(((PXGraph) assignmentHelper).Caches[type]);
        object obj1 = condition.Value == null ? (object) null : (condition.IsField.GetValueOrDefault() ? assignmentHelper.EvaluateField(item, condition.Value) : (flag ? (object) AttrValue(condition.Value) : (object) condition.Value));
        object obj2 = condition.Value2 == null ? (object) null : (condition.IsField.GetValueOrDefault() ? assignmentHelper.EvaluateField(item, condition.Value2) : (flag ? (object) AttrValue(condition.Value2) : (object) condition.Value2));
        filter = new PXFilterRow()
        {
          DataField = $"{type.Name}__{condition.FieldName}",
          Condition = (PXCondition) condition.Condition.GetValueOrDefault(),
          Value = obj1,
          Value2 = obj2
        };
      }
      PXCache cach = assignmentHelper._Graph.Caches[type];
      PXFilterRow pxFilterRow1 = filter;
      int? nullable = condition.OpenBrackets;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      pxFilterRow1.OpenBrackets = valueOrDefault1;
      PXFilterRow pxFilterRow2 = filter;
      nullable = condition.CloseBrackets;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      pxFilterRow2.CloseBrackets = valueOrDefault2;
      PXFilterRow pxFilterRow3 = filter;
      nullable = condition.Operator;
      int num = nullable.GetValueOrDefault() == 1 ? 1 : 0;
      pxFilterRow3.OrOperator = num != 0;
      yield return (object) filter;
    }

    static string AttrValue(string value)
    {
      DateTime result;
      return DateTime.TryParse(value, out result) ? result.ToString("yyyy-MM-dd HH:mm:ss.fff", (IFormatProvider) CultureInfo.InvariantCulture) : value;
    }
  }

  private void TraceResult(EPRule rule, List<EPRuleBaseCondition> conditions, int count)
  {
    PXTrace.WriteInformation("{0} - {1} :: {2} - {3}", new object[4]
    {
      (object) rule.StepName,
      (object) rule.Name,
      (object) count,
      count > 0 ? (object) "Satisfied" : (object) "Unsatisfied"
    });
    string str = (string) null;
    foreach (EPRuleBaseCondition condition in conditions)
      str += $"{GraphHelper.GetType(condition.Entity).Name} - {condition.FieldName} - {PXEnumDescriptionAttribute.GetInfo(typeof (PXCondition), (object) (PXCondition) condition.Condition.GetValueOrDefault()).Value} - {condition.Value} | {condition.Value2}\n";
    PXTrace.WriteInformation("Conditions:\n{0}", new object[1]
    {
      (object) str
    });
  }

  private static System.Type CreateResultView(PXGraph graph, List<EPRuleBaseCondition> conditions)
  {
    List<System.Type> source = new List<System.Type>();
    List<System.Type> typeList = new List<System.Type>();
    HashSet<System.Type> typeSet = new HashSet<System.Type>();
    foreach (EPRuleBaseCondition condition in conditions)
    {
      System.Type type = GraphHelper.GetType(condition.Entity);
      if (condition.FieldName.EndsWith("_Attributes"))
        typeSet.Add(type);
      if (!source.Contains(type))
        source.Add(type);
    }
    foreach (System.Type type1 in typeSet)
    {
      if (source.Count<System.Type>((Func<System.Type, bool>) (t => t == typeof (CSAnswers))) == 0)
        source.Add(typeof (CSAnswers));
      System.Type type2 = BqlCommand.Compose(new System.Type[7]
      {
        typeof (PX.Data.Select<,>),
        typeof (CSAnswers),
        typeof (Where<,>),
        typeof (CSAnswers.refNoteID),
        typeof (Equal<>),
        typeof (Current<>),
        type1.GetNestedType("noteID")
      });
      PXView pxView = new PXView(graph, false, BqlCommand.CreateInstance(new System.Type[1]
      {
        type2
      }));
      graph.Views.Add($"tmp__{type1.FullName}_CSAnswers", pxView);
    }
    for (int index = 0; index < source.Count; ++index)
    {
      System.Type type = source[index];
      PXDBAttributeAttribute.Activate(graph.Caches[type]);
      if (index == 0)
        typeList.Add(type);
      else if (index != source.Count - 1)
      {
        typeList.Add(typeof (LeftJoin<,,>));
        typeList.Add(type);
        typeList.Add(typeof (On<True, Equal<True>>));
      }
      else
      {
        typeList.Add(typeof (LeftJoin<,>));
        typeList.Add(type);
        typeList.Add(typeof (On<True, Equal<True>>));
      }
    }
    typeList.Insert(0, typeList.Count == 1 ? typeof (PX.Data.Select<>) : typeof (Select2<,>));
    return BqlCommand.Compose(typeList.ToArray());
  }

  private void Select(
    Table item,
    PXView itemView,
    List<System.Type> Tables,
    List<object> pars,
    int depth = 0)
  {
    if (pars == null)
      pars = new List<object>();
    MethodInfo method = BqlCommand.Compose(new System.Type[2]
    {
      typeof (EPApprovalView<>),
      Tables[depth]
    }).GetMethod(nameof (Select), BindingFlags.Static | BindingFlags.Public);
    IList listOfSelectResults = this.GetListOfSelectResults(item, itemView, Tables, depth, method);
    ++depth;
    foreach (PXResult pxResult1 in (IEnumerable) listOfSelectResults)
    {
      pars.Add(pxResult1[0]);
      if (depth == Tables.Count)
      {
        PXResult pxResult2 = Tables.Count == 1 ? pxResult1 : (PXResult) itemView.CreateResult(pars.ToArray());
        if (!this.results.Contains(pxResult2))
          this.results.Add(pxResult2);
      }
      else
        this.Select(item, itemView, Tables, pars, depth);
      pars.RemoveAt(depth - 1);
    }
    --depth;
  }

  private IList GetListOfSelectResults(
    Table item,
    PXView itemView,
    List<System.Type> Tables,
    int depth,
    MethodInfo select)
  {
    IList list;
    if (!typeof (Table).IsAssignableFrom(Tables[depth]))
      list = (IList) select.Invoke((object) this, new object[2]
      {
        (object) this.processGraph,
        null
      });
    else
      list = (IList) new PXResult<Table>(item).SingleToList<PXResult<Table>>();
    IList listOfSelectResults = list;
    if (listOfSelectResults.Count == 0)
    {
      listOfSelectResults = (IList) new List<PXResult>();
      listOfSelectResults.Add((object) (PXResult) itemView.CreateResult(new object[Tables.Count]));
    }
    return listOfSelectResults;
  }

  private PXFilterRow CheckEmployeeInWorkgroup(EPEmployee employee, EPRuleBaseCondition rule)
  {
    PXFilterRow pxFilterRow = new PXFilterRow()
    {
      DataField = typeof (EPCompanyTree.description).Name,
      Condition = (PXCondition) rule.Condition.Value,
      Value = (object) rule.Value,
      Value2 = (object) null
    };
    PXSelectJoin<EPCompanyTree, InnerJoin<EPCompanyTreeMember, On<EPCompanyTree.workGroupID, Equal<EPCompanyTreeMember.workGroupID>>>, Where<EPCompanyTreeMember.contactID, Equal<Required<EPEmployee.defContactID>>, And<EPCompanyTreeMember.active, Equal<True>>>> pxSelectJoin = new PXSelectJoin<EPCompanyTree, InnerJoin<EPCompanyTreeMember, On<EPCompanyTree.workGroupID, Equal<EPCompanyTreeMember.workGroupID>>>, Where<EPCompanyTreeMember.contactID, Equal<Required<EPEmployee.defContactID>>, And<EPCompanyTreeMember.active, Equal<True>>>>((PXGraph) this);
    int num1 = 0;
    int num2 = 0;
    List<object> objectList = ((PXSelectBase) pxSelectJoin).View.Select((object[]) null, new object[1]
    {
      (object) employee.DefContactID
    }, (object[]) null, (string[]) null, (bool[]) null, new PXFilterRow[1]
    {
      pxFilterRow
    }, ref num1, 1, ref num2);
    return new PXFilterRow()
    {
      DataField = $"{typeof (EPEmployee).Name}__{typeof (EPEmployee.bAccountID).Name}",
      Condition = objectList.Count > 0 ? (PXCondition) 12 : (PXCondition) 11,
      Value = (object) null,
      Value2 = (object) null
    };
  }

  private ApproveInfo CreateApproveInfo(int? OwnerID, int? WorkgroupID, EPRule rule)
  {
    this.FillOwnerWorkgroup(ref OwnerID, ref WorkgroupID);
    if (!OwnerID.HasValue && !WorkgroupID.HasValue)
      return (ApproveInfo) null;
    return new ApproveInfo()
    {
      OwnerID = OwnerID,
      WorkgroupID = WorkgroupID,
      RuleID = rule.RuleID,
      StepID = rule.StepID,
      WaitTime = rule.WaitTime
    };
  }

  private object EvaluateField(Table item, string field)
  {
    return this._Graph.Views[this._Graph.PrimaryView].Cache.GetValue((object) item, field) ?? (object) field;
  }

  private IEnumerable getItemRecord() => (IEnumerable) this.results;
}
