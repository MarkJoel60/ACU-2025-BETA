// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderTypeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class SOOrderTypeMaint : PXGraph<SOOrderTypeMaint, SOOrderType>
{
  public PXSelectJoin<SOOrderType, LeftJoin<SOOrderTypeOperation, On2<SOOrderTypeOperation.FK.OrderType, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>>>>, Where2<Where2<Where<SOOrderTypeOperation.iNDocType, NotEqual<INTranType.transfer>, Or<FeatureInstalled<FeaturesSet.warehouse>>>, And<Where<SOOrderType.requireAllocation, NotEqual<True>, Or<AllocationAllowed>>>>, And<Where<SOOrderType.requireShipping, Equal<boolFalse>, Or<FeatureInstalled<FeaturesSet.inventory>>>>>> soordertype;
  public PXSelect<SOOrderType, Where<SOOrderType.orderType, Equal<Current<SOOrderType.orderType>>>> currentordertype;
  [PXCopyPasteHiddenView]
  public PXSelect<SOOrderTypeOperation, Where<SOOrderTypeOperation.orderType, Equal<Optional<SOOrderType.orderType>>>> operations;
  [PXCopyPasteHiddenView]
  public PXSelect<SOOrderTypeOperation, Where<SOOrderTypeOperation.orderType, Equal<Optional<SOOrderType.orderType>>, And<SOOrderTypeOperation.operation, Equal<Optional<SOOrderType.defaultOperation>>>>> defaultOperation;
  [PXCopyPasteHiddenView]
  public PXSelect<SOOrderType, Where<SOOrderType.template, Equal<Required<SOOrderType.orderType>>, And<SOOrderType.orderType, NotEqual<SOOrderType.template>>>> references;
  [PXCopyPasteHiddenView]
  public PXSelect<SOQuickProcessParameters, Where<SOQuickProcessParameters.orderType, Equal<Current<SOOrderType.orderType>>>> quickProcessPreset;

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    if (a.Searches.Length == 1)
    {
      PXResult<SOOrderType> pxResult = PXResultset<SOOrderType>.op_Implicit(PXSelectBase<SOOrderType, PXSelectJoin<SOOrderType, LeftJoin<SOOrderTypeOperation, On2<SOOrderTypeOperation.FK.OrderType, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>>>>, Where<SOOrderType.requireShipping, Equal<boolFalse>, Or<FeatureInstalled<FeaturesSet.inventory>>>>.Config>.Search<SOOrderType.orderType>((PXGraph) this, a.Searches[0], Array.Empty<object>()));
      if (pxResult != null && (!((PXSelectBase) this.soordertype).View.BqlSelect.Meet(((PXSelectBase) this.soordertype).Cache, (object) PXResult<SOOrderType>.op_Implicit(pxResult), Array.Empty<object>()) || !((PXSelectBase) this.soordertype).View.BqlSelect.Meet(((PXSelectBase) this.operations).Cache, (object) PXResult.Unwrap<SOOrderTypeOperation>((object) pxResult), Array.Empty<object>())))
        a.Searches[0] = (object) null;
    }
    return ((PXAction) new PXCancel<SOOrderType>((PXGraph) this, nameof (Cancel))).Press(a);
  }

  [PXCustomizeBaseAttribute(typeof (PXDefaultAttribute), "Constant", false)]
  [PXFormula(typeof (False))]
  protected void SOQuickProcessParameters_HideWhenNothingToPrint_CacheAttached(PXCache sender)
  {
  }

  public SOOrderTypeMaint()
  {
    ((PXSelectBase) this.operations).Cache.AllowInsert = ((PXSelectBase) this.operations).Cache.AllowDelete = false;
    PXGraph.FieldVerifyingEvents fieldVerifying1 = ((PXGraph) this).FieldVerifying;
    SOOrderTypeMaint soOrderTypeMaint1 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying1 = new PXFieldVerifying((object) soOrderTypeMaint1, __vmethodptr(soOrderTypeMaint1, SOOrderType_Mask_FieldVerifying));
    fieldVerifying1.AddHandler<SOOrderType.salesSubMask>(pxFieldVerifying1);
    PXGraph.FieldVerifyingEvents fieldVerifying2 = ((PXGraph) this).FieldVerifying;
    SOOrderTypeMaint soOrderTypeMaint2 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying2 = new PXFieldVerifying((object) soOrderTypeMaint2, __vmethodptr(soOrderTypeMaint2, SOOrderType_Mask_FieldVerifying));
    fieldVerifying2.AddHandler<SOOrderType.miscSubMask>(pxFieldVerifying2);
    PXGraph.FieldVerifyingEvents fieldVerifying3 = ((PXGraph) this).FieldVerifying;
    SOOrderTypeMaint soOrderTypeMaint3 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying3 = new PXFieldVerifying((object) soOrderTypeMaint3, __vmethodptr(soOrderTypeMaint3, SOOrderType_Mask_FieldVerifying));
    fieldVerifying3.AddHandler<SOOrderType.freightSubMask>(pxFieldVerifying3);
    PXGraph.FieldVerifyingEvents fieldVerifying4 = ((PXGraph) this).FieldVerifying;
    SOOrderTypeMaint soOrderTypeMaint4 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying4 = new PXFieldVerifying((object) soOrderTypeMaint4, __vmethodptr(soOrderTypeMaint4, SOOrderType_Mask_FieldVerifying));
    fieldVerifying4.AddHandler<SOOrderType.discSubMask>(pxFieldVerifying4);
  }

  protected virtual void SOOrderType_Mask_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOOrderType row = (SOOrderType) e.Row;
    if (row != null && (row.Active.GetValueOrDefault() && !(row.Behavior == "BL") || e.NewValue != null))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrderType_Template_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    SOOrderType row = (SOOrderType) e.Row;
    if (row == null || sender.GetStatus((object) row) != 2 || !(row.OrderType == (string) e.NewValue))
      return;
    e.NewValue = (object) null;
  }

  private void HandleQuickProcessPreset(SOOrderType ordertype, SOOrderType prevtype)
  {
    bool? allowQuickProcess1 = ordertype.AllowQuickProcess;
    bool? allowQuickProcess2 = prevtype.AllowQuickProcess;
    if (allowQuickProcess1.GetValueOrDefault() == allowQuickProcess2.GetValueOrDefault() & allowQuickProcess1.HasValue == allowQuickProcess2.HasValue && !(ordertype.Behavior != prevtype.Behavior) && !(ordertype.ARDocType != prevtype.ARDocType))
      return;
    ((PXSelectBase<SOQuickProcessParameters>) this.quickProcessPreset).DeleteCurrent();
    if (!ordertype.AllowQuickProcess.GetValueOrDefault())
      return;
    ((PXSelectBase<SOQuickProcessParameters>) this.quickProcessPreset).Insert();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOOrderType, SOOrderType.behavior> e)
  {
    if (e.Row == null || !EnumerableExtensions.IsNotIn<string>(e.Row.Behavior, "SO", "TR", "IN", "CM", "MO", Array.Empty<string>()))
      return;
    e.Row.AllowQuickProcess = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.RowInserted<SOOrderType> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase<SOQuickProcessParameters>) this.quickProcessPreset).DeleteCurrent();
    if (!e.Row.AllowQuickProcess.GetValueOrDefault())
      return;
    ((PXSelectBase<SOQuickProcessParameters>) this.quickProcessPreset).Insert();
  }

  protected virtual void SOOrderType_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    SOOrderType row = (SOOrderType) e.Row;
    SOOrderType oldRow = (SOOrderType) e.OldRow;
    this.HandleQuickProcessPreset(row, oldRow);
    bool? nullable = row.Active;
    if (nullable.GetValueOrDefault())
    {
      if (oldRow.SalesSubMask == null && row.SalesSubMask == null)
        sender.SetDefaultExt<SOOrderType.salesSubMask>((object) row);
      if (oldRow.MiscSubMask == null && row.MiscSubMask == null)
        sender.SetDefaultExt<SOOrderType.miscSubMask>((object) row);
      if (oldRow.FreightSubMask == null && row.FreightSubMask == null)
        sender.SetDefaultExt<SOOrderType.freightSubMask>((object) row);
      if (oldRow.DiscSubMask == null && row.DiscSubMask == null)
        sender.SetDefaultExt<SOOrderType.discSubMask>((object) row);
    }
    nullable = row.CustomerOrderIsRequired;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      row.CustomerOrderValidation = "N";
    if (row.Template != null && row.Template != row.OrderType && row.Template != ((SOOrderType) e.OldRow).Template)
    {
      SOOrderType soOrderType = PXResultset<SOOrderType>.op_Implicit(PXSelectBase<SOOrderType, PXSelect<SOOrderType, Where<SOOrderType.orderType, Equal<Required<SOOrderType.orderType>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.Template
      }));
      ((PXSelectBase<SOOrderType>) this.soordertype).Current = row;
      if (soOrderType == null)
        return;
      row.Behavior = soOrderType.Behavior;
      row.DefaultOperation = soOrderType.DefaultOperation;
      row.RequireShipping = soOrderType.RequireShipping;
      row.RequireAllocation = soOrderType.RequireAllocation;
      row.ARDocType = soOrderType.ARDocType;
      row.INDocType = soOrderType.INDocType;
      row.ShipmentPlanType = soOrderType.ShipmentPlanType;
      row.OrderPlanType = soOrderType.OrderPlanType;
      PXView view1 = ((PXSelectBase) this.operations).View;
      object[] objArray1 = new object[1]{ (object) row };
      object[] objArray2 = Array.Empty<object>();
      foreach (SOOrderTypeOperation orderTypeOperation in view1.SelectMultiBound(objArray1, objArray2))
        ((PXSelectBase<SOOrderTypeOperation>) this.operations).Delete(orderTypeOperation);
      PXView view2 = ((PXSelectBase) this.operations).View;
      object[] objArray3 = new object[1]
      {
        (object) soOrderType
      };
      object[] objArray4 = Array.Empty<object>();
      foreach (SOOrderTypeOperation orderTypeOperation in view2.SelectMultiBound(objArray3, objArray4))
        ((PXSelectBase<SOOrderTypeOperation>) this.operations).Insert(new SOOrderTypeOperation()
        {
          OrderType = row.OrderType,
          Operation = orderTypeOperation.Operation,
          INDocType = orderTypeOperation.INDocType,
          ShipmentPlanType = orderTypeOperation.ShipmentPlanType,
          OrderPlanType = orderTypeOperation.OrderPlanType,
          AutoCreateIssueLine = orderTypeOperation.AutoCreateIssueLine,
          Active = orderTypeOperation.Active
        });
    }
    if (!sender.ObjectsEqual<SOOrderType.behavior>(e.Row, e.OldRow))
    {
      if (row.Behavior == "BL")
      {
        row.ARDocType = "UND";
        row.INDocType = "UND";
        row.AllowQuickProcess = new bool?(false);
        row.RequireLotSerial = new bool?(false);
        row.CalculateFreight = new bool?(false);
        row.ShipFullIfNegQtyAllowed = new bool?(false);
        row.RecalculateDiscOnPartialShipment = new bool?(false);
        row.ShipSeparately = new bool?(false);
        row.CopyNotes = new bool?(false);
        row.CopyFiles = new bool?(false);
        row.CopyHeaderNotesToShipment = new bool?(false);
        row.CopyHeaderFilesToShipment = new bool?(false);
        row.CopyHeaderNotesToInvoice = new bool?(false);
        row.CopyHeaderFilesToInvoice = new bool?(false);
        row.CopyLineNotesToShipment = new bool?(false);
        row.CopyLineFilesToShipment = new bool?(false);
        row.CopyLineNotesToInvoice = new bool?(false);
        row.CopyLineFilesToInvoice = new bool?(false);
        row.CopyLineNotesToInvoiceOnlyNS = new bool?(false);
        row.CopyLineFilesToInvoiceOnlyNS = new bool?(false);
      }
      else if (row.Behavior == "TR")
        row.ARDocType = "UND";
      else if (row.Behavior == "MO")
      {
        row.ARDocType = "INC";
        row.BillSeparately = new bool?(true);
      }
      else if (oldRow.Behavior == "MO")
        row.ARDocType = "INV";
    }
    if ((row.Template == null || row.Behavior == "BL") && !sender.ObjectsEqual<SOOrderType.behavior, SOOrderType.aRDocType>(e.Row, e.OldRow))
    {
      PXView view = ((PXSelectBase) this.operations).View;
      object[] objArray5 = new object[1]{ (object) row };
      object[] objArray6 = Array.Empty<object>();
      foreach (SOOrderTypeOperation orderTypeOperation in view.SelectMultiBound(objArray5, objArray6))
        ((PXSelectBase<SOOrderTypeOperation>) this.operations).Delete(orderTypeOperation);
      string str = SOBehavior.DefaultOperation(row.Behavior, row.ARDocType);
      if (str != null)
      {
        row.DefaultOperation = str;
        SOOrderTypeOperation orderTypeOperation = new SOOrderTypeOperation();
        orderTypeOperation.OrderType = row.OrderType;
        orderTypeOperation.Operation = row.DefaultOperation;
        if (row.Behavior == "BL")
          orderTypeOperation.INDocType = "UND";
        ((PXSelectBase<SOOrderTypeOperation>) this.operations).Insert(orderTypeOperation);
        if (EnumerableExtensions.IsIn<string>(row.Behavior, "RM", "MO"))
          ((PXSelectBase<SOOrderTypeOperation>) this.operations).Insert(new SOOrderTypeOperation()
          {
            OrderType = row.OrderType,
            Operation = str == "I" ? "R" : "I"
          });
      }
    }
    nullable = row.RequireShipping;
    bool? requireShipping = oldRow.RequireShipping;
    if (nullable.GetValueOrDefault() == requireShipping.GetValueOrDefault() & nullable.HasValue == requireShipping.HasValue)
    {
      bool? requireAllocation = row.RequireAllocation;
      nullable = oldRow.RequireAllocation;
      if (requireAllocation.GetValueOrDefault() == nullable.GetValueOrDefault() & requireAllocation.HasValue == nullable.HasValue)
        goto label_54;
    }
    PXView view3 = ((PXSelectBase) this.operations).View;
    object[] objArray7 = new object[1]{ (object) row };
    object[] objArray8 = Array.Empty<object>();
    foreach (SOOrderTypeOperation orderTypeOperation in view3.SelectMultiBound(objArray7, objArray8))
      GraphHelper.MarkUpdated(((PXSelectBase) this.operations).Cache, (object) orderTypeOperation);
