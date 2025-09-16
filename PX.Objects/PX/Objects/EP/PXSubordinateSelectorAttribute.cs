// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXSubordinateSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.TM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// Allow show employees which are subordinated of coworkers for logined employee.
/// You can specify additional filter for EPEmployee records.
/// It's a 'BIZACCT' dimension selector.
/// </summary>
/// <example>[PXSubordinateSelector]</example>
public class PXSubordinateSelectorAttribute : PXAggregateAttribute
{
  public System.Type DescriptionField
  {
    get => this.GetAttribute<PXSelectorAttribute>().DescriptionField;
    set => this.GetAttribute<PXSelectorAttribute>().DescriptionField = value;
  }

  public System.Type SubstituteKey
  {
    get => this.GetAttribute<PXSelectorAttribute>().SubstituteKey;
    set => this.GetAttribute<PXSelectorAttribute>().SubstituteKey = value;
  }

  public PXSubordinateSelectorAttribute(System.Type where)
    : this("BIZACCT", PXSubordinateSelectorAttribute.GetCommand(where), true, true)
  {
  }

  protected PXSubordinateSelectorAttribute(
    string DimensionName,
    System.Type seach,
    bool validCombo,
    bool restrictInactiveEmployee)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute(DimensionName)
    {
      ValidComboRequired = validCombo
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(seach, new System.Type[4]
    {
      typeof (CREmployee.acctCD),
      typeof (CREmployee.bAccountID),
      typeof (CREmployee.acctName),
      typeof (CREmployee.departmentID)
    })
    {
      SubstituteKey = typeof (CREmployee.acctCD),
      DescriptionField = typeof (CREmployee.acctName)
    });
    if (!restrictInactiveEmployee)
      return;
    this.AddInactiveEmplRestriction();
  }

  public PXSubordinateSelectorAttribute()
    : this((System.Type) null)
  {
  }

  protected PXSubordinateSelectorAttribute(
    PXDimensionAttribute dimAttr,
    PXSelectorAttribute selAttr,
    bool restrictInactiveEmployee)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) dimAttr);
    this._Attributes.Add((PXEventSubscriberAttribute) selAttr);
    if (!restrictInactiveEmployee)
      return;
    this.AddInactiveEmplRestriction();
  }

  private static System.Type GetCommand(System.Type where)
  {
    System.Type type = typeof (Where<CREmployee.userID, Equal<Current<AccessInfo.userID>>, Or<EPCompanyTreeMember.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>);
    if (where != (System.Type) null)
      type = BqlCommand.Compose(new System.Type[4]
      {
        typeof (Where2<,>),
        typeof (Where<CREmployee.userID, Equal<Current<AccessInfo.userID>>, Or<EPCompanyTreeMember.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>),
        typeof (And<>),
        where
      });
    return BqlCommand.Compose(new System.Type[5]
    {
      typeof (Search5<,,,>),
      typeof (CREmployee.bAccountID),
      typeof (LeftJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.contactID, Equal<CREmployee.defContactID>>>),
      type,
      typeof (Aggregate<GroupBy<CREmployee.acctCD>>)
    });
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatingEvents fieldUpdating1 = sender.Graph.FieldUpdating;
    System.Type itemType1 = sender.GetItemType();
    string fieldName1 = ((PXEventSubscriberAttribute) this)._FieldName;
    PXSelectorAttribute attribute = this.GetAttribute<PXSelectorAttribute>();
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) attribute, __vmethodptr(attribute, SubstituteKeyFieldUpdating));
    fieldUpdating1.RemoveHandler(itemType1, fieldName1, pxFieldUpdating1);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = sender.Graph.FieldUpdating;
    System.Type itemType2 = sender.GetItemType();
    string fieldName2 = ((PXEventSubscriberAttribute) this)._FieldName;
    PXSubordinateSelectorAttribute selectorAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) selectorAttribute, __vmethodptr(selectorAttribute, FieldUpdating));
    fieldUpdating2.AddHandler(itemType2, fieldName2, pxFieldUpdating2);
  }

  protected virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    CREmployee crEmployee = PXResultset<CREmployee>.op_Implicit(PXSelectBase<CREmployee, PXSelect<CREmployee, Where<CREmployee.acctCD, Equal<Required<CREmployee.acctCD>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
    {
      e.NewValue
    }));
    if (crEmployee != null)
    {
      e.NewValue = (object) crEmployee.BAccountID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PXDimensionAttribute attribute1 = this.GetAttribute<PXDimensionAttribute>();
      // ISSUE: virtual method pointer
      new PXFieldUpdating((object) attribute1, __vmethodptr(attribute1, FieldUpdating)).Invoke(sender, e);
      ((CancelEventArgs) e).Cancel = false;
      PXSelectorAttribute attribute2 = this.GetAttribute<PXSelectorAttribute>();
      // ISSUE: virtual method pointer
      new PXFieldUpdating((object) attribute2, __vmethodptr(attribute2, SubstituteKeyFieldUpdating)).Invoke(sender, e);
    }
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    if (!(typeof (ISubscriber) != typeof (IPXFieldUpdatingSubscriber)) || !(typeof (ISubscriber) != typeof (IPXRowPersistingSubscriber)) || !(typeof (ISubscriber) != typeof (IPXFieldDefaultingSubscriber)))
      return;
    base.GetSubscriber<ISubscriber>(subscribers);
  }

  private void AddInactiveEmplRestriction()
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<CREmployee.vStatus, NotEqual<VendorStatus.inactive>>), "The status of employee '{0}' is '{1}'.", new System.Type[2]
    {
      typeof (CREmployee.acctCD),
      typeof (CREmployee.vStatus)
    }));
  }

  public static bool IsSubordinated(PXGraph graph, object employeeId)
  {
    BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
    {
      PXSubordinateSelectorAttribute.GetCommand((System.Type) null)
    });
    PXFilterRow[] pxFilterRowArray1 = new PXFilterRow[1]
    {
      new PXFilterRow()
      {
        DataField = typeof (CREmployee.bAccountID).Name,
        Condition = (PXCondition) 0,
        Value = employeeId
      }
    };
    PXView pxView = new PXView(graph, true, instance);
    int num1 = 0;
    int num2 = 0;
    PXFilterRow[] pxFilterRowArray2 = pxFilterRowArray1;
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    return pxView.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, pxFilterRowArray2, ref local1, 1, ref local2).Return<List<object>, int>((Func<List<object>, int>) (_ => _.Count<object>()), 0) > 0;
  }
}
