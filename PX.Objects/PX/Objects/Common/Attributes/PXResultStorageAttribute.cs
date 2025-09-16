// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.PXResultStorageAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;

#nullable disable
namespace PX.Objects.Common.Attributes;

public class PXResultStorageAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldUpdatingSubscriber
{
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.ReturnValue != null)
    {
      using (GZipStream gzipStream = new GZipStream((Stream) new SparseMemoryStream((byte[][]) e.ReturnValue, 2048 /*0x0800*/), CompressionMode.Decompress))
      {
        try
        {
          e.ReturnValue = this.Deserialize((Stream) gzipStream, sender.Graph);
        }
        catch
        {
        }
      }
    }
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (byte[][]), new bool?(false), new bool?(true), new int?(), new int?(), new int?(-1), (object) null, this._FieldName, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
  }

  public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || e.NewValue is byte[][])
      return;
    SparseMemoryStream sparseMemoryStream = new SparseMemoryStream(2048 /*0x0800*/);
    GZipStream gzipStream = new GZipStream((Stream) sparseMemoryStream, CompressionMode.Compress);
    this.Serialize((Stream) gzipStream, sender.Graph, e.NewValue);
    gzipStream.Close();
    ((Stream) sparseMemoryStream).Close();
    e.NewValue = (object) sparseMemoryStream.GetBuf();
  }

  private void Serialize(Stream stream, PXGraph graph, object obj)
  {
    if (!(obj is IEnumerable enumerable))
      return;
    using (XmlWriter xmlWriter = XmlWriter.Create(stream))
    {
      xmlWriter.WriteStartElement("results");
      bool flag = true;
      foreach (object obj1 in enumerable)
      {
        if (obj1 is PXResult pxResult)
        {
          if (flag)
          {
            string str = string.Join(",", ((IEnumerable<Type>) pxResult.Tables).Select<Type, string>((Func<Type, string>) (t => t.FullName)));
            xmlWriter.WriteAttributeString("types", str);
            flag = false;
          }
          xmlWriter.WriteStartElement("result");
          foreach (Type table in pxResult.Tables)
          {
            string xml = graph.Caches[table].ToXml(pxResult[table]);
            xmlWriter.WriteRaw(xml);
          }
          xmlWriter.WriteEndElement();
        }
        else
        {
          Type type = obj1.GetType();
          if (flag)
          {
            xmlWriter.WriteAttributeString("types", type.FullName);
            flag = false;
          }
          string xml = graph.Caches[type].ToXml(obj1);
          xmlWriter.WriteRaw(xml);
        }
      }
      xmlWriter.WriteEndElement();
    }
  }

  private object Deserialize(Stream stream, PXGraph graph)
  {
    List<object> objectList = new List<object>();
    using (XmlReader xmlReader = XmlReader.Create(stream))
    {
      if (xmlReader.ReadToDescendant("results"))
      {
        string attribute = xmlReader.GetAttribute("types");
        if (attribute == null)
          return (object) objectList;
        Type[] array = ((IEnumerable<string>) attribute.Split(',')).Select<string, Type>((Func<string, Type>) (type => Type.GetType(type))).ToArray<Type>();
        xmlReader.Read();
        while (!xmlReader.EOF)
        {
          object obj = (object) null;
          if (array.Length > 1)
          {
            if (xmlReader.Name == "result")
            {
              Type resultType = this.GetResultType(array.Length);
              object[] objArray = new object[array.Length];
              xmlReader.Read();
              for (int index = 0; index < array.Length; ++index)
              {
                string str = xmlReader.ReadOuterXml();
                objArray[index] = graph.Caches[array[index]].FromXml(str);
              }
              obj = Activator.CreateInstance(resultType.MakeGenericType(array), objArray);
              xmlReader.Read();
            }
          }
          else if (xmlReader.Name == "Row")
          {
            string str = xmlReader.ReadOuterXml();
            obj = graph.Caches[array[0]].FromXml(str);
          }
          else
            break;
          objectList.Add(obj);
          if (xmlReader.Name == "results")
            break;
        }
      }
    }
    return (object) objectList;
  }

  private Type GetResultType(int lenght)
  {
    switch (lenght)
    {
      case 1:
        return typeof (PXResult<>);
      case 2:
        return typeof (PXResult<,>);
      case 3:
        return typeof (PXResult<,,>);
      case 4:
        return typeof (PXResult<,,,>);
      case 5:
        return typeof (PXResult<,,,,>);
      case 6:
        return typeof (PXResult<,,,,,>);
      case 7:
        return typeof (PXResult<,,,,,,>);
      case 8:
        return typeof (PXResult<,,,,,,,>);
      case 9:
        return typeof (PXResult<,,,,,,,,>);
      case 10:
        return typeof (PXResult<,,,,,,,,,>);
      case 11:
        return typeof (PXResult<,,,,,,,,,,>);
      case 12:
        return typeof (PXResult<,,,,,,,,,,,>);
      case 13:
        return typeof (PXResult<,,,,,,,,,,,,>);
      case 14:
        return typeof (PXResult<,,,,,,,,,,,,,>);
      case 15:
        return typeof (PXResult<,,,,,,,,,,,,,,>);
      case 16 /*0x10*/:
        return typeof (PXResult<,,,,,,,,,,,,,,,>);
      case 17:
        return typeof (PXResult<,,,,,,,,,,,,,,,,>);
      case 18:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,>);
      case 19:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,>);
      case 20:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,>);
      case 21:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,>);
      case 22:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,>);
      case 23:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,>);
      case 24:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,,>);
      case 25:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,,,>);
      default:
        return (Type) null;
    }
  }
}
