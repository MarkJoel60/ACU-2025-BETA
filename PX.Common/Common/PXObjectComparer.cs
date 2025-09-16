// Decompiled with JetBrains decompiler
// Type: PX.Common.PXObjectComparer
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

#nullable disable
namespace PX.Common;

public class PXObjectComparer
{
  private readonly Hashtable \u0002 = new Hashtable();
  public bool KeepRef;
  public static Dictionary<object, object> Refs = new Dictionary<object, object>((IEqualityComparer<object>) new ReflectionSerializer.ObjectComparer<object>());

  public static object GetRef(object x) => PXObjectComparer.Refs[x];

  private void \u0002(string _param1)
  {
  }

  public void Compare(object a, object b, PXObjectComparer.StackFrame stack)
  {
    if (this.KeepRef && stack == null)
      PXObjectComparer.Refs.Clear();
    if (a == null && b == null)
      return;
    if (a == null ^ b == null)
      throw new Exception("Object is null");
    if (a.GetType() != b.GetType())
      throw new Exception("Distinct types");
    if (a.Equals(b))
      return;
    Type type = a.GetType();
    if ((type.IsPrimitive ? 1 : (a is string ? 1 : 0)) != 0)
      throw new Exception("Distinct value");
    if (this.\u0002.ContainsKey(a))
      return;
    if (this.KeepRef)
      PXObjectComparer.Refs[b] = a;
    this.\u0002[a] = b;
    if (a is IXmlSerializable && stack != null)
      return;
    if (type != typeof (PropertyDescriptorCollection))
    {
      for (Type c = type; c != typeof (object) && !type.IsArray && !typeof (ISerializable).IsAssignableFrom(c) && !typeof (Component).IsAssignableFrom(c); c = c.BaseType)
      {
        foreach (FieldInfo field in c.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
          object a1 = field.GetValue(a);
          object b1 = field.GetValue(b);
          if (!field.IsNotSerialized && !Attribute.IsDefined((MemberInfo) field, typeof (NonComparedAttribute)))
            this.Compare(a1, b1, new PXObjectComparer.StackFrame()
            {
              Prev = stack,
              Prop = field.Name,
              A = a,
              B = b
            });
        }
      }
    }
    switch (a)
    {
      case IDictionary _:
        IDictionary dictionary1 = (IDictionary) a;
        IDictionary dictionary2 = (IDictionary) b;
        if (dictionary1.Count != dictionary2.Count)
          throw new Exception("Distinct count");
        object[] array1 = dictionary1.Keys.ToArray<object>();
        object[] array2 = dictionary2.Keys.ToArray<object>();
        for (int index = 0; index < array1.Length; ++index)
        {
          object obj1 = array1[index];
          object obj2 = array2[index];
          try
          {
            this.Compare(obj1, obj2, new PXObjectComparer.StackFrame()
            {
              Prev = stack,
              Prop = "key:" + index.ToString(),
              A = a,
              B = b
            });
          }
          catch
          {
            break;
          }
          this.Compare(dictionary1[obj1], dictionary2[obj2], new PXObjectComparer.StackFrame()
          {
            Prev = stack,
            Prop = "value:" + index.ToString(),
            A = a,
            B = b
          });
        }
        break;
      case IEnumerable _:
        object[] array3 = ((IEnumerable) a).ToArray<object>();
        object[] array4 = ((IEnumerable) b).ToArray<object>();
        if (array3.Length != array4.Length)
          throw new Exception("Distinct length");
        for (int index = 0; index < array3.Length; ++index)
          this.Compare(array3[index], array4[index], new PXObjectComparer.StackFrame()
          {
            Prev = stack,
            Prop = "array:" + index.ToString(),
            A = a,
            B = b
          });
        break;
    }
  }

  public class StackFrame
  {
    public PXObjectComparer.StackFrame Prev;
    public object A;
    public object B;
    public string Prop;

    public string Line => $"{this.A.GetType().Name}->{this.Prop}";

    public string Stack
    {
      get
      {
        string str = "";
        if (this.Prev != null)
          str = this.Prev.Stack + "\n";
        return str + this.Line;
      }
    }
  }
}
