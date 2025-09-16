// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpdateHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Data.Update.Storage;
using PX.DbServices.Model;
using PX.Security;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.Data.Update;

public static class PXUpdateHelper
{
  private static Guid? UpdateProcessID;
  private static object UpdateLock = new object();

  public static bool UpdateSupported
  {
    get
    {
      bool result = Directory.Exists(PXInstanceHelper.UpdateDataFolder) && Directory.GetFiles(PXInstanceHelper.UpdateDataFolder, "*.sql").Length != 0 && (Directory.GetFiles(PXInstanceHelper.UpdateDataFolder, "*.adb", SearchOption.AllDirectories).Length != 0 || Directory.GetFiles(PXInstanceHelper.UpdateDataFolder, "*.xml", SearchOption.AllDirectories).Length != 0);
      if (!result)
        return false;
      string appSetting = ConfigurationManager.AppSettings["AutoUpdate"];
      return ((appSetting == null ? 0 : (bool.TryParse(appSetting, out result) ? 1 : 0)) & (result ? 1 : 0)) != 0;
    }
  }

  public static bool VersionsEquals()
  {
    PXDBVersCompareResults versCompareResults = PXDBVersCompareResults.NotExist;
    try
    {
      System.Version database = PXVersionHelper.Convert(IEnumerableExtensions.GetPriorityVersion(PXVersionHelper.GetDatabaseVersions()).Version);
      System.Version assemblyVersion = PXVersionHelper.GetAssemblyVersion();
      versCompareResults = PXUpdateHelper.CompareVersion(assemblyVersion, database);
      if (!(assemblyVersion == new System.Version(1, 0, 0, 0)))
      {
        if (versCompareResults != PXDBVersCompareResults.NotExist)
          goto label_5;
      }
      return true;
    }
    catch
    {
    }
label_5:
    return versCompareResults == PXDBVersCompareResults.Current;
  }

  public static PXCheckDBResult CheckDatabase()
  {
    Exception exception = (Exception) null;
    PXCheckDBResult pxCheckDbResult = new PXCheckDBResult(PXDBVersCompareResults.NotExist, PXInstanceHelper.HostName, PXInstanceHelper.InstanceID, false);
    try
    {
      pxCheckDbResult = PXUpdateHelper.CheckDatabaseInternal();
    }
    catch
    {
      if (PXInstanceHelper.ThrowExceptions)
        throw;
    }
    if (PXInstanceHelper.ThrowExceptions && exception != null)
      throw exception;
    string message = pxCheckDbResult.ToString();
    try
    {
      throw new Exception(message);
    }
    catch
    {
    }
    return pxCheckDbResult;
  }

  private static PXCheckDBResult CheckDatabaseInternal()
  {
    if (PXDatabase.Provider.CheckDatabaseLock())
      return new PXCheckDBResult(PXDBVersCompareResults.Locked, PXInstanceHelper.HostName, PXInstanceHelper.InstanceID, PXUpdateHelper.UpdateSupported);
    List<DataVersion> list = PXVersionHelper.GetDatabaseVersions().ToList<DataVersion>();
    if (list.Count == 0)
      list.Add(new DataVersion()
      {
        ComponentType = (ComponentType) 80 /*0x50*/,
        ComponentName = "Database is empty",
        Version = "0.0.0.0"
      });
    System.Version assemblyVersion = PXVersionHelper.GetAssemblyVersion();
    System.Version database = PXVersionHelper.Convert(IEnumerableExtensions.GetPriorityVersion((IEnumerable<DataVersion>) list).Version);
    return list != null && database > PXVersionHelper.Convert(PXVersionHelper.ZeroVersion) && database < PXVersionHelper.Convert(PXVersionHelper.MinimumVersion) ? new PXCheckDBResult(PXDBVersCompareResults.Minimum, PXInstanceHelper.HostName, PXInstanceHelper.InstanceID, PXUpdateHelper.UpdateSupported) : new PXCheckDBResult(PXUpdateHelper.CompareVersion(assemblyVersion, database), PXInstanceHelper.HostName, PXInstanceHelper.InstanceID, PXUpdateHelper.UpdateSupported, assemblyVersion, (IEnumerable<DataVersion>) list);
  }

