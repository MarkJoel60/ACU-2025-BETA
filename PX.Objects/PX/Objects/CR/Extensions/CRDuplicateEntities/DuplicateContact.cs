// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.DuplicateContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

/// <exclude />
[PXHidden]
[Serializable]
public class DuplicateContact : Contact
{
  [PXDBInt]
  [PXUIField(DisplayName = "Business Account")]
  [PXSelector(typeof (BAccount.bAccountID), SubstituteKey = typeof (BAccount.acctCD))]
  public override int? BAccountID { get; set; }

  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.lastName), typeof (Contact.firstName), typeof (Contact.midName), typeof (Contact.title)})]
  [PersonDisplayName(typeof (Contact.lastName), typeof (Contact.firstName), typeof (Contact.midName), typeof (Contact.title))]
  [PXDefault]
  [PXUIRequired(typeof (Where<Contact.contactType, Equal<ContactTypesAttribute.lead>, Or<Contact.contactType, Equal<ContactTypesAttribute.person>>>))]
  [PXNavigateSelector(typeof (Search2<Contact.displayName, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.contactID>>>, Where2<Where<Contact.contactType, Equal<ContactTypesAttribute.lead>, Or<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>, And<Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>>>>))]
  [PXPersonalDataField]
  public override 
  #nullable disable
  string DisplayName { get; set; }

  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DuplicateContact.contactID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DuplicateContact.bAccountID>
  {
  }

  public new abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DuplicateContact.displayName>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DuplicateContact.defAddressID>
  {
  }

  public new abstract class contactPriority : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DuplicateContact.contactPriority>
  {
  }

  public new abstract class duplicateStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DuplicateContact.duplicateStatus>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DuplicateContact.status>
  {
  }

  public new abstract class contactType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DuplicateContact.contactType>
  {
  }

  public new abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DuplicateContact.isActive>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DuplicateContact.noteID>
  {
  }
}
