// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARReleaseProcessMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class ARReleaseProcessMultipleBaseCurrencies : PXGraphExtension<ARReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXOverride]
  public void PerformBasicReleaseChecks(
    PXGraph selectGraph,
    ARRegister document,
    ARReleaseProcessMultipleBaseCurrencies.PerformBasicReleaseChecksDelegate baseMethod)
  {
    baseMethod(selectGraph, document);
    IEnumerable<Customer> source = GraphHelper.RowCast<Customer>((IEnumerable) PXSelectBase<ARRegister, PXViewOf<ARRegister>.BasedOn<SelectFromBase<ARRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<ARRegister.branchID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.branchID>>>, FbqlJoins.Inner<Customer>.On<BqlOperand<ARRegister.customerID, IBqlInt>.IsEqual<Customer.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<P.AsString>>>>, And<BqlOperand<ARRegister.refNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.GL.Branch.baseCuryID, IBqlString>.IsNotEqual<Customer.baseCuryID>>>>.And<BqlOperand<Customer.baseCuryID, IBqlString>.IsNotNull>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[2]
    {
      (object) document.DocType,
      (object) document.RefNbr
    }));
    if (source.Any<Customer>())
    {
      Customer customer = source.First<Customer>();
      throw new ReleaseException("The document cannot be released, because the document's base currency differs from the base currency of the {0} entity associated with the {1} account.", new object[2]
      {
        (object) PXOrgAccess.GetCD((int?) customer?.COrgBAccountID),
        (object) customer?.AcctCD
      });
    }
  }

  public delegate void PerformBasicReleaseChecksDelegate(PXGraph selectGraph, ARRegister document);
}
