// Decompiled with JetBrains decompiler
// Type: PX.Licensing.ILicensingManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.SM;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Licensing;

internal interface ILicensingManager : ILicensing
{
  Task StartAsync(CancellationToken cancellationToken);

  string InstallationId { get; }

  void TrackRequest();

  void TrackAuthentication();

  IEnumerable<RowActiveUserInfo> GetCurrentUsers();

  void RemoveSession(ILicensingSession session);

  void RequestLogOut(string company);

  void InvalidateLicense();

  PXLicense GetLicense();

  PXLicense GetLicense(LicenseBucket bucket);

  bool ValidateLicense(LicenseBucket license, string installationId = null);

  IPXLicensePolicy Policy { get; }

  void InitializePXLogin(PXLogin toInitialize);
}
