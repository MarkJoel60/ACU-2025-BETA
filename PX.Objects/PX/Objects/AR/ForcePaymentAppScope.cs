// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ForcePaymentAppScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ForcePaymentAppScope : IDisposable
{
  private static readonly string scopeKey = "ForcePaymentApp";

  public ForcePaymentAppScope() => PXContext.SetSlot<bool>(ForcePaymentAppScope.scopeKey, true);

  void IDisposable.Dispose() => PXContext.SetSlot<bool>(ForcePaymentAppScope.scopeKey, false);

  public static bool IsActive => PXContext.GetSlot<bool>(ForcePaymentAppScope.scopeKey);
}
