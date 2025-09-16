// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FALocationHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

/// <summary>
/// Contains the history of changes of the asset location.
/// </summary>
[PXCacheName("FA Location History")]
[Serializable]
public class FALocationHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IFALocation
{
  protected int? _AssetID;
  protected 
  #nullable disable
  string _TransactionType;
  protected DateTime? _TransactionDate;
  protected string _PeriodID;
  protected int? _BuildingID;
  protected string _Floor;
  protected string _Room;
  protected int? _EmployeeID;
  protected Guid? _Custodian;
  protected int? _LocationID;
  protected string _Department;
  protected int? _RevisionID;
  protected int? _PrevRevisionID;
  protected int? _FAAccountID;
  protected int? _FASubID;
  protected int? _AccumulatedDepreciationAccountID;
  protected int? _AccumulatedDepreciationSubID;
  protected int? _DepreciatedExpenseAccountID;
  protected int? _DepreciatedExpenseSubID;
  protected string _RefNbr;
  protected string _Reason;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// A reference to <see cref="T:PX.Objects.FA.FixedAsset" />.
  /// This field is a part of the primary key.
  /// The full primary key contains the <see cref="P:PX.Objects.FA.FALocationHistory.AssetID" /> and the <see cref="P:PX.Objects.FA.FALocationHistory.RevisionID" /> fields.
  /// </summary>
  /// <value>
  /// By default, the value is set to the current fixed asset identifier.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FALocationHistory.assetID>>>>))]
  [PXDBDefault(typeof (FixedAsset.assetID))]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  /// <summary>The type of the change of the asset location.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.FA.FALocationHistory.transactionType.ListAttribute" />.
  /// By default, the value is set to <see cref="F:PX.Objects.FA.FALocationHistory.transactionType.Displacement" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [FALocationHistory.transactionType.List]
  [PXUIField(DisplayName = "Transaction Type")]
  public virtual string TransactionType
  {
    get => this._TransactionType;
    set => this._TransactionType = value;
  }

  /// <summary>The date when the asset location was changed.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Transaction Date")]
  [PXDefault]
  public virtual DateTime? TransactionDate
  {
    get => this._TransactionDate;
    set => this._TransactionDate = value;
  }

