// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.ValueLabelList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public class ValueLabelList : IEnumerable<ValueLabelPair>, IEnumerable
{
  private readonly List<ValueLabelPair> _valueLabelPairs = new List<ValueLabelPair>();

  public ValueLabelList()
  {
  }

  public ValueLabelList(IEnumerable<ValueLabelPair> valueLabelPairs)
  {
    this._valueLabelPairs = valueLabelPairs.ToList<ValueLabelPair>();
  }

  public void Add(string value, string label)
  {
    this._valueLabelPairs.Add(new ValueLabelPair(value, label));
  }

  public void Add(IEnumerable<ValueLabelPair> list) => this._valueLabelPairs.AddRange(list);

  public IEnumerator<ValueLabelPair> GetEnumerator()
  {
    return (IEnumerator<ValueLabelPair>) this._valueLabelPairs.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._valueLabelPairs.GetEnumerator();
}
