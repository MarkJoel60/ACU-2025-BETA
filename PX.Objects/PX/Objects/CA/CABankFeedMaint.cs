// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA.BankFeed;
using PX.Objects.CA.Descriptor;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.EP.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

public class CABankFeedMaint : PXGraph<
#nullable disable
CABankFeedMaint, CABankFeed>
{
  public PXFilter<CABankFeedMaint.TransactionsFilter> Filter;
  public PXSelect<CABankFeed> BankFeed;
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelect<CABankFeedDetail, Where<CABankFeedDetail.bankFeedID, Equal<Current<CABankFeed.bankFeedID>>>> BankFeedDetail;
  public PXSelect<CABankFeedCorpCard, Where<CABankFeedCorpCard.bankFeedID, Equal<Current<CABankFeed.bankFeedID>>>> BankFeedCorpCC;
  public PXSelect<CABankFeed, Where<CABankFeed.bankFeedID, Equal<Current<CABankFeed.bankFeedID>>>> CurrentBankFeed;
  public PXSelect<CABankFeedExpense, Where<CABankFeedExpense.bankFeedID, Equal<Current<CABankFeed.bankFeedID>>>> BankFeedExpense;
  public PXSelect<CashAccount> CashAccounts;
  public PXSelect<CABankFeedFieldMapping, Where<CABankFeedFieldMapping.bankFeedID, Equal<Current<CABankFeed.bankFeedID>>>> BankFeedFieldMapping;
  public PXSelect<CABankFeedAccountMapping, Where<CABankFeedAccountMapping.bankFeedID, Equal<Optional<CABankFeed.bankFeedID>>>> BankFeedAccountMapping;
  [PXVirtualDAC]
  public PXSelect<BankFeedCategory> BankFeedCategories;
  [PXVirtualDAC]
  public PXSelectOrderBy<BankFeedTransaction, OrderBy<Desc<BankFeedTransaction.date>>> BankFeedTransactions;
  public PXAction<CABankFeed> activateFeed;
  public PXAction<CABankFeed> connectFeed;
  public PXAction<CABankFeed> updateFeed;
  public PXAction<CABankFeed> syncUpdateFeed;
  public PXAction<CABankFeed> syncConnectFeed;
  public PXAction<CABankFeed> suspendFeed;
  public PXAction<CABankFeed> disconnectFeed;
  public PXAction<CABankFeed> showCategories;
  public PXAction<CABankFeed> showAccessId;
  public PXAction<CABankFeed> checkConnection;
  public PXAction<CABankFeed> migrateFeed;
  public PXAction<CABankFeed> loadTransactions;
  public PXCancelClose<CABankFeed> cancelClose;
  public PXAction<CABankFeed> setDefaultMapping;

  private (string, string)[] CorpCardFilters { get; set; }

  private string[] AvailableTransactionFields { get; set; }

  [InjectDependency]
  internal Func<string, BankFeedManager> BankFeedManagerProvider { get; set; }

  public IEnumerable bankFeedCategories()
  {
    foreach (object obj in ((PXSelectBase) this.BankFeedCategories).Cache.Cached)
      yield return obj;
  }

  public IEnumerable bankFeedTransactions()
  {
    foreach (object obj in ((PXSelectBase) this.BankFeedTransactions).Cache.Cached)
      yield return obj;
  }

  [PXUIField(DisplayName = "Activate", Visible = true)]
  [PXButton(CommitChanges = true, Category = "Processing")]
  public virtual IEnumerable ActivateFeed(PXAdapter adapter)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current == null)
      return adapter.Get();
    this.CheckFeedAccountIsMappedToCashAccount();
    foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()))
    {
      CABankFeedDetail det = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
      if (det.CashAccountID.HasValue)
        this.CheckCashAccountAlreadyLinked(det);
    }
    current.Status = "A";
    ((PXSelectBase<CABankFeed>) this.BankFeed).Update(current);
    ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Connect", Visible = true)]
  [PXProcessButton(Category = "Processing")]
  public virtual IEnumerable ConnectFeed(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaint.\u003C\u003Ec__DisplayClass29_0 cDisplayClass290 = new CABankFeedMaint.\u003C\u003Ec__DisplayClass29_0();
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current == null)
      return adapter.Get();
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass290.manager = this.GetSpecificManager(current);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass290.copy = this.CopyBankFeedRecord(current);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass290, __methodptr(\u003CConnectFeed\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Update Credentials", Visible = true)]
  [PXButton(CommitChanges = true, Category = "Processing", PopupCommand = "syncUpdateFeed")]
  public virtual IEnumerable UpdateFeed(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaint.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new CABankFeedMaint.\u003C\u003Ec__DisplayClass31_0();
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current == null)
      return adapter.Get();
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310.copy = this.CopyBankFeedRecord(current);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310.manager = this.GetSpecificManager(cDisplayClass310.copy);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass310, __methodptr(\u003CUpdateFeed\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXButton]
  public virtual IEnumerable SyncUpdateFeed(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaint.\u003C\u003Ec__DisplayClass33_0 cDisplayClass330 = new CABankFeedMaint.\u003C\u003Ec__DisplayClass33_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass330.formProcRes = adapter.CommandArguments;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    // ISSUE: reference to a compiler-generated field
    if (string.IsNullOrEmpty(cDisplayClass330.formProcRes) || current == null)
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass330.copy = this.CopyBankFeedRecord(current);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass330.manager = this.GetSpecificManager(current);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass330, __methodptr(\u003CSyncUpdateFeed\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXButton]
  public virtual IEnumerable SyncConnectFeed(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaint.\u003C\u003Ec__DisplayClass35_0 cDisplayClass350 = new CABankFeedMaint.\u003C\u003Ec__DisplayClass35_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass350.formProcRes = adapter.CommandArguments;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    // ISSUE: reference to a compiler-generated field
    if (string.IsNullOrEmpty(cDisplayClass350.formProcRes) || current == null)
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass350.copy = this.CopyBankFeedRecord(current);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass350.manager = this.GetSpecificManager(current);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass350, __methodptr(\u003CSyncConnectFeed\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Suspend", Visible = true)]
  [PXButton(CommitChanges = true, Category = "Processing")]
  public virtual IEnumerable SuspendFeed(PXAdapter adapter)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    PXCache cache = ((PXSelectBase) this.BankFeedDetail).Cache;
    if (current == null)
      return adapter.Get();
    current.Status = "S";
    ((PXSelectBase<CABankFeed>) this.BankFeed).Update(current);
    foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()))
    {
      CABankFeedDetail caBankFeedDetail = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
      if (this.AllowMultipleMappingForDetail(current, caBankFeedDetail))
      {
        this.ClearManualImportDate(caBankFeedDetail);
        ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Update(caBankFeedDetail);
        cache.SetValue<CABankFeedDetail.importStartDate>((object) caBankFeedDetail, (object) null);
      }
    }
    ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Disconnect", Visible = true)]
  [PXButton(CommitChanges = true, Category = "Processing")]
  public virtual IEnumerable DisconnectFeed(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaint.\u003C\u003Ec__DisplayClass39_0 cDisplayClass390 = new CABankFeedMaint.\u003C\u003Ec__DisplayClass39_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass390.bankFeed = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass390.bankFeed == null)
      return adapter.Get();
    // ISSUE: reference to a compiler-generated method
    if (((PXSelectBase<CABankFeed>) this.BankFeed).Ask(PXMessages.LocalizeFormatNoPrefix("The bank connection and all linked accounts will be deleted from the {0} bank feed. Proceed?", new object[1]
    {
      (object) ((IEnumerable<(string, string)>) CABankFeedType.ListAttribute.GetTypes).Where<(string, string)>(new Func<(string, string), bool>(cDisplayClass390.\u003CDisconnectFeed\u003Eb__0)).Select<(string, string), string>((Func<(string, string), string>) (ii => ii.Item2)).FirstOrDefault<string>()
    }), (MessageButtons) 4) == 6)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CABankFeedMaint.\u003C\u003Ec__DisplayClass39_1 cDisplayClass391 = new CABankFeedMaint.\u003C\u003Ec__DisplayClass39_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass390.bankFeed.CreateExpenseReceipt = new bool?(false);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass390.bankFeed.CreateReceiptForPendingTran = new bool?(false);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass390.bankFeed = ((PXSelectBase<CABankFeed>) this.BankFeed).Update(cDisplayClass390.bankFeed);
      ((PXAction) this.Save).Press();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass391.copy = this.CopyBankFeedRecord(cDisplayClass390.bankFeed);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass391.manager = this.GetSpecificManager(cDisplayClass390.bankFeed);
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass391, __methodptr(\u003CDisconnectFeed\u003Eb__2)));
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Categories")]
  [PXButton]
  public virtual IEnumerable ShowCategories(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaint.\u003C\u003Ec__DisplayClass41_0 cDisplayClass410 = new CABankFeedMaint.\u003C\u003Ec__DisplayClass41_0();
    IEnumerable enumerable = adapter.Get();
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current == null)
      return enumerable;
    if (((PXSelectBase) this.BankFeedCategories).Cache.Cached.Count() > 0L)
    {
      ((PXSelectBase<BankFeedCategory>) this.BankFeedCategories).AskExt(true);
      return enumerable;
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass410.copy = this.CopyBankFeedRecord(current);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass410.manager = this.GetSpecificManager(current);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass410, __methodptr(\u003CShowCategories\u003Eb__0)));
    return enumerable;
  }

  [PXUIField(DisplayName = "Show Access ID", Visible = true)]
  [PXButton(Category = "Processing")]
  public virtual void ShowAccessId()
  {
    ((PXSelectBase<CABankFeed>) this.CurrentBankFeed).AskExt(true);
  }

  [PXUIField(DisplayName = "Load Transactions in Test Mode", Visible = true)]
  [PXButton(Category = "Processing")]
  public virtual IEnumerable CheckConnection(PXAdapter adapter)
  {
    // ISSUE: method pointer
    ((PXSelectBase<CABankFeedMaint.TransactionsFilter>) this.Filter).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CCheckConnection\u003Eb__45_0)), true);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Migrate", Visible = true)]
  [PXButton(Category = "Processing")]
  public IEnumerable MigrateFeed(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaint.\u003C\u003Ec__DisplayClass47_0 cDisplayClass470 = new CABankFeedMaint.\u003C\u003Ec__DisplayClass47_0();
    IEnumerable enumerable = adapter.Get();
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current == null)
      return enumerable;
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass470.copy = !string.IsNullOrEmpty(current.InstitutionID) ? this.CopyBankFeedRecord(current) : throw new PXException("'{0}' cannot be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName(((PXSelectBase) this.BankFeed).Cache, "InstitutionID")
    });
    // ISSUE: reference to a compiler-generated field
    cDisplayClass470.manager = this.GetSpecificManager(current);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass470, __methodptr(\u003CMigrateFeed\u003Eb__0)));
    return enumerable;
  }

  [PXUIField(DisplayName = "Load Transactions", Visible = true)]
  [PXButton]
  public virtual IEnumerable LoadTransactions(PXAdapter adapter)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current == null)
      return adapter.Get();
    this.ShowTransactionsFromService(current);
    return adapter.Get();
  }

  [PXUIField]
  [PXCancelCloseButton(CommitChanges = true, ClosePopup = true)]
  public virtual IEnumerable CancelClose(PXAdapter adapter)
  {
    ((PXAction) this.Cancel).Press();
    throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<CABankFeedImport>(), false, string.Empty);
  }

  [PXButton]
  [PXUIField(DisplayName = "Set Default Mapping", Visible = true)]
  public virtual void SetDefaultMapping()
  {
    if (((PXSelectBase<CABankFeedFieldMapping>) this.BankFeedFieldMapping).Any<CABankFeedFieldMapping>())
    {
      if (((PXSelectBase) this.BankFeedFieldMapping).View.Ask((object) null, "Replace Current Rules", "All the existing mapping rules will be removed and replaced with the default ones.", (MessageButtons) 4, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
      {
        [(WebDialogResult) 6] = "Replace",
        [(WebDialogResult) 7] = "Cancel"
      }, (MessageIcon) 0) != 6)
        return;
      ((PXSelectBase) this.BankFeedFieldMapping).Cache.DeleteAll((IEnumerable<object>) ((PXSelectBase<CABankFeedFieldMapping>) this.BankFeedFieldMapping).Select(Array.Empty<object>()));
    }
    this.CreateDefaultMappingRules();
  }

  public CABankFeedMaint()
  {
    PXCache cache1 = ((PXSelectBase) this.BankFeedCategories).Cache;
    cache1.AllowInsert = false;
    cache1.AllowUpdate = false;
    cache1.AllowDelete = false;
    PXCache cache2 = ((PXSelectBase) this.BankFeedTransactions).Cache;
    cache2.AllowInsert = false;
    cache2.AllowUpdate = false;
  }

  public virtual void StoreBankFeedUser(CABankFeed bankFeed, string externalUser)
  {
    CABankFeed caBankFeed = ((PXSelectBase<CABankFeed>) this.BankFeed).Locate(bankFeed);
    caBankFeed.ExternalUserID = externalUser;
    ((PXSelectBase<CABankFeed>) this.BankFeed).Update(caBankFeed);
    ((PXAction) this.Save).Press();
  }

  public virtual void CreateUserRecord(string externalUser, int organizationId)
  {
    PXCache cach = ((PXGraph) this).Caches[typeof (CABankFeedUser)];
    cach.Insert((object) new CABankFeedUser()
    {
      ExternalUserID = externalUser,
      OrganizationID = new int?(organizationId)
    });
    cach.Persist((PXDBOperation) 2);
  }

  public virtual void DisconnectBankFeed() => this.DisconnectBankFeed(false);

  public virtual void DeleteAccounts()
  {
    foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()))
      ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Delete(PXResult<CABankFeedDetail>.op_Implicit(pxResult));
    foreach (PXResult<CABankFeedCorpCard> pxResult in ((PXSelectBase<CABankFeedCorpCard>) this.BankFeedCorpCC).Select(Array.Empty<object>()))
      ((PXSelectBase<CABankFeedCorpCard>) this.BankFeedCorpCC).Delete(PXResult<CABankFeedCorpCard>.op_Implicit(pxResult));
  }

  public virtual IEnumerable<Tuple<CABankFeedDetail, CABankFeed>> GetBankFeedDetailsByInstitution(
    string instId)
  {
    object[] objArray = new object[1]{ (object) instId };
    foreach (PXResult<CABankFeedDetail, CABankFeed> pxResult in PXSelectBase<CABankFeedDetail, PXSelectReadonly2<CABankFeedDetail, InnerJoin<CABankFeed, On<CABankFeed.bankFeedID, Equal<CABankFeedDetail.bankFeedID>>>, Where<CABankFeed.institutionID, Equal<Required<CABankFeed.institutionID>>>>.Config>.Select((PXGraph) this, objArray))
      yield return new Tuple<CABankFeedDetail, CABankFeed>(PXResult<CABankFeedDetail, CABankFeed>.op_Implicit(pxResult), PXResult<CABankFeedDetail, CABankFeed>.op_Implicit(pxResult));
  }

  public virtual IEnumerable<CABankFeedAccountMapping> GetStoredMappingForInstitution(CABankFeed row)
  {
    return GraphHelper.RowCast<CABankFeedAccountMapping>((IEnumerable) PXSelectBase<CABankFeedAccountMapping, PXSelect<CABankFeedAccountMapping, Where<CABankFeedAccountMapping.institutionID, Equal<Required<CABankFeedAccountMapping.institutionID>>, And<CABankFeedAccountMapping.type, Equal<Required<CABankFeedAccountMapping.type>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.InstitutionID,
      (object) row.Type
    }));
  }

  internal void CheckFeedAccountAlreadyExists(
    IEnumerable<Tuple<CABankFeedDetail, CABankFeed>> data,
    string accountName,
    string accountMask)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    CABankFeedDetail caBankFeedDetail1 = (CABankFeedDetail) null;
    CABankFeed caBankFeed1 = (CABankFeed) null;
    foreach (Tuple<CABankFeedDetail, CABankFeed> tuple in data)
    {
      CABankFeedDetail caBankFeedDetail2 = tuple.Item1;
      CABankFeed caBankFeed2 = tuple.Item2;
      string accountName1 = caBankFeedDetail2.AccountName;
      string accountMask1 = caBankFeedDetail2.AccountMask;
      string lowerInvariant1 = accountName1?.Trim().ToLowerInvariant();
      string lowerInvariant2 = accountMask1?.Trim().ToLowerInvariant();
      if (current.BankFeedID != caBankFeed2.BankFeedID && lowerInvariant1 == accountName && lowerInvariant2 == accountMask)
      {
        caBankFeedDetail1 = caBankFeedDetail2;
        caBankFeed1 = caBankFeed2;
        break;
      }
    }
    if (caBankFeedDetail1 == null)
      return;
    if (string.IsNullOrEmpty(accountMask))
      throw new PXException("The {0} account is already linked to the {1} bank feed.", new object[2]
      {
        (object) caBankFeedDetail1.AccountName,
        (object) caBankFeed1.BankFeedID
      });
    throw new PXException("The {0} account with the {1} account mask is already linked to the {2} bank feed.", new object[3]
    {
      (object) caBankFeedDetail1.AccountName,
      (object) caBankFeedDetail1.AccountMask,
      (object) caBankFeed1.BankFeedID
    });
  }

  internal virtual void StoreBankFeed(BankFeedFormResponse formResponse)
  {
    this.CheckNewItem(formResponse);
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    current.AccessToken = formResponse.AccessToken;
    current.ExternalItemID = formResponse.ItemID;
    current.Institution = formResponse.InstitutionName;
    current.InstitutionID = formResponse.InstitutionID;
    current.Status = "R";
    ((PXSelectBase<CABankFeed>) this.BankFeed).Update(current);
    this.AddAccounts(formResponse);
    ((PXAction) this.Save).Press();
  }

  internal virtual void MigrateBankFeed(BankFeedFormResponse formResponse)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current == null)
      return;
    if (string.IsNullOrEmpty(current.InstitutionID))
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName(((PXSelectBase) this.BankFeed).Cache, "InstitutionID")
      });
    string institutionId = formResponse.InstitutionID;
    if (institutionId != current.InstitutionID)
      throw new PXException("Institution ID from the {0} bank feed service is not equal to the {1} ID.", new object[2]
      {
        (object) institutionId,
        (object) current.InstitutionID
      });
    CABankFeed copy1 = (CABankFeed) ((PXSelectBase) this.BankFeed).Cache.CreateCopy((object) current);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      Dictionary<int, (string, string)> source1 = new Dictionary<int, (string, string)>();
      List<CABankFeedDetail> detailsFromCust = GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>())).Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.CashAccountID.HasValue && i.AccountName != null)).ToList<CABankFeedDetail>();
      List<CABankFeedCorpCard> list = GraphHelper.RowCast<CABankFeedCorpCard>((IEnumerable) ((PXSelectBase<CABankFeedCorpCard>) this.BankFeedCorpCC).Select(Array.Empty<object>())).Where<CABankFeedCorpCard>((Func<CABankFeedCorpCard, bool>) (i => detailsFromCust.Any<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (ii => ii.AccountID == i.AccountID)))).ToList<CABankFeedCorpCard>();
      this.DisconnectBankFeed(true);
      this.StoreBankFeed(formResponse);
      foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()))
      {
        CABankFeedDetail det = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
        CABankFeedDetail caBankFeedDetail = detailsFromCust.Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.AccountName == det.AccountName && i.AccountMask == det.AccountMask)).FirstOrDefault<CABankFeedDetail>();
        if (caBankFeedDetail != null && !source1.ContainsKey(caBankFeedDetail.CashAccountID.Value))
        {
          CABankFeedDetail copy2 = (CABankFeedDetail) ((PXSelectBase) this.BankFeedDetail).Cache.CreateCopy((object) det);
          int key = caBankFeedDetail.CashAccountID.Value;
          string descr = caBankFeedDetail.Descr;
          int? statementStartDay = caBankFeedDetail.StatementStartDay;
          source1.Add(key, (copy2.AccountID, caBankFeedDetail.AccountID));
          copy2.CashAccountID = new int?(key);
          copy2.Descr = descr;
          copy2.StatementStartDay = statementStartDay;
          ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Update(copy2);
          this.CheckCashAccountAlreadyLinked(copy2);
        }
      }
      if (source1.Count<KeyValuePair<int, (string, string)>>() > 0)
      {
        ((PXAction) this.Save).Press();
        IEnumerable<CABankFeedDetail> source2 = GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()));
        foreach (KeyValuePair<int, (string, string)> keyValuePair in source1)
        {
          KeyValuePair<int, (string, string)> kvp = keyValuePair;
          foreach (CABankFeedCorpCard bankFeedCorpCard1 in list.Where<CABankFeedCorpCard>((Func<CABankFeedCorpCard, bool>) (i => i.AccountID == kvp.Value.Item2)))
          {
            CABankFeedCorpCard bankFeedCorpCard2 = ((PXSelectBase<CABankFeedCorpCard>) this.BankFeedCorpCC).Insert();
            CABankFeedDetail caBankFeedDetail = source2.Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.AccountID == kvp.Value.Item1)).FirstOrDefault<CABankFeedDetail>();
            bankFeedCorpCard2.AccountID = caBankFeedDetail.AccountID;
            CABankFeedCorpCard bankFeedCorpCard3 = ((PXSelectBase<CABankFeedCorpCard>) this.BankFeedCorpCC).Update(bankFeedCorpCard2);
            bankFeedCorpCard3.MatchField = bankFeedCorpCard1.MatchField;
            bankFeedCorpCard3.MatchRule = bankFeedCorpCard1.MatchRule;
            bankFeedCorpCard3.MatchValue = bankFeedCorpCard1.MatchValue;
            bankFeedCorpCard3.CorpCardID = bankFeedCorpCard1.CorpCardID;
            bankFeedCorpCard3.EmployeeID = bankFeedCorpCard1.EmployeeID;
            ((PXSelectBase<CABankFeedCorpCard>) this.BankFeedCorpCC).Update(bankFeedCorpCard3);
          }
        }
        ((PXAction) this.Save).Press();
      }
      transactionScope.Complete();
    }
    CustomizationFeedHelper.DisconnectFeed(copy1);
  }

  internal virtual void UpdateAccounts(IEnumerable<BankFeedAccount> accounts)
  {
    bool flag = false;
    IEnumerable<CABankFeedDetail> source = GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()));
    foreach (BankFeedAccount account1 in accounts)
    {
      BankFeedAccount account = account1;
      if (!source.Any<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (x => x.AccountID == account.AccountID)))
      {
        CABankFeedDetail caBankFeedDetail1 = source.FirstOrDefault<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.AccountName == account.Name && i.AccountMask == account.Mask));
        if (caBankFeedDetail1 != null)
        {
          caBankFeedDetail1.AccountID = account.AccountID;
          ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Update(caBankFeedDetail1);
          flag = true;
        }
        else
        {
          CABankFeedDetail caBankFeedDetail2 = ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Insert();
          caBankFeedDetail2.AccountID = account.AccountID;
          caBankFeedDetail2.AccountName = account.Name;
          caBankFeedDetail2.AccountMask = account.Mask;
          caBankFeedDetail2.AccountType = account.Type;
          caBankFeedDetail2.AccountSubType = account.Subtype;
          caBankFeedDetail2.Currency = account.Currency;
          ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Update(caBankFeedDetail2);
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    ((PXAction) this.Save).Press();
  }

  internal virtual void AddAccounts(BankFeedFormResponse bankFeedResponse)
  {
    IEnumerable<BankFeedAccount> accounts = bankFeedResponse.Accounts;
    if (accounts.Count<BankFeedAccount>() <= 0)
      return;
    List<CABankFeedDetail> source = new List<CABankFeedDetail>();
    foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()))
    {
      CABankFeedDetail caBankFeedDetail = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
      source.Add(caBankFeedDetail);
    }
    foreach (BankFeedAccount bankFeedAccount in accounts)
    {
      BankFeedAccount account = bankFeedAccount;
      CABankFeedDetail caBankFeedDetail = source.FirstOrDefault<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (x => x.AccountID == account.AccountID)) ?? ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Insert();
      caBankFeedDetail.AccountID = account.AccountID;
      caBankFeedDetail.AccountName = account.Name;
      caBankFeedDetail.AccountMask = account.Mask;
      caBankFeedDetail.AccountType = account.Type;
      caBankFeedDetail.AccountSubType = account.Subtype;
      caBankFeedDetail.Currency = account.Currency;
      ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Update(caBankFeedDetail);
    }
  }

  internal virtual CABankFeed GetStoredBankFeedByIds(string externalUserId, string externalItemId)
  {
    return PXResultset<CABankFeed>.op_Implicit(PXSelectBase<CABankFeed, PXSelect<CABankFeed, Where<CABankFeed.externalUserID, Equal<Required<CABankFeed.externalUserID>>, And<CABankFeed.externalItemID, Equal<Required<CABankFeed.externalItemID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) externalUserId,
      (object) externalItemId
    }));
  }

  internal virtual void CheckNewItem(BankFeedFormResponse formResponse)
  {
    IEnumerable<Tuple<CABankFeedDetail, CABankFeed>> detailsByInstitution = this.GetBankFeedDetailsByInstitution(formResponse.InstitutionID);
    foreach (BankFeedAccount account in formResponse.Accounts)
    {
      string lowerInvariant1 = account.Name?.Trim().ToLowerInvariant();
      string lowerInvariant2 = account.Mask?.Trim().ToLowerInvariant();
      this.CheckFeedAccountAlreadyExists(detailsByInstitution, lowerInvariant1, lowerInvariant2);
    }
  }

  internal void AddOrUpdateFeedAccountCashAccountMapping(
    CABankFeedDetail det,
    IEnumerable<CABankFeedAccountMapping> mapping)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    string name = det.AccountName;
    string mask = det.AccountMask;
    int? cashAccountId = det.CashAccountID;
    CABankFeedAccountMapping feedAccountMapping1 = mapping.Where<CABankFeedAccountMapping>((Func<CABankFeedAccountMapping, bool>) (i =>
    {
      if (i.AccountName == name && i.AccountMask == mask)
      {
        int? cashAccountId1 = i.CashAccountID;
        int? nullable = cashAccountId;
        if (cashAccountId1.GetValueOrDefault() == nullable.GetValueOrDefault() & cashAccountId1.HasValue == nullable.HasValue)
          return i.BankFeedID == null;
      }
      return false;
    })).FirstOrDefault<CABankFeedAccountMapping>();
    if (feedAccountMapping1 != null)
    {
      feedAccountMapping1.BankFeedID = det.BankFeedID;
      feedAccountMapping1.LineNbr = det.LineNbr;
      ((PXSelectBase<CABankFeedAccountMapping>) this.BankFeedAccountMapping).Update(feedAccountMapping1);
    }
    else
    {
      foreach (CABankFeedAccountMapping feedAccountMapping2 in ((PXGraph) this).Caches[typeof (CABankFeedAccountMapping)].Cached)
      {
        if (feedAccountMapping2.BankFeedID == null && !feedAccountMapping2.LineNbr.HasValue && ((PXSelectBase) this.BankFeedAccountMapping).Cache.GetStatus((object) feedAccountMapping2) == 2)
          ((PXSelectBase<CABankFeedAccountMapping>) this.BankFeedAccountMapping).Delete(feedAccountMapping2);
      }
      ((PXSelectBase<CABankFeedAccountMapping>) this.BankFeedAccountMapping).Insert(new CABankFeedAccountMapping()
      {
        BankFeedID = det.BankFeedID,
        LineNbr = det.LineNbr,
        AccountMask = det.AccountMask,
        AccountName = det.AccountName,
        CashAccountID = det.CashAccountID,
        Type = current.Type,
        InstitutionID = current.InstitutionID
      });
    }
  }

  internal void UnmapBankFeedDetail(CABankFeedDetail det)
  {
    CABankFeedAccountMapping mappingForDetail = this.GetMappingForDetail(det);
    if (mappingForDetail == null)
      return;
    mappingForDetail.BankFeedID = (string) null;
    mappingForDetail.LineNbr = new int?();
    ((PXSelectBase<CABankFeedAccountMapping>) this.BankFeedAccountMapping).Update(mappingForDetail);
  }

  internal void UnmapBankFeedDetails()
  {
    if (((PXSelectBase<CABankFeed>) this.BankFeed).Current == null)
      return;
    foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()))
    {
      CABankFeedDetail det = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
      if (det.CashAccountID.HasValue)
      {
        ((PXSelectBase) this.BankFeedDetail).Cache.SetValue<CABankFeedDetail.importStartDate>((object) det, (object) null);
        this.UnmapBankFeedDetail(det);
      }
    }
  }

  internal void ClearLoginFailedStatus()
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current == null || !(current.RetrievalStatus == "L"))
      return;
    DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, LocaleInfo.GetTimeZone());
    current.RetrievalStatus = "S";
    current.ErrorMessage = (string) null;
    current.RetrievalDate = new DateTime?(dateTime);
    ((PXSelectBase<CABankFeed>) this.BankFeed).Update(current);
    foreach (CABankFeedDetail caBankFeedDetail in GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>())).Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.RetrievalStatus == "L")))
    {
      caBankFeedDetail.RetrievalStatus = "S";
      caBankFeedDetail.ErrorMessage = (string) null;
      current.RetrievalDate = new DateTime?(dateTime);
      ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Update(caBankFeedDetail);
    }
    ((PXAction) this.Save).Press();
  }

  protected virtual void _(Events.RowSelected<CABankFeedCorpCard> e)
  {
    CABankFeedCorpCard corpCard = e.Row;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankFeedCorpCard>>) e).Cache;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (corpCard == null)
      return;
    bool flag = corpCard.MatchField != "N";
    PXUIFieldAttribute.SetEnabled<CABankFeedCorpCard.matchValue>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankFeedCorpCard>>) e).Cache, (object) corpCard, flag);
    PXUIFieldAttribute.SetEnabled<CABankFeedCorpCard.matchRule>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankFeedCorpCard>>) e).Cache, (object) corpCard, flag);
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    this.SetCorpCardFilters(cache, corpCard);
    if (corpCard.AccountID != null && corpCard.CorpCardID.HasValue)
    {
      CABankFeedDetail caBankFeedDetail = GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>())).Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.AccountID == corpCard.AccountID)).FirstOrDefault<CABankFeedDetail>();
      if (caBankFeedDetail != null)
      {
        string accountMask = caBankFeedDetail.AccountMask;
        if (!CABankMatchingProcess.IsCardNumberMatch(accountMask, corpCard.CardNumber))
          propertyException = new PXSetPropertyException("The number of the selected corporate card ({0}) differs from the {2} account mask of the {1} account to which the card is matched.", (PXErrorLevel) 2, new object[3]
          {
            (object) corpCard.CardNumber,
            (object) caBankFeedDetail.AccountName,
            (object) accountMask
          });
      }
    }
    cache.RaiseExceptionHandling<CABankFeedCorpCard.cardNumber>((object) corpCard, (object) corpCard.CardNumber, (Exception) propertyException);
  }

  protected virtual void _(Events.RowSelected<CABankFeed> e)
  {
    CABankFeed row = e.Row;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankFeed>>) e).Cache;
    PXCache pxCache = (PXCache) GraphHelper.Caches<CABankFeedDetail>((PXGraph) this);
    if (row == null)
      return;
    bool flag1 = this.IsServiceFeed(row);
    bool flag2 = row.Status == "D";
    bool flag3 = row.Status == "A";
    bool flag4 = row.Status == "S";
    bool flag5 = row.Status == "R";
    bool flag6 = row.Status == "M";
    bool flag7 = (flag4 | flag3 | flag5) & flag1;
    cache.AllowDelete = flag2 | flag6 || !flag1;
    ((PXSelectBase) this.BankFeedFieldMapping).AllowSelect = flag1;
    this.SetEnabledRequired(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankFeed>>) e).Cache, row);
    ((PXAction) this.connectFeed).SetEnabled(flag2 & flag1);
    ((PXAction) this.connectFeed).SetVisible(flag1);
    ((PXAction) this.disconnectFeed).SetEnabled(flag7 & flag1);
    ((PXAction) this.disconnectFeed).SetVisible(flag1);
    ((PXAction) this.showCategories).SetEnabled(flag7);
    ((PXAction) this.updateFeed).SetEnabled(((flag4 || flag2 ? 0 : (!flag6 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0);
    ((PXAction) this.updateFeed).SetVisible(flag1);
    ((PXAction) this.connectFeed).SetDisplayOnMainToolbar(flag2);
    ((PXAction) this.updateFeed).SetDisplayOnMainToolbar(!flag4 && !flag2 && !flag6);
    ((PXAction) this.activateFeed).SetDisplayOnMainToolbar(flag4);
    ((PXAction) this.suspendFeed).SetEnabled(flag3);
    ((PXAction) this.checkConnection).SetEnabled(!flag2 && !flag6);
    ((PXAction) this.showAccessId).SetEnabled(((flag2 ? 0 : (!flag6 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0);
    ((PXAction) this.showAccessId).SetVisible(flag1);
    ((PXAction) this.migrateFeed).SetEnabled(flag6);
    ((PXAction) this.migrateFeed).SetVisible(flag6);
    ((PXAction) this.migrateFeed).SetDisplayOnMainToolbar(flag6);
    ((PXAction) this.activateFeed).SetEnabled(flag4 || flag5 && !flag1 || flag5 && GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>())).Any<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.CashAccountID.HasValue)));
    PXUIFieldAttribute.SetEnabled<CABankFeed.defaultExpenseItemID>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<CABankFeed.type>(cache, (object) row, flag2 || flag5 && !flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.createExpenseReceipt>(cache, (object) row, !flag2);
    PXUIFieldAttribute.SetVisible<CABankFeed.createReceiptForPendingTran>(cache, (object) row, !flag2 & flag1);
    PXUIFieldAttribute.SetVisible<CABankFeed.multipleMapping>(cache, (object) row, !flag2);
    PXUIFieldAttribute.SetVisible<CABankFeed.externalUserID>(cache, (object) row, !string.IsNullOrEmpty(row.ExternalUserID));
    ((PXSelectBase) this.BankFeedDetail).AllowInsert = !flag1;
    ((PXSelectBase) this.BankFeedDetail).AllowDelete = !flag1;
    PXUIFieldAttribute.SetEnabled<CABankFeedDetail.accountName>(pxCache, (object) null, !flag1);
    PXUIFieldAttribute.SetEnabled<CABankFeedDetail.accountMask>(pxCache, (object) null, !flag1);
    PXUIFieldAttribute.SetEnabled<CABankFeedDetail.currency>(pxCache, (object) null, !flag1);
    PXUIFieldAttribute.SetEnabled<CABankFeedDetail.accountMask>(pxCache, (object) null, !flag1);
    PXUIFieldAttribute.SetEnabled<CABankFeedDetail.accountType>(pxCache, (object) null, !flag1);
    PXUIFieldAttribute.SetEnabled<CABankFeedDetail.accountSubType>(pxCache, (object) null, !flag1);
    PXUIFieldAttribute.SetVisible<CABankFeedDetail.accountMask>(pxCache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CABankFeedDetail.accountType>(pxCache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CABankFeedDetail.accountSubType>(pxCache, (object) null, flag1);
    if (!flag1)
      return;
    bool flag8 = !this.GetSpecificManager(row).DefaultImportStartDate.HasValue || !flag7;
    PXUIFieldAttribute.SetEnabled<CABankFeed.importStartDate>(cache, (object) row, flag8);
  }

  protected virtual void _(Events.RowSelected<CABankFeedDetail> e)
  {
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankFeedDetail>>) e).Cache;
    CABankFeedDetail row = e.Row;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current == null || row == null)
      return;
    bool flag1 = current.Status == "S";
    bool flag2 = this.AllowMultipleMappingForDetail(current, row);
    this.SetCashAccountWarningIfNeeded(cache, row);
    PXUIFieldAttribute.SetEnabled<CABankFeedDetail.hidden>(cache, (object) row, !row.CashAccountID.HasValue);
    PXUIFieldAttribute.SetEnabled<CABankFeedDetail.importStartDate>(cache, (object) row, flag2 && !flag1);
  }

  protected virtual void _(
    Events.RowSelected<CABankFeedMaint.TransactionsFilter> e)
  {
    PXCache cache = ((PXSelectBase) this.BankFeedTransactions).Cache;
    CABankFeedMaint.TransactionsFilter row = e.Row;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (row == null || current == null || current.BankFeedID == null)
      return;
    string[] transactionFields = this.GetAvailableTransactionFields(current);
    if (transactionFields != null)
    {
      foreach (string str in transactionFields)
        PXUIFieldAttribute.SetVisible(cache, (object) null, str, true);
    }
    int? maxTransactions = row.MaxTransactions;
    int num = 1000;
    bool flag = maxTransactions.GetValueOrDefault() > num & maxTransactions.HasValue;
    ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<CABankFeedMaint.TransactionsFilter.maxTransactions>((object) row, (object) row.MaxTransactions, flag ? (Exception) new PXSetPropertyException("Loading a large number of transactions might slow down the system.", (PXErrorLevel) 2) : (Exception) null);
  }

  protected virtual void _(Events.RowPersisting<CABankFeed> e)
  {
    CABankFeed row = e.Row;
    if (row.CreateExpenseReceipt.GetValueOrDefault() && !row.DefaultExpenseItemID.HasValue)
      throw new PXRowPersistingException(PXDataUtils.FieldName<CABankFeed.defaultExpenseItemID>(), (object) e.Row.DefaultExpenseItemID, "Default expense item is required to create expense receipts.");
    bool? valueOriginal = ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CABankFeed>>) e).Cache.GetValueOriginal<CABankFeed.multipleMapping>((object) row) as bool?;
    int num;
    if (e.Operation != 3 && row.Status == "A")
    {
      bool? nullable = valueOriginal;
      bool? multipleMapping = row.MultipleMapping;
      num = !(nullable.GetValueOrDefault() == multipleMapping.GetValueOrDefault() & nullable.HasValue == multipleMapping.HasValue) ? 1 : 0;
    }
    else
      num = 0;
    bool flag = num != 0;
    if (!(row.Status == "A"))
      return;
    foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()))
    {
      CABankFeedDetail det = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
      int? cashAccountId = det.CashAccountID;
      if (cashAccountId.HasValue)
      {
        if (flag)
          this.CheckCashAccountAlreadyLinked(det);
        if (CAMatchProcess.PK.Find((PXGraph) this, cashAccountId) != null)
          throw new PXRowPersistingException(PXDataUtils.FieldName<CABankFeedDetail.cashAccountID>(), (object) cashAccountId, "Transactions are being retrieved for the {0} bank feed. Your changes will be lost.", new object[1]
          {
            (object) row.BankFeedID
          });
      }
    }
  }

  protected virtual void _(Events.RowPersisting<CABankFeedDetail> e)
  {
    if (e.Operation == 3)
      return;
    CABankFeedDetail row = e.Row;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current.Status == "A")
      this.CheckCashAccountAlreadyLinked(row);
    DateTime? valueOriginal = ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CABankFeedDetail>>) e).Cache.GetValueOriginal<CABankFeedDetail.importStartDate>((object) current) as DateTime?;
    DateTime? importStartDate = row.ImportStartDate;
    if ((valueOriginal.HasValue == importStartDate.HasValue ? (valueOriginal.HasValue ? (valueOriginal.GetValueOrDefault() != importStartDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 || !current.MultipleMapping.GetValueOrDefault())
      return;
    this.CheckManualImportStartDate(row);
  }

  protected virtual void _(Events.RowPersisting<CABankFeedExpense> e)
  {
    CABankFeedExpense row = e.Row;
    PXCache cache = ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CABankFeedExpense>>) e).Cache;
    if (e.Operation == 3)
      return;
    PXDefaultAttribute.SetPersistingCheck<CABankFeedExpense.inventoryItemID>(cache, (object) row, row.DoNotCreate.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 0);
  }

  protected virtual void _(Events.RowPersisting<CABankFeedCorpCard> e)
  {
    CABankFeedCorpCard row = e.Row;
    PXCache cache = ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CABankFeedCorpCard>>) e).Cache;
    if (e.Operation == 3)
      return;
    bool flag = row.MatchField != "N";
    PXDefaultAttribute.SetPersistingCheck<CABankFeedCorpCard.matchValue>(cache, (object) row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (row.MatchRule == "N" & flag)
    {
      string displayName = PXUIFieldAttribute.GetDisplayName(cache, "MatchRule");
      throw new PXRowPersistingException("MatchRule", (object) row.MatchValue, "'{0}' cannot be empty.", new object[1]
      {
        (object) displayName
      });
    }
  }

  public virtual void Persist()
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current != null && (current.Status == "A" || current.Status == "R"))
    {
      foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()))
      {
        CABankFeedDetail caBankFeedDetail = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
        if (caBankFeedDetail.CashAccountID.HasValue && ((PXSelectBase) this.BankFeedDetail).Cache.GetStatus((object) caBankFeedDetail) != 3)
          this.UpdateCashAccountEntry(this.GetCashAccount(caBankFeedDetail.CashAccountID));
      }
    }
    ((PXGraph) this).Persist();
  }

  protected virtual void _(Events.RowUpdated<CABankFeedCorpCard> e)
  {
    CABankFeedCorpCard row = e.Row;
    CABankFeedCorpCard oldRow = e.OldRow;
    if (row == null || oldRow == null || !(row.AccountID != oldRow.AccountID))
      return;
    ((PXSelectBase) this.BankFeedCorpCC).View.RequestRefresh();
  }

  protected virtual void _(Events.RowDeleting<CABankFeed> e)
  {
    CABankFeed bankFeed = e.Row;
    if (bankFeed == null)
      return;
    bool flag = this.IsServiceFeed(bankFeed);
    if (((!(bankFeed.Status != "D") ? 0 : (bankFeed.Status != "M" ? 1 : 0)) & (flag ? 1 : 0)) != 0)
    {
      string str = ((IEnumerable<(string, string)>) CABankFeedStatus.ListAttribute.GetStatuses).Where<(string, string)>((Func<(string, string), bool>) (ii => ii.Item1 == bankFeed.Status)).Select<(string, string), string>((Func<(string, string), string>) (ii => ii.Item2)).FirstOrDefault<string>();
      throw new PXException("The {0} bank feed has the {1} status.", new object[2]
      {
        (object) bankFeed.BankFeedID,
        (object) str
      });
    }
  }

  protected virtual void _(
    Events.FieldSelecting<CABankFeedDetail, CABankFeedDetail.importStartDate> e)
  {
    if (e?.Row == null)
      return;
    CABankFeedDetail row = e.Row;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (!row.CashAccountID.HasValue || current == null || row.ImportStartDate.HasValue)
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<CABankFeedDetail, CABankFeedDetail.importStartDate>>) e).ReturnValue = (object) CABankFeedMaint.GetImportStartDate((PXGraph) this, current, row);
  }

  protected virtual void _(
    Events.FieldSelecting<CABankFeedCorpCard, CABankFeedCorpCard.accountID> e)
  {
    if (e?.Row == null)
      return;
    CABankFeedCorpCard corpCard = e.Row;
    if (corpCard?.AccountID == null)
      return;
    CABankFeedDetail caBankFeedDetail = GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>())).FirstOrDefault<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.AccountID == corpCard.AccountID));
    if (caBankFeedDetail == null)
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<CABankFeedCorpCard, CABankFeedCorpCard.accountID>>) e).ReturnValue = (object) caBankFeedDetail.AccountName;
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.multipleMapping> e)
  {
    CABankFeed row = e.Row as CABankFeed;
    if ((e.NewValue as bool?).GetValueOrDefault())
    {
      foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()))
      {
        CABankFeedDetail det = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
        if (det.CashAccountID.HasValue)
          this.AddOrUpdateFeedAccountCashAccountMapping(det, this.GetStoredMappingForInstitution(row));
      }
    }
    else
      this.UnmapBankFeedDetails();
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.importStartDate> e)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if ((current != null ? (current.ImportStartDate.HasValue ? 1 : 0) : 0) == 0)
      return;
    foreach (PXResult<CABankFeedDetail> pxResult in ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>()))
    {
      CABankFeedDetail bankFeedDetail = PXResult<CABankFeedDetail>.op_Implicit(pxResult);
      if (bankFeedDetail.CashAccountID.HasValue)
        bankFeedDetail.ImportStartDate = new DateTime?(CABankFeedMaint.GetImportStartDate((PXGraph) this, ((PXSelectBase<CABankFeed>) this.BankFeed).Current, bankFeedDetail));
    }
  }

  protected virtual void _(
    Events.FieldSelecting<CABankFeedMaint.TransactionsFilter, CABankFeedMaint.TransactionsFilter.lineNbr> e)
  {
    if (e?.Row == null)
      return;
    CABankFeedMaint.TransactionsFilter tranFilter = e.Row;
    CABankFeedMaint.TransactionsFilter transactionsFilter = tranFilter;
    if ((transactionsFilter != null ? (transactionsFilter.LineNbr.HasValue ? 1 : 0) : 0) == 0)
      return;
    CABankFeedDetail caBankFeedDetail = GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>())).FirstOrDefault<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i =>
    {
      int? lineNbr1 = i.LineNbr;
      int? lineNbr2 = tranFilter.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    }));
    if (caBankFeedDetail == null)
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<CABankFeedMaint.TransactionsFilter, CABankFeedMaint.TransactionsFilter.lineNbr>>) e).ReturnValue = (object) caBankFeedDetail.AccountName;
  }

  protected virtual void _(Events.FieldUpdated<CABankFeed.type> e)
  {
    CABankFeed row = e.Row as CABankFeed;
    PXCache cache = ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CABankFeed.type>>) e).Cache;
    if (row == null)
      return;
    if (this.IsServiceFeed(row))
    {
      DateTime? defaultImportStartDate = this.GetSpecificManager(row).DefaultImportStartDate;
      if (defaultImportStartDate.HasValue)
        cache.SetValueExt<CABankFeed.importStartDate>((object) row, (object) defaultImportStartDate);
    }
    this.CorpCardFilters = ((string, string)[]) null;
  }

  protected virtual void _(
    Events.FieldVerifying<CABankFeed.importStartDate> e)
  {
    CABankFeed row = (CABankFeed) e.Row;
    DateTime? newValue = (DateTime?) ((Events.FieldVerifyingBase<Events.FieldVerifying<CABankFeed.importStartDate>, object, object>) e).NewValue;
    if (row == null || !newValue.HasValue || !this.IsServiceFeed(row))
      return;
    DateTime? minimumImportStartDate = this.GetSpecificManager(row).MinimumImportStartDate;
    if (!minimumImportStartDate.HasValue)
      return;
    DateTime? nullable1 = newValue;
    DateTime? nullable2 = minimumImportStartDate;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException<CABankFeed.importStartDate>("Plaid cannot provide more than 730 days (2 years) of historical bank transaction data. For best performance and reliability, it is recommended to request no more than 90 days of transaction history.", (PXErrorLevel) 4);
  }

  protected virtual void _(
    Events.FieldUpdated<CABankFeedCorpCard.matchField> e)
  {
    CABankFeedCorpCard row = e.Row as CABankFeedCorpCard;
    PXCache cache = ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CABankFeedCorpCard.matchField>>) e).Cache;
    if (row == null || !((string) e.NewValue == "N"))
      return;
    cache.SetValueExt<CABankFeedCorpCard.matchRule>((object) row, (object) "N");
    cache.SetValueExt<CABankFeedCorpCard.matchValue>((object) row, (object) null);
  }

  protected virtual void _(
    Events.FieldUpdated<CABankFeedCorpCard.accountID> e)
  {
    CABankFeedCorpCard corpCard = e.Row as CABankFeedCorpCard;
    PXCache cache = ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CABankFeedCorpCard.accountID>>) e).Cache;
    if (corpCard == null)
      return;
    int? nullable = new int?();
    CABankFeedDetail caBankFeedDetail = GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>())).Where<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => i.AccountID == corpCard.AccountID && i.BankFeedID == corpCard.BankFeedID)).FirstOrDefault<CABankFeedDetail>();
    if (caBankFeedDetail != null)
      nullable = caBankFeedDetail.CashAccountID;
    cache.SetValueExt<CABankFeedCorpCard.cashAccountID>((object) corpCard, (object) nullable);
  }

  protected virtual void _(
    Events.FieldUpdated<CABankFeedDetail.cashAccountID> e)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    CABankFeedDetail row = e.Row as CABankFeedDetail;
    PXCache cache = ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CABankFeedDetail.cashAccountID>>) e).Cache;
    int? newValue = (int?) e.NewValue;
    if (current.MultipleMapping.GetValueOrDefault())
    {
      this.ClearManualImportDate(row);
      this.UnmapBankFeedDetail(row);
      if (newValue.HasValue && current.InstitutionID != null)
        this.AddOrUpdateFeedAccountCashAccountMapping(row, this.GetStoredMappingForInstitution(current));
    }
    if (newValue.HasValue)
    {
      DateTime importStartDate = CABankFeedMaint.GetImportStartDate((PXGraph) this, ((PXSelectBase<CABankFeed>) this.BankFeed).Current, row);
      cache.SetValue<CABankFeedDetail.importStartDate>((object) row, (object) importStartDate);
      if (current.Status == "R" && this.IsServiceFeed(current))
        ((PXSelectBase) this.BankFeed).Cache.SetValue<CABankFeed.status>((object) current, (object) "A");
      cache.SetValue<CABankFeedDetail.hidden>((object) row, (object) false);
    }
    else
    {
      cache.SetValue<CABankFeedDetail.importStartDate>((object) row, (object) null);
      if (current.Status != "M" && GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>())).All<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => !i.CashAccountID.HasValue)))
        ((PXSelectBase) this.BankFeed).Cache.SetValue<CABankFeed.status>((object) current, (object) "R");
    }
    foreach (PXResult<CABankFeedCorpCard> pxResult in ((PXSelectBase<CABankFeedCorpCard>) this.BankFeedCorpCC).Select(Array.Empty<object>()))
    {
      CABankFeedCorpCard bankFeedCorpCard = PXResult<CABankFeedCorpCard>.op_Implicit(pxResult);
      if (bankFeedCorpCard.AccountID == row.AccountID)
      {
        if (!newValue.HasValue)
        {
          ((PXSelectBase<CABankFeedCorpCard>) this.BankFeedCorpCC).Delete(bankFeedCorpCard);
        }
        else
        {
          bankFeedCorpCard.CashAccountID = newValue;
          bankFeedCorpCard.CorpCardID = new int?();
          ((PXSelectBase<CABankFeedCorpCard>) this.BankFeedCorpCC).Update(bankFeedCorpCard);
        }
      }
    }
  }

  protected virtual void _(
    Events.FieldUpdated<CABankFeedDetail.importStartDate> e)
  {
    CABankFeedDetail row = e.Row as CABankFeedDetail;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    DateTime? newValue = e.NewValue as DateTime?;
    if (row == null || current == null)
      return;
    bool? multipleMapping = current.MultipleMapping;
    bool flag = false;
    if (multipleMapping.GetValueOrDefault() == flag & multipleMapping.HasValue)
      return;
    DateTime dateMultipleMapping = CABankFeedMaint.GetImportStratDateMultipleMapping((PXGraph) this, current, row);
    if (newValue.HasValue)
    {
      DateTime? nullable = newValue;
      DateTime dateTime = dateMultipleMapping;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() == dateTime ? 1 : 0) : 0) == 0)
      {
        this.CheckManualImportStartDate(row, dateMultipleMapping);
        this.SetManualImportDate(row, newValue);
        return;
      }
    }
    this.ClearManualImportDate(row);
  }

  protected virtual void _(
    Events.FieldUpdated<CABankFeedDetail.statementPeriod> e)
  {
    if (!(e.Row is CABankFeedDetail row))
      return;
    string newValue = (string) e.NewValue;
    if (!((string) ((Events.FieldUpdatedBase<Events.FieldUpdated<CABankFeedDetail.statementPeriod>, object, object>) e).OldValue != newValue))
      return;
    int num = newValue == "W" ? 2 : 1;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CABankFeedDetail.statementPeriod>>) e).Cache.SetValueExt<CABankFeedDetail.statementStartDay>((object) row, (object) num);
  }

  protected virtual void _(
    Events.FieldUpdated<CABankFeedCorpCard.corpCardID> e)
  {
    CABankFeedCorpCard row = e.Row as CABankFeedCorpCard;
    if (e.Row == null)
      return;
    using (IEnumerator<PXResult<EPEmployeeCorpCardLink>> enumerator = PXSelectBase<EPEmployeeCorpCardLink, PXSelect<EPEmployeeCorpCardLink, Where<EPEmployeeCorpCardLink.corpCardID, Equal<Required<EPEmployeeCorpCardLink.corpCardID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.CorpCardID
    }).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      EPEmployeeCorpCardLink employeeCorpCardLink = PXResult<EPEmployeeCorpCardLink>.op_Implicit(enumerator.Current);
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CABankFeedCorpCard.corpCardID>>) e).Cache.SetValueExt<CABankFeedCorpCard.employeeID>(e.Row, (object) employeeCorpCardLink.EmployeeID);
    }
  }

  protected virtual void _(
    Events.FieldVerifying<CABankFeedDetail.statementStartDay> e)
  {
    if ((CABankFeedDetail) e.Row != null && !((Events.FieldVerifyingBase<Events.FieldVerifying<CABankFeedDetail.statementStartDay>>) e).Cancel)
    {
      int? newValue = (int?) ((Events.FieldVerifyingBase<Events.FieldVerifying<CABankFeedDetail.statementStartDay>, object, object>) e).NewValue;
      if (newValue.HasValue)
      {
        int? nullable = newValue;
        int num1 = 0;
        if (!(nullable.GetValueOrDefault() <= num1 & nullable.HasValue))
        {
          nullable = newValue;
          int num2 = 31 /*0x1F*/;
          if (!(nullable.GetValueOrDefault() > num2 & nullable.HasValue))
            return;
        }
      }
      throw new PXSetPropertyException<CABankFeedDetail.statementStartDay>("Incorrect Statement Start Day. Select a value between 1 and 31.", (PXErrorLevel) 4);
    }
  }

  protected virtual void _(
    Events.FieldDefaulting<CABankFeedMaint.TransactionsFilter.date> e)
  {
    if (!(e.Row is CABankFeedMaint.TransactionsFilter))
      return;
    DateTime date = PXTimeZoneInfo.Now.Subtract(new TimeSpan(7, 0, 0, 0)).Date;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<CABankFeedMaint.TransactionsFilter.date>, object, object>) e).NewValue = (object) date;
  }

  public static DateTime GetImportStartDateOverrideEnabled(
    PXGraph graph,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail)
  {
    DateTime dateOverrideEnabled = CABankFeedMaint.GetImportStartDateOverrideEnabled(bankFeed, bankFeedDetail);
    IEnumerable<CABankTran> transMultipleMapping = CABankFeedMaint.GetLatestBankTransMultipleMapping(graph, bankFeed, bankFeedDetail);
    if (transMultipleMapping.Count<CABankTran>() == 1)
    {
      CABankTran caBankTran = transMultipleMapping.First<CABankTran>();
      if (PXCache<CABankTran>.GetExtension<CABankTranFeedSource>(caBankTran).Source == null)
      {
        DateTime? tranDate = caBankTran.TranDate;
        DateTime dateTime = dateOverrideEnabled;
        if ((tranDate.HasValue ? (tranDate.GetValueOrDefault() >= dateTime ? 1 : 0) : 0) != 0)
          dateOverrideEnabled = caBankTran.TranDate.Value.AddDays(1.0);
      }
    }
    return dateOverrideEnabled;
  }

  public static DateTime GetImportStartDateOverrideEnabled(
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail)
  {
    DateTime dateOverrideEnabled = bankFeed.ImportStartDate.Value;
    if (bankFeedDetail.ManualImportDate.HasValue)
    {
      DateTime dateTime = bankFeedDetail.ManualImportDate.Value;
      if (dateTime > dateOverrideEnabled)
        dateOverrideEnabled = dateTime;
    }
    return dateOverrideEnabled;
  }

  public static DateTime GetImportStratDateMultipleMapping(
    PXGraph graph,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail)
  {
    DateTime dateMultipleMapping = DateTime.MinValue;
    CABankTran caBankTran1 = (CABankTran) null;
    CABankTran caBankTran2 = (CABankTran) null;
    CABankFeedAccountMapping mappingForDetail = CABankFeedMaint.GetMappingForDetail(graph, bankFeedDetail);
    foreach (CABankTran caBankTran3 in CABankFeedMaint.GetLatestBankTransMultipleMapping(graph, bankFeed, bankFeedDetail))
    {
      CABankTranFeedSource extension = PXCache<CABankTran>.GetExtension<CABankTranFeedSource>(caBankTran3);
      Guid? feedAccountMapId1 = extension.BankFeedAccountMapID;
      bool flag = extension.Source == null;
      if (!feedAccountMapId1.HasValue)
      {
        if (caBankTran2 == null)
        {
          caBankTran2 = caBankTran3;
        }
        else
        {
          DateTime? tranDate1 = caBankTran3.TranDate;
          DateTime? tranDate2 = caBankTran2.TranDate;
          if ((tranDate1.HasValue & tranDate2.HasValue ? (tranDate1.GetValueOrDefault() > tranDate2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          {
            DateTime? tranDate3 = caBankTran3.TranDate;
            DateTime? tranDate4 = caBankTran2.TranDate;
            if (((tranDate3.HasValue == tranDate4.HasValue ? (tranDate3.HasValue ? (tranDate3.GetValueOrDefault() == tranDate4.GetValueOrDefault() ? 1 : 0) : 1) : 0) & (flag ? 1 : 0)) == 0)
              goto label_8;
          }
          caBankTran2 = caBankTran3;
        }
      }
label_8:
      if (feedAccountMapId1.HasValue)
      {
        Guid? nullable = feedAccountMapId1;
        Guid? feedAccountMapId2 = mappingForDetail.BankFeedAccountMapID;
        if ((nullable.HasValue == feedAccountMapId2.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == feedAccountMapId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          caBankTran1 = caBankTran3;
          break;
        }
      }
    }
    DateTime? nullable1;
    if (caBankTran1 != null)
    {
      nullable1 = caBankTran1.TranDate;
      dateMultipleMapping = nullable1.Value;
    }
    else if (caBankTran2 != null)
    {
      nullable1 = caBankTran2.TranDate;
      dateMultipleMapping = nullable1.Value;
      if (PXCache<CABankTran>.GetExtension<CABankTranFeedSource>(caBankTran2).Source == null)
        dateMultipleMapping = dateMultipleMapping.AddDays(1.0);
    }
    nullable1 = bankFeed.ImportStartDate;
    DateTime dateTime = dateMultipleMapping;
    if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() > dateTime ? 1 : 0) : 0) != 0)
    {
      nullable1 = bankFeed.ImportStartDate;
      dateMultipleMapping = nullable1.Value;
    }
    return dateMultipleMapping;
  }

  public static DateTime GetImportStratDateSingleMapping(
    PXGraph graph,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail)
  {
    DateTime dateSingleMapping = DateTime.MinValue;
    CABankTran caBankTran1 = (CABankTran) null;
    PXSelectGroupBy<CABankTran, Where<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.tranDate, GreaterEqual<Required<CABankTran.tranDate>>>>, Aggregate<GroupBy<CABankTranFeedSource.source, Max<CABankTran.tranDate>>>> pxSelectGroupBy = new PXSelectGroupBy<CABankTran, Where<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.tranDate, GreaterEqual<Required<CABankTran.tranDate>>>>, Aggregate<GroupBy<CABankTranFeedSource.source, Max<CABankTran.tranDate>>>>(graph);
    IEnumerable<CABankTran> source = Enumerable.Empty<CABankTran>();
    using (new PXFieldScope(((PXSelectBase) pxSelectGroupBy).View, new Type[3]
    {
      typeof (CABankTranFeedSource.source),
      typeof (CABankTran.tranDate),
      typeof (CABankTran.cashAccountID)
    }))
      source = GraphHelper.RowCast<CABankTran>((IEnumerable) ((PXSelectBase<CABankTran>) pxSelectGroupBy).Select(new object[2]
      {
        (object) bankFeedDetail.CashAccountID,
        (object) bankFeed.ImportStartDate
      }));
    DateTime? maxDate = source.Max<CABankTran, DateTime?>((Func<CABankTran, DateTime?>) (i => i.TranDate));
    if (maxDate.HasValue)
    {
      foreach (CABankTran caBankTran2 in source.Where<CABankTran>((Func<CABankTran, bool>) (i =>
      {
        DateTime? tranDate = i.TranDate;
        DateTime? nullable = maxDate;
        if (tranDate.HasValue != nullable.HasValue)
          return false;
        return !tranDate.HasValue || tranDate.GetValueOrDefault() == nullable.GetValueOrDefault();
      })))
      {
        if (PXCache<CABankTran>.GetExtension<CABankTranFeedSource>(caBankTran2).Source == null)
        {
          caBankTran1 = caBankTran2;
          break;
        }
        caBankTran1 = caBankTran2;
      }
    }
    DateTime? nullable1;
    if (caBankTran1 != null)
    {
      if (PXCache<CABankTran>.GetExtension<CABankTranFeedSource>(caBankTran1).Source != bankFeed.Type)
      {
        nullable1 = caBankTran1.TranDate;
        dateSingleMapping = nullable1.Value.AddDays(1.0);
      }
      else
      {
        nullable1 = caBankTran1.TranDate;
        dateSingleMapping = nullable1.Value;
      }
    }
    if (caBankTran1 != null)
    {
      nullable1 = bankFeed.ImportStartDate;
      DateTime dateTime = dateSingleMapping;
      if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() > dateTime ? 1 : 0) : 0) == 0)
        goto label_22;
    }
    nullable1 = bankFeed.ImportStartDate;
    dateSingleMapping = nullable1.Value;
