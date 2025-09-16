// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.CopyLinkToSOInvoiceAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Attributes;
using System;

#nullable disable
namespace PX.Objects.SO;

public class CopyLinkToSOInvoiceAttribute(
  Type counterField,
  Type amountField,
  Type[] linkChildKeys,
  Type[] linkParentKeys) : CopyChildLinkAttribute(counterField, amountField, linkChildKeys, linkParentKeys)
{
  public override void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (this.IsParentDeleted(sender, e.Row))
      return;
    base.RowDeleted(sender, e);
  }

  protected override bool IsParentDeleted(PXCache childCache, object childRow)
  {
    return PXParentAttribute.SelectParent(childCache, childRow, typeof (PX.Objects.AR.ARInvoice)) == null;
  }
}
