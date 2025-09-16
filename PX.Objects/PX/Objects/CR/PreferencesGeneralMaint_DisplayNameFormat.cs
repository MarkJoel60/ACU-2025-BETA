// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PreferencesGeneralMaint_DisplayNameFormat
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CR;

public class PreferencesGeneralMaint_DisplayNameFormat : PXGraphExtension<PreferencesGeneralMaint>
{
  [PXOverride]
  public virtual void UpdatePersonDisplayNames(string PersonDisplayNameFormat)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      switch (PersonDisplayNameFormat)
      {
        case "WESTERN":
          this.SetContactsWesternOrder();
          break;
        case "EASTERN":
          this.SetContactsEasternOrder();
          break;
        case "LEGACY":
          this.SetContactsLegacyOrder();
          break;
        case "EASTERN_WITH_TITLE":
          this.SetContactsEasternWithTitleOrder();
          break;
      }
      this.UpdateBAccounts();
      transactionScope.Complete();
    }
    PXDatabase.ClearCompanyCache();
  }

  protected virtual void SetContactsWesternOrder()
  {
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, IsNull<BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.firstName, Space>>, IBqlString>.Concat<Contact.lastName>, BqlOperand<Empty, IBqlString>.Concat<Contact.lastName>>>, Select<Contact, Where<Contact.lastName, IsNotNull>>>(), Array.Empty<PXDataValue>());
  }

  protected virtual void SetContactsEasternOrder()
  {
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, IIf<BqlOperand<Contact.firstName, IBqlString>.IsNull, Contact.lastName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, CommaSpace>>, IBqlString>.Concat<Contact.firstName>>>, Select<Contact, Where<Contact.lastName, IsNotNull>>>(), Array.Empty<PXDataValue>());
  }

  protected virtual void SetContactsLegacyOrder()
  {
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, Contact.lastName>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNull, And<Contact.firstName, IsNull, And<Contact.midName, IsNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, CommaSpace>>, IBqlString>.Concat<Contact.midName>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNull, And<Contact.firstName, IsNull, And<Contact.midName, IsNotNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, CommaSpace>>, IBqlString>.Concat<Contact.firstName>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNull, And<Contact.firstName, IsNotNull, And<Contact.midName, IsNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, CommaSpace>>, Contact.firstName>>, Space>>, IBqlString>.Concat<Contact.midName>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNull, And<Contact.firstName, IsNotNull, And<Contact.midName, IsNotNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, CommaSpace>>, IBqlString>.Concat<Contact.title>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNotNull, And<Contact.firstName, IsNull, And<Contact.midName, IsNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, Space>>, Contact.midName>>, CommaSpace>>, IBqlString>.Concat<Contact.title>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNotNull, And<Contact.firstName, IsNull, And<Contact.midName, IsNotNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, Space>>, Contact.firstName>>, CommaSpace>>, IBqlString>.Concat<Contact.title>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNotNull, And<Contact.firstName, IsNotNull, And<Contact.midName, IsNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, Space>>, Contact.firstName>>, Space>>, Contact.midName>>, CommaSpace>>, IBqlString>.Concat<Contact.title>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNotNull, And<Contact.firstName, IsNotNull, And<Contact.midName, IsNotNull>>>>>>(), Array.Empty<PXDataValue>());
  }

  protected virtual void SetContactsEasternWithTitleOrder()
  {
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, Contact.lastName>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNull, And<Contact.firstName, IsNull, And<Contact.midName, IsNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, CommaSpace>>, IBqlString>.Concat<Contact.midName>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNull, And<Contact.firstName, IsNull, And<Contact.midName, IsNotNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, CommaSpace>>, IBqlString>.Concat<Contact.firstName>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNull, And<Contact.firstName, IsNotNull, And<Contact.midName, IsNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.lastName, CommaSpace>>, Contact.firstName>>, Space>>, IBqlString>.Concat<Contact.midName>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNull, And<Contact.firstName, IsNotNull, And<Contact.midName, IsNotNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.title, Space>>, IBqlString>.Concat<Contact.lastName>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNotNull, And<Contact.firstName, IsNull, And<Contact.midName, IsNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.title, Space>>, Contact.lastName>>, CommaSpace>>, IBqlString>.Concat<Contact.midName>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNotNull, And<Contact.firstName, IsNull, And<Contact.midName, IsNotNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.title, Space>>, Contact.lastName>>, CommaSpace>>, IBqlString>.Concat<Contact.firstName>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNotNull, And<Contact.firstName, IsNotNull, And<Contact.midName, IsNull>>>>>>(), Array.Empty<PXDataValue>());
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<Contact.displayName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Contact.title, Space>>, Contact.lastName>>, CommaSpace>>, Contact.firstName>>, Space>>, IBqlString>.Concat<Contact.midName>>, Select<Contact, Where<Contact.lastName, IsNotNull, And<Contact.title, IsNotNull, And<Contact.firstName, IsNotNull, And<Contact.midName, IsNotNull>>>>>>(), Array.Empty<PXDataValue>());
  }

  protected virtual void UpdateBAccounts()
  {
    PXDatabase.Update((PXGraph) this.Base, (IBqlUpdate) new Update<Set<BAccount.acctName, Contact.displayName>, Select2<BAccount, InnerJoin<Contact, On<Contact.contactID, Equal<BAccount.defContactID>>>, Where<BAccount.type, Equal<BAccountType.employeeType>>>>(), Array.Empty<PXDataValue>());
  }
}
