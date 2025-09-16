// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.FA.Overrides.AssetProcess;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FA;

public class AssetProcess : PXGraph<PX.Objects.FA.AssetProcess>
{
  public PXSelect<FABookHist> bookhist;
  public PXSelect<FARegister> register;
  public PXSelect<FATran> booktran;
  public PXSelect<FixedAsset> fixedasset;
  public PXSelect<FABookBalance> bookbalance;
  public PXSelect<FADetails> fadetail;
  public PXSelect<FAAccrualTran> accrualtran;
  public PXSetup<FASetup> fasetup;
  public PXSetup<GLSetup> glsetup;
  public JournalEntry je = PXGraph.CreateInstance<JournalEntry>();
  protected static Dictionary<string, string> reversalTranType = new Dictionary<string, string>()
  {
    {
      "P+",
      "P-"
    },
    {
      "P-",
      "P+"
    },
    {
      "D+",
      "D-"
    },
    {
      "D-",
      "D+"
    },
    {
      "R+",
      "R-"
    },
    {
      "R-",
      "R+"
    },
    {
      "S+",
      "S-"
    },
    {
      "S-",
      "S+"
    },
    {
      "PR",
      "PD"
    },
    {
      "PD",
      "PR"
    },
    {
      "A+",
      "A-"
    },
    {
      "A-",
      "A+"
    },
    {
      "TP",
      "TP"
    },
    {
      "TD",
      "TD"
    }
  };
  protected static Dictionary<string, string> disposalreversalTranType = new Dictionary<string, string>()
  {
    {
      "PD",
      "PR"
    },
    {
      "A-",
      "A+"
    },
    {
      "S+",
      "S-"
    },
    {
      "D+",
      "D-"
    },
    {
      "D-",
      "D+"
    }
  };
  private DepreciationCalculation depreciationCalculationGraph;
  private static readonly string[] NonReclassifiableTranTypes = new string[7]
  {
    "P+",
    "P-",
    "R+",
    "R-",
    "TP",
    "TD",
    "PR"
  };
  private static readonly string[] FullyReclassifiableTranTypes = new string[2]
  {
    "S+",
    "S-"
  };

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  public IFABookPeriodUtils FABookPeriodUtils { get; set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public bool UpdateGL
  {
    get => ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault();
  }

  public bool AutoPost
  {
    get => ((PXSelectBase<FASetup>) this.fasetup).Current.AutoPost.GetValueOrDefault();
  }

  public bool SummPost
  {
    get => ((PXSelectBase<FASetup>) this.fasetup).Current.SummPost.GetValueOrDefault();
  }

  public bool SummPostDepr
  {
    get => ((PXSelectBase<FASetup>) this.fasetup).Current.SummPostDepreciation.GetValueOrDefault();
  }

  public AssetProcess()
  {
    PXResultset<FASetup>.op_Implicit(((PXSelectBase<FASetup>) this.fasetup).Select(Array.Empty<object>()));
  }

  public virtual void Clear()
  {
    ((PXGraph) this).Clear();
    ((PXGraph) this.je).Clear();
  }

  public static FixedAsset GetSourceForNewAccounts(PXGraph graph, FixedAsset asset)
  {
    int? nullable1 = asset.AssetID;
    int? nullable2;
    int? classId;
    if (asset.OldClassID.HasValue)
    {
      nullable2 = asset.OldClassID;
      classId = asset.ClassID;
      if (!(nullable2.GetValueOrDefault() == classId.GetValueOrDefault() & nullable2.HasValue == classId.HasValue))
      {
        nullable1 = asset.ClassID;
        goto label_6;
      }
    }
    PX.Objects.FA.Standalone.FADetails details = AssetMaint.FetchDetailsLite(graph, asset);
    FALocationHistory currentLocation = graph.GetCurrentLocation(details);
    FALocationHistory prevLocation = graph.GetPrevLocation(currentLocation);
    if (prevLocation != null)
    {
      classId = prevLocation.ClassID;
      nullable2 = asset.ClassID;
      if (!(classId.GetValueOrDefault() == nullable2.GetValueOrDefault() & classId.HasValue == nullable2.HasValue))
        nullable1 = asset.ClassID;
    }
label_6:
    return PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select(graph, new object[1]
    {
      (object) nullable1
    })) ?? throw new PXException("{0} '{1}' cannot be found in the system.", new object[2]
    {
      (object) "Asset Class",
      (object) asset.ClassID
    });
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.accumulatedDepreciationAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.accumulatedDepreciationSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.depreciatedExpenseAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.depreciatedExpenseSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FADetails.depreciateFromDate> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.FA.Standalone.FADetails.depreciateFromDate> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookBalance.depreciationMethodID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookBalance.averagingConvention> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute]
  protected virtual void _(PX.Data.Events.CacheAttached<FABookBalance.deprFromDate> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<FATran.tranPeriodID> e)
  {
  }

  public static void SuspendAsset(FixedAsset asset)
  {
    TransactionEntry instance = PXGraph.CreateInstance<TransactionEntry>();
    ((PXGraph) instance).GetExtension<TransactionEntry.TransactionEntryFixedAssetChecksExtension>().CheckUnreleasedTransactions(asset);
    ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.SuspendAsset))).FireOn((PXGraph) instance, asset);
    foreach (FABookBalance faBookBalance in GraphHelper.RowCast<FABookBalance>((IEnumerable) PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectMultiBound((PXGraph) instance, new object[1]
    {
      (object) asset
    }, Array.Empty<object>())).Where<FABookBalance>((Func<FABookBalance, bool>) (item => item.Status != "F")))
    {
      if (faBookBalance.CurrDeprPeriod == null)
        throw new PXException("Fixed asset not acquired or fully depreciated.");
      faBookBalance.Status = "S";
      ((PXSelectBase<FABookBalance>) instance.bookbalances).Update(faBookBalance);
    }
    ((PXGraph) instance).Actions.PressSave();
  }

  public static void UnsuspendAsset(FixedAsset asset, string CurrentPeriod)
  {
    TransactionEntry instance = PXGraph.CreateInstance<TransactionEntry>();
    ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.ActivateAsset))).FireOn((PXGraph) instance, asset);
    ((PXSelectBase<FixedAsset>) instance.Asset).Update(asset);
    int? nullable1 = new int?(PXDatabase.GetSlot<FABookCollection>("FABookCollection", new Type[1]
    {
      typeof (FABook)
    }).Books.Where<KeyValuePair<int, FABook>>((Func<KeyValuePair<int, FABook>, bool>) (book => book.Value.UpdateGL.GetValueOrDefault())).Select<KeyValuePair<int, FABook>, int>((Func<KeyValuePair<int, FABook>, int>) (book => book.Key)).FirstOrDefault<int>());
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXGraph) instance).Actions.PressSave();
      foreach (FABookBalance faBookBalance in GraphHelper.RowCast<FABookBalance>((IEnumerable) PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectMultiBound((PXGraph) instance, new object[1]
      {
        (object) asset
      }, Array.Empty<object>())).Where<FABookBalance>((Func<FABookBalance, bool>) (item => item.Status == "S")))
      {
        faBookBalance.Status = "A";
        if (faBookBalance.CurrDeprPeriod == null)
          throw new PXException("Fixed asset not acquired or fully depreciated.");
        int? nullable2;
        string str1;
        if (!faBookBalance.UpdateGL.GetValueOrDefault())
        {
          IFABookPeriodRepository service1 = ((PXGraph) instance).GetService<IFABookPeriodRepository>();
          IFABookPeriodUtils service2 = ((PXGraph) instance).GetService<IFABookPeriodUtils>();
          string finPeriodID = CurrentPeriod;
          nullable2 = nullable1;
          int? bookID = nullable2 ?? faBookBalance.BookID;
          int? assetId1 = faBookBalance.AssetID;
          DateTime? date = new DateTime?(service2.GetFABookPeriodStartDate(finPeriodID, bookID, assetId1));
          int? bookId = faBookBalance.BookID;
          int? assetId2 = faBookBalance.AssetID;
          str1 = service1.GetFABookPeriodIDOfDate(date, bookId, assetId2);
        }
        else
          str1 = CurrentPeriod;
        string str2 = str1;
        if (string.CompareOrdinal(faBookBalance.CurrDeprPeriod, str2) > 0)
          str2 = faBookBalance.CurrDeprPeriod;
        if (string.Equals(str2, faBookBalance.CurrDeprPeriod))
        {
          ((PXSelectBase<FABookBalance>) instance.bookbalances).Update(faBookBalance);
          ((PXGraph) instance).Actions.PressSave();
        }
        else
        {
          int? nullable3;
          ref int? local = ref nullable3;
          nullable2 = ((PXGraph) instance).GetService<IFABookPeriodUtils>().PeriodMinusPeriod(str2, faBookBalance.CurrDeprPeriod, faBookBalance.BookID, faBookBalance.AssetID);
          int num = nullable2.HasValue ? nullable2.GetValueOrDefault() : throw new PXException("New Current Period '{0}' less than previous Current Period '{1}'.", new object[2]
          {
            (object) PeriodIDAttribute.FormatForError(str2),
            (object) PeriodIDAttribute.FormatForError(faBookBalance.CurrDeprPeriod)
          });
          local = new int?(num);
          PX.Objects.FA.AssetProcess.SuspendAssetForPeriods(EnumerableExtensions.AsSingleEnumerable<FABookBalance>(faBookBalance), nullable3.Value);
        }
      }
      transactionScope.Complete();
    }
  }

  public static void SuspendBalanceForPeriods(
    TransactionEntry graph,
    FABookBalance bookbal,
    int Periods)
  {
    if (Periods < 1)
      return;
    IFABookPeriodUtils service = ((PXGraph) graph).GetService<IFABookPeriodUtils>();
    FABookHist faBookHist1 = new FABookHist();
    faBookHist1.AssetID = bookbal.AssetID;
    faBookHist1.BookID = bookbal.BookID;
    faBookHist1.FinPeriodID = service.PeriodPlusPeriodsCount(bookbal.CurrDeprPeriod, -bookbal.YtdSuspended.Value, bookbal.BookID, bookbal.AssetID);
    FABookHist keyedHistory1 = faBookHist1;
    FAHelper.InsertFABookHist((PXGraph) graph, keyedHistory1, ref bookbal).YtdSuspended = new int?(Periods);
    for (int counter = 0; counter < Periods; ++counter)
    {
      FABookHist faBookHist2 = new FABookHist();
      faBookHist2.AssetID = bookbal.AssetID;
      faBookHist2.BookID = bookbal.BookID;
      faBookHist2.FinPeriodID = service.PeriodPlusPeriodsCount(bookbal.CurrDeprPeriod, counter, bookbal.BookID, bookbal.AssetID);
      FABookHist keyedHistory2 = faBookHist2;
      FABookHist faBookHist3 = FAHelper.InsertFABookHist((PXGraph) graph, keyedHistory2, ref bookbal);
      faBookHist3.YtdReversed = new int?(counter == 0 ? Periods : 0);
      faBookHist3.Suspended = new bool?(true);
      faBookHist3.Closed = new bool?(true);
    }
    FABookHist faBookHist4 = new FABookHist();
    faBookHist4.AssetID = bookbal.AssetID;
    faBookHist4.BookID = bookbal.BookID;
    faBookHist4.FinPeriodID = service.PeriodPlusPeriodsCount(bookbal.CurrDeprPeriod, Periods, bookbal.BookID, bookbal.AssetID);
    FABookHist keyedHistory3 = faBookHist4;
    FAHelper.InsertFABookHist((PXGraph) graph, keyedHistory3, ref bookbal);
    FABookBalance copy = PXCache<FABookBalance>.CreateCopy(bookbal);
    copy.NoteID = new Guid?(Guid.NewGuid());
    if (!string.IsNullOrEmpty(copy.CurrDeprPeriod))
      copy.CurrDeprPeriod = PX.Objects.FA.AssetProcess.ExtendPeriod(graph, copy.CurrDeprPeriod, Periods, copy.BookID, copy.AssetID);
    if (!string.IsNullOrEmpty(copy.LastDeprPeriod))
      copy.LastDeprPeriod = PX.Objects.FA.AssetProcess.ExtendPeriod(graph, copy.LastDeprPeriod, Periods, copy.BookID, copy.AssetID);
    if (!string.IsNullOrEmpty(copy.DeprToPeriod))
    {
      copy.DeprToPeriod = PX.Objects.FA.AssetProcess.ExtendPeriod(graph, copy.DeprToPeriod, Periods, copy.BookID, copy.AssetID);
      copy.LastPeriod = copy.DeprToPeriod;
    }
    copy.DeprToDate = new DateTime?();
    ((PXSelectBase<FABookBalance>) graph.bookbalances).Update(copy);
  }

  public static string ExtendPeriod(
    TransactionEntry graph,
    string finPeriodID,
    int counter,
    int? bookID,
    int? assetID)
  {
    IFABookPeriodUtils service = ((PXGraph) graph).GetService<IFABookPeriodUtils>();
    string str = finPeriodID;
    try
    {
      str = service.PeriodPlusPeriodsCount(finPeriodID, counter, bookID, assetID);
    }
    catch (PXFABookPeriodException ex)
    {
      IPeriodSetup periodSetup = ((PXGraph) graph).GetService<IFABookPeriodRepository>().FindFABookPeriodSetup(bookID).LastOrDefault<IPeriodSetup>();
      if (periodSetup != null)
      {
        if (periodSetup.PeriodNbr != null)
          throw new PXSetPropertyException("The {0} period does not exist for the {1} book. To proceed, generate the necessary periods on the Generate Book Calendars (FA501000) form.", new object[2]
          {
            (object) PeriodIDAttribute.FormatForError(PX.Objects.GL.FinPeriods.FinPeriodUtils.OffsetPeriod(finPeriodID, counter, Convert.ToInt32(periodSetup.PeriodNbr))),
            (object) BookMaint.FindByID((PXGraph) graph, bookID).BookCode
          });
      }
    }
    return str;
  }

  public static void CloseBalanceHistoryForPeriods(
    TransactionEntry graph,
    FABookBalance bookbal,
    int periodsCount)
  {
    if (periodsCount < 1)
      return;
    IFABookPeriodUtils service = ((PXGraph) graph).GetService<IFABookPeriodUtils>();
    for (int counter = 0; counter < periodsCount; ++counter)
    {
      FABookHist faBookHist = new FABookHist();
      faBookHist.AssetID = bookbal.AssetID;
      faBookHist.BookID = bookbal.BookID;
      faBookHist.FinPeriodID = service.PeriodPlusPeriodsCount(bookbal.CurrDeprPeriod, counter, bookbal.BookID, bookbal.AssetID);
      FABookHist keyedHistory = faBookHist;
      FAHelper.InsertFABookHist((PXGraph) graph, keyedHistory, ref bookbal).Closed = new bool?(true);
    }
    FABookHist faBookHist1 = new FABookHist();
    faBookHist1.AssetID = bookbal.AssetID;
    faBookHist1.BookID = bookbal.BookID;
    faBookHist1.FinPeriodID = service.PeriodPlusPeriodsCount(bookbal.CurrDeprPeriod, periodsCount, bookbal.BookID, bookbal.AssetID);
    FABookHist keyedHistory1 = faBookHist1;
    FAHelper.InsertFABookHist((PXGraph) graph, keyedHistory1, ref bookbal);
  }

  public static void SuspendAssetForPeriods(IEnumerable<FABookBalance> balances, int Periods)
  {
    TransactionEntry instance = PXGraph.CreateInstance<TransactionEntry>();
    foreach (FABookBalance balance in balances)
      PX.Objects.FA.AssetProcess.SuspendBalanceForPeriods(instance, balance, Periods);
    ((PXGraph) instance).Actions.PressSave();
  }

  public static void SuspendAssetToPeriod(
    IEnumerable<FABookBalance> balances,
    DateTime? Date,
    string PeriodID)
  {
    TransactionEntry instance = PXGraph.CreateInstance<TransactionEntry>();
    foreach (FABookBalance balance in balances)
    {
      OrganizationFinPeriod organizationFinPeriodInFa = instance.FinPeriodUtils.GetOpenOrganizationFinPeriodInFA(PeriodID, balance.AssetID);
      Date = Date ?? organizationFinPeriodInFa.StartDate;
      if (balance.LastDeprPeriod == null || string.CompareOrdinal(balance.LastDeprPeriod, balance.DeprToPeriod) < 0)
      {
        string finPeriodID1 = balance.UpdateGL.GetValueOrDefault() ? PeriodID : ((PXGraph) instance).GetService<IFABookPeriodRepository>().GetFABookPeriodIDOfDate(Date, balance.BookID, balance.AssetID);
        int valueOrDefault = ((PXGraph) instance).GetService<IFABookPeriodUtils>().PeriodMinusPeriod(finPeriodID1, balance.CurrDeprPeriod ?? finPeriodID1, balance.BookID, balance.AssetID).GetValueOrDefault();
        PX.Objects.FA.AssetProcess.SuspendBalanceForPeriods(instance, balance, valueOrDefault);
      }
    }
    ((PXGraph) instance).Actions.PressSave();
  }

  public static void CloseAssetHistoryToPeriod(
    IEnumerable<FABookBalance> balances,
    DateTime? Date,
    string PeriodID)
  {
    TransactionEntry instance = PXGraph.CreateInstance<TransactionEntry>();
    foreach (FABookBalance balance in balances)
    {
      OrganizationFinPeriod organizationFinPeriodInFa = instance.FinPeriodUtils.GetOpenOrganizationFinPeriodInFA(PeriodID, balance.AssetID);
      Date = Date ?? organizationFinPeriodInFa.StartDate;
      if (balance.LastDeprPeriod == null || string.CompareOrdinal(balance.LastDeprPeriod, balance.DeprToPeriod) < 0)
      {
        string finPeriodID1 = balance.UpdateGL.GetValueOrDefault() ? PeriodID : ((PXGraph) instance).GetService<IFABookPeriodRepository>().GetFABookPeriodIDOfDate(Date, balance.BookID, balance.AssetID);
        int valueOrDefault = ((PXGraph) instance).GetService<IFABookPeriodUtils>().PeriodMinusPeriod(finPeriodID1, balance.CurrDeprPeriod ?? finPeriodID1, balance.BookID, balance.AssetID).GetValueOrDefault();
        PX.Objects.FA.AssetProcess.CloseBalanceHistoryForPeriods(instance, balance, valueOrDefault);
      }
    }
    ((PXGraph) instance).Actions.PressSave();
  }

  public static void RestrictAdditonDeductionForCalcMethod(
    PXGraph graph,
    int? assetID,
    string method)
  {
    if (PX.Objects.FA.AssetProcess.BookBalanceWithDeprMethodExistsForAsset(graph, assetID, method))
    {
      FADepreciationMethod depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.depreciationMethod, Equal<Required<FADepreciationMethod.depreciationMethod>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
      {
        (object) method
      }));
      throw new PXException("Additions and deductions cannot be processed for the {0} calculation method. Dispose of the asset and create a new asset with the adjusted acquisition cost.", new object[1]
      {
        (object) PXStringListAttribute.GetLocalizedLabel<FADepreciationMethod.depreciationMethod>(graph.Caches[typeof (FADepreciationMethod)], (object) depreciationMethod)
      });
    }
  }

  public static bool BookBalanceWithDeprMethodExistsForAsset(
    PXGraph graph,
    int? assetID,
    string method)
  {
    return PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelectJoin<FABookBalance, InnerJoin<FADepreciationMethod, On<FADepreciationMethod.methodID, Equal<FABookBalance.depreciationMethodID>>>, Where<FABookBalance.assetID, Equal<Required<FixedAsset.assetID>>, And<FADepreciationMethod.depreciationMethod, Equal<Required<FADepreciationMethod.depreciationMethod>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[2]
    {
      (object) assetID,
      (object) method
    })) != null;
  }

  public static DocumentList<FARegister> ReverseDisposal(
    FixedAsset asset,
    DateTime? revDate,
    string revPeriodID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PX.Objects.FA.AssetProcess.\u003C\u003Ec__DisplayClass56_0 cDisplayClass560 = new PX.Objects.FA.AssetProcess.\u003C\u003Ec__DisplayClass56_0();
    TransactionEntry instance = PXGraph.CreateInstance<TransactionEntry>();
    PXCacheEx.Adjust<PXRestrictorAttribute>(((PXSelectBase) instance.Trans).Cache, (object) null).For<FATran.assetID>((Action<PXRestrictorAttribute>) (attr =>
    {
      if (!((IEnumerable<Type>) BqlCommand.Decompose(attr.RestrictingCondition)).Contains<Type>(typeof (FixedAssetStatus.disposed)))
        return;
      attr.SuppressVerify = true;
    }));
    FARegister faRegister = ((PXSelectBase<FARegister>) instance.Document).Insert(new FARegister()
    {
      BranchID = asset.BranchID,
      Origin = "L",
      DocDesc = PXMessages.LocalizeFormatNoPrefix("Disposal Reversal of Asset {0}", new object[1]
      {
        (object) asset.AssetCD
      })
    });
    // ISSUE: reference to a compiler-generated field
    cDisplayClass560.doc = faRegister;
    FARegister disposalRegister = PX.Objects.FA.AssetProcess.GetDisposalRegister((PXGraph) instance, asset.AssetID);
    foreach (PXResult<FATran, FABook> pxResult in PXSelectBase<FATran, PXSelectJoin<FATran, LeftJoin<FABook, On<FATran.bookID, Equal<FABook.bookID>>>, Where<FATran.refNbr, Equal<Current<FARegister.refNbr>>>>.Config>.SelectMultiBound((PXGraph) instance, new object[1]
    {
      (object) disposalRegister
    }, Array.Empty<object>()))
    {
      FATran faTran1 = PXResult<FATran, FABook>.op_Implicit(pxResult);
      FABook faBook = PXResult<FATran, FABook>.op_Implicit(pxResult);
      revPeriodID = revPeriodID ?? faTran1.FinPeriodID;
      DateTime? nullable;
      if (!faBook.UpdateGL.GetValueOrDefault())
      {
        IFABookPeriodRepository service = ((PXGraph) instance).GetService<IFABookPeriodRepository>();
        nullable = revDate;
        DateTime? date = nullable ?? faTran1.TranDate;
        int? bookId = faTran1.BookID;
        int? branchId = faTran1.BranchID;
        revPeriodID = service.FindFABookPeriodOfDateByBranchID(date, bookId, branchId).FinPeriodID;
      }
      FATran faTran2 = new FATran();
      // ISSUE: reference to a compiler-generated field
      faTran2.RefNbr = cDisplayClass560.doc.RefNbr;
      faTran2.LineNbr = new int?();
      faTran2.DebitAccountID = faTran1.CreditAccountID;
      faTran2.DebitSubID = faTran1.CreditSubID;
      faTran2.CreditAccountID = faTran1.DebitAccountID;
      faTran2.CreditSubID = faTran1.DebitSubID;
      faTran2.TranDesc = ((PXGraph) instance).MakeLocalizedDescription<FATran.tranDesc>("{0} - disposal reversed", faTran1.TranDesc);
      faTran2.Released = new bool?(false);
      faTran2.TranAmt = faTran1.TranAmt;
      faTran2.RGOLAmt = faTran1.RGOLAmt;
      faTran2.AssetID = faTran1.AssetID;
      faTran2.BookID = faTran1.BookID;
      faTran2.TranType = PX.Objects.FA.AssetProcess.disposalreversalTranType[faTran1.TranType];
      nullable = revDate;
      faTran2.TranDate = nullable ?? faTran1.TranDate;
      faTran2.FinPeriodID = revPeriodID ?? faTran1.FinPeriodID;
      FATran faTran3 = faTran2;
      ((PXSelectBase<FATran>) instance.Trans).Insert(faTran3);
    }
    if (((PXSelectBase) instance.Trans).Cache.IsInsertedUpdatedDeleted)
      ((PXGraph) instance).Actions.PressSave();
    if (((PXSelectBase<FASetup>) instance.fasetup).Current.AutoReleaseReversal.GetValueOrDefault())
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) instance, new PXToggleAsyncDelegate((object) cDisplayClass560, __methodptr(\u003CReverseDisposal\u003Eb__1)));
    }
    return instance.created;
  }

  public static DocumentList<FARegister> ReverseAsset(FixedAsset asset, FASetup fasetup)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PX.Objects.FA.AssetProcess.\u003C\u003Ec__DisplayClass57_0 cDisplayClass570 = new PX.Objects.FA.AssetProcess.\u003C\u003Ec__DisplayClass57_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass570.docgraph = PXGraph.CreateInstance<TransactionEntry>();
    // ISSUE: reference to a compiler-generated field
    FARegister faRegister = ((PXSelectBase<FARegister>) cDisplayClass570.docgraph.Document).Insert(new FARegister()
    {
      BranchID = asset.BranchID,
      Origin = "V",
      DocDesc = PXMessages.LocalizeFormatNoPrefix("Full Reversal of Asset {0}", new object[1]
      {
        (object) asset.AssetCD
      })
    });
    // ISSUE: reference to a compiler-generated field
    cDisplayClass570.doc = faRegister;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    foreach (FATran faTran in GraphHelper.RowCast<FATran>((IEnumerable) PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Current<FixedAsset.assetID>>, And<FATran.released, Equal<True>>>>.Config>.SelectMultiBound((PXGraph) cDisplayClass570.docgraph, new object[1]
    {
      (object) asset
    }, Array.Empty<object>())).Select<FATran, FATran>(cDisplayClass570.\u003C\u003E9__1 ?? (cDisplayClass570.\u003C\u003E9__1 = new Func<FATran, FATran>(cDisplayClass570.\u003CReverseAsset\u003Eb__1))))
    {
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<FATran>) cDisplayClass570.docgraph.Trans).Insert(faTran);
    }
    // ISSUE: reference to a compiler-generated field
    if (((PXSelectBase) cDisplayClass570.docgraph.Trans).Cache.IsInsertedUpdatedDeleted)
    {
      // ISSUE: reference to a compiler-generated field
      ((PXGraph) cDisplayClass570.docgraph).Actions.PressSave();
    }
    // ISSUE: reference to a compiler-generated field
    if (((PXSelectBase<FASetup>) cDisplayClass570.docgraph.fasetup).Current.AutoReleaseReversal.GetValueOrDefault())
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) cDisplayClass570.docgraph, new PXToggleAsyncDelegate((object) cDisplayClass570, __methodptr(\u003CReverseAsset\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    return cDisplayClass570.docgraph.created;
  }

  public static List<FABookBalance> PrepareDisposal(
    TransactionEntry docgraph,
    FixedAsset asset,
    FADetails assetdet,
    bool IsMassProcess,
    bool deprBeforeDisposal)
  {
    PXSelectBase<FABookBalance> pxSelectBase = (PXSelectBase<FABookBalance>) new PXSelectReadonly<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FixedAsset.assetID>>>>((PXGraph) docgraph);
    List<FABookBalance> books = new List<FABookBalance>();
    string str1 = assetdet.DisposalPeriodID ?? docgraph.FinPeriodRepository.FindFinPeriodByDate(assetdet.DisposalDate, PXAccess.GetParentOrganizationID(asset.BranchID))?.FinPeriodID;
    foreach (PXResult<FABookBalance> pxResult in pxSelectBase.Select(new object[1]
    {
      (object) assetdet.AssetID
    }))
    {
      FABookBalance faBookBalance1 = PXResult<FABookBalance>.op_Implicit(pxResult);
      bool? nullable = faBookBalance1.Depreciate;
      if (!nullable.GetValueOrDefault() || !deprBeforeDisposal)
      {
        nullable = faBookBalance1.Depreciate;
        if (nullable.GetValueOrDefault())
          PX.Objects.FA.AssetProcess.SuspendAssetToPeriod((IEnumerable<FABookBalance>) new List<FABookBalance>()
          {
            faBookBalance1
          }, assetdet.DisposalDate, str1);
        else
          PX.Objects.FA.AssetProcess.CloseAssetHistoryToPeriod((IEnumerable<FABookBalance>) new List<FABookBalance>()
          {
            faBookBalance1
          }, assetdet.DisposalDate, str1);
      }
      FABookBalance faBookBalance2 = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FABookBalance.assetID>>, And<FABookBalance.bookID, Equal<Current<FABookBalance.bookID>>>>>.Config>.SelectSingleBound((PXGraph) docgraph, new object[1]
      {
        (object) faBookBalance1
      }, Array.Empty<object>()));
      FABookBalance faBookBalance3 = faBookBalance2;
      nullable = faBookBalance1.UpdateGL;
      string str2 = !nullable.GetValueOrDefault() || assetdet.DisposalPeriodID == null ? ((PXGraph) docgraph).GetService<IFABookPeriodRepository>().GetFABookPeriodIDOfDate(assetdet.DisposalDate, faBookBalance2.BookID, faBookBalance2.AssetID) : assetdet.DisposalPeriodID;
      faBookBalance3.DisposalPeriodID = str2;
      faBookBalance2.OrigDeprToDate = faBookBalance2.DeprToDate;
      faBookBalance2.DisposalAmount = assetdet.SaleAmount;
      books.Add(((PXSelectBase<FABookBalance>) docgraph.bookbalances).Update(faBookBalance2));
    }
    ((PXGraph) docgraph).Actions.PressSave();
    ((PXGraph) docgraph).Clear();
    if (asset.Depreciable.GetValueOrDefault() & deprBeforeDisposal)
    {
      PX.Objects.FA.AssetProcess.DepreciateAsset((IEnumerable<FABookBalance>) books, assetdet.DisposalDate, str1, IsMassProcess, false);
      books = GraphHelper.RowCast<FABookBalance>((IEnumerable) pxSelectBase.Select(new object[1]
      {
        (object) assetdet.AssetID
      })).ToList<FABookBalance>();
      books.ForEach((Action<FABookBalance>) (bal => bal.DisposalAmount = assetdet.SaleAmount));
    }
    Dictionary<int?, FABookBalance> origin = PX.Objects.FA.AssetProcess.SetDeprTo(books, assetdet.DisposalDate);
    PX.Objects.FA.AssetProcess.CalculateAsset((IEnumerable<FABookBalance>) books, (string) null);
    PX.Objects.FA.AssetProcess.ResetDeprTo(books, origin);
    books.ForEach(new Action<FABookBalance>(((GraphHelper) docgraph).EnsureRowPersistence));
    ((PXGraph) docgraph).Actions.PressSave();
    return books;
  }

  protected static Dictionary<int?, FABookBalance> SetDeprTo(
    List<FABookBalance> books,
    DateTime? deprToDate)
  {
    Dictionary<int?, FABookBalance> dictionary = new Dictionary<int?, FABookBalance>();
    foreach (FABookBalance faBookBalance in books.Where<FABookBalance>((Func<FABookBalance, bool>) (balance => balance != null && balance.BookID.HasValue && string.CompareOrdinal(balance.DeprToPeriod, balance.DisposalPeriodID) > 0)))
    {
      dictionary.Add(faBookBalance.BookID, new FABookBalance()
      {
        DeprToDate = faBookBalance.DeprToDate,
        DeprToPeriod = faBookBalance.DeprToPeriod
      });
      faBookBalance.DeprToDate = deprToDate;
      faBookBalance.DeprToPeriod = faBookBalance.DisposalPeriodID;
    }
    return dictionary;
  }

  protected static void ResetDeprTo(
    List<FABookBalance> books,
    Dictionary<int?, FABookBalance> origin)
  {
    foreach (FABookBalance book in books)
    {
      FABookBalance faBookBalance;
      if (origin.TryGetValue(book.BookID, out faBookBalance))
      {
        book.DeprToDate = faBookBalance.DeprToDate;
        book.DeprToPeriod = faBookBalance.DeprToPeriod;
      }
    }
  }

  public static DocumentList<FARegister> DisposeAsset(
    PXResultset<FixedAsset, FADetails> assets,
    FASetup fasetup,
    bool IsMassProcess,
    bool deprBeforeDisposal,
    string reason)
  {
    TransactionEntry instance = PXGraph.CreateInstance<TransactionEntry>();
    foreach (PXResult<FixedAsset, FADetails> asset1 in (PXResultset<FixedAsset>) assets)
    {
      FixedAsset asset2 = PXResult<FixedAsset, FADetails>.op_Implicit(asset1);
      FADetails faDetails = PXResult<FixedAsset, FADetails>.op_Implicit(asset1);
      PXProcessing<FixedAsset>.SetCurrentItem((object) asset2);
      try
      {
        instance.CheckIfAssetCanBeDisposed(asset2, faDetails, fasetup, faDetails.DisposalDate.Value, faDetails.DisposalPeriodID, deprBeforeDisposal);
        List<FABookBalance> books = PX.Objects.FA.AssetProcess.PrepareDisposal(instance, asset2, faDetails, IsMassProcess, deprBeforeDisposal);
        PX.Objects.FA.AssetProcess.DisposeAsset(instance, asset2, (IEnumerable<FABookBalance>) books, fasetup, reason);
        PXProcessing<FixedAsset>.SetProcessed();
      }
      catch (Exception ex)
      {
        if (IsMassProcess)
          PXProcessing<FixedAsset>.SetError(ex);
        else
          throw;
      }
    }
    if (((PXSelectBase<FASetup>) instance.fasetup).Current.AutoReleaseDisp.GetValueOrDefault() && instance.created.Count > 0)
      AssetTranRelease.ReleaseDoc((List<FARegister>) instance.created, IsMassProcess);
    return instance.created;
  }

  public static void DisposeAsset(
    TransactionEntry docgraph,
    FixedAsset asset,
    IEnumerable<FABookBalance> books,
    FASetup setup,
    string reason)
  {
    FADetails det = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectSingleBound((PXGraph) docgraph, new object[1]
    {
      (object) asset
    }, Array.Empty<object>()));
    if (setup.SummPost.GetValueOrDefault())
      TransactionEntry.SegregateRegister((PXGraph) docgraph, asset.BranchID.Value, "S", (string) null, det.DisposalDate, "", docgraph.created);
    else
      ((PXSelectBase<FARegister>) docgraph.Document).Insert(new FARegister()
      {
        Origin = "S",
        DocDate = det.DisposalDate,
        BranchID = asset.BranchID
      });
    FARegister current = ((PXSelectBase<FARegister>) docgraph.Document).Current;
    if (current != null && string.IsNullOrEmpty(current.Reason))
    {
      current.Reason = reason;
      ((PXSelectBase<FARegister>) docgraph.Document).Update(current);
    }
    foreach (FABookBalance book in books)
    {
      FABookBalance faBookBalance1 = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FABookBalance.assetID>>, And<FABookBalance.bookID, Equal<Current<FABookBalance.bookID>>>>>.Config>.SelectSingleBound((PXGraph) docgraph, new object[1]
      {
        (object) book
      }, Array.Empty<object>()));
      FABookPeriod faBookPeriod = PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.finPeriodID, Equal<Required<FABookBalance.deprToPeriod>>, And<FABookPeriod.bookID, Equal<Required<FABookBalance.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>>>>>.Config>.SelectSingleBound((PXGraph) docgraph, new object[0], new object[3]
      {
        (object) faBookBalance1.DeprToPeriod,
        (object) faBookBalance1.BookID,
        (object) ((PXGraph) docgraph).GetService<IFABookPeriodRepository>().GetFABookPeriodOrganizationID(faBookBalance1)
      }));
      DateTime? nullable1 = faBookBalance1.DeprToDate;
      if (!nullable1.HasValue && faBookPeriod != null)
      {
        nullable1 = faBookPeriod.EndDate;
        if (nullable1.HasValue)
        {
          FABookBalance faBookBalance2 = faBookBalance1;
          nullable1 = faBookPeriod.EndDate;
          DateTime? nullable2 = new DateTime?(nullable1.Value.AddDays(-1.0));
          faBookBalance2.DeprToDate = nullable2;
        }
      }
      faBookBalance1.DisposalAmount = book.DisposalAmount;
      bool? nullable3 = faBookBalance1.Depreciate;
      string str;
      if (nullable3.GetValueOrDefault())
      {
        nullable3 = asset.UnderConstruction;
        if (!nullable3.GetValueOrDefault())
        {
          str = string.CompareOrdinal(faBookBalance1.DeprToPeriod, faBookBalance1.DisposalPeriodID) > 0 ? faBookBalance1.DisposalPeriodID : faBookBalance1.DeprToPeriod;
          if (!string.IsNullOrEmpty(faBookBalance1.LastPeriod) && string.CompareOrdinal(faBookBalance1.LastPeriod, faBookBalance1.DeprToPeriod) > 0 && string.CompareOrdinal(faBookBalance1.LastPeriod, str) > 0)
          {
            str = faBookBalance1.LastPeriod;
            goto label_15;
          }
          goto label_15;
        }
      }
      str = faBookBalance1.DisposalPeriodID;
