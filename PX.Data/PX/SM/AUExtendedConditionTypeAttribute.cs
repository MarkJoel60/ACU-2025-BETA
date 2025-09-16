// Decompiled with JetBrains decompiler
// Type: PX.SM.AUExtendedConditionTypeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public class AUExtendedConditionTypeAttribute : PXIntListAttribute
{
  public AUExtendedConditionTypeAttribute()
    : base(new int[15]
    {
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      22,
      23
    }, new string[15]
    {
      "Equals",
      "Does Not Equal",
      "Is Greater Than",
      "Is Greater Than or Equal To",
      "Is Less Than",
      "Is Less Than or Equal To",
      "Contains",
      "Starts With",
      "Ends With",
      "Does Not Contain",
      "Is Between",
      "Is Empty",
      "Is Not Empty",
      "Is In",
      "Is Not In"
    })
  {
  }
}
