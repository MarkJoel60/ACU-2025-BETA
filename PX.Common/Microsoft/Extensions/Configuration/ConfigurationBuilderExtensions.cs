// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.ConfigurationBuilderExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

#nullable disable
namespace Microsoft.Extensions.Configuration;

internal static class ConfigurationBuilderExtensions
{
  internal static IConfigurationBuilder AddAppSettings(
    this IConfigurationBuilder _param0,
    NameValueCollection _param1,
    params Func<IEnumerable<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>>[] _param2)
  {
    ConfigurationBuilderExtensions.\u000E obj = new ConfigurationBuilderExtensions.\u000E();
    obj.\u0002 = _param1;
    return MemoryConfigurationBuilderExtensions.AddInMemoryCollection(_param0, ((IEnumerable<Func<IEnumerable<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>>>) _param2).Aggregate<Func<IEnumerable<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>>, IEnumerable<KeyValuePair<string, string>>>(((IEnumerable<string>) obj.\u0002.AllKeys).Select<string, KeyValuePair<string, string>>(new Func<string, KeyValuePair<string, string>>(obj.\u0002)), ConfigurationBuilderExtensions.\u0002.\u000E ?? (ConfigurationBuilderExtensions.\u0002.\u000E = new Func<IEnumerable<KeyValuePair<string, string>>, Func<IEnumerable<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>>, IEnumerable<KeyValuePair<string, string>>>(ConfigurationBuilderExtensions.\u0002.\u0002.\u0002))));
  }

  internal static IConfigurationBuilder AddAppSettings(
    this IConfigurationBuilder _param0,
    params Func<IEnumerable<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>>[] _param1)
  {
    return _param0.AddAppSettings(ConfigurationManager.AppSettings, _param1);
  }

  internal static IConfigurationBuilder AddConnectionStrings(
    this IConfigurationBuilder _param0,
    ConnectionStringSettingsCollection _param1)
  {
    return MemoryConfigurationBuilderExtensions.AddInMemoryCollection(_param0, _param1.Cast<ConnectionStringSettings>().Select<ConnectionStringSettings, KeyValuePair<string, string>>(ConfigurationBuilderExtensions.\u0002.\u0006 ?? (ConfigurationBuilderExtensions.\u0002.\u0006 = new Func<ConnectionStringSettings, KeyValuePair<string, string>>(ConfigurationBuilderExtensions.\u0002.\u0002.\u0002))));
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly ConfigurationBuilderExtensions.\u0002 \u0002 = new ConfigurationBuilderExtensions.\u0002();
    public static Func<IEnumerable<KeyValuePair<string, string>>, Func<IEnumerable<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>>, IEnumerable<KeyValuePair<string, string>>> \u000E;
    public static Func<ConnectionStringSettings, KeyValuePair<string, string>> \u0006;

    internal IEnumerable<KeyValuePair<string, string>> \u0002(
      IEnumerable<KeyValuePair<string, string>> _param1,
      Func<IEnumerable<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>> _param2)
    {
      return _param2(_param1);
    }

    internal KeyValuePair<string, string> \u0002(ConnectionStringSettings _param1)
    {
      return new KeyValuePair<string, string>(ConfigurationPath.Combine(new string[2]
      {
        "ConnectionStrings",
        _param1.Name
      }), _param1.ConnectionString);
    }
  }

  private sealed class \u000E
  {
    public NameValueCollection \u0002;

    internal KeyValuePair<string, string> \u0002(string _param1)
    {
      return new KeyValuePair<string, string>(_param1, this.\u0002[_param1]);
    }
  }
}