label_15:
      FABookHistory faBookHistory = PXResultset<FABookHistory>.op_Implicit(PXSelectBase<FABookHistory, PXSelect<FABookHistory, Where<FABookHistory.assetID, Equal<Current<FABookBalance.assetID>>, And<FABookHistory.bookID, Equal<Current<FABookBalance.bookID>>, And<FABookHistory.finPeriodID, Equal<Required<FABookBalance.deprToPeriod>>>>>>.Config>.SelectSingleBound((PXGraph) docgraph, new object[1]
      {
        (object) faBookBalance1
      }, new object[1]{ (object) str }));
      if (string.CompareOrdinal(faBookBalance1.DeprFromPeriod, str) > 0)
        throw new PXException("The disposal period ({0}) cannot be earlier than the period of the placed-in service date ({1}).", new object[2]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(str),
          (object) FinPeriodIDFormattingAttribute.FormatForError(faBookBalance1.DeprFromPeriod)
        });
      if (faBookHistory == null)
        throw new PXException();
      if (string.CompareOrdinal(faBookBalance1.LastDeprPeriod, faBookBalance1.DisposalPeriodID) > 0)
        throw new PXException("The asset cannot be disposed of in past periods.");
      nullable3 = faBookHistory.Suspended;
      nullable3 = !nullable3.GetValueOrDefault() ? setup.DepreciateInDisposalPeriod : throw new PXException("The asset cannot be disposed of in suspended periods.");
      Decimal? nullable4;
      Decimal? nullable5;
      Decimal? nullable6;
      Decimal? nullable7;
      Decimal? nullable8;
      if (nullable3.GetValueOrDefault() && faBookBalance1.Status == "A")
      {
        nullable4 = faBookBalance1.DisposalAmount;
        nullable5 = faBookHistory.YtdAcquired;
        nullable3 = faBookBalance1.Depreciate;
        nullable6 = nullable3.GetValueOrDefault() ? faBookHistory.YtdCalculated : new Decimal?(0M);
        nullable7 = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable9;
        if (!(nullable4.HasValue & nullable7.HasValue))
        {
          nullable6 = new Decimal?();
          nullable9 = nullable6;
        }
        else
          nullable9 = new Decimal?(nullable4.GetValueOrDefault() - nullable7.GetValueOrDefault());
        nullable8 = nullable9;
      }
      else
      {
        nullable7 = faBookBalance1.DisposalAmount;
        nullable6 = faBookHistory.YtdAcquired;
        nullable3 = faBookBalance1.Depreciate;
        nullable5 = nullable3.GetValueOrDefault() ? faBookHistory.YtdDepreciated : new Decimal?(0M);
        nullable4 = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable10;
        if (!(nullable7.HasValue & nullable4.HasValue))
        {
          nullable5 = new Decimal?();
          nullable10 = nullable5;
        }
        else
          nullable10 = new Decimal?(nullable7.GetValueOrDefault() - nullable4.GetValueOrDefault());
        nullable8 = nullable10;
      }
      nullable4 = nullable8;
      Decimal num1 = 0M;
      int? nullable11;
      int? nullable12;
      if (nullable4.GetValueOrDefault() > num1 & nullable4.HasValue)
      {
        nullable11 = asset.GainAcctID;
        nullable12 = asset.GainSubID;
      }
      else
      {
        nullable11 = asset.LossAcctID;
        nullable12 = asset.LossSubID;
      }
      nullable4 = faBookHistory.YtdCalculated;
      nullable7 = faBookHistory.YtdDepreciated;
      Decimal? nullable13;
      if (!(nullable4.HasValue & nullable7.HasValue))
      {
        nullable5 = new Decimal?();
        nullable13 = nullable5;
      }
      else
        nullable13 = new Decimal?(nullable4.GetValueOrDefault() - nullable7.GetValueOrDefault());
      Decimal? nullable14 = nullable13;
      nullable7 = nullable14;
      Decimal num2 = 0M;
      bool flag = nullable7.GetValueOrDefault() > num2 & nullable7.HasValue;
      nullable14 = new Decimal?(Math.Abs(nullable14.Value));
      nullable3 = setup.DepreciateInDisposalPeriod;
      if (nullable3.GetValueOrDefault() && faBookBalance1.Status == "A")
      {
        nullable3 = faBookBalance1.Depreciate;
        if (nullable3.GetValueOrDefault())
        {
          nullable7 = nullable14;
          Decimal num3 = 0M;
          if (!(nullable7.GetValueOrDefault() == num3 & nullable7.HasValue))
          {
            FATran faTran = new FATran()
            {
              AssetID = faBookHistory.AssetID,
              BookID = faBookHistory.BookID,
              TranDate = det.DisposalDate,
              FinPeriodID = faBookBalance1.DisposalPeriodID,
              TranAmt = nullable14
            };
            faTran.TranType = flag ? (faBookBalance1.CurrDeprPeriod == faTran.FinPeriodID ? "C+" : "D+") : (faBookBalance1.CurrDeprPeriod == faTran.FinPeriodID ? "C-" : "D-");
            faTran.TranDesc = PXMessages.LocalizeFormatNoPrefix("Depreciation Adjustment on Disposal for Asset {0}", new object[1]
            {
              ((PXSelectBase<FATran>) docgraph.Trans).GetValueExt<FATran.assetID>(faTran)
            });
            ((PXSelectBase<FATran>) docgraph.Trans).Insert(faTran);
          }
        }
      }
      FATran faTran1 = new FATran()
      {
        AssetID = faBookBalance1.AssetID,
        BookID = faBookBalance1.BookID,
        TranDate = det.DisposalDate,
        FinPeriodID = faBookBalance1.DisposalPeriodID,
        TranType = "PD",
        CreditAccountID = asset.FAAccountID,
        CreditSubID = asset.FASubID,
        DebitAccountID = nullable11,
        DebitSubID = nullable12,
        TranAmt = faBookHistory.YtdAcquired
      };
      faTran1.TranDesc = PXMessages.LocalizeFormatNoPrefix("Cost Disposal for Asset {0}", new object[1]
      {
        ((PXSelectBase<FATran>) docgraph.Trans).GetValueExt<FATran.assetID>(faTran1)
      });
      ((PXSelectBase<FATran>) docgraph.Trans).Insert(faTran1);
      Decimal? nullable15 = new Decimal?(0M);
      nullable3 = faBookBalance1.Depreciate;
      if (nullable3.GetValueOrDefault())
      {
        nullable3 = asset.UnderConstruction;
        if (!nullable3.GetValueOrDefault())
        {
          if (PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.SelectSingleBound((PXGraph) docgraph, (object[]) null, new object[1]
          {
            (object) faBookBalance1.DepreciationMethodID
          })).IsNewZealandMethod)
          {
            nullable15 = PX.Objects.FA.AssetProcess.NewZelandAdditionDisposal(docgraph, asset, det, faBookBalance1);
            nullable7 = nullable8;
            nullable4 = nullable15;
            Decimal? nullable16;
            if (!(nullable7.HasValue & nullable4.HasValue))
            {
              nullable5 = new Decimal?();
              nullable16 = nullable5;
            }
            else
              nullable16 = new Decimal?(nullable7.GetValueOrDefault() - nullable4.GetValueOrDefault());
            nullable8 = nullable16;
          }
        }
      }
      nullable3 = faBookBalance1.Depreciate;
      if (nullable3.GetValueOrDefault())
      {
        nullable3 = asset.UnderConstruction;
        if (!nullable3.GetValueOrDefault())
        {
          FATran faTran2 = new FATran();
          faTran2.AssetID = faBookBalance1.AssetID;
          faTran2.BookID = faBookBalance1.BookID;
          faTran2.TranDate = det.DisposalDate;
          faTran2.FinPeriodID = faBookBalance1.DisposalPeriodID;
          faTran2.TranType = "A-";
          faTran2.DebitAccountID = asset.AccumulatedDepreciationAccountID;
          faTran2.DebitSubID = asset.AccumulatedDepreciationSubID;
          faTran2.ReclassificationOnDebitProhibited = new bool?(true);
          faTran2.CreditAccountID = nullable11;
          faTran2.CreditSubID = nullable12;
          nullable3 = ((PXSelectBase<FASetup>) docgraph.fasetup).Current.DepreciateInDisposalPeriod;
          nullable4 = !nullable3.GetValueOrDefault() || !(faBookBalance1.Status == "A") ? faBookHistory.YtdDepreciated : faBookHistory.YtdCalculated;
          nullable7 = nullable15;
          Decimal? nullable17;
          if (!(nullable4.HasValue & nullable7.HasValue))
          {
            nullable5 = new Decimal?();
            nullable17 = nullable5;
          }
          else
            nullable17 = new Decimal?(nullable4.GetValueOrDefault() - nullable7.GetValueOrDefault());
          faTran2.TranAmt = nullable17;
          FATran faTran3 = faTran2;
          faTran3.TranDesc = PXMessages.LocalizeFormatNoPrefix("Depreciation Disposal for Asset {0}", new object[1]
          {
            ((PXSelectBase<FATran>) docgraph.Trans).GetValueExt<FATran.assetID>(faTran3)
          });
          ((PXSelectBase<FATran>) docgraph.Trans).Insert(faTran3);
        }
      }
      FATran faTran4 = new FATran()
      {
        AssetID = faBookBalance1.AssetID,
        BookID = faBookBalance1.BookID,
        TranDate = det.DisposalDate,
        FinPeriodID = faBookBalance1.DisposalPeriodID,
        TranType = "S+"
      };
      nullable7 = faBookBalance1.DisposalAmount;
      Decimal num4 = 0M;
      if (nullable7.GetValueOrDefault() >= num4 & nullable7.HasValue)
      {
        faTran4.DebitAccountID = asset.DisposalAccountID;
        faTran4.DebitSubID = asset.DisposalSubID;
        faTran4.CreditAccountID = nullable11;
        faTran4.CreditSubID = nullable12;
      }
      else
      {
        faTran4.DebitAccountID = nullable11;
        faTran4.DebitSubID = nullable12;
        faTran4.CreditAccountID = asset.DisposalAccountID;
        faTran4.CreditSubID = asset.DisposalSubID;
      }
      FATran faTran5 = faTran4;
      nullable7 = faBookBalance1.DisposalAmount;
      Decimal? nullable18 = new Decimal?(Math.Abs(nullable7.Value));
      faTran5.TranAmt = nullable18;
      faTran4.RGOLAmt = nullable8;
      faTran4.TranDesc = PXMessages.LocalizeFormatNoPrefix("Sale of Asset {0}", new object[1]
      {
        ((PXSelectBase<FATran>) docgraph.Trans).GetValueExt<FATran.assetID>(faTran4)
      });
      ((PXSelectBase<FATran>) docgraph.Trans).Insert(faTran4);
    }
    if (!((PXSelectBase) docgraph.Trans).Cache.IsInsertedUpdatedDeleted)
      return;
    ((PXGraph) docgraph).Actions.PressSave();
  }

  public static Decimal? NewZelandAdditionDisposal(
    TransactionEntry docgraph,
    FixedAsset asset,
    FADetails det,
    FABookBalance bookbal)
  {
    string finPeriodIdOfYear = PX.Objects.GL.FinPeriods.FinPeriodUtils.GetFirstFinPeriodIDOfYear(bookbal.DisposalPeriodID);
    Decimal? nullable1 = new Decimal?(0M);
    List<FATran> source = new List<FATran>();
    foreach (PXResult<FATran> pxResult in PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.bookID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FATran.finPeriodID, IBqlString>.IsGreaterEqual<P.AsString>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, Equal<FATran.tranType.depreciationPlus>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, Equal<FATran.tranType.depreciationMinus>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, Equal<FATran.tranType.calculatedPlus>>>>>.Or<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.calculatedMinus>>>>>>>.Config>.Select((PXGraph) docgraph, new object[3]
    {
      (object) asset.AssetID,
      (object) bookbal.BookID,
      (object) finPeriodIdOfYear
    }))
    {
      FATran faTran1 = PXResult<FATran>.op_Implicit(pxResult);
      FATran faTran2 = new FATran()
      {
        CreditAccountID = faTran1.CreditAccountID,
        CreditSubID = faTran1.CreditSubID,
        DebitAccountID = faTran1.DebitAccountID,
        DebitSubID = faTran1.DebitSubID,
        TranAmt = faTran1.TranAmt
      };
      if (faTran1.TranType == "D-" || faTran1.TranType == "C-")
      {
        faTran2.CreditAccountID = faTran1.DebitAccountID;
        faTran2.CreditSubID = faTran1.DebitSubID;
        faTran2.DebitAccountID = faTran1.CreditAccountID;
        faTran2.DebitSubID = faTran1.CreditSubID;
        FATran faTran3 = faTran2;
        Decimal? tranAmt = faTran1.TranAmt;
        Decimal? nullable2 = tranAmt.HasValue ? new Decimal?(-tranAmt.GetValueOrDefault()) : new Decimal?();
        faTran3.TranAmt = nullable2;
      }
      source.Add(faTran2);
    }
    foreach (var data in source.GroupBy(x => new
    {
      CreditAccountID = x.CreditAccountID,
      CreditSubID = x.CreditSubID,
      DebitAccountID = x.DebitAccountID,
      DebitSubID = x.DebitSubID
    }).Select(g => new
    {
      CreditAccountID = g.Key.CreditAccountID,
      CreditSubID = g.Key.CreditSubID,
      DebitAccountID = g.Key.DebitAccountID,
      DebitSubID = g.Key.DebitSubID,
      tranAmt = g.Sum<FATran>((Func<FATran, Decimal?>) (x => x.TranAmt))
    }))
    {
      FATran faTran = new FATran()
      {
        AssetID = bookbal.AssetID,
        BookID = bookbal.BookID,
        TranDate = det.DisposalDate,
        FinPeriodID = bookbal.DisposalPeriodID,
        DebitAccountID = data.CreditAccountID,
        DebitSubID = data.CreditSubID,
        CreditAccountID = data.DebitAccountID,
        CreditSubID = data.DebitSubID,
        TranAmt = data.tranAmt
      };
      faTran.TranType = !(bookbal.CurrDeprPeriod == faTran.FinPeriodID) ? "D-" : "C-";
      faTran.TranDesc = ((PXGraph) docgraph).MakeLocalizedDescription<FATran.tranDesc>("{0} - reversed", (string) ((PXSelectBase<FATran>) docgraph.Trans).GetValueExt<FATran.assetID>(faTran));
      Decimal? nullable3 = nullable1;
      Decimal? tranAmt = data.tranAmt;
      nullable1 = nullable3.HasValue & tranAmt.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + tranAmt.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<FATran>) docgraph.Trans).Insert(faTran);
    }
    return nullable1;
  }

  public static DocumentList<FARegister> SplitAsset(
    FixedAsset asset,
    DateTime? splitDate,
    string splitPeriodID,
    bool deprBeforeSplit,
    Dictionary<FixedAsset, Decimal> splits)
  {
    TransactionEntry instance1 = PXGraph.CreateInstance<TransactionEntry>();
    OrganizationFinPeriod periodInSubledger = instance1.FinPeriodUtils.GetOpenOrganizationFinPeriodInSubledger<OrganizationFinPeriod.fAClosed>(splitPeriodID, asset.BranchID);
    splitDate = splitDate ?? periodInSubledger.StartDate;
    ((PXSelectBase<FARegister>) instance1.Document).Insert(new FARegister()
    {
      BranchID = asset.BranchID,
      Origin = "I",
      DocDesc = PXMessages.LocalizeFormatNoPrefix("Split of Asset {0}", new object[1]
      {
        (object) asset.AssetCD
      })
    });
    string str1 = PXMessages.LocalizeFormatNoPrefix("Split of Asset {0}", new object[1]
    {
      (object) asset.AssetCD
    });
    foreach (KeyValuePair<FixedAsset, Decimal> split in splits)
    {
      foreach (PXResult<FABookBalance> pxResult1 in PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) instance1, new object[1]
      {
        (object) split.Key.AssetID
      }))
      {
        FABookBalance faBookBalance1 = PXResult<FABookBalance>.op_Implicit(pxResult1);
        FABookBalance faBookBalance2 = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FixedAsset.assetID>>, And<FABookBalance.bookID, Equal<Required<FABook.bookID>>>>>.Config>.Select((PXGraph) instance1, new object[2]
        {
          (object) asset.AssetID,
          (object) faBookBalance1.BookID
        }));
        string bookPeriodIdOfDate = ((PXGraph) instance1).GetService<IFABookPeriodRepository>().GetFABookPeriodIDOfDate(splitDate, faBookBalance2.BookID, faBookBalance2.AssetID);
        bool? nullable1 = faBookBalance2.UpdateGL;
        string str2 = nullable1.GetValueOrDefault() ? periodInSubledger.FinPeriodID : bookPeriodIdOfDate;
        if (deprBeforeSplit)
        {
          PX.Objects.FA.AssetProcess.DepreciateAsset((IEnumerable<FABookBalance>) new List<FABookBalance>()
          {
            faBookBalance2
          }, splitDate, str2, false, false);
        }
        else
        {
          nullable1 = asset.UnderConstruction;
          if (!nullable1.GetValueOrDefault())
          {
            PX.Objects.FA.AssetProcess.SuspendAssetToPeriod((IEnumerable<FABookBalance>) new List<FABookBalance>()
            {
              faBookBalance2
            }, splitDate, str2);
            PX.Objects.FA.AssetProcess.CalculateAsset((IEnumerable<FABookBalance>) new List<FABookBalance>()
            {
              faBookBalance2
            }, (string) null);
          }
        }
        if (!deprBeforeSplit)
        {
          nullable1 = asset.UnderConstruction;
          if (nullable1.GetValueOrDefault())
            goto label_11;
        }
        faBookBalance2 = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelectReadonly<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FixedAsset.assetID>>, And<FABookBalance.bookID, Equal<Required<FABook.bookID>>>>>.Config>.Select((PXGraph) instance1, new object[2]
        {
          (object) asset.AssetID,
          (object) faBookBalance1.BookID
        }));
        faBookBalance1.CurrDeprPeriod = faBookBalance2.CurrDeprPeriod;
        faBookBalance1.LastDeprPeriod = faBookBalance2.LastDeprPeriod;
        faBookBalance1.LastPeriod = faBookBalance2.LastPeriod;
        faBookBalance1.DeprToPeriod = faBookBalance2.DeprToPeriod;
        ((PXSelectBase<FABookBalance>) instance1.bookbalances).Update(faBookBalance1);
