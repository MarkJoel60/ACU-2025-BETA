// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSrvOrdQuickProcessParams
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.SO;
using System;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSSrvOrdQuickProcessParams : SOQuickProcessParameters
{
  [PXString(4, IsFixed = true)]
  [PXUnboundDefault(typeof (FSSrvOrdType.srvOrdType))]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSSrvOrdQuickProcessParams.allowInvoiceServiceOrder.Step), true, DisplayName = "Allow Billing")]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Current<FSServiceOrder.allowInvoice>, Equal<False>>))]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<Current<FSServiceOrder.allowInvoice>, Equal<False>>))]
  public bool? AllowInvoiceServiceOrder { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSSrvOrdQuickProcessParams.completeServiceOrder.Step), true, DisplayName = "Complete")]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Current<FSServiceOrder.completed>, Equal<False>, And<Current<FSServiceOrder.openDoc>, Equal<True>>>))]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<Current<FSServiceOrder.completed>, Equal<False>, And<Current<FSServiceOrder.openDoc>, Equal<True>>>))]
  [PXQuickProcess.Step.IsInsertedJustBefore(typeof (FSSrvOrdQuickProcessParams.generateInvoiceFromServiceOrder))]
  public bool? CompleteServiceOrder { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSSrvOrdQuickProcessParams.closeServiceOrder.Step), true, DisplayName = "Close")]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Current<FSServiceOrder.closed>, Equal<False>, And<Where<Current<FSServiceOrder.completed>, Equal<True>, Or<Current<FSServiceOrder.openDoc>, Equal<True>>>>>))]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<Current<FSServiceOrder.closed>, Equal<False>, And<Where<Current<FSServiceOrder.completed>, Equal<True>, Or<Current<FSServiceOrder.openDoc>, Equal<True>>>>>), PreventStepPresenceChanging = false)]
  [PXQuickProcess.Step.IsInsertedJustBefore(typeof (FSSrvOrdQuickProcessParams.generateInvoiceFromServiceOrder))]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSSrvOrdQuickProcessParams.completeServiceOrder), new Type[] {})]
  public bool? CloseServiceOrder { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSSrvOrdQuickProcessParams.generateInvoiceFromServiceOrder.Step), true, DisplayName = "Run Billing")]
  [PXQuickProcess.Step.IsInsertedJustAfter(typeof (FSSrvOrdQuickProcessParams.allowInvoiceServiceOrder))]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSSrvOrdQuickProcessParams.allowInvoiceServiceOrder), new Type[] {})]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<Current<FSServiceOrder.allowInvoice>, Equal<True>, And2<Where<Current<FSServiceOrder.completed>, Equal<True>>, Or<FSServiceOrder.closed, Equal<True>>>>))]
  [PXUIEnabled(typeof (Where<FSSrvOrdQuickProcessParams.sOQuickProcess, Equal<False>>))]
  public bool? GenerateInvoiceFromServiceOrder { get; set; }

  [PXUIField(DisplayName = "Use Sales Order Quick Processing")]
  [PXBool]
  public bool? SOQuickProcess { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSSrvOrdQuickProcessParams.emailSalesOrder.Step), true, DisplayName = "Email Sales Order/Quote")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSSrvOrdQuickProcessParams.generateInvoiceFromServiceOrder), new Type[] {})]
  public virtual bool? EmailSalesOrder { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (SOOrderTypeQuickProcess.orderType))]
  public override string OrderType { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.createShipment.Step), false, DisplayName = "Create Shipment")]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.tR>>>))]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.tR>>>))]
  public override bool? CreateShipment { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Warehouse ID", FieldClass = "INSITE")]
  [PXQuickProcess.Step.RelatedParameter(typeof (FSSrvOrdQuickProcessParams.createShipment), "siteID")]
  [FSOrderSiteSelector]
  public override int? SiteID { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.confirmShipment.Step), false, DisplayName = "Confirm Shipment")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSSrvOrdQuickProcessParams.createShipment), new Type[] {})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.tR>>>))]
  public override bool? ConfirmShipment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.updateIN.Step), false, DisplayName = "Update IN")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSSrvOrdQuickProcessParams.confirmShipment), new Type[] {})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.tR>>>))]
  public override bool? UpdateIN { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.prepareInvoiceFromShipment.Step), false, DisplayName = "Prepare Invoice")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSSrvOrdQuickProcessParams.confirmShipment), new Type[] {})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where2<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM>, And<Current<PX.Objects.SO.SOOrderType.aRDocType>, NotEqual<ARDocType.noUpdate>>>, And<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.tR>>>>))]
  public override bool? PrepareInvoiceFromShipment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.prepareInvoice.Step), false, DisplayName = "Prepare Invoice")]
  [PXQuickProcess.Step.IsApplicable(typeof (Where2<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM>, And<Current<PX.Objects.SO.SOOrderType.aRDocType>, NotEqual<ARDocType.noUpdate>>>, And<Not<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.tR>>>>>))]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSSrvOrdQuickProcessParams.generateInvoiceFromServiceOrder), new Type[] {})]
  public override bool? PrepareInvoice { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.emailInvoice.Step), false, DisplayName = "Email Invoice")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSSrvOrdQuickProcessParams.prepareInvoice), new Type[] {typeof (FSSrvOrdQuickProcessParams.prepareInvoiceFromShipment)})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where2<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM>, And<Current<PX.Objects.SO.SOOrderType.aRDocType>, NotEqual<ARDocType.noUpdate>>>, Or<Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Sales_Order_Invoice>>>>))]
  public override bool? EmailInvoice { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.releaseInvoice.Step), false, DisplayName = "Release Invoice")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSSrvOrdQuickProcessParams.prepareInvoice), new Type[] {typeof (FSSrvOrdQuickProcessParams.prepareInvoiceFromShipment), typeof (FSSrvOrdQuickProcessParams.generateInvoiceFromServiceOrder)})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<True>))]
  public override bool? ReleaseInvoice { get; set; }

  [PXUIField(DisplayName = "Open All Created Documents in New Tabs")]
  [PXUIEnabled(typeof (Where<FSSrvOrdQuickProcessParams.autoDownloadReports, Equal<False>>))]
  [PXQuickProcess.AutoRedirectOption]
  public override bool? AutoRedirect { get; set; }

  [PXUIField(DisplayName = "Download Created Printable Documents")]
  [PXUIEnabled(typeof (Where<FSSrvOrdQuickProcessParams.autoRedirect, Equal<True>>))]
  [PXQuickProcess.AutoDownloadReportsOption]
  public override bool? AutoDownloadReports { get; set; }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.srvOrdType>
  {
  }

  public abstract class allowInvoiceServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.allowInvoiceServiceOrder>
  {
    public class Step : PXQuickProcess.Step.Definition<ServiceOrderEntry>
    {
      public Step()
        : base((Expression<Func<ServiceOrderEntry, PXAction>>) (g => g.allowBilling))
      {
      }

      public virtual string OnSuccessMessage => "Success";

      public virtual string OnFailureMessage => "Failure";
    }
  }

  public abstract class completeServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.completeServiceOrder>
  {
    public class Step : PXQuickProcess.Step.Definition<ServiceOrderEntry>
    {
      public Step()
        : base((Expression<Func<ServiceOrderEntry, PXAction>>) (g => g.completeOrder))
      {
      }

      public virtual string OnSuccessMessage => "Success";

      public virtual string OnFailureMessage => "Failure";
    }
  }

  public abstract class closeServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.closeServiceOrder>
  {
    public class Step : PXQuickProcess.Step.Definition<ServiceOrderEntry>
    {
      public Step()
        : base((Expression<Func<ServiceOrderEntry, PXAction>>) (g => g.closeOrder))
      {
      }

      public virtual string OnSuccessMessage => "Success";

      public virtual string OnFailureMessage => "Failure";
    }
  }

  public abstract class generateInvoiceFromServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.generateInvoiceFromServiceOrder>
  {
    public class Step : PXQuickProcess.Step.Definition<ServiceOrderEntry>
    {
      public Step()
        : base((Expression<Func<ServiceOrderEntry, PXAction>>) (g => g.invoiceOrder))
      {
      }

      public virtual string OnSuccessMessage => "Document <*> is created.";

      public virtual string OnFailureMessage => "Failure";
    }
  }

  public abstract class sOQuickProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.sOQuickProcess>
  {
  }

  public abstract class emailSalesOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.emailSalesOrder>
  {
    public class Step : PXQuickProcess.Step.Definition<SOOrderEntry>
    {
      public Step()
        : base((Expression<Func<SOOrderEntry, PXAction>>) (g => g.emailSalesOrder))
      {
      }

      public virtual string OnSuccessMessage => "An email with the sales order has been sent.";

      public virtual string OnFailureMessage => "Sending the sales order by email.";
    }
  }

  public new abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.orderType>
  {
  }

  public new abstract class createShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.createShipment>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSrvOrdQuickProcessParams.siteID>
  {
  }

  public new abstract class confirmShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.confirmShipment>
  {
  }

  public new abstract class updateIN : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.updateIN>
  {
  }

  public new abstract class prepareInvoiceFromShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.prepareInvoiceFromShipment>
  {
  }

  public new abstract class prepareInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.prepareInvoice>
  {
  }

  public new abstract class emailInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.emailInvoice>
  {
  }

  public new abstract class releaseInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.releaseInvoice>
  {
  }

  public new abstract class autoRedirect : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.autoRedirect>
  {
  }

  public new abstract class autoDownloadReports : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSrvOrdQuickProcessParams.autoDownloadReports>
  {
  }
}
