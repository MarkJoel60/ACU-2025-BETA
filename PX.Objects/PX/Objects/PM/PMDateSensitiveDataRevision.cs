// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMDateSensitiveDataRevision
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CT;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents the Project Financial Vision filter on the Project Financial Vision (PM405000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProjectDateSensitiveCostsInquiry" /> graph).
/// </summary>
[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMDateSensitiveDataRevision : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The project identifier.</summary>
  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>), WarnIfCompleted = false, Required = true)]
  public virtual int? ProjectID { get; set; }

  /// <summary>The period type of the date sensitive data analysis.</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("Y")]
  [PXUIField(DisplayName = "Period")]
  [ProjectDateSensitivePeriod.List]
  public virtual 
  #nullable disable
  string Period { get; set; }

  /// <summary>
  /// The account group type of the date sensitive data analysis.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("E")]
  [PXUIField(DisplayName = "Account Group Type")]
  [ProjectDateSensitiveAccountGroups.List]
  public virtual string AccountGroups { get; set; }

  /// <summary>
  /// The start date of time range of the date sensitive data analysis.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate { get; set; }

  /// <summary>
  /// The end date of time range of the date sensitive data analysis.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate { get; set; }

  /// <summary>The value of the filter by project task.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [ProjectTask(typeof (PMDateSensitiveDataRevision.projectID), SkipDefaultTask = true, AllowNull = true)]
  public virtual int? ProjectTaskID { get; set; }

  /// <summary>The value of the filter by account group.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [AccountGroup(typeof (Where<BqlChainableConditionLite<Match<PMAccountGroup, Current<AccessInfo.userName>>>.And<BqlOperand<PMAccountGroup.type, IBqlString>.IsEqual<BqlField<PMDateSensitiveDataRevision.accountGroups, IBqlString>.FromCurrent>>>))]
  public virtual int? AccountGroupID { get; set; }

  /// <summary>The value of the filter by inventory group.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PMInventorySelector(Filterable = true)]
  public virtual int? InventoryID { get; set; }

  /// <summary>The value of the filter by cost code.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostCode.CostCodeID" /> field.
  /// </value>
  [CostCode(Filterable = false, SkipVerification = true)]
  public virtual int? CostCodeID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the data should be grouped by project task.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Group by Project Task")]
  public virtual bool? GroupByProjectTaskID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the data should be grouped by account group.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Group by Account Group")]
  public virtual bool? GroupByAccountGroupID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the data should be grouped by inventory item.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Group by Inventory ID")]
  public virtual bool? GroupByInventoryID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the data should be grouped by cost code.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Group by Cost Code", FieldClass = "COSTCODE")]
  public virtual bool? GroupByCostCodeID { get; set; }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.projectID>
  {
  }

  public abstract class period : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.period>
  {
  }

  public abstract class accountGroups : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.accountGroups>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.endDate>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.projectTaskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.accountGroupID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.inventoryID>
  {
  }

  public abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.costCodeID>
  {
  }

  public abstract class groupByProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.groupByProjectTaskID>
  {
  }

  public abstract class groupByAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.groupByAccountGroupID>
  {
  }

  public abstract class groupByInventoryID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.groupByInventoryID>
  {
  }

  public abstract class groupByCostCodeID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMDateSensitiveDataRevision.groupByCostCodeID>
  {
  }
}
