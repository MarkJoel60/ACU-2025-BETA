// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.Options
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using PX.Common;

#nullable enable
namespace PX.Data.Access.ActiveDirectory;

[PXInternalUseOnly]
public class Options
{
  internal bool Enabled { get; set; }

  internal ActiveDirectoryProtocol Protocol { get; set; }

  internal 
  #nullable disable
  string Path { get; set; }

  /// <summary>
  /// The optional URL or the authority cloud type. If not specified then https://login.microsoftonline.com authority will be used.
  /// The allowed values can can be divided into two kinds:
  /// <list type="bullet">
  /// <item>An URL with the path to the authority host like https://login.microsoftonline.us</item>
  /// <item>A valid string enum value from the <see cref="T:Microsoft.Identity.Client.AzureCloudInstance" /> enum.</item>
  /// </list>
  /// </summary>
  /// <value>The graph API cloud instance or URL.</value>
  internal string GraphApiAzureCloudOrUrl { get; set; }

  /// <summary>
  /// The optional scope passed to the authority endpoint during the authentication.
  /// If not specified then https://graph.microsoft.com/.default scope will be used. See this for info:
  /// <list type="bullet">
  /// <item>https://learn.microsoft.com/en-us/azure/active-directory/develop/scopes-oidc#the-default-scope</item>
  /// <item>https://learn.microsoft.com/en-us/azure/active-directory/develop/scopes-oidc#default-when-the-user-has-already-given-consent</item>
  /// </list>
  /// </summary>
  /// <value>
  /// The graph API scope passed to the authority endpoint during the authentication.
  /// </value>
  internal string GraphApiScope { get; set; }

  internal string DC { get; set; }

  internal string User { get; set; }

  internal string Password { get; set; }

  internal int Timeout { get; set; } = 120;

  internal bool SSL { get; set; }

  internal string RootGroup { get; set; }

  internal bool Preload { get; set; }

  internal bool RootGroupRestrictionEnabled { get; set; }

  internal bool KeepErrorsInCache { get; set; }

  [PXInternalUseOnly]
  public int ADGroupCacheLimit { get; set; } = 100;

  internal void BindFrom(IConfiguration configuration)
  {
    ConfigurationBinder.Bind((IConfiguration) configuration.GetSection("activeDirectory"), (object) this, (System.Action<BinderOptions>) (options => options.BindNonPublicProperties = true));
    ConfigurationBinder.Bind(configuration, (object) this);
  }
}
