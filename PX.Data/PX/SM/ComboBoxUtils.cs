// Decompiled with JetBrains decompiler
// Type: PX.SM.ComboBoxUtils
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public static class ComboBoxUtils
{
  public static void PopulateFileTypes<TField>(PXCache cache, string valueList, object data) where TField : IBqlField
  {
    List<int> AllowedValues = new List<int>();
    List<string> AllowedLabels = new List<string>();
    ComboBoxUtils.GetComboValues(valueList, ref AllowedValues, ref AllowedLabels);
    PXIntListAttribute.SetList<TField>(cache, data, AllowedValues.ToArray(), AllowedLabels.ToArray());
  }

  private static void GetComboValues(
    string valueList,
    ref List<int> AllowedValues,
    ref List<string> AllowedLabels)
  {
    if (string.IsNullOrEmpty(valueList))
      return;
    string str1 = valueList;
    char[] chArray = new char[1]{ ';' };
    foreach (string str2 in str1.Split(chArray))
    {
      if (!string.IsNullOrEmpty(str2))
      {
        string[] strArray = str2.Split('|');
        if (strArray.Length == 2)
        {
          AllowedValues.Add(Convert.ToInt32(strArray[0]));
          AllowedLabels.Add(strArray[1]);
        }
      }
    }
  }
}
