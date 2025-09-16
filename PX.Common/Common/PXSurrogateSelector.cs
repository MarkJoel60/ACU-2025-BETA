// Decompiled with JetBrains decompiler
// Type: PX.Common.PXSurrogateSelector
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

#nullable disable
namespace PX.Common;

public class PXSurrogateSelector : ISurrogateSelector
{
  private static HashSet<Type> \u0002 = new HashSet<Type>();
  private static readonly Dictionary<Type, ISerializationSurrogate> \u000E = new Dictionary<Type, ISerializationSurrogate>();
  public static bool IsCheckEnabled = false;

  public void ChainSelector(ISurrogateSelector selector) => throw new NotImplementedException();

  public ISurrogateSelector GetNextSelector() => throw new NotImplementedException();

  private static bool \u0002(Type _param0)
  {
    return _param0.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new Type[2]
    {
      typeof (SerializationInfo),
      typeof (StreamingContext)
    }, (ParameterModifier[]) null) != (ConstructorInfo) null;
  }

  private static bool \u000E(Type _param0)
  {
    for (Type element = _param0; element != typeof (object) && !(element == (Type) null); element = element.BaseType)
    {
      if (!Attribute.IsDefined((MemberInfo) element, typeof (SerializableAttribute), false))
        return false;
    }
    return true;
  }

  public static bool BaseSerializable(Type type)
  {
    if (((IEnumerable<Type>) type.GetInterfaces()).Any<Type>(PXSurrogateSelector.\u0002.\u000E ?? (PXSurrogateSelector.\u0002.\u000E = new Func<Type, bool>(PXSurrogateSelector.\u0002.\u0002.\u0002))))
      return false;
    for (Type element = type; element != typeof (object) && !(element == (Type) null) && !(element == typeof (MarshalByRefObject)); element = element.BaseType)
    {
      if (Attribute.IsDefined((MemberInfo) element, typeof (SerializableAttribute), false))
        return true;
    }
    return false;
  }

  public static bool BaseIgnoreSerializable(Type type)
  {
    for (Type element = type; element != typeof (object) && !(element == (Type) null) && !(element == typeof (MarshalByRefObject)); element = element.BaseType)
    {
      if (Attribute.IsDefined((MemberInfo) element, typeof (IgnoreXmlSurrogateAttribute), false))
        return true;
    }
    return false;
  }

  public ISerializationSurrogate GetSurrogate(
    Type type,
    StreamingContext context,
    out ISurrogateSelector selector)
  {
    selector = (ISurrogateSelector) this;
    PXSurrogateSelector.\u0002.Add(type);
    return PXSurrogateSelector.InternalGetSurrogate(type);
  }

  public static ISerializationSurrogate InternalGetSurrogate(Type type)
  {
    lock (PXSurrogateSelector.\u000E)
    {
      ISerializationSurrogate surrogate;
      if (!PXSurrogateSelector.\u000E.TryGetValue(type, out surrogate))
      {
        surrogate = PXSurrogateSelector.\u0002(type);
        PXSurrogateSelector.\u000E.Add(type, surrogate);
      }
      return surrogate;
    }
  }

  private static ISerializationSurrogate \u0002(Type _param0)
  {
    if (_param0.IsValueType && _param0 != typeof (BitVector32) && _param0.FullName != "System.ComponentModel.AttributeCollection+AttributeEntry")
      return (ISerializationSurrogate) null;
    if (_param0.IsArray)
      return (ISerializationSurrogate) null;
    if (_param0.IsInterface)
      return (ISerializationSurrogate) null;
    if (typeof (ISerializable).IsAssignableFrom(_param0))
      return (ISerializationSurrogate) null;
    if (PXSurrogateSelector.\u000E(_param0))
      return (ISerializationSurrogate) null;
    if (typeof (IXmlSerializable).IsAssignableFrom(_param0) && !Attribute.IsDefined((MemberInfo) _param0, typeof (IgnoreXmlSurrogateAttribute), true))
      return (ISerializationSurrogate) new XmlSurrogate();
    int num = _param0 == typeof (PropertyDescriptorCollection) || _param0 == typeof (AttributeCollection) || _param0 == typeof (EnumConverter) || _param0 == typeof (ColorConverter) || _param0 == typeof (FontConverter.FontNameConverter) || _param0 == typeof (TypeConverter.StandardValuesCollection) || _param0.FullName == "System.ComponentModel.AttributeCollection+AttributeEntry" || typeof (PropertyDescriptor).IsAssignableFrom(_param0) ? 1 : (_param0.FullName.StartsWith("PX.") ? 1 : 0);
    bool flag = PXSurrogateSelector.BaseSerializable(_param0);
    if (num == 0 && !flag)
      throw new Exception("The type is not marked as serializable: " + _param0.FullName);
    return FormatterServices.GetSurrogateForCyclicalReference((ISerializationSurrogate) new CommonSurrogate());
  }

  public static BinaryFormatter CreateFormatter()
  {
    return new BinaryFormatter()
    {
      SurrogateSelector = (ISurrogateSelector) new PXSurrogateSelector()
    };
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly PXSurrogateSelector.\u0002 \u0002 = new PXSurrogateSelector.\u0002();
    public static Func<Type, bool> \u000E;

    internal bool \u0002(Type _param1)
    {
      return _param1.FullName != null && _param1.FullName.Equals("PX.Data.IBqlTable", StringComparison.OrdinalIgnoreCase);
    }
  }
}
