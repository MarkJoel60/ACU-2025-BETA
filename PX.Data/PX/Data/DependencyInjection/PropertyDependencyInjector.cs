// Decompiled with JetBrains decompiler
// Type: PX.Data.DependencyInjection.PropertyDependencyInjector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Core;
using PX.Api;
using PX.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.DependencyInjection;

/// <exclude />
[Obfuscation(Exclude = true)]
internal class PropertyDependencyInjector : IDependencyInjector
{
  private readonly IComponentContext _componentContext;

  public PropertyDependencyInjector(IComponentContext componentContext)
  {
    this._componentContext = componentContext;
  }

  /// <param name="graph">Instance to inject to.</param>
  /// <param name="graphType">
  /// Corresponds to the first parameter of <see cref="M:PX.Data.PXGraph.CreateInstance(System.Type,System.String)" />.
  /// Can be different from <paramref name="graph" /><c>.GetType()</c> when graph is customized.</param>
  /// <param name="prefix">Corresponds to the second parameter of <see cref="M:PX.Data.PXGraph.CreateInstance(System.Type,System.String)" />.</param>
  public void InjectDependencies(PXGraph graph, System.Type graphType, string prefix)
  {
    IEnumerable<PropertyInfo> injectableProperties1 = PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.GetInjectableProperties((object) graph);
    if (injectableProperties1 != null)
      EnumerableExtensions.ForEach<PropertyInfo>(injectableProperties1, (System.Action<PropertyInfo>) (p => PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.InjectProperty(this._componentContext, (object) graph, (Parameter) this.GetGraphParameters(graph), p)));
    if (graph.Extensions == null)
      return;
    foreach (PXGraphExtension extension1 in graph.Extensions)
    {
      PXGraphExtension extension = extension1;
      IEnumerable<PropertyInfo> injectableProperties2 = PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.GetInjectableProperties((object) extension);
      if (injectableProperties2 != null)
        EnumerableExtensions.ForEach<PropertyInfo>(injectableProperties2, (System.Action<PropertyInfo>) (p => PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.InjectProperty(this._componentContext, (object) extension, (Parameter) this.GetGraphExtensionParameters(graph, extension), p)));
    }
  }

  /// <param name="action">Instance to inject to.</param>
  public void InjectDependencies(PXAction action)
  {
    IEnumerable<PropertyInfo> injectableProperties = PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.GetInjectableProperties((object) action);
    if (injectableProperties == null)
      return;
    EnumerableExtensions.ForEach<PropertyInfo>(injectableProperties, (System.Action<PropertyInfo>) (p => PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.InjectProperty(this._componentContext, (object) action, (Parameter) this.GetActionParameters(action), p)));
  }

  /// <param name="attribute">Instance to inject to.</param>
  /// <param name="cache"><tt>PXCache</tt> instance to which the <paramref name="attribute" /> is attached.</param>
  public void InjectDependencies(PXEventSubscriberAttribute attribute, PXCache cache)
  {
    if (cache == null)
    {
      IEnumerable<PropertyInfo> injectableProperties = PropertyDependencyInjector.PropertyInjector<InjectDependencyOnTypeLevelAttribute>.GetInjectableProperties((object) attribute);
      if (injectableProperties == null)
        return;
      EnumerableExtensions.ForEach<PropertyInfo>(injectableProperties, (System.Action<PropertyInfo>) (p => PropertyDependencyInjector.PropertyInjector<InjectDependencyOnTypeLevelAttribute>.InjectProperty(this._componentContext, (object) attribute, (Parameter) this.GetEventSubscriberAttributeParameters(attribute), p)));
    }
    else
    {
      IEnumerable<PropertyInfo> injectableProperties = PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.GetInjectableProperties((object) attribute);
      if (injectableProperties == null)
        return;
      EnumerableExtensions.ForEach<PropertyInfo>(injectableProperties, (System.Action<PropertyInfo>) (p => PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.InjectProperty(this._componentContext, (object) attribute, (Parameter) this.GetEventSubscriberAttributeParameters(attribute, cache), p)));
    }
  }

  /// <param name="action">Instance to inject to.</param>
  public void InjectDependencies(PXSelectBase indexer)
  {
    IEnumerable<PropertyInfo> injectableProperties = PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.GetInjectableProperties((object) indexer);
    if (injectableProperties == null)
      return;
    EnumerableExtensions.ForEach<PropertyInfo>(injectableProperties, (System.Action<PropertyInfo>) (p => PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.InjectProperty(this._componentContext, (object) indexer, (Parameter) this.GetIndexerParameters(indexer), p)));
  }

