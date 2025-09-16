// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.ToolbarButtonDescriptor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Services;

public class ToolbarButtonDescriptor
{
  public string Target;
  public string Command;
  public string CommandArgument;
  public string Text;
  public bool UsesSignalR;

  public ToolbarButtonDescriptor(string target, string command, string text = null)
  {
    this.Target = target;
    this.Command = command;
    this.Text = text;
  }
}
