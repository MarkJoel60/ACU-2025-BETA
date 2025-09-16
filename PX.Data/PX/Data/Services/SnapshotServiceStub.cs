// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.SnapshotServiceStub
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Points;
using PX.SM;
using System.Xml;

#nullable disable
namespace PX.Data.Services;

/// <exclude />
public class SnapshotServiceStub : ISnapshotServiceWithLogger, ISnapshotService
{
  public void WriteManifest(XmlDocument manifest)
  {
  }

  public void ApplyDataAfterSnapshotRestoration(UPCompany company)
  {
  }

  public IExecutionObserver GetExecutionObserver(SnapshotExecutionMode mode)
  {
    return (IExecutionObserver) new SimpleExecutionObserver();
  }
}
