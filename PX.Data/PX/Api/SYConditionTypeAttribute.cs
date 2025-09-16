// Decompiled with JetBrains decompiler
// Type: PX.Api.SYConditionTypeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Api;

public class SYConditionTypeAttribute(int[] intList = null) : PXIntListAttribute(intList ?? SYConditionTypeAttribute.defaultIntList, intList == null ? SYConditionTypeAttribute.GetInfoMessages(SYConditionTypeAttribute.defaultIntList) : SYConditionTypeAttribute.GetInfoMessages(intList))
{
  public static int[] defaultIntList = new int[13]
  {
    1,
    2,
    3,
    4,
    5,
    6,
    11,
    7,
    8,
    9,
    12,
    13,
    10
  };

  public static string[] GetInfoMessages(int[] intList)
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < intList.Length; ++index)
    {
      switch (intList[index])
      {
        case 1:
          stringList.Add("Equals");
          break;
        case 2:
          stringList.Add("Does Not Equal");
          break;
        case 3:
          stringList.Add("Is Greater Than");
          break;
        case 4:
          stringList.Add("Is Greater Than or Equal To");
          break;
        case 5:
          stringList.Add("Is Less Than");
          break;
        case 6:
          stringList.Add("Is Less Than or Equal To");
          break;
        case 7:
          stringList.Add("Contains");
          break;
        case 8:
          stringList.Add("Starts With");
          break;
        case 9:
          stringList.Add("Ends With");
          break;
        case 10:
          stringList.Add("Does Not Contain");
          break;
        case 11:
          stringList.Add("Is Between");
          break;
        case 12:
          stringList.Add("Is Empty");
          break;
        case 13:
          stringList.Add("Is Not Empty");
          break;
      }
    }
    return stringList.ToArray();
  }
}
