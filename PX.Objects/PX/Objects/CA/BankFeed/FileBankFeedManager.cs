// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.FileBankFeedManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.Wiki.ExternalFiles;
using PX.Objects.CA.BankFeed.DataSync;
using PX.Objects.CA.GraphExtensions;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CA.BankFeed;

public abstract class FileBankFeedManager
{
  private const string incorrectDrCr = "incorrectDrCr";
  private const string zeroAmount = "zeroAmount";
  private const string returnedTrans = "returnedTrans";
  private static readonly (string, string)[] requiredFieldsAmtInSameCol = new (string, string)[5]
  {
    ("AccountName", "Bank Account Name"),
    ("TransactionID", "Ext. Tran. ID"),
    ("Date", "Tran. Date"),
    ("Amount", "Transaction Amount"),
    ("Name", "Tran. Desc")
  };
  private static readonly (string, string)[] requiredFieldsAmtInDiffCols = new (string, string)[6]
  {
    ("AccountName", "Bank Account Name"),
    ("TransactionID", "Ext. Tran. ID"),
    ("Date", "Tran. Date"),
    ("CreditAmount", "Receipt Amount"),
    ("DebitAmount", "Disbursement Amount"),
    ("Name", "Tran. Desc")
  };
  private static readonly (string, string)[] requiredFieldsAmtTypeDependsOnVal = new (string, string)[6]
  {
    ("AccountName", "Bank Account Name"),
    ("TransactionID", "Ext. Tran. ID"),
    ("Date", "Tran. Date"),
    ("Amount", "Transaction Amount"),
    ("DebitCreditParameter", "Debit/Credit Property in Separate Column"),
    ("Name", "Tran. Desc")
  };
  private static readonly (string, string)[] optionalFields = new (string, string)[6]
  {
    ("CardNumber", "Card Number"),
    ("ExtRefNbr", "Ext. Ref. Nbr."),
    ("UserDesc", "Custom Tran. Desc."),
    ("InvoiceInfo", "Invoice Nbr."),
    ("PayeeName", "Payee/Payer"),
    ("TranCode", "Tran. Code")
  };
  private static readonly HashSet<string> requiredFieldsForMapping = new HashSet<string>()
  {
    "AccountName",
    "TransactionID",
    "Date",
    "Amount",
    "Name",
    "DebitAmount",
    "CreditAmount",
    "DebitCreditParameter"
  };
  private static readonly (string, string)[] availableCorpCardFilters = new (string, string)[4]
  {
    ("N", " "),
    ("U", "Custom Tran. Desc."),
    ("R", "Card Number"),
    ("Y", "Payee/Payer")
  };
  private static readonly (string, string)[] availableExpenseReceiptFilters = new (string, string)[1]
  {
    ("A", "Name")
  };
  protected Dictionary<string, CABankFeedFieldMapping> _mapping;
  private readonly SyFormulaProcessor _formulaProcessor = new SyFormulaProcessor();
  private string _currentFileName;
  private CABankFeed _currentBankFeed;
  private PXGraph _importGraph;

  public string GetAmountFormat(CABankFeed bankFeed)
  {
    return this.PredefinedAmountFormat ?? bankFeed.FileAmountFormat;
  }

  public virtual (string, string)[] AvailableFieldsForMapping(CABankFeed bankFeed)
  {
    (string, string)[] valueTupleArray1 = new (string, string)[0];
    switch (this.GetAmountFormat(bankFeed))
    {
      case "S":
        valueTupleArray1 = FileBankFeedManager.requiredFieldsAmtInSameCol;
        break;
      case "D":
        valueTupleArray1 = FileBankFeedManager.requiredFieldsAmtInDiffCols;
        break;
      case "P":
        valueTupleArray1 = FileBankFeedManager.requiredFieldsAmtTypeDependsOnVal;
        break;
    }
    (string, string)[] valueTupleArray2 = new (string, string)[valueTupleArray1.Length + FileBankFeedManager.optionalFields.Length];
    valueTupleArray1.CopyTo((Array) valueTupleArray2, 0);
    FileBankFeedManager.optionalFields.CopyTo((Array) valueTupleArray2, valueTupleArray1.Length);
    return valueTupleArray2;
  }

  public virtual Dictionary<string, string> DefaultFieldMapping => new Dictionary<string, string>();

  public virtual (string, string)[] AvailableCorpCardFilters
  {
    get => FileBankFeedManager.availableCorpCardFilters;
  }

  public virtual (string, string)[] AvailableExpenseReceiptFilters
  {
    get => FileBankFeedManager.availableExpenseReceiptFilters;
  }

  public virtual HashSet<string> ReqiredFieldsForMapping
  {
    get => FileBankFeedManager.requiredFieldsForMapping;
  }

  public virtual bool CanChangeRequiredFieldsMapping => true;

  public virtual string PredefinedAmountFormat => (string) null;

  public virtual bool NeedDataSample => true;

  public abstract string FileFormat { get; }

