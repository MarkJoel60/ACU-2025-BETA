// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentProcessHelper`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Wiki.Parser;
using PX.Objects.CS;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

/// <summary>Implements the Workgroup assignment process logic.</summary>
public class EPAssignmentProcessHelper<Table> : PXGraph<EPAssignmentProcessHelper<Table>> where Table : class, IBqlTable
{
  public PXSelect<PX.Objects.CR.BAccount> baccount;
  public PXSelect<PX.Objects.AR.Customer> customer;
  public PXSelect<PX.Objects.AP.Vendor> vendor;
  public PXSelect<EPEmployee> employee;
  public PXView attributeView;
  /// <summary>
  /// This list is used to track the path passed to check and prevent cyclic references.
  /// </summary>
  private readonly List<int> path = new List<int>();
  private PXGraph processGraph;
  private System.Type processMapType;
  private IBqlTable currentItem;
  private CSAnswers currentAttribute;
  private readonly PXGraph _Graph;

  public EPAssignmentProcessHelper(PXGraph graph)
    : this()
  {
    this._Graph = graph;
  }

  public EPAssignmentProcessHelper()
  {
    // ISSUE: method pointer
    this.attributeView = new PXView((PXGraph) this, false, (BqlCommand) new Select<CSAnswers>(), (Delegate) new PXSelectDelegate((object) this, __methodptr(getAttributeRecord)));
  }

