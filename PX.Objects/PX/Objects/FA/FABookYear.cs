// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookYear
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("FA Book Year")]
[DebuggerDisplay("{GetType()}: BookID = {BookID}, OrganizationID = {OrganizationID}, Year = {Year}, tstamp = {PX.Data.PXDBTimestampAttribute.ToString(tstamp)}}")]
[Serializable]
public class FABookYear : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IYear
{
  protected int? _BookID;
  protected 
  #nullable disable
  string _Year;
  protected DateTime? _StartDate;
  protected short? _FinPeriods;
  protected DateTime? _EndDate;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (Search<FABook.bookID>), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXUIField(DisplayName = "Book")]
  [PXParent(typeof (Select<FABook, Where<FABook.bookID, Equal<Current<FABookYear.bookID>>>>))]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  public virtual int? OrganizationID { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault("")]
  [PXUIField]
  [PXSelector(typeof (Search<FABookYear.year, Where<FABookYear.organizationID, Equal<Current<FABookYear.organizationID>>>, OrderBy<Desc<FABookYear.year>>>))]
  public virtual string Year
  {
    get => this._Year;
    set => this._Year = value;
  }

  [PXDBString(6, IsFixed = true)]
  [FinPeriodIDFormatting]
  [PXUIField]
  [PXDefault]
  public virtual string StartMasterFinPeriodID { get; set; }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Start Date", Enabled = false)]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Number of Periods", Enabled = false)]
  public virtual short? FinPeriods
  {
    get => this._FinPeriods;
    set => this._FinPeriods = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
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

  public class PK : 
    PrimaryKeyOf<FABookYear>.By<FABookYear.bookID, FABookYear.organizationID, FABookYear.year>
  {
    public static FABookYear Find(
      PXGraph graph,
      int? bookID,
      int? organizationID,
      string year,
      PKFindOptions options = 0)
    {
      return (FABookYear) PrimaryKeyOf<FABookYear>.By<FABookYear.bookID, FABookYear.organizationID, FABookYear.year>.FindBy(graph, (object) bookID, (object) organizationID, (object) year, options);
    }
  }

  public static class FK
  {
    public class Book : 
      PrimaryKeyOf<FABook>.By<FABook.bookID>.ForeignKeyOf<FABookYear>.By<FABookYear.bookID>
    {
    }

    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<FABookYear>.By<FABookYear.organizationID>
    {
    }
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookYear.bookID>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookYear.organizationID>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookYear.year>
  {
  }

  public abstract class startMasterFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookYear.startMasterFinPeriodID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FABookYear.startDate>
  {
  }

  public abstract class finPeriods : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FABookYear.finPeriods>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FABookYear.endDate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FABookYear.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABookYear.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookYear.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookYear.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABookYear.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookYear.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookYear.lastModifiedDateTime>
  {
  }
}
