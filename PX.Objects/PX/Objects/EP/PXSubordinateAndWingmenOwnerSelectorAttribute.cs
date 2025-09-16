// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXSubordinateAndWingmenOwnerSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using PX.TM;
using System;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// Allows show employees which are subordinated or wingman for current logined employee.
/// </summary>
/// <example>[PXSubordinateAndWingmenOwnerSelector]</example>
[Obsolete]
public class PXSubordinateAndWingmenOwnerSelectorAttribute : PXOwnerSelectorAttribute
{
  public PXSubordinateAndWingmenOwnerSelectorAttribute()
    : base((Type) null, typeof (Search5<PXOwnerSelectorAttribute.EPEmployee.pKID, LeftJoin<Users, On<Users.pKID, Equal<PXOwnerSelectorAttribute.EPEmployee.pKID>>>, Where<PXOwnerSelectorAttribute.EPEmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<PXOwnerSelectorAttribute.EPEmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<PXOwnerSelectorAttribute.EPEmployee.bAccountID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.expenses>>>>, Aggregate<GroupBy<PXOwnerSelectorAttribute.EPEmployee.pKID>>>), false)
  {
  }
}
