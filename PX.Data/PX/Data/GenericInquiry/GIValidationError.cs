// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.GIValidationError
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.GenericInquiry;

internal abstract class GIValidationError
{
  public PXErrorLevel ErrorLevel { get; protected set; }

  public string Message { get; protected set; }

  public string[] Arguments { get; protected set; }
}
