// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.DatabaseLock
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert;
using System;

#nullable disable
namespace PX.Data.Update;

public static class DatabaseLock
{
  public static readonly object Locker = new object();
  private static DatabaseLock.DatabaseLockHandle LockHandle;

  public static bool CheckDatabaseLock(this PXDatabaseProvider provider)
  {
    try
    {
      using (new PXImpersonationContext(PXInstanceHelper.ScopeUser))
      {
        lock (DatabaseLock.Locker)
        {
          if (DatabaseLock.LockHandle != null)
          {
            if (DatabaseLock.LockHandle.LockAccess)
              return true;
          }
        }
        if (!PXAccess.NoConnectionString())
        {
          DbmsMaintenance maintenance = provider.GetMaintenance();
          int num = maintenance.IsDatabaseLocked(PXInstanceHelper.InstanceID) ? 1 : 0;
          if (num == 0)
            maintenance.UnlockDatabase();
          return num != 0;
        }
      }
    }
    catch
    {
      if (PXInstanceHelper.ThrowExceptions)
        throw;
    }
    return false;
  }

  public static void DatabaseOperation(
    this PXDatabaseProvider provider,
    System.Action act,
    bool lockDB = false,
    bool disableFullText = true)
  {
    try
    {
      DbmsMaintenance maintenance = provider.GetMaintenance();
      lock (DatabaseLock.Locker)
        DatabaseLock.LockHandle = DatabaseLock.LockHandle == null ? new DatabaseLock.DatabaseLockHandle(PXLongOperation.GetOperationKey() ?? new object(), lockDB) : throw new PXException("The database is locked.");
      try
      {
        if (lockDB)
          maintenance.LockDatabase(PXInstanceHelper.InstanceID, "Company Maintenance");
        if (disableFullText)
          maintenance.DisableFullText();
        act();
      }
      finally
      {
        try
        {
          if (disableFullText)
            maintenance.EnableFullText();
          if (lockDB)
            maintenance.UnlockDatabase();
        }
        finally
        {
          DatabaseLock.LockHandle = (DatabaseLock.DatabaseLockHandle) null;
        }
      }
    }
    catch (Exception ex)
    {
      PXUpdateLog.WriteMessage(ex);
      throw;
    }
  }

  private class DatabaseLockHandle
  {
    public readonly bool LockAccess;
    public object ProcessID;

    public DatabaseLockHandle(object process, bool lockAccess = true)
    {
      this.ProcessID = process;
      this.LockAccess = lockAccess;
    }
  }
}
