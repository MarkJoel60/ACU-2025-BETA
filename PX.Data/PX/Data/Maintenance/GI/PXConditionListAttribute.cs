// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.PXConditionListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Maintenance.GI;

public class PXConditionListAttribute : PXStringListAttribute
{
  public static Dictionary<string, string> Conditions = new Dictionary<string, string>()
  {
    {
      "E",
      "Equals"
    },
    {
      "NE",
      "Does Not Equal"
    },
    {
      "G",
      "Is Greater Than"
    },
    {
      "GE",
      "Is Greater Than or Equal To"
    },
    {
      "L",
      "Is Less Than"
    },
    {
      "LE",
      "Is Less Than or Equal To"
    },
    {
      "B",
      "Is Between"
    },
    {
      "LI",
      "Contains"
    },
    {
      "NL",
      "Does Not Contain"
    },
    {
      "RL",
      "Starts With"
    },
    {
      "LL",
      "Ends With"
    },
    {
      "NU",
      "Is Empty"
    },
    {
      "NN",
      "Is Not Empty"
    },
    {
      "IN",
      "Is In"
    },
    {
      "NI",
      "Is Not In"
    }
  };

  public PXConditionListAttribute()
    : base(PXConditionListAttribute.Conditions.Keys.ToArray<string>(), PXConditionListAttribute.Conditions.Values.ToArray<string>())
  {
  }

  public static class ConditionValues
  {
    public const string EqualsTo = "E";
    public const string NotEqualsTo = "NE";
    public const string GreaterThan = "G";
    public const string GreateThanOrEqualsTo = "GE";
    public const string LessThan = "L";
    public const string LessThanOrEqualsTo = "LE";
    public const string Between = "B";
    public const string Like = "LI";
    public const string NotLike = "NL";
    public const string RightLike = "RL";
    public const string LeftLike = "LL";
    public const string IsNull = "NU";
    public const string IsNotNull = "NN";
    public const string In = "IN";
    public const string NotIn = "NI";
  }
}
