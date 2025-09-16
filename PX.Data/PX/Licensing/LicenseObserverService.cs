// Decompiled with JetBrains decompiler
// Type: PX.Licensing.LicenseObserverService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Common;
using PX.Data;
using PX.Data.Update;
using Serilog;
using Serilog.Context;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading;

#nullable disable
namespace PX.Licensing;

internal sealed class LicenseObserverService
{
  private const int MinutesInDay = 1440;
  private static readonly TimeSpan MinConnectionAttemptInterval = TimeSpan.FromHours(12.0);
  private readonly LicensingManager _licensingManager;
  private readonly ILogger _licensingLogger;
  private readonly int? _forcedInterval;
  private readonly Thread _thread;
  private readonly Random _random = new Random();
  private readonly TimeSpan _randomTimeOfDayOffset;
  private System.DateTime _lastConnectionAttempt = System.DateTime.MinValue;
  internal const string LicenseCheckSlotName = "ProviderLicenseCheck";

  public LicenseObserverService(
    IOptions<LicenseObserverServiceOptions> options,
    LicensingManager licensingManager,
    ILogger licensingLogger)
  {
    this._licensingManager = licensingManager;
    this._licensingLogger = licensingLogger;
    this._licensingLogger.Verbose<LicenseObserverServiceOptions>("Got options: {@Options}", options.Value);
    int? licenseThreadPeriod = options.Value.ParsedLicenseThreadPeriod;
    if (licenseThreadPeriod.HasValue)
    {
      if (licenseThreadPeriod.GetValueOrDefault() < 300000)
      {
        this._forcedInterval = new int?(options.Value.ParsedLicenseThreadPeriod.Value);
        this._licensingLogger.Information<string, int?>("{Reason}, forcing interval to {ForcedInterval}", "Configuration key found", this._forcedInterval);
      }
      else
        this._licensingLogger.Warning<int?>("Configured value for forced interval too big ({ForcedInterval}), discarding", options.Value.ParsedLicenseThreadPeriod);
    }
    else if (!string.IsNullOrWhiteSpace(options.Value.LicenseThreadPeriod))
      this._licensingLogger.Warning<string>("Configured value for forced interval is invalid ({Value}), discarding", options.Value.LicenseThreadPeriod);
    this._randomTimeOfDayOffset = TimeSpan.FromMinutes((double) this._random.Next(1440));
    this._licensingLogger.Verbose<TimeSpan>("Set time offset to {RandomTimeOfDayOffset}", this._randomTimeOfDayOffset);
    this._thread = new Thread(new ParameterizedThreadStart(this.ThreadStartPoint))
    {
      IsBackground = true
    };
  }

  internal void Start()
  {
    using (this._licensingLogger.TimeOperationVerbose(nameof (Start)))
    {
      using (new PXImpersonationContext("admin"))
        this._thread.Start((object) WindowsIdentity.GetCurrent());
    }
  }

  private void ThreadStartPoint(object identity)
  {
    LogContext.Reset();
    ILogger ilogger = this._licensingLogger.ForMethodContext(nameof (ThreadStartPoint));
    int millisecondsTimeout1 = this._forcedInterval ?? this._random.Next(100000, 500000);
    ilogger.Verbose<int>("Sleeping for {SleepMs} ms", millisecondsTimeout1);
    Thread.Sleep(millisecondsTimeout1);
    WindowsIdentity windowsIdentity;
    try
    {
      windowsIdentity = (WindowsIdentity) identity;
    }
    catch (Exception ex)
    {
      ilogger.Error(ex, "Can't get WindowsIdentity from {Identity}", new object[1]
      {
        identity
      });
      throw;
    }
    WindowsImpersonationContext impersonationContext;
    try
    {
      impersonationContext = windowsIdentity.Impersonate();
    }
    catch (Exception ex)
    {
      ilogger.Error<string>(ex, "Exception while trying to impersonate identity {Identity}", windowsIdentity.Name);
      throw;
    }
    try
    {
      while (true)
      {
        using (LogContext.PushProperty("PeriodicLicenseCheckId", (object) Guid.NewGuid(), false))
        {
          bool flag = false;
          try
          {
            ilogger.Debug<string>("Periodic license check under identity {Identity}", windowsIdentity.Name);
            foreach (KeyValuePair<string, PXDatabase.ProviderBucket> keyValuePair in PXDatabase.Providers.Where<KeyValuePair<string, PXDatabase.ProviderBucket>>((Func<KeyValuePair<string, PXDatabase.ProviderBucket>, bool>) (p => p.Value != null && p.Value.Initialised)))
            {
              try
              {
                ilogger.Debug<string>("Periodic license check for provider {Provider}", keyValuePair.Key);
                PXContext.SetSlot<string>("ProviderLicenseCheck", keyValuePair.Key);
                this.CheckLicense();
              }
              catch (Exception ex)
              {
                ilogger.Error<string>(ex, "Error while performing periodic license check for provider {Provider}", keyValuePair.Key);
                throw;
              }
              finally
              {
                PXContext.SetSlot("ProviderLicenseCheck", (object) null);
              }
            }
            int millisecondsTimeout2 = this._forcedInterval ?? this._random.Next(300000, 1800000);
            ilogger.Verbose<int>("Sleeping for {SleepMs} ms", millisecondsTimeout2);
            Thread.Sleep(millisecondsTimeout2);
          }
          catch (ThreadAbortException ex)
          {
            ilogger.Verbose((Exception) ex, "Thread aborted");
            throw;
          }
          catch
          {
            flag = true;
          }
          if (flag)
            Thread.Sleep(10000);
        }
      }
    }
    finally
    {
      impersonationContext.Undo();
    }
  }

