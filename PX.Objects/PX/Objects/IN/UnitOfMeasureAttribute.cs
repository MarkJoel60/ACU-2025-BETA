// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.UnitOfMeasureAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// Attribute used to automatically create parent UnitOfMeasure for newly created INUnits that are created outside of UnitOfMeasureMaint.
/// </summary>
public class UnitOfMeasureAttribute : PXEventSubscriberAttribute, IPXRowPersistedSubscriber
{
  public void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != 1 || !(e.Row is INUnit row))
      return;
    short? unitType = row.UnitType;
    int? nullable = unitType.HasValue ? new int?((int) unitType.GetValueOrDefault()) : new int?();
    int num1 = 3;
    if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
    {
      unitType = row.UnitType;
      nullable = unitType.HasValue ? new int?((int) unitType.GetValueOrDefault()) : new int?();
      int num2 = 1;
      if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
        return;
    }
    PXEntryStatus status = sender.GetStatus((object) row);
    if (status != 2 && status != 1)
      return;
    UnitOfMeasureMaint instance = PXGraph.CreateInstance<UnitOfMeasureMaint>();
    instance.AddNew(row.FromUnit);
    if (!(row.ToUnit != row.FromUnit))
      return;
    instance.AddNew(row.ToUnit);
  }
}