label_11:
        int? nullable2 = new int?(0);
        int? nullable3 = new int?(0);
        Decimal? nullable4 = faBookBalance1.AcquisitionCost;
        FATran faTran1 = (FATran) null;
        FATran faTran2 = (FATran) null;
        string str3 = (string) null;
        if (string.IsNullOrEmpty(faBookBalance2.DeprToPeriod))
          str3 = GraphHelper.RowCast<FABookHistory>((IEnumerable) PXSelectBase<FABookHistory, PXSelect<FABookHistory, Where<FABookHistory.assetID, Equal<Required<FABookBalance.assetID>>, And<FABookHistory.bookID, Equal<Required<FABookBalance.bookID>>>>, OrderBy<Desc<FABookHistory.finPeriodID>>>.Config>.Select((PXGraph) instance1, new object[2]
          {
            (object) faBookBalance2.AssetID,
            (object) faBookBalance2.BookID
          })).FirstOrDefault<FABookHistory>().With<FABookHistory, string>((Func<FABookHistory, string>) (h => h.FinPeriodID));
        foreach (PXResult<FABookHist> pxResult2 in PXSelectBase<FABookHist, PXSelectReadonly<FABookHist, Where<FABookHist.assetID, Equal<Required<FABookHist.assetID>>, And<FABookHist.bookID, Equal<Required<FABookHist.bookID>>, And<FABookHist.finPeriodID, LessEqual<Required<FABookHist.finPeriodID>>>>>, OrderBy<Asc<FABookHist.finPeriodID>>>.Config>.Select((PXGraph) instance1, new object[3]
        {
          (object) faBookBalance2.AssetID,
          (object) faBookBalance2.BookID,
          (object) (str3 ?? faBookBalance2.DeprToPeriod)
        }))
        {
          FABookHist faBookHist1 = PXResult<FABookHist>.op_Implicit(pxResult2);
          if (string.IsNullOrEmpty(faBookBalance2.LastDeprPeriod))
          {
            nullable1 = faBookHist1.Suspended;
            if (!nullable1.GetValueOrDefault())
              goto label_19;
          }
          if (string.CompareOrdinal(faBookHist1.FinPeriodID, faBookBalance2.CurrDeprPeriod) <= 0 || faBookBalance2.Status == "F")
          {
            FABookHist faBookHist2 = new FABookHist();
            faBookHist2.AssetID = faBookBalance1.AssetID;
            faBookHist2.BookID = faBookBalance1.BookID;
            faBookHist2.FinPeriodID = faBookHist1.FinPeriodID;
            FABookHist keyedHistory = faBookHist2;
            FABookBalance bookBalance = faBookBalance1;
            FABookHist faBookHist3 = FAHelper.InsertFABookHist((PXGraph) instance1, keyedHistory, ref bookBalance);
            faBookHist3.Suspended = faBookHist1.Suspended;
            faBookHist3.Closed = faBookHist1.Closed;
            faBookHist3.YtdSuspended = faBookHist1.YtdSuspended;
            faBookHist3.YtdReversed = faBookHist1.YtdReversed;
            FABookHist faBookHist4 = faBookHist3;
            int? nullable5 = faBookHist4.YtdSuspended;
            int? nullable6 = nullable2;
            faBookHist4.YtdSuspended = nullable5.HasValue & nullable6.HasValue ? new int?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new int?();
            FABookHist faBookHist5 = faBookHist3;
            nullable6 = faBookHist5.YtdReversed;
            nullable5 = nullable3;
            faBookHist5.YtdReversed = nullable6.HasValue & nullable5.HasValue ? new int?(nullable6.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new int?();
            nullable2 = faBookHist1.YtdSuspended;
            nullable3 = faBookHist1.YtdReversed;
          }
label_19:
          Decimal? ptdDeprBase = faBookHist1.PtdDeprBase;
          Decimal num1 = 0M;
          if (!(ptdDeprBase.GetValueOrDefault() == num1 & ptdDeprBase.HasValue))
          {
            string strA = str2;
            if (!string.IsNullOrEmpty(faBookBalance2.DeprToPeriod) && string.CompareOrdinal(strA, faBookBalance2.DeprToPeriod) > 0)
              strA = faBookBalance2.DeprToPeriod;
            FATran faTran3 = new FATran();
            faTran3.AssetID = faBookBalance2.AssetID;
            faTran3.BookID = faBookBalance2.BookID;
            faTran3.TranType = "P-";
            faTran3.TranDate = splitDate;
            faTran3.FinPeriodID = strA;
            faTran3.TranPeriodID = faBookHist1.FinPeriodID;
            TransactionEntry graph1 = instance1;
            Decimal? nullable7 = faBookHist1.PtdDeprBase;
            Decimal num2 = split.Value;
            Decimal? nullable8 = nullable7.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * num2 / 100M) : new Decimal?();
            faTran3.TranAmt = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) graph1, nullable8));
            FATran faTran4 = faTran3;
            faTran1 = ((PXSelectBase<FATran>) instance1.Trans).Insert(faTran4);
            FATran faTran5 = new FATran();
            faTran5.AssetID = faBookBalance1.AssetID;
            faTran5.BookID = faBookBalance1.BookID;
            faTran5.TranType = "P+";
            faTran5.TranDate = splitDate;
            faTran5.FinPeriodID = strA;
            faTran5.TranPeriodID = faBookHist1.FinPeriodID;
            TransactionEntry graph2 = instance1;
            nullable7 = faBookHist1.PtdDeprBase;
            Decimal num3 = split.Value;
            Decimal? nullable9 = nullable7.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * num3 / 100M) : new Decimal?();
            faTran5.TranAmt = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) graph2, nullable9));
            faTran5.TranDesc = str1;
            FATran faTran6 = faTran5;
            faTran2 = ((PXSelectBase<FATran>) instance1.Trans).Insert(faTran6);
            nullable7 = nullable4;
            Decimal? tranAmt = faTran6.TranAmt;
            nullable4 = nullable7.HasValue & tranAmt.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - tranAmt.GetValueOrDefault()) : new Decimal?();
          }
        }
        Decimal? nullable10 = nullable4;
        Decimal num4 = 0M;
        if (!(nullable10.GetValueOrDefault() == num4 & nullable10.HasValue) && faTran1 != null && faTran2 != null)
        {
          FATran faTran7 = faTran1;
          nullable10 = faTran7.TranAmt;
          Decimal? nullable11 = nullable4;
          faTran7.TranAmt = nullable10.HasValue & nullable11.HasValue ? new Decimal?(nullable10.GetValueOrDefault() + nullable11.GetValueOrDefault()) : new Decimal?();
          FATran faTran8 = faTran2;
          nullable11 = faTran8.TranAmt;
          nullable10 = nullable4;
          faTran8.TranAmt = nullable11.HasValue & nullable10.HasValue ? new Decimal?(nullable11.GetValueOrDefault() + nullable10.GetValueOrDefault()) : new Decimal?();
        }
        nullable10 = faBookBalance2.YtdReconciled;
        if (nullable10.HasValue)
        {
          PXSelect<FATran, Where<FATran.refNbr, Equal<Current<FARegister.refNbr>>>> trans1 = instance1.Trans;
          FATran faTran9 = new FATran();
          faTran9.AssetID = asset.AssetID;
          faTran9.BookID = faBookBalance2.BookID;
          faTran9.TranDate = splitDate;
          faTran9.FinPeriodID = str2;
          faTran9.TranType = "R-";
          TransactionEntry graph3 = instance1;
          nullable10 = faBookBalance2.YtdReconciled;
          Decimal num5 = split.Value;
          Decimal? nullable12 = nullable10.HasValue ? new Decimal?(nullable10.GetValueOrDefault() * num5 / 100M) : new Decimal?();
          Decimal num6 = nullable12.Value;
          faTran9.TranAmt = new Decimal?(PXDBCurrencyAttribute.BaseRound((PXGraph) graph3, num6));
          faTran9.TranDesc = str1;
          ((PXSelectBase<FATran>) trans1).Insert(faTran9);
          PXSelect<FATran, Where<FATran.refNbr, Equal<Current<FARegister.refNbr>>>> trans2 = instance1.Trans;
          FATran faTran10 = new FATran();
          faTran10.AssetID = split.Key.AssetID;
          faTran10.BookID = faBookBalance1.BookID;
          faTran10.TranDate = splitDate;
          faTran10.FinPeriodID = str2;
          faTran10.TranType = "R+";
          TransactionEntry graph4 = instance1;
          nullable10 = faBookBalance2.YtdReconciled;
          Decimal num7 = split.Value;
          Decimal? nullable13;
          if (!nullable10.HasValue)
          {
            nullable12 = new Decimal?();
            nullable13 = nullable12;
          }
          else
            nullable13 = new Decimal?(nullable10.GetValueOrDefault() * num7 / 100M);
          nullable12 = nullable13;
          Decimal num8 = nullable12.Value;
          faTran10.TranAmt = new Decimal?(PXDBCurrencyAttribute.BaseRound((PXGraph) graph4, num8));
          faTran10.TranDesc = str1;
          ((PXSelectBase<FATran>) trans2).Insert(faTran10);
        }
        nullable10 = faBookBalance2.YtdDepreciated;
        if (nullable10.HasValue)
        {
          string strA = str2;
          if (string.CompareOrdinal(strA, faBookBalance2.DeprFromPeriod) < 0)
            strA = faBookBalance2.DeprFromPeriod;
          else if (!string.IsNullOrEmpty(faBookBalance2.DeprToPeriod) && string.CompareOrdinal(strA, faBookBalance2.DeprToPeriod) > 0)
            strA = faBookBalance2.DeprToPeriod;
          PXSelect<FATran, Where<FATran.refNbr, Equal<Current<FARegister.refNbr>>>> trans3 = instance1.Trans;
          FATran faTran11 = new FATran();
          faTran11.AssetID = asset.AssetID;
          faTran11.BookID = faBookBalance2.BookID;
          faTran11.TranDate = splitDate;
          faTran11.FinPeriodID = strA;
          faTran11.TranType = "A-";
          TransactionEntry graph5 = instance1;
          nullable10 = faBookBalance2.YtdDepreciated;
          Decimal num9 = split.Value;
          Decimal? nullable14 = nullable10.HasValue ? new Decimal?(nullable10.GetValueOrDefault() * num9 / 100M) : new Decimal?();
          Decimal num10 = nullable14.Value;
          faTran11.TranAmt = new Decimal?(PXDBCurrencyAttribute.BaseRound((PXGraph) graph5, num10));
          faTran11.TranDesc = str1;
          faTran11.CreditAccountID = asset.DepreciatedExpenseAccountID;
          faTran11.CreditSubID = asset.DepreciatedExpenseSubID;
          faTran11.ReclassificationOnCreditProhibited = new bool?(true);
          faTran11.DebitAccountID = asset.AccumulatedDepreciationAccountID;
          faTran11.DebitSubID = asset.AccumulatedDepreciationSubID;
          faTran11.ReclassificationOnDebitProhibited = new bool?(true);
          ((PXSelectBase<FATran>) trans3).Insert(faTran11);
          PXSelect<FATran, Where<FATran.refNbr, Equal<Current<FARegister.refNbr>>>> trans4 = instance1.Trans;
          FATran faTran12 = new FATran();
          faTran12.AssetID = split.Key.AssetID;
          faTran12.BookID = faBookBalance1.BookID;
          faTran12.TranDate = splitDate;
          faTran12.FinPeriodID = strA;
          faTran12.TranType = "A+";
          TransactionEntry graph6 = instance1;
          nullable10 = faBookBalance2.YtdDepreciated;
          Decimal num11 = split.Value;
          Decimal? nullable15;
          if (!nullable10.HasValue)
          {
            nullable14 = new Decimal?();
            nullable15 = nullable14;
          }
          else
            nullable15 = new Decimal?(nullable10.GetValueOrDefault() * num11 / 100M);
          nullable14 = nullable15;
          Decimal num12 = nullable14.Value;
          faTran12.TranAmt = new Decimal?(PXDBCurrencyAttribute.BaseRound((PXGraph) graph6, num12));
          faTran12.TranDesc = str1;
          faTran12.CreditAccountID = split.Key.AccumulatedDepreciationAccountID;
          faTran12.CreditSubID = split.Key.AccumulatedDepreciationSubID;
          faTran12.ReclassificationOnCreditProhibited = new bool?(true);
          faTran12.DebitAccountID = split.Key.DepreciatedExpenseAccountID;
          faTran12.DebitSubID = split.Key.DepreciatedExpenseSubID;
          faTran12.ReclassificationOnDebitProhibited = new bool?(true);
          ((PXSelectBase<FATran>) trans4).Insert(faTran12);
        }
      }
    }
    DocumentList<Batch> documentList = new DocumentList<Batch>((PXGraph) instance1);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (((PXSelectBase) instance1.Trans).Cache.IsInsertedUpdatedDeleted)
      {
        PXUpdate<Set<FADetails.salvageAmount, Required<FADetails.salvageAmount>>, FADetails, Where<BqlOperand<FADetails.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Update((PXGraph) instance1, new object[2]
        {
          (object) asset.SalvageAmtAfterSplit,
          (object) asset.AssetID
        });
        PXUpdate<Set<FABookBalance.salvageAmount, Required<FABookBalance.salvageAmount>>, FABookBalance, Where<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Update((PXGraph) instance1, new object[2]
        {
          (object) asset.SalvageAmtAfterSplit,
          (object) asset.AssetID
        });
        foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXViewOf<FABookBalance>.BasedOn<SelectFromBase<FABookBalance, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<P.AsInt>>.Order<By<Asc<FABookBalance.assetID, Asc<FABookBalance.bookID>>>>>.ReadOnly.Config>.Select((PXGraph) instance1, new object[1]
        {
          (object) asset.AssetID
        }))
        {
          FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
          PXTimeStampScope.PutPersisted(((PXSelectBase) instance1.bookbalances).Cache, (object) faBookBalance, new object[1]
          {
            (object) faBookBalance.tstamp
          });
        }
        ((PXGraph) instance1).Actions.PressSave();
        PXTimeStampScope.SetRecordComesFirst(typeof (FABookBalance), true);
      }
      if (((PXSelectBase<FASetup>) instance1.fasetup).Current.AutoReleaseSplit.GetValueOrDefault() && instance1.created.Count > 0)
        documentList = AssetTranRelease.ReleaseDoc((List<FARegister>) instance1.created, false, false);
      transactionScope.Complete((PXGraph) instance1);
    }
    PostGraph instance2 = PXGraph.CreateInstance<PostGraph>();
    foreach (Batch b in (List<Batch>) documentList)
    {
      ((PXGraph) instance2).Clear();
      instance2.PostBatchProc(b);
    }
    return instance1.created;
  }

  public static void AcquireAsset(
    TransactionEntry docgraph,
    int BranchID,
    IDictionary<FABookBalance, List<OperableTran>> booktrn,
    FARegister register)
  {
    int? nullable1;
    if (register != null)
    {
      ((PXSelectBase<FARegister>) docgraph.Document).Current = register;
      nullable1 = register.BranchID;
      int num = BranchID;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        FARegister copy = PXCache<FARegister>.CreateCopy(register);
        copy.BranchID = new int?(BranchID);
        register = ((PXSelectBase<FARegister>) docgraph.Document).Update(copy);
      }
    }
    else
    {
      TransactionEntry.SegregateRegister((PXGraph) docgraph, BranchID, "P", (string) null, new DateTime?(), "", docgraph.created);
      register = ((PXSelectBase<FARegister>) docgraph.Document).Current;
    }
    ((PXSelectBase) docgraph.Asset).Cache.Clear();
    ((PXGraph) docgraph).Clear((PXClearOption) 4);
    foreach (KeyValuePair<FABookBalance, List<OperableTran>> keyValuePair in (IEnumerable<KeyValuePair<FABookBalance, List<OperableTran>>>) booktrn)
    {
      FixedAsset asset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) docgraph, new object[1]
      {
        (object) keyValuePair.Key.AssetID
      }));
      FADetails details = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) docgraph, new object[1]
      {
        (object) keyValuePair.Key.AssetID
      }));
      FABookBalance bookBal = ((PXSelectBase<FABookBalance>) docgraph.bookbalances).SelectSingle(new object[2]
      {
        (object) keyValuePair.Key.AssetID,
        (object) keyValuePair.Key.BookID
      });
      foreach (OperableTran operableTran in keyValuePair.Value)
      {
        if (operableTran.Op == 1)
        {
          if (docgraph.UpdateGL)
          {
            bool? released = register.Released;
            bool flag = false;
            if (!(released.GetValueOrDefault() == flag & released.HasValue))
              goto label_12;
          }
          GraphHelper.MarkUpdated(((PXSelectBase) docgraph.Document).Cache, (object) register);
          EnumerableExtensions.ForEach<FATran>(GraphHelper.RowCast<FATran>((IEnumerable) PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Required<FATran.assetID>>, And<FATran.bookID, Equal<Required<FATran.bookID>>>>>.Config>.Select((PXGraph) docgraph, new object[2]
          {
            (object) keyValuePair.Key.AssetID,
            (object) keyValuePair.Key.BookID
          })), (Action<FATran>) (tran => ((PXSelectBase<FATran>) docgraph.Trans).Delete(tran)));
          PXDatabase.Delete<FABookHistory>(new PXDataFieldRestrict[2]
          {
            (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.assetID>((object) keyValuePair.Key.AssetID),
            (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.bookID>((object) keyValuePair.Key.BookID)
          });
          bookBal.MaxHistoryPeriodID = (string) null;
          GraphHelper.EnsureRowPersistence((PXGraph) docgraph, (object) bookBal);
          operableTran.Op = (PXDBOperation) 2;
          ((PXSelectBase<FARegister>) docgraph.Document).Update(((PXSelectBase<FARegister>) docgraph.Document).Current);
        }
label_12:
        if (string.IsNullOrEmpty(keyValuePair.Key.LastDeprPeriod))
        {
          if (operableTran.Op == 3 || operableTran.Op == 1)
          {
            PXDatabase.Delete<FABookHistory>(new PXDataFieldRestrict[2]
            {
              (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.assetID>((object) keyValuePair.Key.AssetID),
              (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.bookID>((object) keyValuePair.Key.BookID)
            });
            if (operableTran.Op != 3)
            {
              bookBal.MaxHistoryPeriodID = (string) null;
              GraphHelper.EnsureRowPersistence((PXGraph) docgraph, (object) bookBal);
            }
          }
          if (operableTran.Op == 2)
          {
            nullable1 = asset.SplittedFrom;
            if (!nullable1.HasValue)
              goto label_19;
          }
          if (operableTran.Op != 1)
            goto label_20;
label_19:
          FABookHist faBookHist = new FABookHist();
          faBookHist.AssetID = keyValuePair.Key.AssetID;
          faBookHist.BookID = keyValuePair.Key.BookID;
          faBookHist.FinPeriodID = PX.Objects.FA.AssetProcess.GetDeprFromPeriod((PXGraph) docgraph, keyValuePair.Key, asset, details);
          FABookHist keyedHistory = faBookHist;
          FAHelper.InsertFABookHist((PXGraph) docgraph, keyedHistory, ref bookBal);
          bookBal.CurrDeprPeriod = PX.Objects.FA.AssetProcess.GetDeprFromPeriod((PXGraph) docgraph, keyValuePair.Key, asset, details);
          ((PXSelectBase<FABookBalance>) docgraph.bookbalances).Update(bookBal);
        }
label_20:
        switch (operableTran.Op - 1)
        {
          case 0:
            FATran copy = PXCache<FATran>.CreateCopy(operableTran.Tran);
            copy.TranDate = details.ReceiptDate;
            ((PXSelectBase) docgraph.Trans).Cache.SetDefaultExt<FATran.finPeriodID>((object) copy);
            copy.TranAmt = keyValuePair.Key.AcquisitionCost;
            ((PXSelectBase<FATran>) docgraph.Trans).Update(copy);
            continue;
          case 1:
            nullable1 = asset.SplittedFrom;
            if (!nullable1.HasValue)
            {
              FATran faTran1 = new FATran()
              {
                AssetID = keyValuePair.Key.AssetID,
                BookID = keyValuePair.Key.BookID,
                TranDate = details.ReceiptDate,
                TranAmt = keyValuePair.Key.AcquisitionCost,
                TranType = "P+",
                Released = new bool?(!docgraph.UpdateGL)
              };
              faTran1.TranDesc = PXMessages.LocalizeFormatNoPrefix("Purchase for Asset {0}", new object[1]
              {
                ((PXSelectBase<FATran>) docgraph.Trans).GetValueExt<FATran.assetID>(faTran1)
              });
              FATran faTran2 = ((PXSelectBase<FATran>) docgraph.Trans).Insert(faTran1);
              if (!docgraph.UpdateGL)
              {
                FATran faTran3 = (FATran) null;
                if (!string.IsNullOrEmpty(keyValuePair.Key.LastDeprPeriod))
                {
                  FATran faTran4 = new FATran()
                  {
                    AssetID = keyValuePair.Key.AssetID,
                    BookID = keyValuePair.Key.BookID,
                    TranDate = keyValuePair.Key.DeprFromDate,
                    FinPeriodID = keyValuePair.Key.DeprFromPeriod,
                    TranAmt = keyValuePair.Key.Tax179Amount,
                    TranType = "D+",
                    MethodDesc = "TAX179",
                    Released = new bool?(true)
                  };
                  faTran4.TranDesc = PXMessages.LocalizeFormatNoPrefix("Depreciation for Asset {0}", new object[1]
                  {
                    ((PXSelectBase<FATran>) docgraph.Trans).GetValueExt<FATran.assetID>(faTran4)
                  });
                  ((PXSelectBase<FATran>) docgraph.Trans).Insert(faTran4);
                  FATran faTran5 = new FATran()
                  {
                    AssetID = keyValuePair.Key.AssetID,
                    BookID = keyValuePair.Key.BookID,
                    TranDate = keyValuePair.Key.DeprFromDate,
                    FinPeriodID = keyValuePair.Key.DeprFromPeriod,
                    TranAmt = keyValuePair.Key.BonusAmount,
                    TranType = "D+",
                    MethodDesc = "BONUS",
                    Released = new bool?(true)
                  };
                  faTran5.TranDesc = PXMessages.LocalizeFormatNoPrefix("Depreciation for Asset {0}", new object[1]
                  {
                    ((PXSelectBase<FATran>) docgraph.Trans).GetValueExt<FATran.assetID>(faTran5)
                  });
                  ((PXSelectBase<FATran>) docgraph.Trans).Insert(faTran5);
                  FATran faTran6 = new FATran();
                  faTran6.AssetID = keyValuePair.Key.AssetID;
                  faTran6.BookID = keyValuePair.Key.BookID;
                  faTran6.TranDate = new DateTime?(((PXGraph) docgraph).GetService<IFABookPeriodUtils>().GetFABookPeriodEndDate(keyValuePair.Key.LastDeprPeriod, keyValuePair.Key.BookID, keyValuePair.Key.AssetID));
                  faTran6.FinPeriodID = keyValuePair.Key.LastDeprPeriod;
                  Decimal? ytdDepreciated = keyValuePair.Key.YtdDepreciated;
                  Decimal? tax179Amount = keyValuePair.Key.Tax179Amount;
                  Decimal? bonusAmount = keyValuePair.Key.BonusAmount;
                  Decimal? nullable2 = tax179Amount.HasValue & bonusAmount.HasValue ? new Decimal?(tax179Amount.GetValueOrDefault() + bonusAmount.GetValueOrDefault()) : new Decimal?();
                  faTran6.TranAmt = ytdDepreciated.HasValue & nullable2.HasValue ? new Decimal?(ytdDepreciated.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
                  faTran6.TranType = "D+";
                  faTran6.Released = new bool?(true);
                  FATran faTran7 = faTran6;
                  faTran7.TranDesc = PXMessages.LocalizeFormatNoPrefix("Depreciation for Asset {0}", new object[1]
                  {
                    ((PXSelectBase<FATran>) docgraph.Trans).GetValueExt<FATran.assetID>(faTran7)
                  });
                  faTran3 = ((PXSelectBase<FATran>) docgraph.Trans).Insert(faTran7);
                }
                FATran faTran8 = new FATran()
                {
                  AssetID = keyValuePair.Key.AssetID,
                  BookID = keyValuePair.Key.BookID,
                  TranDate = details.ReceiptDate,
                  TranAmt = keyValuePair.Key.AcquisitionCost,
                  TranType = "R+",
                  Released = new bool?(true)
                };
                ((PXSelectBase<FATran>) docgraph.Trans).Insert(faTran8);
                bookBal = (FABookBalance) ((PXSelectBase) docgraph.bookbalances).Cache.Locate((object) bookBal) ?? bookBal;
                bookBal.IsAcquired = new bool?(true);
                ((PXSelectBase<FABookBalance>) docgraph.bookbalances).Update(bookBal);
                asset.IsAcquired = new bool?(true);
                ((PXSelectBase<FixedAsset>) docgraph.Asset).Update(asset);
                ((PXSelectBase<FARegister>) docgraph.Document).Current.Released = new bool?(true);
                ((SelectedEntityEvent<FARegister>) PXEntityEventBase<FARegister>.Container<FARegister.Events>.Select((Expression<Func<FARegister.Events, PXEntityEvent<FARegister.Events>>>) (ev => ev.ReleaseDocument))).FireOn((PXGraph) docgraph, ((PXSelectBase<FARegister>) docgraph.Document).Current);
                FABookHist faBookHist1 = new FABookHist();
                faBookHist1.AssetID = faTran2.AssetID;
                faBookHist1.BookID = faTran2.BookID;
                faBookHist1.FinPeriodID = faTran2.FinPeriodID;
                FABookHist keyedHistory1 = faBookHist1;
                FABookHist faBookHist2 = FAHelper.InsertFABookHist((PXGraph) docgraph, keyedHistory1, ref bookBal);
                FABookHist faBookHist3 = faBookHist2;
                Decimal? ptdAcquired = faBookHist3.PtdAcquired;
                Decimal? nullable3 = faTran2.TranAmt;
                faBookHist3.PtdAcquired = ptdAcquired.HasValue & nullable3.HasValue ? new Decimal?(ptdAcquired.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist4 = faBookHist2;
                nullable3 = faBookHist4.YtdAcquired;
                Decimal? tranAmt = faTran2.TranAmt;
                faBookHist4.YtdAcquired = nullable3.HasValue & tranAmt.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + tranAmt.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist5 = faBookHist2;
                Decimal? ytdBal = faBookHist5.YtdBal;
                nullable3 = faTran2.TranAmt;
                faBookHist5.YtdBal = ytdBal.HasValue & nullable3.HasValue ? new Decimal?(ytdBal.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                if (!((PXSelectBase<FASetup>) docgraph.fasetup).Current.UpdateGL.GetValueOrDefault())
                {
                  FABookHist faBookHist6 = faBookHist2;
                  nullable3 = faBookHist6.PtdReconciled;
                  Decimal? acquisitionCost = keyValuePair.Key.AcquisitionCost;
                  faBookHist6.PtdReconciled = nullable3.HasValue & acquisitionCost.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + acquisitionCost.GetValueOrDefault()) : new Decimal?();
                  FABookHist faBookHist7 = faBookHist2;
                  Decimal? ytdReconciled = faBookHist7.YtdReconciled;
                  nullable3 = keyValuePair.Key.AcquisitionCost;
                  faBookHist7.YtdReconciled = ytdReconciled.HasValue & nullable3.HasValue ? new Decimal?(ytdReconciled.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                }
                FABookHist faBookHist8 = new FABookHist();
                faBookHist8.AssetID = keyValuePair.Key.AssetID;
                faBookHist8.BookID = keyValuePair.Key.BookID;
                faBookHist8.FinPeriodID = PX.Objects.FA.AssetProcess.GetDeprFromPeriod((PXGraph) docgraph, keyValuePair.Key, asset, details);
                FABookHist keyedHistory2 = faBookHist8;
                FABookHist faBookHist9 = FAHelper.InsertFABookHist((PXGraph) docgraph, keyedHistory2, ref bookBal);
                FABookHist faBookHist10 = faBookHist9;
                nullable3 = faBookHist10.PtdDeprBase;
                Decimal? nullable4 = keyValuePair.Key.AcquisitionCost;
                faBookHist10.PtdDeprBase = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist11 = faBookHist9;
                nullable4 = faBookHist11.YtdDeprBase;
                nullable3 = keyValuePair.Key.AcquisitionCost;
                faBookHist11.YtdDeprBase = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist12 = faBookHist9;
                nullable3 = faBookHist12.PtdDepreciated;
                Decimal? tax179Amount1 = keyValuePair.Key.Tax179Amount;
                Decimal? bonusAmount1 = keyValuePair.Key.BonusAmount;
                nullable4 = tax179Amount1.HasValue & bonusAmount1.HasValue ? new Decimal?(tax179Amount1.GetValueOrDefault() + bonusAmount1.GetValueOrDefault()) : new Decimal?();
                faBookHist12.PtdDepreciated = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist13 = faBookHist9;
                nullable4 = faBookHist13.YtdDepreciated;
                Decimal? tax179Amount2 = keyValuePair.Key.Tax179Amount;
                Decimal? bonusAmount2 = keyValuePair.Key.BonusAmount;
                nullable3 = tax179Amount2.HasValue & bonusAmount2.HasValue ? new Decimal?(tax179Amount2.GetValueOrDefault() + bonusAmount2.GetValueOrDefault()) : new Decimal?();
                faBookHist13.YtdDepreciated = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist14 = faBookHist9;
                nullable3 = faBookHist14.YtdBal;
                Decimal? tax179Amount3 = keyValuePair.Key.Tax179Amount;
                Decimal? bonusAmount3 = keyValuePair.Key.BonusAmount;
                nullable4 = tax179Amount3.HasValue & bonusAmount3.HasValue ? new Decimal?(tax179Amount3.GetValueOrDefault() + bonusAmount3.GetValueOrDefault()) : new Decimal?();
                faBookHist14.YtdBal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist15 = faBookHist9;
                nullable4 = faBookHist15.PtdTax179;
                nullable3 = keyValuePair.Key.Tax179Amount;
                faBookHist15.PtdTax179 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist16 = faBookHist9;
                nullable3 = faBookHist16.YtdTax179;
                nullable4 = keyValuePair.Key.Tax179Amount;
                faBookHist16.YtdTax179 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist17 = faBookHist9;
                nullable4 = faBookHist17.PtdBonus;
                nullable3 = keyValuePair.Key.BonusAmount;
                faBookHist17.PtdBonus = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist18 = faBookHist9;
                nullable3 = faBookHist18.YtdBonus;
                nullable4 = keyValuePair.Key.BonusAmount;
                faBookHist18.YtdBonus = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                faBookHist9.Closed = new bool?(faTran3 != null);
                KeyValuePair<FABookBalance, List<OperableTran>> b = keyValuePair;
                foreach (FABookHistory faBookHistory in GraphHelper.RowCast<FABookPeriod>((IEnumerable) PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, Greater<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.finPeriodID, Less<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.endDate, Greater<FABookPeriod.startDate>>>>>>>.Config>.Select((PXGraph) docgraph, new object[4]
                {
                  (object) keyValuePair.Key.BookID,
                  (object) ((PXGraph) docgraph).GetService<IFABookPeriodRepository>().GetFABookPeriodOrganizationID(keyValuePair.Key),
                  (object) keyValuePair.Key.DeprFromPeriod,
                  (object) keyValuePair.Key.LastDeprPeriod
                })).Select<FABookPeriod, FABookHist>((Func<FABookPeriod, FABookHist>) (per =>
                {
                  return new FABookHist()
                  {
                    AssetID = b.Key.AssetID,
                    BookID = b.Key.BookID,
                    FinPeriodID = per.FinPeriodID
                  };
                })).Select<FABookHist, FABookHist>((Func<FABookHist, FABookHist>) (hist => hist = FAHelper.InsertFABookHist((PXGraph) docgraph, hist, ref bookBal))))
                  faBookHistory.Closed = new bool?(true);
                bookBal.InitPeriod = keyValuePair.Key.DeprFromPeriod;
                ((PXSelectBase<FABookBalance>) docgraph.bookbalances).Update(bookBal);
                if (!string.IsNullOrEmpty(keyValuePair.Key.LastDeprPeriod))
                {
                  FABookHist faBookHist19 = new FABookHist();
                  faBookHist19.AssetID = keyValuePair.Key.AssetID;
                  faBookHist19.BookID = keyValuePair.Key.BookID;
                  faBookHist19.FinPeriodID = keyValuePair.Key.LastDeprPeriod;
                  FABookHist keyedHistory3 = faBookHist19;
                  FABookHist faBookHist20 = FAHelper.InsertFABookHist((PXGraph) docgraph, keyedHistory3, ref bookBal);
                  FABookHist faBookHist21 = faBookHist20;
                  Decimal? nullable5 = faBookHist21.PtdDepreciated;
                  Decimal? nullable6 = keyValuePair.Key.YtdDepreciated;
                  Decimal? nullable7 = keyValuePair.Key.Tax179Amount;
                  Decimal? nullable8 = keyValuePair.Key.BonusAmount;
                  Decimal? nullable9 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
                  Decimal? nullable10;
                  if (!(nullable6.HasValue & nullable9.HasValue))
                  {
                    nullable8 = new Decimal?();
                    nullable10 = nullable8;
                  }
                  else
                    nullable10 = new Decimal?(nullable6.GetValueOrDefault() - nullable9.GetValueOrDefault());
                  Decimal? nullable11 = nullable10;
                  faBookHist21.PtdDepreciated = nullable5.HasValue & nullable11.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable11.GetValueOrDefault()) : new Decimal?();
                  FABookHist faBookHist22 = faBookHist20;
                  Decimal? nullable12 = faBookHist22.YtdDepreciated;
                  Decimal? nullable13 = keyValuePair.Key.YtdDepreciated;
                  nullable8 = keyValuePair.Key.Tax179Amount;
                  nullable7 = keyValuePair.Key.BonusAmount;
                  nullable6 = nullable8.HasValue & nullable7.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
                  Decimal? nullable14;
                  if (!(nullable13.HasValue & nullable6.HasValue))
                  {
                    nullable7 = new Decimal?();
                    nullable14 = nullable7;
                  }
                  else
                    nullable14 = new Decimal?(nullable13.GetValueOrDefault() - nullable6.GetValueOrDefault());
                  nullable5 = nullable14;
                  Decimal? nullable15;
                  if (!(nullable12.HasValue & nullable5.HasValue))
                  {
                    nullable6 = new Decimal?();
                    nullable15 = nullable6;
                  }
                  else
                    nullable15 = new Decimal?(nullable12.GetValueOrDefault() + nullable5.GetValueOrDefault());
                  faBookHist22.YtdDepreciated = nullable15;
                  FABookHist faBookHist23 = faBookHist20;
                  nullable5 = faBookHist23.YtdBal;
                  nullable6 = keyValuePair.Key.YtdDepreciated;
                  nullable7 = keyValuePair.Key.Tax179Amount;
                  nullable8 = keyValuePair.Key.BonusAmount;
                  nullable13 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
                  Decimal? nullable16;
                  if (!(nullable6.HasValue & nullable13.HasValue))
                  {
                    nullable8 = new Decimal?();
                    nullable16 = nullable8;
                  }
                  else
                    nullable16 = new Decimal?(nullable6.GetValueOrDefault() - nullable13.GetValueOrDefault());
                  nullable12 = nullable16;
                  Decimal? nullable17;
                  if (!(nullable5.HasValue & nullable12.HasValue))
                  {
                    nullable13 = new Decimal?();
                    nullable17 = nullable13;
                  }
                  else
                    nullable17 = new Decimal?(nullable5.GetValueOrDefault() - nullable12.GetValueOrDefault());
                  faBookHist23.YtdBal = nullable17;
                  faBookHist20.Closed = new bool?(true);
                  bool? underConstruction;
                  if (string.CompareOrdinal(keyValuePair.Key.LastDeprPeriod, keyValuePair.Key.DeprToPeriod) < 0)
                  {
                    FABookHist faBookHist24 = new FABookHist();
                    faBookHist24.AssetID = keyValuePair.Key.AssetID;
                    faBookHist24.BookID = keyValuePair.Key.BookID;
                    faBookHist24.FinPeriodID = ((PXGraph) docgraph).GetService<IFABookPeriodUtils>().PeriodPlusPeriodsCount(keyValuePair.Key.LastDeprPeriod, 1, keyValuePair.Key.BookID, keyValuePair.Key.AssetID);
                    FABookHist keyedHistory4 = faBookHist24;
                    FAHelper.InsertFABookHist((PXGraph) docgraph, keyedHistory4, ref bookBal).Closed = new bool?(false);
                    bookBal.Status = "A";
                    bookBal.CurrDeprPeriod = ((PXGraph) docgraph).GetService<IFABookPeriodUtils>().PeriodPlusPeriodsCount(keyValuePair.Key.LastDeprPeriod, 1, keyValuePair.Key.BookID, keyValuePair.Key.AssetID);
                    bookBal.InitPeriod = PX.Objects.FA.AssetProcess.GetDeprFromPeriod((PXGraph) docgraph, keyValuePair.Key, asset, details);
                    FABookBalance faBookBalance = bookBal;
                    underConstruction = asset.UnderConstruction;
                    string str = underConstruction.GetValueOrDefault() ? bookBal.InitPeriod : keyValuePair.Key.DeprToPeriod;
                    faBookBalance.LastPeriod = str;
                    ((PXSelectBase<FABookBalance>) docgraph.bookbalances).Update(bookBal);
                    if (!PX.Objects.FA.AssetProcess.IsFullyDepreciatedAsset((PXGraph) docgraph, bookBal.AssetID))
                    {
                      asset = ((PXSelectBase<FixedAsset>) docgraph.Asset).Update(((PXSelectBase<FixedAsset>) docgraph.Asset).Locate(asset) ?? asset);
                      ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.NotFullyDepreciateAsset))).FireOn((PXGraph) docgraph, asset);
                    }
                  }
                  if (string.CompareOrdinal(keyValuePair.Key.LastDeprPeriod, keyValuePair.Key.DeprToPeriod) == 0)
                  {
                    bookBal.Status = "F";
                    bookBal.CurrDeprPeriod = (string) null;
                    bookBal.InitPeriod = PX.Objects.FA.AssetProcess.GetDeprFromPeriod((PXGraph) docgraph, keyValuePair.Key, asset, details);
                    FABookBalance faBookBalance = bookBal;
                    underConstruction = asset.UnderConstruction;
                    string str = underConstruction.GetValueOrDefault() ? bookBal.InitPeriod : keyValuePair.Key.DeprToPeriod;
                    faBookBalance.LastPeriod = str;
                    ((PXSelectBase<FABookBalance>) docgraph.bookbalances).Update(bookBal);
                    if (PX.Objects.FA.AssetProcess.IsFullyDepreciatedAsset((PXGraph) docgraph, bookBal.AssetID))
                    {
                      asset = ((PXSelectBase<FixedAsset>) docgraph.Asset).Update(((PXSelectBase<FixedAsset>) docgraph.Asset).Locate(asset) ?? asset);
                      ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.FullyDepreciateAsset))).FireOn((PXGraph) docgraph, asset);
                      continue;
                    }
                    continue;
                  }
                  continue;
                }
                continue;
              }
              continue;
            }
            continue;
          case 2:
            ((PXSelectBase<FATran>) docgraph.Trans).Delete(operableTran.Tran);
            continue;
          default:
            continue;
        }
      }
    }
    if (!((PXSelectBase) docgraph.Trans).Cache.IsInsertedUpdatedDeleted)
      return;
    ((PXAction) docgraph.Save).Press();
  }

  protected static string GetDeprFromPeriod(
    PXGraph docgraph,
    FABookBalance bookbal,
    FixedAsset asset,
    FADetails details)
  {
    return asset.UnderConstruction.GetValueOrDefault() && bookbal.DeprFromPeriod == null && !details.DepreciateFromDate.HasValue ? docgraph.GetService<IFABookPeriodRepository>().GetFABookPeriodIDOfDate(details.ReceiptDate, bookbal.BookID, bookbal.AssetID) : bookbal.DeprFromPeriod;
  }

  public static bool CalculateAsset(IEnumerable<FABookBalance> books, string maxPeriodID)
  {
    bool asset = true;
    DepreciationCalculation instance = PXGraph.CreateInstance<DepreciationCalculation>();
    foreach (FABookBalance book in books)
    {
      ((PXGraph) instance).Clear();
      ((PXGraph) instance).SelectTimeStamp();
      PXProcessing<FABookBalance>.SetCurrentItem((object) book);
      ((PXCache) GraphHelper.Caches<FABookBalance>((PXGraph) instance)).SetStatus((object) book, (PXEntryStatus) 0);
      try
      {
        instance.Calculate(book, maxPeriodID);
        if (PXProcessing<FABookBalance>.GetItemMessage() == null)
          PXProcessing<FABookBalance>.SetProcessed();
      }
      catch (PXException ex)
      {
        PXProcessing<FABookBalance>.SetError((Exception) ex);
        asset = false;
      }
    }
    return asset;
  }

  public static bool DepreciateAsset(
    IEnumerable<FABookBalance> books,
    DateTime? DateTo,
    string PeriodTo,
    bool IsMassProcess)
  {
    return PX.Objects.FA.AssetProcess.DepreciateAsset(books, DateTo, PeriodTo, IsMassProcess, true);
  }

  public static bool DepreciateAsset(
    IEnumerable<FABookBalance> books,
    DateTime? DateTo,
    string PeriodTo,
    bool IsMassProcess,
    bool IncludeLastPeriod)
  {
    bool flag = true;
    TransactionEntry instance1 = PXGraph.CreateInstance<TransactionEntry>();
    DepreciationCalculation instance2 = PXGraph.CreateInstance<DepreciationCalculation>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance1).FieldDefaulting.AddHandler<FATran.finPeriodID>(PX.Objects.FA.AssetProcess.\u003C\u003Ec.\u003C\u003E9__69_0 ?? (PX.Objects.FA.AssetProcess.\u003C\u003Ec.\u003C\u003E9__69_0 = new PXFieldDefaulting((object) PX.Objects.FA.AssetProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CDepreciateAsset\u003Eb__69_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance1).FieldVerifying.AddHandler<FATran.finPeriodID>(PX.Objects.FA.AssetProcess.\u003C\u003Ec.\u003C\u003E9__69_1 ?? (PX.Objects.FA.AssetProcess.\u003C\u003Ec.\u003C\u003E9__69_1 = new PXFieldVerifying((object) PX.Objects.FA.AssetProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CDepreciateAsset\u003Eb__69_1))));
    PXCacheEx.Adjust<FABookPeriodSelectorAttribute>(((PXSelectBase) instance1.Trans).Cache, (object) null).For<FATran.finPeriodID>((Action<FABookPeriodSelectorAttribute>) (atr => atr.RedefaultOnDateChanged = false));
    foreach (FABookBalance book in books)
    {
      PXProcessing<FABookBalance>.SetCurrentItem((object) book);
      try
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod> pxResult1 = (PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod>) PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelectJoin<FABookBalance, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<FABookBalance.assetID>>, InnerJoin<FAClass, On<FixedAsset.classID, Equal<FixedAsset.classID>>, InnerJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>, InnerJoin<FADepreciationMethod, On<FADepreciationMethod.methodID, Equal<FABookBalance.depreciationMethodID>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<FixedAsset.branchID>>, LeftJoin<OrganizationFinPeriod, On<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.Branch.branchID>, And<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>>>>>>>>>, Where<FABookBalance.assetID, Equal<Current<FABookBalance.assetID>>, And<FABookBalance.bookID, Equal<Current<FABookBalance.bookID>>>>>.Config>.SelectSingleBound((PXGraph) instance1, new object[1]
          {
            (object) book
          }, new object[1]{ (object) PeriodTo }));
          FixedAsset fixedAsset1 = PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod>.op_Implicit(pxResult1);
          FAClass faClass = PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod>.op_Implicit(pxResult1);
          FABookBalance assetBalance = PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod>.op_Implicit(pxResult1);
          FADetails faDetails1 = PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod>.op_Implicit(pxResult1);
          FADepreciationMethod depreciationMethod = PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod>.op_Implicit(pxResult1);
          OrganizationFinPeriod organizationFinPeriod = PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod>.op_Implicit(pxResult1);
          if (organizationFinPeriod != null && !organizationFinPeriod.FAClosed.GetValueOrDefault())
          {
            TransactionEntry transactionEntry = instance1;
            List<object> objectList = new List<object>();
            objectList.Add((object) new PXResult<OrganizationFinPeriod, PX.Objects.GL.Branch, FixedAsset>(PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod>.op_Implicit(pxResult1), PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod>.op_Implicit(pxResult1), PXResult<FABookBalance, FixedAsset, FAClass, FADetails, FADepreciationMethod, PX.Objects.GL.Branch, OrganizationFinPeriod>.op_Implicit(pxResult1)));
            PXQueryParameters pxQueryParameters = PXQueryParameters.ExplicitParameters(new object[2]
            {
              (object) book.AssetID,
              (object) PeriodTo
            });
            PXSelectBase<OrganizationFinPeriod, PXSelectJoin<OrganizationFinPeriod, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>>, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>, And<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>>, Where<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>, And<OrganizationFinPeriod.fAClosed, NotEqual<True>>>>.Config>.StoreResult((PXGraph) transactionEntry, objectList, pxQueryParameters);
          }
          instance1.FinPeriodUtils.GetOpenOrganizationFinPeriodInFA(PeriodTo, book.AssetID);
          ((PXGraph) instance2).Clear();
          PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.StoreResult((PXGraph) instance2, (IBqlTable) fixedAsset1);
          PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.StoreResult((PXGraph) instance2, (IBqlTable) faDetails1);
          DepreciationCalculation depreciationCalculation = instance2;
          List<object> objectList1 = new List<object>();
          objectList1.Add((object) faClass);
          PXQueryParameters pxQueryParameters1 = PXQueryParameters.ExplicitParameters(new object[1]
          {
            (object) fixedAsset1.ClassID
          });
          PXSelectBase<FAClass, PXSelect<FAClass, Where<FAClass.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.StoreResult((PXGraph) depreciationCalculation, objectList1, pxQueryParameters1);
          PXCache<FixedAsset>.StoreOriginal((PXGraph) instance2, fixedAsset1);
          PXCache<FADetails>.StoreOriginal((PXGraph) instance2, faDetails1);
          PXCache<FABookBalance>.StoreOriginal((PXGraph) instance2, assetBalance);
          PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.StoreResult((PXGraph) instance2, (IBqlTable) depreciationMethod);
          instance2.Calculate(assetBalance, PeriodTo);
          string finPeriodID = assetBalance.UpdateGL.GetValueOrDefault() || !DateTo.HasValue ? PeriodTo : ((PXGraph) instance1).GetService<IFABookPeriodRepository>().GetFABookPeriodIDOfDate(DateTo, assetBalance.BookID, assetBalance.AssetID);
          if (!IncludeLastPeriod)
            finPeriodID = ((PXGraph) instance1).GetService<IFABookPeriodUtils>().PeriodPlusPeriodsCount(finPeriodID, -1, assetBalance.BookID, assetBalance.AssetID);
          foreach (PXResult<FABookHistory, FixedAsset, PX.Objects.GL.Branch, FABook, FABookPeriod, FADetails> pxResult2 in PXSelectBase<FABookHistory, PXSelectJoin<FABookHistory, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<FABookHistory.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<FABook, On<FABookHistory.bookID, Equal<FABook.bookID>>, LeftJoin<FABookPeriod, On<FABookPeriod.bookID, Equal<FABookHistory.bookID>, And<FABookPeriod.organizationID, Equal<IIf<Where<FABook.updateGL, Equal<True>>, PX.Objects.GL.Branch.organizationID, FinPeriod.organizationID.masterValue>>, And<FABookPeriod.finPeriodID, Equal<FABookHistory.finPeriodID>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>>>>, LeftJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>>>>>>, Where<FABookHistory.assetID, Equal<Current<FABookBalance.assetID>>, And<FABookHistory.bookID, Equal<Current<FABookBalance.bookID>>, And<FABookHistory.finPeriodID, GreaterEqual<Required<FABookHistory.finPeriodID>>, And<FABookHistory.finPeriodID, LessEqual<Required<FABookHistory.finPeriodID>>, And<FABookHistory.closed, NotEqual<True>>>>>>, OrderBy<Asc<FABookHistory.finPeriodID>>>.Config>.SelectMultiBound((PXGraph) instance1, new object[1]
          {
            (object) assetBalance
          }, new object[2]
          {
            (object) assetBalance.CurrDeprPeriod,
            (object) finPeriodID
          }))
          {
            FABookHistory faBookHistory = PXResult<FABookHistory, FixedAsset, PX.Objects.GL.Branch, FABook, FABookPeriod, FADetails>.op_Implicit(pxResult2);
            FABookPeriod faBookPeriod = PXResult<FABookHistory, FixedAsset, PX.Objects.GL.Branch, FABook, FABookPeriod, FADetails>.op_Implicit(pxResult2);
            FixedAsset fixedAsset2 = PXResult<FABookHistory, FixedAsset, PX.Objects.GL.Branch, FABook, FABookPeriod, FADetails>.op_Implicit(pxResult2);
            FADetails faDetails2 = PXResult<FABookHistory, FixedAsset, PX.Objects.GL.Branch, FABook, FABookPeriod, FADetails>.op_Implicit(pxResult2);
            FABook faBook = PXResult<FABookHistory, FixedAsset, PX.Objects.GL.Branch, FABook, FABookPeriod, FADetails>.op_Implicit(pxResult2);
            int? nullable1 = faBookPeriod.BookID;
            if (nullable1.HasValue)
            {
              TransactionEntry graph = instance1;
              nullable1 = fixedAsset2.BranchID;
              int BranchID = nullable1.Value;
              string finPeriodId = faBookPeriod.FinPeriodID;
              DateTime? DocDate = new DateTime?();
              DocumentList<FARegister> created = instance1.created;
              TransactionEntry.SegregateRegister((PXGraph) graph, BranchID, "D", finPeriodId, DocDate, (string) null, created);
              ((PXSelectBase<FARegister>) instance1.Document).Current.DocDesc = PXMessages.LocalizeNoPrefix("Fixed Asset Depreciation");
              PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.StoreResult((PXGraph) instance1, (IBqlTable) fixedAsset2);
              PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>.Config>.StoreResult((PXGraph) instance1, (IBqlTable) fixedAsset2);
              PXSelectorAttribute.StoreCached<FABookBalance.assetID>(((PXSelectBase) instance1.bookbalances).Cache, (object) assetBalance, (object) fixedAsset2);
              ((PXSelectBase<FABookBalance>) instance1.bookbalances).StoreResult((IBqlTable) assetBalance);
              FATran faTran1 = new FATran();
              faTran1.AssetID = faBookHistory.AssetID;
              faTran1.BookID = faBookHistory.BookID;
              DateTime? endDate = faBookPeriod.EndDate;
              DateTime dateTime = endDate.Value;
              faTran1.TranDate = new DateTime?(dateTime.AddDays(-1.0));
              faTran1.FinPeriodID = faBookPeriod.FinPeriodID;
              Decimal? nullable2;
              Decimal? nullable3;
              if (!(faBookHistory.FinPeriodID == assetBalance.CurrDeprPeriod))
              {
                Decimal? ptdCalculated = faBookHistory.PtdCalculated;
                Decimal? ptdAdjusted = faBookHistory.PtdAdjusted;
                Decimal? nullable4 = ptdCalculated.HasValue & ptdAdjusted.HasValue ? new Decimal?(ptdCalculated.GetValueOrDefault() + ptdAdjusted.GetValueOrDefault()) : new Decimal?();
                nullable2 = faBookHistory.PtdDeprDisposed;
                nullable3 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
              }
              else
              {
                nullable2 = faBookHistory.YtdCalculated;
                Decimal? ytdDepreciated = faBookHistory.YtdDepreciated;
                nullable3 = nullable2.HasValue & ytdDepreciated.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - ytdDepreciated.GetValueOrDefault()) : new Decimal?();
              }
              faTran1.TranAmt = nullable3;
              faTran1.TranType = "C+";
              FATran faTran2 = faTran1;
              faTran2.TranDesc = PXMessages.LocalizeFormatNoPrefix("Depreciation for Asset {0}", new object[1]
              {
                (object) fixedAsset2.AssetCD
              });
              ((PXSelectBase<FixedAsset>) instance1.Asset).Current = fixedAsset2;
              ((PXSelectBase<FADetails>) instance1.assetdetails).Current = faDetails2;
              PXSelectorAttribute.StoreCached<FATran.assetID>(((PXSelectBase) instance1.Trans).Cache, (object) faTran2, (object) fixedAsset2);
              PXSelectorAttribute.StoreCached<FATran.bookID>(((PXSelectBase) instance1.Trans).Cache, (object) faTran2, (object) faBook);
              PXSelectorAttribute.StoreCached<FATran.finPeriodID>(((PXSelectBase) instance1.Trans).Cache, (object) faTran2, (object) faBookPeriod);
              FATran faTran3 = ((PXSelectBase<FATran>) instance1.Trans).Insert(faTran2);
              Decimal? nullable5;
              if (faBookHistory.FinPeriodID == assetBalance.DeprFromPeriod)
              {
                nullable2 = faBookHistory.YtdTax179Calculated;
                Decimal? ytdTax179 = faBookHistory.YtdTax179;
                nullable5 = nullable2.HasValue & ytdTax179.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - ytdTax179.GetValueOrDefault()) : new Decimal?();
                Decimal num = 0M;
                if (nullable5.GetValueOrDefault() > num & nullable5.HasValue)
                {
                  FATran faTran4 = faTran3;
                  nullable5 = faTran4.TranAmt;
                  nullable2 = faBookHistory.YtdTax179Calculated;
                  Decimal? nullable6 = faBookHistory.YtdTax179;
                  Decimal? nullable7 = nullable2.HasValue & nullable6.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
                  Decimal? nullable8;
                  if (!(nullable5.HasValue & nullable7.HasValue))
                  {
                    nullable6 = new Decimal?();
                    nullable8 = nullable6;
                  }
                  else
                    nullable8 = new Decimal?(nullable5.GetValueOrDefault() - nullable7.GetValueOrDefault());
                  faTran4.TranAmt = nullable8;
                  FATran faTran5 = new FATran();
                  faTran5.AssetID = faBookHistory.AssetID;
                  faTran5.BookID = faBookHistory.BookID;
                  endDate = faBookPeriod.EndDate;
                  dateTime = endDate.Value;
                  faTran5.TranDate = new DateTime?(dateTime.AddDays(-1.0));
                  faTran5.FinPeriodID = faBookPeriod.FinPeriodID;
                  faTran5.TranAmt = assetBalance.Tax179Amount;
                  faTran5.TranType = "C+";
                  faTran5.MethodDesc = "TAX179";
                  FATran faTran6 = faTran5;
                  faTran6.TranDesc = PXMessages.LocalizeFormatNoPrefix("Depreciation for Asset {0}", new object[1]
                  {
                    (object) fixedAsset2.AssetCD
                  });
                  ((PXSelectBase<FixedAsset>) instance1.Asset).Current = fixedAsset2;
                  ((PXSelectBase<FADetails>) instance1.assetdetails).Current = faDetails2;
                  PXSelectorAttribute.StoreCached<FATran.assetID>(((PXSelectBase) instance1.Trans).Cache, (object) faTran6, (object) fixedAsset2);
                  PXSelectorAttribute.StoreCached<FATran.bookID>(((PXSelectBase) instance1.Trans).Cache, (object) faTran6, (object) faBook);
                  PXSelectorAttribute.StoreCached<FATran.finPeriodID>(((PXSelectBase) instance1.Trans).Cache, (object) faTran6, (object) faBookPeriod);
                  ((PXSelectBase<FATran>) instance1.Trans).Insert(faTran6);
                }
              }
              if (faBookHistory.FinPeriodID == assetBalance.DeprFromPeriod)
              {
                nullable5 = faBookHistory.YtdBonusCalculated;
                Decimal? ytdBonus = faBookHistory.YtdBonus;
                Decimal? nullable9;
                if (!(nullable5.HasValue & ytdBonus.HasValue))
                {
                  nullable2 = new Decimal?();
                  nullable9 = nullable2;
                }
                else
                  nullable9 = new Decimal?(nullable5.GetValueOrDefault() - ytdBonus.GetValueOrDefault());
                Decimal? nullable10 = nullable9;
                Decimal num = 0M;
                if (nullable10.GetValueOrDefault() > num & nullable10.HasValue)
                {
                  FATran faTran7 = faTran3;
                  nullable10 = faTran7.TranAmt;
                  nullable5 = faBookHistory.YtdBonusCalculated;
                  nullable2 = faBookHistory.YtdBonus;
                  Decimal? nullable11 = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
                  Decimal? nullable12;
                  if (!(nullable10.HasValue & nullable11.HasValue))
                  {
                    nullable2 = new Decimal?();
                    nullable12 = nullable2;
                  }
                  else
                    nullable12 = new Decimal?(nullable10.GetValueOrDefault() - nullable11.GetValueOrDefault());
                  faTran7.TranAmt = nullable12;
                  FATran faTran8 = new FATran();
                  faTran8.AssetID = faBookHistory.AssetID;
                  faTran8.BookID = faBookHistory.BookID;
                  endDate = faBookPeriod.EndDate;
                  dateTime = endDate.Value;
                  faTran8.TranDate = new DateTime?(dateTime.AddDays(-1.0));
                  faTran8.FinPeriodID = faBookPeriod.FinPeriodID;
                  faTran8.TranAmt = assetBalance.BonusAmount;
                  faTran8.TranType = "C+";
                  faTran8.MethodDesc = "BONUS";
                  FATran faTran9 = faTran8;
                  faTran9.TranDesc = PXMessages.LocalizeFormatNoPrefix("Depreciation for Asset {0}", new object[1]
                  {
                    (object) fixedAsset2.AssetCD
                  });
                  ((PXSelectBase<FixedAsset>) instance1.Asset).Current = fixedAsset2;
                  ((PXSelectBase<FADetails>) instance1.assetdetails).Current = faDetails2;
                  PXSelectorAttribute.StoreCached<FATran.assetID>(((PXSelectBase) instance1.Trans).Cache, (object) faTran9, (object) fixedAsset2);
                  PXSelectorAttribute.StoreCached<FATran.bookID>(((PXSelectBase) instance1.Trans).Cache, (object) faTran9, (object) faBook);
                  PXSelectorAttribute.StoreCached<FATran.finPeriodID>(((PXSelectBase) instance1.Trans).Cache, (object) faTran9, (object) faBookPeriod);
                  ((PXSelectBase<FATran>) instance1.Trans).Insert(faTran9);
                }
              }
            }
          }
          if (((PXSelectBase) instance1.Trans).Cache.IsInsertedUpdatedDeleted)
          {
            ((PXGraph) instance1).Actions.PressSave();
            ((PXGraph) instance1).Clear();
          }
          transactionScope.Complete();
        }
        if (PXProcessing<FABookBalance>.GetItemMessage() == null)
          PXProcessing<FABookBalance>.SetProcessed();
      }
      catch (Exception ex)
      {
        PXProcessing<FABookBalance>.SetError(ex);
        flag = false;
      }
    }
    if (((PXSelectBase<FASetup>) instance1.fasetup).Current.AutoReleaseDepr.GetValueOrDefault() && instance1.created.Count > 0)
    {
      instance1.created.Sort((Comparison<FARegister>) ((a, b) => string.Compare(a.FinPeriodID, b.FinPeriodID, StringComparison.Ordinal)));
      AssetTranRelease.ReleaseDoc((List<FARegister>) instance1.created, IsMassProcess);
    }
    return flag;
  }

  public static void SetLastDeprPeriod(
    FixedAsset asset,
    PXSelectBase<FABookBalance> bookBalances,
    FABookBalance bookBal,
    string LastDeprPeriod)
  {
    if (LastDeprPeriod == null || !asset.Depreciable.GetValueOrDefault())
      return;
    LastDeprPeriod = bookBal.DeprToPeriod == null || string.CompareOrdinal(LastDeprPeriod, bookBal.DeprToPeriod) <= 0 ? LastDeprPeriod : bookBal.DeprToPeriod;
    bookBal = (FABookBalance) ((PXSelectBase) bookBalances).Cache.Locate((object) bookBal) ?? bookBal;
    if (string.CompareOrdinal(bookBal.LastDeprPeriod, LastDeprPeriod) >= 0)
      return;
    bookBal.LastDeprPeriod = LastDeprPeriod;
    bookBal.CurrDeprPeriod = bookBal.DeprToPeriod == null || string.CompareOrdinal(bookBal.LastDeprPeriod, bookBal.DeprToPeriod) < 0 ? ((PXSelectBase) bookBalances).Cache.Graph.GetService<IFABookPeriodUtils>().PeriodPlusPeriodsCount(bookBal.LastDeprPeriod, 1, bookBal.BookID, bookBal.AssetID) : (string) null;
    if (string.CompareOrdinal(bookBal.LastDeprPeriod, bookBal.DeprToPeriod) == 0 && bookBal.CurrDeprPeriod == null)
      bookBal.Status = "F";
    bookBalances.Update(bookBal);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.")]
  public static void SetLastDeprPeriod(
    PXSelectBase<FABookBalance> bookBalances,
    FABookBalance bookBal,
    string LastDeprPeriod)
  {
    PXGraph graph = ((PXSelectBase) bookBalances).Cache.Graph;
    if (((PXCache) GraphHelper.Caches<FixedAsset>(graph)).InternalCurrent is FixedAsset asset)
    {
      int? assetId1 = asset.AssetID;
      int? assetId2 = bookBal.AssetID;
      if (assetId1.GetValueOrDefault() == assetId2.GetValueOrDefault() & assetId1.HasValue == assetId2.HasValue)
        goto label_3;
    }
    asset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) bookBal.AssetID
    }));
