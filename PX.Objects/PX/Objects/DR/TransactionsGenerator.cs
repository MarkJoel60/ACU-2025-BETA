// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.TransactionsGenerator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

public class TransactionsGenerator
{
  protected PXGraph _graph;
  protected DRDeferredCode _code;
  protected Func<Decimal, Decimal> _roundingFunction;
  private IFinPeriodRepository _finPeriodRepository;

  /// <param name="roundingFunction">
  /// An optional parameter specifying a function that would be used to round
  /// the calculated transaction amounts. If <c>null</c>, the generator will use
  /// <see cref="M:PX.Objects.CM.PXDBCurrencyAttribute.BaseRound(PX.Data.PXGraph,System.Decimal)" /> by default.
  /// </param>
  /// <param name="financialPeriodProvider">
  /// An optional parameter specifying an object that would be used to manipulate
  /// financial periods, e.g. extract a start date or an end date for a given period ID.
  /// If <c>null</c>, the generator will use <see cref="!:FinancialPeriodProvider.Default" />.
  /// </param>
  public TransactionsGenerator(
    PXGraph graph,
    DRDeferredCode code,
    IFinPeriodRepository finPeriodRepository = null,
    Func<Decimal, Decimal> roundingFunction = null)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (code == null)
      throw new ArgumentNullException(nameof (code));
    this._graph = graph;
    this._code = code;
    this._roundingFunction = roundingFunction ?? (Func<Decimal, Decimal>) (rawAmount => PXDBCurrencyAttribute.BaseRound(this._graph, rawAmount));
    this._finPeriodRepository = finPeriodRepository ?? this._graph.GetService<IFinPeriodRepository>();
  }

  public virtual IList<DRScheduleTran> GenerateTransactions(
    DRSchedule deferralSchedule,
    DRScheduleDetail scheduleDetail)
  {
    if (deferralSchedule == null)
      throw new ArgumentNullException(nameof (deferralSchedule));
    if (scheduleDetail == null)
      throw new ArgumentNullException(nameof (scheduleDetail));
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>())
      this.ValidateTerms(scheduleDetail);
    else
      this.ValidateTerms(deferralSchedule);
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(scheduleDetail.BranchID);
    Decimal deferredAmount = scheduleDetail.TotalAmt.Value;
    List<DRScheduleTran> transactions = new List<DRScheduleTran>();
    short num1 = 0;
    if (this._code.ReconNowPct.Value > 0M)
    {
      Decimal num2 = this._roundingFunction(deferredAmount * this._code.ReconNowPct.Value * 0.01M);
      deferredAmount -= num2;
      ++num1;
      DRScheduleTran drScheduleTran = new DRScheduleTran()
      {
        BranchID = scheduleDetail.BranchID,
        AccountID = scheduleDetail.AccountID,
        SubID = scheduleDetail.SubID,
        Amount = new Decimal?(num2),
        RecDate = deferralSchedule.DocDate,
        FinPeriodID = scheduleDetail.FinPeriodID,
        TranPeriodID = scheduleDetail.TranPeriodID,
        LineNbr = new int?((int) num1),
        DetailLineNbr = scheduleDetail.DetailLineNbr,
        ScheduleID = scheduleDetail.ScheduleID,
        ComponentID = scheduleDetail.ComponentID,
        Status = this.GetStatus()
      };
      transactions.Add(drScheduleTran);
    }
    bool flag1 = DeferredMethodType.RequiresTerms(this._code.Method);
    DateTime? nullable1 = new DateTime?();
    DateTime? termEndDate = new DateTime?();
    DateTime? nullable2;
    if (flag1)
    {
      bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.aSC606>();
      ref DateTime? local1 = ref nullable1;
      DateTime dateTime1;
      if (!flag2)
      {
        nullable2 = deferralSchedule.TermStartDate;
        dateTime1 = nullable2.Value;
      }
      else
      {
        nullable2 = scheduleDetail.TermStartDate;
        dateTime1 = nullable2.Value;
      }
      local1 = new DateTime?(dateTime1);
      ref DateTime? local2 = ref termEndDate;
      DateTime dateTime2;
      if (!flag2)
      {
        nullable2 = deferralSchedule.TermEndDate;
        dateTime2 = nullable2.Value;
      }
      else
      {
        nullable2 = scheduleDetail.TermEndDate;
        dateTime2 = nullable2.Value;
      }
      local2 = new DateTime?(dateTime2);
    }
    nullable2 = deferralSchedule.DocDate;
    DateTime documentDate = nullable2.Value;
    FinPeriod finPeriodByDate1 = this._finPeriodRepository.FindFinPeriodByDate(new DateTime?(documentDate), parentOrganizationId);
    int num3 = flag1 ? this.CalcOccurrences(nullable1.Value, termEndDate.Value, parentOrganizationId) : (int) this._code.Occurrences.Value;
    List<DRScheduleTran> drScheduleTranList = new List<DRScheduleTran>((int) this._code.Occurrences.Value);
    FinPeriod finPeriod1 = (FinPeriod) null;
    for (int index = 0; index < num3; ++index)
    {
      try
      {
        short? nullable3;
        if (finPeriod1 == null)
        {
          FinPeriod finPeriod2;
          if (!flag1)
          {
            IFinPeriodRepository periodRepository = this._finPeriodRepository;
            string finPeriodId = scheduleDetail.FinPeriodID;
            nullable3 = this._code.StartOffset;
            int offset = (int) nullable3.Value;
            int? organizationID = parentOrganizationId;
            finPeriod2 = periodRepository.GetOffsetPeriod(finPeriodId, offset, organizationID);
          }
          else
            finPeriod2 = this._finPeriodRepository.FindFinPeriodByDate(nullable1, parentOrganizationId);
          finPeriod1 = finPeriod2;
        }
        else
        {
          IFinPeriodRepository periodRepository = this._finPeriodRepository;
          string finPeriodId = finPeriod1.FinPeriodID;
          nullable3 = this._code.Frequency;
          int offset = (int) nullable3.Value;
          int? organizationID = parentOrganizationId;
          finPeriod1 = periodRepository.GetOffsetPeriod(finPeriodId, offset, organizationID);
        }
      }
      catch (PXFinPeriodException ex)
      {
        object[] objArray = new object[1]
        {
          (object) this._code.DeferredCodeID
        };
        throw new PXException((Exception) ex, "Deferral transactions cannot be generated for deferral code {0} because the financial periods are not configured yet. Configure financial periods for the range of time that your revenue recognition schedule covers.", objArray);
      }
      ++num1;
      string finPeriodId1 = finPeriod1.FinPeriodID;
      DateTime minimumDate = flag1 ? nullable1.Value : documentDate;
      DateTime? maximumDate;
      if (!flag1)
      {
        nullable2 = new DateTime?();
        maximumDate = nullable2;
      }
      else
        maximumDate = termEndDate;
      int? organizationID1 = parentOrganizationId;
      DateTime recognitionDate = this.GetRecognitionDate(finPeriodId1, minimumDate, maximumDate, organizationID1);
      FinPeriod finPeriodByDate2 = this._finPeriodRepository.FindFinPeriodByDate(new DateTime?(recognitionDate), parentOrganizationId);
      DRScheduleTran drScheduleTran = new DRScheduleTran()
      {
        BranchID = scheduleDetail.BranchID,
        AccountID = scheduleDetail.AccountID,
        SubID = scheduleDetail.SubID,
        RecDate = new DateTime?(recognitionDate),
        FinPeriodID = finPeriodByDate2?.FinPeriodID,
        TranPeriodID = finPeriodByDate2?.MasterFinPeriodID,
        LineNbr = new int?((int) num1),
        DetailLineNbr = scheduleDetail.DetailLineNbr,
        ScheduleID = scheduleDetail.ScheduleID,
        ComponentID = scheduleDetail.ComponentID,
        Status = this.GetStatus()
      };
      drScheduleTranList.Add(drScheduleTran);
    }
    this.SetAmounts((IList<DRScheduleTran>) drScheduleTranList, deferredAmount, deferralSchedule.DocDate, nullable1, termEndDate, parentOrganizationId);
    if (DeferredMethodType.RequiresTerms(this._code) && !this._code.RecognizeInPastPeriods.GetValueOrDefault())
    {
      foreach (DRScheduleTran drScheduleTran in drScheduleTranList.Where<DRScheduleTran>((Func<DRScheduleTran, bool>) (transaction =>
      {
        DateTime? recDate = transaction.RecDate;
        DateTime dateTime = documentDate;
        return recDate.HasValue && recDate.GetValueOrDefault() < dateTime;
      })))
      {
        drScheduleTran.RecDate = new DateTime?(documentDate);
        drScheduleTran.FinPeriodID = finPeriodByDate1?.FinPeriodID;
        drScheduleTran.TranPeriodID = finPeriodByDate1?.MasterFinPeriodID;
      }
    }
    transactions.AddRange((IEnumerable<DRScheduleTran>) drScheduleTranList);
    return (IList<DRScheduleTran>) transactions;
  }

  protected virtual string GetStatus() => !(this._code.Method == "C") ? "O" : "J";

  /// <summary>
  /// If applicable, creates a single related transaction for all original posted transactions
  /// whose recognition date is earlier than (or equal to) the current document date.
  /// Does not set any amounts.
  /// </summary>
  /// <param name="transactionList">
  /// Transaction list where the new transaction will be put (if created).
  /// </param>
  /// <param name="lineCounter">
  /// Transaction line counter. Will be incremented if any transactions are created by this procedure.
  /// </param>
  private void AddRelatedTransactionForPostedBeforeDocumentDate(
    IList<DRScheduleTran> transactionList,
    DRScheduleDetail relatedScheduleDetail,
    IEnumerable<DRScheduleTran> originalPostedTransactions,
    int? branchID,
    ref short lineCounter)
  {
    if (!originalPostedTransactions.Where<DRScheduleTran>((Func<DRScheduleTran, bool>) (transaction =>
    {
      DateTime? recDate = transaction.RecDate;
      DateTime? docDate = relatedScheduleDetail.DocDate;
      return recDate.HasValue & docDate.HasValue && recDate.GetValueOrDefault() <= docDate.GetValueOrDefault();
    })).Any<DRScheduleTran>())
      return;
    ++lineCounter;
    DRScheduleTran drScheduleTran = new DRScheduleTran()
    {
      BranchID = branchID,
      AccountID = relatedScheduleDetail.AccountID,
      SubID = relatedScheduleDetail.SubID,
      RecDate = relatedScheduleDetail.DocDate,
      FinPeriodID = relatedScheduleDetail.FinPeriodID,
      TranPeriodID = relatedScheduleDetail.TranPeriodID,
      LineNbr = new int?((int) lineCounter),
      DetailLineNbr = relatedScheduleDetail.DetailLineNbr,
      ScheduleID = relatedScheduleDetail.ScheduleID,
      ComponentID = relatedScheduleDetail.ComponentID,
      Status = "O"
    };
    transactionList.Add(drScheduleTran);
  }

  /// <summary>
  /// Adds a related transaction for every original transaction
  /// in <paramref name="originalTransactions" /> using information
  /// from the provided related <see cref="T:PX.Objects.DR.DRScheduleDetail" />.
  /// Does not set any transaction amounts.
  /// </summary>
  /// <param name="transactionList">
  /// Transaction list where the new transaction will be put (if created).
  /// </param>
  /// <param name="lineCounter">
  /// Transaction line counter. Will be incremented if any transactions are created by this procedure.
  /// </param>
  private void AddRelatedTransactions(
    IList<DRScheduleTran> transactionList,
    DRScheduleDetail relatedScheduleDetail,
    IEnumerable<DRScheduleTran> originalTransactions,
    int? branchID,
    ref short lineCounter)
  {
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(branchID);
    foreach (DRScheduleTran originalTransaction in originalTransactions)
    {
      ++lineCounter;
      DRScheduleTran drScheduleTran = new DRScheduleTran()
      {
        BranchID = branchID,
        AccountID = relatedScheduleDetail.AccountID,
        SubID = relatedScheduleDetail.SubID,
        LineNbr = new int?((int) lineCounter),
        DetailLineNbr = relatedScheduleDetail.DetailLineNbr,
        ScheduleID = relatedScheduleDetail.ScheduleID,
        ComponentID = relatedScheduleDetail.ComponentID,
        Status = "O"
      };
      DRSetup drSetup = PXResultset<DRSetup>.op_Implicit(((PXSelectBase<DRSetup>) new PXSelect<DRSetup>(this._graph)).Select(Array.Empty<object>()));
      DateTime? nullable1;
      string finPeriodID;
      if (relatedScheduleDetail.Module == "AR" && PXAccess.FeatureInstalled<FeaturesSet.aSC606>() && drSetup.RecognizeAdjustmentsInPreviousPeriods.GetValueOrDefault() && DeferredMethodType.RequiresTerms(this._code) && this._code.RecognizeInPastPeriods.GetValueOrDefault())
      {
        nullable1 = originalTransaction.RecDate;
        finPeriodID = originalTransaction.FinPeriodID;
      }
      else
      {
        short? startOffset = this._code.StartOffset;
        int? nullable2 = startOffset.HasValue ? new int?((int) startOffset.GetValueOrDefault()) : new int?();
        int num = 0;
        if (nullable2.GetValueOrDefault() < num & nullable2.HasValue)
        {
          nullable1 = originalTransaction.RecDate;
          finPeriodID = originalTransaction.FinPeriodID;
        }
        else
        {
          nullable1 = originalTransaction.RecDate.Value < relatedScheduleDetail.DocDate.Value ? relatedScheduleDetail.DocDate : originalTransaction.RecDate;
          finPeriodID = string.CompareOrdinal(originalTransaction.FinPeriodID, relatedScheduleDetail.FinPeriodID) < 0 ? relatedScheduleDetail.FinPeriodID : originalTransaction.FinPeriodID;
        }
      }
      drScheduleTran.RecDate = nullable1;
      FinPeriod byId = this._finPeriodRepository.GetByID(finPeriodID, parentOrganizationId);
      drScheduleTran.FinPeriodID = byId.FinPeriodID;
      drScheduleTran.TranPeriodID = byId.MasterFinPeriodID;
      transactionList.Add(drScheduleTran);
    }
  }

  /// <summary>
  /// During the related transaction generation, checks that the original open transaction
  /// collection doesn't contain any non-open transactions.
  /// </summary>
  private static void ValidateOpenTransactions(
    IEnumerable<DRScheduleTran> originalOpenTransactions)
  {
    if (originalOpenTransactions != null && originalOpenTransactions.Any<DRScheduleTran>((Func<DRScheduleTran, bool>) (transaction => transaction.Status != "O" && transaction.Status != "J")))
      throw new PXArgumentException(nameof (originalOpenTransactions), "The collection contains posted transactions.");
  }

  /// <summary>
  /// During the related transaction generation, checks that the original posted transaction
  /// collection doesn't contain any non-posted transactions.
  /// </summary>
  private static void ValidatePostedTransactions(
    IEnumerable<DRScheduleTran> originalPostedTransactions)
  {
    if (originalPostedTransactions != null && originalPostedTransactions.Any<DRScheduleTran>((Func<DRScheduleTran, bool>) (transaction => transaction.Status != "P")))
      throw new PXArgumentException(nameof (originalPostedTransactions), "The collection contains non-posted transactions.");
  }

  /// <summary>
  /// Generates the related transactions given the list of original
  /// </summary>
  /// <param name="relatedScheduleDetail">
  /// The schedule detail
  /// to which the related transactions will pertain.
  /// </param>
  /// <param name="originalOpenTransactions">
  /// Original transactions in the Open (or Projected) status.
  /// </param>
  /// <param name="originalPostedTransactions">
  /// Original transactions in the Posted status.
  /// </param>
  /// <param name="amountToDistributeForUnposted">
  /// Amount to distribute among the related transactions that are
  /// created for original Open transactions.
  /// </param>
  /// <param name="amountToDistributeForPosted">
  /// Amount to distribute among the related transactions that are
  /// created for original Posted transactions.
  /// </param>
  /// <param name="branchID">Branch ID for the related transactions.</param>
  /// <returns></returns>
  public virtual IList<DRScheduleTran> GenerateRelatedTransactions(
    DRScheduleDetail relatedScheduleDetail,
    IEnumerable<DRScheduleTran> originalOpenTransactions,
    IEnumerable<DRScheduleTran> originalPostedTransactions,
    Decimal amountToDistributeForUnposted,
    Decimal amountToDistributeForPosted,
    int? branchID)
  {
    TransactionsGenerator.ValidateOpenTransactions(originalOpenTransactions);
    TransactionsGenerator.ValidatePostedTransactions(originalPostedTransactions);
    List<DRScheduleTran> transactionList = new List<DRScheduleTran>();
    short lineCounter = 0;
    if (originalPostedTransactions != null && originalPostedTransactions.Any<DRScheduleTran>())
    {
      Decimal num1 = originalPostedTransactions.Sum<DRScheduleTran>((Func<DRScheduleTran, Decimal>) (transaction => transaction.Amount.GetValueOrDefault()));
      this.AddRelatedTransactionForPostedBeforeDocumentDate((IList<DRScheduleTran>) transactionList, relatedScheduleDetail, originalPostedTransactions, branchID, ref lineCounter);
      transactionsAddedDuringPreviousStep = (int) lineCounter;
      Decimal num2 = originalPostedTransactions.Where<DRScheduleTran>((Func<DRScheduleTran, bool>) (transaction =>
      {
        DateTime? recDate = transaction.RecDate;
        DateTime? docDate = relatedScheduleDetail.DocDate;
        return recDate.HasValue & docDate.HasValue && recDate.GetValueOrDefault() <= docDate.GetValueOrDefault();
      })).Sum<DRScheduleTran>((Func<DRScheduleTran, Decimal>) (transaction => transaction.Amount.GetValueOrDefault()));
      Decimal multiplier = !(num1 == 0M) || !(amountToDistributeForPosted == 0M) ? amountToDistributeForPosted / num1 : 1M;
      Decimal num3 = amountToDistributeForPosted;
      if (transactionList.Any<DRScheduleTran>())
      {
        transactionList[0].Amount = new Decimal?(multiplier * num2);
        num3 -= transactionList[0].Amount.GetValueOrDefault();
      }
      IEnumerable<DRScheduleTran> drScheduleTrans = originalPostedTransactions.Where<DRScheduleTran>((Func<DRScheduleTran, bool>) (transaction =>
      {
        DateTime? recDate = transaction.RecDate;
        DateTime? docDate = relatedScheduleDetail.DocDate;
        return recDate.HasValue & docDate.HasValue && recDate.GetValueOrDefault() > docDate.GetValueOrDefault();
      }));
      this.AddRelatedTransactions((IList<DRScheduleTran>) transactionList, relatedScheduleDetail, drScheduleTrans, branchID, ref lineCounter);
      Decimal relatedTransactionTotal = 0M;
      if (drScheduleTrans.Any<DRScheduleTran>())
      {
        drScheduleTrans.SkipLast<DRScheduleTran>(1).ForEach<DRScheduleTran>((Action<DRScheduleTran, int>) ((originalTransaction, i) =>
        {
          Decimal num4 = multiplier;
          Decimal? amount = originalTransaction.Amount;
          Decimal valueOrDefault = (amount.HasValue ? new Decimal?(num4 * amount.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
          transactionList[transactionsAddedDuringPreviousStep + i].Amount = new Decimal?(this._roundingFunction(valueOrDefault));
          relatedTransactionTotal += transactionList[transactionsAddedDuringPreviousStep + i].Amount.GetValueOrDefault();
        }));
        transactionList[transactionList.Count - 1].Amount = new Decimal?(num3 - relatedTransactionTotal);
      }
    }
    int transactionsAddedDuringPreviousStep = (int) lineCounter;
    if (originalOpenTransactions != null && originalOpenTransactions.Any<DRScheduleTran>())
    {
      Decimal num5 = originalOpenTransactions.Sum<DRScheduleTran>((Func<DRScheduleTran, Decimal>) (transaction => transaction.Amount.GetValueOrDefault()));
      this.AddRelatedTransactions((IList<DRScheduleTran>) transactionList, relatedScheduleDetail, originalOpenTransactions, branchID, ref lineCounter);
      Decimal multiplier = !(num5 == 0M) || !(amountToDistributeForUnposted == 0M) ? amountToDistributeForUnposted / num5 : 1M;
      Decimal relatedTransactionTotal = 0M;
      originalOpenTransactions.SkipLast<DRScheduleTran>(1).ForEach<DRScheduleTran>((Action<DRScheduleTran, int>) ((originalTransaction, i) =>
      {
        Decimal num6 = multiplier * originalTransaction.Amount.Value;
        transactionList[transactionsAddedDuringPreviousStep + i].Amount = new Decimal?(this._roundingFunction(num6));
        relatedTransactionTotal += transactionList[transactionsAddedDuringPreviousStep + i].Amount.GetValueOrDefault();
      }));
      transactionList[transactionList.Count - 1].Amount = new Decimal?(amountToDistributeForUnposted - relatedTransactionTotal);
    }
    else if (amountToDistributeForUnposted > 0M)
    {
      ++lineCounter;
      DRScheduleTran drScheduleTran = new DRScheduleTran()
      {
        Amount = new Decimal?(amountToDistributeForUnposted),
        BranchID = branchID,
        AccountID = relatedScheduleDetail.AccountID,
        SubID = relatedScheduleDetail.SubID,
        RecDate = relatedScheduleDetail.DocDate,
        FinPeriodID = relatedScheduleDetail.FinPeriodID,
        TranPeriodID = relatedScheduleDetail.TranPeriodID,
        LineNbr = new int?((int) lineCounter),
        DetailLineNbr = relatedScheduleDetail.DetailLineNbr,
        ScheduleID = relatedScheduleDetail.ScheduleID,
        ComponentID = relatedScheduleDetail.ComponentID,
        Status = "O"
      };
      transactionList.Add(drScheduleTran);
    }
    return (IList<DRScheduleTran>) transactionList;
  }

  /// <summary>
  /// Checks the presence and consistency of deferral term start / end dates,
  /// as well as ensures that the document date is no later than the Term End Date
  /// in case recognizing in past periods is forbidden.
  /// </summary>
  /// <param name="deferralSchedule">
  /// Deferral schedule from which the document date will be taken.
  /// </param>
  protected virtual void ValidateTerms(DRSchedule deferralSchedule)
  {
    if (DeferredMethodType.RequiresTerms(this._code))
    {
      DateTime? nullable = deferralSchedule.TermStartDate;
      if (nullable.HasValue)
      {
        nullable = deferralSchedule.TermEndDate;
        if (nullable.HasValue)
        {
          nullable = deferralSchedule.TermStartDate;
          DateTime? termEndDate = deferralSchedule.TermEndDate;
          if ((nullable.HasValue & termEndDate.HasValue ? (nullable.GetValueOrDefault() > termEndDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            return;
          throw new PXException("Term End Date ({0:d}) cannot be earlier than Term Start Date ({1:d}).", new object[2]
          {
            (object) deferralSchedule.TermEndDate,
            (object) deferralSchedule.TermStartDate
          });
        }
      }
      throw new PXException("Deferral transactions cannot be generated unless both Term Start Date and Term End Date are specified in the deferral schedule.");
    }
  }

  protected virtual void ValidateTerms(DRScheduleDetail deferralDetail)
  {
    if (DeferredMethodType.RequiresTerms(this._code))
    {
      DateTime? nullable = deferralDetail.TermStartDate;
      if (nullable.HasValue)
      {
        nullable = deferralDetail.TermEndDate;
        if (nullable.HasValue)
        {
          nullable = deferralDetail.TermStartDate;
          DateTime? termEndDate = deferralDetail.TermEndDate;
          if ((nullable.HasValue & termEndDate.HasValue ? (nullable.GetValueOrDefault() > termEndDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            return;
          throw new PXException("Term End Date ({0:d}) cannot be earlier than Term Start Date ({1:d}).", new object[2]
          {
            (object) deferralDetail.TermEndDate,
            (object) deferralDetail.TermStartDate
          });
        }
      }
      throw new PXException("Deferral transactions cannot be generated unless both Term Start Date and Term End Date are specified in the deferral schedule.");
    }
  }

  protected virtual int CalcOccurrences(DateTime startDate, DateTime endDate, int? organizationID)
  {
    if (this._finPeriodRepository.FindFinPeriodByDate(new DateTime?(startDate), organizationID) == null)
      throw new PXException("Deferral transactions cannot be generated for deferral code {0} because the financial periods that correspond to the Term Start Date or Term End Date do not exist for the company of the branch. Configure financial periods for the range of time that your revenue recognition schedule covers.", new object[1]
      {
        (object) this._code.DeferredCodeID
      });
    if (this._finPeriodRepository.FindFinPeriodByDate(new DateTime?(endDate), organizationID) == null)
      throw new PXException("Deferral transactions cannot be generated for deferral code {0} because the financial periods that correspond to the Term Start Date or Term End Date do not exist for the company of the branch. Configure financial periods for the range of time that your revenue recognition schedule covers.", new object[1]
      {
        (object) this._code.DeferredCodeID
      });
    return this._finPeriodRepository.PeriodsBetweenInclusive(startDate, endDate, organizationID).Where<FinPeriod>((Func<FinPeriod, int, bool>) ((_, periodIndex) =>
    {
      int num1 = periodIndex;
      short? frequency = this._code.Frequency;
      int? nullable1 = frequency.HasValue ? new int?((int) frequency.GetValueOrDefault()) : new int?();
      int? nullable2 = nullable1.HasValue ? new int?(num1 % nullable1.GetValueOrDefault()) : new int?();
      int num2 = 0;
      return nullable2.GetValueOrDefault() == num2 & nullable2.HasValue;
    })).Count<FinPeriod>();
  }

  /// <summary>
  /// Returns the appropriate recognition date in a given financial period,
  /// taking into account the <see cref="T:PX.Objects.DR.DRScheduleOption" /> settings of the
  /// deferral code, as well as the absolute recognition date boundaries provided
  /// as arguments to this method.
  /// </summary>
  /// <param name="finPeriod">The financial period in which recognition must happen.</param>
  /// <param name="minimumDate">The earliest date where recognition is allowed.</param>
  /// <param name="maximumDate">The latest date where recognition is allowed.</param>
  /// <returns></returns>
  protected DateTime GetRecognitionDate(
    string finPeriod,
    DateTime minimumDate,
    DateTime? maximumDate,
    int? organizationID)
  {
    DateTime dateTime1 = minimumDate;
    short? nullable1;
    switch (this._code.ScheduleOption)
    {
      case "S":
        dateTime1 = this._finPeriodRepository.PeriodStartDate(finPeriod, organizationID);
        break;
      case "E":
        dateTime1 = this._finPeriodRepository.PeriodEndDate(finPeriod, organizationID);
        break;
      case "D":
        DateTime dateTime2 = this._finPeriodRepository.PeriodStartDate(finPeriod, organizationID);
        DateTime dateTime3 = this._finPeriodRepository.PeriodEndDate(finPeriod, organizationID);
        if ((int) this._code.FixedDay.Value <= dateTime2.Day)
        {
          dateTime1 = dateTime2;
          break;
        }
        if ((int) this._code.FixedDay.Value >= dateTime3.Day)
        {
          dateTime1 = dateTime3;
          break;
        }
        ref DateTime local = ref dateTime1;
        int year = dateTime2.Year;
        int month = dateTime2.Month;
        nullable1 = this._code.FixedDay;
        int day = (int) nullable1.Value;
        local = new DateTime(year, month, day);
        break;
    }
    if (dateTime1 < minimumDate)
    {
      nullable1 = this._code.StartOffset;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num = 0;
      if (nullable2.GetValueOrDefault() >= num & nullable2.HasValue)
        return minimumDate;
    }
    DateTime dateTime4 = dateTime1;
    DateTime? nullable3 = maximumDate;
    return (nullable3.HasValue ? (dateTime4 > nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? maximumDate.Value : dateTime1;
  }

  protected virtual void SetAmounts(
    IList<DRScheduleTran> deferredTransactions,
    Decimal deferredAmount,
    DateTime? docDate,
    DateTime? termStartDate,
    DateTime? termEndDate,
    int? organizationID)
  {
    switch (this._code.Method)
    {
      case "C":
      case "E":
        this.SetAmountsEvenPeriods(deferredTransactions, deferredAmount);
        break;
      case "P":
        this.SetAmountsProrateDays(deferredTransactions, deferredAmount, docDate.Value, organizationID);
        break;
      case "D":
        this.SetAmountsExactDays(deferredTransactions, deferredAmount, organizationID);
        break;
      case "F":
        this.SetAmountsFlexibleProrateByDays(deferredTransactions, deferredAmount, termStartDate.Value, termEndDate.Value, organizationID);
        break;
      case "L":
        this.SetAmountsFlexibleByDays(deferredTransactions, deferredAmount, termStartDate.Value, termEndDate.Value, organizationID);
        break;
    }
  }

  protected void SetAmountsEvenPeriods(
    IList<DRScheduleTran> deferredTransactions,
    Decimal deferredAmount)
  {
    if (deferredTransactions.Count <= 0)
      return;
    Decimal num1 = this._roundingFunction(deferredAmount / (Decimal) deferredTransactions.Count);
    Decimal num2 = 0M;
    for (int index = 0; index < deferredTransactions.Count - 1; ++index)
    {
      deferredTransactions[index].Amount = new Decimal?(num1);
      num2 += num1;
    }
    deferredTransactions[deferredTransactions.Count - 1].Amount = new Decimal?(deferredAmount - num2);
  }

  private void SetAmountsProrateDays(
    IList<DRScheduleTran> deferredTransactions,
    Decimal deferredAmount,
    DateTime documentDate,
    int? organizationID)
  {
    if (deferredTransactions.Count <= 0)
      return;
    if (deferredTransactions.Count == 1)
    {
      deferredTransactions[0].Amount = new Decimal?(deferredAmount);
    }
    else
    {
      string finPeriodId = this._finPeriodRepository.FindFinPeriodByDate(new DateTime?(documentDate), organizationID)?.FinPeriodID;
      DateTime dateTime1 = this._finPeriodRepository.PeriodStartDate(finPeriodId, organizationID);
      short? startOffset = this._code.StartOffset;
      int? nullable = startOffset.HasValue ? new int?((int) startOffset.GetValueOrDefault()) : new int?();
      int num1 = 0;
      if (nullable.GetValueOrDefault() > num1 & nullable.HasValue || dateTime1 == documentDate)
      {
        this.SetAmountsEvenPeriods(deferredTransactions, deferredAmount);
      }
      else
      {
        DateTime dateTime2 = this._finPeriodRepository.PeriodEndDate(finPeriodId, organizationID);
        int num2 = dateTime2.Subtract(dateTime1).Days + 1;
        int days = dateTime2.Subtract(documentDate).Days;
        Decimal num3 = deferredAmount / (Decimal) (deferredTransactions.Count - 1);
        Decimal num4 = num3 * (Decimal) days / (Decimal) num2;
        Decimal num5 = this._roundingFunction(num3);
        Decimal num6 = this._roundingFunction(num4);
        deferredTransactions[0].Amount = new Decimal?(num6);
        Decimal num7 = num6;
        for (int index = 1; index < deferredTransactions.Count - 1; ++index)
        {
          deferredTransactions[index].Amount = new Decimal?(num5);
          num7 += num5;
        }
        deferredTransactions[deferredTransactions.Count - 1].Amount = new Decimal?(deferredAmount - num7);
      }
    }
  }

  private void SetAmountsExactDays(
    IList<DRScheduleTran> deferredTransactions,
    Decimal deferredAmount,
    int? organizationID)
  {
    int num1 = 0;
    foreach (DRScheduleTran deferredTransaction in (IEnumerable<DRScheduleTran>) deferredTransactions)
    {
      TimeSpan timeSpan = this._finPeriodRepository.PeriodEndDate(deferredTransaction.FinPeriodID, organizationID).Subtract(this._finPeriodRepository.PeriodStartDate(deferredTransaction.FinPeriodID, organizationID));
      num1 += timeSpan.Days + 1;
    }
    Decimal num2 = deferredAmount / (Decimal) num1;
    Decimal num3 = 0M;
    for (int index = 0; index < deferredTransactions.Count - 1; ++index)
    {
      Decimal num4 = this._roundingFunction((Decimal) (this._finPeriodRepository.PeriodEndDate(deferredTransactions[index].FinPeriodID, organizationID).Subtract(this._finPeriodRepository.PeriodStartDate(deferredTransactions[index].FinPeriodID, organizationID)).Days + 1) * num2);
      deferredTransactions[index].Amount = new Decimal?(num4);
      num3 += num4;
    }
    deferredTransactions[deferredTransactions.Count - 1].Amount = new Decimal?(deferredAmount - num3);
  }

  private void SetAmountsFlexibleByDays(
    IList<DRScheduleTran> deferredTransactions,
    Decimal deferredAmount,
    DateTime startDate,
    DateTime endDate,
    int? organizationID)
  {
    if (!deferredTransactions.Any<DRScheduleTran>())
      return;
    int num1 = (int) (endDate - startDate).TotalDays + 1;
    Decimal num2 = deferredAmount / (Decimal) num1;
    TimeSpan timeSpan;
    if (deferredTransactions.Count > 1)
    {
      DRScheduleTran deferredTransaction = deferredTransactions[0];
      timeSpan = this._finPeriodRepository.PeriodStartDate(deferredTransactions[1].FinPeriodID, organizationID).Subtract(startDate);
      Decimal? nullable = new Decimal?(this._roundingFunction((Decimal) (int) timeSpan.TotalDays * num2));
      deferredTransaction.Amount = nullable;
    }
    IEnumerable<DRScheduleTran> drScheduleTrans = deferredTransactions.Skip<DRScheduleTran>(1);
    foreach (var data in drScheduleTrans.Zip(drScheduleTrans.Skip<DRScheduleTran>(1), (tran, next) => new
    {
      Tran = tran,
      NextTran = next
    }))
    {
      DateTime dateTime = this._finPeriodRepository.PeriodStartDate(data.Tran.FinPeriodID, organizationID);
      timeSpan = this._finPeriodRepository.PeriodStartDate(data.NextTran.FinPeriodID, organizationID).Subtract(dateTime);
      int totalDays = (int) timeSpan.TotalDays;
      data.Tran.Amount = new Decimal?(this._roundingFunction((Decimal) totalDays * num2));
    }
    DRScheduleTran drScheduleTran = deferredTransactions.Last<DRScheduleTran>();
    drScheduleTran.Amount = new Decimal?(0M);
    Decimal num3 = deferredAmount;
    Decimal? nullable1 = deferredTransactions.Sum<DRScheduleTran>((Func<DRScheduleTran, Decimal?>) (t => t.Amount));
    drScheduleTran.Amount = nullable1.HasValue ? new Decimal?(num3 - nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void SetAmountsFlexibleProrateByDays(
    IList<DRScheduleTran> deferredTransactions,
    Decimal deferredAmount,
    DateTime startDate,
    DateTime endDate,
    int? organizationID)
  {
    IEnumerable<FinPeriod> source = this._finPeriodRepository.PeriodsBetweenInclusive(startDate, endDate, organizationID);
    Decimal num1 = 1.0M - this.PeriodProportionByDays(startDate, organizationID, -1);
    Decimal num2 = this.PeriodProportionByDays(endDate, organizationID);
    Decimal num3 = source.Count<FinPeriod>() == 1 ? 1M : (Decimal) (source.Count<FinPeriod>() - 2) + num1 + num2;
    Decimal num4 = deferredAmount / num3;
    foreach (var data in deferredTransactions.Zip(deferredTransactions.Skip<DRScheduleTran>(1), (tran, next) => new
    {
      Tran = tran,
      NextTran = next
    }))
    {
      var item = data;
      Decimal num5 = (Decimal) source.SkipWhile<FinPeriod>((Func<FinPeriod, bool>) (p => p.FinPeriodID != item.Tran.FinPeriodID)).TakeWhile<FinPeriod>((Func<FinPeriod, bool>) (p => p.FinPeriodID != item.NextTran.FinPeriodID)).Count<FinPeriod>();
      if (item.Tran.FinPeriodID == source.First<FinPeriod>().FinPeriodID)
        num5 += num1 - 1.0M;
      item.Tran.Amount = new Decimal?(this._roundingFunction(num4 * num5));
    }
    DRScheduleTran drScheduleTran = deferredTransactions.Last<DRScheduleTran>();
    drScheduleTran.Amount = new Decimal?(0M);
    Decimal num6 = deferredAmount;
    Decimal? nullable = deferredTransactions.Sum<DRScheduleTran>((Func<DRScheduleTran, Decimal?>) (t => t.Amount));
    drScheduleTran.Amount = new Decimal?(this._roundingFunction((nullable.HasValue ? new Decimal?(num6 - nullable.GetValueOrDefault()) : new Decimal?()).Value));
  }

  private Decimal PeriodProportionByDays(DateTime date, int? organizationID, int shift = 0)
  {
    FinPeriod finPeriodByDate = this._finPeriodRepository.FindFinPeriodByDate(new DateTime?(date), organizationID);
    if (finPeriodByDate == null)
      throw new PXException("Deferral transactions cannot be generated for deferral code {0} because the financial periods that correspond to the Term Start Date or Term End Date do not exist for the company of the branch. Configure financial periods for the range of time that your revenue recognition schedule covers.", new object[1]
      {
        (object) this._code.DeferredCodeID
      });
    DateTime dateTime = this._finPeriodRepository.PeriodStartDate(finPeriodByDate.FinPeriodID, organizationID);
    int num = (int) this._finPeriodRepository.PeriodEndDate(finPeriodByDate.FinPeriodID, organizationID).Subtract(dateTime).TotalDays + 1;
    return (Decimal) (date.Subtract(dateTime).TotalDays + 1.0 + (double) shift) / (Decimal) num;
  }
}
