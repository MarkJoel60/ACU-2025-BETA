// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Abstractions.Periods;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("FA Book Period")]
[DebuggerDisplay("{GetType()}: BookID = {BookID}, OrganizationID = {OrganizationID}, FinPeriodID = {FinPeriodID}, tstamp = {PX.Data.PXDBTimestampAttribute.ToString(tstamp)}}")]
[Serializable]
public class FABookPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPeriod, IPeriodSetup
{
  protected int? _BookID;
  protected 
  #nullable disable
  string _FinYear;
  protected string _FinPeriodID;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected string _Descr;
  protected bool? _Closed;
  protected bool? _DateLocked;
  protected bool? _Active;
  protected string _PeriodNbr;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _Custom;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (FABookYear.bookID))]
  [PXParent(typeof (Select<FABookYear, Where<FABookYear.bookID, Equal<Current<FABookPeriod.bookID>>, And<FABookYear.organizationID, Equal<Current<FABookPeriod.organizationID>>>>>))]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDefault(0)]
  [PXDBInt(IsKey = true)]
  public virtual int? OrganizationID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXDefault(typeof (FABookYear.year))]
  [PXUIField]
  [PXParent(typeof (Select<FABookYear, Where<FABookYear.year, Equal<Current<FABookPeriod.finYear>>, And<FABookYear.bookID, Equal<Current<FABookPeriod.bookID>>, And<FABookYear.organizationID, Equal<Current<FABookPeriod.organizationID>>>>>>))]
  public virtual string FinYear
  {
    get => this._FinYear;
    set => this._FinYear = value;
  }

  [FABookPeriodID(typeof (FABookPeriod.bookID), null, true, null, null, null, typeof (FABookPeriod.organizationID), null, null, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [FABookPeriodID(null, null, true, null, null, null, null, null, null)]
  [PXUIField]
  [PXDefault]
  public virtual string MasterFinPeriodID { get; set; }

  /// <summary>
  /// The field used to display and edit the <see cref="P:PX.Objects.FA.FABookPeriod.StartDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.FA.FABookPeriod.StartDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? StartDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (FABookPeriod.startDate), typeof (FABookPeriod.endDate)})] get
    {
      if (this._StartDate.HasValue && this._EndDate.HasValue)
      {
        DateTime? startDate = this._StartDate;
        DateTime? endDate = this._EndDate;
        if ((startDate.HasValue == endDate.HasValue ? (startDate.HasValue ? (startDate.GetValueOrDefault() == endDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          return new DateTime?(this._StartDate.Value.AddDays(-1.0));
      }
      return this._StartDate;
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
  /// The field used to display and edit the <see cref="P:PX.Objects.FA.FABookPeriod.EndDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.FA.FABookPeriod.EndDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (FABookPeriod.endDate)})] get
    {
      ref DateTime? local = ref this._EndDate;
      return !local.HasValue ? new DateTime?() : new DateTime?(local.GetValueOrDefault().AddDays(-1.0));
    }
    set => this._EndDate = value?.AddDays(1.0);
  }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Start Date", Enabled = false)]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2021 R1.")]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed in GL", Enabled = false)]
  public virtual bool? Closed
  {
    get => this._Closed;
    set => this._Closed = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Date Locked", Enabled = false, Visible = false)]
  public virtual bool? DateLocked
  {
    get => this._DateLocked;
    set => this._DateLocked = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Period Nbr.", Enabled = false)]
  public virtual string PeriodNbr
  {
    get => this._PeriodNbr;
    set => this._PeriodNbr = value;
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

  public virtual bool? Custom
  {
    get => this._Custom;
    set => this._Custom = value;
  }

  public class PK : 
    PrimaryKeyOf<FABookPeriod>.By<FABookPeriod.bookID, FABookPeriod.organizationID, FABookPeriod.finPeriodID>
  {
    public static FABookPeriod Find(
      PXGraph graph,
      int? bookID,
      int? organizationID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (FABookPeriod) PrimaryKeyOf<FABookPeriod>.By<FABookPeriod.bookID, FABookPeriod.organizationID, FABookPeriod.finPeriodID>.FindBy(graph, (object) bookID, (object) organizationID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Book : 
      PrimaryKeyOf<FABook>.By<FABook.bookID>.ForeignKeyOf<FABookPeriod>.By<FABookPeriod.bookID>
    {
    }

    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<FABookPeriod>.By<FABookPeriod.organizationID>
    {
    }
  }

  public class Key : OrganizationDependedPeriodKey
  {
    public int? BookID { get; protected set; }

    public bool IsPostingBook { get; protected set; }

    public void SetBookID(FABook book)
    {
      this.BookID = book.BookID;
      this.IsPostingBook = book.UpdateGL.GetValueOrDefault();
    }

    public override bool Defined => base.Defined && this.BookID.HasValue;

    public override List<object> ToListOfObjects(bool skipPeriodID = false)
    {
      List<object> listOfObjects = base.ToListOfObjects(skipPeriodID);
      listOfObjects.Add((object) this.BookID);
      return listOfObjects;
    }

    public override bool IsNotPeriodPartsEqual(object otherKey)
    {
      if (!base.IsNotPeriodPartsEqual(otherKey))
        return false;
      int? bookId1 = ((FABookPeriod.Key) otherKey).BookID;
      int? bookId2 = this.BookID;
      return bookId1.GetValueOrDefault() == bookId2.GetValueOrDefault() & bookId1.HasValue == bookId2.HasValue;
    }

    public override bool IsMasterCalendar => base.IsMasterCalendar && this.IsPostingBook;
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookPeriod.bookID>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookPeriod.organizationID>
  {
    public const int NonPostingBookValue = 0;

    public class nonPostingBookValue : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      FABookPeriod.organizationID.nonPostingBookValue>
    {
      public nonPostingBookValue()
        : base(0)
      {
      }
    }
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookPeriod.finYear>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookPeriod.finPeriodID>
  {
  }

  public abstract class masterFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookPeriod.masterFinPeriodID>
  {
  }

  public abstract class startDateUI : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FABookPeriod.startDateUI>
  {
  }

  public abstract class endDateUI : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FABookPeriod.endDateUI>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FABookPeriod.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FABookPeriod.endDate>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookPeriod.descr>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2021 R1.")]
  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookPeriod.closed>
  {
  }

  public abstract class dateLocked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookPeriod.dateLocked>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookPeriod.active>
  {
  }

  public abstract class periodNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookPeriod.periodNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FABookPeriod.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABookPeriod.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookPeriod.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookPeriod.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FABookPeriod.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookPeriod.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookPeriod.lastModifiedDateTime>
  {
  }

  public abstract class custom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookPeriod.custom>
  {
  }
}
