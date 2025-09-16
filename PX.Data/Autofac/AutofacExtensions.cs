// Decompiled with JetBrains decompiler
// Type: Autofac.AutofacExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac.Builder;
using Autofac.Features.Scanning;
using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace Autofac;

[PXInternalUseOnly]
public static class AutofacExtensions
{
  internal static 
  #nullable disable
  IRegistrationBuilder<TLimit, TReflectionActivatorData, TStyle> WithInternalConstructor<TLimit, TReflectionActivatorData, TStyle>(
    this IRegistrationBuilder<TLimit, TReflectionActivatorData, TStyle> registration)
    where TReflectionActivatorData : ReflectionActivatorData
  {
    return RegistrationExtensions.FindConstructorsWith<TLimit, TReflectionActivatorData, TStyle>(registration, (Func<System.Type, ConstructorInfo[]>) (type => type.GetTypeInfo().DeclaredConstructors.Where<ConstructorInfo>((Func<ConstructorInfo, bool>) (c => c.IsPublic || c.IsAssembly)).ToArray<ConstructorInfo>()));
  }

  public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterAssemblyTypesAssignableToWithCachingStatic(
    ContainerBuilder builder,
    Func<System.Type, bool> predicate,
    System.Type baseType,
    Assembly[] assemblies)
  {
    List<System.Type> substituteTypes = AutofacExtensions.GetSubstituteTypes("Module_", predicate, baseType, assemblies);
    return RegistrationExtensions.RegisterTypes(builder, substituteTypes.ToArray());
  }

  public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterAssemblyTypesAssignableToWithCaching(
    this ContainerBuilder builder,
    Func<System.Type, bool> predicate,
    System.Type baseType,
    params Assembly[] assemblies)
  {
    List<System.Type> substituteTypes = AutofacExtensions.GetSubstituteTypes("Autofac_", predicate, baseType, assemblies);
    return RegistrationExtensions.RegisterTypes(builder, substituteTypes.ToArray());
  }

  private static List<System.Type> GetSubstituteTypes(
    string prefix,
    Func<System.Type, bool> predicate,
    System.Type baseType,
    params Assembly[] assemblies)
  {
    string str = (string) null;
    if (predicate != null)
      str = $"{predicate.Target.GetType().FullName} {predicate.Method.ToString()}";
    else if (baseType != (System.Type) null)
      str = baseType.AssemblyQualifiedName;
    string key = prefix + str;
    List<System.Type> substituteTypes = new List<System.Type>();
    foreach (System.Type enumTypesInAssembly in PXSubstManager.EnumTypesInAssemblies(key, (List<Exception>) null, true, assemblies))
    {
      if (enumTypesInAssembly.IsClass && !enumTypesInAssembly.IsAbstract)
      {
        if (predicate != null && predicate(enumTypesInAssembly))
        {
          substituteTypes.Add(enumTypesInAssembly);
          PXSubstManager.AddTypeToNamedList(key, enumTypesInAssembly);
        }
        else if (baseType != (System.Type) null && baseType.IsAssignableFrom(enumTypesInAssembly))
        {
          substituteTypes.Add(enumTypesInAssembly);
          PXSubstManager.AddTypeToNamedList(key, enumTypesInAssembly);
        }
      }
    }
    PXSubstManager.SaveTypeCache(key);
    return substituteTypes;
  }
}
