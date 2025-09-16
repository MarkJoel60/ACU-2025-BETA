// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.IntercompanyGoodsInTransitInq
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
public class IntercompanyGoodsInTransitInq : PXGraph<
#nullable disable
IntercompanyGoodsInTransitInq>
{
  public PXCancel<IntercompanyGoodsInTransitFilter> Cancel;
  public PXFilter<IntercompanyGoodsInTransitFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXViewOf<IntercompanyGoodsInTransitResult>.BasedOn<SelectFromBase<IntercompanyGoodsInTransitResult, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  IntercompanyGoodsInTransitResult.stkItem, 
  #nullable disable
  Equal<True>>>>, And<BqlOperand<
  #nullable enable
  IntercompanyGoodsInTransitResult.operation, IBqlString>.IsEqual<
  #nullable disable
  SOOperation.issue>>>, And<BqlOperand<
  #nullable enable
  IntercompanyGoodsInTransitResult.shipmentConfirmed, IBqlBool>.IsEqual<
  #nullable disable
  True>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  IntercompanyGoodsInTransitResult.pOReceiptNbr, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyGoodsInTransitResult.receiptReleased, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>, And<BqlOperand<
  #nullable enable
  IntercompanyGoodsInTransitResult.excludeFromIntercompanyProc, IBqlBool>.IsNotEqual<
  #nullable disable
  True>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.inventoryID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyGoodsInTransitResult.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  IntercompanyGoodsInTransitFilter.inventoryID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  IntercompanyGoodsInTransitResult.shipDate, IBqlDateTime>.IsLessEqual<
  #nullable disable
  BqlField<
  #nullable enable
  IntercompanyGoodsInTransitFilter.shippedBefore, IBqlDateTime>.FromCurrent>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.showOverdueItems>, 
  #nullable disable
  NotEqual<True>>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyGoodsInTransitResult.daysOverdue, IBqlInt>.IsNotNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.showItemsWithoutReceipt>, 
  #nullable disable
  NotEqual<True>>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyGoodsInTransitResult.pOReceiptNbr, IBqlString>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.sellingCompany>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyGoodsInTransitResult.sellingBranchBAccountID, IBqlInt>.IsEqual<
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
  IntercompanyGoodsInTransitResult.sellingSiteID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  IntercompanyGoodsInTransitFilter.sellingSiteID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  IntercompanyGoodsInTransitFilter.purchasingCompany>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  IntercompanyGoodsInTransitResult.purchasingBranchBAccountID, IBqlInt>.IsEqual<
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
  IntercompanyGoodsInTransitResult.purchasingSiteID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  IntercompanyGoodsInTransitFilter.purchasingSiteID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionLite<Not<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>>.Or<Where<IntercompanyGoodsInTransitResult.sellingBranchID, InsideBranchesOf<Current<IntercompanyGoodsInTransitFilter.orgBAccountID>>>>>>>.ReadOnly Results;

  public IntercompanyGoodsInTransitInq()
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
    PXUIFieldAttribute.SetVisible<IntercompanyGoodsInTransitFilter.sellingCompany>(((PXSelectBase) this.Filter).Cache, (object) null, !flag);
    if (!flag)
      return;
    PXUIFieldAttribute.SetDisplayName<IntercompanyGoodsInTransitFilter.orgBAccountID>(((PXSelectBase) this.Filter).Cache, "Selling Company/Branch");
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
