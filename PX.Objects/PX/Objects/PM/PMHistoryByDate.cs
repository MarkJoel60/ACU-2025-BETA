// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMHistoryByDate
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
namespace PX.Objects.PM;

/// <summary>
/// Represents a line of the project transaction history by date.
/// The line contains actual <see cref="T:PX.Objects.PM.PMTran">project transaction</see> values aggregated by project key that are used in the date-sensitive cost analysis.
/// </summary>
[PXCacheName("Project History By Date")]
[Serializable]
public class PMHistoryByDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IProjectFilter
{
  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> of the project transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMTask">project task</see> of the project transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectTaskID { get; set; }

  /// <exclude />
  public int? TaskID => this.ProjectTaskID;

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMAccountGroup">project account group</see> of the project transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public virtual int? AccountGroupID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> of the project transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMCostCode">project cost code</see> of the project transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostCode.CostCodeID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public virtual int? CostCodeID { get; set; }

  /// <summary>The project transaction date.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTran.Date" /> field.
  /// </value>
  [PXDBDate(IsKey = true)]
  public virtual DateTime? Date { get; set; }

  /// <summary>The financial period.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.FinPeriodID" /> field.
  /// </value>
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  public virtual 
  #nullable disable
  string PeriodID { get; set; }

  /// <summary>
  /// The year of the <see cref="P:PX.Objects.PM.PMHistoryByDate.Date" />.
  /// </summary>
  [PXDBInt]
  [PXFormula(typeof (DatePart<DatePart.year, PMHistoryByDate.date>))]
  public int? Year { get; set; }

  /// <summary>
  /// The quarter of the <see cref="P:PX.Objects.PM.PMHistoryByDate.Year" />.
  /// </summary>
  [PXDBInt]
  [PMQuartetOfTheYear(typeof (PMHistoryByDate.date))]
  public int? Quarter { get; set; }

  /// <summary>
  /// The month of the <see cref="P:PX.Objects.PM.PMHistoryByDate.Date" />.
  /// </summary>
  [PXDBInt]
  [PXFormula(typeof (DatePart<DatePart.month, PMHistoryByDate.date>))]
  public int? Month { get; set; }

  /// <summary>
  /// The week of the <see cref="P:PX.Objects.PM.PMHistoryByDate.Month" />.
  /// </summary>
  [PXDBInt]
  [PMWeekOfTheMonth(typeof (PMHistoryByDate.date))]
  public int? Week { get; set; }

  /// <summary>
  /// The week of the <see cref="P:PX.Objects.PM.PMHistoryByDate.Year" />.
  /// </summary>
  [PXDBInt]
  [PMWeekOfTheYear(typeof (PMHistoryByDate.date))]
  public int? WeekOfYear { get; set; }

  /// <summary>
  /// The day of the <see cref="P:PX.Objects.PM.PMHistoryByDate.Date" />.
  /// </summary>
  [PXDBInt]
  [PXFormula(typeof (DatePart<DatePart.day, PMHistoryByDate.date>))]
  public int? Day { get; set; }

  /// <summary>
  /// The total quantity of the corresponding project transactions.
  /// </summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ActualQty { get; set; }

  /// <summary>
  /// The total amount of the corresponding project transactions.
  /// </summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMHistoryByDate.CuryActualAmount">actual amount</see> in the base currency.
  /// </summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ActualAmount { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <exclude />
  [PXInt]
  [PXUnboundDefault(0)]
  public virtual int? GroupID { get; set; }

  public class PK : 
    PrimaryKeyOf<PMHistoryByDate>.By<PMHistoryByDate.projectID, PMHistoryByDate.projectTaskID, PMHistoryByDate.accountGroupID, PMHistoryByDate.inventoryID, PMHistoryByDate.costCodeID, PMHistoryByDate.date, PMHistoryByDate.periodID>
  {
    public static PMHistoryByDate Find(
      PXGraph graph,
      int? projectID,
      int? projectTaskID,
      int? accountGroupID,
      int? inventoryID,
      int? costCodeID,
      DateTime? date,
      string periodID,
      PKFindOptions options = 0)
    {
      return (PMHistoryByDate) PrimaryKeyOf<PMHistoryByDate>.By<PMHistoryByDate.projectID, PMHistoryByDate.projectTaskID, PMHistoryByDate.accountGroupID, PMHistoryByDate.inventoryID, PMHistoryByDate.costCodeID, PMHistoryByDate.date, PMHistoryByDate.periodID>.FindBy(graph, (object) projectID, (object) projectTaskID, (object) accountGroupID, (object) inventoryID, (object) costCodeID, (object) date, (object) periodID, options);
    }
  }

  public static class FK
  {
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMHistoryByDate>.By<PMHistoryByDate.projectID>
    {
    }

    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMHistoryByDate>.By<PMHistoryByDate.projectID, PMHistoryByDate.projectTaskID>
    {
    }

    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<PMHistoryByDate>.By<PMHistoryByDate.accountGroupID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMHistoryByDate>.By<PMHistoryByDate.inventoryID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMHistoryByDate>.By<PMHistoryByDate.costCodeID>
    {
    }
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.projectTaskID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.costCodeID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMHistoryByDate.date>
  {
  }

  public abstract class periodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMHistoryByDate.periodID>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.year>
  {
  }

  public abstract class quarter : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.quarter>
  {
  }

  public abstract class month : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.month>
  {
  }

  public abstract class week : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.week>
  {
  }

  public abstract class weekOfYear : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.weekOfYear>
  {
  }

  public abstract class day : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.day>
  {
  }

  public abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMHistoryByDate.actualQty>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMHistoryByDate.curyActualAmount>
  {
  }

  public abstract class actualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMHistoryByDate.actualAmount>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMHistoryByDate.Tstamp>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDate.groupID>
  {
  }
}
