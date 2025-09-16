// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ExternalTransactionValidation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.Common.Attributes;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

public class ExternalTransactionValidation : PXGraph<
#nullable disable
ExternalTransactionValidation>
{
  public PXFilter<ExternalTransactionValidation.ExternalTransactionFilter> Filter;
  public PXCancel<ExternalTransactionValidation.ExternalTransactionFilter> Cancel;
  public PXAction<ExternalTransactionValidation.ExternalTransactionFilter> ViewDocument;
  public PXAction<ExternalTransactionValidation.ExternalTransactionFilter> ViewOrigDocument;
  public PXAction<ExternalTransactionValidation.ExternalTransactionFilter> ViewProcessingCenter;
  public PXAction<ExternalTransactionValidation.ExternalTransactionFilter> ViewExternalTransaction;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<ExternalTransaction, ExternalTransactionValidation.ExternalTransactionFilter, Where<ExternalTransaction.refNbr, IsNotNull, And<ExternalTransaction.docType, In3<ARDocType.payment, ARDocType.prepayment, ARDocType.refund, ARDocType.cashSale>, And2<Where<ExternalTransaction.procStatus, In3<ExtTransactionProcStatusCode.authorizeHeldForReview, ExtTransactionProcStatusCode.captureHeldForReview>, Or<ExternalTransaction.needSync, Equal<True>>>, And<Where<ExternalTransaction.processingCenterID, Equal<Current<ExternalTransactionValidation.ExternalTransactionFilter.processingCenterID>>, Or<Current<ExternalTransactionValidation.ExternalTransactionFilter.processingCenterID>, IsNull>>>>>>, OrderBy<Desc<ExternalTransaction.refNbr>>> PaymentTrans;

  public ExternalTransactionValidation()
  {
    ((PXProcessing<ExternalTransaction>) this.PaymentTrans).SetProcessCaption("Validate");
    ((PXProcessing<ExternalTransaction>) this.PaymentTrans).SetProcessAllCaption("Validate All");
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    ExternalTransaction current = ((PXSelectBase<ExternalTransaction>) this.PaymentTrans).Current;
    if (current != null)
    {
      PXGraph sourceDocumentGraph = CCTransactionsHistoryEnq.FindSourceDocumentGraph(current.DocType, current.RefNbr, (string) null, (string) null);
      if (sourceDocumentGraph != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException(sourceDocumentGraph, true, "View Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return (IEnumerable) ((PXSelectBase<ExternalTransactionValidation.ExternalTransactionFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewOrigDocument(PXAdapter adapter)
  {
    ExternalTransaction current = ((PXSelectBase<ExternalTransaction>) this.PaymentTrans).Current;
    if (current != null && !string.IsNullOrWhiteSpace(current.OrigRefNbr))
    {
      SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) current.OrigRefNbr, new object[1]
      {
        (object) current.OrigDocType
      }));
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Orig. Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return (IEnumerable) ((PXSelectBase<ExternalTransactionValidation.ExternalTransactionFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewProcessingCenter(PXAdapter adapter)
  {
    if (((PXSelectBase<ExternalTransaction>) this.PaymentTrans).Current != null)
    {
      CustomerPaymentMethod customerPaymentMethod = PXResultset<CustomerPaymentMethod>.op_Implicit(PXSelectBase<CustomerPaymentMethod, PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<ExternalTransaction>) this.PaymentTrans).Current.PMInstanceID
      }));
      CCProcessingCenterMaint instance = PXGraph.CreateInstance<CCProcessingCenterMaint>();
      ((PXSelectBase<CCProcessingCenter>) instance.ProcessingCenter).Current = PXResultset<CCProcessingCenter>.op_Implicit(((PXSelectBase<CCProcessingCenter>) instance.ProcessingCenter).Search<CCProcessingCenter.processingCenterID>((object) customerPaymentMethod.CCProcessingCenterID, Array.Empty<object>()));
      if (((PXSelectBase<CCProcessingCenter>) instance.ProcessingCenter).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Processing Center");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return (IEnumerable) ((PXSelectBase<ExternalTransactionValidation.ExternalTransactionFilter>) this.Filter).Select(Array.Empty<object>());
  }

  protected virtual void ExternalTransactionFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ExternalTransactionValidation.ExternalTransactionFilter))
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<ExternalTransaction>) this.PaymentTrans).SetProcessDelegate(ExternalTransactionValidation.\u003C\u003Ec.\u003C\u003E9__14_0 ?? (ExternalTransactionValidation.\u003C\u003Ec.\u003C\u003E9__14_0 = new PXProcessingBase<ExternalTransaction>.ProcessListDelegate((object) ExternalTransactionValidation.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CExternalTransactionFilter_RowSelected\u003Eb__14_0))));
  }

  public static void ValidateCCPayment(
    PXGraph graph,
    List<IExternalTransaction> list,
    bool isMassProcess)
  {
    bool flag = false;
    ARCashSaleEntry arCashSaleEntry = (ARCashSaleEntry) null;
    ARPaymentEntry arPaymentEntry = (ARPaymentEntry) null;
    SOInvoiceEntry soInvoiceEntry = (SOInvoiceEntry) null;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index] != null)
      {
        if (index % 100 == 0)
        {
          ((PXGraph) arCashSaleEntry)?.Clear();
          ((PXGraph) arPaymentEntry)?.Clear();
          ((PXGraph) soInvoiceEntry)?.Clear();
        }
        IExternalTransaction externalTransaction = list[index];
        foreach (PXResult<ExternalTransaction, PX.Objects.AR.Standalone.ARRegister, ARPayment, ExternalTransactionValidation.ARPaymentByVoidLink, ARCashSale> pxResult in PXSelectBase<ExternalTransaction, PXSelectJoin<ExternalTransaction, InnerJoin<PX.Objects.AR.Standalone.ARRegister, On<PX.Objects.AR.Standalone.ARRegister.refNbr, Equal<ExternalTransaction.refNbr>, And<PX.Objects.AR.Standalone.ARRegister.docType, Equal<ExternalTransaction.docType>>>, LeftJoin<ARPayment, On<ARPayment.refNbr, Equal<PX.Objects.AR.Standalone.ARRegister.refNbr>, And<ARPayment.docType, Equal<PX.Objects.AR.Standalone.ARRegister.docType>>>, LeftJoin<ExternalTransactionValidation.ARPaymentByVoidLink, On<ExternalTransactionValidation.ARPaymentByVoidLink.refNbr, Equal<ExternalTransaction.voidRefNbr>, And<ExternalTransactionValidation.ARPaymentByVoidLink.docType, Equal<ExternalTransaction.voidDocType>>>, LeftJoin<ARCashSale, On<ARCashSale.refNbr, Equal<PX.Objects.AR.Standalone.ARRegister.refNbr>, And<ARCashSale.docType, Equal<PX.Objects.AR.Standalone.ARRegister.docType>>>>>>>, Where<ExternalTransaction.transactionID, Equal<Required<ExternalTransaction.transactionID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
        {
          (object) externalTransaction.TransactionID
        }))
        {
          if (pxResult != null)
          {
            try
            {
              ARCashSale arCashSale = PXResult<ExternalTransaction, PX.Objects.AR.Standalone.ARRegister, ARPayment, ExternalTransactionValidation.ARPaymentByVoidLink, ARCashSale>.op_Implicit(pxResult);
              if (arCashSale != null && arCashSale != null && arCashSale.RefNbr != null)
              {
                arCashSaleEntry = arCashSaleEntry ?? PXGraph.CreateInstance<ARCashSaleEntry>();
                ARCashSaleEntryPaymentTransaction extension = ((PXGraph) arCashSaleEntry).GetExtension<ARCashSaleEntryPaymentTransaction>();
                ((PXSelectBase<ARCashSale>) arCashSaleEntry.Document).Current = arCashSale;
                if (extension.CanValidate(((PXSelectBase<ARCashSale>) arCashSaleEntry.Document).Current))
                  ((PXAction) extension.validateCCPayment).Press();
              }
              else
              {
                ARPayment arPayment = PXResult<ExternalTransaction, PX.Objects.AR.Standalone.ARRegister, ARPayment, ExternalTransactionValidation.ARPaymentByVoidLink, ARCashSale>.op_Implicit(pxResult);
                if (arPayment != null && arPayment != null && arPayment.RefNbr != null)
                {
                  arPaymentEntry = arPaymentEntry ?? PXGraph.CreateInstance<ARPaymentEntry>();
                  ARPaymentEntryPaymentTransaction extension = ((PXGraph) arPaymentEntry).GetExtension<ARPaymentEntryPaymentTransaction>();
                  ExternalTransactionValidation.ARPaymentByVoidLink paymentByVoidLink = PXResult<ExternalTransaction, PX.Objects.AR.Standalone.ARRegister, ARPayment, ExternalTransactionValidation.ARPaymentByVoidLink, ARCashSale>.op_Implicit(pxResult);
                  if (paymentByVoidLink != null && paymentByVoidLink.RefNbr != null)
                    ((PXSelectBase<ARPayment>) arPaymentEntry.Document).Current = (ARPayment) paymentByVoidLink;
                  if (paymentByVoidLink != null && paymentByVoidLink.RefNbr != null && extension.CanValidate())
                  {
                    ((PXAction) extension.validateCCPayment).Press();
                  }
                  else
                  {
                    ((PXSelectBase<ARPayment>) arPaymentEntry.Document).Current = arPayment;
                    if (extension.CanValidate())
                      ((PXAction) extension.validateCCPayment).Press();
                  }
                }
              }
              if (isMassProcess)
                PXProcessing<ExternalTransaction>.SetInfo(index, "The record has been processed successfully.");
            }
            catch (Exception ex)
            {
              flag = true;
              if (!isMassProcess)
                throw new PX.Objects.Common.PXMassProcessException(index, ex);
              PXProcessing<ExternalTransaction>.SetError(index, ex);
            }
          }
        }
      }
    }
    if (flag)
      throw new PXOperationCompletedWithErrorException("At least one document could not be validated.");
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  protected virtual void ExternalTransaction_CreateProfile_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  protected virtual void ExternalTransaction_NeedSync_CacheAttached(PXCache sender)
  {
  }

  [PXHidden]
  public class ARPaymentByVoidLink : ARPayment
  {
    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExternalTransactionValidation.ARPaymentByVoidLink.docType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExternalTransactionValidation.ARPaymentByVoidLink.refNbr>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class ExternalTransactionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID>), DescriptionField = typeof (CCProcessingCenter.name))]
    [PXUIField]
    [DeprecatedProcessing(ChckVal = DeprecatedProcessingAttribute.CheckVal.ProcessingCenterId)]
    [DisabledProcCenter(CheckFieldValue = DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId)]
    public virtual string ProcessingCenterID { get; set; }

    [PXDBString(IsUnicode = false)]
    [ExternalTransactionValidation.DisplayTypes.List]
    [PXDefault("HELDFORREVIEW")]
    [PXUIField(DisplayName = "Display Transactions")]
    public virtual string DisplayType { get; set; }

    public abstract class processingCenterID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExternalTransactionValidation.ExternalTransactionFilter.processingCenterID>
    {
    }

    public abstract class displayType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExternalTransactionValidation.ExternalTransactionFilter.displayType>
    {
    }
  }

  private static class DisplayTypes
  {
    public const string HeldForReview = "HELDFORREVIEW";

    [PXLocalizable]
    public class UI
    {
      public const string HeldForReview = "Held for Review";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[1]{ "HELDFORREVIEW" }, new string[1]
        {
          "Held for Review"
        })
      {
      }
    }
  }
}
