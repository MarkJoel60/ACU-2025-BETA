// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMForecastHistoryAccum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXBreakInheritance]
[PMForecastHistoryAccum]
[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMForecastHistoryAccum : PMForecastHistory
{
  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> of the project budget forecast.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public override int? ProjectID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMTask">project task</see> of the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public override int? ProjectTaskID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMAccountGroup">project account group</see> of the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public override int? AccountGroupID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> of the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public override int? InventoryID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMCostCode">project cost code</see> of the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostCode.CostCodeID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public override int? CostCodeID { get; set; }

  /// <summary>The financial period.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.FinPeriodID" /> field.
  /// </value>
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  public override 
  #nullable disable
  string PeriodID { get; set; }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastHistoryAccum.projectID>
  {
  }

  public new abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMForecastHistoryAccum.projectTaskID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMForecastHistoryAccum.accountGroupID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMForecastHistoryAccum.inventoryID>
  {
  }

  public new abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMForecastHistoryAccum.costCodeID>
  {
  }

  public new abstract class periodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMForecastHistoryAccum.periodID>
  {
  }
}
