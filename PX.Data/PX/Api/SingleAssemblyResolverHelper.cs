// Decompiled with JetBrains decompiler
// Type: PX.Api.SingleAssemblyResolverHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;

#nullable disable
namespace PX.Api;

[PXInternalUseOnly]
public static class SingleAssemblyResolverHelper
{
  /// <summary>
  /// Registers custom <see cref="T:System.Web.Http.Dispatcher.IAssembliesResolver" /> implementation that returns only specified assemblies.
  /// </summary>
  [PXInternalUseOnly]
  public static HttpConfiguration UseCustomAssembliesResolver(
    this HttpConfiguration httpConfiguration,
    params Assembly[] assemblies)
  {
    httpConfiguration.Services.Replace(typeof (IAssembliesResolver), (object) new SingleAssemblyResolverHelper.CustomAssembliesResolver(assemblies));
    return httpConfiguration;
  }

  private sealed class CustomAssembliesResolver : IAssembliesResolver
  {
    private readonly Assembly[] _assemblies;

    public CustomAssembliesResolver(Assembly[] assemblies)
    {
      if (assemblies == null)
        throw new ArgumentNullException(nameof (assemblies));
      this._assemblies = assemblies.Length != 0 ? assemblies : throw new PXArgumentException(nameof (assemblies), "You must specify at least one assembly.");
    }

    public ICollection<Assembly> GetAssemblies()
    {
      return (ICollection<Assembly>) new List<Assembly>((IEnumerable<Assembly>) this._assemblies);
    }
  }
}
