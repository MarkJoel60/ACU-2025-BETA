// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.Prepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class Prepayments : PXGraphExtension<POOrderEntry>
{
  public PXSelectJoin<POOrderPrepayment, InnerJoin<PX.Objects.AP.APRegister, On<PX.Objects.AP.APRegister.docType, Equal<POOrderPrepayment.aPDocType>, And<PX.Objects.AP.APRegister.refNbr, Equal<POOrderPrepayment.aPRefNbr>>>>, Where<POOrderPrepayment.orderType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<POOrderPrepayment.orderNbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>> PrepaymentDocuments;
  public PXAction<PX.Objects.PO.POOrder> createPrepayment;

  [PXUIField]
  [PXButton]
  public virtual void CreatePrepayment()
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current != null)
    {
      ((PXAction) this.Base.Save).Press();
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      ((PXGraph) instance).GetExtension<PX.Objects.PO.GraphExtensions.APInvoiceEntryExt.Prepayments>().AddPOOrderProc(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current, true);
      throw new PXPopupRedirectException((PXGraph) instance, "New Prepayment", true);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POOrder> e)
  {
    PXAction<PX.Objects.PO.POOrder> createPrepayment = this.createPrepayment;
    PX.Objects.PO.POOrder row = e.Row;
    int num1;
    if (row == null)
    {
      num1 = 0;
    }
    else
    {
      Decimal? curyUnprepaidTotal = row.CuryUnprepaidTotal;
      Decimal num2 = 0M;
      num1 = curyUnprepaidTotal.GetValueOrDefault() > num2 & curyUnprepaidTotal.HasValue ? 1 : 0;
    }
    ((PXAction) createPrepayment).SetEnabled(num1 != 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<POOrderPrepayment.statusText> e)
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null || e.Row == null)
      return;
    PXSelectJoinGroupBy<POOrderPrepayment, InnerJoin<PX.Objects.AP.APRegister, On<PX.Objects.AP.APRegister.docType, Equal<POOrderPrepayment.aPDocType>, And<PX.Objects.AP.APRegister.refNbr, Equal<POOrderPrepayment.aPRefNbr>>>>, Where<POOrderPrepayment.orderType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<POOrderPrepayment.orderNbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>, Aggregate<Sum<POOrderPrepayment.curyAppliedAmt, Sum<PX.Objects.AP.APRegister.curyDocBal>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<POOrderPrepayment, InnerJoin<PX.Objects.AP.APRegister, On<PX.Objects.AP.APRegister.docType, Equal<POOrderPrepayment.aPDocType>, And<PX.Objects.AP.APRegister.refNbr, Equal<POOrderPrepayment.aPRefNbr>>>>, Where<POOrderPrepayment.orderType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<POOrderPrepayment.orderNbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>, Aggregate<Sum<POOrderPrepayment.curyAppliedAmt, Sum<PX.Objects.AP.APRegister.curyDocBal>>>>((PXGraph) this.Base);
    using (new PXFieldScope(((PXSelectBase) selectJoinGroupBy).View, new Type[8]
    {
      typeof (POOrderPrepayment.orderType),
      typeof (POOrderPrepayment.orderNbr),
      typeof (POOrderPrepayment.aPDocType),
      typeof (POOrderPrepayment.aPRefNbr),
      typeof (PX.Objects.AP.APRegister.docType),
      typeof (PX.Objects.AP.APRegister.refNbr),
      typeof (POOrderPrepayment.curyAppliedAmt),
      typeof (PX.Objects.AP.APRegister.curyDocBal)
    }))
    {
      PXResult<POOrderPrepayment, PX.Objects.AP.APRegister> pxResult = (PXResult<POOrderPrepayment, PX.Objects.AP.APRegister>) PXResultset<POOrderPrepayment>.op_Implicit(((PXSelectBase<POOrderPrepayment>) selectJoinGroupBy).SelectWindowed(0, 1, Array.Empty<object>()));
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<POOrderPrepayment.statusText>>) e).ReturnValue = (object) PXMessages.LocalizeFormatNoPrefix("Total Applied to Order Amount {0}, Total Balance Amount {1}", new object[2]
      {
        (object) this.Base.FormatQty(new Decimal?(((Decimal?) PXResult<POOrderPrepayment, PX.Objects.AP.APRegister>.op_Implicit(pxResult)?.CuryAppliedAmt).GetValueOrDefault())),
        (object) this.Base.FormatQty(new Decimal?(((Decimal?) PXResult<POOrderPrepayment, PX.Objects.AP.APRegister>.op_Implicit(pxResult)?.CuryDocBal).GetValueOrDefault()))
      });
    }
  }
}
