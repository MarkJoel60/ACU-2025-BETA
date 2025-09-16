// Decompiled with JetBrains decompiler
// Type: PX.Data.DependencyInjection.ParameterResolutionHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using PX.Common;
using System;
using System.Reflection;

#nullable enable
namespace PX.Data.DependencyInjection;

/// <summary>An Autofac parameter resolution helper.</summary>
[PXInternalUseOnly]
public static class ParameterResolutionHelper
{
  public static 
  #nullable disable
  IRegistrationBuilder<TLimit, TReflectionActivatorData, TStyle> WithRegisteredDerivedTypeAsParameter<TLimit, TReflectionActivatorData, TStyle>(
    this IRegistrationBuilder<TLimit, TReflectionActivatorData, TStyle> registration,
    System.Type baseParameter,
    System.Type derivedParameter)
    where TReflectionActivatorData : ReflectionActivatorData
  {
    ExceptionExtensions.ThrowOnNull<IRegistrationBuilder<TLimit, TReflectionActivatorData, TStyle>>(registration, nameof (registration), (string) null);
    ExceptionExtensions.ThrowOnNull<System.Type>(baseParameter, nameof (baseParameter), (string) null);
    ExceptionExtensions.ThrowOnNull<System.Type>(derivedParameter, nameof (derivedParameter), (string) null);
    if (!baseParameter.IsAssignableFrom(derivedParameter))
      throw new ArgumentException($"{derivedParameter.FullName} can not be converted to {baseParameter.FullName}", nameof (derivedParameter));
    return RegistrationExtensions.WithParameter<TLimit, TReflectionActivatorData, TStyle>(registration, (Parameter) new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>) ((parInfo, ctx) => parInfo.ParameterType == baseParameter), (Func<ParameterInfo, IComponentContext, object>) ((parInfo, ctx) => ResolutionExtensions.Resolve(ctx, derivedParameter))));
  }
}
