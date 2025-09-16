// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Consolidation.ConsolidationClient
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Newtonsoft.Json;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.GL.Consolidation;

internal class ConsolidationClient : IDisposable
{
  protected const string LOGIN_URL = "/entity/auth/login";
  protected const string LOGOUT_URL = "/entity/auth/logout";
  protected const string ENDPOINT_URL = "/entity/GLConsolidation/22.200.001/";
  protected const string JSON_CONTENT_TYPE = "application/json";
  protected const string BRANCH_URL = "/entity/GLConsolidation/22.200.001/Branch";
  protected const string ORGANIZATION_URL = "/entity/GLConsolidation/22.200.001/Organization";
  protected const string GLCONSOLACCOUNT_URL = "/entity/GLConsolidation/22.200.001/ConsolAccount";
  protected const string LEDGER_URL = "/entity/GLConsolidation/22.200.001/Ledger";
  protected const string CONSOL_DATA_URL = "/entity/GLConsolidation/22.200.001/ConsolidationData";
  protected bool _loggedIn;
  protected string _host;
  protected string _login;
  protected string _password;
  private static readonly HttpClient _client = new HttpClient();

  public ConsolidationClient(string url, string login, string password)
  {
    this._host = url;
    this._login = login;
    this._password = password;
    this.Login();
  }

  public ConsolidationClient(string url, string login, string password, int? timeout)
  {
    this._host = url;
    this._login = login;
    this._password = password;
    if (timeout.HasValue && ConsolidationClient._client.Timeout.TotalSeconds != (double) timeout.Value)
      ConsolidationClient._client.Timeout = TimeSpan.FromSeconds((double) timeout.Value);
    this.Login();
  }

  public void Dispose()
  {
    if (!this._loggedIn)
      return;
    this.Logout();
  }

  public void Relogin() => this.Login();

  protected void Login()
  {
    var data = new
    {
      name = this._login,
      password = this._password
    };
    string body = this.Serialize((object) data);
    try
    {
      System.Threading.Tasks.Task.Run<string>((Func<Task<string>>) (() => this.Post("/entity/auth/login", body))).Wait();
    }
    catch (Exception ex)
    {
      throw new PXException(ConsolidationClient.GetServerError(ex));
    }
    this._loggedIn = true;
  }

  protected void Logout()
  {
    try
    {
      System.Threading.Tasks.Task.Run<string>((Func<Task<string>>) (() => this.Post("/entity/auth/logout", string.Empty))).Wait();
    }
    catch (Exception ex)
    {
      throw new PXException(ConsolidationClient.GetServerError(ex));
    }
    this._loggedIn = false;
  }

  public virtual async Task<IEnumerable<ConsolAccountAPI>> GetConsolAccounts()
  {
    return ((IEnumerable<ConsolAccountAPITmp>) this.Deserialize<ConsolAccountAPITmp[]>(await this.Get("/entity/GLConsolidation/22.200.001/ConsolAccount"))).Select<ConsolAccountAPITmp, ConsolAccountAPI>((Func<ConsolAccountAPITmp, ConsolAccountAPI>) (_ => new ConsolAccountAPI()
    {
      AccountCD = _.AccountCD.value,
      Description = _.Description.value
    }));
  }

  public virtual async System.Threading.Tasks.Task UpdateConsolAccount(ConsolAccountAPI account)
  {
    string str = await this.Put("/entity/GLConsolidation/22.200.001/ConsolAccount", this.Serialize((object) new ConsolAccountAPITmp(account.AccountCD, account.Description)));
  }

  public virtual async System.Threading.Tasks.Task InsertConsolAccount(ConsolAccountAPI account)
  {
    string str = await this.Put("/entity/GLConsolidation/22.200.001/ConsolAccount", this.Serialize((object) new ConsolAccountAPITmp(account.AccountCD, account.Description)));
  }

  public virtual async System.Threading.Tasks.Task DeleteConsolAccount(ConsolAccountAPI account)
  {
    string str = await this.Delete("/entity/GLConsolidation/22.200.001/ConsolAccount/" + account.AccountCD);
  }

  public virtual async Task<IEnumerable<LedgerAPI>> GetLedgers()
  {
    return ((IEnumerable<LedgerAPITmp>) this.Deserialize<LedgerAPITmp[]>(await this.Get("/entity/GLConsolidation/22.200.001/Ledger"))).Select<LedgerAPITmp, LedgerAPI>((Func<LedgerAPITmp, LedgerAPI>) (_ => new LedgerAPI()
    {
      LedgerCD = _.LedgerCD.value,
      Descr = _.Descr.value,
      BalanceType = _.BalanceType.value.Substring(0, 1)
    }));
  }

