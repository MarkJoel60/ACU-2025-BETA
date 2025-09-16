// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CombinedStringListsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// Combines all <see cref="T:PX.Data.PXStringListAttribute" /> from specified fields.
/// For proper work use <see cref="M:PX.Objects.CR.CombinedStringListsAttribute.CoalesceConcatValues(System.String[])" />
/// or child attribute with custom db query: <see cref="T:PX.Objects.CR.CombinedDBStringListsAttribute" />.
/// First not null value will be used, so order is important.
/// </summary>
public class CombinedStringListsAttribute : PXStringListAttribute
{
  public const string ListsPrefix = "L__";

  public CombinedStringListsAttribute(params System.Type[] fields)
  {
    this.Fields = ((IEnumerable<System.Type>) fields).ToList<System.Type>();
    this.IsLocalizable = false;
  }

  protected List<System.Type> Fields { get; }

  protected static string GetValueWithPrefix(string value, int fieldIndex)
  {
    return CombinedStringListsAttribute.GetPrefix(fieldIndex) + value;
  }

  public static string GetPrefix(int fieldIndex) => fieldIndex.ToString() + "L__";

  public static string CoalesceConcatValues(params string[] values)
  {
    for (int fieldIndex = 0; fieldIndex < values.Length; ++fieldIndex)
    {
      if (values[fieldIndex] != null)
        return CombinedStringListsAttribute.GetValueWithPrefix(values[fieldIndex], fieldIndex);
    }
    return (string) null;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    IEnumerable<(string, string)> source = this.Fields.Select<System.Type, (string, IEnumerable<PXStringListAttribute>)>((Func<System.Type, (string, IEnumerable<PXStringListAttribute>)>) (f => (f.Name, sender.GetAttributesOfType<PXStringListAttribute>((object) null, f.Name)))).SelectMany<(string, IEnumerable<PXStringListAttribute>), (string, string)>((Func<(string, IEnumerable<PXStringListAttribute>), int, IEnumerable<(string, string)>>) ((l, index) => GetValues(l.Name, l.Attributes, index)));
    this._AllowedValues = source.Select<(string, string), string>((Func<(string, string), string>) (f => f.value)).ToArray<string>();
    this._AllowedLabels = source.Select<(string, string), string>((Func<(string, string), string>) (f => f.label)).ToArray<string>();
    base.CacheAttached(sender);

    static IEnumerable<(string value, string label)> GetValues(
      string fieldName,
      IEnumerable<PXStringListAttribute> list,
      int fieldIndex)
    {
      PXStringListAttribute stringListAttribute = list.FirstOrDefault<PXStringListAttribute>();
      return (stringListAttribute != null ? stringListAttribute.ValueLabelDic.Select<KeyValuePair<string, string>, (string, string)>((Func<KeyValuePair<string, string>, (string, string)>) (f => (CombinedStringListsAttribute.GetValueWithPrefix(f.Key, fieldIndex), f.Value))) : (IEnumerable<(string, string)>) null) ?? throw new InvalidOperationException("There are no defined PXStringListAttribute on field " + fieldName);
    }
  }
}
