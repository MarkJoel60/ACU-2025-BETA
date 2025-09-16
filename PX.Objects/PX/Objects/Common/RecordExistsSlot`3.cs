// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.RecordExistsSlot`3
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
public class RecordExistsSlot<Table, KeyField, Where> : IPrefetchable, IPXCompanyDependent
  where Table : class, IBqlTable, new()
  where KeyField : IBqlField
  where Where : IBqlWhere, new()
{
  public bool RecordExists { get; private set; }

  public void Prefetch()
  {
    PXSelectBase<Table> select = this.CreateSelect(PXGraph.CreateInstance<PXGraph>());
    using (new PXFieldScope(((PXSelectBase) select).View, new Type[1]
    {
      typeof (KeyField)
    }))
      this.RecordExists = (object) select.SelectSingle(Array.Empty<object>()) != null;
  }

  protected virtual PXSelectBase<Table> CreateSelect(PXGraph tempGraph)
  {
    return (PXSelectBase<Table>) new PXSelectReadonly<Table, Where>(tempGraph);
  }

  public static bool IsRowsExists()
  {
    return PXDatabase.GetSlot<RecordExistsSlot<Table, KeyField, Where>>(typeof (RecordExistsSlot<Table, KeyField, Where>).ToString(), new Type[1]
    {
      typeof (Table)
    }).RecordExists;
  }
}