  public virtual void SynchronizeFolder(CABankFeed bankFeed)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      IEnumerable<Tuple<UploadFileRevision, UploadFile>> attachedFiles = this.GetAttachedFiles(bankFeed.ProviderID.Value);
      IEnumerable<Tuple<UploadFileRevision, UploadFile>> newFilesAfterSync = this.SyncFolder(bankFeed, attachedFiles);
      this.SetCurrentFileForBankFeedDetails(bankFeed, attachedFiles, newFilesAfterSync);
      transactionScope.Complete();
    }
  }

  public virtual IEnumerable<BankFeedTransaction> GetTransactions(
    LoadTransactionsData input,
    CABankFeed bankFeed)
  {
    FileBankFeedManager fileBankFeedManager1 = this;
    Guid? nullable1 = bankFeed.ProviderID;
    Guid guid = nullable1.Value;
    CABankFeedDetail detail = input.Details[0];
    fileBankFeedManager1._currentBankFeed = bankFeed;
    DateTime? nullable2 = detail.FileDateTime;
    if (nullable2.HasValue)
    {
      FileBankFeedManager fileBankFeedManager2 = fileBankFeedManager1;
      Guid providerId = guid;
      nullable2 = detail.FileDateTime;
      DateTime startDate = nullable2.Value;
      IEnumerable<Tuple<UploadFileRevision, UploadFile>> files = fileBankFeedManager2.GetNextFilesInSequence(providerId, startDate);
      IEnumerable<CABankFeedFieldMapping> mapping = fileBankFeedManager1.GetMapping(bankFeed);
      fileBankFeedManager1._mapping = EnumerableExtensions.Distinct<CABankFeedFieldMapping, string>(mapping, (Func<CABankFeedFieldMapping, string>) (i => i.TargetField)).ToDictionary<CABankFeedFieldMapping, string, CABankFeedFieldMapping>((Func<CABankFeedFieldMapping, string>) (i => i.TargetField), (Func<CABankFeedFieldMapping, CABankFeedFieldMapping>) (i => i));
      foreach (Tuple<UploadFileRevision, UploadFile> tuple in files)
      {
        Tuple<UploadFileRevision, UploadFile> file = tuple;
        List<BankFeedTransaction> bankFeedTransactionList = new List<BankFeedTransaction>();
        UploadFile uploadFile = file.Item2;
        UploadFileRevision uploadFileRevision = file.Item1;
        nullable1 = uploadFileRevision.FileID;
        Guid? fileId = detail.FileID;
        int? fileRevisionId1;
        if ((nullable1.HasValue == fileId.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == fileId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          fileRevisionId1 = uploadFileRevision.FileRevisionID;
          int? fileRevisionId2 = detail.FileRevisionID;
          if (fileRevisionId1.GetValueOrDefault() == fileRevisionId2.GetValueOrDefault() & fileRevisionId1.HasValue == fileRevisionId2.HasValue && detail.RetrievalStatus == "S")
            continue;
        }
        fileId = uploadFileRevision.FileID;
        nullable1 = detail.FileID;
        if ((fileId.HasValue == nullable1.HasValue ? (fileId.HasValue ? (fileId.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        {
          int? fileRevisionId3 = uploadFileRevision.FileRevisionID;
          fileRevisionId1 = detail.FileRevisionID;
          if (fileRevisionId3.GetValueOrDefault() == fileRevisionId1.GetValueOrDefault() & fileRevisionId3.HasValue == fileRevisionId1.HasValue)
            goto label_13;
        }
        detail.FileID = uploadFileRevision.FileID;
        detail.FileRevisionID = uploadFileRevision.FileRevisionID;
        detail.FileDateTime = uploadFileRevision.CreatedDateTime;
        detail.RetrievalStatus = "N";
        CABankFeedDetail caBankFeedDetail = detail;
        nullable2 = new DateTime?();
        DateTime? nullable3 = nullable2;
        caBankFeedDetail.RetrievalDate = nullable3;
        fileBankFeedManager1.UpdateBankFeedDetail(detail);
        fileBankFeedManager1.PersistBankFeedDetails();
label_13:
        Enumerable.Empty<BankFeedTransaction>();
        IEnumerable<BankFeedTransaction> transactionsFromFile;
        try
        {
          transactionsFromFile = fileBankFeedManager1.GetTransactionsFromFile(input, file);
        }
        catch (FileContentValidationException ex)
        {
          Tuple<UploadFileRevision, UploadFile> storedFile = files.Where<Tuple<UploadFileRevision, UploadFile>>((Func<Tuple<UploadFileRevision, UploadFile>, bool>) (i =>
          {
            if (!(i.Item2.Name == file.Item2.Name))
              return false;
            DateTime? createdDateTime1 = i.Item1.CreatedDateTime;
            DateTime? createdDateTime2 = file.Item1.CreatedDateTime;
            return createdDateTime1.HasValue & createdDateTime2.HasValue && createdDateTime1.GetValueOrDefault() > createdDateTime2.GetValueOrDefault();
          })).LastOrDefault<Tuple<UploadFileRevision, UploadFile>>();
          if (storedFile == null)
          {
            if (fileBankFeedManager1.GetSharedFolderFiles(fileBankFeedManager1._currentBankFeed).Where<BankFeedFile>((Func<BankFeedFile, bool>) (i => i.FileName == file.Item2.Name)).FirstOrDefault<BankFeedFile>() != null)
              throw;
            continue;
          }
          transactionsFromFile = fileBankFeedManager1.GetTransactionsFromFile(input, storedFile);
        }
        foreach (BankFeedTransaction transaction in (IEnumerable<BankFeedTransaction>) transactionsFromFile.OrderBy<BankFeedTransaction, DateTime?>((Func<BankFeedTransaction, DateTime?>) (i => i.Date)))
          yield return transaction;
      }
    }
  }

  public virtual IEnumerable<BankFeedTransaction> GetTransactions(
    LoadTransactionsData data,
    CABankFeed bankFeed,
    BankFeedFile file)
  {
    this._currentBankFeed = bankFeed;
    this._mapping = EnumerableExtensions.Distinct<CABankFeedFieldMapping, string>(this.GetMapping(bankFeed), (Func<CABankFeedFieldMapping, string>) (i => i.TargetField)).ToDictionary<CABankFeedFieldMapping, string, CABankFeedFieldMapping>((Func<CABankFeedFieldMapping, string>) (i => i.TargetField), (Func<CABankFeedFieldMapping, CABankFeedFieldMapping>) (i => i));
    byte[] numArray = this.GetExchanger(bankFeed.FolderLogin, bankFeed.FolderPassword, bankFeed.SshCertificateName).Download($"{bankFeed.FolderPath}/{file.FileName}");
    FileInfo fileInfo = new FileInfo(file.FileName, (string) null, numArray);
    Tuple<IPXSYProvider, SYProviderObject, SYProviderField[]> providerWithParams = this.CreateProviderWithParams(file.FileName, new PXSYParameter[1]
    {
      new PXSYParameter("InContext", "true")
    });
    IPXSYProvider ipxsyProvider = providerWithParams.Item1;
    SYProviderField[] source = providerWithParams.Item3;
    PXSYTable transFromfile;
    try
    {
      PXContext.SetSlot<FileInfo>("InContext", fileInfo);
      string[] array = ((IEnumerable<SYProviderField>) source).Select<SYProviderField, string>((Func<SYProviderField, string>) (i => i.Name)).ToArray<string>();
      transFromfile = ipxsyProvider.Import((string) null, array, new PXSYFilterRow[0], (string) null, (PXSYSyncTypes) 0);
    }
    finally
    {
      PXContext.SetSlot("InContext", (object) null);
    }
    this._currentFileName = file.FileName;
    IEnumerable<BankFeedTransaction> transactionsFromFile = this.GetTransactionsFromFile(data, transFromfile);
    if (data.TransactionsOrder == LoadTransactionsData.Order.DescDate)
      return (IEnumerable<BankFeedTransaction>) transactionsFromFile.OrderByDescending<BankFeedTransaction, DateTime?>((Func<BankFeedTransaction, DateTime?>) (i => i.Date));
    return data.TransactionsOrder == LoadTransactionsData.Order.AscDate ? (IEnumerable<BankFeedTransaction>) transactionsFromFile.OrderBy<BankFeedTransaction, DateTime?>((Func<BankFeedTransaction, DateTime?>) (i => i.Date)) : transactionsFromFile;
  }

  private string ProviderName(string id, string[] extensions)
  {
    if (extensions.Length != 1)
      return "Bank Feed " + id;
    return $"Bank Feed {id} ({extensions[0]})";
  }

  public virtual void SetUpDataProvider(CABankFeed bankFeed)
  {
    (string ProviderType, string[] Extensions) andFileExtension = this.GetProviderTypeAndFileExtension();
    Tuple<IFileExchange, IEnumerable<ExternalFileInfo>> folderData = (Tuple<IFileExchange, IEnumerable<ExternalFileInfo>>) null;
    if (this.NeedDataSample)
    {
      try
      {
        folderData = this.GetSharedFolderFiles(bankFeed, andFileExtension.Extensions);
      }
      catch
      {
        throw new PXException("Unable to connect to URL {0}. Verify the values in the Connection to Source File section and try again.", new object[1]
        {
          (object) bankFeed.FolderPath
        });
      }
    }
    string str = this.ProviderName(bankFeed.BankFeedID, andFileExtension.Extensions);
    SYProviderMaint syProviderGraph = this.GetSyProviderGraph();
    ((PXSelectBase<SYProvider>) syProviderGraph.Providers).Current = PXResultset<SYProvider>.op_Implicit(((PXSelectBase<SYProvider>) syProviderGraph.Providers).Search<SYProvider.name>((object) str, Array.Empty<object>()));
    Guid guid1 = ((PXSelectBase<SYProvider>) syProviderGraph.Providers).Current == null ? this.CreateDataProvider(syProviderGraph, bankFeed, folderData) : this.UseExistingDataProvider(syProviderGraph);
    CABankFeedMaint bankFeedGraph = this.GetBankFeedGraph(bankFeed);
    bankFeed = ((PXSelectBase<CABankFeed>) bankFeedGraph.BankFeed).Current;
    Guid? providerId = bankFeed.ProviderID;
    Guid guid2 = guid1;
    if ((providerId.HasValue ? (providerId.GetValueOrDefault() != guid2 ? 1 : 0) : 1) == 0)
      return;
    bankFeed.ProviderID = new Guid?(guid1);
    bankFeed.Status = "R";
    ((PXSelectBase<CABankFeed>) bankFeedGraph.BankFeed).Update(bankFeed);
    CABankFeedMaintFile extension = ((PXGraph) bankFeedGraph).GetExtension<CABankFeedMaintFile>();
    extension.ClearCurrentFileId();
    extension.ClearFileFieldsMapping();
    extension.AddFileFieldsMapping(bankFeed);
    ((PXAction) bankFeedGraph.Save).Press();
  }

  public virtual void CheckConnection(CABankFeed bankFeed)
  {
    (string ProviderType, string[] Extensions) andFileExtension = this.GetProviderTypeAndFileExtension();
    this.GetSharedFolderFiles(bankFeed, andFileExtension.Extensions);
  }

  public virtual IEnumerable<BankFeedFile> GetSharedFolderFiles(CABankFeed bankFeed)
  {
    (string ProviderType, string[] Extensions) andFileExtension = this.GetProviderTypeAndFileExtension();
    Tuple<IFileExchange, IEnumerable<ExternalFileInfo>> sharedFolderFiles = this.GetSharedFolderFiles(bankFeed, andFileExtension.Extensions);
    PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
    return (IEnumerable<BankFeedFile>) sharedFolderFiles.Item2.Select<ExternalFileInfo, BankFeedFile>((Func<ExternalFileInfo, BankFeedFile>) (i => new BankFeedFile()
    {
      FileName = i.Name,
      UploadDate = new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(i.Date, timeZone))
    })).ToList<BankFeedFile>();
  }

  public void SetImportGraph(PXGraph graph) => this._importGraph = graph;

  protected virtual void SetValue(
    string mappingTarget,
    PXSYRow fileRow,
    BankFeedTransaction tran,
    Action<BankFeedTransaction, Decimal?> setValue)
  {
    if (!this._mapping.ContainsKey(mappingTarget))
      return;
    CABankFeedFieldMapping mappingRow = this._mapping[mappingTarget];
    if (!mappingRow.Active.GetValueOrDefault())
      return;
    string evaluatedValue = this.GetEvaluatedValue(mappingRow, fileRow);
    if (string.IsNullOrEmpty(evaluatedValue))
      return;
    Decimal num;
    try
    {
      num = this.ParseDecimal(evaluatedValue);
    }
    catch (FormatException ex)
    {
      throw new FileContentValidationException("Cannot get decimal value from string {0} (file {1} transaction ID {2}).", new object[3]
      {
        (object) evaluatedValue,
        (object) this._currentFileName,
        (object) tran.TransactionID
      });
    }
    setValue(tran, new Decimal?(num));
  }

  protected virtual void SetValue(
    string mappingTarget,
    PXSYRow fileRow,
    BankFeedTransaction tran,
    Action<BankFeedTransaction, DateTime?> setValue)
  {
    if (!this._mapping.ContainsKey(mappingTarget))
      return;
    CABankFeedFieldMapping mappingRow = this._mapping[mappingTarget];
    if (!mappingRow.Active.GetValueOrDefault())
      return;
    string evaluatedValue = this.GetEvaluatedValue(mappingRow, fileRow);
    if (string.IsNullOrEmpty(evaluatedValue))
      return;
    DateTime dateTime;
    try
    {
      dateTime = this.ParseDateTime(evaluatedValue);
    }
    catch (FormatException ex)
    {
      throw new FileContentValidationException("Cannot get date value from string {0} (file {1} transaction ID {2}).", new object[3]
      {
        (object) evaluatedValue,
        (object) this._currentFileName,
        (object) tran.TransactionID
      });
    }
    setValue(tran, new DateTime?(dateTime));
  }

  protected virtual void SetValue(
    string mappingTarget,
    PXSYRow fileRow,
    BankFeedTransaction tran,
    Action<BankFeedTransaction, string> setValue)
  {
    if (!this._mapping.ContainsKey(mappingTarget))
      return;
    CABankFeedFieldMapping mappingRow = this._mapping[mappingTarget];
    if (!mappingRow.Active.GetValueOrDefault())
      return;
    string evaluatedValue = this.GetEvaluatedValue(mappingRow, fileRow);
    setValue(tran, evaluatedValue);
  }

  protected virtual DateTime ParseDateTime(string strDate) => Convert.ToDateTime(strDate);

  protected virtual Decimal ParseDecimal(string strDecimal) => Convert.ToDecimal(strDecimal);

  protected virtual IEnumerable<BankFeedTransaction> GetTransactionsFromFile(
    LoadTransactionsData input,
    Tuple<UploadFileRevision, UploadFile> storedFile)
  {
    UploadFile uploadFile = storedFile.Item2;
    UploadFileRevision uploadFileRevision = storedFile.Item1;
    string name = uploadFile.Name;
    int num = uploadFileRevision.FileRevisionID.Value;
    Tuple<IPXSYProvider, SYProviderObject, SYProviderField[]> providerWithParams = this.CreateProviderWithParams(name);
    PXSYTable transFromfile = providerWithParams.Item1.Import(providerWithParams.Item2.Name, ((IEnumerable<SYProviderField>) providerWithParams.Item3).Select<SYProviderField, string>((Func<SYProviderField, string>) (i => i.Name)).ToArray<string>(), new PXSYFilterRow[0], (num - 1).ToString(), (PXSYSyncTypes) 2);
    this._currentFileName = uploadFile.Name;
    return this.GetTransactionsFromFile(input, transFromfile);
  }

  protected virtual void ValidateAndSetAmount(
    LoadTransactionsData input,
    PXSYRow fileRow,
    BankFeedTransaction tran)
  {
    bool testMode = input.TestMode;
    switch (this.GetAmountFormat(this._currentBankFeed))
    {
      case "S":
        this.SetValue("Amount", fileRow, tran, (Action<BankFeedTransaction, Decimal?>) ((o, i) => o.Amount = i));
        if (testMode)
          break;
        this.ValidateAmountFromSameColumn(tran);
        break;
      case "D":
        this.SetValue("CreditAmount", fileRow, tran, (Action<BankFeedTransaction, Decimal?>) ((o, i) => o.CreditAmount = i));
        this.SetValue("DebitAmount", fileRow, tran, (Action<BankFeedTransaction, Decimal?>) ((o, i) => o.DebitAmount = i));
        if (testMode)
          break;
        this.ValidateAmountFromDifferentColumns(tran);
        break;
      case "P":
        this.SetValue("Amount", fileRow, tran, (Action<BankFeedTransaction, Decimal?>) ((o, i) => o.Amount = i));
        this.SetValue("DebitCreditParameter", fileRow, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.DebitCreditParameter = i));
        if (input.TestMode)
          break;
        this.ValidateAmountDependsOnParameter(tran);
        break;
    }
  }

  protected virtual IEnumerable<BankFeedTransaction> GetTransactionsFromFile(
    LoadTransactionsData input,
    PXSYTable transFromfile)
  {
    string accountId = (string) null;
    bool testMode = input.TestMode;
    List<BankFeedTransaction> transactionsFromFile = new List<BankFeedTransaction>();
    Dictionary<string, int> transCounters = new Dictionary<string, int>();
    if (input.Details != null && input.Details[0] != null)
    {
      accountId = input.Details[0].AccountID;
      if (accountId != null)
        accountId = accountId.Trim().ToLowerInvariant();
    }
    DateTime startDate = input.StartDate;
    DateTime dateTime1 = startDate;
    foreach (PXSYRow row in transFromfile.Rows)
    {
      BankFeedTransaction tran = new BankFeedTransaction();
      tran.Pending = new bool?(false);
      this.SetValue("AccountName", row, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.AccountID = i));
      string lowerInvariant = tran.AccountID?.Trim().ToLowerInvariant();
      if ((testMode || !(accountId != lowerInvariant)) && !this.IgnoreAndWriteToLogIfNeeded(input, row, transCounters))
      {
        this.SetValue("TransactionID", row, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.TransactionID = i));
        if (!testMode && string.IsNullOrEmpty(tran.TransactionID))
          this.ThrowFieldIsEmptyException("TransactionID", tran);
        this.SetValue("Date", row, tran, (Action<BankFeedTransaction, DateTime?>) ((o, i) => o.Date = i));
        DateTime? date;
        if (!testMode)
        {
          date = tran.Date;
          if (!date.HasValue)
            this.ThrowFieldIsEmptyException("Date", tran);
        }
        date = tran.Date;
        DateTime dateTime2 = startDate;
        if ((date.HasValue ? (date.GetValueOrDefault() < dateTime2 ? 1 : 0) : 0) == 0 || input.IgnoreDates)
        {
          this.ValidateAndSetAmount(input, row, tran);
          this.SetValue("AccountName", row, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.AccountName = i));
          this.SetValue("Name", row, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.Name = i));
          this.SetValue("CardNumber", row, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.CardNumber = i));
          this.SetValue("ExtRefNbr", row, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.ExtRefNbr = i));
          this.SetValue("UserDesc", row, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.UserDesc = i));
          this.SetValue("InvoiceInfo", row, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.InvoiceInfo = i));
          this.SetValue("PayeeName", row, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.PayeeName = i));
          this.SetValue("TranCode", row, tran, (Action<BankFeedTransaction, string>) ((o, i) => o.TranCode = i));
          date = tran.Date;
          DateTime dateTime3 = dateTime1;
          if ((date.HasValue ? (date.GetValueOrDefault() > dateTime3 ? 1 : 0) : 0) != 0)
          {
            date = tran.Date;
            dateTime1 = date.Value;
          }
          this.UpdateCounter("returnedTrans", transCounters);
          if (tran.Amount.GetValueOrDefault() + tran.DebitAmount.GetValueOrDefault() + tran.CreditAmount.GetValueOrDefault() == 0M)
            this.UpdateCounter("zeroAmount", transCounters);
          transactionsFromFile.Add(tran);
        }
      }
    }
    input.StartDate = dateTime1;
    this.WriteParsingResultsToLog(accountId, transCounters);
    return (IEnumerable<BankFeedTransaction>) transactionsFromFile;
  }

  protected virtual SYProviderMaint GetSyProviderGraph()
  {
    return PXGraph.CreateInstance<SYProviderMaint>();
  }

  protected virtual PXGraph GetImportGraph()
  {
    if (this._importGraph == null)
      this._importGraph = (PXGraph) PXGraph.CreateInstance<CABankFeedImport>();
    return this._importGraph;
  }

  protected virtual CABankFeedMaint GetBankFeedGraph(CABankFeed bankFeed)
  {
    CABankFeedMaint instance = PXGraph.CreateInstance<CABankFeedMaint>();
    if (bankFeed != null && bankFeed.BankFeedID != null)
      ((PXSelectBase<CABankFeed>) instance.BankFeed).Current = PXResultset<CABankFeed>.op_Implicit(PXSelectBase<CABankFeed, PXSelect<CABankFeed, Where<CABankFeed.bankFeedID, Equal<Required<CABankFeed.bankFeedID>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) bankFeed.BankFeedID
      }));
    return instance;
  }

  protected virtual List<PXSYParameter> GetProviderParams(Guid providerId, string filename)
  {
    List<PXSYParameter> providerParams = new List<PXSYParameter>();
    foreach (SYProviderParameter providerParameter in GraphHelper.RowCast<SYProviderParameter>((IEnumerable) PXSelectBase<SYProviderParameter, PXSelect<SYProviderParameter, Where<SYProviderParameter.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select(this.GetImportGraph(), new object[1]
    {
      (object) providerId
    })))
    {
      if (providerParameter.Name == "FileName")
        providerParams.Add(new PXSYParameter(providerParameter.Name, filename));
      else
        providerParams.Add(new PXSYParameter(providerParameter.Name, providerParameter.Value));
    }
    return providerParams;
  }

  protected virtual SYProvider GetSyProvider(Guid providerId)
  {
    return PXResultset<SYProvider>.op_Implicit(PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select(this.GetImportGraph(), new object[1]
    {
      (object) providerId
    }));
  }

  protected virtual IEnumerable<SYProviderField> GetProviderFields(
    Guid providerId,
    string objectName)
  {
    return GraphHelper.RowCast<SYProviderField>((IEnumerable) PXSelectBase<SYProviderField, PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Required<SYProviderField.providerID>>, And<SYProviderField.objectName, Equal<Required<SYProviderField.objectName>>, And<SYProviderField.isActive, Equal<True>>>>, OrderBy<Asc<SYProviderField.lineNbr>>>.Config>.Select(this.GetImportGraph(), new object[2]
    {
      (object) providerId,
      (object) objectName
    }));
  }

  protected virtual SYProviderObject GetProviderObject(Guid providerId)
  {
    return PXResultset<SYProviderObject>.op_Implicit(PXSelectBase<SYProviderObject, PXSelect<SYProviderObject, Where<SYProviderObject.providerID, Equal<Required<SYProviderObject.providerID>>>, OrderBy<Asc<SYProviderObject.lineNbr>>>.Config>.Select(this.GetImportGraph(), new object[1]
    {
      (object) providerId
    }));
  }

  protected virtual IEnumerable<Tuple<UploadFileRevision, UploadFile>> GetAttachedFiles(
    Guid providerId)
  {
    List<Tuple<UploadFileRevision, UploadFile>> attachedFiles = new List<Tuple<UploadFileRevision, UploadFile>>();
    foreach (PXResult<UploadFileRevision, UploadFile> pxResult in PXSelectBase<UploadFileRevision, PXSelectJoin<UploadFileRevision, InnerJoin<UploadFile, On<UploadFileRevision.fileID, Equal<UploadFile.fileID>>, InnerJoin<NoteDoc, On<UploadFile.fileID, Equal<NoteDoc.fileID>>, InnerJoin<SYProvider, On<NoteDoc.noteID, Equal<SYProvider.noteID>>>>>, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>, OrderBy<Desc<UploadFileRevision.createdDateTime>>>.Config>.Select(this.GetImportGraph(), new object[1]
    {
      (object) providerId
    }))
      attachedFiles.Add(new Tuple<UploadFileRevision, UploadFile>(PXResult<UploadFileRevision, UploadFile>.op_Implicit(pxResult), PXResult<UploadFileRevision, UploadFile>.op_Implicit(pxResult)));
    return (IEnumerable<Tuple<UploadFileRevision, UploadFile>>) attachedFiles;
  }

  protected virtual IEnumerable<Tuple<UploadFileRevision, UploadFile>> GetNextFilesInSequence(
    Guid providerId,
    DateTime startDate)
  {
    PXGraph importGraph = this.GetImportGraph();
    object[] objArray = new object[2]
    {
      (object) providerId,
      (object) startDate
    };
    foreach (PXResult<UploadFileRevision, UploadFile> pxResult in PXSelectBase<UploadFileRevision, PXSelectJoin<UploadFileRevision, InnerJoin<UploadFile, On<UploadFileRevision.fileID, Equal<UploadFile.fileID>>, InnerJoin<NoteDoc, On<UploadFile.fileID, Equal<NoteDoc.fileID>>, InnerJoin<SYProvider, On<NoteDoc.noteID, Equal<SYProvider.noteID>>>>>, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>, And<UploadFileRevision.createdDateTime, GreaterEqual<Required<UploadFileRevision.createdDateTime>>>>, OrderBy<Asc<UploadFileRevision.createdDateTime>>>.Config>.Select(importGraph, objArray))
      yield return new Tuple<UploadFileRevision, UploadFile>(PXResult<UploadFileRevision, UploadFile>.op_Implicit(pxResult), PXResult<UploadFileRevision, UploadFile>.op_Implicit(pxResult));
  }

  protected virtual IEnumerable<CABankFeedFieldMapping> GetMapping(CABankFeed bankFeed)
  {
    return GraphHelper.RowCast<CABankFeedFieldMapping>((IEnumerable) PXSelectBase<CABankFeedFieldMapping, PXSelect<CABankFeedFieldMapping, Where<CABankFeedFieldMapping.bankFeedID, Equal<Required<CABankFeedFieldMapping.bankFeedID>>>>.Config>.Select(this.GetImportGraph(), new object[1]
    {
      (object) bankFeed.BankFeedID
    }));
  }

  protected virtual IEnumerable<CABankFeedDetail> GetBankFeedDetailByBankFeedId(string bankFeedId)
  {
    PXGraph importGraph = this.GetImportGraph();
    object[] objArray = new object[1]{ (object) bankFeedId };
    foreach (PXResult<CABankFeedDetail> pxResult in PXSelectBase<CABankFeedDetail, PXSelectJoin<CABankFeedDetail, InnerJoin<CABankFeed, On<CABankFeedDetail.bankFeedID, Equal<CABankFeed.bankFeedID>>, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CABankFeedDetail.cashAccountID>>>>, Where<CABankFeed.status, Equal<CABankFeedStatus.active>, And<CABankFeed.bankFeedID, Equal<Required<CABankFeed.bankFeedID>>>>>.Config>.Select(importGraph, objArray))
      yield return PXResult<CABankFeedDetail>.op_Implicit(pxResult);
  }

  protected virtual IFileExchange GetExchanger(string login, string password)
  {
    return this.GetExchanger(login, password, (string) null);
  }

  protected virtual IFileExchange GetExchanger(
    string login,
    string password,
    string sshCertificateName)
  {
    FileInfo fileInfo = (FileInfo) null;
    string str = (string) null;
    if (!string.IsNullOrEmpty(sshCertificateName))
      (fileInfo, str) = this.GetSshCetificate(sshCertificateName);
    return FileExchangeHelper.GetExchanger("C", login, password, fileInfo, str);
  }

  protected abstract (string ProviderType, string[] Extensions) GetProviderTypeAndFileExtension();

  private void SetCurrentFileForBankFeedDetails(
    CABankFeed bankFeed,
    IEnumerable<Tuple<UploadFileRevision, UploadFile>> storedFilesBeforeSync,
    IEnumerable<Tuple<UploadFileRevision, UploadFile>> newFilesAfterSync)
  {
    Tuple<UploadFileRevision, UploadFile> tuple = newFilesAfterSync.FirstOrDefault<Tuple<UploadFileRevision, UploadFile>>();
    IEnumerable<CABankFeedDetail> detailByBankFeedId = this.GetBankFeedDetailByBankFeedId(bankFeed.BankFeedID);
    bool flag = false;
    if ((!detailByBankFeedId.All<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => !i.FileDateTime.HasValue)) ? 0 : (storedFilesBeforeSync.GroupBy<Tuple<UploadFileRevision, UploadFile>, Guid?>((Func<Tuple<UploadFileRevision, UploadFile>, Guid?>) (i => i.Item1.FileID)).Count<IGrouping<Guid?, Tuple<UploadFileRevision, UploadFile>>>() == 1 ? 1 : 0)) != 0)
    {
      UploadFileRevision uploadFileRevision = storedFilesBeforeSync.LastOrDefault<Tuple<UploadFileRevision, UploadFile>>().Item1;
      foreach (CABankFeedDetail detail in detailByBankFeedId)
      {
        flag = true;
        detail.FileID = uploadFileRevision.FileID;
        detail.FileDateTime = uploadFileRevision.CreatedDateTime;
        detail.FileRevisionID = uploadFileRevision.FileRevisionID;
        detail.RetrievalStatus = "N";
        detail.RetrievalDate = new DateTime?();
        this.UpdateBankFeedDetail(detail);
      }
    }
    else if (tuple != null)
    {
      UploadFileRevision uploadFileRevision = tuple.Item1;
      foreach (CABankFeedDetail detail in detailByBankFeedId.Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => !i.FileDateTime.HasValue)))
      {
        flag = true;
        detail.FileID = uploadFileRevision.FileID;
        detail.FileRevisionID = uploadFileRevision.FileRevisionID;
        detail.FileDateTime = uploadFileRevision.CreatedDateTime;
        detail.RetrievalStatus = "N";
        detail.RetrievalDate = new DateTime?();
        this.UpdateBankFeedDetail(detail);
      }
    }
    if (!flag)
      return;
    this.PersistBankFeedDetails();
  }

  private IEnumerable<Tuple<UploadFileRevision, UploadFile>> SyncFolder(
    CABankFeed bankFeed,
    IEnumerable<Tuple<UploadFileRevision, UploadFile>> storedFiles)
  {
    IEnumerable<Tuple<UploadFileRevision, UploadFile>> tuples = Enumerable.Empty<Tuple<UploadFileRevision, UploadFile>>();
    (string ProviderType, string[] Extensions) andFileExtension = this.GetProviderTypeAndFileExtension();
    Tuple<IFileExchange, IEnumerable<ExternalFileInfo>> sharedFolderFiles = this.GetSharedFolderFiles(bankFeed, andFileExtension.Extensions);
    var latestStoredFile = storedFiles.Select((e, i) => new
    {
      Value = e.Item1,
      Index = i
    }).Where(e => e.Value.OriginalTimestamp.HasValue).FirstOrDefault();
    if (latestStoredFile != null)
    {
      HashSet<string> storedFilesWithMaxOriginalTs = storedFiles.Skip<Tuple<UploadFileRevision, UploadFile>>(latestStoredFile.Index).TakeWhile<Tuple<UploadFileRevision, UploadFile>>((Func<Tuple<UploadFileRevision, UploadFile>, bool>) (e =>
      {
        DateTime? originalTimestamp1 = e.Item1.OriginalTimestamp;
        DateTime? originalTimestamp2 = latestStoredFile.Value.OriginalTimestamp;
        if (originalTimestamp1.HasValue != originalTimestamp2.HasValue)
          return false;
        return !originalTimestamp1.HasValue || originalTimestamp1.GetValueOrDefault() == originalTimestamp2.GetValueOrDefault();
      })).Select<Tuple<UploadFileRevision, UploadFile>, string>((Func<Tuple<UploadFileRevision, UploadFile>, string>) (i => i.Item2.Name)).ToHashSet<string>();
      DateTime? dateTime = latestStoredFile.Value.OriginalTimestamp;
      IOrderedEnumerable<ExternalFileInfo> orderedEnumerable = sharedFolderFiles.Item2.Where<ExternalFileInfo>((Func<ExternalFileInfo, bool>) (i =>
      {
        DateTime date1 = i.Date;
        DateTime? nullable1 = dateTime;
        if ((nullable1.HasValue ? (date1 > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          return true;
        DateTime date2 = i.Date;
        DateTime? nullable2 = dateTime;
        return (nullable2.HasValue ? (date2 == nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0 && !storedFilesWithMaxOriginalTs.Contains(i.Name);
      })).OrderBy<ExternalFileInfo, DateTime>((Func<ExternalFileInfo, DateTime>) (i => i.Date));
      if (orderedEnumerable.Count<ExternalFileInfo>() > 0)
        tuples = this.SaveNewFiles(bankFeed, sharedFolderFiles.Item1, (IEnumerable<ExternalFileInfo>) orderedEnumerable);
    }
    else if (sharedFolderFiles.Item2.Count<ExternalFileInfo>() > 0)
    {
      IOrderedEnumerable<ExternalFileInfo> newFiles = sharedFolderFiles.Item2.OrderBy<ExternalFileInfo, DateTime>((Func<ExternalFileInfo, DateTime>) (i => i.Date));
      tuples = this.SaveNewFiles(bankFeed, sharedFolderFiles.Item1, (IEnumerable<ExternalFileInfo>) newFiles);
    }
    return tuples;
  }

  private Tuple<IPXSYProvider, SYProviderObject, SYProviderField[]> CreateProviderWithParams(
    string fileName,
    PXSYParameter[] additionalParams = null)
  {
    Guid providerId = this._currentBankFeed.ProviderID.Value;
    IPXSYProvider provider = this.CreateProvider(this.GetSyProvider(providerId));
    List<PXSYParameter> providerParams = this.GetProviderParams(providerId, fileName);
    if (additionalParams != null)
      providerParams.AddRange((IEnumerable<PXSYParameter>) additionalParams);
    provider.SetParameters(providerParams.ToArray());
    SYProviderObject providerObject = this.GetProviderObject(providerId);
    if (providerObject == null || providerObject.Name == null)
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) "Name"
      });
    IEnumerable<SYProviderField> providerFields = this.GetProviderFields(providerId, providerObject.Name);
    return new Tuple<IPXSYProvider, SYProviderObject, SYProviderField[]>(provider, providerObject, providerFields.ToArray<SYProviderField>());
  }

  protected virtual IEnumerable<Tuple<UploadFileRevision, UploadFile>> SaveNewFiles(
    CABankFeed bankFeed,
    IFileExchange fileExchange,
    IEnumerable<ExternalFileInfo> newFiles)
  {
    List<Tuple<UploadFileRevision, UploadFile>> tupleList = new List<Tuple<UploadFileRevision, UploadFile>>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
      SYProvider syProvider1 = PXResultset<SYProvider>.op_Implicit(PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) bankFeed.ProviderID
      }));
      if (syProvider1 == null)
        throw new PXException("The data provider could not be found in the system.");
      foreach (ExternalFileInfo newFile in newFiles)
      {
        byte[] numArray = fileExchange.Download(newFile.FullName);
        Path.GetExtension(newFile.Name);
        Path.GetFileNameWithoutExtension(newFile.Name);
        FileInfo fileInfo = new FileInfo(newFile.Name, (string) null, numArray);
        fileInfo.OriginalName = newFile.Name;
        if (instance.SaveFile(fileInfo, (FileExistsAction) 1))
        {
          Guid? uid = fileInfo.UID;
          if (uid.HasValue)
          {
            string screenId = PXSiteMapProviderExtensions.FindSiteMapNodeByGraphType(PXSiteMap.Provider, typeof (CABankTransactionsImport).FullName)?.ScreenID;
            UploadFileMaintenance.UpdatePrimaryScreenId(((PXSelectBase<UploadFile>) instance.Files).Current.FileID, screenId);
            UploadFileRevision current1 = ((PXSelectBase<UploadFileRevision>) instance.Revisions).Current;
            current1.OriginalTimestamp = new DateTime?(newFile.Date);
            ((PXSelectBase<UploadFileRevision>) instance.Revisions).Update(current1);
            PXCache cach = ((PXGraph) instance).Caches[typeof (SYProvider)];
            SYProvider syProvider2 = syProvider1;
            Guid[] guidArray = new Guid[1];
            uid = fileInfo.UID;
            guidArray[0] = uid.Value;
            PXNoteAttribute.SetFileNotes(cach, (object) syProvider2, guidArray);
            ((PXGraph) instance).Actions.PressSave();
            UploadFileRevision current2 = ((PXSelectBase<UploadFileRevision>) instance.Revisions).Current;
            UploadFile current3 = ((PXSelectBase<UploadFile>) instance.Files).Current;
            tupleList.Add(new Tuple<UploadFileRevision, UploadFile>(current2, current3));
            continue;
          }
        }
        throw new PXException("The {0} file cannot be saved.", new object[1]
        {
          (object) newFile.Name
        });
      }
      transactionScope.Complete();
    }
    return (IEnumerable<Tuple<UploadFileRevision, UploadFile>>) tupleList;
  }

  private string GetEvaluatedValue(CABankFeedFieldMapping mappingRow, PXSYRow fileRow)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    return this._formulaProcessor.Evaluate(mappingRow.SourceFieldOrValue, new SyFormulaFinalDelegate((object) new FileBankFeedManager.\u003C\u003Ec__DisplayClass67_0()
    {
      fileRow = fileRow
    }, __methodptr(\u003CGetEvaluatedValue\u003Eb__0))) as string;
  }

  private void ValidateAmountFromSameColumn(BankFeedTransaction bankFeedTran)
  {
    if (bankFeedTran.Amount.HasValue)
      return;
    this.ThrowFieldIsEmptyException("Amount", bankFeedTran);
  }

  private void ValidateAmountDependsOnParameter(BankFeedTransaction bankFeedTran)
  {
    string debitCreditParameter = bankFeedTran.DebitCreditParameter;
    Decimal? amount = bankFeedTran.Amount;
    if (debitCreditParameter == null)
      this.ThrowFieldIsEmptyException("DebitCreditParameter", bankFeedTran);
    if (!amount.HasValue)
      this.ThrowFieldIsEmptyException("Amount", bankFeedTran);
    Decimal? nullable = amount;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() < num & nullable.HasValue)
    {
      string transactionId = bankFeedTran.TransactionID;
      throw new FileContentValidationException("The amount must not be negative for {1} transaction (file {2}).", new object[3]
      {
        (object) PXUIFieldAttribute.GetDisplayName(this.GetImportGraph().Caches[typeof (BankFeedTransaction)], "Amount"),
        (object) transactionId,
        (object) this._currentFileName
      });
    }
  }

  private void ValidateAmountFromDifferentColumns(BankFeedTransaction bankFeedTran)
  {
    Decimal valueOrDefault1 = bankFeedTran.CreditAmount.GetValueOrDefault();
    Decimal valueOrDefault2 = bankFeedTran.DebitAmount.GetValueOrDefault();
    string transactionId = bankFeedTran.TransactionID;
    string displayName1 = PXUIFieldAttribute.GetDisplayName(this.GetImportGraph().Caches[typeof (BankFeedTransaction)], "CreditAmount");
    string displayName2 = PXUIFieldAttribute.GetDisplayName(this.GetImportGraph().Caches[typeof (BankFeedTransaction)], "DebitAmount");
    if (valueOrDefault1 != 0M && valueOrDefault2 != 0M)
      throw new FileContentValidationException("The system does not allow to have both values in the {0} and {1} columns for the {2} transaction (file {3}).", new object[4]
      {
        (object) displayName1,
        (object) displayName2,
        (object) transactionId,
        (object) this._currentFileName
      });
    if (valueOrDefault1 < 0M)
      throw new FileContentValidationException("The amount must not be negative for {1} transaction (file {2}).", new object[3]
      {
        (object) displayName1,
        (object) transactionId,
        (object) this._currentFileName
      });
    if (valueOrDefault2 < 0M)
      throw new FileContentValidationException("The amount must not be negative for {1} transaction (file {2}).", new object[3]
      {
        (object) displayName2,
        (object) transactionId,
        (object) this._currentFileName
      });
  }

  private bool IgnoreAndWriteToLogIfNeeded(
    LoadTransactionsData input,
    PXSYRow fileRow,
    Dictionary<string, int> transCounters)
  {
    string drCr = (string) null;
    CABankFeed currentBankFeed = this._currentBankFeed;
    string amountFormat = this.GetAmountFormat(currentBankFeed);
    if (!input.TestMode && amountFormat == "P")
    {
      this.SetValue("DebitCreditParameter", fileRow, (BankFeedTransaction) null, (Action<BankFeedTransaction, string>) ((o, i) => drCr = i));
      string lowerInvariant1 = currentBankFeed.CreditLabel.Trim().ToLowerInvariant();
      string lowerInvariant2 = currentBankFeed.DebitLabel.Trim().ToLowerInvariant();
      drCr = drCr.Trim().ToLowerInvariant();
      if (drCr != null && drCr != lowerInvariant2 && drCr != lowerInvariant1)
      {
        this.UpdateCounter("incorrectDrCr", transCounters);
        return true;
      }
    }
    return false;
  }

  private void UpdateCounter(string key, Dictionary<string, int> transCounters)
  {
    if (transCounters.ContainsKey(key))
      transCounters[key]++;
    else
      transCounters.Add(key, 1);
  }

  private void WriteParsingResultsToLog(string accountId, Dictionary<string, int> transCounters)
  {
    int num1;
    transCounters.TryGetValue("incorrectDrCr", out num1);
    int num2;
    transCounters.TryGetValue("zeroAmount", out num2);
    int num3;
    transCounters.TryGetValue("returnedTrans", out num3);
    PXTrace.WriteInformation($"Number of fetched transactions for the {accountId} account from {this._currentFileName}: {num3 + num1}. " + $"Transactions with 0 amount: {num2}. " + $"Other value in Receipt/Disbursement Property column: {num1}");
  }

  private string GetMappingTargetName(string mappingTarget)
  {
    return ((IEnumerable<(string, string)>) this.AvailableFieldsForMapping(this._currentBankFeed)).Where<(string, string)>((Func<(string, string), bool>) (i => i.Item1 == mappingTarget)).Select<(string, string), string>((Func<(string, string), string>) (i => i.Item2)).FirstOrDefault<string>();
  }

  private void ThrowFieldIsEmptyException(string mappingTarget, BankFeedTransaction tran)
  {
    string mappingTargetName = this.GetMappingTargetName(mappingTarget);
    if (tran.TransactionID == null)
      throw new FileContentValidationException("The {0} column cannot be empty (file {1}).", new object[2]
      {
        (object) mappingTargetName,
        (object) this._currentFileName
      });
    throw new FileContentValidationException("The {0} field cannot be empty (file {1} transaction {2}).", new object[3]
    {
      (object) mappingTargetName,
      (object) this._currentFileName,
      (object) tran.TransactionID
    });
  }

  private IPXSYProvider CreateProvider(SYProvider provider)
  {
    IPXSYProvider ipxsyProvider = (IPXSYProvider) null;
    try
    {
      if (provider != null)
      {
        if (!string.IsNullOrEmpty(provider.ProviderType))
        {
          Type type = PXBuildManager.GetType(provider.ProviderType, false);
          if (type != (Type) null)
            ipxsyProvider = Activator.CreateInstance(type) as IPXSYProvider;
        }
      }
    }
    catch
    {
      throw new PXException("The provider does not exist in the system.");
    }
    return ipxsyProvider != null ? ipxsyProvider : throw new PXException("The provider does not exist in the system.");
  }

  private Tuple<IFileExchange, IEnumerable<ExternalFileInfo>> GetSharedFolderFiles(
    CABankFeed bankFeed,
    string[] extensions)
  {
    try
    {
      string folderLogin = bankFeed.FolderLogin;
      string folderPassword = bankFeed.FolderPassword;
      string sshCertificateName = bankFeed.SshCertificateName;
      string folderPath = bankFeed.FolderPath;
      IFileExchange exchanger = this.GetExchanger(folderLogin, folderPassword, sshCertificateName);
      return new Tuple<IFileExchange, IEnumerable<ExternalFileInfo>>(exchanger, (IEnumerable<ExternalFileInfo>) exchanger.ListFiles(folderPath).OrderByDescending<ExternalFileInfo, DateTime>((Func<ExternalFileInfo, DateTime>) (i => i.Date)).Where<ExternalFileInfo>((Func<ExternalFileInfo, bool>) (i =>
      {
        string extension = Path.GetExtension(i.Name);
        return extension != null && ((IEnumerable<string>) extensions).Contains<string>(extension.ToLowerInvariant());
      })).ToList<ExternalFileInfo>());
    }
    catch (Exception ex)
    {
      throw new PXException("Test connection error: {0}", new object[1]
      {
        (object) ex.Message
      });
    }
  }

  private Guid UseExistingDataProvider(SYProviderMaint syProviderGraph)
  {
    SYProvider current = ((PXSelectBase<SYProvider>) syProviderGraph.Providers).Current;
    bool? isActive = current.IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
    {
      current.IsActive = new bool?(true);
      ((PXSelectBase<SYProvider>) syProviderGraph.Providers).Update(current);
      ((PXAction) ((PXGraph<SYProviderMaint, SYProvider>) syProviderGraph).Save).Press();
    }
    return current.ProviderID.Value;
  }

  private Guid CreateDataProvider(
    SYProviderMaint syProviderGraph,
    CABankFeed bankFeed,
    Tuple<IFileExchange, IEnumerable<ExternalFileInfo>> folderData)
  {
    (string ProviderType, string[] Extensions) andFileExtension = this.GetProviderTypeAndFileExtension();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      SYProvider syProvider1 = ((PXSelectBase<SYProvider>) syProviderGraph.Providers).Insert(new SYProvider()
      {
        Name = this.ProviderName(bankFeed.BankFeedID, andFileExtension.Extensions)
      });
      syProvider1.ProviderType = andFileExtension.ProviderType;
      SYProvider syProvider2 = ((PXSelectBase<SYProvider>) syProviderGraph.Providers).Update(syProvider1);
      if (this.NeedDataSample)
        this.SaveDataSample(bankFeed, folderData, syProvider2);
      ((PXAction) ((PXGraph<SYProviderMaint, SYProvider>) syProviderGraph).Save).Press();
      ((PXAction) syProviderGraph.FillSchemaObjects).Press();
      SYProviderObject syProviderObject = ((PXSelectBase<SYProviderObject>) syProviderGraph.Objects).SelectSingle(Array.Empty<object>());
      if (syProviderObject != null)
      {
        syProviderObject.IsActive = new bool?(true);
        syProviderObject = ((PXSelectBase<SYProviderObject>) syProviderGraph.Objects).Update(syProviderObject);
      }
      ((PXSelectBase<SYProviderObject>) syProviderGraph.Objects).Current = syProviderObject;
      ((PXAction) syProviderGraph.FillSchemaFields).Press();
      ((PXAction) ((PXGraph<SYProviderMaint, SYProvider>) syProviderGraph).Save).Press();
      transactionScope.Complete();
      return syProvider2.ProviderID.Value;
    }
  }

  private void SaveDataSample(
    CABankFeed bankFeed,
    Tuple<IFileExchange, IEnumerable<ExternalFileInfo>> folderData,
    SYProvider syProvider)
  {
    IEnumerable<ExternalFileInfo> source = folderData.Item2;
    IFileExchange ifileExchange = folderData.Item1;
    string fileFormat = bankFeed.FileFormat;
    string folderPath = bankFeed.FolderPath;
    ExternalFileInfo externalFileInfo = source.Count<ExternalFileInfo>() != 0 ? source.LastOrDefault<ExternalFileInfo>() : throw new PXException("No files with the {0} format were found in the {1} folder.", new object[2]
    {
      (object) CABankFeedFileFormat.GetCABankFeedFileFormatName(fileFormat),
      (object) folderPath
    });
    byte[] numArray = ifileExchange.Download(externalFileInfo.FullName);
    FileInfo fileInfo = new FileInfo(externalFileInfo.Name, (string) null, numArray);
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    if (!instance.SaveFile(fileInfo, (FileExistsAction) 1) || !fileInfo.UID.HasValue)
      throw new PXException("The {0} file cannot be saved.", new object[1]
      {
        (object) externalFileInfo.Name
      });
    string screenId = PXSiteMapProviderExtensions.FindSiteMapNodeByGraphType(PXSiteMap.Provider, typeof (CABankTransactionsImport).FullName)?.ScreenID;
    UploadFileMaintenance.UpdatePrimaryScreenId(((PXSelectBase<UploadFile>) instance.Files).Current.FileID, screenId);
    UploadFileRevision current = ((PXSelectBase<UploadFileRevision>) instance.Revisions).Current;
    current.OriginalTimestamp = new DateTime?(externalFileInfo.Date);
    ((PXSelectBase<UploadFileRevision>) instance.Revisions).Update(current);
    PXNoteAttribute.SetFileNotes(((PXGraph) instance).Caches[typeof (SYProvider)], (object) syProvider, new Guid[1]
    {
      fileInfo.UID.Value
    });
    ((PXGraph) instance).Actions.PressSave();
  }

  private void UpdateBankFeedDetail(CABankFeedDetail detail)
  {
    this.GetImportGraph().Caches[typeof (CABankFeedDetail)].Update((object) detail);
  }

  private void PersistBankFeedDetails()
  {
    PXCache cach = this.GetImportGraph().Caches[typeof (CABankFeedDetail)];
    foreach (object obj in cach.Updated)
    {
      cach.Persist(obj, (PXDBOperation) 1);
      cach.Persisted(false);
      PXTimeStampScope.PutPersisted(cach, obj, new object[1]
      {
        (object) PXDatabase.SelectTimeStamp()
      });
    }
  }

  private (FileInfo, string) GetSshCetificate(string sshCertificateName)
  {
    FileInfo fileInfo = (FileInfo) null;
    CetrificateFile cetrificateFile = (CetrificateFile) null;
    if (!string.IsNullOrEmpty(sshCertificateName))
    {
      CertificateMaintenance instance1 = PXGraph.CreateInstance<CertificateMaintenance>();
      PXDBCryptStringAttribute.SetDecrypted<Certificate.password>(((PXSelectBase) instance1.CertificateFile).Cache, true);
      cetrificateFile = PXResultset<CetrificateFile>.op_Implicit(((PXSelectBase<CetrificateFile>) instance1.CertificateFile).Select(new object[1]
      {
        (object) sshCertificateName
      }));
      if (cetrificateFile != null)
      {
        Guid? fileId = cetrificateFile.FileID;
        if (fileId.HasValue)
        {
          UploadFileMaintenance instance2 = PXGraph.CreateInstance<UploadFileMaintenance>();
          fileId = cetrificateFile.FileID;
          Guid guid = fileId.Value;
          fileInfo = instance2.GetFile(guid);
        }
      }
    }
    return (fileInfo, ((Certificate) cetrificateFile)?.Password);
  }
}
