// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.ValueLabelPair
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

public struct ValueLabelPair
{
  public string Value { get; private set; }

  public string Label { get; private set; }

  /// <summary>
  /// Initializes a new instance of <see cref="T:PX.Objects.Common.ValueLabelPair" /> using a
  /// value and its corresponding label.
  /// </summary>
  public ValueLabelPair(string value, string label)
  {
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    if (label == null)
      throw new ArgumentNullException(nameof (label));
    this.Value = value;
    this.Label = label;
  }

  public class KeyComparer : IEqualityComparer<ValueLabelPair>
  {
    public bool Equals(ValueLabelPair x, ValueLabelPair y) => string.Equals(x.Value, y.Value);

    public int GetHashCode(ValueLabelPair valueLabelPair) => valueLabelPair.Value.GetHashCode();
  }
}
