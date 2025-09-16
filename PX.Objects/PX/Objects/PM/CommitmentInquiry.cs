// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CommitmentInquiry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.CN.Subcontracts.PM.Descriptor;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.TM;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[TableDashboardType]
[Serializable]
public class CommitmentInquiry : PXGraph<
#nullable disable
CommitmentInquiry>
{
  public PXFilter<CommitmentInquiry.ProjectBalanceFilter> Filter;
  public PXCancel<CommitmentInquiry.ProjectBalanceFilter> Cancel;
  [PXFilterable(new Type[] {})]
  [PXViewName("Commitments")]
  public FbqlSelect<SelectFromBase<PMCommitment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CommitmentInquiry.POLineCommitment>.On<BqlOperand<
  #nullable enable
  PMCommitment.commitmentID, IBqlGuid>.IsEqual<
  #nullable disable
  CommitmentInquiry.POLineCommitment.commitmentID>>>, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  PMCommitment.projectID, IBqlInt>.IsEqual<
  #nullable disable
  PMProject.contractID>>>, FbqlJoins.Left<PX.Objects.PO.POOrder>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.POLineCommitment.orderNbr, 
  #nullable disable
  Equal<PX.Objects.PO.POOrder.orderNbr>>>>>.And<BqlOperand<
  #nullable enable
  CommitmentInquiry.POLineCommitment.orderType, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.PO.POOrder.orderType>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMCommitment.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.projectID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.projectID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMCommitment.accountGroupID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.accountGroupID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.accountGroupID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMCommitment.projectTaskID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.projectTaskID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.projectTaskID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMCommitment.costCodeID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.costCode, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.costCode>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMCommitment.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.inventoryID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.inventoryID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.ownerID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.ownerID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.ownerID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PMProject.status, IBqlString>.IsNotEqual<
  #nullable disable
  ProjectStatus.closed>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.POLineCommitment.vendorID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.vendorID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.vendorID>, IBqlInt>.IsNull>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.POLineCommitment.relatedDocumentType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.relatedDocumentType, IBqlString>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.relatedDocumentType>, IBqlString>.IsEqual<
  #nullable disable
  RelatedDocumentType.all>>>>, PMCommitment>.View Items;
  public FbqlSelect<SelectFromBase<CommitmentInquiry.PMCommitmentAlias, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CommitmentInquiry.POLineCommitment>.On<BqlOperand<
  #nullable enable
  CommitmentInquiry.PMCommitmentAlias.commitmentID, IBqlGuid>.IsEqual<
  #nullable disable
  CommitmentInquiry.POLineCommitment.commitmentID>>>, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  CommitmentInquiry.PMCommitmentAlias.projectID, IBqlInt>.IsEqual<
  #nullable disable
  PMProject.contractID>>>, FbqlJoins.Left<PX.Objects.PO.POOrder>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.POLineCommitment.orderNbr, 
  #nullable disable
  Equal<PX.Objects.PO.POOrder.orderNbr>>>>>.And<BqlOperand<
  #nullable enable
  CommitmentInquiry.POLineCommitment.orderType, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.PO.POOrder.orderType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.PMCommitmentAlias.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.projectID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.projectID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.PMCommitmentAlias.accountGroupID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.accountGroupID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.accountGroupID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.PMCommitmentAlias.projectTaskID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.projectTaskID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.projectTaskID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.PMCommitmentAlias.costCodeID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.costCode, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.costCode>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.PMCommitmentAlias.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.inventoryID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.inventoryID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.ownerID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.ownerID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.ownerID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.POLineCommitment.vendorID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.vendorID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.vendorID>, IBqlInt>.IsNull>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CommitmentInquiry.POLineCommitment.relatedDocumentType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.relatedDocumentType, IBqlString>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  CommitmentInquiry.ProjectBalanceFilter.relatedDocumentType>, IBqlString>.IsEqual<
  #nullable disable
  RelatedDocumentType.all>>>>.Aggregate<To<Sum<CommitmentInquiry.PMCommitmentAlias.qty>, Sum<CommitmentInquiry.PMCommitmentAlias.amount>, Sum<CommitmentInquiry.PMCommitmentAlias.openQty>, Sum<CommitmentInquiry.PMCommitmentAlias.openAmount>, Sum<CommitmentInquiry.PMCommitmentAlias.receivedQty>, Sum<CommitmentInquiry.PMCommitmentAlias.invoicedQty>, Sum<CommitmentInquiry.PMCommitmentAlias.invoicedAmount>>>, CommitmentInquiry.PMCommitmentAlias>.View Totals;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMCostCode> dummyCostCode;
  public PXAction<CommitmentInquiry.ProjectBalanceFilter> createCommitment;
  public PXAction<CommitmentInquiry.ProjectBalanceFilter> viewProject;
  public PXAction<CommitmentInquiry.ProjectBalanceFilter> viewExternalCommitment;
  public PXAction<CommitmentInquiry.ProjectBalanceFilter> viewVendor;

  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  [PXMergeAttributes]
  [ActiveOrInPlanningProjectTask(typeof (CommitmentInquiry.ProjectBalanceFilter.projectID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCommitment.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Project Description")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.description> e)
  {
  }

  [PXUIField(DisplayName = "Create External Commitment")]
  [PXButton(Tooltip = "Create External Commitment")]
  public virtual IEnumerable CreateCommitment(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToScreen((PXGraph) PXGraph.CreateInstance<ExternalCommitmentEntry>(), "Commitment Entry - Create External Commitment");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewProject(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCommitment>) this.Items).Current != null)
      ProjectAccountingService.NavigateToProjectScreen(((PXSelectBase<PMCommitment>) this.Items).Current.ProjectID);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewExternalCommitment(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCommitment>) this.Items).Current != null)
    {
      ExternalCommitmentEntry instance = PXGraph.CreateInstance<ExternalCommitmentEntry>();
      ((PXSelectBase<PMCommitment>) instance.Commitments).Current = ((PXSelectBase<PMCommitment>) this.Items).Current;
      ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View External Commitment");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewVendor(PXAdapter adapter)
  {
    if (((PXSelectBase<PMCommitment>) this.Items).Current != null)
    {
      VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
      VendorR topFirst = PXSelectBase<VendorR, PXViewOf<VendorR>.BasedOn<SelectFromBase<VendorR, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POLine>.On<BqlOperand<PX.Objects.PO.POLine.vendorID, IBqlInt>.IsEqual<VendorR.bAccountID>>>>.Where<BqlOperand<PX.Objects.PO.POLine.commitmentID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) ((PXSelectBase<PMCommitment>) this.Items).Current.CommitmentID
      }).TopFirst;
      ((PXSelectBase<VendorR>) instance.BAccount).Current = topFirst;
      ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Vendor");
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMCommitment> e)
  {
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POOrder.ownerID>((PXCache) GraphHelper.Caches<PX.Objects.PO.POOrder>((PXGraph) this), (object) null, false);
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class PMCommitmentAlias : PMCommitment
  {
    [PXDBQuantity(BqlField = typeof (PMCommitment.qty))]
    [PXUIField(DisplayName = "Revised Qty.")]
    public override Decimal? Qty { get; set; }

    [PXDBBaseCury(BqlField = typeof (PMCommitment.amount))]
    [PXUIField(DisplayName = "Revised Amt.")]
    public override Decimal? Amount { get; set; }

    [PXDBQuantity(BqlField = typeof (PMCommitment.openQty))]
    [PXUIField(DisplayName = "Open Qty.")]
    public override Decimal? OpenQty { get; set; }

    [PXDBBaseCury(BqlField = typeof (PMCommitment.openAmount))]
    [PXUIField(DisplayName = "Open Amt.")]
    public override Decimal? OpenAmount { get; set; }

    [PXDBQuantity(BqlField = typeof (PMCommitment.receivedQty))]
    [PXUIField(DisplayName = "Received Qty.")]
    public override Decimal? ReceivedQty { get; set; }

    [PXDBQuantity(BqlField = typeof (PMCommitment.invoicedQty))]
    [PXUIField(DisplayName = "Invoiced Qty.")]
    public override Decimal? InvoicedQty { get; set; }

    [PXDBBaseCury(BqlField = typeof (PMCommitment.invoicedAmount))]
    [PXUIField(DisplayName = "Invoiced Amt.")]
    public override Decimal? InvoicedAmount { get; set; }

    public new abstract class qty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.qty>
    {
    }

    public new abstract class amount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.amount>
    {
    }

    public new abstract class openQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.openQty>
    {
    }

    public new abstract class openAmount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.openAmount>
    {
    }

    public new abstract class receivedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.receivedQty>
    {
    }

    public new abstract class invoicedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.invoicedQty>
    {
    }

    public new abstract class invoicedAmount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.invoicedAmount>
    {
    }

    public new abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.projectID>
    {
    }

    public new abstract class accountGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.accountGroupID>
    {
    }

    public new abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.projectTaskID>
    {
    }

    public new abstract class costCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.costCodeID>
    {
    }

    public new abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.inventoryID>
    {
    }

    public new abstract class commitmentID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.commitmentID>
    {
    }

    public new abstract class refNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      CommitmentInquiry.PMCommitmentAlias.refNoteID>
    {
    }
  }

  [PXCacheName("Commitments")]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class ProjectBalanceFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _ProjectID;
    protected int? _AccountGroupID;
    protected int? _ProjectTaskID;
    protected int? _InventoryID;
    protected Decimal? _Qty;
    protected Decimal? _Amount;
    protected Decimal? _OpenQty;
    protected Decimal? _OpenAmount;
    protected Decimal? _ReceivedQty;
    protected Decimal? _InvoicedQty;
    protected Decimal? _InvoicedAmount;

    [Project(typeof (Where<PMProject.nonProject, Equal<False>, And<PMProject.baseType, Equal<CTPRType.project>>>))]
    public virtual int? ProjectID
    {
      get => this._ProjectID;
      set => this._ProjectID = value;
    }

    [AccountGroup]
    public virtual int? AccountGroupID
    {
      get => this._AccountGroupID;
      set => this._AccountGroupID = value;
    }

    [ProjectTask(typeof (CommitmentInquiry.ProjectBalanceFilter.projectID))]
    public virtual int? ProjectTaskID
    {
      get => this._ProjectTaskID;
      set => this._ProjectTaskID = value;
    }

    [PXDBInt]
    [PXUIField]
    [PMInventorySelector]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [CostCode(Filterable = false, SkipVerification = true)]
    public virtual int? CostCode { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Related Document Type")]
    [PXUnboundDefault("ALL")]
    [RelatedDocumentType.List]
    public string RelatedDocumentType { get; set; }

    [PXDecimal]
    [PXUnboundDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Revised Quantity")]
    public virtual Decimal? Qty
    {
      get => this._Qty;
      set => this._Qty = value;
    }

    [PXBaseCury]
    [PXUnboundDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Revised Amount")]
    public virtual Decimal? Amount
    {
      get => this._Amount;
      set => this._Amount = value;
    }

    [PXDecimal]
    [PXUnboundDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Open Quantity")]
    public virtual Decimal? OpenQty
    {
      get => this._OpenQty;
      set => this._OpenQty = value;
    }

    [PXBaseCury]
    [PXUnboundDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Open Amount")]
    public virtual Decimal? OpenAmount
    {
      get => this._OpenAmount;
      set => this._OpenAmount = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Received Quantity")]
    public virtual Decimal? ReceivedQty
    {
      get => this._ReceivedQty;
      set => this._ReceivedQty = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Invoiced Quantity")]
    public virtual Decimal? InvoicedQty
    {
      get => this._InvoicedQty;
      set => this._InvoicedQty = value;
    }

    [PXDBBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Invoiced Amount")]
    public virtual Decimal? InvoicedAmount
    {
      get => this._InvoicedAmount;
      set => this._InvoicedAmount = value;
    }

    [POVendor]
    public virtual int? VendorID { get; set; }

    [Owner(DisplayName = "Project Manager")]
    public virtual int? OwnerID { get; set; }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.projectID>
    {
    }

    public abstract class accountGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.accountGroupID>
    {
    }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.projectTaskID>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.inventoryID>
    {
    }

    public abstract class costCode : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.costCode>
    {
    }

    public abstract class relatedDocumentType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.relatedDocumentType>
    {
    }

    public abstract class qty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.qty>
    {
    }

    public abstract class amount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.amount>
    {
    }

    public abstract class openQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.openQty>
    {
    }

    public abstract class openAmount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.openAmount>
    {
    }

    public abstract class receivedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.receivedQty>
    {
    }

    public abstract class invoicedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.invoicedQty>
    {
    }

    public abstract class invoicedAmount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.invoicedAmount>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.vendorID>
    {
    }

    public abstract class ownerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.ProjectBalanceFilter.ownerID>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (SelectFrom<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.InnerJoin<PX.Objects.AP.Vendor>.On<BqlOperand<PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<PX.Objects.PO.POLine.vendorID>>), Persistent = false)]
  [Serializable]
  public class POLineCommitment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _OrderNbr;

    [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.PO.POLine.orderType))]
    [PXDBDefault(typeof (PX.Objects.PO.POOrder.orderType))]
    [PXUIField]
    public virtual string OrderType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.PO.POLine.orderType))]
    [PXUIField]
    public virtual string OrderNbr { get; set; }

    [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.lineNbr))]
    [PXUIField]
    public virtual int? LineNbr { get; set; }

    [PXDBGuid(false, BqlField = typeof (PX.Objects.PO.POLine.commitmentID))]
    public virtual Guid? CommitmentID { get; set; }

    [PXString]
    [RelatedDocumentType.List]
    [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.PO.POLine.orderType, In3<POOrderType.regularOrder, POOrderType.projectDropShip>>, RelatedDocumentType.purchaseOrder>, Case<Where<PX.Objects.PO.POLine.orderType, Equal<POOrderType.regularSubcontract>>, RelatedDocumentType.subcontract>>), typeof (string))]
    public virtual string RelatedDocumentType { get; set; }

    [PXString]
    [PXUIField]
    [PXDBCalced(typeof (PX.Objects.PO.POLine.orderType), typeof (string))]
    [RelatedDocumentType.DetailList]
    public virtual string RelatedDocumentTypeExt { get; set; }

    [POVendor(BqlField = typeof (PX.Objects.PO.POLine.vendorID))]
    public virtual int? VendorID { get; set; }

    [PXDBString(BqlField = typeof (PX.Objects.AP.Vendor.acctName))]
    [PXUIField]
    public virtual string VendorName { get; set; }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CommitmentInquiry.POLineCommitment.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CommitmentInquiry.POLineCommitment.orderNbr>
    {
    }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.POLineCommitment.lineNbr>
    {
    }

    public abstract class commitmentID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      CommitmentInquiry.POLineCommitment.commitmentID>
    {
    }

    public abstract class relatedDocumentType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CommitmentInquiry.POLineCommitment.relatedDocumentType>
    {
    }

    public abstract class relatedDocumentTypeExt : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CommitmentInquiry.POLineCommitment.relatedDocumentTypeExt>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CommitmentInquiry.POLineCommitment.vendorID>
    {
    }

    public abstract class vendorName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CommitmentInquiry.POLineCommitment.vendorName>
    {
    }
  }
}
