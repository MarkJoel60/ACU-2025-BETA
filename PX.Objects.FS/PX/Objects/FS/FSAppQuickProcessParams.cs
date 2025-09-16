// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppQuickProcessParams
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
public class FSAppQuickProcessParams : SOQuickProcessParameters
{
  [PXString(4, IsFixed = true)]
  [PXUnboundDefault(typeof (FSSrvOrdType.srvOrdType))]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSAppQuickProcessParams.closeAppointment.Step), true, DisplayName = "Close")]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<True, Equal<True>>))]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Current<FSAppointment.completed>, Equal<True>, And<Current<FSAppointment.closed>, Equal<False>>>))]
  public virtual bool? CloseAppointment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSAppQuickProcessParams.emailSignedAppointment.Step), true, DisplayName = "Email Signed Appointment")]
  [PXQuickProcess.Step.IsInsertedJustAfter(typeof (FSAppQuickProcessParams.generateInvoiceFromAppointment))]
  public virtual bool? EmailSignedAppointment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSAppQuickProcessParams.generateInvoiceFromAppointment.Step), true, DisplayName = "Run Billing")]
  [PXQuickProcess.Step.IsStartPoint(typeof (Where<Current<FSAppointment.closed>, Equal<True>>))]
  [PXQuickProcess.Step.IsInsertedJustAfter(typeof (FSAppQuickProcessParams.closeAppointment))]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSAppQuickProcessParams.closeAppointment), new Type[] {})]
  [PXUIEnabled(typeof (Where<FSAppQuickProcessParams.sOQuickProcess, Equal<False>>))]
  public virtual bool? GenerateInvoiceFromAppointment { get; set; }

  [PXUIField(DisplayName = "Use Sales Order Quick Processing")]
  [PXBool]
  public bool? SOQuickProcess { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (FSAppQuickProcessParams.emailSalesOrder.Step), true, DisplayName = "Email Sales Order/Quote")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSAppQuickProcessParams.generateInvoiceFromAppointment), new Type[] {})]
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
  [PXQuickProcess.Step.RelatedParameter(typeof (FSAppQuickProcessParams.createShipment), "siteID")]
  [FSOrderSiteSelector]
  public override int? SiteID { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.confirmShipment.Step), false, DisplayName = "Confirm Shipment")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSAppQuickProcessParams.createShipment), new Type[] {})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.tR>>>))]
  public override bool? ConfirmShipment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.updateIN.Step), false, DisplayName = "Update IN")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSAppQuickProcessParams.confirmShipment), new Type[] {})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.tR>>>))]
  public override bool? UpdateIN { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.prepareInvoiceFromShipment.Step), false, DisplayName = "Prepare Invoice")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSAppQuickProcessParams.confirmShipment), new Type[] {})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where2<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM>, And<Current<PX.Objects.SO.SOOrderType.aRDocType>, NotEqual<ARDocType.noUpdate>>>, And<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.tR>>>>))]
  public override bool? PrepareInvoiceFromShipment { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.prepareInvoice.Step), false, DisplayName = "Prepare Invoice")]
  [PXQuickProcess.Step.IsApplicable(typeof (Where2<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM>, And<Current<PX.Objects.SO.SOOrderType.aRDocType>, NotEqual<ARDocType.noUpdate>>>, And<Not<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.tR>>>>>))]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSAppQuickProcessParams.generateInvoiceFromAppointment), new Type[] {})]
  public override bool? PrepareInvoice { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.emailInvoice.Step), false, DisplayName = "Email Invoice")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSAppQuickProcessParams.prepareInvoice), new Type[] {typeof (FSAppQuickProcessParams.prepareInvoiceFromShipment)})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where2<Where<Current<SOOrderTypeQuickProcess.behavior>, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM>, And<Current<PX.Objects.SO.SOOrderType.aRDocType>, NotEqual<ARDocType.noUpdate>>>, Or<Where<Current<FSSrvOrdType.postTo>, Equal<FSPostTo.Sales_Order_Invoice>>>>))]
  public override bool? EmailInvoice { get; set; }

  [PXQuickProcess.Step.IsBoundTo(typeof (SOQuickProcessParameters.releaseInvoice.Step), false, DisplayName = "Release Invoice")]
  [PXQuickProcess.Step.RequiresSteps(typeof (FSAppQuickProcessParams.prepareInvoice), new Type[] {typeof (FSAppQuickProcessParams.prepareInvoiceFromShipment), typeof (FSAppQuickProcessParams.generateInvoiceFromAppointment)})]
  [PXQuickProcess.Step.IsApplicable(typeof (Where<True>))]
  public override bool? ReleaseInvoice { get; set; }

  [PXUIField(DisplayName = "Open All Created Documents in New Tabs")]
  [PXUIEnabled(typeof (Where<FSAppQuickProcessParams.autoDownloadReports, Equal<False>>))]
  [PXQuickProcess.AutoRedirectOption]
  public override bool? AutoRedirect { get; set; }

  [PXUIField(DisplayName = "Download Created Printable Documents")]
  [PXUIEnabled(typeof (Where<FSAppQuickProcessParams.autoRedirect, Equal<True>>))]
  [PXQuickProcess.AutoDownloadReportsOption]
  public override bool? AutoDownloadReports { get; set; }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppQuickProcessParams.srvOrdType>
  {
  }

  public abstract class closeAppointment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.closeAppointment>
  {
    public class Step : PXQuickProcess.Step.Definition<AppointmentEntry>
    {
      public Step()
        : base((Expression<Func<AppointmentEntry, PXAction>>) (g => g.closeAppointment))
      {
      }

      public virtual string OnSuccessMessage => "Success";

      public virtual string OnFailureMessage => "Failure";
    }
  }

  public abstract class emailSignedAppointment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.emailSignedAppointment>
  {
    public class Step : PXQuickProcess.Step.Definition<AppointmentEntry>
    {
      public Step()
        : base((Expression<Func<AppointmentEntry, PXAction>>) (g => g.emailSignedAppointment))
      {
      }

      public virtual string OnSuccessMessage => "Success";

      public virtual string OnFailureMessage => "Failure";
    }
  }

  public abstract class generateInvoiceFromAppointment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.generateInvoiceFromAppointment>
  {
    public class Step : PXQuickProcess.Step.Definition<AppointmentEntry>
    {
      public Step()
        : base((Expression<Func<AppointmentEntry, PXAction>>) (g => g.invoiceAppointment))
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
    FSAppQuickProcessParams.sOQuickProcess>
  {
  }

  public abstract class emailSalesOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.emailSalesOrder>
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
    FSAppQuickProcessParams.orderType>
  {
  }

  public new abstract class createShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.createShipment>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppQuickProcessParams.siteID>
  {
  }

  public new abstract class confirmShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.confirmShipment>
  {
  }

  public new abstract class updateIN : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.updateIN>
  {
  }

  public new abstract class prepareInvoiceFromShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.prepareInvoiceFromShipment>
  {
  }

  public new abstract class prepareInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.prepareInvoice>
  {
  }

  public new abstract class emailInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.emailInvoice>
  {
  }

  public new abstract class releaseInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.releaseInvoice>
  {
  }

  public new abstract class autoRedirect : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.autoRedirect>
  {
  }

  public new abstract class autoDownloadReports : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppQuickProcessParams.autoDownloadReports>
  {
  }
}
