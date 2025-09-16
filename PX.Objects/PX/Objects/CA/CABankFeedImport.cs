// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.CCProcessingBase;
using PX.Common;
using PX.Data;
using PX.Objects.CA.BankFeed;
using PX.Objects.CA.Descriptor;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.EP.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.CA;

public class CABankFeedImport : PXGraph<CABankFeedImport>
{
  public PXCancel<CABankFeed> Cancel;
  public PXAction<CABankFeed> ViewBankFeed;
  public PXAction<CABankFeed> AddBankFeed;
  public PXAction<CABankFeed> EditBankFeed;
  [PXFilterable(new Type[] {})]
  public PXProcessing<CABankFeed> BankFeeds;
  protected IEnumerable<CABankFeedDetail> BankFeedDetails;
  protected IEnumerable<CABankFeedCorpCard> CorpCardDetails;
  protected IEnumerable<CABankFeedExpense> ExpenseDetails;
  protected IEnumerable<CABankFeedAccountMapping> AccountMappingDetails;
  protected BankFeedTransactionsImport ImportGraph;
  protected ExpenseClaimDetailEntry ExpenseGraph;
  protected CABankMatchingProcess MatchingGraph;
  protected CABankTranHeader lastProcessedTranHeader;
  protected int TranRecordProcessed;
  private readonly SyFormulaProcessor _formulaProcessor = new SyFormulaProcessor();

  [InjectDependency]
  internal Func<string, BankFeedManager> BankFeedManagerProvider { get; set; }

