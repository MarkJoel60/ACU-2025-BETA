// Decompiled with JetBrains decompiler
// Type: PX.Data.FilterVariable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

internal class FilterVariable
{
  public const string CURRENT_USER = "@me";
  public const string CURRENT_USER_GROUPS = "@mygroups";
  public const string CURRENT_USER_GROUPS_TREE = "@myworktree";
  public const string CURRENT_BRANCH = "@branch";
  public const string CURRENT_ORGANIZATION = "@company";
  private static readonly Dictionary<string, FilterVariable.VariableDetails> variableInfo = new Dictionary<string, FilterVariable.VariableDetails>()
  {
    {
      "@me",
      new FilterVariable.VariableDetails(new PXCondition[2]
      {
        PXCondition.EQ,
        PXCondition.NE
      }, FilterVariableType.CurrentUser)
    },
    {
      "@mygroups",
      new FilterVariable.VariableDetails(new PXCondition[2]
      {
        PXCondition.IN,
        PXCondition.NI
      }, FilterVariableType.CurrentUserGroups)
    },
    {
      "@myworktree",
      new FilterVariable.VariableDetails(new PXCondition[2]
      {
        PXCondition.IN,
        PXCondition.NI
      }, FilterVariableType.CurrentUserGroupsTree)
    },
    {
      "@branch",
      new FilterVariable.VariableDetails(new PXCondition[2]
      {
        PXCondition.EQ,
        PXCondition.NE
      }, FilterVariableType.CurrentBranch)
    },
    {
      "@company",
      new FilterVariable.VariableDetails(new PXCondition[2]
      {
        PXCondition.IN,
        PXCondition.NI
      }, FilterVariableType.CurrentOrganization)
    }
  };

  public static FilterVariableType? GetVariableType(string variable)
  {
    if (!string.IsNullOrEmpty(variable))
    {
      string lower = variable.ToLower();
      if (FilterVariable.variableInfo.ContainsKey(lower))
        return new FilterVariableType?(FilterVariable.variableInfo[lower].Type);
    }
    return new FilterVariableType?();
  }

  public static string GetConditionViolationMessage(string variable, PXCondition condition)
  {
    string violationMessage = (string) null;
    if (!string.IsNullOrEmpty(variable))
    {
      string lower = variable.ToLower();
      if (FilterVariable.variableInfo.ContainsKey(lower))
      {
        int num = lower == "@mygroups" || lower == "@myworktree" ? 1 : (lower == "@company" ? 1 : 0);
        if (!((IEnumerable<PXCondition>) FilterVariable.variableInfo[lower].Conditions).Contains<PXCondition>(condition))
        {
          switch (lower)
          {
            case "@me":
            case "@branch":
              violationMessage = string.Format(PXMessages.LocalizeNoPrefix("The variables {0} and {1} can be used only in conjunction with condition '{2}' or '{3}' only."), (object) "@me", (object) "@branch", (object) "Equals", (object) "Does Not Equal");
              break;
            case "@mygroups":
            case "@myworktree":
            case "@company":
              violationMessage = string.Format(PXMessages.LocalizeNoPrefix("The variables {0}, {1}, and {2} can be used in conjunction with the conditions '{3}' or '{4}' only."), (object) "@mygroups", (object) "@myworktree", (object) "@company", (object) "Is In", (object) "Is Not In");
              break;
          }
        }
      }
    }
    return violationMessage;
  }

  private class VariableDetails
  {
    public PXCondition[] Conditions { get; private set; }

    public FilterVariableType Type { get; private set; }

    public VariableDetails(PXCondition[] conditions, FilterVariableType type)
    {
      this.Conditions = conditions;
      this.Type = type;
    }
  }
}
