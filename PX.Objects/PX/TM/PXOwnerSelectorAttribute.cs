// Decompiled with JetBrains decompiler
// Type: PX.TM.PXOwnerSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.TM;

/// <summary>Allows show employees for specified work group.</summary>
/// <example>[PXOwnerSelector(typeof(MyDac.myField)]</example>
[Obsolete]
public class PXOwnerSelectorAttribute : PXAggregateAttribute
{
  protected readonly int _SelAttrIndex;
  protected 
  #nullable disable
  System.Type _workgroupType;

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
      typeof (PXOwnerSelectorAttribute.EPEmployee.acctName),
      typeof (PXOwnerSelectorAttribute.EPEmployee.acctCD),
      typeof (PXOwnerSelectorAttribute.EPEmployee.departmentID)
    };
    PXOwnerSelectorAttribute.OwnerSubstituteSelectorAttribute selectorAttribute1;
    PXSelectorAttribute selectorAttribute2 = (PXSelectorAttribute) (selectorAttribute1 = new PXOwnerSelectorAttribute.OwnerSubstituteSelectorAttribute(type, typeArray));
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) this._Attributes).Count - 1;
    selectorAttribute2.DescriptionField = typeof (PXOwnerSelectorAttribute.EPEmployee.acctName);
    selectorAttribute2.SubstituteKey = typeof (PXOwnerSelectorAttribute.EPEmployee.acctCD);
    selectorAttribute2.ValidateValue = validateValue;
    selectorAttribute2.CacheGlobal = true;
    this._workgroupType = workgroupType;
    if (inquiryMode)
      return;
    this._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PXOwnerSelectorAttribute.EPEmployee.vStatus, IsNull, Or<PXOwnerSelectorAttribute.EPEmployee.vStatus, NotEqual<VendorStatus.inactive>>>), "The status of employee '{0}' is '{1}'.", new System.Type[2]
    {
      typeof (PXOwnerSelectorAttribute.EPEmployee.acctCD),
      typeof (PXOwnerSelectorAttribute.EPEmployee.vStatus)
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
      return typeof (Search<PXOwnerSelectorAttribute.EPEmployee.pKID, Where<PXOwnerSelectorAttribute.EPEmployee.acctCD, IsNotNull>>);
    return BqlCommand.Compose(new System.Type[17]
    {
      typeof (Search2<,,>),
      typeof (PXOwnerSelectorAttribute.EPEmployee.pKID),
      typeof (LeftJoin<,>),
      typeof (EPCompanyTreeMember),
      typeof (On<,,>),
      typeof (EPCompanyTreeMember.contactID),
      typeof (Equal<PXOwnerSelectorAttribute.EPEmployee.defContactID>),
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
      Guid guid;
      if (e.NewValue == null || GUID.TryParse(e.NewValue.ToString(), ref guid))
        return;
      base.SubstituteKeyFieldUpdating(sender, e);
    }

    public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
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
        e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(true), new int?(), new int?(), new int?(), (object) null, alias, (string) null, (string) null, PXLocalizer.Localize("The owner is not found. Change the owner or make sure that the specified employee is associated with a user.", typeof (Messages).FullName), (PXErrorLevel) 2, new bool?(false), new bool?(), new bool?(), (PXUIVisibility) 3, (string) null, (string[]) null, (string[]) null);
      }
    }
  }

  [PXProjection(typeof (Select<CREmployee, Where<CREmployee.userID, IsNotNull>>), Persistent = false)]
  [CRCacheIndependentPrimaryGraph(typeof (EmployeeMaint), typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<PXOwnerSelectorAttribute.EPEmployee.bAccountID>>>>))]
  [CRCacheIndependentPrimaryGraph(typeof (AccessUsers), typeof (Select<Users, Where<Current<PXOwnerSelectorAttribute.EPEmployee.bAccountID>, IsNull, And<Users.pKID, Equal<Current<PXOwnerSelectorAttribute.EPEmployee.pKID>>>>>))]
  [CRCacheIndependentPrimaryGraph(typeof (EmployeeMaint), typeof (Where<Current<PXOwnerSelectorAttribute.EPEmployee.bAccountID>, IsNull>))]
  [PXHidden]
  [Obsolete]
  public class EPEmployee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Guid? _PKID;
    protected int? _BAccountID;
    protected string _AcctCD;
    protected string _AcctName;
    protected string _DepartmentID;
    protected string _VStatus;
    protected int? _SupervisorID;

    [PXDBGuidMaintainDeleted(BqlField = typeof (CREmployee.userID))]
    [PXDefault]
    [PXUser]
    [PXUIField]
    public virtual Guid? PKID
    {
      get => this._PKID;
      set => this._PKID = value;
    }

    [PXDBIdentity(BqlTable = typeof (CREmployee))]
    [PXUIField]
    public virtual int? BAccountID
    {
      get => this._BAccountID;
      set => this._BAccountID = value;
    }

    [PXDBString(30, IsUnicode = true, InputMask = "", BqlTable = typeof (CREmployee), IsKey = true)]
    [PXUIField]
    public virtual string AcctCD
    {
      get => this._AcctCD;
      set => this._AcctCD = value;
    }

    [PXDBString(60, IsUnicode = true, BqlTable = typeof (CREmployee))]
    [PXUIField(DisplayName = "Employee Name")]
    public virtual string AcctName
    {
      get => this._AcctName;
      set => this._AcctName = value;
    }

    [PXDBString(30, IsUnicode = true, BqlTable = typeof (CREmployee))]
    [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
    [PXUIField]
    public virtual string DepartmentID
    {
      get => this._DepartmentID;
      set => this._DepartmentID = value;
    }

    [PXDBString(1, IsFixed = true, BqlField = typeof (CREmployee.vStatus))]
    [PXUIField(DisplayName = "Status")]
    [VendorStatus.List]
    [PXDefault("A")]
    public virtual string VStatus
    {
      get => this._VStatus;
      set => this._VStatus = value;
    }

    [PXDBInt(BqlField = typeof (CREmployee.defContactID))]
    [PXUIField(DisplayName = "Default Contact")]
    [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<PXOwnerSelectorAttribute.EPEmployee.parentBAccountID>>>>))]
    public virtual int? DefContactID { get; set; }

    [PXDBInt(BqlField = typeof (CREmployee.parentBAccountID))]
    [PXUIField(DisplayName = "Branch")]
    public virtual int? ParentBAccountID { get; set; }

    [PXDBInt(BqlField = typeof (CREmployee.supervisorID))]
    [PXEPEmployeeSelector]
    [PXUIField]
    public virtual int? SupervisorID
    {
      get => this._SupervisorID;
      set => this._SupervisorID = value;
    }

    public abstract class pKID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      PXOwnerSelectorAttribute.EPEmployee.pKID>
    {
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXOwnerSelectorAttribute.EPEmployee.bAccountID>
    {
    }

    public abstract class acctCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXOwnerSelectorAttribute.EPEmployee.acctCD>
    {
    }

    public abstract class acctName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXOwnerSelectorAttribute.EPEmployee.acctName>
    {
    }

    public abstract class departmentID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXOwnerSelectorAttribute.EPEmployee.departmentID>
    {
    }

    public abstract class vStatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXOwnerSelectorAttribute.EPEmployee.vStatus>
    {
    }

    public abstract class defContactID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXOwnerSelectorAttribute.EPEmployee.defContactID>
    {
    }

    public abstract class parentBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXOwnerSelectorAttribute.EPEmployee.parentBAccountID>
    {
    }

    public abstract class supervisorID : IBqlField, IBqlOperand
    {
    }
  }
}
