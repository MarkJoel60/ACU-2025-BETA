// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.PXLongOperationPars
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Context;
using PX.Data;
using PX.Translation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Async.Internal;

[Serializable]
internal class PXLongOperationPars
{
  private readonly WindowsIdentity _identity;
  private readonly string _screenId;
  private readonly bool _isModernUiScreen;
  private readonly PXDictionaryManager _dictionaryManager;
  private readonly CultureInfo _cultureInfo;
  private readonly PXExecutionContext _executionContext;
  private readonly Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers> _subscribers;
  private readonly IDictionary _contextSlotsToBeCopied;
  private readonly Dictionary<System.Type, object> _extensions = new Dictionary<System.Type, object>();
  private System.Action<CancellationToken> _delegate;

  internal static PXLongOperationPars Capture(
    ISlotStorageProvider slots,
    HttpContext httpContext,
    System.Action<CancellationToken> method,
    byte[] timeStamp,
    PXLongOperationState state)
  {
    return new PXLongOperationPars(slots, httpContext, method, timeStamp, state);
  }

  private PXLongOperationPars(
    ISlotStorageProvider slots,
    HttpContext httpContext,
    System.Action<CancellationToken> method,
    byte[] timeStamp,
    PXLongOperationState state)
  {
    this._identity = WindowsIdentity.GetCurrent();
    this._screenId = ((ISlotStore) slots).GetScreenID();
    this._isModernUiScreen = ((ISlotStore) slots).IsModernUiScreen();
    this._dictionaryManager = TypeKeyedOperationExtensions.Get<PXDictionaryManager>((ISlotStore) slots);
    this._cultureInfo = Thread.CurrentThread.CurrentCulture;
    this._executionContext = PXExecutionContext.Get((ISlotStore) slots, httpContext);
    this._subscribers = TypeKeyedOperationExtensions.Get<Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>>((ISlotStore) slots);
    this.UserContext = PXSessionContextFactory.GetSessionContext((ISlotStore) slots).Clone();
    this._contextSlotsToBeCopied = PXContextCopyingRequiredAttribute.Capture(slots);
    this._delegate = method;
    this.TimeStamp = timeStamp;
    this.State = state;
  }

  internal void Restore(ISlotStorageProvider slots)
  {
    PXContextCopyingRequiredAttribute.SetToStorage(this._contextSlotsToBeCopied, slots);
    TypeKeyedOperationExtensions.Set<PXCacheExtensionCollection>((ISlotStore) slots, new PXCacheExtensionCollection());
    PXSessionContextFactory.InitializeContext((ISlotStore) slots, this.UserContext);
    ((ISlotStore) slots).SetScreenID(this._screenId, this._isModernUiScreen);
    TypeKeyedOperationExtensions.Set<PXDictionaryManager>((ISlotStore) slots, this._dictionaryManager);
    this._identity.Impersonate();
    LocaleInfo.SetAllCulture(this._cultureInfo);
    PXExecutionContext.Get((ISlotStore) slots, (HttpContext) null).Restore(this._executionContext);
    TypeKeyedOperationExtensions.Set<Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>>((ISlotStore) slots, this._subscribers);
  }

  internal void PopAndRunDelegate(CancellationToken cancellationToken)
  {
    System.Action<CancellationToken> action = this._delegate;
    if (action == null)
      throw new InvalidOperationException("_delegate is null");
    this._delegate = (System.Action<CancellationToken>) null;
    action(cancellationToken);
  }

  internal byte[] TimeStamp { get; }

  internal PXLongOperationState State { get; }

  internal PXSessionContext UserContext { get; }

  internal void AddExtension<T>(T data) where T : class
  {
    Dictionary<System.Type, object> extensions = this._extensions;
    System.Type key = typeof (T);
    // ISSUE: variable of a boxed type
    __Boxed<T> local = (object) (data ?? throw new ArgumentNullException(nameof (data)));
    extensions.Add(key, (object) local);
  }

  internal T GetExtension<T>() where T : class => (T) this._extensions[typeof (T)];
}
