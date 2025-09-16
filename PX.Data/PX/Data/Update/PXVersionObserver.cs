// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXVersionObserver
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Export.Authentication;

#nullable disable
namespace PX.Data.Update;

internal class PXVersionObserver
{
  private static object Locker = new object();
  private static PXVersionObserver Singleton;
  private string Version;
  private System.DateTime? Date;

  public static void Initialise()
  {
    if (PXVersionObserver.WasUpdated())
      PXVersionObserver.RequestLogout();
    lock (PXVersionObserver.Locker)
    {
      if (PXVersionObserver.Singleton == null)
        PXVersionObserver.Singleton = PXVersionObserver.Prefetch();
      PXDatabase.Subscribe(typeof (PX.SM.Version), (PXDatabaseTableChanged) (() =>
      {
        lock (PXVersionObserver.Locker)
          PXVersionObserver.Singleton.Notify();
      }), nameof (PXVersionObserver));
    }
  }

  public static PXVersionObserver Prefetch()
  {
    PXVersionObserver pxVersionObserver = new PXVersionObserver();
    try
    {
      using (new PXImpersonationContext(PXInstanceHelper.ScopeUser))
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.SM.Version>(new PXDataField(typeof (PX.SM.Version).Name), new PXDataField(typeof (PX.SM.Version.altered).Name)))
        {
          if (pxDataRecord != null)
          {
            pxVersionObserver.Version = pxDataRecord.GetString(0);
            pxVersionObserver.Date = pxDataRecord.GetDateTime(1);
          }
        }
      }
    }
    catch
    {
    }
    return pxVersionObserver;
  }

  public void Notify()
  {
    PXVersionObserver pxVersionObserver = PXVersionObserver.Prefetch();
    if (this.Version == null || pxVersionObserver.Version == null || !pxVersionObserver.Date.HasValue)
      return;
    if (!(this.Version != pxVersionObserver.Version) && this.Date.HasValue)
    {
      System.DateTime? date1 = this.Date;
      System.DateTime? date2 = pxVersionObserver.Date;
      if ((date1.HasValue & date2.HasValue ? (date1.GetValueOrDefault() < date2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
    }
    PXVersionObserver.RequestLogout();
    this.Date = pxVersionObserver.Date;
    this.Version = pxVersionObserver.Version;
  }

  private static bool WasUpdated()
  {
    try
    {
      using (new PXImpersonationContext(PXInstanceHelper.ScopeUser))
      {
        bool flag = PXFileStatusWriter.WasUpdated();
        return !WebConfig.IsClusterEnabled && flag;
      }
    }
    catch
    {
      return false;
    }
  }

  private static void RequestLogout() => LogoutRequestTracker.RequestLogout();
}
