// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.BaseNotDecimalUnitErrorRedirectorScope`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public abstract class BaseNotDecimalUnitErrorRedirectorScope<TDetailQty> : IDisposable where TDetailQty : IBqlField
{
  protected PXCache MasterCache { get; }

  protected Dictionary<object, PXNotDecimalUnitException> RedirectedSplits { get; }

  protected Type DetailType => BqlCommand.GetItemType<TDetailQty>();

  public BaseNotDecimalUnitErrorRedirectorScope(PXCache masterCache)
  {
    this.MasterCache = masterCache;
    this.RedirectedSplits = new Dictionary<object, PXNotDecimalUnitException>();
    PXGraph.ExceptionHandlingEvents exceptionHandling1 = masterCache.Graph.ExceptionHandling;
    BaseNotDecimalUnitErrorRedirectorScope<TDetailQty> errorRedirectorScope = this;
    // ISSUE: virtual method pointer
    PXExceptionHandling exceptionHandling2 = new PXExceptionHandling((object) errorRedirectorScope, __vmethodptr(errorRedirectorScope, SplitExceptionHandling));
    exceptionHandling1.AddHandler<TDetailQty>(exceptionHandling2);
  }

  protected virtual void SplitExceptionHandling(
    PXCache splitCache,
    PXExceptionHandlingEventArgs args)
  {
    if (!(args.Exception is PXNotDecimalUnitException exception))
      return;
    this.RedirectedSplits[args.Row] = exception;
  }

  public virtual void Dispose()
  {
    PXGraph.ExceptionHandlingEvents exceptionHandling1 = this.MasterCache.Graph.ExceptionHandling;
    BaseNotDecimalUnitErrorRedirectorScope<TDetailQty> errorRedirectorScope = this;
    // ISSUE: virtual method pointer
    PXExceptionHandling exceptionHandling2 = new PXExceptionHandling((object) errorRedirectorScope, __vmethodptr(errorRedirectorScope, SplitExceptionHandling));
    exceptionHandling1.RemoveHandler<TDetailQty>(exceptionHandling2);
    if (!this.RedirectedSplits.Any<KeyValuePair<object, PXNotDecimalUnitException>>())
      return;
    this.HandleErrors();
  }

  protected abstract void HandleErrors();
}
