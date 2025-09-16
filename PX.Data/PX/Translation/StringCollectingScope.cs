// Decompiled with JetBrains decompiler
// Type: PX.Translation.StringCollectingScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Context;
using PX.Data;
using System;
using System.Globalization;
using System.Reactive.Disposables;
using System.Threading;

#nullable disable
namespace PX.Translation;

/// <exclude />
public class StringCollectingScope : IDisposable
{
  private readonly CultureInfo previousThreadCulture;
  private readonly CultureInfo previousThreadUICulture;
  private readonly CultureInfo previousCulture;
  private readonly CultureInfo previousUICulture;
  private readonly IDisposable _contextCleanup;

  public StringCollectingScope()
  {
    this.previousThreadCulture = Thread.CurrentThread.CurrentCulture;
    this.previousThreadUICulture = Thread.CurrentThread.CurrentUICulture;
    this.previousCulture = PXContext.PXIdentity.Culture;
    this.previousUICulture = PXContext.PXIdentity.UICulture;
    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
    PXContext.PXIdentity.Culture = CultureInfo.InvariantCulture;
    PXContext.PXIdentity.UICulture = CultureInfo.InvariantCulture;
    ISlotStore instance = SlotStore.Instance;
    this._contextCleanup = StringCollectingScope.CreateContextInitializer(instance)(instance);
  }

  internal static Func<ISlotStore, IDisposable> CreateContextInitializer(ISlotStore slots)
  {
    return !(PXAccess.Provider is PXDBFeatureAccessProvider provider) ? (Func<ISlotStore, IDisposable>) (_ => Disposable.Empty) : provider.CreateStringCollectionContextInitializer(slots);
  }

  public void Dispose()
  {
    Thread.CurrentThread.CurrentCulture = this.previousThreadCulture;
    Thread.CurrentThread.CurrentUICulture = this.previousThreadUICulture;
    PXContext.PXIdentity.Culture = this.previousCulture;
    PXContext.PXIdentity.UICulture = this.previousUICulture;
    this._contextCleanup.Dispose();
  }
}
