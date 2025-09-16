// Decompiled with JetBrains decompiler
// Type: PX.TM.EPCompanyTreeMember
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.TM;

/// <summary>Represents a member of a workgroup.</summary>
/// <remarks>
/// Company tree is a model of your organization's hierarchy that includes temporary and permanent workgroups.
/// The company tree may reflect the administrative hierarchy and include sub-hierarchies of workgroups created within
/// specific branches or departments. The company tree is used for creating assignment rules in approval and assignment
/// maps and for determining the scope of the users who want to view items assigned to them.
/// The records of this type are created and edited on the <i>Company Tree (EP204061)</i> form,
/// which corresponds to the <see cref="!:CompanyTreeMaint" /> graph,
/// and imported on the <i>Import Company Tree (EP204060)</i> form, which
/// corresponds to the <see cref="!:ImportCompanyTreeMaint" /> graph.
/// </remarks>
[Serializable]
public class EPCompanyTreeMember : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _WorkGroupID;
  protected int? _ContactID;
  protected int? _WaitTime;
  protected bool? _IsOwner;
  protected bool? _Active;
  protected 
  #nullable disable
  string _MembershipType;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  /// <inheritdoc cref="P:PX.TM.EPCompanyTree.WorkGroupID" />
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (EPCompanyTree.workGroupID))]
  [PXParent(typeof (Select<EPCompanyTree, Where<EPCompanyTree.workGroupID, Equal<Current<EPCompanyTreeMember.workGroupID>>>>))]
  public virtual int? WorkGroupID
  {
    get => this._WorkGroupID;
    set => this._WorkGroupID = value;
  }

  /// <summary>The identifier of the member of the workgroup.</summary>
  /// <value>
  /// Corresponds to the value of <see cref="!:PX.CR.Contact.ContactID" />
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Contact", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  /// <inheritdoc cref="P:PX.TM.EPCompanyTree.WaitTime" />
  [PXDBTimeSpanLong(Format = TimeSpanFormatType.DaysHoursMinites)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Wait Time")]
  public virtual int? WaitTime
  {
    get => this._WaitTime;
    set => this._WaitTime = value;
  }

  /// <summary>
  /// Specifies whether the member is the owner of the workgroup.
  /// </summary>
  /// 
  ///             /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Owner")]
  public virtual bool? IsOwner
  {
    get => this._IsOwner;
    set => this._IsOwner = value;
  }

  /// <summary>
  /// Specifies whether the user is an active member of the workgroup.
  /// </summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  /// <summary>The workgroup membership type of the user.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.TM.MembershipTypeListAttribute" /> class.
  /// The default value is <see cref="F:PX.TM.MembershipTypeListAttribute.Permanent" />
  /// </value>
  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Membership Type")]
  [PXDefault("PERM")]
  [MembershipTypeList]
  public virtual string MembershipType
  {
    get => this._MembershipType;
    set => this._MembershipType = value;
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
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeMember.workGroupID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeMember.contactID>
  {
  }

  public abstract class waitTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeMember.waitTime>
  {
  }

  public abstract class isOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPCompanyTreeMember.isOwner>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPCompanyTreeMember.active>
  {
  }

  public abstract class membershipType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCompanyTreeMember.membershipType>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPCompanyTreeMember.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCompanyTreeMember.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPCompanyTreeMember.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPCompanyTreeMember.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCompanyTreeMember.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EPCompanyTreeMember.lastModifiedDateTime>
  {
  }
}
