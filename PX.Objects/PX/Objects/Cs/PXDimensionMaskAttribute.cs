// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXDimensionMaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.CS;

public class PXDimensionMaskAttribute : PXDimensionAttribute
{
  protected string _Mask;
  protected string _defaultValue;
  protected string[] _allowedValues;
  protected string[] _allowedLabels;

  public PXDimensionMaskAttribute(
    string dimension,
    string mask,
    string defaultValue,
    string[] allowedValues,
    string[] allowedLabels)
    : base(dimension)
  {
    this._Mask = mask;
    if (allowedLabels.Length != allowedValues.Length)
      throw new ArgumentException();
    this._defaultValue = defaultValue;
    this._allowedValues = allowedValues;
    this._allowedLabels = allowedLabels;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    IEnumerable<Tuple<string, string>> source = ((IEnumerable<string>) this._allowedValues).Zip<string, string, Tuple<string, string>>((IEnumerable<string>) this._allowedLabels, (Func<string, string, Tuple<string, string>>) ((v, l) => new Tuple<string, string>(v, l))).Where<Tuple<string, string>>((Func<Tuple<string, string>, bool>) (t => !PXAccess.IsStringListValueDisabled(sender.GetItemType().FullName, ((PXEventSubscriberAttribute) this)._FieldName, t.Item1)));
    this._allowedValues = source.Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (p => p.Item1)).ToArray<string>();
    this._allowedLabels = source.Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (p => p.Item2)).ToArray<string>();
    string key = $"_{this._Mask}_Segments_";
    if (((Dictionary<string, PXView>) sender.Graph.Views).ContainsKey(key))
      return;
    // ISSUE: method pointer
    sender.Graph.Views[key] = new PXView(sender.Graph, true, (BqlCommand) new Select<PXDimensionAttribute.SegmentValue>(), (Delegate) new PXSelectDelegate<short?>((object) this, __methodptr(_MaskGetArgs)));
  }

  internal IEnumerable _MaskGetArgs([PXShort] short? segment)
  {
    PXDimensionMaskAttribute dimensionMaskAttribute = this;
    if (dimensionMaskAttribute._Definition.Dimensions.ContainsKey(dimensionMaskAttribute._Dimension) && segment.HasValue)
    {
      short? nullable1 = segment;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num = 0;
      if (!(nullable2.GetValueOrDefault() < num & nullable2.HasValue))
      {
        short? nullable3 = segment;
        nullable2 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
        int length = dimensionMaskAttribute._Definition.Values[dimensionMaskAttribute._Dimension].Length;
        if (!(nullable2.GetValueOrDefault() >= length & nullable2.HasValue))
        {
          PXSegment seg = dimensionMaskAttribute._Definition.Dimensions[dimensionMaskAttribute._Dimension][(int) segment.Value - 1];
          for (int i = 0; i < dimensionMaskAttribute._allowedValues.Length; ++i)
            yield return (object) new PXDimensionAttribute.SegmentValue(new string(char.Parse(dimensionMaskAttribute._allowedValues[i]), (int) seg.Length), PXMessages.LocalizeNoPrefix(dimensionMaskAttribute._allowedLabels[i]), new bool?(false));
        }
      }
    }
  }

  public virtual void SelfRowSelecting(
    PXCache sender,
    PXRowSelectingEventArgs e,
    PXSegment[] segs,
    int total)
  {
    char c = !string.IsNullOrEmpty(this._defaultValue) ? this._defaultValue[0] : ' ';
    object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (!(obj is string))
      return;
    string str1 = (string) obj;
    if (c != ' ')
      str1 = str1.TrimEnd();
    string str2;
    if (str1.Length < total)
    {
      str2 = str1 + new string(c, total - str1.Length);
    }
    else
    {
      if (str1.Length <= total)
        return;
      str2 = str1.Substring(0, total);
    }
    int index1 = 0;
    char[] charArray = str2.ToCharArray();
    foreach (PXSegment seg in segs)
    {
      char ch = charArray[index1];
      int num = index1 + (int) seg.Length;
      for (int index2 = index1; index2 < num; ++index2)
        charArray[index2] = ch;
      index1 += (int) seg.Length;
    }
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) new string(charArray));
  }

  public virtual void CopySegments(PXSegment[] segments, out PXSegment[] copy)
  {
    copy = new PXSegment[segments.Length];
    for (int index = 0; index < segments.Length; ++index)
      copy[index] = new PXSegment('A', ' ', segments[index].Length, true, (short) 1, (short) 0, segments[index].Separator, false, segments[index].Descr, '_');
  }

  public PXSegment[] CopySegments(PXSegment[] segments)
  {
    PXSegment[] copy;
    this.CopySegments(segments, out copy);
    return copy;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (((PXEventSubscriberAttribute) this)._AttributeLevel != 2 && !e.IsAltered)
      return;
    e.ReturnState = (object) PXSegmentedState.CreateInstance(e.ReturnState, ((PXEventSubscriberAttribute) this)._FieldName, this._Definition.Dimensions.ContainsKey(this._Dimension) ? this.CopySegments(this._Definition.Dimensions[this._Dimension]) : new PXSegment[0], $"_{this._Mask}_Segments_", new DimensionLookupMode?((DimensionLookupMode) 0), new bool?(false), this._Wildcard);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!this._Definition.Dimensions.ContainsKey(this._Dimension))
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("Dimension '{0}' does not exist in the system.", new object[1]
      {
        (object) this._Dimension
      }));
    int count = 0;
    for (int index = 0; index < this._Definition.Dimensions[this._Dimension].Length; ++index)
      count += (int) this._Definition.Dimensions[this._Dimension][index].Length;
    e.NewValue = (object) new string(char.Parse(this._defaultValue), count);
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this._Definition.Dimensions.ContainsKey(this._Dimension))
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("Dimension '{0}' does not exist in the system.", new object[1]
      {
        (object) this._Dimension
      }));
    if (e.NewValue == null)
      return;
    string str = (string) e.NewValue;
    List<string> stringList = new List<string>();
    for (int index1 = 0; index1 < this._Definition.Dimensions[this._Dimension].Length; ++index1)
    {
      string strB = str.Substring(0, (int) this._Definition.Dimensions[this._Dimension][index1].Length);
      bool flag = false;
      string[] allowedValues = this._allowedValues;
      int index2 = 0;
      while (index2 < allowedValues.Length && !(flag = new string(char.Parse(allowedValues[index2]), strB.Length).CompareTo(strB) == 0))
        ++index2;
      if (!flag)
        stringList.Add(strB);
      str = str.Substring((int) this._Definition.Dimensions[this._Dimension][index1].Length);
    }
    if (stringList.Count <= 0)
      return;
    if (stringList.Count == 1)
      throw new PXSetPropertyException("'{0}' of '{1}' does not exist in the system.", new object[2]
      {
        (object) stringList[0],
        (object) $"[{((PXEventSubscriberAttribute) this)._FieldName}]"
      });
    stringList.Add($"[{((PXEventSubscriberAttribute) this)._FieldName}]");
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    int num;
    for (num = 0; num < stringList.Count - 1; ++num)
    {
      stringBuilder1.Append('{');
      stringBuilder1.Append(num);
      stringBuilder1.Append('}');
      if (num < stringList.Count - 2)
        stringBuilder1.Append(", ");
    }
    stringBuilder2.Append('{');
    stringBuilder2.Append(num);
    stringBuilder2.Append('}');
    throw new PXSetPropertyException(string.Format(PXMessages.LocalizeFormat("{0} of {1} do not exist in the system.", new object[2]
    {
      (object) stringBuilder1.ToString(),
      (object) stringBuilder2.ToString()
    }), (object[]) stringList.ToArray()));
  }

  public void SynchronizeLabels(string[] allowedvalues, string[] allowedlabels)
  {
    for (int index1 = 0; index1 < allowedvalues.Length; ++index1)
    {
      int index2 = Array.IndexOf<string>(this._allowedValues, allowedvalues[index1]);
      if (index2 >= 0)
        this._allowedLabels[index2] = allowedlabels[index1];
    }
  }

  public void ReplaceValuesAndLabels(string[] allowedValues, string[] allowedLabels)
  {
    this._allowedValues = allowedValues;
    this._allowedLabels = allowedLabels;
  }

  public static string MakeSub(
    string mask,
    string[] _allowedValues,
    int DefaultValueIdx,
    params string[] sources)
  {
    string key = "SUBACCOUNT";
    PXDimensionAttribute.Definition definition = PXDimensionAttribute.GetDefinition(key);
    int num = definition.Dimensions[key].Cast<PXSegment>().Sum<PXSegment>((Func<PXSegment, int>) (x => (int) x.Length));
    mask = PXDimensionAttribute.AdjustValueLength(mask, num);
    for (int index = 0; index < sources.Length; ++index)
      sources[index] = PXDimensionAttribute.AdjustValueLength(sources[index], num);
    if (string.IsNullOrEmpty(mask) && DefaultValueIdx >= 0)
      return sources[DefaultValueIdx];
    int startIndex = 0;
    StringBuilder stringBuilder = new StringBuilder();
    for (int SourceIdx1 = 0; SourceIdx1 < definition.Dimensions[key].Length; ++SourceIdx1)
    {
      int length = (int) definition.Dimensions[key][SourceIdx1].Length;
      string strB = mask.Substring(startIndex, length);
      bool flag = false;
      for (int SourceIdx2 = 0; SourceIdx2 < _allowedValues.Length; ++SourceIdx2)
      {
        if (flag = new string(char.Parse(_allowedValues[SourceIdx2]), strB.Length).CompareTo(strB) == 0)
        {
          if (string.IsNullOrEmpty(sources[SourceIdx2]) || sources[SourceIdx2].Length < startIndex + length)
          {
            if (DefaultValueIdx < 0)
              throw new PXMaskArgumentException(SourceIdx2);
            if (string.IsNullOrEmpty(sources[DefaultValueIdx]) || sources[DefaultValueIdx].Length < startIndex + length)
              throw new PXMaskArgumentException(DefaultValueIdx);
            stringBuilder.Append(sources[DefaultValueIdx].Substring(startIndex, length));
            break;
          }
          stringBuilder.Append(sources[SourceIdx2].Substring(startIndex, length));
          break;
        }
      }
      if (!flag)
      {
        if (new string(' ', strB.Length).CompareTo(strB) != 0)
          throw new PXMaskValueException(SourceIdx1);
        stringBuilder.Append(strB);
      }
      startIndex += length;
    }
    return stringBuilder.ToString();
  }

  public static string MakeSub<Field>(
    PXGraph graph,
    string mask,
    string[] allowedValues,
    params object[] sourceIDs)
    where Field : IBqlField
  {
    return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, allowedValues, -1, sourceIDs);
  }

  public static string MakeSub<Field>(
    PXGraph graph,
    string mask,
    bool? stkItem,
    string[] allowedValues,
    params object[] sourceIDs)
    where Field : IBqlField
  {
    return stkItem.GetValueOrDefault() ? PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, allowedValues, -1, sourceIDs) : PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, allowedValues, 0, sourceIDs);
  }

  public static string MakeSub<Field>(
    PXGraph graph,
    string mask,
    string[] allowedValues,
    int DefaultValueIdx,
    params object[] sourceIDs)
    where Field : IBqlField
  {
    string[] strArray = new string[sourceIDs.Length];
    Dictionary<object, string> slot = PXDatabase.GetSlot<Dictionary<object, string>>("SubCDs", new Type[1]
    {
      typeof (PX.Objects.GL.Sub)
    });
    lock (((ICollection) slot).SyncRoot)
    {
      for (int index = 0; index < sourceIDs.Length; ++index)
      {
        if (sourceIDs[index] != null)
        {
          string str;
          if (!slot.TryGetValue(sourceIDs[index], out str))
          {
            PX.Objects.GL.Sub sub = PXResultset<PX.Objects.GL.Sub>.op_Implicit(PXSelectBase<PX.Objects.GL.Sub, PXSelect<PX.Objects.GL.Sub, Where<PX.Objects.GL.Sub.subID, Equal<Required<PX.Objects.GL.Sub.subID>>>>.Config>.Select(graph, new object[1]
            {
              sourceIDs[index]
            }));
            slot[sourceIDs[index]] = sub == null ? (str = (string) null) : (str = sub.SubCD);
          }
          strArray[index] = str;
        }
      }
    }
    try
    {
      return PXDimensionMaskAttribute.MakeSub(mask, allowedValues, DefaultValueIdx, strArray);
    }
    catch (PXMaskValueException ex)
    {
      throw new PXMaskValueException(new object[2]
      {
        (object) ++ex.SourceIdx,
        (object) PXUIFieldAttribute.GetDisplayName(graph.Caches[BqlCommand.GetItemType(typeof (Field))], typeof (Field).Name.ToLower())
      });
    }
  }
}
