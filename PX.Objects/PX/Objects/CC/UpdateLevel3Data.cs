// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.UpdateLevel3Data
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.Standalone;
using PX.Objects.CC.GraphExtensions;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CC;

public class UpdateLevel3Data : PXGraph<
#nullable disable
UpdateLevel3Data>
{
  public PXCancel<UpdateLevel3Data.L3DocumentProcessingFilter> Cancel;
  public PXFilter<UpdateLevel3Data.L3DocumentProcessingFilter> Filter;
  public PXAction<UpdateLevel3Data.L3DocumentProcessingFilter> ViewDocument;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<PX.Objects.AR.ARPayment, UpdateLevel3Data.L3DocumentProcessingFilter, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.ARPayment.FK.Customer>, InnerJoin<ExternalTransaction, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ExternalTransaction.docType, 
  #nullable disable
  Equal<PX.Objects.AR.ARPayment.docType>>>>>.And<BqlOperand<
  #nullable enable
  ExternalTransaction.refNbr, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.AR.ARPayment.refNbr>>>>>> L3Payments;

  public UpdateLevel3Data()
  {
    PXUIFieldAttribute.SetDisplayName<ExternalTransaction.docType>(((PXGraph) this).Caches[typeof (ExternalTransaction)], "Type");
    PXUIFieldAttribute.SetDisplayName<ExternalTransaction.refNbr>(((PXGraph) this).Caches[typeof (ExternalTransaction)], "Reference Nbr.");
    if (((PXSelectBase<UpdateLevel3Data.L3DocumentProcessingFilter>) this.Filter).Current == null)
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<PX.Objects.AR.ARPayment>) this.L3Payments).SetProcessDelegate(UpdateLevel3Data.\u003C\u003Ec.\u003C\u003E9__0_0 ?? (UpdateLevel3Data.\u003C\u003Ec.\u003C\u003E9__0_0 = new PXProcessingBase<PX.Objects.AR.ARPayment>.ProcessListDelegate((object) UpdateLevel3Data.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__0_0))));
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) this.L3Payments).Current;
    PXGraph sourceDocumentGraph = CCTransactionsHistoryEnq.FindSourceDocumentGraph(current.DocType, current.RefNbr, (string) null, (string) null);
    if (sourceDocumentGraph != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException(sourceDocumentGraph, true, "");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return (IEnumerable) ((PXSelectBase<UpdateLevel3Data.L3DocumentProcessingFilter>) this.Filter).Select(Array.Empty<object>());
  }

  public IEnumerable l3Payments()
  {
    UpdateLevel3Data.L3DocumentProcessingFilter current = ((PXSelectBase<UpdateLevel3Data.L3DocumentProcessingFilter>) this.Filter).Current;
    FbqlSelect<SelectFromBase<PX.Objects.AR.ARPayment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.Customer>.On<PX.Objects.AR.ARPayment.FK.Customer>>, FbqlJoins.Inner<ExternalTransaction>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ExternalTransaction.docType, Equal<PX.Objects.AR.ARPayment.docType>>>>>.And<BqlOperand<ExternalTransaction.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARPayment.refNbr>>>>, FbqlJoins.Left<UpdateLevel3Data.ARTranCashSale>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<UpdateLevel3Data.ARTranCashSale.tranType, Equal<PX.Objects.AR.ARPayment.docType>>>>>.And<BqlOperand<UpdateLevel3Data.ARTranCashSale.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARPayment.refNbr>>>>, FbqlJoins.Left<PX.Objects.AR.ARAdjust>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARAdjust.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>>>>, And<BqlOperand<PX.Objects.AR.ARAdjust.adjgRefNbr, IBqlString>.IsEqual<PX.Objects.AR.ARPayment.refNbr>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARAdjust.adjdDocType, In3<ARDocType.invoice, ARDocType.finCharge, ARDocType.debitMemo, ARDocType.creditMemo>>>>>.And<BqlOperand<PX.Objects.AR.ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>>>>, FbqlJoins.Left<PX.Objects.AR.ARInvoice>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.docType, Equal<PX.Objects.AR.ARAdjust.adjdDocType>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARAdjust.adjdRefNbr>>>>.And<Where<BqlOperand<PX.Objects.AR.ARInvoice.paymentsByLinesAllowed, IBqlBool>.IsNotEqual<True>>>>>, FbqlJoins.Left<ARTran>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.tranType, Equal<PX.Objects.AR.ARInvoice.docType>>>>, And<BqlOperand<ARTran.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARInvoice.refNbr>>>>.And<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.lineType, IsNull>>>>.Or<BqlOperand<ARTran.lineType, IBqlString>.IsNotEqual<SOLineType.discount>>>>, And<BqlOperand<ARTran.curyTranAmt, IBqlDecimal>.IsGreater<decimal0>>>>.And<BqlOperand<ARTran.inventoryID, IBqlInt>.IsNotNull>>>>>, FbqlJoins.Left<SOAdjust>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>>>>, And<BqlOperand<SOAdjust.adjgRefNbr, IBqlString>.IsEqual<PX.Objects.AR.ARPayment.refNbr>>>>.And<Where<BqlOperand<SOAdjust.voided, IBqlBool>.IsNotEqual<True>>>>>, FbqlJoins.Left<PX.Objects.SO.SOOrder>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.orderType, Equal<SOAdjust.adjdOrderType>>>>, And<BqlOperand<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.IsEqual<SOAdjust.adjdOrderNbr>>>>.And<Where<BqlOperand<PX.Objects.SO.SOOrder.hold, IBqlBool>.IsNotEqual<True>>>>>, FbqlJoins.Left<SOLine>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.orderType, Equal<PX.Objects.SO.SOOrder.orderType>>>>, And<BqlOperand<SOLine.orderNbr, IBqlString>.IsEqual<PX.Objects.SO.SOOrder.orderNbr>>>>.And<Where<BqlOperand<SOLine.operation, IBqlString>.IsEqual<SOOperation.issue>>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARPayment.docType, In3<ARDocType.payment, ARDocType.prepayment, ARDocType.cashSale>>>>, And<BqlOperand<ExternalTransaction.procStatus, IBqlString>.IsEqual<ExtTransactionProcStatusCode.captureSuccess>>>, And<BqlOperand<ExternalTransaction.l3Status, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ExternalTransaction.settled, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<ExternalTransaction.processingCenterID, IBqlString>.IsEqual<P.AsString>>>.Aggregate<To<GroupBy<PX.Objects.AR.ARRegister.documentKey>, Count<UpdateLevel3Data.ARTranCashSale.refNbr>, Count<ARTran.refNbr>, Count<SOLine.orderNbr>>>.Having<BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Count<UpdateLevel3Data.ARTranCashSale.refNbr>>, Greater<FunctionWrapper<decimal0>>>>>>.Or<BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Count<ARTran.refNbr>>, Greater<FunctionWrapper<decimal0>>>>>>.Or<BqlAggregatedOperand<Count<SOLine.orderNbr>, IBqlInt>.IsGreater<decimal0>>>>.Order<By<BqlField<PX.Objects.AR.ARPayment.refNbr, IBqlString>.Desc>>, PX.Objects.AR.ARPayment>.View view = new FbqlSelect<SelectFromBase<PX.Objects.AR.ARPayment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.Customer>.On<PX.Objects.AR.ARPayment.FK.Customer>>, FbqlJoins.Inner<ExternalTransaction>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ExternalTransaction.docType, Equal<PX.Objects.AR.ARPayment.docType>>>>>.And<BqlOperand<ExternalTransaction.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARPayment.refNbr>>>>, FbqlJoins.Left<UpdateLevel3Data.ARTranCashSale>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<UpdateLevel3Data.ARTranCashSale.tranType, Equal<PX.Objects.AR.ARPayment.docType>>>>>.And<BqlOperand<UpdateLevel3Data.ARTranCashSale.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARPayment.refNbr>>>>, FbqlJoins.Left<PX.Objects.AR.ARAdjust>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARAdjust.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>>>>, And<BqlOperand<PX.Objects.AR.ARAdjust.adjgRefNbr, IBqlString>.IsEqual<PX.Objects.AR.ARPayment.refNbr>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARAdjust.adjdDocType, In3<ARDocType.invoice, ARDocType.finCharge, ARDocType.debitMemo, ARDocType.creditMemo>>>>>.And<BqlOperand<PX.Objects.AR.ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>>>>, FbqlJoins.Left<PX.Objects.AR.ARInvoice>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.docType, Equal<PX.Objects.AR.ARAdjust.adjdDocType>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARAdjust.adjdRefNbr>>>>.And<Where<BqlOperand<PX.Objects.AR.ARInvoice.paymentsByLinesAllowed, IBqlBool>.IsNotEqual<True>>>>>, FbqlJoins.Left<ARTran>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.tranType, Equal<PX.Objects.AR.ARInvoice.docType>>>>, And<BqlOperand<ARTran.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARInvoice.refNbr>>>>.And<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.lineType, IsNull>>>>.Or<BqlOperand<ARTran.lineType, IBqlString>.IsNotEqual<SOLineType.discount>>>>, And<BqlOperand<ARTran.curyTranAmt, IBqlDecimal>.IsGreater<decimal0>>>>.And<BqlOperand<ARTran.inventoryID, IBqlInt>.IsNotNull>>>>>, FbqlJoins.Left<SOAdjust>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>>>>, And<BqlOperand<SOAdjust.adjgRefNbr, IBqlString>.IsEqual<PX.Objects.AR.ARPayment.refNbr>>>>.And<Where<BqlOperand<SOAdjust.voided, IBqlBool>.IsNotEqual<True>>>>>, FbqlJoins.Left<PX.Objects.SO.SOOrder>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.orderType, Equal<SOAdjust.adjdOrderType>>>>, And<BqlOperand<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.IsEqual<SOAdjust.adjdOrderNbr>>>>.And<Where<BqlOperand<PX.Objects.SO.SOOrder.hold, IBqlBool>.IsNotEqual<True>>>>>, FbqlJoins.Left<SOLine>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.orderType, Equal<PX.Objects.SO.SOOrder.orderType>>>>, And<BqlOperand<SOLine.orderNbr, IBqlString>.IsEqual<PX.Objects.SO.SOOrder.orderNbr>>>>.And<Where<BqlOperand<SOLine.operation, IBqlString>.IsEqual<SOOperation.issue>>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARPayment.docType, In3<ARDocType.payment, ARDocType.prepayment, ARDocType.cashSale>>>>, And<BqlOperand<ExternalTransaction.procStatus, IBqlString>.IsEqual<ExtTransactionProcStatusCode.captureSuccess>>>, And<BqlOperand<ExternalTransaction.l3Status, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ExternalTransaction.settled, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<ExternalTransaction.processingCenterID, IBqlString>.IsEqual<P.AsString>>>.Aggregate<To<GroupBy<PX.Objects.AR.ARRegister.documentKey>, Count<UpdateLevel3Data.ARTranCashSale.refNbr>, Count<ARTran.refNbr>, Count<SOLine.orderNbr>>>.Having<BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Count<UpdateLevel3Data.ARTranCashSale.refNbr>>, Greater<FunctionWrapper<decimal0>>>>>>.Or<BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Count<ARTran.refNbr>>, Greater<FunctionWrapper<decimal0>>>>>>.Or<BqlAggregatedOperand<Count<SOLine.orderNbr>, IBqlInt>.IsGreater<decimal0>>>>.Order<By<BqlField<PX.Objects.AR.ARPayment.refNbr, IBqlString>.Desc>>, PX.Objects.AR.ARPayment>.View((PXGraph) this);
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultSorted = true,
      IsResultTruncated = true
    };
    using (new PXFieldScope(((PXSelectBase) view).View, new Type[13]
    {
      typeof (PX.Objects.AR.ARPayment.docType),
      typeof (PX.Objects.AR.ARPayment.refNbr),
      typeof (PX.Objects.AR.ARPayment.docDate),
      typeof (PX.Objects.AR.ARPayment.finPeriodID),
      typeof (PX.Objects.AR.ARPayment.customerID),
      typeof (PX.Objects.AR.Customer.acctName),
      typeof (PX.Objects.AR.ARPayment.status),
      typeof (PX.Objects.AR.ARPayment.curyOrigDocAmt),
      typeof (PX.Objects.AR.ARPayment.curyID),
      typeof (PX.Objects.AR.ARPayment.processingCenterID),
      typeof (PX.Objects.AR.ARPayment.paymentMethodID),
      typeof (ExternalTransaction.l3Status),
      typeof (ExternalTransaction.l3Error)
    }))
    {
      foreach (PXResult<PX.Objects.AR.ARPayment> pxResult in ((PXSelectBase<PX.Objects.AR.ARPayment>) view).SelectWithViewContext(new object[2]
      {
        (object) current.ProcessingStatus,
        (object) current.ProcessingCenterID
      }))
        ((List<object>) pxDelegateResult).Add((object) pxResult);
    }
    return (IEnumerable) pxDelegateResult;
  }

  public static void UpdateL3Data(PXGraph graph, List<PX.Objects.AR.ARPayment> list)
  {
    bool flag = false;
    ARPaymentEntry arPaymentEntry = (ARPaymentEntry) null;
    ARCashSaleEntry arCashSaleEntry = (ARCashSaleEntry) null;
    string str = string.Empty;
    int num = 0;
    foreach (PX.Objects.AR.ARPayment arPayment1 in list)
    {
      try
      {
        switch (arPayment1.DocType)
        {
          case "CSL":
            ARCashSale arCashSale = ARCashSale.PK.Find(graph, arPayment1.DocType, arPayment1.RefNbr);
            if (arCashSale != null)
            {
              arCashSaleEntry = arCashSaleEntry ?? PXGraph.CreateInstance<ARCashSaleEntry>();
              ARCashSaleEntryLevel3 extension = ((PXGraph) arCashSaleEntry).GetExtension<ARCashSaleEntryLevel3>();
              ((PXSelectBase<ARCashSale>) arCashSaleEntry.Document).Current = arCashSale;
              ((PXAction) extension.updateL3Data).Press();
              break;
            }
            continue;
          case "PMT":
          case "PPM":
            PX.Objects.AR.ARPayment arPayment2 = PX.Objects.AR.ARPayment.PK.Find(graph, arPayment1.DocType, arPayment1.RefNbr);
            if (arPayment2 != null)
            {
              arPaymentEntry = arPaymentEntry ?? PXGraph.CreateInstance<ARPaymentEntry>();
              ARPaymentEntryLevel3 extension = ((PXGraph) arPaymentEntry).GetExtension<ARPaymentEntryLevel3>();
              ((PXSelectBase<PX.Objects.AR.ARPayment>) arPaymentEntry.Document).Current = arPayment2;
              ((PXAction) extension.updateL3Data).Press();
              break;
            }
            continue;
        }
        PXProcessing<PX.Objects.AR.ARPayment>.SetInfo(num, "The record has been processed successfully.");
      }
      catch (CCL3ProcessingException ex)
      {
        flag = false;
        PXProcessing<PX.Objects.AR.ARPayment>.SetWarning(num, (Exception) ex);
      }
      catch (Exception ex)
      {
        flag = true;
        str = ex.Message;
        PXProcessing<PX.Objects.AR.ARPayment>.SetError(num, ex);
      }
      ++num;
    }
    if (flag)
      throw new PXOperationCompletedWithErrorException(str);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2024R2.")]
  public static void UpdateL3Data(PXGraph graph, List<IExternalTransaction> externalTransactions)
  {
    throw new NotImplementedException();
  }

  [PXHidden]
  public class L3DocumentProcessingFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(10, IsUnicode = true)]
    [CCProcessingCenterSelector(CCProcessingFeature.Level3)]
    [PXUIField]
    [DeprecatedProcessing(ChckVal = DeprecatedProcessingAttribute.CheckVal.ProcessingCenterId)]
    [DisabledProcCenter(CheckFieldValue = DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId)]
    public virtual string ProcessingCenterID { get; set; }

    [PXDBString(IsUnicode = false)]
    [ExtTransactionL3StatusCode.List]
    [PXDefault("PEN")]
    [PXUIField(DisplayName = "Processing Status")]
    public virtual string ProcessingStatus { get; set; }

    public abstract class processingCenterID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      UpdateLevel3Data.L3DocumentProcessingFilter.processingCenterID>
    {
    }

    public abstract class processingStatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      UpdateLevel3Data.L3DocumentProcessingFilter.processingStatus>
    {
    }
  }

  [PXHidden]
  public class ARTranCashSale : ARTran
  {
    public new abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      UpdateLevel3Data.ARTranCashSale.tranType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      UpdateLevel3Data.ARTranCashSale.refNbr>
    {
    }
  }

  [Obsolete]
  [PXHidden]
  public class ExternalTransactionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public virtual string ProcessingCenterID { get; set; }

    public virtual string ProcessingStatus { get; set; }

    public abstract class processingCenterID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      UpdateLevel3Data.ExternalTransactionFilter.processingCenterID>
    {
    }

    public abstract class processingStatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      UpdateLevel3Data.ExternalTransactionFilter.processingStatus>
    {
    }
  }
}
