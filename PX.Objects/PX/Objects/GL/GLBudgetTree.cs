// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLBudgetTree
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Represents a node in the template budget structure. The structure defines the tree of budget articles and is used
/// when articles are preloaded into a new budget (see <see cref="T:PX.Objects.GL.GLBudgetLine" />).
/// Records of this type don't maintain any budget amounts and are used only to define the structure of a budget,
/// which, however, can be altered in any budget for a particular ledger, branch and year.
/// 
/// The class is used for both group (see <see cref="P:PX.Objects.GL.GLBudgetTree.IsGroup" />) and leaf nodes.
/// To maintain tree structure, the class stores a link to the parent node in the<see cref="P:PX.Objects.GL.GLBudgetTree.ParentGroupID" /> field.
/// 
/// Records of this type are created on the Budget Configuration (GL205000) form (see the <see cref="T:PX.Objects.GL.GLBudgetTreeMaint" /> graph).
/// </summary>
[PXPrimaryGraph(typeof (GLBudgetTreeMaint))]
[PXCacheName("GL Budget Tree")]
[Serializable]
public class GLBudgetTree : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted
{
  protected Guid? _GroupID;
  protected Guid? _ParentGroupID;
  protected int? _SortOrder;
  protected bool? _IsGroup;
  protected bool? _Rollup;
  protected int? _AccountID;
  protected 
  #nullable disable
  string _Description;
  protected int? _SubID;
  protected string _AccountMask;
  protected string _SubMask;
  protected bool? _Secured;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;
  protected bool? _Included;

  /// <summary>The unique identifier of the budget tree node.</summary>
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual Guid? GroupID
  {
    get => this._GroupID;
    set => this._GroupID = value;
  }

  /// <summary>The identifier of the parent budget tree node.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.GLBudgetTree.GroupID" /> field.
  /// The value is equal to <see cref="F:System.Guid.Empty" /> for the nodes on the first level of the tree.
  /// </value>
  [PXDBGuid(false)]
  [PXUIField]
  public virtual Guid? ParentGroupID
  {
    get => this._ParentGroupID;
    set => this._ParentGroupID = value;
  }

