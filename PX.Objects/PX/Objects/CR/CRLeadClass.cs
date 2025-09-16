// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRLeadClass
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

/// <exclude />
[PXCacheName("Lead Class")]
[PXPrimaryGraph(typeof (CRLeadClassMaint))]
[Serializable]
public class CRLeadClass : 
  CRBaseClass,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ITargetToContact,
  ITargetToAccount,
  ITargetToOpportunity,
  INotable
{
  [PXSelector(typeof (CRLeadClass.classID))]
  [PXUIField]
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string ClassID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsInternal { get; set; }

  [PXUIField]
  [PXDBString(250, IsUnicode = true)]
  public virtual string Description { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Default Source")]
  [CRMSources(BqlField = typeof (CRLead.source))]
  public virtual string DefaultSource { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Default Owner")]
  [PXDefault("N")]
  [CRDefaultOwner]
  public override string DefaultOwner { get; set; }

  [AssignmentMap(typeof (AssignmentMapType.AssignmentMapTypeLead))]
  [PXDefault]
  [PXUIRequired(typeof (Where<CRLeadClass.defaultOwner, Equal<CRDefaultOwnerAttribute.assignmentMap>>))]
  [PXUIEnabled(typeof (Where<CRLeadClass.defaultOwner, Equal<CRDefaultOwnerAttribute.assignmentMap>>))]
  public override int? DefaultAssignmentMapID { get; set; }

  [PXSelector(typeof (CRContactClass.classID), DescriptionField = typeof (CRContactClass.description), CacheGlobal = true)]
  [PXUIField]
  [PXDBString(10, IsUnicode = true)]
  public virtual string TargetContactClassID { get; set; }

  [PXSelector(typeof (CRCustomerClass.cRCustomerClassID), DescriptionField = typeof (CRCustomerClass.description), CacheGlobal = true)]
  [PXUIField]
  [PXDBString(10, IsUnicode = true)]
  public virtual string TargetBAccountClassID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? RequireBAccountCreation { get; set; }

  [PXSelector(typeof (CROpportunityClass.cROpportunityClassID), DescriptionField = typeof (CROpportunityClass.description), CacheGlobal = true)]
  [PXUIField]
  [PXDBString(10, IsUnicode = true)]
  public virtual string TargetOpportunityClassID { get; set; }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Opportunity Stage")]
  [CROpportunityStages(typeof (CRLeadClass.targetOpportunityClassID), null, OnlyActiveStages = true)]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<CRLeadClass.targetOpportunityClassID, IBqlString>.IsNull>, Null>, CRLeadClass.targetOpportunityStage>))]
  [PXUIEnabled(typeof (Where<BqlOperand<CRLeadClass.targetOpportunityClassID, IBqlString>.IsNotNull>))]
  public virtual string TargetOpportunityStage { get; set; }

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

  public class PK : PrimaryKeyOf<CRLeadClass>.By<CRLeadClass.classID>
  {
    public static CRLeadClass Find(PXGraph graph, string classID, PKFindOptions options = 0)
    {
      return (CRLeadClass) PrimaryKeyOf<CRLeadClass>.By<CRLeadClass.classID>.FindBy(graph, (object) classID, options);
    }
  }

  public static class FK
  {
    public class DefaultAssignmentMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<CRLeadClass>.By<CRLeadClass.defaultAssignmentMapID>
    {
    }

    public class DefaultEmailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<CRLeadClass>.By<CRLeadClass.defaultEMailAccountID>
    {
    }

    public class TargetContactClass : 
      PrimaryKeyOf<CRContactClass>.By<CRContactClass.classID>.ForeignKeyOf<CRLeadClass>.By<CRLeadClass.targetContactClassID>
    {
    }

    public class TargetBusinessAccountClass : 
      PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>.ForeignKeyOf<CRLeadClass>.By<CRLeadClass.targetBAccountClassID>
    {
    }

    public class TargetOpportunityClass : 
      PrimaryKeyOf<CROpportunityClass>.By<CROpportunityClass.cROpportunityClassID>.ForeignKeyOf<CRLeadClass>.By<CRLeadClass.targetOpportunityClassID>
    {
    }
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLeadClass.classID>
  {
  }

  public abstract class isInternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRLeadClass.isInternal>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLeadClass.description>
  {
  }

  public abstract class defaultSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLeadClass.defaultSource>
  {
  }

  public abstract class defaultOwner : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRLeadClass.defaultOwner>
  {
  }

  public abstract class defaultAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRLeadClass.defaultAssignmentMapID>
  {
  }

  public abstract class targetContactClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRLeadClass.targetContactClassID>
  {
  }

  public abstract class targetBAccountClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRLeadClass.targetBAccountClassID>
  {
  }

  public abstract class requireBAccountCreation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRLeadClass.requireBAccountCreation>
  {
  }

  public abstract class targetOpportunityClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRLeadClass.targetOpportunityClassID>
  {
  }

  public abstract class targetOpportunityStage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRLeadClass.targetOpportunityStage>
  {
  }

  public abstract class defaultEMailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRLeadClass.defaultEMailAccountID>
  {
    public class EmailAccountRule : 
      EMailAccount.userID.PreventMakingPersonalIfUsedAsSystem<SelectFromBase<CRLeadClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<CRLeadClass.defaultEMailAccountID>.IsRelatedTo<EMailAccount.emailAccountID>.AsSimpleKey.WithTablesOf<EMailAccount, CRLeadClass>, EMailAccount, CRLeadClass>.SameAsCurrent>>
    {
    }
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRLeadClass.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRLeadClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRLeadClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRLeadClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRLeadClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRLeadClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRLeadClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRLeadClass.lastModifiedDateTime>
  {
  }
}
