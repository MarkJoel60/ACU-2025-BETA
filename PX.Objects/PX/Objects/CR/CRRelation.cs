// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRRelation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.SM;
using PX.TM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Relations")]
[DebuggerDisplay("{GetType().Name,nq} (ID = {RelationID}, Role = {Role}): {RefEntityType}: {RefNoteID} => {TargetType}: {TargetNoteID}, BAccountID = {EntityID}, ContactID = {ContactID}")]
[Serializable]
public class CRRelation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  [PXUIField(Visible = false)]
  [PXReverseRelation]
  public virtual int? RelationID { get; set; }

  [PXParent(typeof (Select<ARInvoice, Where<ARInvoice.noteID, Equal<Current<CRRelation.refNoteID>>>>))]
  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBString(40)]
  [PXUIField(DisplayName = "Ref Type")]
  [PXDefault]
  [CRRelationTypeListAttribure(typeof (CRRelation.role), null, null)]
  [PXFormula(typeof (Default<CRRelation.role>))]
  public virtual 
  #nullable disable
  string RefEntityType { get; set; }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Role")]
  [PXDefault]
  [CRRoleTypeList.FullList]
  [PXUIEnabled(typeof (Where<Not<IsInDatabase>>))]
  public virtual string Role { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Primary")]
  [PXDefault(false)]
  public virtual bool? IsPrimary { get; set; }

  [PXDBString(40)]
  [PXUIField(DisplayName = "Type")]
  [PXDefault]
  [CRRelationTypeListAttribure(typeof (CRRelation.role), null, null)]
  [PXFormula(typeof (Default<CRRelation.role>))]
  [PXUIEnabled(typeof (Where<Not<IsInDatabase>>))]
  public virtual string TargetType { get; set; }

  [EntityIDSelector(typeof (CRRelation.targetType))]
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Document")]
  [PXFormula(typeof (Default<CRRelation.targetType>))]
  [PXUIEnabled(typeof (Where<BqlOperand<CRRelation.role, IBqlString>.IsNotIn<CRRoleTypeList.referrer, CRRoleTypeList.supervisor, CRRoleTypeList.businessUser, CRRoleTypeList.decisionMaker, CRRoleTypeList.technicalExpert, CRRoleTypeList.supportEngineer, CRRoleTypeList.evaluator, CRRoleTypeList.licensee>>))]
  public virtual Guid? TargetNoteID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? DocNoteID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<BAccount.bAccountID, Where<BAccount.type, In3<BAccountType.prospectType, BAccountType.customerType, BAccountType.vendorType, BAccountType.combinedType>, And<Match<Current<AccessInfo.userName>>>>>), new System.Type[] {typeof (BAccount.acctCD), typeof (BAccount.acctName), typeof (BAccount.classID), typeof (BAccount.type), typeof (BAccount.parentBAccountID), typeof (BAccount.acctReferenceNbr)}, SubstituteKey = typeof (BAccount.acctCD), DescriptionField = typeof (BAccount.acctName), Filterable = true, DirtyRead = true)]
  [PXUIField(DisplayName = "Account")]
  [PXFormula(typeof (Default<CRRelation.targetNoteID>))]
  [CRRelationAccount]
  public virtual int? EntityID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact")]
  [PXFormula(typeof (Default<CRRelation.targetNoteID, CRRelation.entityID>))]
  [CRRelationContactSelector]
  public virtual int? ContactID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Add to CC")]
  public virtual bool? AddToCC { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Creator", Enabled = false)]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created At", Enabled = false)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By", Enabled = false)]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Account/Employee", Enabled = false)]
  public virtual string EntityCD { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Name", Enabled = false)]
  public virtual string Name { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Contact", Enabled = false)]
  public virtual string ContactName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Email", Enabled = false)]
  public virtual string Email { get; set; }

  /// <summary>Status of the related entity.</summary>
  [PXString]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string Status { get; set; }

  /// <summary>Description of the related entity.</summary>
  [PXString]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string Description { get; set; }

  /// <summary>An owner of the related entity.</summary>
  [Owner(IsDBField = false, Enabled = false)]
  public virtual int? OwnerID { get; set; }

  /// <summary>Document date of the related entity.</summary>
  [PXDate]
  [PXUIField(DisplayName = "Document Date", Enabled = false)]
  public virtual DateTime? DocumentDate { get; set; }

  public static void FillUnboundData(
    CRRelation relation,
    Contact contact,
    BAccount businessAccount,
    Users user)
  {
    relation.Name = businessAccount?.AcctName;
    relation.EntityCD = businessAccount?.AcctCD;
    relation.Email = contact?.EMail;
    if (businessAccount?.Type != "EP")
    {
      relation.ContactName = contact?.DisplayName;
    }
    else
    {
      if (string.IsNullOrEmpty(relation.Name))
        relation.Name = user?.FullName;
      if (!string.IsNullOrEmpty(relation.Email))
        return;
      relation.Email = user?.Email;
    }
  }

  public class PK : PrimaryKeyOf<CRRelation>.By<CRRelation.relationID>
  {
    public static CRRelation Find(PXGraph graph, int? relationID, PKFindOptions options = 0)
    {
      return (CRRelation) PrimaryKeyOf<CRRelation>.By<CRRelation.relationID>.FindBy(graph, (object) relationID, options);
    }
  }

  public class UK : 
    PrimaryKeyOf<CRRelation>.By<CRRelation.refNoteID, CRRelation.targetNoteID, CRRelation.role>
  {
    public static CRRelation Find(
      PXGraph graph,
      Guid? refNoteID,
      Guid? targetNoteID,
      string role,
      PKFindOptions options = 0)
    {
      return (CRRelation) PrimaryKeyOf<CRRelation>.By<CRRelation.refNoteID, CRRelation.targetNoteID, CRRelation.role>.FindBy(graph, (object) refNoteID, (object) targetNoteID, (object) role, options);
    }
  }

  public abstract class relationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRRelation.relationID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRRelation.refNoteID>
  {
  }

  public abstract class refEntityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.refEntityType>
  {
  }

  public abstract class role : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.role>
  {
  }

  public abstract class isPrimary : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRRelation.isPrimary>
  {
  }

  public abstract class targetType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.targetType>
  {
  }

  public abstract class targetNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRRelation.targetNoteID>
  {
  }

  public abstract class docNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRRelation.docNoteID>
  {
  }

  public abstract class entityID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRRelation.entityID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRRelation.contactID>
  {
  }

  public abstract class addToCC : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRRelation.addToCC>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRRelation.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRRelation.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRRelation.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRRelation.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRRelation.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRRelation.lastModifiedDateTime>
  {
  }

  public abstract class entityCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.entityCD>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.name>
  {
  }

  public abstract class contactName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.contactName>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.email>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.status>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.description>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.ownerID>
  {
  }

  public abstract class documentDate : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRRelation.documentDate>
  {
  }
}
