// Decompiled with JetBrains decompiler
// Type: PX.Data.PXInactiveFieldState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXInactiveFieldState : PXFieldState
{
  protected PXInactiveFieldState(object value)
    : base(value)
  {
  }

  public static PXFieldState CreateInstance(string fieldName, string displayName)
  {
    return (PXFieldState) new PXInactiveFieldState((object) PXFieldState.CreateInstance((object) null, typeof (object), new bool?(false), new bool?(true), fieldName: fieldName, displayName: displayName, enabled: new bool?(false), visible: new bool?(false), readOnly: new bool?(true), visibility: PXUIVisibility.Invisible));
  }
}
