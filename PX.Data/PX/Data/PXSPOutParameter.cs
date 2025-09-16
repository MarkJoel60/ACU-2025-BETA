// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSPOutParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Data;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class PXSPOutParameter : PXSPParameter
{
  public PXSPOutParameter(string fieldName, object value)
    : this(new Column(fieldName), value)
  {
  }

  public PXSPOutParameter(Column column, object value)
    : base(column, ParameterDirection.Output, value)
  {
  }

  public PXSPOutParameter(string fieldName, PXDbType valueType, object value)
    : this(new Column(fieldName), valueType, value)
  {
  }

  public PXSPOutParameter(Column column, PXDbType valueType, object value)
    : base(column, ParameterDirection.Output, valueType, value)
  {
  }

  public PXSPOutParameter(string fieldName, PXDbType valueType, int? valueLength, object value)
    : this(new Column(fieldName), valueType, valueLength, value)
  {
  }

  public PXSPOutParameter(Column column, PXDbType valueType, int? valueLength, object value)
    : base(column, ParameterDirection.Output, valueType, valueLength, value)
  {
  }

  public PXSPOutParameter(
    string fieldName,
    PXDbType valueType,
    int? valueLength,
    int? scale,
    object value)
    : this(new Column(fieldName), valueType, valueLength, scale, value)
  {
  }

  public PXSPOutParameter(
    Column column,
    PXDbType valueType,
    int? valueLength,
    int? scale,
    object value)
    : base(column, ParameterDirection.Output, valueType, valueLength, scale, value)
  {
  }
}
