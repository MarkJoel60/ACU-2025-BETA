// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSPInOutParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Data;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class PXSPInOutParameter : PXSPParameter
{
  public PXSPInOutParameter(string fieldName, object value)
    : this(new Column(fieldName), value)
  {
  }

  public PXSPInOutParameter(Column column, object value)
    : base(column, ParameterDirection.InputOutput, value)
  {
  }

  public PXSPInOutParameter(string fieldName, PXDbType valueType, object value)
    : this(new Column(fieldName), valueType, value)
  {
  }

  public PXSPInOutParameter(Column column, PXDbType valueType, object value)
    : base(column, ParameterDirection.InputOutput, valueType, value)
  {
  }

  public PXSPInOutParameter(string fieldName, PXDbType valueType, int? valueLength, object value)
    : this(new Column(fieldName), valueType, valueLength, value)
  {
  }

  public PXSPInOutParameter(Column column, PXDbType valueType, int? valueLength, object value)
    : base(column, ParameterDirection.InputOutput, valueType, valueLength, value)
  {
  }

  public PXSPInOutParameter(
    string fieldName,
    PXDbType valueType,
    int? valueLength,
    int? scale,
    object value)
    : this(new Column(fieldName), valueType, valueLength, scale, value)
  {
  }

  public PXSPInOutParameter(
    Column column,
    PXDbType valueType,
    int? valueLength,
    int? scale,
    object value)
    : base(column, ParameterDirection.InputOutput, valueType, valueLength, scale, value)
  {
  }
}