  [PXButton]
  [PXUIField(DisplayName = "")]
  protected virtual void viewBankFeed()
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeeds).Current;
    CABankFeedMaint instance = PXGraph.CreateInstance<CABankFeedMaint>();
    PXSelectBase<CABankFeed> pxSelectBase = (PXSelectBase<CABankFeed>) new PXSelect<CABankFeed, Where<CABankFeed.bankFeedID, Equal<Required<CABankFeed.bankFeedID>>>>((PXGraph) this);
    ((PXSelectBase<CABankFeed>) instance.BankFeed).Current = pxSelectBase.SelectSingle(new object[1]
    {
      (object) current.BankFeedID
    });
    if (((PXSelectBase<CABankFeed>) instance.BankFeed).Current != null)
      throw new PXRedirectRequiredException((PXGraph) instance, false, string.Empty);
  }

  [PXUIField(DisplayName = "")]
  [PXInsertButton(CommitChanges = true)]
  public virtual void addBankFeed()
  {
    CABankFeedMaint instance = PXGraph.CreateInstance<CABankFeedMaint>();
    ((PXSelectBase<CABankFeed>) instance.BankFeed).Current = ((PXSelectBase<CABankFeed>) instance.BankFeed).Insert();
    ((PXSelectBase) instance.BankFeed).Cache.IsDirty = false;
    throw new PXRedirectRequiredException((PXGraph) instance, false, string.Empty);
  }

  [PXUIField(DisplayName = "")]
  [PXEditDetailButton(CommitChanges = true)]
  public virtual void editBankFeed()
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeeds).Current;
    CABankFeedMaint instance = PXGraph.CreateInstance<CABankFeedMaint>();
    ((PXSelectBase<CABankFeed>) instance.BankFeed).Current = PXResultset<CABankFeed>.op_Implicit(((PXSelectBase<CABankFeed>) instance.BankFeed).Search<CABankFeed.bankFeedID>((object) current.BankFeedID, Array.Empty<object>()));
    throw new PXRedirectRequiredException((PXGraph) instance, false, string.Empty);
  }

  public CABankFeedImport()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<CABankFeed>) this.BankFeeds).SetProcessDelegate(new PXProcessingBase<CABankFeed>.ProcessListDelegate((object) new CABankFeedImport.\u003C\u003Ec__DisplayClass21_0()
    {
      guid = (Guid) ((PXGraph) this).UID
    }, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  public virtual async Task<Dictionary<int, string>> DoImportTransactionsAndCreateReceipts(
    CABankFeed bankFeed,
    Guid guid)
  {
    return !this.AllowBatchDownloading(bankFeed, this.BankFeedDetails) ? await this.DoImportTransSingleModeAndCreateReceipts(bankFeed, guid) : await this.DoImportTransBatchModeAndCreateReceipts(bankFeed, guid);
  }

  public virtual async Task<Dictionary<int, string>> DoImportTransaAndCreateReceiptsForAccounts(
    CABankFeed bankFeed,
    List<CABankFeedDetail> details,
    Guid guid)
  {
    return !this.AllowBatchDownloading(bankFeed, (IEnumerable<CABankFeedDetail>) details) ? await this.DoImportTransSingleModeAndCreateReceiptsForAccounts(bankFeed, details, guid) : await this.DoImportTransBatchModeAndCreateReceiptsForAccounts(bankFeed, details, guid);
  }

  public virtual void UpdateRetrievalStatus(
    CABankFeed item,
    string status,
    string message = null,
    DateTime? time = null)
  {
    item.RetrievalStatus = status;
    item.RetrievalDate = new DateTime?(time ?? PXTimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, LocaleInfo.GetTimeZone()));
    item.ErrorMessage = this.ShrinkMessage(message);
    this.PersistUpdatedData(item);
  }

  public virtual void UpdateRetrievalStatus(
    CABankFeed item,
    BankFeedImportException importException)
  {
    item.RetrievalStatus = importException.Reason != BankFeedImportException.ExceptionReason.LoginFailed ? "E" : "L";
    item.RetrievalDate = new DateTime?(importException.ErrorTime);
    item.ErrorMessage = this.ShrinkMessage(((Exception) importException).Message);
    this.PersistUpdatedData(item);
  }

  protected virtual async Task<Dictionary<int, string>> DoImportTransBatchModeAndCreateReceiptsForAccounts(
    CABankFeed bankFeed,
    List<CABankFeedDetail> details,
    Guid guid)
  {
    BankFeedImportException importException = (BankFeedImportException) null;
    Dictionary<int, string> lastUpdatedStatements = new Dictionary<int, string>();
    if (this.BankFeedDetails.Count<CABankFeedDetail>() == 0)
      return lastUpdatedStatements;
    foreach (KeyValuePair<DateTime, List<CABankFeedDetail>> detailsIntoBatch in this.GroupDetailsIntoBatches(details.Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.CashAccountID.HasValue && this.BankFeedDetails.Any<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (ii =>
    {
      int? lineNbr1 = ii.LineNbr;
      int? lineNbr2 = i.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue && ii.BankFeedID == i.BankFeedID;
    })))).Select<CABankFeedDetail, CABankFeedDetail>((Func<CABankFeedDetail, CABankFeedDetail>) (i =>
    {
      i.ImportStartDate = new DateTime?(CABankFeedMaint.GetImportStartDate((PXGraph) this, bankFeed, i));
      return i;
    }))))
    {
      DateTime key1 = detailsIntoBatch.Key;
      List<CABankFeedDetail> detailsInBatch = detailsIntoBatch.Value;
      List<BankFeedTransaction> list = (await this.GetTransactions(bankFeed, (IEnumerable<CABankFeedDetail>) detailsInBatch, key1)).ToList<BankFeedTransaction>();
      int tranInd = 0;
      foreach (CABankFeedDetail detail in detailsInBatch)
      {
        ImportResult receipts = this.DoImportTransBatchModeAndCreateReceipts(bankFeed, detail, guid, list, tranInd);
        tranInd = receipts.TransactionIndex;
        if (receipts.IsOk)
        {
          int key2 = detail.CashAccountID.Value;
          if (!lastUpdatedStatements.ContainsKey(key2) && receipts.LastUpdatedStatementRefNbr != null)
            lastUpdatedStatements.Add(key2, receipts.LastUpdatedStatementRefNbr);
        }
        else
          importException = receipts.ImportException;
      }
      detailsInBatch = (List<CABankFeedDetail>) null;
    }
    if (importException != null)
      throw importException;
    return lastUpdatedStatements;
  }

  protected virtual async Task<Dictionary<int, string>> DoImportTransBatchModeAndCreateReceipts(
    CABankFeed bankFeed,
    Guid guid)
  {
    return await this.DoImportTransBatchModeAndCreateReceiptsForAccounts(bankFeed, this.BankFeedDetails.ToList<CABankFeedDetail>(), guid);
  }

  protected virtual async Task<Dictionary<int, string>> DoImportTransSingleModeAndCreateReceipts(
    CABankFeed bankFeed,
    Guid guid)
  {
    return await this.DoImportTransSingleModeAndCreateReceiptsForAccounts(bankFeed, this.BankFeedDetails.ToList<CABankFeedDetail>(), guid);
  }

  protected virtual async Task<Dictionary<int, string>> DoImportTransSingleModeAndCreateReceiptsForAccounts(
    CABankFeed bankFeed,
    List<CABankFeedDetail> details,
    Guid guid)
  {
    BankFeedImportException importException = (BankFeedImportException) null;
    Dictionary<int, string> lastUpdatedStatements = new Dictionary<int, string>();
    foreach (CABankFeedDetail detail in this.BankFeedDetails.Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.CashAccountID.HasValue && details.Any<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (ii =>
    {
      int? lineNbr1 = ii.LineNbr;
      int? lineNbr2 = i.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue && ii.BankFeedID == i.BankFeedID;
    })))))
    {
      ImportResult receiptsForAccount = await this.DoImportTransAndCreateReceiptsForAccount(bankFeed, detail, guid);
      if (receiptsForAccount.IsOk)
      {
        int key = detail.CashAccountID.Value;
        if (!lastUpdatedStatements.ContainsKey(key) && receiptsForAccount.LastUpdatedStatementRefNbr != null)
          lastUpdatedStatements.Add(key, receiptsForAccount.LastUpdatedStatementRefNbr);
      }
      else
        importException = receiptsForAccount.ImportException;
    }
    if (importException != null)
      throw importException;
    Dictionary<int, string> receiptsForAccounts = lastUpdatedStatements;
    importException = (BankFeedImportException) null;
    lastUpdatedStatements = (Dictionary<int, string>) null;
    return receiptsForAccounts;
  }

  protected virtual ImportResult DoImportTransBatchModeAndCreateReceipts(
    CABankFeed bankFeed,
    CABankFeedDetail detail,
    Guid guid,
    List<BankFeedTransaction> trans,
    int tranInd)
  {
    BankFeedImportException feedImportException = (BankFeedImportException) null;
    string str = (string) null;
    try
    {
      this.TranRecordProcessed = 0;
      using (new CAMatchProcessContext(this.GetMatchingGraph(), detail.CashAccountID, new Guid?(guid)))
      {
        for (int index = tranInd; index < trans.Count; ++index)
        {
          tranInd = index;
          BankFeedTransaction tran = trans[tranInd];
          this.DisableSubordinationCheck(this.GetExpenseGraph());
          if (!(tran.AccountID != detail.AccountID))
          {
            DateTime? date = tran.Date;
            DateTime? importStartDate = detail.ImportStartDate;
            if ((date.HasValue & importStartDate.HasValue ? (date.GetValueOrDefault() < importStartDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            {
              using (PXTransactionScope transactionScope = new PXTransactionScope())
              {
                this.BeforeImportTransaction(tran, bankFeed, detail);
                CABankTran bankTran = (CABankTran) null;
                bool? nullable = tran.Pending;
                bool flag1 = false;
                if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
                {
                  bankTran = this.CreateOrUpdateBankStatement(tran, bankFeed, detail, (CABankTransactionsImport) this.GetImportGraph());
                  if (bankTran != null)
                    str = bankTran.HeaderRefNbr;
                }
                nullable = bankFeed.CreateExpenseReceipt;
                if (nullable.GetValueOrDefault() && bankFeed.DefaultExpenseItemID.HasValue)
                {
                  Decimal? amount = tran.Amount;
                  Decimal num = 0M;
                  if (amount.GetValueOrDefault() > num & amount.HasValue)
                  {
                    nullable = tran.Pending;
                    bool flag2 = false;
                    if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) || bankTran == null)
                    {
                      nullable = tran.Pending;
                      if (nullable.GetValueOrDefault())
                      {
                        nullable = bankFeed.CreateReceiptForPendingTran;
                        if (!nullable.GetValueOrDefault())
                          goto label_17;
                      }
                      else
                        goto label_17;
                    }
                    if (PXAccess.FeatureInstalled<FeaturesSet.expenseManagement>())
                    {
                      EPExpenseClaimDetails updateExpenseReceipt = this.CreateOrUpdateExpenseReceipt(tran, bankFeed, detail, this.GetExpenseGraph());
                      if (updateExpenseReceipt != null && bankTran != null)
                        this.MatchTransactionAndReceipt(bankTran, updateExpenseReceipt, this.GetMatchingGraph());
                    }
                  }
                }
label_17:
                transactionScope.Complete();
              }
            }
          }
          else
            break;
        }
      }
      this.UpdateRetrievalStatusForDetail(detail, "S");
      this.LogTranRecordProcessedCount(detail);
    }
    catch (Exception ex)
    {
      feedImportException = this.HandleExceptions(ex, detail);
    }
    return new ImportResult()
    {
      ImportException = feedImportException,
      IsOk = feedImportException == null,
      LastUpdatedStatementRefNbr = str,
      TransactionIndex = tranInd
    };
  }

  protected virtual async Task<ImportResult> DoImportTransAndCreateReceiptsForAccount(
    CABankFeed bankFeed,
    CABankFeedDetail detail,
    Guid guid)
  {
    CABankFeedImport graph = this;
    BankFeedImportException importException = (BankFeedImportException) null;
    string headerRefNbr = (string) null;
    try
    {
      graph.TranRecordProcessed = 0;
      DateTime importStartDate1 = CABankFeedMaint.GetImportStartDate((PXGraph) graph, bankFeed, detail);
      IEnumerable<BankFeedTransaction> transactions = await graph.GetTransactions(bankFeed, (IEnumerable<CABankFeedDetail>) new CABankFeedDetail[1]
      {
        detail
      }, importStartDate1);
      using (new CAMatchProcessContext(graph.GetMatchingGraph(), detail.CashAccountID, new Guid?(guid)))
      {
        foreach (BankFeedTransaction tran in transactions)
        {
          graph.DisableSubordinationCheck(graph.GetExpenseGraph());
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            if (detail != null)
            {
              if (detail.ImportStartDate.HasValue)
              {
                DateTime? importStartDate2 = detail.ImportStartDate;
                DateTime? date = tran.Date;
                if ((importStartDate2.HasValue & date.HasValue ? (importStartDate2.GetValueOrDefault() <= date.GetValueOrDefault() ? 1 : 0) : 0) == 0)
                  goto label_20;
              }
              graph.BeforeImportTransaction(tran, bankFeed, detail);
              CABankTran bankTran = (CABankTran) null;
              bool? nullable = tran.Pending;
              bool flag1 = false;
              if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
              {
                bankTran = graph.CreateOrUpdateBankStatement(tran, bankFeed, detail, (CABankTransactionsImport) graph.GetImportGraph());
                if (bankTran != null)
                  headerRefNbr = bankTran.HeaderRefNbr;
              }
              nullable = bankFeed.CreateExpenseReceipt;
              if (nullable.GetValueOrDefault() && bankFeed.DefaultExpenseItemID.HasValue)
              {
                Decimal? amount = tran.Amount;
                Decimal num = 0M;
                if (amount.GetValueOrDefault() > num & amount.HasValue)
                {
                  nullable = tran.Pending;
                  bool flag2 = false;
                  if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) || bankTran == null)
                  {
                    nullable = tran.Pending;
                    if (nullable.GetValueOrDefault())
                    {
                      nullable = bankFeed.CreateReceiptForPendingTran;
                      if (!nullable.GetValueOrDefault())
                        goto label_20;
                    }
                    else
                      goto label_20;
                  }
                  if (PXAccess.FeatureInstalled<FeaturesSet.expenseManagement>())
                  {
                    EPExpenseClaimDetails updateExpenseReceipt = graph.CreateOrUpdateExpenseReceipt(tran, bankFeed, detail, graph.GetExpenseGraph());
                    if (updateExpenseReceipt != null && bankTran != null)
                      graph.MatchTransactionAndReceipt(bankTran, updateExpenseReceipt, graph.GetMatchingGraph());
                  }
                }
              }
            }
label_20:
            transactionScope.Complete();
          }
        }
      }
      graph.UpdateRetrievalStatusForDetail(detail, "S");
      graph.LogTranRecordProcessedCount(detail);
    }
    catch (Exception ex)
    {
      importException = graph.HandleExceptions(ex, detail);
    }
    ImportResult receiptsForAccount = new ImportResult()
    {
      ImportException = importException,
      IsOk = importException == null,
      LastUpdatedStatementRefNbr = headerRefNbr
    };
    importException = (BankFeedImportException) null;
    headerRefNbr = (string) null;
    return receiptsForAccount;
  }

  protected virtual void UpdateRetrievalStatusForDetail(
    CABankFeedDetail item,
    string status,
    string message = null,
    DateTime? time = null)
  {
    item.RetrievalStatus = status;
    item.RetrievalDate = new DateTime?(time ?? PXTimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, LocaleInfo.GetTimeZone()));
    item.ErrorMessage = message;
    if (this.TranRecordProcessed >= 1)
      item.OverrideDate = new bool?(false);
    this.PersistUpdatedData(item);
  }

  protected virtual void UpdateRetrievalStatusForDetail(
    CABankFeedDetail item,
    BankFeedImportException importException)
  {
    item.RetrievalStatus = importException.Reason != BankFeedImportException.ExceptionReason.LoginFailed ? "E" : "L";
    if (this.TranRecordProcessed >= 1)
      item.OverrideDate = new bool?(false);
    item.RetrievalDate = new DateTime?(importException.ErrorTime);
    item.ErrorMessage = this.ShrinkMessage(((Exception) importException).Message);
    this.PersistUpdatedData(item);
  }

  protected virtual void PersistUpdatedData(CABankFeed item)
  {
    ((PXGraph) this).Caches[typeof (CABankFeed)].Update((object) item);
    ((PXGraph) this).Caches[typeof (CABankFeed)].Persist((PXDBOperation) 1);
  }

  protected virtual void PersistUpdatedData(CABankFeedDetail item)
  {
    ((PXGraph) this).Caches[typeof (CABankFeedDetail)].Update((object) item);
    ((PXGraph) this).Caches[typeof (CABankFeedDetail)].Persist((PXDBOperation) 1);
  }

  protected virtual CABankTran CreateOrUpdateBankStatement(
    BankFeedTransaction tran,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail,
    CABankTransactionsImport importGraph)
  {
    CABankTranHeader caBankTranHeader = this.AccountMappingDetails.Where<CABankFeedAccountMapping>((Func<CABankFeedAccountMapping, bool>) (i =>
    {
      if (!(i.BankFeedID == bankFeedDetail.BankFeedID))
        return false;
      int? lineNbr1 = i.LineNbr;
      int? lineNbr2 = bankFeedDetail.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })).FirstOrDefault<CABankFeedAccountMapping>() == null || !bankFeed.MultipleMapping.GetValueOrDefault() ? this.GetTransHeaderForSingleMapping(tran, bankFeed, bankFeedDetail, importGraph) : this.GetTransHeaderForMultipleMapping(tran, bankFeed, bankFeedDetail, importGraph);
    if (caBankTranHeader == null)
      return (CABankTran) null;
    CABankTran updateBankStatement = (CABankTran) null;
    foreach (PXResult<CABankTran> pxResult in ((PXSelectBase<CABankTran>) importGraph.Details).Select(Array.Empty<object>()))
    {
      CABankTran caBankTran = PXResult<CABankTran>.op_Implicit(pxResult);
      if (caBankTran.ExtTranID == tran.TransactionID)
      {
        updateBankStatement = caBankTran;
        break;
      }
    }
    if (updateBankStatement == null)
    {
      updateBankStatement = this.AddBankTransactionToStatement(tran, bankFeed, bankFeedDetail, importGraph);
      if (updateBankStatement != null)
        ++this.TranRecordProcessed;
    }
    this.lastProcessedTranHeader = caBankTranHeader;
    return updateBankStatement;
  }

  protected virtual CABankTran AddBankTransactionToStatement(
    BankFeedTransaction tran,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail,
    CABankTransactionsImport importGraph)
  {
    CABankTran bankTran = (CABankTran) null;
    try
    {
      CABankFeedAccountMapping feedAccountMapping = this.AccountMappingDetails.Where<CABankFeedAccountMapping>((Func<CABankFeedAccountMapping, bool>) (i =>
      {
        if (!(i.BankFeedID == bankFeedDetail.BankFeedID))
          return false;
        int? lineNbr1 = i.LineNbr;
        int? lineNbr2 = bankFeedDetail.LineNbr;
        return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
      })).FirstOrDefault<CABankFeedAccountMapping>();
      bankTran = ((PXSelectBase<CABankTran>) importGraph.Details).Insert();
      CABankTranFeedSource extension = PXCache<CABankTran>.GetExtension<CABankTranFeedSource>(bankTran);
      bankTran.ExtRefNbr = tran.TransactionID;
      bankTran.ExtTranID = tran.TransactionID;
      bankTran.TranDate = tran.Date;
      bankTran.TranDesc = Extentions.Truncate(tran.Name, 512 /*0x0200*/);
      CABankTran caBankTran1 = bankTran;
      Decimal? amount = tran.Amount;
      Decimal num1 = 0M;
      Decimal? nullable1 = amount.GetValueOrDefault() >= num1 & amount.HasValue ? tran.Amount : new Decimal?(0M);
      caBankTran1.CuryCreditAmt = nullable1;
      CABankTran caBankTran2 = bankTran;
      amount = tran.Amount;
      Decimal num2 = 0M;
      Decimal? nullable2;
      if (!(amount.GetValueOrDefault() < num2 & amount.HasValue))
      {
        nullable2 = new Decimal?(0M);
      }
      else
      {
        Decimal num3 = (Decimal) -1;
        amount = tran.Amount;
        nullable2 = amount.HasValue ? new Decimal?(num3 * amount.GetValueOrDefault()) : new Decimal?();
      }
      caBankTran2.CuryDebitAmt = nullable2;
      this.TransferMappedValues(bankFeed.BankFeedID, bankTran, tran);
      extension.Source = bankFeed.Type;
      if (feedAccountMapping != null)
        extension.BankFeedAccountMapID = feedAccountMapping.BankFeedAccountMapID;
      ((PXSelectBase<CABankTran>) importGraph.Details).Update(bankTran);
      CABankTranHeader current = ((PXSelectBase<CABankTranHeader>) importGraph.Header).Current;
      current.CuryEndBalance = current.CuryDetailsEndBalance;
      ((PXSelectBase<CABankTranHeader>) importGraph.Header).Update(current);
      ((PXGraph) importGraph).Persist();
    }
    catch
    {
      if (bankTran != null)
        ((PXSelectBase) importGraph.Details).Cache.Remove((object) bankTran);
      throw;
    }
    return bankTran;
  }

  protected virtual CABankTranHeader GetTransHeaderForMultipleMapping(
    BankFeedTransaction tran,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail,
    CABankTransactionsImport importGraph)
  {
    bool flag = false;
    CABankTranHeader forMultipleMapping = (CABankTranHeader) null;
    CABankFeedImport.StatementDates statementDates = this.GetStatementDates(bankFeedDetail, tran.Date.Value);
    if (this.lastProcessedTranHeader != null)
    {
      int? cashAccountId1 = this.lastProcessedTranHeader.CashAccountID;
      int? cashAccountId2 = bankFeedDetail.CashAccountID;
      if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
      {
        DateTime startDate1 = statementDates.StartDate;
        DateTime? nullable = this.lastProcessedTranHeader.StartBalanceDate;
        if ((nullable.HasValue ? (startDate1 == nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          DateTime endDate1 = statementDates.EndDate;
          nullable = this.lastProcessedTranHeader.EndBalanceDate;
          if ((nullable.HasValue ? (endDate1 == nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            DateTime startDate2 = statementDates.StartDate;
            nullable = tran.Date;
            if ((nullable.HasValue ? (startDate2 <= nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            {
              DateTime endDate2 = statementDates.EndDate;
              nullable = tran.Date;
              if ((nullable.HasValue ? (endDate2 >= nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                forMultipleMapping = this.lastProcessedTranHeader;
                flag = true;
              }
            }
          }
        }
      }
    }
    if (!flag)
    {
      CABankTranHeader headerByStartEndDate = this.GetBankTranHeaderByStartEndDate(bankFeedDetail.CashAccountID, statementDates.StartDate, statementDates.EndDate, (PXGraph) importGraph);
      if (headerByStartEndDate != null)
      {
        forMultipleMapping = headerByStartEndDate;
        ((PXSelectBase<CABankTranHeader>) importGraph.Header).Current = forMultipleMapping;
        flag = true;
      }
    }
    if (!flag)
      forMultipleMapping = ((PXSelectBase<CABankTranHeader>) importGraph.Header).Insert(new CABankTranHeader()
      {
        CashAccountID = bankFeedDetail.CashAccountID,
        DocDate = new DateTime?(statementDates.EndDate),
        StartBalanceDate = new DateTime?(statementDates.StartDate),
        EndBalanceDate = new DateTime?(statementDates.EndDate)
      });
    return forMultipleMapping;
  }

  protected virtual CABankTranHeader GetTransHeaderForSingleMapping(
    BankFeedTransaction tran,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail,
    CABankTransactionsImport importGraph)
  {
    bool flag = false;
    CABankTranHeader forSingleMapping = (CABankTranHeader) null;
    if (this.lastProcessedTranHeader != null)
    {
      int? cashAccountId1 = this.lastProcessedTranHeader.CashAccountID;
      int? cashAccountId2 = bankFeedDetail.CashAccountID;
      if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
      {
        DateTime? startBalanceDate = this.lastProcessedTranHeader.StartBalanceDate;
        DateTime? date1 = tran.Date;
        if ((startBalanceDate.HasValue & date1.HasValue ? (startBalanceDate.GetValueOrDefault() <= date1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          DateTime? endBalanceDate = this.lastProcessedTranHeader.EndBalanceDate;
          DateTime? date2 = tran.Date;
          if ((endBalanceDate.HasValue & date2.HasValue ? (endBalanceDate.GetValueOrDefault() >= date2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            forSingleMapping = this.lastProcessedTranHeader;
            flag = true;
          }
        }
      }
    }
    DateTime? nullable1;
    DateTime? nullable2;
    if (!flag)
    {
      forSingleMapping = this.GetLatestBankTranHeaderWithTransByCashAccount(bankFeedDetail.CashAccountID, (PXGraph) importGraph);
      DateTime? endBalanceDate = (DateTime?) forSingleMapping?.EndBalanceDate;
      nullable1 = tran.Date;
      if ((endBalanceDate.HasValue & nullable1.HasValue ? (endBalanceDate.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = (DateTime?) forSingleMapping?.StartBalanceDate;
        nullable2 = tran.Date;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          return (CABankTranHeader) null;
        ((PXSelectBase<CABankTranHeader>) importGraph.Header).Current = forSingleMapping;
        flag = true;
      }
    }
    if (!flag)
    {
      CABankTranHeader headerByCashAccount = this.GetLatestBankTranHeaderByCashAccount(bankFeedDetail.CashAccountID, (PXGraph) importGraph);
      if (headerByCashAccount != null)
      {
        forSingleMapping = headerByCashAccount;
        nullable2 = (DateTime?) forSingleMapping?.EndBalanceDate;
        nullable1 = tran.Date;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          nullable1 = forSingleMapping.StartBalanceDate;
          nullable2 = tran.Date;
          if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            ((PXSelectBase<CABankTranHeader>) importGraph.Header).Current = forSingleMapping;
            flag = true;
          }
        }
      }
    }
    if (!flag)
    {
      CABankFeedDetail bankFeedDet = bankFeedDetail;
      nullable2 = tran.Date;
      DateTime date = nullable2.Value;
      CABankFeedImport.StatementDates statementDates1 = this.GetStatementDates(bankFeedDet, date);
      if (forSingleMapping != null)
      {
        DateTime startDate = statementDates1.StartDate;
        nullable2 = forSingleMapping.EndBalanceDate;
        if ((nullable2.HasValue ? (startDate <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          DateTime endDate = statementDates1.EndDate;
          nullable2 = forSingleMapping.EndBalanceDate;
          if ((nullable2.HasValue ? (endDate > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            CABankFeedImport.StatementDates statementDates2 = statementDates1;
            nullable2 = forSingleMapping.EndBalanceDate;
            DateTime dateTime = nullable2.Value.AddDays(1.0);
            statementDates2.StartDate = dateTime;
            goto label_24;
          }
        }
      }
      if (forSingleMapping != null)
      {
        DateTime startDate = statementDates1.StartDate;
        nullable2 = forSingleMapping.StartBalanceDate;
        if ((nullable2.HasValue ? (startDate < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          DateTime endDate = statementDates1.EndDate;
          nullable2 = forSingleMapping.StartBalanceDate;
          if ((nullable2.HasValue ? (endDate >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            CABankFeedImport.StatementDates statementDates3 = statementDates1;
            nullable2 = forSingleMapping.StartBalanceDate;
            DateTime dateTime = nullable2.Value.AddDays(-1.0);
            statementDates3.EndDate = dateTime;
          }
        }
      }
label_24:
      forSingleMapping = new CABankTranHeader();
      forSingleMapping.CashAccountID = bankFeedDetail.CashAccountID;
      forSingleMapping.DocDate = new DateTime?(statementDates1.EndDate);
      forSingleMapping.StartBalanceDate = new DateTime?(statementDates1.StartDate);
      forSingleMapping.EndBalanceDate = new DateTime?(statementDates1.EndDate);
      ((PXSelectBase<CABankTranHeader>) importGraph.Header).Insert(forSingleMapping);
    }
    return forSingleMapping;
  }

  protected virtual string CalcValue(string formula, BankFeedTransaction bankFeedTransaction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedImport.\u003C\u003Ec__DisplayClass41_0 cDisplayClass410 = new CABankFeedImport.\u003C\u003Ec__DisplayClass41_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass410.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass410.bankFeedTransaction = bankFeedTransaction;
    string empty = string.Empty;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass410.cABankFeedMappingSourceHelper = new CABankFeedMappingSourceHelper(GraphHelper.GetPrimaryCache((PXGraph) this));
    // ISSUE: method pointer
    SyFormulaFinalDelegate formulaFinalDelegate = new SyFormulaFinalDelegate((object) cDisplayClass410, __methodptr(\u003CCalcValue\u003Eb__0));
    return this._formulaProcessor.Evaluate(formula, formulaFinalDelegate)?.ToString();
  }

  protected virtual void BeforeImportTransaction(
    BankFeedTransaction tran,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail)
  {
  }

  private void TransferMappedValues(
    string bankFeedId,
    CABankTran bankTran,
    BankFeedTransaction bankFeedTransaction)
  {
    foreach (CABankFeedFieldMapping feedFieldMapping in GraphHelper.RowCast<CABankFeedFieldMapping>((IEnumerable) PXSelectBase<CABankFeedFieldMapping, PXSelectReadonly<CABankFeedFieldMapping, Where<CABankFeedFieldMapping.bankFeedID, Equal<Required<CABankFeedFieldMapping.bankFeedID>>, And<CABankFeedFieldMapping.active, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) bankFeedId
    })))
    {
      string str = this.CalcValue(feedFieldMapping.SourceFieldOrValue, bankFeedTransaction);
      string targetField = feedFieldMapping.TargetField;
      if (targetField != null)
      {
        switch (targetField.Length)
        {
          case 8:
            switch (targetField[0])
            {
              case 'T':
                switch (targetField)
                {
                  case "TranCode":
                    bankTran.TranCode = str;
                    continue;
                  case "TranDesc":
                    bankTran.TranDesc = Extentions.Truncate(str, 512 /*0x0200*/);
                    continue;
                  default:
                    continue;
                }
              case 'U':
                if (targetField == "UserDesc")
                {
                  bankTran.UserDesc = Extentions.Truncate(str, 512 /*0x0200*/);
                  continue;
                }
                continue;
              default:
                continue;
            }
          case 9:
            switch (targetField[0])
            {
              case 'E':
                if (targetField == "ExtRefNbr")
                {
                  bankTran.ExtRefNbr = str;
                  continue;
                }
                continue;
              case 'P':
                if (targetField == "PayeeName")
                {
                  bankTran.PayeeName = Extentions.Truncate(str, 256 /*0x0100*/);
                  continue;
                }
                continue;
              default:
                continue;
            }
          case 10:
            switch (targetField[0])
            {
              case 'C':
                if (targetField == "CardNumber")
                {
                  bankTran.CardNumber = str;
                  continue;
                }
                continue;
              case 'I':
                if (targetField == "InvoiceNbr")
                {
                  bankTran.InvoiceInfo = str;
                  continue;
                }
                continue;
              default:
                continue;
            }
          default:
            continue;
        }
      }
    }
  }

  protected virtual EPExpenseClaimDetails CreateOrUpdateExpenseReceipt(
    BankFeedTransaction tran,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail,
    ExpenseClaimDetailEntry expenseGraph)
  {
    ((PXSelectBase<EPExpenseClaimDetails>) expenseGraph.ClaimDetails).Current = (EPExpenseClaimDetails) null;
    CABankFeedCorpCard suitableCorpCard = this.GetSuitableCorpCard(tran);
    if (suitableCorpCard == null)
      return (EPExpenseClaimDetails) null;
    int? forExpenseReceipt = this.GetExpenseItemForExpenseReceipt(tran, bankFeed);
    if (!forExpenseReceipt.HasValue)
      return (EPExpenseClaimDetails) null;
    EPExpenseClaimDetails expenseClaimDetails = (EPExpenseClaimDetails) null;
    if (!string.IsNullOrEmpty(tran.PendingTransactionID))
    {
      ((PXSelectBase<EPExpenseClaimDetails>) expenseGraph.ClaimDetails).Current = this.GetExpenseClaimDetailsByRefNbr(tran.PendingTransactionID);
      expenseClaimDetails = ((PXSelectBase<EPExpenseClaimDetails>) expenseGraph.ClaimDetails).Current;
    }
    if (expenseClaimDetails == null)
    {
      ((PXSelectBase<EPExpenseClaimDetails>) expenseGraph.ClaimDetails).Current = this.GetExpenseClaimDetailsByRefNbr(tran.TransactionID);
      expenseClaimDetails = ((PXSelectBase<EPExpenseClaimDetails>) expenseGraph.ClaimDetails).Current;
    }
    this.CheckEmplIsLinkedToCorpCard(suitableCorpCard, bankFeed);
    bool flag1 = false;
    if (expenseClaimDetails != null && expenseClaimDetails.RefNbr != null)
      return (EPExpenseClaimDetails) null;
    try
    {
      if (expenseClaimDetails == null)
      {
        expenseClaimDetails = ((PXSelectBase<EPExpenseClaimDetails>) expenseGraph.ClaimDetails).Insert();
        flag1 = true;
      }
      ExpenseClaimDetailsBankFeedExt extension = PXCache<EPExpenseClaimDetails>.GetExtension<ExpenseClaimDetailsBankFeedExt>(expenseClaimDetails);
      string bankTranStatus = extension.BankTranStatus;
      expenseClaimDetails.ExpenseDate = tran.Date;
      expenseClaimDetails.CuryUnitCost = tran.Amount;
      expenseClaimDetails.CuryExtCost = tran.Amount;
      expenseClaimDetails.CuryID = string.IsNullOrEmpty(tran.IsoCurrencyCode) ? bankFeedDetail.Currency : tran.IsoCurrencyCode;
      extension.BankTranStatus = tran.Pending.GetValueOrDefault() ? "P" : "R";
      if (flag1)
      {
        expenseClaimDetails.EmployeeID = suitableCorpCard.EmployeeID;
        expenseClaimDetails.InventoryID = forExpenseReceipt;
        expenseClaimDetails.Qty = new Decimal?((Decimal) 1);
        expenseClaimDetails.ExpenseRefNbr = tran.TransactionID;
        expenseClaimDetails.TranDesc = Extentions.Truncate(tran.Name, 256 /*0x0100*/);
        extension.Category = tran.Category;
        expenseClaimDetails.PaidWith = "CardComp";
        expenseClaimDetails.CorpCardID = suitableCorpCard.CorpCardID;
      }
      expenseClaimDetails = ((PXSelectBase<EPExpenseClaimDetails>) expenseGraph.ClaimDetails).Update(expenseClaimDetails);
      bool? nullable;
      if (tran.Pending.GetValueOrDefault())
      {
        nullable = expenseClaimDetails.Hold;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          ((PXAction) expenseGraph.hold).Press();
      }
      nullable = tran.Pending;
      bool flag3 = false;
      if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
      {
        nullable = expenseClaimDetails.Hold;
        if (nullable.GetValueOrDefault() && bankTranStatus == "P")
          ((PXAction) expenseGraph.Submit).Press();
      }
      ((PXGraph) expenseGraph).Persist();
      return ((PXSelectBase<EPExpenseClaimDetails>) expenseGraph.ClaimDetails).Current;
    }
    catch
    {
      if (expenseClaimDetails != null)
        ((PXSelectBase) expenseGraph.ClaimDetails).Cache.Remove((object) expenseClaimDetails);
      throw;
    }
  }

  protected virtual void MatchTransactionAndReceipt(
    CABankTran bankTran,
    EPExpenseClaimDetails expenseReceipt,
    CABankMatchingProcess matchingGraph)
  {
    if (expenseReceipt.Hold.GetValueOrDefault() || !expenseReceipt.CorpCardID.HasValue || expenseReceipt.BankTranDate.HasValue)
      return;
    bool? useForCorpCard = this.GetCashAccountById(bankTran.CashAccountID.Value).UseForCorpCard;
    bool flag = false;
    if (useForCorpCard.GetValueOrDefault() == flag & useForCorpCard.HasValue)
      return;
    ((PXGraph) matchingGraph).Clear();
    CABankTranMatch caBankTranMatch = new CABankTranMatch()
    {
      TranID = bankTran.TranID,
      TranType = bankTran.TranType,
      DocModule = "EP",
      DocRefNbr = expenseReceipt.ClaimDetailCD,
      DocType = "ECD",
      ReferenceID = expenseReceipt.EmployeeID,
      CuryApplAmt = expenseReceipt.ClaimCuryTranAmtWithTaxes
    };
    ((PXSelectBase<CABankTranMatch>) matchingGraph.TranMatch).Insert(caBankTranMatch);
    expenseReceipt.BankTranDate = bankTran.TranDate;
    ((PXSelectBase<EPExpenseClaimDetails>) matchingGraph.ExpenseReceipts).Update(expenseReceipt);
    bankTran.DocumentMatched = new bool?(true);
    ((PXSelectBase<CABankTran>) matchingGraph.CABankTran).Update(bankTran);
    ((PXGraph) matchingGraph).Persist();
  }

  protected virtual int? GetExpenseItemForExpenseReceipt(
    BankFeedTransaction tran,
    CABankFeed bankFeed)
  {
    int? forExpenseReceipt = bankFeed.DefaultExpenseItemID;
    foreach (CABankFeedExpense expenseDetail in this.ExpenseDetails)
    {
      string tranByExpenseFilter = this.GetValueFromTranByExpenseFilter(tran, expenseDetail);
      if (tranByExpenseFilter != null && expenseDetail.MatchValue != null && this.CheckValueByRule(tranByExpenseFilter, expenseDetail.MatchValue, expenseDetail.MatchRule))
      {
        forExpenseReceipt = expenseDetail.DoNotCreate.GetValueOrDefault() || !expenseDetail.InventoryItemID.HasValue ? new int?() : expenseDetail.InventoryItemID;
        break;
      }
    }
    return forExpenseReceipt;
  }

  protected virtual CABankFeedCorpCard GetSuitableCorpCard(BankFeedTransaction tran)
  {
    CABankFeedCorpCard suitableCorpCard = (CABankFeedCorpCard) null;
    foreach (CABankFeedCorpCard corpCard in this.CorpCardDetails.Where<CABankFeedCorpCard>((Func<CABankFeedCorpCard, bool>) (i => i.AccountID == tran.AccountID)))
    {
      if (corpCard.MatchRule == "N")
      {
        suitableCorpCard = corpCard;
        break;
      }
      string byCorpCardFilter = this.GetValueFromTranByCorpCardFilter(tran, corpCard);
      if (byCorpCardFilter != null && corpCard.MatchValue != null && this.CheckValueByRule(byCorpCardFilter, corpCard.MatchValue, corpCard.MatchRule))
      {
        suitableCorpCard = corpCard;
        break;
      }
    }
    return suitableCorpCard;
  }

  public virtual CABankFeedImport.StatementDates GetStatementDates(
    CABankFeedDetail bankFeedDet,
    DateTime date)
  {
    CABankFeedImport.StatementDates statementDates = (CABankFeedImport.StatementDates) null;
    switch (bankFeedDet.StatementPeriod)
    {
      case "M":
        statementDates = this.GetStatementDatesForMonth(bankFeedDet, date);
        break;
      case "W":
        statementDates = this.GetStatementDatesForWeek(bankFeedDet, date);
        break;
      case "D":
        statementDates = new CABankFeedImport.StatementDates()
        {
          StartDate = date,
          EndDate = date
        };
        break;
    }
    return statementDates;
  }

  protected virtual CABankFeedImport.StatementDates GetStatementDatesForWeek(
    CABankFeedDetail bankFeedDet,
    DateTime date)
  {
    int num1 = (int) (date.DayOfWeek + 1);
    int num2 = num1;
    int? statementStartDay1 = bankFeedDet.StatementStartDay;
    int valueOrDefault1 = statementStartDay1.GetValueOrDefault();
    DateTime dateTime1;
    DateTime dateTime2;
    if (num2 == valueOrDefault1 & statementStartDay1.HasValue)
    {
      dateTime1 = new DateTime(date.Year, date.Month, date.Day);
      dateTime2 = dateTime1.AddDays(6.0);
    }
    else
    {
      int num3 = num1;
      int? statementStartDay2 = bankFeedDet.StatementStartDay;
      int valueOrDefault2 = statementStartDay2.GetValueOrDefault();
      if (num3 > valueOrDefault2 & statementStartDay2.HasValue)
      {
        DateTime dateTime3 = new DateTime(date.Year, date.Month, date.Day);
        ref DateTime local = ref dateTime3;
        statementStartDay2 = bankFeedDet.StatementStartDay;
        double num4 = (double) (statementStartDay2.Value - num1);
        dateTime1 = local.AddDays(num4);
        dateTime2 = dateTime1.AddDays(6.0);
      }
      else
      {
        DateTime dateTime4 = new DateTime(date.Year, date.Month, date.Day);
        ref DateTime local = ref dateTime4;
        statementStartDay2 = bankFeedDet.StatementStartDay;
        double num5 = (double) (statementStartDay2.Value - num1 - 7);
        dateTime1 = local.AddDays(num5);
        dateTime2 = dateTime1.AddDays(6.0);
      }
    }
    return new CABankFeedImport.StatementDates()
    {
      StartDate = dateTime1,
      EndDate = dateTime2
    };
  }

  protected virtual CABankFeedImport.StatementDates GetStatementDatesForMonth(
    CABankFeedDetail bankFeedDet,
    DateTime date)
  {
    int day = bankFeedDet.StatementStartDay.Value;
    int num = DateTime.DaysInMonth(date.Year, date.Month);
    DateTime dateTime1;
    DateTime timeByComponents;
    if (date.Date.Day < day && date.Date.Day < num)
    {
      dateTime1 = new DateTime(date.Year, date.Month, 1).AddMonths(-1);
      dateTime1 = this.GetDateTimeByComponents(dateTime1.Year, dateTime1.Month, day);
      timeByComponents = this.GetDateTimeByComponents(date.Year, date.Month, day);
    }
    else
    {
      dateTime1 = this.GetDateTimeByComponents(date.Year, date.Month, day);
      DateTime dateTime2 = new DateTime(date.Year, date.Month, 1).AddMonths(1);
      timeByComponents = this.GetDateTimeByComponents(dateTime2.Year, dateTime2.Month, day);
    }
    DateTime dateTime3 = timeByComponents.AddDays(-1.0);
    return new CABankFeedImport.StatementDates()
    {
      StartDate = dateTime1,
      EndDate = dateTime3
    };
  }

  protected virtual void CheckEmplIsLinkedToCorpCard(
    CABankFeedCorpCard corpCardItem,
    CABankFeed bankFeed)
  {
    if (PXResultset<EPEmployeeCorpCardLink>.op_Implicit(PXSelectBase<EPEmployeeCorpCardLink, PXSelect<EPEmployeeCorpCardLink, Where<EPEmployeeCorpCardLink.corpCardID, Equal<Required<EPEmployeeCorpCardLink.corpCardID>>, And<EPEmployeeCorpCardLink.employeeID, Equal<Required<EPEmployeeCorpCardLink.employeeID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) corpCardItem.CorpCardID,
      (object) corpCardItem.EmployeeID
    })) == null)
      throw new PXException("The {0} employee is not linked to the {1} corporate card. Select another employee on the Corporate Cards tab of the Bank Feeds (CA205500) form for the {2} bank feed and the {1} corporate card.", new object[3]
      {
        (object) corpCardItem.EmployeeName,
        (object) corpCardItem.CardName,
        (object) bankFeed.BankFeedID
      });
  }

  protected virtual void DisableSubordinationCheck(ExpenseClaimDetailEntry expenseGraph)
  {
    PXConfigureSubordinateAndWingmenSelectorAttribute selectorAttribute = ((PXSelectBase) expenseGraph.ClaimDetails).Cache.GetAttributes((object) null, "EmployeeID").OfType<PXConfigureSubordinateAndWingmenSelectorAttribute>().FirstOrDefault<PXConfigureSubordinateAndWingmenSelectorAttribute>();
    if (selectorAttribute == null)
      return;
    selectorAttribute.AllowSubordinationAndWingmenCheck = false;
  }

  public static async Task<Dictionary<int, string>> ImportTransactionsForAccounts(
    CABankFeed bankFeed,
    List<CABankFeedDetail> items,
    Guid guid)
  {
    if (bankFeed == null)
      throw new PXArgumentException(nameof (bankFeed));
    if (items == null)
      throw new PXArgumentException(nameof (items));
    if (items.Any<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.BankFeedID != bankFeed.BankFeedID)))
      throw new PXArgumentException(nameof (items));
    Exception lastException = (Exception) null;
    Dictionary<int, string> lastUpdatedStatements = new Dictionary<int, string>();
    CABankFeedImport graph = PXGraph.CreateInstance<CABankFeedImport>();
    graph.CheckFeed(bankFeed);
    graph.BeforeImportTransactions(bankFeed);
    try
    {
      lastUpdatedStatements = await graph.DoImportTransaAndCreateReceiptsForAccounts(bankFeed, items, guid);
      graph.UpdateRetrievalStatus(bankFeed, "S", time: graph.GetLastProcessedTime());
    }
    catch (BankFeedImportException ex)
    {
      lastException = (Exception) ex;
      graph.UpdateRetrievalStatus(bankFeed, ex);
    }
    catch (Exception ex)
    {
      lastException = ex;
      graph.UpdateRetrievalStatus(bankFeed, "E", ex.Message);
    }
    if (lastException != null)
      throw lastException;
    Dictionary<int, string> dictionary = lastUpdatedStatements;
    lastException = (Exception) null;
    lastUpdatedStatements = (Dictionary<int, string>) null;
    graph = (CABankFeedImport) null;
    return dictionary;
  }

  public static async Task<Dictionary<int, string>> ImportTransactions(
    List<CABankFeed> items,
    Guid guid)
  {
    if (items == null)
      throw new PXArgumentException(nameof (items));
    return await PXGraph.CreateInstance<CABankFeedImport>().DoImportTransactions(items, guid);
  }

  public async Task<Dictionary<int, string>> DoImportTransactions(List<CABankFeed> items, Guid guid)
  {
    Exception lastException = (Exception) null;
    Dictionary<int, string> lastUpdatedStatements = new Dictionary<int, string>();
    PXProcessingInfo processingInfo = PXLongOperation.GetCustomInfoForCurrentThread("PXProcessingState") as PXProcessingInfo;
    for (int i = 0; i < items.Count; ++i)
    {
      CABankFeed item = items[i];
      try
      {
        this.CheckFeed(item);
        this.BeforeImportTransactions(item);
        foreach (KeyValuePair<int, string> receipt in await this.DoImportTransactionsAndCreateReceipts(item, guid))
        {
          if (!lastUpdatedStatements.ContainsKey(receipt.Key))
            lastUpdatedStatements.Add(receipt.Key, receipt.Value);
        }
        DateTime? lastProcessedTime = this.GetLastProcessedTime();
        this.UpdateRetrievalStatus(item, "S", time: lastProcessedTime);
      }
      catch (CAMatchProcessContext.CashAccountLockedException ex)
      {
        lastException = (Exception) ex;
        this.UpdateProcessingInfo(i, processingInfo, (Exception) ex);
      }
      catch (BankFeedImportException ex)
      {
        lastException = (Exception) ex;
        this.UpdateRetrievalStatus(item, ex);
        this.UpdateProcessingInfo(i, processingInfo, (Exception) ex);
      }
      catch (Exception ex)
      {
        lastException = ex;
        this.UpdateRetrievalStatus(item, "E", ex.Message);
        this.UpdateProcessingInfo(i, processingInfo, ex);
      }
      item = (CABankFeed) null;
    }
    if (processingInfo == null && lastException != null)
      throw lastException;
    Dictionary<int, string> dictionary = lastUpdatedStatements;
    lastException = (Exception) null;
    lastUpdatedStatements = (Dictionary<int, string>) null;
    processingInfo = (PXProcessingInfo) null;
    return dictionary;
  }

  protected virtual void BeforeImportTransactions(CABankFeed bankFeed)
  {
    this.ReadDetailsData(bankFeed);
  }

  protected virtual bool AllowBatchDownloading(
    CABankFeed bankFeed,
    IEnumerable<CABankFeedDetail> details)
  {
    bool flag = false;
    BankFeedManager specificManager = this.GetSpecificManager(bankFeed);
    if (specificManager.AllowBatchDownloading && details.Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.CashAccountID.HasValue)).Count<CABankFeedDetail>() >= specificManager.NumberOfAccountsForBatchDownloading)
      flag = true;
    return flag;
  }

  protected virtual CACorpCard GetCorpCardById(int? corpCardId)
  {
    return PXResultset<CACorpCard>.op_Implicit(PXSelectBase<CACorpCard, PXSelect<CACorpCard, Where<CACorpCard.corpCardID, Equal<Required<CACorpCard.corpCardID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) corpCardId
    }));
  }

  protected virtual CashAccount GetCashAccountById(int cashAccount)
  {
    return PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) cashAccount
    }));
  }

  protected virtual IEnumerable<CABankFeedDetail> GetBankFeedDetailByBankFeedId(string bankFeedId)
  {
    object[] objArray = new object[1]{ (object) bankFeedId };
    foreach (PXResult<CABankFeedDetail> pxResult in PXSelectBase<CABankFeedDetail, PXSelectJoin<CABankFeedDetail, InnerJoin<CABankFeed, On<CABankFeedDetail.bankFeedID, Equal<CABankFeed.bankFeedID>>, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CABankFeedDetail.cashAccountID>>>>, Where<CABankFeed.status, Equal<CABankFeedStatus.active>, And<CABankFeed.bankFeedID, Equal<Required<CABankFeed.bankFeedID>>>>>.Config>.Select((PXGraph) this, objArray))
      yield return PXResult<CABankFeedDetail>.op_Implicit(pxResult);
  }

  public virtual IEnumerable<CABankFeedAccountMapping> GetBankFeedAccountMapByBankFeedId(
    string bankFeedId)
  {
    object[] objArray = new object[1]{ (object) bankFeedId };
    foreach (PXResult<CABankFeedAccountMapping> pxResult in PXSelectBase<CABankFeedAccountMapping, PXSelectJoin<CABankFeedAccountMapping, InnerJoin<CABankFeedDetail, On<CABankFeedAccountMapping.bankFeedID, Equal<CABankFeedDetail.bankFeedID>, And<CABankFeedAccountMapping.lineNbr, Equal<CABankFeedDetail.lineNbr>>>, InnerJoin<CABankFeed, On<CABankFeedDetail.bankFeedID, Equal<CABankFeed.bankFeedID>>, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CABankFeedDetail.cashAccountID>>>>>, Where<CABankFeed.status, Equal<CABankFeedStatus.active>, And<CABankFeed.bankFeedID, Equal<Required<CABankFeed.bankFeedID>>, And<CABankFeed.multipleMapping, Equal<True>>>>>.Config>.Select((PXGraph) this, objArray))
      yield return PXResult<CABankFeedAccountMapping>.op_Implicit(pxResult);
  }

  protected virtual IEnumerable<CABankFeedCorpCard> GetBankFeedCordCardByBankFeedId(
    string bankFeedId)
  {
    object[] objArray = new object[1]{ (object) bankFeedId };
    foreach (PXResult<CABankFeedCorpCard> pxResult in PXSelectBase<CABankFeedCorpCard, PXSelectJoin<CABankFeedCorpCard, InnerJoin<CABankFeed, On<CABankFeedCorpCard.bankFeedID, Equal<CABankFeed.bankFeedID>>, InnerJoin<CACorpCard, On<CACorpCard.corpCardID, Equal<CABankFeedCorpCard.corpCardID>>, InnerJoin<CABankFeedDetail, On<CABankFeedCorpCard.bankFeedID, Equal<CABankFeedDetail.bankFeedID>, And<CABankFeedCorpCard.accountID, Equal<CABankFeedDetail.accountID>>>, InnerJoin<EPEmployee, On<CABankFeedCorpCard.employeeID, Equal<EPEmployee.bAccountID>>>>>>, Where<CABankFeed.status, Equal<CABankFeedStatus.active>, And<CABankFeed.bankFeedID, Equal<Required<CABankFeed.bankFeedID>>>>>.Config>.Select((PXGraph) this, objArray))
      yield return PXResult<CABankFeedCorpCard>.op_Implicit(pxResult);
  }

  protected virtual IEnumerable<CABankFeedExpense> GetBankFeedExpenseByBankFeedId(string bankFeedId)
  {
    object[] objArray = new object[1]{ (object) bankFeedId };
    foreach (PXResult<CABankFeedExpense> pxResult in PXSelectBase<CABankFeedExpense, PXSelectJoin<CABankFeedExpense, InnerJoin<CABankFeed, On<CABankFeedExpense.bankFeedID, Equal<CABankFeed.bankFeedID>>>, Where<CABankFeed.status, Equal<CABankFeedStatus.active>, And<CABankFeed.bankFeedID, Equal<Required<CABankFeed.bankFeedID>>>>>.Config>.Select((PXGraph) this, objArray))
      yield return PXResult<CABankFeedExpense>.op_Implicit(pxResult);
  }

  protected virtual CABankTranHeader GetLatestBankTranHeaderWithTransByCashAccount(
    int? cashAccountId,
    PXGraph graph)
  {
    return PXResultset<CABankTranHeader>.op_Implicit(PXSelectBase<CABankTranHeader, PXSelectJoin<CABankTranHeader, InnerJoin<CABankTran, On<CABankTran.headerRefNbr, Equal<CABankTranHeader.refNbr>>>, Where<CABankTranHeader.cashAccountID, Equal<Required<CABankTranHeader.cashAccountID>>>, OrderBy<Desc<CABankTranHeader.endBalanceDate>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) cashAccountId
    }));
  }

  protected virtual CABankTranHeader GetLatestBankTranHeaderByCashAccount(
    int? cashAccountId,
    PXGraph graph)
  {
    return PXResultset<CABankTranHeader>.op_Implicit(PXSelectBase<CABankTranHeader, PXSelect<CABankTranHeader, Where<CABankTranHeader.cashAccountID, Equal<Required<CABankTranHeader.cashAccountID>>>, OrderBy<Desc<CABankTranHeader.endBalanceDate>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) cashAccountId
    }));
  }

  protected virtual CABankTranHeader GetBankTranHeaderByStartEndDate(
    int? cashAccountId,
    DateTime startDate,
    DateTime endDate,
    PXGraph graph)
  {
    return PXResultset<CABankTranHeader>.op_Implicit(PXSelectBase<CABankTranHeader, PXSelect<CABankTranHeader, Where<CABankTranHeader.cashAccountID, Equal<Required<CABankTranHeader.cashAccountID>>, And<CABankTranHeader.startBalanceDate, Equal<Required<CABankTranHeader.startBalanceDate>>, And<CABankTranHeader.endBalanceDate, Equal<Required<CABankTranHeader.endBalanceDate>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[3]
    {
      (object) cashAccountId,
      (object) startDate,
      (object) endDate
    }));
  }

  protected virtual EPExpenseClaimDetails GetExpenseClaimDetailsByRefNbr(string refNbr)
  {
    foreach (PXResult<EPExpenseClaimDetails> pxResult in PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.expenseRefNbr, Equal<Required<EPExpenseClaimDetails.expenseRefNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) refNbr
    }))
    {
      EPExpenseClaimDetails claimDetailsByRefNbr = PXResult<EPExpenseClaimDetails>.op_Implicit(pxResult);
      if (claimDetailsByRefNbr.ExpenseRefNbr.Equals(refNbr, StringComparison.Ordinal))
        return claimDetailsByRefNbr;
    }
    return (EPExpenseClaimDetails) null;
  }

  protected virtual async Task<IEnumerable<BankFeedTransaction>> GetTransactions(
    CABankFeed bankFeed,
    IEnumerable<CABankFeedDetail> details,
    DateTime startDate)
  {
    LoadTransactionsData input = new LoadTransactionsData();
    input.AccountsID = details.Select<CABankFeedDetail, string>((Func<CABankFeedDetail, string>) (i => i.AccountID)).ToArray<string>();
    input.StartDate = startDate;
    input.EndDate = PXTimeZoneInfo.Now;
    if (input.AccountsID.Length > 1)
      input.TransactionsOrder = LoadTransactionsData.Order.CustomAccountAscDate;
    return await this.GetSpecificManager(bankFeed).GetTransactionsAsync(input, bankFeed);
  }

  private void CheckFeed(CABankFeed bankFeed)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.bankFeedIntegration>())
      throw new PXException("The Bank Feed Integration feature is disabled on the Enable/Disable Features (CS100000) form.");
    if (bankFeed.Status == "D" || bankFeed.Status == "S" || bankFeed.Status == "M" || bankFeed.Status == "R")
    {
      string str = ((IEnumerable<(string, string)>) CABankFeedStatus.ListAttribute.GetStatuses).Where<(string, string)>((Func<(string, string), bool>) (ii => ii.Item1 == bankFeed.Status)).Select<(string, string), string>((Func<(string, string), string>) (ii => ii.Item2)).FirstOrDefault<string>();
      throw new PXException("Transactions cannot be imported for the {0} bank feed with the {1} status. The bank feed status should be Active.", new object[2]
      {
        (object) bankFeed.BankFeedID,
        (object) str
      });
    }
  }

  private SortedList<DateTime, List<CABankFeedDetail>> GroupDetailsIntoBatches(
    IEnumerable<CABankFeedDetail> details)
  {
    DateTime? nullable = new DateTime?();
    SortedList<DateTime, List<CABankFeedDetail>> sortedList = new SortedList<DateTime, List<CABankFeedDetail>>();
    List<CABankFeedDetail> source = new List<CABankFeedDetail>();
    int num = 60;
    foreach (CABankFeedDetail caBankFeedDetail in (IEnumerable<CABankFeedDetail>) details.Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.ImportStartDate.HasValue)).OrderBy<CABankFeedDetail, DateTime?>((Func<CABankFeedDetail, DateTime?>) (i => i.ImportStartDate)).ThenBy<CABankFeedDetail, int?>((Func<CABankFeedDetail, int?>) (i => i.LineNbr)))
    {
      if (!nullable.HasValue)
      {
        nullable = caBankFeedDetail.ImportStartDate;
        source.Add(caBankFeedDetail);
      }
      else if (caBankFeedDetail.ImportStartDate.Value.Subtract(nullable.Value).TotalDays > (double) num)
      {
        sortedList.Add(nullable.Value, source);
        source = new List<CABankFeedDetail>()
        {
          caBankFeedDetail
        };
        nullable = caBankFeedDetail.ImportStartDate;
      }
      else
        source.Add(caBankFeedDetail);
    }
    if (source.Count<CABankFeedDetail>() > 0)
      sortedList.Add(nullable.Value, source);
    return sortedList;
  }

  private string GetValueFromTranByCorpCardFilter(
    BankFeedTransaction tran,
    CABankFeedCorpCard corpCard)
  {
    string byCorpCardFilter = (string) null;
    string matchField = corpCard.MatchField;
    if (matchField != null && matchField.Length == 1)
    {
      switch (matchField[0])
      {
        case 'A':
          byCorpCardFilter = tran.Name;
          break;
        case 'C':
          byCorpCardFilter = tran.CheckNumber;
          break;
        case 'M':
          byCorpCardFilter = tran.Memo;
          break;
        case 'O':
          byCorpCardFilter = tran.AccountOwner;
          break;
        case 'P':
          byCorpCardFilter = tran.PartnerAccountId;
          break;
        case 'R':
          byCorpCardFilter = tran.CardNumber;
          break;
        case 'U':
          byCorpCardFilter = tran.UserDesc;
          break;
        case 'Y':
          byCorpCardFilter = tran.PayeeName;
          break;
      }
    }
    return byCorpCardFilter;
  }

  private bool CheckValueByRule(string value, string pattern, string mathRule)
  {
    bool flag = false;
    value = value.Trim().ToLowerInvariant();
    pattern = pattern.Trim().ToLowerInvariant();
    if (pattern != string.Empty)
    {
      switch (mathRule)
      {
        case "S":
          flag = value.StartsWith(pattern);
          break;
        case "E":
          flag = value.EndsWith(pattern);
          break;
        case "C":
          flag = value.Contains(pattern);
          break;
      }
    }
    return flag;
  }

  private DateTime GetDateTimeByComponents(int year, int month, int day)
  {
    DateTime timeByComponents = DateTime.MinValue;
    int day1 = DateTime.DaysInMonth(year, month);
    timeByComponents = day1 >= day ? new DateTime(year, month, day) : new DateTime(year, month, day1);
    return timeByComponents;
  }

  private string GetValueFromTranByExpenseFilter(
    BankFeedTransaction tran,
    CABankFeedExpense expense)
  {
    string tranByExpenseFilter = (string) null;
    if (expense.MatchField == "C")
      tranByExpenseFilter = tran.Category;
    if (expense.MatchField == "A")
      tranByExpenseFilter = tran.Name;
    return tranByExpenseFilter;
  }

  private string ShrinkMessage(string message)
  {
    return message == null || message.Length <= 250 ? message : message.Substring(0, 250);
  }

  private DateTime? GetLastProcessedTime()
  {
    DateTime? lastProcessedTime = new DateTime?();
    CABankFeedDetail caBankFeedDetail = GraphHelper.RowCast<CABankFeedDetail>(((PXGraph) this).Caches[typeof (CABankFeedDetail)].Cached).Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (ii => ii.RetrievalStatus == "S")).OrderByDescending<CABankFeedDetail, DateTime?>((Func<CABankFeedDetail, DateTime?>) (ii => ii.RetrievalDate)).FirstOrDefault<CABankFeedDetail>();
    if (caBankFeedDetail != null)
    {
      DateTime? retrievalDate = caBankFeedDetail.RetrievalDate;
      if (retrievalDate.HasValue)
      {
        ref DateTime? local = ref lastProcessedTime;
        retrievalDate = caBankFeedDetail.RetrievalDate;
        DateTime dateTime = retrievalDate.Value;
        local = new DateTime?(dateTime);
      }
    }
    return lastProcessedTime;
  }

  private void UpdateProcessingInfo(int index, PXProcessingInfo processingInfo, Exception ex)
  {
    if (processingInfo == null)
      return;
    PXProcessingMessage processingMessage = new PXProcessingMessage((PXErrorLevel) 5, ex.Message);
    processingInfo.Messages[index] = processingMessage;
    PXTrace.WriteError(ex);
  }

  private BankFeedImportException HandleExceptions(Exception ex, CABankFeedDetail detail)
  {
    BankFeedImportException importException;
    switch (ex)
    {
      case CAMatchProcessContext.CashAccountLockedException _:
        throw ex;
      case PXOuterException pxOuterException:
        string message = string.Join(" ", ((IEnumerable<string>) pxOuterException.InnerMessages).Select<string, string>((Func<string, string>) (i => PXMessages.LocalizeNoPrefix(i))));
        if (message == string.Empty)
          message = ex.Message;
        importException = new BankFeedImportException(message);
        this.UpdateRetrievalStatusForDetail(detail, importException);
        break;
      case BankFeedImportException feedImportException:
        importException = feedImportException;
        this.UpdateRetrievalStatusForDetail(detail, importException);
        break;
      default:
        importException = new BankFeedImportException(ex.Message);
        this.UpdateRetrievalStatusForDetail(detail, importException);
        break;
    }
    return importException;
  }

  private BankFeedManager GetSpecificManager(CABankFeed bankfeed)
  {
    return this.BankFeedManagerProvider(bankfeed.Type);
  }

  private BankFeedTransactionsImport GetImportGraph()
  {
    if (this.ImportGraph == null)
      this.ImportGraph = PXGraph.CreateInstance<BankFeedTransactionsImport>();
    return this.ImportGraph;
  }

  private ExpenseClaimDetailEntry GetExpenseGraph()
  {
    if (this.ExpenseGraph == null)
      this.ExpenseGraph = PXGraph.CreateInstance<ExpenseClaimDetailEntry>();
    return this.ExpenseGraph;
  }

  private CABankMatchingProcess GetMatchingGraph()
  {
    if (this.MatchingGraph == null)
      this.MatchingGraph = PXGraph.CreateInstance<CABankMatchingProcess>();
    return this.MatchingGraph;
  }

  private void ReadDetailsData(CABankFeed bankFeed)
  {
    this.BankFeedDetails = this.GetBankFeedDetailByBankFeedId(bankFeed.BankFeedID);
    this.CorpCardDetails = this.GetBankFeedCordCardByBankFeedId(bankFeed.BankFeedID);
    this.ExpenseDetails = this.GetBankFeedExpenseByBankFeedId(bankFeed.BankFeedID);
    this.AccountMappingDetails = this.GetBankFeedAccountMapByBankFeedId(bankFeed.BankFeedID);
  }

  private void LogTranRecordProcessedCount(CABankFeedDetail detail)
  {
    if (!detail.CashAccountID.HasValue)
      return;
    PXTrace.WriteInformation($"Number of created bank transactions for cash account {this.GetCashAccountById(detail.CashAccountID.Value).CashAccountCD.Trim()}: {this.TranRecordProcessed}.");
  }

  public class StatementDates
  {
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
  }
}
