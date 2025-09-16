// Decompiled with JetBrains decompiler
// Type: PX.Api.ApiFieldInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Soap.Screen;
using PX.Data;
using System;
using System.Reflection;

#nullable disable
namespace PX.Api;

public class ApiFieldInfo
{
  public System.Type DataType;
  public bool HasPropertyInDac = true;
  public bool EmitXsiTypeAttribute;
  public bool PrimaryKey;
  public PXUIVisibility Visibility;

  public bool IsNillable => this.DataType.IsValueType;

  public void CopyFrom(PXFieldState src)
  {
    this.PrimaryKey = src.PrimaryKey;
    this.DataType = src.DataType;
    this.Visibility = src.Visibility;
  }

  public static ApiFieldInfo Create(string tableName, string name)
  {
    return ApiFieldInfo.Create(new PXGraph().Caches[ServiceManager.GetTableType(tableName)], name);
  }

  public static ApiFieldInfo Create(PXCache cache, string field)
  {
    ApiFieldInfo apiFieldInfo = new ApiFieldInfo();
    PXFieldState stateExt;
    try
    {
      stateExt = (PXFieldState) cache.GetStateExt((object) null, field);
    }
    catch
    {
      return (ApiFieldInfo) null;
    }
    if (stateExt != null)
    {
      apiFieldInfo.CopyFrom(stateExt);
      apiFieldInfo.HasPropertyInDac = cache.GetFieldOrdinal(field) >= 0;
      apiFieldInfo.EmitXsiTypeAttribute = apiFieldInfo.DataType == typeof (object);
      if (apiFieldInfo.EmitXsiTypeAttribute && apiFieldInfo.HasPropertyInDac)
      {
        System.Type reflectionType = ApiFieldInfo.GetReflectionType(cache, field);
        if (reflectionType != (System.Type) null)
          apiFieldInfo.DataType = reflectionType;
      }
      return apiFieldInfo;
    }
    System.Type reflectionType1 = ApiFieldInfo.GetReflectionType(cache, field);
    if (reflectionType1 == (System.Type) null)
      return (ApiFieldInfo) null;
    apiFieldInfo.DataType = reflectionType1;
    return apiFieldInfo;
  }

  private static System.Type GetReflectionType(PXCache cache, string field)
  {
    PropertyInfo property = cache.GetItemType().GetProperty(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty);
    if (property == (PropertyInfo) null)
      return (System.Type) null;
    System.Type type = property.PropertyType;
    if (type.IsGenericType)
    {
      if (!(property.PropertyType.GetGenericTypeDefinition() == typeof (Nullable<>)))
        return (System.Type) null;
      type = Nullable.GetUnderlyingType(property.PropertyType);
    }
    return (type.IsValueType || type == typeof (byte[]) ? 1 : (type.IsAssignableFrom(typeof (string)) ? 1 : 0)) == 0 ? (System.Type) null : type;
  }
}
