// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataFieldValue`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDataFieldValue<Field> : PXDataFieldValue where Field : IBqlField
{
  public PXDataFieldValue(object value)
    : this(PXDbType.Unspecified, new int?(), value, PXComp.EQ)
  {
  }

  public PXDataFieldValue(object value, PXComp comp)
    : this(PXDbType.Unspecified, new int?(), value, comp)
  {
  }

  public PXDataFieldValue(PXDbType valueType, object value)
    : this(valueType, new int?(), value, PXComp.EQ)
  {
  }

  public PXDataFieldValue(PXDbType valueType, int? valueLength, object value)
    : this(valueType, valueLength, value, PXComp.EQ)
  {
  }

  public PXDataFieldValue(PXDbType valueType, int? valueLength, object value, PXComp comp)
    : base(typeof (Field).Name, valueType, valueLength, value, comp)
  {
  }

  public PXDataFieldValue(string tableAlias, object value, PXComp comp)
    : base((SQLExpression) new Column(typeof (Field).Name, tableAlias), value, comp)
  {
  }
}
