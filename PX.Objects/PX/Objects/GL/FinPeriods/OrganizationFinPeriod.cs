// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.OrganizationFinPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.GL.FinPeriods;

[PXProjection(typeof (Select<FinPeriod, Where<FinPeriod.organizationID, NotEqual<FinPeriod.organizationID.masterValue>>>), Persistent = true)]
[Serializable]
public class OrganizationFinPeriod : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IFinPeriod,
  IPeriod,
  IPeriodSetup
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _FinYear;
  protected string _Descr;
  protected string _PeriodNbr;
  protected bool? _APClosed;
  protected bool? _ARClosed;
  protected bool? _INClosed;
  protected bool? _CAClosed;
  protected bool? _FAClosed;
  protected bool? _PRClosed;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected bool? _Custom;
  protected bool? _DateLocked;
  protected DateTime? _FinDate;
  protected int? _Length;
  protected bool? _IsAdjustment;

  /// <summary>
  /// Indicates whether the record is selected for mass processing.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Organization(true, IsKey = true, BqlTable = typeof (FinPeriod))]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// Key field.
  /// Unique identifier of the Financial Period.
  /// </summary>
  /// 
  ///             Consists of the year and the number of the period in the year. For more information see <see cref="T:PX.Objects.GL.FinPeriodIDAttribute" />
  /// .
  [PXDBString(6, IsKey = true, IsFixed = true, BqlTable = typeof (FinPeriod))]
  [FinPeriodIDFormatting]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (FinPeriod))]
  [PXDefault]
  [PXUIField]
  public virtual string MasterFinPeriodID { get; set; }

  /// <summary>The financial year of the period.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinYear.Year" /> field.
  /// </value>
  [PXDBString(4, IsFixed = true, BqlTable = typeof (FinPeriod))]
  [PXDefault(typeof (OrganizationFinYear.year))]
  [PXUIField]
  [PXParent(typeof (Select<OrganizationFinYear, Where<OrganizationFinYear.organizationID, Equal<Current<OrganizationFinPeriod.organizationID>>, And<OrganizationFinYear.year, Equal<Current<OrganizationFinPeriod.finYear>>>>>))]
  public virtual string FinYear
  {
    get => this._FinYear;
    set => this._FinYear = value;
  }

  /// <summary>The description of the Financial Period.</summary>
  [PXDBLocalizableString(60, IsUnicode = true, BqlTable = typeof (FinPeriod))]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>
  /// The number of the period in the <see cref="P:PX.Objects.GL.FinPeriods.OrganizationFinPeriod.FinYear">financial year</see>.
  /// </summary>
  [PXDBString(2, IsFixed = true, BqlTable = typeof (FinPeriod))]
  [PXDefault("")]
  public virtual string PeriodNbr
  {
    get => this._PeriodNbr;
    set => this._PeriodNbr = value;
  }

  /// <summary>The status of the financial period.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.status.ListAttribute" />.
  /// </value>
  [PXDBString(8, BqlTable = typeof (FinPeriod))]
  [PXUIField]
  [FinPeriod.status.List]
  [PXDefault("Inactive")]
  public virtual string Status { get; set; }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2019R1.")]
  [PXDBBool(BqlTable = typeof (FinPeriod))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active", Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<FinPeriod.status, Equal<FinPeriod.status.open>, Or<FinPeriod.status, Equal<FinPeriod.status.closed>>>, True>, False>))]
  public virtual bool? Active { get; set; }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2019R1.")]
  [PXDBBool(BqlTable = typeof (FinPeriod))]
  [PXDefault(false)]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<FinPeriod.status, Equal<FinPeriod.status.locked>, Or<FinPeriod.status, Equal<FinPeriod.status.closed>>>, True>, False>))]
  public virtual bool? Closed { get; set; }

  /// <summary>
  /// Indicates whether the period is closed in the Accounts Payable module.
  /// When <c>false</c>, the period is active in AP.
  /// </summary>
  [PXDBBool(BqlTable = typeof (FinPeriod))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed in AP", Enabled = false)]
  public virtual bool? APClosed
  {
    get => this._APClosed;
    set => this._APClosed = value;
  }

  /// <summary>
  /// Indicates whether the period is closed in the Accounts Receivable module.
  /// When <c>false</c>, the period is active in AR.
  /// </summary>
  [PXDBBool(BqlTable = typeof (FinPeriod))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed in AR", Enabled = false)]
  public virtual bool? ARClosed
  {
    get => this._ARClosed;
    set => this._ARClosed = value;
  }

  /// <summary>
  /// Indicates whether the period is closed in the Inventory module.
  /// When <c>false</c>, the period is active in IN.
  /// </summary>
  [PXDBBool(BqlTable = typeof (FinPeriod))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed in IN", Enabled = false)]
  public virtual bool? INClosed
  {
    get => this._INClosed;
    set => this._INClosed = value;
  }

  /// <summary>
  /// Indicates whether the period is closed in the Cash Management module.
  /// When <c>false</c>, the period is active in CA.
  /// </summary>
  [PXDBBool(BqlTable = typeof (FinPeriod))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed in CA", Enabled = false)]
  public virtual bool? CAClosed
  {
    get => this._CAClosed;
    set => this._CAClosed = value;
  }

  /// <summary>
  /// Indicates whether the period is closed in the Fixed Assets module.
  /// When <c>false</c>, the period is active in FA.
  /// </summary>
  [PXDBBool(BqlTable = typeof (FinPeriod))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed in FA", Enabled = false)]
  public virtual bool? FAClosed
  {
    get => this._FAClosed;
    set => this._FAClosed = value;
  }

  /// <summary>
  /// Indicates whether the period is closed in the Payroll module.
  /// When <c>false</c>, the period is active in PR.
  /// This field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.PayrollModule">Payroll feature</see> is enabled.
  /// </summary>
  [PXDBBool(BqlTable = typeof (FinPeriod))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed in PR", Enabled = false, FieldClass = "PayrollModule")]
  public virtual bool? PRClosed
  {
    get => this._PRClosed;
    set => this._PRClosed = value;
  }

  /// <summary>
  /// The field used to display and edit the <see cref="P:PX.Objects.GL.FinPeriods.OrganizationFinPeriod.StartDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.GL.FinPeriods.OrganizationFinPeriod.StartDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? StartDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (OrganizationFinPeriod.startDate), typeof (OrganizationFinPeriod.endDate)})] get
    {
      return !this.IsAdjustment.GetValueOrDefault() ? this._StartDate : new DateTime?(this._StartDate.Value.AddDays(-1.0));
    }
    set
    {
      DateTime? nullable1;
      if (value.HasValue && this._EndDate.HasValue)
      {
        DateTime? nullable2 = value;
        DateTime? endDateUi = this.EndDateUI;
        if ((nullable2.HasValue == endDateUi.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == endDateUi.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          nullable1 = new DateTime?(value.Value.AddDays(1.0));
          goto label_4;
        }
      }
      nullable1 = value;
label_4:
      this._StartDate = nullable1;
    }
  }

  /// <summary>
  /// The field used to display and edit the <see cref="P:PX.Objects.GL.FinPeriods.OrganizationFinPeriod.EndDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.GL.FinPeriods.OrganizationFinPeriod.EndDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (OrganizationFinPeriod.endDate)})] get
    {
      ref DateTime? local = ref this._EndDate;
      return !local.HasValue ? new DateTime?() : new DateTime?(local.GetValueOrDefault().AddDays(-1.0));
    }
    set => this._EndDate = value?.AddDays(1.0);
  }

  /// <summary>The start date of the period.</summary>
  [PXDefault]
  [PXDBDate(BqlTable = typeof (FinPeriod))]
  [PXUIField(DisplayName = "Start Date", Enabled = false)]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>The end date of the period (exclusive).</summary>
  [PXDefault]
  [PXDBDate(BqlTable = typeof (FinPeriod))]
  [PXUIField]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  /// <summary>
  /// Indicates whether the start and end dates of the Financial Period are defined by user.
  /// </summary>
  /// <value>
  /// Defaults to the value of the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinYear.CustomPeriods" /> field of the year of the period.
  /// </value>
  [PXDBBool(BqlTable = typeof (FinPeriod))]
  [PXDefault(false)]
  public virtual bool? Custom
  {
    get => this._Custom;
    set => this._Custom = value;
  }

  /// <summary>
  /// When <c>true</c>, indicates that the <see cref="P:PX.Objects.GL.FinPeriods.OrganizationFinPeriod.EndDate" /> of the period is locked and can not be edited.
  /// </summary>
  /// <value>
  /// Note, that if the date is locked for a particular period, it is also impossible to change end dates for the preceding periods.
  /// </value>
  [PXDBBool(BqlTable = typeof (FinPeriod))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Date Locked", Enabled = false, Visible = false)]
  public virtual bool? DateLocked
  {
    get => this._DateLocked;
    set => this._DateLocked = value;
  }

  /// <summary>
  /// The date used for <see cref="!:TaxTran">tax transactions</see> posted in the period.
  /// See the <see cref="!:TaxTran.FinDate" /> field.
  /// </summary>
  /// <value>
  /// The value of this field is calculated from the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.EndDate" /> field.
  /// </value>
  [PXDate]
  [PXUIField]
  [PXDBCalced(typeof (Sub<FinPeriod.endDate, int1>), typeof (DateTime))]
  public virtual DateTime? FinDate
  {
    get => this._FinDate;
    set => this._FinDate = value;
  }

  /// <summary>The read-only length of the period in days.</summary>
  [PXInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Length (Days)", Visible = true, Enabled = false)]
  public virtual int? Length
  {
    get
    {
      if (this._StartDate.HasValue && this._EndDate.HasValue)
      {
        DateTime? startDate = this._StartDate;
        DateTime? endDate1 = this._EndDate;
        if ((startDate.HasValue == endDate1.HasValue ? (startDate.HasValue ? (startDate.GetValueOrDefault() != endDate1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          DateTime? endDate2 = this._EndDate;
          startDate = this._StartDate;
          return new int?((endDate2.HasValue & startDate.HasValue ? new TimeSpan?(endDate2.GetValueOrDefault() - startDate.GetValueOrDefault()) : new TimeSpan?()).Value.Days);
        }
      }
      return new int?(0);
    }
  }

  /// <summary>
  /// Indicates whether the period is an adjustment period - an additional period used to post adjustments.
  /// The adjustment period can be created only if the <see cref="!:FinYear.CustomPeriod" /> option is activated for the corresponding Financial Year.
  /// For more information see the <see cref="P:PX.Objects.GL.FinYearSetup.HasAdjustmentPeriod" /> field and
  /// the documentation for the Financial Year (GL101000) form.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Adjustment Period", Visible = false, Enabled = false)]
  public virtual bool? IsAdjustment
  {
    [PXDependsOnFields(new Type[] {typeof (FinPeriod.startDate), typeof (FinPeriod.endDate)})] get
    {
      int num;
      if (this._StartDate.HasValue && this._EndDate.HasValue)
      {
        DateTime? startDate = this._StartDate;
        DateTime? endDate = this._EndDate;
        num = startDate.HasValue == endDate.HasValue ? (startDate.HasValue ? (startDate.GetValueOrDefault() == endDate.GetValueOrDefault() ? 1 : 0) : 1) : 0;
      }
      else
        num = 0;
      return new bool?(num != 0);
    }
    set => this._IsAdjustment = value;
  }

  [PXNote(BqlTable = typeof (FinPeriod))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp(BqlTable = typeof (FinPeriod))]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID(BqlTable = typeof (FinPeriod))]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlTable = typeof (FinPeriod))]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime(BqlTable = typeof (FinPeriod))]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlTable = typeof (FinPeriod))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlTable = typeof (FinPeriod))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlTable = typeof (FinPeriod))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<OrganizationFinPeriod>.By<OrganizationFinPeriod.organizationID, OrganizationFinPeriod.finPeriodID>
  {
    public static OrganizationFinPeriod Find(
      PXGraph graph,
      int? organizationID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (OrganizationFinPeriod) PrimaryKeyOf<OrganizationFinPeriod>.By<OrganizationFinPeriod.organizationID, OrganizationFinPeriod.finPeriodID>.FindBy(graph, (object) organizationID, (object) finPeriodID, options);
    }

    public static OrganizationFinPeriod FindByBranch(
      PXGraph graph,
      int? branchID,
      string finPeriodID)
    {
      return PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXSelectJoin<OrganizationFinPeriod, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, Where<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>, And<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) finPeriodID,
        (object) branchID
      }));
    }
  }

  public static class FK
  {
    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<OrganizationFinPeriod>.By<OrganizationFinPeriod.organizationID>
    {
    }

    public class FinYear : 
      PrimaryKeyOf<OrganizationFinYear>.By<OrganizationFinYear.organizationID, OrganizationFinYear.year>.ForeignKeyOf<OrganizationFinPeriod>.By<OrganizationFinPeriod.organizationID, OrganizationFinPeriod.finYear>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.selected>
  {
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationFinPeriod.organizationID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriod.finPeriodID>
  {
  }

  public abstract class masterFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriod.masterFinPeriodID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OrganizationFinPeriod.finYear>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OrganizationFinPeriod.descr>
  {
  }

  public abstract class periodNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriod.periodNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OrganizationFinPeriod.status>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.active>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.closed>
  {
  }

  public abstract class aPClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.aPClosed>
  {
  }

  public abstract class aRClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.aRClosed>
  {
  }

  public abstract class iNClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.iNClosed>
  {
  }

  public abstract class cAClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.cAClosed>
  {
  }

  public abstract class fAClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.fAClosed>
  {
  }

  public abstract class pRClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.pRClosed>
  {
  }

  public abstract class startDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinPeriod.startDateUI>
  {
  }

  public abstract class endDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinPeriod.endDateUI>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinPeriod.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinPeriod.endDate>
  {
  }

  public abstract class custom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.custom>
  {
  }

  public abstract class dateLocked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OrganizationFinPeriod.dateLocked>
  {
  }

  public abstract class finDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinPeriod.finDate>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OrganizationFinPeriod.length>
  {
  }

  public abstract class isAdjustment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OrganizationFinPeriod.isAdjustment>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  OrganizationFinPeriod.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  OrganizationFinPeriod.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    OrganizationFinPeriod.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriod.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinPeriod.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    OrganizationFinPeriod.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriod.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinPeriod.lastModifiedDateTime>
  {
  }
}
