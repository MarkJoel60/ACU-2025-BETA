// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.ProjectEntryDistributionExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class ProjectEntryDistributionExt : PXGraphExtension<ProjectEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  [PXOverride]
  public virtual List<string> ValidateProjectClosure(
    int? projectID,
    Func<int?, List<string>> baseMethod)
  {
    List<string> stringList = baseMethod(projectID);
    IEnumerable<INTran> firstTableItems1 = PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<INTran.released, IBqlBool>.IsEqual<False>>>.Aggregate<To<GroupBy<INTran.docType>, GroupBy<INTran.refNbr>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems1.Select<INTran, string>((Func<INTran, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} inventory transaction related to the project is unreleased.", new object[1]
    {
      (object) x.RefNbr
    }))));
    IEnumerable<PX.Objects.SO.SOOrder> firstTableItems2 = PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.SO.SOOrder.status, IBqlString>.IsNotIn<SOOrderStatus.completed, SOOrderStatus.cancelled>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems2.Select<PX.Objects.SO.SOOrder, string>((Func<PX.Objects.SO.SOOrder, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} sales order related to the project is not completed.", new object[1]
    {
      (object) x.OrderNbr
    }))));
    IEnumerable<PX.Objects.PO.POOrder> firstTableItems3 = PXSelectBase<PX.Objects.PO.POOrder, PXViewOf<PX.Objects.PO.POOrder>.BasedOn<SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POLine>.On<BqlOperand<PX.Objects.PO.POLine.orderNbr, IBqlString>.IsEqual<PX.Objects.PO.POOrder.orderNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.PO.POOrder.status, IBqlString>.IsNotIn<POOrderStatus.closed, POOrderStatus.completed, POOrderStatus.cancelled>>>.Aggregate<To<GroupBy<PX.Objects.PO.POOrder.orderNbr>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems3.Select<PX.Objects.PO.POOrder, string>((Func<PX.Objects.PO.POOrder, string>) (x => PXMessages.LocalizeFormatNoPrefix(x.OrderType == "RS" ? "The project cannot be closed because the {0} subcontract related to the project is unprocessed." : "The project cannot be closed because the {0} purchase order related to the project is unprocessed.", new object[1]
    {
      (object) x.OrderNbr
    }))));
    return stringList;
  }
}