  /// <summary>
  /// The financial period when the asset location was changed.
  /// </summary>
  [FABookPeriodID(null, null, true, typeof (FALocationHistory.assetID), null, null, null, null, null)]
  [PXUIField(DisplayName = "Period ID")]
  public virtual string PeriodID
  {
    get => this._PeriodID;
    set => this._PeriodID = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<FAClass.assetID>), SubstituteKey = typeof (FAClass.assetCD), DescriptionField = typeof (FAClass.description), CacheGlobal = true)]
  [PXDefault(typeof (Search<FixedAsset.classID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>>>))]
  [PXUIField(DisplayName = "Asset Class", Visible = false)]
  public virtual int? ClassID { get; set; }

  /// <summary>
  /// The building in which the fixed asset is physically located.
  /// A reference to <see cref="T:PX.Objects.CR.Building" />.
  /// Changing this field leads to the creation of a revision of the asset location.
  /// </summary>
  /// <value>An integer identifier of the building.</value>
  [PXDBInt]
  [PXSelector(typeof (Search<Building.buildingID, Where<Building.branchID, Equal<Current<FALocationHistory.locationID>>>>), SubstituteKey = typeof (Building.buildingCD), DescriptionField = typeof (Building.description))]
  [PXUIField(DisplayName = "Building")]
  public virtual int? BuildingID
  {
    get => this._BuildingID;
    set => this._BuildingID = value;
  }

  /// <summary>
  /// The floor on which the fixed asset is physically located.
  /// Changing this field leads to the creation of a revision of the asset location.
  /// </summary>
  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Floor")]
  public virtual string Floor
  {
    get => this._Floor;
    set => this._Floor = value;
  }

  /// <summary>
  /// The room in which the fixed asset is physically located.
  /// Changing this field leads to the creation of a revision of the asset location.
  /// </summary>
  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Room")]
  public virtual string Room
  {
    get => this._Room;
    set => this._Room = value;
  }

  /// <summary>
  /// The custodian of the fixed asset.
  /// A reference to <see cref="T:PX.Objects.EP.EPEmployee" />.
  /// Changing this field leads to the creation of a revision of the asset location.
  /// </summary>
  /// <value>An integer identifier of the employee.</value>
  [PXDBInt]
  [PXSelector(typeof (EPEmployee.bAccountID), SubstituteKey = typeof (EPEmployee.acctCD), DescriptionField = typeof (EPEmployee.acctName))]
  [PXUIField(DisplayName = "Custodian")]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  /// <summary>
  /// The user of custodian of the fixed asset.
  /// A reference to <see cref="T:PX.SM.Users" />.
  /// </summary>
  /// <value>A GUID identifier of the user.</value>
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2019R2.")]
  [PXDBGuid(false)]
  [PXFormula(typeof (Selector<FALocationHistory.employeeID, EPEmployee.userID>))]
  public virtual Guid? Custodian
  {
    get => this._Custodian;
    set => this._Custodian = value;
  }

  /// <summary>
  /// The branch of the fixed asset.
  /// A reference to <see cref="T:PX.Objects.GL.Branch" />.
  /// Changing this field leads to the creation of a revision of the asset location.
  /// </summary>
  /// <value>
  /// An integer identifier of the branch.
  /// This is a required field.
  /// By default, the value is set to the branch of current custodian (if exists) or the current branch.
  /// </value>
  [Branch(typeof (Coalesce<Search2<PX.Objects.CR.Location.vBranchID, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<EPEmployee.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>>, Where<EPEmployee.bAccountID, Equal<Current<FALocationHistory.employeeID>>>>, Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>), null, true, true, true, IsDetail = false)]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  /// <summary>
  /// The department of the fixed asset.
  /// A reference to <see cref="T:PX.Objects.EP.EPDepartment" />.
  /// Changing this field leads to the creation of a revision of the asset location.
  /// </summary>
  /// <value>
  /// An integer identifier of the department.
  /// This is a required field.
  /// By default, the value is set to the custodian department.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<EPEmployee.departmentID, Where<EPEmployee.bAccountID, Equal<Current<FALocationHistory.employeeID>>>>))]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField(DisplayName = "Department")]
  public virtual string Department
  {
    get => this._Department;
    set => this._Department = value;
  }

  /// <summary>
  /// The number of the location revision.
  /// This field is a part of the primary key.
  /// The full primary key contains the <see cref="P:PX.Objects.FA.FALocationHistory.AssetID" /> and the <see cref="P:PX.Objects.FA.FALocationHistory.RevisionID" /> fields.
  /// </summary>
  /// <value>A unique integer autoincremented number.</value>
  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  /// <summary>
  /// The number of the previous revision of the asset location.
  /// </summary>
  /// <value>
  /// This is the unbound calculated field which contains the decremented number of the active revision (<see cref="P:PX.Objects.FA.FALocationHistory.RevisionID" />).
  /// </value>
  [PXInt]
  [PXDBCalced(typeof (Sub<FALocationHistory.revisionID, int1>), typeof (int))]
  public virtual int? PrevRevisionID
  {
    get => this._PrevRevisionID;
    set => this._PrevRevisionID = value;
  }

  /// <summary>
  /// The Fixed Assets account.
  /// A reference to <see cref="T:PX.Objects.GL.Account" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the account.
  /// This is a required field.
  /// By default, the value is set to the Fixed Assets account of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.FAAccountID" />).
  /// </value>
  [PXDefault(typeof (Search<FixedAsset.fAAccountID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>>>))]
  [AccountAny]
  public virtual int? FAAccountID
  {
    get => this._FAAccountID;
    set => this._FAAccountID = value;
  }

  /// <summary>
  /// The Fixed Assets subaccount associated with the Fixed Assets account (<see cref="P:PX.Objects.FA.FALocationHistory.FAAccountID" />).
  /// A reference to <see cref="T:PX.Objects.GL.Sub" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the subaccount.
  /// By default, the value is set to the Fixed Assets subaccount of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.FASubID" />).
  /// </value>
  [PXDefault]
  [SubAccountAny(typeof (FALocationHistory.fAAccountID))]
  public virtual int? FASubID
  {
    get => this._FASubID;
    set => this._FASubID = value;
  }

  /// <summary>
  /// The Accumulated Depreciation account.
  /// A reference to <see cref="T:PX.Objects.GL.Account" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the account.
  /// This is a required field if <see cref="P:PX.Objects.FA.FixedAsset.Depreciable" /> flag is set to <c>true</c>.
  /// By default, the value is set to the Accumulated Depreciation account of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.AccumulatedDepreciationAccountID" />).
  /// </value>
  [PXDefault(typeof (Search<FixedAsset.accumulatedDepreciationAccountID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>>>))]
  [AccountAny]
  public virtual int? AccumulatedDepreciationAccountID
  {
    get => this._AccumulatedDepreciationAccountID;
    set => this._AccumulatedDepreciationAccountID = value;
  }

  /// <summary>
  /// The Accumulated Depreciation subaccount associated with the Accumulated Depreciation account (<see cref="P:PX.Objects.FA.FALocationHistory.FAAccountID" />).
  /// A reference to <see cref="T:PX.Objects.GL.Sub" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the subaccount.
  /// This is a required field if <see cref="P:PX.Objects.FA.FixedAsset.Depreciable" /> flag is set to <c>true</c>.
  /// By default, the value is set to the Accumulated Depreciation subaccount of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.FASubID" />).
  /// </value>
  [PXDefault]
  [SubAccountAny(typeof (FALocationHistory.accumulatedDepreciationAccountID))]
  public virtual int? AccumulatedDepreciationSubID
  {
    get => this._AccumulatedDepreciationSubID;
    set => this._AccumulatedDepreciationSubID = value;
  }

  /// <summary>
  /// The Depreciation Expense account.
  /// A reference to <see cref="T:PX.Objects.GL.Account" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the account.
  /// This is a required field if <see cref="P:PX.Objects.FA.FixedAsset.Depreciable" /> flag is set to <c>true</c>.
  /// By default, the value is set to the Depreciation Expense account of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.DepreciatedExpenseAccountID" />).
  /// </value>
  [PXDefault(typeof (Search<FixedAsset.depreciatedExpenseAccountID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>>>))]
  [AccountAny]
  public virtual int? DepreciatedExpenseAccountID
  {
    get => this._DepreciatedExpenseAccountID;
    set => this._DepreciatedExpenseAccountID = value;
  }

  /// <summary>
  /// The Depreciation Expense subaccount associated with the Depreciation Expense account (<see cref="P:PX.Objects.FA.FALocationHistory.DepreciatedExpenseAccountID" />).
  /// A reference to <see cref="T:PX.Objects.GL.Sub" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the subaccount.
  /// This is a required field if <see cref="P:PX.Objects.FA.FixedAsset.Depreciable" /> flag is set to <c>true</c>.
  /// By default, the value is set to the Depreciation Expense subaccount of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.DepreciatedExpenseSubID" />).
  /// </value>
  [PXDefault]
  [SubAccountAny(typeof (FALocationHistory.depreciatedExpenseAccountID))]
  public virtual int? DepreciatedExpenseSubID
  {
    get => this._DepreciatedExpenseSubID;
    set => this._DepreciatedExpenseSubID = value;
  }

  /// <summary>
  /// The Disposal account.
  /// A reference to <see cref="T:PX.Objects.GL.Account" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the account.
  /// By default, the value is set to the Disposal account of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.DisposalAccountID" />).
  /// </value>
  [PXDefault(typeof (Search<FixedAsset.disposalAccountID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>>>))]
  [AccountAny(DisplayName = "Proceeds Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public virtual int? DisposalAccountID { get; set; }

  /// <summary>
  /// The Disposal subaccount associated with the Disposal account (<see cref="P:PX.Objects.FA.FALocationHistory.DisposalAccountID" />).
  /// A reference to <see cref="T:PX.Objects.GL.Sub" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the subaccount.
  /// By default, the value is set to the Disposal subaccount of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.DisposalSubID" />).
  /// </value>
  [SubAccountAny(typeof (FALocationHistory.disposalAccountID), DisplayName = "Proceeds Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual int? DisposalSubID { get; set; }

  /// <summary>
  /// The Gain account.
  /// A reference to <see cref="T:PX.Objects.GL.Account" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the account.
  /// This is a required field.
  /// By default, the value is set to the Gain account of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.GainAcctID" />).
  /// </value>
  [PXDefault(typeof (Search<FixedAsset.gainAcctID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>>>))]
  [AccountAny(DisplayName = "Gain Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public virtual int? GainAcctID { get; set; }

  /// <summary>
  /// The Gain subaccount associated with the Gain account (<see cref="!:GainAccountID" />).
  /// A reference to <see cref="T:PX.Objects.GL.Sub" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the subaccount.
  /// This is a required field.
  /// By default, the value is set to the Gain subaccount of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.GainSubID" />).
  /// </value>
  [PXDefault]
  [SubAccountAny(typeof (FALocationHistory.gainAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Gain Sub.")]
  public virtual int? GainSubID { get; set; }

  /// <summary>
  /// The Loss account.
  /// A reference to <see cref="T:PX.Objects.GL.Account" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the account.
  /// This is a required field.
  /// By default, the value is set to the Loss account of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.LossAcctID" />).
  /// </value>
  [PXDefault(typeof (Search<FixedAsset.lossAcctID, Where<FixedAsset.assetID, Equal<Current<FixedAsset.assetID>>>>))]
  [AccountAny(DisplayName = "Loss Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public virtual int? LossAcctID { get; set; }

  /// <summary>
  /// The Loss subaccount associated with the Loss account (<see cref="!:LossAccountID" />).
  /// A reference to <see cref="T:PX.Objects.GL.Sub" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the subaccount.
  /// This is a required field.
  /// By default, the value is set to the Loss subaccount of the fixed asset (<see cref="P:PX.Objects.FA.FixedAsset.LossSubID" />).
  /// </value>
  [PXDefault]
  [SubAccountAny(typeof (FALocationHistory.lossAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Loss Sub.")]
  public virtual int? LossSubID { get; set; }

  /// <summary>
  /// The reference number of the related transfer document.
  /// A reference to <see cref="T:PX.Objects.FA.FARegister" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the fixed asset document.
  /// By default, the value is set to the number of the current document.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (FARegister.refNbr))]
  [PXSelector(typeof (Search<FARegister.refNbr>))]
  [PXUIField(DisplayName = "Transfer Document Nbr.", Visible = false)]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>
  /// The reason of the change of the asset location.
  /// The information field, which value is entered manually.
  /// </summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Reason")]
  public virtual string Reason
  {
    get => this._Reason;
    set => this._Reason = value;
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
  [PXUIField(DisplayName = "Modification Date")]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public virtual int? BranchID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  public class PK : 
    PrimaryKeyOf<FALocationHistory>.By<FALocationHistory.assetID, FALocationHistory.revisionID>
  {
    public static FALocationHistory Find(
      PXGraph graph,
      int? assetID,
      int? revisionID,
      PKFindOptions options = 0)
    {
      return (FALocationHistory) PrimaryKeyOf<FALocationHistory>.By<FALocationHistory.assetID, FALocationHistory.revisionID>.FindBy(graph, (object) assetID, (object) revisionID, options);
    }
  }

  public static class FK
  {
    public class FixedAsset : 
      PrimaryKeyOf<FixedAsset>.By<FixedAsset.assetID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.assetID>
    {
    }

    public class AssetClass : 
      PrimaryKeyOf<FAClass>.By<FAClass.assetID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.classID>
    {
    }

    public class Custodian : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.employeeID>
    {
    }

    public class FixedAssetsAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.fAAccountID>
    {
    }

    public class FixedAssetsSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.fASubID>
    {
    }

    public class AccumulatedDepreciationAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.accumulatedDepreciationAccountID>
    {
    }

    public class AccumulatedDepreciationSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.accumulatedDepreciationSubID>
    {
    }

    public class DepreciationExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.depreciatedExpenseAccountID>
    {
    }

    public class DepreciationExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.depreciatedExpenseSubID>
    {
    }

    public class ProceedsAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.disposalAccountID>
    {
    }

    public class ProceedsSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.disposalSubID>
    {
    }

    public class GainAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.gainAcctID>
    {
    }

    public class GainSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.gainSubID>
    {
    }

    public class LossAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.lossAcctID>
    {
    }

    public class LossSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FALocationHistory>.By<FALocationHistory.lossSubID>
    {
    }
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.assetID>
  {
  }

  public abstract class transactionType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FALocationHistory.transactionType>
  {
    public const string Displacement = "D";
    public const string ChangeResponsible = "R";

    /// <summary>The type of the change of the asset location.</summary>
    /// <value>
    /// The class exposes the following values:
    /// <list type="bullet">
    /// <item> <term><c>"D"</c></term> <description>Displacement (change of the physical location)</description> </item>
    /// <item> <term><c>"R"</c></term> <description>Change of the responsible</description> </item>
    /// </list>
    /// </value>
    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "D", "R" }, new string[2]
        {
          "Displacement",
          "Change Responsible"
        })
      {
      }
    }

    public class displacement : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FALocationHistory.transactionType.displacement>
    {
      public displacement()
        : base("D")
      {
      }
    }

    public class changeResponsible : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FALocationHistory.transactionType.changeResponsible>
    {
      public changeResponsible()
        : base("R")
      {
      }
    }
  }

  public abstract class transactionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FALocationHistory.transactionDate>
  {
  }

  public abstract class periodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FALocationHistory.periodID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.classID>
  {
  }

  public abstract class buildingID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.buildingID>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FALocationHistory.floor>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FALocationHistory.room>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.employeeID>
  {
  }

  public abstract class custodian : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FALocationHistory.custodian>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.locationID>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FALocationHistory.department>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.revisionID>
  {
  }

  public abstract class prevRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FALocationHistory.prevRevisionID>
  {
  }

  public abstract class fAAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.fAAccountID>
  {
  }

  public abstract class fASubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.fASubID>
  {
  }

  public abstract class accumulatedDepreciationAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FALocationHistory.accumulatedDepreciationAccountID>
  {
  }

  public abstract class accumulatedDepreciationSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FALocationHistory.accumulatedDepreciationSubID>
  {
  }

  public abstract class depreciatedExpenseAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FALocationHistory.depreciatedExpenseAccountID>
  {
  }

  public abstract class depreciatedExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FALocationHistory.depreciatedExpenseSubID>
  {
  }

  public abstract class disposalAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FALocationHistory.disposalAccountID>
  {
  }

  public abstract class disposalSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.disposalSubID>
  {
  }

  public abstract class gainAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.gainAcctID>
  {
  }

  public abstract class gainSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.gainSubID>
  {
  }

  public abstract class lossAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.lossAcctID>
  {
  }

  public abstract class lossSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistory.lossSubID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FALocationHistory.refNbr>
  {
  }

  public abstract class reason : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FALocationHistory.reason>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FALocationHistory.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FALocationHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FALocationHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FALocationHistory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FALocationHistory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FALocationHistory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FALocationHistory.lastModifiedDateTime>
  {
  }
}
