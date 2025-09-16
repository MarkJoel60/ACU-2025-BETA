// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BAccountCRM
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXPrimaryGraph(typeof (BusinessAccountMaint))]
[CRCacheIndependentPrimaryGraphList(new System.Type[] {typeof (BusinessAccountMaint)}, new System.Type[] {typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<BAccount.bAccountID>>, Or<Current<BAccount.bAccountID>, Less<Zero>>>>)})]
[PXHidden]
[Serializable]
public class BAccountCRM : BAccount
{
  [PXDimensionSelector("BIZACCT", typeof (Search2<BAccountCRM.acctCD, LeftJoin<Contact, On<Contact.bAccountID, Equal<BAccountCRM.bAccountID>, And<Contact.contactID, Equal<BAccountCRM.defContactID>>>, LeftJoin<Address, On<Address.bAccountID, Equal<BAccountCRM.bAccountID>, And<Address.addressID, Equal<BAccountCRM.defAddressID>>>>>, Where2<Where<BAccountCRM.type, Equal<BAccountType.customerType>, Or<BAccountCRM.type, Equal<BAccountType.prospectType>, Or<BAccountCRM.type, Equal<BAccountType.combinedType>, Or<BAccountCRM.type, Equal<BAccountType.vendorType>>>>>, And<Match<Current<AccessInfo.userName>>>>>), typeof (BAccountCRM.acctCD), new System.Type[] {typeof (BAccountCRM.acctCD), typeof (BAccountCRM.acctName), typeof (BAccountCRM.type), typeof (BAccount.classID), typeof (BAccount.status), typeof (Contact.phone1), typeof (Address.city), typeof (Address.countryID), typeof (Contact.eMail)})]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override 
  #nullable disable
  string AcctCD { get; set; }

  [PXDBInt]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  [PXSelector(typeof (Search<Contact.contactID>))]
  public override int? DefContactID { get; set; }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountCRM.bAccountID>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountCRM.acctName>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BAccountCRM.noteID>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountCRM.defLocationID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountCRM.type>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccountCRM.parentBAccountID>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountCRM.defAddressID>
  {
  }

  public new abstract class cOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountCRM.cOrgBAccountID>
  {
  }

  public new abstract class vOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountCRM.vOrgBAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountCRM.acctCD>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountCRM.defContactID>
  {
  }
}
