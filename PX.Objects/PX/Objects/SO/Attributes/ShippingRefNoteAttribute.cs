// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Attributes.ShippingRefNoteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common;
using System;

#nullable disable
namespace PX.Objects.SO.Attributes;

public class ShippingRefNoteAttribute : PXGuidAttribute
{
  public virtual void CacheAttached(PXCache sender)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ShippingRefNoteAttribute.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new ShippingRefNoteAttribute.\u003C\u003Ec__DisplayClass0_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.\u003C\u003E4__this = this;
    base.CacheAttached(sender);
    if (sender.Graph.PrimaryItemType == (Type) null)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.cacheType = sender.GetItemType();
    // ISSUE: method pointer
    PXButtonDelegate pxButtonDelegate = new PXButtonDelegate((object) cDisplayClass00, __methodptr(\u003CCacheAttached\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    string str = $"{cDisplayClass00.cacheType.Name}~{((PXEventSubscriberAttribute) this)._FieldName}~Link";
    PXNamedAction.AddAction(sender.Graph, sender.Graph.PrimaryItemType, str, (string) null, (string) null, false, pxButtonDelegate, new PXEventSubscriberAttribute[1]
    {
      (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        IgnoresArchiveDisabling = true
      }
    });
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    Type targetType;
    object[] targetKeys;
    ShippingRefNoteAttribute.GetTargetTypeAndKeys(sender, e.Row, out targetType, out targetKeys);
    if (targetType != (Type) null)
      e.ReturnValue = PXRefNoteBaseAttribute.GetEntityRowID(sender.Graph.Caches[targetType], targetKeys, ", ");
    e.ReturnState = (object) PXRefNoteBaseAttribute.PXLinkState.CreateInstance(e.ReturnState, targetType, targetKeys);
  }

  public static void GetTargetTypeAndKeys(
    PXCache cache,
    object row,
    out Type targetType,
    out object[] targetKeys)
  {
    targetType = (Type) null;
    targetKeys = (object[]) null;
    string str1 = (string) cache.GetValue<PX.Objects.SO.SOOrderShipment.shipmentType>(row);
    string a = (string) cache.GetValue<PX.Objects.SO.SOOrderShipment.shipmentNbr>(row);
    string str2 = (string) cache.GetValue<PX.Objects.SO.SOOrderShipment.operation>(row);
    string str3 = (string) cache.GetValue<PX.Objects.SO.SOOrderShipment.invoiceType>(row);
    string str4 = (string) cache.GetValue<PX.Objects.SO.SOOrderShipment.invoiceNbr>(row);
    if (row != null && EnumerableExtensions.IsIn<string>(str1, "I", "T") && !string.Equals(a, "<NEW>"))
    {
      targetType = typeof (PX.Objects.SO.SOShipment);
      targetKeys = new object[1]{ (object) a };
    }
    else if (row != null && str1 == "H" && !string.IsNullOrEmpty(a))
    {
      targetType = typeof (PX.Objects.PO.POReceipt);
      string str5 = str2 == "R" ? "RN" : "RT";
      targetKeys = new object[2]
      {
        (object) str5,
        (object) a
      };
    }
    else
    {
      if (row == null || (!(str1 == "I") || !string.Equals(a, "<NEW>")) && !(str1 == "N") || string.IsNullOrEmpty(str3) || string.IsNullOrEmpty(str4))
        return;
      targetType = typeof (PX.Objects.AR.ARInvoice);
      targetKeys = new object[2]
      {
        (object) str3,
        (object) str4
      };
    }
  }

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 16 /*0x10*/) == 16 /*0x10*/)
      return;
    base.CommandPreparing(sender, e);
  }
}