label_54:
    if (!sender.ObjectsEqual<SOOrderType.aRDocType, SOOrderType.requireShipping, SOOrderType.activeOperationsCntr>((object) row, (object) oldRow))
    {
      if (row.Behavior == "RM")
      {
        int? activeOperationsCntr = row.ActiveOperationsCntr;
        int num = 2;
        if (activeOperationsCntr.GetValueOrDefault() < num & activeOperationsCntr.HasValue && row.ARDocType == "UND" || !row.RequireShipping.GetValueOrDefault())
          row.UseShippedNotInvoiced = new bool?(false);
      }
      else if (row.ARDocType == "UND" || !row.RequireShipping.GetValueOrDefault())
        row.UseShippedNotInvoiced = new bool?(false);
    }
    object obj1;
    sender.RaiseFieldDefaulting<SOOrderType.allowRefundBeforeReturn>(e.OldRow, ref obj1);
    object obj2;
    sender.RaiseFieldDefaulting<SOOrderType.allowRefundBeforeReturn>(e.Row, ref obj2);
    if (!obj2.Equals(obj1))
      row.AllowRefundBeforeReturn = (bool?) obj2;
    row.CanHavePayments = (bool?) PXFormulaAttribute.Evaluate<SOOrderType.canHavePayments>(sender, (object) row);
    row.CanHaveRefunds = (bool?) PXFormulaAttribute.Evaluate<SOOrderType.canHaveRefunds>(sender, (object) row);
    SOOrderTypeOperation typeOperation = PXResultset<SOOrderTypeOperation>.op_Implicit(((PXSelectBase<SOOrderTypeOperation>) this.defaultOperation).Select(new object[2]
    {
      (object) row.OrderType,
      (object) row.DefaultOperation
    }));
    if (this.CanAuthRemainder(row, typeOperation))
      return;
    sender.SetDefaultExt<SOOrderType.authorizeRemainderAfterPartialCapture>(e.Row);
  }

  protected virtual void SOOrderType_DefaultOperation_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrderType row))
      return;
    SOOrderTypeOperation orderTypeOperation = PXResultset<SOOrderTypeOperation>.op_Implicit(((PXSelectBase<SOOrderTypeOperation>) this.defaultOperation).Select(new object[2]
    {
      (object) row.OrderType,
      (object) row.DefaultOperation
    }));
    if (orderTypeOperation == null)
      return;
    sender.SetValueExt<SOOrderType.iNDocType>((object) row, (object) orderTypeOperation.INDocType);
  }

  protected virtual void SOOrderTypeOperation_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SOOrderTypeOperation row = (SOOrderTypeOperation) e.Row;
    SOOrderType current = ((PXSelectBase<SOOrderType>) this.soordertype).Current;
    if (row == null || current == null || row.Operation != current.DefaultOperation)
      return;
    current.INDocType = row.INDocType;
    current.OrderPlanType = row.OrderPlanType;
    current.ShipmentPlanType = row.ShipmentPlanType;
    if (row.INDocType == "UND")
    {
      row.OrderPlanType = (string) null;
      row.ShipmentPlanType = (string) null;
      current.RequireShipping = new bool?(false);
      current.RequireAllocation = new bool?(false);
    }
    if (row.INDocType == "TRX")
    {
      current.RequireShipping = new bool?(true);
      current.ARDocType = "UND";
    }
    if (row.INDocType == "UND")
    {
      sender.SetValue<SOOrderTypeOperation.orderPlanType>(e.Row, (object) null);
      sender.SetValue<SOOrderTypeOperation.shipmentPlanType>(e.Row, (object) null);
    }
    if (row.INDocType != null && !(row.INDocType == "TRX"))
      return;
    ((PXSelectBase) this.soordertype).Cache.SetValue<SOOrderType.customerOrderIsRequired>((object) current, (object) false);
    ((PXSelectBase) this.soordertype).Cache.SetValue<SOOrderType.customerOrderValidation>((object) current, (object) "N");
  }

  protected virtual void SOOrderTypeOperation_INDocType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOOrderTypeOperation row = (SOOrderTypeOperation) e.Row;
    if (row != null)
    {
      short? nullable1 = INTranType.InvtMult((string) e.NewValue);
      short? nullable2;
      int? nullable3;
      if (row.Operation == "I")
      {
        nullable2 = nullable1;
        nullable3 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        int num = 0;
        if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
          goto label_6;
      }
      if (row.Operation == "R")
      {
        nullable2 = nullable1;
        nullable3 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        int num = 0;
        if (nullable3.GetValueOrDefault() < num & nullable3.HasValue)
          goto label_6;
      }
      SOOrderType current = ((PXSelectBase<SOOrderType>) this.currentordertype).Current;
      if (!((string) e.NewValue == "UND") || current == null || !EnumerableExtensions.IsIn<string>(current.Behavior, "SO", "TR", "RM"))
        return;
      throw new PXSetPropertyException("This type of inventory transaction cannot be used if the Process Shipments check box is selected.");
label_6:
      throw new PXSetPropertyException("Selected Inventory Transaction Type is not supported for this type operation.");
    }
  }

  protected virtual void SOOrderTypeOperation_InvtMult_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is SOOrderTypeOperation row))
      return;
    e.NewValue = (object) INTranType.InvtMult(row.INDocType);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrderTypeOperation_Active_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrderTypeOperation row) || !EnumerableExtensions.IsIn<string>(((PXSelectBase<SOOrderType>) this.currentordertype).Current.Behavior, "RM", "IN", "MO"))
      return;
    if (row.Operation == "I")
    {
      bool? active = row.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
      {
        foreach (PXResult<SOOrderTypeOperation> pxResult in ((PXSelectBase<SOOrderTypeOperation>) this.operations).Select(Array.Empty<object>()))
        {
          SOOrderTypeOperation orderTypeOperation = PXResult<SOOrderTypeOperation>.op_Implicit(pxResult);
          if (orderTypeOperation.Operation == "R")
          {
            orderTypeOperation.AutoCreateIssueLine = new bool?(false);
            ((PXSelectBase<SOOrderTypeOperation>) this.operations).Update(orderTypeOperation);
          }
        }
      }
    }
    ((PXSelectBase) this.operations).View.RequestRefresh();
  }

  protected virtual void SOOrderTypeOperation_INDocType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is SOOrderTypeOperation) || PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return;
    e.NewValue = (object) "UND";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrderType_INDocType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is SOOrderType) || PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return;
    e.NewValue = (object) "UND";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrderType_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    SOOrderType ordertype = (SOOrderType) e.Row;
    SOOrderType soOrderType1 = PXResultset<SOOrderType>.op_Implicit(((PXSelectBase<SOOrderType>) this.references).SelectWindowed(0, 1, new object[1]
    {
      (object) ordertype.OrderType
    }));
    bool? isSystem = ordertype.IsSystem;
    bool flag1 = false;
    bool flag2 = isSystem.GetValueOrDefault() == flag1 & isSystem.HasValue && soOrderType1 == null && ordertype.OrderType != null;
    bool isBlanket = ordertype.Behavior == "BL";
    bool isTransfer = ordertype.Behavior == "TR";
    bool isQuote = ordertype.Behavior == "QT";
    bool flag3 = EnumerableExtensions.IsIn<string>(ordertype.Behavior, "IN", "CM", "MO");
    bool isMixedOrder = ordertype.Behavior == "MO";
    PXUIFieldAttribute.SetEnabled<SOOrderType.template>(sender, e.Row, flag2);
    SOOrderTypeOperation typeOperation = PXResultset<SOOrderTypeOperation>.op_Implicit(((PXSelectBase<SOOrderTypeOperation>) this.defaultOperation).Select(new object[2]
    {
      (object) ordertype.OrderType,
      (object) ordertype.DefaultOperation
    })) ?? new SOOrderTypeOperation();
    PXUIFieldAttribute.SetEnabled<SOOrderType.billSeparately>(sender, e.Row, ordertype.ARDocType != "UND" && !isMixedOrder);
    PXUIFieldAttribute.SetEnabled<SOOrderType.invoiceNumberingID>(sender, e.Row, ordertype.ARDocType != "UND");
    if (ordertype.ARDocType == "UND")
    {
      INTranType.CustomListAttribute customListAttribute = (INTranType.CustomListAttribute) new INTranType.SONonARListAttribute();
      PXStringListAttribute.SetList<SOOrderTypeOperation.iNDocType>(((PXSelectBase) this.operations).Cache, (object) null, customListAttribute.AllowedValues, customListAttribute.AllowedLabels);
    }
    else
    {
      INTranType.CustomListAttribute customListAttribute = (INTranType.CustomListAttribute) new INTranType.SOListAttribute();
      PXStringListAttribute.SetList<SOOrderTypeOperation.iNDocType>(((PXSelectBase) this.operations).Cache, (object) null, customListAttribute.AllowedValues, customListAttribute.AllowedLabels);
    }
    if (isMixedOrder)
    {
      ARDocType.MixedOrderListAttribute orderListAttribute = new ARDocType.MixedOrderListAttribute();
      PXStringListAttribute.SetList<SOOrderType.aRDocType>(((PXSelectBase) this.soordertype).Cache, (object) ordertype, orderListAttribute.AllowedValues, orderListAttribute.AllowedLabels);
    }
    else
    {
      ARDocType.SOListAttribute soListAttribute = new ARDocType.SOListAttribute();
      PXStringListAttribute.SetList<SOOrderType.aRDocType>(((PXSelectBase) this.soordertype).Cache, (object) ordertype, soListAttribute.AllowedValues, soListAttribute.AllowedLabels);
    }
    PXUIFieldAttribute.SetEnabled<SOOrderType.requireShipping>(sender, e.Row, EnumerableExtensions.IsNotIn<string>(typeOperation.INDocType, "UND", "TRX") & flag2 && !SOBehavior.IsPredefinedBehavior(ordertype.Behavior));
    PXUIFieldAttribute.SetEnabled<SOOrderType.aRDocType>(sender, e.Row, ((!(typeOperation.INDocType != "TRX") ? 0 : (!isBlanket ? 1 : 0)) & (flag2 ? 1 : 0)) != 0);
    PXUIFieldAttribute.SetEnabled<SOOrderType.behavior>(sender, e.Row, flag2);
    PXUIFieldAttribute.SetEnabled<SOOrderType.defaultOperation>(sender, e.Row, flag2 && ordertype.Behavior == "RM");
    PXUIFieldAttribute.SetVisible<SOOrderTypeOperation.active>(((PXSelectBase) this.operations).Cache, (object) null, EnumerableExtensions.IsIn<string>(ordertype.Behavior, "RM", "IN", "MO"));
    PXUIFieldAttribute.SetVisible<SOOrderTypeOperation.autoCreateIssueLine>(((PXSelectBase) this.operations).Cache, (object) null, ordertype.Behavior == "RM");
    PXUIFieldAttribute.SetEnabled<SOOrderType.copyLineNotesToInvoiceOnlyNS>(sender, (object) ordertype, ordertype.CopyLineNotesToInvoice.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<SOOrderType.copyLineFilesToInvoiceOnlyNS>(sender, (object) ordertype, ordertype.CopyLineFilesToInvoice.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<SOOrderType.requireAllocation>(sender, (object) ordertype, ordertype.RequireShipping.GetValueOrDefault() | isBlanket);
    PXCache pxCache1 = sender;
    SOOrderType soOrderType2 = ordertype;
    bool? nullable = ordertype.RequireShipping;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOOrderType.requireLotSerial>(pxCache1, (object) soOrderType2, num1 != 0);
    PXCache pxCache2 = sender;
    SOOrderType soOrderType3 = ordertype;
    nullable = ordertype.RequireShipping;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOOrderType.copyLotSerialFromShipment>(pxCache2, (object) soOrderType3, num2 != 0);
    PXCache pxCache3 = sender;
    SOOrderType soOrderType4 = ordertype;
    nullable = ordertype.PostLineDiscSeparately;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOOrderType.useDiscountSubFromSalesSub>(pxCache3, (object) soOrderType4, num3 != 0);
    bool flag4 = typeOperation.INDocType != null && typeOperation.INDocType != "TRX";
    PXUIFieldAttribute.SetEnabled<SOOrderType.customerOrderIsRequired>(sender, (object) ordertype, flag4);
    PXCache pxCache4 = sender;
    SOOrderType soOrderType5 = ordertype;
    int num4;
    if (flag4)
    {
      nullable = ordertype.CustomerOrderIsRequired;
      num4 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<SOOrderType.customerOrderValidation>(pxCache4, (object) soOrderType5, num4 != 0);
    int num5;
    if (!(ordertype.Behavior == "RM"))
    {
      if (ordertype.ARDocType != "UND")
      {
        nullable = ordertype.RequireShipping;
        num5 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num5 = 0;
    }
    else
    {
      int? activeOperationsCntr = ordertype.ActiveOperationsCntr;
      int num6 = 1;
      if (activeOperationsCntr.GetValueOrDefault() > num6 & activeOperationsCntr.HasValue || ordertype.ARDocType != "UND")
      {
        nullable = ordertype.RequireShipping;
        num5 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num5 = 0;
    }
    bool flag5 = num5 != 0;
    PXUIFieldAttribute.SetEnabled<SOOrderType.useShippedNotInvoiced>(sender, e.Row, flag5);
    PXCache pxCache5 = sender;
    int num7;
    if (flag5)
    {
      nullable = ordertype.UseShippedNotInvoiced;
      num7 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num7 = 0;
    PXUIFieldAttribute.SetEnabled<SOOrderType.shippedNotInvoicedAcctID>(pxCache5, (object) null, num7 != 0);
    PXCache pxCache6 = sender;
    int num8;
    if (flag5)
    {
      nullable = ordertype.UseShippedNotInvoiced;
      num8 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num8 = 0;
    PXUIFieldAttribute.SetEnabled<SOOrderType.shippedNotInvoicedSubID>(pxCache6, (object) null, num8 != 0);
    PXUIFieldAttribute.SetVisible<SOOrderType.recalculateDiscOnPartialShipment>(sender, e.Row, PXAccess.FeatureInstalled<FeaturesSet.inventory>() && PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>());
    int num9;
    if (EnumerableExtensions.IsIn<string>(ordertype.Behavior, "SO", "RM"))
    {
      nullable = ordertype.RequireShipping;
      if (nullable.GetValueOrDefault())
      {
        num9 = ordertype.DefaultOperation == "I" ? 1 : 0;
        goto label_27;
      }
    }
    num9 = 0;
label_27:
    bool orchestrationAllowed = num9 != 0;
    PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row).For<SOOrderType.orchestrationStrategy>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Enabled = orchestrationAllowed;
      a.Visible = orchestrationAllowed;
    }));
    bool orchestrationEnabled = orchestrationAllowed && ordertype.OrchestrationStrategy != "NA";
    PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row).For<SOOrderType.limitWarehouse>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Enabled = orchestrationEnabled;
      a.Visible = orchestrationAllowed;
    }));
    PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row).For<SOOrderType.numberOfWarehouses>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Enabled = orchestrationEnabled && ordertype.LimitWarehouse.GetValueOrDefault();
      a.Visible = orchestrationAllowed;
    }));
    bool flag6 = PXAccess.FeatureInstalled<FeaturesSet.userDefinedOrderTypes>();
    ((PXSelectBase) this.soordertype).AllowInsert = flag6;
    ((PXSelectBase) this.currentordertype).AllowInsert = flag6;
    bool flag7 = flag6 || sender.GetStatus(e.Row) != 2;
    ((PXSelectBase) this.soordertype).AllowUpdate = flag7;
    ((PXSelectBase) this.currentordertype).AllowUpdate = flag7;
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>())
    {
      PXDefaultAttribute.SetDefault<SOOrderType.behavior>(sender, (object) "IN");
      PXStringListAttribute.SetList<SOOrderType.behavior>(sender, (object) null, new string[4]
      {
        "IN",
        "QT",
        "CM",
        "MO"
      }, new string[4]
      {
        "Invoice",
        "Quote",
        "Credit Memo",
        "Mixed Order"
      });
    }
    bool flag8 = this.CanAuthRemainder(ordertype, typeOperation);
    PXUIFieldAttribute.SetVisible<SOOrderType.authorizeRemainderAfterPartialCapture>(sender, e.Row, flag8);
    PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row).For<SOOrderType.intercompanySalesAcctDefault>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = !isTransfer && !isQuote && !isMixedOrder)).SameFor<SOOrderType.intercompanyCOGSAcctDefault>();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster;
    if (isTransfer)
    {
      attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row);
      attributeAdjuster.For<SOOrderType.disableAutomaticTaxCalculation>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = false));
    }
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained;
    if (isQuote)
    {
      attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row);
      chained = attributeAdjuster.For<SOOrderType.recalculateDiscOnPartialShipment>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = false));
      chained = chained.SameFor<SOOrderType.copyHeaderNotesToShipment>();
      chained = chained.SameFor<SOOrderType.copyHeaderFilesToShipment>();
      chained = chained.SameFor<SOOrderType.copyHeaderNotesToInvoice>();
      chained = chained.SameFor<SOOrderType.copyHeaderFilesToInvoice>();
      chained = chained.SameFor<SOOrderType.copyLineNotesToShipment>();
      chained = chained.SameFor<SOOrderType.copyLineFilesToShipment>();
      chained = chained.SameFor<SOOrderType.copyLineNotesToInvoice>();
      chained = chained.SameFor<SOOrderType.copyLineNotesToInvoiceOnlyNS>();
      chained = chained.SameFor<SOOrderType.copyLineFilesToInvoice>();
      chained.SameFor<SOOrderType.copyLineFilesToInvoiceOnlyNS>();
    }
    if (flag3)
    {
      attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row);
      chained = attributeAdjuster.For<SOOrderType.copyHeaderNotesToShipment>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = false));
      chained = chained.SameFor<SOOrderType.copyHeaderFilesToShipment>();
      chained = chained.SameFor<SOOrderType.copyLineNotesToShipment>();
      chained.SameFor<SOOrderType.copyLineFilesToShipment>();
    }
    if (isBlanket)
    {
      attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row);
      chained = attributeAdjuster.For<SOOrderType.creditHoldEntry>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = false));
      chained = chained.SameFor<SOOrderType.daysToKeep>();
      chained = chained.SameFor<SOOrderType.billSeparately>();
      chained = chained.SameFor<SOOrderType.shipSeparately>();
      chained = chained.SameFor<SOOrderType.calculateFreight>();
      chained = chained.SameFor<SOOrderType.shipFullIfNegQtyAllowed>();
      chained = chained.SameFor<SOOrderType.disableAutomaticDiscountCalculation>();
      chained = chained.SameFor<SOOrderType.disableAutomaticTaxCalculation>();
      chained = chained.SameFor<SOOrderType.recalculateDiscOnPartialShipment>();
      chained = chained.SameFor<SOOrderType.allowRefundBeforeReturn>();
      chained = chained.SameFor<SOOrderType.copyNotes>();
      chained = chained.SameFor<SOOrderType.copyFiles>();
      chained = chained.SameFor<SOOrderType.copyHeaderNotesToShipment>();
      chained = chained.SameFor<SOOrderType.copyHeaderFilesToShipment>();
      chained = chained.SameFor<SOOrderType.copyHeaderNotesToInvoice>();
      chained = chained.SameFor<SOOrderType.copyHeaderFilesToInvoice>();
      chained = chained.SameFor<SOOrderType.copyLineNotesToShipment>();
      chained = chained.SameFor<SOOrderType.copyLineFilesToShipment>();
      chained = chained.SameFor<SOOrderType.copyLineNotesToInvoice>();
      chained = chained.SameFor<SOOrderType.copyLineNotesToInvoiceOnlyNS>();
      chained = chained.SameFor<SOOrderType.copyLineFilesToInvoice>();
      chained = chained.SameFor<SOOrderType.copyLineFilesToInvoiceOnlyNS>();
      chained = chained.SameFor<SOOrderType.invoiceNumberingID>();
      chained = chained.SameFor<SOOrderType.markInvoicePrinted>();
      chained = chained.SameFor<SOOrderType.markInvoiceEmailed>();
      chained = chained.SameFor<SOOrderType.invoiceHoldEntry>();
      chained = chained.SameFor<SOOrderType.useCuryRateFromSO>();
      chained = chained.SameFor<SOOrderType.salesAcctDefault>();
      chained = chained.SameFor<SOOrderType.salesSubMask>();
      chained = chained.SameFor<SOOrderType.freightAcctID>();
      chained = chained.SameFor<SOOrderType.freightAcctDefault>();
      chained = chained.SameFor<SOOrderType.freightSubID>();
      chained = chained.SameFor<SOOrderType.freightSubMask>();
      chained = chained.SameFor<SOOrderType.discountAcctID>();
      chained = chained.SameFor<SOOrderType.discAcctDefault>();
      chained = chained.SameFor<SOOrderType.discountSubID>();
      chained = chained.SameFor<SOOrderType.discSubMask>();
      chained = chained.SameFor<SOOrderType.postLineDiscSeparately>();
      chained = chained.SameFor<SOOrderType.useDiscountSubFromSalesSub>();
      chained = chained.SameFor<SOOrderType.autoWriteOff>();
      chained = chained.SameFor<SOOrderType.useShippedNotInvoiced>();
      chained = chained.SameFor<SOOrderType.shippedNotInvoicedAcctID>();
      chained = chained.SameFor<SOOrderType.shippedNotInvoicedSubID>();
      chained = chained.SameFor<SOOrderType.intercompanySalesAcctDefault>();
      chained.SameFor<SOOrderType.intercompanyCOGSAcctDefault>();
    }
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row);
    chained = attributeAdjuster.For<SOOrderType.copyHeaderNotesToInvoice>((Action<PXUIFieldAttribute>) (a => a.Enabled = ordertype.ARDocType != "UND"));
    chained = chained.SameFor<SOOrderType.copyHeaderFilesToInvoice>();
    chained = chained.SameFor<SOOrderType.copyLineNotesToInvoice>();
    chained = chained.SameFor<SOOrderType.copyLineNotesToInvoiceOnlyNS>();
    chained = chained.SameFor<SOOrderType.copyLineFilesToInvoice>();
    chained = chained.SameFor<SOOrderType.copyLineFilesToInvoiceOnlyNS>();
    chained = chained.For<SOOrderType.copyHeaderFilesToShipment>((Action<PXUIFieldAttribute>) (a => a.Enabled = ordertype.RequireShipping.GetValueOrDefault()));
    chained = chained.SameFor<SOOrderType.copyHeaderNotesToShipment>();
    chained = chained.SameFor<SOOrderType.copyLineFilesToShipment>();
    chained.SameFor<SOOrderType.copyLineNotesToShipment>();
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row);
    chained = attributeAdjuster.For<SOOrderType.copyLineNotesToChildOrder>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = isBlanket));
    chained = chained.SameFor<SOOrderType.copyLineFilesToChildOrder>();
    chained.SameFor<SOOrderType.dfltChildOrderType>();
    object obj;
    sender.RaiseFieldDefaulting<SOOrderType.allowRefundBeforeReturn>(e.Row, ref obj);
    PXCache pxCache7 = sender;
    SOOrderType soOrderType6 = ordertype;
    nullable = (bool?) obj;
    int num10 = !nullable.GetValueOrDefault() ? 0 : (!isMixedOrder ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<SOOrderType.allowRefundBeforeReturn>(pxCache7, (object) soOrderType6, num10 != 0);
  }

  protected virtual void SOOrderType_CopyLineNotesToInvoice_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrderType row) || row.CopyLineNotesToInvoice.GetValueOrDefault())
      return;
    row.CopyLineNotesToInvoiceOnlyNS = new bool?(false);
  }

  protected virtual void SOOrderType_CopyLineFilesToInvoice_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrderType row) || row.CopyLineFilesToInvoice.GetValueOrDefault())
      return;
    row.CopyLineFilesToInvoiceOnlyNS = new bool?(false);
  }

  protected virtual void SOOrderType_RequireShipping_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrderType row))
      return;
    if (row.RequireShipping.GetValueOrDefault())
    {
      row.RequireLotSerial = new bool?(false);
    }
    else
    {
      row.RequireAllocation = new bool?(false);
      row.RequireLotSerial = new bool?(row.INDocType != "UND");
      row.CopyLotSerialFromShipment = new bool?(false);
    }
  }

  protected virtual void SOOrderType_PostLineDiscSeparately_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrderType row) || row.PostLineDiscSeparately.GetValueOrDefault())
      return;
    row.UseDiscountSubFromSalesSub = new bool?(false);
  }

  protected virtual void SOOrderTypeOperation_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SOOrderType current = ((PXSelectBase<SOOrderType>) this.soordertype).Current;
    SOOrderTypeOperation row1 = (SOOrderTypeOperation) e.Row;
    if (current == null || row1 == null)
      return;
    bool flag1 = current.Behavior == "BL";
    SOOrderType soOrderType = PXResultset<SOOrderType>.op_Implicit(((PXSelectBase<SOOrderType>) this.references).SelectWindowed(0, 1, new object[1]
    {
      (object) current.OrderType
    }));
    bool? isSystem = current.IsSystem;
    bool flag2 = false;
    bool flag3 = isSystem.GetValueOrDefault() == flag2 & isSystem.HasValue && soOrderType == null && current.OrderType != null && !flag1;
    bool? nullable;
    bool flag4;
    if (row1.Operation == "I")
    {
      nullable = row1.Active;
      flag4 = nullable.GetValueOrDefault();
    }
    else
    {
      SOOrderTypeOperation orderTypeOperation = PXResultset<SOOrderTypeOperation>.op_Implicit(((PXSelectBase<SOOrderTypeOperation>) this.defaultOperation).Select(new object[2]
      {
        (object) row1.OrderType,
        (object) "I"
      }));
      flag4 = orderTypeOperation != null && orderTypeOperation.Active.GetValueOrDefault();
    }
    PXUIFieldAttribute.SetEnabled<SOOrderTypeOperation.active>(sender, e.Row, row1.Operation != current.DefaultOperation && current.Behavior != "MO");
    PXUIFieldAttribute.SetEnabled<SOOrderTypeOperation.iNDocType>(sender, e.Row, flag3);
    PXCache pxCache = sender;
    object row2 = e.Row;
    int num1;
    if (row1.INDocType != "UND")
    {
      nullable = current.RequireShipping;
      num1 = nullable.Value ? 1 : 0;
    }
    else
      num1 = 0;
    int num2 = flag3 ? 1 : 0;
    int num3 = num1 & num2;
    PXUIFieldAttribute.SetEnabled<SOOrderTypeOperation.shipmentPlanType>(pxCache, row2, num3 != 0);
    PXUIFieldAttribute.SetEnabled<SOOrderTypeOperation.orderPlanType>(sender, e.Row, row1.INDocType != "UND" && current.OrderType != null && !flag1);
    PXUIFieldAttribute.SetEnabled<SOOrderTypeOperation.autoCreateIssueLine>(sender, e.Row, ((!(current.Behavior == "RM") ? 0 : (row1.Operation == "R" ? 1 : 0)) & (flag4 ? 1 : 0) & (flag3 ? 1 : 0)) != 0);
    PXUIFieldAttribute.SetEnabled<SOOrderTypeOperation.requireReasonCode>(sender, e.Row, !flag1);
  }

  protected virtual void Validate(PXCache sender, SOOrderType row)
  {
    SOOrderTypeOperation orderTypeOperation = PXResultset<SOOrderTypeOperation>.op_Implicit(((PXSelectBase<SOOrderTypeOperation>) this.defaultOperation).Select(new object[2]
    {
      (object) row.OrderType,
      (object) row.DefaultOperation
    }));
    if (orderTypeOperation == null)
      return;
    short? nullable1 = new short?((short) 0);
    short? nullable2 = INTranType.InvtMult(orderTypeOperation.INDocType);
    switch (row.ARDocType)
    {
      case "INV":
      case "DRM":
      case "CSL":
        nullable1 = new short?((short) -1);
        break;
      case "CRM":
      case "RCS":
        nullable1 = new short?((short) 1);
        break;
    }
    if (row.INDocType == null)
    {
      PXException pxException = (PXException) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<SOOrderTypeOperation.iNDocType>(sender)
      });
      ((PXSelectBase) this.operations).Cache.RaiseExceptionHandling<SOOrderTypeOperation.iNDocType>((object) orderTypeOperation, (object) orderTypeOperation.INDocType, (Exception) pxException);
    }
    if (row.Behavior != "RM")
    {
      short? nullable3 = nullable2;
      int? nullable4 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
      nullable3 = nullable1;
      int? nullable5 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
      if (!(nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue) && nullable2.GetValueOrDefault() != (short) 0 && nullable1.GetValueOrDefault() != (short) 0)
      {
        PXException pxException = (PXException) new PXSetPropertyException("Selected combination of Inventory Transaction Type and AR Document Type is not supported.");
        sender.RaiseExceptionHandling<SOOrderType.aRDocType>((object) row, (object) row.ARDocType, (Exception) pxException);
        ((PXSelectBase) this.operations).Cache.RaiseExceptionHandling<SOOrderTypeOperation.iNDocType>((object) orderTypeOperation, (object) orderTypeOperation.INDocType, (Exception) pxException);
      }
    }
    if (row.INDocType != orderTypeOperation.INDocType)
    {
      PXException pxException = (PXException) new PXSetPropertyException("Selected Inventory Transaction Type is not supported for this type operation.");
      ((PXSelectBase) this.operations).Cache.RaiseExceptionHandling<SOOrderTypeOperation.iNDocType>((object) orderTypeOperation, (object) orderTypeOperation.INDocType, (Exception) pxException);
    }
    if (!(row.ARDocType == "UND") || !(row.INDocType == "TRX") || !(row.Behavior != "TR"))
      return;
    PXException pxException1 = (PXException) new PXSetPropertyException("The Transfer inventory transaction type can be specified only for order types with the Transfer Order automation behavior.");
    sender.RaiseExceptionHandling<SOOrderType.behavior>((object) row, (object) row.Behavior, (Exception) pxException1);
  }

  protected virtual void SOOrderType_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is SOOrderType row))
      return;
    bool flag = row.Behavior == "BL";
    PXDefaultAttribute.SetPersistingCheck<SOOrderType.dfltChildOrderType>(sender, (object) row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    int? nullable1;
    if (row.UseShippedNotInvoiced.GetValueOrDefault())
    {
      nullable1 = row.ShippedNotInvoicedAcctID;
      if (!nullable1.HasValue)
        throw new PXRowPersistingException("shippedNotInvoicedAcctID", (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "shippedNotInvoicedAcctID"
        });
    }
    bool? nullable2 = row.UseShippedNotInvoiced;
    if (nullable2.GetValueOrDefault())
    {
      nullable1 = row.ShippedNotInvoicedSubID;
      if (!nullable1.HasValue && PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
        throw new PXRowPersistingException("shippedNotInvoicedSubID", (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "shippedNotInvoicedSubID"
        });
    }
    if (e.Operation == 2 || e.Operation == 1)
      this.Validate(sender, row);
    nullable2 = row.RequireShipping;
    if (nullable2.GetValueOrDefault() && this.HasIssueOperation(row.OrderType) || !(row.OrchestrationStrategy != "NA"))
      return;
    PXSetPropertyException<SOOrderType.orchestrationStrategy> propertyException = new PXSetPropertyException<SOOrderType.orchestrationStrategy>("The configuration does not support orchestration.", (PXErrorLevel) 4);
    if (((PXSelectBase) this.currentordertype).Cache.RaiseExceptionHandling<SOOrderType.orchestrationStrategy>((object) row, (object) row.OrchestrationStrategy, (Exception) propertyException))
      throw new PXRowPersistingException("orchestrationStrategy", (object) row, "The configuration does not support orchestration.");
  }

  protected virtual void SOOrderType_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    SOOrderType row = (SOOrderType) e.Row;
    SOOrderType soOrderType1 = PXResultset<SOOrderType>.op_Implicit(((PXSelectBase<SOOrderType>) this.references).SelectWindowed(0, 1, new object[1]
    {
      (object) row.OrderType
    }));
    if (soOrderType1 != null)
      throw new PXSetPropertyException("Order type cannot be deleted. It is in use as template for order type '{0}'.", new object[1]
      {
        (object) soOrderType1.OrderType
      });
    if (row.Behavior == "SO")
    {
      SOOrderType soOrderType2 = PXResultset<SOOrderType>.op_Implicit(PXSelectBase<SOOrderType, PXSelect<SOOrderType, Where<BqlOperand<SOOrderType.dfltChildOrderType, IBqlString>.IsEqual<BqlField<SOOrderType.orderType, IBqlString>.AsOptional>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.OrderType
      }));
      if (soOrderType2 != null)
        throw new PXSetPropertyException("The {0} order type is the default child order type for the {1} order type.", new object[2]
        {
          (object) row.OrderType,
          (object) soOrderType2.OrderType
        });
    }
    if (PXResultset<SOLine>.op_Implicit(PXSelectBase<SOLine, PXSelectReadonly<SOLine, Where<SOLine.orderType, Equal<Required<SOOrderType.orderType>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.OrderType
    })) != null)
      throw new PXSetPropertyException("Order type cannot be deleted. It is used in transactions.");
  }

  protected virtual void SOOrderTypeOperation_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    SOOrderType current = ((PXSelectBase<SOOrderType>) this.soordertype).Current;
    SOOrderTypeOperation row = (SOOrderTypeOperation) e.Row;
    if (e.Operation == 3 || current == null || row == null)
      return;
    PXDefaultAttribute.SetPersistingCheck<SOOrderTypeOperation.orderPlanType>(sender, (object) row, row.INDocType != "UND" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<SOOrderTypeOperation.shipmentPlanType>(sender, (object) row, !(row.INDocType != "UND") || !current.RequireShipping.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    sender.VerifyFieldAndRaiseException<SOOrderTypeOperation.orderPlanType>((object) row);
  }

  private bool CanAuthRemainder(SOOrderType ordertype, SOOrderTypeOperation typeOperation)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>())
      return false;
    if (ordertype.Behavior == "SO")
      return true;
    return ordertype.Behavior == "RM" && typeOperation?.Operation == "I";
  }

  public virtual void Persist()
  {
    foreach (SOOrderType soOrderType in GraphHelper.RowCast<SOOrderType>(((PXSelectBase) this.currentordertype).Cache.Inserted).Union<SOOrderType>(GraphHelper.RowCast<SOOrderType>(((PXSelectBase) this.currentordertype).Cache.Updated)))
    {
      foreach (SOOrderTypeOperation orderTypeOperation in ((PXSelectBase) this.operations).View.SelectMultiBound((object[]) new SOOrderType[1]
      {
        soOrderType
      }, Array.Empty<object>()))
      {
        if (orderTypeOperation.INDocType == "UND" && EnumerableExtensions.IsIn<string>(soOrderType.Behavior, "SO", "TR", "RM"))
          throw new PXRowPersistingException(typeof (SOOrderTypeOperation.iNDocType).Name, (object) null, "The {0} automation behavior requires shipment processing and therefore it cannot be selected if the No Update type of the inventory transaction is selected in the Operations table. Select another inventory transaction type to proceed.", new object[1]
          {
            (object) PXStringListAttribute.GetLocalizedLabel<SOOrderType.behavior>(((PXSelectBase) this.currentordertype).Cache, (object) soOrderType)
          });
      }
    }
    ((PXGraph) this).Persist();
  }

  protected virtual bool HasIssueOperation(string orderType)
  {
    return GraphHelper.RowCast<SOOrderTypeOperation>((IEnumerable) ((PXSelectBase<SOOrderTypeOperation>) this.operations).Select(new object[1]
    {
      (object) orderType
    })).Where<SOOrderTypeOperation>((Func<SOOrderTypeOperation, bool>) (o => EnumerableExtensions.IsIn<string>(o.INDocType, "III", "INV") && o.Operation == "I" && o.Active.GetValueOrDefault())).Count<SOOrderTypeOperation>() > 0;
  }
}
