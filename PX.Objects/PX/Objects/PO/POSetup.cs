// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXPrimaryGraph(typeof (POSetupMaint))]
[PXCacheName("Purchasing Preferences")]
[Serializable]
public class POSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _StandardPONumberingID;
  protected string _RegularPONumberingID;
  protected string _ReceiptNumberingID;
  protected bool? _RequireReceiptControlTotal;
  protected bool? _RequireOrderControlTotal;
  protected bool? _RequireBlanketControlTotal;
  protected bool? _RequireDropShipControlTotal;
  protected bool? _HoldReceipts;
  protected bool? _AddServicesFromNormalPOtoPR;
  protected bool? _AddServicesFromDSPOtoPR;
  protected bool? _OrderRequestApproval;
  protected int? _DefaultReceiptAssignmentMapID;
  protected bool? _ReceiptRequestApproval;
  protected bool? _AutoCreateInvoiceOnReceipt;
  protected int? _FreightExpenseAcctID;
  protected int? _FreightExpenseSubID;
  protected string _RCReturnReasonCodeID;
  protected string _PPVAllocationMode;
  protected string _PPVReasonCodeID;
  protected bool? _AutoReleaseAP;
  protected bool? _AutoReleaseIN;
  protected bool? _AutoCreateLCAP;
  protected bool? _AutoReleaseLCIN;
  protected bool? _CopyLineDescrSO;
  protected bool? _CopyLineNoteSO;
  protected string _ShipDestType;
  protected bool? _UpdateSubOnOwnerChange;
  protected bool? _AutoAddLineReceiptBarcode;
  protected bool? _ReceiptByOneBarcodeReceiptBarcode;
  protected string _DefaultReceiptQty;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("POORDER")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string StandardPONumberingID
  {
    get => this._StandardPONumberingID;
    set => this._StandardPONumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("POORDER")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string RegularPONumberingID
  {
    get => this._RegularPONumberingID;
    set => this._RegularPONumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("PORECEIPT")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string ReceiptNumberingID
  {
    get => this._ReceiptNumberingID;
    set => this._ReceiptNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("POLANDCOST")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string LandedCostDocNumberingID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "For Receipts")]
  public virtual bool? RequireReceiptControlTotal
  {
    get => this._RequireReceiptControlTotal;
    set => this._RequireReceiptControlTotal = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "For Normal and Standard Orders")]
  public virtual bool? RequireOrderControlTotal
  {
    get => this._RequireOrderControlTotal;
    set => this._RequireOrderControlTotal = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "For Blanket Orders")]
  public virtual bool? RequireBlanketControlTotal
  {
    get => this._RequireBlanketControlTotal;
    set => this._RequireBlanketControlTotal = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "For Drop-Ship Orders")]
  public virtual bool? RequireDropShipControlTotal
  {
    get => this._RequireDropShipControlTotal;
    set => this._RequireDropShipControlTotal = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "For Project Drop-Ship Orders")]
  public virtual bool? RequireProjectDropShipControlTotal { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "For Landed Costs")]
  public virtual bool? RequireLandedCostsControlTotal { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hold Receipts on Entry")]
  public virtual bool? HoldReceipts
  {
    get => this._HoldReceipts;
    set => this._HoldReceipts = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hold Landed Costs on Entry")]
  public virtual bool? HoldLandedCosts { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Process Service lines from Normal Purchase Orders via Purchase Receipts")]
  public virtual bool? AddServicesFromNormalPOtoPR
  {
    get => this._AddServicesFromNormalPOtoPR;
    set => this._AddServicesFromNormalPOtoPR = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Process Service lines from Drop-Ship Purchase Orders via Purchase Receipts")]
  public virtual bool? AddServicesFromDSPOtoPR
  {
    get => this._AddServicesFromDSPOtoPR;
    set => this._AddServicesFromDSPOtoPR = value;
  }

  [EPRequireApproval]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Approval")]
  public virtual bool? OrderRequestApproval
  {
    get => this._OrderRequestApproval;
    set => this._OrderRequestApproval = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypePurchaseOrderReceipt>>>))]
  [PXUIField(DisplayName = "Receipt Assignment Map")]
  public virtual int? DefaultReceiptAssignmentMapID
  {
    get => this._DefaultReceiptAssignmentMapID;
    set => this._DefaultReceiptAssignmentMapID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Approval")]
  public virtual bool? ReceiptRequestApproval
  {
    get => this._ReceiptRequestApproval;
    set => this._ReceiptRequestApproval = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? AutoCreateInvoiceOnReceipt
  {
    get => this._AutoCreateInvoiceOnReceipt;
    set => this._AutoCreateInvoiceOnReceipt = value;
  }

  [Account]
  [PXForeignReference(typeof (POSetup.FK.FreightExpenseAccount))]
  public virtual int? FreightExpenseAcctID
  {
    get => this._FreightExpenseAcctID;
    set => this._FreightExpenseAcctID = value;
  }

  [SubAccount(typeof (POSetup.freightExpenseAcctID))]
  [PXForeignReference(typeof (Field<POSetup.freightExpenseSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? FreightExpenseSubID
  {
    get => this._FreightExpenseSubID;
    set => this._FreightExpenseSubID = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, In3<ReasonCodeUsages.issue, ReasonCodeUsages.vendorReturn>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField(DisplayName = "PO Return Reason Code")]
  [PXDefault]
  [PXForeignReference(typeof (POSetup.FK.POReturnReasonCode))]
  public virtual string RCReturnReasonCodeID
  {
    get => this._RCReturnReasonCodeID;
    set => this._RCReturnReasonCodeID = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.adjustment>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField(DisplayName = "Tax Reason Code", Required = false)]
  [PXDefault]
  public virtual string TaxReasonCodeID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [PXUIField]
  [PPVMode.List]
  public virtual string PPVAllocationMode
  {
    get => this._PPVAllocationMode;
    set => this._PPVAllocationMode = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.adjustment>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField(DisplayName = "Reason Code")]
  [PXDefault]
  [PXForeignReference(typeof (POSetup.FK.PPVReasonCode))]
  public virtual string PPVReasonCodeID
  {
    get => this._PPVReasonCodeID;
    set => this._PPVReasonCodeID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Release AP Documents Automatically")]
  public virtual bool? AutoReleaseAP
  {
    get => this._AutoReleaseAP;
    set => this._AutoReleaseAP = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Release IN Documents Automatically")]
  public virtual bool? AutoReleaseIN
  {
    get => this._AutoReleaseIN;
    set => this._AutoReleaseIN = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Bill on LC Release")]
  public virtual bool? AutoCreateLCAP
  {
    get => this._AutoCreateLCAP;
    set => this._AutoCreateLCAP = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Release LC IN Adjustments Automatically")]
  public virtual bool? AutoReleaseLCIN
  {
    get => this._AutoReleaseLCIN;
    set => this._AutoReleaseLCIN = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Descriptions from Sales Orders")]
  public virtual bool? CopyLineDescrSO
  {
    get => this._CopyLineDescrSO;
    set => this._CopyLineDescrSO = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Notes from Sales Orders")]
  public virtual bool? CopyLineNoteSO
  {
    get => this._CopyLineNoteSO;
    set => this._CopyLineNoteSO = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Notes from Service Order", FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? CopyLineNotesFromServiceOrder { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Attachments from Service Order", FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? CopyLineAttachmentsFromServiceOrder { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [POShipDestType.List]
  [PXUIField(DisplayName = "Default Ship Dest. Type")]
  public virtual string ShipDestType
  {
    get => this._ShipDestType;
    set => this._ShipDestType = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Update Sub. on Order Owner Change", FieldClass = "SUBACCOUNT")]
  public virtual bool? UpdateSubOnOwnerChange
  {
    get => this._UpdateSubOnOwnerChange;
    set => this._UpdateSubOnOwnerChange = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatically Add Receipt Line for Barcode")]
  public virtual bool? AutoAddLineReceiptBarcode
  {
    get => this._AutoAddLineReceiptBarcode;
    set => this._AutoAddLineReceiptBarcode = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Add One Unit per Barcode")]
  public virtual bool? ReceiptByOneBarcodeReceiptBarcode
  {
    get => this._ReceiptByOneBarcodeReceiptBarcode;
    set => this._ReceiptByOneBarcodeReceiptBarcode = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Process Return with Original Cost")]
  public virtual bool? ReturnOrigCost { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("O")]
  [DefaultReceiptQuantity.List]
  [PXUIField(DisplayName = "Default Receipt Quantity")]
  public virtual string DefaultReceiptQty
  {
    get => this._DefaultReceiptQty;
    set => this._DefaultReceiptQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Notes to Receipt")]
  public virtual bool? CopyLineNotesToReceipt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Attachments to Receipt")]
  public virtual bool? CopyLineFilesToReceipt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Changing Currency Rate on Receipt", FieldClass = "Multicurrency")]
  public virtual bool? ChangeCuryRateOnReceipt { get; set; }

  [PXDefault("N")]
  [APInvoiceValidationMode.List]
  [PXUIField(DisplayName = "Bill Against Commitments")]
  [PXDBString(1, IsFixed = true)]
  public virtual string APInvoiceValidation { get; set; }

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

  public static class FK
  {
    public class BlanketOrderNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<POSetup>.By<POSetup.standardPONumberingID>
    {
    }

    public class RegularOrderNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<POSetup>.By<POSetup.regularPONumberingID>
    {
    }

    public class ReceiptNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<POSetup>.By<POSetup.receiptNumberingID>
    {
    }

    public class LandedCostNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<POSetup>.By<POSetup.landedCostDocNumberingID>
    {
    }

    public class ReceiptAssignmentMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<POSetup>.By<POSetup.defaultReceiptAssignmentMapID>
    {
    }

    public class FreightExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POSetup>.By<POSetup.freightExpenseAcctID>
    {
    }

    public class FreightExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<POSetup>.By<POSetup.freightExpenseSubID>
    {
    }

    public class POReturnReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<POSetup>.By<POSetup.rCReturnReasonCodeID>
    {
    }

    public class TaxReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<POSetup>.By<POSetup.taxReasonCodeID>
    {
    }

    public class PPVReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<POSetup>.By<POSetup.pPVReasonCodeID>
    {
    }
  }

  public abstract class standardPONumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetup.standardPONumberingID>
  {
  }

  public abstract class regularPONumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetup.regularPONumberingID>
  {
  }

  public abstract class receiptNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetup.receiptNumberingID>
  {
  }

  public abstract class landedCostDocNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetup.landedCostDocNumberingID>
  {
  }

  public abstract class requireReceiptControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.requireReceiptControlTotal>
  {
  }

  public abstract class requireOrderControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.requireOrderControlTotal>
  {
  }

  public abstract class requireBlanketControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.requireBlanketControlTotal>
  {
  }

  public abstract class requireDropShipControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.requireDropShipControlTotal>
  {
  }

  public abstract class requireProjectDropShipControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.requireProjectDropShipControlTotal>
  {
  }

  public abstract class requireLandedCostsControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.requireLandedCostsControlTotal>
  {
  }

  public abstract class holdReceipts : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POSetup.holdReceipts>
  {
  }

  public abstract class holdLandedCosts : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POSetup.holdLandedCosts>
  {
  }

  public abstract class addServicesFromNormalPOtoPR : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.addServicesFromNormalPOtoPR>
  {
  }

  public abstract class addServicesFromDSPOtoPR : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.addServicesFromDSPOtoPR>
  {
  }

  public abstract class orderRequestApproval : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.orderRequestApproval>
  {
  }

  public abstract class defaultReceiptAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POSetup.defaultReceiptAssignmentMapID>
  {
  }

  public abstract class receiptRequestApproval : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.receiptRequestApproval>
  {
  }

  public abstract class autoCreateInvoiceOnReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.autoCreateInvoiceOnReceipt>
  {
  }

  public abstract class freightExpenseAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POSetup.freightExpenseAcctID>
  {
  }

  public abstract class freightExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POSetup.freightExpenseSubID>
  {
  }

  public abstract class rCReturnReasonCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetup.rCReturnReasonCodeID>
  {
  }

  public abstract class taxReasonCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POSetup.taxReasonCodeID>
  {
  }

  public abstract class pPVAllocationMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetup.pPVAllocationMode>
  {
  }

  public abstract class pPVReasonCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POSetup.pPVReasonCodeID>
  {
  }

  public abstract class autoReleaseAP : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POSetup.autoReleaseAP>
  {
  }

  public abstract class autoReleaseIN : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POSetup.autoReleaseIN>
  {
  }

  public abstract class autoCreateLCAP : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POSetup.autoCreateLCAP>
  {
  }

  public abstract class autoReleaseLCIN : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POSetup.autoReleaseLCIN>
  {
  }

  public abstract class copyLineDescrSO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POSetup.copyLineDescrSO>
  {
  }

  public abstract class copyLineNoteSO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POSetup.copyLineNoteSO>
  {
  }

  public abstract class copyLineNotesFromServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.copyLineNotesFromServiceOrder>
  {
  }

  public abstract class copyLineAttachmentsFromServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.copyLineAttachmentsFromServiceOrder>
  {
  }

  public abstract class shipDestType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POSetup.shipDestType>
  {
  }

  public abstract class updateSubOnOwnerChange : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.updateSubOnOwnerChange>
  {
  }

  public abstract class autoAddLineReceiptBarcode : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.autoAddLineReceiptBarcode>
  {
  }

  public abstract class receiptByOneBarcodeReceiptBarcode : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.receiptByOneBarcodeReceiptBarcode>
  {
  }

  public abstract class returnOrigCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POSetup.returnOrigCost>
  {
  }

  public abstract class defaultReceiptQty : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetup.defaultReceiptQty>
  {
  }

  public abstract class copyLineNotesToReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.copyLineNotesToReceipt>
  {
  }

  public abstract class copyLineFilesToReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.copyLineFilesToReceipt>
  {
  }

  public abstract class changeCuryRateOnReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSetup.changeCuryRateOnReceipt>
  {
  }

  public abstract class aPInvoiceValidation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetup.aPInvoiceValidation>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POSetup.lastModifiedDateTime>
  {
  }
}
