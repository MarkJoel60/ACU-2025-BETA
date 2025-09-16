// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.PXSiteLockout
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.Model;
using PX.SM;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

#nullable disable
namespace PX.Data.Maintenance;

public static class PXSiteLockout
{
  private const string SlotKey = "_SCHEDULED_LOCKOUT_IN_DB";
  private static readonly string LockFileName = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "lockout");
  private const int Timeout = -1;
  private const string Host = "all";
  private static readonly ReaderWriterLock _lockoutSync = new ReaderWriterLock();
  private static PXSiteLockout.ILock _localLock = (PXSiteLockout.ILock) new PXSiteLockout.LocalFileLock(PXSiteLockout.LockFileName);
  private static PXSiteLockout.ILock _globalLock = (PXSiteLockout.ILock) new PXSiteLockout.DBLock("_SCHEDULED_LOCKOUT_IN_DB");
  private static PXSiteLockout.ILock _currentLock = (PXSiteLockout.ILock) null;

  private static PXSiteLockout.ILock FindNewestLock()
  {
    PXSiteLockout._localLock.Refresh();
    PXSiteLockout._globalLock.Refresh();
    System.DateTime? dateTime1 = PXSiteLockout._localLock.DateTime;
    System.DateTime? dateTime2 = PXSiteLockout._globalLock.DateTime;
    return !dateTime2.HasValue || !dateTime1.HasValue ? (dateTime2.HasValue ? PXSiteLockout._globalLock : (dateTime1.HasValue ? PXSiteLockout._localLock : (PXSiteLockout.ILock) null)) : (dateTime1.Value > dateTime2.Value ? PXSiteLockout._localLock : PXSiteLockout._globalLock);
  }

  private static PXSiteLockout.ILock SafeGetCurrentLock()
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXSiteLockout._lockoutSync);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      if (PXSiteLockout._currentLock == null || PXSiteLockout._currentLock != null && !PXSiteLockout._currentLock.IsLocked)
      {
        ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
        if (PXSiteLockout._currentLock == null || PXSiteLockout._currentLock != null && !PXSiteLockout._currentLock.IsLocked)
          return PXSiteLockout._currentLock = PXSiteLockout.FindNewestLock();
      }
      return PXSiteLockout._currentLock;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  private static System.DateTime? SafeGetLockoutTime()
  {
    return PXSiteLockout.SafeGetCurrentLock()?.DateTime;
  }

  private static string SafeGetLockoutMessage() => PXSiteLockout.SafeGetCurrentLock()?.Message;

  public static System.DateTime? DateTime
  {
    get
    {
      System.DateTime? lockoutTime = PXSiteLockout.SafeGetLockoutTime();
      if (!lockoutTime.HasValue)
        return new System.DateTime?();
      return !PXContext.PXIdentity.IsAnonymous() && PXContext.PXIdentity.TimeZone != null ? new System.DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(lockoutTime.Value, LocaleInfo.GetTimeZone())) : new System.DateTime?(TimeZone.CurrentTimeZone.ToLocalTime(lockoutTime.Value));
    }
  }

  public static System.DateTime? DateTimeUtc => PXSiteLockout.SafeGetLockoutTime();

  public static string Message => PXSiteLockout.SafeGetLockoutMessage();

  public static void Lock(System.DateTime lockoutTime, string message, bool lockoutAll)
  {
    System.DateTime utc = PXTimeZoneInfo.ConvertTimeToUtc(lockoutTime, LocaleInfo.GetTimeZone());
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXSiteLockout._lockoutSync);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireWriterLock();
      if (lockoutAll)
        PXSiteLockout._globalLock.Lock(utc, message);
      else
        PXSiteLockout._localLock.Lock(utc, message);
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  public static void Unlock(bool unlockAll)
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXSiteLockout._lockoutSync);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireWriterLock();
      if (PXSiteLockout._localLock.IsLocked)
        PXSiteLockout._localLock.Unlock();
      if (!unlockAll || !PXSiteLockout._globalLock.IsLocked)
        return;
      PXSiteLockout._globalLock.Unlock();
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  public static PXSiteLockout.Status GetStatus(bool needRefresh = false)
  {
    if (needRefresh)
    {
      PXSiteLockout._localLock.Refresh();
      PXSiteLockout._globalLock.Refresh();
    }
    System.DateTime? lockoutTime = PXSiteLockout.SafeGetLockoutTime();
    if (!lockoutTime.HasValue)
      return PXSiteLockout.Status.Free;
    System.DateTime? nullable = lockoutTime;
    System.DateTime utcNow = PXExecutionContext.Current.Time.UtcNow;
    return (nullable.HasValue ? (nullable.GetValueOrDefault() > utcNow ? 1 : 0) : 0) != 0 ? PXSiteLockout.Status.Pending : PXSiteLockout.Status.Locked;
  }

  public static PXSiteLockout.LockoutType GetLockoutType(bool needRefresh = false)
  {
    if (needRefresh)
    {
      PXSiteLockout._localLock.Refresh();
      PXSiteLockout._globalLock.Refresh();
    }
    return !PXSiteLockout._localLock.IsLocked ? (!PXSiteLockout._globalLock.IsLocked ? PXSiteLockout.LockoutType.None : PXSiteLockout.LockoutType.Global) : (!PXSiteLockout._globalLock.IsLocked ? PXSiteLockout.LockoutType.Instance : PXSiteLockout.LockoutType.Instance | PXSiteLockout.LockoutType.Global);
  }

  public enum Status
  {
    Free,
    Pending,
    Locked,
  }

  [Flags]
  public enum LockoutType
  {
    None = 0,
    Instance = 1,
    Global = 2,
  }

  private interface ILock
  {
    bool IsLocked { get; }

    System.DateTime? DateTime { get; }

    string Message { get; }

    void Refresh();

    void Lock(System.DateTime lockoutTime, string message);

    void Unlock();
  }

  private class LocalFileLock : PXSiteLockout.ILock
  {
    private const string ElementName_Lockout = "Lockout";
    private const string ElementName_DateTime = "DateTime";
    private const string ElementName_Message = "Message";
    private string _fileName;
    private System.DateTime? _dateTime;
    private string _message;

    public LocalFileLock(string fileName)
    {
      this._fileName = fileName;
      this.Refresh();
    }

    public bool IsLocked => this._dateTime.HasValue;

    public System.DateTime? DateTime => this._dateTime;

    public string Message => this._message;

    public void Refresh()
    {
      if (!File.Exists(this._fileName))
      {
        this._dateTime = new System.DateTime?();
        this._message = (string) null;
      }
      else
      {
        using (StreamReader streamReader = new StreamReader(this._fileName))
        {
          XDocument xdocument = XDocument.Load((TextReader) streamReader);
          this._dateTime = new System.DateTime?(System.DateTime.Parse(xdocument.Descendants((XName) "DateTime").Single<XElement>().Value, (IFormatProvider) CultureInfo.InvariantCulture.DateTimeFormat));
          this._message = xdocument.Descendants((XName) "Message").Single<XElement>().Value;
        }
      }
    }

    public void Lock(System.DateTime lockoutTime, string message)
    {
      using (StreamWriter streamWriter = new StreamWriter(this._fileName, false))
        streamWriter.Write((object) new XDocument(new object[1]
        {
          (object) new XElement((XName) "Lockout", new object[2]
          {
            (object) new XElement((XName) "DateTime", (object) lockoutTime.ToString((IFormatProvider) CultureInfo.InvariantCulture.DateTimeFormat)),
            (object) new XElement((XName) "Message", (object) message)
          })
        }));
      this._dateTime = new System.DateTime?(lockoutTime);
      this._message = message;
    }

    public void Unlock()
    {
      if (!File.Exists(this._fileName))
        return;
      File.Delete(this._fileName);
      this._dateTime = new System.DateTime?();
      this._message = (string) null;
    }
  }

  private class DBLock : PXSiteLockout.ILock
  {
    private string _slotKey;

    private PXSiteLockout.DBLock.Definition Current
    {
      get
      {
        return this.InLoginScope<PXSiteLockout.DBLock.Definition>((Func<PXSiteLockout.DBLock.Definition>) (() => PXDatabase.GetSlot<PXSiteLockout.DBLock.Definition>(this._slotKey, typeof (UPLock)) ?? PXSiteLockout.DBLock.Definition.Empty));
      }
    }

    public DBLock(string slotKey) => this._slotKey = slotKey;

    private static string GetLoginScopeUser()
    {
      string loginScopeUser = "admin";
      if (PXDatabase.Companies.Length != 0)
        loginScopeUser = $"{loginScopeUser}@{PXDatabase.Companies[0]}";
      return loginScopeUser;
    }

    /// <summary>
    /// Executes function in login scope under 'admin' user and first company.
    /// </summary>
    private T InLoginScope<T>(Func<T> func)
    {
      using (new PXLoginScope(PXSiteLockout.DBLock.GetLoginScopeUser(), PXAccess.GetAdministratorRoles()))
        return func();
    }

    /// <summary>
    /// Executes action in login scope under 'admin' user and first company.
    /// </summary>
    private void InLoginScope(System.Action action)
    {
      using (new PXLoginScope(PXSiteLockout.DBLock.GetLoginScopeUser(), PXAccess.GetAdministratorRoles()))
        action();
    }

    public bool IsLocked => this.Current.IsLocked;

    public System.DateTime? DateTime => this.Current.DateTime;

    public string Message => this.Current.Message;

    public void Refresh()
    {
      this.InLoginScope((System.Action) (() => PXDatabase.ResetSlot<PXSiteLockout.DBLock.Definition>(this._slotKey, typeof (UPLock))));
    }

    public void Lock(System.DateTime lockoutTime, string message)
    {
      PXMultitenancyHelper.ExecuteOnAllDB((System.Action) (() =>
      {
        new UPLock((PXLockoutReason) 2, "all", new System.DateTime?(lockoutTime), message).Insert();
        PXDatabase.SelectTimeStamp();
      }));
    }

    public void Unlock()
    {
      PXMultitenancyHelper.ExecuteOnAllDB((System.Action) (() =>
      {
        UPLock.Delete((PXLockoutReason) 2);
        PXDatabase.SelectTimeStamp();
      }));
    }

    private class Definition : IPrefetchable, IPXCompanyDependent
    {
      public static readonly PXSiteLockout.DBLock.Definition Empty = new PXSiteLockout.DBLock.Definition();
      private UPLock _upLock;

      public bool IsLocked => this._upLock != null;

      public System.DateTime? DateTime
      {
        get => this._upLock == null ? new System.DateTime?() : this._upLock.Date;
      }

      public string Message => this._upLock == null ? (string) null : this._upLock.Purpose;

      public void Prefetch()
      {
        try
        {
          this._upLock = UPLock.SelectSingle((PXLockoutReason) 2);
        }
        catch
        {
        }
      }
    }
  }
}
