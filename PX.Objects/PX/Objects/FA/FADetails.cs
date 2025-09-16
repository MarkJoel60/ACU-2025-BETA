// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FADetails
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.FA;

/// <summary>
/// Contains the additional properties of <see cref="T:PX.Objects.FA.FixedAsset" />.
/// </summary>
[PXProjection(typeof (Select2<FADetails, LeftJoin<FABookHistoryRecon, On<FABookHistoryRecon.assetID, Equal<FADetails.assetID>, And<FABookHistoryRecon.updateGL, Equal<True>>>>>), new Type[] {typeof (FADetails)})]
[PXCacheName("FA Details")]
[Serializable]
public class FADetails : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected 
  #nullable disable
  string _PropertyType;
  protected string _Status;
  protected string _Condition;
  protected DateTime? _ReceiptDate;
  protected string _ReceiptType;
  protected string _ReceiptNbr;
  protected string _PONumber;
  protected string _BillNumber;
  protected string _Manufacturer;
  protected string _Model;
  protected string _SerialNumber;
  protected int? _LocationRevID;
  protected Decimal? _CurrentCost;
  protected Decimal? _AccrualBalance;
  protected bool? _IsReconciled;
  protected string _Barcode;
  protected string _TagNbr;
  protected DateTime? _LastCountDate;
  protected DateTime? _DepreciateFromDate;
  protected Decimal? _AcquisitionCost;
  protected Decimal? _SalvageAmount;
  protected Decimal? _ReplacementCost;
  protected DateTime? _DisposalDate;
  protected string _DisposalPeriodID;
  protected int? _DisposalMethodID;
  protected Decimal? _SaleAmount;
  protected string _Warrantor;
  protected DateTime? _WarrantyExpirationDate;
  protected string _WarrantyCertificateNumber;
  protected DateTime? _NextServiceDate;
  protected Decimal? _NextServiceValue;
  protected DateTime? _NextMeasurementUsageDate;
  protected DateTime? _LastServiceDate;
  protected Decimal? _LastServiceValue;
  protected DateTime? _LastMeasurementUsageDate;
  protected Decimal? _TotalExpectedUsage;
  protected Decimal? _FairMarketValue;
  protected int? _LessorID;
  protected int? _LeaseRentTerm;
  protected string _LeaseNumber;
  protected Decimal? _RentAmount;
  protected Decimal? _RetailCost;
  protected string _ManufacturingYear;
  protected string _ReportingLineNbr;
  protected bool? _IsTemplate;
  protected int? _TemplateID;
  protected bool? _Hold;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// A reference to <see cref="T:PX.Objects.FA.FixedAsset" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the fixed asset.
  /// It is a required value.
  /// By default, the value is set to the current fixed asset identifier.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FADetails.assetID>>>>))]
  [PXDBDefault(typeof (FixedAsset.assetID))]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  /// <summary>The type of the fixed asset property.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.FA.FADetails.propertyType.ListAttribute" />.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [FADetails.propertyType.List]
  [PXDefault("CP")]
  [PXUIField(DisplayName = "Property Type")]
  public virtual string PropertyType
  {
    get => this._PropertyType;
    set => this._PropertyType = value;
  }

  /// <summary>The status of the fixed asset.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.FA.FixedAssetStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [FixedAssetStatus.List]
  [PXDefault("A")]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>The condition of the fixed asset.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.FA.FADetails.condition.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [FADetails.condition.List]
  [PXDefault("G")]
  public virtual string Condition
  {
    get => this._Condition;
    set => this._Condition = value;
  }

  /// <summary>The acquisition date of the fixed asset.</summary>
  [PXDBDate]
  [PXDefault(typeof (PX.Objects.PO.POReceipt.receiptDate))]
  [PXUIField(DisplayName = "Receipt Date")]
  public virtual DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
  }

  /// <summary>
  /// The type of the purchase receipt.
  /// This field is a part of the compound reference to the purchasing document (<see cref="T:PX.Objects.PO.POReceipt" />).
  /// The full reference contains the <see cref="T:PX.Objects.FA.FADetails.receiptType" /> and <see cref="T:PX.Objects.FA.FADetails.receiptNbr" /> fields.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PO.POReceiptType.ListAttribute" />.
  /// </value>
  [PXDBString(2, IsFixed = true, InputMask = "")]
  [POReceiptType.List]
  [PXUIField(DisplayName = "Receipt Type")]
  public virtual string ReceiptType
  {
    get => this._ReceiptType;
    set => this._ReceiptType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [POReceiptType.RefNbr(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Optional<PX.Objects.PO.POReceipt.receiptType>>>>), Filterable = true)]
  [PXUIField(DisplayName = "Receipt Nbr.")]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  /// <summary>
  /// The number of the purchase order related to the purchase document.
  /// </summary>
  /// <value>The information field.</value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Number")]
  [PXDefault(typeof (Search2<PX.Objects.PO.POOrder.orderNbr, InnerJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.pOType, Equal<PX.Objects.PO.POOrder.orderType>, And<PX.Objects.PO.POReceiptLine.pONbr, Equal<PX.Objects.PO.POOrder.orderNbr>>>>, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Current<FADetails.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Current<FADetails.receiptNbr>>>>>))]
  public virtual string PONumber
  {
    get => this._PONumber;
    set => this._PONumber = value;
  }

  /// <summary>The number of the bill.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Bill Number")]
  public virtual string BillNumber
  {
    get => this._BillNumber;
    set => this._BillNumber = value;
  }

  /// <summary>The name of the fixed asset manufacturer.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Manufacturer")]
  public virtual string Manufacturer
  {
    get => this._Manufacturer;
    set => this._Manufacturer = value;
  }

  /// <summary>The name of the fixed asset model.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Model")]
  public virtual string Model
  {
    get => this._Model;
    set => this._Model = value;
  }

  /// <summary>The serial number of the fixed asset.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Serial Number")]
  public virtual string SerialNumber
  {
    get => this._SerialNumber;
    set => this._SerialNumber = value;
  }

  /// <summary>
  /// The number of the actual revision of the asset location.
  /// This field is a part of the compound reference to <see cref="T:PX.Objects.FA.FALocationHistory" />.
  /// The full reference contains the <see cref="T:PX.Objects.FA.FADetails.assetID" /> and <see cref="T:PX.Objects.FA.FADetails.locationRevID" /> fields.
  /// </summary>
  [PXDBInt]
  public virtual int? LocationRevID
  {
    get => this._LocationRevID;
    set => this._LocationRevID = value;
  }

  /// <summary>
  /// The cost of the fixed asset in the current depreciation period.
  /// </summary>
  /// <value>
  /// The value is read-only and is selected from the appropriate <see cref="T:PX.Objects.FA.FABookHistoryRecon.ytdAcquired" /> field.
  /// </value>
  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistoryRecon.ytdAcquired))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Basis", Enabled = false)]
  public virtual Decimal? CurrentCost
  {
    get => this._CurrentCost;
    set => this._CurrentCost = value;
  }

  /// <summary>
  /// The reconciled part of the current cost of the fixed asset.
  /// </summary>
  /// <value>
  /// The value is read-only and is selected from the appropriate <see cref="T:PX.Objects.FA.FABookHistoryRecon.ytdReconciled" /> field.
  /// </value>
  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistoryRecon.ytdReconciled))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AccrualBalance
  {
    get => this._AccrualBalance;
    set => this._AccrualBalance = value;
  }

  /// <summary>
  /// A flag that indicates (if set to <c>true</c>) that a fixed asset is fully reconciled with the General Ledger module.
  /// </summary>
  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<IsNull<FABookHistoryRecon.ytdAcquired, decimal0>, Equal<IsNull<FABookHistoryRecon.ytdReconciled, decimal0>>>, True>, False>), typeof (bool))]
  public virtual bool? IsReconciled
  {
    get => this._IsReconciled;
    set => this._IsReconciled = value;
  }

  /// <summary>
  /// The identifier of the transfer period.
  /// This is an unbound service field that is used to pass the parameter to transfer processing.
  /// </summary>
  [PXString(6, IsFixed = true)]
  [PXUIField(DisplayName = "Transfer Period", Enabled = false)]
  [FinPeriodIDFormatting]
  [Obsolete("This property is not used anymore and will be removed in Acumatica 2018R2")]
  public virtual string TransferPeriod { get; set; }

  /// <summary>The barcode of the fixed asset.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(20, IsUnicode = true)]
  [PXUIField(DisplayName = "Barcode")]
  public virtual string Barcode
  {
    get => this._Barcode;
    set => this._Barcode = value;
  }

  /// <summary>The tag of the fixed asset.</summary>
  /// <value>
  /// The value can be entered manually or can be auto-numbered.
  /// </value>
  [PXDBString(20, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Tag Number", Enabled = false)]
  [FADetails.tagNbr.Numbering]
  public virtual string TagNbr
  {
    get => this._TagNbr;
    set => this._TagNbr = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Count Date")]
  public virtual DateTime? LastCountDate
  {
    get => this._LastCountDate;
    set => this._LastCountDate = value;
  }

  /// <summary>The date when depreciation of the fixed asset starts.</summary>
  /// <value>
  /// The date can not be greater than <see cref="T:PX.Objects.FA.FADetails.receiptDate" />.
  /// </value>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Placed-in-Service Date")]
  [PXFormula(typeof (IIf<Where<Current<FixedAsset.underConstruction>, Equal<False>>, FADetails.receiptDate, Null>))]
  public virtual DateTime? DepreciateFromDate
  {
    get => this._DepreciateFromDate;
    set => this._DepreciateFromDate = value;
  }

  /// <summary>
  /// The cost of the fixed asset at the time of acquisition.
  /// </summary>
  /// <value>The value can be changed during the life of the asset.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Orig. Acquisition Cost")]
  public virtual Decimal? AcquisitionCost
  {
    get => this._AcquisitionCost;
    set => this._AcquisitionCost = value;
  }

  /// <summary>The salvage amount of the fixed asset.</summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Salvage Amount")]
  public virtual Decimal? SalvageAmount
  {
    get => this._SalvageAmount;
    set => this._SalvageAmount = value;
  }

  /// <summary>The replacement cost of the fixed asset.</summary>
  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Replacement Cost")]
  public virtual Decimal? ReplacementCost
  {
    get => this._ReplacementCost;
    set => this._ReplacementCost = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  public virtual string BaseCuryID { get; set; }

  /// <summary>The date of fixed asset disposal.</summary>
  /// <value>The field is filled in only for disposed fixed assets.</value>
  [PXDBDate]
  [PXUIField(DisplayName = "Disposal Date", Enabled = false)]
  public virtual DateTime? DisposalDate
  {
    get => this._DisposalDate;
    set => this._DisposalDate = value;
  }

  /// <summary>The date of fixed asset disposal.</summary>
  /// <value>The field is filled in only for disposed fixed assets.</value>
  [PXDate]
  [PXFormula(typeof (Switch<Case<Where<FADetails.status, Equal<FixedAssetStatus.disposed>>, FADetails.disposalDate>, Null>))]
  [PXUIField(DisplayName = "Disposal Date", Enabled = false)]
  public virtual DateTime? DisplayDisposalDate { get; set; }

  /// <summary>The period of fixed asset disposal.</summary>
  /// <value>The field is filled in only for disposed fixed assets.</value>
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (FADetails.disposalPeriodID))]
  public virtual string DisposalPeriodID
  {
    get => this._DisposalPeriodID;
    set => this._DisposalPeriodID = value;
  }

  /// <summary>The period of fixed asset disposal.</summary>
  /// <value>The field is filled in only for disposed fixed assets.</value>
  [PXString(6, IsFixed = true)]
  public virtual string DisplayDisposalPeriodID { get; set; }

  /// <summary>
  /// A reference to <see cref="T:PX.Objects.FA.FADisposalMethod" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the disposal method.
  /// The field is filled in only for disposed fixed assets.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (FADisposalMethod.disposalMethodID), SubstituteKey = typeof (FADisposalMethod.disposalMethodCD), DescriptionField = typeof (FADisposalMethod.description))]
  [PXUIField(DisplayName = "Disposal Method", Enabled = false)]
  public virtual int? DisposalMethodID
  {
    get => this._DisposalMethodID;
    set => this._DisposalMethodID = value;
  }

  /// <summary>
  /// A reference to <see cref="T:PX.Objects.FA.FADisposalMethod" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the disposal method.
  /// The field is filled in only for disposed fixed assets.
  /// </value>
  [PXInt]
  [PXSelector(typeof (FADisposalMethod.disposalMethodID), SubstituteKey = typeof (FADisposalMethod.disposalMethodCD), DescriptionField = typeof (FADisposalMethod.description))]
  [PXFormula(typeof (Switch<Case<Where<FADetails.status, Equal<FixedAssetStatus.disposed>>, FADetails.disposalMethodID>, Null>))]
  [PXUIField(DisplayName = "Disposal Method", Enabled = false)]
  public virtual int? DisplayDisposalMethodID { get; set; }

  /// <summary>The amount of fixed asset disposal.</summary>
  /// <value>
  /// The field is filled in only for disposed fixed assets. The value of the field can be zero.
  /// </value>
  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Disposal Amount", Enabled = false)]
  public virtual Decimal? SaleAmount
  {
    get => this._SaleAmount;
    set => this._SaleAmount = value;
  }

  /// <summary>The amount of fixed asset disposal.</summary>
  /// <value>
  /// The field is filled in only for disposed fixed assets. The value of the field can be zero.
  /// </value>
  [PXBaseCury]
  [PXFormula(typeof (Switch<Case<Where<FADetails.status, Equal<FixedAssetStatus.disposed>>, FADetails.saleAmount>, decimal0>))]
  [PXUIField(DisplayName = "Disposal Amount", Enabled = false)]
  public virtual Decimal? DisplaySaleAmount { get; set; }

  /// <summary>The name of the fixed asset warrantor.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Warrantor")]
  public virtual string Warrantor
  {
    get => this._Warrantor;
    set => this._Warrantor = value;
  }

  /// <summary>The expiration date of the fixed asset warranty.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBDate]
  [PXUIField(DisplayName = "Warranty Expires On")]
  public virtual DateTime? WarrantyExpirationDate
  {
    get => this._WarrantyExpirationDate;
    set => this._WarrantyExpirationDate = value;
  }

  /// <summary>The certificate number of the fixed asset warranty.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Warranty Certificate Number")]
  public virtual string WarrantyCertificateNumber
  {
    get => this._WarrantyCertificateNumber;
    set => this._WarrantyCertificateNumber = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Next Service Date")]
  public virtual DateTime? NextServiceDate
  {
    get => this._NextServiceDate;
    set => this._NextServiceDate = value;
  }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Next Service Value")]
  public virtual Decimal? NextServiceValue
  {
    get => this._NextServiceValue;
    set => this._NextServiceValue = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Next Measurement Date")]
  [PXFormula(typeof (CalcNextMeasurementDate<FADetails.lastMeasurementUsageDate, FADetails.depreciateFromDate, FADetails.assetID>))]
  public virtual DateTime? NextMeasurementUsageDate
  {
    get => this._NextMeasurementUsageDate;
    set => this._NextMeasurementUsageDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Service Date")]
  public virtual DateTime? LastServiceDate
  {
    get => this._LastServiceDate;
    set => this._LastServiceDate = value;
  }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Last Service Value")]
  public virtual Decimal? LastServiceValue
  {
    get => this._LastServiceValue;
    set => this._LastServiceValue = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Measurement Date", Enabled = false)]
  public virtual DateTime? LastMeasurementUsageDate
  {
    get => this._LastMeasurementUsageDate;
    set => this._LastMeasurementUsageDate = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Expected Usage")]
  public virtual Decimal? TotalExpectedUsage
  {
    get => this._TotalExpectedUsage;
    set => this._TotalExpectedUsage = value;
  }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Fair Market Value")]
  public virtual Decimal? FairMarketValue
  {
    get => this._FairMarketValue;
    set => this._FairMarketValue = value;
  }

  [VendorNonEmployeeActive]
  [PXUIField(DisplayName = "Lessor")]
  public virtual int? LessorID
  {
    get => this._LessorID;
    set => this._LessorID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Lease/Rent Term, months")]
  public virtual int? LeaseRentTerm
  {
    get => this._LeaseRentTerm;
    set => this._LeaseRentTerm = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Lease Number")]
  public virtual string LeaseNumber
  {
    get => this._LeaseNumber;
    set => this._LeaseNumber = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Rent Amount")]
  public virtual Decimal? RentAmount
  {
    get => this._RentAmount;
    set => this._RentAmount = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Retail Cost")]
  public virtual Decimal? RetailCost
  {
    get => this._RetailCost;
    set => this._RetailCost = value;
  }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Manufacturing Year")]
  public virtual string ManufacturingYear
  {
    get => this._ManufacturingYear;
    set => this._ManufacturingYear = value;
  }

  [PXDBString(3, IsFixed = true, InputMask = "")]
  [FADetails.reportingLineNbr.List]
  [PXDefault("NAP")]
  [PXUIField(DisplayName = "Personal Property Type")]
  public virtual string ReportingLineNbr
  {
    get => this._ReportingLineNbr;
    set => this._ReportingLineNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Template")]
  public virtual bool? IsTemplate
  {
    get => this._IsTemplate;
    set => this._IsTemplate = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search2<FixedAsset.assetID, InnerJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>>, Where<FADetails.isTemplate, Equal<True>>>), new Type[] {typeof (FixedAsset.assetCD), typeof (FixedAsset.assetTypeID), typeof (FixedAsset.description), typeof (FixedAsset.usefulLife)}, SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField(DisplayName = "Template", Enabled = false)]
  public virtual int? TemplateID
  {
    get => this._TemplateID;
    set => this._TemplateID = value;
  }

  /// <summary>
  /// A flag that indicates (if set to <c>true</c>) that the fixed asset is on hold and thus cannot be depreciated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hold")]
  public virtual bool? Hold => new bool?(this.Status == "H");

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

  public class PK : PrimaryKeyOf<FADetails>.By<FADetails.assetID>
  {
    public static FADetails Find(PXGraph graph, int? assetID, PKFindOptions options = 0)
    {
      return (FADetails) PrimaryKeyOf<FADetails>.By<FADetails.assetID>.FindBy(graph, (object) assetID, options);
    }
  }

  public static class FK
  {
    public class FixedAsset : 
      PrimaryKeyOf<FixedAsset>.By<FixedAsset.assetID>.ForeignKeyOf<FADetails>.By<FADetails.assetID>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<PX.Objects.PO.POReceipt>.By<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>.ForeignKeyOf<FADetails>.By<FADetails.receiptType, FADetails.receiptNbr>
    {
    }
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADetails.assetID>
  {
  }

  public abstract class propertyType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.propertyType>
  {
    public const string Property = "CP";
    public const string GrantProperty = "GP";
    public const string Leased = "CL";
    public const string LeasedtoOthers = "LO";
    public const string Rented = "CR";
    public const string RentedtoOthers = "RO";
    public const string Credit = "CC";

    /// <summary>The type of the fixed asset property.</summary>
    /// <value>
    /// The class exposes the following values:
    /// <list type="bullet">
    /// <item> <term><c>CP</c></term> <description>Property</description> </item>
    /// <item> <term><c>GP</c></term> <description>Grant Property</description> </item>
    /// <item> <term><c>CL</c></term> <description>Leased</description> </item>
    /// <item> <term><c>LO</c></term> <description>Leased to Others</description> </item>
    /// <item> <term><c>CR</c></term> <description>Rented</description> </item>
    /// <item> <term><c>RO</c></term> <description>Rented to Others</description> </item>
    /// <item> <term><c>CC</c></term> <description>To the Credit of</description> </item>
    /// </list>
    /// </value>
    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[7]
        {
          "CP",
          "GP",
          "CL",
          "LO",
          "CR",
          "RO",
          "CC"
        }, new string[7]
        {
          "Property",
          "Grant Property",
          "Leased",
          "Leased to Others",
          "Rented",
          "Rented to Others",
          "To the Credit of"
        })
      {
      }
    }

    public class property : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.propertyType.property>
    {
      public property()
        : base("CP")
      {
      }
    }

    public class grantProperty : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADetails.propertyType.grantProperty>
    {
      public grantProperty()
        : base("GP")
      {
      }
    }

    public class leased : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.propertyType.leased>
    {
      public leased()
        : base("CL")
      {
      }
    }

    public class leasedtoOthers : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADetails.propertyType.leasedtoOthers>
    {
      public leasedtoOthers()
        : base("LO")
      {
      }
    }

    public class rented : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.propertyType.rented>
    {
      public rented()
        : base("CR")
      {
      }
    }

    public class rentedtoOthers : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADetails.propertyType.rentedtoOthers>
    {
      public rentedtoOthers()
        : base("RO")
      {
      }
    }

    public class credit : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.propertyType.credit>
    {
      public credit()
        : base("CC")
      {
      }
    }
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.status>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.condition>
  {
    public const string Good = "G";
    public const string Avg = "A";
    public const string Poor = "P";

    /// <summary>The condition of the fixed asset.</summary>
    /// <value>
    /// The class exposes the following values:
    /// <list type="bullet">
    /// <item> <term><c>G</c></term> <description>Good</description> </item>
    /// <item> <term><c>A</c></term> <description>Average</description> </item>
    /// <item> <term><c>P</c></term> <description>Poor</description> </item>
    /// </list>
    /// </value>
    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "G", "A", "P" }, new string[3]
        {
          "Good",
          "Average",
          "Poor"
        })
      {
      }
    }

    public class good : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.condition.good>
    {
      public good()
        : base("G")
      {
      }
    }

    public class avg : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.condition.avg>
    {
      public avg()
        : base("A")
      {
      }
    }

    public class poor : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.condition.poor>
    {
      public poor()
        : base("P")
      {
      }
    }
  }

  public abstract class receiptDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FADetails.receiptDate>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.receiptNbr>
  {
  }

  public abstract class pONumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.pONumber>
  {
  }

  public abstract class billNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.billNumber>
  {
  }

  public abstract class manufacturer : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.manufacturer>
  {
  }

  public abstract class model : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.model>
  {
  }

  public abstract class serialNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.serialNumber>
  {
  }

  public abstract class locationRevID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADetails.locationRevID>
  {
  }

  public abstract class currentCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FADetails.currentCost>
  {
  }

  public abstract class accrualBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.accrualBalance>
  {
  }

  public abstract class isReconciled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FADetails.isReconciled>
  {
  }

  [Obsolete("This class is not used anymore and will be removed in Acumatica 2018R2")]
  public abstract class transferPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.transferPeriod>
  {
  }

  public abstract class barcode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.barcode>
  {
  }

  public abstract class tagNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.tagNbr>
  {
    public class NumberingAttribute : AutoNumberAttribute
    {
      public NumberingAttribute()
        : base(typeof (FASetup.tagNumberingID), typeof (FADetails.createdDateTime))
      {
        this.NullMode = AutoNumberAttribute.NullNumberingMode.UserNumbering;
      }

      public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
      {
        base.RowPersisting(sender, e);
        FASetup current = (FASetup) sender.Graph.Caches[typeof (FASetup)].Current;
        FADetails row = (FADetails) e.Row;
        FixedAsset fixedAsset = FixedAsset.PK.Find(sender.Graph, row.AssetID);
        if (row == null || !current.CopyTagFromAssetID.GetValueOrDefault() || (e.Operation & 3) != 2)
          return;
        row.TagNbr = fixedAsset.AssetCD;
      }
    }
  }

  public abstract class lastCountDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.lastCountDate>
  {
  }

  public abstract class depreciateFromDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.depreciateFromDate>
  {
  }

  public abstract class acquisitionCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.acquisitionCost>
  {
  }

  public abstract class salvageAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FADetails.salvageAmount>
  {
  }

  public abstract class replacementCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.replacementCost>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FADetails.baseCuryID>
  {
  }

  public abstract class disposalDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FADetails.disposalDate>
  {
  }

  public abstract class displayDisposalDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.displayDisposalDate>
  {
  }

  public abstract class disposalPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.disposalPeriodID>
  {
  }

  public abstract class displayDisposalPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.displayDisposalPeriodID>
  {
  }

  public abstract class disposalMethodID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADetails.disposalMethodID>
  {
  }

  public abstract class displayDisposalMethodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FADetails.displayDisposalMethodID>
  {
  }

  public abstract class saleAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FADetails.saleAmount>
  {
  }

  public abstract class displaySaleAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.displaySaleAmount>
  {
  }

  public abstract class warrantor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.warrantor>
  {
  }

  public abstract class warrantyExpirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.warrantyExpirationDate>
  {
  }

  public abstract class warrantyCertificateNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.warrantyCertificateNumber>
  {
  }

  public abstract class nextServiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.nextServiceDate>
  {
  }

  public abstract class nextServiceValue : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.nextServiceValue>
  {
  }

  public abstract class nextMeasurementUsageDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.nextMeasurementUsageDate>
  {
  }

  public abstract class lastServiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.lastServiceDate>
  {
  }

  public abstract class lastServiceValue : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.lastServiceValue>
  {
  }

  public abstract class lastMeasurementUsageDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.lastMeasurementUsageDate>
  {
  }

  public abstract class totalExpectedUsage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.totalExpectedUsage>
  {
  }

  public abstract class fairMarketValue : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.fairMarketValue>
  {
  }

  public abstract class lessorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADetails.lessorID>
  {
  }

  public abstract class leaseRentTerm : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADetails.leaseRentTerm>
  {
  }

  public abstract class leaseNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.leaseNumber>
  {
  }

  public abstract class rentAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FADetails.rentAmount>
  {
  }

  public abstract class retailCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FADetails.retailCost>
  {
  }

  public abstract class manufacturingYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.manufacturingYear>
  {
  }

  public abstract class reportingLineNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.reportingLineNbr>
  {
    public const string NotAplicable = "NAP";
    public const string Line10 = "L10";
    public const string Line11 = "L11";
    public const string Line12 = "L12";
    public const string Line13 = "L13";
    public const string Line14 = "L14";
    public const string Line15 = "L15";
    public const string Line16 = "L16";
    public const string Line16a = "A16";
    public const string Line17 = "L17";
    public const string Line18 = "L18";
    public const string Line19 = "L19";
    public const string Line20 = "L20";
    public const string Line21 = "L21";
    public const string Line22 = "L22";
    public const string Line23 = "L23";
    public const string Others = "0TH";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[17]
        {
          "NAP",
          "L10",
          "L11",
          "L12",
          "L13",
          "L14",
          "L15",
          "L16",
          "A16",
          "L17",
          "L18",
          "L19",
          "L20",
          "L21",
          "L22",
          "L23",
          "0TH"
        }, new string[17]
        {
          "Not Applicable",
          "10. Office Furniture & Machines & Library",
          "11. EDP Equipment/Computers/Word Processors",
          "12. Store Bar & Lounge and Restaurant Furniture & Equipment",
          "13. Machinery and Manufacturing Equipment",
          "14. Farm Grove and Dairy Equipment",
          "15. Professional Medical Dental & Laboratory Equipment",
          "16. Hotel Motel & Apartment Complex",
          "16a. Rental Units - Stove Refrig. Furniture Drapes & Appliances",
          "17. Mobile Home Attachments",
          "18. Service Station & Bulk Plant Equipment",
          "19. Sings - Billboard Pole Wall Portable Directional Etc.",
          "20. Leasehold improvements",
          "21. Pollution Control Equipment",
          "22. Equipment owned by you but rented leased or held by others",
          "23. Supplies - Not Held for Resale",
          "24. Other"
        })
      {
      }
    }

    public class notAplicable : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADetails.reportingLineNbr.notAplicable>
    {
      public notAplicable()
        : base("NAP")
      {
      }
    }

    public class line10 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line10>
    {
      public line10()
        : base("L10")
      {
      }
    }

    public class line11 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line11>
    {
      public line11()
        : base("L11")
      {
      }
    }

    public class line12 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line12>
    {
      public line12()
        : base("L12")
      {
      }
    }

    public class line13 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line13>
    {
      public line13()
        : base("L13")
      {
      }
    }

    public class line14 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line14>
    {
      public line14()
        : base("L14")
      {
      }
    }

    public class line15 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line15>
    {
      public line15()
        : base("L15")
      {
      }
    }

    public class line16 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line16>
    {
      public line16()
        : base("L16")
      {
      }
    }

    public class line16a : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line16a>
    {
      public line16a()
        : base("A16")
      {
      }
    }

    public class line17 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line17>
    {
      public line17()
        : base("L17")
      {
      }
    }

    public class line18 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line18>
    {
      public line18()
        : base("L18")
      {
      }
    }

    public class line19 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line19>
    {
      public line19()
        : base("L19")
      {
      }
    }

    public class line20 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line20>
    {
      public line20()
        : base("L20")
      {
      }
    }

    public class line21 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line21>
    {
      public line21()
        : base("L21")
      {
      }
    }

    public class line22 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line22>
    {
      public line22()
        : base("L22")
      {
      }
    }

    public class line23 : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.line23>
    {
      public line23()
        : base("L23")
      {
      }
    }

    public class others : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.reportingLineNbr.others>
    {
      public others()
        : base("0TH")
      {
      }
    }
  }

  public abstract class isTemplate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FADetails.isTemplate>
  {
  }

  public abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADetails.templateID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FADetails.hold>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FADetails.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FADetails.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FADetails.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.lastModifiedDateTime>
  {
  }
}
