// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(typeof (INSetupMaint))]
[PXCacheName("IN Setup")]
[Serializable]
public class INSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _BatchNumberingID;
  protected string _IssueNumberingID;
  protected string _ReceiptNumberingID;
  protected string _AdjustmentNumberingID;
  protected string _ReplenishmentNumberingID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _HoldEntry;
  protected bool? _RequireControlTotal;
  protected bool? _UseInventorySubItem;
  protected bool? _AutoAddLineBarcode;
  protected bool? _AddByOneBarcode;
  protected int? _ARClearingAcctID;
  protected int? _ARClearingSubID;
  protected int? _INTransitAcctID;
  protected int? _INTransitSubID;
  protected int? _INProgressAcctID;
  protected int? _INProgressSubID;
  protected string _IssuesReasonCode;
  protected string _ReceiptReasonCode;
  protected string _AdjustmentReasonCode;
  protected string _AssemblyDisassemblyReasonCode;
  protected string _DfltPostClassID;
  protected string _DfltLotSerClassID;
  protected bool? _UpdateGL;
  protected bool? _SummPost;
  protected bool? _AutoPost;
  protected short? _PerRetainTran;
  protected short? _PerRetainHist;
  protected bool? _NegQty;
  protected string _PINumberingID;
  protected bool? _PIUseTags;
  protected int? _PILastTagNumber;
  protected string _PIReasonCode;
  protected string _KitAssemblyNumberingID;
  protected short? _TurnoverPeriodsPerYear;
  protected int? _TransitSiteID;
  protected int? _TransitBranchID;

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("BATCH")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string BatchNumberingID
  {
    get => this._BatchNumberingID;
    set => this._BatchNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("INISSUE")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string IssueNumberingID
  {
    get => this._IssueNumberingID;
    set => this._IssueNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("INRECEIPT")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string ReceiptNumberingID
  {
    get => this._ReceiptNumberingID;
    set => this._ReceiptNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("INADJUST")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string AdjustmentNumberingID
  {
    get => this._AdjustmentNumberingID;
    set => this._AdjustmentNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("INREPL")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string ReplenishmentNumberingID
  {
    get => this._ReplenishmentNumberingID;
    set => this._ReplenishmentNumberingID = value;
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

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hold Documents on Entry")]
  public virtual bool? HoldEntry
  {
    get => this._HoldEntry;
    set => this._HoldEntry = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Validate Document Totals on Entry")]
  public virtual bool? RequireControlTotal
  {
    get => this._RequireControlTotal;
    set => this._RequireControlTotal = value;
  }

  [PXBool]
  [PXUIField(Visible = false)]
  public virtual bool? UseInventorySubItem
  {
    get => new bool?(PXAccess.FeatureInstalled<FeaturesSet.subItem>());
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatically Add Receipt Line for Barcode")]
  public virtual bool? AutoAddLineBarcode
  {
    get => this._AutoAddLineBarcode;
    set => this._AutoAddLineBarcode = value;
  }

  /// <summary>
  /// Indicates that the system can populate the Alternate ID box of the Purchase Orders and Sales Orders forms with the barcodes.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Display Barcodes in Order Lines")]
  public virtual bool? ShowBarcodesInOrderLines { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Add One Unit per Barcode")]
  public virtual bool? AddByOneBarcode
  {
    get => this._AddByOneBarcode;
    set => this._AddByOneBarcode = value;
  }

  [Account(DisplayName = "AR Clearing Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (INSetup.FK.ARClearingAccount))]
  public virtual int? ARClearingAcctID
  {
    get => this._ARClearingAcctID;
    set => this._ARClearingAcctID = value;
  }

  [SubAccount(typeof (INSetup.aRClearingAcctID), DisplayName = "AR Clearing Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (INSetup.FK.ARClearingSubaccount))]
  public virtual int? ARClearingSubID
  {
    get => this._ARClearingSubID;
    set => this._ARClearingSubID = value;
  }

  [PXDefault]
  [Account(DisplayName = "In-Transit Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (INSetup.FK.InTransitAccount))]
  public virtual int? INTransitAcctID
  {
    get => this._INTransitAcctID;
    set => this._INTransitAcctID = value;
  }

  [SubAccount(typeof (INSetup.iNTransitAcctID), DisplayName = "In-Transit Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault]
  [PXForeignReference(typeof (INSetup.FK.InTransitSubaccount))]
  public virtual int? INTransitSubID
  {
    get => this._INTransitSubID;
    set => this._INTransitSubID = value;
  }

  [PXDefault]
  [PXUIRequired(typeof (Where<FeatureInstalled<FeaturesSet.kitAssemblies>>))]
  [Account(DisplayName = "Work In-Progress Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (INSetup.FK.WorkInProgressAccount))]
  public virtual int? INProgressAcctID
  {
    get => this._INProgressAcctID;
    set => this._INProgressAcctID = value;
  }

  [SubAccount(typeof (INSetup.iNTransitAcctID), DisplayName = "Work In-Progress Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault]
  [PXUIRequired(typeof (Where<FeatureInstalled<FeaturesSet.kitAssemblies>>))]
  [PXForeignReference(typeof (INSetup.FK.WorkInProgressSubaccount))]
  public virtual int? INProgressSubID
  {
    get => this._INProgressSubID;
    set => this._INProgressSubID = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.issue>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXDefault]
  [PXUIField(DisplayName = "Issue/Return Reason Code")]
  [PXForeignReference(typeof (INSetup.FK.IssuesReasonCode))]
  public virtual string IssuesReasonCode
  {
    get => this._IssuesReasonCode;
    set => this._IssuesReasonCode = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.receipt>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXDefault]
  [PXUIField(DisplayName = "Receipt Reason Code")]
  [PXForeignReference(typeof (INSetup.FK.ReceiptReasonCode))]
  public virtual string ReceiptReasonCode
  {
    get => this._ReceiptReasonCode;
    set => this._ReceiptReasonCode = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.adjustment>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXDefault]
  [PXUIField(DisplayName = "Adjustment Reason Code")]
  [PXForeignReference(typeof (INSetup.FK.AdjustmentReasonCode))]
  public virtual string AdjustmentReasonCode
  {
    get => this._AdjustmentReasonCode;
    set => this._AdjustmentReasonCode = value;
  }

  /// <exclude />
  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.assemblyDisassembly>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField(DisplayName = "Assembly/Disassembly Reason Code")]
  [PXForeignReference(typeof (INSetup.FK.AssemblyDisassemblyReasonCode))]
  public virtual string AssemblyDisassemblyReasonCode
  {
    get => this._AssemblyDisassemblyReasonCode;
    set => this._AssemblyDisassemblyReasonCode = value;
  }

  /// <exclude />
  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.transfer>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField(DisplayName = "Transfer Reason Code")]
  [PXForeignReference(typeof (INSetup.FK.TransferReasonCode))]
  public virtual string TransferReasonCode { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassID), typeof (INItemClass.itemClassCD), new Type[] {typeof (INItemClass.itemClassCD), typeof (INItemClass.descr), typeof (INItemClass.stkItem)}, DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
  [PXRestrictor(typeof (Where<INItemClass.stkItem, Equal<boolTrue>>), "The entered item class is not a stock item class.", new Type[] {})]
  public virtual int? DfltStkItemClassID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassID), typeof (INItemClass.itemClassCD), new Type[] {typeof (INItemClass.itemClassCD), typeof (INItemClass.descr), typeof (INItemClass.stkItem)}, DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
  [PXRestrictor(typeof (Where<INItemClass.stkItem, Equal<boolFalse>>), "The entered item class is not a non-stock item class.", new Type[] {})]
  public virtual int? DfltNonStkItemClassID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string DfltPostClassID
  {
    get => this._DfltPostClassID;
    set => this._DfltPostClassID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string DfltLotSerClassID
  {
    get => this._DfltLotSerClassID;
    set => this._DfltLotSerClassID = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Update GL")]
  [PXDefault(false)]
  public virtual bool? UpdateGL
  {
    get => this._UpdateGL;
    set => this._UpdateGL = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Post Summary on Updating GL")]
  [PXDefault(false)]
  public virtual bool? SummPost
  {
    get => this._SummPost;
    set => this._SummPost = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Automatically Post on Release")]
  [PXDefault(true)]
  public virtual bool? AutoPost
  {
    get => this._AutoPost;
    set => this._AutoPost = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Keep Transactions for")]
  [PXDefault(99)]
  public virtual short? PerRetainTran
  {
    get => this._PerRetainTran;
    set => this._PerRetainTran = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Periods to Retain History")]
  [PXDefault(0)]
  public virtual short? PerRetainHist
  {
    get => this._PerRetainHist;
    set => this._PerRetainHist = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Allow Negative Quantity")]
  [PXDefault(false)]
  public virtual bool? NegQty
  {
    get => this._NegQty;
    set => this._NegQty = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("PIID")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string PINumberingID
  {
    get => this._PINumberingID;
    set => this._PINumberingID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Tags")]
  public virtual bool? PIUseTags
  {
    get => this._PIUseTags;
    set => this._PIUseTags = value;
  }

  [PXDBInt(MinValue = 0)]
  [PXUIField(DisplayName = "Last Tag Number")]
  [PXDefault(0)]
  public virtual int? PILastTagNumber
  {
    get => this._PILastTagNumber;
    set => this._PILastTagNumber = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.adjustment>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXDefault]
  [PXUIField(DisplayName = "Phys.Inventory Reason Code")]
  [PXForeignReference(typeof (INSetup.FK.PIReasonCode))]
  public virtual string PIReasonCode
  {
    get => this._PIReasonCode;
    set => this._PIReasonCode = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("INKITASSY")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string KitAssemblyNumberingID
  {
    get => this._KitAssemblyNumberingID;
    set => this._KitAssemblyNumberingID = value;
  }

  [PXDBShort(MinValue = 1, MaxValue = 12)]
  [PXDefault(12)]
  [PXUIField(DisplayName = "Turnover Periods per Year")]
  public virtual short? TurnoverPeriodsPerYear
  {
    get => this._TurnoverPeriodsPerYear;
    set => this._TurnoverPeriodsPerYear = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault("EQPNBR")]
  [PXUIField(DisplayName = "Equipment Numbering Sequence", Visible = false)]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string ServiceItemNumberingID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Model Attribute")]
  [PXSelector(typeof (CSAttribute.attributeID))]
  public virtual string ModelAttribute { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Manufacture Attribute")]
  [PXSelector(typeof (CSAttribute.attributeID))]
  public virtual string ManufactureAttribute { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Replan Back Orders")]
  public bool? ReplanBackOrders { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXDimensionSelector("INSITE", typeof (INSite.siteID), typeof (INSite.siteCD), DirtyRead = true)]
  [PXUIField(DisplayName = "Site used for keep transit items", Required = true)]
  public virtual int? TransitSiteID
  {
    get => this._TransitSiteID;
    set => this._TransitSiteID = value;
  }

  [Branch(null, null, true, true, true)]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "In-Transit Branch", Required = true)]
  public virtual int? TransitBranchID
  {
    get => this._TransitBranchID;
    set => this._TransitBranchID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Release PI Adjustment Automatically")]
  public bool? AutoReleasePIAdjustment { get; set; }

  [Site(DisplayName = "Default Warehouse", DescriptionField = typeof (INSite.descr))]
  public virtual int? DefaultSiteID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allocate Items in Documents on Hold")]
  public virtual bool? AllocateDocumentsOnHold { get; set; }

  /// <summary>
  /// Optional Production Numbering Sequence for use with Production Transaction (IN308000) screen when the Manufacturing feature is enabled.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string ManufacturingNumberingID { get; set; }

  [PXBool]
  [PXUnboundDefault(true)]
  [PXUIField(DisplayName = "Include Sales", Enabled = false)]
  public virtual bool? IncludeSaleInTurnover { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Production Orders")]
  public virtual bool? IncludeProductionInTurnover { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Assemblies")]
  public virtual bool? IncludeAssemblyInTurnover { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Issues and Adjustments")]
  public virtual bool? IncludeIssueInTurnover { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Transfers")]
  public virtual bool? IncludeTransferInTurnover { get; set; }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? LeaveOldCrossSellSuggestion { get; set; }

  public static class FK
  {
    public class BatchNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<INSetup>.By<INSetup.batchNumberingID>
    {
    }

    public class IssueNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<INSetup>.By<INSetup.issueNumberingID>
    {
    }

    public class ReceiptNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<INSetup>.By<INSetup.receiptNumberingID>
    {
    }

    public class AdjustmentNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<INSetup>.By<INSetup.adjustmentNumberingID>
    {
    }

    public class ReplenishmentNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<INSetup>.By<INSetup.replenishmentNumberingID>
    {
    }

    public class PINumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<INSetup>.By<INSetup.pINumberingID>
    {
    }

    public class KitAssemblyNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<INSetup>.By<INSetup.kitAssemblyNumberingID>
    {
    }

    public class ServiceItemNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<INSetup>.By<INSetup.serviceItemNumberingID>
    {
    }

    public class ARClearingAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSetup>.By<INSetup.aRClearingAcctID>
    {
    }

    public class ARClearingSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSetup>.By<INSetup.aRClearingSubID>
    {
    }

    public class InTransitAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSetup>.By<INSetup.iNTransitAcctID>
    {
    }

    public class InTransitSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSetup>.By<INSetup.iNTransitSubID>
    {
    }

    public class WorkInProgressAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSetup>.By<INSetup.iNProgressAcctID>
    {
    }

    public class WorkInProgressSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSetup>.By<INSetup.iNProgressSubID>
    {
    }

    public class IssuesReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INSetup>.By<INSetup.issuesReasonCode>
    {
    }

    public class ReceiptReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INSetup>.By<INSetup.receiptReasonCode>
    {
    }

    public class AdjustmentReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INSetup>.By<INSetup.adjustmentReasonCode>
    {
    }

    public class AssemblyDisassemblyReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INSetup>.By<INSetup.assemblyDisassemblyReasonCode>
    {
    }

    public class TransferReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INSetup>.By<INSetup.transferReasonCode>
    {
    }

    public class PIReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INSetup>.By<INSetup.pIReasonCode>
    {
    }

    public class DefaultStockItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INSetup>.By<INSetup.dfltStkItemClassID>
    {
    }

    public class DefaultNonStockItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INSetup>.By<INSetup.dfltNonStkItemClassID>
    {
    }

    public class DefaultPostClass : 
      PrimaryKeyOf<INPostClass>.By<INPostClass.postClassID>.ForeignKeyOf<INSetup>.By<INSetup.dfltPostClassID>
    {
    }

    public class DefaultLotSerialClass : 
      PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>.ForeignKeyOf<INSetup>.By<INSetup.dfltLotSerClassID>
    {
    }

    public class ModelAttribute : 
      PrimaryKeyOf<CSAttribute>.By<CSAttribute.attributeID>.ForeignKeyOf<INSetup>.By<INSetup.modelAttribute>
    {
    }

    public class ManufactureAttribute : 
      PrimaryKeyOf<CSAttribute>.By<CSAttribute.attributeID>.ForeignKeyOf<INSetup>.By<INSetup.manufactureAttribute>
    {
    }

    public class TransitSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INSetup>.By<INSetup.transitSiteID>
    {
    }

    public class TransitBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INSetup>.By<INSetup.transitBranchID>
    {
    }

    public class DefaultSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INSetup>.By<INSetup.defaultSiteID>
    {
    }
  }

  public abstract class batchNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.batchNumberingID>
  {
  }

  public abstract class issueNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.issueNumberingID>
  {
  }

  public abstract class receiptNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.receiptNumberingID>
  {
  }

  public abstract class adjustmentNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.adjustmentNumberingID>
  {
  }

  public abstract class replenishmentNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.replenishmentNumberingID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSetup.lastModifiedDateTime>
  {
  }

  public abstract class holdEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSetup.holdEntry>
  {
  }

  public abstract class requireControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.requireControlTotal>
  {
  }

  public abstract class useInventorySubItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.useInventorySubItem>
  {
  }

  public abstract class autoAddLineBarcode : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.autoAddLineBarcode>
  {
  }

  public abstract class showBarcodesInOrderLines : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.showBarcodesInOrderLines>
  {
  }

  public abstract class addByOneBarcode : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSetup.addByOneBarcode>
  {
  }

  public abstract class aRClearingAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.aRClearingAcctID>
  {
  }

  public abstract class aRClearingSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.aRClearingSubID>
  {
  }

  public abstract class iNTransitAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.iNTransitAcctID>
  {
  }

  public abstract class iNTransitSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.iNTransitSubID>
  {
  }

  public abstract class iNProgressAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.iNProgressAcctID>
  {
  }

  public abstract class iNProgressSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.iNProgressSubID>
  {
  }

  public abstract class issuesReasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.issuesReasonCode>
  {
  }

  public abstract class receiptReasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.receiptReasonCode>
  {
  }

  public abstract class adjustmentReasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.adjustmentReasonCode>
  {
  }

  public abstract class assemblyDisassemblyReasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.assemblyDisassemblyReasonCode>
  {
  }

  public abstract class transferReasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.transferReasonCode>
  {
  }

  public abstract class dfltStkItemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.dfltStkItemClassID>
  {
  }

  public abstract class dfltNonStkItemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INSetup.dfltNonStkItemClassID>
  {
  }

  public abstract class dfltPostClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSetup.dfltPostClassID>
  {
  }

  public abstract class dfltLotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.dfltLotSerClassID>
  {
  }

  public abstract class updateGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSetup.updateGL>
  {
  }

  public abstract class summPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSetup.summPost>
  {
  }

  public abstract class autoPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSetup.autoPost>
  {
  }

  public abstract class perRetainTran : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INSetup.perRetainTran>
  {
  }

  public abstract class perRetainHist : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INSetup.perRetainHist>
  {
  }

  public abstract class negQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSetup.negQty>
  {
  }

  public abstract class pINumberingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSetup.pINumberingID>
  {
  }

  public abstract class pIUseTags : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSetup.pIUseTags>
  {
  }

  public abstract class pILastTagNumber : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.pILastTagNumber>
  {
  }

  public abstract class pIReasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSetup.pIReasonCode>
  {
  }

  public abstract class kitAssemblyNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.kitAssemblyNumberingID>
  {
  }

  public abstract class turnoverPeriodsPerYear : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INSetup.turnoverPeriodsPerYear>
  {
  }

  public abstract class serviceItemNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.serviceItemNumberingID>
  {
  }

  public abstract class modelAttribute : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSetup.modelAttribute>
  {
  }

  public abstract class manufactureAttribute : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.manufactureAttribute>
  {
  }

  public abstract class replanBackOrders : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSetup.replanBackOrders>
  {
  }

  public abstract class transitSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.transitSiteID>
  {
  }

  public abstract class transitBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.transitBranchID>
  {
  }

  public abstract class autoReleasePIAdjustment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.autoReleasePIAdjustment>
  {
  }

  public abstract class defaultSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSetup.defaultSiteID>
  {
  }

  public abstract class allocateDocumentsOnHold : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.allocateDocumentsOnHold>
  {
  }

  public abstract class manufacturingNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSetup.manufacturingNumberingID>
  {
  }

  public abstract class includeSaleInTurnover : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.includeSaleInTurnover>
  {
  }

  public abstract class includeProductionInTurnover : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.includeProductionInTurnover>
  {
  }

  public abstract class includeAssemblyInTurnover : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.includeAssemblyInTurnover>
  {
  }

  public abstract class includeIssueInTurnover : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.includeIssueInTurnover>
  {
  }

  public abstract class includeTransferInTurnover : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.includeTransferInTurnover>
  {
  }

  public abstract class leaveOldCrossSellSuggestion : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSetup.leaveOldCrossSellSuggestion>
  {
  }
}
