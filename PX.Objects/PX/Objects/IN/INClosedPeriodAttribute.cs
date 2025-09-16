// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INClosedPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// FinPeriod selector that extends <see cref="T:PX.Objects.GL.FinPeriodSelectorAttribute" />.
/// Displays and accepts only Closed Fin Periods.
/// When Date is supplied through aSourceType parameter FinPeriod is defaulted with the FinPeriod for the given date.
/// </summary>
public class INClosedPeriodAttribute(Type aSourceType) : FinPeriodSelectorAttribute(typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.status, NotEqual<FinPeriod.status.inactive>, Or<FinPeriod.iNClosed, Equal<True>>>, OrderBy<Desc<FinPeriod.finPeriodID>>>))
{
  public INClosedPeriodAttribute()
    : this((Type) null)
  {
  }
}
