// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FieldErrorScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

public sealed class FieldErrorScope : IDisposable
{
  private const string _key = "FieldError";
  private string _SLOT_KEY;

  public FieldErrorScope(Type FieldType)
    : this(FieldType.FullName, FieldErrorScope.Action.Reset)
  {
  }

  public FieldErrorScope(Type FieldType, FieldErrorScope.Action action)
    : this(FieldType.FullName, action)
  {
  }

  public FieldErrorScope(string FieldFullName, FieldErrorScope.Action action)
  {
    this._SLOT_KEY = $"{FieldFullName}_{"FieldError"}";
    PXDatabase.GetSlot<FieldErrorScope.ActionWrapper>(this._SLOT_KEY, Array.Empty<Type>()).Value = action;
  }

  public void Dispose()
  {
    PXDatabase.ResetSlot<FieldErrorScope.ActionWrapper>(this._SLOT_KEY, Array.Empty<Type>());
  }

  public static FieldErrorScope.Action GetAction(Type FieldType)
  {
    return FieldErrorScope.GetAction(FieldType.FullName);
  }

  public static FieldErrorScope.Action GetAction(string FieldFullName)
  {
    return PXDatabase.GetSlot<FieldErrorScope.ActionWrapper>($"{FieldFullName}_{"FieldError"}", Array.Empty<Type>()).Value;
  }

  public static bool NeedsSet(Type FieldType) => FieldErrorScope.NeedsSet(FieldType.FullName);

  public static bool NeedsSet(string FieldFullName)
  {
    return FieldErrorScope.GetAction(FieldFullName) == FieldErrorScope.Action.Set;
  }

  public static bool NeedsReset(Type FieldType) => FieldErrorScope.NeedsReset(FieldType.FullName);

  public static bool NeedsReset(string FieldFullName)
  {
    return FieldErrorScope.GetAction(FieldFullName) == FieldErrorScope.Action.Reset;
  }

  public enum Action
  {
    None,
    Set,
    Reset,
  }

  private class ActionWrapper
  {
    public FieldErrorScope.Action Value { get; set; }

    public ActionWrapper() => this.Value = FieldErrorScope.Action.None;
  }
}
