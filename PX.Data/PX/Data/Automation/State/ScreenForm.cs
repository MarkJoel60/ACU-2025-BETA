// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.ScreenForm
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class ScreenForm
{
  public readonly bool Customized;
  public readonly bool IsVisible;
  public readonly string Caption;
  public readonly List<ScreenFieldForm> Fields = new List<ScreenFieldForm>();

  public ScreenForm(bool customized, bool isVisible, string caption)
  {
    this.Customized = customized;
    this.IsVisible = isVisible;
    this.Caption = caption;
  }
}
