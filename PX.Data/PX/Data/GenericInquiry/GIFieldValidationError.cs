// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.GIFieldValidationError
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.GenericInquiry;

internal class GIFieldValidationError : GIRowValidationError
{
  public GIFieldValidationError(
    string fieldName,
    IBqlTable row,
    PXErrorLevel errorLevel,
    string message)
    : base(row, errorLevel, message)
  {
    this.FieldName = fieldName;
  }

  public GIFieldValidationError(
    string fieldName,
    IBqlTable row,
    PXErrorLevel errorLevel,
    string message,
    params string[] arguments)
    : base(row, errorLevel, message, arguments)
  {
    this.FieldName = fieldName;
  }

  public string FieldName { get; private set; }
}
