// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.PrintInvoicesFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("Print/Email Document Filter")]
[Serializable]
public class PrintInvoicesFilter : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IPrintable,
  IMassEmailingAction
{
  protected int? _OwnerID;
  protected bool? _MyOwner;
  protected int? _WorkGroupID;
  protected bool? _MyWorkGroup;
  protected 
  #nullable disable
  string _Action;
  protected bool? _ShowAll;
  protected DateTime? _BeginDate;
  protected DateTime? _EndDate;
  protected bool? _PrintWithDeviceHub;
  protected bool? _DefinePrinterManually = new bool?(false);
  protected Guid? _PrinterID;
  protected int? _NumberOfCopies;

  [PXDBInt]
  [CRCurrentOwnerID]
  public virtual int? CurrentOwnerID { get; set; }

  [SubordinateOwner(DisplayName = "Assigned To")]
  public virtual int? OwnerID
  {
    get => !this._MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
    set => this._OwnerID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Me")]
  public virtual bool? MyOwner
  {
    get => this._MyOwner;
    set => this._MyOwner = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
  public virtual int? WorkGroupID
  {
    get => !this._MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
    set => this._WorkGroupID = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField]
  public virtual bool? MyWorkGroup
  {
    get => this._MyWorkGroup;
    set => this._MyWorkGroup = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? FilterSet
  {
    get
    {
      return new bool?(this.OwnerID.HasValue || this.WorkGroupID.HasValue || this.MyWorkGroup.GetValueOrDefault());
    }
  }

  [PXString(IsFixed = true)]
  [PXDefault("N")]
  [PrintARDocuments.PrintARDocumentsAction.List]
  [PXUIField(DisplayName = "Action")]
  public virtual string Action
  {
    get => this._Action;
    set => this._Action = value;
  }

  [PXDefault]
  [PXDBBool]
  [PXUIField]
  public virtual bool? ShowAll
  {
    get => this._ShowAll;
    set => this._ShowAll = value;
  }

  [PXDate]
  [PXUIField]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? BeginDate
  {
    get => this._BeginDate;
    set => this._BeginDate = value;
  }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBBool]
  [PXDefault(typeof (FeatureInstalled<FeaturesSet.deviceHub>))]
  [PXUIField(DisplayName = "Print with DeviceHub")]
  public virtual bool? PrintWithDeviceHub
  {
    get => this._PrintWithDeviceHub;
    set => this._PrintWithDeviceHub = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Define Printer Manually")]
  public virtual bool? DefinePrinterManually
  {
    get => this._DefinePrinterManually;
    set => this._DefinePrinterManually = value;
  }

  [PXPrinterSelector]
  public virtual Guid? PrinterID
  {
    get
    {
      return !this.PrintWithDeviceHub.GetValueOrDefault() || !this.DefinePrinterManually.GetValueOrDefault() ? new Guid?() : this._PrinterID;
    }
    set => this._PrinterID = value;
  }

  [PXDBInt(MinValue = 1)]
  [PXDefault(1)]
  [PXFormula(typeof (Selector<PrintInvoicesFilter.printerID, SMPrinter.defaultNumberOfCopies>))]
  [PXUIField]
  public virtual int? NumberOfCopies
  {
    get => this._NumberOfCopies;
    set => this._NumberOfCopies = value;
  }

  /// <summary>The identifier of the customer.</summary>
  [Customer]
  public int? CustomerID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.IMassEmailingAction.AggregateEmails" />
  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Send Documents in One Email")]
  public bool? AggregateEmails { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.IMassEmailingAction.AggregateAttachments" />
  [PXDefault(false)]
  [PXDBBool]
  [PXFormula(typeof (Default<PrintInvoicesFilter.aggregateEmails>))]
  [PXUIField(DisplayName = "Combine into One File")]
  public bool? AggregateAttachments { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.IMassEmailingAction.AggregatedAttachmentFileName" />
  [PXDefault("Invoices and Memos {0}.pdf")]
  [PXDBString]
  public string AggregatedAttachmentFileName { get; set; }

  public abstract class currentOwnerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PrintInvoicesFilter.currentOwnerID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PrintInvoicesFilter.ownerID>
  {
  }

  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PrintInvoicesFilter.myOwner>
  {
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PrintInvoicesFilter.workGroupID>
  {
  }

  public abstract class myWorkGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PrintInvoicesFilter.myWorkGroup>
  {
  }

  public abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PrintInvoicesFilter.filterSet>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PrintInvoicesFilter.action>
  {
  }

  public abstract class showAll : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PrintInvoicesFilter.showAll>
  {
  }

  public abstract class beginDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PrintInvoicesFilter.beginDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PrintInvoicesFilter.endDate>
  {
  }

  public abstract class printWithDeviceHub : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PrintInvoicesFilter.printWithDeviceHub>
  {
  }

  public abstract class definePrinterManually : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PrintInvoicesFilter.definePrinterManually>
  {
  }

  public abstract class printerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PrintInvoicesFilter.printerID>
  {
  }

  public abstract class numberOfCopies : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PrintInvoicesFilter.numberOfCopies>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PrintInvoicesFilter.customerID>
  {
  }

  public abstract class aggregateEmails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PrintInvoicesFilter.aggregateEmails>
  {
  }

  public abstract class aggregateAttachments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PrintInvoicesFilter.aggregateAttachments>
  {
  }

  public abstract class aggregatedAttachmentFileName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PrintInvoicesFilter.aggregatedAttachmentFileName>
  {
  }
}
