// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.MasterFinYear
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.GL.FinPeriods;

[PXProjection(typeof (Select<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<FinPeriod.organizationID.masterValue>>>), Persistent = true)]
[PXPrimaryGraph(new Type[] {typeof (MasterFinPeriodMaint)}, new Type[] {typeof (Select<MasterFinYear, Where<MasterFinYear.year, Equal<Current<MasterFinYear.year>>>>)})]
[DebuggerDisplay("{GetType()}: Year = {Year}, tstamp = {PX.Data.PXDBTimestampAttribute.ToString(tstamp)}")]
[Serializable]
public class MasterFinYear : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IFinYear, IYear
{
  /// <summary>
  /// Key field.
  /// The financial year.
  /// </summary>
  [PXDBString(4, IsKey = true, IsFixed = true, BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXDefault("")]
  [PXUIField]
  [PXSelector(typeof (Search3<MasterFinYear.year, OrderBy<Desc<MasterFinYear.year>>>))]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string Year { get; set; }

  [PXExtraKey]
  [PXDBInt(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXDefault(0)]
  public virtual int? OrganizationID { get; set; }

  /// <summary>The start date of the year.</summary>
  [PXDBDate(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Start Date", Enabled = false)]
  public virtual DateTime? StartDate { get; set; }

  /// <summary>The end date of the year (inclusive).</summary>
  [PXDBDate(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? EndDate { get; set; }

  /// <summary>The number of periods in the year.</summary>
  [PXDBShort(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Number of Periods", Enabled = false)]
  public virtual short? FinPeriods { get; set; }

  /// <summary>
  /// Indicates whether the <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">periods</see> of the year can be modified by user.
  /// </summary>
  [PXDBBool(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "User-Defined Periods")]
  public virtual bool? CustomPeriods { get; set; }

  /// <summary>The start date of the financial year.</summary>
  /// <value>
  /// Defaults to the value of the <see cref="P:PX.Objects.GL.FinYearSetup.BegFinYear" /> field of the financial year setup record.
  /// </value>
  [PXDBDate(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXDefault(typeof (Search<FinYearSetup.begFinYear>))]
  [PXUIField]
  public virtual DateTime? BegFinYearHist { get; set; }

  /// <summary>The start date of the first period of the year.</summary>
  /// <value>
  /// Defaults to the value of the <see cref="P:PX.Objects.GL.FinYearSetup.PeriodsStartDate" /> field of the financial year setup record.
  /// </value>
  [PXDBDate(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXDefault(typeof (Search<FinYearSetup.periodsStartDate>))]
  [PXUIField]
  public virtual DateTime? PeriodsStartDateHist { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(DescriptionField = typeof (MasterFinYear.year), BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<MasterFinYear>.By<MasterFinYear.year>
  {
    public static MasterFinYear Find(PXGraph graph, string year, PKFindOptions options = 0)
    {
      return (MasterFinYear) PrimaryKeyOf<MasterFinYear>.By<MasterFinYear.year>.FindBy(graph, (object) year, options);
    }
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MasterFinYear.year>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MasterFinYear.organizationID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  MasterFinYear.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  MasterFinYear.endDate>
  {
  }

  public abstract class finPeriods : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  MasterFinYear.finPeriods>
  {
  }

  public abstract class customPeriods : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MasterFinYear.customPeriods>
  {
  }

  public abstract class begFinYearHist : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    MasterFinYear.begFinYearHist>
  {
  }

  public abstract class periodsStartDateHist : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    MasterFinYear.periodsStartDateHist>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  MasterFinYear.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  MasterFinYear.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  MasterFinYear.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MasterFinYear.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    MasterFinYear.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    MasterFinYear.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MasterFinYear.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    MasterFinYear.lastModifiedDateTime>
  {
  }
}
