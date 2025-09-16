// Decompiled with JetBrains decompiler
// Type: PX.Data.ContainerBuilderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data;

internal static class ContainerBuilderExtensions
{
  /// <summary>
  /// <para>Register a component as a <typeparamref name="TInterface" /> service, making it a default implementation
  /// and allowing you to replace it with a single custom implementation.</para>
  /// <para>If there is more than one default implementation or more than one custom implementation registered,
  /// or default and custom registrations have different lifetime, an exception is thrown.</para>
  /// </summary>
  /// <remarks>
  /// <para>Default = OK<br />
  /// Default + Custom = OK<br />
  /// Default + Default = Exception<br />
  /// Default + Custom + Custom = Exception</para>
  /// <para>
  /// Default (SingleInstance) + Custom (SingleInstance) = OK<br />
  /// Default (SingleInstance) + Custom (InstancePerDependency) = Exception</para>
  /// </remarks>
  /// <param name="builder">Container builder.</param>
  /// <typeparam name="TInterface">Service type.</typeparam>
  /// <typeparam name="TDefaultType">The type of the component implementation.</typeparam>
  /// <returns>Registration builder allowing the registration to be configured.</returns>
  /// <exception cref="T:System.InvalidOperationException">More than one default registration or more than one custom registration exists,
  /// or registrations have different lifetime.</exception>
  /// <exception cref="T:System.ArgumentNullException"><paramref name="builder" /> is <see langword="null" /></exception>
  public static 
  #nullable disable
  IRegistrationBuilder<TDefaultType, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterAsDefaultAllowingSingleOverride<TInterface, TDefaultType>(
    this ContainerBuilder builder)
    where TDefaultType : class
  {
    return builder.RegisterAsDefaultAllowingSingleOverrideImpl<TInterface, TDefaultType, ConcreteReflectionActivatorData>((Func<ContainerBuilder, IRegistrationBuilder<TDefaultType, ConcreteReflectionActivatorData, SingleRegistrationStyle>>) (b => RegistrationExtensions.RegisterType<TDefaultType>(b)));
  }

  /// <summary>
  /// <para>Register a component as a <typeparamref name="TInterface" /> service, making it a default implementation
  /// and allowing you to replace it with a single custom implementation.</para>
  /// <para>If there is more than one default implementation or more than one custom implementation registered,
  /// or default and custom registrations have different lifetime, an exception is thrown.</para>
  /// </summary>
  /// <remarks>
  /// <para>Default = OK<br />
  /// Default + Custom = OK<br />
  /// Default + Default = Exception<br />
  /// Default + Custom + Custom = Exception</para>
  /// <para>
  /// Default (SingleInstance) + Custom (SingleInstance) = OK<br />
  /// Default (SingleInstance) + Custom (InstancePerDependency) = Exception</para>
  /// </remarks>
  /// <param name="builder">Container builder.</param>
  /// <param name="delegate">The delegate to register.</param>
  /// <typeparam name="TInterface">Service type.</typeparam>
  /// <returns>Registration builder allowing the registration to be configured.</returns>
  /// <exception cref="T:System.InvalidOperationException">More than one default registration or more than one custom registration exists,
  /// or registrations have different lifetime.</exception>
  /// <exception cref="T:System.ArgumentNullException"><paramref name="builder" /> or <paramref name="delegate" /> is <see langword="null" /></exception>
  public static IRegistrationBuilder<TInterface, SimpleActivatorData, SingleRegistrationStyle> RegisterAsDefaultAllowingSingleOverride<TInterface>(
    this ContainerBuilder builder,
    Func<IComponentContext, TInterface> @delegate)
  {
    return builder.RegisterAsDefaultAllowingSingleOverrideImpl<TInterface, TInterface, SimpleActivatorData>((Func<ContainerBuilder, IRegistrationBuilder<TInterface, SimpleActivatorData, SingleRegistrationStyle>>) (b => RegistrationExtensions.Register<TInterface>(b, @delegate)));
  }

