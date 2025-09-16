// Decompiled with JetBrains decompiler
// Type: PX.Licensing.CachedLicenseDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using Serilog;
using Serilog.Events;
using System;
using System.Threading;

#nullable disable
namespace PX.Licensing;

internal class CachedLicenseDefinition : 
  IPrefetchable<ILicensingManager>,
  IPXCompanyDependent,
  ICrossCompanyPrefetchable
{
  private static ILogger Logger = Serilog.Core.Logger.None;
  private readonly Guid _instanceId = Guid.NewGuid();
  private PXLicense _cachedLicense;
  private const string SlotKey = "CachedLicenseDefinition";
  private static readonly System.Type SlotSubscriptionTable = typeof (PX.SM.Licensing);

  internal static void InitializeLogging(ILogger logger)
  {
    CachedLicenseDefinition.Logger = LicensingLog.ForClassContext(logger, typeof (CachedLicenseDefinition));
  }

  private ILogger GetLogger()
  {
    return CachedLicenseDefinition.Logger.ForContext("CachedLicenseDefinitionInstanceId", (object) this._instanceId, false);
  }

  public PXLicense GetCachedLicense() => this._cachedLicense;

  public void SetCachedLicense(PXLicense value)
  {
    PXLicense old = Interlocked.Exchange<PXLicense>(ref this._cachedLicense, value);
    if (old == value)
      return;
    ILogger logger = this.GetLogger();
    if (!logger.IsEnabled((LogEventLevel) 2))
      return;
    if (value == null)
      logger.ForContext("CachedLicenseChangeType", (object) "Cleared", false).Information<string>("Set cached license: {LicenseState}", "None");
    else if (old == null)
      logger.ForContext("CachedLicenseChangeType", (object) "Set", false).ForContext("License", (object) value, true).Information<PXLicenseState>("Set cached license: {LicenseState}", value.State);
    else if (value.HasChangedFrom(old))
    {
      logger.ForContext("CachedLicenseChangeType", (object) "Changed", false).ForContext("License", (object) value, true).Information<PXLicenseState>("Set cached license: {LicenseState}", value.State);
    }
    else
    {
      if (!logger.IsEnabled((LogEventLevel) 1))
        return;
      logger.ForContext("CachedLicenseChangeType", (object) "NoChangeDetected", false).ForContext("License", (object) value, true).Debug<PXLicenseState>("Set cached license: {LicenseState}", value.State);
    }
  }

  void IPrefetchable<ILicensingManager>.Prefetch(ILicensingManager licensingManager)
  {
    using (new PXIgnoreChangeScope())
    {
      try
      {
        this.SetCachedLicense(licensingManager.GetLicense());
      }
      catch (Exception ex)
      {
        this.GetLogger().Error(ex, "Error while prefetching license");
        throw;
      }
    }
  }

  internal static CachedLicenseDefinition GetSlot(ILicensingManager licensingManager)
  {
    return PXDatabase.GetSlot<CachedLicenseDefinition, ILicensingManager>(nameof (CachedLicenseDefinition), licensingManager, CachedLicenseDefinition.SlotSubscriptionTable);
  }

  internal static void ResetSlot()
  {
    PXDatabase.ResetSlot<CachedLicenseDefinition>(nameof (CachedLicenseDefinition), CachedLicenseDefinition.SlotSubscriptionTable);
  }

  internal static void Subscribe(System.Action handler)
  {
    PXDatabase.Subscribe(CachedLicenseDefinition.SlotSubscriptionTable, (PXDatabaseTableChanged) (() => handler()));
  }
}
