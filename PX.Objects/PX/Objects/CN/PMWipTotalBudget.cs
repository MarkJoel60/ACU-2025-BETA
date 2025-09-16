// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipTotalBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.CN;

[PXCacheName("PM WIP Total Budget")]
[PXProjection(typeof (SelectFromBase<PMWipCostProjectionBudget, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<PMWipCostProjectionBudget.projectID, Sum<PMWipCostProjectionBudget.originalContractAmount, Sum<PMWipCostProjectionBudget.originalCostAmount, Sum<PMWipCostProjectionBudget.changeOrderContractAmount, Sum<PMWipCostProjectionBudget.changeOrderCostAmount, Sum<PMWipCostProjectionBudget.costToComplete, Sum<PMWipCostProjectionBudget.costProjectionCostAtCompletion, Sum<PMWipCostProjectionBudget.costProjectionCostToComplete, Sum<PMWipCostProjectionBudget.curyCommittedOrigAmount, Sum<PMWipCostProjectionBudget.curyCommittedCOAmount, Sum<PMWipCostProjectionBudget.projectedCostAtCompletion>>>>>>>>>>>>))]
[Serializable]
public class PMWipTotalBudget : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMWipCostProjectionBudget.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBDecimal(BqlField = typeof (PMWipCostProjectionBudget.originalContractAmount))]
  public virtual Decimal? OriginalContractAmount { get; set; }

  [PXDBDecimal(BqlField = typeof (PMWipCostProjectionBudget.originalCostAmount))]
  public virtual Decimal? OriginalCostAmount { get; set; }

  [PXDBDecimal(BqlField = typeof (PMWipCostProjectionBudget.changeOrderContractAmount))]
  public virtual Decimal? ChangeOrderContractAmount { get; set; }

  [PXDBDecimal(BqlField = typeof (PMWipCostProjectionBudget.changeOrderCostAmount))]
  public virtual Decimal? ChangeOrderCostAmount { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMWipCostProjectionBudget.costToComplete))]
  public virtual Decimal? CostToComplete { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMWipCostProjectionBudget.costProjectionCostAtCompletion))]
  public virtual Decimal? CostProjectionCostAtCompletion { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMWipCostProjectionBudget.costProjectionCostToComplete))]
  public virtual Decimal? CostProjectionCostToComplete { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMWipCostProjectionBudget.curyCommittedOrigAmount))]
  public virtual Decimal? CuryCommittedOrigAmount { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMWipCostProjectionBudget.curyCommittedCOAmount))]
  public virtual Decimal? CuryCommittedCOAmount { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMWipCostProjectionBudget.projectedCostAtCompletion))]
  public virtual Decimal? ProjectedCostAtCompletion { get; set; }

  public abstract class projectID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMWipTotalBudget.projectID>
  {
  }

  public abstract class originalContractAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalBudget.originalContractAmount>
  {
  }

  public abstract class originalCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalBudget.originalCostAmount>
  {
  }

  public abstract class changeOrderContractAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalBudget.changeOrderContractAmount>
  {
  }

  public abstract class changeOrderCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalBudget.changeOrderCostAmount>
  {
  }

  public abstract class costToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalBudget.costToComplete>
  {
  }

  public abstract class costProjectionCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalBudget.costProjectionCostAtCompletion>
  {
  }

  public abstract class costProjectionCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalBudget.costProjectionCostToComplete>
  {
  }

  public abstract class curyCommittedOrigAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalBudget.curyCommittedOrigAmount>
  {
  }

  public abstract class curyCommittedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalBudget.curyCommittedCOAmount>
  {
  }

  public abstract class projectedCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalBudget.projectedCostAtCompletion>
  {
  }
}
