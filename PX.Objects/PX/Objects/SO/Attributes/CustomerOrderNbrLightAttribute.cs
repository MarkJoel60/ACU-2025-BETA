// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Attributes.CustomerOrderNbrLightAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO.DAC.Unbound;

#nullable disable
namespace PX.Objects.SO.Attributes;

public class CustomerOrderNbrLightAttribute : CustomerOrderNbrBaseAttribute
{
  public CustomerOrderNbrLightAttribute()
    : base(typeof (CreateSOOrderFilter.orderType), typeof (CreateSOOrderFilter.orderNbr), typeof (CreateSOOrderFilter.customerID))
  {
  }

  public override void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.RowSelected(sender, e);
    CreateSOOrderFilter row = (CreateSOOrderFilter) e.Row;
    SOOrderType soOrderType = SOOrderType.PK.Find(sender.Graph, row.OrderType);
    Numbering numbering = Numbering.PK.Find(sender.Graph, soOrderType?.OrderNumberingID);
    bool flag = numbering != null && numbering.UserNumbering.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<CreateSOOrderFilter.orderNbr>(sender, e.Row, flag);
    PXDefaultAttribute.SetPersistingCheck<CreateSOOrderFilter.orderNbr>(sender, e.Row, flag ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
  }
}
