// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.GraphExtensions.CABankFeedImportFile
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.Common;
using PX.Data;
using PX.Objects.CA.BankFeed;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.CA.GraphExtensions;

public class CABankFeedImportFile : PXGraphExtension<CABankFeedImport>
{
  private FileBankFeedManager _fileBankFeedManager;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankFeedIntegration>();

  [InjectDependency]
  internal Func<string, FileBankFeedManager> FileBankFeedManagerProvider { get; set; }

  internal CABankFeedImportFile.LoadSpecificFileOptions LoadSpecificFile { get; set; }

  [PXOverride]
  public virtual CABankTranHeader GetTransHeaderForSingleMapping(
    BankFeedTransaction tran,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail,
    CABankTransactionsImport importGraph,
    Func<BankFeedTransaction, CABankFeed, CABankFeedDetail, CABankTransactionsImport, CABankTranHeader> baseMethod)
  {
    CABankTranHeader forSingleMapping1 = baseMethod(tran, bankFeed, bankFeedDetail, importGraph);
    if (forSingleMapping1 != null || bankFeed.Type != "F" || this.LoadSpecificFile != null && !this.LoadSpecificFile.IgnoreDates)
      return forSingleMapping1;
    bool flag = false;
    CABankTranHeader forSingleMapping2 = this.GetLatestBankTranHeaderWithTransByStartEndDate(bankFeedDetail.CashAccountID, tran.Date.Value, tran.Date.Value, (PXGraph) importGraph);
    if (forSingleMapping2 != null)
    {
      ((PXSelectBase<CABankTranHeader>) importGraph.Header).Current = forSingleMapping2;
      flag = true;
    }
    DateTime? date1;
    if (!flag)
    {
      int? cashAccountId = bankFeedDetail.CashAccountID;
      date1 = tran.Date;
      DateTime startDate = date1.Value;
      date1 = tran.Date;
      DateTime endDate = date1.Value;
      CABankTransactionsImport graph = importGraph;
      forSingleMapping2 = this.GetLatestBankTranHeaderByStartEndDate(cashAccountId, startDate, endDate, (PXGraph) graph);
      if (forSingleMapping2 != null)
      {
        ((PXSelectBase<CABankTranHeader>) importGraph.Header).Current = forSingleMapping2;
        flag = true;
      }
    }
    if (!flag)
    {
      CABankFeedImport caBankFeedImport = this.Base;
      CABankFeedDetail bankFeedDet = bankFeedDetail;
      date1 = tran.Date;
      DateTime date2 = date1.Value;
      CABankFeedImport.StatementDates statementDates = caBankFeedImport.GetStatementDates(bankFeedDet, date2);
      forSingleMapping2 = new CABankTranHeader();
      forSingleMapping2.CashAccountID = bankFeedDetail.CashAccountID;
      forSingleMapping2.DocDate = new DateTime?(statementDates.EndDate);
      forSingleMapping2.StartBalanceDate = new DateTime?(statementDates.StartDate);
      forSingleMapping2.EndBalanceDate = new DateTime?(statementDates.EndDate);
      ((PXSelectBase<CABankTranHeader>) importGraph.Header).Insert(forSingleMapping2);
    }
    return forSingleMapping2;
  }

  [PXOverride]
  public virtual void BeforeImportTransactions(CABankFeed bankFeed, Action<CABankFeed> baseMethod)
  {
    baseMethod(bankFeed);
    if (!(bankFeed.Type == "F"))
      return;
    this.GetSpecificFileManager(bankFeed).SynchronizeFolder(bankFeed);
  }

  [PXOverride]
  public virtual async Task<IEnumerable<BankFeedTransaction>> GetTransactions(
    CABankFeed bankFeed,
    IEnumerable<CABankFeedDetail> details,
    DateTime startDate,
    Func<CABankFeed, IEnumerable<CABankFeedDetail>, DateTime, Task<IEnumerable<BankFeedTransaction>>> baseMethod)
  {
    if (!(bankFeed.Type == "F"))
      return await baseMethod(bankFeed, details, startDate);
    CABankFeedDetail caBankFeedDetail = details.First<CABankFeedDetail>();
    LoadTransactionsData transactionsData = new LoadTransactionsData();
    transactionsData.StartDate = startDate;
    transactionsData.EndDate = PXTimeZoneInfo.Now;
    transactionsData.Details = new CABankFeedDetail[1]
    {
      caBankFeedDetail
    };
    FileBankFeedManager specificFileManager = this.GetSpecificFileManager(bankFeed);
    if (this.LoadSpecificFile == null)
      return await System.Threading.Tasks.Task.FromResult<IEnumerable<BankFeedTransaction>>(specificFileManager.GetTransactions(transactionsData, bankFeed));
    transactionsData.IgnoreDates = this.LoadSpecificFile.IgnoreDates;
    return specificFileManager.GetTransactions(transactionsData, bankFeed, new BankFeedFile()
    {
      FileName = this.LoadSpecificFile.FileName
    });
  }

