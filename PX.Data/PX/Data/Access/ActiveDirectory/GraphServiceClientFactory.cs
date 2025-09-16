// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.GraphServiceClientFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Graph;
using Microsoft.Identity.Client;
using PX.Common;
using Serilog;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

internal class GraphServiceClientFactory : IGraphServiceClientFactory
{
  private readonly ILogger _logger;

  public GraphServiceClientFactory(ILogger logger) => this._logger = logger;

  /// <inheritdoc />
  public IGraphServiceClient CreateClient(GraphApiClientConfiguration configuration)
  {
    ExceptionExtensions.ThrowOnNull<GraphApiClientConfiguration>(configuration, nameof (configuration), (string) null);
    DelegateAuthenticationProvider authenticationProvider = this.CreateAuthenticationProvider(this.ConfigureAuthority(((AbstractApplicationBuilder<ConfidentialClientApplicationBuilder>) ConfidentialClientApplicationBuilder.Create(configuration.ClientID).WithClientSecret(configuration.ClientSecret)).WithTenantId(configuration.TenantID), configuration).Build(), configuration.Scope);
    return (IGraphServiceClient) new GraphServiceClient(configuration.ApiEndpoint, (IAuthenticationProvider) authenticationProvider, (IHttpProvider) null);
  }

  private ConfidentialClientApplicationBuilder ConfigureAuthority(
    ConfidentialClientApplicationBuilder clientApplicationBuilder,
    GraphApiClientConfiguration configuration)
  {
    if (configuration.AzureCloudFromCustomSettings.HasValue)
      return ((AbstractApplicationBuilder<ConfidentialClientApplicationBuilder>) clientApplicationBuilder).WithAuthority(configuration.AzureCloudFromCustomSettings.Value, configuration.TenantID, true);
    return !string.IsNullOrWhiteSpace(configuration.AuthorityUrl) ? ((AbstractApplicationBuilder<ConfidentialClientApplicationBuilder>) clientApplicationBuilder).WithAuthority(configuration.AuthorityUrl, configuration.TenantID, true) : ((AbstractApplicationBuilder<ConfidentialClientApplicationBuilder>) clientApplicationBuilder).WithAuthority(configuration.AzureCloudFromEndpoint, configuration.TenantID, true);
  }

  private DelegateAuthenticationProvider CreateAuthenticationProvider(
    IConfidentialClientApplication clientApplication,
    string scope)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    return new DelegateAuthenticationProvider(new AuthenticateRequestAsyncDelegate((object) new GraphServiceClientFactory.\u003C\u003Ec__DisplayClass4_0()
    {
      clientApplication = clientApplication,
      scope = scope,
      \u003C\u003E4__this = this
    }, __methodptr(\u003CCreateAuthenticationProvider\u003Eb__0)));
  }
}
