// Decompiled with JetBrains decompiler
// Type: PX.Common.ISessionContextFactory
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.Web;

#nullable disable
namespace PX.Common;

[PXInternalUseOnly]
public interface ISessionContextFactory
{
  void BeginRequest(HttpContextBase httpContext);

  void AuthenticateRequest(HttpContextBase httpContext);

  void SaveRequestToSession(HttpContextBase httpContext);

  void Abandon(HttpContextBase httpContext);

  void Invalidate(HttpContextBase httpContext);
}
