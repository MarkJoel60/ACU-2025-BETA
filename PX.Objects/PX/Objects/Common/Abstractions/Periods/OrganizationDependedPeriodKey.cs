// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Abstractions.Periods.OrganizationDependedPeriodKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Abstractions.Periods;

public class OrganizationDependedPeriodKey
{
  public string PeriodID { get; set; }

  public int? OrganizationID { get; set; }

  public virtual bool Defined => this.PeriodID != null && this.OrganizationID.HasValue;

  public virtual List<object> ToListOfObjects(bool skipPeriodID = false)
  {
    List<object> listOfObjects = new List<object>();
    if (!skipPeriodID)
      listOfObjects.Add((object) this.PeriodID);
    listOfObjects.Add((object) this.OrganizationID);
    return listOfObjects;
  }

  public virtual bool IsNotPeriodPartsEqual(object otherKey)
  {
    int? organizationId1 = ((OrganizationDependedPeriodKey) otherKey).OrganizationID;
    int? organizationId2 = this.OrganizationID;
    return organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue;
  }

  public virtual bool IsMasterCalendar
  {
    get
    {
      int? organizationId = this.OrganizationID;
      int num = 0;
      return organizationId.GetValueOrDefault() == num & organizationId.HasValue;
    }
  }
}
