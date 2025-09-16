// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCustomDimensionSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <summary>The base class for custom dimension selector attributes.
/// Derive the attribute class from this class and implement the
/// <tt>GetRecords()</tt> method.</summary>
public class PXCustomDimensionSelectorAttribute : PXDimensionSelectorAttribute
{
  /// <summary>Initializes a new instance of the attribute.</summary>
  public PXCustomDimensionSelectorAttribute(string dimension, System.Type type)
    : base(dimension, type)
  {
    PXSelectorAttribute selectorAttribute = (PXSelectorAttribute) new PXCustomDimensionSelectorAttribute.selectorAttribute(type, new Func<IEnumerable>(this.GetRecords));
    this._Attributes.RemoveAt(this._Attributes.Count - 1);
    this._Attributes.Add((PXEventSubscriberAttribute) selectorAttribute);
  }

  /// <summary>Initializes a new instance of the attribute.</summary>
  public PXCustomDimensionSelectorAttribute(string dimension, System.Type type, System.Type substituteKey)
    : base(dimension, type, substituteKey)
  {
    PXSelectorAttribute selectorAttribute = (PXSelectorAttribute) new PXCustomDimensionSelectorAttribute.selectorAttribute(type, new Func<IEnumerable>(this.GetRecords));
    selectorAttribute.SubstituteKey = substituteKey;
    this._Attributes.RemoveAt(this._Attributes.Count - 1);
    this._Attributes.Add((PXEventSubscriberAttribute) selectorAttribute);
  }

  /// <summary>Initializes a new instance of the attribute.</summary>
  public PXCustomDimensionSelectorAttribute(
    string dimension,
    System.Type type,
    System.Type substituteKey,
    params System.Type[] fieldList)
    : base(dimension, type, substituteKey, fieldList)
  {
    PXSelectorAttribute selectorAttribute = (PXSelectorAttribute) new PXCustomDimensionSelectorAttribute.selectorAttribute(type, fieldList, new Func<IEnumerable>(this.GetRecords));
    selectorAttribute.SubstituteKey = substituteKey;
    this._Attributes.RemoveAt(this._Attributes.Count - 1);
    this._Attributes.Add((PXEventSubscriberAttribute) selectorAttribute);
  }

  public virtual IEnumerable GetRecords()
  {
    yield break;
  }

  protected class selectorAttribute : PXCustomSelectorAttribute
  {
    private readonly Func<IEnumerable> _delegate;

    public selectorAttribute(System.Type searchType, Func<IEnumerable> @delegate)
      : base(searchType)
    {
      this._delegate = @delegate != null ? @delegate : throw new ArgumentNullException(nameof (@delegate));
    }

    public selectorAttribute(System.Type searchType, System.Type[] fieldList, Func<IEnumerable> @delegate)
      : base(searchType, fieldList)
    {
      this._delegate = @delegate != null ? @delegate : throw new ArgumentNullException(nameof (@delegate));
    }

    public IEnumerable GetRecords() => this._delegate();
  }
}
