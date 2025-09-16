// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.LabelListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public class LabelListAttribute : PXStringListAttribute
{
  public LabelListAttribute(Type labelProviderType)
  {
    if (!typeof (ILabelProvider).IsAssignableFrom(labelProviderType))
      throw new PXException("The specified type '{0}' must implement the ILabelProvider interface.", new object[1]
      {
        (object) labelProviderType.Name
      });
    try
    {
      ILabelProvider instance = Activator.CreateInstance(labelProviderType) as ILabelProvider;
      List<string> values = new List<string>();
      List<string> labels = new List<string>();
      EnumerableExtensions.ForEach<ValueLabelPair>(instance.ValueLabelPairs, (Action<ValueLabelPair>) (pair =>
      {
        values.Add(pair.Value);
        labels.Add(pair.Label);
      }));
      this._AllowedValues = values.ToArray();
      this._AllowedLabels = labels.ToArray();
      this._NeutralAllowedLabels = this._AllowedLabels;
    }
    catch (MissingMethodException ex)
    {
      object[] objArray = new object[1]
      {
        (object) labelProviderType.Name
      };
      throw new PXException((Exception) ex, "The label provider class '{0}' must have a parameterless constructor.", objArray);
    }
  }

  public LabelListAttribute(IEnumerable<ValueLabelPair> valueLabelPairs)
    : base(valueLabelPairs.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Value)).ToArray<string>(), valueLabelPairs.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Label)).ToArray<string>())
  {
  }
}
