// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTimeState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class PXTimeState : PXIntState
{
  protected PXTimeState(object value)
    : base(value)
  {
  }

  public static PXFieldState CreateInstance(
    PXIntState value,
    int[] allowedValues,
    string[] allowedLabels)
  {
    if (value == null)
      return (PXFieldState) null;
    PXTimeState instance = new PXTimeState((object) value);
    instance._AllowedImages = value.AllowedImages;
    instance._AllowedLabels = allowedLabels;
    instance._AllowedValues = allowedValues;
    instance._ExclusiveValues = value.ExclusiveValues;
    instance._EmptyPossible = value.EmptyPossible;
    instance._MaxValue = value.MaxValue;
    instance._MinValue = value.MinValue;
    instance._NeutralLabels = value._NeutralLabels;
    return (PXFieldState) instance;
  }
}
