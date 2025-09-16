// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CROpportunityClass
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
[PXCacheName("Opportunity Class")]
[PXPrimaryGraph(typeof (CROpportunityClassMaint))]
[Serializable]
public class CROpportunityClass : 
  CRBaseClass,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ITargetToAccount,
  INotable
{
  protected 
  #nullable disable
  string _CROpportunityClassID;
  protected string _Description;
  protected bool? _IsInternal;
  protected bool? _showContactActivities;
  protected string _TargetBAccountClassID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXSelector(typeof (CROpportunityClass.cROpportunityClassID))]
  [PXUIField]
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string CROpportunityClassID
  {
    get => this._CROpportunityClassID;
    set => this._CROpportunityClassID = value;
  }

  [PXUIField]
  [PXDBString(250, IsUnicode = true)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Default Owner")]
  [PXDefault("N")]
  [CRDefaultOwner]
  public override string DefaultOwner { get; set; }

  [AssignmentMap(typeof (AssignmentMapType.AssignmentMapTypeOpportunity))]
  [PXDefault]
  [PXUIRequired(typeof (Where<CROpportunityClass.defaultOwner, Equal<CRDefaultOwnerAttribute.assignmentMap>>))]
  [PXUIEnabled(typeof (Where<CROpportunityClass.defaultOwner, Equal<CRDefaultOwnerAttribute.assignmentMap>>))]
  public override int? DefaultAssignmentMapID { get; set; }

  [EmailAccountRaw]
  [PXForeignReference]
  public virtual int? DefaultEMailAccountID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsInternal
  {
    get => this._IsInternal;
    set => this._IsInternal = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Activities from Source Lead")]
  public virtual bool? ShowContactActivities
  {
    get => this._showContactActivities;
    set => this._showContactActivities = value;
  }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXSelector(typeof (CRContactClass.classID), DescriptionField = typeof (CRContactClass.description), CacheGlobal = true)]
  [PXUIField]
  [PXDBString(10, IsUnicode = true)]
  public virtual string TargetContactClassID { get; set; }

  [PXSelector(typeof (CRCustomerClass.cRCustomerClassID), DescriptionField = typeof (CRCustomerClass.description), CacheGlobal = true)]
  [PXUIField]
  [PXDBString(10, IsUnicode = true)]
  public virtual string TargetBAccountClassID
  {
    get => this._TargetBAccountClassID;
    set => this._TargetBAccountClassID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<CROpportunityClass>.By<CROpportunityClass.cROpportunityClassID>
  {
    public static CROpportunityClass Find(
      PXGraph graph,
      string cROpportunityClassID,
      PKFindOptions options = 0)
    {
      return (CROpportunityClass) PrimaryKeyOf<CROpportunityClass>.By<CROpportunityClass.cROpportunityClassID>.FindBy(graph, (object) cROpportunityClassID, options);
    }
  }

  public static class FK
  {
    public class DefaultAssignmentMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<CROpportunityClass>.By<CROpportunityClass.defaultAssignmentMapID>
    {
    }

    public class DefaultEmailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<CROpportunityClass>.By<CROpportunityClass.defaultEMailAccountID>
    {
    }

    public class TargetContactClass : 
      PrimaryKeyOf<CRContactClass>.By<CRContactClass.classID>.ForeignKeyOf<CROpportunityClass>.By<CROpportunityClass.targetContactClassID>
    {
    }

    public class TargetBusinessAccountClass : 
      PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>.ForeignKeyOf<CROpportunityClass>.By<CROpportunityClass.targetBAccountClassID>
    {
    }
  }

  public abstract class cROpportunityClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityClass.cROpportunityClassID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityClass.description>
  {
  }

  public abstract class defaultOwner : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityClass.defaultOwner>
  {
  }

  public abstract class defaultAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityClass.defaultAssignmentMapID>
  {
  }

  public abstract class defaultEMailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityClass.defaultEMailAccountID>
  {
    public class EmailAccountRule : 
      EMailAccount.userID.PreventMakingPersonalIfUsedAsSystem<SelectFromBase<CROpportunityClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<CROpportunityClass.defaultEMailAccountID>.IsRelatedTo<EMailAccount.emailAccountID>.AsSimpleKey.WithTablesOf<EMailAccount, CROpportunityClass>, EMailAccount, CROpportunityClass>.SameAsCurrent>>
    {
    }
  }

  public abstract class isInternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityClass.isInternal>
  {
  }

  public abstract class showContactActivities : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityClass.showContactActivities>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunityClass.noteID>
  {
  }

  public abstract class targetContactClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityClass.targetContactClassID>
  {
  }

  public abstract class targetBAccountClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityClass.targetBAccountClassID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CROpportunityClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunityClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunityClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityClass.lastModifiedDateTime>
  {
  }
}
