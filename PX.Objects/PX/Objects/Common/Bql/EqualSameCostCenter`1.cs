// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Bql.EqualSameCostCenter`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.SQLTree;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.Bql;

/// <exclude />
public class EqualSameCostCenter<Operand> : In<Operand> where Operand : IBqlParameter
{
  public virtual void Verify(
    PXCache cacheBase,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(false);
    List<int> intList = (List<int>) null;
    if (((InBase<Operand>) this)._operand1 == null)
      ((InBase<Operand>) this)._operand1 = BqlCommon.createOperand<Operand>(((InBase<Operand>) this)._operand1);
    if (!(((InBase<Operand>) this)._operand1 is IBqlParameter operand1))
      throw new NotImplementedException();
    if (pars.Count <= 0)
      return;
    pars.RemoveAt(0);
    Type referencedType = operand1.GetReferencedType();
    if (referencedType.IsNested && cacheBase.Graph != null)
    {
      Type itemType = BqlCommand.GetItemType(referencedType);
      PXCache cach = cacheBase.Graph.Caches[itemType];
      if (cach.InternalCurrent != null)
      {
        int? costCenterID = cach.GetValue(cach.Current, referencedType.Name) as int?;
        if (costCenterID.HasValue)
          intList = this.GetSameCostCenters(cach.Graph, costCenterID);
      }
      else
      {
        result = new bool?(true);
        return;
      }
    }
    result = new bool?(intList != null && value is int num && intList.Contains(num));
  }

  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    if (((InBase<Operand>) this)._operand1 == null)
      ((InBase<Operand>) this)._operand1 = BqlCommon.createOperand<Operand>(((InBase<Operand>) this)._operand1);
    SQLExpression sqlExpression = (SQLExpression) null;
    bool flag2 = flag1 & ((InBase<Operand>) this)._operand1.AppendExpression(ref sqlExpression, graph, info, selection);
    if (graph == null || !info.BuildExpression)
      return flag2;
    List<int> source = (List<int>) null;
    if (((InBase<Operand>) this)._operand1 is IBqlParameter operand1)
    {
      Type referencedType = operand1.GetReferencedType();
      Type type = referencedType.IsNested ? BqlCommand.GetItemType(referencedType) : throw new NotImplementedException();
      PXCache cach = graph.Caches[type];
      if (cach.Current != null)
      {
        int? costCenterID = cach.GetValue(cach.Current, referencedType.Name) as int?;
        if (costCenterID.HasValue)
          source = this.GetSameCostCenters(cach.Graph, costCenterID);
      }
    }
    // ISSUE: explicit non-virtual call
    exp = source == null || __nonvirtual (source.Count) <= 0 ? SQLExpressionExt.EQ((SQLExpression) new SQLConst((object) 1), (object) 0) : exp.In((IEnumerable<SQLExpression>) source.Select<int, SQLConst>((Func<int, SQLConst>) (c => new SQLConst((object) c))));
    return flag2;
  }

  private List<int> GetSameCostCenters(PXGraph graph, int? costCenterID)
  {
    int? nullable = costCenterID;
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return new List<int>() { 0 };
    INCostCenter costCenter = INCostCenter.PK.Find(graph, costCenterID);
    if (costCenter == null)
      return (List<int>) null;
    switch (costCenter.CostLayerType)
    {
      case "S":
        return this.GetSameSpecialCostCenters(graph, costCenter);
      case "P":
        return this.GetSameProjectCostCenters(graph, costCenter);
      default:
        if (ProjectDefaultAttribute.IsProject(graph, costCenter.ProjectID))
          return this.GetSameProjectCostCenters(graph, costCenter);
        throw new NotImplementedException();
    }
  }

  private List<int> GetSameSpecialCostCenters(PXGraph graph, INCostCenter costCenter)
  {
    return ((IEnumerable<PXResult<INCostCenter>>) PXSelectBase<INCostCenter, PXViewOf<INCostCenter>.BasedOn<SelectFromBase<INCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostCenter.costLayerType, Equal<BqlField<INCostCenter.costLayerType, IBqlString>.FromCurrent>>>>, And<BqlOperand<INCostCenter.sOOrderType, IBqlString>.IsEqual<BqlField<INCostCenter.sOOrderType, IBqlString>.FromCurrent>>>, And<BqlOperand<INCostCenter.sOOrderNbr, IBqlString>.IsEqual<BqlField<INCostCenter.sOOrderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<INCostCenter.sOOrderLineNbr, IBqlInt>.IsEqual<BqlField<INCostCenter.sOOrderLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      (object) costCenter
    }, Array.Empty<object>())).AsEnumerable<PXResult<INCostCenter>>().Select<PXResult<INCostCenter>, int>((Func<PXResult<INCostCenter>, int>) (c => PXResult<INCostCenter>.op_Implicit(c).CostCenterID.Value)).ToList<int>();
  }

  private List<int> GetSameProjectCostCenters(PXGraph graph, INCostCenter costCenter)
  {
    return ((IEnumerable<PXResult<INCostCenter>>) PXSelectBase<INCostCenter, PXViewOf<INCostCenter>.BasedOn<SelectFromBase<INCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostCenter.costLayerType, Equal<BqlField<INCostCenter.costLayerType, IBqlString>.FromCurrent>>>>, And<BqlOperand<INCostCenter.projectID, IBqlInt>.IsEqual<BqlField<INCostCenter.projectID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INCostCenter.taskID, IBqlInt>.IsEqual<BqlField<INCostCenter.taskID, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      (object) costCenter
    }, Array.Empty<object>())).AsEnumerable<PXResult<INCostCenter>>().Select<PXResult<INCostCenter>, int>((Func<PXResult<INCostCenter>, int>) (c => PXResult<INCostCenter>.op_Implicit(c).CostCenterID.Value)).ToList<int>();
  }
}
