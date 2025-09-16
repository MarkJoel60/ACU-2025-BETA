// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class ContactSelectorAttribute : PXSelectorAttribute
{
  public ContactSelectorAttribute(bool showContactsWithNullEmail, params System.Type[] contactTypes)
    : base(ContactSelectorAttribute.GetQuery(typeof (Contact.contactID), showContactsWithNullEmail, contactTypes))
  {
    if (contactTypes == null || contactTypes.Length == 0)
      throw new ArgumentNullException(nameof (contactTypes));
    this.DescriptionField = typeof (Contact.displayName);
  }

  protected ContactSelectorAttribute(
    System.Type searchField,
    bool showContactsWithNullEmail,
    System.Type[] contactTypes,
    System.Type[] fieldList)
    : base(ContactSelectorAttribute.GetQuery(searchField, showContactsWithNullEmail, contactTypes), fieldList)
  {
    if (contactTypes == null || contactTypes.Length == 0)
      throw new ArgumentNullException(nameof (contactTypes));
    this.DescriptionField = typeof (Contact.displayName);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.SelectorMode = (PXSelectorMode) 0;
  }

  private static System.Type GetQuery(
    System.Type searchField,
    bool showContactsWithNullEmail,
    System.Type[] contactTypes)
  {
    System.Type type1 = BqlCommand.Compose(new System.Type[8]
    {
      typeof (Where<,,>),
      typeof (BAccount.bAccountID),
      typeof (IsNull),
      typeof (Or<>),
      typeof (Match<,>),
      typeof (BAccount),
      typeof (Current<>),
      typeof (AccessInfo.userName)
    });
    System.Type type2 = (System.Type) null;
    if (contactTypes.Length == 1)
    {
      type2 = BqlCommand.Compose(new System.Type[6]
      {
        typeof (Where<,,>),
        typeof (Contact.contactType),
        typeof (Equal<>),
        ((IEnumerable<System.Type>) contactTypes).Single<System.Type>(),
        typeof (And<>),
        type1
      });
    }
    else
    {
      foreach (System.Type type3 in ((IEnumerable<System.Type>) typeof (ContactTypesAttribute).GetNestedTypes()).Where<System.Type>((Func<System.Type, bool>) (x => typeof (IConstant<string>).IsAssignableFrom(x))).Except<System.Type>((IEnumerable<System.Type>) contactTypes))
      {
        if (type2 == (System.Type) null)
          type2 = BqlCommand.Compose(new System.Type[6]
          {
            typeof (Where<,,>),
            typeof (Contact.contactType),
            typeof (NotEqual<>),
            type3,
            typeof (And<>),
            type1
          });
        else
          type2 = BqlCommand.Compose(new System.Type[6]
          {
            typeof (Where<,,>),
            typeof (Contact.contactType),
            typeof (NotEqual<>),
            type3,
            typeof (And<>),
            type2
          });
      }
    }
    System.Type query;
    if (showContactsWithNullEmail)
    {
      query = BqlCommand.Compose(new System.Type[9]
      {
        typeof (Search2<,,>),
        searchField,
        typeof (LeftJoin<,>),
        typeof (BAccount),
        typeof (On<,>),
        typeof (BAccount.bAccountID),
        typeof (Equal<>),
        typeof (Contact.bAccountID),
        type2
      });
    }
    else
    {
      System.Type type4 = BqlCommand.Compose(new System.Type[3]
      {
        typeof (Where<,>),
        typeof (Contact.eMail),
        typeof (IsNotNull)
      });
      query = BqlCommand.Compose(new System.Type[14]
      {
        typeof (Search2<,,>),
        searchField,
        typeof (LeftJoin<,>),
        typeof (BAccount),
        typeof (On<,>),
        typeof (BAccount.bAccountID),
        typeof (Equal<>),
        typeof (Contact.bAccountID),
        typeof (Where2<,>),
        type4,
        typeof (And2<,>),
        type2,
        typeof (And<>),
        type1
      });
    }
    return query;
  }
}
