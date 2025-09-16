// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.DataProviders.CommitmentDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.PO;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CN.Common.Services.DataProviders;

public class CommitmentDataProvider
{
  public static POOrder GetCommitment(PXGraph graph, string orderNumber, string orderType)
  {
    return PXResultset<POOrder>.op_Implicit(PXSelectBase<POOrder, PXViewOf<POOrder>.BasedOn<SelectFromBase<POOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POOrder.orderNbr, Equal<P.AsString>>>>>.And<BqlOperand<POOrder.orderType, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(graph, new object[2]
    {
      (object) orderNumber,
      (object) orderType
    }));
  }

  public static IEnumerable<POLine> GetCommitmentLines(
    PXGraph graph,
    string orderNumber,
    string orderType,
    int? projectId)
  {
    return PXSelectBase<POLine, PXViewOf<POLine>.BasedOn<SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.orderNbr, Equal<P.AsString>>>>, And<BqlOperand<POLine.orderType, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<POLine.projectID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select(graph, new object[3]
    {
      (object) orderNumber,
      (object) orderType,
      (object) projectId
    }).FirstTableItems;
  }
}
