// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookPeriodSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("FA Book Period Template")]
[Serializable]
public class FABookPeriodSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPeriodSetup
{
  protected int? _BookID;
  protected 
  #nullable disable
  string _PeriodNbr;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected string _Descr;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _Custom;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (FABookYearSetup.bookID))]
  [PXParent(typeof (Select<FABookYearSetup, Where<FABookYearSetup.bookID, Equal<Current<FABookPeriodSetup.bookID>>>>))]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Period Nbr.", Enabled = false)]
  public virtual string PeriodNbr
  {
    get => this._PeriodNbr;
    set => this._PeriodNbr = value;
  }

  /// <summary>
  /// The field used to display and edit the <see cref="P:PX.Objects.FA.FABookPeriodSetup.StartDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.FA.FABookPeriodSetup.StartDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? StartDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (FABookPeriodSetup.startDate), typeof (FABookPeriodSetup.endDate)})] get
    {
      if (this._EndDate.HasValue && this._StartDate.HasValue)
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
  /// The field used to display and edit the <see cref="P:PX.Objects.FA.FABookPeriodSetup.EndDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.FA.FABookPeriodSetup.EndDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (FABookPeriodSetup.endDate)})] get
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
  [PXDefault]
  [PXUIField(DisplayName = "End Date", Enabled = false)]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
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
    PrimaryKeyOf<FABookPeriodSetup>.By<FABookPeriodSetup.bookID, FABookPeriodSetup.periodNbr>
  {
    public static FABookPeriodSetup Find(
      PXGraph graph,
      int? bookID,
      string periodNbr,
      PKFindOptions options = 0)
    {
      return (FABookPeriodSetup) PrimaryKeyOf<FABookPeriodSetup>.By<FABookPeriodSetup.bookID, FABookPeriodSetup.periodNbr>.FindBy(graph, (object) bookID, (object) periodNbr, options);
    }
  }

  public static class FK
  {
    public class Book : 
      PrimaryKeyOf<FABook>.By<FABook.bookID>.ForeignKeyOf<FABookPeriodSetup>.By<FABookPeriodSetup.bookID>
    {
    }
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookPeriodSetup.bookID>
  {
  }

  public abstract class periodNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookPeriodSetup.periodNbr>
  {
  }

  public abstract class startDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookPeriodSetup.startDateUI>
  {
  }

  public abstract class endDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookPeriodSetup.endDateUI>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookPeriodSetup.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FABookPeriodSetup.endDate>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookPeriodSetup.descr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FABookPeriodSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABookPeriodSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookPeriodSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookPeriodSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FABookPeriodSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookPeriodSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookPeriodSetup.lastModifiedDateTime>
  {
  }

  public abstract class custom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookPeriodSetup.custom>
  {
  }
}
