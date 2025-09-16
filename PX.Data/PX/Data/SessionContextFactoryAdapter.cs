// Decompiled with JetBrains decompiler
// Type: PX.Data.SessionContextFactoryAdapter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using PX.Common;
using System.Web;

#nullable enable
namespace PX.Data;

internal sealed class SessionContextFactoryAdapter : ISessionContextFactoryAdapter
{
  void ISessionContextFactoryAdapter.ClearSession(HttpContextBase httpContext)
  {
    PXSessionStateStore.ClearSession(httpContext);
  }

  private sealed class ServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      RegistrationExtensions.RegisterInstance<SessionContextFactoryAdapter>(builder, new SessionContextFactoryAdapter()).As<ISessionContextFactoryAdapter>();
    }
  }
}
