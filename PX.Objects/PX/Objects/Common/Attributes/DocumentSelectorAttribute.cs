// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.DocumentSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

public class DocumentSelectorAttribute : PXSelectorAttribute
{
  public DocumentSelectorAttribute(Type type)
    : base(type)
  {
  }

  public DocumentSelectorAttribute(Type type, params Type[] fieldList)
    : base(type, fieldList)
  {
  }

  public virtual void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    object newValue = e.NewValue;
    sender.Graph.Caches[BqlCommand.GetItemType(this._SubstituteKey)].RaiseFieldUpdating(this._SubstituteKey.Name, (object) null, ref newValue);
    e.NewValue = newValue;
    base.SubstituteKeyFieldUpdating(sender, e);
  }

  public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.SubstituteKeyFieldSelecting(sender, e);
    object returnValue = e.ReturnValue;
    sender.Graph.Caches[BqlCommand.GetItemType(this._SubstituteKey)].RaiseFieldSelecting(this._SubstituteKey.Name, (object) null, ref returnValue, false);
    e.ReturnValue = returnValue;
  }
}