  [PXOverride]
  public virtual void BeforeImportTransaction(
    BankFeedTransaction tran,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail,
    Action<BankFeedTransaction, CABankFeed, CABankFeedDetail> baseMethod)
  {
    if (bankFeed.Type == "F")
      this.SetTranAmountByFileAmountFormat(tran, bankFeed);
    else
      baseMethod(tran, bankFeed, bankFeedDetail);
  }

  [PXOverride]
  public virtual CABankTran AddBankTransactionToStatement(
    BankFeedTransaction tran,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail,
    CABankTransactionsImport importGraph,
    Func<BankFeedTransaction, CABankFeed, CABankFeedDetail, CABankTransactionsImport, CABankTran> baseMethod)
  {
    if (!(bankFeed.Type == "F"))
      return baseMethod(tran, bankFeed, bankFeedDetail, importGraph);
    CABankTran bankTran = (CABankTran) null;
    try
    {
      CABankFeedAccountMapping feedAccountMapping = this.Base.GetBankFeedAccountMapByBankFeedId(bankFeed.BankFeedID).Where<CABankFeedAccountMapping>((Func<CABankFeedAccountMapping, bool>) (i =>
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
      Decimal? amount1 = tran.Amount;
      Decimal num1 = 0M;
      Decimal? nullable1 = amount1.GetValueOrDefault() >= num1 & amount1.HasValue ? tran.Amount : new Decimal?(0M);
      caBankTran1.CuryCreditAmt = nullable1;
      CABankTran caBankTran2 = bankTran;
      Decimal? amount2 = tran.Amount;
      Decimal num2 = 0M;
      Decimal? nullable2;
      if (!(amount2.GetValueOrDefault() < num2 & amount2.HasValue))
      {
        nullable2 = new Decimal?(0M);
      }
      else
      {
        Decimal num3 = (Decimal) -1;
        amount2 = tran.Amount;
        nullable2 = amount2.HasValue ? new Decimal?(num3 * amount2.GetValueOrDefault()) : new Decimal?();
      }
      caBankTran2.CuryDebitAmt = nullable2;
      this.TransferMappedValuesFromFile(bankFeed, bankTran, tran);
      extension.Source = bankFeed.Type;
      if (feedAccountMapping != null)
        extension.BankFeedAccountMapID = feedAccountMapping.BankFeedAccountMapID;
      ((PXSelectBase<CABankTran>) importGraph.Details).Update(bankTran);
      CABankTranHeader current = ((PXSelectBase<CABankTranHeader>) importGraph.Header).Current;
      current.CuryEndBalance = current.CuryDetailsEndBalance;
      ((PXSelectBase<CABankTranHeader>) importGraph.Header).Update(current);
      ((PXGraph) importGraph).Persist();
      return bankTran;
    }
    catch
    {
      if (bankTran != null)
        ((PXSelectBase) importGraph.Details).Cache.Remove((object) bankTran);
      throw;
    }
  }

  [PXOverride]
  public virtual bool AllowBatchDownloading(
    CABankFeed bankFeed,
    IEnumerable<CABankFeedDetail> details,
    Func<CABankFeed, IEnumerable<CABankFeedDetail>, bool> baseMethod)
  {
    return !(bankFeed.Type == "F") && baseMethod(bankFeed, details);
  }

  protected virtual FileBankFeedManager GetSpecificFileManager(CABankFeed bankFeed)
  {
    if (this._fileBankFeedManager == null || this._fileBankFeedManager.FileFormat != bankFeed.FileFormat)
    {
      FileBankFeedManager fileBankFeedManager = this.FileBankFeedManagerProvider(bankFeed.FileFormat);
      fileBankFeedManager.SetImportGraph((PXGraph) this.Base);
      this._fileBankFeedManager = fileBankFeedManager;
    }
    return this._fileBankFeedManager;
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  protected virtual void SetTransactionAmount(
    CABankFeed bankFeed,
    CABankTran bankTran,
    BankFeedTransaction bankFeedTran)
  {
    switch (this.GetSpecificFileManager(bankFeed).GetAmountFormat(bankFeed))
    {
      case "S":
        this.SetAmountFromSameColumn(bankTran, bankFeedTran);
        break;
      case "D":
        this.SetAmountFromDifferentColumns(bankTran, bankFeedTran);
        break;
      case "P":
        this.SetAmountDependsOnParameter(bankFeed, bankTran, bankFeedTran);
        break;
    }
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  private void SetAmountFromSameColumn(CABankTran bankTran, BankFeedTransaction bankFeedTran)
  {
    CABankTran caBankTran1 = bankTran;
    Decimal? amount1 = bankFeedTran.Amount;
    Decimal num1 = 0M;
    Decimal? nullable1 = amount1.GetValueOrDefault() >= num1 & amount1.HasValue ? bankFeedTran.Amount : new Decimal?(0M);
    caBankTran1.CuryCreditAmt = nullable1;
    CABankTran caBankTran2 = bankTran;
    Decimal? amount2 = bankFeedTran.Amount;
    Decimal num2 = 0M;
    Decimal? nullable2;
    if (!(amount2.GetValueOrDefault() < num2 & amount2.HasValue))
    {
      nullable2 = new Decimal?(0M);
    }
    else
    {
      Decimal num3 = (Decimal) -1;
      Decimal? amount3 = bankFeedTran.Amount;
      nullable2 = amount3.HasValue ? new Decimal?(num3 * amount3.GetValueOrDefault()) : new Decimal?();
    }
    caBankTran2.CuryDebitAmt = nullable2;
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  private void SetAmountDependsOnParameter(
    CABankFeed bankFeed,
    CABankTran bankTran,
    BankFeedTransaction bankFeedTran)
  {
    string debitCreditParameter = bankFeedTran.DebitCreditParameter;
    Decimal? amount = bankFeedTran.Amount;
    string lowerInvariant = bankFeed.CreditLabel.Trim().ToLowerInvariant();
    if (debitCreditParameter.Trim().ToLowerInvariant() == lowerInvariant)
    {
      bankTran.CuryCreditAmt = new Decimal?(0M);
      bankTran.CuryDebitAmt = amount;
      BankFeedTransaction bankFeedTransaction = bankFeedTran;
      Decimal num = (Decimal) -1;
      Decimal? nullable1 = amount;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num * nullable1.GetValueOrDefault()) : new Decimal?();
      bankFeedTransaction.Amount = nullable2;
    }
    else
    {
      bankTran.CuryCreditAmt = amount;
      bankTran.CuryDebitAmt = new Decimal?(0M);
      bankFeedTran.Amount = amount;
    }
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  private void SetAmountFromDifferentColumns(CABankTran bankTran, BankFeedTransaction bankFeedTran)
  {
    Decimal valueOrDefault1 = bankFeedTran.CreditAmount.GetValueOrDefault();
    Decimal valueOrDefault2 = bankFeedTran.DebitAmount.GetValueOrDefault();
    if (valueOrDefault1 != 0M)
    {
      bankTran.CuryCreditAmt = new Decimal?(0M);
      bankTran.CuryDebitAmt = new Decimal?(valueOrDefault1);
      bankFeedTran.Amount = new Decimal?(-1M * valueOrDefault1);
    }
    else
    {
      bankTran.CuryCreditAmt = new Decimal?(valueOrDefault2);
      bankTran.CuryDebitAmt = new Decimal?(0M);
      bankFeedTran.Amount = new Decimal?(valueOrDefault2);
    }
  }

  private void SetTranAmountByFileAmountFormat(BankFeedTransaction tran, CABankFeed bankFeed)
  {
    if (bankFeed.FileAmountFormat == "D")
    {
      Decimal valueOrDefault1 = tran.CreditAmount.GetValueOrDefault();
      Decimal valueOrDefault2 = tran.DebitAmount.GetValueOrDefault();
      if (valueOrDefault1 != 0M)
        tran.Amount = new Decimal?(-1M * valueOrDefault1);
      else
        tran.Amount = new Decimal?(valueOrDefault2);
    }
    else
    {
      if (!(bankFeed.FileAmountFormat == "P"))
        return;
      string debitCreditParameter = tran.DebitCreditParameter;
      Decimal? amount = tran.Amount;
      string lowerInvariant = bankFeed.CreditLabel.Trim().ToLowerInvariant();
      if (debitCreditParameter.Trim().ToLowerInvariant() == lowerInvariant)
      {
        BankFeedTransaction bankFeedTransaction = tran;
        Decimal num = (Decimal) -1;
        Decimal? nullable1 = amount;
        Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num * nullable1.GetValueOrDefault()) : new Decimal?();
        bankFeedTransaction.Amount = nullable2;
      }
      else
        tran.Amount = amount;
    }
  }

  private void TransferMappedValuesFromFile(
    CABankFeed bankFeed,
    CABankTran bankTran,
    BankFeedTransaction bankFeedTransaction)
  {
    foreach (PXResult<CABankFeedFieldMapping> pxResult in PXSelectBase<CABankFeedFieldMapping, PXSelect<CABankFeedFieldMapping, Where<CABankFeedFieldMapping.bankFeedID, Equal<Required<CABankFeedFieldMapping.bankFeedID>>, And<CABankFeedFieldMapping.active, Equal<True>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) bankFeed.BankFeedID
    }))
    {
      switch (PXResult<CABankFeedFieldMapping>.op_Implicit(pxResult).TargetField)
      {
        case "CardNumber":
          bankTran.CardNumber = bankFeedTransaction.CardNumber;
          continue;
        case "ExtRefNbr":
          bankTran.ExtRefNbr = bankFeedTransaction.ExtRefNbr;
          continue;
        case "UserDesc":
          bankTran.UserDesc = bankFeedTransaction.UserDesc;
          continue;
        case "InvoiceInfo":
          bankTran.InvoiceInfo = bankFeedTransaction.InvoiceInfo;
          continue;
        case "PayeeName":
          bankTran.PayeeName = bankFeedTransaction.PayeeName;
          continue;
        case "TranCode":
          bankTran.TranCode = bankFeedTransaction.TranCode;
          continue;
        default:
          continue;
      }
    }
  }

  protected virtual CABankTranHeader GetLatestBankTranHeaderWithTransByStartEndDate(
    int? cashAccountId,
    DateTime startDate,
    DateTime endDate,
    PXGraph graph)
  {
    return PXResultset<CABankTranHeader>.op_Implicit(PXSelectBase<CABankTranHeader, PXSelectJoin<CABankTranHeader, InnerJoin<CABankTran, On<CABankTran.headerRefNbr, Equal<CABankTranHeader.refNbr>>>, Where<CABankTranHeader.cashAccountID, Equal<Required<CABankTranHeader.cashAccountID>>, And<CABankTranHeader.startBalanceDate, LessEqual<Required<CABankTranHeader.startBalanceDate>>, And<CABankTranHeader.endBalanceDate, GreaterEqual<Required<CABankTranHeader.endBalanceDate>>>>>, OrderBy<Desc<CABankTranHeader.createdDateTime>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[3]
    {
      (object) cashAccountId,
      (object) startDate,
      (object) endDate
    }));
  }

  protected virtual CABankTranHeader GetLatestBankTranHeaderByStartEndDate(
    int? cashAccountId,
    DateTime startDate,
    DateTime endDate,
    PXGraph graph)
  {
    return PXResultset<CABankTranHeader>.op_Implicit(PXSelectBase<CABankTranHeader, PXSelect<CABankTranHeader, Where<CABankTranHeader.cashAccountID, Equal<Required<CABankTranHeader.cashAccountID>>, And<CABankTranHeader.startBalanceDate, LessEqual<Required<CABankTranHeader.startBalanceDate>>, And<CABankTranHeader.endBalanceDate, GreaterEqual<Required<CABankTranHeader.endBalanceDate>>>>>, OrderBy<Desc<CABankTranHeader.createdDateTime>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[3]
    {
      (object) cashAccountId,
      (object) startDate,
      (object) endDate
    }));
  }

  public class LoadSpecificFileOptions
  {
    public string FileName { get; set; }

    public bool IgnoreDates { get; set; }
  }
}
