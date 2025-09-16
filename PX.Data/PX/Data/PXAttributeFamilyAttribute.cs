// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAttributeFamilyAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.Data;

/// <summary>Allows to specify rules, which attributes can not be combined
/// together.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class PXAttributeFamilyAttribute : Attribute
{
  public readonly 
  #nullable disable
  System.Type RootType;
  private static readonly System.Type[] Empty = new System.Type[0];

  public PXAttributeFamilyAttribute(System.Type rootType) => this.RootType = rootType;

  public static System.Type[] GetRoots(System.Type t)
  {
    foreach (MemberInfo element in t.CreateList<System.Type>((Func<System.Type, System.Type>) (_ => _.BaseType)))
    {
      PXAttributeFamilyAttribute[] customAttributes = (PXAttributeFamilyAttribute[]) Attribute.GetCustomAttributes(element, typeof (PXAttributeFamilyAttribute));
      if (((IEnumerable<PXAttributeFamilyAttribute>) customAttributes).Any<PXAttributeFamilyAttribute>())
        return ((IEnumerable<PXAttributeFamilyAttribute>) customAttributes).Select<PXAttributeFamilyAttribute, System.Type>((Func<PXAttributeFamilyAttribute, System.Type>) (_ => _.RootType)).ToArray<System.Type>();
    }
    return PXAttributeFamilyAttribute.Empty;
  }

  public static bool IsSameFamily(System.Type a, System.Type b)
  {
    System.Type[] roots1 = PXAttributeFamilyAttribute.GetRoots(a);
    if (EnumerableExtensions.IsNullOrEmpty<System.Type>((IEnumerable<System.Type>) roots1))
      return false;
    System.Type[] roots2 = PXAttributeFamilyAttribute.GetRoots(b);
    return !EnumerableExtensions.IsNullOrEmpty<System.Type>((IEnumerable<System.Type>) roots2) && ((IEnumerable<System.Type>) roots1).Any<System.Type>(new Func<System.Type, bool>(((Enumerable) roots2).Contains<System.Type>));
  }

  public static PXAttributeFamilyAttribute FromType(System.Type t)
  {
    foreach (MemberInfo element in t.CreateList<System.Type>((Func<System.Type, System.Type>) (_ => _.BaseType)))
    {
      PXAttributeFamilyAttribute customAttribute = (PXAttributeFamilyAttribute) Attribute.GetCustomAttribute(element, typeof (PXAttributeFamilyAttribute));
      if (customAttribute != null)
        return customAttribute;
    }
    return (PXAttributeFamilyAttribute) null;
  }

  internal static void CheckAttributes(
    PXCache.DACFieldDescriptor prop,
    PXEventSubscriberAttribute[] attributes)
  {
    foreach (IGrouping<System.Type, PXEventSubscriberAttribute> source in (IEnumerable<IGrouping<System.Type, PXEventSubscriberAttribute>>) ((IEnumerable<PXEventSubscriberAttribute>) attributes).SelectMany((Func<PXEventSubscriberAttribute, IEnumerable<System.Type>>) (a => (IEnumerable<System.Type>) PXAttributeFamilyAttribute.GetRoots(a.GetType())), (a, t) => new
    {
      t = t,
      a = a
    }).ToLookup(_ => _.t, _ => _.a))
    {
      if (!(source.Key == (System.Type) null) && source.Count<PXEventSubscriberAttribute>() > 1)
        PXValidationWriter.AddTypeError(prop.DeclaringType, "Incompatible attributes have been detected. Family: {0}, Property {1}::{2}, Attributes: {3}", (object) source.Key.Name, (object) prop.DeclaringType, (object) prop.Name, (object) source.Select<PXEventSubscriberAttribute, string>((Func<PXEventSubscriberAttribute, string>) (_ => _.GetType().Name)).JoinToString<string>(","));
    }
  }
}
