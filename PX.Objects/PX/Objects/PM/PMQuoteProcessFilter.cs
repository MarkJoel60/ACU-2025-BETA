// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteProcessFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <exclude />
[PXCacheName("Print/Email Document Filter")]
[Serializable]
public class PMQuoteProcessFilter : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IPrintable,
  IMassEmailingAction
{
  protected int? _OwnerID;
  protected int? _WorkGroupID;

  [PXWorkflowMassProcessing(DisplayName = "Action")]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [PXDBInt]
  [CRCurrentOwnerID]
  public virtual int? CurrentOwnerID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Me")]
  public virtual bool? MyOwner { get; set; }

  [SubordinateOwner(DisplayName = "Assigned To")]
  public virtual int? OwnerID
  {
    get => !this.MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
    set => this._OwnerID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
  public virtual int? WorkGroupID
  {
    get => !this.MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
    set => this._WorkGroupID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? MyWorkGroup { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? FilterSet
  {
    get
    {
      return new bool?(this.OwnerID.HasValue || this.WorkGroupID.HasValue || this.MyWorkGroup.GetValueOrDefault());
    }
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? EndDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show All")]
  public virtual bool? ShowAll { get; set; }

  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType)}, null, null, null)]
  public virtual int? BAccountID { get; set; }

  [PXDBBool]
  [PXDefault(typeof (FeatureInstalled<FeaturesSet.deviceHub>))]
  [PXUIField(DisplayName = "Print with DeviceHub")]
  public virtual bool? PrintWithDeviceHub { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Define Printer Manually")]
  public virtual bool? DefinePrinterManually { get; set; } = new bool?(false);

  [PXPrinterSelector]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuoteProcessFilter.printWithDeviceHub, NotEqual<True>>>>>.Or<BqlOperand<PMQuoteProcessFilter.definePrinterManually, IBqlBool>.IsNotEqual<True>>>.Else<PMQuoteProcessFilter.printerID>))]
  public virtual Guid? PrinterID { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(1)]
  [PXFormula(typeof (Selector<PMQuoteProcessFilter.printerID, SMPrinter.defaultNumberOfCopies>))]
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
  [PXFormula(typeof (Default<PMQuoteProcessFilter.aggregateEmails>))]
  [PXUIField(DisplayName = "Combine into One File")]
  public bool? AggregateAttachments { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.IMassEmailingAction.AggregatedAttachmentFileName" />
  [PXDefault("Project Quotes {0}.pdf")]
  [PXDBString]
  public string AggregatedAttachmentFileName { get; set; }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuoteProcessFilter.action>
  {
  }

  public abstract class currentOwnerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMQuoteProcessFilter.currentOwnerID>
  {
  }

  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuoteProcessFilter.myOwner>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuoteProcessFilter.ownerID>
  {
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuoteProcessFilter.workGroupID>
  {
  }

  public abstract class myWorkGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuoteProcessFilter.myWorkGroup>
  {
  }

  public abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuoteProcessFilter.filterSet>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMQuoteProcessFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMQuoteProcessFilter.endDate>
  {
  }

  public abstract class showAll : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuoteProcessFilter.showAll>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuoteProcessFilter.bAccountID>
  {
  }

  public abstract class printWithDeviceHub : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMQuoteProcessFilter.printWithDeviceHub>
  {
  }

  public abstract class definePrinterManually : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMQuoteProcessFilter.definePrinterManually>
  {
  }

  public abstract class printerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuoteProcessFilter.printerID>
  {
  }

  public abstract class numberOfCopies : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMQuoteProcessFilter.numberOfCopies>
  {
  }

  public abstract class aggregateEmails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMQuoteProcessFilter.aggregateEmails>
  {
  }

  public abstract class aggregateAttachments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMQuoteProcessFilter.aggregateAttachments>
  {
  }

  public abstract class aggregatedAttachmentFileName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuoteProcessFilter.aggregatedAttachmentFileName>
  {
  }
}
