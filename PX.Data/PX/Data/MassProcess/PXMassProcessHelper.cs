// Decompiled with JetBrains decompiler
// Type: PX.Data.MassProcess.PXMassProcessHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.MassProcess;

public static class PXMassProcessHelper
{
  public static PXFieldState InitValueFieldState(PXCache cache, FieldValue field = null)
  {
    if (field != null && field.Name != null && cache.GetStateExt((object) null, field.Name) is PXFieldState stateExt)
    {
      stateExt.SetFieldName("value");
      stateExt.Value = (object) field.Value;
      stateExt.Enabled = true;
      return stateExt;
    }
    PXFieldState instance = PXStringState.CreateInstance((object) null, new int?(), new bool?(), "value", new bool?(false), new int?(0), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
    instance.DisplayName = "Value";
    return instance;
  }
}
