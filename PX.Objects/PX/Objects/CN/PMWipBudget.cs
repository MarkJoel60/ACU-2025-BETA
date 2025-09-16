// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

[PXCacheName("PM WIP Budget")]
[PXProjection(typeof (SelectFrom<PMBudget>))]
[Serializable]
public class PMWipBudget : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(1, BqlField = typeof (PMBudget.type))]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMBudget.curyAmount))]
  public virtual Decimal? CuryAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipBudget.type, Equal<AccountType.income>>, PMWipBudget.curyAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? OriginalContractAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipBudget.type, Equal<AccountType.expense>>, PMWipBudget.curyAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? OriginalCostAmount { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMBudget.curyChangeOrderAmount))]
  public virtual Decimal? CuryChangeOrderAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipBudget.type, Equal<AccountType.income>>, PMWipBudget.curyChangeOrderAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? ChangeOrderContractAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipBudget.type, Equal<AccountType.expense>>, PMWipBudget.curyChangeOrderAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? ChangeOrderCostAmount { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMBudget.curyCostToComplete))]
  public virtual Decimal? CuryCostToComplete { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipBudget.type, Equal<AccountType.expense>>, PMWipBudget.curyCostToComplete>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CostToComplete { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMBudget.curyCostProjectionCostAtCompletion))]
  public virtual Decimal? CostProjectionCostAtCompletion { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipBudget.type, Equal<AccountType.expense>>, PMBudget.curyCostProjectionCostToComplete>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CostProjectionCostToComplete { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMBudget.curyCommittedOrigAmount))]
  public virtual Decimal? CuryCommittedOrigAmount { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMBudget.curyCommittedCOAmount))]
  public virtual Decimal? CuryCommittedCOAmount { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipBudget.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipBudget.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipBudget.costCodeID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipBudget.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipBudget.inventoryID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipBudget.type>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMWipBudget.curyAmount>
  {
  }

  public abstract class originalContractAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.originalContractAmount>
  {
  }

  public abstract class originalCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.originalCostAmount>
  {
  }

  public abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.curyChangeOrderAmount>
  {
  }

  public abstract class changeOrderContractAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.changeOrderContractAmount>
  {
  }

  public abstract class changeOrderCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.changeOrderCostAmount>
  {
  }

  public abstract class curyCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.curyCostToComplete>
  {
  }

  public abstract class costToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.costToComplete>
  {
  }

  public abstract class costProjectionCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.costProjectionCostAtCompletion>
  {
  }

  public abstract class costProjectionCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.costProjectionCostToComplete>
  {
  }

  public abstract class curyCommittedOrigAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.curyCommittedOrigAmount>
  {
  }

  public abstract class curyCommittedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipBudget.curyCommittedCOAmount>
  {
  }
}
