// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.IGraphApiClientConfigurationProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

/// <summary>
/// Provides MS Graph API configuration <see cref="T:PX.Data.Access.ActiveDirectory.GraphApiClientConfiguration" /> from options.
/// </summary>
internal interface IGraphApiClientConfigurationProvider
{
  /// <summary>
  /// Creates <see cref="T:PX.Data.Access.ActiveDirectory.GraphApiClientConfiguration" /> configuration based on the application <paramref name="options" />.
  /// </summary>
  /// <param name="options">Application Active Directory options.</param>
  /// <remarks>
  /// The default configuration connects to the Microsoft Azure public cloud. Use <see cref="P:PX.Data.Access.ActiveDirectory.Options.Path" />,
  /// <see cref="!:Options.GraphApiAuthorityKindOrUrl" /> and <see cref="P:PX.Data.Access.ActiveDirectory.Options.GraphApiScope" /> to configure Azure AD
  /// for national clouds like US Government cloud:<br /><br />
  /// https://learn.microsoft.com/en-us/graph/deployments
  /// </remarks>
  /// <returns>
  /// <see cref="T:PX.Data.Access.ActiveDirectory.GraphApiClientConfiguration" /> instance based on provided options.
  /// </returns>
  GraphApiClientConfiguration CreateConfiguration(Options options);
}
