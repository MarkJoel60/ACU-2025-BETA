// Decompiled with JetBrains decompiler
// Type: PX.Api.CustomizedTypeManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Extensions;
using PX.Data;
using System;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.Api;

public static class CustomizedTypeManager
{
  private const 
  #nullable disable
  string CST = "Cst_";
  private const string WRAPPER = "Wrapper.";
  private const string NESTED_SEPARATOR = "+";

  public static bool IsCustomizedType(System.Type t) => t.Name.StartsWith("Cst_");

  public static bool IsCustomizedType(string typeFullName)
  {
    return StringExtensions.LastSegment(typeFullName, '.').StartsWith("Cst_");
  }

  public static System.Type GetTypeNotCustomized(System.Type t)
  {
    if (!CustomizedTypeManager.IsCustomizedType(t))
      return t;
    System.Type t1;
    if (t.IsSubclassOf(typeof (PXGraph)) || t.IsSubclassOf(typeof (PXGraphExtension)))
    {
      t1 = t;
      do
      {
        t1 = t1.BaseType;
      }
      while (CustomizedTypeManager.IsCustomizedType(t1));
    }
    else
      t1 = PXBuildManager.GetType(CustomizedTypeManager.GetTypeNotCustomized(t.FullName), true);
    return t1;
  }

  public static string GetTypeNotCustomized(string fullName)
  {
    if (fullName.StartsWith("Wrapper."))
      fullName = fullName.Substring("Wrapper.".Length);
    return fullName.Replace("Cst_", "").Replace("\\+", "+");
  }

  public static System.Type GetTypeNotCustomized(PXGraph g)
  {
    return CustomizedTypeManager.GetTypeNotCustomized(g.GetType());
  }

  public static string GetCustomizedTypeClassName(System.Type t)
  {
    return "Cst_" + CustomizedTypeManager.GetTypeNotCustomized(t).CreateList<System.Type>((Func<System.Type, System.Type>) (_ => _.DeclaringType)).Select<System.Type, string>((Func<System.Type, string>) (_ => _.Name)).Reverse<string>().JoinToString<string>("+");
  }

  public static string GetWrapperTypeName(System.Type t)
  {
    if (!typeof (PXGraph).IsAssignableFrom(t) && !typeof (PXGraphExtension).IsAssignableFrom(t))
      throw new PXException("Invalid argument in GetWrapperTypeName");
    return "Wrapper." + CustomizedTypeManager.GetCustomizedTypeFullName(t);
  }

  public static string GetCustomizedTypeFullName(System.Type t)
  {
    return string.IsNullOrEmpty(t.Namespace) ? CustomizedTypeManager.GetCustomizedTypeClassName(t) : $"{t.Namespace}.{CustomizedTypeManager.GetCustomizedTypeClassName(t)}";
  }

  public static System.Type GetExtendedGraphType<TExtension>(out bool secondLevel) where TExtension : PXGraphExtension
  {
    return CustomizedTypeManager.GetExtendedGraphType(typeof (TExtension), out secondLevel);
  }

  public static System.Type GetExtendedGraphType(System.Type extension, out bool secondLevel)
  {
    return PXExtensionManager.GetExtendedGraphType(extension, out secondLevel);
  }
}
