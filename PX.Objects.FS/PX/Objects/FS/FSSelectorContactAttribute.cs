// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorContactAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorContactAttribute : PXSelectorAttribute
{
  public FSSelectorContactAttribute(System.Type currentCustomer)
    : base(((IBqlTemplate) BqlTemplate.OfCommand<Search2<PX.Objects.CR.Contact.contactID, InnerJoin<BAccount, On<BAccount.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>, LeftJoin<CRLead, On<PX.Objects.CR.Contact.contactID, Equal<CRLead.contactID>>>>, Where<PX.Objects.CR.Contact.contactType, NotIn3<ContactTypesAttribute.bAccountProperty, ContactTypesAttribute.broker>, And<CRLead.contactID, IsNull, And<PX.Objects.CR.Contact.isActive, Equal<True>, And<Where2<Where<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>, And<Where<BAccount.bAccountID, Equal<Current<BqlPlaceholder.A>>, Or<Current<BqlPlaceholder.A>, IsNull>>>>>>>>>>.Replace<BqlPlaceholder.A>(currentCustomer)).ToType())
  {
    this.SubstituteKey = typeof (PX.Objects.CR.Contact.displayName);
    this.Filterable = true;
    this.DirtyRead = true;
  }
}
