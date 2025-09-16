// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXLiteSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

public class PXLiteSelectorAttribute : PXEventSubscriberAttribute
{
  protected Type _SearchType;
  protected Type _SubstituteKey;

  public Type SubstituteKey
  {
    get => this._SubstituteKey;
    set
    {
      this._SubstituteKey = value != (Type) null && typeof (IBqlField).IsAssignableFrom(value) && BqlCommand.GetItemType(value) == BqlCommand.GetItemType(this._SearchType) ? value : throw new PXArgumentException();
    }
  }

  public PXLiteSelectorAttribute(Type SearchType)
  {
    this._SearchType = SearchType != (Type) null && typeof (IBqlField).IsAssignableFrom(SearchType) ? SearchType : throw new PXArgumentException();
  }

  public virtual object GetValueExt(PXCache sender, object item)
  {
    List<PXDataField> pxDataFieldList = new List<PXDataField>();
    pxDataFieldList.Add(new PXDataField(this._SubstituteKey.Name));
    pxDataFieldList.Add((PXDataField) new PXDataFieldValue(this._SearchType.Name, sender.GetValue(item, this._FieldOrdinal)));
    TypeCode typeCode = Type.GetTypeCode(((PXFieldState) sender.Graph.Caches[BqlCommand.GetItemType(this._SearchType)].GetStateExt((object) null, this._SubstituteKey.Name)).DataType);
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(BqlCommand.GetItemType(this._SearchType), pxDataFieldList.ToArray()))
    {
      if (pxDataRecord == null)
        return (object) null;
      if (typeCode == TypeCode.String)
        return (object) pxDataRecord.GetString(0);
      throw new PXException();
    }
  }

  public static object GetValueExt(PXCache cache, object data, string name)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(name))
    {
      if (subscriberAttribute is PXLiteSelectorAttribute)
        return ((PXLiteSelectorAttribute) subscriberAttribute).GetValueExt(cache, data);
    }
    return (object) null;
  }
}
