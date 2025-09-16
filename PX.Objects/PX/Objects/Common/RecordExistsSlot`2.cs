// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.RecordExistsSlot`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common;

public class RecordExistsSlot<Table, KeyField> : 
  RecordExistsSlot<Table, KeyField, Where<True, Equal<True>>>
  where Table : class, IBqlTable, new()
  where KeyField : IBqlField
{
  protected override PXSelectBase<Table> CreateSelect(PXGraph tempGraph)
  {
    return (PXSelectBase<Table>) new PXSelectReadonly<Table>(tempGraph);
  }
}
