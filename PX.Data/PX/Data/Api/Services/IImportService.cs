// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Services.IImportService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Api.Services;

[PXInternalUseOnly]
public interface IImportService
{
  PXGraph CreateGraphForMobile(string screenId);

  PXGraph CreateGraphForMobile(string graphName, string screenId);

  PXGraph CreateGraphForMobile(string graphName, string screenId, string prefix);
}
