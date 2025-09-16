// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Extensions;
using PX.Objects.FA.Overrides.AssetProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Objects.FA;

public static class FAHelper
{
  public static string CloseFABookHistory(
    this PXGraph graph,
    FABookBalance bal,
    IEnumerable<string> periodIDs)
  {
    string strB = (string) null;
    foreach (string strA in periodIDs.Where<string>((Func<string, bool>) (id => !string.IsNullOrEmpty(id))))
    {
      if (strB == null)
        GraphHelper.EnsureCachePersistence<FABookHist>(graph);
      FABookHist faBookHist = new FABookHist();
      faBookHist.AssetID = bal.AssetID;
      faBookHist.BookID = bal.BookID;
      faBookHist.FinPeriodID = strA;
      faBookHist.Closed = new bool?(true);
      FABookHist keyedHistory = faBookHist;
      FAHelper.InsertFABookHist(graph, keyedHistory, ref bal);
      if (strB == null || string.CompareOrdinal(strA, strB) > 0)
        strB = strA;
    }
    return strB;
  }

  public static string CloseFABookHistory(
    this PXGraph graph,
    FABookBalance bal,
    string toPeriod,
    bool force = false)
  {
    FABookHistory faBookHistory = PXResultset<FABookHistory>.op_Implicit(PXSelectBase<FABookHistory, PXSelect<FABookHistory, Where<FABookHistory.assetID, Equal<Required<FABookBalance.assetID>>, And<FABookHistory.bookID, Equal<Required<FABookBalance.bookID>>, And<FABookHistory.closed, NotEqual<True>>>>, OrderBy<Asc<FABookHistory.finPeriodID>>>.Config>.Select(graph, new object[2]
    {
      (object) bal.AssetID,
      (object) bal.BookID
    }));
    string str = force ? bal.DeprFromPeriod : (faBookHistory == null || string.CompareOrdinal(faBookHistory.FinPeriodID, bal.HistPeriod) <= 0 ? bal.HistPeriod : faBookHistory.FinPeriodID);
    List<string> periodIDs = new List<string>();
    for (; !string.IsNullOrEmpty(str) && string.CompareOrdinal(str, toPeriod) < 0; str = graph.GetService<IFABookPeriodUtils>().GetNextFABookPeriodID(str, bal.BookID, bal.AssetID))
      periodIDs.Add(str);
    return graph.CloseFABookHistory(bal, (IEnumerable<string>) periodIDs);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2022R1.")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FALocationHistory GetCurrentLocation(this PXGraph graph, FADetails details)
  {
    return PXResultset<FALocationHistory>.op_Implicit(PXSelectBase<FALocationHistory, PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Current<FADetails.assetID>>, And<FALocationHistory.revisionID, Equal<Current<FADetails.locationRevID>>>>>.Config>.SelectSingleBound(graph, new object[1]
    {
      (object) details
    }, Array.Empty<object>()));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FALocationHistory GetCurrentLocation(this PXGraph graph, PX.Objects.FA.Standalone.FADetails details)
  {
    return PXResultset<FALocationHistory>.op_Implicit(PXSelectBase<FALocationHistory, PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Current<PX.Objects.FA.Standalone.FADetails.assetID>>, And<FALocationHistory.revisionID, Equal<Current<PX.Objects.FA.Standalone.FADetails.locationRevID>>>>>.Config>.SelectSingleBound(graph, new object[1]
    {
      (object) details
    }, Array.Empty<object>()));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FALocationHistory GetPrevLocation(this PXGraph graph, FALocationHistory current)
  {
    return PXResultset<FALocationHistory>.op_Implicit(PXSelectBase<FALocationHistory, PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Current<FALocationHistory.assetID>>, And<FALocationHistory.revisionID, Less<Current<FALocationHistory.revisionID>>>>, OrderBy<Desc<FALocationHistory.revisionID>>>.Config>.SelectSingleBound(graph, new object[1]
    {
      (object) current
    }, Array.Empty<object>()));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static FAClass GetAssetClass(this PXGraph graph, int? classID)
  {
    return PXResultset<FAClass>.op_Implicit(PXSelectBase<FAClass, PXViewOf<FAClass>.BasedOn<SelectFromBase<FAClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FAClass.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) classID
    }));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsLocationChanged(
    this PXGraph graph,
    FALocationHistory current,
    FALocationHistory prev = null)
  {
    prev = prev ?? graph.GetPrevLocation(current);
    return prev != null && !((PXCache) GraphHelper.Caches<FALocationHistory>(graph)).ObjectsEqual<FALocationHistory.locationID, FALocationHistory.buildingID, FALocationHistory.floor, FALocationHistory.room, FALocationHistory.employeeID, FALocationHistory.department>((object) prev, (object) current);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsAccountsChanged(
    this PXGraph graph,
    FALocationHistory current,
    FALocationHistory prev = null)
  {
    prev = prev ?? graph.GetPrevLocation(current);
    return prev != null && !((PXCache) GraphHelper.Caches<FALocationHistory>(graph)).ObjectsEqual<FALocationHistory.locationID, FALocationHistory.fAAccountID, FALocationHistory.fASubID, FALocationHistory.accumulatedDepreciationAccountID, FALocationHistory.accumulatedDepreciationSubID>((object) (prev ?? graph.GetPrevLocation(current)), (object) current);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool WillAccountsBeChanged(
    this PXGraph graph,
    FixedAsset current,
    FixedAsset prev,
    out int? fAAccountID,
    out int? fASubID,
    out int? accumulatedDepreciationAccountID,
    out int? accumulatedDepreciationSubID)
  {
    fAAccountID = new int?();
    fASubID = new int?();
    accumulatedDepreciationAccountID = new int?();
    accumulatedDepreciationSubID = new int?();
    if (((PXCache) GraphHelper.Caches<FixedAsset>(graph)).ObjectsEqual<FixedAsset.classID, FixedAsset.fAAccountID, FixedAsset.fASubID, FixedAsset.accumulatedDepreciationAccountID, FixedAsset.accumulatedDepreciationSubID>((object) prev, (object) current))
      return false;
    if (((PXCache) GraphHelper.Caches<FixedAsset>(graph)).ObjectsEqual<FixedAsset.classID>((object) prev, (object) current))
    {
      fAAccountID = current.FAAccountID;
      fASubID = current.FASubID;
      accumulatedDepreciationAccountID = current.AccumulatedDepreciationAccountID;
      accumulatedDepreciationSubID = current.AccumulatedDepreciationSubID;
    }
    else
    {
      PXCache cach = graph.Caches[typeof (FixedAsset)];
      FASetup current1 = (FASetup) graph.Caches[typeof (FASetup)].Current;
      FixedAsset sourceForNewAccounts = PX.Objects.FA.AssetProcess.GetSourceForNewAccounts(graph, current);
      fAAccountID = (int?) cach.GetValue<FixedAsset.fAAccountID>((object) sourceForNewAccounts);
      accumulatedDepreciationAccountID = (int?) cach.GetValue<FixedAsset.accumulatedDepreciationAccountID>((object) sourceForNewAccounts);
      if (current1.UpdateGL.GetValueOrDefault())
      {
        fASubID = AssetMaint.MakeSubID<FixedAsset.fASubMask, FixedAsset.fASubID>(cach, current);
        accumulatedDepreciationSubID = AssetMaint.MakeSubID<FixedAsset.accumDeprSubMask, FixedAsset.accumulatedDepreciationSubID>(cach, current);
      }
      else
      {
        fASubID = (int?) cach.GetValue<FixedAsset.fASubID>((object) sourceForNewAccounts);
        accumulatedDepreciationSubID = (int?) cach.GetValue<FixedAsset.accumulatedDepreciationSubID>((object) sourceForNewAccounts);
      }
    }
    return true;
  }

  public static FABookHist InsertFABookHist(
    PXGraph graph,
    FABookHist keyedHistory,
    ref FABookBalance bookBalance)
  {
    FABookHist faBookHist = GraphHelper.Caches<FABookHist>(graph).Insert(keyedHistory);
    if (faBookHist == null || bookBalance.MaxHistoryPeriodID != null && string.Compare(keyedHistory.FinPeriodID, bookBalance.MaxHistoryPeriodID) <= 0)
      return faBookHist;
    bookBalance.MaxHistoryPeriodID = keyedHistory.FinPeriodID;
    GraphHelper.EnsureCachePersistence(graph, ((object) bookBalance).GetType());
    GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<FABookBalance>(graph), (object) bookBalance, false);
    return faBookHist;
  }

  public static string GetFABookHistoryMaxPeriodID(PXGraph graph, int? assetID, int? bookID)
  {
    string finPeriodId = PXResultset<FABookHistory>.op_Implicit(PXSelectBase<FABookHistory, PXViewOf<FABookHistory>.BasedOn<SelectFromBase<FABookHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<P.AsInt>>>>>.And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<P.AsInt>>>.Order<By<BqlField<FABookHistory.finPeriodID, IBqlString>.Desc>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[2]
    {
      (object) assetID,
      (object) bookID
    }))?.FinPeriodID;
    string strB = ((PXCache) GraphHelper.Caches<FABookHist>(graph)).Inserted.Cast<FABookHist>().Max<FABookHist, string>((Func<FABookHist, string>) (hist => hist.FinPeriodID));
    return string.Compare(finPeriodId, strB) <= 0 ? strB : finPeriodId;
  }
}
