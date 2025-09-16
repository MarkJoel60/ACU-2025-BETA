// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.GraphExtensions.CABankFeedBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA.BankFeed;
using PX.Objects.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA.GraphExtensions;

public abstract class CABankFeedBase<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TPrimary : class, IBqlTable, new()
{
  public PXSetup<PX.Objects.CA.CASetup> CASetup;
  public PXSelectJoin<CABankFeedDetail, InnerJoin<CABankFeed, On<CABankFeed.bankFeedID, Equal<CABankFeedDetail.bankFeedID>>>, Where<CABankFeedDetail.cashAccountID, Equal<Required<CABankFeedDetail.cashAccountID>>, And<Where<CABankFeed.status, Equal<CABankFeedStatus.active>>>>> BankFeedDetail;
  public PXAction<TPrimary> retrieveTransactions;
  public PXAction<TPrimary> syncUpdateFeed;

  [InjectDependency]
  internal Func<string, BankFeedManager> BankFeedManagerProvider { get; set; }

  [PXUIField(DisplayName = "Retrieve Transactions", Visible = true)]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable RetrieveTransactions(PXAdapter adapter)
  {
    IEnumerable enumerable = adapter.Get();
    int? cashAccountId = this.GetCashAccountId();
    if (!cashAccountId.HasValue)
      return enumerable;
    Dictionary<CABankFeed, List<CABankFeedDetail>> dictionary = ((IEnumerable<PXResult<CABankFeedDetail>>) ((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(new object[1]
    {
      (object) cashAccountId
    })).AsEnumerable<PXResult<CABankFeedDetail>>().Cast<PXResult<CABankFeedDetail, CABankFeed>>().GroupBy<PXResult<CABankFeedDetail, CABankFeed>, CABankFeed, CABankFeedDetail>((Func<PXResult<CABankFeedDetail, CABankFeed>, CABankFeed>) (i => PXResult<CABankFeedDetail, CABankFeed>.op_Implicit(i)), (Func<PXResult<CABankFeedDetail, CABankFeed>, CABankFeedDetail>) (i => PXResult<CABankFeedDetail, CABankFeed>.op_Implicit(i)), this.GetEqualityComparerByField()).ToDictionary<IGrouping<CABankFeed, CABankFeedDetail>, CABankFeed, List<CABankFeedDetail>>((Func<IGrouping<CABankFeed, CABankFeedDetail>, CABankFeed>) (i => i.Key), (Func<IGrouping<CABankFeed, CABankFeedDetail>, List<CABankFeedDetail>>) (i => i.ToList<CABankFeedDetail>()));
    CABankFeed bankFeed = dictionary.Where<KeyValuePair<CABankFeed, List<CABankFeedDetail>>>((Func<KeyValuePair<CABankFeed, List<CABankFeedDetail>>, bool>) (i => i.Key.RetrievalStatus == "L")).Select<KeyValuePair<CABankFeed, List<CABankFeedDetail>>, CABankFeed>((Func<KeyValuePair<CABankFeed, List<CABankFeedDetail>>, CABankFeed>) (i => i.Key)).FirstOrDefault<CABankFeed>();
    if (bankFeed != null)
    {
      this.UpdateFeed(bankFeed);
      return enumerable;
    }
    this.ImportTransactions((IDictionary<CABankFeed, List<CABankFeedDetail>>) dictionary);
    return enumerable;
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXButton]
  public virtual IEnumerable SyncUpdateFeed(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedBase<TGraph, TPrimary>.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new CABankFeedBase<TGraph, TPrimary>.\u003C\u003Ec__DisplayClass9_0();
    IEnumerable enumerable = adapter.Get();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.formProcRes = adapter.CommandArguments;
    int? cashAccountId = this.GetCashAccountId();
    // ISSUE: reference to a compiler-generated field
    if (string.IsNullOrEmpty(cDisplayClass90.formProcRes) || !cashAccountId.HasValue)
      return enumerable;
    PXResult<CABankFeedDetail, CABankFeed> pxResult = (PXResult<CABankFeedDetail, CABankFeed>) PXResultset<CABankFeedDetail>.op_Implicit(((PXSelectBase<CABankFeedDetail>) this.BankFeedDetail).Select(new object[1]
    {
      (object) cashAccountId
    }));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.bankFeed = PXResult<CABankFeedDetail, CABankFeed>.op_Implicit(pxResult);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.bankfeedDetail = PXResult<CABankFeedDetail, CABankFeed>.op_Implicit(pxResult);
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.bankFeed == null)
      return enumerable;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.guid = (Guid) this.Base.UID;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.importToSingle = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ImportToSingleAccount.GetValueOrDefault();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.manager = this.GetSpecificManager(cDisplayClass90.bankFeed);
    // ISSUE: method pointer
    PXLongOperation.StartOperation<TGraph>((PXGraphExtension<TGraph>) this, new PXToggleAsyncDelegate((object) cDisplayClass90, __methodptr(\u003CSyncUpdateFeed\u003Eb__0)));
    return enumerable;
  }

  protected virtual void ImportTransactions(
    IDictionary<CABankFeed, List<CABankFeedDetail>> bankFeedWithDetails)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedBase<TGraph, TPrimary>.\u003C\u003Ec__DisplayClass10_0 cDisplayClass100 = new CABankFeedBase<TGraph, TPrimary>.\u003C\u003Ec__DisplayClass10_0()
    {
      bankFeedWithDetails = bankFeedWithDetails,
      guid = (Guid) this.Base.UID
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.importForOneCashAcc = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ImportToSingleAccount.GetValueOrDefault() || cDisplayClass100.bankFeedWithDetails.Any<KeyValuePair<CABankFeed, List<CABankFeedDetail>>>((Func<KeyValuePair<CABankFeed, List<CABankFeedDetail>>, bool>) (i => i.Key.MultipleMapping.GetValueOrDefault()));
    // ISSUE: method pointer
    PXLongOperation.StartOperation<TGraph>((PXGraphExtension<TGraph>) this, new PXToggleAsyncDelegate((object) cDisplayClass100, __methodptr(\u003CImportTransactions\u003Eb__1)));
  }

  protected virtual void UpdateFeed(CABankFeed bankFeed)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankFeedBase<TGraph, TPrimary>.\u003C\u003Ec__DisplayClass11_0 cDisplayClass110 = new CABankFeedBase<TGraph, TPrimary>.\u003C\u003Ec__DisplayClass11_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.bankFeed = bankFeed;
    if (this.Base.Views[this.Base.PrimaryView].Ask("The credentials for the bank feed connection have been changed. Do you want to update them?", (MessageButtons) 1) == 2)
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.manager = this.GetSpecificManager(cDisplayClass110.bankFeed);
    // ISSUE: method pointer
    PXLongOperation.StartOperation<TGraph>((PXGraphExtension<TGraph>) this, new PXToggleAsyncDelegate((object) cDisplayClass110, __methodptr(\u003CUpdateFeed\u003Eb__0)));
  }

