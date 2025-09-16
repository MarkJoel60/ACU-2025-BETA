// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataFieldAssign`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDataFieldAssign<Field> : PXDataFieldAssign where Field : IBqlField
{
  public PXDataFieldAssign(object value)
    : base(typeof (Field).Name, value)
  {
  }

  public PXDataFieldAssign(PXDbType valueType, object value)
    : base(typeof (Field).Name, valueType, value)
  {
  }

  public PXDataFieldAssign(PXDbType valueType, int? valueLength, object value)
    : base(typeof (Field).Name, valueType, valueLength, value)
  {
  }
}
