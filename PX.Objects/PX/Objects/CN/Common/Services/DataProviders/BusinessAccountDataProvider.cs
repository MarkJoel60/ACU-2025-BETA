// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.DataProviders.BusinessAccountDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.CN.Common.Services.DataProviders;

public class BusinessAccountDataProvider : IBusinessAccountDataProvider
{
  public BAccount GetBusinessAccount(PXGraph graph, int? accountId)
  {
    return PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXViewOf<BAccount>.BasedOn<SelectFromBase<BAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) accountId
    }));
  }

  public BAccountR GetBusinessAccountReceivable(PXGraph graph, int? accountId)
  {
    return PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) accountId
    }));
  }
}
