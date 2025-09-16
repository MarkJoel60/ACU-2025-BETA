// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APReleaseProcessMultipleBaseCurrencies
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
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public class APReleaseProcessMultipleBaseCurrencies : PXGraphExtension<APReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  [PXOverride]
  public void PerformBasicReleaseChecks(
    APRegister document,
    APReleaseProcessMultipleBaseCurrencies.PerformBasicReleaseChecksDelegate baseMethod)
  {
    baseMethod(document);
    IEnumerable<Vendor> source = PXSelectBase<APRegister, PXViewOf<APRegister>.BasedOn<SelectFromBase<APRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<APRegister.branchID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.branchID>>>, FbqlJoins.Inner<Vendor>.On<BqlOperand<APRegister.vendorID, IBqlInt>.IsEqual<Vendor.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.docType, Equal<P.AsString>>>>, PX.Data.And<BqlOperand<APRegister.refNbr, IBqlString>.IsEqual<P.AsString>>>, PX.Data.And<BqlOperand<PX.Objects.GL.Branch.baseCuryID, IBqlString>.IsNotEqual<Vendor.baseCuryID>>>>.And<BqlOperand<Vendor.baseCuryID, IBqlString>.IsNotNull>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) document.DocType, (object) document.RefNbr).RowCast<Vendor>();
    if (source.Any<Vendor>())
    {
      Vendor vendor = source.First<Vendor>();
      throw new ReleaseException("The document cannot be released, because the document's base currency differs from the base currency of the {0} entity associated with the {1} account.", new object[2]
      {
        (object) PXOrgAccess.GetCD((int?) vendor?.VOrgBAccountID),
        (object) vendor?.AcctCD
      });
    }
  }

  public delegate void PerformBasicReleaseChecksDelegate(APRegister document);
}
