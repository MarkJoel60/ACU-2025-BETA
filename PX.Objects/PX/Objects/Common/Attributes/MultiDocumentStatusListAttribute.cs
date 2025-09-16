// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.MultiDocumentStatusListAttribute
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
namespace PX.Objects.Common.Attributes;

public class MultiDocumentStatusListAttribute : PXStringListAttribute
{
  protected Type[] _documentStatusFieldList;

  public MultiDocumentStatusListAttribute(params Type[] documentStatusFieldList)
  {
    this._documentStatusFieldList = documentStatusFieldList != null && documentStatusFieldList.Length > 1 ? documentStatusFieldList : throw new PXArgumentException(nameof (documentStatusFieldList));
    this.IsLocalizable = false;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.FillDocumentStatusList(sender);
  }

  protected virtual void FillDocumentStatusList(PXCache cache)
  {
    string[] allowedValues = this._AllowedValues;
    if ((allowedValues != null ? (!((IEnumerable<string>) allowedValues).Any<string>((Func<string, bool>) (v => v != null)) ? 1 : 0) : 1) == 0)
      return;
    Dictionary<string, string> result = new Dictionary<string, string>();
    foreach (Type documentStatusField in this._documentStatusFieldList)
      this.CopyDocumentStatusValues(cache.Graph, documentStatusField, result);
    this._AllowedValues = result.Keys.ToArray<string>();
    this._AllowedLabels = result.Values.ToArray<string>();
  }

  protected virtual void CopyDocumentStatusValues(
    PXGraph graph,
    Type documentStatusField,
    Dictionary<string, string> result)
  {
    PXCache documentCache = graph.Caches[BqlCommand.GetItemType(documentStatusField)];
    PXStringListAttribute stringListAttribute = documentCache.GetAttributesReadonly(documentStatusField.Name).OfType<PXStringListAttribute>().FirstOrDefault<PXStringListAttribute>();
    if (stringListAttribute == null)
      return;
    EnumerableExtensions.AddRange<string, string>((IDictionary<string, string>) result, stringListAttribute.ValueLabelDic.Select<KeyValuePair<string, string>, KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, KeyValuePair<string, string>>) (documentStatus => new KeyValuePair<string, string>(this.GetDocumentStatusValue(documentCache, documentStatus.Key), $"{documentCache.DisplayName} - {PXMessages.LocalizeNoPrefix(documentStatus.Value)}"))));
  }

  public virtual string GetDocumentStatusValue(PXCache documentCache, string documentStatusValue)
  {
    return $"{GraphHelper.GetName(documentCache)}~{documentStatusValue}";
  }
}
