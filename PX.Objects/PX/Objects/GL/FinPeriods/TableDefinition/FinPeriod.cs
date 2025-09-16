// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Abstractions.Periods;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.GL.FinPeriods.TableDefinition;

[PXCacheName("Financial Period")]
[Serializable]
public class FinPeriod : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IFinPeriod,
  IPeriod,
  IPeriodSetup
{
  protected 
  #nullable disable
  string _FinPeriodID;
  protected string _FinYear;
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
  protected bool? _DateLocked;
  protected bool? _Custom;
  protected DateTime? _FinDate;
  protected int? _Length;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// Indicates whether the record is selected for mass processing.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; } = new bool?(false);

  [PXDBInt(IsKey = true)]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// Key field.
  /// Unique identifier of the Financial Period.
  /// </summary>
  /// 
  ///             Consists of the year and the number of the period in the year. For more information see <see cref="T:PX.Objects.GL.FinPeriodIDAttribute" />
  /// .
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual string MasterFinPeriodID { get; set; }

  /// <summary>The financial year of the period.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinYear.Year" /> field.
  /// </value>
  [PXDBString(4, IsFixed = true)]
  public virtual string FinYear
  {
    get => this._FinYear;
    set => this._FinYear = value;
  }

  /// <summary>The description of the Financial Period.</summary>
  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>
  /// The number of the period in the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.FinYear">financial year</see>.
  /// </summary>
  [PXDBString(2, IsFixed = true)]
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
  [PXDBString(8)]
  [PXUIField]
  [FinPeriod.status.List]
  [PXDefault("Inactive")]
  public virtual string Status { get; set; }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2019R1.")]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active", Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<FinPeriod.status, Equal<FinPeriod.status.open>, Or<FinPeriod.status, Equal<FinPeriod.status.closed>>>, True>, False>))]
  public virtual bool? Active { get; set; }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2019R1.")]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<FinPeriod.status, Equal<FinPeriod.status.locked>, Or<FinPeriod.status, Equal<FinPeriod.status.closed>>>, True>, False>))]
  public virtual bool? Closed { get; set; }

  /// <summary>
  /// Indicates whether the period is closed in the Accounts Payable module.
  /// When <c>false</c>, the period is active in AP.
  /// </summary>
  [PXDBBool]
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
  [PXDBBool]
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
  [PXDBBool]
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
  [PXDBBool]
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
  [PXDBBool]
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
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed in PR", Enabled = false, FieldClass = "PayrollModule")]
  public virtual bool? PRClosed
  {
    get => this._PRClosed;
    set => this._PRClosed = value;
  }

  /// <summary>
  /// The field used to display and edit the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.StartDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.StartDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? StartDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (FinPeriod.startDate), typeof (FinPeriod.endDate)})] get
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
  /// The field used to display and edit the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.EndDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.EndDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (FinPeriod.endDate)})] get
    {
      ref DateTime? local = ref this._EndDate;
      return !local.HasValue ? new DateTime?() : new DateTime?(local.GetValueOrDefault().AddDays(-1.0));
    }
    set => this._EndDate = value?.AddDays(1.0);
  }

  /// <summary>The start date of the period.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Start Date", Enabled = false)]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>The end date of the period (exclusive).</summary>
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  /// <summary>
  /// When <c>true</c>, indicates that the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.EndDate" /> of the period is locked and can not be edited.
  /// </summary>
  /// <value>
  /// Note, that if the date is locked for a particular period, it is also impossible to change end dates for the preceding periods.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Date Locked", Enabled = false, Visible = false)]
  public virtual bool? DateLocked
  {
    get => this._DateLocked;
    set => this._DateLocked = value;
  }

  /// <summary>
  /// Indicates whether the start and end dates of the Financial Period are defined by user.
  /// </summary>
  /// <value>
  /// Defaults to the value of the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinYear.CustomPeriods" /> field of the year of the period.
  /// </value>
  [PXDBBool]
  public virtual bool? Custom
  {
    get => this._Custom;
    set => this._Custom = value;
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
  [PXDBCalced(typeof (Switch<Case<Where<FinPeriod.startDate, Equal<FinPeriod.endDate>>, True>, False>), typeof (bool))]
  public virtual bool? IsAdjustment { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public class PK : PrimaryKeyOf<FinPeriod>.By<FinPeriod.organizationID, FinPeriod.finPeriodID>
  {
    public static FinPeriod Find(
      PXGraph graph,
      int? organizationID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (FinPeriod) PrimaryKeyOf<FinPeriod>.By<FinPeriod.organizationID, FinPeriod.finPeriodID>.FindBy(graph, (object) organizationID, (object) finPeriodID, options);
    }

    public static FinPeriod FindByBranch(PXGraph graph, int? branchID, string finPeriodID)
    {
      return PXResultset<FinPeriod>.op_Implicit(PXSelectBase<FinPeriod, PXSelectJoin<FinPeriod, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>, And<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) finPeriodID,
        (object) branchID
      }));
    }
  }

  public static class FK
  {
    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<FinPeriod>.By<FinPeriod.organizationID>
    {
    }

    public class FinYear : 
      PrimaryKeyOf<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.By<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>.ForeignKeyOf<FinPeriod>.By<FinPeriod.organizationID, FinPeriod.finYear>
    {
    }
  }

  public class Key : OrganizationDependedPeriodKey
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.selected>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FinPeriod.organizationID>
  {
    public const int MasterValue = 0;

    public class masterValue : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    FinPeriod.organizationID.masterValue>
    {
      public masterValue()
        : base(0)
      {
      }
    }
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriod.finPeriodID>
  {
  }

  public abstract class masterFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriod.masterFinPeriodID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriod.finYear>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriod.descr>
  {
  }

  public abstract class periodNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriod.periodNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriod.status>
  {
    public const string Inactive = "Inactive";
    public const string Open = "Open";
    public const string Closed = "Closed";
    public const string Locked = "Locked";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[4]
        {
          "Inactive",
          "Open",
          "Closed",
          "Locked"
        }, new string[4]
        {
          "Inactive",
          "Open",
          "Closed",
          "Locked"
        })
      {
      }
    }

    public class inactive : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FinPeriod.status.inactive>
    {
      public inactive()
        : base("Inactive")
      {
      }
    }

    public class open : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FinPeriod.status.open>
    {
      public open()
        : base("Open")
      {
      }
    }

    public class closed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FinPeriod.status.closed>
    {
      public closed()
        : base("Closed")
      {
      }
    }

    public class locked : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FinPeriod.status.locked>
    {
      public locked()
        : base("Locked")
      {
      }
    }
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.active>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.closed>
  {
  }

  public abstract class aPClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.aPClosed>
  {
  }

  public abstract class aRClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.aRClosed>
  {
  }

  public abstract class iNClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.iNClosed>
  {
  }

  public abstract class cAClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.cAClosed>
  {
  }

  public abstract class fAClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.fAClosed>
  {
  }

  public abstract class pRClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.pRClosed>
  {
  }

  public abstract class startDateUI : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinPeriod.startDateUI>
  {
  }

  public abstract class endDateUI : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinPeriod.endDateUI>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinPeriod.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinPeriod.endDate>
  {
  }

  public abstract class dateLocked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.dateLocked>
  {
  }

  public abstract class custom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.custom>
  {
  }

  public abstract class finDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinPeriod.finDate>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FinPeriod.length>
  {
  }

  public abstract class isAdjustment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod.isAdjustment>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FinPeriod.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FinPeriod.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FinPeriod.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriod.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinPeriod.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FinPeriod.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriod.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinPeriod.lastModifiedDateTime>
  {
  }
}
