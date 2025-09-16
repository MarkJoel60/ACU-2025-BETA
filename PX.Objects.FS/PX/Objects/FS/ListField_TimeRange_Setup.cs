// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_TimeRange_Setup
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public abstract class ListField_TimeRange_Setup : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.TimeRange_Setup().ID_LIST, new ID.TimeRange_Setup().TX_LIST)
    {
    }
  }
}
