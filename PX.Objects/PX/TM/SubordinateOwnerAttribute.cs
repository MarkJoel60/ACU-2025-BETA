// Decompiled with JetBrains decompiler
// Type: PX.TM.SubordinateOwnerAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.TM;

/// <summary>
/// Allows to show contacts which are subordinated or coworkers for current logged in employee.
/// </summary>
/// <example>[SubordinateOwner]</example>
public class SubordinateOwnerAttribute : OwnerAttribute
{
  public SubordinateOwnerAttribute()
    : base((Type) null, typeof (Search5<OwnerAttribute.Owner.contactID, LeftJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.contactID, Equal<OwnerAttribute.Owner.contactID>>>, Where<OwnerAttribute.Owner.contactID, Equal<Current<AccessInfo.contactID>>, Or<EPCompanyTreeMember.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>, Aggregate<GroupBy<OwnerAttribute.Owner.contactID>>>), false, true)
  {
  }
}