  private static PXDBVersCompareResults CompareVersion(System.Version assembly, System.Version database)
  {
    if (database == (System.Version) null)
      return PXDBVersCompareResults.NotExist;
    if (database == new System.Version(0, 0, 0, 0))
      return PXDBVersCompareResults.Empty;
    if (database < PXVersionHelper.Convert(PXVersionHelper.MinimumVersion))
      return PXDBVersCompareResults.Minimum;
    int num = assembly.CompareTo(database);
    if (num == 0)
      return PXDBVersCompareResults.Current;
    if (num < 0)
      return PXDBVersCompareResults.Later;
    if (num > 0)
      return PXDBVersCompareResults.Early;
    throw new Exception();
  }

  public static bool ChectUpdateStatus()
  {
    try
    {
      if (!PXFileStatusWriter.GetUpdateStatus())
        return false;
      using (new PXImpersonationContext(PXInstanceHelper.ScopeUser))
      {
        using (PXDataRecord pxDataRecord1 = PXDatabase.SelectSingle<UPHistory>((PXDataField) new PXDataField<UPHistory.updateID>(), (PXDataField) new PXDataField<UPHistory.started>(), (PXDataField) new PXDataField<UPHistory.finished>(), (PXDataField) new PXDataFieldOrder<UPHistory.updateID>(true)))
        {
          if (pxDataRecord1 == null)
            return true;
          using (PXDataRecord pxDataRecord2 = PXDatabase.SelectSingle<UPHistoryComponents>((PXDataField) new PXDataField<UPHistoryComponents.updateID>(), (PXDataField) new PXDataField<UPHistoryComponents.fromVersion>(), (PXDataField) new PXDataField<UPHistoryComponents.toVersion>(), (PXDataField) new PXDataFieldOrder<UPHistoryComponents.updateID>(true), (PXDataField) new PXDataFieldOrder<UPHistoryComponents.updateComponentID>(true)))
          {
            if (pxDataRecord2 == null)
              return true;
            string version1 = PXVersionHelper.GetAssemblyVersion().ToString(true);
            if (PXVersionHelper.Compare(version1, PXVersionHelper.DefaultVersion) != 0)
            {
              if (PXVersionHelper.Compare(version1, pxDataRecord2.GetString(2)) != 0)
                return false;
            }
          }
          bool flag1 = false;
          foreach (PXDataRecord pxDataRecord3 in PXDatabase.SelectMulti<UPErrors>((PXDataField) new PXDataField<UPErrors.updateID>(), (PXDataField) new PXDataField<UPErrors.errorID>(), (PXDataField) new PXDataField<UPErrors.skip>(), (PXDataField) new PXDataFieldValue<UPErrors.updateID>(PXDbType.Int, (object) pxDataRecord1.GetInt32(0)), (PXDataField) new PXDataFieldOrder<UPErrors.updateID>(true)))
          {
            bool? boolean = pxDataRecord3.GetBoolean(2);
            bool flag2 = true;
            if (!(boolean.GetValueOrDefault() == flag2 & boolean.HasValue))
            {
              flag1 = true;
              break;
            }
          }
          return !(!pxDataRecord1.GetDateTime(2).HasValue | flag1);
        }
      }
    }
    catch
    {
      return true;
    }
  }

  public static bool CheckUpdateLock()
  {
    Guid? nullable = new Guid?();
    lock (PXUpdateHelper.UpdateLock)
      nullable = PXUpdateHelper.UpdateProcessID;
    return nullable.HasValue && PXLongOperation.GetStatus((object) PXUpdateHelper.UpdateProcessID, out TimeSpan _, out Exception _) == PXLongRunStatus.InProcess || PXDatabase.Provider.CheckDatabaseLock();
  }