label_22:
    return dateSingleMapping;
  }

  public static DateTime GetImportStartDate(
    PXGraph graph,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail)
  {
    return !bankFeed.MultipleMapping.GetValueOrDefault() ? CABankFeedMaint.GetImportStratDateSingleMapping(graph, bankFeed, bankFeedDetail) : (!bankFeedDetail.OverrideDate.GetValueOrDefault() || !bankFeedDetail.ManualImportDate.HasValue ? CABankFeedMaint.GetImportStratDateMultipleMapping(graph, bankFeed, bankFeedDetail) : CABankFeedMaint.GetImportStartDateOverrideEnabled(graph, bankFeed, bankFeedDetail));
  }

  private static IEnumerable<CABankTran> GetLatestBankTransMultipleMapping(
    PXGraph graph,
    CABankFeed bankFeed,
    CABankFeedDetail bankFeedDetail)
  {
    PXSelectGroupBy<CABankTran, Where<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.tranDate, GreaterEqual<Required<CABankTran.tranDate>>>>, Aggregate<GroupBy<CABankTranFeedSource.source, GroupBy<CABankTranFeedSource.bankFeedAccountMapID, Max<CABankTran.tranDate>>>>> pxSelectGroupBy = new PXSelectGroupBy<CABankTran, Where<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.tranDate, GreaterEqual<Required<CABankTran.tranDate>>>>, Aggregate<GroupBy<CABankTranFeedSource.source, GroupBy<CABankTranFeedSource.bankFeedAccountMapID, Max<CABankTran.tranDate>>>>>(graph);
    Enumerable.Empty<CABankTran>();
    using (new PXFieldScope(((PXSelectBase) pxSelectGroupBy).View, new Type[4]
    {
      typeof (CABankTranFeedSource.source),
      typeof (CABankTranFeedSource.bankFeedAccountMapID),
      typeof (CABankTran.tranDate),
      typeof (CABankTran.cashAccountID)
    }))
      return GraphHelper.RowCast<CABankTran>((IEnumerable) ((PXSelectBase<CABankTran>) pxSelectGroupBy).Select(new object[2]
      {
        (object) bankFeedDetail.CashAccountID,
        (object) bankFeed.ImportStartDate
      }));
  }

  protected virtual CABankFeedDetail FindDuplicateCashAccountLink(CABankFeedDetail row)
  {
    if (!row.CashAccountID.HasValue)
      return (CABankFeedDetail) null;
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    bool? multipleMapping;
    if (current != null)
    {
      multipleMapping = current.MultipleMapping;
      bool flag = false;
      if (multipleMapping.GetValueOrDefault() == flag & multipleMapping.HasValue)
      {
        foreach (CABankFeedDetail duplicateCashAccountLink in ((PXGraph) this).Caches[typeof (CABankFeedDetail)].Cached)
        {
          int? nullable1 = duplicateCashAccountLink.LineNbr;
          int? nullable2 = row.LineNbr;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            nullable2 = duplicateCashAccountLink.CashAccountID;
            nullable1 = row.CashAccountID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && duplicateCashAccountLink.BankFeedID == row.BankFeedID)
            {
              PXEntryStatus status = ((PXSelectBase) this.BankFeedDetail).Cache.GetStatus((object) duplicateCashAccountLink);
              if (status != 3 && status != 4)
                return duplicateCashAccountLink;
            }
          }
        }
      }
    }
    foreach (PXResult<CABankFeed, CABankFeedDetail> pxResult in PXSelectBase<CABankFeed, PXSelectJoin<CABankFeed, InnerJoin<CABankFeedDetail, On<CABankFeedDetail.bankFeedID, Equal<CABankFeed.bankFeedID>>>, Where<CABankFeedDetail.cashAccountID, Equal<Required<CABankFeedDetail.cashAccountID>>, And<Where<CABankFeed.status, Equal<CABankFeedStatus.active>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.CashAccountID
    }))
    {
      CABankFeedDetail caBankFeedDetail = PXResult<CABankFeed, CABankFeedDetail>.op_Implicit(pxResult);
      CABankFeed caBankFeed = PXResult<CABankFeed, CABankFeedDetail>.op_Implicit(pxResult);
      if (row.BankFeedID != caBankFeedDetail.BankFeedID)
      {
        multipleMapping = current.MultipleMapping;
        bool flag1 = false;
        if (!(multipleMapping.GetValueOrDefault() == flag1 & multipleMapping.HasValue))
        {
          multipleMapping = caBankFeed.MultipleMapping;
          bool flag2 = false;
          if (!(multipleMapping.GetValueOrDefault() == flag2 & multipleMapping.HasValue))
            continue;
        }
        return PXResult<CABankFeed, CABankFeedDetail>.op_Implicit(pxResult);
      }
    }
    return (CABankFeedDetail) null;
  }

  protected virtual void UpdateCashAccountEntry(CashAccount cashAccount)
  {
    if (cashAccount == null || cashAccount.StatementImportTypeName == null)
      return;
    cashAccount.StatementImportTypeName = (string) null;
    ((PXSelectBase<CashAccount>) this.CashAccounts).Update(cashAccount);
  }

  protected virtual CashAccount GetCashAccount(int? cashAccountId)
  {
    return PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) cashAccountId
    }));
  }

  protected virtual CABankFeed GetBankFeed(string bankFeedId)
  {
    return PXResultset<CABankFeed>.op_Implicit(PXSelectBase<CABankFeed, PXSelect<CABankFeed, Where<CABankFeed.bankFeedID, Equal<Required<CABankFeed.bankFeedID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) bankFeedId
    }));
  }

  protected virtual CABankFeedAccountMapping GetMappingForDetail(CABankFeedDetail detail)
  {
    return GraphHelper.RowCast<CABankFeedAccountMapping>((IEnumerable) ((PXSelectBase<CABankFeedAccountMapping>) this.BankFeedAccountMapping).Select(Array.Empty<object>())).Where<CABankFeedAccountMapping>((Func<CABankFeedAccountMapping, bool>) (i =>
    {
      if (!(i.BankFeedID == detail.BankFeedID))
        return false;
      int? lineNbr1 = i.LineNbr;
      int? lineNbr2 = detail.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })).FirstOrDefault<CABankFeedAccountMapping>();
  }

  protected virtual (string, string)[] GetCorpCardFilters(CABankFeed bankFeed)
  {
    return this.GetSpecificManager(bankFeed).AvailableCorpCardFilters;
  }

  private void ShowTransactionsFromService(CABankFeed bankFeed)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedMaint.\u003C\u003Ec__DisplayClass110_0 displayClass1100 = new CABankFeedMaint.\u003C\u003Ec__DisplayClass110_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1100.bankFeed = bankFeed;
    // ISSUE: reference to a compiler-generated field
    displayClass1100.filter = ((PXSelectBase<CABankFeedMaint.TransactionsFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (displayClass1100.filter == null || !this.ValidateFields(displayClass1100.filter))
      return;
    // ISSUE: reference to a compiler-generated field
    this.CopyBankFeedRecord(displayClass1100.bankFeed);
    // ISSUE: reference to a compiler-generated method
    string str1 = GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>())).Where<CABankFeedDetail>(new Func<CABankFeedDetail, bool>(displayClass1100.\u003CShowTransactionsFromService\u003Eb__0)).Select<CABankFeedDetail, string>((Func<CABankFeedDetail, string>) (i => i.AccountID)).FirstOrDefault<string>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    displayClass1100.manager = this.GetSpecificManager(displayClass1100.bankFeed);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    displayClass1100.tranFilter = displayClass1100.manager.GetTransactionsFilterForTesting(displayClass1100.filter.DateTo.Value);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    displayClass1100.tranFilter.StartDate = displayClass1100.filter.Date.Value;
    // ISSUE: reference to a compiler-generated field
    displayClass1100.tranFilter.AccountsID = new string[1]
    {
      str1
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    displayClass1100.tranFilter.TransactionsLimit = displayClass1100.filter.MaxTransactions;
    // ISSUE: reference to a compiler-generated field
    displayClass1100.tranFilter.TransactionsOrder = LoadTransactionsData.Order.DescDate;
    // ISSUE: reference to a compiler-generated field
    displayClass1100.tranFilter.TestMode = true;
    ((PXSelectBase) this.BankFeedTransactions).Cache.Clear();
    PXLongOperation.ClearStatus(((PXGraph) this).UID);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1100, __methodptr(\u003CShowTransactionsFromService\u003Eb__2)));
    PXLongOperation.WaitCompletion(((PXGraph) this).UID);
    string str2 = PXMessages.LocalizeNoPrefix("Receipt");
    string str3 = PXMessages.LocalizeNoPrefix("Disbursement");
    IEnumerable<BankFeedTransaction> customInfo = PXLongOperation.GetCustomInfo(((PXGraph) this).UID) as IEnumerable<BankFeedTransaction>;
    int num1 = 0;
    if (customInfo != null)
    {
      PXLongOperation.ClearStatus(((PXGraph) this).UID);
      foreach (BankFeedTransaction bankFeedTransaction1 in customInfo)
      {
        BankFeedTransaction bankFeedTransaction2 = bankFeedTransaction1;
        Decimal? amount = bankFeedTransaction1.Amount;
        Decimal num2 = 0M;
        string str4 = amount.GetValueOrDefault() < num2 & amount.HasValue ? str2 : str3;
        bankFeedTransaction2.Type = str4;
        BankFeedTransaction bankFeedTransaction3 = bankFeedTransaction1;
        amount = bankFeedTransaction1.Amount;
        Decimal? nullable = new Decimal?(Math.Abs(amount.Value));
        bankFeedTransaction3.Amount = nullable;
        GraphHelper.Hold(((PXSelectBase) this.BankFeedTransactions).Cache, (object) bankFeedTransaction1);
        ++num1;
      }
    }
    if (num1 != 0)
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<CABankFeedMaint.TransactionsFilter.date>((object) displayClass1100.filter, (object) displayClass1100.filter.Date, (Exception) new PXException("There are no transactions for the selected dates."));
  }

  private void DisconnectBankFeed(bool keepUserId)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    current.AccessToken = (string) null;
    current.Institution = (string) null;
    current.InstitutionID = (string) null;
    current.ExternalItemID = (string) null;
    current.MultipleMapping = new bool?(false);
    current.Status = "D";
    if (!keepUserId)
      current.ExternalUserID = (string) null;
    ((PXSelectBase<CABankFeed>) this.BankFeed).Update(current);
    this.DeleteAccounts();
    ((PXAction) this.Save).Press();
  }

  private void CheckFeedAccountIsMappedToCashAccount()
  {
    if (GraphHelper.RowCast<CABankFeedDetail>((IEnumerable) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(Array.Empty<object>())).All<CABankFeedDetail>((Func<CABankFeedDetail, bool>) (i => !i.CashAccountID.HasValue)))
      throw new PXException("At least one bank feed account must be mapped to the cash account on the Cash Accounts tab.");
  }

  private bool ValidateFields(CABankFeedMaint.TransactionsFilter filter)
  {
    if (!filter.Date.HasValue)
    {
      ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<CABankFeedMaint.TransactionsFilter.date>((object) filter, (object) filter.Date, (Exception) new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeedMaint.TransactionsFilter.date>(((PXSelectBase) this.Filter).Cache)
      }));
      return false;
    }
    if (!filter.DateTo.HasValue)
    {
      ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<CABankFeedMaint.TransactionsFilter.date>((object) filter, (object) filter.DateTo, (Exception) new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeedMaint.TransactionsFilter.dateTo>(((PXSelectBase) this.Filter).Cache)
      }));
      return false;
    }
    if (!filter.MaxTransactions.HasValue)
    {
      ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<CABankFeedMaint.TransactionsFilter.maxTransactions>((object) filter, (object) filter.MaxTransactions, (Exception) new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeedMaint.TransactionsFilter.dateTo>(((PXSelectBase) this.Filter).Cache)
      }));
      return false;
    }
    if (!filter.LineNbr.HasValue)
    {
      ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<CABankFeedMaint.TransactionsFilter.lineNbr>((object) filter, (object) filter.LineNbr, (Exception) new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankFeedMaint.TransactionsFilter.lineNbr>(((PXSelectBase) this.Filter).Cache)
      }));
      return false;
    }
    DateTime? date = filter.Date;
    DateTime? dateTo = filter.DateTo;
    if ((date.HasValue & dateTo.HasValue ? (date.GetValueOrDefault() > dateTo.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return true;
    ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<CABankFeedMaint.TransactionsFilter.dateTo>((object) filter, (object) filter.DateTo, (Exception) new PXException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
    {
      (object) filter.Date
    }));
    return false;
  }

  private string[] GetAvailableTransactionFields(CABankFeed bankFeed)
  {
    if (this.AvailableTransactionFields == null & this.IsServiceFeed(bankFeed))
      this.AvailableTransactionFields = this.GetSpecificManager(bankFeed).AvailableTransactionFields;
    return this.AvailableTransactionFields;
  }

  private CABankFeed CopyBankFeedRecord(CABankFeed bankFeed)
  {
    return (CABankFeed) ((PXSelectBase) this.BankFeed).Cache.CreateCopy((object) bankFeed);
  }

  private BankFeedManager GetSpecificManager(CABankFeed bankfeed)
  {
    return this.BankFeedManagerProvider(bankfeed.Type);
  }

  private void CheckCashAccountAlreadyLinked(CABankFeedDetail det)
  {
    CABankFeedDetail duplicateCashAccountLink = this.FindDuplicateCashAccountLink(det);
    if (duplicateCashAccountLink != null)
    {
      CashAccount cashAccount = this.GetCashAccount(det.CashAccountID);
      CABankFeed bankFeed = this.GetBankFeed(duplicateCashAccountLink.BankFeedID);
      string str = $"{duplicateCashAccountLink.AccountName}:{duplicateCashAccountLink.AccountMask}";
      PXSetPropertyException propertyException = new PXSetPropertyException("The {0} cash account is already linked to the {1} bank account in the {2} bank feed.", (PXErrorLevel) 4, new object[3]
      {
        (object) cashAccount.CashAccountCD.Trim(),
        (object) str,
        (object) bankFeed.BankFeedID
      });
      ((PXSelectBase) this.BankFeedDetail).Cache.RaiseExceptionHandling<CABankFeedDetail.cashAccountID>((object) det, (object) cashAccount.CashAccountCD, (Exception) propertyException);
      throw propertyException;
    }
  }

  private void CheckManualImportStartDate(CABankFeedDetail det, DateTime defaultImportDate)
  {
    DateTime? importStartDate = det.ImportStartDate;
    PXCache cache = ((PXSelectBase) this.BankFeedDetail).Cache;
    DateTime? nullable = importStartDate;
    DateTime dateTime = defaultImportDate;
    if ((nullable.HasValue ? (nullable.GetValueOrDefault() < dateTime ? 1 : 0) : 0) != 0)
    {
      CashAccount cashAccount = this.GetCashAccount(det.CashAccountID);
      PXSetPropertyException propertyException = new PXSetPropertyException("The date in the Import Transactions From box cannot be earlier than {0} because there are bank transactions loaded for the {1} cash account from this bank account via the bank feed or entered manually.", (PXErrorLevel) 4, new object[2]
      {
        (object) defaultImportDate.Date.ToShortDateString(),
        (object) cashAccount.CashAccountCD.Trim()
      });
      cache.RaiseExceptionHandling<CABankFeedDetail.importStartDate>((object) det, (object) importStartDate, (Exception) propertyException);
      throw propertyException;
    }
  }

  private void CheckManualImportStartDate(CABankFeedDetail det)
  {
    DateTime dateMultipleMapping = CABankFeedMaint.GetImportStratDateMultipleMapping((PXGraph) this, ((PXSelectBase<CABankFeed>) this.BankFeed).Current, det);
    this.CheckManualImportStartDate(det, dateMultipleMapping);
  }

  private void SetCashAccountWarningIfNeeded(PXCache cache, CABankFeedDetail detail)
  {
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (!detail.CashAccountID.HasValue)
    {
      propertyException = new PXSetPropertyException("Specify a cash account to import data from the bank feed.", (PXErrorLevel) 2);
    }
    else
    {
      CashAccount cashAccount = PXSelectorAttribute.Select<CABankFeedDetail.cashAccountID>(cache, (object) detail) as CashAccount;
      if (!string.IsNullOrEmpty(detail.Currency) && cashAccount.CuryID != detail.Currency)
        propertyException = new PXSetPropertyException("Select a cash account in the {0} currency.", (PXErrorLevel) 2, new object[1]
        {
          (object) detail.Currency
        });
    }
    if (((PXFieldState) cache.GetStateExt<CABankFeedDetail.cashAccountID>((object) detail)).ErrorLevel == 4)
      return;
    cache.RaiseExceptionHandling<CABankFeedDetail.cashAccountID>((object) detail, (object) detail.CashAccountID, (Exception) propertyException);
  }

  private void SetEnabledRequired(PXCache cache, CABankFeed bf)
  {
    if (bf == null || !bf.CreateExpenseReceipt.HasValue)
      return;
    PXCache pxCache1 = cache;
    CABankFeed caBankFeed1 = bf;
    bool? createExpenseReceipt = bf.CreateExpenseReceipt;
    int num1 = createExpenseReceipt.Value ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CABankFeed.defaultExpenseItemID>(pxCache1, (object) caBankFeed1, num1 != 0);
    PXCache pxCache2 = cache;
    CABankFeed caBankFeed2 = bf;
    createExpenseReceipt = bf.CreateExpenseReceipt;
    int num2 = createExpenseReceipt.Value ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CABankFeed.createReceiptForPendingTran>(pxCache2, (object) caBankFeed2, num2 != 0);
    PXCache pxCache3 = cache;
    createExpenseReceipt = bf.CreateExpenseReceipt;
    int num3 = createExpenseReceipt.Value ? 1 : 0;
    PXUIFieldAttribute.SetRequired<CABankFeed.defaultExpenseItemID>(pxCache3, num3 != 0);
  }

  private static CABankFeedAccountMapping GetMappingForDetail(
    PXGraph graph,
    CABankFeedDetail detail)
  {
    return GraphHelper.RowCast<CABankFeedAccountMapping>((IEnumerable) PXSelectBase<CABankFeedAccountMapping, PXSelect<CABankFeedAccountMapping, Where<CABankFeedAccountMapping.bankFeedID, Equal<Required<CABankFeed.bankFeedID>>>>.Config>.Select(graph, new object[1]
    {
      (object) detail.BankFeedID
    })).Where<CABankFeedAccountMapping>((Func<CABankFeedAccountMapping, bool>) (i =>
    {
      if (!(i.BankFeedID == detail.BankFeedID))
        return false;
      int? lineNbr1 = i.LineNbr;
      int? lineNbr2 = detail.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })).FirstOrDefault<CABankFeedAccountMapping>();
  }

  private bool AllowMultipleMappingForDetail(CABankFeed bankFeed, CABankFeedDetail bankFeedDetail)
  {
    return bankFeedDetail.CashAccountID.HasValue && bankFeed.MultipleMapping.GetValueOrDefault();
  }

  private void SetManualImportDate(CABankFeedDetail bankFeedDet, DateTime? newDate)
  {
    bankFeedDet.ManualImportDate = newDate;
    bankFeedDet.OverrideDate = new bool?(true);
  }

  private void ClearManualImportDate(CABankFeedDetail bankFeedDet)
  {
    bankFeedDet.ManualImportDate = new DateTime?();
    bankFeedDet.OverrideDate = new bool?(false);
  }

  private void CreateDefaultMappingRules()
  {
    this.InsertBankFeedFieldMapping("ExtRefNbr", "=ISNULL([Check Number], [Transaction ID])");
    if (!EnumerableExtensions.IsIn<string>(((PXSelectBase<CABankFeed>) this.CurrentBankFeed).Current.Type, "P", "T"))
      return;
    this.InsertBankFeedFieldMapping("CardNumber", "=TRIM(ISNULL([Account Owner], ''))");
  }

  private void InsertBankFeedFieldMapping(string targetField, string sourceFieldOrValue)
  {
    ((PXSelectBase<CABankFeedFieldMapping>) this.BankFeedFieldMapping).Insert(new CABankFeedFieldMapping()
    {
      BankFeedID = ((PXSelectBase<CABankFeed>) this.CurrentBankFeed).Current.BankFeedID,
      TargetField = targetField,
      SourceFieldOrValue = sourceFieldOrValue,
      Active = new bool?(true)
    });
  }

  private void SetCorpCardFilters(PXCache cache, CABankFeedCorpCard corpCard)
  {
    CABankFeed current = ((PXSelectBase<CABankFeed>) this.BankFeed).Current;
    if (current != null && this.CorpCardFilters == null)
      this.CorpCardFilters = this.GetCorpCardFilters(current);
    PXStringListAttribute.SetList<CABankFeedCorpCard.matchField>(cache, (object) corpCard, this.CorpCardFilters);
  }

  private bool IsServiceFeed(CABankFeed bankFeed)
  {
    return bankFeed.Type == "P" || bankFeed.Type == "M" || bankFeed.Type == "T";
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    ((PXGraph) this).CopyPasteGetScript(isImportSimple, script, containers);
    this.HandleCopyPaste(isImportSimple, script, containers);
  }

  protected virtual void HandleCopyPaste(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
  }

  [PXMergeAttributes]
  [CABankFeedMaint.PXFormulaEditor_AddFields]
  public virtual void _(
    Events.CacheAttached<CABankFeedFieldMapping.sourceFieldOrValue> e)
  {
  }

  [PXCacheName("TransactionsFilter")]
  public class TransactionsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    [PXSelector(typeof (Search<CABankFeedDetail.lineNbr, Where<CABankFeedDetail.bankFeedID, Equal<Current<CABankFeed.bankFeedID>>>, OrderBy<Asc<CABankFeedDetail.lineNbr>>>), new Type[] {typeof (CABankFeedDetail.accountName), typeof (CABankFeedDetail.accountMask), typeof (CABankFeedDetail.accountID)}, SubstituteKey = typeof (CABankFeedDetail.accountName))]
    [PXUIField(DisplayName = "Account Name", Required = true)]
    public virtual int? LineNbr { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "Date From", Required = true)]
    public virtual DateTime? Date { get; set; }

    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Date To", Required = true)]
    public virtual DateTime? DateTo { get; set; }

    [PXInt(MinValue = 1, MaxValue = 9999)]
    [PXDefault(30)]
    [PXUIField(DisplayName = "Max. Number of Records", Required = true)]
    public virtual int? MaxTransactions { get; set; }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CABankFeedMaint.TransactionsFilter.lineNbr>
    {
    }

    public abstract class date : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CABankFeedMaint.TransactionsFilter.date>
    {
    }

    public abstract class dateTo : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CABankFeedMaint.TransactionsFilter.dateTo>
    {
    }

    public abstract class maxTransactions : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CABankFeedMaint.TransactionsFilter.maxTransactions>
    {
    }
  }

  public class PXFormulaEditor_AddFieldsAttribute : PXFormulaEditor.OptionsProviderAttribute
  {
    public virtual void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
    {
      CABankFeedMaint caBankFeedMaint = (CABankFeedMaint) graph;
      if (((PXSelectBase<CABankFeed>) caBankFeedMaint.BankFeed).Current == null)
        return;
      CABankFeed currentPrimaryObject = GraphHelper.GetCurrentPrimaryObject(graph) as CABankFeed;
      CABankFeedMappingSourceHelper mappingSourceHelper = new CABankFeedMappingSourceHelper(((PXSelectBase) caBankFeedMaint.BankFeed).Cache);
      foreach (string str in currentPrimaryObject.Type == "F" ? mappingSourceHelper.GetFileFieldListForFormula(currentPrimaryObject) : mappingSourceHelper.GetFieldListForFormula(currentPrimaryObject.Type))
        options.Add(new FormulaOption()
        {
          Category = "Fields/Bank Feed Transaction",
          Value = str
        });
    }
  }
}
