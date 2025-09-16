// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXReminderSyncHandler
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Web;
using System.Xml;

#nullable disable
namespace PX.Objects.EP;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2021R2.")]
public class PXReminderSyncHandler : IHttpHandler
{
  public virtual void ProcessRequest(HttpContext context)
  {
    TasksAndEventsReminder andEventsReminder = new TasksAndEventsReminder();
    using (XmlWriter xmlWriter = XmlWriter.Create(context.Response.OutputStream))
    {
      xmlWriter.WriteStartElement("result");
      xmlWriter.WriteAttributeString("count", andEventsReminder.GetListCount().ToString());
      xmlWriter.WriteEndElement();
    }
  }

  public bool IsReusable => true;
}
