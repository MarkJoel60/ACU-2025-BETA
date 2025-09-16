// Decompiled with JetBrains decompiler
// Type: PX.Data.PXColorState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXColorState : PXStringState
{
  protected PXColorState(object value)
    : base(value)
  {
  }

  public static PXFieldState CreateInstance(PXFieldState value)
  {
    if (!(value is PXStringState pxStringState))
      return value;
    PXColorState instance = new PXColorState((object) pxStringState);
    instance._AllowedLabels = pxStringState.AllowedLabels;
    instance._AllowedValues = pxStringState.AllowedValues;
    instance._InputMask = pxStringState.InputMask;
    instance._NeutralLabels = pxStringState._NeutralLabels;
    instance._ExclusiveValues = pxStringState.ExclusiveValues;
    instance._EmptyPossible = pxStringState.EmptyPossible;
    instance._MultiSelect = pxStringState.MultiSelect;
    return (PXFieldState) instance;
  }
}