  private PropertyDependencyInjector.AutoWiringWithParametersParameter GetGraphParameters(
    PXGraph graph)
  {
    return new PropertyDependencyInjector.AutoWiringWithParametersParameter(graph.GetType(), new Parameter[2]
    {
      (Parameter) new TypedParameter(CustomizedTypeManager.GetTypeNotCustomized(graph), (object) graph),
      (Parameter) new TypedParameter(typeof (PXGraph), (object) graph)
    });
  }

  private PropertyDependencyInjector.AutoWiringWithParametersParameter GetGraphExtensionParameters(
    PXGraph graph,
    PXGraphExtension extension)
  {
    return new PropertyDependencyInjector.AutoWiringWithParametersParameter(extension.GetType(), new Parameter[4]
    {
      (Parameter) new TypedParameter(CustomizedTypeManager.GetTypeNotCustomized(graph), (object) graph),
      (Parameter) new TypedParameter(typeof (PXGraph), (object) graph),
      (Parameter) new TypedParameter(extension.GetType(), (object) extension),
      (Parameter) new TypedParameter(typeof (PXGraphExtension), (object) extension)
    });
  }

  private PropertyDependencyInjector.AutoWiringWithParametersParameter GetActionParameters(
    PXAction action)
  {
    return new PropertyDependencyInjector.AutoWiringWithParametersParameter(action.GetType(), new Parameter[4]
    {
      (Parameter) new TypedParameter(action.GetType(), (object) action),
      (Parameter) new TypedParameter(typeof (PXAction), (object) action),
      (Parameter) new TypedParameter(CustomizedTypeManager.GetTypeNotCustomized(action.Graph), (object) action.Graph),
      (Parameter) new TypedParameter(typeof (PXGraph), (object) action.Graph)
    });
  }

  private PropertyDependencyInjector.AutoWiringWithParametersParameter GetEventSubscriberAttributeParameters(
    PXEventSubscriberAttribute attribute,
    PXCache cache)
  {
    return new PropertyDependencyInjector.AutoWiringWithParametersParameter(attribute.GetType(), new Parameter[6]
    {
      (Parameter) new TypedParameter(attribute.GetType(), (object) attribute),
      (Parameter) new TypedParameter(typeof (PXEventSubscriberAttribute), (object) attribute),
      (Parameter) new TypedParameter(cache.GetType(), (object) cache),
      (Parameter) new TypedParameter(typeof (PXCache), (object) cache),
      (Parameter) new TypedParameter(CustomizedTypeManager.GetTypeNotCustomized(cache.Graph), (object) cache.Graph),
      (Parameter) new TypedParameter(typeof (PXGraph), (object) cache.Graph)
    });
  }

  private PropertyDependencyInjector.AutoWiringWithParametersParameter GetEventSubscriberAttributeParameters(
    PXEventSubscriberAttribute attribute)
  {
    return new PropertyDependencyInjector.AutoWiringWithParametersParameter(attribute.GetType(), new Parameter[2]
    {
      (Parameter) new TypedParameter(attribute.GetType(), (object) attribute),
      (Parameter) new TypedParameter(typeof (PXEventSubscriberAttribute), (object) attribute)
    });
  }

  private PropertyDependencyInjector.AutoWiringWithParametersParameter GetIndexerParameters(
    PXSelectBase indexer)
  {
    return new PropertyDependencyInjector.AutoWiringWithParametersParameter(indexer.GetType(), new Parameter[2]
    {
      (Parameter) new TypedParameter(indexer.GetType(), (object) indexer),
      (Parameter) new TypedParameter(typeof (PXSelectBase), (object) indexer)
    });
  }

  [Obfuscation(Exclude = true)]
  internal static class PropertyInjector<AttributeType> where AttributeType : Attribute
  {
    private static readonly ConcurrentDictionary<System.Type, PropertyInfo[]> TypeProperties = new ConcurrentDictionary<System.Type, PropertyInfo[]>();
    private static readonly ConcurrentDictionary<PropertyInfo, Action<object, object>> PropertySetters = new ConcurrentDictionary<PropertyInfo, Action<object, object>>();
    private static readonly MethodInfo CallPropertySetterOpenGenericMethod = typeof (PropertyDependencyInjector.PropertyInjector<AttributeType>).GetTypeInfo().GetDeclaredMethod("CallPropertySetter");

