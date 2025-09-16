// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTemplateMailButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the specific properties.</summary>
public class PXTemplateMailButtonAttribute : PXButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute, setting the image and the tooltip.
  /// </summary>
  public PXTemplateMailButtonAttribute()
  {
    this.ImageKey = "Note";
    this.Tooltip = "Load the email template.";
  }
}
