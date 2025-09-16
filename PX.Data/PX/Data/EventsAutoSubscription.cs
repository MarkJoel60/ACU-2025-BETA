// Decompiled with JetBrains decompiler
// Type: PX.Data.EventsAutoSubscription
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

#nullable enable
namespace PX.Data;

internal class EventsAutoSubscription
{
  internal static void Process(
    ILGenerator ilGenerator,
    System.Type graph,
    List<System.Type> graphExtensions,
    List<System.Type> persistingCaches,
    List<System.Type> readonlyCaches,
    out Dictionary<System.Type, PXGraph.AlteredState> alteredState)
  {
    alteredState = new Dictionary<System.Type, PXGraph.AlteredState>();
    EventsAutoSubscription.Processor.ProcessGraph(graph, persistingCaches, readonlyCaches, alteredState, ilGenerator);
    for (int index = 0; index < graphExtensions.Count<System.Type>(); ++index)
      EventsAutoSubscription.Processor.ProcessExtension(graphExtensions[index], index, graph, persistingCaches, readonlyCaches, alteredState, ilGenerator);
    if (alteredState.Count <= 0)
      return;
    EventsAutoSubscription.ProcessAlteredState(alteredState);
  }

  private static void ProcessAlteredState(
    Dictionary<System.Type, PXGraph.AlteredState> alteredState)
  {
    foreach (KeyValuePair<System.Type, PXGraph.AlteredState> keyValuePair in alteredState)
    {
      System.Type type1;
      PXGraph.AlteredState alteredState1;
      EnumerableExtensions.Deconstruct<System.Type, PXGraph.AlteredState>(keyValuePair, ref type1, ref alteredState1);
      System.Type table = type1;
      PXGraph.AlteredState alteredState2 = alteredState1;
      alteredState2.Generator.Emit(OpCodes.Ret);
      Lazy<IEnumerable<System.Type>> lazy = new Lazy<IEnumerable<System.Type>>((Func<IEnumerable<System.Type>>) (() => (IEnumerable<System.Type>) PXCache._GetExtensions(table, false)));
      foreach (KeyValuePair<string, List<PXEventSubscriberAttribute>> field in alteredState2.Fields)
      {
        string str1;
        List<PXEventSubscriberAttribute> subscriberAttributeList1;
        EnumerableExtensions.Deconstruct<string, List<PXEventSubscriberAttribute>>(field, ref str1, ref subscriberAttributeList1);
        string str2 = str1;
        List<PXEventSubscriberAttribute> subscriberAttributeList2 = subscriberAttributeList1;
        System.Type itemType = table;
        if (table.GetProperty(str2, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public) == (PropertyInfo) null)
        {
          foreach (System.Type type2 in lazy.Value.Reverse<System.Type>())
          {
            if (type2.IsDefined(typeof (PXDBInterceptorAttribute), true) && type2.GetProperty(str2, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public) != (PropertyInfo) null)
            {
              itemType = type2;
              break;
            }
          }
        }
        foreach (PXEventSubscriberAttribute attr in subscriberAttributeList2)
        {
          attr.InjectAttributeDependencies((PXCache) null);
          attr.prepare(str2, -1, itemType);
        }
      }
    }
  }

  private class Processor
  {
    private readonly ILGenerator _il;
    private readonly System.Type _graph;
    private readonly System.Type _inspectedType;
    private readonly int _extensionIndex;
    private readonly List<System.Type> _persistingCaches;
    private readonly List<System.Type> _readonlyCaches;
    private readonly Dictionary<System.Type, PXGraph.AlteredState> _alteredState;
    private static readonly MethodInfo Type_GetTypeFromHandle = new Func<RuntimeTypeHandle, System.Type>(System.Type.GetTypeFromHandle).Method;
    private static readonly System.Reflection.FieldInfo PXGraph_Views = typeof (PXGraph).GetField("Views");
    private static readonly System.Reflection.FieldInfo PXViewCollection_Caches = typeof (PXViewCollection).GetField("Caches");
    private static readonly MethodInfo TypeList_Add = typeof (List<System.Type>).GetMethod("Add", new System.Type[1]
    {
      typeof (System.Type)
    });
    private static readonly System.Reflection.FieldInfo PXGraph_Extensions = typeof (PXGraph).GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic);
    private static readonly ImmutableDictionary<string, MethodInfo> PXGraph_GetEventContainersOf = ImmutableDictionary.ToImmutableDictionary<string, string, MethodInfo>(EventDescription.NameToDefinition.Keys, (Func<string, string>) (name => name), (Func<string, MethodInfo>) (name => typeof (PXGraph).GetProperty(name).GetGetMethod()));

