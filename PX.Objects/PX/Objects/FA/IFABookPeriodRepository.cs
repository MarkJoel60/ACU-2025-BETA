// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.IFABookPeriodRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public interface IFABookPeriodRepository
{
  int GetFABookPeriodOrganizationID(int? bookID, int? assetID, bool check = true);

  int GetFABookPeriodOrganizationID(FABookBalance balance, bool check = true);

  IYearSetup FindFABookYearSetup(FABook book, bool clearQueryCache = false);

  IYearSetup FindFABookYearSetup(int? bookID, bool clearQueryCache = false);

  IEnumerable<IPeriodSetup> FindFABookPeriodSetup(FABook book, bool clearQueryCache = false);

  IEnumerable<IPeriodSetup> FindFABookPeriodSetup(int? bookID, bool clearQueryCache = false);

  FABookYear FindFirstFABookYear(
    int? bookID,
    int? organizationID,
    bool clearQueryCache = false,
    bool mergeCache = false);

  FABookYear FindLastFABookYear(
    int? bookID,
    int? organizationID,
    bool clearQueryCache = false,
    bool mergeCache = false);

  FABookYear FindMasterFABookYearByID(
    FABook book,
    string yearNumber,
    bool clearQueryCache = false,
    bool mergeCache = false);

  FABookPeriod FindMasterFABookPeriodByID(
    FABook book,
    string periodID,
    bool clearQueryCache = false,
    bool mergeCache = false);

  FABookPeriod FindOrganizationFABookPeriodByID(
    string periodID,
    int? bookID,
    int? assetID,
    bool clearQueryCache = false,
    bool mergeCache = false);

  FABookPeriod FindNextNonAdjustmentMasterFABookPeriod(
    FABook book,
    string prevFABookPeriodID,
    bool clearQueryCache = false,
    bool mergeCache = false);

  FABookPeriod FindLastNonAdjustmentOrganizationFABookPeriodOfYear(
    FAOrganizationBook book,
    string finYear,
    bool clearQueryCache = false,
    bool mergeCache = false);

  FABookPeriod FindFABookPeriodOfDate(DateTime? date, int? bookID, int? assetID, bool check = true);

  FABookPeriod FindFABookPeriodOfDateByBranchID(
    DateTime? date,
    int? bookID,
    int? branchID,
    bool check = true);

  string GetFABookPeriodIDOfDate(DateTime? date, int? bookID, int? assetID, bool check = true);

  short GetQuarterNumberOfDate(DateTime? date, int? bookID, int? assetID);

  short GetPeriodNumberOfDate(DateTime? date, int? bookID, int? assetID);

  int GetYearNumberOfDate(DateTime? date, int? bookID, int? assetID);

  FABookPeriod FindMappedPeriod(FABookPeriod.Key fromPeriodKey, FABookPeriod.Key toPeriodKey);

  FABookPeriod FindByKey(int? bookID, int? organizaionID, string periodID);

  FABookPeriod FindPeriodByMasterPeriodID(int? bookID, int? organizaionID, string masterPeriodID);

  FABookPeriod GetMappedFABookPeriod(
    int? bookID,
    int? sourceOrganizationID,
    string finPeriodID,
    int? targetOrganizationID);

  ProcessingResult<FABookPeriod> FindMappedFABookPeriod(
    int? bookID,
    int? sourceOrganizationID,
    string sourcefinPeriodID,
    int? targetOrganizationID);

  ProcessingResult<FABookPeriod> FindMappedFABookPeriodUsingFinPeriod(
    int? bookID,
    int? sourceOrganizationID,
    string sourcefinPeriodID,
    int? targetOrganizationID);

  FABookPeriod GetMappedFABookPeriodByBranches(
    int? bookID,
    int? sourceBranchID,
    string finPeriodID,
    int? targetBranchID);

  ProcessingResult<FABookPeriod> GetFABookPeriodByMasterPeriodID(
    int? bookID,
    int? organizationID,
    string masterFinPeriodID);

  bool IsPostingFABook(int? bookID);
}