    private static void CallPropertySetter<TDeclaringType, TValue>(
      Action<TDeclaringType, TValue> setter,
      object target,
      object value)
    {
      setter((TDeclaringType) target, (TValue) value);
    }

    private static IEnumerable<PropertyInfo> GetProperties(System.Type type)
    {
      return (IEnumerable<PropertyInfo>) PropertyDependencyInjector.PropertyInjector<AttributeType>.TypeProperties.GetOrAdd(type, (Func<System.Type, PropertyInfo[]>) (t =>
      {
        IEnumerable<PropertyInfo> propertyInfos = (IEnumerable<PropertyInfo>) t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        for (; t != typeof (object) && t != (System.Type) null; t = t.BaseType)
          propertyInfos = propertyInfos.Concat<PropertyInfo>(((IEnumerable<PropertyInfo>) t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => ((IEnumerable<MethodInfo>) p.GetAccessors()).All<MethodInfo>((Func<MethodInfo, bool>) (pa => pa.IsPrivate)))));
        return propertyInfos.Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.CanWrite && p.IsDefined(typeof (AttributeType)))).ToArray<PropertyInfo>();
      }));
    }

    public static void InjectProperty(
      IComponentContext context,
      object instance,
      Parameter resolveParameters,
      PropertyInfo property)
    {
      ParameterInfo parameter = property.SetMethod.GetParameters()[0];
      Func<object> func;
      if (!resolveParameters.CanSupplyValue(parameter, context, ref func))
        return;
      PropertyDependencyInjector.PropertyInjector<AttributeType>.PropertySetters.GetOrAdd(property, (Func<PropertyInfo, Action<object, object>>) (p =>
      {
        MethodInfo setMethod = p.SetMethod;
        System.Type declaringType = setMethod.DeclaringType;
        System.Type parameterType = setMethod.GetParameters()[0].ParameterType;
        Delegate target = setMethod.CreateDelegate(typeof (Action<,>).MakeGenericType(declaringType, parameterType));
        return (Action<object, object>) PropertyDependencyInjector.PropertyInjector<AttributeType>.CallPropertySetterOpenGenericMethod.MakeGenericMethod(declaringType, parameterType).CreateDelegate(typeof (Action<object, object>), (object) target);
      }))(instance, func());
    }

    public static IEnumerable<PropertyInfo> GetInjectableProperties(object instance)
    {
      IEnumerable<PropertyInfo> properties = PropertyDependencyInjector.PropertyInjector<AttributeType>.GetProperties(CustomizedTypeManager.GetTypeNotCustomized(ExceptionExtensions.CheckIfNull<object>(instance, nameof (instance), (string) null).GetType()));
      return !properties.Any<PropertyInfo>() ? (IEnumerable<PropertyInfo>) null : properties;
    }
  }

  [Obfuscation(Exclude = true)]
  private class AutoWiringWithParametersParameter : Parameter
  {
    private readonly Parameter[] _parameters;

    public AutoWiringWithParametersParameter(System.Type instanceType, params Parameter[] parameters)
    {
      NamedParameter namedParameter = new NamedParameter("Autofac.AutowiringPropertyInjector.InstanceType", (object) instanceType);
      this._parameters = EnumerableExtensions.Append<Parameter>(parameters, (Parameter) namedParameter);
    }

    public virtual bool CanSupplyValue(
      ParameterInfo pi,
      IComponentContext context,
      out Func<object> valueProvider)
    {
      if (pi == null)
        throw new ArgumentNullException(nameof (pi));
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      valueProvider = (Func<object>) null;
      TypedService service = new TypedService(pi.ParameterType);
      Autofac.Core.ServiceRegistration registration;
      if (context.ComponentRegistry.TryGetServiceRegistration((Service) service, ref registration))
        valueProvider = (Func<object>) (() =>
        {
          IComponentContext icomponentContext = context;
          ResolveRequest resolveRequest = new ResolveRequest((Service) service, registration, (IEnumerable<Parameter>) this._parameters, (IComponentRegistration) null);
          ref ResolveRequest local = ref resolveRequest;
          return icomponentContext.ResolveComponent(ref local);
        });
      return valueProvider != null;
    }
  }
}
