// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.DataProviders.FinancialPeriodDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Bql;
using PX.Objects.GL.FinPeriods;

#nullable disable
namespace PX.Objects.CN.Common.Services.DataProviders;

public class FinancialPeriodDataProvider
{
  public static OrganizationFinPeriod GetFinancialPeriod(PXGraph graph, string financialPeriodId)
  {
    return PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXViewOf<OrganizationFinPeriod>.BasedOn<SelectFromBase<OrganizationFinPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<OrganizationFinPeriod.finPeriodID, Equal<P.AsString>>>>>.And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>>>.Config>.Select(graph, new object[1]
    {
      (object) financialPeriodId
    }));
  }
}
