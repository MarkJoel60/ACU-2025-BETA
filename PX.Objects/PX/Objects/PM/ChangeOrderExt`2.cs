// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// This class implements graph extension to use common logic for change orders
/// </summary>
/// <typeparam name="TGraph">The entry <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
/// <typeparam name="TPrimary">The primary DAC (a <see cref="T:PX.Data.IBqlTable" /> type) of the <typeparam name="TGraph" />graph.</typeparam>
public abstract class ChangeOrderExt<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  [PXCopyPasteHiddenView]
  public PXSelect<ReversingChangeOrder, Where<ReversingChangeOrder.origRefNbr, Equal<Optional<PMChangeOrder.refNbr>>>> ReversingChangeOrders;
  public PXSetup<PMSetup> Setup;
  public PXAction<TPrimary> viewChangeOrder;
  public PXAction<PMProject> viewOrigChangeOrder;
  public PXAction<PMChangeOrder> viewCurrentReversingChangeOrder;
  public PXAction<PMChangeOrder> viewReversingChangeOrders;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.changeOrder>();

  public abstract PXSelectBase<PMChangeOrder> ChangeOrder { get; }

  public abstract PMChangeOrder CurrentChangeOrder { get; }

  [PXUIField]
  [PXButton(ImageKey = "Inquiry")]
  public virtual IEnumerable ViewChangeOrder(PXAdapter adapter)
  {
    if (this.CurrentChangeOrder != null && !string.IsNullOrEmpty(this.CurrentChangeOrder.RefNbr))
      ProjectAccountingService.NavigateToChangeOrderScreen(this.CurrentChangeOrder.RefNbr, "View Change Order");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewOrigChangeOrder(PXAdapter adapter)
  {
    if (this.CurrentChangeOrder != null && !string.IsNullOrEmpty(this.CurrentChangeOrder.OrigRefNbr))
      ProjectAccountingService.NavigateToChangeOrderScreen(this.CurrentChangeOrder.OrigRefNbr, "View Change Order");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewCurrentReversingChangeOrder(PXAdapter adapter)
  {
    if (((PXSelectBase<ReversingChangeOrder>) this.ReversingChangeOrders).Current != null && !string.IsNullOrEmpty(((PXSelectBase<ReversingChangeOrder>) this.ReversingChangeOrders).Current.OrigRefNbr))
      ProjectAccountingService.NavigateToChangeOrderScreen(((PXSelectBase<ReversingChangeOrder>) this.ReversingChangeOrders).Current.RefNbr, "View Reversing Change Order");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewReversingChangeOrders(PXAdapter adapter)
  {
    if (this.CurrentChangeOrder == null)
      return adapter.Get();
    string[] reversingOrderRefs = this.GetReversingOrderRefs(this.CurrentChangeOrder);
    if (reversingOrderRefs.Length == 0)
      return adapter.Get();
    if (reversingOrderRefs.Length == 1 && !string.IsNullOrEmpty(reversingOrderRefs[0]))
    {
      ProjectAccountingService.NavigateToChangeOrderScreen(reversingOrderRefs[0], "View Reversing Change Order");
    }
    else
    {
      this.ChangeOrder.Current = PXResultset<PMChangeOrder>.op_Implicit(this.ChangeOrder.Select(Array.Empty<object>()));
      ((PXSelectBase<ReversingChangeOrder>) this.ReversingChangeOrders).AskExt();
    }
    return adapter.Get();
  }

  protected virtual string[] GetReversingOrderRefs(PMChangeOrder changeOrder)
  {
    return GraphHelper.RowCast<ReversingChangeOrder>((IEnumerable) ((PXSelectBase<ReversingChangeOrder>) this.ReversingChangeOrders).Select(new object[1]
    {
      (object) changeOrder.RefNbr
    })).Select<ReversingChangeOrder, string>((Func<ReversingChangeOrder, string>) (x => x.RefNbr)).ToArray<string>();
  }

  protected virtual void _(
    Events.FieldSelecting<PMChangeOrder.reversingRefNbr> e)
  {
    if (!(e.Row is PMChangeOrder row))
      return;
    string[] reversingOrderRefs = this.GetReversingOrderRefs(row);
    ((Events.FieldSelectingBase<Events.FieldSelecting<PMChangeOrder.reversingRefNbr>>) e).ReturnValue = reversingOrderRefs.Length == 0 ? (object) (string) null : (reversingOrderRefs.Length == 1 ? (object) reversingOrderRefs[0] : (object) "<LIST>");
  }

  public virtual bool ChangeOrderEnabled() => this.ChangeOrderFeatureEnabled();

  public virtual bool ChangeOrderVisible()
  {
    return this.ChangeOrderFeatureEnabled() && !this.IsChangeOrderUserNumberingOn();
  }

  public virtual bool ChangeOrderFeatureEnabled() => ChangeOrderExt<TGraph, TPrimary>.IsActive();

  public bool IsChangeOrderUserNumberingOn()
  {
    return !string.IsNullOrWhiteSpace(((PXSelectBase<PMSetup>) this.Setup).Current?.ChangeOrderNumbering) && ((bool?) Numbering.PK.Find((PXGraph) this.Base, ((PXSelectBase<PMSetup>) this.Setup).Current?.ChangeOrderNumbering)?.UserNumbering).GetValueOrDefault();
  }
}
