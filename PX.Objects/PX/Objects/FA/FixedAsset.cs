// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FixedAsset
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FA;

/// <summary>
/// Contains the main properties of fixed assets and their classes.
/// Fixed assets are edited on the Fixed Assets (FA303000) form (which corresponds to the <see cref="T:PX.Objects.FA.AssetMaint" /> graph).
/// The fixed asset classes edited through the Fixed Asset Classes (FA201000) form (corresponds to the <see cref="T:PX.Objects.FA.AssetClassMaint" /> graph).
/// </summary>
[PXPrimaryGraph(new System.Type[] {typeof (AssetClassMaint), typeof (AssetClassMaint), typeof (AssetMaint), typeof (AssetMaint)}, new System.Type[] {typeof (Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>, And<FixedAsset.recordType, Equal<FARecordType.classType>>>>), typeof (Where<FAClass.assetID, Less<Zero>, And<FAClass.recordType, Equal<FARecordType.classType>>>), typeof (Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>, And<FixedAsset.recordType, Equal<FARecordType.assetType>>>>), typeof (Where<FixedAsset.assetID, Less<Zero>, And<FixedAsset.recordType, Equal<FARecordType.assetType>>>)})]
[PXCacheName("Fixed Asset")]
[Serializable]
public class FixedAsset : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
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
  /// An unbound service field, which indicates that the fixed asset is marked for processing.
  /// </summary>
  /// <value>
  /// If the value of the field is <c>true</c>, the asset will be processed; otherwise, the asset will not be processed.
  /// </value>
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  /// <summary>
  /// The identifier of the fixed asset. The identifier is used for foreign references; it can be negative for newly inserted records.
  /// </summary>
  /// <value>A unique integer number.</value>
  [PXDBIdentity]
  [PXUIField]
  public virtual int? AssetID { get; set; }

  /// <summary>
  /// A type of the entity: fixed asset, class, or element.
  /// </summary>
  /// <value>
  ///  The field can have one of the following values:
  ///  <list type="bullet">
  ///  <item> <term><c>"C"</c></term> <description>indicates a fixed asset class.</description> </item>
  ///  <item> <term><c>"A"</c></term> <description>indicates a fixed asset.</description> </item>
  ///  <item> <term><c>"E"</c></term> <description>indicates a fixed asset element (also called component).</description> </item>
  /// </list>
  ///  </value>
  [PXDefault("A")]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Record Type", TabOrder = 0)]
  [FARecordType.List]
  public virtual string RecordType { get; set; }

  /// <summary>
  /// A string identifier, which contains a key value. This field is also a selector for navigation.
  /// </summary>
  /// <value>The value can be entered manually or can be auto-numbered.</value>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search2<FixedAsset.assetCD, LeftJoin<FixedAsset.FADetails, On<FixedAsset.FADetails.assetID, Equal<FixedAsset.assetID>>, LeftJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<FixedAsset.assetID>, And<FALocationHistory.revisionID, Equal<FixedAsset.FADetails.locationRevID>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<FALocationHistory.locationID>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<FALocationHistory.employeeID>>, LeftJoin<FAClass, On<FAClass.assetID, Equal<FixedAsset.classID>>>>>>>, Where<FixedAsset.recordType, Equal<Current<FixedAsset.recordType>>>>), new System.Type[] {typeof (FixedAsset.assetCD), typeof (FixedAsset.description), typeof (FixedAsset.classID), typeof (FAClass.description), typeof (FixedAsset.depreciable), typeof (FixedAsset.underConstruction), typeof (FixedAsset.usefulLife), typeof (FixedAsset.assetTypeID), typeof (FixedAsset.FADetails.status), typeof (PX.Objects.GL.Branch.branchCD), typeof (EPEmployee.acctName), typeof (FALocationHistory.department)}, Filterable = true)]
  [FARecordType.Numbering]
  [PXFieldDescription]
  public virtual string AssetCD { get; set; }

  /// <summary>
  /// A reference to the branch, <see cref="T:PX.Objects.GL.Branch" />.
  /// </summary>
  /// <value>An integer identifier of the branch. It is a required value. By default, the value is set to the identifier of the current branch.</value>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The base <see cref="T:PX.Objects.CM.Currency" /> of the Fixed Asset, corresponds to the Base Currency of the Branch of Fixed Asset.
  /// </summary>
  [PXDefault(typeof (AccessInfo.baseCuryID))]
  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXUIField(DisplayName = "Currency")]
  public virtual string BaseCuryID { get; set; }

  /// <summary>A reference to the fixed asset class.</summary>
  /// <value>An integer identifier of the fixed asset class. It is a required value.</value>
  [PXDBInt]
  [PXRestrictor(typeof (Where<FAClass.active, Equal<True>>), "Fixed Asset Class '{0}' is inactive.", new System.Type[] {typeof (FAClass.assetCD)})]
  [PXSelector(typeof (Search<FAClass.assetID, Where<FAClass.recordType, Equal<FARecordType.classType>>>), new System.Type[] {typeof (FAClass.assetCD), typeof (FAClass.assetTypeID), typeof (FAClass.description), typeof (FAClass.usefulLife)}, SubstituteKey = typeof (FAClass.assetCD), DescriptionField = typeof (FAClass.description), CacheGlobal = true)]
  [PXUIField]
  public virtual int? ClassID { get; set; }

  /// <summary>
  /// The service reference to the previous class, which is used in the transfer process only.
  /// </summary>
  /// <value>An integer identifier of the fixed asset class.</value>
  [PXInt]
  [PXDBCalced(typeof (FixedAsset.classID), typeof (int), Persistent = true)]
  public virtual int? OldClassID { get; set; }

  /// <summary>
  /// The reference to the parent fixed asset, which is generally used in fixed asset components.
  /// </summary>
  /// <value>An integer identifier of the fixed asset. It is a required value for the fixed asset component.</value>
  [PXDBInt]
  [PXParent(typeof (Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FixedAsset.parentAssetID>>>>), UseCurrent = true, LeaveChildren = true)]
  [PXSelector(typeof (Search<FixedAsset.assetID, Where<FixedAsset.assetID, NotEqual<Current<FixedAsset.assetID>>, And<Where<FixedAsset.recordType, Equal<Current<FixedAsset.recordType>>, And<Current<FixedAsset.recordType>, NotEqual<FARecordType.elementType>, Or<Current<FixedAsset.recordType>, Equal<FARecordType.elementType>, And<FixedAsset.recordType, Equal<FARecordType.assetType>>>>>>>>), new System.Type[] {typeof (FixedAsset.assetCD), typeof (FixedAsset.assetTypeID), typeof (FixedAsset.description), typeof (FixedAsset.usefulLife)}, SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField]
  public virtual int? ParentAssetID { get; set; }

  /// <summary>
  /// The reference to the fixed asset type, <see cref="T:PX.Objects.FA.FAType" />
  /// </summary>
  /// <value>An integer identifier of the fixed asset type. It is a required value. By default, the value is inserted from the fixed asset class.</value>
  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCC")]
  [PXSelector(typeof (Search<FAType.assetTypeID>), DescriptionField = typeof (FAType.description))]
  [PXUIField]
  [PXDefault(typeof (Search<FixedAsset.assetTypeID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [PXFormula(typeof (Switch<Case<Where<FixedAsset.classID, IsNotNull>, Selector<FixedAsset.classID, FAClass.assetTypeID>>, Null>))]
  public virtual string AssetTypeID { get; set; }

  /// <summary>The status of the fixed asset.</summary>
  /// <value>
  /// The allowed values of the asset status described in <see cref="T:PX.Objects.FA.FixedAssetStatus.ListAttribute" />
  /// </value>
  [PXString(1, IsFixed = true)]
  [PXDBScalar(typeof (Search<FixedAsset.FADetails.status, Where<FixedAsset.FADetails.assetID, Equal<FixedAsset.assetID>>>))]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [FixedAssetStatus.List]
  [FASyncStatus]
  [PXDefault("A")]
  public virtual string Status { get; set; }

  /// <summary>The description of fixed asset.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  /// <summary>
  /// The reference to the construction account, <see cref="T:PX.Objects.GL.Account" />
  /// </summary>
  /// <value>An integer identifier of the construction account. By default, the value is inserted from the fixed asset class.</value>
  [Account]
  [PXDefault(typeof (Search<FixedAsset.constructionAccountID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  public virtual int? ConstructionAccountID { get; set; }

  /// <summary>
  /// The reference to the construction subaccount associated with the construction account, <see cref="T:PX.Objects.GL.Sub" />
  /// </summary>
  /// <value>An integer identifier of the construction subaccount. By default, the value is inserted from the fixed asset class.</value>
  [SubAccount(typeof (FixedAsset.constructionAccountID))]
  [PXDefault(typeof (Search<FixedAsset.constructionSubID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  public virtual int? ConstructionSubID { get; set; }

  /// <summary>
  /// The reference to the Fixed Assets account, <see cref="T:PX.Objects.GL.Account" />
  /// </summary>
  /// <value>An integer identifier of the account. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (Search<FixedAsset.fAAccountID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [Account]
  public virtual int? FAAccountID { get; set; }

  /// <summary>
  /// The reference to the Fixed Assets subaccount associated with the Fixed Assets account, <see cref="T:PX.Objects.GL.Sub" />
  /// </summary>
  /// <value>An integer identifier of the subaccount. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault]
  [SubAccount(typeof (FixedAsset.fAAccountID))]
  public virtual int? FASubID { get; set; }

  /// <summary>
  /// The reference to the FA Accrual account, <see cref="T:PX.Objects.GL.Account" />
  /// </summary>
  /// <value>An integer identifier of the account. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (FASetup.fAAccrualAcctID))]
  [Account]
  public virtual int? FAAccrualAcctID { get; set; }

  /// <summary>
  /// The reference to the FA Accrual subaccount associated with the FA Accrual account, <see cref="T:PX.Objects.GL.Sub" />
  /// </summary>
  /// <value>An integer identifier of the subaccount. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (FASetup.fAAccrualSubID))]
  [SubAccount(typeof (FixedAsset.fAAccrualAcctID))]
  public virtual int? FAAccrualSubID { get; set; }

  /// <summary>
  /// The reference to the Accumulated Depreciation account, <see cref="T:PX.Objects.GL.Account" />
  /// </summary>
  /// <value>An integer identifier of the account. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (Search<FixedAsset.accumulatedDepreciationAccountID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [Account]
  public virtual int? AccumulatedDepreciationAccountID { get; set; }

  /// <summary>
  /// The reference to the Accumulated Depreciation subaccount associated with the Accumulated Depreciation account, <see cref="T:PX.Objects.GL.Sub" />
  /// </summary>
  /// <value>An integer identifier of the subaccount. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault]
  [SubAccount(typeof (FixedAsset.accumulatedDepreciationAccountID))]
  public virtual int? AccumulatedDepreciationSubID { get; set; }

  /// <summary>
  /// The reference to the Depreciation Expense account, <see cref="T:PX.Objects.GL.Account" />
  /// </summary>
  /// <value>An integer identifier of the account. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (Search<FixedAsset.depreciatedExpenseAccountID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [Account]
  public virtual int? DepreciatedExpenseAccountID { get; set; }

  /// <summary>
  /// The reference to the Depreciation Expense subaccount associated with the Depreciation Expense account, <see cref="T:PX.Objects.GL.Sub" />
  /// </summary>
  /// <value>An integer identifier of the subaccount. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault]
  [SubAccount(typeof (FixedAsset.depreciatedExpenseAccountID))]
  public virtual int? DepreciatedExpenseSubID { get; set; }

  /// <summary>
  /// The reference to the Proceeds account, <see cref="T:PX.Objects.GL.Account" />
  /// </summary>
  /// <value>An integer identifier of the account. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (Search<FixedAsset.disposalAccountID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [Account]
  public virtual int? DisposalAccountID { get; set; }

  /// <summary>
  /// The reference to the Proceeds subaccount associated with the Proceeds account, <see cref="T:PX.Objects.GL.Sub" />
  /// </summary>
  /// <value>An integer identifier of the subaccount. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (Search<FixedAsset.disposalSubID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [SubAccount(typeof (FixedAsset.disposalAccountID))]
  public virtual int? DisposalSubID { get; set; }

  /// <summary>
  /// The reference to the Rent account, <see cref="T:PX.Objects.GL.Account" />
  /// </summary>
  /// <value>An integer identifier of the account</value>
  [Account]
  public virtual int? RentAccountID { get; set; }

  /// <summary>
  /// The reference to the Rent subaccount associated with the Rent account, <see cref="T:PX.Objects.GL.Sub" />
  /// </summary>
  /// <value>An integer identifier of the subaccount.</value>
  [SubAccount(typeof (FixedAsset.rentAccountID))]
  public virtual int? RentSubID { get; set; }

  /// <summary>
  /// The reference to the Lease account, <see cref="T:PX.Objects.GL.Account" />
  /// </summary>
  /// <value>An integer identifier of the account</value>
  [Account]
  public virtual int? LeaseAccountID { get; set; }

  /// <summary>
  /// The reference to the Lease subaccount associated with the Lease account, <see cref="T:PX.Objects.GL.Sub" />
  /// </summary>
  /// <value>An integer identifier of the subaccount.</value>
  [SubAccount(typeof (FixedAsset.leaseAccountID))]
  public virtual int? LeaseSubID { get; set; }

  /// <summary>
  /// The reference to the Gain account, <see cref="T:PX.Objects.GL.Account" />
  /// </summary>
  /// <value>An integer identifier of the account. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (Search<FixedAsset.gainAcctID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [Account(DisplayName = "Gain Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  public virtual int? GainAcctID { get; set; }

  /// <summary>
  /// The reference to the Gain subaccount associated with the Gain account, <see cref="T:PX.Objects.GL.Sub" />
  /// </summary>
  /// <value>An integer identifier of the subaccount. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (Search<FixedAsset.gainSubID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [SubAccount(typeof (FixedAsset.gainAcctID), DescriptionField = typeof (Sub.description), DisplayName = "Gain Sub.")]
  public virtual int? GainSubID { get; set; }

  /// <summary>
  /// The reference to the Loss account, <see cref="T:PX.Objects.GL.Account" />
  /// </summary>
  /// <value>An integer identifier of the account. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (Search<FixedAsset.lossAcctID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [Account(DisplayName = "Loss Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  public virtual int? LossAcctID { get; set; }

  /// <summary>
  /// The reference to the Loss subaccount associated with the Loss account, <see cref="T:PX.Objects.GL.Sub" />
  /// </summary>
  /// <value>An integer identifier of the subaccount. By default, the value is inserted from the fixed asset class.</value>
  [PXDefault(typeof (Search<FixedAsset.lossSubID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [SubAccount(typeof (FixedAsset.lossAcctID), DescriptionField = typeof (Sub.description), DisplayName = "Loss Sub.")]
  public virtual int? LossSubID { get; set; }

  /// <summary>
  /// A subaccount mask that is used to generate the Fixed Assets subaccount.
  /// </summary>
  /// <value>
  /// The allowed symbols of the subaccount mask described in <see cref="T:PX.Objects.FA.SubAccountMaskAttribute" />
  /// </value>
  [PXDBString(30, IsUnicode = false, InputMask = "")]
  public virtual string FASubMask { get; set; }

  /// <summary>
  /// A subaccount mask that is used to generate the Accumulated Depreciation subaccount.
  /// </summary>
  /// <value>
  /// The allowed symbols of the subaccount mask described in <see cref="T:PX.Objects.FA.SubAccountMaskAttribute" />
  /// </value>
  [PXDBString(30, IsUnicode = false, InputMask = "")]
  public virtual string AccumDeprSubMask { get; set; }

  /// <summary>
  /// A subaccount mask that is used to generate the Depreciation Expence subaccount.
  /// </summary>
  /// <value>
  /// The allowed symbols of the subaccount mask described in <see cref="T:PX.Objects.FA.SubAccountMaskAttribute" />
  /// </value>
  [PXDBString(30, IsUnicode = false, InputMask = "")]
  public virtual string DeprExpenceSubMask { get; set; }

  /// <summary>
  /// A flag that determines whether the Fixed Assets subaccount mask has to be used for the Accumulated Depreciation subaccount.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use Fixed Asset Sub. Mask", FieldClass = "SUBACCOUNT")]
  [PXUIRequired(typeof (FixedAsset.depreciable))]
  [PXUIEnabled(typeof (FixedAsset.depreciable))]
  public virtual bool? UseFASubMask { get; set; }

  /// <summary>
  /// A subaccount mask that is used to generate the Proceed subaccount.
  /// </summary>
  /// <value>
  /// The allowed symbols of the subaccount mask described in <see cref="T:PX.Objects.FA.SubAccountMaskAttribute" />
  /// </value>
  [PXDBString(30, IsUnicode = false, InputMask = "")]
  public virtual string ProceedsSubMask { get; set; }

  /// <summary>
  /// A subaccount mask that is used to generate the Gain and Loss subaccounts.
  /// </summary>
  /// <value>
  /// The allowed symbols of the subaccount mask described in <see cref="T:PX.Objects.FA.SubAccountMaskAttribute" />
  /// </value>
  [PXDBString(30, IsUnicode = false, InputMask = "")]
  public virtual string GainLossSubMask { get; set; }

  /// <summary>
  /// A flag that determines whether the fixed asset should be depreciated.
  /// </summary>
  /// <value>By default, the value is inserted from the appropriate fixed asset class.</value>
  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where<EntryStatus, Equal<EntryStatus.inserted>, And<FixedAsset.classID, IsNotNull>>, Selector<FixedAsset.classID, FixedAsset.depreciable>, Case<Where<EntryStatus, Equal<EntryStatus.inserted>, And<FixedAsset.classID, IsNull>>, True>>, FixedAsset.depreciable>))]
  [PXUIField]
  [PXUIEnabled(typeof (Where<FixedAsset.underConstruction, NotEqual<True>, And<Where<FixedAsset.recordType, NotEqual<FARecordType.assetType>, Or<FixedAsset.isAcquired, NotEqual<True>, Or<EntryStatus, Equal<EntryStatus.inserted>>>>>>))]
  public virtual bool? Depreciable { get; set; }

  /// <summary>
  /// A flag that determines whether the fixed asset is under construction.
  /// </summary>
  /// <value>By default, the value is inserted from the appropriate fixed asset class.</value>
  [PXDBBool]
  [PXDefault(typeof (Search<FixedAsset.underConstruction, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [PXUIField]
  public virtual bool? UnderConstruction { get; set; }

  /// <summary>The useful life of the fixed asset measured in years.</summary>
  /// <value> An integer number of years of useful life. By default, the value is inserted from the appropriate fixed asset class.</value>
  [PXDBDecimal(4, MinValue = 0.0)]
  [PXDefault(typeof (Search<FixedAsset.usefulLife, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [PXUIField]
  [PXUIRequired(typeof (FixedAsset.depreciable))]
  [PXUIEnabled(typeof (FixedAsset.depreciable))]
  public virtual Decimal? UsefulLife { get; set; }

  /// <summary>
  /// A flag that indicates whether the fixed asset is tangible.
  /// </summary>
  /// <value>If a type is associated with the fixed asset, the default value is inserted from the fixed asset type (<see cref="T:PX.Objects.FA.FAType" />).
  /// If no type is associated with the fixed asset, the default value is <c>true</c>.</value>
  [PXDBBool]
  [PXDefault(true, typeof (Search<FixedAsset.isTangible, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  [PXFormula(typeof (Switch<Case<Where<FixedAsset.assetTypeID, IsNotNull>, Selector<FixedAsset.assetTypeID, FAType.isTangible>>, True>))]
  [PXUIField(DisplayName = "Tangible", Enabled = false)]
  public virtual bool? IsTangible { get; set; }

  /// <summary>
  /// A flag that indicates whether the fixed asset is active.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  /// <summary>
  /// A flag that indicates whether the fixed asset was acquired.
  /// </summary>
  [PXDBBool]
  public virtual bool? IsAcquired { get; set; }

  /// <summary>
  /// A flag that indicates whether the fixed asset is suspended.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Suspended")]
  public virtual bool? Suspended { get; set; }

  /// <summary>
  /// A flag that determines whether new fixed assets should have the On Hold status by default.
  /// This field makes sense only for fixed asset classes.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hold on Entry")]
  public virtual bool? HoldEntry { get; set; }

  /// <summary>
  /// A flag that determines whether the Straight Line depreciation method is equivalent to the Remaining Value depreciation method.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Accelerated Depreciation for SL Depr. Method")]
  [PXDefault(false)]
  [PXUIRequired(typeof (FixedAsset.depreciable))]
  [PXUIEnabled(typeof (FixedAsset.depreciable))]
  public virtual bool? AcceleratedDepreciation { get; set; }

  /// <summary>
  /// The reference to the service schedule, <see cref="T:PX.Objects.FA.FAServiceSchedule" />. This field is reserved for future use.
  /// </summary>
  /// <value>An integer identifier of the service schedule.
  /// By default, the value is inserted from the appropriate fixed asset class.</value>
  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (Search<FAServiceSchedule.scheduleID>), new System.Type[] {typeof (FAServiceSchedule.scheduleCD), typeof (FAServiceSchedule.serviceEveryPeriod), typeof (FAServiceSchedule.serviceEveryValue), typeof (FAServiceSchedule.serviceAfterUsageValue), typeof (FAServiceSchedule.serviceAfterUsageUOM), typeof (FAServiceSchedule.description)}, SubstituteKey = typeof (FAServiceSchedule.scheduleCD), DescriptionField = typeof (FAServiceSchedule.description))]
  [PXDefault(typeof (Search<FixedAsset.serviceScheduleID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  public virtual int? ServiceScheduleID { get; set; }

  /// <summary>
  /// The reference to the usage schedule, <see cref="T:PX.Objects.FA.FAUsageSchedule" />. This field is reserved for future use.
  /// </summary>
  /// <value>An integer identifier of the usage schedule.
  /// By default, the value is inserted from the appropriate fixed asset class.</value>
  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (Search<FAUsageSchedule.scheduleID>), new System.Type[] {typeof (FAUsageSchedule.scheduleCD), typeof (FAUsageSchedule.readUsageEveryPeriod), typeof (FAUsageSchedule.readUsageEveryValue), typeof (FAUsageSchedule.usageUOM), typeof (FAUsageSchedule.description)}, SubstituteKey = typeof (FAUsageSchedule.scheduleCD), DescriptionField = typeof (FAUsageSchedule.usageUOM))]
  [PXDefault(typeof (Search<FixedAsset.usageScheduleID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>))]
  public virtual int? UsageScheduleID { get; set; }

  [PXSearchable(8, "Fixed Asset ID:{0}", new System.Type[] {typeof (FixedAsset.assetCD)}, new System.Type[] {typeof (FixedAsset.description), typeof (FixedAsset.assetTypeID), typeof (FixedAsset.branchID), typeof (FixedAsset.recordType), typeof (FixedAsset.classID), typeof (FAClass.description)}, Line1Format = "{1}{3}{4}", Line1Fields = new System.Type[] {typeof (FixedAsset.parentAssetID), typeof (FixedAsset.assetCD), typeof (FixedAsset.classID), typeof (FAClass.description), typeof (FixedAsset.assetTypeID)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (FixedAsset.description)})]
  [PXNote(DescriptionField = typeof (FixedAsset.assetCD), Selector = typeof (FixedAsset.assetCD))]
  public virtual Guid? NoteID { get; set; }

  /// <summary>The number of objects in the fixed asset.</summary>
  /// <value>A decimal number (which can be a fractional number). The default value is <c>1.0</c>.</value>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  /// <summary>
  /// An identifier of the source fixed asset if the current fixed asset has been splitted.
  /// </summary>
  [PXDBInt]
  public virtual int? SplittedFrom { get; set; }

  /// <summary>The disposal amount of the asset.</summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Proceeds Amount")]
  public virtual Decimal? DisposalAmt { get; set; }

  /// <exclude />
  [PXBaseCury]
  public virtual Decimal? SalvageAmtAfterSplit { get; set; }

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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class Events : PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>
  {
    public PXEntityEvent<FixedAsset> ActivateAsset;
    public PXEntityEvent<FixedAsset> DisposeAsset;
    public PXEntityEvent<FixedAsset> SuspendAsset;
    public PXEntityEvent<FixedAsset> FullyDepreciateAsset;
    public PXEntityEvent<FixedAsset> NotFullyDepreciateAsset;
    public PXEntityEvent<FixedAsset> ReverseAsset;
  }

  public class PK : PrimaryKeyOf<FixedAsset>.By<FixedAsset.assetID>
  {
    public static FixedAsset Find(PXGraph graph, int? assetID, PKFindOptions options = 0)
    {
      return (FixedAsset) PrimaryKeyOf<FixedAsset>.By<FixedAsset.assetID>.FindBy(graph, (object) assetID, options);
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FixedAsset.selected>
  {
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.assetID>
  {
  }

  public abstract class recordType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FixedAsset.recordType>
  {
  }

  public abstract class assetCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FixedAsset.assetCD>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.branchID>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FixedAsset.baseCuryID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.classID>
  {
  }

  public abstract class oldClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.oldClassID>
  {
  }

  public abstract class parentAssetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.parentAssetID>
  {
  }

  public abstract class assetTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FixedAsset.assetTypeID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FixedAsset.status>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FixedAsset.description>
  {
  }

  public abstract class constructionAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FixedAsset.constructionAccountID>
  {
  }

  public abstract class constructionSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.constructionSubID>
  {
  }

  public abstract class fAAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.fAAccountID>
  {
  }

  public abstract class fASubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.fASubID>
  {
  }

  public abstract class fAAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.fAAccrualAcctID>
  {
  }

  public abstract class fAAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.fAAccrualSubID>
  {
  }

  public abstract class accumulatedDepreciationAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FixedAsset.accumulatedDepreciationAccountID>
  {
  }

  public abstract class accumulatedDepreciationSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FixedAsset.accumulatedDepreciationSubID>
  {
  }

  public abstract class depreciatedExpenseAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FixedAsset.depreciatedExpenseAccountID>
  {
  }

  public abstract class depreciatedExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FixedAsset.depreciatedExpenseSubID>
  {
  }

  public abstract class disposalAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.disposalAccountID>
  {
  }

  public abstract class disposalSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.disposalSubID>
  {
  }

  public abstract class rentAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.rentAccountID>
  {
  }

  public abstract class rentSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.rentSubID>
  {
  }

  public abstract class leaseAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.leaseAccountID>
  {
  }

  public abstract class leaseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.leaseSubID>
  {
  }

  public abstract class gainAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.gainAcctID>
  {
  }

  public abstract class gainSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.gainSubID>
  {
  }

  public abstract class lossAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.lossAcctID>
  {
  }

  public abstract class lossSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.lossSubID>
  {
  }

  public abstract class fASubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FixedAsset.fASubMask>
  {
  }

  public abstract class accumDeprSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FixedAsset.accumDeprSubMask>
  {
  }

  public abstract class deprExpenceSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FixedAsset.deprExpenceSubMask>
  {
  }

  public abstract class useFASubMask : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FixedAsset.useFASubMask>
  {
  }

  public abstract class proceedsSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FixedAsset.proceedsSubMask>
  {
  }

  public abstract class gainLossSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FixedAsset.gainLossSubMask>
  {
  }

  public abstract class depreciable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FixedAsset.depreciable>
  {
  }

  public abstract class underConstruction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FixedAsset.underConstruction>
  {
  }

  public abstract class usefulLife : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FixedAsset.usefulLife>
  {
  }

  public abstract class isTangible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FixedAsset.isTangible>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FixedAsset.active>
  {
  }

  public abstract class isAcquired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FixedAsset.isAcquired>
  {
  }

  public abstract class suspended : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FixedAsset.suspended>
  {
  }

  public abstract class holdEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FixedAsset.holdEntry>
  {
  }

  public abstract class acceleratedDepreciation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FixedAsset.acceleratedDepreciation>
  {
  }

  public abstract class serviceScheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.serviceScheduleID>
  {
  }

  public abstract class usageScheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.usageScheduleID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FixedAsset.noteID>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FixedAsset.qty>
  {
  }

  public abstract class splittedFrom : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FixedAsset.splittedFrom>
  {
  }

  public abstract class disposalAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FixedAsset.disposalAmt>
  {
  }

  public abstract class salvageAmtAfterSplit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FixedAsset.salvageAmtAfterSplit>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FixedAsset.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FixedAsset.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FixedAsset.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FixedAsset.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FixedAsset.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FixedAsset.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FixedAsset.lastModifiedDateTime>
  {
  }

  [Serializable]
  public class FADetails : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _AssetID;
    protected string _Status;
    protected int? _LocationRevID;

    [PXDBInt(IsKey = true)]
    public virtual int? AssetID
    {
      get => this._AssetID;
      set => this._AssetID = value;
    }

    [PXDBString(1, IsFixed = true)]
    [FixedAssetStatus.List]
    public virtual string Status
    {
      get => this._Status;
      set => this._Status = value;
    }

    [PXDBInt]
    public virtual int? LocationRevID
    {
      get => this._LocationRevID;
      set => this._LocationRevID = value;
    }

    public abstract class assetID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FixedAsset.FADetails.assetID>
    {
    }

    public abstract class status : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FixedAsset.FADetails.status>
    {
    }

    public abstract class locationRevID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FixedAsset.FADetails.locationRevID>
    {
    }
  }
}
