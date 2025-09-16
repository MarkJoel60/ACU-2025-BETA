// Decompiled with JetBrains decompiler
// Type: PX.SM.ExchangeColorListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Update.WebServices;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.SM;

public class ExchangeColorListAttribute : PXStringListAttribute
{
  public ExchangeColorListAttribute()
    : base(ExchangeColorListAttribute.GetValues(), ExchangeColorListAttribute.GetLabels())
  {
  }

  private static string[] GetValues() => Enum.GetNames(typeof (CategoryColor));

  private static string[] GetLabels()
  {
    List<string> stringList = new List<string>();
    foreach (string str in ExchangeColorListAttribute.GetValues())
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < str.Length; ++index)
      {
        if (index != 0 && char.IsUpper(str[index]))
          stringBuilder.Append(" ");
        stringBuilder.Append(str[index]);
      }
      stringList.Add(stringBuilder.ToString());
    }
    return stringList.ToArray();
  }
}
