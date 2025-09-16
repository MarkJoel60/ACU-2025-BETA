// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.IFABookPeriodUtils
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.FinPeriods;
using System;

#nullable disable
namespace PX.Objects.FA;

public interface IFABookPeriodUtils
{
  int? PeriodMinusPeriod(string finPeriodID1, string finPeriodID2, int? bookID, int? assetID);

  string PeriodPlusPeriodsCount(string finPeriodID, int counter, int? bookID, int organizationID);

  string PeriodPlusPeriodsCount(string finPeriodID, int counter, int? bookID, int? assetID);

  string GetNextFABookPeriodID(string finPeriodID, int? bookID, int organizationID);

  string GetNextFABookPeriodID(string finPeriodID, int? bookID, int? assetID);

  DateTime GetFABookPeriodStartDate(string finPeriodID, int? bookID, int? assetID);

  DateTime GetFABookPeriodEndDate(string finPeriodID, int? bookID, int? assetID);

  OrganizationFinPeriod GetNearestOpenOrganizationMappedFABookPeriodInSubledger<TClosedInSubledgerField>(
    int? bookID,
    int? sourceBranchID,
    string sourcefinPeriodID,
    int? targetBranchID)
    where TClosedInSubledgerField : IBqlField;
}
