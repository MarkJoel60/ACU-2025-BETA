// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProductionBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[ExcludeFromCodeCoverage]
[PXProjection(typeof (Select4<PMCostBudget, Where<PMCostBudget.isProduction, Equal<True>, And<PMCostBudget.type, Equal<AccountType.expense>>>, Aggregate<GroupBy<PMCostBudget.projectID, GroupBy<PMCostBudget.revenueTaskID, GroupBy<PMCostBudget.revenueInventoryID, Sum<PMCostBudget.curyRevisedAmount, Sum<PMCostBudget.curyActualAmount>>>>>>>), Persistent = false)]
[Serializable]
public class PMProductionBudget : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMCostBudget.revenueTaskID))]
  public virtual int? RevenueTaskID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMCostBudget.revenueInventoryID))]
  public virtual int? RevenueInventoryID { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMCostBudget.curyRevisedAmount))]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMCostBudget.curyActualAmount))]
  public virtual Decimal? CuryActualAmount { get; set; }

  public abstract class projectID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMProductionBudget.projectID>
  {
  }

  public abstract class revenueTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProductionBudget.revenueTaskID>
  {
  }

  public abstract class revenueInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProductionBudget.revenueInventoryID>
  {
  }

  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProductionBudget.curyRevisedAmount>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProductionBudget.curyActualAmount>
  {
  }
}
