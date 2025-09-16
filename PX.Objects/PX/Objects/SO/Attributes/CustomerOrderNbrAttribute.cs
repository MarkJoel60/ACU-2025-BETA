// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Attributes.CustomerOrderNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO.Attributes;

public class CustomerOrderNbrAttribute : CustomerOrderNbrBaseAttribute
{
  public CustomerOrderNbrAttribute()
    : base(typeof (SOOrder.orderType), typeof (SOOrder.orderNbr), typeof (SOOrder.customerID))
  {
  }

  public override void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.RowSelected(sender, e);
    SOOrder row = (SOOrder) e.Row;
    if (row == null || row.OrderType == null || PXUIFieldAttribute.GetErrorOnly<SOOrder.customerOrderNbr>(sender, e.Row) != null)
      return;
    SOOrderType soOrderType = SOOrderType.PK.Find(sender.Graph, row.OrderType);
    if ((soOrderType != null ? (!soOrderType.CustomerOrderIsRequired.GetValueOrDefault() ? 1 : 0) : 1) != 0 || string.IsNullOrWhiteSpace(row.CustomerOrderNbr) || soOrderType.CustomerOrderValidation != "W")
      return;
    SOOrder customerOrderDuplicate = this.FindCustomerOrderDuplicate(sender, row.CustomerOrderNbr, row);
    if (customerOrderDuplicate == null)
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("The same customer order number is used in the {0} sales order.", (PXErrorLevel) 2, new object[1]
    {
      (object) customerOrderDuplicate.OrderNbr
    });
    sender.RaiseExceptionHandling<SOOrder.customerOrderNbr>(e.Row, (object) row.CustomerOrderNbr, (Exception) propertyException);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    SOOrder row = (SOOrder) e.Row;
    if (row == null || row.OrderType == null || !row.CustomerID.HasValue || EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    SOOrderType soOrderType = SOOrderType.PK.Find(sender.Graph, row.OrderType);
    if (!soOrderType.CustomerOrderIsRequired.GetValueOrDefault())
      return;
    if (string.IsNullOrWhiteSpace(row.CustomerOrderNbr))
    {
      int num = PXDBOperationExt.Command(e.Operation) == 2 ? 0 : (sender.Graph.UnattendedMode ? 1 : 0);
      if (num == 0 && sender.RaiseExceptionHandling<SOOrder.customerOrderNbr>((object) row, (object) null, (Exception) new PXSetPropertyKeepPreviousException("The customer order number cannot be empty.")))
        throw new PXRowPersistingException("customerOrderNbr", (object) null, "The customer order number cannot be empty.");
      if (num != 0)
      {
        if (sender.RaiseExceptionHandling<SOOrder.customerOrderNbr>((object) row, (object) null, (Exception) new PXSetPropertyKeepPreviousException("The customer order number cannot be empty in the {0} sales order.", (PXErrorLevel) 4, new object[1]
        {
          (object) row.OrderNbr
        })))
          throw new PXRowPersistingException("customerOrderNbr", (object) null, "The customer order number cannot be empty in the {0} sales order.", new object[1]
          {
            (object) row.OrderNbr
          });
      }
    }
    if (soOrderType.CustomerOrderValidation != "E")
      return;
    SOOrder customerOrderDuplicate = this.FindCustomerOrderDuplicate(sender, row.CustomerOrderNbr, row);
    if (customerOrderDuplicate == null)
      return;
    if (sender.RaiseExceptionHandling<SOOrder.customerOrderNbr>((object) row, (object) row.CustomerOrderNbr, (Exception) new PXSetPropertyException("The same customer order number is used in the {0} sales order. Specify another customer order number.", (PXErrorLevel) 4, new object[1]
    {
      (object) customerOrderDuplicate.OrderNbr
    })))
      throw new PXRowPersistingException("customerOrderNbr", (object) null, "The same customer order number is used in the {0} sales order. Specify another customer order number.", new object[1]
      {
        (object) customerOrderDuplicate.OrderNbr
      });
  }

  protected virtual SOOrder FindCustomerOrderDuplicate(
    PXCache orderCache,
    string customerOrderNbr,
    SOOrder order)
  {
    return this.FindCustomerOrder(orderCache, order.OrderType, order.OrderNbr, order.CustomerID, customerOrderNbr, (object) order);
  }
}
