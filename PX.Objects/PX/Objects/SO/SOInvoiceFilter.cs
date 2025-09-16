// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoiceFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Print/Email Document Filter")]
public class SOInvoiceFilter : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IPrintable,
  IMassEmailingAction
{
  [PXWorkflowMassProcessing(DisplayName = "Action")]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? EndDate { get; set; }

  [PXUIField(DisplayName = "Customer")]
  [Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
  public virtual int? CustomerID { get; set; }

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
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOInvoiceFilter.printWithDeviceHub, NotEqual<True>>>>>.Or<BqlOperand<SOInvoiceFilter.definePrinterManually, IBqlBool>.IsNotEqual<True>>>.Else<SOInvoiceFilter.printerID>))]
  public virtual Guid? PrinterID { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(1)]
  [PXFormula(typeof (Selector<SOInvoiceFilter.printerID, SMPrinter.defaultNumberOfCopies>))]
  [PXUIField]
  public virtual int? NumberOfCopies { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.IMassEmailingAction.AggregateEmails" />
  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Send Documents in One Email")]
  public bool? AggregateEmails { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.IMassEmailingAction.AggregateAttachments" />
  [PXDefault(false)]
  [PXDBBool]
  [PXFormula(typeof (Default<SOInvoiceFilter.aggregateEmails>))]
  [PXUIField(DisplayName = "Combine into One File")]
  public bool? AggregateAttachments { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.IMassEmailingAction.AggregatedAttachmentFileName" />
  [PXDefault("Invoices and Memos {0}.pdf")]
  [PXDBString]
  public string AggregatedAttachmentFileName { get; set; }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoiceFilter.action>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOInvoiceFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOInvoiceFilter.endDate>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoiceFilter.customerID>
  {
  }

  public abstract class showPrinted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOInvoiceFilter.showPrinted>
  {
  }

  public abstract class printWithDeviceHub : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOInvoiceFilter.printWithDeviceHub>
  {
  }

  public abstract class definePrinterManually : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOInvoiceFilter.definePrinterManually>
  {
  }

  public abstract class printerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOInvoiceFilter.printerID>
  {
  }

  public abstract class numberOfCopies : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoiceFilter.numberOfCopies>
  {
  }

  public abstract class aggregateEmails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOInvoiceFilter.aggregateEmails>
  {
  }

  public abstract class aggregateAttachments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOInvoiceFilter.aggregateAttachments>
  {
  }

  public abstract class aggregatedAttachmentFileName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOInvoiceFilter.aggregatedAttachmentFileName>
  {
  }
}
