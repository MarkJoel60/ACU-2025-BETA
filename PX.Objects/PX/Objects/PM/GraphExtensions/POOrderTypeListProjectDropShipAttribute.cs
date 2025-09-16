// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.POOrderTypeListProjectDropShipAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.PO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class POOrderTypeListProjectDropShipAttribute : POOrderType.ListAttribute
{
  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (ServiceLocator.Current.GetInstance<IProjectSettingsManager>().IsPMVisible("PO"))
      return;
    int index = Array.IndexOf<string>(this._AllowedValues, "PD");
    if (index < 0)
      return;
    List<string> stringList1 = new List<string>((IEnumerable<string>) this._AllowedValues);
    stringList1.RemoveAt(index);
    this._AllowedValues = stringList1.ToArray();
    List<string> stringList2 = new List<string>((IEnumerable<string>) this._AllowedLabels);
    stringList2.RemoveAt(index);
    this._AllowedLabels = stringList2.ToArray();
  }
}
