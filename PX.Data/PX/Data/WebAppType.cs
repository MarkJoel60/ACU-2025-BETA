// Decompiled with JetBrains decompiler
// Type: PX.Data.WebAppType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using PX.Common;
using System;

#nullable enable
namespace PX.Data;

/// <summary>
/// Represents the global web application type (e.g., ERP, Support Portal, etc.).<br />
/// Different application types use the same database but typically have their own subsets of data,
/// split up by the <see cref="F:PX.DbServices.Model.AcumaticaDb.WebAppType" /> column in the database.
/// </summary>
[PXInternalUseOnly]
public abstract class WebAppType
{
  /// <summary>
  /// The identifier of the current application type. Must be unique.
  /// </summary>
  /// <remarks>We use a separate <see langword="int" /> value and not a type name of the current
  /// <see cref="T:PX.Data.WebAppType" /> implementation because this value is persisted into the database
  /// to the <see cref="F:PX.DbServices.Model.AcumaticaDb.WebAppType" /> column.
  /// This solution has some drawbacks since we cannot make sure that the values are unique across all
  /// application types, but allows us to optimize on the database level.</remarks>
  public abstract int AppTypeId { get; }

  [Obsolete("WebAppType will be injected into constructors of PXDatabaseProvider and SQLTree classes")]
  public static 
  #nullable disable
  WebAppType Current { get; private set; }

  [Obsolete]
  private class ServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (container => WebAppType.Current = ResolutionExtensions.Resolve<WebAppType>((IComponentContext) container)));
    }
  }

  [Obsolete("Will be removed once IsPortal flag is eliminated")]
  internal static class Predefined
  {
    public const int Erp = 0;
    public const int Portal = 1;
  }
}
