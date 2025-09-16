// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.SO;

[Serializable]
public class SOShipmentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPrintable
{
  [PXWorkflowMassProcessing(DisplayName = "Action")]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [Site(DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  public virtual int? SiteID { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate]
  [PXUIField]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? EndDate { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (Search<CarrierPlugin.carrierPluginID>))]
  [PXRestrictor(typeof (Where<Where2<Where<CarrierPlugin.isActive, Equal<True>, And<BqlField<SOShipmentFilter.action, IBqlString>.FromCurrent, Equal<SOInvoiceShipment.WellKnownActions.SOShipmentScreen.confirmShipment>>>, Or<Where<BqlField<SOShipmentFilter.action, IBqlString>.FromCurrent, NotEqual<SOInvoiceShipment.WellKnownActions.SOShipmentScreen.confirmShipment>>>>>), null, new Type[] {})]
  public virtual string CarrierPluginID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
  [PXRestrictor(typeof (Where<Where2<Where<PX.Objects.CS.Carrier.isActive, Equal<True>, And<BqlField<SOShipmentFilter.action, IBqlString>.FromCurrent, Equal<SOInvoiceShipment.WellKnownActions.SOShipmentScreen.confirmShipment>>>, Or<Where<BqlField<SOShipmentFilter.action, IBqlString>.FromCurrent, NotEqual<SOInvoiceShipment.WellKnownActions.SOShipmentScreen.confirmShipment>>>>>), null, new Type[] {})]
  public virtual string ShipVia { get; set; }

  [Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
  public virtual int? CustomerID { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Invoice Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? InvoiceDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("B")]
  [SOPackageType.ForFiltering.List]
  [PXUIField(DisplayName = "Packaging Type")]
  public virtual string PackagingType { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Printed")]
  public virtual bool? ShowPrinted { get; set; }

  [PXDBBool]
  [PXDefault(typeof (FeatureInstalled<FeaturesSet.deviceHub>))]
  [PXUIField(DisplayName = "Print with DeviceHub")]
  public virtual bool? PrintWithDeviceHub { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Define Printer Manually")]
  public virtual bool? DefinePrinterManually { get; set; } = new bool?(false);

  [PXPrinterSelector]
  public virtual Guid? PrinterID { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(1)]
  [PXFormula(typeof (Selector<SOShipmentFilter.printerID, SMPrinter.defaultNumberOfCopies>))]
  [PXUIField]
  public virtual int? NumberOfCopies { get; set; }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipmentFilter.action>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentFilter.siteID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOShipmentFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOShipmentFilter.endDate>
  {
  }

  public abstract class carrierPluginID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentFilter.carrierPluginID>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipmentFilter.shipVia>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentFilter.customerID>
  {
  }

  public abstract class invoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipmentFilter.invoiceDate>
  {
  }

  public abstract class packagingType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentFilter.packagingType>
  {
  }

  public abstract class showPrinted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipmentFilter.showPrinted>
  {
  }

  public abstract class printWithDeviceHub : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentFilter.printWithDeviceHub>
  {
  }

  public abstract class definePrinterManually : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentFilter.definePrinterManually>
  {
  }

  public abstract class printerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOShipmentFilter.printerID>
  {
  }

  public abstract class numberOfCopies : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentFilter.numberOfCopies>
  {
  }

  public class PackagingTypeListAttribute : SOPackageType.ForFiltering.ListAttribute
  {
  }
}
