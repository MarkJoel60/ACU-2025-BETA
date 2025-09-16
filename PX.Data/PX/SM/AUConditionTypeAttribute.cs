// Decompiled with JetBrains decompiler
// Type: PX.SM.AUConditionTypeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public class AUConditionTypeAttribute : PXIntListAttribute
{
  public AUConditionTypeAttribute()
    : base(typeof (AUConditionType), new string[12]
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
      "Is Between",
      "Is Empty",
      "Is Not Empty"
    })
  {
  }
}