  /// <summary>
  /// An integer field that defines the order in which budget tree nodes that belong to one <see cref="P:PX.Objects.GL.GLBudgetTree.ParentGroupID">parent</see>
  /// appear relative to each other.
  /// The value of this field is changed by users when they move budget tree nodes by using the corresponding actions on the Budget Configuration (GL205000) form.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  [PXUIField]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  /// <summary>
  /// The identifier of the GL <see cref="T:PX.Objects.GL.Account">account</see> of the budget tree node.
  /// </summary>
  /// <value>
  /// The value of the field can be empty for the group nodes(see<see cref="P:PX.Objects.GL.GLBudgetTree.IsGroup" />).
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Node")]
  [PXDefault(false)]
  public virtual bool? IsGroup
  {
    get => this._IsGroup;
    set => this._IsGroup = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Rollup", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? Rollup
  {
    get => this._Rollup;
    set => this._Rollup = value;
  }

  /// <summary>
  /// Identifier of the GL <see cref="T:PX.Objects.GL.Account" /> of the budget tree node.
  /// May be empty for the group (see <see cref="P:PX.Objects.GL.GLBudgetTree.IsGroup" />) nodes.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(null, typeof (Search<Account.accountID, Where<Account.accountCD, Like<Current<SelectedNode.accountMaskWildcard>>>, OrderBy<Asc<Account.accountCD>>>))]
  [PXRestrictor(typeof (Where<Account.active, Equal<True>>), "Account is inactive.", new Type[] {}, ReplaceInherited = true)]
  [PXUIField]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>The description of the budget tree node.</summary>
  /// <value>
  /// Defaults to the description of the <see cref="P:PX.Objects.GL.GLBudgetTree.AccountID">account</see>, but can be overwritten by a user.
  /// </value>
  [PXDBString(150, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Required = true)]
  [PXDefault(typeof (Search<Account.description, Where<Account.accountID, Equal<Current<GLBudgetTree.accountID>>>>))]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// The identifier of the GL <see cref="T:PX.Objects.GL.Sub">subaccount</see> of the budget tree node.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// The value of the field can be empty for the group (see <see cref="P:PX.Objects.GL.GLBudgetTree.IsGroup" />) nodes.
  /// </value>
  [SubAccount]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// For a group node (see <see cref="P:PX.Objects.GL.GLBudgetTree.IsGroup" />), defines the mask for selection of the child budget tree nodes by their accounts.
  /// The selector on the <see cref="P:PX.Objects.GL.GLBudgetTree.AccountID" /> field of the child nodes allows only accounts,
  /// whose <see cref="P:PX.Objects.GL.Account.AccountCD" /> matches the specified mask.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Mask", Required = false)]
  [PXDefault(typeof (Search<Account.accountCD, Where<Account.accountID, Equal<Current<GLBudgetTree.accountID>>>>))]
  public virtual string AccountMask
  {
    get => this._AccountMask;
    set => this._AccountMask = value;
  }

  /// <summary>
  /// For a group node (see <see cref="P:PX.Objects.GL.GLBudgetTree.IsGroup" />), defines the mask for selection of the child budget tree nodes by their subaccounts.
  /// The selector on the <see cref="P:PX.Objects.GL.GLBudgetTree.SubID" /> field of the child nodes allows only subaccounts,
  /// whose <see cref="P:PX.Objects.GL.Sub.SubCD" /> matches the specified mask.
  /// </summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Subaccount Mask", Required = false)]
  public virtual string SubMask
  {
    get => this._SubMask;
    set => this._SubMask = value;
  }

  /// <summary>
  /// An unbound field that indicates (if set to <c>true</c>) that access to the budget tree node is restricted, because the node is included into a restriction group or groups.
  /// </summary>
  /// <value>
  /// This field is populated only in the context of the Budget Configuration (GL205000) form (<see cref="T:PX.Objects.GL.GLBudgetTreeMaint" />).
  /// </value>
  [PXBool]
  [PXUIField(DisplayName = "Secured", Enabled = false)]
  [PXUnboundDefault(false)]
  public virtual bool? Secured
  {
    get => this._Secured;
    set => this._Secured = value;
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

  [PXDBTimestamp]
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  /// <summary>
  /// The group mask showing which <see cref="T:PX.SM.RelationGroup">restriction groups</see> the budget tree node belongs to.
  /// To learn more about the way restriction groups are managed, see the documentation for the GL Account Access (GL104000) form
  /// (which corresponds to the <see cref="T:PX.Objects.GL.GLAccess" /> graph).
  /// </summary>
  [PXDBGroupMask]
  public virtual byte[] GroupMask { get; set; }

  /// <summary>
  /// An unbound field that is used in the user interface to include the budget tree node into a <see cref="T:PX.SM.RelationGroup">restriction group</see>.
  /// Also see <see cref="P:PX.Objects.GL.GLBudgetTree.GroupMask" />.
  /// </summary>
  [PXUnboundDefault(false)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  public class PK : PrimaryKeyOf<GLBudgetTree>.By<GLBudgetTree.groupID>
  {
    public static GLBudgetTree Find(PXGraph graph, Guid? groupID, PKFindOptions options = 0)
    {
      return (GLBudgetTree) PrimaryKeyOf<GLBudgetTree>.By<GLBudgetTree.groupID>.FindBy(graph, (object) groupID, options);
    }
  }

  public static class FK
  {
    public class ParentBudgetTree : 
      PrimaryKeyOf<GLBudgetTree>.By<GLBudgetTree.groupID>.ForeignKeyOf<GLBudgetTree>.By<GLBudgetTree.parentGroupID>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLBudgetTree>.By<GLBudgetTree.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLBudgetTree>.By<GLBudgetTree.subID>
    {
    }
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudgetTree.groupID>
  {
  }

  public abstract class parentGroupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudgetTree.parentGroupID>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetTree.sortOrder>
  {
  }

  public abstract class isGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetTree.isGroup>
  {
  }

  public abstract class rollup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetTree.rollup>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetTree.accountID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLBudgetTree.description>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetTree.subID>
  {
  }

  public abstract class accountMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLBudgetTree.accountMask>
  {
  }

  public abstract class subMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLBudgetTree.subMask>
  {
  }

  public abstract class secured : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetTree.secured>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudgetTree.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLBudgetTree.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLBudgetTree.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLBudgetTree.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLBudgetTree.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLBudgetTree.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLBudgetTree.tStamp>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLBudgetTree.groupMask>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetTree.included>
  {
  }
}
