// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyRateTypeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CM;

public class CurrencyRateTypeMaint : PXGraph<CurrencyRateTypeMaint>
{
  public PXSavePerRow<CurrencyRateType> Save;
  public PXCancel<CurrencyRateType> Cancel;
  [PXImport(typeof (CurrencyRateType))]
  public PXSelect<CurrencyRateType> CuryRateTypeRecords;

  protected virtual void CurrencyRateType_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CurrencyRateType row = (CurrencyRateType) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<CurrencyRateType.onlineRateAdjustment>(sender, (object) row, row.RefreshOnline.GetValueOrDefault());
  }
}
