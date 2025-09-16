// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipCostProjection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

[PXCacheName("PM WIP Cost Projection")]
[PXProjection(typeof (SelectFromBase<PMCostProjectionLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMCostProjection>.On<BqlOperand<PMCostProjectionLine.projectID, IBqlInt>.IsEqual<PMCostProjection.projectID>>>>.Where<BqlOperand<PMCostProjection.released, IBqlBool>.IsEqual<True>>.AggregateTo<GroupBy<PMCostProjectionLine.projectID>, GroupBy<PMCostProjectionLine.costCodeID>, GroupBy<PMCostProjectionLine.accountGroupID>, GroupBy<PMCostProjectionLine.taskID>, GroupBy<PMCostProjectionLine.inventoryID>>))]
[Serializable]
public class PMWipCostProjection : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMCostProjectionLine.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(BqlField = typeof (PMCostProjectionLine.taskID))]
  public virtual int? TaskID { get; set; }

  [PXDBInt(BqlField = typeof (PMCostProjectionLine.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBInt(BqlField = typeof (PMCostProjectionLine.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  [PXDBInt(BqlField = typeof (PMCostProjectionLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  public abstract class projectID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMWipCostProjection.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipCostProjection.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipCostProjection.costCodeID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipCostProjection.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipCostProjection.inventoryID>
  {
  }
}
