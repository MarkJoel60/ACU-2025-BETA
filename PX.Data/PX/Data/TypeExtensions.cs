// Decompiled with JetBrains decompiler
// Type: PX.Data.TypeExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Reflection;

#nullable disable
namespace PX.Data;

internal static class TypeExtensions
{
  public static int GetPersistOrder(this System.Type type)
  {
    PXDBInterceptorAttribute customAttribute = type.GetCustomAttribute<PXDBInterceptorAttribute>(false);
    PersistOrder persistOrder = customAttribute != null ? customAttribute.PersistOrder : PersistOrder.NotSpecified;
    if (persistOrder == PersistOrder.NotSpecified)
    {
      System.Type baseType = type.BaseType;
      if (baseType != typeof (object) && typeof (IBqlTable).IsAssignableFrom(baseType))
        return baseType.GetPersistOrder();
    }
    return persistOrder != PersistOrder.NotSpecified ? (int) persistOrder : 10;
  }

  internal static bool IsInstanceOfGenericType(this System.Type type, System.Type genericType)
  {
    for (; type != (System.Type) null; type = type.BaseType)
    {
      if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
        return true;
    }
    return false;
  }
}
