// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSPInParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Data;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class PXSPInParameter : PXSPParameter
{
  public PXSPInParameter(string fieldName, object value)
    : this(new Column(fieldName), value)
  {
  }

  public PXSPInParameter(Column column, object value)
    : base(column, ParameterDirection.Input, value)
  {
  }

  public PXSPInParameter(string fieldName, PXDbType valueType, object value)
    : this(new Column(fieldName), valueType, value)
  {
  }

  public PXSPInParameter(Column column, PXDbType valueType, object value)
    : base(column, ParameterDirection.Input, valueType, value)
  {
  }

  public PXSPInParameter(string fieldName, PXDbType valueType, int? valueLength, object value)
    : this(new Column(fieldName), valueType, valueLength, value)
  {
  }

  public PXSPInParameter(Column column, PXDbType valueType, int? valueLength, object value)
    : base(column, ParameterDirection.Input, valueType, valueLength, value)
  {
  }
}
