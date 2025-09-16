// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSPParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Data;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class PXSPParameter : PXDataFieldParam
{
  public readonly ParameterDirection Direction;
  public readonly int? Scale;

  public PXSPParameter(string fieldName, ParameterDirection direction, object value)
    : this(new Column(fieldName), direction, value)
  {
  }

  public PXSPParameter(Column column, ParameterDirection direction, object value)
    : base(column, value)
  {
    this.Direction = direction;
  }

  public PXSPParameter(
    string fieldName,
    ParameterDirection direction,
    PXDbType valueType,
    object value)
    : this(new Column(fieldName), direction, valueType, value)
  {
  }

  public PXSPParameter(
    Column column,
    ParameterDirection direction,
    PXDbType valueType,
    object value)
    : base(column, valueType, value)
  {
    this.Direction = direction;
  }

  public PXSPParameter(
    string fieldName,
    ParameterDirection direction,
    PXDbType valueType,
    int? valueLength,
    object value)
    : this(new Column(fieldName), direction, valueType, valueLength, value)
  {
  }

  public PXSPParameter(
    Column column,
    ParameterDirection direction,
    PXDbType valueType,
    int? valueLength,
    object value)
    : base(column, valueType, valueLength, value)
  {
    this.Direction = direction;
  }

  public PXSPParameter(
    string fieldName,
    ParameterDirection direction,
    PXDbType valueType,
    int? valueLength,
    int? scale,
    object value)
    : this(new Column(fieldName), direction, valueType, valueLength, scale, value)
  {
  }

  public PXSPParameter(
    Column column,
    ParameterDirection direction,
    PXDbType valueType,
    int? valueLength,
    int? scale,
    object value)
    : base(column, valueType, valueLength, value)
  {
    this.Direction = direction;
    this.Scale = scale;
  }
}
