// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Attributes.CopiedShipmentNoteIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Attributes;
using System;

#nullable disable
namespace PX.Objects.SO.Attributes;

public class CopiedShipmentNoteIDAttribute : CopiedNoteIDAttribute
{
  public CopiedShipmentNoteIDAttribute()
    : base((Type) null)
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1. The constructor is obsolete. Use the parameterless constructor instead. The CopiedShipmentNoteIDAttribute constructor is exactly the same as the parameterless one. It does not provide any additional functionality and does not save values of provided fields in the note. The constructor will be removed in a future version of Acumatica ERP.")]
  public CopiedShipmentNoteIDAttribute(params Type[] searches)
    : base((Type) null, searches)
  {
  }

  protected override string GetEntityType(PXCache cache, Guid? noteId)
  {
    PXSelect<SOOrderShipment, Where<SOOrderShipment.shippingRefNoteID, Equal<Required<Note.noteID>>>> pxSelect = new PXSelect<SOOrderShipment, Where<SOOrderShipment.shippingRefNoteID, Equal<Required<Note.noteID>>>>(cache.Graph);
    SOOrderShipment row = PXResultset<SOOrderShipment>.op_Implicit(((PXSelectBase<SOOrderShipment>) pxSelect).Select(new object[1]
    {
      (object) noteId
    }));
    if (row != null)
    {
      Type targetType;
      ShippingRefNoteAttribute.GetTargetTypeAndKeys(((PXSelectBase) pxSelect).Cache, (object) row, out targetType, out object[] _);
      if (targetType != (Type) null)
        return targetType.FullName;
    }
    return cache.GetItemType().FullName;
  }
}
