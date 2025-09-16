// Decompiled with JetBrains decompiler
// Type: PX.Api.PXSYItem
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.Api;

public class PXSYItem
{
  private object _OrigValue;
  public bool IsAbsent;

  public PXFieldState State { get; set; }

  public string Value { get; set; }

  public object NativeValue
  {
    get => this._OrigValue ?? (object) this.Value;
    set => this._OrigValue = value;
  }

  internal PXDBLocalizableStringAttribute.Translations Translations { get; set; }

  public PXSYItem(string value) => this.Value = value;

  public PXSYItem(string value, object nativeValue)
  {
    this.Value = value;
    this.NativeValue = nativeValue;
  }

  internal PXSYItem(string value, object nativeValue, PXFieldState state, bool isAbsent)
  {
    this.Value = value;
    this.NativeValue = nativeValue;
    this.State = state;
    this.IsAbsent = isAbsent;
  }

  internal PXSYItem(
    string value,
    object nativeValue,
    PXFieldState state,
    bool isAbsent,
    PXDBLocalizableStringAttribute.Translations translations)
  {
    this.Value = value;
    this.NativeValue = nativeValue;
    this.State = state;
    this.IsAbsent = isAbsent;
    this.Translations = translations;
  }

  public override string ToString() => this.Value;
}
