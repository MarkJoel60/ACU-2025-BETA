// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.PlaidBankFeedManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using AutoMapper;
using PX.BankFeed.Common;
using PX.BankFeed.Plaid;
using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal class PlaidBankFeedManager : BankFeedManager
{
  private readonly IBankFeedClient _client;
  private static readonly (string, string)[] availableCorpCardFilters = new (string, string)[3]
  {
    ("N", " "),
    ("O", "Account Owner"),
    ("A", "Name")
  };
  private static readonly string[] availableTransactionFields = new string[11]
  {
    "TransactionID",
    "Date",
    "Amount",
    "IsoCurrencyCode",
    "Name",
    "Category",
    "Pending",
    "PendingTransactionID",
    "Type",
    "AccountID",
    "AccountOwner"
  };
  private static readonly IMapper _mapper = new MapperConfiguration((Action<IMapperConfigurationExpression>) (c => c.AddProfile(typeof (PlaidMapperProfile)))).CreateMapper();

  public override (string, string)[] AvailableCorpCardFilters
  {
    get => PlaidBankFeedManager.availableCorpCardFilters;
  }

  public override string[] AvailableTransactionFields
  {
    get => PlaidBankFeedManager.availableTransactionFields;
  }

  public override bool AllowBatchDownloading => true;

  public override int NumberOfAccountsForBatchDownloading => 31 /*0x1F*/;

  public override DateTime? DefaultImportStartDate
  {
    get
    {
      DateTime dateTime = PXTimeZoneInfo.Now;
      dateTime = dateTime.AddDays(-90.0);
      return new DateTime?(dateTime.Date);
    }
  }

  public override DateTime? MinimumImportStartDate
  {
    get
    {
      DateTime dateTime = PXTimeZoneInfo.Now;
      dateTime = dateTime.AddDays(-730.0);
      return new DateTime?(dateTime.Date);
    }
  }

  public PlaidBankFeedManager(IBankFeedClient client) => this._client = client;

  public override async System.Threading.Tasks.Task ConnectAsync(CABankFeed bankFeed)
  {
    DateTime dateTime = PXTimeZoneInfo.Now;
    dateTime = dateTime.Date;
    int num1 = dateTime.Subtract(bankFeed.ImportStartDate.Value).Days;
    if (num1 < 30)
      num1 = 30;
    int num2 = bankFeed.OrganizationID.Value;
    ConnectTransactions connectTransactions = new ConnectTransactions()
    {
      DaysRequested = num1
    };
    GetFormTokenRequest formTokenRequest = new GetFormTokenRequest()
    {
      Transactions = connectTransactions
    };
    GetFormTokenResponse connectFormTokenAsync = await this._client.GetConnectFormTokenAsync(num2, formTokenRequest, bankFeed.IsTestFeed.IsTrue(), new CancellationToken());
    throw new PXPluginRedirectException<PXPluginRedirectOptions>((PXPluginRedirectOptions) new PlaidFormOptions()
    {
      Token = connectFormTokenAsync.Token,
      Mode = "Connect"
    });
  }

  public override async System.Threading.Tasks.Task UpdateAsync(CABankFeed bankFeed)
  {
    GetFormTokenRequest formTokenRequest = new GetFormTokenRequest();
    string accessToken = bankFeed.AccessToken;
    this.CheckAccessToken(accessToken);
    formTokenRequest.AccessToken = accessToken;
    GetFormTokenResponse updateFormTokenAsync = await this._client.GetUpdateFormTokenAsync(bankFeed.OrganizationID.Value, formTokenRequest, bankFeed.IsTestFeed.IsTrue(), new CancellationToken());
    throw new PXPluginRedirectException<PXPluginRedirectOptions>((PXPluginRedirectOptions) new PlaidFormOptions()
    {
      Token = updateFormTokenAsync.Token,
      Mode = "Update"
    });
  }

  public override async Task<IEnumerable<BankFeedCategory>> GetCategoriesAsync(CABankFeed bankFeed)
  {
    return (IEnumerable<BankFeedCategory>) (await this._client.GetCategoriesAsync(bankFeed.OrganizationID.Value, bankFeed.IsTestFeed.IsTrue(), new CancellationToken())).Categories.Select<Category, BankFeedCategory>((Func<Category, BankFeedCategory>) (i => new BankFeedCategory()
    {
      Category = string.Join(":", i.Hierarchy)
    })).ToList<BankFeedCategory>();
  }

  public override async System.Threading.Tasks.Task ProcessConnectResponseAsync(
    string response,
    CABankFeed bankFeed)
  {
    PlaidBankFeedManager plaidBankFeedManager = this;
    int organizationId = bankFeed.OrganizationID.Value;
    ConnectResponse connectResponse = plaidBankFeedManager.ParseResponse<ConnectResponse>(response);
    bool isSandbox = bankFeed.IsTestFeed.IsTrue();
    ExchangeTokenResponse accessTokenInfo = await plaidBankFeedManager.ExchangeTokenAsync(connectResponse, organizationId, isSandbox);
    try
    {
      BankFeedFormResponse responseObjectAsync = await plaidBankFeedManager.GetBankFeedResponseObjectAsync(connectResponse, accessTokenInfo, organizationId, isSandbox);
      CABankFeedMaint bankFeedGraph = plaidBankFeedManager.GetBankFeedGraph(bankFeed);
      if (bankFeed.Status == "M")
        bankFeedGraph.MigrateBankFeed(responseObjectAsync);
      else
        bankFeedGraph.StoreBankFeed(responseObjectAsync);
    }
    catch (object ex)
    {
      await plaidBankFeedManager._client.DeleteItemAsync(organizationId, new DeleteItemRequest()
      {
        AccessToken = accessTokenInfo.AccessToken
      }, isSandbox, new CancellationToken());
      throw;
    }
    connectResponse = (ConnectResponse) null;
    accessTokenInfo = (ExchangeTokenResponse) null;
  }

  public override async Task<IEnumerable<BankFeedTransaction>> GetTransactionsAsync(
    LoadTransactionsData input,
    CABankFeed bankFeed)
  {
    IEnumerable<BankFeedTransaction> transactionsAsync;
    try
    {
      int offset = 0;
      int num1 = 0;
      int organizationId = bankFeed.OrganizationID.Value;
      string accessToken = bankFeed.AccessToken;
      string[] accountsId = input.AccountsID;
      this.CheckAccessToken(accessToken);
      int? transLimit = input.TransactionsLimit;
      List<BankFeedTransaction> transactions = new List<BankFeedTransaction>();
      GetTransactionsRequest tranRequest = new GetTransactionsRequest();
      Dictionary<string, int> tranCounts = ((IEnumerable<string>) accountsId).Distinct<string>().ToDictionary<string, string, int>((Func<string, string>) (e => e), (Func<string, int>) (e => 0));
      tranRequest.Options = new TransactionOptions()
      {
        Accounts = accountsId
      };
      tranRequest.EndDate = input.EndDate;
      tranRequest.StartDate = input.StartDate;
      tranRequest.AccessToken = accessToken;
      int? nullable;
      int num2;
      do
      {
        do
        {
          int num3 = 250;
          if (transLimit.HasValue)
          {
            if (num1 != 0)
            {
              int num4 = num1 - offset;
              nullable = transLimit;
              int valueOrDefault = nullable.GetValueOrDefault();
              if (num4 < valueOrDefault & nullable.HasValue)
              {
                num3 = num1 - offset;
                goto label_8;
              }
            }
            nullable = transLimit;
            int num5 = 250;
            if (nullable.GetValueOrDefault() < num5 & nullable.HasValue)
              num3 = transLimit.Value;
          }
label_8:
          tranRequest.Options.Count = num3;
          tranRequest.Options.Offset = offset;
          GetTransactionsCollectionResponse tranactionsAsync = await this._client.GetTranactionsAsync(organizationId, tranRequest, bankFeed.IsTestFeed.IsTrue(), new CancellationToken());
          offset += tranactionsAsync.Transactions.Count;
          transactions.AddRange((IEnumerable<BankFeedTransaction>) PlaidBankFeedManager._mapper.Map<List<BankFeedTransaction>>((object) tranactionsAsync.Transactions));
          num1 = tranactionsAsync.TotalTransactions;
          if (tranactionsAsync == null || num1 <= offset)
            goto label_12;
        }
        while (!transLimit.HasValue);
        nullable = transLimit;
        num2 = offset;
      }
      while (nullable.GetValueOrDefault() > num2 & nullable.HasValue);
label_12:
      foreach (BankFeedTransaction bankFeedTransaction in transactions)
        ++tranCounts[bankFeedTransaction.AccountID];
      foreach (KeyValuePair<string, int> keyValuePair in tranCounts)
        PXTrace.WriteInformation($"Number of fetched transactions for the {keyValuePair.Key} account from Plaid: {keyValuePair.Value}.");
      transactionsAsync = input.TransactionsOrder != LoadTransactionsData.Order.AscDate ? (input.TransactionsOrder != LoadTransactionsData.Order.CustomAccountAscDate ? (IEnumerable<BankFeedTransaction>) transactions : (IEnumerable<BankFeedTransaction>) transactions.OrderBy<BankFeedTransaction, int>((Func<BankFeedTransaction, int>) (x =>
      {
        int num6 = Array.IndexOf<string>(accountsId, x.AccountID);
        return num6 == -1 ? int.MaxValue : num6;
      })).ThenBy<BankFeedTransaction, DateTime?>((Func<BankFeedTransaction, DateTime?>) (x => x.Date))) : (IEnumerable<BankFeedTransaction>) transactions.OrderBy<BankFeedTransaction, DateTime?>((Func<BankFeedTransaction, DateTime?>) (x => x.Date));
    }
    catch (BankFeedException ex) when (ex.Reason == 1)
    {
      throw new BankFeedImportException(((Exception) ex).Message, ex);
    }
    return transactionsAsync;
  }

  public override async System.Threading.Tasks.Task DeleteAsync(CABankFeed bankFeed)
  {
    PlaidBankFeedManager plaidBankFeedManager = this;
    int num = bankFeed.OrganizationID.Value;
    string accessToken = bankFeed.AccessToken;
    plaidBankFeedManager.CheckAccessToken(accessToken);
    await plaidBankFeedManager._client.DeleteItemAsync(num, new DeleteItemRequest()
    {
      AccessToken = accessToken
    }, bankFeed.IsTestFeed.IsTrue(), new CancellationToken());
    plaidBankFeedManager.GetBankFeedGraph(bankFeed).DisconnectBankFeed();
  }

  public override async Task<IEnumerable<BankFeedAccount>> GetAccountsAsync(CABankFeed bankFeed)
  {
    int num1 = bankFeed.OrganizationID.Value;
    string accessToken = bankFeed.AccessToken;
    this.CheckAccessToken(accessToken);
    IBankFeedClient client = this._client;
    int num2 = num1;
    GetAccountsRequest getAccountsRequest = new GetAccountsRequest();
    getAccountsRequest.AccessToken = accessToken;
    int num3 = bankFeed.IsTestFeed.IsTrue() ? 1 : 0;
    CancellationToken cancellationToken = new CancellationToken();
    GetAccountsCollectionResponse accountsAsync = await client.GetAccountsAsync(num2, getAccountsRequest, num3 != 0, cancellationToken);
    List<BankFeedAccount> destination = new List<BankFeedAccount>();
    PlaidBankFeedManager._mapper.Map<List<Account>, List<BankFeedAccount>>(accountsAsync.Accounts, destination);
    return (IEnumerable<BankFeedAccount>) destination;
  }

  public override System.Threading.Tasks.Task ProcessUpdateResponseAsync(
    string responseStr,
    CABankFeed bankFeed)
  {
    UpdateResponse response = this.ParseResponse<UpdateResponse>(responseStr);
    if (!response.Updated)
      throw new PXException("Credentials were not updated. Error reason: {0}.", new object[1]
      {
        (object) response.ErrorReason
      });
    this.GetBankFeedGraph(bankFeed).ClearLoginFailedStatus();
    return System.Threading.Tasks.Task.CompletedTask;
  }

  public override LoadTransactionsData GetTransactionsFilterForTesting(DateTime loadingDate)
  {
    return new LoadTransactionsData()
    {
      StartDate = loadingDate,
      EndDate = loadingDate
    };
  }

  protected virtual async Task<ExchangeTokenResponse> ExchangeTokenAsync(
    ConnectResponse response,
    int organizationId,
    bool isSandbox)
  {
    string publicToken = response.PublicToken;
    if (string.IsNullOrEmpty(publicToken))
      throw new PXException("Cannot get the public token from Plaid.");
    ExchangeTokenResponse exchangeTokenResponse = await this._client.ExchangeTokenAsync(organizationId, new ExchangeTokenRequest()
    {
      PublicToken = publicToken
    }, isSandbox, new CancellationToken());
    return !string.IsNullOrEmpty(exchangeTokenResponse?.AccessToken) ? exchangeTokenResponse : throw new PXException("Cannot get the access token from Plaid.");
  }

  protected virtual async Task<BankFeedFormResponse> GetBankFeedResponseObjectAsync(
    ConnectResponse response,
    ExchangeTokenResponse accessTokenInfo,
    int organizationId,
    bool isSandbox)
  {
    List<string> list = response.Metadata.Accounts.Select<AccountMetadata, string>((Func<AccountMetadata, string>) (i => i.Id)).ToList<string>();
    GetAccountsCollectionResponse accountsAsync = await this._client.GetAccountsAsync(organizationId, new GetAccountsRequest()
    {
      Options = new AccountOptions() { Accounts = list },
      AccessToken = accessTokenInfo.AccessToken
    }, isSandbox, new CancellationToken());
    BankFeedFormResponse destination = new BankFeedFormResponse()
    {
      Accounts = (IEnumerable<BankFeedAccount>) new List<BankFeedAccount>()
    };
    PlaidBankFeedManager._mapper.Map<ExchangeTokenResponse, BankFeedFormResponse>(accessTokenInfo, destination);
    PlaidBankFeedManager._mapper.Map<ConnectResponse, BankFeedFormResponse>(response, destination);
    PlaidBankFeedManager._mapper.Map<List<Account>, IEnumerable<BankFeedAccount>>(accountsAsync.Accounts, destination.Accounts);
    return destination;
  }

  private void CheckAccessToken(string accessToken)
  {
    if (accessToken == null)
      throw new PXArgumentException(nameof (accessToken));
  }
}
