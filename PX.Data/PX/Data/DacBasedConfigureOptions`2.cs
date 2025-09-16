// Decompiled with JetBrains decompiler
// Type: PX.Data.DacBasedConfigureOptions`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using System;

#nullable disable
namespace PX.Data;

internal class DacBasedConfigureOptions<TOptions, TDac> : IConfigureOptions<TOptions>
  where TOptions : class
  where TDac : class, IBqlTable, new()
{
  private readonly DacBasedConfigureOptions<TOptions, TDac>.ConfigureFromDacDelegate _configurationAction;
  private readonly Func<TDac> _dacValueProvider;

  public DacBasedConfigureOptions(
    DacBasedConfigureOptions<TOptions, TDac>.ConfigureFromDacDelegate configurationAction,
    Func<TDac> dacValueProvider)
  {
    this._configurationAction = configurationAction ?? throw new ArgumentNullException(nameof (configurationAction));
    this._dacValueProvider = dacValueProvider ?? throw new ArgumentNullException(nameof (dacValueProvider));
  }

  void IConfigureOptions<TOptions>.Configure(TOptions options)
  {
    TDac actualDacValue = this._dacValueProvider();
    this._configurationAction(options, actualDacValue);
  }

  public delegate void ConfigureFromDacDelegate(TOptions optionsToBeConfigured, TDac actualDacValue)
    where TOptions : class
    where TDac : class, IBqlTable, new();
}
