// Decompiled with JetBrains decompiler
// Type: PX.Data.UnitySerializationHolderProxy
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Reflection;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

[Serializable]
internal class UnitySerializationHolderProxy : ISerializable, IObjectReference
{
  private static readonly ConstructorInfo constructor;
  private readonly ISerializable _base;
  private static readonly System.Reflection.FieldInfo dataField;
  private static readonly System.Reflection.FieldInfo unityTypeField;
  private static readonly System.Reflection.FieldInfo assemblyNameField;
  internal const int EmptyUnity = 1;
  internal const int NullUnity = 2;
  internal const int MissingUnity = 3;
  internal const int RuntimeTypeUnity = 4;
  internal const int ModuleUnity = 5;
  internal const int AssemblyUnity = 6;
  internal const int GenericParameterTypeUnity = 7;
  internal const int PartialInstantiationTypeUnity = 8;
  private static readonly PXAppCodeTypeBinder Binider = new PXAppCodeTypeBinder();

  private string m_data
  {
    get => (string) UnitySerializationHolderProxy.dataField.GetValue((object) this._base);
    set => UnitySerializationHolderProxy.dataField.SetValue((object) this._base, (object) value);
  }

  private string m_assemblyName
  {
    get => (string) UnitySerializationHolderProxy.assemblyNameField.GetValue((object) this._base);
    set
    {
      UnitySerializationHolderProxy.assemblyNameField.SetValue((object) this._base, (object) value);
    }
  }

  private int m_unityType
  {
    get => (int) UnitySerializationHolderProxy.unityTypeField.GetValue((object) this._base);
    set
    {
      UnitySerializationHolderProxy.unityTypeField.SetValue((object) this._base, (object) value);
    }
  }

  static UnitySerializationHolderProxy()
  {
    System.Type type = typeof (ValueType).Assembly.GetType("System.UnitySerializationHolder", true);
    UnitySerializationHolderProxy.constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, new System.Type[2]
    {
      typeof (SerializationInfo),
      typeof (StreamingContext)
    }, (ParameterModifier[]) null);
    UnitySerializationHolderProxy.dataField = type.GetField(nameof (m_data), BindingFlags.Instance | BindingFlags.NonPublic);
    UnitySerializationHolderProxy.unityTypeField = type.GetField(nameof (m_unityType), BindingFlags.Instance | BindingFlags.NonPublic);
    UnitySerializationHolderProxy.assemblyNameField = type.GetField(nameof (m_assemblyName), BindingFlags.Instance | BindingFlags.NonPublic);
  }

  internal UnitySerializationHolderProxy(SerializationInfo info, StreamingContext context)
  {
    this._base = (ISerializable) UnitySerializationHolderProxy.constructor.Invoke(new object[2]
    {
      (object) info,
      (object) context
    });
  }

  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    this._base.GetObjectData(info, context);
  }

  public object GetRealObject(StreamingContext context)
  {
    IObjectReference objectReference = (IObjectReference) this._base;
    try
    {
      return objectReference.GetRealObject(context);
    }
    catch
    {
      if (this.m_unityType == 4)
      {
        System.Type type = UnitySerializationHolderProxy.Binider.BindToType(this.m_assemblyName, this.m_data);
        if (type != (System.Type) null)
          return (object) type;
      }
      throw;
    }
  }
}