label_3:
    PX.Objects.FA.AssetProcess.SetLastDeprPeriod(asset, bookBalances, bookBal, LastDeprPeriod);
  }

  public static string GetFixedAssetStatus(PXGraph graph, FADetails details)
  {
    FixedAsset fixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) details.AssetID
    }));
    if (fixedAsset == null)
      throw new PXException("{0} '{1}' cannot be found in the system.", new object[2]
      {
        (object) "AssetID",
        (object) details.AssetID
      });
    return !PX.Objects.FA.AssetProcess.IsFullyDepreciatedAsset(graph, details.AssetID) ? fixedAsset.Status : "F";
  }

  public static void AdjustFixedAssetStatus(PXGraph graph, int? assetID)
  {
    FixedAsset fixedAsset = (FixedAsset) graph.Caches[typeof (FixedAsset)].Current;
    if (fixedAsset != null)
    {
      int? assetId = fixedAsset.AssetID;
      int? nullable = assetID;
      if (assetId.GetValueOrDefault() == nullable.GetValueOrDefault() & assetId.HasValue == nullable.HasValue)
        goto label_3;
    }
    fixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select(graph, new object[1]
    {
      (object) assetID
    }));
label_3:
    if (fixedAsset == null)
      return;
    switch (fixedAsset.Status)
    {
      case "A":
        if (!PX.Objects.FA.AssetProcess.IsFullyDepreciatedAsset(graph, assetID))
          break;
        ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.FullyDepreciateAsset))).FireOn(graph, fixedAsset);
        break;
      case "F":
        if (PX.Objects.FA.AssetProcess.IsFullyDepreciatedAsset(graph, assetID))
          break;
        ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.ActivateAsset))).FireOn(graph, fixedAsset);
        break;
    }
  }

  public virtual void DataChecking(FARegister doc)
  {
    if (!((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && doc.Origin != "P")
      throw new PXException("Only Purchasing Register can be released in Initialization Mode.");
    foreach (PXResult<FATran, FABook, PX.Objects.GL.Branch, FABookPeriod> pxResult in PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlOperand<FATran.bookID, IBqlInt>.IsEqual<FABook.bookID>>>, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<FATran.branchID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.branchID>>>, FbqlJoins.Inner<FABookPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.finPeriodID, Equal<FABookPeriod.finPeriodID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.organizationID, Equal<FABookPeriod.organizationID>>>>>.And<BqlOperand<FABook.bookID, IBqlInt>.IsEqual<FABookPeriod.bookID>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.refNbr, Equal<P.AsString>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>.Aggregate<To<GroupBy<FABookPeriod.finPeriodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) doc.RefNbr
    }))
    {
      FABookPeriod faBookPeriod1 = PXResult<FATran, FABook, PX.Objects.GL.Branch, FABookPeriod>.op_Implicit(pxResult);
      PXResultset<FABookPeriod, FinPeriod> pxResultset = PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsEqual<FinPeriod.finPeriodID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.finPeriodID, Equal<P.AsString>>>>, And<BqlOperand<FABookPeriod.organizationID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FABookPeriod.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select<PXResultset<FABookPeriod, FinPeriod>>((PXGraph) this, new object[3]
      {
        (object) faBookPeriod1.FinPeriodID,
        (object) faBookPeriod1.OrganizationID,
        (object) faBookPeriod1.BookID
      });
      FABookPeriod faBookPeriod2 = PXResultset<FABookPeriod, FinPeriod>.op_Implicit(pxResultset);
      FinPeriod finPeriod = PXResultset<FABookPeriod, FinPeriod>.op_Implicit(pxResultset);
      PXException pxException = new PXException("The document cannot be released, because the {0} period in the posting book does not match the financial period in the general ledger for the {1} company. To amend the periods in the posting book based on the periods in the general ledger, on the Book Calendars (FA304000) form, click Synchronize FA Calendar with GL.", new object[2]
      {
        (object) PeriodIDAttribute.FormatForError(faBookPeriod1.FinPeriodID),
        (object) PXAccess.GetOrganizationCD(faBookPeriod1.OrganizationID)
      });
      if (faBookPeriod2 != null)
      {
        DateTime? nullable1;
        DateTime? nullable2;
        if (faBookPeriod2 != null && finPeriod != null && finPeriod.FinPeriodID != null)
        {
          nullable1 = faBookPeriod2.StartDate;
          nullable2 = finPeriod.StartDate;
          if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
          {
            nullable2 = faBookPeriod2.EndDate;
            nullable1 = finPeriod.EndDate;
            if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
              goto label_8;
          }
          else
            goto label_8;
        }
        if (finPeriod == null || finPeriod.FinPeriodID == null)
        {
          FinPeriod firstPeriod = this.FinPeriodRepository.FindFirstPeriod(faBookPeriod1.OrganizationID);
          if (firstPeriod != null && firstPeriod.FinPeriodID != null)
          {
            nullable1 = firstPeriod.StartDate;
            nullable2 = faBookPeriod2.StartDate;
            if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
              continue;
          }
          throw pxException;
        }
        continue;
      }
label_8:
      throw pxException;
    }
  }

  public virtual void ProcessAssetTran(
    JournalEntry je,
    FARegister doc,
    DocumentList<Batch> created)
  {
    if (doc == null)
      return;
    this.DataChecking(doc);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (PXResult<FATran, FixedAsset, FADetails, FABook, FABookBalance, FABookHist> pxResult1 in PXSelectBase<FATran, PXSelectJoin<FATran, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<FATran.assetID>>, InnerJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>, InnerJoin<FABook, On<FABook.bookID, Equal<FATran.bookID>>, InnerJoin<FABookBalance, On<FABookBalance.assetID, Equal<FATran.assetID>, And<FABookBalance.bookID, Equal<FATran.bookID>>>, LeftJoin<FABookHist, On<FABookHist.assetID, Equal<FATran.assetID>, And<FABookHist.bookID, Equal<FATran.bookID>, And<FABookHist.finPeriodID, Equal<FATran.finPeriodID>>>>>>>>>, Where<FATran.refNbr, Equal<Required<FARegister.refNbr>>, And<FATran.released, Equal<False>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) doc.RefNbr
      }))
      {
        FATran fatran = PXResult<FATran, FixedAsset, FADetails, FABook, FABookBalance, FABookHist>.op_Implicit(pxResult1);
        FixedAsset asset = PXResult<FATran, FixedAsset, FADetails, FABook, FABookBalance, FABookHist>.op_Implicit(pxResult1);
        FADetails details = PXResult<FATran, FixedAsset, FADetails, FABook, FABookBalance, FABookHist>.op_Implicit(pxResult1);
        FABook faBook = PXResult<FATran, FixedAsset, FADetails, FABook, FABookBalance, FABookHist>.op_Implicit(pxResult1);
        FABookBalance bookBalance = PXResult<FATran, FixedAsset, FADetails, FABook, FABookBalance, FABookHist>.op_Implicit(pxResult1);
        FABookHist faBookHist1 = PXResult<FATran, FixedAsset, FADetails, FABook, FABookBalance, FABookHist>.op_Implicit(pxResult1);
        PXCache<FATran>.StoreOriginal((PXGraph) this, fatran);
        PXCache<FixedAsset>.StoreOriginal((PXGraph) this, asset);
        PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.StoreResult((PXGraph) this, (IBqlTable) asset);
        if (asset.Status == "H")
          throw new PXException("Transactions cannot be posted for asset 'On Hold'.");
        if (asset.Status == "D" && fatran.Origin == "V")
          throw new PXException("Unable Reverse Disposed Fixed Asset '{0}'", new object[1]
          {
            (object) asset.AssetCD
          });
        if (asset.Status != "D" && fatran.Origin == "L")
          throw new PXException("The Status of Fixed Asset '{0}' is '{1}'. Unable Reverse Disposal.", new object[2]
          {
            (object) asset.AssetCD,
            (object) new FixedAssetStatus.ListAttribute().ValueLabelDic[asset.Status]
          });
        bool? nullable1 = faBookHist1.Suspended;
        if (nullable1.GetValueOrDefault() && fatran.TranType != "P+" && fatran.TranType != "P-" && fatran.TranType != "R+" && fatran.TranType != "R-" && fatran.Origin != "V")
          throw new PXException("Transactions cannot be posted to suspended period in Book '{0}'.", new object[1]
          {
            ((PXSelectBase) this.booktran).Cache.GetValueExt<FATran.bookID>((object) fatran)
          });
        FADepreciationMethod method = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) bookBalance.DepreciationMethodID
        }));
        if ((fatran.TranType == "C+" || fatran.TranType == "C-") && method == null)
          throw new PXException("Depreciation method does not exist.");
        FixedAsset cls = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.classID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) asset.ClassID
        }));
        if (doc.Origin == "L")
          ((PXGraph) this).CloseFABookHistory(bookBalance, fatran.FinPeriodID);
        int? parentOrganizationId = PXAccess.GetParentOrganizationID(fatran.BranchID);
        PXResultset<FABookPeriod> pxResultset = new PXResultset<FABookPeriod>();
        if (doc.Origin == "T" && !string.IsNullOrEmpty(bookBalance.CurrDeprPeriod))
          pxResultset.AddRange((IEnumerable<PXResult<FABookPeriod>>) PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FATran.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, GreaterEqual<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.finPeriodID, Less<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.endDate, Greater<FABookPeriod.startDate>>>>>>>.Config>.Select((PXGraph) this, new object[4]
          {
            (object) fatran.BookID,
            (object) parentOrganizationId,
            (object) bookBalance.CurrDeprPeriod,
            (object) fatran.FinPeriodID
          }));
        if (doc.Origin == "T" && string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && !string.IsNullOrEmpty(bookBalance.LastDeprPeriod))
          pxResultset.AddRange((IEnumerable<PXResult<FABookPeriod>>) PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, GreaterEqual<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.finPeriodID, Less<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.endDate, Greater<FABookPeriod.startDate>>>>>>>.Config>.Select((PXGraph) this, new object[4]
          {
            (object) fatran.BookID,
            (object) parentOrganizationId,
            (object) bookBalance.LastDeprPeriod,
            (object) fatran.FinPeriodID
          }));
        if (doc.Origin == "I" && fatran.TranType == "D+")
          pxResultset.AddRange((IEnumerable<PXResult<FABookPeriod>>) PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, GreaterEqual<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.finPeriodID, Less<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.endDate, Greater<FABookPeriod.startDate>>>>>>>.Config>.Select((PXGraph) this, new object[4]
          {
            (object) fatran.BookID,
            (object) parentOrganizationId,
            (object) bookBalance.DeprFromPeriod,
            (object) fatran.FinPeriodID
          }));
        if ((fatran.TranType == "PD" || doc.Origin == "I" && (fatran.TranType == "D+" || fatran.TranType == "D-")) && string.Compare(fatran.FinPeriodID, bookBalance.DeprToPeriod) > 0 && bookBalance.Status == "F")
          pxResultset.AddRange((IEnumerable<PXResult<FABookPeriod>>) PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, Greater<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.finPeriodID, LessEqual<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.endDate, Greater<FABookPeriod.startDate>>>>>>>.Config>.Select((PXGraph) this, new object[4]
          {
            (object) fatran.BookID,
            (object) parentOrganizationId,
            (object) bookBalance.DeprToPeriod,
            (object) fatran.FinPeriodID
          }));
        if (fatran.TranType == "PR")
        {
          FATran faTran = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Current<FATran.assetID>>, And<FATran.bookID, Equal<Current<FATran.bookID>>, And<FATran.tranType, Equal<FATran.tranType.purchasingDisposal>, And<FATran.finPeriodID, LessEqual<Current<FATran.finPeriodID>>>>>>, OrderBy<Desc<FATran.finPeriodID>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
          {
            (object) fatran
          }, Array.Empty<object>()));
          pxResultset.AddRange((IEnumerable<PXResult<FABookPeriod>>) PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, Greater<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.finPeriodID, Less<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.endDate, Greater<FABookPeriod.startDate>>>>>>>.Config>.Select((PXGraph) this, new object[4]
          {
            (object) fatran.BookID,
            (object) parentOrganizationId,
            faTran != null ? (object) faTran.FinPeriodID : (object) fatran.FinPeriodID,
            (object) fatran.FinPeriodID
          }));
        }
        if ((fatran.TranType == "C+" || fatran.TranType == "C-") && bookBalance.Status == "F" && method.IsPureStraightLine)
          pxResultset.AddRange((IEnumerable<PXResult<FABookPeriod>>) PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, Greater<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.finPeriodID, Less<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.endDate, Greater<FABookPeriod.startDate>>>>>>>.Config>.Select((PXGraph) this, new object[4]
          {
            (object) fatran.BookID,
            (object) parentOrganizationId,
            (object) bookBalance.DeprToPeriod,
            (object) fatran.FinPeriodID
          }));
        if (pxResultset != null)
        {
          string LastDeprPeriod = ((PXGraph) this).CloseFABookHistory(bookBalance, GraphHelper.RowCast<FABookPeriod>((IEnumerable) pxResultset).Select<FABookPeriod, string>((Func<FABookPeriod, string>) (p => p.FinPeriodID)));
          PX.Objects.FA.AssetProcess.SetLastDeprPeriod(asset, (PXSelectBase<FABookBalance>) this.bookbalance, bookBalance, LastDeprPeriod);
        }
        if (doc.Origin == "V")
        {
          nullable1 = faBook.UpdateGL;
          if (nullable1.GetValueOrDefault())
          {
            OrganizationFinPeriod periodInSubledger = this.FinPeriodUtils.GetNearestOpenOrganizationFinPeriodInSubledger<OrganizationFinPeriod.fAClosed>(fatran.FinPeriodID, fatran.BranchID);
            if (string.CompareOrdinal(periodInSubledger.FinPeriodID, fatran.FinPeriodID) > 0)
              fatran.FinPeriodID = periodInSubledger.FinPeriodID;
          }
        }
        else if (fatran.TranType == "P+" || fatran.TranType == "P-" || fatran.TranType == "R+" || fatran.TranType == "R-")
        {
          nullable1 = faBook.UpdateGL;
          if (nullable1.GetValueOrDefault())
          {
            OrganizationFinPeriod periodInSubledger = this.FinPeriodUtils.GetNearestOpenOrganizationFinPeriodInSubledger<OrganizationFinPeriod.fAClosed>(fatran.FinPeriodID, fatran.BranchID);
            fatran.FinPeriodID = periodInSubledger.FinPeriodID;
          }
        }
        if (fatran.TranType == "R+" || fatran.TranType == "R-")
        {
          if (string.CompareOrdinal(fatran.TranPeriodID, bookBalance.DeprFromPeriod) < 0)
            fatran.TranPeriodID = bookBalance.DeprFromPeriod;
          if (!string.IsNullOrEmpty(bookBalance.DeprToPeriod) && string.CompareOrdinal(fatran.TranPeriodID, bookBalance.DeprToPeriod) > 0)
            fatran.TranPeriodID = bookBalance.DeprToPeriod;
        }
        if (fatran.TranType == "P+" || fatran.TranType == "P-")
        {
          if (doc.Origin != "I" && this.UseAcceleratedDepreciation(cls, method))
          {
            if (string.CompareOrdinal(fatran.TranPeriodID, bookBalance.CurrDeprPeriod) < 0)
            {
              fatran.TranPeriodID = bookBalance.CurrDeprPeriod;
            }
            else
            {
              nullable1 = faBook.UpdateGL;
              if (nullable1.GetValueOrDefault())
              {
                OrganizationFinPeriod periodInSubledger = this.FinPeriodUtils.GetNearestOpenOrganizationFinPeriodInSubledger<OrganizationFinPeriod.fAClosed>(fatran.TranPeriodID, fatran.BranchID);
                if (string.CompareOrdinal(fatran.TranPeriodID, periodInSubledger.FinPeriodID) < 0)
                  fatran.TranPeriodID = periodInSubledger.FinPeriodID;
              }
            }
          }
          if (string.CompareOrdinal(fatran.TranPeriodID, bookBalance.DeprFromPeriod) < 0)
            fatran.TranPeriodID = bookBalance.DeprFromPeriod;
          string str = (string) null;
          foreach (PXResult<FABookPeriod, OrganizationFinPeriod> pxResult2 in PXSelectBase<FABookPeriod, PXSelectJoin<FABookPeriod, LeftJoin<OrganizationFinPeriod, On<FABookPeriod.finPeriodID, Equal<OrganizationFinPeriod.finPeriodID>, And<FABookPeriod.organizationID, Equal<OrganizationFinPeriod.organizationID>>>>, Where<FABookPeriod.bookID, Equal<Required<FATran.bookID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>, And<FABookPeriod.finPeriodID, GreaterEqual<Required<FABookBalance.deprFromPeriod>>, And<FABookPeriod.finPeriodID, LessEqual<Required<FATran.finPeriodID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>>>>>>>.Config>.Select((PXGraph) this, new object[4]
          {
            (object) fatran.BookID,
            (object) PX.Objects.FA.AssetProcess.GetDeprFromPeriod((PXGraph) je, bookBalance, asset, details),
            (object) fatran.FinPeriodID,
            (object) PXAccess.GetParentOrganizationID(fatran.BranchID)
          }))
          {
            FABookPeriod faBookPeriod = PXResult<FABookPeriod, OrganizationFinPeriod>.op_Implicit(pxResult2);
            OrganizationFinPeriod organizationFinPeriod = PXResult<FABookPeriod, OrganizationFinPeriod>.op_Implicit(pxResult2);
            FABookHist keyedHistory = new FABookHist();
            keyedHistory.AssetID = fatran.AssetID;
            keyedHistory.BookID = fatran.BookID;
            keyedHistory.FinPeriodID = faBookPeriod.FinPeriodID;
            FABookHist faBookHist2 = FAHelper.InsertFABookHist((PXGraph) this, keyedHistory, ref bookBalance);
            FABookHist faBookHist3 = faBookHist2;
            nullable1 = organizationFinPeriod.FAClosed;
            bool? nullable2 = new bool?(nullable1.GetValueOrDefault());
            faBookHist3.Closed = nullable2;
            if (str == null || string.CompareOrdinal(faBookPeriod.FinPeriodID, str) > 0)
            {
              nullable1 = faBookHist2.Closed;
              if (nullable1.GetValueOrDefault())
                str = faBookPeriod.FinPeriodID;
            }
          }
          PX.Objects.FA.AssetProcess.SetLastDeprPeriod(asset, (PXSelectBase<FABookBalance>) this.bookbalance, bookBalance, str);
        }
        switch (fatran.TranType)
        {
          case "A+":
          case "C+":
          case "D+":
            if (!fatran.CreditAccountID.HasValue)
            {
              fatran.CreditAccountID = asset.AccumulatedDepreciationAccountID;
              fatran.CreditSubID = asset.AccumulatedDepreciationSubID;
            }
            if (!fatran.DebitAccountID.HasValue)
            {
              fatran.DebitAccountID = asset.DepreciatedExpenseAccountID;
              fatran.DebitSubID = asset.DepreciatedExpenseSubID;
              break;
            }
            break;
          case "A-":
          case "C-":
          case "D-":
            if (!fatran.DebitAccountID.HasValue)
            {
              fatran.DebitAccountID = asset.AccumulatedDepreciationAccountID;
              fatran.DebitSubID = asset.AccumulatedDepreciationSubID;
            }
            if (!fatran.CreditAccountID.HasValue)
            {
              fatran.CreditAccountID = asset.DepreciatedExpenseAccountID;
              fatran.CreditSubID = asset.DepreciatedExpenseSubID;
              break;
            }
            break;
          case "P+":
            if (!fatran.IsOriginReversal || !fatran.DebitAccountID.HasValue)
            {
              fatran.DebitAccountID = asset.FAAccountID;
              fatran.DebitSubID = asset.FASubID;
            }
            if (!fatran.IsOriginReversal || !fatran.CreditAccountID.HasValue)
            {
              fatran.CreditAccountID = asset.FAAccrualAcctID;
              fatran.CreditSubID = asset.FAAccrualSubID;
              break;
            }
            break;
          case "P-":
            if (!fatran.IsOriginReversal || !fatran.CreditAccountID.HasValue)
            {
              fatran.CreditAccountID = asset.FAAccountID;
              fatran.CreditSubID = asset.FASubID;
            }
            if (!fatran.IsOriginReversal || !fatran.DebitAccountID.HasValue)
            {
              fatran.DebitAccountID = asset.FAAccrualAcctID;
              fatran.DebitSubID = asset.FAAccrualSubID;
              break;
            }
            break;
          case "PD":
            if (!fatran.CreditAccountID.HasValue)
            {
              fatran.CreditAccountID = asset.FAAccountID;
              fatran.CreditSubID = asset.FASubID;
            }
            if (!fatran.DebitAccountID.HasValue)
              throw new PXException("'{0}' cannot be empty.", new object[1]
              {
                (object) typeof (FATran.creditAccountID).Name
              });
            break;
          case "PR":
            if (!fatran.DebitAccountID.HasValue)
            {
              fatran.DebitAccountID = asset.FAAccountID;
              fatran.DebitSubID = asset.FASubID;
            }
            if (!fatran.CreditAccountID.HasValue)
            {
              fatran.CreditAccountID = asset.FAAccrualAcctID;
              fatran.CreditSubID = asset.FAAccrualSubID;
              break;
            }
            break;
          case "R+":
            if (!fatran.IsOriginReversal || !fatran.DebitAccountID.HasValue)
            {
              fatran.DebitAccountID = asset.FAAccrualAcctID;
              fatran.DebitSubID = asset.FAAccrualSubID;
            }
            if (!fatran.CreditAccountID.HasValue)
            {
              fatran.CreditAccountID = asset.FAAccrualAcctID;
              fatran.CreditSubID = asset.FAAccrualSubID;
              break;
            }
            break;
          case "R-":
            if (!fatran.IsOriginReversal || !fatran.CreditAccountID.HasValue)
            {
              fatran.CreditAccountID = asset.FAAccrualAcctID;
              fatran.CreditSubID = asset.FAAccrualSubID;
            }
            if (!fatran.DebitAccountID.HasValue)
            {
              fatran.DebitAccountID = asset.FAAccrualAcctID;
              fatran.DebitSubID = asset.FAAccrualSubID;
              break;
            }
            break;
          case "S+":
            if (!fatran.DebitAccountID.HasValue)
              throw new PXException("'{0}' cannot be empty.", new object[1]
              {
                (object) typeof (FATran.debitAccountID).Name
              });
            if (!fatran.CreditAccountID.HasValue)
              throw new PXException("'{0}' cannot be empty.", new object[1]
              {
                (object) typeof (FATran.creditAccountID).Name
              });
            break;
          case "S-":
            if (!fatran.DebitAccountID.HasValue)
              throw new PXException("'{0}' cannot be empty.", new object[1]
              {
                (object) typeof (FATran.debitAccountID).Name
              });
            if (!fatran.CreditAccountID.HasValue)
              throw new PXException("'{0}' cannot be empty.", new object[1]
              {
                (object) typeof (FATran.creditAccountID).Name
              });
            break;
          case "TD":
            if (!fatran.IsOriginReversal)
              fatran.TranAmt = bookBalance.YtdDepreciated;
            asset = (FixedAsset) ((PXSelectBase) this.fixedasset).Cache.Locate((object) asset) ?? asset;
            asset.AccumulatedDepreciationAccountID = fatran.CreditAccountID;
            asset.AccumulatedDepreciationSubID = fatran.CreditSubID;
            ((PXSelectBase<FixedAsset>) this.fixedasset).Update(asset);
            break;
          case "TP":
            if (!fatran.IsOriginReversal)
              fatran.TranAmt = bookBalance.YtdAcquired ?? faBookHist1.YtdAcquired;
            asset = (FixedAsset) ((PXSelectBase) this.fixedasset).Cache.Locate((object) asset) ?? asset;
            asset.FAAccountID = fatran.DebitAccountID;
            asset.FASubID = fatran.DebitSubID;
            asset.BranchID = fatran.BranchID;
            ((PXSelectBase<FixedAsset>) this.fixedasset).Update(asset);
            break;
        }
        FABookHist keyedHistory1 = new FABookHist();
        keyedHistory1.AssetID = fatran.AssetID;
        keyedHistory1.BookID = fatran.BookID;
        keyedHistory1.FinPeriodID = fatran.FinPeriodID;
        FABookHist faBookHist4 = FAHelper.InsertFABookHist((PXGraph) this, keyedHistory1, ref bookBalance);
        Decimal? nullable3;
        Decimal? nullable4;
        switch (fatran.TranType)
        {
          case "A+":
            if (fatran.Origin == "L")
            {
              FABookHist faBookHist5 = faBookHist4;
              nullable4 = faBookHist5.PtdDeprDisposed;
              nullable3 = fatran.TranAmt;
              faBookHist5.PtdDeprDisposed = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            }
            else
            {
              FABookHist faBookHist6 = faBookHist4;
              nullable3 = faBookHist6.PtdAdjusted;
              nullable4 = fatran.TranAmt;
              faBookHist6.PtdAdjusted = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            }
            FABookHist faBookHist7 = faBookHist4;
            nullable4 = faBookHist7.YtdDepreciated;
            nullable3 = fatran.TranAmt;
            faBookHist7.YtdDepreciated = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist8 = faBookHist4;
            nullable3 = faBookHist8.YtdBal;
            nullable4 = fatran.TranAmt;
            faBookHist8.YtdBal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            break;
          case "A-":
            if (fatran.Origin == "S")
            {
              FABookHist faBookHist9 = faBookHist4;
              nullable4 = faBookHist9.PtdDeprDisposed;
              nullable3 = fatran.TranAmt;
              faBookHist9.PtdDeprDisposed = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            }
            else
            {
              FABookHist faBookHist10 = faBookHist4;
              nullable3 = faBookHist10.PtdAdjusted;
              nullable4 = fatran.TranAmt;
              faBookHist10.PtdAdjusted = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            }
            FABookHist faBookHist11 = faBookHist4;
            nullable4 = faBookHist11.YtdDepreciated;
            nullable3 = fatran.TranAmt;
            faBookHist11.YtdDepreciated = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist12 = faBookHist4;
            nullable3 = faBookHist12.YtdBal;
            nullable4 = fatran.TranAmt;
            faBookHist12.YtdBal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            break;
          case "C+":
            if (string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && string.IsNullOrEmpty(bookBalance.LastDeprPeriod) || !string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && !string.Equals(bookBalance.CurrDeprPeriod, faBookHist4.FinPeriodID) && !PX.Objects.FA.AssetProcess.IsDepreciatedInCurrentDeprPeriodNotByDisposal((PXGraph) this, asset.AssetID, bookBalance.BookID, bookBalance.CurrDeprPeriod))
              throw new PXException("Calculated depreciation of the Fixed Asset '{0}' (Book '{1}') tries to post into the period {2}. It can be posted only to current period {3}.", new object[4]
              {
                (object) asset.AssetCD,
                (object) faBook.BookCode,
                (object) PeriodIDAttribute.FormatForError(faBookHist4.FinPeriodID),
                (object) PeriodIDAttribute.FormatForError(bookBalance.CurrDeprPeriod)
              });
            fatran.TranType = "D+";
            switch (fatran.MethodDesc)
            {
              case "TAX179":
                FABookHist faBookHist13 = faBookHist4;
                nullable3 = faBookHist13.PtdTax179;
                nullable4 = fatran.TranAmt;
                faBookHist13.PtdTax179 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist14 = faBookHist4;
                nullable4 = faBookHist14.YtdTax179;
                nullable3 = fatran.TranAmt;
                faBookHist14.YtdTax179 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                break;
              case "BONUS":
                FABookHist faBookHist15 = faBookHist4;
                nullable3 = faBookHist15.PtdBonus;
                nullable4 = fatran.TranAmt;
                faBookHist15.PtdBonus = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist16 = faBookHist4;
                nullable4 = faBookHist16.YtdBonus;
                nullable3 = fatran.TranAmt;
                faBookHist16.YtdBonus = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                break;
            }
            FABookHist faBookHist17 = faBookHist4;
            nullable3 = faBookHist17.PtdDepreciated;
            nullable4 = fatran.TranAmt;
            faBookHist17.PtdDepreciated = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist18 = faBookHist4;
            nullable4 = faBookHist18.YtdDepreciated;
            nullable3 = fatran.TranAmt;
            faBookHist18.YtdDepreciated = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist19 = faBookHist4;
            nullable3 = faBookHist19.YtdBal;
            nullable4 = fatran.TranAmt;
            faBookHist19.YtdBal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            faBookHist4.Closed = new bool?(true);
            if (bookBalance.Status == "F" && method.IsPureStraightLine)
            {
              FABookHist faBookHist20 = faBookHist4;
              nullable4 = faBookHist20.PtdCalculated;
              nullable3 = fatran.TranAmt;
              faBookHist20.PtdCalculated = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
              FABookHist faBookHist21 = faBookHist4;
              nullable3 = faBookHist21.YtdCalculated;
              nullable4 = fatran.TranAmt;
              faBookHist21.YtdCalculated = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
              break;
            }
            break;
          case "C-":
            if (string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && string.IsNullOrEmpty(bookBalance.LastDeprPeriod) || !string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && !string.Equals(bookBalance.CurrDeprPeriod, faBookHist4.FinPeriodID))
              throw new PXException("Calculated depreciation of the Fixed Asset '{0}' (Book '{1}') tries to post into the period {2}. It can be posted only to current period {3}.", new object[4]
              {
                (object) asset.AssetCD,
                (object) faBook.BookCode,
                (object) PeriodIDAttribute.FormatForError(faBookHist4.FinPeriodID),
                (object) PeriodIDAttribute.FormatForError(bookBalance.CurrDeprPeriod)
              });
            fatran.TranType = "D-";
            switch (fatran.MethodDesc)
            {
              case "TAX179":
                FABookHist faBookHist22 = faBookHist4;
                nullable3 = faBookHist22.PtdTax179;
                nullable4 = fatran.TranAmt;
                faBookHist22.PtdTax179 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist23 = faBookHist4;
                nullable4 = faBookHist23.YtdTax179;
                nullable3 = fatran.TranAmt;
                faBookHist23.YtdTax179 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
                break;
              case "BONUS":
                FABookHist faBookHist24 = faBookHist4;
                nullable3 = faBookHist24.PtdBonus;
                nullable4 = fatran.TranAmt;
                faBookHist24.PtdBonus = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist25 = faBookHist4;
                nullable4 = faBookHist25.YtdBonus;
                nullable3 = fatran.TranAmt;
                faBookHist25.YtdBonus = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
                break;
            }
            FABookHist faBookHist26 = faBookHist4;
            nullable3 = faBookHist26.PtdDepreciated;
            nullable4 = fatran.TranAmt;
            faBookHist26.PtdDepreciated = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist27 = faBookHist4;
            nullable4 = faBookHist27.YtdDepreciated;
            nullable3 = fatran.TranAmt;
            faBookHist27.YtdDepreciated = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist28 = faBookHist4;
            nullable3 = faBookHist28.YtdBal;
            nullable4 = fatran.TranAmt;
            faBookHist28.YtdBal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            faBookHist4.Closed = new bool?(true);
            if (bookBalance.Status == "F" && method.IsPureStraightLine)
            {
              FABookHist faBookHist29 = faBookHist4;
              nullable4 = faBookHist29.PtdCalculated;
              nullable3 = fatran.TranAmt;
              faBookHist29.PtdCalculated = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
              FABookHist faBookHist30 = faBookHist4;
              nullable3 = faBookHist30.YtdCalculated;
              nullable4 = fatran.TranAmt;
              faBookHist30.YtdCalculated = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
              break;
            }
            break;
          case "D+":
            if (string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && string.IsNullOrEmpty(bookBalance.LastDeprPeriod) || !string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && string.CompareOrdinal(bookBalance.CurrDeprPeriod, faBookHist4.FinPeriodID) <= 0 && !fatran.IsOriginReversal)
              throw new PXException("Depreciation adjustments (D+/D-) can be posted only to closed periods.");
            switch (fatran.MethodDesc)
            {
              case "TAX179":
                FABookHist faBookHist31 = faBookHist4;
                nullable4 = faBookHist31.PtdTax179Recap;
                nullable3 = fatran.TranAmt;
                faBookHist31.PtdTax179Recap = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist32 = faBookHist4;
                nullable3 = faBookHist32.YtdTax179Recap;
                nullable4 = fatran.TranAmt;
                faBookHist32.YtdTax179Recap = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
                break;
              case "BONUS":
                FABookHist faBookHist33 = faBookHist4;
                nullable4 = faBookHist33.PtdBonusRecap;
                nullable3 = fatran.TranAmt;
                faBookHist33.PtdBonusRecap = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist34 = faBookHist4;
                nullable3 = faBookHist34.YtdBonusRecap;
                nullable4 = fatran.TranAmt;
                faBookHist34.YtdBonusRecap = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
                break;
            }
            FABookHist faBookHist35 = faBookHist4;
            nullable4 = faBookHist35.PtdDepreciated;
            nullable3 = fatran.TranAmt;
            faBookHist35.PtdDepreciated = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist36 = faBookHist4;
            nullable3 = faBookHist36.YtdDepreciated;
            nullable4 = fatran.TranAmt;
            faBookHist36.YtdDepreciated = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist37 = faBookHist4;
            nullable4 = faBookHist37.YtdBal;
            nullable3 = fatran.TranAmt;
            faBookHist37.YtdBal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            break;
          case "D-":
            if (string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && string.IsNullOrEmpty(bookBalance.LastDeprPeriod) || !string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && string.CompareOrdinal(bookBalance.CurrDeprPeriod, faBookHist4.FinPeriodID) <= 0 && !fatran.IsOriginReversal)
              throw new PXException("Depreciation adjustments (D+/D-) can be posted only to closed periods.");
            if (fatran.Origin == "L")
            {
              FABookHistory faBookHistory = PXResultset<FABookHistory>.op_Implicit(PXSelectBase<FABookHistory, PXSelectReadonly<FABookHistory, Where<FABookHistory.assetID, Equal<Required<FABookHistory.assetID>>, And<FABookHistory.bookID, Equal<Required<FABookHistory.bookID>>, And<FABookHistory.finPeriodID, Equal<Required<FABookHistory.finPeriodID>>>>>>.Config>.Select((PXGraph) this, new object[3]
              {
                (object) faBookHist4.AssetID,
                (object) faBookHist4.BookID,
                (object) faBookHist4.FinPeriodID
              }));
              Decimal num1 = 0M;
              foreach (PXResult<FATran> pxResult3 in PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.bookID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FATran.origin, IBqlString>.IsEqual<FARegister.origin.disposalReversal>>>, And<BqlOperand<FATran.finPeriodID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, Equal<FATran.tranType.depreciationPlus>>>>>.Or<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.depreciationMinus>>>>>.Config>.Select((PXGraph) this, new object[3]
              {
                (object) faBookHist4.AssetID,
                (object) faBookHist4.BookID,
                (object) faBookHist4.FinPeriodID
              }))
              {
                FATran faTran = PXResult<FATran>.op_Implicit(pxResult3);
                if (faTran.TranType == "D-")
                  num1 += faTran.TranAmt.GetValueOrDefault();
                else if (faTran.TranType == "D+")
                  num1 -= faTran.TranAmt.GetValueOrDefault();
              }
              if (faBookHistory != null)
              {
                nullable4 = faBookHistory.PtdDepreciated;
                Decimal num2 = num1;
                if (nullable4.GetValueOrDefault() == num2 & nullable4.HasValue)
                {
                  faBookHist4.Reopen = new bool?(true);
                  faBookHist4.Closed = new bool?(false);
                }
              }
            }
            switch (fatran.MethodDesc)
            {
              case "TAX179":
                FABookHist faBookHist38 = faBookHist4;
                nullable4 = faBookHist38.PtdTax179Recap;
                nullable3 = fatran.TranAmt;
                faBookHist38.PtdTax179Recap = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist39 = faBookHist4;
                nullable3 = faBookHist39.YtdTax179Recap;
                nullable4 = fatran.TranAmt;
                faBookHist39.YtdTax179Recap = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                break;
              case "BONUS":
                FABookHist faBookHist40 = faBookHist4;
                nullable4 = faBookHist40.PtdBonusRecap;
                nullable3 = fatran.TranAmt;
                faBookHist40.PtdBonusRecap = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                FABookHist faBookHist41 = faBookHist4;
                nullable3 = faBookHist41.YtdBonusRecap;
                nullable4 = fatran.TranAmt;
                faBookHist41.YtdBonusRecap = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                break;
            }
            FABookHist faBookHist42 = faBookHist4;
            nullable4 = faBookHist42.PtdDepreciated;
            nullable3 = fatran.TranAmt;
            faBookHist42.PtdDepreciated = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist43 = faBookHist4;
            nullable3 = faBookHist43.YtdDepreciated;
            nullable4 = fatran.TranAmt;
            faBookHist43.YtdDepreciated = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist44 = faBookHist4;
            nullable4 = faBookHist44.YtdBal;
            nullable3 = fatran.TranAmt;
            faBookHist44.YtdBal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            break;
          case "P+":
            FABookHist faBookHist45 = faBookHist4;
            nullable3 = faBookHist45.PtdAcquired;
            nullable4 = fatran.TranAmt;
            faBookHist45.PtdAcquired = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist46 = faBookHist4;
            nullable4 = faBookHist46.YtdAcquired;
            nullable3 = fatran.TranAmt;
            faBookHist46.YtdAcquired = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist47 = faBookHist4;
            nullable3 = faBookHist47.YtdBal;
            nullable4 = fatran.TranAmt;
            faBookHist47.YtdBal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            break;
          case "P-":
            FABookHist faBookHist48 = faBookHist4;
            nullable4 = faBookHist48.PtdAcquired;
            nullable3 = fatran.TranAmt;
            faBookHist48.PtdAcquired = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist49 = faBookHist4;
            nullable3 = faBookHist49.YtdAcquired;
            nullable4 = fatran.TranAmt;
            faBookHist49.YtdAcquired = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist50 = faBookHist4;
            nullable4 = faBookHist50.YtdBal;
            nullable3 = fatran.TranAmt;
            faBookHist50.YtdBal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            break;
          case "PD":
            FABookHist faBookHist51 = faBookHist4;
            nullable4 = faBookHist51.YtdBal;
            nullable3 = fatran.TranAmt;
            faBookHist51.YtdBal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            break;
          case "PR":
            FABookHist faBookHist52 = faBookHist4;
            nullable3 = faBookHist52.YtdBal;
            nullable4 = fatran.TranAmt;
            faBookHist52.YtdBal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            break;
          case "R+":
          case "R-":
            if (PX.Objects.FA.AssetProcess.IsFullyDepreciatedAsset((PXGraph) this, asset.AssetID) && string.CompareOrdinal(fatran.FinPeriodID, bookBalance.DeprToPeriod) > 0)
            {
              faBookHist4.Closed = new bool?(true);
              break;
            }
            break;
          case "S+":
            FABookHist faBookHist53 = faBookHist4;
            nullable3 = faBookHist53.PtdDisposalAmount;
            nullable4 = fatran.TranAmt;
            faBookHist53.PtdDisposalAmount = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist54 = faBookHist4;
            nullable4 = faBookHist54.YtdDisposalAmount;
            nullable3 = fatran.TranAmt;
            faBookHist54.YtdDisposalAmount = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist55 = faBookHist4;
            nullable3 = faBookHist55.PtdRGOL;
            nullable4 = fatran.RGOLAmt;
            faBookHist55.PtdRGOL = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist56 = faBookHist4;
            nullable4 = faBookHist56.YtdRGOL;
            nullable3 = fatran.RGOLAmt;
            faBookHist56.YtdRGOL = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            break;
          case "S-":
            FABookHist faBookHist57 = faBookHist4;
            nullable3 = faBookHist57.PtdDisposalAmount;
            nullable4 = fatran.TranAmt;
            faBookHist57.PtdDisposalAmount = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist58 = faBookHist4;
            nullable4 = faBookHist58.YtdDisposalAmount;
            nullable3 = fatran.TranAmt;
            faBookHist58.YtdDisposalAmount = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist59 = faBookHist4;
            nullable3 = faBookHist59.PtdRGOL;
            nullable4 = fatran.RGOLAmt;
            faBookHist59.PtdRGOL = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist60 = faBookHist4;
            nullable4 = faBookHist60.YtdRGOL;
            nullable3 = fatran.RGOLAmt;
            faBookHist60.YtdRGOL = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            break;
          case "TD":
          case "TP":
            if (string.CompareOrdinal(fatran.FinPeriodID, bookBalance.DeprToPeriod) > 0)
            {
              ((PXSelectBase) this.bookhist).Cache.SetStatus((object) faBookHist4, (PXEntryStatus) 0);
              break;
            }
            break;
        }
        FABookHist keyedHistory2 = new FABookHist();
        keyedHistory2.AssetID = fatran.AssetID;
        keyedHistory2.BookID = fatran.BookID;
        keyedHistory2.FinPeriodID = fatran.TranPeriodID;
        FABookHist faBookHist61 = FAHelper.InsertFABookHist((PXGraph) this, keyedHistory2, ref bookBalance);
        switch (fatran.TranType)
        {
          case "P+":
            FABookHist faBookHist62 = faBookHist61;
            nullable3 = faBookHist62.PtdDeprBase;
            nullable4 = fatran.TranAmt;
            faBookHist62.PtdDeprBase = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist63 = faBookHist61;
            nullable4 = faBookHist63.YtdDeprBase;
            nullable3 = fatran.TranAmt;
            faBookHist63.YtdDeprBase = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            break;
          case "P-":
            FABookHist faBookHist64 = faBookHist61;
            nullable3 = faBookHist64.PtdDeprBase;
            nullable4 = fatran.TranAmt;
            faBookHist64.PtdDeprBase = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist65 = faBookHist61;
            nullable4 = faBookHist65.YtdDeprBase;
            nullable3 = fatran.TranAmt;
            faBookHist65.YtdDeprBase = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            break;
          case "R+":
            FABookHist faBookHist66 = faBookHist61;
            nullable3 = faBookHist66.PtdReconciled;
            nullable4 = fatran.TranAmt;
            faBookHist66.PtdReconciled = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist67 = faBookHist61;
            nullable4 = faBookHist67.YtdReconciled;
            nullable3 = fatran.TranAmt;
            faBookHist67.YtdReconciled = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            break;
          case "R-":
            FABookHist faBookHist68 = faBookHist61;
            nullable3 = faBookHist68.PtdReconciled;
            nullable4 = fatran.TranAmt;
            faBookHist68.PtdReconciled = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            FABookHist faBookHist69 = faBookHist61;
            nullable4 = faBookHist69.YtdReconciled;
            nullable3 = fatran.TranAmt;
            faBookHist69.YtdReconciled = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            break;
          case "TP":
          case "TD":
            if (!string.IsNullOrEmpty(bookBalance.DeprToPeriod) && string.CompareOrdinal(fatran.TranPeriodID, bookBalance.DeprToPeriod) > 0)
            {
              if (!string.IsNullOrEmpty(bookBalance.CurrDeprPeriod))
                throw new PXException("The active fixed asset {0} cannot be transferred in the {1} period after the last {2} period of the depreciation schedule in the {3} book.", new object[4]
                {
                  (object) asset.AssetCD,
                  (object) PeriodIDAttribute.FormatForError(fatran.TranPeriodID),
                  (object) PeriodIDAttribute.FormatForError(bookBalance.DeprToPeriod),
                  (object) faBook.BookCode
                });
              ((PXSelectBase) this.bookhist).Cache.SetStatus((object) faBookHist61, (PXEntryStatus) 0);
            }
            if (!fatran.IsOriginReversal)
            {
              if (!string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && !string.IsNullOrEmpty(bookBalance.LastDeprPeriod) && string.CompareOrdinal(bookBalance.CurrDeprPeriod, fatran.TranPeriodID) > 0)
                throw new PXException("The active fixed asset {0} cannot be transferred in the {1} period before the current {2} period in the {3} book.", new object[4]
                {
                  (object) asset.AssetCD,
                  (object) PeriodIDAttribute.FormatForError(fatran.TranPeriodID),
                  (object) PeriodIDAttribute.FormatForError(bookBalance.CurrDeprPeriod),
                  (object) faBook.BookCode
                });
              if (!string.IsNullOrEmpty(bookBalance.LastDeprPeriod) && string.CompareOrdinal(bookBalance.LastDeprPeriod, fatran.TranPeriodID) > 0)
                throw new PXException("Fully-depreciated asset cannot be transferred before Period {0}", new object[1]
                {
                  (object) PeriodIDAttribute.FormatForError(bookBalance.LastDeprPeriod)
                });
              break;
            }
            break;
        }
        PXSelectBase<FABookBalance> pxSelectBase = (PXSelectBase<FABookBalance>) new PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FABookBalance.assetID>>, And<FABookBalance.status, NotEqual<Required<FABookBalance.status>>>>>((PXGraph) this);
        PXCache<FABookBalance>.StoreOriginal((PXGraph) this, bookBalance);
        string origin = doc.Origin;
        int? nullable5;
        if (origin != null && origin.Length == 1)
        {
          switch (origin[0])
          {
            case 'D':
              bookBalance = (FABookBalance) ((PXSelectBase) this.bookbalance).Cache.Locate((object) bookBalance) ?? bookBalance;
              if (string.Equals(bookBalance.DeprToPeriod, fatran.FinPeriodID))
              {
                bookBalance.LastDeprPeriod = fatran.FinPeriodID;
                bookBalance.CurrDeprPeriod = (string) null;
                bookBalance.Status = "F";
                ((PXSelectBase<FABookBalance>) this.bookbalance).Update(bookBalance);
                if (PX.Objects.FA.AssetProcess.IsFullyDepreciatedAsset((PXGraph) this, bookBalance.AssetID))
                {
                  asset = ((PXSelectBase<FixedAsset>) this.fixedasset).Update(((PXSelectBase<FixedAsset>) this.fixedasset).Locate(asset) ?? asset);
                  ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.FullyDepreciateAsset))).FireOn((PXGraph) this, asset);
                  break;
                }
                break;
              }
              bookBalance.LastDeprPeriod = fatran.FinPeriodID;
              bookBalance.CurrDeprPeriod = this.FABookPeriodUtils.PeriodPlusPeriodsCount(bookBalance.LastDeprPeriod, 1, bookBalance.BookID, bookBalance.AssetID);
              ((PXSelectBase<FABookBalance>) this.bookbalance).Update(bookBalance);
              FABookHist keyedHistory3 = new FABookHist();
              keyedHistory3.AssetID = bookBalance.AssetID;
              keyedHistory3.BookID = bookBalance.BookID;
              keyedHistory3.FinPeriodID = bookBalance.CurrDeprPeriod;
              FAHelper.InsertFABookHist((PXGraph) this, keyedHistory3, ref bookBalance);
              break;
            case 'I':
              if ((fatran.TranType == "P+" || fatran.TranType == "P-") && string.CompareOrdinal(fatran.FinPeriodID, bookBalance.LastPeriod) > 0)
              {
                bookBalance.LastPeriod = fatran.FinPeriodID;
                ((PXSelectBase<FABookBalance>) this.bookbalance).Update(bookBalance);
                break;
              }
              break;
            case 'L':
              details.DisposalDate = new DateTime?();
              details.DisposalMethodID = new int?();
              FADetails faDetails = details;
              nullable3 = new Decimal?();
              Decimal? nullable6 = nullable3;
              faDetails.SaleAmount = nullable6;
              bookBalance = (FABookBalance) ((PXSelectBase) this.bookbalance).Cache.Locate((object) bookBalance) ?? bookBalance;
              bookBalance.DeprToDate = bookBalance.OrigDeprToDate;
              ((PXSelectBase) this.bookbalance).Cache.SetDefaultExt<FABookBalance.deprToPeriod>((object) bookBalance);
              FABookHistory faBookHistory1 = PXResultset<FABookHistory>.op_Implicit(PXSelectBase<FABookHistory, PXSelect<FABookHistory, Where<FABookHistory.assetID, Equal<Current<FABookBalance.assetID>>, And<FABookHistory.bookID, Equal<Current<FABookBalance.bookID>>, And<FABookHistory.ytdReversed, Greater<int0>>>>, OrderBy<Desc<FABookHistory.finPeriodID>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
              {
                (object) bookBalance
              }, Array.Empty<object>()));
              if (faBookHistory1 != null)
              {
                FABookBalance faBookBalance = bookBalance;
                string str;
                if (!bookBalance.Depreciate.GetValueOrDefault() || asset.UnderConstruction.GetValueOrDefault())
                {
                  str = (string) null;
                }
                else
                {
                  IFABookPeriodUtils faBookPeriodUtils = this.FABookPeriodUtils;
                  string deprToPeriod = bookBalance.DeprToPeriod;
                  nullable5 = faBookHistory1.YtdReversed;
                  int valueOrDefault = nullable5.GetValueOrDefault();
                  int? bookId = bookBalance.BookID;
                  int? assetId = bookBalance.AssetID;
                  str = faBookPeriodUtils.PeriodPlusPeriodsCount(deprToPeriod, valueOrDefault, bookId, assetId);
                }
                faBookBalance.DeprToPeriod = str;
              }
              bool flag1 = string.CompareOrdinal(bookBalance.LastDeprPeriod, bookBalance.DeprToPeriod) >= 0;
              bookBalance.Status = ((!bookBalance.Depreciate.GetValueOrDefault() ? 0 : (!asset.UnderConstruction.GetValueOrDefault() ? 1 : 0)) & (flag1 ? 1 : 0)) != 0 ? "F" : "A";
              string str1 = bookBalance.DeprFromPeriod;
              string finPeriodID = (string) null;
              nullable1 = bookBalance.Depreciate;
              if (!nullable1.GetValueOrDefault())
              {
                FABookHistory faBookHistory2 = PXResultset<FABookHistory>.op_Implicit(PXSelectBase<FABookHistory, PXViewOf<FABookHistory>.BasedOn<SelectFromBase<FABookHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<BqlField<FABookBalance.assetID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<BqlField<FABookBalance.bookID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<FABookHistory.closed, IBqlBool>.IsEqual<True>>>.Order<By<Desc<FABookHistory.finPeriodID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
                {
                  (object) bookBalance
                }, Array.Empty<object>()));
                if (faBookHistory2 != null)
                {
                  finPeriodID = faBookHistory2.FinPeriodID;
                  str1 = this.FABookPeriodUtils.PeriodPlusPeriodsCount(finPeriodID, 1, bookBalance.BookID, bookBalance.AssetID);
                }
              }
              FABookBalance faBookBalance1 = bookBalance;
              nullable1 = bookBalance.Depreciate;
              string str2 = !nullable1.GetValueOrDefault() ? str1 : (flag1 ? (string) null : (PX.Objects.FA.AssetProcess.IsDepreciatedInCurrentDeprPeriodNotByDisposal((PXGraph) this, asset.AssetID, bookBalance.BookID, faBookHist61.FinPeriodID) ? this.FABookPeriodUtils.PeriodPlusPeriodsCount(faBookHist61.FinPeriodID, 1, bookBalance.BookID, bookBalance.AssetID) : faBookHist61.FinPeriodID));
              faBookBalance1.CurrDeprPeriod = str2;
              FABookBalance faBookBalance2 = bookBalance;
              nullable1 = bookBalance.Depreciate;
              string str3 = !nullable1.GetValueOrDefault() ? finPeriodID : (flag1 ? bookBalance.DeprToPeriod : this.FABookPeriodUtils.PeriodPlusPeriodsCount(bookBalance.CurrDeprPeriod, -1, bookBalance.BookID, bookBalance.AssetID));
              faBookBalance2.LastDeprPeriod = str3;
              bookBalance.LastPeriod = string.CompareOrdinal(faBookHist61.FinPeriodID, bookBalance.DisposalPeriodID) > 0 ? fatran.FinPeriodID : bookBalance.DeprToPeriod;
              ((PXSelectBase<FABookBalance>) this.bookbalance).Update(bookBalance);
              if ((!(bookBalance.Status == "F") || !PX.Objects.FA.AssetProcess.IsFullyDepreciatedAsset((PXGraph) this, bookBalance.AssetID) ? "A" : "F") == "F")
              {
                asset = ((PXSelectBase<FixedAsset>) this.fixedasset).Update(((PXSelectBase<FixedAsset>) this.fixedasset).Locate(asset) ?? asset);
                ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.FullyDepreciateAsset))).FireOn((PXGraph) this, asset);
                break;
              }
              asset = ((PXSelectBase<FixedAsset>) this.fixedasset).Update(((PXSelectBase<FixedAsset>) this.fixedasset).Locate(asset) ?? asset);
              ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.ActivateAsset))).FireOn((PXGraph) this, asset);
              break;
            case 'P':
              bookBalance = (FABookBalance) ((PXSelectBase) this.bookbalance).Cache.Locate((object) bookBalance) ?? bookBalance;
              bookBalance.InitPeriod = PX.Objects.FA.AssetProcess.GetDeprFromPeriod((PXGraph) je, bookBalance, asset, details);
              FABookBalance faBookBalance3 = bookBalance;
              nullable1 = asset.UnderConstruction;
              string str4 = nullable1.GetValueOrDefault() ? bookBalance.InitPeriod : bookBalance.DeprToPeriod;
              faBookBalance3.LastPeriod = str4;
              if ((fatran.TranType == "D-" || fatran.TranType == "D+") && string.CompareOrdinal(fatran.FinPeriodID, bookBalance.LastPeriod) > 0)
              {
                bookBalance.LastPeriod = fatran.FinPeriodID;
                bookBalance.LastDeprPeriod = fatran.FinPeriodID;
              }
              if (bookBalance.Status == "F" && string.CompareOrdinal(fatran.FinPeriodID, bookBalance.LastPeriod) > 0)
                bookBalance.LastPeriod = fatran.FinPeriodID;
              ((PXSelectBase<FABookBalance>) this.bookbalance).Update(bookBalance);
              break;
            case 'R':
              if ((fatran.TranType == "D-" || fatran.TranType == "D+") && string.CompareOrdinal(fatran.FinPeriodID, bookBalance.LastPeriod) > 0)
              {
                bookBalance.LastPeriod = fatran.FinPeriodID;
                bookBalance.LastDeprPeriod = fatran.FinPeriodID;
                ((PXSelectBase<FABookBalance>) this.bookbalance).Update(bookBalance);
              }
              if ((!asset.Depreciable.GetValueOrDefault() || asset.UnderConstruction.GetValueOrDefault()) && (fatran.TranType == "P+" || fatran.TranType == "P-") && string.CompareOrdinal(fatran.FinPeriodID, bookBalance.CurrDeprPeriod) > 0)
              {
                bookBalance.CurrDeprPeriod = fatran.FinPeriodID;
                ((PXSelectBase<FABookBalance>) this.bookbalance).Update(bookBalance);
                break;
              }
              break;
            case 'S':
              bookBalance = (FABookBalance) ((PXSelectBase) this.bookbalance).Cache.Locate((object) bookBalance) ?? bookBalance;
              bookBalance.CurrDeprPeriod = (string) null;
              bookBalance.Status = "D";
              if (fatran.TranType == "D+" || fatran.TranType == "D-")
                bookBalance.LastDeprPeriod = fatran.FinPeriodID;
              if (string.CompareOrdinal(bookBalance.DeprToPeriod, bookBalance.DisposalPeriodID) > 0)
              {
                bookBalance.DeprToDate = details.DisposalDate;
                bookBalance.DeprToPeriod = bookBalance.DisposalPeriodID;
              }
              bookBalance.LastPeriod = bookBalance.DisposalPeriodID;
              ((PXSelectBase<FABookBalance>) this.bookbalance).Update(bookBalance);
              ((PXSelectBase) pxSelectBase).View.Clear();
              if (pxSelectBase.SelectWindowed(0, 1, new object[2]
              {
                (object) bookBalance.AssetID,
                (object) "D"
              }).Count == 0)
              {
                asset = ((PXSelectBase<FixedAsset>) this.fixedasset).Update(((PXSelectBase<FixedAsset>) this.fixedasset).Locate(asset) ?? asset);
                ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.DisposeAsset))).FireOn((PXGraph) this, asset);
                break;
              }
              break;
            case 'T':
              bookBalance = (FABookBalance) ((PXSelectBase) this.bookbalance).Cache.Locate((object) bookBalance) ?? bookBalance;
              if (asset.Status != "F" && string.IsNullOrEmpty(bookBalance.CurrDeprPeriod))
              {
                bookBalance.CurrDeprPeriod = bookBalance.DeprFromPeriod ?? fatran.FinPeriodID;
                ((PXSelectBase<FABookBalance>) this.bookbalance).Update(bookBalance);
                break;
              }
              break;
            case 'V':
              bookBalance = (FABookBalance) ((PXSelectBase) this.bookbalance).Cache.Locate((object) bookBalance) ?? bookBalance;
              bookBalance.Status = "R";
              bookBalance.CurrDeprPeriod = (string) null;
              bookBalance.LastPeriod = fatran.FinPeriodID;
              ((PXSelectBase<FABookBalance>) this.bookbalance).Update(bookBalance);
              ((PXSelectBase) pxSelectBase).View.Clear();
              if (pxSelectBase.SelectWindowed(0, 1, new object[2]
              {
                (object) bookBalance.AssetID,
                (object) "R"
              }).Count == 0)
              {
                asset = ((PXSelectBase<FixedAsset>) this.fixedasset).Update(((PXSelectBase<FixedAsset>) this.fixedasset).Locate(asset) ?? asset);
                ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.ReverseAsset))).FireOn((PXGraph) this, asset);
              }
              FAAccrualTran faAccrualTran1 = PXResultset<FAAccrualTran>.op_Implicit(PXSelectBase<FAAccrualTran, PXSelect<FAAccrualTran, Where<FAAccrualTran.tranID, Equal<Current<FATran.gLtranID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
              {
                (object) fatran
              }, Array.Empty<object>()));
              if (faAccrualTran1 != null)
              {
                FAAccrualTran copy = (FAAccrualTran) ((PXSelectBase) this.accrualtran).Cache.CreateCopy((object) faAccrualTran1);
                FAAccrualTran faAccrualTran2 = copy;
                nullable3 = faAccrualTran2.ClosedAmt;
                nullable4 = fatran.TranAmt;
                faAccrualTran2.ClosedAmt = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
                FAAccrualTran faAccrualTran3 = copy;
                nullable4 = faAccrualTran3.ClosedQty;
                Decimal? nullable7;
                if (!nullable4.HasValue)
                {
                  nullable3 = new Decimal?();
                  nullable7 = nullable3;
                }
                else
                  nullable7 = new Decimal?(Decimal.op_Decrement(nullable4.GetValueOrDefault()));
                faAccrualTran3.ClosedQty = nullable7;
                FAAccrualTran faAccrualTran4 = copy;
                nullable4 = faAccrualTran4.OpenAmt;
                nullable3 = fatran.TranAmt;
                faAccrualTran4.OpenAmt = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                FAAccrualTran faAccrualTran5 = copy;
                nullable3 = faAccrualTran5.OpenQty;
                Decimal? nullable8;
                if (!nullable3.HasValue)
                {
                  nullable4 = new Decimal?();
                  nullable8 = nullable4;
                }
                else
                  nullable8 = new Decimal?(Decimal.op_Increment(nullable3.GetValueOrDefault()));
                faAccrualTran5.OpenQty = nullable8;
                ((PXSelectBase<FAAccrualTran>) this.accrualtran).Update(copy);
                break;
              }
              break;
          }
        }
        if (this.UpdateGL)
        {
          nullable1 = faBook.UpdateGL;
          if (nullable1.GetValueOrDefault() && doc.Origin != "I")
          {
            this.SetControlAccountFlags(fatran);
            bool flag2 = this.SummPost || this.SummPostDepr && fatran.Origin == "D";
            PX.Objects.FA.AssetProcess.SegregateBatch(je, asset.BranchID, fatran.TranDate, fatran.FinPeriodID, doc.DocDesc, created);
            PX.Objects.GL.GLTran gltran1 = new PX.Objects.GL.GLTran();
            gltran1.SummPost = new bool?(flag2);
            gltran1.ReclassificationProhibited = fatran.ReclassificationOnDebitProhibited;
            gltran1.AccountID = fatran.DebitAccountID;
            gltran1.SubID = fatran.DebitSubID;
            gltran1.CuryDebitAmt = fatran.TranAmt;
            gltran1.CuryCreditAmt = new Decimal?(0M);
            gltran1.DebitAmt = fatran.TranAmt;
            gltran1.CreditAmt = new Decimal?(0M);
            gltran1.TranType = fatran.TranType;
            gltran1.Released = new bool?(true);
            gltran1.TranDesc = fatran.TranDesc;
            gltran1.RefNbr = fatran.RefNbr;
            PX.Objects.GL.GLTran glTran1 = gltran1;
            int? nullable9;
            if (!flag2)
            {
              nullable9 = fatran.LineNbr;
            }
            else
            {
              nullable5 = new int?();
              nullable9 = nullable5;
            }
            glTran1.TranLineNbr = nullable9;
            switch (fatran.TranType)
            {
              case "R+":
                gltran1.ProjectID = ProjectDefaultAttribute.NonProject();
                break;
              case "R-":
                this.ApplyOriginalBranchAndProject(doc, fatran, gltran1);
                break;
              case "TD":
                gltran1.BranchID = fatran.SrcBranchID;
                break;
              default:
                gltran1.BranchID = fatran.BranchID;
                break;
            }
            ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(gltran1);
            PX.Objects.GL.GLTran gltran2 = new PX.Objects.GL.GLTran();
            gltran2.SummPost = new bool?(flag2);
            gltran2.ReclassificationProhibited = fatran.ReclassificationOnCreditProhibited;
            gltran2.AccountID = fatran.CreditAccountID;
            gltran2.SubID = fatran.CreditSubID;
            gltran2.CuryDebitAmt = new Decimal?(0M);
            gltran2.CuryCreditAmt = fatran.TranAmt;
            gltran2.DebitAmt = new Decimal?(0M);
            gltran2.CreditAmt = fatran.TranAmt;
            gltran2.TranType = fatran.TranType;
            gltran2.Released = new bool?(true);
            gltran2.TranDesc = fatran.TranDesc;
            gltran2.RefNbr = fatran.RefNbr;
            PX.Objects.GL.GLTran glTran2 = gltran2;
            int? nullable10;
            if (!flag2)
            {
              nullable10 = fatran.LineNbr;
            }
            else
            {
              nullable5 = new int?();
              nullable10 = nullable5;
            }
            glTran2.TranLineNbr = nullable10;
            switch (fatran.TranType)
            {
              case "R+":
                this.ApplyOriginalBranchAndProject(doc, fatran, gltran2);
                break;
              case "R-":
                gltran2.ProjectID = ProjectDefaultAttribute.NonProject();
                break;
              case "TP":
                gltran2.BranchID = fatran.SrcBranchID;
                break;
              default:
                gltran2.BranchID = fatran.BranchID;
                break;
            }
            ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(gltran2);
            if (((PXSelectBase) je.GLTranModuleBatNbr).Cache.IsInsertedUpdatedDeleted)
            {
              ((PXAction) je.Save).Press();
              if (((PXSelectBase<Batch>) je.BatchModule).Current != null)
              {
                fatran.BatchNbr = ((PXSelectBase<Batch>) je.BatchModule).Current.BatchNbr;
                if (created.Find((object) ((PXSelectBase<Batch>) je.BatchModule).Current) == null)
                  created.Add(((PXSelectBase<Batch>) je.BatchModule).Current);
              }
            }
            else
              ((PXSelectBase<Batch>) je.BatchModule).Current = (Batch) null;
            FARegister faRegister = doc;
            nullable1 = faRegister.Posted;
            faRegister.Posted = fatran.BatchNbr != null ? new bool?(true) : nullable1;
          }
        }
        fatran.Released = new bool?(true);
        ((PXSelectBase<FATran>) this.booktran).Update(fatran);
        if (fatran.TranType == "P+")
        {
          FABookBalance faBookBalance4 = (FABookBalance) ((PXSelectBase) this.bookbalance).Cache.Locate((object) bookBalance) ?? bookBalance;
          faBookBalance4.IsAcquired = new bool?(true);
          ((PXSelectBase<FABookBalance>) this.bookbalance).Update(faBookBalance4);
          asset.IsAcquired = new bool?(true);
          ((PXSelectBase<FixedAsset>) this.fixedasset).Update(asset);
        }
      }
      doc = ((PXSelectBase<FARegister>) this.register).Update(doc);
      doc.Released = new bool?(true);
      ((SelectedEntityEvent<FARegister>) PXEntityEventBase<FARegister>.Container<FARegister.Events>.Select((Expression<Func<FARegister.Events, PXEntityEvent<FARegister.Events>>>) (ev => ev.ReleaseDocument))).FireOn((PXGraph) this, doc);
      ((PXGraph) this).Actions.PressSave();
      transactionScope.Complete();
    }
  }

  private bool UseAcceleratedDepreciation(FixedAsset cls, FADepreciationMethod method)
  {
    return (this.depreciationCalculationGraph = this.depreciationCalculationGraph ?? PXGraph.CreateInstance<DepreciationCalculation>()).UseAcceleratedDepreciation(cls, method);
  }

  private void ApplyOriginalBranchAndProject(FARegister faregister, FATran fatran, PX.Objects.GL.GLTran gltran)
  {
    PX.Objects.GL.GLTran glTran = PXResultset<PX.Objects.GL.GLTran>.op_Implicit(PXSelectBase<PX.Objects.GL.GLTran, PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.tranID, Equal<Current<FATran.gLtranID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) fatran
    }, Array.Empty<object>()));
    if (glTran != null)
    {
      gltran.BranchID = glTran.BranchID;
    }
    else
    {
      if (!(faregister.Origin == "I") && !(faregister.Origin == "V"))
        throw new PXException("Reconcilliation transaction has no reference to the original GL transaction.");
      gltran.BranchID = fatran.BranchID;
    }
    if (glTran == null || !glTran.ProjectID.HasValue)
      return;
    gltran.ProjectID = glTran.ProjectID;
    gltran.TaskID = glTran.TaskID;
  }

  protected static bool IsDepreciatedInCurrentDeprPeriodNotByDisposal(
    PXGraph graph,
    int? assetID,
    int? bookID,
    string finPeriodID)
  {
    FARegister disposalRegister = PX.Objects.FA.AssetProcess.GetDisposalRegister(graph, assetID);
    return PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelectReadonly<FATran, Where<FATran.assetID, Equal<Required<FixedAsset.assetID>>, And<FATran.bookID, Equal<Required<FATran.bookID>>, And<FATran.released, Equal<True>, And<FATran.finPeriodID, Equal<Required<FATran.finPeriodID>>, And<FATran.tranType, Equal<FATran.tranType.depreciationPlus>, And<FATran.refNbr, NotEqual<Required<FATran.refNbr>>>>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[4]
    {
      (object) assetID,
      (object) bookID,
      (object) finPeriodID,
      (object) disposalRegister?.RefNbr
    })) != null;
  }

  protected static FARegister GetDisposalRegister(PXGraph graph, int? assetID)
  {
    return PXResultset<FARegister>.op_Implicit(PXSelectBase<FARegister, PXSelectReadonly2<FARegister, LeftJoin<FATran, On<FARegister.refNbr, Equal<FATran.refNbr>>>, Where<FATran.assetID, Equal<Required<FixedAsset.assetID>>, And<FARegister.origin, Equal<FARegister.origin.disposal>, And<FARegister.released, Equal<True>>>>, OrderBy<Desc<FATran.tranID>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) assetID
    }));
  }

  protected virtual void SetControlAccountFlags(FATran fatran)
  {
    if (((IEnumerable<string>) PX.Objects.FA.AssetProcess.NonReclassifiableTranTypes).Contains<string>(fatran.TranType))
    {
      this.CheckControlAccountFlagsIsNotDefined(fatran);
      fatran.ReclassificationOnDebitProhibited = new bool?(true);
      fatran.ReclassificationOnCreditProhibited = new bool?(true);
    }
    else if (((IEnumerable<string>) PX.Objects.FA.AssetProcess.FullyReclassifiableTranTypes).Contains<string>(fatran.TranType))
    {
      this.CheckControlAccountFlagsIsNotDefined(fatran);
      fatran.ReclassificationOnDebitProhibited = new bool?(false);
      fatran.ReclassificationOnCreditProhibited = new bool?(false);
    }
    else if (fatran.TranType == "PD")
    {
      this.CheckControlAccountFlagsIsNotDefined(fatran);
      fatran.ReclassificationOnDebitProhibited = new bool?(true);
      fatran.ReclassificationOnCreditProhibited = new bool?(false);
    }
    else if (fatran.TranType == "A+")
    {
      if (!fatran.ReclassificationOnDebitProhibited.HasValue)
        fatran.ReclassificationOnDebitProhibited = new bool?(false);
      if (fatran.ReclassificationOnCreditProhibited.HasValue)
        return;
      fatran.ReclassificationOnCreditProhibited = new bool?(true);
    }
    else if (fatran.TranType == "A-")
    {
      if (!fatran.ReclassificationOnDebitProhibited.HasValue)
        fatran.ReclassificationOnDebitProhibited = new bool?(true);
      if (fatran.ReclassificationOnCreditProhibited.HasValue)
        return;
      fatran.ReclassificationOnCreditProhibited = new bool?(false);
    }
    else if (fatran.TranType == "D+")
    {
      this.CheckControlAccountFlagsIsNotDefined(fatran);
      fatran.ReclassificationOnDebitProhibited = new bool?(false);
      fatran.ReclassificationOnCreditProhibited = new bool?(true);
    }
    else if (fatran.TranType == "D-")
    {
      this.CheckControlAccountFlagsIsNotDefined(fatran);
      fatran.ReclassificationOnDebitProhibited = new bool?(true);
      fatran.ReclassificationOnCreditProhibited = new bool?(false);
    }
    else
      throw new PXException("{0} flag has value on {1}, but it must be undefined for transaction type '{2}'.", new object[2]
      {
        ((PXGraph) this).Caches[typeof (FATran)].GetValueExt<FATran.tranType>((object) fatran),
        (object) ((object) fatran).ToString()
      });
  }

  private void CheckControlAccountFlagsIsNotDefined(FATran fatran)
  {
    if (fatran.ReclassificationOnDebitProhibited.HasValue)
      throw new PXException("Flags of control accounts were not defined for type '{0}'.", new object[3]
      {
        (object) typeof (FATran.reclassificationOnDebitProhibited).Name,
        (object) ((object) fatran).ToString(),
        ((PXGraph) this).Caches[typeof (FATran)].GetValueExt<FATran.tranType>((object) fatran)
      });
    if (fatran.ReclassificationOnCreditProhibited.HasValue)
      throw new PXException("Flags of control accounts were not defined for type '{0}'.", new object[3]
      {
        (object) typeof (FATran.reclassificationOnCreditProhibited).Name,
        (object) ((object) fatran).ToString(),
        ((PXGraph) this).Caches[typeof (FATran)].GetValueExt<FATran.tranType>((object) fatran)
      });
  }

  public static bool IsFullyDepreciatedAsset(PXGraph graph, int? AssetID)
  {
    return PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FABookBalance.assetID>>, And<FABookBalance.status, NotEqual<FixedAssetStatus.fullyDepreciated>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) AssetID
    })) == null;
  }

  private static void SegregateBatch(
    JournalEntry je,
    int? BranchID,
    DateTime? DocDate,
    string FinPeriodID,
    string descr,
    DocumentList<Batch> created)
  {
    if (((PXSelectBase) je.GLTranModuleBatNbr).Cache.IsInsertedUpdatedDeleted)
      ((PXGraph) je).Clear();
    Batch batch1 = created.Find<Batch.branchID, Batch.finPeriodID>((object) BranchID, (object) FinPeriodID);
    if (batch1 != null)
    {
      if (!WebConfig.ParallelProcessingDisabled)
      {
        int? lineCntr = batch1.LineCntr;
        int num = 500;
        if (!(lineCntr.GetValueOrDefault() < num & lineCntr.HasValue))
          goto label_10;
      }
      if (!((PXSelectBase) je.BatchModule).Cache.ObjectsEqual((object) ((PXSelectBase<Batch>) je.BatchModule).Current, (object) batch1))
        ((PXGraph) je).Clear();
      PXCache<Batch>.StoreOriginal((PXGraph) je, batch1);
      if (batch1.Description != descr)
      {
        batch1.Description = "";
        ((PXSelectBase<Batch>) je.BatchModule).Update(batch1);
      }
      ((PXSelectBase<Batch>) je.BatchModule).Current = batch1;
      return;
    }
label_10:
    ((PXGraph) je).Clear();
    Batch data = new Batch();
    data.Module = "FA";
    data.Status = "U";
    data.Released = new bool?(true);
    data.Hold = new bool?(false);
    data.TranPeriodID = FinPeriodID;
    data.BranchID = BranchID;
    data.DebitTotal = new Decimal?(0M);
    data.CreditTotal = new Decimal?(0M);
    data.Description = descr;
    OpenPeriodAttribute.SetValidatePeriod<Batch.finPeriodID>(((PXSelectBase) je.BatchModule).Cache, (object) data, PeriodValidation.DefaultUpdate);
    Batch batch2 = ((PXSelectBase<Batch>) je.BatchModule).Insert(data);
    OpenPeriodAttribute.SetValidatePeriod<Batch.finPeriodID>(((PXSelectBase) je.BatchModule).Cache, (object) batch2, PeriodValidation.DefaultSelectUpdate);
    batch2.DateEntered = DocDate;
    batch2.FinPeriodID = FinPeriodID;
    FinPeriodIDAttribute.SetMasterPeriodID<Batch.finPeriodID>(((PXSelectBase) je.BatchModule).Cache, (object) batch2);
  }

  public static void TransferAsset(
    PXGraph graph,
    FixedAsset asset,
    FALocationHistory location,
    ref FARegister register)
  {
    PXCache cach1 = graph.Caches[typeof (FALocationHistory)];
    PXCache cach2 = graph.Caches[typeof (FixedAsset)];
    PXCache cach3 = graph.Caches[typeof (FARegister)];
    PXCache cach4 = graph.Caches[typeof (FATran)];
    FASetup current1 = (FASetup) graph.Caches[typeof (FASetup)].Current;
    PXCache cach5 = graph.Caches[typeof (FADetails)];
    if (asset.UnderConstruction.GetValueOrDefault())
    {
      if (PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, In3<FATran.tranType.calculatedPlus, FATran.tranType.calculatedMinus, FATran.tranType.depreciationPlus, FATran.tranType.depreciationMinus, FATran.tranType.adjustingDeprPlus, FATran.tranType.adjustingDeprMinus>>>>>.And<BqlOperand<FATran.assetID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select(graph, new object[1]
      {
        (object) asset.AssetID
      })) != null)
        throw new PXException("The fixed asset has depreciation transactions and cannot be transferred to the Under Construction class.");
    }
    FAClass assetClass = graph.GetAssetClass(asset.OldClassID);
    bool? underConstruction;
    int num;
    if (assetClass == null)
    {
      num = 0;
    }
    else
    {
      underConstruction = assetClass.UnderConstruction;
      num = underConstruction.GetValueOrDefault() ? 1 : 0;
    }
    bool flag = num != 0;
    underConstruction = asset.UnderConstruction;
    if (!underConstruction.GetValueOrDefault() && flag && (cach5.Current is FADetails current2 ? (!current2.DepreciateFromDate.HasValue ? 1 : 0) : 1) != 0)
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FADetails.depreciateFromDate>(cach5)
      });
    if (!cach2.IsInsertedUpdatedDeleted)
    {
      cach2.ClearQueryCacheObsolete();
      cach2.Clear();
      PXCache<FixedAsset> pxCache = GraphHelper.Caches<FixedAsset>(graph);
      PXGraph pxGraph = graph;
      object[] objArray1 = new object[1]{ (object) asset };
      object[] objArray2 = Array.Empty<object>();
      FixedAsset fixedAsset;
      asset = fixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelectReadonly<FixedAsset, Where<FixedAsset.assetCD, Equal<Current<FixedAsset.assetCD>>>>.Config>.SelectSingleBound(pxGraph, objArray1, objArray2));
      ((PXCache) pxCache).Current = (object) fixedAsset;
    }
    PX.Objects.FA.AssetProcess.FAAccounts oldAccounts = PX.Objects.FA.AssetProcess.GetOldAccounts(graph, asset, location);
    PX.Objects.FA.AssetProcess.FAAccounts newAccounts = PX.Objects.FA.AssetProcess.GetNewAccounts(graph, cach2, cach1, current1, asset, location);
    FALocationHistory copy1 = PXCache<FALocationHistory>.CreateCopy(location);
    newAccounts.CopyAccountsTo(location);
    if (PX.Objects.FA.AssetProcess.IsTransfer(asset, copy1, location))
      GraphHelper.MarkUpdated(cach1, (object) location);
    foreach (PXResult<FARegister, FATran> pxResult in PXSelectBase<FARegister, PXSelectJoin<FARegister, InnerJoin<FATran, On<FARegister.refNbr, Equal<FATran.refNbr>>>, Where<FARegister.released, NotEqual<True>, And<FARegister.origin, Equal<FARegister.origin.transfer>, And<FATran.assetID, Equal<Current<FixedAsset.assetID>>>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      (object) asset
    }, Array.Empty<object>()))
    {
      FARegister faRegister = PXResult<FARegister, FATran>.op_Implicit(pxResult);
      FATran faTran = PXResult<FARegister, FATran>.op_Implicit(pxResult);
      register = register ?? faRegister;
      cach4.Delete((object) faTran);
      location.RefNbr = (string) null;
    }
    if (current1.UpdateGL.GetValueOrDefault() && (PX.Objects.FA.AssetProcess.RequireTransferAsset(oldAccounts, newAccounts) || PX.Objects.FA.AssetProcess.RequireTransferDepreciation(asset, oldAccounts, newAccounts, new bool?(flag))))
    {
      PXGraph pxGraph = graph;
      object[] objArray = new object[1]
      {
        (object) asset.AssetID
      };
      PXResultset<FABookBalance> pxResultset;
      if ((pxResultset = PXSelectBase<FABookBalance, PXViewOf<FABookBalance>.BasedOn<SelectFromBase<FABookBalance, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.updateGL, Equal<True>>>>>.And<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select(pxGraph, objArray)).Count > 0)
      {
        if (register == null)
          register = (FARegister) cach3.Insert((object) new FARegister()
          {
            BranchID = newAccounts.BranchID,
            Origin = "T",
            DocDate = location.TransactionDate
          });
        else
          cach3.Current = (object) register;
        register.Reason = location.Reason;
        if (asset.IsAcquired.GetValueOrDefault() && !string.IsNullOrEmpty(location.PeriodID) && oldAccounts.BranchID.HasValue)
        {
          if (!(graph is IAssetTransferInformationCheckable informationCheckable))
            throw new NotImplementedException();
          informationCheckable.CheckAssetTransferInformation(asset, cach5.Current as FADetails);
          foreach (PXResult<FABookBalance> pxResult in pxResultset)
          {
            FABookBalance bal = PXResult<FABookBalance>.op_Implicit(pxResult);
            if (PX.Objects.FA.AssetProcess.RequireTransferAsset(oldAccounts, newAccounts))
              PX.Objects.FA.AssetProcess.InsertTransferAssetTransaction(cach4, ref register, bal, asset, location, oldAccounts, newAccounts, new bool?(flag));
            if (PX.Objects.FA.AssetProcess.RequireTransferDepreciation(asset, oldAccounts, newAccounts, new bool?(flag)))
              PX.Objects.FA.AssetProcess.InsertTransferDepreciationTransaction(cach4, ref register, bal, asset, location, oldAccounts, newAccounts);
            location.RefNbr = register.RefNbr;
          }
        }
      }
    }
    PX.Objects.FA.AssetProcess.UpdateAssetAccounts(cach2, asset, oldAccounts, newAccounts);
    FALocationHistory prevLocation = graph.GetPrevLocation(location);
    if (prevLocation != null && location.RefNbr == null && !graph.IsLocationChanged(location, prevLocation) && !graph.IsAccountsChanged(location, prevLocation))
    {
      GraphHelper.Caches<FALocationHistory>(graph).Delete(location);
      FALocationHistory copy2 = PXCache<FALocationHistory>.CreateCopy(location);
      copy2.RevisionID = prevLocation.RevisionID;
      copy2.TransactionDate = prevLocation.TransactionDate;
      copy2.PeriodID = prevLocation.PeriodID;
      copy2.RefNbr = prevLocation.RefNbr;
      GraphHelper.Caches<FALocationHistory>(graph).Update(copy2);
      FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Current<FALocationHistory.assetID>>>>.Config>.SelectSingleBound(graph, new object[1]
      {
        (object) location
      }, Array.Empty<object>()));
      faDetails.LocationRevID = copy2.RevisionID;
      GraphHelper.Caches<FADetails>(graph).Update(faDetails);
    }
    if (PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.refNbr, Equal<Current<FARegister.refNbr>>>>.Config>.SelectSingleBound(graph, new object[1]
    {
      cach3.Current
    }, Array.Empty<object>())) != null)
      return;
    cach3.Delete(cach3.Current);
  }

  protected static PX.Objects.FA.AssetProcess.FAAccounts GetOldAccounts(
    PXGraph graph,
    FixedAsset asset,
    FALocationHistory location)
  {
    if (!(graph.Caches[typeof (FixedAsset)].GetOriginal((object) asset) is FixedAsset fixedAsset))
      fixedAsset = asset;
    FixedAsset asset1 = fixedAsset;
    if (location.RefNbr == null)
      return new PX.Objects.FA.AssetProcess.FAAccounts(asset1);
    FARegister faRegister = PXResultset<FARegister>.op_Implicit(PXSelectBase<FARegister, PXSelect<FARegister, Where<FARegister.refNbr, Equal<Required<FARegister.refNbr>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) location.RefNbr
    }));
    if (faRegister == null || faRegister.Released.GetValueOrDefault())
      return new PX.Objects.FA.AssetProcess.FAAccounts(asset1);
    FALocationHistory prevLocation = graph.GetPrevLocation(location);
    return prevLocation == null ? new PX.Objects.FA.AssetProcess.FAAccounts(asset1) : new PX.Objects.FA.AssetProcess.FAAccounts(prevLocation);
  }

  protected static PX.Objects.FA.AssetProcess.FAAccounts GetNewAccounts(
    PXGraph graph,
    PXCache assetCache,
    PXCache locationsCache,
    FASetup fasetup,
    FixedAsset asset,
    FALocationHistory location)
  {
    PX.Objects.FA.AssetProcess.FAAccounts newAccounts;
    bool? nullable1;
    if (asset.OldClassID.HasValue)
    {
      int? oldClassId = asset.OldClassID;
      int? classId = asset.ClassID;
      if (!(oldClassId.GetValueOrDefault() == classId.GetValueOrDefault() & oldClassId.HasValue == classId.HasValue))
      {
        FixedAsset asset1 = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select(graph, new object[1]
        {
          (object) asset.ClassID
        }));
        newAccounts = asset1 != null ? new PX.Objects.FA.AssetProcess.FAAccounts(asset1) : throw new PXException("{0} '{1}' cannot be found in the system.", new object[2]
        {
          (object) "Asset Class",
          (object) asset.ClassID
        });
        newAccounts.DisposalSubID = AssetMaint.MakeSubID<FixedAsset.proceedsSubMask, FixedAsset.disposalSubID>(assetCache, asset);
        newAccounts.GainSubID = AssetMaint.MakeSubID<FixedAsset.gainLossSubMask, FixedAsset.gainSubID>(assetCache, asset);
        newAccounts.LossSubID = AssetMaint.MakeSubID<FixedAsset.gainLossSubMask, FixedAsset.lossSubID>(assetCache, asset);
        newAccounts.BranchID = location.BranchID;
        nullable1 = fasetup.UpdateGL;
        if (nullable1.GetValueOrDefault())
        {
          newAccounts.FASubID = AssetMaint.MakeSubID<FixedAsset.fASubMask, FixedAsset.fASubID>(assetCache, asset);
          newAccounts.AccumulatedDepreciationSubID = AssetMaint.MakeSubID<FixedAsset.accumDeprSubMask, FixedAsset.accumulatedDepreciationSubID>(assetCache, asset);
          newAccounts.DepreciatedExpenseSubID = AssetMaint.MakeSubID<FixedAsset.deprExpenceSubMask, FixedAsset.depreciatedExpenseSubID>(assetCache, asset);
          goto label_31;
        }
        newAccounts.FASubID = (int?) assetCache.GetValue<FixedAsset.fASubID>((object) asset1);
        newAccounts.AccumulatedDepreciationSubID = (int?) assetCache.GetValue<FixedAsset.accumulatedDepreciationSubID>((object) asset1);
        newAccounts.DepreciatedExpenseSubID = (int?) assetCache.GetValue<FixedAsset.depreciatedExpenseSubID>((object) asset1);
        goto label_31;
      }
    }
    newAccounts = new PX.Objects.FA.AssetProcess.FAAccounts(asset);
    newAccounts.BranchID = location.BranchID;
    FixedAsset original = assetCache.GetOriginal((object) asset) as FixedAsset;
    if (assetCache.ObjectsEqual<FixedAsset.fAAccountID>((object) asset, (object) original))
      newAccounts.FAAccountID = location.FAAccountID;
    if (assetCache.ObjectsEqual<FixedAsset.fASubID>((object) asset, (object) original))
      newAccounts.FASubID = location.FASubID;
    if (assetCache.ObjectsEqual<FixedAsset.accumulatedDepreciationAccountID>((object) asset, (object) original))
      newAccounts.AccumulatedDepreciationAccountID = location.AccumulatedDepreciationAccountID;
    if (assetCache.ObjectsEqual<FixedAsset.accumulatedDepreciationSubID>((object) asset, (object) original))
      newAccounts.AccumulatedDepreciationSubID = location.AccumulatedDepreciationSubID;
    FALocationHistory faLocationHistory1;
    if (locationsCache.GetStatus((object) location) != 2)
    {
      faLocationHistory1 = locationsCache.GetOriginal((object) location) as FALocationHistory;
    }
    else
    {
      FALocationHistory faLocationHistory2 = faLocationHistory1 = graph.GetPrevLocation(location);
    }
    FALocationHistory faLocationHistory3 = faLocationHistory1;
    if (faLocationHistory3 == null || !locationsCache.ObjectsEqual<FALocationHistory.locationID, FALocationHistory.department>((object) faLocationHistory3, (object) location))
    {
      if (assetCache.ObjectsEqual<FixedAsset.fASubID>((object) asset, (object) original))
        newAccounts.FASubID = AssetMaint.MakeSubID<FixedAsset.fASubMask, FixedAsset.fASubID>(assetCache, asset);
      if (assetCache.ObjectsEqual<FixedAsset.accumulatedDepreciationSubID>((object) asset, (object) original))
        newAccounts.AccumulatedDepreciationSubID = AssetMaint.MakeSubID<FixedAsset.accumDeprSubMask, FixedAsset.accumulatedDepreciationSubID>(assetCache, asset);
      if (assetCache.ObjectsEqual<FixedAsset.depreciatedExpenseSubID>((object) asset, (object) original))
        newAccounts.DepreciatedExpenseSubID = AssetMaint.MakeSubID<FixedAsset.deprExpenceSubMask, FixedAsset.depreciatedExpenseSubID>(assetCache, asset);
      if (assetCache.ObjectsEqual<FixedAsset.disposalSubID>((object) asset, (object) original))
        newAccounts.DisposalSubID = AssetMaint.MakeSubID<FixedAsset.proceedsSubMask, FixedAsset.disposalSubID>(assetCache, asset);
      if (assetCache.ObjectsEqual<FixedAsset.gainSubID>((object) asset, (object) original))
        newAccounts.GainSubID = AssetMaint.MakeSubID<FixedAsset.gainLossSubMask, FixedAsset.gainSubID>(assetCache, asset);
      if (assetCache.ObjectsEqual<FixedAsset.lossSubID>((object) asset, (object) original))
        newAccounts.LossSubID = AssetMaint.MakeSubID<FixedAsset.gainLossSubMask, FixedAsset.lossSubID>(assetCache, asset);
    }
