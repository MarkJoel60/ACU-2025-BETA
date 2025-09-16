// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXDBCostScalarAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class PXDBCostScalarAttribute(Type search) : PXDBScalarAttribute(search)
{
  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    base.RowSelecting(sender, e);
    if (sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) != null)
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) 0M);
  }
}
