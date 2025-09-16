// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLBudgetLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Represents a budget article. The class is used for both group (see <see cref="P:PX.Objects.GL.GLBudgetLine.IsGroup" />) and leaf articles.
/// To maintain tree structure, the class stores a link to the parent budget article in the <see cref="P:PX.Objects.GL.GLBudgetLine.ParentGroupID" /> field.
/// 
/// The budget article holds the amount allocated to a particular account-subaccount pair
/// (or a group of accounts and subaccounts, such as <see cref="P:PX.Objects.GL.GLBudgetLine.AccountMask" />) for a particular year.
/// Distribution of this amount between the periods of the year is stored in the corresponding <see cref="T:PX.Objects.GL.GLBudgetLineDetail" /> records.
/// 
/// Records of this type are created on the Budgets (GL302010) form (see the <see cref="T:PX.Objects.GL.GLBudgetEntry" /> graph)
/// either manually or by the preload mechanism that creates GLBudgetLine from <see cref="T:PX.Objects.GL.GLBudgetTree" /> records."/&gt;
/// </summary>
[PXPrimaryGraph(typeof (GLBudgetEntry), Filter = typeof (BudgetFilter))]
[PXCacheName("Budget Article")]
[Serializable]
public class GLBudgetLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected Guid? _GroupID;
  protected Guid? _ParentGroupID;
  protected bool? _Rollup;
  protected bool? _IsGroup;
  protected bool? _IsPreloaded;
  protected Decimal? _AllocatedAmount;
  protected Decimal? _ReleasedAmount;
  protected 
  #nullable disable
  string _AccountMask;
  protected string _SubMask;
  protected bool? _Comparison;
  protected byte[] _GroupMask;
  public Decimal[] Allocated;
  public Decimal[] _Compared;
  protected int? _TreeSortOrder;

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// The unique identifier of the budget article.
  /// This field is a part of the compound key.
  /// </summary>
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual Guid? GroupID
  {
    get => this._GroupID;
    set => this._GroupID = value;
  }

  /// <summary>
  /// The identifier of the parent <see cref="T:PX.Objects.GL.GLBudgetLine">budget article</see>.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.GLBudgetLine.GroupID" /> field of the parent.
  /// The value is equal to <see cref="F:System.Guid.Empty" /> for the nodes on the first level of the tree.
  /// </value>
  [PXDBGuid(false)]
  [PXDefault]
  [PXUIField]
  public virtual Guid? ParentGroupID
  {
    get => this._ParentGroupID;
    set => this._ParentGroupID = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? Rollup
  {
    get => this._Rollup;
    set => this._Rollup = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the budget article represents a group of other articles.
  /// That is, the article has child articles, which are linked to this article by their <see cref="P:PX.Objects.GL.GLBudgetLine.ParentGroupID" /> field.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Node", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? IsGroup
  {
    get => this._IsGroup;
    set => this._IsGroup = value;
  }

  /// Specifies (if set to <c>true</c>
  /// ) that the budget article was created from a
  ///             <see cref="T:PX.Objects.GL.GLBudgetTree">budget tree configuration node</see>
  ///  by the budget tree
  ///             preload process. (The preload process is invoked when a user creates a budget for a new year (see <see cref="M:PX.Objects.GL.GLBudgetEntry.BudgetFilter_RowUpdated(PX.Data.PXCache,PX.Data.PXRowUpdatedEventArgs)" />
  /// )
  ///             or converts an existing budget (see <see cref="M:PX.Objects.GL.GLBudgetEntry.ConvertBudget" />
  /// ).)
  [PXDBBool]
  [PXUIField(DisplayName = "Preloaded", Visible = false, Enabled = false)]
  [PXDefault(false)]
  public virtual bool? IsPreloaded
  {
    get => this._IsPreloaded;
    set => this._IsPreloaded = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the budget article belongs.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(typeof (BudgetFilter.branchID), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Ledger">ledger</see> to which the budget article belongs.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (BudgetFilter.ledgerID))]
  [PXUIField]
  [PXSelector(typeof (Ledger.ledgerID), SubstituteKey = typeof (Ledger.ledgerCD), CacheGlobal = true)]
  public virtual int? LedgerID { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.GL.GLBudgetLine.FinYear">financial year</see> to which the budget article belongs.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:FinYear.year" /> field.
  /// </value>
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(Visible = false, DisplayName = "Financial Year")]
  [PXDefault(typeof (BudgetFilter.finYear))]
  public virtual string FinYear { get; set; }

  /// <summary>
  /// The identifier of the GL <see cref="T:PX.Objects.GL.Account">account</see> of the budget article.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// The value of the field can be empty for the group articles (see <see cref="P:PX.Objects.GL.GLBudgetLine.IsGroup" />).
  /// </value>
  [Account(null, typeof (Search<Account.accountID, Where<Account.accountCD, Like<Current<SelectedGroup.accountMaskWildcard>>>, OrderBy<Asc<Account.accountCD>>>))]
  [PXRestrictor(typeof (Where<Account.active, Equal<True>>), "Account is inactive.", new Type[] {}, ReplaceInherited = true)]
  [PXDefault]
  public virtual int? AccountID { get; set; }

  /// <summary>
  /// The identifier of the GL <see cref="T:PX.Objects.GL.Sub">subaccount</see> of the budget article.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// Can be empty for the group articles (see <see cref="P:PX.Objects.GL.GLBudgetLine.IsGroup" />).
  /// </value>
  [SubAccount]
  [PXRestrictor(typeof (Where<Sub.active, Equal<True>>), "Subaccount {0} is inactive.", new Type[] {typeof (Sub.subCD)})]
  [PXDefault]
  public virtual int? SubID { get; set; }

  /// <summary>The description of the budget article.</summary>
  /// <value>
  /// Defaults to the description of the <see cref="P:PX.Objects.GL.GLBudgetLine.AccountID">account</see>, but can be overwritten by user.
  /// </value>
  [PXDBString(150, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXDefault(typeof (Search<Account.description, Where<Account.accountID, Equal<Current<GLBudgetLine.accountID>>>>))]
  public virtual string Description { get; set; }

  /// <summary>
  /// The amount that is budgeted for the article for a particular <see cref="P:PX.Objects.GL.GLBudgetLine.FinYear">year</see>.
  /// </summary>
  /// <value>
  /// The value can be edited by a user and indicates the decision to allocate the specified amount
  /// to the particular budget article.
  /// After the article is released, the amount can still be edited. If the amount is changed, the article will be marked as unreleased (see <see cref="P:PX.Objects.GL.GLBudgetLine.Released" />)
  /// and can be released again, which will result in updating the budget ledger figures to match the current state of the article.
  /// </value>
  [PXDBDecimal(typeof (Search2<PX.Objects.CM.Currency.decimalPlaces, InnerJoin<Ledger, On<Ledger.baseCuryID, Equal<PX.Objects.CM.Currency.curyID>>>, Where<Ledger.ledgerID, Equal<Current<GLBudgetLine.ledgerID>>>>))]
  [PXUIField(DisplayName = "Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Amount { get; set; }

  /// <summary>
  /// The total amount of the budget article distributed between the periods of the year.
  /// </summary>
  /// <value>
  /// The value of this field is calculated as a sum of amounts of the detail lines of the budget article (see <see cref="P:PX.Objects.GL.GLBudgetLineDetail.Amount" />)
  /// and cannot be edited by a user.
  /// A budget article cannot be released until its <see cref="P:PX.Objects.GL.GLBudgetLine.Amount" /> and <see cref="P:PX.Objects.GL.GLBudgetLine.AllocatedAmount" /> are equal to each other.
  /// </value>
  [PXDBDecimal(typeof (Search2<PX.Objects.CM.Currency.decimalPlaces, InnerJoin<Ledger, On<Ledger.baseCuryID, Equal<PX.Objects.CM.Currency.curyID>>>, Where<Ledger.ledgerID, Equal<Current<GLBudgetLine.ledgerID>>>>))]
  [PXUIField(DisplayName = "Distributed Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AllocatedAmount
  {
    get => this._AllocatedAmount;
    set => this._AllocatedAmount = value;
  }

  /// <summary>
  /// The currently released amount of the article, which matches the figures in the budget ledger.
  /// </summary>
  /// <value>
  /// This field is updated with the value of the <see cref="P:PX.Objects.GL.GLBudgetLine.Amount" /> field upon release of the budget article.
  /// The difference between the values of this field and the <see cref="P:PX.Objects.GL.GLBudgetLine.Amount" /> field shows the difference
  /// between the current state of the article and the corresponding figures in the budget ledger.
  /// </value>
  [PXDBDecimal(typeof (Search2<PX.Objects.CM.Currency.decimalPlaces, InnerJoin<Ledger, On<Ledger.baseCuryID, Equal<PX.Objects.CM.Currency.curyID>>>, Where<Ledger.ledgerID, Equal<Current<GLBudgetLine.ledgerID>>>>))]
  [PXUIField(DisplayName = "Released Amount", Enabled = false, Visible = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ReleasedAmount
  {
    get => this._ReleasedAmount;
    set => this._ReleasedAmount = value;
  }

  /// <summary>
  /// For a group article (see <see cref="P:PX.Objects.GL.GLBudgetLine.IsGroup" />), defines the mask for selection of the child budget articles by their accounts.
  /// The selector on the <see cref="P:PX.Objects.GL.GLBudgetLine.AccountID" /> field of the child articles allows only accounts
  /// whose <see cref="P:PX.Objects.GL.Account.AccountCD" /> matches the specified mask.
  /// </summary>
  [PXUIField(DisplayName = "Account Mask", Enabled = false, Visible = false)]
  [PXDefault("")]
  [PXDBString(10, IsUnicode = true)]
  public virtual string AccountMask { get; set; }

  /// <summary>
  /// For a group article (see <see cref="P:PX.Objects.GL.GLBudgetLine.IsGroup" />), defines the mask for selection of the child budget articles by their subaccounts.
  /// The selector on the <see cref="P:PX.Objects.GL.GLBudgetLine.SubID" /> field of the child articles allows only subaccounts
  /// whose <see cref="P:PX.Objects.GL.Sub.SubCD" /> matches the specified mask.
  /// </summary>
  [PXUIField(DisplayName = "Subaccount Mask", Enabled = false, Visible = false)]
  [PXDefault("")]
  [PXDBString(30, IsUnicode = true)]
  public virtual string SubMask { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the budget article has been released at least once.
  /// A released article can be released again if its <see cref="P:PX.Objects.GL.GLBudgetLine.Amount" /> is not equal to its <see cref="P:PX.Objects.GL.GLBudgetLine.ReleasedAmount" />.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the article was released at least once.
  /// Different from <see cref="P:PX.Objects.GL.GLBudgetLine.Released" /> in that the latter will be reset to <c>false</c> upon edits of
  /// a released article, while this field will still be <c>true</c> even if the article can be released again.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? WasReleased { get; set; }

  [PXBool]
  public virtual bool? Comparison
  {
    get => this._Comparison;
    set => this._Comparison = value;
  }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  /// <summary>
  /// The group mask showing which <see cref="T:PX.SM.RelationGroup">restriction groups</see> the budget article belongs to.
  /// The value of this field is inherited from the <see cref="P:PX.Objects.GL.GLBudgetTree.GroupMask">group mask</see> of the corresponding budget tree node,
  /// because access to budgets is managed through the budget tree configuration.
  /// </summary>
  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID(Visible = false)]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(Visible = false)]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public virtual Decimal[] Compared
  {
    get => this._Compared;
    set => this._Compared = value;
  }

  /// <summary>
  /// An integer field that defines the order in which group budget articles (see <see cref="P:PX.Objects.GL.GLBudgetLine.IsGroup" />) that belong to one <see cref="P:PX.Objects.GL.GLBudgetLine.ParentGroupID">parent</see>
  /// appear relative to each other in the tree view.
  /// </summary>
  /// <value>
  /// The value of this field is automatically assigned during budget tree preload from
  /// <see cref="P:PX.Objects.GL.GLBudgetTree.SortOrder" /> of the corresponding tree node.
  /// </value>
  [PXDBInt]
  [PXDefault(0)]
  [PXUIField]
  public virtual int? TreeSortOrder
  {
    get => this._TreeSortOrder;
    set => this._TreeSortOrder = value;
  }

  /// <summary>
  /// The internal field that defines the order in which non-group budget articles appear relative to each other.
  /// </summary>
  /// <value>
  /// This field is populated only in the <see cref="T:PX.Objects.GL.GLBudgetEntry" /> graph and is not stored in the database.
  /// </value>
  [PXInt]
  public virtual int? SortOrder { get; set; }

  public virtual bool? IsUploaded { get; set; }

  /// <summary>The field is reserved for internal use.</summary>
  public virtual bool? Cleared { get; set; }

  [PXBool]
  public virtual bool? IsRolledUp { get; set; }

  public class PK : 
    PrimaryKeyOf<GLBudgetLine>.By<GLBudgetLine.branchID, GLBudgetLine.ledgerID, GLBudgetLine.groupID, GLBudgetLine.parentGroupID, GLBudgetLine.finYear>
  {
    public static GLBudgetLine Find(
      PXGraph graph,
      int? branchID,
      int? ledgerID,
      Guid? groupID,
      Guid? parentGroupID,
      string finYear,
      PKFindOptions options = 0)
    {
      return (GLBudgetLine) PrimaryKeyOf<GLBudgetLine>.By<GLBudgetLine.branchID, GLBudgetLine.ledgerID, GLBudgetLine.groupID, GLBudgetLine.parentGroupID, GLBudgetLine.finYear>.FindBy(graph, (object) branchID, (object) ledgerID, (object) groupID, (object) parentGroupID, (object) finYear, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLBudgetLine>.By<GLBudgetLine.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLBudgetLine>.By<GLBudgetLine.ledgerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLBudgetLine>.By<GLBudgetLine.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLBudgetLine>.By<GLBudgetLine.subID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetLine.selected>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudgetLine.groupID>
  {
  }

  public abstract class parentGroupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudgetLine.parentGroupID>
  {
  }

  public abstract class rollup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetLine.rollup>
  {
  }

  public abstract class isGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetLine.isGroup>
  {
  }

  public abstract class isPreloaded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetLine.isPreloaded>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLine.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLine.ledgerID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLBudgetLine.finYear>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLine.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLine.subID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLBudgetLine.description>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLBudgetLine.amount>
  {
  }

  public abstract class allocatedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLBudgetLine.allocatedAmount>
  {
  }

  public abstract class releasedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLBudgetLine.releasedAmount>
  {
  }

  public abstract class accountMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLBudgetLine.accountMask>
  {
  }

  public abstract class subMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLBudgetLine.subMask>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetLine.released>
  {
  }

  public abstract class wasReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetLine.wasReleased>
  {
  }

  public abstract class comparison : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetLine.comparison>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudgetLine.noteID>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLBudgetLine.groupMask>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLBudgetLine.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudgetLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLBudgetLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLBudgetLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLBudgetLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLBudgetLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLBudgetLine.lastModifiedDateTime>
  {
  }

  public abstract class treeSortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLine.treeSortOrder>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLine.sortOrder>
  {
  }

  public abstract class isUploaded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetLine.isUploaded>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetLine.cleared>
  {
  }

  public abstract class isRolledUp : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLBudgetLine.isRolledUp>
  {
  }
}
