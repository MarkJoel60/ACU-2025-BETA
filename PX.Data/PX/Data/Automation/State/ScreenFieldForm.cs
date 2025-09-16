// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.ScreenFieldForm
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class ScreenFieldForm
{
  public readonly string FieldFormID;
  public readonly string DataBinding;
  public readonly string Caption;
  public readonly bool IsRequired;
  public readonly bool IsVisible;
  public readonly string Defaulting;
  public readonly string Value;
  public readonly string Before;
  public readonly string After;

  public ScreenFieldForm(
    string fieldFormID,
    string dataBinding,
    string caption,
    bool isRequired,
    bool isVisible,
    string defaulting,
    string value,
    string before,
    string after)
  {
    this.FieldFormID = fieldFormID;
    this.DataBinding = dataBinding;
    this.Caption = caption;
    this.IsRequired = isRequired;
    this.IsVisible = isVisible;
    this.Defaulting = defaulting;
    this.Value = value;
    this.Before = before;
    this.After = after;
  }
}
