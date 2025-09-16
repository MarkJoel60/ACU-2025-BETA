// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaLinkMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CA;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM.DAC;
using PX.Objects.PO;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.PM;

public class ProformaLinkMaint : PXGraph<
#nullable disable
ProformaLinkMaint>
{
  public PXFilter<ProformaLinkMaint.ProformaLinkFilter> Filter;
  public PXCancel<ProformaLinkMaint.ProformaLinkFilter> Cancel;
  public PXSave<ProformaLinkMaint.ProformaLinkFilter> Save;
  [PXViewName("Transactions")]
  public FbqlSelect<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMRegister.module, 
  #nullable disable
  Equal<PMTran.tranType>>>>>.And<BqlOperand<
  #nullable enable
  PMRegister.refNbr, IBqlString>.IsEqual<
  #nullable disable
  PMTran.refNbr>>>>, FbqlJoins.Left<PX.Objects.AP.APRegister>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.AP.APRegister.docType, 
  #nullable disable
  Equal<PMTran.origTranType>>>>, And<BqlOperand<
  #nullable enable
  PX.Objects.AP.APRegister.refNbr, IBqlString>.IsEqual<
  #nullable disable
  PMTran.origRefNbr>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.AP.APRegister.batchNbr, IBqlString>.IsEqual<
  #nullable disable
  PMTran.batchNbr>>>>, FbqlJoins.Left<PMAccountGroup>.On<BqlOperand<
  #nullable enable
  PMAccountGroup.groupID, IBqlInt>.IsEqual<
  #nullable disable
  PMTran.accountGroupID>>>, FbqlJoins.Left<PostGraphExt.OffsetPMAccountGroup>.On<BqlOperand<
  #nullable enable
  PostGraphExt.OffsetPMAccountGroup.groupID, IBqlInt>.IsEqual<
  #nullable disable
  PMTran.offsetAccountGroupID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.proformaRefNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  ProformaLinkMaint.ProformaLinkFilter.refNbr, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMAccountGroup.type, 
  #nullable disable
  Equal<AccountType.expense>>>>>.Or<BqlOperand<
  #nullable enable
  PostGraphExt.OffsetPMAccountGroup.type, IBqlString>.IsEqual<
  #nullable disable
  AccountType.expense>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.tranType, 
  #nullable disable
  NotEqual<BatchModule.moduleAP>>>>>.Or<BqlOperand<
  #nullable enable
  PX.Objects.AP.APRegister.refNbr, IBqlString>.IsNotNull>>>>, 
  #nullable disable
  And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProformaLinkMaint.ProformaLinkFilter.lineNbr>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PMTran.proformaLineNbr, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.ProformaLinkFilter.lineNbr, IBqlInt>.FromCurrent>>>>>>.And<
  #nullable disable
  Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProformaLinkMaint.ProformaLinkFilter.module>, 
  #nullable disable
  Equal<PMTranModules.allModules>>>>>.Or<BqlOperand<
  #nullable enable
  PMTran.tranType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.ProformaLinkFilter.module, IBqlString>.FromCurrent>>>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  PMTran.proformaLineNbr, IBqlInt>.Asc>>, 
  #nullable disable
  PMTran>.View Transactions;
  public PXFilter<ProformaLinkMaint.TranFilter> AvailableTransactionsFilter;
  public PXSetupOptional<PMProforma, Where<PMProforma.refNbr, Equal<Current<ProformaLinkMaint.ProformaLinkFilter.refNbr>>, And<PMProforma.corrected, NotEqual<True>>>> Proforma;
  [PXHidden]
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMRegister.module, 
  #nullable disable
  Equal<PMTran.tranType>>>>>.And<BqlOperand<
  #nullable enable
  PMRegister.refNbr, IBqlString>.IsEqual<
  #nullable disable
  PMTran.refNbr>>>>, FbqlJoins.Left<PX.Objects.AP.APRegister>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.AP.APRegister.docType, 
  #nullable disable
  Equal<PMTran.origTranType>>>>, And<BqlOperand<
  #nullable enable
  PX.Objects.AP.APRegister.refNbr, IBqlString>.IsEqual<
  #nullable disable
  PMTran.origRefNbr>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.AP.APRegister.batchNbr, IBqlString>.IsEqual<
  #nullable disable
  PMTran.batchNbr>>>>, FbqlJoins.Left<PMAccountGroup>.On<BqlOperand<
  #nullable enable
  PMAccountGroup.groupID, IBqlInt>.IsEqual<
  #nullable disable
  PMTran.accountGroupID>>>, FbqlJoins.Left<PostGraphExt.OffsetPMAccountGroup>.On<BqlOperand<
  #nullable enable
  PostGraphExt.OffsetPMAccountGroup.groupID, IBqlInt>.IsEqual<
  #nullable disable
  PMTran.offsetAccountGroupID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.proformaRefNbr, 
  #nullable disable
  IsNull>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMAccountGroup.type, 
  #nullable disable
  Equal<AccountType.expense>>>>>.Or<BqlOperand<
  #nullable enable
  PostGraphExt.OffsetPMAccountGroup.type, IBqlString>.IsEqual<
  #nullable disable
  AccountType.expense>>>>>, And<BqlOperand<
  #nullable enable
  PMTran.projectID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.ProformaLinkFilter.projectID, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.tranType, 
  #nullable disable
  NotEqual<BatchModule.moduleAP>>>>>.Or<BqlOperand<
  #nullable enable
  PX.Objects.AP.APRegister.refNbr, IBqlString>.IsNotNull>>>>, 
  #nullable disable
  And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProformaLinkMaint.TranFilter.projectTaskID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PMTran.taskID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.TranFilter.projectTaskID, IBqlInt>.FromCurrent>>>>>, 
  #nullable disable
  And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProformaLinkMaint.TranFilter.costCodeID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PMTran.costCodeID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.TranFilter.costCodeID, IBqlInt>.FromCurrent>>>>>, 
  #nullable disable
  And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProformaLinkMaint.TranFilter.vendorID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PMTran.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.TranFilter.vendorID, IBqlInt>.FromCurrent>>>>>, 
  #nullable disable
  And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProformaLinkMaint.TranFilter.module>, 
  #nullable disable
  Equal<PMTranModules.allModules>>>>>.Or<BqlOperand<
  #nullable enable
  PMTran.tranType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.TranFilter.module, IBqlString>.FromCurrent>>>>>, 
  #nullable disable
  And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProformaLinkMaint.TranFilter.origDocType>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PMRegister.origDocType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.TranFilter.origDocType, IBqlString>.FromCurrent>>>>>, 
  #nullable disable
  And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProformaLinkMaint.TranFilter.origRefNbr>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PMTran.origRefNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.TranFilter.origRefNbr, IBqlString>.FromCurrent>>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PMTran.date, IBqlDateTime>.IsBetween<
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.TranFilter.dateFrom, IBqlDateTime>.FromCurrent, 
  #nullable disable
  BqlField<
  #nullable enable
  ProformaLinkMaint.TranFilter.dateTo, IBqlDateTime>.FromCurrent>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  PMTran.date, IBqlDateTime>.Asc>>, 
  #nullable disable
  PMTran>.View AvailableTransactions;
  public PXAction<ProformaLinkMaint.ProformaLinkFilter> addTransactions;
  public PXAction<ProformaLinkMaint.ProformaLinkFilter> appendTransactions;
  public PXAction<ProformaLinkMaint.ProformaLinkFilter> removeTransaction;
  public PXAction<ProformaLinkMaint.ProformaLinkFilter> viewDocument;
  public PXAction<ProformaLinkMaint.ProformaLinkFilter> viewBill;
  public PXAction<ProformaLinkMaint.TranFilter> viewVendor;
  public PXAction<ProformaLinkMaint.TranFilter> viewTransaction;

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProforma.refNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Pro Forma Invoice Line Nbr.")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProformaLine.lineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Pro Forma Invoice Line Nbr.")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.proformaLineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Orig. Source")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.tranType> _)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Orig. Doc. Nbr.")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.origRefNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Orig. Doc. Line Nbr.")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.origLineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Vendor")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.bAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Financial Period")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.finPeriodID> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<PMRegister.refNbr>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.refNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Account Group", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.accountGroupID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Quantity", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.qty> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Billable Quantity", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.billableQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Unit Rate", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.tranCuryUnitRate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Billable", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.billable> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.branchID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "UOM", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.uOM> e)
  {
  }

  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  [PXUIField]
  [PXButton]
  public IEnumerable AddTransactions(PXAdapter adapter)
  {
    if (((PXSelectBase) this.AvailableTransactions).View.AskExt() == 1)
      this.AddSelectedLines();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public IEnumerable AppendTransactions(PXAdapter adapter)
  {
    this.AddSelectedLines();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public IEnumerable RemoveTransaction(PXAdapter adapter)
  {
    if (((PXSelectBase<PMTran>) this.Transactions).Current != null && !((PXSelectBase<PMTran>) this.Transactions).Current.Billed.GetValueOrDefault())
    {
      ((PXSelectBase<PMTran>) this.Transactions).Current.ProformaRefNbr = (string) null;
      ((PXSelectBase<PMTran>) this.Transactions).Current.ProformaLineNbr = new int?();
      ((PXSelectBase<PMTran>) this.Transactions).Current.Selected = new bool?(false);
      ((PXSelectBase<PMTran>) this.Transactions).UpdateCurrent();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
    ((PXSelectBase<PMRegister>) instance.Document).Current = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) instance.Document).Search<PMRegister.refNbr>((object) ((PXSelectBase<PMTran>) this.Transactions).Current.RefNbr, new object[1]
    {
      (object) ((PXSelectBase<PMTran>) this.Transactions).Current.TranType
    }));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBill(PXAdapter adapter)
  {
    Guid? nullable = (Guid?) PXResultset<PMRegister>.op_Implicit(PXSelectBase<PMRegister, PXViewOf<PMRegister>.BasedOn<SelectFromBase<PMRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMRegister.refNbr, IBqlString>.IsEqual<BqlField<PMTran.refNbr, IBqlString>.FromCurrent>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))?.OrigNoteID;
    EntityHelper entityHelper = new EntityHelper((PXGraph) this);
    if (nullable.HasValue)
    {
      entityHelper.NavigateToRow(nullable, (PXRedirectHelper.WindowMode) 3);
      return adapter.Get();
    }
    PMTran pmTran = PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.projectID, Equal<BqlField<PMTran.projectID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PMTran.tranID, IBqlLong>.IsEqual<BqlField<PMTran.tranID, IBqlLong>.FromCurrent>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (pmTran == null)
      return adapter.Get();
    if (pmTran.OrigModule == "CA")
      nullable = (Guid?) PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXViewOf<CAAdj>.BasedOn<SelectFromBase<CAAdj, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CAAdj.adjTranType, Equal<P.AsString>>>>>.And<BqlOperand<CAAdj.adjRefNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) pmTran.OrigTranType,
        (object) pmTran.OrigRefNbr
      }))?.NoteID;
    else if (pmTran.OrigModule == "IN")
      nullable = (Guid?) PXResultset<PX.Objects.IN.INRegister>.op_Implicit(PXSelectBase<PX.Objects.IN.INRegister, PXViewOf<PX.Objects.IN.INRegister>.BasedOn<SelectFromBase<PX.Objects.IN.INRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.docType, Equal<PX.Objects.IN.INRegister.docType>>>>>.And<BqlOperand<INTran.refNbr, IBqlString>.IsEqual<PX.Objects.IN.INRegister.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.tranType, Equal<P.AsString>>>>>.And<BqlOperand<INTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) pmTran.OrigTranType,
        (object) pmTran.OrigRefNbr
      }))?.NoteID;
    if (nullable.HasValue)
      entityHelper.NavigateToRow(nullable, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewVendor(PXAdapter adapter)
  {
    VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
    ((PXSelectBase<PX.Objects.AP.VendorR>) instance.BAccount).Current = PXResultset<PX.Objects.AP.VendorR>.op_Implicit(PXSelectBase<PX.Objects.AP.VendorR, PXSelect<PX.Objects.AP.VendorR, Where<PX.Objects.AP.VendorR.bAccountID, Equal<Current<PMTran.bAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewTransaction(PXAdapter adapter)
  {
    TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
    PMTran pmTran = PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.projectID, Equal<BqlField<PMTran.projectID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PMTran.tranID, IBqlLong>.IsEqual<BqlField<PMTran.tranID, IBqlLong>.FromCurrent>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (pmTran == null)
      return adapter.Get();
    ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectID = pmTran.ProjectID;
    ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.TranID = pmTran.TranID;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<ProformaLinkMaint.ProformaLinkFilter> e)
  {
    if (e.Row == null)
      return;
    bool valueOrDefault = e.Row.IsEditable.GetValueOrDefault();
    bool flag = e.Row.LineType == "T";
    ((PXAction) this.addTransactions).SetEnabled(valueOrDefault && !flag);
    ((PXAction) this.removeTransaction).SetEnabled(valueOrDefault && !flag);
    PXUIFieldAttribute.SetEnabled<ProformaLinkMaint.ProformaLinkFilter.refNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ProformaLinkMaint.ProformaLinkFilter>>) e).Cache, (object) e.Row, e.Row.ProjectID.HasValue);
    PXUIFieldAttribute.SetEnabled<ProformaLinkMaint.ProformaLinkFilter.lineNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ProformaLinkMaint.ProformaLinkFilter>>) e).Cache, (object) e.Row, e.Row.RefNbr != null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ProformaLinkMaint.ProformaLinkFilter, ProformaLinkMaint.ProformaLinkFilter.projectID> e)
  {
    e.Row.RefNbr = (string) null;
    e.Row.LineNbr = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ProformaLinkMaint.ProformaLinkFilter, ProformaLinkMaint.ProformaLinkFilter.refNbr> e)
  {
    e.Row.LineNbr = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ProformaLinkMaint.TranFilter, ProformaLinkMaint.TranFilter.dateTo> e)
  {
    DateTime? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ProformaLinkMaint.TranFilter, ProformaLinkMaint.TranFilter.dateTo>, ProformaLinkMaint.TranFilter, object>) e).NewValue as DateTime?;
    if (!newValue.HasValue)
      return;
    DateTime? dateFrom = e.Row.DateFrom;
    if (!dateFrom.HasValue)
      return;
    DateTime date = newValue.Value.Date;
    dateFrom = e.Row.DateFrom;
    if ((dateFrom.HasValue ? (date < dateFrom.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException<ProformaLinkMaint.TranFilter.dateTo>("To Date should be later than From Date ({0:d}).", new object[1]
      {
        (object) e.Row.DateFrom
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ProformaLinkMaint.TranFilter, ProformaLinkMaint.TranFilter.dateFrom> e)
  {
    DateTime? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ProformaLinkMaint.TranFilter, ProformaLinkMaint.TranFilter.dateFrom>, ProformaLinkMaint.TranFilter, object>) e).NewValue as DateTime?;
    if (!newValue.HasValue || ((PXSelectBase<PMProforma>) this.Proforma).Current == null)
      return;
    DateTime date = newValue.Value.Date;
    DateTime? invoiceDate = ((PXSelectBase<PMProforma>) this.Proforma).Current.InvoiceDate;
    if ((invoiceDate.HasValue ? (date > invoiceDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException<ProformaLinkMaint.TranFilter.dateFrom>("From Date should be earlier than the date of the current pro forma invoice ({0:d}).", new object[1]
      {
        (object) ((PXSelectBase<PMProforma>) this.Proforma).Current.InvoiceDate
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ProformaLinkMaint.TranFilter, ProformaLinkMaint.TranFilter.dateFrom> e)
  {
    object newValue = e.NewValue;
    if (!e.Row.DateFrom.HasValue || !e.Row.DateTo.HasValue || !(((PXSelectBase<ProformaLinkMaint.TranFilter>) this.AvailableTransactionsFilter).Current.DateTo.Value.Date < e.Row.DateFrom.Value.Date))
      return;
    ((PXSelectBase<ProformaLinkMaint.TranFilter>) this.AvailableTransactionsFilter).Current.DateTo = e.Row.DateFrom;
  }

  protected virtual void AddSelectedLines()
  {
    foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) this.AvailableTransactions).Select(Array.Empty<object>()))
    {
      PMTran pmTran = PXResult<PMTran>.op_Implicit(pxResult);
      if (pmTran.Selected.GetValueOrDefault())
      {
        pmTran.ProformaRefNbr = ((PXSelectBase<ProformaLinkMaint.ProformaLinkFilter>) this.Filter).Current.RefNbr;
        if (((PXSelectBase<ProformaLinkMaint.ProformaLinkFilter>) this.Filter).Current.LineNbr.HasValue)
          pmTran.ProformaLineNbr = ((PXSelectBase<ProformaLinkMaint.ProformaLinkFilter>) this.Filter).Current.LineNbr;
        ((PXSelectBase<PMTran>) this.Transactions).UpdateCurrent();
      }
    }
  }

  /// <summary>
  /// Filter used in the header of the form to filter out the proforma and any line of the proforma for the selected Project.
  /// </summary>
  [PXCacheName("Link to Pro Forma Invoice")]
  [Serializable]
  public class ProformaLinkFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>The project ID.</summary>
    [PXDefault]
    [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>), WarnIfCompleted = false)]
    public virtual int? ProjectID { get; set; }

    /// <summary>The reference number of the pro forma invoice.</summary>
    [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    [PXDefault]
    [PXSelector(typeof (Search<PMProforma.refNbr, Where<PMProforma.projectID, Equal<Current<ProformaLinkMaint.ProformaLinkFilter.projectID>>, And<PMProforma.corrected, NotEqual<True>>>, OrderBy<Desc<PMProforma.refNbr>>>), DescriptionField = typeof (PMProforma.description))]
    [PXUIField]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// The original sequence number of the line among all the pro forma invoice lines.
    /// </summary>
    /// <remarks>The sequence of line numbers of the pro forma invoice lines belonging to a single document can include gaps.</remarks>
    [PXSelector(typeof (Search<PMProformaLine.lineNbr, Where<PMProformaLine.refNbr, Equal<Current<ProformaLinkMaint.ProformaLinkFilter.refNbr>>, And<PMProformaLine.corrected, NotEqual<True>>>>), new Type[] {typeof (PMProformaLine.lineNbr), typeof (PMProformaLine.description), typeof (PMProformaLine.taskID), typeof (PMProformaLine.costCodeID), typeof (PMProformaLine.inventoryID), typeof (PMProformaLine.uOM), typeof (PMProformaLine.qty), typeof (PMProformaLine.curyLineTotal)}, DescriptionField = typeof (PMProformaLine.description))]
    [PXUIField(DisplayName = "Pro Forma Invoice Line Nbr.")]
    [PXDBInt]
    public virtual int? LineNbr { get; set; }

    /// <summary>
    /// Returns true if Proforma is on hold. Transactions can be linked or unlinked only for a Proforma with an 'On Hold' status.
    /// </summary>
    [PXFormula(typeof (Selector<ProformaLinkMaint.ProformaLinkFilter.refNbr, PMProforma.hold>))]
    [PXBool]
    public virtual bool? IsEditable { get; set; }

    /// <summary>
    /// Returns the line type of the selected line. Can be either 'P' for Progressive or 'T' for Transactional.
    /// </summary>
    [PXFormula(typeof (Selector<ProformaLinkMaint.ProformaLinkFilter.lineNbr, PMProformaLine.type>))]
    [PXString]
    public virtual string LineType { get; set; }

    /// <summary>
    /// The identifier of the functional area to which the GL batch that spawned the transaction belongs.
    /// </summary>
    [PXString(2, IsFixed = true)]
    [PXUIField(DisplayName = "Source")]
    [PMTranModules.StringListWithAll]
    [PXUnboundDefault("**")]
    public virtual string Module { get; set; }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProformaLinkMaint.ProformaLinkFilter.projectID>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProformaLinkMaint.ProformaLinkFilter.refNbr>
    {
    }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProformaLinkMaint.ProformaLinkFilter.lineNbr>
    {
    }

    public abstract class isEditable : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ProformaLinkMaint.ProformaLinkFilter.isEditable>
    {
    }

    public abstract class lineType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProformaLinkMaint.ProformaLinkFilter.lineType>
    {
    }

    public abstract class module : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProformaLinkMaint.ProformaLinkFilter.module>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class TranFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDefault(typeof (ProformaLinkMaint.ProformaLinkFilter.projectID))]
    [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>), WarnIfCompleted = false, Enabled = false)]
    public virtual int? ProjectID { get; set; }

    [ProjectTask(typeof (ProformaLinkMaint.TranFilter.projectID))]
    public virtual int? ProjectTaskID { get; set; }

    [AccountGroup(typeof (Where<Match<PMAccountGroup, Current<AccessInfo.userName>>>))]
    public virtual int? AccountGroupID { get; set; }

    [CostCode(Filterable = false, SkipVerification = true)]
    public virtual int? CostCodeID { get; set; }

    [PXDefault(typeof (Search<PMProject.startDate, Where<PMProject.contractID, Equal<Current<ProformaLinkMaint.TranFilter.projectID>>>>))]
    [PXDBDate]
    [PXUIField(DisplayName = "From Date", Required = false)]
    public virtual DateTime? DateFrom { get; set; }

    [PXDefault(typeof (Search<PMProforma.invoiceDate, Where<PMProforma.refNbr, Equal<Current<ProformaLinkMaint.ProformaLinkFilter.refNbr>>>>))]
    [PXDBDate]
    [PXUIField(DisplayName = "To Date", Required = false)]
    public virtual DateTime? DateTo { get; set; }

    [POVendor]
    public virtual int? VendorID { get; set; }

    [PXDBString(3, IsFixed = true)]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    [PMOrigDocType.List]
    public virtual string OrigDocType { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Orig. Doc. Nbr.")]
    public virtual string OrigRefNbr { get; set; }

    /// <summary>
    /// The identifier of the functional area to which the GL batch that spawned the transaction belongs.
    /// </summary>
    [PXString(2, IsFixed = true)]
    [PXUIField(DisplayName = "Source")]
    [PMTranModules.StringListWithAll]
    [PXUnboundDefault("**")]
    public virtual string Module { get; set; }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProformaLinkMaint.TranFilter.projectID>
    {
    }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProformaLinkMaint.TranFilter.projectTaskID>
    {
    }

    public abstract class accountGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProformaLinkMaint.TranFilter.accountGroupID>
    {
    }

    public abstract class costCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProformaLinkMaint.TranFilter.costCodeID>
    {
    }

    public abstract class dateFrom : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ProformaLinkMaint.TranFilter.dateFrom>
    {
    }

    public abstract class dateTo : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ProformaLinkMaint.TranFilter.dateTo>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProformaLinkMaint.TranFilter.vendorID>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProformaLinkMaint.TranFilter.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProformaLinkMaint.TranFilter.origRefNbr>
    {
    }

    public abstract class module : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ProformaLinkMaint.TranFilter.module>
    {
    }
  }
}