  public virtual async Task<IEnumerable<BranchAPI>> GetBranches()
  {
    return ((IEnumerable<BranchAPITmp>) this.Deserialize<BranchAPITmp[]>(await this.Get("/entity/GLConsolidation/22.200.001/Branch"))).Select<BranchAPITmp, BranchAPI>((Func<BranchAPITmp, BranchAPI>) (_ => new BranchAPI()
    {
      BranchCD = _.BranchCD.value,
      OrganizationCD = _.OrganizationCD.value,
      AcctName = _.AcctName.value,
      LedgerCD = _.LedgerCD.value
    }));
  }

  public virtual async Task<IEnumerable<OrganizationAPI>> GetOrganizations()
  {
    return ((IEnumerable<OrganizationAPITmp>) this.Deserialize<OrganizationAPITmp[]>(await this.Get("/entity/GLConsolidation/22.200.001/Organization"))).Select<OrganizationAPITmp, OrganizationAPI>((Func<OrganizationAPITmp, OrganizationAPI>) (_ => new OrganizationAPI()
    {
      OrganizationCD = _.OrganizationCD.value,
      OrganizationName = _.OrganizationName.value,
      LedgerCD = _.LedgerCD.value
    }));
  }

  public virtual async Task<IEnumerable<ConsolidationItemAPI>> GetConsolidationData(
    string ledgerCD,
    string branchCD)
  {
    return (IEnumerable<ConsolidationItemAPI>) ((IEnumerable<ConsolidationItemAPITmp>) this.Deserialize<ConsolidationDataAPITmp>(await this.Put("/entity/GLConsolidation/22.200.001/ConsolidationData?$expand=Result", this.Serialize((object) new ConsolidationDataParametersAPITmp(ledgerCD, branchCD)))).Result).Select<ConsolidationItemAPITmp, ConsolidationItemAPI>((Func<ConsolidationItemAPITmp, ConsolidationItemAPI>) (_ => _.ToApiItem())).ToList<ConsolidationItemAPI>();
  }

  protected virtual async Task<string> Get(string uri)
  {
    HttpResponseMessage response = await ConsolidationClient._client.GetAsync(this._host + uri);
    string content = await response.Content.ReadAsStringAsync();
    string result = this.GetResult(response, content);
    response = (HttpResponseMessage) null;
    return result;
  }

  protected virtual async Task<string> Put(string uri, string body)
  {
    HttpResponseMessage response = await ConsolidationClient._client.PutAsync(this._host + uri, (HttpContent) new StringContent(body, Encoding.UTF8, "application/json"));
    string content = await response.Content.ReadAsStringAsync();
    string result = this.GetResult(response, content);
    response = (HttpResponseMessage) null;
    return result;
  }

  protected virtual async Task<string> Post(string uri, string body)
  {
    HttpResponseMessage response = await ConsolidationClient._client.PostAsync(this._host + uri, (HttpContent) new StringContent(body, Encoding.UTF8, "application/json"));
    string content = await response.Content.ReadAsStringAsync();
    string result = this.GetResult(response, content);
    response = (HttpResponseMessage) null;
    return result;
  }

  protected virtual async Task<string> Delete(string uri)
  {
    HttpResponseMessage response = await ConsolidationClient._client.DeleteAsync(this._host + uri);
    string content = await response.Content.ReadAsStringAsync();
    string result = this.GetResult(response, content);
    response = (HttpResponseMessage) null;
    return result;
  }

  protected string GetResult(HttpResponseMessage response, string content)
  {
    if (response.IsSuccessStatusCode)
      return content;
    if (response.StatusCode == HttpStatusCode.InternalServerError || response.StatusCode == (HttpStatusCode) 422)
      throw new ApiException(this.GetInternalError(content));
    throw new ApiException(PXMessages.LocalizeFormatNoPrefix("HTTP Status Code = {0}", new object[1]
    {
      (object) response.StatusCode
    }));
  }

  protected string GetInternalError(string errorContent)
  {
    string str = (string) null;
    BranchLedgerApiExceptionAPI ledgerApiExceptionApi = this.Deserialize<BranchLedgerApiExceptionAPI>(errorContent);
    if (ledgerApiExceptionApi.BranchCD?.error != null)
      str = ledgerApiExceptionApi.BranchCD?.error;
    if (ledgerApiExceptionApi.LedgerCD?.error != null)
      str = (str == null ? "" : str + " ") + ledgerApiExceptionApi.BranchCD?.error;
    return str ?? this.Deserialize<CommonApiExceptionAPI>(errorContent).exceptionMessage;
  }

  internal static string GetServerError(Exception ex)
  {
    Exception exception = ex;
    while (!(exception is ApiException) && exception.InnerException != null)
      exception = exception.InnerException;
    return exception is ApiException ? exception.Message : ex.Message;
  }

  protected string Serialize(object obj) => JsonConvert.SerializeObject(obj);

  protected T Deserialize<T>(string body) => JsonConvert.DeserializeObject<T>(body);
}
