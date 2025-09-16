// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.PXSelectorMarkerAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// PXSelector Marker to be used in PXFormula when a field cannot be decorated with PXSelector Attribute (Example: A field is already defined with StringList attribute to render a Combo box).
/// </summary>
public class PXSelectorMarkerAttribute(Type type) : PXSelectorAttribute(type)
{
  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    if (typeof (ISubscriber) == typeof (IPXFieldSelectingSubscriber) || typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber))
      return;
    ((PXEventSubscriberAttribute) this).GetSubscriber<ISubscriber>(subscribers);
  }
}
