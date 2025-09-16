// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAnyPeriodFilterableAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// FinPeriod selector that extends <see cref="T:PX.Objects.GL.AnyPeriodFilterableAttribute" />.
/// Displays any periods (active, closed, etc), maybe filtered.
/// When Date is supplied through aSourceType parameter FinPeriod is defaulted with the FinPeriod for the given date.
/// Default columns list includes 'Active' and  'Closed in GL' and 'Closed in AP'  columns
/// </summary>
public class APAnyPeriodFilterableAttribute : AnyPeriodFilterableAttribute
{
  public APAnyPeriodFilterableAttribute(Type aSearchType, Type aSourceType)
    : base(aSearchType, aSourceType)
  {
  }

  public APAnyPeriodFilterableAttribute(Type aSourceType)
    : this((Type) null, aSourceType)
  {
  }

  public APAnyPeriodFilterableAttribute()
    : this((Type) null)
  {
  }
}
