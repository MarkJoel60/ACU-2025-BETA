// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXSubordinateAndWingmenSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.SM;
using PX.TM;
using System;

#nullable disable
namespace PX.Objects.EP;

public class PXSubordinateAndWingmenSelectorAttribute(System.Type where, System.Type delegationOf) : 
  PXSubordinateSelectorAttribute("BIZACCT", PXSubordinateAndWingmenSelectorAttribute.GetCommand(where, delegationOf), true, true)
{
  public PXSubordinateAndWingmenSelectorAttribute(System.Type delegationOf)
    : this((System.Type) null, delegationOf)
  {
  }

  [Obsolete("This constructor is deprecated, use PXSubordinateAndWingmenSelectorAttribute(Type delegationOf) instead")]
  public PXSubordinateAndWingmenSelectorAttribute()
    : this((System.Type) null, (System.Type) null)
  {
  }

  private static System.Type GetCommand(System.Type where, System.Type delegationOf)
  {
    System.Type type;
    if (delegationOf != (System.Type) null)
      type = BqlCommand.Compose(new System.Type[11]
      {
        typeof (Where<,,>),
        typeof (CREmployee.defContactID),
        typeof (Equal<Current<AccessInfo.contactID>>),
        typeof (Or<,,>),
        typeof (CREmployee.defContactID),
        typeof (IsSubordinateOfContact<Current<AccessInfo.contactID>>),
        typeof (Or<,>),
        typeof (CREmployee.bAccountID),
        typeof (WingmanUser<,>),
        typeof (Current<AccessInfo.userID>),
        delegationOf
      });
    else
      type = typeof (Where<CREmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<CREmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<CREmployee.bAccountID, WingmanUser<Current<AccessInfo.userID>>>>>);
    if (where != (System.Type) null)
      type = BqlCommand.Compose(new System.Type[4]
      {
        typeof (Where2<,>),
        typeof (Where<CREmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<CREmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>>>),
        typeof (And<>),
        where
      });
    return BqlCommand.Compose(new System.Type[5]
    {
      typeof (Search5<,,,>),
      typeof (CREmployee.bAccountID),
      typeof (LeftJoin<Users, On<Users.pKID, Equal<CREmployee.userID>>>),
      type,
      typeof (Aggregate<GroupBy<CREmployee.acctCD>>)
    });
  }
}
