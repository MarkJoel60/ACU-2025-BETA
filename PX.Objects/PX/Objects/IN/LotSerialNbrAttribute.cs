// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.LotSerialNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[PXDBString(100, IsUnicode = true)]
[PXUIField(DisplayName = "Lot/Serial Nbr.", FieldClass = "LotSerial")]
public class LotSerialNbrAttribute : PXAggregateAttribute
{
  protected int _DBAttrIndex = -1;

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXDBStringAttribute attribute = (PXDBStringAttribute) this._Attributes[this._DBAttrIndex];
    if (attribute == null)
      return;
    attribute.InputMask = new string('C', attribute.Length);
    attribute.PromptChar = new char?(' ');
  }

  public LotSerialNbrAttribute()
  {
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
    {
      if (attribute is PXDBFieldAttribute)
      {
        this._DBAttrIndex = ((List<PXEventSubscriberAttribute>) this._Attributes).IndexOf(attribute);
        break;
      }
    }
  }

  public virtual bool IsKey
  {
    get => ((PXDBFieldAttribute) this._Attributes[this._DBAttrIndex]).IsKey;
    set => ((PXDBFieldAttribute) this._Attributes[this._DBAttrIndex]).IsKey = value;
  }

  public virtual Type BqlField
  {
    get => ((PXDBFieldAttribute) this._Attributes[this._DBAttrIndex]).BqlField;
    set
    {
      PXDBStringAttribute attribute = (PXDBStringAttribute) this._Attributes[this._DBAttrIndex];
      ((PXDBFieldAttribute) attribute).BqlField = value;
      ((PXEventSubscriberAttribute) this).BqlTable = ((PXEventSubscriberAttribute) attribute).BqlTable;
    }
  }
}
