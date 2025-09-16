// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimDetailSalesAccountID`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public class ExpenseClaimDetailSalesAccountID<IsBillable, InventoryID, CustomerID, CustomerLocationID> : 
  BqlFormulaEvaluator<IsBillable, InventoryID, CustomerID, CustomerLocationID>
  where IsBillable : IBqlField
  where InventoryID : IBqlField
  where CustomerID : IBqlField
  where CustomerLocationID : IBqlField
{
  public override object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> parameters)
  {
    if (!((bool?) parameters[typeof (IsBillable)]).GetValueOrDefault())
      return (object) null;
    if (((ARSetup) PXSelectBase<ARSetup, PXViewOf<ARSetup>.BasedOn<SelectFromBase<ARSetup, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.Select(cache.Graph))?.IntercompanySalesAccountDefault == "L")
    {
      int? parameter1 = (int?) parameters[typeof (CustomerID)];
      PXResult<PX.Objects.AR.Customer, PX.Objects.CR.Location> pxResult = (PXResult<PX.Objects.AR.Customer, PX.Objects.CR.Location>) (PXResult<PX.Objects.AR.Customer>) PXSelectBase<PX.Objects.AR.Customer, PXViewOf<PX.Objects.AR.Customer>.BasedOn<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Location>.On<BqlOperand<PX.Objects.AR.Customer.defLocationID, IBqlInt>.IsEqual<PX.Objects.CR.Location.locationID>>>>.Where<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound(cache.Graph, (object[]) null, (object) parameter1);
      PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) pxResult;
      PX.Objects.CR.Location location = (PX.Objects.CR.Location) pxResult;
      if (customer != null && customer.IsBranch.GetValueOrDefault())
      {
        int? parameter2 = (int?) parameters[typeof (CustomerLocationID)];
        return (object) (int?) ((PX.Objects.CR.Location) PXSelectBase<PX.Objects.CR.Location, PXViewOf<PX.Objects.CR.Location>.BasedOn<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound(cache.Graph, (object[]) null, (object) parameter2) ?? location)?.CSalesAcctID;
      }
    }
    int? parameter = (int?) parameters[typeof (InventoryID)];
    return (object) (int?) ((PX.Objects.IN.InventoryItem) PXSelectBase<PX.Objects.IN.InventoryItem, PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound(cache.Graph, (object[]) null, (object) parameter))?.SalesAcctID;
  }
}