  public static bool NeedsUpdate(out PXCheckDBResult dbCheckResult)
  {
    dbCheckResult = (PXCheckDBResult) null;
    if (PXAccess.NoConnectionString())
      return false;
    dbCheckResult = PXUpdateHelper.CheckDatabase();
    if (dbCheckResult.CompareResult != PXDBVersCompareResults.Empty)
    {
      PXUpdateHelper.ClearUpdatePackages();
      PXUpdateHelper.ResetUsersOnlineStatus();
      PXLongOperation.ClearAbortedProcesses();
    }
    PXVersionObserver.Initialise();
    return dbCheckResult.NeedUpdate;
  }

  public static void Update(bool forceUpdate, PXCheckDBResult checkResult = null)
  {
    checkResult = checkResult ?? PXUpdateHelper.CheckDatabaseInternal();
    lock (PXUpdateHelper.UpdateLock)
      PXUpdateHelper.UpdateProcessID = !PXUpdateHelper.UpdateProcessID.HasValue || !PXLongOperation.Exists((object) PXUpdateHelper.UpdateProcessID) ? new Guid?(Guid.NewGuid()) : throw new PXException("The update operation is already in progress.");
    try
    {
      using (new PXImpersonationContext(PXInstanceHelper.ScopeUser))
        PXLongOperation.StartOperation((object) PXUpdateHelper.UpdateProcessID, new PXToggleAsyncDelegate(new PXUpdate(checkResult)
        {
          ForceUpdate = forceUpdate
        }.Start));
    }
    catch (Exception ex)
    {
      PXUpdateHelper.UpdateProcessID = new Guid?();
      PXUpdateLog.WriteMessage(ex);
    }
  }

  public static PXUpdateStatusResult GetUpdateStatus()
  {
    if (!PXUpdateHelper.UpdateProcessID.HasValue)
      return new PXUpdateStatusResult(PXLongRunStatus.NotExists);
    Exception message;
    PXLongRunStatus status = PXLongOperation.GetStatus((object) PXUpdateHelper.UpdateProcessID, out TimeSpan _, out message);
    object customInfo = PXLongOperation.GetCustomInfo((object) PXUpdateHelper.UpdateProcessID);
    switch (status)
    {
      case PXLongRunStatus.InProcess:
        switch (customInfo)
        {
          case null:
            return new PXUpdateStatusResult(status);
          case PXUpdateStatus _:
            return new PXUpdateStatusResult(customInfo as PXUpdateStatus);
          case PXUpdateQuestion _:
            return new PXUpdateStatusResult(customInfo as PXUpdateQuestion);
          default:
            throw new Exception("Unknown update status.");
        }
      case PXLongRunStatus.Completed:
        PXUpdateHelper.UpdateProcessID = new Guid?();
        return new PXUpdateStatusResult(status);
      case PXLongRunStatus.Aborted:
        return new PXUpdateStatusResult(message);
      default:
        return new PXUpdateStatusResult(status);
    }
  }

  public static void SetUpdateAnswer(WebDialogResult answer)
  {
    if (!PXUpdateHelper.UpdateProcessID.HasValue || !(PXLongOperation.GetCustomInfo((object) PXUpdateHelper.UpdateProcessID) is PXUpdateQuestion customInfo))
      return;
    customInfo.SetAnswer(answer);
  }

  private static void ResetUsersOnlineStatus()
  {
    try
    {
      using (new PXImpersonationContext(PXInstanceHelper.ScopeUser))
        ServiceLocator.Current.GetInstance<IUserManagementService>().MarkAllUsersOffline();
    }
    catch
    {
    }
  }

  private static void ClearUpdatePackages()
  {
    try
    {
      using (new PXImpersonationContext(PXInstanceHelper.ScopeUser))
        PXStorageHelper.GetAppDataProvider().Clear();
    }
    catch
    {
    }
  }
}
