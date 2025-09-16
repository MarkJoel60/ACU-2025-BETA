// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCustomerClass
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

/// <summary>Represents a business account class in CRM.</summary>
/// <remarks>
/// A <i>business account class</i> is a special entity that contains different default sets of additional information about business accounts
/// and may help a user to easily group business accounts into classes.
/// The records of this type are created and edited on the <i>Business Account Classes (CR208000)</i> form,
/// which corresponds to the <see cref="T:PX.Objects.CR.CRCustomerClassMaint" /> graph.
/// </remarks>
[PXCacheName("Business Account Class")]
[PXPrimaryGraph(typeof (CRCustomerClassMaint))]
[Serializable]
public class CRCustomerClass : CRBaseClass, IBqlTable, IBqlTableSystemDataStorage, INotable
{
  protected bool? _IsInternal;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The user-friendly unique identifier of the business account class.
  /// This field is the primary key field.
  /// </summary>
  /// <value>The value can be entered only manually.</value>
  [PXSelector(typeof (CRCustomerClass.cRCustomerClassID))]
  [PXUIField]
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string CRCustomerClassID { get; set; }

  /// <summary>The brief description of the business account class.</summary>
  [PXUIField]
  [PXDBString(250, IsUnicode = true)]
  public virtual string Description { get; set; }

  /// <summary>
  /// The field defines a way that a default owner should be determined for a newly created business accounts of this class.
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
  /// and the workgroup to a newly created business accounts of this class.
  /// </summary>
  /// <value>
  /// The value of this field corresonds to the value of the <see cref="P:PX.Objects.EP.EPAssignmentMap.AssignmentMapID" /> field.
  /// </value>
  [AssignmentMap(typeof (AssignmentMapType.AssignmentMapTypeProspect))]
  [PXDefault]
  [PXUIRequired(typeof (Where<CRCustomerClass.defaultOwner, Equal<CRDefaultOwnerAttribute.assignmentMap>>))]
  [PXUIEnabled(typeof (Where<CRCustomerClass.defaultOwner, Equal<CRDefaultOwnerAttribute.assignmentMap>>))]
  public override int? DefaultAssignmentMapID { get; set; }

  /// <summary>
  /// The identifier of the default email account for this business account class.
  /// </summary>
  /// <value>
  /// The value of the this field corresponds to the value of the <see cref="P:PX.SM.EMailAccount.EmailAccountID" /> field.
  /// </value>
  [EmailAccountRaw]
  [PXForeignReference]
  public virtual int? DefaultEMailAccountID { get; set; }

  /// <summary>
  /// This field indicates that the business accounts of this class are hidden from user of the Self-Service Portal
  /// so that only Acumatica ERP users can view the business accounts.
  /// </summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsInternal
  {
    get => this._IsInternal;
    set => this._IsInternal = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), DescriptionField = typeof (PX.Objects.CM.Currency.description), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency ID")]
  [PXDefault(typeof (FbqlSelect<SelectFromBase<CRCustomerClass, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRSetup>.On<BqlOperand<CRSetup.defaultCustomerClassID, IBqlString>.IsEqual<CRCustomerClass.cRCustomerClassID>>>>, CRCustomerClass>.SearchFor<CRCustomerClass.curyID>))]
  public virtual string CuryID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Enable Currency Override")]
  [PXDefault(true, typeof (FbqlSelect<SelectFromBase<CRCustomerClass, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRSetup>.On<BqlOperand<CRSetup.defaultCustomerClassID, IBqlString>.IsEqual<CRCustomerClass.cRCustomerClassID>>>>, CRCustomerClass>.SearchFor<CRCustomerClass.allowOverrideCury>))]
  public virtual bool? AllowOverrideCury { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>
  {
    public static CRCustomerClass Find(
      PXGraph graph,
      string cRCustomerClassID,
      PKFindOptions options = 0)
    {
      return (CRCustomerClass) PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>.FindBy(graph, (object) cRCustomerClassID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Default Assignment Map</summary>
    public class DefaultAssignmentMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<CRCustomerClass>.By<CRCustomerClass.defaultAssignmentMapID>
    {
    }

    /// <summary>Default Email Account</summary>
    public class DefaultEmailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<CRCustomerClass>.By<CRCustomerClass.defaultEMailAccountID>
    {
    }
  }

  public abstract class cRCustomerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCustomerClass.cRCustomerClassID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCustomerClass.description>
  {
  }

  public abstract class defaultOwner : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCustomerClass.defaultOwner>
  {
  }

  public abstract class defaultAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCustomerClass.defaultAssignmentMapID>
  {
  }

  public abstract class defaultEMailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCustomerClass.defaultEMailAccountID>
  {
    public class EmailAccountRule : 
      EMailAccount.userID.PreventMakingPersonalIfUsedAsSystem<SelectFromBase<CRCustomerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<CRCustomerClass.defaultEMailAccountID>.IsRelatedTo<EMailAccount.emailAccountID>.AsSimpleKey.WithTablesOf<EMailAccount, CRCustomerClass>, EMailAccount, CRCustomerClass>.SameAsCurrent>>
    {
    }
  }

  public abstract class isInternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCustomerClass.isInternal>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCustomerClass.curyID>
  {
  }

  public abstract class allowOverrideCury : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRCustomerClass.allowOverrideCury>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCustomerClass.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRCustomerClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCustomerClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCustomerClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCustomerClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRCustomerClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCustomerClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCustomerClass.lastModifiedDateTime>
  {
  }
}
