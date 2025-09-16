// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.PO;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXPrimaryGraph(typeof (RQSetupMaint))]
[PXCacheName("Requisition Preferences")]
[Serializable]
public class RQSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _RequestNumberingID;
  protected bool? _RequestApproval;
  protected int? _RequestAssignmentMapID;
  protected string _RequestOverBudgetWarning;
  protected int? _MonthRetainRequest;
  protected string _RequisitionNumberingID;
  protected bool? _RequisitionApproval;
  protected int? _RequisitionAssignmentMapID;
  protected string _RequisitionOverBudgetWarning;
  protected bool? _RequisitionMergeLines;
  protected int? _MonthRetainRequisition;
  protected bool? _POHold;
  protected int? _BudgetLedgerId;
  protected string _BudgetCalculation;
  protected string _DefaultReqClassID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("RQITEM")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string RequestNumberingID
  {
    get => this._RequestNumberingID;
    set => this._RequestNumberingID = value;
  }

  [EPRequireApproval]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Require Approval")]
  public virtual bool? RequestApproval
  {
    get => this._RequestApproval;
    set => this._RequestApproval = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypePurchaseRequestItem>, And<EPAssignmentMap.mapType, NotEqual<EPMapType.approval>>>>), DescriptionField = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Assignment Map")]
  public virtual int? RequestAssignmentMapID
  {
    get => this._RequestAssignmentMapID;
    set => this._RequestAssignmentMapID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [POReceiptQtyAction.List]
  [PXDefault("W")]
  [PXUIField(DisplayName = "Over Budget Warning")]
  public virtual string RequestOverBudgetWarning
  {
    get => this._RequestOverBudgetWarning;
    set => this._RequestOverBudgetWarning = value;
  }

  [PXDBInt]
  [PXDefault(3)]
  [PXUIField(DisplayName = "Months Retained")]
  public virtual int? MonthRetainRequest
  {
    get => this._MonthRetainRequest;
    set => this._MonthRetainRequest = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("RQREQUISIT")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string RequisitionNumberingID
  {
    get => this._RequisitionNumberingID;
    set => this._RequisitionNumberingID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Require Approval")]
  public virtual bool? RequisitionApproval
  {
    get => this._RequisitionApproval;
    set => this._RequisitionApproval = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypePurchaseRequisition>, And<EPAssignmentMap.mapType, NotEqual<EPMapType.approval>>>>), DescriptionField = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Assignment Map")]
  public virtual int? RequisitionAssignmentMapID
  {
    get => this._RequisitionAssignmentMapID;
    set => this._RequisitionAssignmentMapID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [POReceiptQtyAction.List]
  [PXDefault("W")]
  [PXUIField(DisplayName = "Over Budget Warning")]
  public virtual string RequisitionOverBudgetWarning
  {
    get => this._RequisitionOverBudgetWarning;
    set => this._RequisitionOverBudgetWarning = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Merge Lines by Default")]
  public virtual bool? RequisitionMergeLines
  {
    get => this._RequisitionMergeLines;
    set => this._RequisitionMergeLines = value;
  }

  [PXDBInt]
  [PXDefault(3)]
  [PXUIField(DisplayName = "Months Retained")]
  public virtual int? MonthRetainRequisition
  {
    get => this._MonthRetainRequisition;
    set => this._MonthRetainRequisition = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Purchase Order on Hold")]
  public bool? POHold
  {
    get => this._POHold;
    set => this._POHold = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Budget Ledger", Required = true)]
  [PXSelector(typeof (Search<PX.Objects.GL.Ledger.ledgerID, Where<PX.Objects.GL.Ledger.balanceType, Equal<BudgetLedger>>>), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD), DescriptionField = typeof (PX.Objects.GL.Ledger.descr))]
  [PXDefault]
  public virtual int? BudgetLedgerId
  {
    get => this._BudgetLedgerId;
    set => this._BudgetLedgerId = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("Y")]
  [PXUIField(DisplayName = "Budget Calculation")]
  [RQBudgetCalculationType.List]
  public virtual string BudgetCalculation
  {
    get => this._BudgetCalculation;
    set => this._BudgetCalculation = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (RQRequestClass.reqClassID), DescriptionField = typeof (RQRequestClass.descr), DirtyRead = true)]
  public virtual string DefaultReqClassID
  {
    get => this._DefaultReqClassID;
    set => this._DefaultReqClassID = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType, Where<PX.Objects.SO.SOOrderType.behavior, Equal<SOBehavior.iN>, Or<Where<PX.Objects.SO.SOOrderType.behavior, Equal<SOBehavior.sO>, And<FeatureInstalled<FeaturesSet.inventory>>>>>>), DescriptionField = typeof (PX.Objects.SO.SOOrderType.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.SO.SOOrderType.active, Equal<True>>), null, new Type[] {})]
  [PXUIField]
  public virtual string SOOrderType { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType, Where<PX.Objects.SO.SOOrderType.behavior, Equal<SOBehavior.qT>>>), DescriptionField = typeof (PX.Objects.SO.SOOrderType.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.SO.SOOrderType.active, Equal<True>>), null, new Type[] {})]
  [PXUIField]
  public virtual string QTOrderType { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class requestNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSetup.requestNumberingID>
  {
  }

  public abstract class requestApproval : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQSetup.requestApproval>
  {
  }

  public abstract class requestAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQSetup.requestAssignmentMapID>
  {
  }

  public abstract class requestOverBudgetWarning : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSetup.requestOverBudgetWarning>
  {
  }

  public abstract class monthRetainRequest : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSetup.monthRetainRequest>
  {
  }

  public abstract class requisitionNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSetup.requisitionNumberingID>
  {
  }

  public abstract class requisitionApproval : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQSetup.requisitionApproval>
  {
  }

  public abstract class requisitionAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQSetup.requisitionAssignmentMapID>
  {
  }

  public abstract class requisitionOverBudgetWarning : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSetup.requisitionOverBudgetWarning>
  {
  }

  public abstract class requisitionMergeLines : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQSetup.requisitionMergeLines>
  {
  }

  public abstract class monthRetainRequisition : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQSetup.monthRetainRequisition>
  {
  }

  public abstract class pOHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQSetup.pOHold>
  {
  }

  public abstract class budgetLedgerId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSetup.budgetLedgerId>
  {
  }

  public abstract class budgetCalculation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSetup.budgetCalculation>
  {
  }

  public abstract class defaultReqClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSetup.defaultReqClassID>
  {
  }

  public abstract class soOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQSetup.soOrderType>
  {
  }

  public abstract class qtOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQSetup.qtOrderType>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQSetup.lastModifiedDateTime>
  {
  }
}
