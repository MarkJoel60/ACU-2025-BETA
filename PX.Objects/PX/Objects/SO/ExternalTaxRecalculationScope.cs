// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.ExternalTaxRecalculationScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

public class ExternalTaxRecalculationScope : IDisposable
{
  protected bool _disposed;

  public ExternalTaxRecalculationScope()
  {
    ExternalTaxRecalculationScope.Context context = PXContext.GetSlot<ExternalTaxRecalculationScope.Context>();
    if (context == null)
    {
      context = new ExternalTaxRecalculationScope.Context();
      PXContext.SetSlot<ExternalTaxRecalculationScope.Context>(context);
    }
    ++context.ReferenceCounter;
  }

  public void Dispose()
  {
    this._disposed = !this._disposed ? true : throw new PXObjectDisposedException();
    ExternalTaxRecalculationScope.Context slot = PXContext.GetSlot<ExternalTaxRecalculationScope.Context>();
    --slot.ReferenceCounter;
    if (slot.ReferenceCounter != 0)
      return;
    PXContext.SetSlot<ExternalTaxRecalculationScope.Context>((ExternalTaxRecalculationScope.Context) null);
  }

  public static bool IsScoped()
  {
    ExternalTaxRecalculationScope.Context slot = PXContext.GetSlot<ExternalTaxRecalculationScope.Context>();
    return slot != null && slot.ReferenceCounter > 0;
  }

  public class Context
  {
    public int ReferenceCounter { get; set; }
  }
}
