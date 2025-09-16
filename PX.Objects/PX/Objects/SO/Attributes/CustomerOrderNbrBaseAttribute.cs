// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Attributes.CustomerOrderNbrBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO.Attributes;

public abstract class CustomerOrderNbrBaseAttribute : 
  PXDefaultAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowUpdatedSubscriber,
  IPXRowSelectedSubscriber
{
  protected readonly Type _orderTypeField;
  protected readonly Type _orderNbrField;
  protected readonly Type _customerIDField;

  public CustomerOrderNbrBaseAttribute(
    Type orderTypeField,
    Type orderNbrField,
    Type customerIDField)
  {
    this.PersistingCheck = (PXPersistingCheck) 2;
    this._orderTypeField = orderTypeField;
    this._orderNbrField = orderNbrField;
    this._customerIDField = customerIDField;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    string newValue = (string) e.NewValue;
    string orderType = (string) sender.GetValue(e.Row, this._orderTypeField.Name);
    string orderNbr = (string) sender.GetValue(e.Row, this._orderNbrField.Name);
    int? customerID = (int?) sender.GetValue(e.Row, this._customerIDField.Name);
    if (orderType == null || !customerID.HasValue)
      return;
    SOOrderType soOrderType = SOOrderType.PK.Find(sender.Graph, orderType);
    if (!soOrderType.CustomerOrderIsRequired.GetValueOrDefault() || string.IsNullOrWhiteSpace(newValue) || EnumerableExtensions.IsNotIn<string>(soOrderType.CustomerOrderValidation, "W", "E"))
      return;
    SOOrder customerOrder = this.FindCustomerOrder(sender, orderType, orderNbr, customerID, newValue, e.Row);
    if (customerOrder == null)
      return;
    if (soOrderType.CustomerOrderValidation == "E")
      throw new PXSetPropertyException("The same customer order number is used in the {0} sales order. Specify another customer order number.", (PXErrorLevel) 4, new object[1]
      {
        (object) customerOrder.OrderNbr
      });
    sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) newValue, (Exception) new PXSetPropertyException("The same customer order number is used in the {0} sales order.", (PXErrorLevel) 2, new object[1]
    {
      (object) customerOrder.OrderNbr
    }));
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    string orderType = (string) sender.GetValue(e.Row, this._orderTypeField.Name);
    SOOrderType soOrderType = SOOrderType.PK.Find(sender.Graph, orderType);
    bool customerOrderNbrIsRequired = (soOrderType != null ? (soOrderType.CustomerOrderIsRequired.GetValueOrDefault() ? 1 : 0) : 0) != 0;
    PXCacheEx.Adjust<PXUIFieldAttribute>(sender, e.Row).For(((PXEventSubscriberAttribute) this)._FieldName, (Action<PXUIFieldAttribute>) (a => a.Required = customerOrderNbrIsRequired));
    this.PersistingCheck = customerOrderNbrIsRequired ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2;
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    string orderType = (string) sender.GetValue(e.Row, this._orderTypeField.Name);
    string orderNbr = (string) sender.GetValue(e.Row, this._orderNbrField.Name);
    int? customerID = (int?) sender.GetValue(e.Row, this._customerIDField.Name);
    int? nullable1 = (int?) sender.GetValue(e.OldRow, this._customerIDField.Name);
    string customerOrderNbr = (string) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (orderType == null || !customerID.HasValue)
      return;
    int? nullable2 = nullable1;
    int? nullable3 = customerID;
    if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
      return;
    SOOrderType soOrderType = SOOrderType.PK.Find(sender.Graph, orderType);
    if (!soOrderType.CustomerOrderIsRequired.GetValueOrDefault() || string.IsNullOrWhiteSpace(customerOrderNbr) || EnumerableExtensions.IsNotIn<string>(soOrderType.CustomerOrderValidation, "W", "E"))
      return;
    SOOrder customerOrder = this.FindCustomerOrder(sender, orderType, orderNbr, customerID, customerOrderNbr, e.Row);
    if (customerOrder == null)
      return;
    PXSetPropertyException propertyException1;
    if (!(soOrderType.CustomerOrderValidation == "E"))
      propertyException1 = new PXSetPropertyException("The same customer order number is used in the {0} sales order.", (PXErrorLevel) 2, new object[1]
      {
        (object) customerOrder.OrderNbr
      });
    else
      propertyException1 = new PXSetPropertyException("The same customer order number is used in the {0} sales order. Specify another customer order number.", (PXErrorLevel) 4, new object[1]
      {
        (object) customerOrder.OrderNbr
      });
    PXSetPropertyException propertyException2 = propertyException1;
    sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) customerOrderNbr, (Exception) propertyException2);
  }

  protected virtual SOOrder FindCustomerOrder(
    PXCache orderCache,
    string orderType,
    string orderNbr,
    int? customerID,
    string customerOrderNbr,
    object row)
  {
    return PXResultset<SOOrder>.op_Implicit(PXSelectBase<SOOrder, PXSelectReadonly<SOOrder, Where<SOOrder.orderType, Equal<Required<SOOrder.orderType>>, And<SOOrder.customerID, Equal<Required<SOOrder.customerID>>, And<SOOrder.customerOrderNbr, Equal<Required<SOOrder.customerOrderNbr>>, And<SOOrder.orderNbr, NotEqual<Required<SOOrder.orderNbr>>>>>>>.Config>.SelectWindowed(orderCache.Graph, 0, 1, new object[4]
    {
      (object) orderType,
      (object) customerID,
      (object) customerOrderNbr,
      (object) (orderNbr ?? string.Empty)
    }));
  }
}
