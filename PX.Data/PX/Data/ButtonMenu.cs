// Decompiled with JetBrains decompiler
// Type: PX.Data.ButtonMenu
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the lookup
/// button.</summary>
/// <example>
/// <code>
/// public PXAction&lt;APInvoice&gt; newVendor;
/// 
/// [PXUIField(DisplayName = "New Vendor",
///            MapEnableRights = PXCacheRights.Select,
///            MapViewRights = PXCacheRights.Select)]
/// [PXLookupButton]
/// public virtual IEnumerable NewVendor(PXAdapter adapter) { ... }
/// </code>
/// </example>
public sealed class ButtonMenu
{
  public readonly string Command;
  public string Text;
  public string ImageSet;
  public string ImageKey;
  public string Icon;
  public string IconDisabled;
  public bool Enabled;
  internal bool ActionEnabled = true;
  public bool Visible = true;
  public bool SyncText;
  public HotKeyInfo HotKey = HotKeyInfo.Empty;
  public PXSpecialButtonType OnClosingPopup;

  public ButtonMenu(string command, string icon)
  {
    this.Command = command;
    this.Text = PXMessages.Localize(command, out string _);
    this.Icon = this.IconDisabled = icon;
    this.Enabled = true;
  }

  public ButtonMenu(string command, string text, string icon)
  {
    this.Command = command;
    this.Text = text;
    this.Icon = this.IconDisabled = icon;
    this.Enabled = true;
  }

  public ButtonMenu(string command, string text, string icon, string iconDisabled)
  {
    this.Command = command;
    this.Text = text;
    this.Icon = icon;
    this.IconDisabled = iconDisabled;
    this.Enabled = true;
  }

  public bool GetEnabled() => this.Enabled && this.ActionEnabled;
}
