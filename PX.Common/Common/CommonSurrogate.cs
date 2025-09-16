// Decompiled with JetBrains decompiler
// Type: PX.Common.CommonSurrogate
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Common;

internal class CommonSurrogate : ISerializationSurrogate
{
  private static List<FieldInfo> \u0002(Type _param0)
  {
    List<FieldInfo> fieldInfoList = new List<FieldInfo>();
    bool flag = PXSurrogateSelector.BaseSerializable(_param0);
    for (Type type = _param0; type != typeof (object) && (!flag || PXSurrogateSelector.BaseSerializable(type)); type = type.BaseType)
    {
      foreach (FieldInfo field in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
      {
        if (!(field.DeclaringType != type) && !field.IsNotSerialized)
          fieldInfoList.Add(field);
      }
    }
    return fieldInfoList;
  }

  public void GetObjectData(object _param1, SerializationInfo _param2, StreamingContext _param3)
  {
    List<FieldInfo> fieldInfoList = CommonSurrogate.\u0002(_param1.GetType());
    int num = 0;
    foreach (FieldInfo fieldInfo in fieldInfoList)
    {
      ++num;
      object obj1 = _param1;
      object obj2 = fieldInfo.GetValue(obj1);
      if (obj2 != null && PXSurrogateSelector.IsCheckEnabled)
      {
        PXSurrogateSelector.InternalGetSurrogate(obj2.GetType());
        string name = obj2.GetType().Name;
        if (name == "PXCacheCollection" || name == "PXGraph" || name == "EventHandlerList")
          throw new Exception("PXCacheCollection");
      }
      string name1 = new string(new char[1]{ (char) num });
      _param2.AddValue(name1, obj2);
    }
  }

  public object SetObjectData(
    object _param1,
    SerializationInfo _param2,
    StreamingContext _param3,
    ISurrogateSelector _param4)
  {
    List<FieldInfo> fieldInfoList = CommonSurrogate.\u0002(_param1.GetType());
    int num = 0;
    foreach (FieldInfo fieldInfo in fieldInfoList)
    {
      ++num;
      string name = new string(new char[1]{ (char) num });
      object obj = _param2.GetValue(name, typeof (object));
      fieldInfo.SetValue(_param1, obj);
    }
    return _param1;
  }
}
