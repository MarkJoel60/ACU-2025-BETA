// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRContactClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using PX.SM;
using PX.SM.Email;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>Represents the contact class in CRM.</summary>
/// <remarks>
/// A <i>contact class</i> is a special entity that contains different default sets of additional information about the contacts
/// and may help the user to easily group contacts into classes.
/// Form IDs without dots: <i>Contact Classes (CR205000)</i> form
/// which corresponds to the <see cref="T:PX.Objects.CR.CRContactClassMaint" /> graph.
/// </remarks>
[PXCacheName("Contact Class")]
[PXPrimaryGraph(typeof (CRContactClassMaint))]
[Serializable]
public class CRContactClass : 
  CRBaseClass,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ITargetToLead,
  ITargetToAccount,
  ITargetToOpportunity,
  INotable
{
  /// <summary>
  /// The user-friendly unique identifier of the contact class.
  /// This field is the primary key field.
  /// </summary>
  /// <value>The value can be entered only manually.</value>
  [PXSelector(typeof (CRContactClass.classID))]
  [PXUIField]
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string ClassID { get; set; }

  /// <summary>
  /// This field indicates that the contacts of the class are hidden from user of the Self-Service Portal
  /// so that only Acumatica ERP users can view the contacts.
  /// </summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsInternal { get; set; }

  /// <summary>The brief description of the contact class.</summary>
  [PXUIField]
  [PXDBString(250, IsUnicode = true)]
  public virtual string Description { get; set; }

  /// <summary>
  /// The field defines a way that a default owner should be determined for a newly created contact of this class.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="M:PX.Objects.CR.CRDefaultOwnerAttribute.#ctor" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.CRDefaultOwnerAttribute.DoNotChange" />.
  /// </value>
  [PXDBString]
  [PXUIField(DisplayName = "Default Owner")]
  [PXDefault("N")]
  [CRDefaultOwner]
  public override string DefaultOwner { get; set; }

  /// <summary>
  /// The identifier of the default assignment map that is used to assign the default owner
  /// and the workgroup to a newly created contact of this class.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.EP.EPAssignmentMap.AssignmentMapID" /> field.
  /// </value>
  [AssignmentMap(typeof (AssignmentMapType.AssignmentMapTypeContact))]
  [PXDefault]
  [PXUIRequired(typeof (Where<CRContactClass.defaultOwner, Equal<CRDefaultOwnerAttribute.assignmentMap>>))]
  [PXUIEnabled(typeof (Where<CRContactClass.defaultOwner, Equal<CRDefaultOwnerAttribute.assignmentMap>>))]
  public override int? DefaultAssignmentMapID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.CRLeadClass">lead class</see> that the system inserts by default
  /// if a user creates a lead to be associated with a contact of this class.
  /// The field is included in <see cref="T:PX.Objects.CR.CRContactClass.FK.TargetLeadClass" />.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.CRLeadClass.ClassID" /> field.
  /// </value>
  [PXSelector(typeof (CRLeadClass.classID), DescriptionField = typeof (CRLeadClass.description), CacheGlobal = true)]
  [PXUIField]
  [PXDBString(10, IsUnicode = true)]
  public virtual string TargetLeadClassID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.CRCustomerClass">business account class</see> that the system inserts by default
  /// if a user creates a business account to be associated with a contact of this class.
  /// The field is included in <see cref="T:PX.Objects.CR.CRContactClass.FK.TargetBusinessAccountClass" />.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.CRCustomerClass.CRCustomerClassID" /> field.
  /// </value>
  [PXSelector(typeof (CRCustomerClass.cRCustomerClassID), DescriptionField = typeof (CRCustomerClass.description), CacheGlobal = true)]
  [PXUIField]
  [PXDBString(10, IsUnicode = true)]
  public virtual string TargetBAccountClassID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.CROpportunityClass">opportunity class</see> that the system inserts by default
  /// for a new opportunity if a user creates an opportunity based on a contact of this class.
  /// The field is included in <see cref="T:PX.Objects.CR.CRContactClass.FK.TargetOpportunityClass" />.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.CROpportunityClass.CROpportunityClassID" /> field.
  /// </value>
  [PXSelector(typeof (CROpportunityClass.cROpportunityClassID), DescriptionField = typeof (CROpportunityClass.description), CacheGlobal = true)]
  [PXUIField]
  [PXDBString(10, IsUnicode = true)]
  public virtual string TargetOpportunityClassID { get; set; }

  /// <summary>
  /// The initial stage of an opportunity created from the contact of this class.
  /// This option defines the <see cref="T:PX.Objects.CR.CROpportunityStagesAttribute.CROppClassStage">opportunity stage</see>
  /// that should be set as a default one for a new opportunity created from contact qualification.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.CROpportunityStagesAttribute.CROppClassStage.StageID" /> field.
  /// </value>
  [PXDBString(2)]
  [PXUIField(DisplayName = "Opportunity Stage")]
  [CROpportunityStages(typeof (CRContactClass.targetOpportunityClassID), null, OnlyActiveStages = true)]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<CRContactClass.targetOpportunityClassID, IBqlString>.IsNull>, Null>, CRContactClass.targetOpportunityStage>))]
  [PXUIEnabled(typeof (Where<BqlOperand<CRContactClass.targetOpportunityClassID, IBqlString>.IsNotNull>))]
  public virtual string TargetOpportunityStage { get; set; }

  /// <summary>
  /// The identifier of the default email account for this contact class.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.SM.EMailAccount.EmailAccountID" /> field.
  /// </value>
  [EmailAccountRaw]
  [PXForeignReference]
  public virtual int? DefaultEMailAccountID { get; set; }

  [PXNote]
  [PXUIField]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<CRContactClass>.By<CRContactClass.classID>
  {
    public static CRContactClass Find(PXGraph graph, string classID, PKFindOptions options = 0)
    {
      return (CRContactClass) PrimaryKeyOf<CRContactClass>.By<CRContactClass.classID>.FindBy(graph, (object) classID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Default Assignment Map</summary>
    public class DefaultAssignmentMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<CRContactClass>.By<CRContactClass.defaultAssignmentMapID>
    {
    }

    /// <summary>Default Email Account</summary>
    public class DefaultEmailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<CRContactClass>.By<CRContactClass.defaultEMailAccountID>
    {
    }

    /// <summary>Target Lead Class</summary>
    public class TargetLeadClass : 
      PrimaryKeyOf<CRLeadClass>.By<CRLeadClass.classID>.ForeignKeyOf<CRContactClass>.By<CRContactClass.targetLeadClassID>
    {
    }

    /// <summary>Target Business Account Class</summary>
    public class TargetBusinessAccountClass : 
      PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>.ForeignKeyOf<CRContactClass>.By<CRContactClass.targetBAccountClassID>
    {
    }

    /// <summary>Target Opportunity Class</summary>
    public class TargetOpportunityClass : 
      PrimaryKeyOf<CROpportunityClass>.By<CROpportunityClass.cROpportunityClassID>.ForeignKeyOf<CRContactClass>.By<CRContactClass.targetOpportunityClassID>
    {
    }
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContactClass.classID>
  {
  }

  public abstract class isInternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRContactClass.isInternal>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContactClass.description>
  {
  }

  public abstract class defaultOwner : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRContactClass.defaultOwner>
  {
  }

  public abstract class defaultAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRContactClass.defaultAssignmentMapID>
  {
  }

  public abstract class targetLeadClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRContactClass.targetLeadClassID>
  {
  }

  public abstract class targetBAccountClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRContactClass.targetBAccountClassID>
  {
  }

  public abstract class targetOpportunityClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRContactClass.targetOpportunityClassID>
  {
  }

  public abstract class targetOpportunityStage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRContactClass.targetOpportunityStage>
  {
  }

  public abstract class defaultEMailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRContactClass.defaultEMailAccountID>
  {
    public class EmailAccountRule : 
      EMailAccount.userID.PreventMakingPersonalIfUsedAsSystem<SelectFromBase<CRContactClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<CRContactClass.defaultEMailAccountID>.IsRelatedTo<EMailAccount.emailAccountID>.AsSimpleKey.WithTablesOf<EMailAccount, CRContactClass>, EMailAccount, CRContactClass>.SameAsCurrent>>
    {
    }
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRContactClass.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRContactClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRContactClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRContactClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRContactClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRContactClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRContactClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRContactClass.lastModifiedDateTime>
  {
  }
}
