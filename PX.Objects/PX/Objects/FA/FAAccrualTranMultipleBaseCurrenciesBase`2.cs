// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAAccrualTranMultipleBaseCurrenciesBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable disable
namespace PX.Objects.FA;

public abstract class FAAccrualTranMultipleBaseCurrenciesBase<TGraphExtension, TGraph> : 
  PXGraphExtension<TGraphExtension, TGraph>
  where TGraphExtension : PXGraphExtension<TGraph>
  where TGraph : PXGraph
{
  protected virtual BqlCommand ModifySelectCommand(GLTranFilter filter, BqlCommand query)
  {
    if (filter.BranchID.HasValue)
    {
      query = BqlCommand.AppendJoin<LeftJoin<PX.Objects.GL.Branch, On<FAAccrualTran.gLTranBranchID, Equal<PX.Objects.GL.Branch.branchID>>>>(query);
      query = query.WhereAnd<Where<BqlOperand<PX.Objects.GL.Branch.baseCuryID, IBqlString>.IsEqual<BqlField<GLTranFilter.branchBaseCuryID, IBqlString>.FromCurrent>>>();
    }
    return query;
  }
}
