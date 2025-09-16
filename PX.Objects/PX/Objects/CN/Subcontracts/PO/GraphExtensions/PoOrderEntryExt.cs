// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.GraphExtensions.PoOrderEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.PO.DAC;
using PX.Objects.CN.Subcontracts.PO.Descriptor.Attributes;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.SO;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.GraphExtensions;

public class PoOrderEntryExt : PXGraphExtension<POOrderEntry>
{
  public PXFilter<PurchaseOrderTypeFilter> TypeFilter;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.ApplyBaseTypeFiltering();
  }

  [PXMergeAttributes]
  [PurchaseOrderTypeRestrictor]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.orderNbr> e)
  {
  }

  [PXMergeAttributes]
  [PurchaseOrderTypeRestrictor]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.pONbr> e)
  {
  }

  [PXMergeAttributes]
  [PurchaseOrderTypeRestrictor]
  protected virtual void _(PX.Data.Events.CacheAttached<SOLineSplit.pONbr> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PurchaseOrderTypeFilter.type1> args)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PurchaseOrderTypeFilter.type1>, object, object>) args).NewValue = this.IsSubcontractScreen() ? (object) "RS" : (object) "BL";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PurchaseOrderTypeFilter.type2> args)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PurchaseOrderTypeFilter.type2>, object, object>) args).NewValue = this.IsSubcontractScreen() ? (object) "RS" : (object) "DP";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PurchaseOrderTypeFilter.type3> args)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PurchaseOrderTypeFilter.type3>, object, object>) args).NewValue = this.IsSubcontractScreen() ? (object) "RS" : (object) "RO";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PurchaseOrderTypeFilter.type4> args)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PurchaseOrderTypeFilter.type4>, object, object>) args).NewValue = this.IsSubcontractScreen() ? (object) "RS" : (object) "SB";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PurchaseOrderTypeFilter.type5> args)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PurchaseOrderTypeFilter.type5>, object, object>) args).NewValue = this.IsSubcontractScreen() ? (object) "RS" : (object) "PD";
  }

  private void ApplyBaseTypeFiltering()
  {
    if (this.IsSubcontractScreen())
      this.AddSubcontractFilters();
    else
      this.AddPurchaseOrderFilters();
  }

  private void AddPurchaseOrderFilters()
  {
    ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).WhereAnd<Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>>();
    ((PXSelectBase<POSetupApproval>) this.Base.SetupApproval).WhereAnd<Where<POSetupApproval.orderType, NotEqual<POOrderType.regularSubcontract>>>();
  }

  private void AddSubcontractFilters()
  {
    ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).WhereAnd<Where<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.regularSubcontract>>>();
    ((PXSelectBase<POSetupApproval>) this.Base.SetupApproval).WhereAnd<Where<POSetupApproval.orderType, Equal<POOrderType.regularSubcontract>>>();
  }

  private bool IsSubcontractScreen()
  {
    return ((object) this.Base).GetType() == typeof (SubcontractEntry) || ((object) this.Base).GetType().BaseType == typeof (SubcontractEntry);
  }
}
