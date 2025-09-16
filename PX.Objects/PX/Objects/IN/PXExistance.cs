// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXExistance
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public class PXExistance : PXBoolAttribute, IPXRowSelectingSubscriber
{
  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    foreach (string key in (IEnumerable<string>) sender.Keys)
    {
      if (sender.GetValue(e.Row, key) == null)
        return;
    }
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) true);
  }
}
