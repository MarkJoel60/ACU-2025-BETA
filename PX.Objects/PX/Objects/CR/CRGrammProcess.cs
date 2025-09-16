// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRGrammProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Extensions.CRDuplicateEntities;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CR;

[Serializable]
public class CRGrammProcess : PXGraph<
#nullable disable
CRGrammProcess>
{
  public PXCancel<Contact> Cancel;
  [PXHidden]
  public PXSelect<BAccount> baccount;
  [PXViewDetailsButton(typeof (Contact))]
  [PXViewDetailsButton(typeof (Contact), typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<Contact.bAccountID>>>>))]
  public FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRGramValidationDateTime.ByLead>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  True>>>, FbqlJoins.Inner<CRGramValidationDateTime.ByContact>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  True>>>, FbqlJoins.Inner<CRGramValidationDateTime.ByBAccount>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  True>>>, FbqlJoins.Left<BAccount>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BAccount.defContactID, 
  #nullable disable
  Equal<Contact.contactID>>>>>.And<BqlOperand<
  #nullable enable
  BAccount.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  Contact.bAccountID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  Contact.contactType, 
  #nullable disable
  Equal<ContactTypesAttribute.lead>>>>, And<BqlOperand<
  #nullable enable
  Contact.grammValidationDateTime, IBqlDateTime>.IsLess<
  #nullable disable
  CRGramValidationDateTime.ByLead.value>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  Contact.contactType, 
  #nullable disable
  Equal<ContactTypesAttribute.person>>>>>.And<BqlOperand<
  #nullable enable
  Contact.grammValidationDateTime, IBqlDateTime>.IsLess<
  #nullable disable
  CRGramValidationDateTime.ByContact.value>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  Contact.contactType, 
  #nullable disable
  Equal<ContactTypesAttribute.bAccountProperty>>>>>.And<BqlOperand<
  #nullable enable
  Contact.grammValidationDateTime, IBqlDateTime>.IsLess<
  #nullable disable
  CRGramValidationDateTime.ByBAccount.value>>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BAccount.bAccountID, 
  #nullable disable
  IsNull>>>, And<BqlOperand<
  #nullable enable
  Contact.contactType, IBqlString>.IsNotEqual<
  #nullable disable
  ContactTypesAttribute.bAccountProperty>>>>.Or<BqlOperand<
  #nullable enable
  BAccount.type, IBqlString>.IsIn<
  #nullable disable
  BAccountType.prospectType, BAccountType.customerType, BAccountType.vendorType, BAccountType.combinedType, BAccountType.empCombinedType>>>>, Contact>.ProcessingView Items;
  public PXSetup<CRSetup> Setup;

  public CRGrammProcess()
  {
    if (!new CRGramProcessor().IsRulesDefined)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (CRSetup), new object[1]
      {
        (object) typeof (CRSetup).Name
      });
    PXUIFieldAttribute.SetDisplayName<Contact.displayName>(((PXSelectBase) this.Items).Cache, "Contact");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<Contact>) this.Items).SetProcessDelegate<CRGrammProcess>(CRGrammProcess.\u003C\u003Ec.\u003C\u003E9__4_0 ?? (CRGrammProcess.\u003C\u003Ec.\u003C\u003E9__4_0 = new PXProcessingBase<Contact>.ProcessItemDelegate<CRGrammProcess>((object) CRGrammProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__4_0))));
    ((PXProcessingBase<Contact>) this.Items).ParallelProcessingOptions = (Action<PXParallelProcessingOptions>) (settings => settings.IsEnabled = true);
    ((PXProcessingBase<Contact>) this.Items).SuppressMerge = true;
    ((PXProcessingBase<Contact>) this.Items).SuppressUpdate = true;
  }

  public static bool PersistGrams(PXGraph graph, Contact contact)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.contactDuplicate>())
      return false;
    BAccount baccount1 = BAccount.PK.Find(graph, contact.BAccountID);
    if (contact != null)
    {
      int? nullable = contact.ContactID;
      int num = 1;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        CRGramProcessor crGramProcessor = new CRGramProcessor(graph);
        Contact contact1 = contact;
        Address address;
        if (!(contact.ContactType == "AP"))
        {
          address = Address.PK.Find(graph, contact.DefAddressID);
        }
        else
        {
          PXGraph graph1 = graph;
          int? addressID;
          if (baccount1 == null)
          {
            nullable = new int?();
            addressID = nullable;
          }
          else
            addressID = baccount1.DefAddressID;
          address = Address.PK.Find(graph1, addressID);
        }
        Location location;
        if (!(contact.ContactType == "AP"))
        {
          location = (Location) null;
        }
        else
        {
          PXGraph graph2 = graph;
          int? bAccountID;
          if (baccount1 == null)
          {
            nullable = new int?();
            bAccountID = nullable;
          }
          else
            bAccountID = baccount1.BAccountID;
          int? locationID;
          if (baccount1 == null)
          {
            nullable = new int?();
            locationID = nullable;
          }
          else
            locationID = baccount1.DefLocationID;
          location = Location.PK.Find(graph2, bAccountID, locationID);
        }
        BAccount baccount2 = contact.ContactType == "AP" ? baccount1 : (BAccount) null;
        PXResult<Contact, Address, Location, BAccount> entities = new PXResult<Contact, Address, Location, BAccount>(contact1, address, location, baccount2);
        (bool IsGramsCreated, string NewDuplicateStatus, DateTime? GramValidationDate) tuple = crGramProcessor.PersistGrams((PXResult<Contact>) entities);
        contact.DuplicateStatus = tuple.NewDuplicateStatus;
        contact.GrammValidationDateTime = tuple.GramValidationDate;
        return tuple.IsGramsCreated;
      }
    }
    return false;
  }
}
