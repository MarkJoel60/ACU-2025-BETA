// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderTypeApproval
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
namespace PX.Objects.SO;

public class SOOrderTypeApproval : IPrefetchable, IPXCompanyDependent
{
  private string[] _orderTypes;

  public static string[] GetOrderTypes()
  {
    return PXDatabase.GetSlot<SOOrderTypeApproval>(nameof (SOOrderTypeApproval), new Type[1]
    {
      typeof (SOSetupApproval)
    })._orderTypes;
  }

  void IPrefetchable.Prefetch()
  {
    HashSet<string> source = new HashSet<string>();
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(typeof (SOSetupApproval), new PXDataField[2]
    {
      (PXDataField) new PXDataField<SOSetupApproval.orderType>(),
      (PXDataField) new PXDataFieldValue<SOSetupApproval.isActive>((object) true)
    }))
    {
      string str = pxDataRecord.GetString(0);
      if (!string.IsNullOrEmpty(str))
        source.Add(str);
    }
    this._orderTypes = source.ToArray<string>();
  }
}
