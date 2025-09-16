// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.IntercompanyReturnedGoodsInTransitInq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class IntercompanyReturnedGoodsInTransitInq : PXGraph<
#nullable disable
IntercompanyReturnedGoodsInTransitInq>
{
  public PXCancel<IntercompanyGoodsInTransitFilter> Cancel;
  public PXFilter<IntercompanyGoodsInTransitFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXViewOf<IntercompanyReturnedGoodsInTransitResult>.BasedOn<SelectFromBase<IntercompanyReturnedGoodsInTransitResult, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<SOShipLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOShipLine.origOrderType, 
  #nullable disable
  Equal<IntercompanyReturnedGoodsInTransitResult.sOType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOShipLine.origOrderNbr, 
  #nullable disable
  Equal<IntercompanyReturnedGoodsInTransitResult.sONbr>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOShipLine.origLineNbr, 
  #nullable disable
  Equal<IntercompanyReturnedGoodsInTransitResult.sOLineNbr>>>>>.And<BqlOperand<
  #nullable enable
  SOShipLine.confirmed, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>>, FbqlJoins.Left<PX.Objects.PO.POReceipt>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.PO.POReceipt.receiptType, 
  #nullable disable
  Equal<IntercompanyReturnedGoodsInTransitResult.origReceiptType>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.IsEqual<
  #nullable disable
  IntercompanyReturnedGoodsInTransitResult.origReceiptNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOShipLine.shipmentNbr, 
  #nullable disable
  IsNull>>>, And<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.stkItem, IBqlBool>.IsEqual<
  #nullable disable
  True>>>, And<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.returnReleased, IBqlBool>.IsEqual<
  #nullable disable
  True>>>, And<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.excludeFromIntercompanyProc, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.inventoryID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  IntercompanyGoodsInTransitFilter.inventoryID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.returnDate, IBqlDateTime>.IsLessEqual<
  #nullable disable
  BqlField<
  #nullable enable
  IntercompanyGoodsInTransitFilter.shippedBefore, IBqlDateTime>.FromCurrent>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.showItemsWithoutReceipt>, 
  #nullable disable
  NotEqual<True>>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.shipmentNbr, IBqlString>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.purchasingCompany>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.purchasingBranchBAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  IntercompanyGoodsInTransitFilter.purchasingCompany, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.purchasingSiteID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.purchasingSiteID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  IntercompanyGoodsInTransitFilter.purchasingSiteID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.sellingCompany>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.sellingBranchBAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  IntercompanyGoodsInTransitFilter.sellingCompany, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.sellingSiteID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.sellingSiteID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  IntercompanyGoodsInTransitFilter.sellingSiteID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.sONbr, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyReturnedGoodsInTransitResult.sOBehavior, IBqlString>.IsEqual<
  #nullable disable
  SOBehavior.rM>>>>>.And<BqlChainableConditionLite<Not<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>>.Or<Where<IntercompanyReturnedGoodsInTransitResult.purchasingBranchID, InsideBranchesOf<Current<IntercompanyGoodsInTransitFilter.orgBAccountID>>>>>>>.ReadOnly Results;

  public IntercompanyReturnedGoodsInTransitInq()
  {
    PXUIFieldAttribute.SetVisible<IntercompanyGoodsInTransitFilter.showItemsWithoutReceipt>(((PXSelectBase) this.Filter).Cache, (object) null, false);
    PXCache cach = ((PXGraph) this).Caches[typeof (PX.Objects.PO.POReceipt)];
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POReceipt.receiptNbr>(cach, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POReceipt.receiptDate>(cach, (object) null, false);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.PO.POReceipt.receiptDate>(cach, "Receipt Date");
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
    PXUIFieldAttribute.SetVisible<IntercompanyGoodsInTransitFilter.purchasingCompany>(((PXSelectBase) this.Filter).Cache, (object) null, !flag);
    if (!flag)
      return;
    PXUIFieldAttribute.SetDisplayName<IntercompanyGoodsInTransitFilter.orgBAccountID>(((PXSelectBase) this.Filter).Cache, "Purchasing Company/Branch");
  }

  protected virtual IEnumerable results()
  {
    using (new PXReadBranchRestrictedScope())
    {
      PXView pxView = new PXView((PXGraph) this, true, ((PXSelectBase) this.Results).View.BqlSelect);
      int startRow = PXView.StartRow;
      int num = 0;
      object[] currents = PXView.Currents;
      object[] parameters = PXView.Parameters;
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num;
      List<object> objectList = pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
      PXView.StartRow = 0;
      return (IEnumerable) objectList;
    }
  }
}
