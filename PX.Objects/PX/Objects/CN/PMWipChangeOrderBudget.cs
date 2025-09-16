// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipChangeOrderBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

/// <summary>
/// A projection over the <see cref="T:PX.Objects.PM.PMChangeOrderBudget" /> class joined with the <see cref="T:PX.Objects.PM.PMChangeOrder" /> class.
/// The projection is used in WIP reports.
/// </summary>
[PXCacheName("PM WIP Change Order Budget")]
[PXProjection(typeof (Select2<PMChangeOrder, InnerJoin<PMChangeOrderBudget, On<PMChangeOrder.refNbr, Equal<PMChangeOrderBudget.refNbr>>>>))]
public class PMWipChangeOrderBudget : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderBudget.RefNbr" />
  [PXDBString(IsKey = true, BqlField = typeof (PMChangeOrderBudget.refNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderBudget.ProjectID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderBudget.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderBudget.ProjectTaskID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderBudget.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderBudget.CostCodeID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderBudget.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderBudget.AccountGroupID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderBudget.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderBudget.InventoryID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderBudget.inventoryID))]
  public virtual int? InventoryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderBudget.Type" />
  [PXDBString(1, BqlField = typeof (PMChangeOrderBudget.type))]
  public virtual string Type { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.Date" />
  [PXDBDate(BqlField = typeof (PMChangeOrder.date))]
  public virtual DateTime? Date { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.CompletionDate" />
  [PXDBDate(BqlField = typeof (PMChangeOrder.completionDate))]
  public virtual DateTime? CompletionDate { get; set; }

  /// <summary>
  /// The amount of the change order line in the base currency for the revenue budget.
  /// </summary>
  /// <value>The value is 0 for the cost budget.</value>
  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipChangeOrderBudget.type, Equal<AccountType.income>>, PMChangeOrderBudget.amount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? ContractAmount { get; set; }

  /// <summary>
  /// The amount of the change order line in the base currency for the cost budget.
  /// </summary>
  /// <value>The value is 0 for the revenue budget.</value>
  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipChangeOrderBudget.type, Equal<AccountType.expense>>, PMChangeOrderBudget.amount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CostAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.Status" />
  [PXDBString(1, BqlField = typeof (PMChangeOrder.status))]
  public virtual string Status { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipChangeOrderBudget.refNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipChangeOrderBudget.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipChangeOrderBudget.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipChangeOrderBudget.costCodeID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipChangeOrderBudget.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipChangeOrderBudget.inventoryID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipChangeOrderBudget.type>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMWipChangeOrderBudget.date>
  {
  }

  public abstract class completionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWipChangeOrderBudget.completionDate>
  {
  }

  public abstract class contractAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipChangeOrderBudget.contractAmount>
  {
  }

  public abstract class costAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipChangeOrderBudget.costAmount>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipChangeOrderBudget.status>
  {
  }
}
