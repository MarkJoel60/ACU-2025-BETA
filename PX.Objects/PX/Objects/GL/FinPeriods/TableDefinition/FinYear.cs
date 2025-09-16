// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.TableDefinition.FinYear
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL.FinPeriods.TableDefinition;

public class FinYear : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Year;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected short? _FinPeriods;
  protected bool? _CustomPeriods;
  protected DateTime? _BegFinYearHist;
  protected DateTime? _PeriodsStartDateHist;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [Organization(false, IsKey = true)]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// Key field.
  /// The financial year.
  /// </summary>
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault("")]
  [PXFieldDescription]
  [PXUIField(DisplayName = "Year")]
  public virtual string Year
  {
    get => this._Year;
    set => this._Year = value;
  }

  /// <summary>The start date of the year.</summary>
  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>The end date of the year (inclusive).</summary>
  [PXDBDate]
  [PXDefault]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  /// <summary>The number of periods in the year.</summary>
  [PXDBShort]
  [PXDefault(0)]
  public virtual short? FinPeriods
  {
    get => this._FinPeriods;
    set => this._FinPeriods = value;
  }

  /// <summary>
  /// Indicates whether the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">periods</see> of the year can be modified by user.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? CustomPeriods
  {
    get => this._CustomPeriods;
    set => this._CustomPeriods = value;
  }

  /// <summary>The start date of the financial year.</summary>
  /// <value>
  /// Defaults to the value of the <see cref="P:PX.Objects.GL.FinYearSetup.BegFinYear" /> field of the financial year setup record.
  /// </value>
  [PXDBDate]
  public virtual DateTime? BegFinYearHist
  {
    get => this._BegFinYearHist;
    set => this._BegFinYearHist = value;
  }

  /// <summary>The start date of the first period of the year.</summary>
  /// <value>
  /// Defaults to the value of the <see cref="P:PX.Objects.GL.FinYearSetup.PeriodsStartDate" /> field of the financial year setup record.
  /// </value>
  [PXDBDate]
  public virtual DateTime? PeriodsStartDateHist
  {
    get => this._PeriodsStartDateHist;
    set => this._PeriodsStartDateHist = value;
  }

  /// <summary>
  /// Key field.
  /// Unique identifier of the Financial Period.
  /// </summary>
  /// 
  ///             Consists of the year and the number of the period in the year. For more information see <see cref="T:PX.Objects.GL.FinPeriodIDAttribute" />
  /// .
  [PXDBString(6, IsFixed = true)]
  [FinPeriodIDFormatting]
  public virtual string StartMasterFinPeriodID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(DescriptionField = typeof (FinYear.year))]
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

  public class PK : PrimaryKeyOf<FinYear>.By<FinYear.organizationID, FinYear.year>
  {
    public static FinYear Find(
      PXGraph graph,
      int? organizationID,
      string year,
      PKFindOptions options = 0)
    {
      return (FinYear) PrimaryKeyOf<FinYear>.By<FinYear.organizationID, FinYear.year>.FindBy(graph, (object) organizationID, (object) year, options);
    }

    public static FinYear FindByBranch(PXGraph graph, int? branchID, string year)
    {
      return PXResultset<FinYear>.op_Implicit(PXSelectBase<FinYear, PXSelectJoin<FinYear, InnerJoin<Branch, On<Branch.organizationID, Equal<FinYear.organizationID>>>, Where<FinYear.year, Equal<Required<FinYear.year>>, And<Branch.branchID, Equal<Required<Branch.branchID>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) year,
        (object) branchID
      }));
    }
  }

  public static class FK
  {
    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<FinYear>.By<FinYear.organizationID>
    {
    }
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FinYear.organizationID>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinYear.year>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinYear.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinYear.endDate>
  {
  }

  public abstract class finPeriods : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FinYear.finPeriods>
  {
  }

  public abstract class customPeriods : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinYear.customPeriods>
  {
  }

  public abstract class begFinYearHist : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinYear.begFinYearHist>
  {
  }

  public abstract class periodsStartDateHist : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinYear.periodsStartDateHist>
  {
  }

  public abstract class startMasterFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinYear.startMasterFinPeriodID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FinYear.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FinYear.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FinYear.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinYear.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinYear.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FinYear.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinYear.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinYear.lastModifiedDateTime>
  {
  }
}
