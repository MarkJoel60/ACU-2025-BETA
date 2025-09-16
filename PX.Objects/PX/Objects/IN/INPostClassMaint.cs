// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPostClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PO.GraphExtensions.POReceiptEntryExt;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INPostClassMaint : PXGraph<INPostClassMaint, INPostClass>
{
  public PXSelect<INPostClass> postclass;
  public PXSelect<INPostClass, Where<INPostClass.postClassID, Equal<Current<INPostClass.postClassID>>>> postclassaccounts;
  public PXSetup<INSetup> insetup;

  public INPostClassMaint()
  {
    PXResultset<INSetup>.op_Implicit(((PXSelectBase<INSetup>) this.insetup).Select(Array.Empty<object>()));
    PXUIFieldAttribute.SetVisible<INPostClass.pPVAcctID>(((PXSelectBase) this.postclass).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INPostClass.pPVSubID>(((PXSelectBase) this.postclass).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INPostClass.pPVAcctDefault>(((PXSelectBase) this.postclass).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INPostClass.pPVSubMask>(((PXSelectBase) this.postclass).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INPostClass.correctionReasonCode>(((PXSelectBase) this.postclass).Cache, (object) null, !Correction.HideCorrectionUI);
    PXUIFieldAttribute.SetEnabled<INPostClass.correctionReasonCode>(((PXSelectBase) this.postclass).Cache, (object) null, !Correction.HideCorrectionUI);
  }

  protected virtual void INPostClass_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXCache pxCache = sender;
    object row = e.Row;
    int num;
    if (e.Row != null)
    {
      bool? cogsSubFromSales = ((INPostClass) e.Row).COGSSubFromSales;
      bool flag = false;
      num = cogsSubFromSales.GetValueOrDefault() == flag & cogsSubFromSales.HasValue ? 1 : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled<INPostClass.cOGSSubMask>(pxCache, row, num != 0);
    INAcctSubDefault.Required(sender, e);
  }

  protected virtual void INPostClass_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    INAcctSubDefault.Required(sender, e);
  }

  protected virtual void INPostClass_COGSSubFromSales_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateCOGSSubMask(sender, e);
  }

  protected virtual void INPostClass_SalesSubMask_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateCOGSSubMask(sender, e);
  }

  public virtual void UpdateCOGSSubMask(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is INPostClass row) || !row.COGSSubFromSales.GetValueOrDefault())
      return;
    sender.SetValueExt<INPostClass.cOGSSubMask>((object) row, (object) row.SalesSubMask);
  }
}
