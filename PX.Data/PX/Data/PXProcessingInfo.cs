// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProcessingInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXProcessingInfo
{
  public System.DateTime StarTime = System.DateTime.UtcNow;

  public PXProcessingMessagesCollection Messages { get; set; }

  public bool ProcessingCompleted { get; set; }

  public int Errors => this.Messages.Errors;

  public int Warnings => this.Messages.Warnings;

  public int Processed => this.Messages.Processed;

  public int Total => this.Messages.Length;
}
