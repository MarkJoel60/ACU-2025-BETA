// Decompiled with JetBrains decompiler
// Type: PX.Data.IsPortalDefaultAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class IsPortalDefaultAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldDefaultingSubscriber,
  IPXCommandPreparingSubscriber
{
  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.NewValue != null)
      return;
    e.NewValue = (object) PXSiteMap.IsPortal;
  }

  public void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    bool isPortal = PXSiteMap.IsPortal;
    if (e.Value == null)
      e.Value = (object) isPortal;
    if (e.DataValue != null)
      return;
    e.DataValue = (object) isPortal;
  }
}
