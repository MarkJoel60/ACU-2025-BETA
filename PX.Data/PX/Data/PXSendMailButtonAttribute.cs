// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSendMailButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the button that sends
/// an email.</summary>
/// <example>
/// <code>
/// public PXAction&lt;EPActivity&gt; Send;
/// 
/// [PXUIField(DisplayName = Messages.Send,
///            MapEnableRights = PXCacheRights.Select)]
/// [PXSendMailButton]
/// protected virtual IEnumerable send(PXAdapter adapter) { ... }
/// </code>
/// </example>
public class PXSendMailButtonAttribute : PXButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute, setting the specific tooltip.
  /// </summary>
  public PXSendMailButtonAttribute() => this.Tooltip = "Send the email message.";
}
