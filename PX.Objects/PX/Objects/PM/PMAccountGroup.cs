// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAccountGroup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>An account group that is used to track the budget, expenses, and revenues of projects.</summary>
[PXPrimaryGraph(typeof (AccountGroupMaint))]
[PXCacheName("Account Group")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMAccountGroup : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted
{
  protected int? _GroupID;
  protected 
  #nullable disable
  string _GroupCD;
  protected string _Description;
  protected bool? _IsActive;
  protected bool? _IsExpense;
  protected string _Type;
  protected int? _AccountID;
  protected short? _SortOrder;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>Gets or Sets the AccountGroup identifier.</summary>
  [PXDBIdentity]
  [PXReferentialIntegrityCheck]
  [PXSelector(typeof (PMAccountGroup.groupID))]
  public virtual int? GroupID
  {
    get => this._GroupID;
    set => this._GroupID = value;
  }

  /// <summary>
  /// Gets or Sets the AccountGroup identifier.
  /// This is a segmented key and format is configured under segmented key maintenance screen in CS module.
  /// </summary>
  [PXDimensionSelector("ACCGROUP", typeof (Search<PMAccountGroup.groupCD>), typeof (PMAccountGroup.groupCD), new System.Type[] {typeof (PMAccountGroup.groupCD), typeof (PMAccountGroup.description), typeof (PMAccountGroup.type), typeof (PMAccountGroup.isActive)}, DescriptionField = typeof (PMTask.description))]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string GroupCD
  {
    get => this._GroupCD;
    set => this._GroupCD = value;
  }

  /// <summary>Gets or Sets the AccountGroup description.</summary>
  [PXDBLocalizableString(250, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>Gets or sets whether Account group is active or not.</summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the account group is an expense account group
  /// and can be selected on the Cost Budget tab of the Projects (PM301000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Expense")]
  public virtual bool? IsExpense
  {
    get => this._IsExpense;
    set => this._IsExpense = value;
  }

  /// <summary>
  /// The default revenue account group of the expense account group.
  /// </summary>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXSelector(typeof (Search<PMAccountGroup.groupID, Where<PMAccountGroup.type, Equal<AccountType.income>>>), SubstituteKey = typeof (PMAccountGroup.groupCD))]
  [PXUIField(DisplayName = "Default Revenue Account Group")]
  [PXDBInt]
  public virtual int? RevenueAccountGroupID { get; set; }

  /// <summary>
  /// The type of the account group, which can be one of the following: Asset, Liability, Expense, Income, and Off-Balance.
  /// </summary>
  [PXDBString(1)]
  [PXDefault("E")]
  [PMAccountType.List]
  [PXUIField]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>The type of the reporting group.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <list type="bullet">
  /// <item>
  /// <term>L</term>
  /// <description>Labor</description>
  /// </item>
  /// <item>
  /// <term>M</term>
  /// <description>Material</description>
  /// </item>
  /// <item>
  /// <term>S</term>
  /// <description>Subcontract</description>
  /// </item>
  /// <item>
  /// <term>E</term>
  /// <description>Equipment</description>
  /// </item>
  /// <item>
  /// <term>O</term>
  /// <description>Other</description>
  /// </item>
  /// <item>
  /// <term>R</term>
  /// <description>Revenue</description>
  /// </item>
  /// </list>
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PX.Objects.PM.ReportGroup.List]
  [PXUIField(DisplayName = "Reporting Group")]
  public virtual string ReportGroup { get; set; }

  /// <summary>
  /// The identifier of the default <see cref="T:PX.Objects.GL.Account">account</see> for the account group.
  /// </summary>
  [PXDBInt]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// Gets or sets sort order. Sort order is used in displaying the Balances for the Project.
  /// </summary>
  [PXDBShort]
  [PXUIField(DisplayName = "Sort Order")]
  public virtual short? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  /// <summary>
  /// The default percentage of the line markup for change requests.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Default Line Markup (%)", FieldClass = "ChangeRequest")]
  public virtual Decimal? DefaultLineMarkupPct { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the system automatically selects the Create Commitment
  /// check box for a change request line on the Estimation tab of the Change Requests (PM308500) form if the
  /// change request line has this account group selected in the Account Group column.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Create Commitment", FieldClass = "ChangeRequest")]
  [PXDefault(false)]
  public virtual bool? CreatesCommitment { get; set; }

  [PXDBGroupMask]
  public virtual byte[] GroupMask { get; set; }

  /// <summary>
  /// An unbound field that is used in the user interface to include the account group into a <see cref="T:PX.SM.RelationGroup">restriction group</see>.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  [PXUnboundDefault(false)]
  public virtual bool? Included { get; set; }

  /// <summary>Gets or sets entity attributes.</summary>
  [CRAttributesField(typeof (PMAccountGroup.classID))]
  public virtual string[] Attributes { get; set; }

  /// <summary>
  /// Gets ClassID for the attributes. Always returns <see cref="F:PX.Objects.PM.GroupTypes.AccountGroup" />
  /// </summary>
  [PXString(20)]
  public virtual string ClassID => "ACCGROUP";

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>Primary Key</summary>
  /// <exclude />
  public class PK : PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>
  {
    public static PMAccountGroup Find(PXGraph graph, int? accountGroupID, PKFindOptions options = 0)
    {
      return (PMAccountGroup) PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.FindBy(graph, (object) accountGroupID, options);
    }
  }

  /// <summary>Unique Key</summary>
  public class UK : PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupCD>
  {
    public static PMAccountGroup Find(PXGraph graph, string accountGroupCD, PKFindOptions options = 0)
    {
      return (PMAccountGroup) PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupCD>.FindBy(graph, (object) accountGroupCD, options);
    }
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAccountGroup.groupID>
  {
  }

  public abstract class groupCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAccountGroup.groupCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAccountGroup.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAccountGroup.isActive>
  {
  }

  public abstract class isExpense : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAccountGroup.isExpense>
  {
  }

  public abstract class revenueAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAccountGroup.revenueAccountGroupID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAccountGroup.type>
  {
  }

  public abstract class reportGroup : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAccountGroup.reportGroup>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAccountGroup.accountID>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  PMAccountGroup.sortOrder>
  {
  }

  public abstract class defaultLineMarkupPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMAccountGroup.defaultLineMarkupPct>
  {
  }

  public abstract class createsCommitment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMAccountGroup.createsCommitment>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMAccountGroup.groupMask>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAccountGroup.included>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAccountGroup.classID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMAccountGroup.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMAccountGroup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMAccountGroup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAccountGroup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMAccountGroup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMAccountGroup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAccountGroup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMAccountGroup.lastModifiedDateTime>
  {
  }
}
