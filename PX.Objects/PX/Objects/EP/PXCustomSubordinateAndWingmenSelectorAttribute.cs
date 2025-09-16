// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXCustomSubordinateAndWingmenSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.SM;
using PX.TM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.EP;

public class PXCustomSubordinateAndWingmenSelectorAttribute : PXCustomSelectorAttribute
{
  private System.Type search;

  public bool AddSubordinationAndWingmenCheck { get; set; } = true;

  public PXCustomSubordinateAndWingmenSelectorAttribute()
    : this((System.Type) null)
  {
  }

  public PXCustomSubordinateAndWingmenSelectorAttribute(System.Type search)
    : base(PXCustomSubordinateAndWingmenSelectorAttribute.GetCommand(search, true), new System.Type[4]
    {
      typeof (CREmployee.acctCD),
      typeof (CREmployee.bAccountID),
      typeof (CREmployee.acctName),
      typeof (CREmployee.departmentID)
    })
  {
    this.search = search;
    ((PXSelectorAttribute) this).SubstituteKey = typeof (CREmployee.acctCD);
    ((PXSelectorAttribute) this).DescriptionField = typeof (CREmployee.acctName);
  }

  protected virtual IEnumerable GetRecords()
  {
    PXCustomSubordinateAndWingmenSelectorAttribute selectorAttribute = this;
    BqlCommand bqlCommand = BqlCommand.CreateInstance(new System.Type[1]
    {
      PXCustomSubordinateAndWingmenSelectorAttribute.GetCommand(selectorAttribute.search, selectorAttribute.AddSubordinationAndWingmenCheck)
    }).WhereAnd<Where<CREmployee.vStatus, NotEqual<VendorStatus.inactive>>>();
    foreach (PXResult<CREmployee> record in new PXView(selectorAttribute._Graph, false, bqlCommand).SelectMulti(Array.Empty<object>()))
      yield return (object) record;
  }

  private static System.Type GetCommand(System.Type where, bool subordinationCheck)
  {
    System.Type type;
    if (subordinationCheck)
    {
      type = typeof (Where<CREmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<CREmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<CREmployee.bAccountID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.expenses>>>>);
      if (where != (System.Type) null)
        type = BqlCommand.Compose(new System.Type[4]
        {
          typeof (Where2<,>),
          typeof (Where<CREmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<CREmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>>>),
          typeof (And<>),
          where
        });
    }
    else
      type = where;
    System.Type command;
    if (type != (System.Type) null)
      command = BqlCommand.Compose(new System.Type[5]
      {
        typeof (Search5<,,,>),
        typeof (CREmployee.bAccountID),
        typeof (LeftJoin<Users, On<Users.pKID, Equal<CREmployee.userID>>>),
        type,
        typeof (Aggregate<GroupBy<CREmployee.acctCD>>)
      });
    else
      command = BqlCommand.Compose(new System.Type[4]
      {
        typeof (Search5<,,>),
        typeof (CREmployee.bAccountID),
        typeof (LeftJoin<Users, On<Users.pKID, Equal<CREmployee.userID>>>),
        typeof (Aggregate<GroupBy<CREmployee.acctCD>>)
      });
    return command;
  }
}
