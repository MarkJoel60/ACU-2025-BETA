// Decompiled with JetBrains decompiler
// Type: PX.SM.BlobStorageProviderDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

internal class BlobStorageProviderDefinition : IPrefetchable, IPXCompanyDependent
{
  private bool _IsInit;
  private Exception _InitError;
  private BlobStorageConfig _Config;
  private IBlobStorageProvider _Provider;

  public bool HasInitError => this._InitError != null;

  private void EnsureInit()
  {
    if (this._IsInit)
      return;
    this._IsInit = true;
    try
    {
      this._Config = (BlobStorageConfig) PXSelectBase<BlobStorageConfig, PXSelectReadonly<BlobStorageConfig>.Config>.SelectSingleBound(new PXGraph(), (object[]) null);
      if (this._Config == null || Str.IsNullOrEmpty(this._Config.Provider))
        return;
      IEnumerable<BlobProviderSettings> firstTableItems = PXSelectBase<BlobProviderSettings, PXSelectReadonly<BlobProviderSettings>.Config>.Select(new PXGraph()).FirstTableItems;
      IBlobStorageProvider provider = PXBlobStorage.CreateProvider(this._Config.Provider);
      provider?.Init(firstTableItems);
      this._Provider = provider;
    }
    catch (Exception ex)
    {
      this._InitError = ex;
      this._IsInit = false;
    }
  }

  public void Prefetch()
  {
  }

  private BlobStorageConfig Config
  {
    get
    {
      this.EnsureInit();
      return this._Config;
    }
  }

  public IBlobStorageProvider Provider
  {
    get
    {
      this.EnsureInit();
      return this._Provider;
    }
  }

  public void Reset()
  {
    this._IsInit = false;
    this._Config = (BlobStorageConfig) null;
    this._Provider = (IBlobStorageProvider) null;
  }

  public void CheckInitError()
  {
    if (!this._IsInit && this._InitError != null)
      throw this._InitError;
  }

  public bool AllowSave
  {
    get => this.Config != null && this._IsInit && this.Config.AllowWrite.GetValueOrDefault();
  }
}
