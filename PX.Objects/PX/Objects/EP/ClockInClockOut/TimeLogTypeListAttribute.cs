// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.TimeLogTypeListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public class TimeLogTypeListAttribute : PXStringListAttribute, IPXRowSelectedSubscriber
{
  void IPXRowSelectedSubscriber.RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    foreach (PXResult<EPTimeLogType> pxResult in PXSelectBase<EPTimeLogType, PXSelect<EPTimeLogType>.Config>.Select(sender.Graph))
    {
      EPTimeLogType epTimeLogType = (EPTimeLogType) pxResult;
      stringList1.Add(epTimeLogType.TimeLogTypeID);
      stringList2.Add(epTimeLogType.Description);
    }
    PXStringListAttribute.SetList(sender, e.Row, this._FieldName, stringList1.ToArray(), stringList2.ToArray());
  }
}
