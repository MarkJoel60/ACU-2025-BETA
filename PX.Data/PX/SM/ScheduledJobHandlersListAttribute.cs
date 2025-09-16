// Decompiled with JetBrains decompiler
// Type: PX.SM.ScheduledJobHandlersListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Data;
using PX.Data.Process;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class ScheduledJobHandlersListAttribute : PXStringListAttribute
{
  public override void CacheAttached(PXCache sender)
  {
    List<string> stringList1 = new List<string>()
    {
      "ProcessAll"
    };
    List<string> stringList2 = new List<string>()
    {
      "Mass-Process"
    };
    if (ServiceLocator.IsLocationProviderSet)
    {
      foreach (IScheduledJobHandler activeHandler in ServiceLocator.Current.GetInstance<IScheduledJobHandlerProvider>().GetActiveHandlers())
      {
        stringList1.Add(activeHandler.Type);
        stringList2.Add(activeHandler.Description);
      }
    }
    this._AllowedValues = stringList1.ToArray();
    this._AllowedLabels = stringList2.ToArray();
    base.CacheAttached(sender);
  }
}
