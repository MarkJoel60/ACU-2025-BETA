// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookPeriodUtils
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

internal class FABookPeriodUtils : IFABookPeriodUtils
{
  protected readonly PXGraph Graph;
  protected IFABookPeriodRepository FABookPeriodRepositoryHelper;

  public FABookPeriodUtils(PXGraph graph)
  {
    this.Graph = graph;
    this.FABookPeriodRepositoryHelper = this.Graph.GetService<IFABookPeriodRepository>();
  }

  public virtual int? PeriodMinusPeriod(
    string finPeriodID1,
    string finPeriodID2,
    int? bookID,
    int? assetID)
  {
    FABookPeriodRepository.CheckNotNullOrEmptyStringContract(finPeriodID1, nameof (finPeriodID1));
    FABookPeriodRepository.CheckNotNullOrEmptyStringContract(finPeriodID2, nameof (finPeriodID2));
    FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
    FABookPeriodRepository.CheckNotNullIDContract(assetID, nameof (assetID));
    if (PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<Where<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>, Or<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>>>>.Config>.Select(this.Graph, new object[4]
    {
      (object) bookID,
      (object) this.FABookPeriodRepositoryHelper.GetFABookPeriodOrganizationID(bookID, assetID),
      (object) finPeriodID1,
      (object) finPeriodID2
    }).Count < 2 && !string.Equals(finPeriodID1, finPeriodID2))
      throw new PXException("Book Calendar cannot be found in the system.");
    return ((PXResult) PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXSelectGroupBy<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, LessEqual<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.finPeriodID, Greater<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.endDate, Greater<FABookPeriod.startDate>>>>>>, Aggregate<GroupBy<FABookPeriod.bookID, GroupBy<FABookPeriod.organizationID, Count>>>>.Config>.Select(this.Graph, new object[4]
    {
      (object) bookID,
      (object) this.FABookPeriodRepositoryHelper.GetFABookPeriodOrganizationID(bookID, assetID),
      (object) finPeriodID1,
      (object) finPeriodID2
    })))?.RowCount;
  }

  public string PeriodPlusPeriodsCount(string finPeriodID, int counter, int? bookID, int? assetID)
  {
    FABookPeriodRepository.CheckNotNullIDContract(assetID, nameof (assetID));
    int periodOrganizationId = this.FABookPeriodRepositoryHelper.GetFABookPeriodOrganizationID(bookID, assetID);
    return this.PeriodPlusPeriodsCount(finPeriodID, counter, bookID, periodOrganizationId);
  }

  public string PeriodPlusPeriodsCount(
    string finPeriodID,
    int counter,
    int? bookID,
    int organizationID)
  {
    FABookPeriodRepository.CheckNotNullOrEmptyStringContract(finPeriodID, nameof (finPeriodID));
    FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
    IYearSetup faBookYearSetup = this.FABookPeriodRepositoryHelper.FindFABookYearSetup(bookID);
    IPeriodSetup periodSetup = this.FABookPeriodRepositoryHelper.FindFABookPeriodSetup(bookID).LastOrDefault<IPeriodSetup>();
    if (faBookYearSetup != null && FiscalPeriodSetupCreator.IsFixedLengthPeriod(faBookYearSetup.FPType) && periodSetup != null && periodSetup.PeriodNbr != null)
      return FinPeriodUtils.OffsetPeriod(finPeriodID, counter, Convert.ToInt32(periodSetup.PeriodNbr));
    if (counter > 0)
    {
      PXResultset<FABookPeriod> pxResultset = PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.finPeriodID, Greater<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>, And<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>>>>>, OrderBy<Asc<FABookPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, counter, new object[3]
      {
        (object) finPeriodID,
        (object) bookID,
        (object) organizationID
      });
      if (pxResultset.Count < counter)
        throw new PXFABookPeriodException();
      return PXResult<FABookPeriod>.op_Implicit(pxResultset[pxResultset.Count - 1]).FinPeriodID;
    }
    if (counter >= 0)
      return finPeriodID;
    PXResultset<FABookPeriod> pxResultset1 = PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.finPeriodID, Less<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>, And<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>>>>>, OrderBy<Desc<FABookPeriod.finPeriodID>>>.Config>.SelectWindowed(this.Graph, 0, -counter, new object[3]
    {
      (object) finPeriodID,
      (object) bookID,
      (object) organizationID
    });
    if (pxResultset1.Count < -counter)
      throw new PXFABookPeriodException();
    return PXResult<FABookPeriod>.op_Implicit(pxResultset1[pxResultset1.Count - 1]).FinPeriodID;
  }

  public string GetNextFABookPeriodID(string finPeriodID, int? bookID, int organizationID)
  {
    return this.PeriodPlusPeriodsCount(finPeriodID, 1, bookID, organizationID);
  }

  public string GetNextFABookPeriodID(string finPeriodID, int? bookID, int? assetID)
  {
    return this.PeriodPlusPeriodsCount(finPeriodID, 1, bookID, assetID);
  }

  public DateTime GetFABookPeriodStartDate(string finPeriodID, int? bookID, int? assetID)
  {
    FABookPeriodRepository.CheckNotNullOrEmptyStringContract(finPeriodID, nameof (finPeriodID));
    FABookPeriodRepository.CheckNotNullIDContract(bookID, nameof (bookID));
    FABookPeriodRepository.CheckNotNullIDContract(assetID, nameof (assetID));
    FABookPeriod faBookPeriod = PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>>.Config>.Select(this.Graph, new object[3]
    {
      (object) bookID,
      (object) this.FABookPeriodRepositoryHelper.GetFABookPeriodOrganizationID(bookID, assetID),
      (object) finPeriodID
    }));
    DateTime? startDate;
    int num;
    if (faBookPeriod == null)
    {
      num = 1;
    }
    else
    {
      startDate = faBookPeriod.StartDate;
      num = !startDate.HasValue ? 1 : 0;
    }
    if (num != 0)
      throw new PXFABookPeriodException();
    startDate = faBookPeriod.StartDate;
    return startDate.Value;
  }

  public DateTime GetFABookPeriodEndDate(string finPeriodID, int? bookID, int? assetID)
  {
    FABookPeriod faBookPeriod = PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>>.Config>.Select(this.Graph, new object[3]
    {
      (object) bookID,
      (object) this.FABookPeriodRepositoryHelper.GetFABookPeriodOrganizationID(bookID, assetID),
      (object) finPeriodID
    }));
    DateTime? endDate;
    int num;
    if (faBookPeriod == null)
    {
      num = 1;
    }
    else
    {
      endDate = faBookPeriod.EndDate;
      num = !endDate.HasValue ? 1 : 0;
    }
    if (num != 0)
      throw new PXFABookPeriodException();
    endDate = faBookPeriod.EndDate;
    return endDate.Value.AddDays(-1.0);
  }

  public virtual OrganizationFinPeriod GetNearestOpenOrganizationMappedFABookPeriodInSubledger<TClosedInSubledgerField>(
    int? bookID,
    int? sourceBranchID,
    string sourcefinPeriodID,
    int? targetBranchID)
    where TClosedInSubledgerField : IBqlField
  {
    if (!this.FABookPeriodRepositoryHelper.IsPostingFABook(bookID))
      return (OrganizationFinPeriod) null;
    IFinPeriodUtils service1 = this.Graph.GetService<IFinPeriodUtils>();
    this.Graph.GetService<IFinPeriodRepository>();
    IFABookPeriodRepository service2 = this.Graph.GetService<IFABookPeriodRepository>();
    int? parentOrganizationId1 = PXAccess.GetParentOrganizationID(sourceBranchID);
    int? parentOrganizationId2 = PXAccess.GetParentOrganizationID(targetBranchID);
    ProcessingResult<FABookPeriod> processingResult = service2.FindMappedFABookPeriod(bookID, parentOrganizationId1, sourcefinPeriodID, parentOrganizationId2);
    if (processingResult.Result == null)
    {
      ProcessingResult<FABookPeriod> periodUsingFinPeriod = service2.FindMappedFABookPeriodUsingFinPeriod(bookID, parentOrganizationId1, sourcefinPeriodID, parentOrganizationId2);
      periodUsingFinPeriod.RaiseIfHasError();
      processingResult = periodUsingFinPeriod;
    }
    FABookPeriod result = processingResult.ThisOrRaiseIfHasError().Result;
    return service1.GetNearestOpenOrganizationFinPeriodInSubledger<TClosedInSubledgerField>((IPeriod) result);
  }
}
