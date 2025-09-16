// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataFieldOrder`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDataFieldOrder<Field> : PXDataFieldOrder where Field : IBqlField
{
  public PXDataFieldOrder(bool isDesc)
    : base(typeof (Field).Name, isDesc)
  {
  }

  public PXDataFieldOrder()
    : this(false)
  {
  }

  public PXDataFieldOrder(string tableAlias, bool isDesc)
    : base(typeof (Field).Name, tableAlias, isDesc)
  {
  }

  public PXDataFieldOrder(string tableAlias)
    : this(tableAlias, false)
  {
  }
}
