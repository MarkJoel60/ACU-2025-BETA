// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListMember
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Marketing List Member")]
[Serializable]
public class CRMarketingListMember : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Member Name")]
  [PXSelector(typeof (Search2<Contact.contactID, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<Contact.bAccountID>, And<Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>>, Where<PX.Objects.GL.Branch.bAccountID, IsNull, And<Where<BAccount.bAccountID, IsNull, Or<Where2<Match<BAccount, Current<AccessInfo.userName>>, And<Where<BAccount.defContactID, Equal<Contact.contactID>, Or<Contact.contactType, NotEqual<ContactTypesAttribute.bAccountProperty>>>>>>>>>>), new System.Type[] {typeof (Contact.contactType), typeof (Contact.memberName), typeof (Contact.salutation), typeof (Contact.fullName), typeof (Contact.eMail), typeof (Contact.phone1), typeof (Contact.isActive), typeof (Contact.bAccountID), typeof (Contact.classID), typeof (Contact.lastModifiedDateTime), typeof (Contact.createdDateTime), typeof (Contact.source), typeof (Contact.assignDate), typeof (Contact.duplicateStatus), typeof (Contact.phone2), typeof (Contact.phone3), typeof (Contact.dateOfBirth), typeof (Contact.fax), typeof (Contact.gender), typeof (Contact.method), typeof (Contact.noCall), typeof (Contact.noEMail), typeof (Contact.noFax), typeof (Contact.noMail), typeof (Contact.noMarketing), typeof (Contact.noMassMail), typeof (Contact.campaignID), typeof (Contact.phone1Type), typeof (Contact.phone2Type), typeof (Contact.phone3Type), typeof (Contact.faxType), typeof (Contact.maritalStatus), typeof (Contact.spouse), typeof (Contact.status), typeof (Contact.resolution), typeof (Contact.languageID), typeof (Contact.ownerID)})]
  [PXParent(typeof (Select<Contact, Where<Contact.contactID, Equal<Current<CRMarketingListMember.contactID>>>>))]
  public virtual int? ContactID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (CRMarketingList.marketingListID))]
  [PXUIField(DisplayName = "Marketing List ID", Visible = true)]
  [PXSelector(typeof (Search<CRMarketingList.marketingListID, Where<CRMarketingList.type, Equal<CRMarketingList.type.@static>>>), DescriptionField = typeof (CRMarketingList.mailListCode))]
  public virtual int? MarketingListID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Subscribed")]
  public virtual bool? IsSubscribed { get; set; }

  /// <summary>
  /// A calculated field that indicates (if set to <c>false</c>) that the record exists in the database.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Virtual", Visible = false, Enabled = false)]
  public virtual bool? IsVirtual { get; set; }

  /// <summary>Represents the type of the marketing list.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.CRMarketingList.type" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.CRMarketingList.type.Static" />.
  /// </value>
  [PXString]
  [PXDefault("S")]
  [PXUIField]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PXDBString]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Format")]
  [NotificationFormat.TemplateList]
  public virtual string Format { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CRMarketingListMember>.By<CRMarketingListMember.marketingListID, CRMarketingListMember.contactID>
  {
    public static CRMarketingListMember Find(
      PXGraph graph,
      int? marketingListID,
      int? contactID,
      PKFindOptions options = 0)
    {
      return (CRMarketingListMember) PrimaryKeyOf<CRMarketingListMember>.By<CRMarketingListMember.marketingListID, CRMarketingListMember.contactID>.FindBy(graph, (object) marketingListID, (object) contactID, options);
    }
  }

  public static class FK
  {
    public class MarketingList : 
      PrimaryKeyOf<CRMarketingList>.By<CRMarketingList.marketingListID>.ForeignKeyOf<CRMarketingListMember>.By<CRMarketingListMember.marketingListID>
    {
    }
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRMarketingListMember.contactID>
  {
  }

  public abstract class marketingListID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRMarketingListMember.marketingListID>
  {
  }

  public abstract class isSubscribed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRMarketingListMember.isSubscribed>
  {
  }

  public abstract class isVirtual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRMarketingListMember.isVirtual>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingListMember.format>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRMarketingListMember.selected>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingListMember.createdByScreenID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRMarketingListMember.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRMarketingListMember.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRMarketingListMember.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingListMember.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRMarketingListMember.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRMarketingListMember.Tstamp>
  {
  }
}
