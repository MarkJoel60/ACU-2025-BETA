// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.AssignBAccountMassProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;

#nullable disable
namespace PX.Objects.CR;

public class AssignBAccountMassProcess : 
  CRBaseAssignProcess<AssignBAccountMassProcess, BAccount, CRSetup.defaultBAccountAssignmentMapID>
{
  [PXViewName("Matching Records")]
  [PXViewDetailsButton(typeof (BAccount))]
  [PXViewDetailsButton(typeof (BAccount), typeof (Select<BAccountParent, Where<BAccountParent.bAccountID, Equal<Current<BAccount.parentBAccountID>>>>))]
  [PXFilterable(new System.Type[] {})]
  public PXProcessingJoin<BAccount, LeftJoin<Contact, On<Contact.bAccountID, Equal<BAccount.bAccountID>, And<Contact.contactID, Equal<BAccount.defContactID>>>, LeftJoin<Address, On<Address.bAccountID, Equal<BAccount.bAccountID>, And<Address.addressID, Equal<BAccount.defAddressID>>>, LeftJoin<BAccountParent, On<BAccountParent.bAccountID, Equal<BAccount.parentBAccountID>>, LeftJoin<Location, On<Location.bAccountID, Equal<BAccount.bAccountID>, And<Location.locationID, Equal<BAccount.defLocationID>>>, LeftJoin<PX.Objects.CS.State, On<PX.Objects.CS.State.countryID, Equal<Address.countryID>, And<PX.Objects.CS.State.stateID, Equal<Address.state>>>>>>>>, Where2<Where<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.combinedType>, Or<BAccount.type, Equal<BAccountType.vendorType>>>>>, And<Match<Current<AccessInfo.userName>>>>, OrderBy<Asc<BAccount.acctName>>> Items;

  protected override PXGraph GetPrimaryGraph(BAccount item)
  {
    return (PXGraph) PXGraph.CreateInstance<BusinessAccountMaint>();
  }
}