    private Processor(
      ILGenerator il,
      System.Type graph,
      System.Type inspectedType,
      int extensionIndex,
      List<System.Type> persistingCaches,
      List<System.Type> readonlyCaches,
      Dictionary<System.Type, PXGraph.AlteredState> alteredState)
    {
      this._il = il;
      this._graph = graph;
      this._inspectedType = inspectedType;
      this._extensionIndex = extensionIndex;
      this._persistingCaches = persistingCaches;
      this._readonlyCaches = readonlyCaches;
      this._alteredState = alteredState;
    }

    public static void ProcessGraph(
      System.Type graph,
      List<System.Type> persistingCaches,
      List<System.Type> readonlyCaches,
      Dictionary<System.Type, PXGraph.AlteredState> alteredState,
      ILGenerator il)
    {
      EventsAutoSubscription.Processor.CheckForPrivateClassicHandlers(graph);
      new EventsAutoSubscription.Processor(il, graph, graph, -1, persistingCaches, readonlyCaches, alteredState).Process();
    }

    public static void ProcessExtension(
      System.Type extension,
      int extensionIndex,
      System.Type graph,
      List<System.Type> persistingCaches,
      List<System.Type> readonlyCaches,
      Dictionary<System.Type, PXGraph.AlteredState> alteredState,
      ILGenerator il)
    {
      new EventsAutoSubscription.Processor(il, graph, extension, extensionIndex, persistingCaches, readonlyCaches, alteredState).Process();
    }

