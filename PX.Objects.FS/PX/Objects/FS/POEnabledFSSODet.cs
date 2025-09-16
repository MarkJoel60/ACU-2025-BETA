// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.POEnabledFSSODet
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (ServiceOrderEntry))]
[PXProjection(typeof (Select2<FSSODet, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSSODet.sOID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSSODet.inventoryID>>, LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>>, LeftJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<FSSODet.siteID>>>>>>, Where<FSServiceOrder.canceled, Equal<False>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>, And<FSSODet.enablePO, Equal<True>, And<FSSODet.poNbr, IsNull, And<Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>, Or<Where<PX.Objects.IN.InventoryItem.nonStockShip, Equal<True>, Or<PX.Objects.IN.InventoryItem.nonStockReceipt, Equal<True>>>>>>>>>>>))]
[Serializable]
public class POEnabledFSSODet : FSSODet
{
  [PXDBInt(BqlField = typeof (FSServiceOrder.branchID))]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (Search<PX.Objects.GL.Branch.branchID>), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? SrvBranchID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.branchLocationID))]
  [PXUIField(DisplayName = "Branch Location ID")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<POEnabledFSSODet.srvBranchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? SrvBranchLocationID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.itemClassID))]
  [PXUIField]
  [PXSelector(typeof (Search<INItemClass.itemClassID, Where<INItemClass.itemType, Equal<INItemTypes.serviceItem>, Or<FeatureInstalled<FeaturesSet.distributionModule>>>>), SubstituteKey = typeof (INItemClass.itemClassCD))]
  public virtual int? InventoryItemClassID { get; set; }

  [PXDBDate(BqlField = typeof (FSServiceOrder.orderDate))]
  [PXUIField]
  public override DateTime? OrderDate { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.customerID))]
  [PXUIField]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.status, IsNull, Or<PX.Objects.CR.BAccount.status, Equal<CustomerStatus.active>, Or<PX.Objects.CR.BAccount.status, Equal<CustomerStatus.oneTime>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (PX.Objects.CR.BAccount.status)})]
  [FSSelectorBusinessAccount_CU_PR_VC]
  public virtual int? SrvCustomerID { get; set; }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POEnabledFSSODet.srvCustomerID>>>), BqlField = typeof (FSServiceOrder.locationID), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Location ID")]
  public virtual int? SrvLocationID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
  [PXDBDefault(typeof (FSServiceOrder.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSSODet.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSSODet.refNbr>>>>>))]
  public override 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Enabled = false)]
  [PXDefault(typeof (FSServiceOrder.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public override string SrvOrdType { get; set; }

  [PXString]
  [PXUIField(DisplayName = "PO Nbr.")]
  [PX.Objects.PO.PO.RefNbr(typeof (Search2<PX.Objects.PO.POOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.PO.POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.regularOrder>, And<PX.Objects.AP.Vendor.bAccountID, IsNotNull>>, OrderBy<Desc<PX.Objects.PO.POOrder.orderNbr>>>), Filterable = true)]
  public virtual string PONbrCreated { get; set; }

  [VendorNonEmployeeActive(DisplayName = "Vendor ID", DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), CacheGlobal = true, Filterable = true)]
  public override int? POVendorID { get; set; }

  [PXFormula(typeof (Default<FSSODet.poVendorID>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<FSSODet.poVendorID>>>>))]
  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSSODet.poVendorID>>>))]
  public override int? POVendorLocationID { get; set; }

  [PXDBDecimal(BqlField = typeof (FSSODet.curyUnitCost))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? SrvCuryUnitCost { get; set; }

  public new class PK : 
    PrimaryKeyOf<POEnabledFSSODet>.By<FSSODet.lineNbr, POEnabledFSSODet.refNbr, POEnabledFSSODet.srvOrdType>
  {
    public static POEnabledFSSODet Find(
      PXGraph graph,
      int? lineNbr,
      string refNbr,
      string srvOrdType,
      PKFindOptions options = 0)
    {
      return (POEnabledFSSODet) PrimaryKeyOf<POEnabledFSSODet>.By<FSSODet.lineNbr, POEnabledFSSODet.refNbr, POEnabledFSSODet.srvOrdType>.FindBy(graph, (object) lineNbr, (object) refNbr, (object) srvOrdType, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.srvBranchID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.srvBranchLocationID>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.srvOrdType, POEnabledFSSODet.refNbr>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.srvOrdType>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.inventoryID, POEnabledFSSODet.subItemID, POEnabledFSSODet.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.inventoryID, POEnabledFSSODet.subItemID, POEnabledFSSODet.siteID, POEnabledFSSODet.locationID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.inventoryID, POEnabledFSSODet.subItemID, POEnabledFSSODet.siteID, POEnabledFSSODet.locationID, POEnabledFSSODet.lotSerialNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.curyInfoID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.taxCategoryID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.projectID, POEnabledFSSODet.projectTaskID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.acctID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.subID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.costCodeID>
    {
    }

    public class Discount : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.discountID>
    {
    }

    public class POVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.poVendorID>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.billCustomerID>
    {
    }

    public class Equipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.SMequipmentID>
    {
    }

    public class Component : 
      PrimaryKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.componentID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.componentID>
    {
    }

    public class EquipmentComponent : 
      PrimaryKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.SMequipmentID, FSEquipmentComponent.lineNbr>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.SMequipmentID, POEnabledFSSODet.equipmentLineRef>
    {
    }

    public class PostInfo : 
      PrimaryKeyOf<FSPostInfo>.By<FSPostInfo.postID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.postID>
    {
    }

    public class PurchaseOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.poType>
    {
    }

    public class PurchaseOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.poType, POEnabledFSSODet.poNbr>
    {
    }

    public class PurchaseSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.pOSiteID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.scheduleID>
    {
    }

    public class ScheduleDetail : 
      PrimaryKeyOf<FSScheduleDet>.By<FSScheduleDet.scheduleID, FSScheduleDet.scheduleDetID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.scheduleID, POEnabledFSSODet.scheduleDetID>
    {
    }

    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<FSSOEmployee>.By<POEnabledFSSODet.staffID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<POEnabledFSSODet>.By<POEnabledFSSODet.inventoryItemClassID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSServiceOrder>.By<POEnabledFSSODet.srvCustomerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<FSServiceOrder>.By<POEnabledFSSODet.srvCustomerID, POEnabledFSSODet.srvLocationID>
    {
    }
  }

  public abstract class srvBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.srvBranchID>
  {
  }

  public abstract class srvBranchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POEnabledFSSODet.srvBranchLocationID>
  {
  }

  public abstract class inventoryItemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POEnabledFSSODet.inventoryItemClassID>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POEnabledFSSODet.orderDate>
  {
  }

  public abstract class srvCustomerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.srvCustomerID>
  {
  }

  public abstract class srvLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.srvLocationID>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POEnabledFSSODet.refNbr>
  {
  }

  public new abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POEnabledFSSODet.srvOrdType>
  {
  }

  public abstract class poNbrCreated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POEnabledFSSODet.poNbrCreated>
  {
  }

  public new abstract class poVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.poVendorID>
  {
  }

  public abstract class srvCuryUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POEnabledFSSODet.srvCuryUnitCost>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.siteID>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.locationID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POEnabledFSSODet.lotSerialNbr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POEnabledFSSODet.curyInfoID>
  {
  }

  public new abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POEnabledFSSODet.taxCategoryID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.projectID>
  {
  }

  public new abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POEnabledFSSODet.projectTaskID>
  {
  }

  public new abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.acctID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.subID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.costCodeID>
  {
  }

  public new abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POEnabledFSSODet.discountID>
  {
  }

  public new abstract class billCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POEnabledFSSODet.billCustomerID>
  {
  }

  public new abstract class SMequipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POEnabledFSSODet.SMequipmentID>
  {
  }

  public new abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.componentID>
  {
  }

  public new abstract class equipmentLineRef : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POEnabledFSSODet.equipmentLineRef>
  {
  }

  public new abstract class postID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.postID>
  {
  }

  public new abstract class poType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POEnabledFSSODet.poType>
  {
  }

  public new abstract class poNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POEnabledFSSODet.poNbr>
  {
  }

  public new abstract class pOSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.pOSiteID>
  {
  }

  public new abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.scheduleID>
  {
  }

  public new abstract class scheduleDetID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POEnabledFSSODet.scheduleDetID>
  {
  }

  public new abstract class staffID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POEnabledFSSODet.staffID>
  {
  }
}
