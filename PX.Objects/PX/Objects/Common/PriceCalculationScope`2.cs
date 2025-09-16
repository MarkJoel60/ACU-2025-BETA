// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.PriceCalculationScope`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common;

[PXInternalUseOnly]
public class PriceCalculationScope<PriceListTable, KeyField> : UpdateIfFieldsChangedScope
  where PriceListTable : class, IBqlTable, new()
  where KeyField : IBqlField
{
  public override bool IsUpdateNeeded(params Type[] changes)
  {
    return base.IsUpdateNeeded(changes) || this.IsPriceListExist();
  }

  public virtual bool IsPriceListExist()
  {
    return RecordExistsSlot<PriceListTable, KeyField, Where<True, Equal<True>>>.IsRowsExists();
  }
}
