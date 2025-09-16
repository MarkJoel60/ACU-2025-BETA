// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.UpdateContactMassProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.MassProcess;
using PX.SM;

#nullable disable
namespace PX.Objects.CR;

public class UpdateContactMassProcess : 
  CRBaseUpdateProcess<UpdateContactMassProcess, Contact, PXMassUpdatableFieldAttribute, Contact.classID>
{
  [PXViewName("Matching Records")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (Contact))]
  [PXViewDetailsButton(typeof (Contact), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<Contact.bAccountID>>>>), ActionName = "Items_BAccount_ViewDetails")]
  [PXViewDetailsButton(typeof (Contact), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<Contact.parentBAccountID>>>>), ActionName = "Items_BAccountParent_ViewDetails")]
  public PXProcessingJoin<Contact, LeftJoin<Address, On<Address.addressID, Equal<Contact.defAddressID>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>, LeftJoin<BAccountParent, On<BAccountParent.bAccountID, Equal<Contact.parentBAccountID>>, LeftJoin<PX.Objects.CS.State, On<PX.Objects.CS.State.countryID, Equal<Address.countryID>, And<PX.Objects.CS.State.stateID, Equal<Address.state>>>, LeftJoin<CRActivityStatistics, On<Contact.noteID, Equal<CRActivityStatistics.noteID>>>>>>>, Where<Contact.contactType, Equal<ContactTypesAttribute.person>, And<Contact.isActive, Equal<True>, And<Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>>>>, OrderBy<Asc<Contact.displayName>>> Items;

  [PXUIField(DisplayName = "Account Name")]
  [CRLeadFullName(typeof (Contact.bAccountID))]
  protected virtual void _(Events.CacheAttached<Contact.fullName> e)
  {
  }
}
