// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactAccountLead
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Workflows;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
[PXProjection(typeof (SelectFromMirror<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<Contact.bAccountID>>>>.LeftJoin<CRLead>.On<BqlOperand<CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>), Persistent = false)]
public class ContactAccountLead : ContactAccount
{
  [PXDBString(1, IsFixed = true, BqlField = typeof (CRLead.status), BqlTable = typeof (CRLead))]
  [PXDefault]
  [PXUIField]
  [LeadWorkflow.States.List(BqlTable = typeof (CRLead))]
  public virtual 
  #nullable disable
  string LeadStatus { get; set; }

  [PXString]
  [PXDBCalced(typeof (BqlOperand<Contact.status, IBqlString>.When<BqlOperand<Contact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>.ElseNull), typeof (string))]
  [ContactTypes(BqlField = typeof (Contact.status))]
  public virtual string RawContactStatus { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Status")]
  [CoalesceCombinedDBStringLists(typeof (ContactAccountLead), new System.Type[] {typeof (ContactAccountLead.leadStatus), typeof (ContactAccountLead.status), typeof (ContactAccountLead.accountStatus)})]
  public virtual string AggregatedStatus { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (CRLead.contactType), BqlTable = typeof (CRLead))]
  [PXStringList(new string[] {"LD"}, new string[] {"Lead"})]
  public virtual string RawLeadType { get; set; }

  [PXString]
  [PXDBCalced(typeof (BqlOperand<Contact.contactType, IBqlString>.When<BqlOperand<Contact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>.ElseNull), typeof (string))]
  [PXStringList(new string[] {"PN"}, new string[] {"Contact"})]
  public virtual string RawContactType { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Type")]
  [CoalesceCombinedDBStringLists(typeof (ContactAccountLead), new System.Type[] {typeof (ContactAccountLead.rawLeadType), typeof (ContactAccountLead.rawContactType), typeof (ContactAccountLead.type)})]
  public virtual string AggregatedType { get; set; }

  public new abstract class accountStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactAccountLead.accountStatus>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAccountLead.status>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAccountLead.type>
  {
  }

  public abstract class leadStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAccountLead.leadStatus>
  {
  }

  public abstract class rawContactStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactAccountLead.rawContactStatus>
  {
  }

  public abstract class aggregatedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactAccountLead.aggregatedStatus>
  {
  }

  public abstract class rawLeadType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactAccountLead.rawLeadType>
  {
  }

  public abstract class rawContactType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactAccountLead.rawContactType>
  {
  }

  public abstract class aggregatedType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactAccountLead.aggregatedType>
  {
  }
}
