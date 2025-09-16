// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSBoxMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.CS;

public class CSBoxMaint : PXGraph<CSBoxMaint>
{
  public PXSetup<CommonSetup> Setup;
  public PXSelectJoin<CSBox, CrossJoin<CommonSetup>> Records;
  public PXSavePerRow<CSBox> Save;
  public PXCancel<CSBox> Cancel;

  public CSBoxMaint()
  {
    CommonSetup current = ((PXSelectBase<CommonSetup>) this.Setup).Current;
  }

  protected virtual void CSBox_BoxWeight_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    CSBox row = (CSBox) e.Row;
    if (row == null)
      return;
    Decimal? newValue = (Decimal?) e.NewValue;
    Decimal? maxWeight = row.MaxWeight;
    if (newValue.GetValueOrDefault() >= maxWeight.GetValueOrDefault() & newValue.HasValue & maxWeight.HasValue)
      throw new PXSetPropertyException("Weight of an empty Box should be less then the Max. Weight");
  }

  protected virtual void CSBox_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    CSBox row = (CSBox) e.Row;
    List<SOShipment> list1 = GraphHelper.RowCast<SOShipment>((IEnumerable) PXSelectBase<SOShipment, PXSelectJoinGroupBy<SOShipment, InnerJoin<SOPackageDetail, On<SOPackageDetail.shipmentNbr, Equal<SOShipment.shipmentNbr>>>, Where<SOPackageDetail.boxID, Equal<Required<CSBox.boxID>>, And<SOShipment.released, NotEqual<True>>>, Aggregate<GroupBy<SOShipment.shipmentNbr>>>.Config>.SelectWindowed((PXGraph) this, 0, 10, new object[1]
    {
      (object) row.BoxID
    })).ToList<SOShipment>();
    if (list1.Any<SOShipment>())
      throw new PXException("The box cannot be deleted because it is used in the following open shipments: {0}.", new object[1]
      {
        (object) string.Join(", ", list1.Select<SOShipment, string>((Func<SOShipment, string>) (_ => _.ShipmentNbr)).Distinct<string>())
      });
    List<SOOrder> list2 = GraphHelper.RowCast<SOOrder>((IEnumerable) PXSelectBase<SOOrder, PXSelectJoinGroupBy<SOOrder, InnerJoin<SOPackageInfo, On<SOPackageInfo.FK.Order>>, Where<SOPackageInfo.boxID, Equal<Required<CSBox.boxID>>, And<SOOrder.completed, NotEqual<True>, And<SOOrder.cancelled, NotEqual<True>>>>, Aggregate<GroupBy<SOOrder.orderType, GroupBy<SOOrder.orderNbr>>>>.Config>.SelectWindowed((PXGraph) this, 0, 10, new object[1]
    {
      (object) row.BoxID
    })).ToList<SOOrder>();
    if (list2.Any<SOOrder>())
    {
      StringBuilder ordersString = new StringBuilder();
      list2.ForEach((Action<SOOrder>) (_ => ordersString.AppendFormat("{0} {1}, ", (object) _.OrderType, (object) _.OrderNbr)));
      ordersString.Remove(ordersString.Length - 2, 2);
      throw new PXException("The box cannot be deleted because it is used in the following open sales orders: {0}.", new object[1]
      {
        (object) ordersString
      });
    }
  }
}
