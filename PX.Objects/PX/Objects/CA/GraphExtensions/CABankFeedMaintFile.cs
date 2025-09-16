// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.GraphExtensions.CABankFeedMaintFile
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CA.BankFeed;
using PX.Objects.Common;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CA.GraphExtensions;

public class CABankFeedMaintFile : PXGraphExtension<
#nullable disable
CABankFeedMaint>
{
  public PXSelect<CABankFeedFieldMapping, Where<CABankFeedFieldMapping.bankFeedID, Equal<Current<CABankFeed.bankFeedID>>>> BankFeedFileFieldMapping;
  [PXVirtualDAC]
  public PXSelectOrderBy<BankFeedTransaction, OrderBy<Desc<BankFeedTransaction.date>>> BankFeedFileTransactions;
  public PXFilter<CABankFeedMaintFile.ShowFileTransactionsFilter> ShowFileTransactions;
  public PXFilter<CABankFeedMaintFile.LoadFileTransactionsFilter> LoadFileTransactions;
  private FileBankFeedManager _fileBankFeedManager;
  public PXAction<CABankFeed> checkFileFolder;
  public PXAction<CABankFeed> setUpDataProvider;
  public PXAction<CABankFeed> loadTransactionsFromFile;

  public IEnumerable bankFeedFileTransactions()
  {
    foreach (object obj in ((PXSelectBase) this.BankFeedFileTransactions).Cache.Cached)
      yield return obj;
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankFeedIntegration>();

  [InjectDependency]
  internal Func<string, FileBankFeedManager> FileBankFeedManagerProvider { get; set; }

  [PXOverride]
  public virtual IEnumerable ActivateFeed(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> baseMethod)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    if (current == null || !(current.Type == "F"))
      return baseMethod(adapter);
    this.CheckFileSettings(current);
    this.CheckDataProvider(current);
    this.CheckMapping(current);
    return baseMethod(adapter);
  }

  [PXOverride]
  public virtual IEnumerable CheckConnection(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> baseMethod)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    if (current == null || !(current.Type == "F"))
      return baseMethod(adapter);
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaintFile.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new CABankFeedMaintFile.\u003C\u003Ec__DisplayClass14_0();
    this.CheckFileSettings(current);
    this.CheckDataProvider(current);
    this.CheckMapping(current);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.manager = this.GetFileSpecificManager(current);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.copy = this.CopyBankFeedRecord(current);
    ((PXGraph) this.Base).Caches[typeof (BankFeedFile)].Clear();
    // ISSUE: method pointer
    PXLongOperation.StartOperation<CABankFeedMaint>((PXGraphExtension<CABankFeedMaint>) this, new PXToggleAsyncDelegate((object) cDisplayClass140, __methodptr(\u003CCheckConnection\u003Eb__0)));
    return (IEnumerable) Enumerable.Empty<CABankFeed>();
  }

  [PXOverride]
  public virtual IEnumerable LoadTransactions(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> baseMethod)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    if (current == null || !(current.Type == "F"))
      return baseMethod(adapter);
    this.ShowTransactionsFromFile(current);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Test Connection")]
  [PXButton]
  public virtual IEnumerable CheckFileFolder(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaintFile.\u003C\u003Ec__DisplayClass17_0 cDisplayClass170 = new CABankFeedMaintFile.\u003C\u003Ec__DisplayClass17_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.bankFeed = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass170.bankFeed == null)
      return adapter.Get();
    ((PXAction) this.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    this.CheckFileSettings(cDisplayClass170.bankFeed);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.manager = this.GetFileSpecificManager(cDisplayClass170.bankFeed);
    // ISSUE: reference to a compiler-generated field
    this.CopyBankFeedRecord(cDisplayClass170.bankFeed);
    // ISSUE: method pointer
    PXLongOperation.StartOperation<CABankFeedMaint>((PXGraphExtension<CABankFeedMaint>) this, new PXToggleAsyncDelegate((object) cDisplayClass170, __methodptr(\u003CCheckFileFolder\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Set Up Data Provider")]
  [PXButton]
  public virtual IEnumerable SetUpDataProvider(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaintFile.\u003C\u003Ec__DisplayClass19_0 cDisplayClass190 = new CABankFeedMaintFile.\u003C\u003Ec__DisplayClass19_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.bankFeed = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass190.bankFeed == null)
      return adapter.Get();
    ((PXAction) this.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    this.CheckFileSettings(cDisplayClass190.bankFeed);
    // ISSUE: reference to a compiler-generated field
    this.CopyBankFeedRecord(cDisplayClass190.bankFeed);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.manager = this.GetFileSpecificManager(cDisplayClass190.bankFeed);
    // ISSUE: method pointer
    PXLongOperation.StartOperation<CABankFeedMaint>((PXGraphExtension<CABankFeedMaint>) this, new PXToggleAsyncDelegate((object) cDisplayClass190, __methodptr(\u003CSetUpDataProvider\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Load File Manually")]
  [PXButton(Category = "Processing")]
  public virtual IEnumerable LoadTransactionsFromFile(PXAdapter adapter)
  {
    CABankFeed current1 = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    CABankFeedMaintFile.LoadFileTransactionsFilter current2 = ((PXSelectBase<CABankFeedMaintFile.LoadFileTransactionsFilter>) this.LoadFileTransactions).Current;
    if (current2 != null && current1 != null && current1.Type == "F")
    {
      if (((PXSelectBase) this.LoadFileTransactions).View.Answer == 1)
        this.ImportTransactionsFromSpecificFile(current2, current1);
      else
        this.OpenLoadTransactionsFromFileDialog(current1);
    }
    return adapter.Get();
  }

  [PXOverride]
  public virtual (string, string)[] GetCorpCardFilters(
    CABankFeed bankFeed,
    Func<CABankFeed, (string, string)[]> baseMethod)
  {
    return bankFeed != null && bankFeed.Type == "F" ? this.GetFileSpecificManager(bankFeed).AvailableCorpCardFilters : baseMethod(bankFeed);
  }

  [PXOverride]
  public virtual void HandleCopyPaste(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    Action<bool, List<Command>, List<Container>> baseMethod)
  {
    foreach (int index in ((PXSelectBase<CABankFeed>) this.Base.BankFeed)?.Current?.Type == "F" ? EnumerableExtensions.SelectIndexesWhere<Command>((IEnumerable<Command>) script, (Func<Command, bool>) (_ => _.ObjectName == "BankFeedFileFieldMapping" || _.ObjectName == "BankFeedFieldMapping")).Reverse<int>() : EnumerableExtensions.SelectIndexesWhere<Command>((IEnumerable<Command>) script, (Func<Command, bool>) (_ => _.ObjectName == "BankFeedFileFieldMapping")).Reverse<int>())
    {
      script.RemoveAt(index);
      containers.RemoveAt(index);
    }
  }

  internal void AddFileFieldsMapping(CABankFeed bankFeed)
  {
    FileBankFeedManager fileSpecificManager = this.GetFileSpecificManager(bankFeed);
    (string, string)[] tupleArray = fileSpecificManager.AvailableFieldsForMapping(((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current);
    HashSet<string> fieldsForMapping = fileSpecificManager.ReqiredFieldsForMapping;
    Dictionary<string, string> defaultFieldMapping = fileSpecificManager.DefaultFieldMapping;
    foreach ((string key, string _) in tupleArray)
      ((PXSelectBase<CABankFeedFieldMapping>) this.BankFeedFileFieldMapping).Insert(new CABankFeedFieldMapping()
      {
        TargetField = key,
        Active = new bool?(fieldsForMapping.Contains(key)),
        SourceFieldOrValue = defaultFieldMapping.ContainsKey(key) ? defaultFieldMapping[key] : ""
      });
  }

  internal void ClearFileFieldsMapping()
  {
    foreach (PXResult<CABankFeedFieldMapping> pxResult in ((PXSelectBase<CABankFeedFieldMapping>) this.Base.BankFeedFieldMapping).Select(Array.Empty<object>()))
      ((PXSelectBase<CABankFeedFieldMapping>) this.Base.BankFeedFieldMapping).Delete(PXResult<CABankFeedFieldMapping>.op_Implicit(pxResult));
  }

  internal void ClearCurrentFileId()
  {
    foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.Base.BankFeedDetail).Select(Array.Empty<object>()))
    {
      CABankFeedDetail detail = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
      this.ClearCurrentFileId(detail);
      ((PXSelectBase<CABankFeedDetail>) this.Base.BankFeedDetail).Update(detail);
    }
  }

  internal void ClearCurrentFileId(CABankFeedDetail detail)
  {
    detail.FileID = new Guid?();
    detail.FileDateTime = new DateTime?();
    detail.FileRevisionID = new int?();
  }

  protected virtual void _(Events.RowSelected<CABankFeed> e)
  {
    CABankFeed row = e.Row;
    PXCache cache1 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankFeed>>) e).Cache;
    if (row == null)
      return;
    bool flag1 = row.Type == "F";
    bool flag2 = row.Status == "R";
    ((PXSelectBase) this.BankFeedFileFieldMapping).AllowSelect = flag1;
    ((PXAction) this.checkFileFolder).SetVisible(flag1);
    ((PXAction) this.setUpDataProvider).SetVisible(flag1);
    ((PXAction) this.loadTransactionsFromFile).SetVisible(flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.folderPath>(cache1, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.fileFormat>(cache1, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.folderLogin>(cache1, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.folderPassword>(cache1, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.sshCertificateName>(cache1, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.fileAmountFormat>(cache1, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.debitLabel>(cache1, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.creditLabel>(cache1, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.providerID>(cache1, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CABankFeed.institution>(cache1, (object) row, flag1);
    if (!flag1)
      return;
    PXCache cache2 = ((PXSelectBase) this.BankFeedFileFieldMapping).Cache;
    FileBankFeedManager fileSpecificManager = this.GetFileSpecificManager(row);
    (string, string)[] valueTupleArray = fileSpecificManager.AvailableFieldsForMapping(row);
    bool hasValue = row.ProviderID.HasValue;
    bool flag3 = fileSpecificManager.PredefinedAmountFormat == null;
    bool flag4 = row.FileAmountFormat == "P";
    PXStringListAttribute.SetList<CABankFeedFieldMapping.targetField>(cache2, (object) null, valueTupleArray);
    ((PXSelectBase) this.BankFeedFileFieldMapping).AllowDelete = false;
    ((PXSelectBase) this.BankFeedFileFieldMapping).AllowInsert = false;
    PXUIFieldAttribute.SetVisible<CABankFeed.fileAmountFormat>(cache1, (object) row, flag3 & hasValue);
    PXUIFieldAttribute.SetVisible<CABankFeed.debitLabel>(cache1, (object) row, flag4 & hasValue);
    PXUIFieldAttribute.SetVisible<CABankFeed.creditLabel>(cache1, (object) row, flag4 & hasValue);
    PXUIFieldAttribute.SetEnabled<CABankFeed.institution>(cache1, (object) row, flag2);
    PXUIFieldAttribute.SetRequired<CABankFeed.institution>(cache1, true);
    PXUIFieldAttribute.SetEnabled<CABankFeedFieldMapping.targetField>(cache2, (object) null, false);
    PXStringListAttribute.SetList<CABankFeedExpense.matchField>(((PXSelectBase) this.Base.BankFeedExpense).Cache, (object) null, this.GetFileSpecificManager(row).AvailableExpenseReceiptFilters);
    this.ShowProviderWarnIfNeeded(cache1, row);
  }

  protected virtual void _(Events.RowSelected<CABankFeedCorpCard> e)
  {
    CABankFeedCorpCard row = e.Row;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankFeedCorpCard>>) e).Cache;
    if (row == null)
      return;
    this.SetMatchFieldWarningIfNeeded(row, cache);
  }

  protected virtual void _(
    Events.RowSelected<CABankFeedMaintFile.ShowFileTransactionsFilter> e)
  {
    PXCache cache = ((PXSelectBase) this.BankFeedFileTransactions).Cache;
    CABankFeedMaintFile.ShowFileTransactionsFilter row = e.Row;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    if (row == null || current == null || current.BankFeedID == null)
      return;
    string[] transactionFields = this.GetAvailableFileTransactionFields(current);
    if (transactionFields == null)
      return;
    foreach (string str in transactionFields)
      PXUIFieldAttribute.SetVisible(cache, (object) null, str, true);
  }

  protected virtual void _(Events.RowSelected<CABankFeedFieldMapping> e)
  {
    CABankFeedFieldMapping row = e.Row;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankFeedFieldMapping>>) e).Cache;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    if (row == null || current == null || !(current.Type == "F"))
      return;
    FileBankFeedManager fileSpecificManager = this.GetFileSpecificManager(current);
    HashSet<string> fieldsForMapping = fileSpecificManager.ReqiredFieldsForMapping;
    bool requiredFieldsMapping = fileSpecificManager.CanChangeRequiredFieldsMapping;
    bool flag = !fieldsForMapping.Contains(row.TargetField);
    bool hasValue = current.ProviderID.HasValue;
    PXUIFieldAttribute.SetEnabled<CABankFeedFieldMapping.active>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<CABankFeedFieldMapping.sourceFieldOrValue>(cache, (object) row, hasValue && requiredFieldsMapping | flag);
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.fileAmountFormat> e)
  {
    if (!(e.Row is CABankFeed row))
      return;
    this.ClearFileFieldsMapping();
    if (row.ProviderID.HasValue)
      this.AddFileFieldsMapping(row);
    row.Status = "R";
  }

  protected virtual void _(
    Events.FieldUpdated<CABankFeedFieldMapping.sourceFieldOrValue> e)
  {
    CABankFeedFieldMapping row = e.Row as CABankFeedFieldMapping;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    if (row == null || current == null)
      return;
    string newValue = e.NewValue as string;
    if (!this.GetFileSpecificManager(current).ReqiredFieldsForMapping.Contains(row.TargetField))
      return;
    this.SetSetupRequiredStatusIfNeeded(current, newValue);
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.fileFormat> e)
  {
    if (!(e.Row is CABankFeed row))
      return;
    string predefinedAmountFormat = this.GetFileSpecificManager(row).PredefinedAmountFormat;
    if (predefinedAmountFormat != null)
      ((PXSelectBase) this.Base.BankFeed).Cache.SetValueExt<CABankFeed.fileAmountFormat>((object) row, (object) predefinedAmountFormat);
    else
      ((PXSelectBase) this.Base.BankFeed).Cache.SetDefaultExt<CABankFeed.fileAmountFormat>((object) row);
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.type> e)
  {
    if (!(e.Row is CABankFeed row))
      return;
    string newValue = e.NewValue as string;
    string oldValue = ((Events.FieldUpdatedBase<Events.FieldUpdated<CABankFeed.type>, object, object>) e).OldValue as string;
    string str = "D";
    if (newValue == "F")
    {
      str = "R";
      this.DeleteMapping();
      this.DeleteExpenseReceiptsMapping();
      if (row.ProviderID.HasValue)
        this.AddFileFieldsMapping(row);
    }
    if (oldValue == "F")
    {
      this.Base.UnmapBankFeedDetails();
      this.Base.DeleteAccounts();
      this.DeleteMapping();
      this.DeleteExpenseReceiptsMapping();
    }
    ((PXSelectBase) this.Base.BankFeed).Cache.SetValue<CABankFeed.status>((object) row, (object) str);
  }

  protected virtual void _(
    Events.FieldUpdated<CABankFeedDetail.accountName> e)
  {
    CABankFeedDetail row = e.Row as CABankFeedDetail;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    if (row == null || current == null)
      return;
    string newValue = e.NewValue as string;
    if (!(current.Type == "F"))
      return;
    row.AccountID = newValue;
    this.ClearCurrentFileId(row);
    if (newValue == null || !current.MultipleMapping.GetValueOrDefault() || current.InstitutionID == null || !row.CashAccountID.HasValue)
      return;
    this.Base.UnmapBankFeedDetail(row);
    this.Base.AddOrUpdateFeedAccountCashAccountMapping(row, this.Base.GetStoredMappingForInstitution(current));
  }

  protected virtual void _(
    Events.FieldUpdated<CABankFeedDetail.cashAccountID> e)
  {
    if (!(e.Row is CABankFeedDetail row))
      return;
    this.ClearCurrentFileId(row);
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.folderPath> e)
  {
    if (!(e.Row is CABankFeed row))
      return;
    string newValue = e.NewValue as string;
    this.SetSetupRequiredStatusIfNeeded(row, newValue);
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.folderLogin> e)
  {
    if (!(e.Row is CABankFeed row))
      return;
    string newValue = e.NewValue as string;
    this.SetSetupRequiredStatusIfNeeded(row, newValue);
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.folderPassword> e)
  {
    if (!(e.Row is CABankFeed row))
      return;
    string newValue = e.NewValue as string;
    this.SetSetupRequiredStatusIfNeeded(row, newValue);
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.creditLabel> e)
  {
    if (!(e.Row is CABankFeed row))
      return;
    string newValue = e.NewValue as string;
    this.SetSetupRequiredStatusIfNeeded(row, newValue);
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.debitLabel> e)
  {
    if (!(e.Row is CABankFeed row))
      return;
    string newValue = e.NewValue as string;
    this.SetSetupRequiredStatusIfNeeded(row, newValue);
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.institution> e)
  {
    if (!(e.Row is CABankFeed row))
      return;
    string newValue = e.NewValue as string;
    if (!(row.Type == "F"))
      return;
    if (row.MultipleMapping.GetValueOrDefault() && newValue != null)
    {
      if (((PXSelectBase<CABankFeed>) this.Base.BankFeed).Ask("If you change the Financial Institution, the history of bank account transactions will be lost and the default Import Transactions From date will be incorrect. Continue?", (MessageButtons) 1) != 1)
        return;
      row.InstitutionID = newValue;
      foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.Base.BankFeedDetail).Select(Array.Empty<object>()))
      {
        CABankFeedDetail det = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
        if (det.CashAccountID.HasValue)
        {
          this.Base.UnmapBankFeedDetail(det);
          if (row.InstitutionID != null)
            this.Base.AddOrUpdateFeedAccountCashAccountMapping(det, this.Base.GetStoredMappingForInstitution(row));
        }
      }
    }
    else
      row.InstitutionID = newValue;
  }

  protected virtual void _(Events.FieldVerifying<CABankFeed.type> e)
  {
    if (!(e.Row is CABankFeed))
      return;
    string oldValue = e.OldValue as string;
    if (!(oldValue == "F") || ((PXSelectBase) this.Base.CurrentBankFeed).View.AskOKCancelWithCallback((object) null, "Warning", "Changing the Bank Feed Type will reset the setup. Continue?", (MessageIcon) 3) == 1)
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<CABankFeed.type>>) e).Cancel = true;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<CABankFeed.type>, object, object>) e).NewValue = (object) oldValue;
  }

  protected virtual void _(Events.RowPersisting<CABankFeed> e)
  {
    CABankFeed row = e.Row;
    if (e.Operation == 3 || !(row.Type == "F"))
      return;
    if (string.IsNullOrEmpty(row.Institution))
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) row, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeed.institution>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CABankFeed>>) e).Cache)
      });
      ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CABankFeed>>) e).Cache.RaiseExceptionHandling<CABankFeed.institution>((object) row, (object) row.Institution, (Exception) propertyException);
      throw propertyException;
    }
    foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.Base.BankFeedDetail).Select(Array.Empty<object>()))
    {
      CABankFeedDetail det = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
      this.CheckFileBankFeedAccount(row, det);
    }
  }

  protected virtual void _(Events.RowPersisting<CABankFeedFieldMapping> e)
  {
    CABankFeedFieldMapping row = e.Row;
    PXCache cache = ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CABankFeedFieldMapping>>) e).Cache;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    if (row == null || current == null)
      return;
    bool flag = !this.GetFileSpecificManager(current).ReqiredFieldsForMapping.Contains(row.TargetField);
    PXDefaultAttribute.SetPersistingCheck<CABankFeedFieldMapping.sourceFieldOrValue>(cache, (object) row, current.Type != "F" || row.Active.GetValueOrDefault() & flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected virtual void _(Events.RowPersisting<CABankFeedDetail> e)
  {
    if (e.Operation == 3)
      return;
    CABankFeedDetail row = e.Row;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    if (!(current.Type == "F"))
      return;
    this.CheckFileBankFeedAccount(current, row);
  }

  protected virtual void _(Events.RowDeleted<CABankFeedDetail> e)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.Base.BankFeed).Current;
    CABankFeedDetail row = e.Row;
    if (row == null || current == null || !(current.Type == "F"))
      return;
    this.Base.UnmapBankFeedDetail(row);
    if (GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.Base.BankFeedDetail).Select(Array.Empty<object>())).Count<CABankFeedDetail>() != 0)
      return;
    ((PXSelectBase) this.Base.BankFeed).Cache.SetValue<CABankFeed.status>((object) current, (object) "R");
  }

  protected virtual FileBankFeedManager GetFileSpecificManager(CABankFeed bankFeed)
  {
    if (this._fileBankFeedManager == null || this._fileBankFeedManager.FileFormat != bankFeed.FileFormat)
      this._fileBankFeedManager = this.FileBankFeedManagerProvider(bankFeed.FileFormat);
    return this._fileBankFeedManager;
  }

  protected virtual void DeleteMapping()
  {
    foreach (PXResult<CABankFeedFieldMapping> pxResult in ((PXSelectBase<CABankFeedFieldMapping>) this.Base.BankFeedFieldMapping).Select(Array.Empty<object>()))
      ((PXSelectBase<CABankFeedFieldMapping>) this.Base.BankFeedFieldMapping).Delete(PXResult<CABankFeedFieldMapping>.op_Implicit(pxResult));
  }

  protected virtual void DeleteExpenseReceiptsMapping()
  {
    foreach (PXResult<CABankFeedExpense> pxResult in ((PXSelectBase<CABankFeedExpense>) this.Base.BankFeedExpense).Select(Array.Empty<object>()))
      ((PXSelectBase<CABankFeedExpense>) this.Base.BankFeedExpense).Delete(PXResult<CABankFeedExpense>.op_Implicit(pxResult));
  }

  private void ImportTransactionsFromSpecificFile(
    CABankFeedMaintFile.LoadFileTransactionsFilter loadFilter,
    CABankFeed bankFeed)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaintFile.\u003C\u003Ec__DisplayClass52_0 cDisplayClass520 = new CABankFeedMaintFile.\u003C\u003Ec__DisplayClass52_0();
    if (loadFilter.FileName == null)
    {
      PXCache cache = ((PXSelectBase) this.LoadFileTransactions).Cache;
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) loadFilter, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeedMaintFile.LoadFileTransactionsFilter.fileName>(cache)
      });
      cache.RaiseExceptionHandling<CABankFeedMaintFile.LoadFileTransactionsFilter.fileName>((object) loadFilter, (object) loadFilter.FileName, (Exception) propertyException);
      throw propertyException;
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass520.copy = this.CopyBankFeedRecord(bankFeed);
    this.GetFileSpecificManager(bankFeed);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass520.fileName = loadFilter.FileName;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass520.ignoreDates = loadFilter.IgnoreDates;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass520.guid = (Guid) ((PXGraph) this.Base).UID;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass520, __methodptr(\u003CImportTransactionsFromSpecificFile\u003Eb__0)));
  }

  private void OpenLoadTransactionsFromFileDialog(CABankFeed bankFeed)
  {
    this.CheckFileSettings(bankFeed);
    this.CheckDataProvider(bankFeed);
    this.CheckMapping(bankFeed);
    if (((PXGraph) this.Base).Caches[typeof (BankFeedFile)].Cached.Count() == 0L)
    {
      foreach (object sharedFolderFile in this.GetFileSpecificManager(bankFeed).GetSharedFolderFiles(this.CopyBankFeedRecord(bankFeed)))
        GraphHelper.Hold(((PXGraph) this.Base).Caches[typeof (BankFeedFile)], sharedFolderFile);
    }
    // ISSUE: method pointer
    ((PXSelectBase<CABankFeedMaintFile.LoadFileTransactionsFilter>) this.LoadFileTransactions).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003COpenLoadTransactionsFromFileDialog\u003Eb__53_0)), true);
  }

  private void ShowTransactionsFromFile(CABankFeed bankFeed)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaintFile.\u003C\u003Ec__DisplayClass54_0 cDisplayClass540 = new CABankFeedMaintFile.\u003C\u003Ec__DisplayClass54_0();
    CABankFeedMaintFile.ShowFileTransactionsFilter current = ((PXSelectBase<CABankFeedMaintFile.ShowFileTransactionsFilter>) this.ShowFileTransactions).Current;
    if (current == null)
      return;
    if (current.FileName == null)
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) current, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeedMaintFile.ShowFileTransactionsFilter.fileName>(((PXSelectBase) this.ShowFileTransactions).Cache)
      });
      ((PXSelectBase) this.ShowFileTransactions).Cache.RaiseExceptionHandling<CABankFeedMaintFile.ShowFileTransactionsFilter.fileName>((object) current, (object) current.FileName, (Exception) propertyException);
      throw propertyException;
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass540.input = new LoadTransactionsData();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass540.input.TransactionsOrder = LoadTransactionsData.Order.DescDate;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass540.input.TestMode = true;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass540.manager = this.GetFileSpecificManager(bankFeed);
    BankFeedFile bankFeedFile = PXSelectorAttribute.Select(((PXSelectBase) this.ShowFileTransactions).Cache, (object) current, "fileName") as BankFeedFile;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass540.fileCopy = ((PXGraph) this.Base).Caches[typeof (BankFeedFile)].CreateCopy((object) bankFeedFile) as BankFeedFile;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass540.bankFeedCopy = this.CopyBankFeedRecord(bankFeed);
    ((PXSelectBase) this.BankFeedFileTransactions).Cache.Clear();
    PXLongOperation.ClearStatus(((PXGraph) this.Base).UID);
    // ISSUE: method pointer
    PXLongOperation.StartOperation<CABankFeedMaint>((PXGraphExtension<CABankFeedMaint>) this, new PXToggleAsyncDelegate((object) cDisplayClass540, __methodptr(\u003CShowTransactionsFromFile\u003Eb__0)));
    PXLongOperation.WaitCompletion(((PXGraph) this.Base).UID);
    object customInfo1 = PXLongOperation.GetCustomInfo(((PXGraph) this.Base).UID, "Error");
    if (customInfo1 != null && customInfo1 is Exception)
    {
      PXLongOperation.ClearStatus(((PXGraph) this.Base).UID);
      throw (Exception) customInfo1;
    }
    if (!(PXLongOperation.GetCustomInfo(((PXGraph) this.Base).UID, "Transactions") is IEnumerable<BankFeedTransaction> customInfo2))
      return;
    PXLongOperation.ClearStatus(((PXGraph) this.Base).UID);
    foreach (object obj in customInfo2)
      GraphHelper.Hold(((PXSelectBase) this.BankFeedFileTransactions).Cache, obj);
  }

  private void CheckFileSettings(CABankFeed bankFeed)
  {
    PXCache cache = ((PXSelectBase) this.Base.BankFeed).Cache;
    if (string.IsNullOrEmpty(bankFeed.FolderPath))
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) bankFeed, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeed.folderPath>(cache)
      });
      cache.RaiseExceptionHandling<CABankFeed.folderPath>((object) bankFeed, (object) bankFeed.FolderPath, (Exception) propertyException);
      throw propertyException;
    }
    if (!bankFeed.FolderPath.StartsWith("sftp://"))
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) bankFeed, "The {0} box should start with '{1}'.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeed.folderPath>(cache),
        (object) "sftp://"
      });
      cache.RaiseExceptionHandling<CABankFeed.folderPath>((object) bankFeed, (object) bankFeed.FolderPath, (Exception) propertyException);
      throw propertyException;
    }
    if (string.IsNullOrEmpty(bankFeed.FolderLogin))
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) bankFeed, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeed.folderLogin>(cache)
      });
      cache.RaiseExceptionHandling<CABankFeed.folderLogin>((object) bankFeed, (object) bankFeed.FolderLogin, (Exception) propertyException);
      throw propertyException;
    }
    if (string.IsNullOrEmpty(bankFeed.FolderPassword) && string.IsNullOrEmpty(bankFeed.SshCertificateName))
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) bankFeed, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeed.folderPassword>(cache)
      });
      cache.RaiseExceptionHandling<CABankFeed.folderPassword>((object) bankFeed, (object) bankFeed.FolderPassword, (Exception) propertyException);
      throw propertyException;
    }
    if (string.IsNullOrEmpty(bankFeed.DebitLabel) && bankFeed.FileAmountFormat == "P")
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) bankFeed, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeed.debitLabel>(cache)
      });
      cache.RaiseExceptionHandling<CABankFeed.debitLabel>((object) bankFeed, (object) bankFeed.DebitLabel, (Exception) propertyException);
      throw propertyException;
    }
    if (string.IsNullOrEmpty(bankFeed.CreditLabel) && bankFeed.FileAmountFormat == "P")
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) bankFeed, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeed.creditLabel>(cache)
      });
      cache.RaiseExceptionHandling<CABankFeed.creditLabel>((object) bankFeed, (object) bankFeed.CreditLabel, (Exception) propertyException);
      throw propertyException;
    }
  }

  private void CheckDataProvider(CABankFeed bankFeed)
  {
    PXCache cache = ((PXSelectBase) this.Base.BankFeed).Cache;
    if (!bankFeed.ProviderID.HasValue)
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) bankFeed, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeed.providerID>(cache)
      });
      cache.RaiseExceptionHandling<CABankFeed.providerID>((object) bankFeed, (object) bankFeed.ProviderID, (Exception) propertyException);
      throw propertyException;
    }
  }

  private void CheckMapping(CABankFeed bankFeed)
  {
    HashSet<string> fieldsForMapping = this.GetFileSpecificManager(bankFeed).ReqiredFieldsForMapping;
    PXCache cache = ((PXSelectBase) this.BankFeedFileFieldMapping).Cache;
    foreach (CABankFeedFieldMapping feedFieldMapping in GraphHelper.RowCast<CABankFeedFieldMapping>((IEnumerable) ((PXSelectBase<CABankFeedFieldMapping>) this.BankFeedFileFieldMapping).Select(Array.Empty<object>())))
    {
      if (fieldsForMapping.Contains(feedFieldMapping.TargetField) && string.IsNullOrEmpty(feedFieldMapping.SourceFieldOrValue))
      {
        PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) feedFieldMapping, "'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<CABankFeedFieldMapping.sourceFieldOrValue>(cache)
        });
        cache.RaiseExceptionHandling<CABankFeedFieldMapping.sourceFieldOrValue>((object) feedFieldMapping, (object) feedFieldMapping.SourceFieldOrValue, (Exception) propertyException);
        throw propertyException;
      }
    }
  }

  private void CheckFileBankFeedAccount(CABankFeed bankFeed, CABankFeedDetail det)
  {
    PXCache cache = ((PXSelectBase) this.Base.BankFeedDetail).Cache;
    CABankFeed caBankFeed = (CABankFeed) null;
    CABankFeedDetail caBankFeedDetail1 = (CABankFeedDetail) null;
    this.CheckFileBankFeedAccountFields(det);
    string lowerInvariant1 = det.AccountName.Trim().ToLowerInvariant();
    foreach (CABankFeedDetail caBankFeedDetail2 in ((PXGraph) this.Base).Caches[typeof (CABankFeedDetail)].Cached)
    {
      string lowerInvariant2 = caBankFeedDetail2.AccountName.Trim().ToLowerInvariant();
      int? lineNbr1 = caBankFeedDetail2.LineNbr;
      int? lineNbr2 = det.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) && caBankFeedDetail2.BankFeedID == bankFeed.BankFeedID && lowerInvariant2 == lowerInvariant1)
      {
        PXEntryStatus status = ((PXSelectBase) this.Base.BankFeedDetail).Cache.GetStatus((object) caBankFeedDetail2);
        if (status != 3 && status != 4)
        {
          caBankFeedDetail1 = caBankFeedDetail2;
          caBankFeed = bankFeed;
          break;
        }
      }
    }
    if (caBankFeedDetail1 != null)
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) det, "The {0} account is already linked to the {1} bank feed.", (PXErrorLevel) 5, new object[2]
      {
        (object) caBankFeedDetail1.AccountName,
        (object) caBankFeed.BankFeedID
      });
      cache.RaiseExceptionHandling<CABankFeedDetail.accountName>((object) det, (object) null, (Exception) propertyException);
      throw propertyException;
    }
    this.Base.CheckFeedAccountAlreadyExists(this.Base.GetBankFeedDetailsByInstitution(bankFeed.InstitutionID), lowerInvariant1, (string) null);
  }

  private void CheckFileBankFeedAccountFields(CABankFeedDetail det)
  {
    PXCache cache = ((PXSelectBase) this.Base.BankFeedDetail).Cache;
    if (string.IsNullOrEmpty(det.AccountName))
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) det, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeedDetail.accountName>(cache)
      });
      cache.RaiseExceptionHandling<CABankFeedDetail.accountName>((object) det, (object) det.AccountName, (Exception) propertyException);
      throw propertyException;
    }
    if (string.IsNullOrEmpty(det.AccountID))
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) det, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeedDetail.accountID>(cache)
      });
      cache.RaiseExceptionHandling<CABankFeedDetail.accountID>((object) det, (object) det.AccountID, (Exception) propertyException);
      throw propertyException;
    }
    if (string.IsNullOrEmpty(det.Currency))
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) det, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeedDetail.currency>(cache)
      });
      cache.RaiseExceptionHandling<CABankFeedDetail.currency>((object) det, (object) det.Currency, (Exception) propertyException);
      throw propertyException;
    }
  }

  private void SetMatchFieldWarningIfNeeded(CABankFeedCorpCard corpCard, PXCache cache)
  {
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (corpCard.AccountID != null && corpCard.MatchField != "N")
    {
      string matchField = corpCard.MatchField;
      string targetField = (string) null;
      switch (matchField)
      {
        case "U":
          targetField = "UserDesc";
          break;
        case "R":
          targetField = "CardNumber";
          break;
        case "Y":
          targetField = "PayeeName";
          break;
      }
      if (targetField != null)
      {
        bool? active = GraphHelper.RowCast<CABankFeedFieldMapping>((IEnumerable) ((PXSelectBase<CABankFeedFieldMapping>) this.BankFeedFileFieldMapping).Select(Array.Empty<object>())).Where<CABankFeedFieldMapping>((Func<CABankFeedFieldMapping, bool>) (i => i.TargetField == targetField)).FirstOrDefault<CABankFeedFieldMapping>().Active;
        bool flag = false;
        if (active.GetValueOrDefault() == flag & active.HasValue)
          propertyException = new PXSetPropertyException((IBqlTable) corpCard, "Mapping was not set on the Source File tab.", (PXErrorLevel) 2);
      }
    }
    cache.RaiseExceptionHandling<CABankFeedCorpCard.matchField>((object) corpCard, (object) corpCard.MatchField, (Exception) propertyException);
  }

  private string[] GetAvailableFileTransactionFields(CABankFeed bankFeed)
  {
    return bankFeed.Type == "F" ? ((IEnumerable<(string, string)>) this.GetFileSpecificManager(bankFeed).AvailableFieldsForMapping(bankFeed)).Select<(string, string), string>((Func<(string, string), string>) (i => i.Item1)).ToArray<string>() : (string[]) null;
  }

  private void SetSetupRequiredStatusIfNeeded(CABankFeed bankFeed, string newValue)
  {
    if (!(bankFeed.Type == "F") || !string.IsNullOrEmpty(newValue))
      return;
    ((PXSelectBase) this.Base.BankFeed).Cache.SetValue<CABankFeed.status>((object) bankFeed, (object) "R");
  }

  private void ShowProviderWarnIfNeeded(PXCache cache, CABankFeed bankFeed)
  {
    Guid? providerId = bankFeed.ProviderID;
    if (!providerId.HasValue)
      return;
    if (((IQueryable<PXResult<SYProvider>>) PXSelectBase<SYProvider, PXViewOf<SYProvider>.BasedOn<SelectFromBase<SYProvider, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SYProvider.providerID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) providerId
    })).Any<PXResult<SYProvider>>())
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) bankFeed, "The data provider could not be found in the system.", (PXErrorLevel) 2);
    cache.RaiseExceptionHandling<CABankFeed.providerID>((object) bankFeed, (object) providerId, (Exception) propertyException);
  }

  private bool NeedSetupFileIntegration(CABankFeed bankFeed)
  {
    return bankFeed.Type == "F" && bankFeed.Status == "R";
  }

  private CABankFeed CopyBankFeedRecord(CABankFeed bankFeed)
  {
    return (CABankFeed) ((PXSelectBase) this.Base.BankFeed).Cache.CreateCopy((object) bankFeed);
  }

  /// <summary>
  /// Defines the filter that contains a list of files from the shared folder.
  /// </summary>
  [PXCacheName("ShowFileTransactionsFilter")]
  public class ShowFileTransactionsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>The file name from the shared folder.</summary>
    [PXString]
    [CABankFeedFile]
    [PXUIField(DisplayName = "File with Transactions")]
    public virtual string FileName { get; set; }

    public abstract class fileName : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      CABankFeedMaintFile.ShowFileTransactionsFilter.fileName>
    {
    }
  }

  /// <summary>
  /// Defines the filter that contains a list of files from the shared folder.
  /// </summary>
  [PXCacheName("LoadFileManuallyFilter")]
  public class LoadFileTransactionsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>The file name from the shared folder.</summary>
    [PXString]
    [CABankFeedFile]
    [PXUIField(DisplayName = "File with Transactions")]
    public virtual string FileName { get; set; }

    /// <summary>
    /// Specifies that the import of transactions ignores the dates of the latest transactions in bank statements.
    /// </summary>
    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Create Transactions Before Import Transactions From Date")]
    public virtual bool? IgnoreDates { get; set; }

    public abstract class fileName : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      CABankFeedMaintFile.LoadFileTransactionsFilter.fileName>
    {
    }

    public abstract class ignoreDates : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CABankFeedMaintFile.LoadFileTransactionsFilter.ignoreDates>
    {
    }
  }
}