  private void CheckLicense()
  {
    ILogger ilogger = this._licensingLogger.ForMethodContext(nameof (CheckLicense));
    using (new PXImpersonationContext(PXInstanceHelper.ScopeUser, PXAccess.GetAdministratorRoles()))
    {
      string str1 = "OK";
      try
      {
        PXLicense license1 = this._licensingManager.License;
        ilogger.ForContext("License", (object) license1, true).Debug<PXLicenseState>("Checking current license: {LicenseState}", license1.State);
        if (license1.Licensed)
        {
          this._licensingManager.InitialiseLicense(this._licensingManager.GetLicense(this._licensingManager.ReadLicense()));
          license1 = this._licensingManager.License;
          ilogger.Debug<PXLicenseState>("License was reinitialized: {LicenseState}", license1.State);
        }
        if (!PXAccess.IsMultiDbMode && license1.ProcessorsAllowed > 0)
        {
          if (license1.ProcessorsAllowed < Environment.ProcessorCount)
          {
            try
            {
              LicenseObserverService.SetAffinity(license1.ProcessorsAllowed);
              ilogger.Debug<int>("Limited processors to {ProcessorCount}", license1.ProcessorsAllowed);
            }
            catch (ThreadAbortException ex)
            {
              throw;
            }
            catch
            {
              ilogger.Error<int>("Error while limiting processors to {ProcessorCount}", license1.ProcessorsAllowed);
            }
          }
        }
        System.DateTime dateTime = license1.NextRequest + this._randomTimeOfDayOffset;
        if (license1 != null && (!license1.Validated || !(dateTime < System.DateTime.UtcNow)))
        {
          if (!PXLicenseHelper.ForceRequestLicense)
            goto label_21;
        }
        if (System.DateTime.UtcNow > this._lastConnectionAttempt.Add(LicenseObserverService.MinConnectionAttemptInterval))
        {
          this._lastConnectionAttempt = System.DateTime.UtcNow;
          ilogger.ForContext("License", license1 != null ? (object) new
          {
            Validated = license1.Validated,
            NextRequest = license1.NextRequest
          } : (object) null, true).Information<string>("Obtaining new license: {NewLicenseReason}", license1 != null ? "time for valid license refresh" : "license is null");
          LicenseBucket licenseBucket = this._licensingManager.RequestLicense(PXLicenseReason.Validating, true);
          PXLicense license2 = this._licensingManager.GetLicense(licenseBucket);
          if (license2.Validated)
          {
            this._licensingManager.WriteLicense(licenseBucket);
            this._licensingManager.InitialiseLicense(license2);
            PXLicense license3 = this._licensingManager.License;
          }
          else
            ilogger.ForContext("License", (object) license2, true).Warning("Got invalid license");
        }
      }
      catch (ThreadAbortException ex)
      {
        throw;
      }
      catch (Exception ex1)
      {
        ilogger.Error(ex1, "Exception while checking current license");
        str1 = ex1.ToString();
        try
        {
          PXUpdateLog.WriteMessage(ex1);
        }
        catch (ThreadAbortException ex2)
        {
          throw;
        }
        catch
        {
        }
      }
label_21:
      string installationId = this._licensingManager.InstallationId;
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.SM.Licensing>((PXDataField) new PXDataField<PX.SM.Licensing.status>(), (PXDataField) new PXDataFieldValue<PX.SM.Licensing.installationID>((object) installationId)))
      {
        if (pxDataRecord == null)
          return;
        string str2 = pxDataRecord.GetString(0);
        if (str1 == str2)
          return;
      }
      System.DateTime utcNow = System.DateTime.UtcNow;
      try
      {
        PXDatabase.Update<PX.SM.Licensing>((PXDataFieldParam) new PXDataFieldAssign<PX.SM.Licensing.activity>((object) utcNow), (PXDataFieldParam) new PXDataFieldAssign<PX.SM.Licensing.status>((object) str1), (PXDataFieldParam) new PXDataFieldRestrict<PX.SM.Licensing.installationID>((object) installationId));
        ilogger.Debug<string, System.DateTime, string>("Updated license status for {InstallationID}: {LicenseActivity} - {LicenseStatus}", installationId, utcNow, str1);
      }
      catch (ThreadAbortException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        ilogger.ForContext("LicenseActivity", (object) utcNow, false).ForContext("LicenseStatus", (object) str1, false).Error<string>(ex, "Exception while updating license status for {InstallationID}", installationId);
      }
    }
  }

  private static void SetAffinity(int total)
  {
    int num1 = 0;
    int int32 = Process.GetCurrentProcess().ProcessorAffinity.ToInt32();
    for (int y = 0; y < Environment.ProcessorCount; ++y)
    {
      if ((int32 & (int) System.Math.Pow(2.0, (double) y)) == (int) System.Math.Pow(2.0, (double) y))
        ++num1;
    }
    if (num1 <= total)
      return;
    Random random = new Random();
    List<int> intList = new List<int>();
    for (int index = 0; index < total && index < Environment.ProcessorCount; ++index)
    {
      int num2 = -1;
      while (num2 < 0 || intList.Contains(num2))
        num2 = random.Next(Environment.ProcessorCount);
      intList.Add(num2);
    }
    int num3 = 0;
    foreach (int y in intList)
      num3 += (int) System.Math.Pow(2.0, (double) y);
    Process.GetCurrentProcess().ProcessorAffinity = (IntPtr) num3;
  }
}
