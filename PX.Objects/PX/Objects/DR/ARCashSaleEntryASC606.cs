// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ARCashSaleEntryASC606
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.DR;

public class ARCashSaleEntryASC606 : PXGraphExtension<ARCashSaleEntry>
{
  public PXAction<ARCashSale> viewSchedule;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.aSC606>();

  [PXUIField]
  [PXLookupButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    ARTran current = ((PXSelectBase<ARTran>) this.Base.Transactions).Current;
    if (current != null && ((PXSelectBase) this.Base.Transactions).Cache.GetStatus((object) current) == null)
    {
      this.ValidateTransactions();
      ((PXAction) this.Base.Save).Press();
      ARCashSaleEntryASC606.ViewScheduleForDocument((PXGraph) this.Base, ((PXSelectBase<ARCashSale>) this.Base.Document).Current);
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

  public static void ViewScheduleForDocument(PXGraph graph, ARCashSale document)
  {
    PXSelectBase<DRSchedule> pxSelectBase = (PXSelectBase<DRSchedule>) new PXSelect<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<Required<ARTran.tranType>>, And<DRSchedule.refNbr, Equal<Required<ARTran.refNbr>>>>>>(graph);
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(pxSelectBase.Select(new object[2]
    {
      (object) document.DocType,
      (object) document.RefNbr
    }));
    bool? nullable;
    if (drSchedule != null && drSchedule.LineNbr.HasValue)
    {
      nullable = document.Released;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        throw new PXException("The operation cannot be completed because the Revenue Recognition by IFRS 15/ASC606 feature is enabled. To continue, re-configure the system as described in the Recognition of Revenue from Customer Contracts section in Help or do one of the following:~- Delete the DR codes and DR schedules generated before the feature was enabled.~- Disable the feature.");
    }
    int num1;
    if (drSchedule == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = drSchedule.IsOverridden;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0)
    {
      nullable = document.Released;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        ARReleaseProcess.Amount netAmount = ASC606Helper.CalculateNetAmount(graph, (PX.Objects.AR.ARRegister) document);
        int? defScheduleID = new int?();
        Decimal? cury = netAmount.Cury;
        Decimal num2 = 0M;
        if (!(cury.GetValueOrDefault() == num2 & cury.HasValue))
        {
          DRSingleProcess instance = PXGraph.CreateInstance<DRSingleProcess>();
          instance.CreateSingleSchedule(document, netAmount, defScheduleID, true);
          ((PXGraph) instance).Actions.PressSave();
          ((PXSelectBase) pxSelectBase).Cache.Clear();
          ((PXSelectBase) pxSelectBase).Cache.ClearQueryCacheObsolete();
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

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ARTran, ARTran.deferredCode> e)
  {
    if (e.Row != null && !e.Row.InventoryID.HasValue && ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARTran, ARTran.deferredCode>, ARTran, object>) e).NewValue != null)
      throw new PXSetPropertyException("Inventory ID cannot be empty.");
  }
}
