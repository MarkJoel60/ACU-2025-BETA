// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PurchaserOrganizationID`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class PurchaserOrganizationID<CustomerID> : BqlFormulaEvaluator<CustomerID> where CustomerID : IBqlField
{
  public override object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> parameters)
  {
    int? parameter = (int?) parameters[typeof (CustomerID)];
    object obj = (object) PXAccess.GetParentOrganizationID(cache.Graph.Accessinfo.BranchID);
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.centralizedPeriodsManagement>() && parameter.HasValue)
      obj = (object) (int?) ((PX.Objects.GL.Branch) PXSelectBase<PX.Objects.GL.Branch, PXViewOf<PX.Objects.GL.Branch>.BasedOn<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Branch.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound(cache.Graph, (object[]) null, (object) parameter))?.OrganizationID ?? obj;
    return obj;
  }
}
