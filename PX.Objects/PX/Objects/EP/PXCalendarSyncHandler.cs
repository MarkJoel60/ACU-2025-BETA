// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXCalendarSyncHandler
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Export;
using PX.Export.Imc;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;

#nullable disable
namespace PX.Objects.EP;

public sealed class PXCalendarSyncHandler : PXFileExportHandler
{
  public const string DATA_SESSION_KEY = "CalendarSyncExportKeys";
  public const string FILE_EXTENSION = "Ics";
  private const string _CALENDAR_SETTINGS_KEY = "id";
  private const string _COMPANY_KEY = "cid";

  protected virtual string ContentType => "Content-Type: text/calendar; charset=UTF-8";

  protected virtual string DataSessionKey => "CalendarSyncExportKeys";

  protected virtual bool NullableData => true;

  protected virtual void Write(Stream stream, PXFileExportHandler.ProcessBag bag)
  {
    if (!(bag.Data is vCalendarIcs vCalendarIcs))
    {
      string parameter1 = bag.Parameters["id"];
      string parameter2 = bag.Parameters["cid"];
      if (!string.IsNullOrEmpty(parameter1))
      {
        EPCalendarSync instance = PXGraph.CreateInstance<EPCalendarSync>();
        try
        {
          using (new PXLoginScope(string.IsNullOrEmpty(parameter2) ? "admin" : "admin@" + parameter2, PXAccess.GetAdministratorRoles()))
          {
            IEnumerable<CRActivity> calendarEvents = instance.GetCalendarEvents(new Guid(parameter1));
            vCalendarIcs = vCalendarIcs.op_Explicit(instance.VCalendarFactory.CreateVCalendar((IEnumerable) calendarEvents));
          }
        }
        catch (FormatException ex)
        {
        }
      }
    }
    if (vCalendarIcs == null)
      vCalendarIcs = new vCalendarIcs();
    using (StreamWriter streamWriter = new StreamWriter(stream))
      ((vCalendarBase<vEventIcs>) vCalendarIcs).Write((TextWriter) streamWriter);
  }

  public static string GetSyncUrl(HttpContext context, string userId)
  {
    return context.Request.GetWebsiteUrl().TrimEnd('/') + PXUrl.ToAbsoluteUrl($"~/calendarSync.ics?{"id"}={userId}&{"cid"}={PXAccess.GetCompanyName()}");
  }
}
