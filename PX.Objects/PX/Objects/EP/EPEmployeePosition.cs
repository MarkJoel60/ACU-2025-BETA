// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEmployeePosition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Employee Position")]
[Serializable]
public class EPEmployeePosition : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _EmployeeID;
  protected bool? _IsActive;
  protected 
  #nullable disable
  string _PositionID;
  protected DateTime? _StartDate;
  protected string _StartReason;
  protected DateTime? _EndDate;
  protected string _TermReason;
  protected bool? _IsTerminated;
  protected bool? _IsRehirable;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (EPEmployee.bAccountID))]
  [PXParent(typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPEmployeePosition.employeeID>>>>))]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (EPEmployee.positionLineCntr))]
  [PXUIField(Visible = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (EPPosition.positionID), DescriptionField = typeof (EPPosition.description))]
  [PXUIField]
  public virtual string PositionID
  {
    get => this._PositionID;
    set => this._PositionID = value;
  }

  [PXDefault]
  [PXDBDate]
  [PXCheckUnique(new Type[] {typeof (EPEmployeePosition.employeeID), typeof (EPEmployeePosition.startDate)})]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBString(3, IsFixed = true)]
  [EPStartReason.List]
  [PXDefault("NEW")]
  [PXUIField(DisplayName = "Start Reason")]
  public virtual string StartReason
  {
    get => this._StartReason;
    set => this._StartReason = value;
  }

  /// <summary>The probation period end date.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Probation Period End Date")]
  [PXUIEnabled(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPEmployeePosition.startReason, Equal<EPStartReason.newStatus>>>>>.Or<BqlOperand<EPEmployeePosition.startReason, IBqlString>.IsEqual<EPStartReason.rehire>>>))]
  public virtual DateTime? ProbationPeriodEndDate { get; set; }

  [PXDefault]
  [PXUIRequired(typeof (Where<EPEmployeePosition.isTerminated, Equal<True>, Or<EPEmployeePosition.isActive, Equal<False>>>))]
  [PXDBDate]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDefault]
  [PXUIRequired(typeof (EPEmployeePosition.isTerminated))]
  [PXDBString(3, IsFixed = true)]
  [EPTermReason.List]
  [PXUIField(DisplayName = "Termination Reason")]
  public virtual string TermReason
  {
    get => this._TermReason;
    set => this._TermReason = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Terminated")]
  public virtual bool? IsTerminated
  {
    get => this._IsTerminated;
    set => this._IsTerminated = value;
  }

  [PXUIEnabled(typeof (Where<EPEmployeePosition.isTerminated, Equal<True>, And<EPEmployeePosition.termReason, NotEqual<EPTermReason.deseased>>>))]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Eligible for Rehire")]
  public virtual bool? IsRehirable
  {
    get => this._IsRehirable;
    set => this._IsRehirable = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

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

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeePosition.employeeID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeePosition.lineNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEmployeePosition.isActive>
  {
  }

  public abstract class positionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployeePosition.positionID>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEmployeePosition.startDate>
  {
  }

  public abstract class startReason : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeePosition.startReason>
  {
  }

  public abstract class probationPeriodEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEmployeePosition.probationPeriodEndDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPEmployeePosition.endDate>
  {
  }

  public abstract class termReason : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployeePosition.termReason>
  {
  }

  public abstract class isTerminated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEmployeePosition.isTerminated>
  {
  }

  public abstract class isRehirable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEmployeePosition.isRehirable>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployeePosition.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPEmployeePosition.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployeePosition.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeePosition.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEmployeePosition.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPEmployeePosition.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeePosition.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEmployeePosition.lastModifiedDateTime>
  {
  }
}
