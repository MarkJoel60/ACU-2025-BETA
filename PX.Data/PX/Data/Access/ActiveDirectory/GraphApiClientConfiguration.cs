// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.GraphApiClientConfiguration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Identity.Client;
using PX.Common;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

/// <summary>Prepared client configuration for MS Graph API</summary>
internal class GraphApiClientConfiguration
{
  /// <summary>Directory (tenant) ID.</summary>
  public string TenantID { get; }

  /// <summary>Application (client) ID.</summary>
  public string ClientID { get; }

  /// <summary>Client secret (password).</summary>
  public string ClientSecret { get; }

  /// <summary>
  /// Gets the optional Azure cloud specified in Active Directory settings.
  /// </summary>
  /// <value>
  /// The optional Azure cloud specified in Active Directory settings.
  /// </value>
  public AzureCloudInstance? AzureCloudFromCustomSettings { get; }

  /// <summary>
  /// Gets the Azure cloud deduced from the <see cref="P:PX.Data.Access.ActiveDirectory.GraphApiClientConfiguration.ApiEndpoint" /> endpoint.
  /// </summary>
  /// <value>
  /// The Azure cloud deduced from the <see cref="P:PX.Data.Access.ActiveDirectory.GraphApiClientConfiguration.ApiEndpoint" /> endpoint.
  /// </value>
  public AzureCloudInstance AzureCloudFromEndpoint { get; }

  /// <summary>
  /// The URL of the MS Graph API endpoint. If it wasn't specified in <see cref="P:PX.Data.Access.ActiveDirectory.Options.Path" /> then the public Azure cloud endpoint https://graph.microsoft.com will be used.
  /// </summary>
  public string ApiEndpoint { get; }

  /// <summary>
  /// Gets the optional custom Azure authority URL. If it wasn't specified in <see cref="P:PX.Data.Access.ActiveDirectory.Options.GraphApiAzureCloudOrUrl" /> then https://login.microsoftonline.com authority will be used.
  /// </summary>
  /// <value>The optional custom Azure authority URL.</value>
  public string AuthorityUrl { get; }

  /// <inheritdoc cref="P:PX.Data.Access.ActiveDirectory.Options.GraphApiScope" />
  public string Scope { get; }

  public GraphApiClientConfiguration(
    string clientID,
    string clientSecret,
    string tenantID,
    AzureCloudInstance? azureCloudFromCustomSettings,
    AzureCloudInstance azureCloudFromEndpoint,
    string apiEndpoint,
    string authorityUrl,
    string scope)
  {
    this.ClientID = ExceptionExtensions.CheckIfNullOrWhiteSpace(clientID, nameof (clientID), (string) null);
    this.ClientSecret = ExceptionExtensions.CheckIfNullOrWhiteSpace(clientSecret, nameof (clientSecret), (string) null);
    this.TenantID = ExceptionExtensions.CheckIfNullOrWhiteSpace(tenantID, nameof (tenantID), (string) null);
    this.AzureCloudFromCustomSettings = azureCloudFromCustomSettings;
    this.AzureCloudFromEndpoint = azureCloudFromEndpoint;
    this.ApiEndpoint = ExceptionExtensions.CheckIfNullOrWhiteSpace(apiEndpoint, nameof (apiEndpoint), (string) null);
    this.AuthorityUrl = authorityUrl;
    this.Scope = ExceptionExtensions.CheckIfNullOrWhiteSpace(scope, nameof (scope), (string) null);
  }
}
