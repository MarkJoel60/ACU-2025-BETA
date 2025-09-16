// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.GraphApiClientConfigurationProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Identity.Client;
using PX.Common;
using System;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

/// <inheritdoc />
internal class GraphApiClientConfigurationProvider : IGraphApiClientConfigurationProvider
{
  private const AzureCloudInstance DefaultAzureCloud = (AzureCloudInstance) 1;
  private const string ApiVersion1 = "v1.0";
  private const string DefaultScope = ".default";
  private const string PublicAzureCloudEndpoint = "https://graph.microsoft.com";
  private const string UsGovAzureCloudGraphEndpoint = "https://graph.microsoft.us";
  private const string AzureChinaCloudGraphEndpoint = "https://microsoftgraph.chinacloudapi.cn";

  /// <inheritdoc />
  public GraphApiClientConfiguration CreateConfiguration(Options options)
  {
    ExceptionExtensions.ThrowOnNull<Options>(options, nameof (options), (string) null);
    (string apiEndpoint1, string str) = GraphApiClientConfigurationProvider.ParsePath(options.Path);
    (AzureCloudInstance? nullable1, bool flag) = this.ParseGraphApiAzureCloudOrUrl(options.GraphApiAzureCloudOrUrl);
    AzureCloudInstance? nullable2;
    AzureCloudInstance azureCloudInstance1;
    string apiEndpointWithoutVersion;
    string apiEndpoint2;
    if (string.IsNullOrWhiteSpace(apiEndpoint1))
    {
      nullable2 = nullable1;
      azureCloudInstance1 = (AzureCloudInstance) ((object) nullable2 ?? (object) 1);
      apiEndpointWithoutVersion = this.GetApiEndpointWithoutVersion(azureCloudInstance1);
      apiEndpoint2 = apiEndpointWithoutVersion + "/v1.0";
    }
    else
    {
      apiEndpoint2 = apiEndpoint1.TrimEnd('/');
      if (this.EndpointHasApiVersion(apiEndpoint2))
      {
        apiEndpointWithoutVersion = this.GetApiEndpointWithoutVersion(apiEndpoint2);
      }
      else
      {
        apiEndpointWithoutVersion = apiEndpoint2;
        if (!flag)
          apiEndpoint2 += "/v1.0";
      }
      azureCloudInstance1 = this.GetAzureCloudInstanceFromApiEndpoint(apiEndpointWithoutVersion, flag);
    }
    if (nullable1.HasValue)
    {
      nullable2 = nullable1;
      AzureCloudInstance azureCloudInstance2 = azureCloudInstance1;
      if (!(nullable2.GetValueOrDefault() == azureCloudInstance2 & nullable2.HasValue))
        throw new InvalidOperationException($"The Active Directory option \"GraphApiAzureCloudOrUrl\" and the endpoint \"{apiEndpoint2}\" are incompatible");
    }
    string apiAzureCloudOrUrl = flag ? options.GraphApiAzureCloudOrUrl : (string) null;
    string scope = Str.NullIfWhitespace(options.GraphApiScope) ?? this.GetScopeFromEndpoint(apiEndpointWithoutVersion);
    return new GraphApiClientConfiguration(options.User, options.Password, str, nullable1, azureCloudInstance1, apiEndpoint2, apiAzureCloudOrUrl, scope);
  }

  private static (string apiEndpoint, string tenantId) ParsePath(string path)
  {
    if (!path.StartsWith("http", StringComparison.OrdinalIgnoreCase))
      return ((string) null, path);
    path = path.TrimEnd('/');
    int length = path.LastIndexOf('/');
    return length <= 0 ? ((string) null, path) : (path.Substring(0, length), path.Substring(length + 1));
  }

  private (AzureCloudInstance? AzureCloudFromCustomSettings, bool IsCustomAuthorityUrl) ParseGraphApiAzureCloudOrUrl(
    string graphApiAzureCloudOrUrl)
  {
    if (string.IsNullOrWhiteSpace(graphApiAzureCloudOrUrl))
      return (new AzureCloudInstance?(), false);
    AzureCloudInstance result;
    AzureCloudInstance? nullable = !Enum.TryParse<AzureCloudInstance>(graphApiAzureCloudOrUrl, true, out result) || result == null ? new AzureCloudInstance?() : new AzureCloudInstance?(result);
    bool flag = !nullable.HasValue && Uri.IsWellFormedUriString(graphApiAzureCloudOrUrl, UriKind.Absolute);
    if (!nullable.HasValue && !flag)
      throw new NotSupportedException($"The value \"{graphApiAzureCloudOrUrl}\" of the Active Directory \"GraphApiAzureCloudOrUrl\" setting is not supported.");
    return (nullable, flag);
  }

  private string GetApiEndpointWithoutVersion(AzureCloudInstance azureCloudInstance)
  {
    switch ((int) azureCloudInstance)
    {
      case 0:
      case 1:
        return "https://graph.microsoft.com";
      case 2:
        return "https://microsoftgraph.chinacloudapi.cn";
      case 3:
        throw new NotSupportedException("Azure Germany cloud is closed by Microsoft. See this link for details: https://learn.microsoft.com/en-us/previous-versions/azure/germany/germany-welcome");
      case 4:
        return "https://graph.microsoft.us";
      default:
        throw new NotSupportedException($"The Azure clound instance value \"{azureCloudInstance}\" is not supported");
    }
  }

  private bool EndpointHasApiVersion(string apiEndpoint)
  {
    return apiEndpoint.EndsWith("/v1.0", StringComparison.OrdinalIgnoreCase);
  }

  private string GetApiEndpointWithoutVersion(string apiEndpoint)
  {
    int length = apiEndpoint.LastIndexOf('/');
    return length < 0 ? apiEndpoint : apiEndpoint.Substring(0, length);
  }

  private AzureCloudInstance GetAzureCloudInstanceFromApiEndpoint(
    string apiEndpointWithoutVersion,
    bool isCustomAuthorityUrl)
  {
    switch (apiEndpointWithoutVersion)
    {
      case "https://graph.microsoft.com":
        return (AzureCloudInstance) 1;
      case "https://graph.microsoft.us":
        return (AzureCloudInstance) 4;
      case "https://microsoftgraph.chinacloudapi.cn":
        return (AzureCloudInstance) 2;
      default:
        if (isCustomAuthorityUrl)
          return (AzureCloudInstance) 0;
        throw new NotSupportedException($"The Active Directory endpoint \"{apiEndpointWithoutVersion}\" is not supported");
    }
  }

  /// <summary>
  /// Gets proper scope from endpoint without version. See https://learn.microsoft.com/en-us/graph/deployments#app-registration-and-token-service-root-endpoints.
  /// </summary>
  /// <param name="apiEndpointWithoutVersion">The Graph API endpoint without version.</param>
  /// <returns>The scope from endpoint.</returns>
  private string GetScopeFromEndpoint(string apiEndpointWithoutVersion)
  {
    return apiEndpointWithoutVersion + "/.default";
  }
}
