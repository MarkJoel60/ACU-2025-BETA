// Decompiled with JetBrains decompiler
// Type: PX.Owin.DeviceHub.IDeviceHubService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Owin.DeviceHub;

[PXInternalUseOnly]
public interface IDeviceHubService
{
  Task SendPrintJobs(string deviceHubId, PrintJob[] jobs, CancellationToken cancellationToken);

  Task UpdatePrinterList(CancellationToken cancellationToken);

  Task SendScanJobs(string deviceHubId, ScanJob[] jobs, CancellationToken cancellationToken);

  Task UpdateScannerList(CancellationToken cancellationToken);
}
