// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.Automation.Services.NavigationValueEvaluator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Automation.Services;
using PX.Data.Automation.State;

#nullable disable
namespace PX.Data.Process.Automation.Services;

internal class NavigationValueEvaluator : INavigationValueEvaluator
{
  public object Evaluate(PXCache cache, object row, ScreenNavigationParameter parameter)
  {
    if (parameter.IsFromSchema)
      return (object) parameter.Value;
    string s = parameter.Value;
    string[] strArray = s != null ? s.Split('.', 2) : (string[]) null;
    string fieldName = strArray == null || strArray.Length < 2 ? parameter.Value : strArray[1];
    return PXFieldState.UnwrapValue(cache.GetValueExt(row, fieldName));
  }
}
