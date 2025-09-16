// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTraceProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Logging.TraceProviders;
using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Diagnostics;

#nullable disable
namespace PX.Data;

[Obsolete("This object is obsolete and will be removed. Rewrite your code without this object or contact your partner for assistance.")]
public abstract class PXTraceProvider : ProviderBase
{
  private TraceLevelFilter _filter;

  protected PXTraceProvider()
  {
    this._filter = new TraceLevelFilter((string) null, this.DefaultMinimumLevel);
  }

  protected virtual SourceLevels DefaultMinimumLevel => SourceLevels.All;

  protected SourceLevels ConfiguredMinimumLevel => this._filter.MinimumLevel;

  public override void Initialize(string provname, NameValueCollection config)
  {
    if (config == null)
      throw new PXArgumentException(nameof (config), "The argument cannot be null.");
    base.Initialize(provname, config);
    this._filter = new TraceLevelFilter(config["minimumLevel"], this.DefaultMinimumLevel);
  }

  public virtual void TraceEvent(PXTrace.Event item)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    if (!this._filter.ShouldTrace(item.EventType))
      return;
    this.TraceEventInternal(item);
  }

  protected abstract void TraceEventInternal(PXTrace.Event item);
}