  /// <summary>
  /// Assigns Owner and Workgroup to the given IAssign instance based on the assigmentment rules.
  /// </summary>
  /// <param name="item">IAssign object</param>
  /// <param name="assignmentMapID">Assignment map</param>
  /// <returns>True if workgroup was assigned; otherwise false</returns>
  /// <remarks>
  /// You have to manualy persist the IAssign object to save the changes.
  /// </remarks>
  public virtual bool Assign(Table item, int? assignmentMapID)
  {
    if ((object) item == null)
      throw new ArgumentNullException(nameof (item));
    int? nullable = assignmentMapID;
    int num1 = 0;
    if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
      throw new ArgumentOutOfRangeException(nameof (assignmentMapID));
    this.path.Clear();
    EPAssignmentMap map = PXResultset<EPAssignmentMap>.op_Implicit(PXSelectBase<EPAssignmentMap, PXSelect<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) assignmentMapID
    }));
    if (map == null)
      return false;
    int? mapType = map.MapType;
    int num2 = 0;
    if (!(mapType.GetValueOrDefault() == num2 & mapType.HasValue))
      throw new ArgumentOutOfRangeException(nameof (assignmentMapID));
    IEnumerable<ApproveInfo> source = this.Assign(item, map, false);
    return source != null && source.Any<ApproveInfo>();
  }

  public virtual IEnumerable<ApproveInfo> Assign(Table item, EPAssignmentMap map, bool isApprove)
  {
    this.path.Clear();
    this.processMapType = GraphHelper.GetType(map.EntityType);
    System.Type type1 = item.GetType();
    PXResultset<EPAssignmentRoute> routes = ((PXSelectBase<EPAssignmentRoute>) new PXSelectReadonly<EPAssignmentRoute, Where<EPAssignmentRoute.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>, And<EPAssignmentRoute.parent, IsNull>>, OrderBy<Asc<EPAssignmentRoute.sequence>>>((PXGraph) this)).Select(new object[2]
    {
      (object) map.AssignmentMapID,
      null
    });
    System.Type type2 = GraphHelper.GetType(map.GraphType);
    if (type2 == (System.Type) null)
    {
      ((PXGraph) this).Caches[type1].Current = (object) item;
      type2 = EntityHelper.GetPrimaryGraphType((PXGraph) this, this.processMapType);
    }
    if (this._Graph != null && type2.IsAssignableFrom(this._Graph.GetType()))
    {
      this.processGraph = this._Graph;
    }
    else
    {
      System.Type type3 = GraphHelper.GetType(map.GraphType);
      if ((object) type3 == null)
        type3 = type2;
      this.processGraph = PXGraph.CreateInstance(type3);
    }
    if (this.processGraph != null && this.processMapType != (System.Type) null)
    {
      if (this.processMapType.IsAssignableFrom(type1))
      {
        this.processGraph.Caches[type1].Current = (object) item;
      }
      else
      {
        if (!type1.IsAssignableFrom(this.processMapType))
          return (IEnumerable<ApproveInfo>) null;
        object instance = this.processGraph.Caches[this.processMapType].CreateInstance();
        ((PXCache) Activator.CreateInstance(typeof (PXCache<>).MakeGenericType(type1), (object) this)).RestoreCopy(instance, (object) item);
        this.processGraph.Caches[this.processMapType].Current = instance;
      }
    }
    List<ApproveInfo> list = this.ProcessLevel(item, map.AssignmentMapID, routes).ToList<ApproveInfo>();
    if (list.Any<ApproveInfo>())
      return (IEnumerable<ApproveInfo>) list;
    PXTrace.WriteWarning("No approver has been assigned to the document. The document is automatically approved.");
    throw new RequestApproveException();
  }

  private IEnumerable<ApproveInfo> ProcessLevel(
    Table item,
    int? assignmentMap,
    PXResultset<EPAssignmentRoute> routes)
  {
    PXSelectReadonly<EPAssignmentRoute, Where<EPAssignmentRoute.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>, And<EPAssignmentRoute.parent, Equal<Required<EPAssignmentRoute.assignmentRouteID>>>>, OrderBy<Asc<EPAssignmentRoute.sequence>>> pxSelectReadonly = new PXSelectReadonly<EPAssignmentRoute, Where<EPAssignmentRoute.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>, And<EPAssignmentRoute.parent, Equal<Required<EPAssignmentRoute.assignmentRouteID>>>>, OrderBy<Asc<EPAssignmentRoute.sequence>>>((PXGraph) this);
    foreach (PXResult<EPAssignmentRoute> route1 in routes)
    {
      EPAssignmentRoute route2 = PXResult<EPAssignmentRoute>.op_Implicit(route1);
      int? nullable1 = route2.AssignmentRouteID;
      if (nullable1.HasValue)
      {
        List<int> path1 = this.path;
        nullable1 = route2.AssignmentRouteID;
        int num1 = nullable1.Value;
        path1.Add(num1);
        if (this.IsPassed(item, route2))
        {
          nullable1 = route2.WorkgroupID;
          if (!nullable1.HasValue)
          {
            nullable1 = route2.OwnerID;
            if (!nullable1.HasValue && route2.OwnerSource == null)
            {
              nullable1 = route2.RouteID;
              if (nullable1.HasValue)
              {
                List<int> path2 = this.path;
                nullable1 = route2.RouteID;
                int num2 = nullable1.Value;
                if (!path2.Contains(num2))
                  return this.ProcessLevel(item, assignmentMap, PXSelectBase<EPAssignmentRoute, PXSelectReadonly<EPAssignmentRoute>.Config>.Search<EPAssignmentRoute.assignmentRouteID>((PXGraph) this, (object) route2.RouteID, Array.Empty<object>()));
              }
              PXResultset<EPAssignmentRoute> routes1 = ((PXSelectBase<EPAssignmentRoute>) pxSelectReadonly).Select(new object[2]
              {
                (object) assignmentMap,
                (object) route2.AssignmentRouteID
              });
              return this.ProcessLevel(item, assignmentMap, routes1);
            }
          }
          int? nullable2 = new int?();
          int? nullable3 = route2.WorkgroupID;
          if (route2.OwnerSource != null)
          {
            PXGraph processGraph = this.processGraph;
            string s = PXTemplateContentParser.Instance.Process(route2.OwnerSource, processGraph, typeof (Table), (object[]) null);
            nullable2 = new int?();
            int result;
            if (int.TryParse(s, out result))
            {
              PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
              {
                (object) result
              }));
              int? nullable4;
              if (contact == null)
              {
                nullable1 = new int?();
                nullable4 = nullable1;
              }
              else
                nullable4 = contact.ContactID;
              nullable2 = nullable4;
            }
            if (!nullable2.HasValue)
            {
              EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.acctCD, Equal<Required<EPEmployee.acctCD>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
              {
                (object) s
              }));
              int? nullable5;
              if (epEmployee == null)
              {
                nullable1 = new int?();
                nullable5 = nullable1;
              }
              else
                nullable5 = epEmployee.DefContactID;
              nullable2 = nullable5;
            }
            if (!nullable2.HasValue)
            {
              EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelectJoin<EPEmployee, InnerJoin<Users, On<Users.pKID, Equal<EPEmployee.userID>>>, Where<Users.username, Equal<Required<Users.username>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
              {
                (object) s
              }));
              int? nullable6;
              if (epEmployee == null)
              {
                nullable1 = new int?();
                nullable6 = nullable1;
              }
              else
                nullable6 = epEmployee.DefContactID;
              nullable2 = nullable6;
            }
          }
          if (!nullable2.HasValue)
            nullable2 = route2.OwnerID;
          if (route2.UseWorkgroupByOwner.GetValueOrDefault() && nullable3.HasValue && nullable2.HasValue)
          {
            EPCompanyTreeMember companyTreeMember = PXResultset<EPCompanyTreeMember>.op_Implicit(PXSelectBase<EPCompanyTreeMember, PXSelectJoin<EPCompanyTreeMember, InnerJoin<EPCompanyTreeH, On<EPCompanyTreeH.workGroupID, Equal<EPCompanyTreeMember.workGroupID>>, InnerJoin<EPEmployee, On<EPEmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>>>, Where<EPCompanyTreeH.parentWGID, Equal<Required<EPCompanyTreeH.parentWGID>>, And<EPCompanyTreeMember.contactID, Equal<Required<EPCompanyTreeMember.contactID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
            {
              (object) nullable3,
              (object) nullable2
            }));
            if (companyTreeMember != null)
              nullable3 = companyTreeMember.WorkGroupID;
          }
          if (nullable3.HasValue && !nullable2.HasValue)
          {
            EPCompanyTreeMember companyTreeMember = PXResultset<EPCompanyTreeMember>.op_Implicit(PXSelectBase<EPCompanyTreeMember, PXSelectJoin<EPCompanyTreeMember, InnerJoin<EPEmployee, On<EPEmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>>, Where<EPCompanyTreeMember.workGroupID, Equal<Required<EPCompanyTreeMember.workGroupID>>, And<EPCompanyTreeMember.isOwner, Equal<boolTrue>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) nullable3
            }));
            if (companyTreeMember != null)
              nullable2 = companyTreeMember.ContactID;
          }
          PXTrace.WriteInformation("Sequence {0} process successfully.", new object[1]
          {
            (object) route2.Sequence
          });
          return EnumerableExtensions.AsSingleEnumerable<ApproveInfo>(new ApproveInfo()
          {
            OwnerID = nullable2,
            WorkgroupID = nullable3,
            WaitTime = route2.WaitTime
          });
        }
      }
    }
    return Enumerable.Empty<ApproveInfo>();
  }

  private bool IsPassed(Table item, EPAssignmentRoute route)
  {
    try
    {
      List<EPAssignmentRule> source = new List<EPAssignmentRule>();
      foreach (PXResult<EPAssignmentRule> pxResult in PXSelectBase<EPAssignmentRule, PXSelectReadonly<EPAssignmentRule, Where<EPAssignmentRule.assignmentRouteID, Equal<Required<EPAssignmentRoute.assignmentRouteID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) route.AssignmentRouteID
      }))
      {
        EPAssignmentRule epAssignmentRule = PXResult<EPAssignmentRule>.op_Implicit(pxResult);
        source.Add(epAssignmentRule);
      }
      if (source.Count == 0)
        return true;
      switch (route.RuleType)
      {
        case "A":
          return source.All<EPAssignmentRule>((Func<EPAssignmentRule, bool>) (rule => this.IsTrue(item, rule)));
        case "T":
          return source.Any<EPAssignmentRule>((Func<EPAssignmentRule, bool>) (rule => this.IsTrue(item, rule)));
        case "F":
          return source.Any<EPAssignmentRule>((Func<EPAssignmentRule, bool>) (rule => !this.IsTrue(item, rule)));
        default:
          return false;
      }
    }
    catch
    {
      return false;
    }
  }

  private bool IsTrue(Table item, EPAssignmentRule rule)
  {
    IBqlTable itemRecord = this.GetItemRecord(rule, (IBqlTable) item);
    return !rule.FieldName.EndsWith("_Attributes") ? this.IsItemRuleTrue(itemRecord, rule) : this.IsAttributeRuleTrue((object) itemRecord, rule);
  }

  private IBqlTable GetItemRecord(EPAssignmentRule rule, IBqlTable item)
  {
    PXGraph processGraph = this.processGraph;
    System.Type type1 = item.GetType();
    System.Type type2 = GraphHelper.GetType(rule.Entity);
    if (type2.IsAssignableFrom(type1))
      return item;
    if (this.processMapType.IsAssignableFrom(type2) && processGraph != null)
      return processGraph.Caches[this.processMapType].Current as IBqlTable;
    if (processGraph != null)
    {
      foreach (CacheEntityItem cacheEntityItem in EMailSourceHelper.TemplateEntity((PXGraph) this, (string) null, item.GetType().FullName, processGraph.GetType().FullName))
      {
        System.Type type3 = GraphHelper.GetType(cacheEntityItem.SubKey);
        if (type2.IsAssignableFrom(type3) && ((Dictionary<string, PXView>) processGraph.Views).ContainsKey(cacheEntityItem.Key))
        {
          object obj = processGraph.Views[cacheEntityItem.Key].SelectSingleBound(new object[1]
          {
            (object) item
          }, Array.Empty<object>());
          return (obj is PXResult ? ((PXResult) obj)[0] : obj) as IBqlTable;
        }
      }
    }
    return item;
  }

  private bool IsItemRuleTrue(IBqlTable item, EPAssignmentRule rule)
  {
    if (item == null)
      return false;
    if (item is EPEmployee && rule.FieldName.Equals(typeof (PX.Objects.CR.BAccount.workgroupID).Name, StringComparison.InvariantCultureIgnoreCase))
      return this.IsEmployeeInWorkgroup((EPEmployee) item, rule);
    this.currentItem = item;
    // ISSUE: method pointer
    PXView pxView = new PXView((PXGraph) this, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      BqlCommand.Compose(new System.Type[2]
      {
        typeof (Select<>),
        item.GetType()
      })
    }), (Delegate) new PXSelectDelegate((object) this, __methodptr(getItemRecord)));
    if (!rule.Condition.HasValue)
      return false;
    PXFilterRow pxFilterRow = new PXFilterRow(rule.FieldName, (PXCondition) rule.Condition.Value, this.GetFieldValue(item, rule.FieldName, rule.FieldValue), (object) null);
    int num1 = 0;
    int num2 = 0;
    return pxView.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, new PXFilterRow[1]
    {
      pxFilterRow
    }, ref num1, 1, ref num2).Count > 0;
  }

  private bool IsEmployeeInWorkgroup(EPEmployee employee, EPAssignmentRule rule)
  {
    PXFilterRow pxFilterRow = new PXFilterRow(typeof (EPCompanyTree.description).Name, (PXCondition) rule.Condition.Value, (object) rule.FieldValue, (object) null);
    PXSelectJoin<EPCompanyTree, InnerJoin<EPCompanyTreeMember, On<EPCompanyTree.workGroupID, Equal<EPCompanyTreeMember.workGroupID>>>, Where<EPCompanyTreeMember.contactID, Equal<Required<EPEmployee.defContactID>>, And<EPCompanyTreeMember.active, Equal<True>>>> pxSelectJoin = new PXSelectJoin<EPCompanyTree, InnerJoin<EPCompanyTreeMember, On<EPCompanyTree.workGroupID, Equal<EPCompanyTreeMember.workGroupID>>>, Where<EPCompanyTreeMember.contactID, Equal<Required<EPEmployee.defContactID>>, And<EPCompanyTreeMember.active, Equal<True>>>>((PXGraph) this);
    int num1 = 0;
    int num2 = 0;
    return ((PXSelectBase) pxSelectJoin).View.Select((object[]) null, new object[1]
    {
      (object) employee.DefContactID
    }, (object[]) null, (string[]) null, (bool[]) null, new PXFilterRow[1]
    {
      pxFilterRow
    }, ref num1, 1, ref num2).Count > 0;
  }

  private bool IsAttributeRuleTrue(object item, EPAssignmentRule rule)
  {
    string str = rule.FieldName.Substring(0, rule.FieldName.Length - "_Attribute".Length - 1);
    if (PXResultset<CSAttribute>.op_Implicit(PXSelectBase<CSAttribute, PXSelectReadonly<CSAttribute>.Config>.Search<CSAttribute.attributeID>((PXGraph) this, (object) str, Array.Empty<object>())) != null)
    {
      int? condition = rule.Condition;
      if (condition.HasValue)
      {
        Guid? entityNoteId = new EntityHelper(this._Graph).GetEntityNoteID(item);
        CSAnswers csAnswers = PXResultset<CSAnswers>.op_Implicit(PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<CSAnswers.refNoteID>>, And<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>>>>.Config>.Select(this._Graph ?? (PXGraph) this, new object[2]
        {
          (object) entityNoteId,
          (object) str
        }));
        if (csAnswers == null)
        {
          condition = rule.Condition;
          switch (condition.Value)
          {
            case 0:
              return string.IsNullOrEmpty(rule.FieldValue);
            case 1:
              return !string.IsNullOrEmpty(rule.FieldValue);
            case 11:
              return true;
            case 12:
              return false;
            default:
              return false;
          }
        }
        else
        {
          this.currentAttribute = csAnswers;
          string name = typeof (CSAnswers.value).Name;
          condition = rule.Condition;
          int num1 = condition.Value;
          string fieldValue = rule.FieldValue;
          PXFilterRow pxFilterRow = new PXFilterRow(name, (PXCondition) num1, (object) fieldValue, (object) null);
          int num2 = 0;
          int num3 = 0;
          return this.attributeView.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, new PXFilterRow[1]
          {
            pxFilterRow
          }, ref num2, 1, ref num3).Count > 0;
        }
      }
    }
    return false;
  }

  private IEnumerable getItemRecord()
  {
    yield return (object) this.currentItem;
  }

  private IEnumerable getAttributeRecord()
  {
    yield return (object) this.currentAttribute;
  }

  private object GetFieldValue(IBqlTable item, string fieldname, string fieldvalue = null)
  {
    try
    {
      PXCache cach = ((PXGraph) this).Caches[item.GetType()];
      object copy = cach.CreateCopy((object) item);
      if (fieldvalue != null)
      {
        cach.SetValueExt(copy, fieldname, (object) fieldvalue);
      }
      else
      {
        object obj;
        cach.RaiseFieldDefaulting(fieldname, copy, ref obj);
        cach.SetValue(copy, fieldname, obj);
      }
      return cach.GetValueExt(copy, fieldname);
    }
    catch (Exception ex)
    {
      return (object) fieldvalue;
    }
  }
}
