// Decompiled with JetBrains decompiler
// Type: PX.Data.IConfigurationExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Specialized;

#nullable disable
namespace PX.Data;

internal static class IConfigurationExtensions
{
  /// <summary>Gets the connection string.</summary>
  /// <param name="configuration">The global configuration.</param>
  /// <param name="connectionConfiguration">The configuration containing <c>connectionString</c> or <c>connectionStringName</c> setting.</param>
  /// <returns>The connection string.</returns>
  /// <exception cref="T:System.ArgumentNullException"><see paramref="configuration" /> or <see paramref="connectionConfiguration" /> is <see langword="null" />.</exception>
  public static string GetConnectionString(
    this IConfiguration configuration,
    NameValueCollection connectionConfiguration)
  {
    if (configuration == null)
      throw new ArgumentNullException(nameof (configuration));
    string connectionString = connectionConfiguration != null ? connectionConfiguration["connectionString"] : throw new ArgumentNullException(nameof (connectionConfiguration));
    if (string.IsNullOrEmpty(connectionString))
      connectionString = ConfigurationExtensions.GetConnectionString(configuration, connectionConfiguration["connectionStringName"]);
    return connectionString;
  }
}
