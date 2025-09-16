// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.GIRowValidationError
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.GenericInquiry;

internal class GIRowValidationError : GIValidationError
{
  public GIRowValidationError(IBqlTable row, PXErrorLevel errorLevel, string message)
  {
    this.Row = row;
    this.ErrorLevel = errorLevel;
    this.Message = message;
  }

  public GIRowValidationError(
    IBqlTable row,
    PXErrorLevel errorLevel,
    string message,
    params string[] arguments)
    : this(row, errorLevel, message)
  {
    this.Arguments = arguments;
  }

  public IBqlTable Row { get; }
}
