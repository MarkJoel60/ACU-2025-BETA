// Decompiled with JetBrains decompiler
// Type: PX.TM.PXSubordinateOwnerSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.TM;

/// <summary>
/// Allows show employees which are subordinated or coworkers for current logined employee.
/// </summary>
/// <example>[PXSubordinateOwnerSelector]</example>
[Obsolete]
public class PXSubordinateOwnerSelectorAttribute : PXOwnerSelectorAttribute
{
  public PXSubordinateOwnerSelectorAttribute()
    : base((Type) null, typeof (Search5<PXOwnerSelectorAttribute.EPEmployee.pKID, LeftJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.contactID, Equal<PXOwnerSelectorAttribute.EPEmployee.defContactID>>>, Where<PXOwnerSelectorAttribute.EPEmployee.pKID, Equal<Current<AccessInfo.userID>>, Or<EPCompanyTreeMember.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>, Aggregate<GroupBy<PXOwnerSelectorAttribute.EPEmployee.pKID>>>), false, true)
  {
  }
}
