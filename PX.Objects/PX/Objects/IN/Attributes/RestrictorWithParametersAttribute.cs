// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.RestrictorWithParametersAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Attributes;

public class RestrictorWithParametersAttribute : PXRestrictorAttribute
{
  protected Type[] _messageParameters;

  public RestrictorWithParametersAttribute(
    Type where,
    string message,
    params Type[] messageParameters)
    : base(where, message, Array.Empty<Type>())
  {
    this._messageParameters = messageParameters;
  }

  public virtual object[] GetMessageParameters(PXCache sender, object itemres, object row)
  {
    return ((IEnumerable<Type>) this._messageParameters).Select<Type, object>((Func<Type, object>) (parameter => this.GetMessageParameter(sender, itemres, parameter))).ToArray<object>();
  }

  protected virtual object GetMessageParameter(PXCache sender, object itemres, Type parameter)
  {
    if (parameter.IsGenericType)
    {
      if (EnumerableExtensions.IsIn<Type>(parameter.GetGenericTypeDefinition(), typeof (Current<>), typeof (Current2<>)))
        return this.GetCurrentValue(sender.Graph, parameter);
      if (parameter.GetGenericTypeDefinition() == typeof (Selector<,>))
        return this.GetSelectorValue(sender.Graph, parameter);
    }
    return this.GetSelectedRowValue(sender, itemres, parameter);
  }

  protected virtual object GetCurrentValue(PXGraph graph, Type current)
  {
    Type[] genericArguments = current.GetGenericArguments();
    Type type = (genericArguments != null ? (genericArguments.Length != 1 ? 1 : 0) : 1) == 0 ? genericArguments[0] : throw new ArgumentException();
    Type itemType = BqlCommand.GetItemType(type);
    PXCache cach = graph.Caches[itemType];
    if (cach.Current == null)
      return (object) null;
    object valueExt = cach.GetValueExt(cach.Current, type.Name);
    return valueExt is PXFieldState pxFieldState ? pxFieldState.Value : valueExt;
  }

  protected virtual object GetSelectorValue(PXGraph graph, Type selector)
  {
    Type[] genericArguments = selector.GetGenericArguments();
    Type type1 = (genericArguments != null ? (genericArguments.Length != 2 ? 1 : 0) : 1) == 0 ? genericArguments[0] : throw new ArgumentException();
    Type type2 = genericArguments[1];
    Type itemType1 = BqlCommand.GetItemType(type1);
    PXCache cach = graph.Caches[itemType1];
    if (cach.Current == null)
      return (object) null;
    object obj = PXSelectorAttribute.Select(cach, cach.Current, type1.Name);
    Type itemType2 = BqlCommand.GetItemType(type2);
    object valueExt = graph.Caches[itemType2].GetValueExt(obj, type2.Name);
    return valueExt is PXFieldState pxFieldState ? pxFieldState.Value : valueExt;
  }

  protected virtual object GetSelectedRowValue(PXCache sender, object itemres, Type parameter)
  {
    Type itemType = BqlCommand.GetItemType(parameter);
    IBqlTable ibqlTable = PXResult.Unwrap(itemres, itemType);
    return sender.Graph.Caches[itemType].GetStateExt((object) ibqlTable, parameter.Name);
  }
}
