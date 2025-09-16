// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPriceClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.AR;

public class ARPriceClassMaint : PXGraph<ARPriceClassMaint>
{
  public PXSavePerRow<ARPriceClass> Save;
  public PXCancel<ARPriceClass> Cancel;
  [PXFilterable(new Type[] {})]
  public PXSelect<ARPriceClass> Records;

  protected virtual void ARPriceClass_CustPriceClassID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARPriceClass) || !("BASE" == e.NewValue.ToString()))
      return;
    ((CancelEventArgs) e).Cancel = true;
    if (sender.RaiseExceptionHandling<ARPriceClass.priceClassID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' is a reserved word and cannot be used here.", new object[1]
    {
      (object) "BASE"
    })))
      throw new PXSetPropertyException(typeof (ARPriceClass.priceClassID).Name, new object[3]
      {
        null,
        (object) "'{0}' is a reserved word and cannot be used here.",
        (object) "BASE"
      });
  }

  protected virtual void ARPriceClass_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is ARPriceClass))
      return;
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (DiscountCustomerPriceClass.customerPriceClassID), (Type) null, (string) null);
  }
}
