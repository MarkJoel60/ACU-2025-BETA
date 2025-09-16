// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMDateSensitiveDataRevisionLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a line of the project financial vision.
/// The records of this type are created dynamically through the Project Financial Vision (PM405000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProjectDateSensitiveCostsInquiry" /> graph).
/// </summary>
[PXCacheName("Project Financial Vision Line")]
[PXPrimaryGraph(typeof (ProjectDateSensitiveCostsInquiry))]
[Serializable]
public class PMDateSensitiveDataRevisionLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The project identifier.</summary>
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PMDateSensitiveDataRevisionLine.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <summary>The curve identifier.</summary>
  [PXDBString(IsKey = true)]
  public 
  #nullable disable
  string CurveID { get; set; }

  /// <summary>The point number.</summary>
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Period Nbr.", Enabled = false)]
  public int? PointNumber { get; set; }

  /// <summary>
  /// The identifier of the project task to which corresponds the line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXDBInt]
  [PXDimensionSelector("PROTASK", typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMDateSensitiveDataRevisionLine.projectID>>>>), typeof (PMTask.taskCD))]
  [PXUIField(DisplayName = "Project Task", Enabled = false)]
  public virtual int? ProjectTaskID { get; set; }

  /// <summary>
  /// The identifier of the account group to which corresponds the line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMAccountGroup.groupID" /> field.
  /// </value>
  [PXDBInt]
  [PXDimensionSelector("ACCGROUP", typeof (Search<PMAccountGroup.groupID>), typeof (PMAccountGroup.groupCD))]
  [PXUIField(DisplayName = "Account Group", Enabled = false)]
  public virtual int? AccountGroupID { get; set; }

  /// <summary>
  /// The identifier of the inventory item to which corresponds the line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.IN.InventoryItem.inventoryID" /> field.
  /// </value>
  [PXDBInt]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXUIField(DisplayName = "Inventory ID", Enabled = false)]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The identifier of the cost code to which corresponds the line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
  /// </value>
  [PXDBInt]
  [PXDimensionSelector("COSTCODE", typeof (Search<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))]
  [PXUIField(DisplayName = "Cost Code", Enabled = false, FieldClass = "COSTCODE")]
  public virtual int? CostCodeID { get; set; }

  /// <summary>The date to which corresponds the line.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  public virtual DateTime? Date { get; set; }

  /// <summary>The year to which corresponds the line.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Year", Enabled = false)]
  public int? Year { get; set; }

  /// <summary>
  /// The quarter of the <see cref="P:PX.Objects.PM.PMDateSensitiveDataRevisionLine.Year" /> to which corresponds the line.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Quarter", Enabled = false)]
  public int? Quarter { get; set; }

  /// <summary>The month to which corresponds the line.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Month", Enabled = false)]
  [PX.Objects.CR.Month.List]
  public int? Month { get; set; }

  /// <summary>
  /// The day of the <see cref="P:PX.Objects.PM.PMDateSensitiveDataRevisionLine.Month" /> to which corresponds the line.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Day", Enabled = false)]
  public int? Day { get; set; }

  /// <summary>
  /// The total quantity of the project transaction released by the <see cref="P:PX.Objects.PM.PMDateSensitiveDataRevisionLine.Date" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Quantity", Enabled = false)]
  public virtual Decimal? ActualQty { get; set; }

  /// <summary>
  /// The total amount of the project transaction released by the <see cref="P:PX.Objects.PM.PMDateSensitiveDataRevisionLine.Date" />.
  /// The amount is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMDateSensitiveDataRevisionLine.actualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount", Enabled = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  /// <summary>
  /// The total amount of the project transaction released by the <see cref="P:PX.Objects.PM.PMDateSensitiveDataRevisionLine.Date" /> in base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ActualAmount { get; set; }

  /// <summary>
  /// The quantity delta in the period to which corresponds the line.
  /// </summary>
  [PXDBQuantity]
  [PXUIField(DisplayName = "Actual PTD Quantity", Enabled = false)]
  public virtual Decimal? ActualQtyDiff { get; set; }

  /// <summary>
  /// The amount delta in the period to which corresponds the line.
  /// The amount is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMDateSensitiveDataRevisionLine.actualAmountDiff))]
  [PXUIField(DisplayName = "Actual PTD Amount", Enabled = false)]
  public virtual Decimal? CuryActualAmountDiff { get; set; }

  /// <summary>
  /// The amount delta in the period to which corresponds the line in base currency.
  /// </summary>
  [PXDBBaseCury]
  public virtual Decimal? ActualAmountDiff { get; set; }

  /// <exclude />
  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <exclude />
  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  /// <exclude />
  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  /// <exclude />
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  /// <exclude />
  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  /// <exclude />
  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  /// <exclude />
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<PMDateSensitiveDataRevisionLine>.By<PMDateSensitiveDataRevisionLine.projectID, PMDateSensitiveDataRevisionLine.curveID, PMDateSensitiveDataRevisionLine.pointNumber>
  {
    public static PMDateSensitiveDataRevisionLine Find(
      PXGraph graph,
      int? projectID,
      string curveID,
      int? pointNumber,
      PKFindOptions options = 0)
    {
      return (PMDateSensitiveDataRevisionLine) PrimaryKeyOf<PMDateSensitiveDataRevisionLine>.By<PMDateSensitiveDataRevisionLine.projectID, PMDateSensitiveDataRevisionLine.curveID, PMDateSensitiveDataRevisionLine.pointNumber>.FindBy(graph, (object) projectID, (object) curveID, (object) pointNumber, options);
    }
  }

  public static class FK
  {
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMDateSensitiveDataRevisionLine>.By<PMDateSensitiveDataRevisionLine.projectID>
    {
    }

    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMDateSensitiveDataRevisionLine>.By<PMDateSensitiveDataRevisionLine.projectID, PMDateSensitiveDataRevisionLine.projectTaskID>
    {
    }

    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<PMDateSensitiveDataRevisionLine>.By<PMDateSensitiveDataRevisionLine.accountGroupID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMDateSensitiveDataRevisionLine>.By<PMDateSensitiveDataRevisionLine.inventoryID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMDateSensitiveDataRevisionLine>.By<PMDateSensitiveDataRevisionLine.costCodeID>
    {
    }
  }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.projectID>
  {
  }

  public abstract class curveID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.curveID>
  {
  }

  public abstract class pointNumber : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.pointNumber>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.projectTaskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.accountGroupID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.inventoryID>
  {
  }

  public abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.costCodeID>
  {
  }

  public abstract class date : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.date>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMDateSensitiveDataRevisionLine.year>
  {
  }

  public abstract class quarter : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.quarter>
  {
  }

  public abstract class month : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMDateSensitiveDataRevisionLine.month>
  {
  }

  public abstract class day : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMDateSensitiveDataRevisionLine.day>
  {
  }

  public abstract class actualQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.actualQty>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.curyActualAmount>
  {
  }

  public abstract class actualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.actualAmount>
  {
  }

  public abstract class actualQtyDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.actualQtyDiff>
  {
  }

  public abstract class curyActualAmountDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.curyActualAmountDiff>
  {
  }

  public abstract class actualAmountDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.actualAmountDiff>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMDateSensitiveDataRevisionLine.lastModifiedDateTime>
  {
  }
}