label_31:
    FAClass assetClass = graph.GetAssetClass(asset.ClassID);
    FixedAsset fixedAsset = asset;
    bool? nullable2;
    if (assetClass == null)
    {
      nullable1 = new bool?();
      nullable2 = nullable1;
    }
    else
      nullable2 = assetClass.UnderConstruction;
    nullable1 = nullable2;
    bool? nullable3 = new bool?(nullable1.GetValueOrDefault());
    fixedAsset.UnderConstruction = nullable3;
    return newAccounts;
  }

  protected static bool IsTransfer(
    FixedAsset asset,
    FALocationHistory oldLocation,
    FALocationHistory newLocation)
  {
    int? faAccountId1 = newLocation.FAAccountID;
    int? faAccountId2 = oldLocation.FAAccountID;
    if (faAccountId1.GetValueOrDefault() == faAccountId2.GetValueOrDefault() & faAccountId1.HasValue == faAccountId2.HasValue)
    {
      int? faSubId1 = newLocation.FASubID;
      int? faSubId2 = oldLocation.FASubID;
      if (faSubId1.GetValueOrDefault() == faSubId2.GetValueOrDefault() & faSubId1.HasValue == faSubId2.HasValue)
      {
        int? disposalAccountId1 = newLocation.DisposalAccountID;
        int? disposalAccountId2 = oldLocation.DisposalAccountID;
        if (disposalAccountId1.GetValueOrDefault() == disposalAccountId2.GetValueOrDefault() & disposalAccountId1.HasValue == disposalAccountId2.HasValue)
        {
          int? disposalSubId1 = newLocation.DisposalSubID;
          int? disposalSubId2 = oldLocation.DisposalSubID;
          if (disposalSubId1.GetValueOrDefault() == disposalSubId2.GetValueOrDefault() & disposalSubId1.HasValue == disposalSubId2.HasValue)
          {
            int? gainAcctId1 = newLocation.GainAcctID;
            int? gainAcctId2 = oldLocation.GainAcctID;
            if (gainAcctId1.GetValueOrDefault() == gainAcctId2.GetValueOrDefault() & gainAcctId1.HasValue == gainAcctId2.HasValue)
            {
              int? gainSubId1 = newLocation.GainSubID;
              int? gainSubId2 = oldLocation.GainSubID;
              if (gainSubId1.GetValueOrDefault() == gainSubId2.GetValueOrDefault() & gainSubId1.HasValue == gainSubId2.HasValue)
              {
                int? lossAcctId1 = newLocation.LossAcctID;
                int? lossAcctId2 = oldLocation.LossAcctID;
                if (lossAcctId1.GetValueOrDefault() == lossAcctId2.GetValueOrDefault() & lossAcctId1.HasValue == lossAcctId2.HasValue)
                {
                  int? lossSubId1 = newLocation.LossSubID;
                  int? lossSubId2 = oldLocation.LossSubID;
                  if (lossSubId1.GetValueOrDefault() == lossSubId2.GetValueOrDefault() & lossSubId1.HasValue == lossSubId2.HasValue)
                  {
                    int? depreciationAccountId1 = newLocation.AccumulatedDepreciationAccountID;
                    int? depreciationAccountId2 = oldLocation.AccumulatedDepreciationAccountID;
                    if (depreciationAccountId1.GetValueOrDefault() == depreciationAccountId2.GetValueOrDefault() & depreciationAccountId1.HasValue == depreciationAccountId2.HasValue)
                    {
                      int? depreciationSubId1 = newLocation.AccumulatedDepreciationSubID;
                      int? depreciationSubId2 = oldLocation.AccumulatedDepreciationSubID;
                      if (depreciationSubId1.GetValueOrDefault() == depreciationSubId2.GetValueOrDefault() & depreciationSubId1.HasValue == depreciationSubId2.HasValue)
                      {
                        int? expenseAccountId1 = newLocation.DepreciatedExpenseAccountID;
                        int? expenseAccountId2 = oldLocation.DepreciatedExpenseAccountID;
                        if (expenseAccountId1.GetValueOrDefault() == expenseAccountId2.GetValueOrDefault() & expenseAccountId1.HasValue == expenseAccountId2.HasValue)
                        {
                          int? depreciatedExpenseSubId1 = newLocation.DepreciatedExpenseSubID;
                          int? depreciatedExpenseSubId2 = oldLocation.DepreciatedExpenseSubID;
                          if (depreciatedExpenseSubId1.GetValueOrDefault() == depreciatedExpenseSubId2.GetValueOrDefault() & depreciatedExpenseSubId1.HasValue == depreciatedExpenseSubId2.HasValue)
                            return false;
                        }
                      }
                    }
                    return asset.Depreciable.GetValueOrDefault();
                  }
                }
              }
            }
          }
        }
      }
    }
    return true;
  }

  protected static bool RequireTransferAsset(
    PX.Objects.FA.AssetProcess.FAAccounts oldAccounts,
    PX.Objects.FA.AssetProcess.FAAccounts newAccounts)
  {
    int? branchId1 = newAccounts.BranchID;
    int? branchId2 = oldAccounts.BranchID;
    if (branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue)
    {
      int? faAccountId1 = newAccounts.FAAccountID;
      int? faAccountId2 = oldAccounts.FAAccountID;
      if (faAccountId1.GetValueOrDefault() == faAccountId2.GetValueOrDefault() & faAccountId1.HasValue == faAccountId2.HasValue)
      {
        int? faSubId1 = newAccounts.FASubID;
        int? faSubId2 = oldAccounts.FASubID;
        return !(faSubId1.GetValueOrDefault() == faSubId2.GetValueOrDefault() & faSubId1.HasValue == faSubId2.HasValue);
      }
    }
    return true;
  }

  protected static bool RequireTransferDepreciation(
    FixedAsset asset,
    PX.Objects.FA.AssetProcess.FAAccounts oldAccounts,
    PX.Objects.FA.AssetProcess.FAAccounts newAccounts,
    bool? previousUnderConstructionValue)
  {
    if (!asset.Depreciable.GetValueOrDefault() || previousUnderConstructionValue.GetValueOrDefault() || asset.UnderConstruction.GetValueOrDefault() || !oldAccounts.AccumulatedDepreciationAccountID.HasValue || !oldAccounts.AccumulatedDepreciationSubID.HasValue)
      return false;
    int? nullable1 = newAccounts.BranchID;
    int? nullable2 = oldAccounts.BranchID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
    {
      nullable2 = newAccounts.AccumulatedDepreciationAccountID;
      nullable1 = oldAccounts.AccumulatedDepreciationAccountID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      {
        nullable1 = newAccounts.AccumulatedDepreciationSubID;
        nullable2 = oldAccounts.AccumulatedDepreciationSubID;
        return !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue);
      }
    }
    return true;
  }

  protected static void InsertTransferAssetTransaction(
    PXCache transactCache,
    ref FARegister register,
    FABookBalance bal,
    FixedAsset asset,
    FALocationHistory location,
    PX.Objects.FA.AssetProcess.FAAccounts oldAccounts,
    PX.Objects.FA.AssetProcess.FAAccounts newAccounts,
    bool? previousUnderConstructionValue)
  {
    string str = !asset.UnderConstruction.GetValueOrDefault() && previousUnderConstructionValue.GetValueOrDefault() || asset.UnderConstruction.GetValueOrDefault() ? bal.CurrDeprPeriod ?? bal.LastPeriod : location.PeriodID ?? bal.CurrDeprPeriod ?? bal.LastPeriod;
    FATran faTran1 = new FATran()
    {
      AssetID = asset.AssetID,
      BookID = bal.BookID,
      TranType = "TP",
      FinPeriodID = bal.UpdateGL.GetValueOrDefault() ? str : (string) null,
      TranDate = location.TransactionDate,
      CreditAccountID = oldAccounts.FAAccountID,
      CreditSubID = oldAccounts.FASubID,
      DebitAccountID = newAccounts.FAAccountID,
      DebitSubID = newAccounts.FASubID,
      TranAmt = bal.YtdAcquired,
      TranDesc = PXMessages.LocalizeFormatNoPrefix("Transfer Purchasing for Asset {0}", new object[1]
      {
        (object) asset.AssetCD
      }),
      BranchID = newAccounts.BranchID,
      SrcBranchID = oldAccounts.BranchID,
      LineNbr = (int?) PXLineNbrAttribute.NewLineNbr<FATran.lineNbr>(transactCache, (object) register)
    };
    FATran faTran2 = (FATran) transactCache.Insert((object) faTran1);
  }

  protected static void InsertTransferDepreciationTransaction(
    PXCache transactCache,
    ref FARegister register,
    FABookBalance bal,
    FixedAsset asset,
    FALocationHistory location,
    PX.Objects.FA.AssetProcess.FAAccounts oldAccounts,
    PX.Objects.FA.AssetProcess.FAAccounts newAccounts)
  {
    FATran faTran1 = new FATran()
    {
      AssetID = asset.AssetID,
      BookID = bal.BookID,
      TranType = "TD",
      FinPeriodID = location.PeriodID ?? bal.CurrDeprPeriod ?? bal.LastPeriod,
      TranDate = location.TransactionDate,
      CreditAccountID = newAccounts.AccumulatedDepreciationAccountID,
      CreditSubID = newAccounts.AccumulatedDepreciationSubID,
      DebitAccountID = oldAccounts.AccumulatedDepreciationAccountID,
      DebitSubID = oldAccounts.AccumulatedDepreciationSubID,
      TranAmt = bal.YtdDepreciated,
      TranDesc = PXMessages.LocalizeFormatNoPrefix("Transfer Depreciation for Asset {0}", new object[1]
      {
        (object) asset.AssetCD
      }),
      BranchID = newAccounts.BranchID,
      SrcBranchID = oldAccounts.BranchID,
      LineNbr = (int?) PXLineNbrAttribute.NewLineNbr<FATran.lineNbr>(transactCache, (object) register)
    };
    FATran faTran2 = (FATran) transactCache.Insert((object) faTran1);
  }

  protected static void UpdateAssetAccounts(
    PXCache assetCache,
    FixedAsset asset,
    PX.Objects.FA.AssetProcess.FAAccounts oldAccounts,
    PX.Objects.FA.AssetProcess.FAAccounts newAccounts)
  {
    FixedAsset copy = (FixedAsset) assetCache.CreateCopy((object) asset);
    newAccounts.CopyMinorAccountsTo(copy);
    int? disposalAccountId1 = newAccounts.DisposalAccountID;
    int? disposalAccountId2 = oldAccounts.DisposalAccountID;
    if (disposalAccountId1.GetValueOrDefault() == disposalAccountId2.GetValueOrDefault() & disposalAccountId1.HasValue == disposalAccountId2.HasValue)
    {
      int? disposalSubId = newAccounts.DisposalSubID;
      int? nullable1 = oldAccounts.DisposalSubID;
      if (disposalSubId.GetValueOrDefault() == nullable1.GetValueOrDefault() & disposalSubId.HasValue == nullable1.HasValue)
      {
        nullable1 = newAccounts.GainAcctID;
        int? nullable2 = oldAccounts.GainAcctID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = newAccounts.GainSubID;
          nullable1 = oldAccounts.GainSubID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = newAccounts.LossAcctID;
            nullable2 = oldAccounts.LossAcctID;
            if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            {
              nullable2 = newAccounts.LossSubID;
              nullable1 = oldAccounts.LossSubID;
              if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              {
                if (!asset.Depreciable.GetValueOrDefault())
                  return;
                nullable1 = newAccounts.DepreciatedExpenseAccountID;
                nullable2 = oldAccounts.DepreciatedExpenseAccountID;
                if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                {
                  nullable2 = newAccounts.DepreciatedExpenseSubID;
                  nullable1 = oldAccounts.DepreciatedExpenseSubID;
                  if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                    return;
                }
              }
            }
          }
        }
      }
    }
    assetCache.Update((object) copy);
  }

  public class AssetProcessFixedAssetChecksExtension : FixedAssetChecksExtensionBase<PX.Objects.FA.AssetProcess>
  {
  }

  protected class FAAccounts
  {
    public int? ClassID { get; set; }

    public int? BranchID { get; set; }

    public int? FAAccountID { get; set; }

    public int? FASubID { get; set; }

    public int? AccumulatedDepreciationAccountID { get; set; }

    public int? AccumulatedDepreciationSubID { get; set; }

    public int? DepreciatedExpenseAccountID { get; set; }

    public int? DepreciatedExpenseSubID { get; set; }

    public int? DisposalAccountID { get; set; }

    public int? DisposalSubID { get; set; }

    public int? GainAcctID { get; set; }

    public int? GainSubID { get; set; }

    public int? LossAcctID { get; set; }

    public int? LossSubID { get; set; }

    public FAAccounts(FixedAsset asset)
    {
      this.ClassID = asset.RecordType == "C" ? asset.AssetID : asset.ClassID;
      this.BranchID = asset.BranchID;
      this.FAAccountID = asset.FAAccountID;
      this.FASubID = asset.FASubID;
      this.AccumulatedDepreciationAccountID = asset.AccumulatedDepreciationAccountID;
      this.AccumulatedDepreciationSubID = asset.AccumulatedDepreciationSubID;
      this.DepreciatedExpenseAccountID = asset.DepreciatedExpenseAccountID;
      this.DepreciatedExpenseSubID = asset.DepreciatedExpenseSubID;
      this.DisposalAccountID = asset.DisposalAccountID;
      this.DisposalSubID = asset.DisposalSubID;
      this.GainAcctID = asset.GainAcctID;
      this.GainSubID = asset.GainSubID;
      this.LossAcctID = asset.LossAcctID;
      this.LossSubID = asset.LossSubID;
    }

    public FAAccounts(FALocationHistory history)
    {
      this.ClassID = history.ClassID;
      this.BranchID = history.LocationID;
      this.FAAccountID = history.FAAccountID;
      this.FASubID = history.FASubID;
      this.AccumulatedDepreciationAccountID = history.AccumulatedDepreciationAccountID;
      this.AccumulatedDepreciationSubID = history.AccumulatedDepreciationSubID;
      this.DepreciatedExpenseAccountID = history.DepreciatedExpenseAccountID;
      this.DepreciatedExpenseSubID = history.DepreciatedExpenseSubID;
      this.DisposalAccountID = history.DisposalAccountID;
      this.DisposalSubID = history.DisposalSubID;
      this.GainAcctID = history.GainAcctID;
      this.GainSubID = history.GainSubID;
      this.LossAcctID = history.LossAcctID;
      this.LossSubID = history.LossSubID;
    }

    public void CopyAccountsTo(FALocationHistory location)
    {
      location.ClassID = this.ClassID;
      location.BranchID = this.BranchID;
      location.FAAccountID = this.FAAccountID;
      location.FASubID = this.FASubID;
      location.AccumulatedDepreciationAccountID = this.AccumulatedDepreciationAccountID;
      location.AccumulatedDepreciationSubID = this.AccumulatedDepreciationSubID;
      location.DepreciatedExpenseAccountID = this.DepreciatedExpenseAccountID;
      location.DepreciatedExpenseSubID = this.DepreciatedExpenseSubID;
      location.DisposalAccountID = this.DisposalAccountID;
      location.DisposalSubID = this.DisposalSubID;
      location.GainAcctID = this.GainAcctID;
      location.GainSubID = this.GainSubID;
      location.LossAcctID = this.LossAcctID;
      location.LossSubID = this.LossSubID;
    }

    public void CopyMinorAccountsTo(FixedAsset asset)
    {
      asset.DepreciatedExpenseAccountID = this.DepreciatedExpenseAccountID;
      asset.DepreciatedExpenseSubID = this.DepreciatedExpenseSubID;
      asset.DisposalAccountID = this.DisposalAccountID;
      asset.DisposalSubID = this.DisposalSubID;
      asset.GainAcctID = this.GainAcctID;
      asset.GainSubID = this.GainSubID;
      asset.LossAcctID = this.LossAcctID;
      asset.LossSubID = this.LossSubID;
    }
  }
}
