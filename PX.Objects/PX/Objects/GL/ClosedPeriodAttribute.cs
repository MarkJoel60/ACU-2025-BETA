// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ClosedPeriodAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// FinPeriod selector that extends <see cref="T:PX.Objects.GL.FinPeriodSelectorAttribute" />.
/// Displays and accepts only Closed Fin Periods.
/// When Date is supplied through aSourceType parameter FinPeriod is defaulted with the FinPeriod for the given date.
/// </summary>
public class ClosedPeriodAttribute : FinPeriodSelectorAttribute
{
  public ClosedPeriodAttribute(Type aSourceType)
    : this((Type) null, aSourceType)
  {
  }

  public ClosedPeriodAttribute()
    : this((Type) null)
  {
  }

  public ClosedPeriodAttribute(
    Type searchType,
    Type sourceType,
    Type branchSourceType = null,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type useMasterCalendarSourceType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    bool useMasterOrganizationIDByDefault = false)
  {
    Type searchType1 = searchType;
    if ((object) searchType1 == null)
      searchType1 = typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.status, Equal<FinPeriod.status.open>, Or<FinPeriod.status, Equal<FinPeriod.status.closed>>>>);
    // ISSUE: explicit constructor call
    base.\u002Ector(searchType1, sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, useMasterOrganizationIDByDefault: useMasterOrganizationIDByDefault);
  }
}
