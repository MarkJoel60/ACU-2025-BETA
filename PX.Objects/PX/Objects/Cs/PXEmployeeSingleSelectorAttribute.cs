// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXEmployeeSingleSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CS;

public class PXEmployeeSingleSelectorAttribute : PXAggregateAttribute
{
  public Type DescriptionField
  {
    get => this.GetAttribute<PXSelectorAttribute>().DescriptionField;
    set => this.GetAttribute<PXSelectorAttribute>().DescriptionField = value;
  }

  public Type SubstituteKey
  {
    get => this.GetAttribute<PXSelectorAttribute>().SubstituteKey;
    set => this.GetAttribute<PXSelectorAttribute>().SubstituteKey = value;
  }

  public PXEmployeeSingleSelectorAttribute()
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute("BIZACCT")
    {
      ValidComboRequired = true
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(PXEmployeeSingleSelectorAttribute.GetCommand(), new Type[9]
    {
      typeof (EPEmployee.acctCD),
      typeof (EPEmployee.bAccountID),
      typeof (EPEmployee.acctName),
      typeof (EPEmployee.classID),
      typeof (EPEmployeePosition.positionID),
      typeof (EPEmployee.departmentID),
      typeof (EPEmployee.defLocationID),
      typeof (Users.username),
      typeof (Users.displayName)
    })
    {
      SubstituteKey = typeof (EPEmployee.acctCD),
      DescriptionField = typeof (EPEmployee.acctName)
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<EPEmployee.vStatus, NotEqual<VendorStatus.inactive>>), "The status of employee '{0}' is '{1}'.", new Type[2]
    {
      typeof (EPEmployee.acctCD),
      typeof (EPEmployee.vStatus)
    }));
  }

  private static Type GetCommand()
  {
    return BqlCommand.Compose(new Type[5]
    {
      typeof (Search5<,,,>),
      typeof (EPEmployee.bAccountID),
      typeof (LeftJoin<Users, On<Users.pKID, Equal<EPEmployee.userID>>, LeftJoin<EMailSyncAccountPreferences, On<EMailSyncAccountPreferences.employeeID, Equal<EPEmployee.bAccountID>, And<EMailSyncAccountPreferences.employeeID, NotEqual<Optional<EMailSyncAccountPreferences.employeeID>>>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>>>>),
      typeof (Where<EMailSyncAccountPreferences.policyName, IsNull>),
      typeof (Aggregate<GroupBy<EPEmployee.acctCD>>)
    });
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatingEvents fieldUpdating1 = sender.Graph.FieldUpdating;
    Type itemType1 = sender.GetItemType();
    string fieldName1 = ((PXEventSubscriberAttribute) this)._FieldName;
    PXSelectorAttribute attribute = this.GetAttribute<PXSelectorAttribute>();
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) attribute, __vmethodptr(attribute, SubstituteKeyFieldUpdating));
    fieldUpdating1.RemoveHandler(itemType1, fieldName1, pxFieldUpdating1);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = sender.Graph.FieldUpdating;
    Type itemType2 = sender.GetItemType();
    string fieldName2 = ((PXEventSubscriberAttribute) this)._FieldName;
    PXEmployeeSingleSelectorAttribute selectorAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) selectorAttribute, __vmethodptr(selectorAttribute, FieldUpdating));
    fieldUpdating2.AddHandler(itemType2, fieldName2, pxFieldUpdating2);
  }

  protected virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.acctCD, Equal<Required<EPEmployee.acctCD>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
    {
      e.NewValue
    }));
    if (epEmployee != null)
    {
      e.NewValue = (object) epEmployee.BAccountID;
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
}
