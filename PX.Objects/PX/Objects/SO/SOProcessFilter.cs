// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOProcessFilter
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
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Print/Email Document Filter")]
[Serializable]
public class SOProcessFilter : 
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

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault(typeof (Search<SOOrderType.orderType, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderType.active, Equal<True>>>>>.And<BqlOperand<SOOrderType.behavior, IBqlString>.IsEqual<SOBehavior.qT>>>, OrderBy<Desc<TestIf<BqlOperand<SOOrderType.orderType, IBqlString>.IsEqual<SOBehavior.qT>>>>>))]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<SOOrderType.active, Equal<True>>>))]
  [PXUIField]
  public virtual string OrderType { get; set; }

  [Customer]
  public virtual int? CustomerID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [SOOrderStatus.List]
  [PXUIField]
  public virtual string Status { get; set; }

  [SalesPerson]
  public virtual int? SalesPersonID { get; set; }

  [PXDBBool]
  [PXDefault(typeof (FeatureInstalled<FeaturesSet.deviceHub>))]
  [PXUIField(DisplayName = "Print with DeviceHub")]
  public virtual bool? PrintWithDeviceHub { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Define Printer Manually")]
  public virtual bool? DefinePrinterManually { get; set; } = new bool?(false);

  [PXPrinterSelector]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOProcessFilter.printWithDeviceHub, NotEqual<True>>>>>.Or<BqlOperand<SOProcessFilter.definePrinterManually, IBqlBool>.IsNotEqual<True>>>.Else<SOProcessFilter.printerID>))]
  public virtual Guid? PrinterID { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(1)]
  [PXFormula(typeof (Selector<SOProcessFilter.printerID, SMPrinter.defaultNumberOfCopies>))]
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
  [PXFormula(typeof (Default<SOProcessFilter.aggregateEmails>))]
  [PXUIField(DisplayName = "Combine into One File")]
  public bool? AggregateAttachments { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.IMassEmailingAction.AggregatedAttachmentFileName" />
  [PXFormula(typeof (Switch<Case<Where<Selector<SOProcessFilter.orderType, SOOrderType.behavior>, Equal<SOBehavior.sO>>, SOProcessFilter.aggregatedAttachmentFileName.sO, Case<Where<Selector<SOProcessFilter.orderType, SOOrderType.behavior>, Equal<SOBehavior.qT>>, SOProcessFilter.aggregatedAttachmentFileName.qT>>, SOProcessFilter.aggregatedAttachmentFileName.def>))]
  [PXDBString]
  public string AggregatedAttachmentFileName { get; set; }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOProcessFilter.action>
  {
  }

  public abstract class currentOwnerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOProcessFilter.currentOwnerID>
  {
  }

  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOProcessFilter.myOwner>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOProcessFilter.ownerID>
  {
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOProcessFilter.workGroupID>
  {
  }

  public abstract class myWorkGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOProcessFilter.myWorkGroup>
  {
  }

  public abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOProcessFilter.filterSet>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOProcessFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOProcessFilter.endDate>
  {
  }

  public abstract class showAll : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOProcessFilter.showAll>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOProcessFilter.orderType>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOProcessFilter.customerID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOProcessFilter.status>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOProcessFilter.salesPersonID>
  {
  }

  public abstract class printWithDeviceHub : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOProcessFilter.printWithDeviceHub>
  {
  }

  public abstract class definePrinterManually : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOProcessFilter.definePrinterManually>
  {
  }

  public abstract class printerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOProcessFilter.printerID>
  {
  }

  public abstract class numberOfCopies : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOProcessFilter.numberOfCopies>
  {
  }

  public abstract class aggregateEmails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOProcessFilter.aggregateEmails>
  {
  }

  public abstract class aggregateAttachments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOProcessFilter.aggregateAttachments>
  {
  }

  public abstract class aggregatedAttachmentFileName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOProcessFilter.aggregatedAttachmentFileName>
  {
    public class sO : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOProcessFilter.aggregatedAttachmentFileName.sO>
    {
      public sO()
        : base("Orders {0}.pdf")
      {
      }
    }

    public class qT : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOProcessFilter.aggregatedAttachmentFileName.qT>
    {
      public qT()
        : base("Quotes {0}.pdf")
      {
      }
    }

    public class def : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOProcessFilter.aggregatedAttachmentFileName.def>
    {
      public def()
        : base("Attachment {0}.pdf")
      {
      }
    }
  }
}
