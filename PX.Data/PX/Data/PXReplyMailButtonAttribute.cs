// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReplyMailButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the button that
/// replies to an email.</summary>
/// <example>
/// <code>
/// public PXAction&lt;EmailFilter&gt; Reply;
/// 
/// [PXUIField(DisplayName = Messages.Reply)]
/// [PXReplyMailButton]
/// protected void reply() { ... }
/// </code>
/// </example>
public class PXReplyMailButtonAttribute : PXButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute, setting the specific tooltip.
  /// </summary>
  public PXReplyMailButtonAttribute() => this.Tooltip = "Reply to the email message.";
}
