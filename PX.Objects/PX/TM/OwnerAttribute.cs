// Decompiled with JetBrains decompiler
// Type: PX.TM.OwnerAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Metadata;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.TM;

/// <summary>Allows show Contacts for specified work group.</summary>
/// <example>[Owner(typeof(MyDac.myWorkgroupField)]</example>
[PXDBInt]
[PXInt]
[PXUIField(DisplayName = "Owner")]
public class OwnerAttribute : PXEntityAttribute, IPXFieldUpdatingSubscriber
{
  private static 
  #nullable disable
  Dictionary<System.Type, List<string>> FieldList = new Dictionary<System.Type, List<string>>();
  protected System.Type _workgroupType;
  protected System.Type _substituteKey;

  [InjectDependencyOnTypeLevel]
  protected IMacroVariablesManager MacroVariablesManager { get; set; }

  public OwnerAttribute()
    : this((System.Type) null)
  {
  }

  public OwnerAttribute(System.Type workgroupType)
    : this(workgroupType, (System.Type) null)
  {
  }

  public OwnerAttribute(
    System.Type workgroupType,
    System.Type search,
    bool validateValue = true,
    bool inquiryMode = false,
    System.Type[] fieldList = null,
    string[] headerList = null,
    System.Type substituteKey = null,
    PXSelectorMode selectorMode = 16 /*0x10*/)
  {
    this._workgroupType = workgroupType;
    this._substituteKey = substituteKey;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) this.CreateSelector(search, fieldList, headerList, validateValue, selectorMode));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    if (inquiryMode)
      return;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<OwnerAttribute.Owner.employeeStatus, IsNull, Or<OwnerAttribute.Owner.employeeStatus, NotEqual<VendorStatus.inactive>>>), "The status of employee '{0}' is '{1}'.", new System.Type[2]
    {
      typeof (OwnerAttribute.Owner.acctCD),
      typeof (OwnerAttribute.Owner.employeeStatus)
    }));
  }

  protected virtual PXSelectorAttribute CreateSelector(
    System.Type search,
    System.Type[] fieldList,
    string[] headerList,
    bool validateValue,
    PXSelectorMode selectorMode)
  {
    System.Type type = search;
    if ((object) type == null)
      type = this.CreateSelect(this._workgroupType);
    System.Type[] typeArray = fieldList;
    if (typeArray == null)
      typeArray = new System.Type[12]
      {
        typeof (OwnerAttribute.Owner.displayName),
        typeof (OwnerAttribute.Owner.salutation),
        typeof (OwnerAttribute.Owner.eMail),
        typeof (OwnerAttribute.Owner.phone1),
        typeof (OwnerAttribute.Owner.departmentID),
        typeof (OwnerAttribute.Owner.acctCD),
        typeof (OwnerAttribute.Owner.employeeStatus),
        typeof (OwnerAttribute.Owner.employeeParentBAccountID),
        typeof (OwnerAttribute.Owner.supervisorID),
        typeof (OwnerAttribute.Owner.supervisorName),
        typeof (OwnerAttribute.Owner.contactType),
        typeof (OwnerAttribute.Owner.userID)
      };
    PXSelectorAttribute selector = new PXSelectorAttribute(type, typeArray);
    PXSelectorAttribute selectorAttribute = selector;
    string[] strArray = headerList;
    if (strArray == null)
      strArray = new string[12]
      {
        "Contact",
        "Job Title",
        "Email",
        "Phone 1",
        "Department",
        "Employee ID",
        "Status",
        "Branch",
        "Reports To (ID)",
        "Reports To (Name)",
        "Type",
        "User ID"
      };
    selectorAttribute.Headers = strArray;
    selector.DescriptionField = typeof (OwnerAttribute.Owner.displayName);
    selector.SelectorMode = selectorMode;
    selector.ValidateValue = validateValue;
    if (this._substituteKey != (System.Type) null)
      selector.SubstituteKey = this._substituteKey;
    return selector;
  }

  protected virtual System.Type CreateSelect(System.Type workgroupType)
  {
    return workgroupType == (System.Type) null ? typeof (FbqlSelect<SelectFromBase<OwnerAttribute.Owner, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<OwnerAttribute.Owner.contactType, IBqlString>.IsEqual<ContactTypesAttribute.employee>>, OwnerAttribute.Owner>.SearchFor<OwnerAttribute.Owner.contactID>) : ((IBqlTemplate) BqlTemplate.OfCommand<Search2<OwnerAttribute.Owner.contactID, LeftJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.contactID, Equal<OwnerAttribute.Owner.contactID>, And<EPCompanyTreeMember.workGroupID, Equal<Optional<BqlPlaceholder.A>>>>>, Where2<Where<Optional<BqlPlaceholder.A>, IsNull, Or<EPCompanyTreeMember.contactID, IsNotNull>>, And<OwnerAttribute.Owner.contactType, Equal<ContactTypesAttribute.employee>>>>>.Replace<BqlPlaceholder.A>(workgroupType)).ToType();
  }

  public virtual void CacheAttached(PXCache sender)
  {
    PXGraph.FieldUpdatingEvents fieldUpdating = sender.Graph.FieldUpdating;
    System.Type itemType1 = sender.GetItemType();
    string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
    OwnerAttribute ownerAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) ownerAttribute1, __vmethodptr(ownerAttribute1, FieldUpdating));
    fieldUpdating.AddHandler(itemType1, fieldName, pxFieldUpdating);
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (!(this._workgroupType != (System.Type) null))
      return;
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    System.Type itemType2 = BqlCommand.GetItemType(this._workgroupType);
    string name = this._workgroupType.Name;
    OwnerAttribute ownerAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) ownerAttribute2, __vmethodptr(ownerAttribute2, Workgroup_FieldVerifying));
    fieldVerifying.AddHandler(itemType2, name, pxFieldVerifying);
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    string newValue = e.NewValue as string;
    if (this.MacroVariablesManager == null || !this.MacroVariablesManager.IsVariable(newValue) || !(newValue == "@me"))
      return;
    e.NewValue = (object) PXAccess.GetContactID();
  }

  protected virtual void Workgroup_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null)
      return;
    int? newValue = (int?) e.NewValue;
    int? nullable1 = (int?) sender.GetValue(e.Row, this._workgroupType.Name);
    int? OwnerID = sender.GetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) is int result || int.TryParse(sender.GetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) as string, out result) ? new int?(result) : (sender.GetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) is PXFieldState valueExt1 ? valueExt1.Value : (object) null) as int?;
    if (!(((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] is PXSelectorAttribute))
      return;
    int? nullable2 = newValue;
    int? nullable3 = nullable1;
    if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue || OwnerAttribute.BelongsToWorkGroup(sender.Graph, newValue, OwnerID))
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) OwnerAttribute.OwnerWorkGroup(sender.Graph, newValue));
    sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) (int?) (sender.GetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) is PXFieldState valueExt2 ? valueExt2.Value : (object) null));
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

  protected virtual void SetBqlTable(System.Type bqlTable)
  {
    ((PXAggregateAttribute) this).SetBqlTable(bqlTable);
    lock (((ICollection) OwnerAttribute.FieldList).SyncRoot)
    {
      List<string> stringList;
      if (!OwnerAttribute.FieldList.TryGetValue(bqlTable, out stringList))
        OwnerAttribute.FieldList[bqlTable] = stringList = new List<string>();
      if (stringList.Contains(this.FieldName))
        return;
      stringList.Add(this.FieldName);
    }
  }

  /// <exclude />
  public static List<string> GetFields(System.Type table)
  {
    DacMetadata.InitializationCompleted.Wait();
    List<string> stringList;
    return OwnerAttribute.FieldList.TryGetValue(table, out stringList) ? stringList : new List<string>();
  }

  public BqlCommand GetSelect() => this.NonDimensionSelectorAttribute.GetSelect();

  [PXHidden]
  [PXBreakInheritance]
  [PXProjection(typeof (Select2<PX.Objects.CR.Contact, LeftJoin<PX.Objects.CR.Standalone.EPEmployee, On<PX.Objects.CR.Standalone.EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>, LeftJoin<UserPreferences, On<UserPreferences.userID, Equal<PX.Objects.CR.Contact.userID>>>>>), Persistent = false)]
  [Serializable]
  public class Owner : PX.Objects.CR.Contact
  {
    [PXDBString(30, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.EPEmployee.departmentID))]
    [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
    [PXUIField]
    public virtual string DepartmentID { get; set; }

    [PXDimension("BIZACCT")]
    [PXDBString(30, IsUnicode = true, InputMask = "", BqlField = typeof (PX.Objects.CR.Standalone.EPEmployee.acctCD))]
    [PXUIField]
    public virtual string AcctCD { get; set; }

    [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.EPEmployee.vStatus))]
    [PXUIField(DisplayName = "Status")]
    [VendorStatus.List]
    public virtual string EmployeeStatus { get; set; }

    [BAccount(DisplayName = "Branch", BqlField = typeof (PX.Objects.CR.Standalone.EPEmployee.parentBAccountID))]
    public virtual int? EmployeeParentBAccountID { get; set; }

    [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.EPEmployee.bAccountID))]
    [PXUIField(Visible = false)]
    public virtual int? EmployeeBAccountID { get; set; }

    [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.EPEmployee.supervisorID))]
    [PXEPEmployeeSelector]
    [PXUIField]
    public virtual int? SupervisorID { get; set; }

    [PXString]
    [PXFormula(typeof (Selector<OwnerAttribute.Owner.supervisorID, PX.Objects.CR.Standalone.EPEmployee.acctName>))]
    [PXUIField]
    public virtual string SupervisorName { get; set; }

    [PXDBGuid(false, BqlField = typeof (PX.Objects.CR.Standalone.EPEmployee.userID))]
    public virtual Guid? EmployeeUserID { get; set; }

    [PXDBString(BqlField = typeof (UserPreferences.mailSignature))]
    [PXUIField(DisplayName = "Email Signature")]
    public virtual string MailSignature { get; set; }

    public new abstract class contactID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OwnerAttribute.Owner.contactID>
    {
    }

    public new abstract class displayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OwnerAttribute.Owner.displayName>
    {
    }

    public new abstract class salutation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OwnerAttribute.Owner.salutation>
    {
    }

    public new abstract class eMail : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OwnerAttribute.Owner.eMail>
    {
    }

    public new abstract class phone1 : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OwnerAttribute.Owner.phone1>
    {
    }

    public new abstract class contactType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OwnerAttribute.Owner.contactType>
    {
    }

    public new abstract class userID : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OwnerAttribute.Owner.userID>
    {
    }

    public abstract class departmentID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OwnerAttribute.Owner.departmentID>
    {
    }

    public abstract class acctCD : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OwnerAttribute.Owner.acctCD>
    {
    }

    public abstract class employeeStatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OwnerAttribute.Owner.employeeStatus>
    {
    }

    public abstract class employeeParentBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      OwnerAttribute.Owner.employeeParentBAccountID>
    {
    }

    public abstract class employeeBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      OwnerAttribute.Owner.employeeBAccountID>
    {
    }

    public abstract class supervisorID : IBqlField, IBqlOperand
    {
    }

    public abstract class supervisorName : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      OwnerAttribute.Owner.supervisorName>
    {
    }

    public abstract class employeeUserID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      OwnerAttribute.Owner.employeeUserID>
    {
    }

    public abstract class mailSignature : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OwnerAttribute.Owner.mailSignature>
    {
    }
  }
}
