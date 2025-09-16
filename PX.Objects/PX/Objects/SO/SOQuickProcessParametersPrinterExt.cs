// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOQuickProcessParametersPrinterExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.SO;

public sealed class SOQuickProcessParametersPrinterExt : 
  PXCacheExtension<
  #nullable disable
  SOQuickProcessParameters>,
  IPrintable
{
  [PXBool]
  [PXDefault(true)]
  public bool? HideWhenNothingToPrint { get; set; } = new bool?(true);

  [PXDBBool]
  [PXDefault(typeof (FeatureInstalled<FeaturesSet.deviceHub>))]
  [PXUIField(DisplayName = "Print with DeviceHub", FieldClass = "DeviceHub")]
  [SOQuickProcessParametersPrinterExt.ReportsPrintingSetting("PrintWithDeviceHub")]
  public bool? PrintWithDeviceHub { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Define Printers Manually", FieldClass = "DeviceHub")]
  [PXUIEnabled(typeof (SOQuickProcessParametersPrinterExt.printWithDeviceHub))]
  [PXFormula(typeof (IIf<Where<SOQuickProcessParametersPrinterExt.printWithDeviceHub, Equal<False>>, False, SOQuickProcessParametersPrinterExt.definePrinterManually>))]
  [SOQuickProcessParametersPrinterExt.ReportsPrintingSetting("DefinePrinterManually")]
  public bool? DefinePrinterManually { get; set; } = new bool?(false);

  [PXPrinterSelector]
  [PXUIEnabled(typeof (SOQuickProcessParametersPrinterExt.definePrinterManually))]
  [PXFormula(typeof (IIf<Where<SOQuickProcessParametersPrinterExt.printWithDeviceHub, Equal<False>, Or<SOQuickProcessParametersPrinterExt.definePrinterManually, Equal<False>>>, Null, SOQuickProcessParametersPrinterExt.printerID>))]
  [SOQuickProcessParametersPrinterExt.ReportsPrintingSetting("PrinterID")]
  public Guid? PrinterID { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(1)]
  [SOQuickProcessParametersPrinterExt.ReportsPrintingSetting("NumberOfCopies")]
  [PXUIField]
  public int? NumberOfCopies { get; set; }

  public abstract class hideWhenNothingToPrint : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParametersPrinterExt.hideWhenNothingToPrint>
  {
  }

  public abstract class printWithDeviceHub : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParametersPrinterExt.printWithDeviceHub>
  {
  }

  public abstract class definePrinterManually : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParametersPrinterExt.definePrinterManually>
  {
  }

  public abstract class printerID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOQuickProcessParametersPrinterExt.printerID>
  {
  }

  public abstract class numberOfCopies : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOQuickProcessParametersPrinterExt.numberOfCopies>
  {
  }

  public class ReportsPrintingSetting : PXAggregateAttribute
  {
    public ReportsPrintingSetting(string parameterName)
    {
      PXAggregateAttribute.AggregatedAttributesCollection attributes = this._Attributes;
      PXEventSubscriberAttribute[] subscriberAttributeArray = new PXEventSubscriberAttribute[5]
      {
        (PXEventSubscriberAttribute) new PXUIVisibleAttribute(typeof (Where<SOQuickProcessParametersPrinterExt.hideWhenNothingToPrint, Equal<False>, Or<SOQuickProcessParametersReportsExt.printConfirmation, Equal<True>, Or<SOQuickProcessParametersReportsExt.printInvoice, Equal<True>, Or<SOQuickProcessParametersReportsExt.printLabels, Equal<True>, Or<SOQuickProcessParametersReportsExt.printPickList, Equal<True>>>>>>)),
        null,
        null,
        null,
        null
      };
      PXQuickProcess.Step.RelatedParameterAttribute parameterAttribute1 = new PXQuickProcess.Step.RelatedParameterAttribute(typeof (SOQuickProcessParametersReportsExt.printConfirmation), parameterName);
      ((PXQuickProcess.Step.RelatedFieldAttribute) parameterAttribute1).SyncVisibilityWithRelatedStep = false;
      subscriberAttributeArray[1] = (PXEventSubscriberAttribute) parameterAttribute1;
      PXQuickProcess.Step.RelatedParameterAttribute parameterAttribute2 = new PXQuickProcess.Step.RelatedParameterAttribute(typeof (SOQuickProcessParametersReportsExt.printInvoice), parameterName);
      ((PXQuickProcess.Step.RelatedFieldAttribute) parameterAttribute2).SyncVisibilityWithRelatedStep = false;
      subscriberAttributeArray[2] = (PXEventSubscriberAttribute) parameterAttribute2;
      PXQuickProcess.Step.RelatedParameterAttribute parameterAttribute3 = new PXQuickProcess.Step.RelatedParameterAttribute(typeof (SOQuickProcessParametersReportsExt.printLabels), parameterName);
      ((PXQuickProcess.Step.RelatedFieldAttribute) parameterAttribute3).SyncVisibilityWithRelatedStep = false;
      subscriberAttributeArray[3] = (PXEventSubscriberAttribute) parameterAttribute3;
      PXQuickProcess.Step.RelatedParameterAttribute parameterAttribute4 = new PXQuickProcess.Step.RelatedParameterAttribute(typeof (SOQuickProcessParametersReportsExt.printPickList), parameterName);
      ((PXQuickProcess.Step.RelatedFieldAttribute) parameterAttribute4).SyncVisibilityWithRelatedStep = false;
      subscriberAttributeArray[4] = (PXEventSubscriberAttribute) parameterAttribute4;
      attributes.AddRange((IEnumerable<PXEventSubscriberAttribute>) subscriberAttributeArray);
    }
  }
}
