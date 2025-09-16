// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.ProjectEntryMaterialManagementExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class ProjectEntryMaterialManagementExt : PXGraphExtension<ProjectEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.materialManagement>();

  public virtual void _(PX.Data.Events.FieldVerifying<PMProject.accountingMode> e)
  {
    this.ValidateAccountingModeChange(e.OldValue as string, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject.accountingMode>, object, object>) e).NewValue as string);
  }

  protected virtual void ValidateAccountingModeChange(string oldValue, string newValue)
  {
    if (oldValue != "L" && newValue == "L")
    {
      if (this.HasNoQtyOnHand() && this.HasNoNonClosedInventoryDocuments())
        return;
      this.RaiseError("You cannot change the inventory tracking method for the {0} project because either there are items with the project-specific costs on hand, or there is at least one non-closed inventory document related to the project.");
    }
    else if (oldValue == "L" && newValue != "L")
    {
      if (this.HasNoNonClosedInventoryDocuments() && this.HasNoLinkedInventoryLocations())
        return;
      this.RaiseError("You cannot change the inventory tracking method for the {0} project because either there is at least one warehouse location linked to the project, or at least one non-closed inventory document related to the project.");
    }
    else if (oldValue == "V" && newValue == "P")
    {
      if (this.HasNoQtyOnHand() && this.HasNoNonClosedInventoryDocuments())
        return;
      this.RaiseError("You cannot change the inventory tracking method for the {0} project because either there are project-related items on hand, or there is at least one non-closed inventory document related to the project.");
    }
    else
    {
      if (!(oldValue == "P") || !(newValue == "V") || this.HasNoNonClosedInventoryDocuments())
        return;
      this.RaiseError("You cannot change the inventory tracking method because there is at least one non-closed inventory document related to the {0} project.");
    }
  }

  protected virtual bool HasNoQtyOnHand()
  {
    INSiteStatusByCostCenter statusByCostCenter = PXResultset<INSiteStatusByCostCenter>.op_Implicit(PXSelectBase<INSiteStatusByCostCenter, PXViewOf<INSiteStatusByCostCenter>.BasedOn<SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCostCenter>.On<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<INCostCenter.costCenterID>>>>.Where<BqlOperand<INCostCenter.projectID, IBqlInt>.IsEqual<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>.Aggregate<To<GroupBy<INCostCenter.projectID>, Sum<INSiteStatusByCostCenter.qtyOnHand>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (statusByCostCenter == null)
      return true;
    Decimal? qtyOnHand = statusByCostCenter.QtyOnHand;
    Decimal num = 0M;
    return qtyOnHand.GetValueOrDefault() == num & qtyOnHand.HasValue;
  }

  protected virtual bool HasNoNonClosedInventoryDocuments()
  {
    return PXResultset<INPIHeader>.op_Implicit(PXSelectBase<INPIHeader, PXViewOf<INPIHeader>.BasedOn<SelectFromBase<INPIHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INPIHeader.status, IBqlString>.IsNotIn<INPIHdrStatus.completed, INPIHdrStatus.cancelled>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>())) == null && PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INTran.released, IBqlBool>.IsNotEqual<True>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>())) == null && PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLine>.On<PX.Objects.SO.SOLine.FK.Order>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.SO.SOOrder.status, IBqlString>.IsNotIn<SOOrderStatus.completed, SOOrderStatus.cancelled>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>())) == null && PXResultset<PX.Objects.PO.POOrder>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrder, PXViewOf<PX.Objects.PO.POOrder>.BasedOn<SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POLine>.On<PX.Objects.PO.POLine.FK.Order>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.PO.POOrder.status, IBqlString>.IsNotIn<POOrderStatus.closed, POOrderStatus.completed, POOrderStatus.cancelled>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>())) == null && PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<SOShipLine.FK.Shipment>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.SO.SOShipment.status, IBqlString>.IsNotIn<SOShipmentStatus.completed, SOShipmentStatus.cancelled>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>())) == null && PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLine.FK.Receipt>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.PO.POReceipt.status, IBqlString>.IsNotIn<POReceiptStatus.released, POReceiptStatus.canceled>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>())) == null;
  }

  protected virtual bool HasNoLinkedInventoryLocations()
  {
    return PXResultset<INLocation>.op_Implicit(PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLocation.projectID, IBqlInt>.IsEqual<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>())) == null;
  }

  protected virtual void RaiseError(string message)
  {
    PXCache cache = ((PXSelectBase) this.Base.Project).Cache;
    PXSetPropertyException<PMProject.accountingMode> propertyException = new PXSetPropertyException<PMProject.accountingMode>(message, (PXErrorLevel) 4);
    if (!cache.RaiseExceptionHandling<PMProject.accountingMode>(cache.Current, (object) false, (Exception) propertyException))
      throw propertyException;
  }
}
