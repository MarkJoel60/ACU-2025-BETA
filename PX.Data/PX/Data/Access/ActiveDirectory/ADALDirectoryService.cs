// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.ADALDirectoryService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Data.Edm;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.ActiveDirectory;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Services.Client;
using System.Data.Services.Common;
using System.Globalization;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

[PXInternalUseOnly]
public class ADALDirectoryService : DataServiceContext
{
  private string _serviceUrl;
  private string _serviceVersion;
  private AuthenticationContext _context;
  private ClientCredential _credentials;
  protected ADALToken _adaltoken;
  private DataServiceQuery<DirectoryObject> _directoryObjects;

  public ADALDirectoryService(
    string serviceUrl,
    string serviceVersion,
    string path,
    AuthenticationContext context,
    ClientCredential creds)
    : this(new Uri(path))
  {
    this._context = context;
    this._credentials = creds;
    this._serviceUrl = serviceUrl;
    this._serviceVersion = serviceVersion;
    this.BuildingRequest += new EventHandler<BuildingRequestEventArgs>(this.OnBuildingRequest);
  }

  protected ADALDirectoryService(Uri serviceRoot)
    : base(serviceRoot, (DataServiceProtocolVersion) 2)
  {
    this.MergeOption = (MergeOption) 3;
    this.ResolveName = new Func<System.Type, string>(this.ResolveNameFromType);
    this.UrlConventions = DataServiceUrlConventions.KeyAsSegment;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.Format.LoadServiceModel = ADALDirectoryService.\u003C\u003EO.\u003C0\u003E__GetInstance ?? (ADALDirectoryService.\u003C\u003EO.\u003C0\u003E__GetInstance = new Func<IEdmModel>(GeneratedEdmModel.GetInstance));
  }

  protected void Init()
  {
    if (this._adaltoken != null && !this._adaltoken.WillExpireIn(10))
      return;
    AuthenticationResult authenticationResult = this._context.AcquireToken(this._serviceUrl, this._credentials);
    this._adaltoken = new ADALToken()
    {
      AdalToken = authenticationResult,
      AccessToken = authenticationResult.AccessToken,
      TokenType = authenticationResult.AccessTokenType,
      ExpiresOn = authenticationResult.ExpiresOn
    };
  }

  internal void OnBuildingRequest(object sender, BuildingRequestEventArgs args)
  {
    args.RequestUri = ADALDirectoryService.GetRequestUriWithDataContractVersion(args.RequestUri, this._serviceVersion);
    string str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}{1}{2}", (object) this._adaltoken.TokenType, (object) " ", (object) this._adaltoken.AccessToken);
    args.Headers.Add("Authorization", str);
  }

  protected string ResolveNameFromType(System.Type clientType) => clientType.FullName;

  protected static Uri GetRequestUriWithDataContractVersion(Uri origRequestUri, string apiVersion)
  {
    NameValueCollection queryString = HttpUtility.ParseQueryString(origRequestUri.Query);
    if (string.IsNullOrEmpty(queryString["api-version"]))
      queryString["api-version"] = apiVersion;
    return new UriBuilder(origRequestUri)
    {
      Query = queryString.ToString()
    }.Uri;
  }

  public DataServiceQuery<User> Users
  {
    get => Queryable.OfType<User>((IQueryable) this.DirectoryObjects) as DataServiceQuery<User>;
  }

  public DataServiceQuery<Role> Roles
  {
    get => Queryable.OfType<Role>((IQueryable) this.DirectoryObjects) as DataServiceQuery<Role>;
  }

  public DataServiceQuery<Group> Groups
  {
    get => Queryable.OfType<Group>((IQueryable) this.DirectoryObjects) as DataServiceQuery<Group>;
  }

  public DataServiceQuery<TenantDetail> Tenants
  {
    get
    {
      return Queryable.OfType<TenantDetail>((IQueryable) this.DirectoryObjects) as DataServiceQuery<TenantDetail>;
    }
  }

  public DataServiceQuery<DirectoryObject> DirectoryObjects
  {
    get
    {
      this.Init();
      if (this._directoryObjects == null)
        this._directoryObjects = this.CreateQuery<DirectoryObject>("directoryObjects");
      return this._directoryObjects;
    }
  }

  public IQueryable<T> TakeAll<T>(QueryOperationResponse<T> qor)
  {
    List<T> source = new List<T>();
    DataServiceQueryContinuation<T> queryContinuation = (DataServiceQueryContinuation<T>) null;
    QueryOperationResponse<T> operationResponse = qor;
    do
    {
      if (queryContinuation != null)
        operationResponse = this.Execute<T>(queryContinuation);
      foreach (T obj in operationResponse)
        source.Add(obj);
    }
    while ((queryContinuation = operationResponse.GetContinuation()) != null);
    return source.AsQueryable<T>();
  }
}
