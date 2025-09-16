// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INRegister
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(new Type[] {typeof (INReceiptEntry), typeof (INIssueEntry), typeof (INTransferEntry), typeof (INAdjustmentEntry), typeof (KitAssemblyEntry), typeof (KitAssemblyEntry), typeof (INProductionEntry)}, new Type[] {typeof (Where<BqlOperand<INRegister.docType, IBqlString>.IsEqual<INDocType.receipt>>), typeof (Where<BqlOperand<INRegister.docType, IBqlString>.IsEqual<INDocType.issue>>), typeof (Where<BqlOperand<INRegister.docType, IBqlString>.IsEqual<INDocType.transfer>>), typeof (Where<BqlOperand<INRegister.docType, IBqlString>.IsEqual<INDocType.adjustment>>), typeof (SelectFromBase<INKitRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitRegister.docType, Equal<INDocType.production>>>>>.And<BqlOperand<INKitRegister.refNbr, IBqlString>.IsEqual<BqlField<INRegister.refNbr, IBqlString>.FromCurrent>>>), typeof (SelectFromBase<INKitRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitRegister.docType, Equal<INDocType.disassembly>>>>>.And<BqlOperand<INKitRegister.refNbr, IBqlString>.IsEqual<BqlField<INRegister.refNbr, IBqlString>.FromCurrent>>>), typeof (Where<BqlOperand<INRegister.docType, IBqlString>.IsEqual<INDocType.manufacturing>>)})]
[INRegisterCacheName("Receipt")]
public class INRegister : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IItemPlanRegister,
  ILotSerialTrackableDocument
{
  protected 
  #nullable disable
  string _KitRevisionID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; } = new bool?(false);

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXString(5, IsUnicode = true)]
  [PXFormula(typeof (Selector<INRegister.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  [PXUIField(DisplayName = "Currency", Enabled = false, FieldClass = "MultipleBaseCurrencies")]
  public virtual string BranchBaseCuryID { get; set; }

  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDefault]
  [INDocType.List]
  [PXUIField]
  [PXFieldDescription]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<BqlOperand<INRegister.docType, IBqlString>.IsEqual<BqlField<INRegister.docType, IBqlString>.AsOptional>>, OrderBy<Desc<INRegister.refNbr>>>), Filterable = true)]
  [INDocType.Numbering]
  [PXFieldDescription]
  public virtual string RefNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("IN")]
  [PXUIField]
  [INRegister.origModule.List]
  public virtual string OrigModule { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string OrigRefNbr { get; set; }

  /// <summary>
  /// The field is used for consolidation by source document (Shipment or Invoice for direct stock item lines) of IN Issues created from one Invoice
  /// </summary>
  [PXString(3)]
  public virtual string SrcDocType { get; set; }

  /// <summary>
  /// The field is used for consolidation by source document (Shipment or Invoice for direct stock item lines) of IN Issues created from one Invoice
  /// </summary>
  [PXString(15, IsUnicode = true)]
  public virtual string SrcRefNbr { get; set; }

  [Site(DisplayName = "Warehouse ID", DescriptionField = typeof (INSite.descr))]
  [PXForeignReference(typeof (INRegister.FK.Site))]
  public virtual int? SiteID { get; set; }

  [ToSite(DisplayName = "To Warehouse ID", DescriptionField = typeof (INSite.descr))]
  [PXForeignReference(typeof (INRegister.FK.ToSite))]
  public virtual int? ToSiteID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("1")]
  [INTransferType.List]
  [PXUIField(DisplayName = "Transfer Type")]
  public virtual string TransferType { get; set; }

  /// <summary>Field used in INReceiptEntry screen.</summary>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Transfer Nbr.")]
  public virtual string TransferNbr { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string TranDesc { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  [PXDBRestrictionBool(typeof (INRegister.released))]
  public virtual bool? ReleasedToVerify { get; set; }

  [PXDBBool]
  [PXDefault(typeof (INSetup.holdEntry))]
  [PXUIField(DisplayName = "Hold", Enabled = false)]
  public virtual bool? Hold { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  [INDocStatus.List]
  public virtual string Status { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? TranDate { get; set; }

  [INOpenPeriod(typeof (INRegister.tranDate), typeof (INRegister.branchID), null, null, null, null, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, typeof (INRegister.tranPeriodID), IsHeader = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TotalQty { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TotalAmount { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TotalCost { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Qty.")]
  public virtual Decimal? ControlQty { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Amount")]
  public virtual Decimal? ControlAmount { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Cost")]
  public virtual Decimal? ControlCost { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<BqlOperand<Batch.module, IBqlString>.IsEqual<BatchModule.moduleIN>>>))]
  public virtual string BatchNbr { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INVENTORY", typeof (Search<InventoryItem.inventoryID>), typeof (InventoryItem.inventoryCD), DescriptionField = typeof (InventoryItem.descr))]
  public virtual int? KitInventoryID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Revision")]
  public virtual string KitRevisionID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? KitLineNbr { get; set; }

  /// <summary>
  /// This field contains the request date of a Kit Assembly which will be used to drive allocation demand for components
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Requested On")]
  public virtual DateTime? KitRequestDate { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "SO Order Type", Visible = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>))]
  public virtual string SOOrderType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "SO Order Nbr.", Visible = false, Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<BqlOperand<PX.Objects.SO.SOOrder.orderType, IBqlString>.IsEqual<BqlField<INRegister.sOOrderType, IBqlString>.FromCurrent>>>))]
  public virtual string SOOrderNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  public virtual string SOShipmentType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "SO Shipment Nbr.", Visible = false, Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOShipment.shipmentNbr>))]
  public virtual string SOShipmentNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "PO Receipt Type", Visible = false, Enabled = false)]
  public virtual string POReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Receipt Nbr.", Visible = false, Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<BqlField<INRegister.pOReceiptType, IBqlString>.FromCurrent>>>))]
  public virtual string POReceiptNbr { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PI Count Reference Nbr.", IsReadOnly = true)]
  [PXSelector(typeof (Search<INPIHeader.pIID>))]
  public virtual string PIID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXSearchable(32 /*0x20*/, "{0}: {1}", new Type[] {typeof (INRegister.docType), typeof (INRegister.refNbr)}, new Type[] {typeof (INRegister.tranDesc), typeof (INRegister.extRefNbr), typeof (INRegister.transferNbr)}, NumberFields = new Type[] {typeof (INRegister.refNbr)}, Line1Format = "{0}{1:d}{2}{3}{4}", Line1Fields = new Type[] {typeof (INRegister.extRefNbr), typeof (INRegister.tranDate), typeof (INRegister.transferType), typeof (INRegister.transferNbr), typeof (INRegister.status)}, Line2Format = "{0}", Line2Fields = new Type[] {typeof (INRegister.tranDesc)}, WhereConstraint = typeof (Where<BqlOperand<INRegister.docType, IBqlString>.IsNotIn<INDocType.production, INDocType.disassembly>>))]
  [PXNote(DescriptionField = typeof (INRegister.refNbr), Selector = typeof (FbqlSelect<SelectFromBase<INRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INRegister.docType, IBqlString>.IsNotIn<INDocType.production, INDocType.disassembly>>.Order<By<BqlField<INRegister.docType, IBqlString>.Asc, BqlField<INRegister.refNbr, IBqlString>.Asc>>, INRegister>.SearchFor<INRegister.refNbr>))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPPVTran { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsTaxAdjustmentTran { get; set; }

  /// <summary>
  /// A stored field indicating if the document is created from a Correction PO Receipt
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCorrection { get; set; }

  /// <summary>
  /// If the document is created from a Correction PO Receipt this field references the original IN Receipt
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<BqlOperand<INRegister.docType, IBqlString>.IsEqual<INDocType.receipt>>>))]
  public virtual string OrigReceiptNbr { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the system should skip verification of inventory item allocations that can be broken by negative adjustment.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Ignore Item Allocations")]
  [PXDefault(false)]
  public virtual bool? IgnoreAllocationErrors { get; set; }

  public class PK : PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>
  {
    public static INRegister Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (INRegister) PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INRegister>.By<INRegister.siteID>
    {
    }

    public class ToSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INRegister>.By<INRegister.toSiteID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INRegister>.By<INRegister.branchID>
    {
    }

    public class KitInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INRegister>.By<INRegister.kitInventoryID>
    {
    }

    public class KitTran : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr, INRegister.kitLineNbr>
    {
    }

    public class KitSpecification : 
      PrimaryKeyOf<INKitSpecHdr>.By<INKitSpecHdr.kitInventoryID, INKitSpecHdr.revisionID>.ForeignKeyOf<INRegister>.By<INRegister.kitInventoryID, INRegister.kitRevisionID>
    {
    }

    public class SOOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<INRegister>.By<INRegister.sOOrderType>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<INRegister>.By<INRegister.sOOrderType, INRegister.sOOrderNbr>
    {
    }

    public class SOShipment : 
      PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentType, PX.Objects.SO.SOShipment.shipmentNbr>.ForeignKeyOf<INRegister>.By<INRegister.sOShipmentType, INRegister.sOShipmentNbr>
    {
    }

    public class POReceipt : 
      PrimaryKeyOf<PX.Objects.PO.POReceipt>.By<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>.ForeignKeyOf<INRegister>.By<INRegister.pOReceiptType, INRegister.pOReceiptNbr>
    {
    }

    public class PIHeader : 
      PrimaryKeyOf<INPIHeader>.By<INPIHeader.pIID>.ForeignKeyOf<INRegister>.By<INRegister.pIID>
    {
    }
  }

  public class Events : PXEntityEventBase<INRegister>.Container<INRegister.Events>
  {
    public PXEntityEvent<INRegister> DocumentReleased;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INRegister.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegister.branchID>
  {
  }

  public abstract class branchBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegister.branchBaseCuryID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.docType>
  {
    public const string DisplayName = "Document Type";
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.refNbr>
  {
    public const string DisplayName = "Reference Nbr.";
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.origModule>
  {
    public const string PI = "PI";

    public class List : PXStringListAttribute
    {
      public List()
        : base(new Tuple<string, string>[5]
        {
          PXStringListAttribute.Pair("SO", "SO"),
          PXStringListAttribute.Pair("PO", "PO"),
          PXStringListAttribute.Pair("IN", "IN"),
          PXStringListAttribute.Pair("PI", "PI"),
          PXStringListAttribute.Pair("AP", "AP")
        })
      {
      }
    }
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.origRefNbr>
  {
  }

  public abstract class srcDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.srcDocType>
  {
  }

  public abstract class srcRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.srcRefNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegister.siteID>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegister.toSiteID>
  {
  }

  public abstract class transferType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.transferType>
  {
  }

  public abstract class transferNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.transferNbr>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.tranDesc>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INRegister.released>
  {
    public class CommonSetupDecPlQtyRule : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<CommonSetup.decPlQty>>.On<OrganizationMaint>.IfExists<Select<INRegister, Where<INRegister.released, NotEqual<True>>>>
    {
      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        return PXMessages.Localize("The decimal places of quantity cannot be changed if unreleased inventory documents exist in the system. Make sure that there are no inventory documents with the On Hold status, and use the Release IN Documents (IN501000) form to process the documents with the Balanced status.");
      }
    }
  }

  public abstract class releasedToVerify : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INRegister.releasedToVerify>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INRegister.hold>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.status>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INRegister.tranDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.tranPeriodID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegister.lineCntr>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INRegister.totalQty>
  {
  }

  public abstract class totalAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INRegister.totalAmount>
  {
  }

  public abstract class totalCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INRegister.totalCost>
  {
  }

  public abstract class controlQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INRegister.controlQty>
  {
  }

  public abstract class controlAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INRegister.controlAmount>
  {
  }

  public abstract class controlCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INRegister.controlCost>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.batchNbr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.extRefNbr>
  {
  }

  public abstract class kitInventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegister.kitInventoryID>
  {
  }

  public abstract class kitRevisionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.kitRevisionID>
  {
  }

  public abstract class kitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRegister.kitLineNbr>
  {
  }

  public abstract class kitRequestDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRegister.kitRequestDate>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.sOOrderNbr>
  {
  }

  public abstract class sOShipmentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.sOShipmentType>
  {
  }

  public abstract class sOShipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.sOShipmentNbr>
  {
  }

  public abstract class pOReceiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.pOReceiptNbr>
  {
  }

  public abstract class pIID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.pIID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INRegister.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegister.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRegister.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INRegister.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegister.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRegister.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INRegister.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INRegister.Tstamp>
  {
  }

  public abstract class isPPVTran : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INRegister.isPPVTran>
  {
  }

  public abstract class isTaxAdjustmentTran : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INRegister.isTaxAdjustmentTran>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INRegister.IsCorrection" />
  public abstract class isCorrection : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INRegister.isCorrection>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INRegister.OrigReceiptNbr" />
  public abstract class origReceiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegister.origReceiptNbr>
  {
  }

  public abstract class ignoreAllocationErrors : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INRegister.ignoreAllocationErrors>
  {
  }
}