    private void Process()
    {
      foreach (MethodInfo method in this._inspectedType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
      {
        System.Type eventType;
        System.Type table;
        System.Type field;
        if (EventsAutoSubscription.Processor.IsGenericHandler(method, out eventType, out table, out field))
        {
          this.ProcessGenericHandler(method, eventType, table, field);
        }
        else
        {
          string eventName;
          string tableName;
          string fieldName;
          if (EventsAutoSubscription.Processor.IsClassicHandler(method, out eventName, out tableName, out fieldName))
            this.ProcessClassicHandler(method, eventName, tableName, fieldName);
        }
      }
    }

    private static bool IsGenericHandler(
      MethodInfo method,
      [NotNullWhen(true)] out System.Type eventType,
      [NotNullWhen(true)] out System.Type table,
      out System.Type? field)
    {
      ParameterInfo[] parameters = method.GetParameters();
      if ((EnumerableExtensions.IsNotIn<int>(parameters.Length, 1, 2) || parameters.Length == 2 && !parameters[1].ParameterType.IsSubclassOf(typeof (Delegate)) || parameters.Length >= 1 && !typeof (IAutoSubscriptionEventMarker).IsAssignableFrom(parameters[0].ParameterType) ? 1 : (!parameters[0].ParameterType.IsGenericType ? 1 : 0)) != 0)
      {
        eventType = (System.Type) null;
        table = (System.Type) null;
        field = (System.Type) null;
        return false;
      }
      System.Type[] genericArguments = parameters[0].ParameterType.GetGenericArguments();
      if ((EnumerableExtensions.IsNotIn<int>(genericArguments.Length, 1, 2, 3) || genericArguments.Length >= 2 && !typeof (IBqlField).IsAssignableFrom(genericArguments[1]) ? 1 : (genericArguments.Length != 1 ? 0 : (typeof (IBqlField).IsAssignableFrom(genericArguments[0]) ? 0 : (!typeof (IBqlTable).IsAssignableFrom(genericArguments[0]) ? 1 : 0)))) != 0)
      {
        eventType = (System.Type) null;
        table = (System.Type) null;
        field = (System.Type) null;
        return false;
      }
      eventType = parameters[0].ParameterType;
      if (genericArguments.Length >= 2)
      {
        System.Type type1 = genericArguments[0];
        System.Type type2 = genericArguments[1];
        table = type1;
        field = type2;
      }
      else if (typeof (IBqlField).IsAssignableFrom(genericArguments[0]) && !typeof (IBqlTable).IsAssignableFrom(genericArguments[0]))
      {
        System.Type itemType = BqlCommand.GetItemType(genericArguments[0]);
        System.Type type = genericArguments[0];
        table = itemType;
        field = type;
      }
      else
      {
        System.Type type = genericArguments[0];
        table = type;
        field = (System.Type) null;
      }
      return true;
    }

    private void ProcessGenericHandler(
      MethodInfo handler,
      System.Type eventType,
      System.Type handlerTable,
      System.Type? handlerField)
    {
      System.Type underlyingTable;
      System.Type underlyingField;
      if (!this.TryGetUnderlyingTarget(handlerTable, handlerField, out underlyingTable, out underlyingField))
        return;
      if (eventType.GetGenericTypeDefinition() == typeof (Events.CacheAttached<>))
      {
        System.Type eventType1 = eventType;
        MethodInfo handler1 = handler;
        System.Type field = underlyingField;
        if ((object) field == null)
          field = handlerField;
        this.EmitCallGenericCacheAttached(eventType1, handler1, field);
      }
      else
        this.EmitAddGenericHandler(eventType, handler, handlerTable, handlerField?.Name, underlyingTable, underlyingField?.Name);
    }

    private bool TryGetUnderlyingTarget(
      System.Type handlerTable,
      System.Type? handlerField,
      out System.Type? underlyingTable,
      out System.Type? underlyingField)
    {
      underlyingTable = (System.Type) null;
      underlyingField = (System.Type) null;
      if (typeof (PXMappedCacheExtension).IsAssignableFrom(handlerTable))
      {
        IBqlMapping map = PXCache._mapping.GetMap(this._graph, handlerTable);
        underlyingTable = map.Table;
        if (handlerField != (System.Type) null)
        {
          System.Reflection.FieldInfo fieldInfo = ((IEnumerable<System.Reflection.FieldInfo>) map.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)).FirstOrDefault<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (f => string.Equals(handlerField.Name, f.Name, StringComparison.OrdinalIgnoreCase)));
          if (fieldInfo != (System.Reflection.FieldInfo) null)
          {
            string name = ((System.Type) fieldInfo.GetValue((object) map)).Name;
            underlyingField = underlyingTable.GetNestedType(name);
            if (underlyingField == (System.Type) null)
            {
              System.Type[] typeArray = (System.Type[]) typeof (PXCache<>).MakeGenericType(underlyingTable).GetMethod("GetExtensionTypesStatic").Invoke((object) null, Array.Empty<object>());
              if (typeArray != null)
              {
                foreach (System.Type type in typeArray)
                {
                  underlyingField = type.GetNestedType(name);
                  if (underlyingField != (System.Type) null)
                    break;
                }
              }
            }
            if (underlyingField == (System.Type) null)
              return false;
          }
        }
      }
      return true;
    }

    private void EmitCallGenericCacheAttached(System.Type eventType, MethodInfo handler, System.Type field)
    {
      this.EmitCallCacheAttached(BqlCommand.GetItemType(field), field.DeclaringType.GetProperty(field.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public).Name, handler, (System.Action<ILGenerator>) (il => il.Emit(OpCodes.Newobj, eventType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0])));
    }

    private void EmitAddGenericHandler(
      System.Type eventType,
      MethodInfo handler,
      System.Type handlerTable,
      string? handlerFieldName,
      System.Type? underlyingTable,
      string? underlyingFieldName)
    {
      EventDescription.Description? typed = EventDescription.GetTyped(eventType, handler);
      if (typed.HasValue)
      {
        EventDescription.Description valueOrDefault = typed.GetValueOrDefault();
        if (valueOrDefault.IsInterceptor && underlyingTable != (System.Type) null)
          throw new Exception($"Interceptor delegate is not supported in generic extensions - {handler.DeclaringType.FullName}::{handler.Name}");
        System.Type table = underlyingTable != (System.Type) null ? underlyingTable : (handlerTable.IsSubclassOf(typeof (PXCacheExtension)) ? PXExtensionManager.GetExtendedType(handlerTable) ?? handlerTable : handlerTable);
        this.EmitCallAddHandler(valueOrDefault, table, underlyingFieldName ?? handlerFieldName, handler, (System.Action<EventDescription.Description>) (descr =>
        {
          this.EmitWrapWithGenericAdapter(eventType, descr.ArgsType, descr.IsInterceptor);
          if (!(underlyingTable != (System.Type) null))
            return;
          this.EmitWrapWithEntityRemapper(handlerTable, descr.ArgsType);
        }));
      }
      else if (!handler.IsDefined(typeof (PXSuppressEventValidationAttribute)))
        throw new Exception(string.Format("Failed to subscribe the graph event {2}::{0}. The method signature looks like an event handler, but {1} is not a valid event name.", (object) handler, (object) MainTools.GetGenericSimpleName(eventType), (object) handler.DeclaringType.FullName));
    }

    private static bool IsClassicHandler(
      MethodInfo method,
      [NotNullWhen(true)] out string eventName,
      [NotNullWhen(true)] out string tableName,
      out string? fieldName)
    {
      ParameterInfo[] parameters = method.GetParameters();
      if ((method.IsSpecialName || method.IsGenericMethod || method.Name.Contains("__") || method.Name.EndsWith("lambda_method") || StringExtensions.LastSegment(method.Name, '.').StartsWith("get_") || StringExtensions.LastSegment(method.Name, '.').StartsWith("set_") || method.IsDefined(typeof (PXOverrideAttribute)) || EnumerableExtensions.IsNotIn<int>(parameters.Length, 1, 2, 3) || parameters.Length == 3 && !parameters[2].ParameterType.IsSubclassOf(typeof (Delegate)) || parameters.Length >= 2 && !parameters[1].ParameterType.IsSubclassOf(typeof (EventArgs)) ? 1 : (parameters.Length < 1 ? 0 : (parameters[0].ParameterType != typeof (PXCache) ? 1 : 0))) != 0)
      {
        eventName = (string) null;
        tableName = (string) null;
        fieldName = (string) null;
        return false;
      }
      string[] strArray = method.Name.Split(new char[1]
      {
        '_'
      }, StringSplitOptions.RemoveEmptyEntries);
      (string, string, string) valueTuple;
      if (strArray != null)
      {
        switch (strArray.Length)
        {
          case 2:
            string str1 = strArray[0];
            valueTuple = (strArray[1], str1, (string) null);
            goto label_7;
          case 3:
            string str2 = strArray[0];
            string str3 = strArray[1];
            valueTuple = (strArray[2], str2, str3);
            goto label_7;
        }
      }
      valueTuple = ((string) null, (string) null, (string) null);
label_7:
      (eventName, tableName, fieldName) = valueTuple;
      return eventName != null;
    }

    private void ProcessClassicHandler(
      MethodInfo handler,
      string eventName,
      string tableName,
      string? fieldName)
    {
      System.Type table = this._persistingCaches.FirstOrDefault<System.Type>((Func<System.Type, bool>) (tc => tc.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase)));
      bool flag = false;
      if (table == (System.Type) null)
      {
        if (handler.IsDefined(typeof (PXSuppressEventValidationAttribute)))
          return;
        table = this._readonlyCaches.FirstOrDefault<System.Type>((Func<System.Type, bool>) (tc => tc.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase)));
        flag = true;
      }
      System.Type mapTable = (System.Type) null;
      IBqlMapping map;
      if (table == (System.Type) null && PXCache._mapping.TryGetMap(this._graph, this._inspectedType, tableName, out map))
      {
        flag = true;
        mapTable = map.Extension;
        table = map.Table;
        tableName = table.Name;
        if (fieldName != null)
        {
          System.Reflection.FieldInfo fieldInfo = ((IEnumerable<System.Reflection.FieldInfo>) map.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)).FirstOrDefault<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (f => string.Equals(fieldName, f.Name, StringComparison.OrdinalIgnoreCase)));
          if (fieldInfo != (System.Reflection.FieldInfo) null)
            fieldName = ((System.Type) fieldInfo.GetValue((object) map)).Name;
        }
      }
      if (table == (System.Type) null)
        throw new Exception(string.Format("Failed to subscribe the event {3}::{1} in the graph {0}. The method signature looks like an event handler, but the cache {2} has not been found in the list of auto-initialized caches. Remove unused event handlers from the code.", (object) this._graph.FullName, (object) handler.Name, (object) tableName, (object) handler.DeclaringType.FullName));
      if (flag)
      {
        this.EmitAddTableToPersistingCaches(table);
        this._persistingCaches.Add(table);
        this._readonlyCaches.Remove(table);
      }
      if (string.Equals(eventName, "CacheAttached", StringComparison.OrdinalIgnoreCase))
        this.EmitCallCacheAttached(table, fieldName, handler);
      else
        this.EmitAddClassicHandler(eventName, handler, table, fieldName, mapTable);
    }

    private void EmitAddClassicHandler(
      string eventName,
      MethodInfo handler,
      System.Type table,
      string? fieldName,
      System.Type? mapTable)
    {
      EventDescription.Description? named = EventDescription.GetNamed(eventName, handler);
      if (named.HasValue)
        this.EmitCallAddHandler(named.GetValueOrDefault(), table, fieldName, handler, (System.Action<EventDescription.Description>) (descr =>
        {
          if (!(mapTable != (System.Type) null))
            return;
          this.EmitWrapWithEntityRemapper(mapTable, descr.ArgsType);
        }));
      else if (!handler.IsDefined(typeof (PXSuppressEventValidationAttribute)))
        throw new Exception(string.Format("Failed to subscribe the graph event {2}::{0}. The method signature looks like an event handler, but {1} is not a valid event name.", (object) handler, (object) eventName, (object) handler.DeclaringType.FullName));
    }

    private static void CheckForPrivateClassicHandlers(System.Type? type)
    {
      if (type == (System.Type) null || EnumerableExtensions.IsIn<System.Type>(type, (System.Type) null, typeof (object), typeof (PXGraph), typeof (PXGraphExtension)))
        return;
      foreach (MethodInfo methodInfo in ((IEnumerable<MethodInfo>) type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic)).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.IsPrivate)))
      {
        if (!methodInfo.IsDefined(typeof (PXSuppressEventValidationAttribute)) && EventsAutoSubscription.Processor.IsClassicHandler(methodInfo, out string _, out string _, out string _))
          throw new Exception($"The method looks like a private event handler. Change the access modifier to virtual protected or rename the method {methodInfo.DeclaringType.FullName}::{methodInfo.Name}.");
      }
      EventsAutoSubscription.Processor.CheckForPrivateClassicHandlers(type.BaseType);
    }

    /// <summary>
    /// emits: <c>typeof(TTable)</c>
    /// </summary>
    private void EmitLoadTypeInstance(System.Type type)
    {
      this._il.Emit(OpCodes.Ldtoken, type);
      this._il.Emit(OpCodes.Call, EventsAutoSubscription.Processor.Type_GetTypeFromHandle);
    }

    /// <summary>
    /// emits: <c>graph.Views.Caches.Add(typeof(TTable))</c>
    /// </summary>
    private void EmitAddTableToPersistingCaches(System.Type table)
    {
      this._il.Emit(OpCodes.Ldarg_0);
      this._il.Emit(OpCodes.Ldfld, EventsAutoSubscription.Processor.PXGraph_Views);
      this._il.Emit(OpCodes.Ldfld, EventsAutoSubscription.Processor.PXViewCollection_Caches);
      this.EmitLoadTypeInstance(table);
      this._il.Emit(OpCodes.Callvirt, EventsAutoSubscription.Processor.TypeList_Add);
    }

    /// <summary>
    /// ~emits: <c>mixin.CacheAttachedHandler(cache)</c>
    /// </summary>
    private void EmitCallCacheAttached(
      System.Type table,
      string fieldName,
      MethodInfo handler,
      System.Action<ILGenerator>? emitWrapArgs = null)
    {
      PXGraph.AlteredState alteredAttributes = this.GetAlteredAttributes(table, fieldName, handler);
      if (alteredAttributes == null)
        return;
      ILGenerator generator = alteredAttributes.Generator;
      this.EmitLoadMixinThis(generator);
      generator.Emit(OpCodes.Ldarg_1);
      if (emitWrapArgs != null)
        emitWrapArgs(generator);
      generator.Emit(OpCodes.Callvirt, handler);
    }

    /// <summary>
    /// emits: <c>this</c> or <c>(TGraphExt)this.Extensions[i]</c>
    /// </summary>
    private void EmitLoadMixinThis(ILGenerator? foreignGenerator = null)
    {
      ILGenerator ilGenerator = foreignGenerator ?? this._il;
      if (this._extensionIndex == -1)
      {
        ilGenerator.Emit(OpCodes.Ldarg_0);
      }
      else
      {
        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Ldfld, EventsAutoSubscription.Processor.PXGraph_Extensions);
        ilGenerator.Emit(OpCodes.Ldc_I4, this._extensionIndex);
        ilGenerator.Emit(OpCodes.Ldelem_Ref);
      }
      ilGenerator.Emit(OpCodes.Castclass, this._inspectedType);
    }

    private void EmitWrapWithGenericAdapter(System.Type eventType, System.Type argsType, bool isInterceptor)
    {
      System.Type type1 = (isInterceptor ? typeof (Events.Event<,>.EventInterceptorDelegate) : typeof (Events.Event<,>.EventDelegate)).MakeGenericType(argsType, eventType);
      this._il.Emit(OpCodes.Newobj, type1.GetConstructors()[0]);
      System.Type type2 = (isInterceptor ? typeof (Events.Event<,>.AdapterInterceptor) : typeof (Events.Event<,>.Adapter)).MakeGenericType(argsType, eventType);
      this._il.Emit(OpCodes.Newobj, type2.GetConstructors()[0]);
      MethodInfo method = type2.GetMethod("Execute");
      this._il.Emit(OpCodes.Ldftn, method);
    }

    private void EmitWrapWithEntityRemapper(System.Type handlerTable, System.Type argsType)
    {
      System.Type type1 = typeof (EventHandlerEntityRemapper<,>.ClassicDelegate).MakeGenericType(handlerTable, argsType);
      this._il.Emit(OpCodes.Newobj, type1.GetConstructors()[0]);
      System.Type type2 = typeof (EventHandlerEntityRemapper<,>).MakeGenericType(handlerTable, argsType);
      this._il.Emit(OpCodes.Newobj, type2.GetConstructors()[0]);
      MethodInfo method = type2.GetMethod("Execute", new System.Type[2]
      {
        typeof (PXCache),
        argsType
      });
      this._il.Emit(OpCodes.Ldftn, method);
    }

    /// <summary>
    /// Emits call to one of the following methods of the proper event container of a graph:
    /// <para><see cref="!:RowEventsBase&lt;TClassicEventArgs, TClassicDelegate, TDelayedCollection&gt;.AddHandler(Type, TClassicDelegate)" /></para>
    /// <para><see cref="!:RowEventsBase&lt;TClassicEventArgs, TClassicDelegate, TDelayedCollection&gt;.AddHandler(Type, EventsBase&lt;TClassicEventArgs, TClassicDelegate, List&lt;TClassicDelegate&gt;&gt;.Interceptor)" /></para>
    /// <para><see cref="!:FieldEventsBase&lt;TClassicEventArgs, TClassicDelegate, TDelayedCollection&gt;.AddHandler(Type, string, TClassicDelegate)" /></para>
    /// <para><see cref="!:FieldEventsBase&lt;TClassicEventArgs, TClassicDelegate, TDelayedCollection&gt;.AddHandler(Type, string, EventsBase&lt;TClassicEventArgs, TClassicDelegate, PXCache.EventDictionary&lt;TClassicDelegate&gt;&gt;.Interceptor)" /></para>
    /// </summary>
    private void EmitCallAddHandler(
      EventDescription.Description eventDescription,
      System.Type table,
      string? fieldName,
      MethodInfo handler,
      System.Action<EventDescription.Description> emitWrapDelegate)
    {
      this._il.Emit(OpCodes.Ldarg_0);
      this._il.Emit(OpCodes.Callvirt, EventsAutoSubscription.Processor.PXGraph_GetEventContainersOf[eventDescription.EventName]);
      this.EmitLoadTypeInstance(table);
      if (fieldName != null)
        this._il.Emit(OpCodes.Ldstr, fieldName);
      if (handler.IsStatic)
        this._il.Emit(OpCodes.Ldnull);
      else
        this.EmitLoadMixinThis();
      this._il.Emit(OpCodes.Ldftn, handler);
      emitWrapDelegate(eventDescription);
      this._il.Emit(OpCodes.Newobj, eventDescription.DelegateType.GetConstructors()[0]);
      (string, System.Type[]) valueTuple;
      ref (string, System.Type[]) local = ref valueTuple;
      System.Type[] typeArray;
      if (fieldName != null)
        typeArray = new System.Type[3]
        {
          typeof (System.Type),
          typeof (string),
          eventDescription.DelegateType
        };
      else
        typeArray = new System.Type[2]
        {
          typeof (System.Type),
          eventDescription.DelegateType
        };
      local = ("AddHandler", typeArray);
      MethodInfo method = eventDescription.EventCollectionType.GetMethod(valueTuple.Item1, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, valueTuple.Item2, (ParameterModifier[]) null);
      this._il.Emit(OpCodes.Callvirt, method);
    }

    private PXGraph.AlteredState? GetAlteredAttributes(
      System.Type table,
      string fieldName,
      MethodInfo cacheAttachedMethod)
    {
      Tuple<string, MethodInfo> tuple = Tuple.Create<string, MethodInfo>(fieldName, cacheAttachedMethod);
      if (!this._alteredState.ContainsKey(table) || !this._alteredState[table].Fields.ContainsKey(fieldName))
      {
        List<MemberInfo> memberList = new List<MemberInfo>();
        PropertyInfo property1 = table.GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (property1 != (PropertyInfo) null)
          memberList.Add((MemberInfo) property1);
        foreach (System.Type extension in PXCache._GetExtensions(table, true))
        {
          PropertyInfo property2 = extension.GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
          if (property2 != (PropertyInfo) null)
            memberList.Add((MemberInfo) property2);
        }
        memberList.Add((MemberInfo) cacheAttachedMethod);
        PXEventSubscriberAttribute[] source = PXExtensionManager.MergeExtensionAttributes((IEnumerable<MemberInfo>) memberList);
        if (source.Length != 0)
        {
          if (!this._alteredState.ContainsKey(table))
            this._alteredState[table] = new PXGraph.AlteredState();
          this._alteredState[table].Fields.Add(fieldName, ((IEnumerable<PXEventSubscriberAttribute>) source).ToList<PXEventSubscriberAttribute>());
          this._alteredState[table].ProcessedMethods.Add(tuple);
        }
      }
      else if (!this._alteredState[table].ProcessedMethods.Contains(tuple))
      {
        List<PXEventSubscriberAttribute> field = this._alteredState[table].Fields[fieldName];
        this._alteredState[table].Fields.Remove(fieldName);
        PXEventSubscriberAttribute[] source = PXExtensionManager.MergeExtensionAttributes((IEnumerable<MemberInfo>) new \u003C\u003Ez__ReadOnlyArray<MemberInfo>(new MemberInfo[1]
        {
          (MemberInfo) cacheAttachedMethod
        }), field);
        if (source.Length != 0)
        {
          this._alteredState[table].Fields.Add(fieldName, ((IEnumerable<PXEventSubscriberAttribute>) source).ToList<PXEventSubscriberAttribute>());
          this._alteredState[table].ProcessedMethods.Add(tuple);
        }
      }
      PXGraph.AlteredState alteredState;
      return !this._alteredState.TryGetValue(table, out alteredState) ? (PXGraph.AlteredState) null : alteredState;
    }
  }

  public class Msg
  {
    public const string InterceptorHandlerUnavailable = "Interceptor delegate is not supported in generic extensions - {0}::{1}";
    public const string FailedSubscribeGraphEvent = "Failed to subscribe the graph event {2}::{0}. The method signature looks like an event handler, but {1} is not a valid event name.";
    public const string FailedSubscribeEvent = "Failed to subscribe the event {3}::{1} in the graph {0}. The method signature looks like an event handler, but the cache {2} has not been found in the list of auto-initialized caches. Remove unused event handlers from the code.";
    public const string MethodLooksLikePrivateEventHandler = "The method looks like a private event handler. Change the access modifier to virtual protected or rename the method {0}::{1}.";
  }
}
