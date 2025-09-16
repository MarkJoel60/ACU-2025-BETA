// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.PXOwnerSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.SM;
using PX.TM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

public class PXOwnerSelectorAttribute : PXAggregateAttribute
{
  protected readonly int _SelAttrIndex;
  protected System.Type _workgroupType;

  public PXOwnerSelectorAttribute()
    : this((System.Type) null)
  {
  }

  public PXOwnerSelectorAttribute(System.Type workgroupType)
    : this(workgroupType, (System.Type) null)
  {
  }

  protected PXOwnerSelectorAttribute(
    System.Type workgroupType,
    System.Type search,
    bool validateValue = true,
    bool inquiryMode = false)
  {
    PXAggregateAttribute.AggregatedAttributesCollection attributes = this._Attributes;
    System.Type type = search;
    if ((object) type == null)
      type = PXOwnerSelectorAttribute.CreateSelect(workgroupType);
    System.Type[] typeArray = new System.Type[3]
    {
      typeof (PX.TM.PXOwnerSelectorAttribute.EPEmployee.acctName),
      typeof (PX.TM.PXOwnerSelectorAttribute.EPEmployee.acctCD),
      typeof (PX.TM.PXOwnerSelectorAttribute.EPEmployee.departmentID)
    };
    PXOwnerSelectorAttribute.OwnerSubstituteSelectorAttribute selectorAttribute1;
    PXSelectorAttribute selectorAttribute2 = (PXSelectorAttribute) (selectorAttribute1 = new PXOwnerSelectorAttribute.OwnerSubstituteSelectorAttribute(type, typeArray));
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) this._Attributes).Count - 1;
    selectorAttribute2.DescriptionField = typeof (PX.TM.PXOwnerSelectorAttribute.EPEmployee.acctName);
    selectorAttribute2.SubstituteKey = typeof (PX.TM.PXOwnerSelectorAttribute.EPEmployee.acctCD);
    selectorAttribute2.ValidateValue = validateValue;
    selectorAttribute2.CacheGlobal = true;
    this._workgroupType = workgroupType;
    if (inquiryMode)
      return;
    this._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PX.TM.PXOwnerSelectorAttribute.EPEmployee.vStatus, IsNull, Or<PX.TM.PXOwnerSelectorAttribute.EPEmployee.vStatus, NotEqual<VendorStatus.inactive>>>), "The status of employee '{0}' is '{1}'.", new System.Type[2]
    {
      typeof (PX.TM.PXOwnerSelectorAttribute.EPEmployee.acctCD),
      typeof (PX.TM.PXOwnerSelectorAttribute.EPEmployee.vStatus)
    }));
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this._workgroupType != (System.Type) null))
      return;
    PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
    System.Type itemType1 = sender.GetItemType();
    PXOwnerSelectorAttribute selectorAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) selectorAttribute1, __vmethodptr(selectorAttribute1, RowUpdated));
    rowUpdated.AddHandler(itemType1, pxRowUpdated);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    System.Type itemType2 = BqlCommand.GetItemType(this._workgroupType);
    string name = this._workgroupType.Name;
    PXOwnerSelectorAttribute selectorAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) selectorAttribute2, __vmethodptr(selectorAttribute2, FieldVerifying));
    fieldVerifying.AddHandler(itemType2, name, pxFieldVerifying);
  }

  private static System.Type CreateSelect(System.Type workgroupType)
  {
    if (workgroupType == (System.Type) null)
      return typeof (Search<PX.TM.PXOwnerSelectorAttribute.EPEmployee.defContactID, Where<PX.TM.PXOwnerSelectorAttribute.EPEmployee.acctCD, IsNotNull>>);
    return BqlCommand.Compose(new System.Type[17]
    {
      typeof (Search2<,,>),
      typeof (PX.TM.PXOwnerSelectorAttribute.EPEmployee.defContactID),
      typeof (LeftJoin<,>),
      typeof (EPCompanyTreeMember),
      typeof (On<,,>),
      typeof (EPCompanyTreeMember.contactID),
      typeof (Equal<PX.TM.PXOwnerSelectorAttribute.EPEmployee.defContactID>),
      typeof (And<,>),
      typeof (EPCompanyTreeMember.workGroupID),
      typeof (Equal<>),
      typeof (Optional<>),
      workgroupType,
      typeof (Where<,,>),
      typeof (Optional<>),
      workgroupType,
      typeof (IsNull),
      typeof (Or<EPCompanyTreeMember.contactID, IsNotNull>)
    });
  }

  protected virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
  }

  protected virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null)
      return;
    int? newValue1 = (int?) e.NewValue;
    int? nullable1 = (int?) sender.GetValue(e.Row, this._workgroupType.Name);
    if (!(sender.GetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) is string valuePending))
      valuePending = (sender.GetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) is PXFieldState valueExt1 ? valueExt1.Value : (object) null) as string;
    string str = valuePending;
    if (!(this._Attributes[this._SelAttrIndex] is PXSelectorAttribute attribute))
      return;
    object copy = sender.CreateCopy(e.Row);
    PXFieldUpdatingEventArgs updatingEventArgs = new PXFieldUpdatingEventArgs(copy, (object) str);
    attribute.SubstituteKeyFieldUpdating(sender, updatingEventArgs);
    int? newValue2 = updatingEventArgs.NewValue as int?;
    int? nullable2 = newValue1;
    int? nullable3 = nullable1;
    if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue || PXOwnerSelectorAttribute.BelongsToWorkGroup(sender.Graph, newValue1, newValue2))
      return;
    sender.SetValue(copy, ((PXEventSubscriberAttribute) this)._FieldName, (object) PXOwnerSelectorAttribute.OwnerWorkGroup(sender.Graph, newValue1));
    sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, sender.GetValueExt(copy, ((PXEventSubscriberAttribute) this)._FieldName) is PXFieldState valueExt2 ? (object) (string) valueExt2.Value : (object) (string) (object) null);
  }

  public static bool BelongsToWorkGroup(PXGraph graph, int? WorkGroupID, int? OwnerID)
  {
    if (!WorkGroupID.HasValue && OwnerID.HasValue)
      return true;
    if (WorkGroupID.HasValue && !OwnerID.HasValue)
      return false;
    return PXSelectBase<EPCompanyTreeMember, PXSelect<EPCompanyTreeMember, Where<EPCompanyTreeMember.workGroupID, Equal<Required<EPCompanyTreeMember.workGroupID>>, And<EPCompanyTreeMember.contactID, Equal<Required<EPCompanyTreeMember.contactID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) WorkGroupID,
      (object) OwnerID
    }).Count > 0;
  }

  public static int? OwnerWorkGroup(PXGraph graph, int? WorkGroupID)
  {
    return PXResultset<EPCompanyTreeMember>.op_Implicit(PXSelectBase<EPCompanyTreeMember, PXSelect<EPCompanyTreeMember, Where<EPCompanyTreeMember.workGroupID, Equal<Required<EPCompanyTreeMember.workGroupID>>, And<EPCompanyTreeMember.isOwner, Equal<Required<EPCompanyTreeMember.isOwner>>>>>.Config>.Select(graph, new object[2]
    {
      (object) WorkGroupID,
      (object) 1
    }))?.ContactID;
  }

  public static int? DefaultWorkgroup(PXGraph graph, int? contactID)
  {
    return ((PXSelectBase<EPCompanyTreeMember>) new PXSelectJoin<EPCompanyTreeMember, InnerJoin<EPCompanyTreeH, On<EPCompanyTreeMember.workGroupID, Equal<EPCompanyTreeH.workGroupID>>>, Where<EPCompanyTreeMember.contactID, Equal<Required<EPCompanyTreeMember.contactID>>>>(graph)).SelectSingle(new object[1]
    {
      (object) (contactID ?? graph.Accessinfo.ContactID)
    })?.WorkGroupID;
  }

  public class OwnerSubstituteSelectorAttribute(System.Type type, params System.Type[] fieldList) : 
    PXSelectorAttribute(type, fieldList)
  {
    public virtual void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      Guid result;
      if (e.NewValue != null && Guid.TryParse(e.NewValue.ToString(), out result))
      {
        PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Required<PX.Objects.EP.EPEmployee.userID>>>>.Config>.SelectSingleBound(sender.Graph, new object[0], new object[1]
        {
          (object) result
        }));
        e.NewValue = (object) (int?) epEmployee?.DefContactID ?? e.NewValue;
      }
      if (e.NewValue == null || int.TryParse(e.NewValue.ToString(), out int _) && !e.NewValue.ToString().StartsWith("0"))
        return;
      base.SubstituteKeyFieldUpdating(sender, e);
    }

    public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      Guid result;
      if (e.ReturnValue != null && Guid.TryParse(e.ReturnValue.ToString(), out result))
      {
        PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Required<PX.Objects.EP.EPEmployee.userID>>>>.Config>.SelectSingleBound(sender.Graph, new object[0], new object[1]
        {
          (object) result
        }));
        e.ReturnValue = (object) (int?) epEmployee?.DefContactID ?? e.ReturnValue;
      }
      else
        base.SubstituteKeyFieldSelecting(sender, e);
    }

    public virtual void DescriptionFieldSelecting(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      string alias)
    {
      object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
      base.DescriptionFieldSelecting(sender, e, alias);
      if (obj == null || e.ReturnValue != null)
        return;
      using (new PXReadDeletedScope(false))
      {
        Users users = PXResultset<Users>.op_Implicit(PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Required<Users.pKID>>>>.Config>.SelectSingleBound(sender.Graph, new object[0], new object[1]
        {
          obj
        }));
        e.ReturnValue = users != null ? (object) users.DisplayName : obj;
        e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(true), new int?(), new int?(), new int?(), (object) null, alias, (string) null, (string) null, PXLocalizer.Localize("The owner is not found. Change the owner or make sure that the specified employee is associated with a user.", typeof (PX.Objects.CR.Messages).FullName), (PXErrorLevel) 2, new bool?(false), new bool?(), new bool?(), (PXUIVisibility) 3, (string) null, (string[]) null, (string[]) null);
      }
    }
  }
}
