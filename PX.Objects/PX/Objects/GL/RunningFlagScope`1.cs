// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.RunningFlagScope`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

public sealed class RunningFlagScope<KeyGraphType> : IDisposable where KeyGraphType : PXGraph
{
  private static string _SLOT_KEY = $"{typeof (KeyGraphType).Name}_Running";

  public RunningFlagScope()
  {
    PXDatabase.GetSlot<RunningFlagScope<KeyGraphType>.BoolWrapper>(RunningFlagScope<KeyGraphType>._SLOT_KEY, Array.Empty<Type>()).Value = true;
  }

  public void Dispose()
  {
    PXDatabase.ResetSlot<RunningFlagScope<KeyGraphType>.BoolWrapper>(RunningFlagScope<KeyGraphType>._SLOT_KEY, Array.Empty<Type>());
  }

  public static bool IsRunning
  {
    get
    {
      return PXDatabase.GetSlot<RunningFlagScope<KeyGraphType>.BoolWrapper>(RunningFlagScope<KeyGraphType>._SLOT_KEY, Array.Empty<Type>()).Value;
    }
  }

  private class BoolWrapper
  {
    public bool Value { get; set; }

    public BoolWrapper() => this.Value = false;
  }
}
