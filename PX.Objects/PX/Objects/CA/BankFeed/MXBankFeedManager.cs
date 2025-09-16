// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.MXBankFeedManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using AutoMapper;
using PX.BankFeed.MX;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal class MXBankFeedManager : BankFeedManager
{
  private const string widgetType = "connect_widget";
  private static readonly (string, string)[] availableCorpCardFilters = new (string, string)[5]
  {
    ("N", " "),
    ("P", "Partner Account ID"),
    ("C", "Check Number"),
    ("M", "Memo"),
    ("A", "Name")
  };
  private static readonly string[] availableTransactionFields = new string[12]
  {
    "TransactionID",
    "Date",
    "Amount",
    "IsoCurrencyCode",
    "Name",
    "Category",
    "Pending",
    "Type",
    "AccountID",
    "CheckNumber",
    "Memo",
    "PartnerAccountId"
  };
  private static readonly IMapper _mapper = new MapperConfiguration((Action<IMapperConfigurationExpression>) (c => c.AddProfile(typeof (MXMapperProfile)))).CreateMapper();
  private static readonly Dictionary<string, MXBankFeedManager.ErrorStatusMessage> errorStatusMessageDict = new Dictionary<string, MXBankFeedManager.ErrorStatusMessage>()
  {
    {
      "PREVENTED",
      new MXBankFeedManager.ErrorStatusMessage("The last 3 attempts to connect have failed. Please re-enter your credentials to continue importing data.", false)
    },
    {
      "DENIED",
      new MXBankFeedManager.ErrorStatusMessage("The credentials entered do not match your credentials at this institution. Please re-enter your credentials to continue importing data.", false)
    },
    {
      "CHALLENGED",
      new MXBankFeedManager.ErrorStatusMessage("To authenticate your connection to the {0} bank, answer the following challenges.", true)
    },
    {
      "REJECTED",
      new MXBankFeedManager.ErrorStatusMessage("The answer or answers provided were incorrect. Please try again.", false)
    },
    {
      "LOCKED",
      new MXBankFeedManager.ErrorStatusMessage("Your account is locked. Log in to the appropriate website for the {0} bank and follow the steps to resolve the issue.", true)
    },
    {
      "IMPEDED",
      new MXBankFeedManager.ErrorStatusMessage("Your attention is needed at this institution's website. Log in to the appropriate website for the {0} bank and follow the steps to resolve the issue.", true)
    },
    {
      "DEGRADED",
      new MXBankFeedManager.ErrorStatusMessage("We are upgrading this connection. Please try again later.", false)
    },
    {
      "DISCONNECTED",
      new MXBankFeedManager.ErrorStatusMessage("It looks like your data from the {0} bank cannot be imported. We are working to resolve the issue.", true)
    },
    {
      "DISCONTINUED",
      new MXBankFeedManager.ErrorStatusMessage("Connections to this institution are no longer supported.", false)
    },
    {
      "CLOSED",
      new MXBankFeedManager.ErrorStatusMessage("This connection has been closed. Please contact your Acumatica support provider.", false)
    },
    {
      "DELAYED",
      new MXBankFeedManager.ErrorStatusMessage("Importing your data from the {0} bank may take a while. Please run the import process later.", false)
    },
    {
      "FAILED",
      new MXBankFeedManager.ErrorStatusMessage("There was a problem validating your credentials with the {0} bank. Please try again later.", true)
    },
    {
      "DISABLED",
      new MXBankFeedManager.ErrorStatusMessage("Importing data from this institution has been disabled.", false)
    },
    {
      "IMPORTED",
      new MXBankFeedManager.ErrorStatusMessage("You must reauthenticate before your data can be imported. Enter your credentials for the {0} bank.", true)
    },
    {
      "EXPIRED",
      new MXBankFeedManager.ErrorStatusMessage("The answer or answers were not provided in time. Please try again.", false)
    },
    {
      "IMPAIRED",
      new MXBankFeedManager.ErrorStatusMessage("You must reauthenticate before your data can be imported. Enter your credentials for the {0} bank.", true)
    }
  };
  private readonly IBankFeedClient _client;
  private readonly BankFeedUserDataProvider _userDataProvider;

  public override (string, string)[] AvailableCorpCardFilters
  {
    get => MXBankFeedManager.availableCorpCardFilters;
  }

  public override string[] AvailableTransactionFields
  {
    get => MXBankFeedManager.availableTransactionFields;
  }

  public override bool AllowBatchDownloading => false;

  public override int NumberOfAccountsForBatchDownloading => 0;

  public MXBankFeedManager(IBankFeedClient client, BankFeedUserDataProvider userDataProvider)
  {
    this._client = client;
    this._userDataProvider = userDataProvider;
  }

  public override async System.Threading.Tasks.Task ConnectAsync(CABankFeed bankFeed)
  {
    MXBankFeedManager mxBankFeedManager = this;
    int organizationId = bankFeed.OrganizationID.Value;
    string externalUser = bankFeed.ExternalUserID;
    if (externalUser == null)
    {
      CABankFeedMaint graph = mxBankFeedManager.GetBankFeedGraph(bankFeed);
      externalUser = mxBankFeedManager._userDataProvider.GetUserForOrganization(organizationId);
      if (externalUser == null)
      {
        externalUser = (await mxBankFeedManager._client.CreateUserAsync(organizationId, new CancellationToken())).User.Guid;
        graph.CreateUserRecord(externalUser, organizationId);
      }
      graph.StoreBankFeedUser(bankFeed, externalUser);
      graph = (CABankFeedMaint) null;
    }
    GetFormTokenRequest formTokenRequest = new GetFormTokenRequest()
    {
      WidgetUrl = new WidgetUrl()
      {
        WidgetType = "connect_widget"
      }
    };
    string url = (await mxBankFeedManager._client.GetFormTokenAsync(organizationId, externalUser, formTokenRequest, new CancellationToken())).WidgetUrl.Url;
    throw new PXPluginRedirectException<PXPluginRedirectOptions>((PXPluginRedirectOptions) new MXFormOptions()
    {
      Url = url,
      Mode = "Connect"
    });
  }

  protected virtual async Task<BankFeedFormResponse> GetBankFeedResponseObjectAsync(
    int organizationId,
    ConnectResponse connectResponse)
  {
    MemberResponse memberInfo = await this._client.GetMemberAsync(organizationId, connectResponse.UserGuid, connectResponse.MemberGuid, new CancellationToken());
    IEnumerable<BankFeedAccount> accountsAsync = await this.GetAccountsAsync(organizationId, connectResponse.MemberGuid, connectResponse.UserGuid);
    BankFeedFormResponse destination = new BankFeedFormResponse()
    {
      Accounts = accountsAsync
    };
    MXBankFeedManager._mapper.Map<ConnectResponse, BankFeedFormResponse>(connectResponse, destination);
    MXBankFeedManager._mapper.Map<MemberResponse, BankFeedFormResponse>(memberInfo, destination);
    BankFeedFormResponse responseObjectAsync = destination;
    memberInfo = (MemberResponse) null;
    return responseObjectAsync;
  }

  public override async System.Threading.Tasks.Task ProcessConnectResponseAsync(
    string response,
    CABankFeed bankFeed)
  {
    MXBankFeedManager mxBankFeedManager = this;
    ConnectResponse connectResponse = mxBankFeedManager.ParseResponse<ConnectResponse>(response);
    int organizationId = bankFeed.OrganizationID.Value;
    CABankFeedMaint graph = mxBankFeedManager.GetBankFeedGraph(bankFeed);
    CABankFeed storedBankFeedByIds = graph.GetStoredBankFeedByIds(connectResponse.UserGuid, connectResponse.MemberGuid);
    if (storedBankFeedByIds != null)
      throw new PXException("The bank feed to connect to the {0} financial institution with the same credentials already exists ({1}).", new object[2]
      {
        (object) storedBankFeedByIds.Institution,
        (object) storedBankFeedByIds.BankFeedID
      });
    try
    {
      BankFeedFormResponse responseObjectAsync = await mxBankFeedManager.GetBankFeedResponseObjectAsync(organizationId, connectResponse);
      if (bankFeed.Status != "M")
        graph.StoreBankFeed(responseObjectAsync);
      else
        graph.MigrateBankFeed(responseObjectAsync);
    }
    catch (object ex)
    {
      await mxBankFeedManager._client.DeleteMemberAsync(organizationId, connectResponse.UserGuid, connectResponse.MemberGuid, new CancellationToken());
      if (bankFeed.Status != "M")
        graph.DisconnectBankFeed();
      throw;
    }
    connectResponse = (ConnectResponse) null;
    graph = (CABankFeedMaint) null;
  }

  public override async System.Threading.Tasks.Task DeleteAsync(CABankFeed bankFeed)
  {
    MXBankFeedManager mxBankFeedManager = this;
    int num = bankFeed.OrganizationID.Value;
    string externalUserId = bankFeed.ExternalUserID;
    string externalItemId = bankFeed.ExternalItemID;
    mxBankFeedManager.CheckExternalUserId(externalUserId);
    mxBankFeedManager.CheckExternalItemId(externalItemId);
    await mxBankFeedManager._client.DeleteMemberAsync(num, externalUserId, externalItemId, new CancellationToken());
    mxBankFeedManager.GetBankFeedGraph(bankFeed).DisconnectBankFeed();
  }

  public override async Task<IEnumerable<BankFeedCategory>> GetCategoriesAsync(CABankFeed bankFeed)
  {
    int organizationId = bankFeed.OrganizationID.Value;
    List<BankFeedCategory> ret = new List<BankFeedCategory>();
    Filter filter = new Filter() { Page = 1, Count = 200 };
    Stack<MXBankFeedManager.CategoryChain> chainStack = new Stack<MXBankFeedManager.CategoryChain>();
    int totalPages;
    do
    {
      GetCategoriesCollectionResponse categoriesAsync = await this._client.GetCategoriesAsync(organizationId, filter, new CancellationToken());
      totalPages = categoriesAsync.Pagination.TotalPages;
      ret.AddRange(this.BoundCategories(categoriesAsync, chainStack));
      ++filter.Page;
    }
    while (filter.Page <= totalPages);
    IEnumerable<BankFeedCategory> categoriesAsync1 = (IEnumerable<BankFeedCategory>) ret;
    ret = (List<BankFeedCategory>) null;
    filter = (Filter) null;
    chainStack = (Stack<MXBankFeedManager.CategoryChain>) null;
    return categoriesAsync1;
  }

  public override Task<IEnumerable<BankFeedAccount>> GetAccountsAsync(CABankFeed bankFeed)
  {
    return this.GetAccountsAsync(bankFeed.OrganizationID.Value, bankFeed.ExternalItemID, bankFeed.ExternalUserID);
  }

  public override async Task<IEnumerable<BankFeedTransaction>> GetTransactionsAsync(
    LoadTransactionsData input,
    CABankFeed bankFeed)
  {
    int totalTrans = 0;
    string accountId = input.AccountsID[0];
    TransactionsFilter filter = new TransactionsFilter();
    filter.StartDate = input.StartDate;
    filter.EndDate = input.EndDate;
    filter.AccountId = accountId;
    ((Filter) filter).Count = 250;
    ((Filter) filter).Page = 1;
    int? transLimit = input.TransactionsLimit;
    int? nullable = bankFeed.OrganizationID;
    int organizationId = nullable.Value;
    List<BankFeedTransaction> transactions = new List<BankFeedTransaction>();
    string extUserId = bankFeed.ExternalUserID;
    await this.CheckMemberStatus(bankFeed);
    int num1;
    int valueOrDefault1;
    do
    {
      do
      {
        GetTransactionsCollectionResponse tranactionsAsync = await this._client.GetTranactionsAsync(organizationId, extUserId, filter, new CancellationToken());
        int count1 = tranactionsAsync.Transactions.Count;
        int totalPages = tranactionsAsync.Pagination.TotalPages;
        TransactionsFilter transactionsFilter = filter;
        ((Filter) transactionsFilter).Page = ((Filter) transactionsFilter).Page + 1;
        if (transLimit.HasValue)
        {
          int num2 = totalTrans + count1;
          nullable = transLimit;
          int valueOrDefault2 = nullable.GetValueOrDefault();
          if (num2 > valueOrDefault2 & nullable.HasValue)
          {
            int num3 = transLimit.Value;
            int num4 = count1;
            nullable = transLimit;
            int valueOrDefault3 = nullable.GetValueOrDefault();
            int count2 = num4 > valueOrDefault3 & nullable.HasValue ? num3 : num3 - count1;
            transactions.AddRange(MXBankFeedManager._mapper.Map<List<BankFeedTransaction>>((object) tranactionsAsync.Transactions).Take<BankFeedTransaction>(count2));
            goto label_7;
          }
        }
        transactions.AddRange((IEnumerable<BankFeedTransaction>) MXBankFeedManager._mapper.Map<List<BankFeedTransaction>>((object) tranactionsAsync.Transactions));
label_7:
        totalTrans += count1;
        if (((Filter) filter).Page > totalPages)
          goto label_10;
      }
      while (!transLimit.HasValue);
      num1 = totalTrans;
      nullable = transLimit;
      valueOrDefault1 = nullable.GetValueOrDefault();
    }
    while (num1 < valueOrDefault1 & nullable.HasValue);
label_10:
    PXTrace.WriteInformation($"Number of fetched transactions for the {accountId} account from MX: {transactions.Count}.");
    IEnumerable<BankFeedTransaction> transactionsAsync = input.TransactionsOrder != LoadTransactionsData.Order.AscDate ? (IEnumerable<BankFeedTransaction>) transactions : (IEnumerable<BankFeedTransaction>) transactions.OrderBy<BankFeedTransaction, DateTime?>((Func<BankFeedTransaction, DateTime?>) (x => x.Date));
    accountId = (string) null;
    filter = (TransactionsFilter) null;
    transactions = (List<BankFeedTransaction>) null;
    extUserId = (string) null;
    return transactionsAsync;
  }

  public override async System.Threading.Tasks.Task UpdateAsync(CABankFeed bankFeed)
  {
    MXBankFeedManager mxBankFeedManager = this;
    int organizationId = bankFeed.OrganizationID.Value;
    string extUserId = bankFeed.ExternalUserID;
    if ((await mxBankFeedManager._client.GetMemberStatusAsync(organizationId, extUserId, bankFeed.ExternalItemID, new CancellationToken())).Member.ConnectionStatus == "CONNECTED")
    {
      IEnumerable<BankFeedAccount> accountsAsync = await mxBankFeedManager.GetAccountsAsync(bankFeed);
      mxBankFeedManager.GetBankFeedGraph(bankFeed).UpdateAccounts(accountsAsync);
    }
    GetFormTokenRequest formTokenRequest = new GetFormTokenRequest()
    {
      WidgetUrl = new WidgetUrl()
      {
        CurrentMemberGuid = bankFeed.ExternalItemID,
        WidgetType = "connect_widget",
        DisableInstitutionSearch = true
      }
    };
    GetFormTokenResponse formTokenAsync = await mxBankFeedManager._client.GetFormTokenAsync(organizationId, extUserId, formTokenRequest, new CancellationToken());
    throw new PXPluginRedirectException<PXPluginRedirectOptions>((PXPluginRedirectOptions) new MXFormOptions()
    {
      Url = formTokenAsync.WidgetUrl.Url,
      Mode = "Update"
    });
  }

  public override async System.Threading.Tasks.Task ProcessUpdateResponseAsync(
    string responseStr,
    CABankFeed bankFeed)
  {
    MXBankFeedManager mxBankFeedManager = this;
    UpdateResponse response = mxBankFeedManager.ParseResponse<UpdateResponse>(responseStr);
    if (!response.Updated)
      throw new PXException("Credentials were not updated. Error reason: {0}.", new object[1]
      {
        (object) response.ErrorReason
      });
    IEnumerable<BankFeedAccount> accountsAsync = await mxBankFeedManager.GetAccountsAsync(bankFeed);
    CABankFeedMaint bankFeedGraph = mxBankFeedManager.GetBankFeedGraph(bankFeed);
    bankFeedGraph.UpdateAccounts(accountsAsync);
    bankFeedGraph.ClearLoginFailedStatus();
  }

  public override LoadTransactionsData GetTransactionsFilterForTesting(DateTime loadingDate)
  {
    return new LoadTransactionsData()
    {
      StartDate = loadingDate,
      EndDate = loadingDate.AddDays(1.0)
    };
  }

  protected virtual async Task<IEnumerable<BankFeedAccount>> GetAccountsAsync(
    int organizationId,
    string itemId,
    string userId)
  {
    this.CheckExternalUserId(userId);
    this.CheckExternalItemId(itemId);
    Filter filter = new Filter() { Page = 1, Count = 200 };
    List<BankFeedAccount> ret = new List<BankFeedAccount>();
    int totalPages;
    do
    {
      GetAccountsCollectionResponse accountsAsync = await this._client.GetAccountsAsync(organizationId, userId, filter, new CancellationToken());
      totalPages = accountsAsync.Pagination.TotalPages;
      ret.AddRange(MXBankFeedManager._mapper.Map<IEnumerable<BankFeedAccount>>((object) accountsAsync.Accounts.Where<Account>((Func<Account, bool>) (i => i.MemberGuid == itemId && !i.IsClosed))));
      ++filter.Page;
    }
    while (filter.Page <= totalPages);
    IEnumerable<BankFeedAccount> accountsAsync1 = (IEnumerable<BankFeedAccount>) ret;
    filter = (Filter) null;
    ret = (List<BankFeedAccount>) null;
    return accountsAsync1;
  }

  protected virtual async System.Threading.Tasks.Task CheckMemberStatus(CABankFeed bankFeed)
  {
    int num = bankFeed.OrganizationID.Value;
    string externalUserId = bankFeed.ExternalUserID;
    this.CheckExternalUserId(externalUserId);
    Member member = (await this._client.GetMemberAsync(num, externalUserId, bankFeed.ExternalItemID, new CancellationToken())).Member;
    DateTime? successfullyAggregatedAt = member.SuccessfullyAggregatedAt;
    if (successfullyAggregatedAt.HasValue)
      PXTrace.WriteInformation($"Last successful update of transactions in MX: {successfullyAggregatedAt.Value.ToString("u")}.");
    if (member.ConnectionStatus != "CONNECTED")
    {
      PXException pxException;
      if (MXBankFeedManager.errorStatusMessageDict.ContainsKey(member.ConnectionStatus))
      {
        MXBankFeedManager.ErrorStatusMessage errorStatusMessage = MXBankFeedManager.errorStatusMessageDict[member.ConnectionStatus];
        if (errorStatusMessage.IncludeInstitution)
          pxException = (PXException) new BankFeedImportException(PXMessages.LocalizeFormatNoPrefix(errorStatusMessage.Message, new object[1]
          {
            (object) member.Name
          }), BankFeedImportException.ExceptionReason.LoginFailed);
        else
          pxException = (PXException) new BankFeedImportException(errorStatusMessage.Message, BankFeedImportException.ExceptionReason.LoginFailed);
      }
      else
        pxException = (PXException) new BankFeedImportException(PXMessages.LocalizeFormatNoPrefix("MX connection has the {0} status.", new object[1]
        {
          (object) member.ConnectionStatus
        }), BankFeedImportException.ExceptionReason.LoginFailed);
      throw pxException;
    }
  }

  private IEnumerable<BankFeedCategory> BoundCategories(
    GetCategoriesCollectionResponse response,
    Stack<MXBankFeedManager.CategoryChain> chainStack)
  {
    foreach (Category category in response.Categories)
    {
      string str;
      if (category.ParentGuid == null)
      {
        str = category.Name;
        chainStack.Clear();
      }
      else
      {
        while (chainStack.Count<MXBankFeedManager.CategoryChain>() > 0 && chainStack.Peek().Guid != category.ParentGuid)
          chainStack.Pop();
        if (chainStack.Count<MXBankFeedManager.CategoryChain>() != 0)
          str = $"{chainStack.Peek().Category}:{category.Name}";
        else
          continue;
      }
      BankFeedCategory bankFeedCategory = new BankFeedCategory()
      {
        Category = str
      };
      chainStack.Push(new MXBankFeedManager.CategoryChain()
      {
        Category = str,
        Guid = category.Guid
      });
      yield return bankFeedCategory;
    }
  }

  private void CheckExternalUserId(string externalUserId)
  {
    if (externalUserId == null)
      throw new PXArgumentException(nameof (externalUserId));
  }

  private void CheckExternalItemId(string externalItemId)
  {
    if (externalItemId == null)
      throw new PXArgumentException(nameof (externalItemId));
  }

  internal class ErrorStatusMessage
  {
    public string Message { get; private set; }

    public bool IncludeInstitution { get; private set; }

    public ErrorStatusMessage(string message, bool includeInstitution)
    {
      this.Message = message;
      this.IncludeInstitution = includeInstitution;
    }
  }

  internal class CategoryChain
  {
    public string Guid { get; set; }

    public string Category { get; set; }
  }
}
