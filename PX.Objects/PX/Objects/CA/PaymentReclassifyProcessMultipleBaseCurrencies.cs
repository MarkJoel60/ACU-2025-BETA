// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentReclassifyProcessMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA;

public class PaymentReclassifyProcessMultipleBaseCurrencies : 
  PXGraphExtension<PaymentReclassifyProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter.branchID> e)
  {
    if (!(e.Row is PaymentReclassifyProcess.Filter row))
      return;
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<PaymentReclassifyProcess.Filter.branchID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter.branchID>>) e).Cache, (object) row) as PX.Objects.GL.Branch;
    if (!(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter.branchID>>) e).Cache.GetValueExt<PaymentReclassifyProcess.Filter.cashAccountID>(e.Row) is PXFieldState valueExt) || !(PXSelectorAttribute.Select<PaymentReclassifyProcess.Filter.cashAccountID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter.branchID>>) e).Cache, (object) row, valueExt.Value) is CashAccount cashAccount) || !(branch?.BaseCuryID != cashAccount.BaseCuryID) && !cashAccount.RestrictVisibilityWithBranch.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter.branchID>>) e).Cache.SetValue<PaymentReclassifyProcess.Filter.cashAccountID>((object) row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter.branchID>>) e).Cache.SetValueExt<PaymentReclassifyProcess.Filter.cashAccountID>((object) row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter.branchID>>) e).Cache.SetValuePending<PaymentReclassifyProcess.Filter.cashAccountID>((object) row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter.branchID>>) e).Cache.RaiseExceptionHandling<PaymentReclassifyProcess.Filter.cashAccountID>((object) row, (object) null, (Exception) null);
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<BAccountR.baseCuryID, EqualBaseCuryID<Current<PaymentReclassifyProcess.Filter.branchID>>>), "", new System.Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<CASplitExt.referenceID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<CASplitExt> e)
  {
    CASplitExt row = e.Row;
    if (row == null || !row.ReferenceID.HasValue || PXSelectorAttribute.Select<CASplitExt.referenceID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CASplitExt>>) e).Cache, (object) row) is BAccountR)
      return;
    BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectReadonly<BAccountR, Where<BAccountR.bAccountID, Equal<Required<CASplitExt.referenceID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.ReferenceID
    }));
    object obj = baccountR == null ? (object) row.ReferenceID : (object) baccountR.AcctCD;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CASplitExt>>) e).Cache.RaiseExceptionHandling<CASplitExt.referenceID>((object) row, obj, (Exception) new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.referenceID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CASplitExt>>) e).Cache)
    }));
  }
}
