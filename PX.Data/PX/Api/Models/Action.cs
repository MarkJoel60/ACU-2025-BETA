// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.Action
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Api.Models;

public class Action : Command
{
  public string ViewTypeName { get; set; }

  public class FieldNameComparer : IEqualityComparer<Action>
  {
    private string CleanFieldName(Action action)
    {
      if (!action.FieldName.Contains("@"))
        return action.FieldName;
      return action.FieldName.Split('@')[0];
    }

    public bool Equals(Action action, Action actionToCompare)
    {
      return this.CleanFieldName(action).OrdinalEquals(this.CleanFieldName(actionToCompare)) && action.ObjectName.OrdinalEquals(actionToCompare.ObjectName);
    }

    public int GetHashCode(Action action)
    {
      return (this.CleanFieldName(action) + action.ObjectName).GetHashCode();
    }
  }
}
