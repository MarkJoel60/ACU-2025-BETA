// Decompiled with JetBrains decompiler
// Type: PX.Api.OrderedDictionaryD
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

#nullable disable
namespace PX.Api;

public class OrderedDictionaryD : OrderedDictionary
{
  public OrderedDictionaryD()
    : base((IEqualityComparer) StringComparer.OrdinalIgnoreCase)
  {
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    bool flag = true;
    foreach (DictionaryEntry dictionaryEntry in (OrderedDictionary) this)
    {
      if (flag)
        flag = false;
      else
        stringBuilder.Append(", ");
      stringBuilder.AppendLine($"{dictionaryEntry.Key}={dictionaryEntry.Value}");
    }
    return stringBuilder.ToString();
  }
}
