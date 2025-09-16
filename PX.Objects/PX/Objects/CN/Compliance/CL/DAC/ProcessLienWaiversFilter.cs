// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.ProcessLienWaiversFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.CL.Descriptor.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.PM;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.DAC;

[PXCacheName("Process LienWaivers Filter")]
[Serializable]
public class ProcessLienWaiversFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPrintable
{
  [PXString]
  [PXUnboundDefault("Print Lien Waivers")]
  [PXUIField(DisplayName = "Action")]
  [ProcessLienWaiverActions]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [Project(typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.project>>>>.And<BqlOperand<PMProject.status, IBqlString>.IsEqual<ProjectStatus.active>>>), DisplayName = "Project")]
  public virtual int? ProjectId { get; set; }

  [Vendor(typeof (Search<BAccountR.bAccountID, Where<BqlOperand<PX.Objects.AP.Vendor.type, IBqlString>.IsEqual<BAccountType.vendorType>>>), DisplayName = "Vendor")]
  public virtual int? VendorId { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Category")]
  [PXUnboundDefault("All")]
  [LienWaiverTypes]
  public virtual string LienWaiverType { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Show Processed")]
  public virtual bool? ShouldShowProcessed { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (FeatureInstalled<FeaturesSet.deviceHub>))]
  [PXUIField(DisplayName = "Print with DeviceHub")]
  public bool? PrintWithDeviceHub { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Define Printer Manually")]
  [PXUIEnabled(typeof (BqlOperand<ProcessLienWaiversFilter.printWithDeviceHub, IBqlBool>.IsEqual<True>))]
  public bool? DefinePrinterManually { get; set; }

  [PXPrinterSelector(DisplayName = "Printer")]
  [PXUIEnabled(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ProcessLienWaiversFilter.printWithDeviceHub, Equal<True>>>>>.And<BqlOperand<ProcessLienWaiversFilter.definePrinterManually, IBqlBool>.IsEqual<True>>))]
  public Guid? PrinterID { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(1)]
  [PXFormula(typeof (Selector<ProcessLienWaiversFilter.printerID, SMPrinter.defaultNumberOfCopies>))]
  [PXUIField(DisplayName = "Number of Copies")]
  [PXUIEnabled(typeof (BqlOperand<ProcessLienWaiversFilter.printWithDeviceHub, IBqlBool>.IsEqual<True>))]
  public int? NumberOfCopies { get; set; }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ProcessLienWaiversFilter.action>
  {
  }

  public abstract class projectId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProcessLienWaiversFilter.projectId>
  {
  }

  public abstract class vendorId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProcessLienWaiversFilter.vendorId>
  {
  }

  public abstract class lienWaiverType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ProcessLienWaiversFilter.lienWaiverType>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ProcessLienWaiversFilter.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ProcessLienWaiversFilter.endDate>
  {
  }

  public abstract class shouldShowProcessed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ProcessLienWaiversFilter.shouldShowProcessed>
  {
  }

  public abstract class printWithDeviceHub : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ProcessLienWaiversFilter.printWithDeviceHub>
  {
  }

  public abstract class definePrinterManually : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ProcessLienWaiversFilter.definePrinterManually>
  {
  }

  public abstract class printerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ProcessLienWaiversFilter.printerID>
  {
  }

  public abstract class numberOfCopies : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ProcessLienWaiversFilter.numberOfCopies>
  {
  }
}
