// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ARInvoiceEntryASC606
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.DR;

public class ARInvoiceEntryASC606 : PXGraphExtension<ARInvoiceEntry>
{
  public PXAction<PX.Objects.AR.ARInvoice> viewSchedule;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.aSC606>();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    ARTran current = ((PXSelectBase<ARTran>) this.Base.Transactions).Current;
    if (current != null && ((PXSelectBase) this.Base.Transactions).Cache.GetStatus((object) current) == null)
    {
      this.ValidateTransactions();
      ((PXAction) this.Base.Save).Press();
      ARInvoiceEntryASC606.ViewScheduleForDocument((PXGraph) this.Base, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current);
    }
    return adapter.Get();
  }

  protected void ValidateTransactions()
  {
    foreach (PXResult<ARTran> pxResult in ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      ARTran arTran = PXResult<ARTran>.op_Implicit(pxResult);
      object deferredCode = (object) arTran.DeferredCode;
      try
      {
        ((PXSelectBase) this.Base.Transactions).Cache.RaiseFieldVerifying<ARTran.deferredCode>((object) arTran, ref deferredCode);
      }
      catch (PXSetPropertyException ex)
      {
        ((PXSelectBase) this.Base.Transactions).Cache.RaiseExceptionHandling<ARTran.deferredCode>((object) arTran, deferredCode, (Exception) ex);
        throw ex;
      }
    }
  }

  public static void ViewScheduleForDocument(PXGraph graph, PX.Objects.AR.ARInvoice document)
  {
    PXSelectBase<DRSchedule> pxSelectBase = (PXSelectBase<DRSchedule>) new PXSelect<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<Required<ARTran.tranType>>, And<DRSchedule.refNbr, Equal<Required<ARTran.refNbr>>>>>>(graph);
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(pxSelectBase.Select(new object[2]
    {
      (object) document.DocType,
      (object) document.RefNbr
    }));
    int? nullable1;
    bool? nullable2;
    if (drSchedule != null)
    {
      nullable1 = drSchedule.LineNbr;
      if (nullable1.HasValue)
      {
        nullable2 = document.Released;
        bool flag = false;
        if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
          throw new PXException("The operation cannot be completed because the Revenue Recognition by IFRS 15/ASC606 feature is enabled. To continue, re-configure the system as described in the Recognition of Revenue from Customer Contracts section in Help or do one of the following:~- Delete the DR codes and DR schedules generated before the feature was enabled.~- Disable the feature.");
      }
    }
    int num1;
    if (drSchedule == null)
    {
      num1 = 1;
    }
    else
    {
      nullable2 = drSchedule.IsOverridden;
      num1 = !nullable2.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0)
    {
      nullable2 = document.Released;
      bool flag = false;
      if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
      {
        ARReleaseProcess.Amount netAmount = ASC606Helper.CalculateNetAmount(graph, (ARRegister) document);
        ARTran arTran = PXResultset<ARTran>.op_Implicit(((PXSelectBase<ARTran>) new PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.defScheduleID, IsNotNull>>>>(graph)).SelectWindowed(0, 1, new object[2]
        {
          (object) document.DocType,
          (object) document.RefNbr
        }));
        int? nullable3;
        if (arTran == null)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = arTran.DefScheduleID;
        int? defScheduleID = nullable3;
        Decimal? cury = netAmount.Cury;
        Decimal num2 = 0M;
        if (!(cury.GetValueOrDefault() == num2 & cury.HasValue))
        {
          DRSingleProcess instance = PXGraph.CreateInstance<DRSingleProcess>();
          instance.CreateSingleSchedule(document, netAmount, defScheduleID, true);
          ((PXGraph) instance).Actions.PressSave();
          ((PXSelectBase) pxSelectBase).Cache.Clear();
          ((PXSelectBase) pxSelectBase).Cache.ClearQueryCacheObsolete();
          ((PXSelectBase) pxSelectBase).View.Clear();
          drSchedule = PXResultset<DRSchedule>.op_Implicit(pxSelectBase.Select(new object[2]
          {
            (object) document.DocType,
            (object) document.RefNbr
          }));
        }
      }
    }
    if (drSchedule == null)
      throw new PXException("Deferral schedule does not exist for the document.");
    PXRedirectHelper.TryRedirect(graph.Caches[typeof (DRSchedule)], (object) drSchedule, "View Schedule", (PXRedirectHelper.WindowMode) 3);
  }

  [PXOverride]
  public virtual void ReverseDRSchedule(
    ARRegister doc,
    ARTran tran,
    ARInvoiceEntryASC606.ReverseDRScheduleDelegate baseDelegate)
  {
    if (string.IsNullOrEmpty(tran.DeferredCode))
      return;
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<Required<DRSchedule.docType>>, And<DRSchedule.refNbr, Equal<Required<DRSchedule.refNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) doc.DocType,
      (object) doc.RefNbr,
      (object) tran.LineNbr
    }));
    if (drSchedule == null)
      return;
    tran.DefScheduleID = drSchedule.ScheduleID;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ARTran, ARTran.deferredCode> e)
  {
    if (e.Row != null && !e.Row.InventoryID.HasValue && ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARTran, ARTran.deferredCode>, ARTran, object>) e).NewValue != null)
      throw new PXSetPropertyException("Inventory ID cannot be empty.");
  }

  public delegate void ReverseDRScheduleDelegate(ARRegister doc, ARTran tran);
}
