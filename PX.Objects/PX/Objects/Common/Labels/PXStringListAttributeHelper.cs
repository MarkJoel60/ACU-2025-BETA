// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Labels.PXStringListAttributeHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.Labels;

public class PXStringListAttributeHelper
{
  public static void SetList<TField>(PXCache cache, object row, ILabelProvider labelProvider) where TField : IBqlField
  {
    PXStringListAttribute.SetList<TField>(cache, row, labelProvider.ValueLabelPairs.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (kvp => kvp.Value)).ToArray<string>(), labelProvider.ValueLabelPairs.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (kvp => kvp.Label)).ToArray<string>());
  }
}
