// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ACHPlugInHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.ACHPlugInBase;
using PX.ACHPlugInBase.Exceptions;
using PX.Data;
using PX.Objects.GL;
using System;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CA;

public static class ACHPlugInHelper
{
  public static IACHPlugIn GetPlugIn(this PaymentMethod pt)
  {
    Type type = PXBuildManager.GetType(pt.APBatchExportPlugInTypeName, true);
    return !typeof (PXGraph).IsAssignableFrom(type) ? Activator.CreateInstance(type) as IACHPlugIn : PXGraph.CreateInstance(type) as IACHPlugIn;
  }

  public static void RunWithVerifications(
    this IACHPlugIn plugin,
    System.Action action,
    CABatchEntry graph,
    CABatch document)
  {
    plugin.RunWithVerifications(action, graph, document, "Some payment instructions are either invalid or empty. Check the payment instructions of the vendors that use the {0} payment method on the Vendors (AP303000) form.", "Some payments cannot be exported. Check errors on the payment detail lines.");
  }

  public static void RunWithVerifications(
    this IACHPlugIn plugin,
    System.Action action,
    CABatchEntry graph,
    CABatch document,
    string errorVendorPaymentMethodSettingAreInvalidOrEmpty,
    string errorSomePaymentsCannotBeExported)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ACHPlugInHelper.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new ACHPlugInHelper.\u003C\u003Ec__DisplayClass2_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.action = action;
    try
    {
      plugin.VerifySettings((IACHDataProvider) graph, document.BatchNbr);
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) graph, new PXToggleAsyncDelegate((object) cDisplayClass20, __methodptr(\u003CRunWithVerifications\u003Eb__0)));
    }
    catch (InvalidMappingSettingException ex)
    {
      ((PXSelectBase) graph.Document).Cache.RaiseExceptionHandling<CABatch.paymentMethodID>((object) ((PXSelectBase<CABatch>) graph.Document).Current, (object) ((PXSelectBase<CABatch>) graph.Document).Current.PaymentMethodID, (Exception) new PXSetPropertyException((IBqlTable) document, ((Exception) ex).Message, (PXErrorLevel) 4));
      throw;
    }
    catch (InvalidRemittanceSettingException ex)
    {
      CashAccount cashAccount = ((PXSelectBase<CashAccount>) graph.cashAccount).SelectSingle(Array.Empty<object>());
      ((PXSelectBase) graph.Document).Cache.RaiseExceptionHandling<CABatch.cashAccountID>((object) ((PXSelectBase<CABatch>) graph.Document).Current, (object) cashAccount?.CashAccountCD, (Exception) new PXSetPropertyException((IBqlTable) document, ((Exception) ex).Message, (PXErrorLevel) 4));
      throw;
    }
    catch (InvalidPaymentInstructionsException ex)
    {
      foreach (Payment issuedPayment in ex.IssuedPayments)
      {
        CABatchDetail caBatchDetail = PXResultset<CABatchDetail>.op_Implicit(PXSelectBase<CABatchDetail, PXSelect<CABatchDetail, Where<CABatchDetail.batchNbr, Equal<Current<CABatch.batchNbr>>, And<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<CABatchDetail.origRefNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>>>>.Config>.Select((PXGraph) graph, new object[2]
        {
          (object) issuedPayment.DocType,
          (object) issuedPayment.RefNbr
        }));
        ((PXSelectBase) graph.BatchPayments).Cache.RaiseExceptionHandling<CABatchDetail.origRefNbr>((object) caBatchDetail, (object) caBatchDetail.OrigRefNbr, (Exception) new PXSetPropertyException((IBqlTable) document, ex.GetErrorFor(issuedPayment), (PXErrorLevel) 5));
      }
      throw new PXException(PXLocalizer.LocalizeFormat(errorVendorPaymentMethodSettingAreInvalidOrEmpty, new object[1]
      {
        (object) ((PXSelectBase<CABatch>) graph.Document).Current?.PaymentMethodID
      }));
    }
    catch (InvalidPaymentsDataException ex)
    {
      foreach (Payment issuedPayment in ex.IssuedPayments)
      {
        CABatchDetail caBatchDetail = PXResultset<CABatchDetail>.op_Implicit(PXSelectBase<CABatchDetail, PXSelect<CABatchDetail, Where<CABatchDetail.batchNbr, Equal<Current<CABatch.batchNbr>>, And<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<CABatchDetail.origRefNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>>>>.Config>.Select((PXGraph) graph, new object[2]
        {
          (object) issuedPayment.DocType,
          (object) issuedPayment.RefNbr
        }));
        ((PXSelectBase) graph.BatchPayments).Cache.RaiseExceptionHandling<CABatchDetail.origRefNbr>((object) caBatchDetail, (object) caBatchDetail.OrigRefNbr, (Exception) new PXSetPropertyException((IBqlTable) document, ex.GetErrorFor(issuedPayment), (PXErrorLevel) 5));
      }
      throw new PXException(PXLocalizer.LocalizeFormat(errorSomePaymentsCannotBeExported, Array.Empty<object>()));
    }
    catch (ServiceClassCodeException ex)
    {
      ((PXSelectBase) graph.Document).Cache.RaiseExceptionHandling<CABatch.paymentMethodID>((object) ((PXSelectBase<CABatch>) graph.Document).Current, (object) ((PXSelectBase<CABatch>) graph.Document).Current.PaymentMethodID, (Exception) new PXSetPropertyException((IBqlTable) document, ((Exception) ex).Message, (PXErrorLevel) 4));
      throw;
    }
  }
}
