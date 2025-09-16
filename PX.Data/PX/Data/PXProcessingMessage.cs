// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProcessingMessage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXProcessingMessage
{
  internal System.Type ItemType { get; set; }

  public PXErrorLevel ErrorLevel { get; set; }

  public string Message { get; set; }

  public PXProcessingMessage()
  {
  }

  public PXProcessingMessage(PXErrorLevel errorLevel, string message)
    : this((System.Type) null, errorLevel, message)
  {
  }

  protected PXProcessingMessage(System.Type itemType, PXErrorLevel errorLevel, string message)
  {
    this.ItemType = itemType;
    this.ErrorLevel = errorLevel;
    this.Message = message;
  }
}
