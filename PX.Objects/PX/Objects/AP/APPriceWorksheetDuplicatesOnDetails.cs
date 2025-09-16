// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPriceWorksheetDuplicatesOnDetails
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class APPriceWorksheetDuplicatesOnDetails : 
  ForbidDuplicateDetailsExtension<APPriceWorksheetMaint, APPriceWorksheet, APPriceWorksheetDetail>
{
  protected override IEnumerable<System.Type> GetDetailUniqueFields()
  {
    yield return typeof (APPriceWorksheetDetail.refNbr);
    yield return typeof (APPriceWorksheetDetail.vendorID);
    yield return typeof (APPriceWorksheetDetail.inventoryID);
    yield return typeof (APPriceWorksheetDetail.siteID);
    yield return typeof (APPriceWorksheetDetail.subItemID);
    yield return typeof (APPriceWorksheetDetail.uOM);
    yield return typeof (APPriceWorksheetDetail.curyID);
    yield return typeof (APPriceWorksheetDetail.breakQty);
  }

  /// Overrides <see cref="M:PX.Objects.AP.APPriceWorksheetMaint.CheckForDuplicateDetails" />
  [PXOverride]
  public virtual void CheckForDuplicateDetails(System.Action baseImpl)
  {
    baseImpl();
    this.CheckForDuplicates();
  }

  protected override void RaiseDuplicateError(APPriceWorksheetDetail duplicate)
  {
    this.DetailsCache.RaiseExceptionHandling<APPriceWorksheetDetail.vendorID>((object) duplicate, (object) duplicate.VendorID, (Exception) new PXSetPropertyException("Duplicate Vendor Price.", PXErrorLevel.RowError, new object[1]
    {
      (object) typeof (APPriceWorksheetDetail.vendorID).Name
    }));
  }
}
