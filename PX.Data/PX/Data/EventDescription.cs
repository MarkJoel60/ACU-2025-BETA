// Decompiled with JetBrains decompiler
// Type: PX.Data.EventDescription
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

#nullable enable
namespace PX.Data;

internal static class EventDescription
{
  public static readonly ImmutableDictionary<string, EventDescription.Definition> NameToDefinition = ImmutableDictionary.ToImmutableDictionary<EventDescription.Definition, string>((IEnumerable<EventDescription.Definition>) new EventDescription.Definition[17]
  {
    PXGraph.RowSelectingEvents.Definition,
    PXGraph.RowSelectedEvents.Definition,
    PXGraph.RowInsertingEvents.Definition,
    PXGraph.RowInsertedEvents.Definition,
    PXGraph.RowUpdatingEvents.Definition,
    PXGraph.RowUpdatedEvents.Definition,
    PXGraph.RowDeletingEvents.Definition,
    PXGraph.RowDeletedEvents.Definition,
    PXGraph.RowPersistingEvents.Definition,
    PXGraph.RowPersistedEvents.Definition,
    PXGraph.FieldSelectingEvents.Definition,
    PXGraph.FieldDefaultingEvents.Definition,
    PXGraph.FieldUpdatingEvents.Definition,
    PXGraph.FieldVerifyingEvents.Definition,
    PXGraph.FieldUpdatedEvents.Definition,
    PXGraph.CommandPreparingEvents.Definition,
    PXGraph.ExceptionHandlingEvents.Definition
  }, (Func<EventDescription.Definition, string>) (d => d.EventName), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  public static EventDescription.Description? GetNamed(string eventName, MethodInfo method)
  {
    EventDescription.Definition definition;
    if (EventDescription.NameToDefinition.TryGetValue(eventName, ref definition))
    {
      ParameterInfo[] parameters = method.GetParameters();
      if (!method.IsDefined(typeof (PXSuppressEventValidationAttribute)))
      {
        if (parameters == null || parameters.Length < 2 || !(parameters[1].ParameterType != definition.ArgsType))
        {
          if (parameters != null && parameters.Length == 3)
          {
            ParameterInfo parameterInfo = parameters[2];
            if (definition.OwnDelegateType.IsAssignableFrom(parameterInfo.ParameterType))
              goto label_6;
          }
          else
            goto label_6;
        }
        throw new Exception($"Invalid argument type in the event handler {method.DeclaringType.FullName}::{method.Name}");
      }
label_6:
      if (parameters.Length == 2)
        return new EventDescription.Description?(new EventDescription.Description(definition, false));
      if (parameters.Length == 3)
        return new EventDescription.Description?(new EventDescription.Description(definition, true));
    }
    return new EventDescription.Description?();
  }

  public static EventDescription.Description? GetTyped(System.Type eventType, MethodInfo method)
  {
    string genericSimpleName = MainTools.GetGenericSimpleName(eventType);
    EventDescription.Definition definition;
    if (EventDescription.NameToDefinition.TryGetValue(genericSimpleName, ref definition))
    {
      ParameterInfo[] parameters = method.GetParameters();
      if (!method.IsDefined(typeof (PXSuppressEventValidationAttribute)))
      {
        if (parameters == null || parameters.Length < 1 || !(parameters[0].ParameterType != eventType))
        {
          if (parameters != null && parameters.Length == 2)
          {
            ParameterInfo parameterInfo = parameters[1];
            if (definition.OwnDelegateType.IsAssignableFrom(parameterInfo.ParameterType))
              goto label_6;
          }
          else
            goto label_6;
        }
        throw new Exception($"Invalid argument type in the event handler {method.DeclaringType.FullName}::{method.Name}");
      }
label_6:
      if (parameters.Length == 1)
        return new EventDescription.Description?(new EventDescription.Description(definition, false));
      if (parameters.Length == 2)
        return new EventDescription.Description?(new EventDescription.Description(definition, true));
    }
    return new EventDescription.Description?();
  }

  internal readonly struct Definition : IEquatable<EventDescription.Definition>
  {
    public Definition(
      System.Type ownDelegateType,
      System.Type argsType,
      System.Type eventCollectionType,
      System.Type interceptorDelegateType)
    {
      string name = ownDelegateType.Name;
      this.EventName = name.Substring(2, name.Length - 2);
      this.ArgsType = argsType;
      this.EventCollectionType = eventCollectionType;
      this.OwnDelegateType = ownDelegateType;
      this.InterceptorDelegateType = interceptorDelegateType;
    }

    public string EventName { get; }

    public System.Type ArgsType { get; }

    public System.Type EventCollectionType { get; }

    public System.Type OwnDelegateType { get; }

    public System.Type InterceptorDelegateType { get; }

    public override bool Equals(object? obj)
    {
      return obj is EventDescription.Definition definition && definition.Equals(this);
    }

    public bool Equals(EventDescription.Definition other) => other.EventName == this.EventName;

    public override int GetHashCode() => HashCode.Combine<string>(this.EventName);
  }

  internal readonly struct Description(EventDescription.Definition definition, bool isInterceptor)
  {
    private readonly EventDescription.Definition _definition = definition;

    public bool IsInterceptor { get; } = isInterceptor;

    public string EventName => this._definition.EventName;

    public System.Type ArgsType => this._definition.ArgsType;

    public System.Type EventCollectionType => this._definition.EventCollectionType;

    public System.Type DelegateType
    {
      get
      {
        return !this.IsInterceptor ? this._definition.OwnDelegateType : this._definition.InterceptorDelegateType;
      }
    }
  }

  public class Msg
  {
    public const string InvalidArgumentTypeInEventHandler = "Invalid argument type in the event handler {0}::{1}";
  }
}