  private static IRegistrationBuilder<TDefaultType, TActivatorData, SingleRegistrationStyle> RegisterAsDefaultAllowingSingleOverrideImpl<TInterface, TDefaultType, TActivatorData>(
    this ContainerBuilder builder,
    Func<ContainerBuilder, IRegistrationBuilder<TDefaultType, TActivatorData, SingleRegistrationStyle>> registrationDelegate)
  {
    if (registrationDelegate == null)
      throw new ArgumentNullException(nameof (registrationDelegate));
    builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (scope =>
    {
      IComponentRegistration[] array1 = ((IComponentContext) scope).ComponentRegistry.RegistrationsFor((Service) new TypedService(typeof (TInterface))).ToArray<IComponentRegistration>();
      IComponentRegistration[] array2 = ((IEnumerable<IComponentRegistration>) array1).Where<IComponentRegistration>(new Func<IComponentRegistration, bool>(IsDefaultRegistration)).ToArray<IComponentRegistration>();
      if (array2.Length > 1)
        throw new InvalidOperationException($"Multiple default registrations of type {typeof (TInterface).FullName} are not allowed.Current default implementations: {GetInstancesFullNameList((IEnumerable<IComponentRegistration>) array2)}");
      IComponentRegistration[] array3 = ((IEnumerable<IComponentRegistration>) array1).Where<IComponentRegistration>(new Func<IComponentRegistration, bool>(IsCustomRegistration)).ToArray<IComponentRegistration>();
      if (array3.Length > 1)
        throw new InvalidOperationException($"Multiple custom registrations of type {typeof (TInterface).FullName} are not allowed.{Environment.NewLine}Current custom implementations:{Environment.NewLine}{GetInstancesFullNameList((IEnumerable<IComponentRegistration>) array3)}");
      IGrouping<System.Type, IComponentRegistration>[] array4 = ((IEnumerable<IComponentRegistration>) array1).GroupBy<IComponentRegistration, System.Type>((Func<IComponentRegistration, System.Type>) (r => r.Lifetime.GetType())).ToArray<IGrouping<System.Type, IComponentRegistration>>();
      if (array4.Length > 1)
        throw new InvalidOperationException($"Different lifetime is not allowed for the implementations of {typeof (TInterface).FullName}.{Environment.NewLine}Current implementations with different lifetime:{Environment.NewLine}{GetInstancesFullNameListByLifetime(array4)}");
    }));
    return RegistrationExtensions.PreserveExistingDefaults<TDefaultType, TActivatorData, SingleRegistrationStyle>(registrationDelegate(builder).As<TInterface>().WithMetadata("DefaultRegistrationMetadata", (object) ContainerBuilderExtensions.DefaultRegistrationMetadata.Instance));

    static bool IsDefaultRegistration(IComponentRegistration registration)
    {
      object obj;
      return registration.Metadata.TryGetValue("DefaultRegistrationMetadata", out obj) && obj is ContainerBuilderExtensions.DefaultRegistrationMetadata;
    }

    static bool IsCustomRegistration(IComponentRegistration registration)
    {
      return !IsDefaultRegistration(registration);
    }

    static string GetInstancesFullNameList(IEnumerable<IComponentRegistration> instances)
    {
      return string.Join(", ", instances.Select<IComponentRegistration, string>((Func<IComponentRegistration, string>) (@object => @object.Activator.LimitType.FullName)));
    }

    static string GetInstancesFullNameListByLifetime(
      IGrouping<System.Type, IComponentRegistration>[] instances)
    {
      return string.Join(Environment.NewLine, ((IEnumerable<IGrouping<System.Type, IComponentRegistration>>) instances).Select<IGrouping<System.Type, IComponentRegistration>, string>((Func<IGrouping<System.Type, IComponentRegistration>, string>) (g => $"{g.Key.Name}: {GetInstancesFullNameList((IEnumerable<IComponentRegistration>) g)}")));
    }
  }

  internal class DefaultRegistrationMetadata
  {
    public static ContainerBuilderExtensions.DefaultRegistrationMetadata Instance { get; } = new ContainerBuilderExtensions.DefaultRegistrationMetadata();

    private DefaultRegistrationMetadata()
    {
    }
  }
}
