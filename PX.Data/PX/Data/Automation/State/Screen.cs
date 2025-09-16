// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.Screen
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Process.Automation.State;

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class Screen
{
  public readonly StateMap<ScreenCondition> Conditions = new StateMap<ScreenCondition>();
  public readonly StateMap<ScreenActionBase> Actions = new StateMap<ScreenActionBase>();
  public readonly StateMap<ScreenTable> Tables = new StateMap<ScreenTable>();
  public bool IsCustomized;

  public bool IsEmpty()
  {
    return this.Conditions.IsEmpty() && this.Actions.IsEmpty() && this.Tables.IsEmpty();
  }
}
