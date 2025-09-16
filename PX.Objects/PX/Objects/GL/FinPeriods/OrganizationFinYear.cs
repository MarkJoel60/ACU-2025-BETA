// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.OrganizationFinYear
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.GL.FinPeriods;

[PXCacheName("Company Financial Period")]
[PXProjection(typeof (Select<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, NotEqual<FinPeriod.organizationID.masterValue>>>), Persistent = true)]
[Serializable]
public class OrganizationFinYear : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IFinYear, IYear
{
  [Organization(true, IsKey = true, BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXParent(typeof (Select<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Current<OrganizationFinYear.organizationID>>>>))]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// Key field.
  /// The financial year.
  /// </summary>
  [PXDBString(4, IsKey = true, IsFixed = true, BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXDefault("")]
  [PXUIField]
  [PXSelector(typeof (Search<OrganizationFinYear.year, Where<OrganizationFinYear.organizationID, Equal<Current<OrganizationFinYear.organizationID>>>, OrderBy<Desc<OrganizationFinYear.year>>>))]
  [PXFieldDescription]
  [PXParent(typeof (Select<MasterFinYear, Where<MasterFinYear.year, Equal<Current<OrganizationFinYear.year>>>>))]
  public virtual 
  #nullable disable
  string Year { get; set; }

  [PXDBString(6, IsFixed = true, BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [FinPeriodIDFormatting]
  [PXDefault]
  [PXUIField]
  public virtual string StartMasterFinPeriodID { get; set; }

  /// <summary>The number of periods in the year.</summary>
  [PXDBShort(BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Number of Periods", Enabled = false)]
  public virtual short? FinPeriods { get; set; }

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

  public class PK : 
    PrimaryKeyOf<OrganizationFinYear>.By<OrganizationFinYear.organizationID, OrganizationFinYear.year>
  {
    public static OrganizationFinYear Find(
      PXGraph graph,
      int? organizationID,
      string year,
      PKFindOptions options = 0)
    {
      return (OrganizationFinYear) PrimaryKeyOf<OrganizationFinYear>.By<OrganizationFinYear.organizationID, OrganizationFinYear.year>.FindBy(graph, (object) organizationID, (object) year, options);
    }

    public static OrganizationFinYear FindByBranch(PXGraph graph, int? branchID, string year)
    {
      return PXResultset<OrganizationFinYear>.op_Implicit(PXSelectBase<OrganizationFinYear, PXSelectJoin<OrganizationFinYear, InnerJoin<Branch, On<Branch.organizationID, Equal<OrganizationFinYear.organizationID>>>, Where<OrganizationFinYear.year, Equal<Required<OrganizationFinYear.year>>, And<Branch.branchID, Equal<Required<Branch.branchID>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) year,
        (object) branchID
      }));
    }
  }

  public static class FK
  {
    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<OrganizationFinYear>.By<OrganizationFinYear.organizationID>
    {
    }

    public class Year : 
      PrimaryKeyOf<MasterFinYear>.By<MasterFinYear.year>.ForeignKeyOf<OrganizationFinYear>.By<OrganizationFinYear.year>
    {
    }
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationFinYear.organizationID>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OrganizationFinYear.year>
  {
  }

  public abstract class startMasterFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinYear.startMasterFinPeriodID>
  {
  }

  public abstract class finPeriods : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  OrganizationFinYear.finPeriods>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinYear.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  OrganizationFinYear.endDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  OrganizationFinYear.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  OrganizationFinYear.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  OrganizationFinYear.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinYear.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinYear.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    OrganizationFinYear.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinYear.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinYear.lastModifiedDateTime>
  {
  }
}
