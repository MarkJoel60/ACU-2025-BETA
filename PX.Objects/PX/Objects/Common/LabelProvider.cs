// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.LabelProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public static class LabelProvider
{
  public static string GetLabel(this ILabelProvider provider, string value)
  {
    return provider.ValueLabelPairs.Single<ValueLabelPair>((Func<ValueLabelPair, bool>) (pair => pair.Value == value)).Label;
  }

  public static IEnumerable<string> Values(this ILabelProvider provider)
  {
    return provider.ValueLabelPairs.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Value));
  }

  public static IEnumerable<string> Labels(this ILabelProvider provider)
  {
    return provider.ValueLabelPairs.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Label));
  }
}
