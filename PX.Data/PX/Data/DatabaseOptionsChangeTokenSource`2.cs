// Decompiled with JetBrains decompiler
// Type: PX.Data.DatabaseOptionsChangeTokenSource`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>
/// Used for tracking options changes based on specific <typeparamref name="TDac" /> modifications in the DB
/// </summary>
/// <typeparam name="TOptions">Options type to be tracked</typeparam>
/// <typeparam name="TDac">DAC to be monitored for changes</typeparam>
internal class DatabaseOptionsChangeTokenSource<TOptions, TDac> : 
  IOptionsChangeTokenSource<TOptions>,
  IDisposable
{
  private PXDatabaseProvider _databaseProvider;
  private readonly string _name;
  private ConfigurationReloadToken _changeToken = DatabaseOptionsChangeTokenSource<TOptions, TDac>.CreateNewToken();

  public DatabaseOptionsChangeTokenSource(PXDatabaseProvider databaseProvider, string name = null)
  {
    this._databaseProvider = databaseProvider ?? throw new ArgumentNullException(nameof (databaseProvider));
    this._name = name ?? Microsoft.Extensions.Options.Options.DefaultName;
    this._databaseProvider.Subscribe(typeof (TDac), new PXDatabaseTableChanged(this.RaiseChanged));
  }

  IChangeToken IOptionsChangeTokenSource<TOptions>.GetChangeToken()
  {
    return (IChangeToken) this._changeToken;
  }

  string IOptionsChangeTokenSource<TOptions>.Name => this._name;

  private static ConfigurationReloadToken CreateNewToken() => new ConfigurationReloadToken();

  private void RaiseChanged()
  {
    Interlocked.Exchange<ConfigurationReloadToken>(ref this._changeToken, DatabaseOptionsChangeTokenSource<TOptions, TDac>.CreateNewToken()).OnReload();
  }

  void IDisposable.Dispose()
  {
    Interlocked.Exchange<PXDatabaseProvider>(ref this._databaseProvider, (PXDatabaseProvider) null)?.UnSubscribe(typeof (TDac), new PXDatabaseTableChanged(this.RaiseChanged));
  }
}
