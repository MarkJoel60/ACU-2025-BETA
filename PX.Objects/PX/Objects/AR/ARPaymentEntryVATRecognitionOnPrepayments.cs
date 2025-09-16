// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentEntryVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class ARPaymentEntryVATRecognitionOnPrepayments : 
  PXGraphExtension<OrdersToApplyTab, ARPaymentEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>();
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).WhereAnd<Where<ARPayment.docType, NotEqual<ARDocType.prepaymentInvoice>, Or<ARPayment.released, Equal<True>>>>();
    ((PXSelectBase<ARTranPostBal>) ((PXGraphExtension<ARPaymentEntry>) this).Base.ARPost).WhereAnd<Where<ARTranPostBal.accountID, Equal<Current<ARRegister.aRAccountID>>, Or<Where2<Not<ARTranPostBal.docType, Equal<ARDocType.prepaymentInvoice>, And<ARTranPostBal.sourceDocType, Equal<ARDocType.creditMemo>>>, And<Not<ARTranPostBal.docType, Equal<ARDocType.creditMemo>, And<ARTranPostBal.sourceDocType, Equal<ARDocType.prepaymentInvoice>>>>>>>>();
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (ARPaymentType.RefNbrAttribute))]
  [ARPaymentType.RefNbr(typeof (Search2<ARRegisterAlias.refNbr, InnerJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegisterAlias.docType>, And<ARPayment.refNbr, Equal<ARRegisterAlias.refNbr>>>, InnerJoinSingleTable<Customer, On<ARRegisterAlias.customerID, Equal<Customer.bAccountID>>>>, Where<ARRegisterAlias.docType, Equal<Current<ARPayment.docType>>, And2<Where<ARRegisterAlias.docType, NotEqual<ARDocType.prepaymentInvoice>, Or<ARRegisterAlias.released, Equal<True>>>, And<Match<Customer, Current<AccessInfo.userName>>>>>, OrderBy<Desc<ARRegisterAlias.refNbr>>>), Filterable = true, IsPrimaryViewCompatible = true)]
  protected virtual void ARPayment_RefNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<ARPayment.adjDate> e)
  {
    ARPayment row = (ARPayment) e.Row;
    if (row == null || !(row.DocType == "PPI"))
      return;
    ARAdjust arAdjust = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXViewOf<ARAdjust>.BasedOn<SelectFromBase<ARAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdDocType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<ARAdjust.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsNotEqual<ARDocType.creditMemo>>>, And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsNotEqual<ARDocType.refund>>>, And<BqlOperand<ARAdjust.released, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>.Order<By<BqlField<ARAdjust.adjgDocDate, IBqlDateTime>.Desc>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, (object[]) null, new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    }));
    if (arAdjust == null)
      return;
    DateTime newValue = (DateTime) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARPayment.adjDate>, object, object>) e).NewValue;
    DateTime? adjgDocDate = arAdjust.AdjgDocDate;
    if ((adjgDocDate.HasValue ? (newValue < adjgDocDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      object[] objArray = new object[1];
      adjgDocDate = arAdjust.AdjgDocDate;
      objArray[0] = (object) adjgDocDate.Value.ToString("d");
      throw new PXSetPropertyException("The application date cannot be earlier than the date of the latest payment applied to the prepayment invoice ({0}).", objArray);
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<ARPayment.adjFinPeriodID> e)
  {
    ARPayment row = (ARPayment) e.Row;
    if (row == null || !(row.DocType == "PPI"))
      return;
    ARAdjust arAdjust = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXViewOf<ARAdjust>.BasedOn<SelectFromBase<ARAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdDocType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<ARAdjust.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsNotEqual<ARDocType.creditMemo>>>, And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsNotEqual<ARDocType.refund>>>, And<BqlOperand<ARAdjust.released, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>.Order<By<BqlField<ARAdjust.adjgFinPeriodID, IBqlString>.Desc>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, (object[]) null, new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    }));
    if (arAdjust != null && arAdjust.AdjgFinPeriodID.CompareTo((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARPayment.adjFinPeriodID>, object, object>) e).NewValue) > 0)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARPayment.adjFinPeriodID>, object, object>) e).NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARPayment.adjFinPeriodID>, object, object>) e).NewValue);
      throw new PXSetPropertyException("The application period cannot be earlier than the period of the latest payment applied to the prepayment invoice ({0}).", new object[1]
      {
        (object) PeriodIDAttribute.FormatForError(arAdjust.AdjgFinPeriodID)
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowInserting<ARPayment> e)
  {
    ARPayment row = e.Row;
    if (!(row?.DocType == "PPI") || !(row.RefNbr == AutoNumberAttribute.GetNewNumberSymbol<ARPayment.refNbr>(((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<ARPayment>>) e).Cache, (object) e.Row)))
      return;
    ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<ARPayment>>) e).Cache.SetValue<ARPayment.released>((object) e.Row, (object) true);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ARPayment> e)
  {
    ARPayment row = e.Row;
    if (row?.DocType == "PPI" && EnumerableExtensions.IsIn<PXEntryStatus>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<ARPayment>>) e).Cache.GetStatus((object) e.Row), (PXEntryStatus) 2, (PXEntryStatus) 4) || row?.DocType != "PPI" || EnumerableExtensions.IsIn<PXEntryStatus>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<ARPayment>>) e).Cache.GetStatus((object) e.Row), (PXEntryStatus) 3, (PXEntryStatus) 4))
      return;
    SOAdjust soAdjust = PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXSelectJoin<SOAdjust, InnerJoin<SOOrderType, On<SOOrderType.orderType, Equal<SOAdjust.adjdOrderType>>>, Where<SOAdjust.adjgDocType, Equal<Required<ARPayment.docType>>, And<SOAdjust.adjgRefNbr, Equal<Required<ARPayment.refNbr>>, And<SOOrderType.behavior, Equal<SOBehavior.bL>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    }));
    if (soAdjust != null)
    {
      ((PXSelectBase) this.Base1.SOAdjustments).Cache.RaiseExceptionHandling<SOAdjust.adjdOrderType>((object) soAdjust, (object) soAdjust.AdjdOrderType, (Exception) new PXSetPropertyException("Prepayment invoices are not supported for blanket sales orders."));
      throw new PXRowPersistingException(typeof (SOAdjust.adjdOrderType).Name, (object) soAdjust.AdjdOrderType, "Prepayment invoices are not supported for blanket sales orders.");
    }
  }

  private void CheckForBlanketOrders(SOAdjust adj)
  {
    if (adj?.AdjgDocType != "PPI" || adj == null || adj.AdjdOrderType == null)
      return;
    if (!((IQueryable<PXResult<SOOrderType>>) PXSelectBase<SOOrderType, PXSelect<SOOrderType, Where<SOOrderType.orderType, Equal<Required<SOOrderType.orderType>>, And<SOOrderType.behavior, Equal<SOBehavior.bL>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, new object[1]
    {
      (object) adj.AdjdOrderType
    })).Any<PXResult<SOOrderType>>())
      return;
    ((PXSelectBase) this.Base1.SOAdjustments).Cache.RaiseExceptionHandling<SOAdjust.adjdOrderType>((object) adj, (object) adj.AdjdOrderType, (Exception) new PXSetPropertyException("Prepayment invoices are not supported for blanket sales orders."));
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<SOAdjust.adjdOrderType> e)
  {
    this.CheckForBlanketOrders(e.Row as SOAdjust);
  }

  protected virtual void _(PX.Data.Events.RowInserting<SOAdjust> e)
  {
    this.CheckForBlanketOrders(e.Row);
  }

  [PXMergeAttributes]
  [Account]
  protected virtual void ARInvoice_PrepaymentAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Sub<IIf<Where<ARPayment.docType, Equal<ARDocType.prepaymentInvoice>, And<ARPayment.pendingPayment, Equal<True>>>, ARPayment.curyOrigDocAmt, ARPayment.curyDocBal>, Add<ARPayment.curyApplAmt, ARPayment.curySOApplAmt>>))]
  protected virtual void ARPayment_CuryUnappliedBal_CacheAttached(PXCache sender)
  {
  }

  [PXOverride]
  public virtual void CheckDocumentBeforeVoiding(PXGraph graph, ARPayment payment)
  {
    if (payment == null || !(payment.DocType == "PMT") && !(payment.DocType == "PPM"))
      return;
    PXResult<ARAdjust, ARAdjust2> pxResult = (PXResult<ARAdjust, ARAdjust2>) PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<ARAdjust2, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust2.adjgRefNbr, Equal<ARAdjust.adjdRefNbr>>>>, And<BqlOperand<ARAdjust2.adjgDocType, IBqlString>.IsEqual<ARAdjust.adjdDocType>>>, Or<BqlOperand<ARAdjust2.adjdRefNbr, IBqlString>.IsEqual<ARAdjust.adjdRefNbr>>>>.And<BqlOperand<ARAdjust2.adjdDocType, IBqlString>.IsEqual<ARAdjust.adjdDocType>>>>>.And<BqlOperand<ARAdjust2.voided, IBqlBool>.IsNotEqual<True>>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<ARAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>>, And<BqlOperand<ARAdjust.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust2.adjgRefNbr, Equal<ARAdjust.adjdRefNbr>>>>, And<BqlOperand<ARAdjust2.adjgDocType, IBqlString>.IsEqual<ARAdjust.adjdDocType>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust2.adjdRefNbr, Equal<ARAdjust.adjdRefNbr>>>>, And<BqlOperand<ARAdjust2.adjdDocType, IBqlString>.IsEqual<ARAdjust.adjdDocType>>>>.And<BqlOperand<ARAdjust2.adjgDocType, IBqlString>.IsEqual<ARDocType.refund>>>>>, OrderBy<Asc<ARAdjust.adjdRefNbr>>>.Config>.Select(graph, new object[2]
    {
      (object) payment.DocType,
      (object) payment.RefNbr
    }));
    if (pxResult != null)
    {
      ARAdjust arAdjust = PXResult<ARAdjust, ARAdjust2>.op_Implicit(pxResult);
      ARAdjust2 arAdjust2 = PXResult<ARAdjust, ARAdjust2>.op_Implicit(pxResult);
      string docType = arAdjust2.AdjdDocType;
      string str = arAdjust2.AdjdRefNbr;
      if (docType == "PPI")
      {
        docType = arAdjust2.AdjgDocType;
        str = arAdjust2.AdjgRefNbr;
      }
      throw new PXException("The {0} document cannot be voided because it is applied to prepayment invoice {1} that has already been applied to the {2} document with the {3} ref. number. To void the document, reverse the application of the prepayment invoice to the {2} document.", new object[5]
      {
        (object) ARDocType.GetDisplayName(payment.DocType),
        (object) arAdjust.AdjdRefNbr,
        (object) ARDocType.GetDisplayName(docType),
        (object) str,
        (object) ARDocType.GetDisplayName(docType)
      });
    }
  }

  [PXOverride]
  public virtual void CheckDocumentBeforeReversing(PXGraph graph, ARAdjust application)
  {
    if (application == null || !(application.AdjdDocType == "PPI") || !(application.AdjgDocType == "PMT") && !(application.AdjgDocType == "PPM"))
      return;
    ARAdjust arAdjust = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXViewOf<ARAdjust>.BasedOn<SelectFromBase<ARAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.prepaymentInvoice>>>>, And<BqlOperand<ARAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdDocType, Equal<ARDocType.prepaymentInvoice>>>>, And<BqlOperand<ARAdjust.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsEqual<ARDocType.refund>>>>>>.And<BqlOperand<ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>>.Config>.Select(graph, new object[2]
    {
      (object) application.AdjdRefNbr,
      (object) application.AdjdRefNbr
    }));
    if (arAdjust != null)
    {
      string docType = arAdjust.AdjdDocType;
      string str = arAdjust.AdjdRefNbr;
      if (docType == "PPI")
      {
        docType = arAdjust.AdjgDocType;
        str = arAdjust.AdjgRefNbr;
      }
      throw new PXException("The application to the prepayment invoice cannot be reversed because the prepayment invoice has already been applied to the {0} document with the {1} ref. number. To reverse the application, reverse the application of the prepayment invoice to the {0} document.", new object[2]
      {
        (object) ARDocType.GetDisplayName(docType),
        (object) str
      });
    }
  }
}
