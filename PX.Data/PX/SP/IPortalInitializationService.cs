// Decompiled with JetBrains decompiler
// Type: PX.SP.IPortalInitializationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using PX.Common;

#nullable enable
namespace PX.SP;

[PXInternalUseOnly]
public interface IPortalInitializationService
{
  void InitPortal(HttpContext httpContext);
}
