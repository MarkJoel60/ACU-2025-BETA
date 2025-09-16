// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.SM;
using PX.SM.Email;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Case Class")]
[PXPrimaryGraph(typeof (CRCaseClassMaint))]
[Serializable]
public class CRCaseClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _LabourItemID;
  protected int? _OvertimeItemID;
  protected int? _RoundingInMinutes;
  protected int? _MinBillTimeInMinutes;
  protected int? _ReopenCaseTimeInDays;
  protected bool? _IsInternal;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (CRCaseClass.caseClassID), DescriptionField = typeof (CRCaseClass.description))]
  public virtual string CaseClassID { get; set; }

  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>
  /// The work calendar is used for the commitments of this Case Class.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.CSCalendar.CalendarID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<CSCalendar, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CSCalendar.calendarID, IBqlString>.IsEqual<CSCalendar.calendarID.defaultCalendarID>>))]
  [PXUIField(DisplayName = "Work Calendar")]
  [PXSelector(typeof (CSCalendar.calendarID), DescriptionField = typeof (CSCalendar.description))]
  [PXForeignReference(typeof (CRCaseClass.FK.WorkCalendar))]
  public virtual string CalendarID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billable")]
  [PXUIEnabled(typeof (Where<BqlOperand<CRCaseClass.perItemBilling, IBqlInt>.IsEqual<BillingTypeListAttribute.perCase>>))]
  [UndefaultFormula(typeof (Switch<Case<Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perActivity>>, False, Case<Where<Editable<CRCaseClass.requireCustomer>, Equal<True>, And<CRCaseClass.allowEmployeeAsContact, Equal<True>>>, False>>, CRCaseClass.isBillable>))]
  public virtual bool? IsBillable { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Billable Option Override")]
  [PXUIEnabled(typeof (Where<BqlOperand<CRCaseClass.perItemBilling, IBqlInt>.IsEqual<BillingTypeListAttribute.perCase>>))]
  [UndefaultFormula(typeof (Switch<Case<Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perActivity>>, False, Case<Where<Editable<CRCaseClass.requireCustomer>, Equal<True>, And<CRCaseClass.allowEmployeeAsContact, Equal<True>>>, False>>, CRCaseClass.allowOverrideBillable>))]
  public virtual bool? AllowOverrideBillable { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Customer")]
  [PXUIEnabled(typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRCaseClass.isBillable, Equal<False>>>>, And<BqlOperand<CRCaseClass.allowOverrideBillable, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<CRCaseClass.requireContract, IBqlBool>.IsEqual<False>>>))]
  [UndefaultFormula(typeof (Switch<Case<Where<Editable<CRCaseClass.requireCustomer>, Equal<True>, And<CRCaseClass.allowEmployeeAsContact, Equal<True>>>, False, Case<Where<CRCaseClass.isBillable, Equal<True>, Or<CRCaseClass.allowOverrideBillable, Equal<True>>>, True, Case<Where<CRCaseClass.requireContract, Equal<True>>, True>>>, CRCaseClass.requireCustomer>))]
  public virtual bool? RequireCustomer { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Contact")]
  public virtual bool? RequireContact { get; set; }

  /// <summary>
  /// Specifies that the Business Account associated with this case class is of the type <see cref="F:PX.Objects.CR.BAccountType.VendorType" />.
  /// If both RequireVendor and  <see cref="P:PX.Objects.CR.CRCaseClass.RequireCustomer" /> are set to true, the associated Business Account must be of the type <see cref="F:PX.Objects.CR.BAccountType.CombinedType" />.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Vendor")]
  public virtual bool? RequireVendor { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Selecting Employee as Case Contact")]
  [UndefaultFormula(typeof (Switch<Case<Where<Editable<CRCaseClass.requireCustomer>, Equal<False>, And<CRCaseClass.allowEmployeeAsContact, Equal<True>>>, False>, CRCaseClass.allowEmployeeAsContact>))]
  public virtual bool? AllowEmployeeAsContact { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perActivity>>, True>, Current<CRCaseClass.requireContract>>))]
  [PXUIEnabled(typeof (Where<CRCaseClass.perItemBilling, NotEqual<BillingTypeListAttribute.perActivity>>))]
  [PXUIField(DisplayName = "Require Contract")]
  public virtual bool? RequireContract { get; set; }

  /// <summary>
  /// Defines the behavior of Closure Notes field on Closing Case pop up window. If check box is selected then Closure Note is required to record information.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Case Closure Notes")]
  public virtual bool? RequireClosureNotes { get; set; }

  [PXDBInt]
  [BillingTypeList]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Billing Mode")]
  public virtual int? PerItemBilling { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Labor Item", Required = false)]
  [PXDimensionSelector("INVENTORY", typeof (Search<InventoryItem.inventoryID, Where<InventoryItem.itemType, Equal<INItemTypes.laborItem>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>>), typeof (InventoryItem.inventoryCD), DescriptionField = typeof (InventoryItem.descr))]
  [PXForeignReference(typeof (Field<CRCaseClass.labourItemID>.IsRelatedTo<InventoryItem.inventoryID>))]
  [PXUIRequired(typeof (Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perCase>, And<Where<CRCaseClass.isBillable, Equal<True>, Or<CRCaseClass.allowOverrideBillable, Equal<True>>>>>))]
  [PXUIEnabled(typeof (Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perCase>>))]
  [PXFormula(typeof (Switch<Case<Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perActivity>>, Null>, Current<CRCaseClass.labourItemID>>))]
  public virtual int? LabourItemID
  {
    get => this._LabourItemID;
    set => this._LabourItemID = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Overtime Labor Item", Required = false)]
  [PXDimensionSelector("INVENTORY", typeof (Search<InventoryItem.inventoryID, Where<InventoryItem.itemType, Equal<INItemTypes.laborItem>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>>), typeof (InventoryItem.inventoryCD), DescriptionField = typeof (InventoryItem.descr))]
  [PXForeignReference(typeof (Field<CRCaseClass.overtimeItemID>.IsRelatedTo<InventoryItem.inventoryID>))]
  [PXUIRequired(typeof (Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perCase>, And<Where<CRCaseClass.isBillable, Equal<True>, Or<CRCaseClass.allowOverrideBillable, Equal<True>>>>>))]
  [PXUIEnabled(typeof (Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perCase>>))]
  [PXFormula(typeof (Switch<Case<Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perActivity>>, Null>, Current<CRCaseClass.overtimeItemID>>))]
  public virtual int? OvertimeItemID
  {
    get => this._OvertimeItemID;
    set => this._OvertimeItemID = value;
  }

  [EmailAccountRaw]
  [PXForeignReference]
  public virtual int? DefaultEMailAccountID { get; set; }

  /// <summary>
  /// The flag is ised for an ability to track the resolution time in activities marked as a solution.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Track Solutions in Activities")]
  [PXDefault(false)]
  public virtual bool? TrackSolutionsInActivities { get; set; }

  /// <summary>
  /// This field contains the condition for case resolution time calculation.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Stop Counting Time", FieldClass = "CaseCommitmentsTracking")]
  [PXDefault(0)]
  [CRCaseClass.stopTimeCounterType.List]
  public virtual int? StopTimeCounterType { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Round Time By")]
  [PXTimeList(5, 6)]
  public virtual int? RoundingInMinutes
  {
    get => this._RoundingInMinutes;
    set => this._RoundingInMinutes = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXTimeList(5, 12)]
  [PXUIField]
  public virtual int? MinBillTimeInMinutes
  {
    get => this._MinBillTimeInMinutes;
    set => this._MinBillTimeInMinutes = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Days Allowed to Reopen Case")]
  public virtual int? ReopenCaseTimeInDays
  {
    get => this._ReopenCaseTimeInDays;
    set => this._ReopenCaseTimeInDays = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsInternal
  {
    get => this._IsInternal;
    set => this._IsInternal = value;
  }

  /// <summary>
  /// When the system sends out an email or creates an activity with the <see cref="P:PX.Objects.EP.EPActivityType.Application">Originated By</see> value set to
  /// <see cref="F:PX.Objects.CR.PXActivityApplicationAttribute.System">System</see> in the <i>Activity Types (CR102000)</i> screen,
  /// and the <see cref="P:PX.Objects.CR.CRCaseClass.IncludeSystemActivitiesResponseTimeCalculation">Include System Activities in Response Time Calculation</see>
  /// check-box is cleared, the system should not reset the <see cref="P:PX.Objects.CR.CRCaseCommitments.InitialResponseDueDateTime">Initial Response Due</see>
  /// or <see cref="P:PX.Objects.CR.CRCaseCommitments.ResponseDueDateTime">Response Due</see> timelines
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IncludeSystemActivitiesResponseTimeCalculation { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<CRCaseClass>.By<CRCaseClass.caseClassID>
  {
    public static CRCaseClass Find(PXGraph graph, string caseClassID, PKFindOptions options = 0)
    {
      return (CRCaseClass) PrimaryKeyOf<CRCaseClass>.By<CRCaseClass.caseClassID>.FindBy(graph, (object) caseClassID, options);
    }
  }

  public static class FK
  {
    public class DefaultEmailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<CRCaseClass>.By<CRCaseClass.defaultEMailAccountID>
    {
    }

    public class LabourItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<CRCaseClass>.By<CRCaseClass.labourItemID>
    {
    }

    public class OvertimeItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<CRCaseClass>.By<CRCaseClass.overtimeItemID>
    {
    }

    public class WorkCalendar : 
      PrimaryKeyOf<CSCalendar>.By<CSCalendar.calendarID>.ForeignKeyOf<CRCaseClass>.By<CRCaseClass.calendarID>
    {
    }
  }

  public abstract class caseClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCaseClass.caseClassID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCaseClass.description>
  {
  }

  public abstract class calendarID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCaseClass.calendarID>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCaseClass.isBillable>
  {
  }

  public abstract class allowOverrideBillable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRCaseClass.allowOverrideBillable>
  {
  }

  public abstract class requireCustomer : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCaseClass.requireCustomer>
  {
  }

  public abstract class requireContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCaseClass.requireContact>
  {
  }

  public abstract class requireVendor : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCaseClass.requireVendor>
  {
  }

  public abstract class allowEmployeeAsContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRCaseClass.allowEmployeeAsContact>
  {
  }

  public abstract class requireContract : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCaseClass.requireContract>
  {
  }

  public abstract class requireClosureNotes : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRCaseClass.requireClosureNotes>
  {
  }

  public abstract class perItemBilling : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCaseClass.perItemBilling>
  {
  }

  public abstract class labourItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCaseClass.labourItemID>
  {
  }

  public abstract class overtimeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCaseClass.overtimeItemID>
  {
  }

  public abstract class defaultEMailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCaseClass.defaultEMailAccountID>
  {
    public class EmailAccountRule : 
      EMailAccount.userID.PreventMakingPersonalIfUsedAsSystem<SelectFromBase<CRCaseClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<CRCaseClass.defaultEMailAccountID>.IsRelatedTo<EMailAccount.emailAccountID>.AsSimpleKey.WithTablesOf<EMailAccount, CRCaseClass>, EMailAccount, CRCaseClass>.SameAsCurrent>>
    {
    }
  }

  public abstract class trackSolutionsInActivities : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRCaseClass.trackSolutionsInActivities>
  {
  }

  public abstract class stopTimeCounterType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCaseClass.stopTimeCounterType>
  {
    public const int CaseDeactivated = 0;
    public const int CaseSolutionProvidedInActivity = 1;

    public class caseDeactivated : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CRCaseClass.stopTimeCounterType.caseDeactivated>
    {
      public caseDeactivated()
        : base(0)
      {
      }
    }

    public class caseSolutionProvidedInActivity : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CRCaseClass.stopTimeCounterType.caseSolutionProvidedInActivity>
    {
      public caseSolutionProvidedInActivity()
        : base(1)
      {
      }
    }

    public class ListAttribute : PXIntListAttribute
    {
      public ListAttribute()
        : base(new (int, string)[2]
        {
          (0, "If Case Becomes Inactive"),
          (1, "If Case Solution Is Provided in Activity")
        })
      {
      }
    }
  }

  public abstract class roundingInMinutes : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCaseClass.roundingInMinutes>
  {
  }

  public abstract class minBillTimeInMinutes : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCaseClass.minBillTimeInMinutes>
  {
  }

  public abstract class reopenCaseTimeInDays : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCaseClass.reopenCaseTimeInDays>
  {
  }

  public abstract class isInternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCaseClass.isInternal>
  {
  }

  public abstract class includeSystemActivitiesResponseTimeCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRCaseClass.includeSystemActivitiesResponseTimeCalculation>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCaseClass.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRCaseClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCaseClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCaseClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRCaseClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCaseClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseClass.lastModifiedDateTime>
  {
  }
}
