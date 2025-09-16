// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBBaseScreenIDAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Base class for PXDB***ByScreenID attributes</summary>
public abstract class PXDBBaseScreenIDAttribute : PXDBStringAttribute
{
  /// <summary>
  /// Initializes a new instance of the <tt>PXDBCreatedByScreenID</tt>
  /// attribute. Limits the maximum value length and sets the input mask.
  /// </summary>
  public PXDBBaseScreenIDAttribute()
    : base(8)
  {
    this.InputMask = "aa.aa.aa.aa";
  }

  protected string GetScreenID(PXCache sender)
  {
    return sender.Graph.Accessinfo != null && sender.Graph.Accessinfo.ScreenID != null ? sender.Graph.Accessinfo.ScreenID.Replace(".", "") : "00000000";
  }
}
