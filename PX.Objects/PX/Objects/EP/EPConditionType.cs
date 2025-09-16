// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPConditionType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.EP;

public class EPConditionType : PXIntListAttribute
{
  public EPConditionType()
    : base(new int[13]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      10,
      6,
      9,
      7,
      8,
      11,
      12
    }, new string[13]
    {
      "Equals",
      "Does Not Equal",
      "Is Greater Than",
      "Is Greater Than or Equal To",
      "Is Less Than",
      "Is Less Than or Equal To",
      "Is Between",
      "Contains",
      "Does Not Contain",
      "Starts With",
      "Ends With",
      "Is Empty",
      "Is Not Empty"
    })
  {
  }
}
