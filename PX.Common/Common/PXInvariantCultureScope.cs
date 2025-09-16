// Decompiled with JetBrains decompiler
// Type: PX.Common.PXInvariantCultureScope
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Context;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Common;

public class PXInvariantCultureScope : PXCultureScope
{
  private static readonly string \u0002 = typeof (PXInvariantCultureScope).FullName + "+ScopeKey";
  private readonly bool? \u000E;

  public PXInvariantCultureScope()
    : base(PXCultureInfo.InvariantCulture)
  {
    this.\u000E = PXContext.GetSlot<bool?>(PXInvariantCultureScope.\u0002);
    if (this.\u000E.GetValueOrDefault())
      return;
    PXContext.SetSlot<bool?>(PXInvariantCultureScope.\u0002, new bool?(true));
  }

  private static bool \u0002()
  {
    return Thread.CurrentThread.CurrentCulture == PXCultureInfo.InvariantCulture && Thread.CurrentThread.CurrentUICulture == PXCultureInfo.InvariantCulture;
  }

  public static bool IsSet()
  {
    return (PXContext.GetSlot<bool?>(PXInvariantCultureScope.\u0002) ?? false) && PXInvariantCultureScope.\u0002();
  }

  public override void Dispose()
  {
    PXContext.SetSlot<bool?>(PXInvariantCultureScope.\u0002, this.\u000E);
    base.Dispose();
  }

  public void AddToContext(HttpContext context)
  {
    context.Slots().Set(PXInvariantCultureScope.\u0002, (object) true);
  }
}
