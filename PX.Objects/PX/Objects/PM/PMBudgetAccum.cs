// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBudgetAccum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXBreakInheritance]
[PMBudgetAccum]
[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMBudgetAccum : PMBudget
{
  [PXDefault]
  [PXDBInt(IsKey = true)]
  public override int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PMTaskCompleted]
  [PXDefault]
  [PXDBInt(IsKey = true)]
  public override int? ProjectTaskID { get; set; }

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public override int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public override int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>The type of the budget line.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"A"</c>: Asset,
  /// <c>"L"</c>: Liability,
  /// <c>"I"</c>: Income,
  /// <c>"E"</c>: Expense,
  /// <c>"O"</c>: Off-Balance
  /// </value>
  [PXDBString(1)]
  public override 
  #nullable disable
  string Type { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">revenue project task</see> associated with the budget line.
  /// </summary>
  [PXDBInt]
  public override int? RevenueTaskID { get; set; }

  [PMUnit(typeof (PMBudgetAccum.inventoryID))]
  public override string UOM { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? ActualQty
  {
    get => this._ActualQty;
    set => this._ActualQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? InvoicedQty { get; set; }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetAccum.projectID>
  {
  }

  public new abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetAccum.projectTaskID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMBudgetAccum.accountGroupID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetAccum.inventoryID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetAccum.costCodeID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudgetAccum.type>
  {
  }

  public new abstract class revenueTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetAccum.revenueTaskID>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudgetAccum.uOM>
  {
  }

  public new abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetAccum.actualQty>
  {
  }

  public new abstract class invoicedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetAccum.invoicedQty>
  {
  }
}