  protected virtual void _(Events.RowSelected<CABankTran> e)
  {
    CABankTran row = e.Row;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTran>>) e).Cache;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<CABankTranFeedSource.bankFeedAccountMapID>(cache, (object) row, false);
  }

  protected IEqualityComparer<CABankFeed> GetEqualityComparerByField()
  {
    return (IEqualityComparer<CABankFeed>) new FieldSubsetEqualityComparer<CABankFeed>(this.Base.Caches[typeof (CABankFeed)], new Type[1]
    {
      typeof (CABankFeed.bankFeedID)
    });
  }

  private static void ImportTransactions(
    CABankFeed bankFeed,
    List<CABankFeedDetail> bankFeedDetail,
    bool importToOneCashAccount,
    Guid guid)
  {
    Dictionary<int, string> result;
    if (importToOneCashAccount)
      result = CABankFeedImport.ImportTransactionsForAccounts(bankFeed, bankFeedDetail, guid).GetAwaiter().GetResult();
    else
      result = CABankFeedImport.ImportTransactions(new List<CABankFeed>()
      {
        bankFeed
      }, guid).GetAwaiter().GetResult();
    if (result == null || result.Count<KeyValuePair<int, string>>() <= 0)
      return;
    PXLongOperation.SetCustomInfo((object) new BankFeedRedirectToStatementCustomInfo(result));
  }

  private BankFeedManager GetSpecificManager(CABankFeed bankfeed)
  {
    return this.BankFeedManagerProvider(bankfeed.Type);
  }

  public abstract int? GetCashAccountId();
}
