// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents the aggregated history of the project budget.
/// Contains the project's actual amounts and actual quantities aggregated by financial periods.
/// </summary>
[PXCacheName("Project History")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ProjectID;
  protected int? _ProjectTaskID;
  protected int? _AccountGroupID;
  protected int? _InventoryID;
  protected int? _CostCodeID;
  protected 
  #nullable disable
  string _PeriodID;
  protected int? _BranchID;
  protected Decimal? _FinPTDQty;
  protected Decimal? _TranPTDQty;
  protected Decimal? _FinYTDQty;
  protected Decimal? _TranYTDQty;
  protected byte[] _tstamp;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Project ID")]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">project task</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Project Task ID")]
  public virtual int? ProjectTaskID
  {
    get => this._ProjectTaskID;
    set => this._ProjectTaskID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">project account group</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Account Group ID")]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Inventory ID")]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">project cost code</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostCode.CostCodeID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Cost Code")]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>The financial period of the master calendar.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.FinPeriodID" /> field.
  /// </value>
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (MasterFinPeriod.finPeriodID), DescriptionField = typeof (MasterFinPeriod.descr))]
  public virtual string PeriodID
  {
    get => this._PeriodID;
    set => this._PeriodID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> associated with the history record.
  /// This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.Objects.GL.Branch.BAccountID" /> field.
  /// </value>
  [Branch(null, null, true, true, true, IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// The financial PTD quantity calculated based on <see cref="P:PX.Objects.PM.PMTran.Qty" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Financial PTD Quantity")]
  public virtual Decimal? FinPTDQty
  {
    get => this._FinPTDQty;
    set => this._FinPTDQty = value;
  }

  /// <summary>
  /// The transaction PTD quantity calculated based on <see cref="P:PX.Objects.PM.PMTran.Qty" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPTDQty
  {
    get => this._TranPTDQty;
    set => this._TranPTDQty = value;
  }

  /// <summary>
  /// The financial PTD amount calculated based on <see cref="P:PX.Objects.PM.PMTran.ProjectCuryAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Financial PTD Amount")]
  public virtual Decimal? FinPTDCuryAmount { get; set; }

  /// <summary>
  /// The financial PTD amount calculated based on <see cref="P:PX.Objects.PM.PMTran.Amount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Financial PTD Amount")]
  public virtual Decimal? FinPTDAmount { get; set; }

  /// <summary>
  /// The transaction PTD amount calculated based on <see cref="P:PX.Objects.PM.PMTran.ProjectCuryAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPTDCuryAmount { get; set; }

  /// <summary>
  /// The transaction PTD amount calculated based on <see cref="P:PX.Objects.PM.PMTran.Amount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPTDAmount { get; set; }

  /// <summary>
  /// The financial YTD quantity calculated based on <see cref="P:PX.Objects.PM.PMTran.Qty" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYTDQty
  {
    get => this._FinYTDQty;
    set => this._FinYTDQty = value;
  }

  /// <summary>
  /// The transaction YTD quantity calculated based on <see cref="P:PX.Objects.PM.PMTran.Qty" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYTDQty
  {
    get => this._TranYTDQty;
    set => this._TranYTDQty = value;
  }

  /// <summary>
  /// The financial YTD amount calculated based on <see cref="P:PX.Objects.PM.PMTran.ProjectCuryAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYTDCuryAmount { get; set; }

  /// <summary>
  /// The financial YTD amount calculated based on <see cref="P:PX.Objects.PM.PMTran.Amount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYTDAmount { get; set; }

  /// <summary>
  /// The transaction YTD amount calculated based on <see cref="P:PX.Objects.PM.PMTran.ProjectCuryAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYTDCuryAmount { get; set; }

  /// <summary>
  /// The transaction YTD amount calculated based on <see cref="P:PX.Objects.PM.PMTran.Amount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYTDAmount { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistory.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistory.projectTaskID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistory.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistory.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistory.costCodeID>
  {
  }

  public abstract class periodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMHistory.periodID>
  {
  }

  public abstract class finPTDQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMHistory.finPTDQty>
  {
  }

  public abstract class tranPTDQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMHistory.tranPTDQty>
  {
  }

  public abstract class finPTDCuryAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMHistory.finPTDCuryAmount>
  {
  }

  public abstract class finPTDAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMHistory.finPTDAmount>
  {
  }

  public abstract class tranPTDCuryAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMHistory.tranPTDCuryAmount>
  {
  }

  public abstract class tranPTDAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMHistory.tranPTDAmount>
  {
  }

  public abstract class finYTDQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMHistory.finYTDQty>
  {
  }

  public abstract class tranYTDQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMHistory.tranYTDQty>
  {
  }

  public abstract class finYTDCuryAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMHistory.finYTDCuryAmount>
  {
  }

  public abstract class finYTDAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMHistory.finYTDAmount>
  {
  }

  public abstract class tranYTDCuryAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMHistory.tranYTDCuryAmount>
  {
  }

  public abstract class tranYTDAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMHistory.tranYTDAmount>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMHistory.Tstamp>
  {
  }
}
