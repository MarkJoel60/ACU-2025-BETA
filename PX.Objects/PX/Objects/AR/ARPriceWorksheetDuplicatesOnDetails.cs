// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPriceWorksheetDuplicatesOnDetails
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class ARPriceWorksheetDuplicatesOnDetails : 
  ForbidDuplicateDetailsExtension<ARPriceWorksheetMaint, ARPriceWorksheet, ARPriceWorksheetDetail>
{
  protected override IEnumerable<Type> GetDetailUniqueFields()
  {
    yield return typeof (ARPriceWorksheetDetail.refNbr);
    yield return typeof (ARPriceWorksheetDetail.priceType);
    yield return typeof (ARPriceWorksheetDetail.customerID);
    yield return typeof (ARPriceWorksheetDetail.custPriceClassID);
    yield return typeof (ARPriceWorksheetDetail.inventoryID);
    yield return typeof (ARPriceWorksheetDetail.siteID);
    yield return typeof (ARPriceWorksheetDetail.subItemID);
    yield return typeof (ARPriceWorksheetDetail.uOM);
    yield return typeof (ARPriceWorksheetDetail.curyID);
    yield return typeof (ARPriceWorksheetDetail.breakQty);
  }

  protected override ARPriceWorksheetDetail[] LoadDetails(ARPriceWorksheet document)
  {
    ARPriceWorksheetDetail[] array = base.LoadDetails(document);
    Array.ForEach<ARPriceWorksheetDetail>(array, (Action<ARPriceWorksheetDetail>) (item => item.PriceType = item.PriceType.ToUpper()));
    return array;
  }

  /// Overrides <see cref="M:PX.Objects.AR.ARPriceWorksheetMaint.CheckForDuplicateDetails" />
  [PXOverride]
  public virtual void CheckForDuplicateDetails(Action baseImpl)
  {
    baseImpl();
    this.CheckForDuplicates();
  }

  protected override void RaiseDuplicateError(ARPriceWorksheetDetail duplicate)
  {
    ((PXCache) this.DetailsCache).RaiseExceptionHandling<ARPriceWorksheetDetail.priceCode>((object) duplicate, (object) duplicate.PriceCode, (Exception) new PXSetPropertyException("Duplicate Sales Price.", (PXErrorLevel) 5, new object[1]
    {
      (object) typeof (ARPriceWorksheetDetail.priceCode).Name
    }));
  }
}
