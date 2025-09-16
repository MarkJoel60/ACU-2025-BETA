// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.SyImport.EntitySetPropertyException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Api.Export.SyImport;

internal sealed class EntitySetPropertyException : PXException
{
  internal string ViewName { get; }

  internal string FieldName { get; }

  internal EntitySetPropertyException(
    string viewName,
    string fieldName,
    string message,
    params object[] args)
    : base(message, args)
  {
    this.ViewName = viewName;
    this.FieldName = fieldName;
  }
}
