// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriodSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Financial period setup records used to define the templates for actual financial periods.
/// The records of this type are edited on the Financial Year (GL101000) form.
/// </summary>
[PXCacheName("Financial Period Template")]
[Serializable]
public class FinPeriodSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPeriodSetup
{
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

  /// <summary>The number of the period in a year.</summary>
  /// <value>
  /// Used to determine the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod.PeriodNbr" /> field when the real period is created from the template.
  /// </value>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Period Nbr.", Enabled = false)]
  [PXParent(typeof (Select<FinYearSetup>))]
  public virtual string PeriodNbr
  {
    get => this._PeriodNbr;
    set => this._PeriodNbr = value;
  }

  /// <summary>
  /// The field used to display and edit the <see cref="P:PX.Objects.GL.FinPeriodSetup.StartDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.GL.FinPeriodSetup.StartDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? StartDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (FinPeriodSetup.startDate), typeof (FinPeriodSetup.endDate)})] get
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
  /// The field used to display and edit the <see cref="P:PX.Objects.GL.FinPeriodSetup.EndDate" /> of the period (inclusive) in the UI.
  /// </summary>
  /// <value>
  /// Depends on and changes the value of the <see cref="P:PX.Objects.GL.FinPeriodSetup.EndDate" /> field, performing additional transformations.
  /// </value>
  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (FinPeriodSetup.endDate)})] get
    {
      ref DateTime? local = ref this._EndDate;
      return !local.HasValue ? new DateTime?() : new DateTime?(local.GetValueOrDefault().AddDays(-1.0));
    }
    set => this._EndDate = value?.AddDays(1.0);
  }

  /// <summary>The start date of the period (inclusive).</summary>
  /// <value>
  /// Used to determine the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod.StartDate" /> field when the real period is created from the template.
  /// </value>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Start Date", Enabled = false)]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>The end date of the period (exclusive).</summary>
  /// <value>
  /// Used to determine the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod.EndDate" /> field when the real period is created from the template.
  /// </value>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "End Date", Enabled = false)]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  /// <summary>The description of the period.</summary>
  /// <value>
  /// Used to determine the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod.Descr" /> field when the real period is created from the template.
  /// </value>
  [PXDBLocalizableString(60, IsUnicode = true)]
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

  /// <summary>
  /// Indicates whether the start and end dates of the Financial Period are defined by user.
  /// </summary>
  public virtual bool? Custom
  {
    get => this._Custom;
    set => this._Custom = value;
  }

  public abstract class periodNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriodSetup.periodNbr>
  {
  }

  public abstract class startDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinPeriodSetup.startDateUI>
  {
  }

  public abstract class endDateUI : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinPeriodSetup.endDateUI>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinPeriodSetup.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinPeriodSetup.endDate>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriodSetup.descr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FinPeriodSetup.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FinPeriodSetup.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FinPeriodSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriodSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinPeriodSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FinPeriodSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriodSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinPeriodSetup.lastModifiedDateTime>
  {
  }

  public abstract class custom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriodSetup.custom>
  {
  }
}
