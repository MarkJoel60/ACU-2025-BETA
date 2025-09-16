// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPCustomWeek
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Globalization;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Stores the information related to the custom week generation.
/// The information will be visible on the Time and Expenses Preferences (EP101000) form.
/// </summary>
[PXCacheName("Custom Week")]
[Serializable]
public class EPCustomWeek : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private int? _year;
  private int? _number;
  private DateTime? _startDate;
  private DateTime? _endDate;
  private bool? _IsFullWeek;
  protected bool? _IsActive;
  protected Guid? _CreatedByID;
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected string _EntityDescription;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXUIField(Visible = false)]
  public virtual int? WeekID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Week")]
  public virtual string FullNumber => $"{this._year}-{this._number:00}";

  [PXDBInt]
  [PXUIField(DisplayName = "Year", Visible = false)]
  public virtual int? Year
  {
    get => this._year;
    set => this._year = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Number")]
  public virtual int? Number
  {
    get => this._number;
    set => this._number = value;
  }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = true, UseTimeZone = false)]
  [PXDefault]
  [PXUIField(DisplayName = "Start")]
  public virtual DateTime? StartDate
  {
    get => this._startDate;
    set => this._startDate = value;
  }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = true, UseTimeZone = false)]
  [PXDefault]
  [PXVerifyEndDate(typeof (EPCustomWeek.startDate), AllowAutoChange = false)]
  [PXUIField]
  public virtual DateTime? EndDate
  {
    get => this._endDate;
    set => this._endDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsFullWeek
  {
    get => this._IsFullWeek;
    set => this._IsFullWeek = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    [PXDependsOnFields(new Type[] {typeof (EPCustomWeek.fullNumber), typeof (EPCustomWeek.startDate), typeof (EPCustomWeek.endDate)})] get
    {
      CultureInfo culture = LocaleInfo.GetCulture();
      return culture != null && !culture.DateTimeFormat.ShortDatePattern.StartsWith("M") ? $"{this.FullNumber} ({this.StartDate:dd/MM} - {this.EndDate:dd/MM})" : $"{this.FullNumber} ({this.StartDate:MM/dd} - {this.EndDate:MM/dd})";
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Description", Visible = false)]
  public virtual string ShortDescription
  {
    [PXDependsOnFields(new Type[] {typeof (EPCustomWeek.fullNumber), typeof (EPCustomWeek.startDate), typeof (EPCustomWeek.endDate)})] get
    {
      CultureInfo culture = LocaleInfo.GetCulture();
      return culture != null && !culture.DateTimeFormat.ShortDatePattern.StartsWith("M") ? $"{this.FullNumber} ({this.StartDate:dd/MM} - {this.EndDate:dd/MM})" : $"{this.FullNumber} ({this.StartDate:MM/dd} - {this.EndDate:MM/dd})";
    }
  }

  [PXDBCreatedByID(DontOverrideValue = true)]
  [PXUIField(Enabled = false)]
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

  [PXUIField(DisplayName = "Created At")]
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

  [PXString(InputMask = "")]
  [PXUIField]
  public virtual string EntityDescription
  {
    get => this._EntityDescription;
    set => this._EntityDescription = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class weekID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCustomWeek.weekID>
  {
  }

  public abstract class fullNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPCustomWeek.fullNumber>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCustomWeek.year>
  {
  }

  public abstract class number : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCustomWeek.number>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPCustomWeek.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPCustomWeek.endDate>
  {
  }

  public abstract class isFullWeek : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPCustomWeek.isFullWeek>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPCustomWeek.isActive>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPCustomWeek.description>
  {
  }

  public abstract class shortDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCustomWeek.shortDescription>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPCustomWeek.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCustomWeek.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPCustomWeek.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPCustomWeek.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCustomWeek.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPCustomWeek.lastModifiedDateTime>
  {
  }

  public abstract class entityDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCustomWeek.entityDescription>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPCustomWeek.Tstamp>
  {
  }
}
