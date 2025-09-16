// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Tools
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.Update;

public static class Tools
{
  public static bool MoveFolder(string src, string dst)
  {
    if ((int) dst[dst.Length - 1] != (int) Path.DirectorySeparatorChar)
      dst += Path.DirectorySeparatorChar.ToString();
    if ((int) src[src.Length - 1] != (int) Path.DirectorySeparatorChar)
      src += Path.DirectorySeparatorChar.ToString();
    if (!Directory.Exists(src))
      return false;
    if (!Directory.Exists(dst))
      Directory.CreateDirectory(dst);
    bool flag = false;
    foreach (string file in Directory.GetFiles(src, "*", SearchOption.AllDirectories))
    {
      string dst1 = file.Replace(src, dst);
      if (dst1.EndsWith(".deploy"))
        dst1 = dst1.Replace(".deploy", "");
      if (Tools.MoveFile(file, dst1))
        flag = true;
    }
    return flag;
  }

  public static bool MoveFile(string src, string dst)
  {
    string directoryName = Path.GetDirectoryName(dst);
    if (!Directory.Exists(directoryName))
      Directory.CreateDirectory(directoryName);
    if (File.Exists(dst))
      return false;
    File.Copy(src, dst);
    return true;
  }

  public static void RedeployFile(string src, string dst)
  {
    if (!File.Exists(src))
    {
      src += ".deploy";
      if (!File.Exists(src))
        return;
    }
    string directoryName = Path.GetDirectoryName(dst);
    if (!Directory.Exists(directoryName))
      Directory.CreateDirectory(directoryName);
    if (dst.EndsWith(".deploy"))
      dst = dst.Replace(".deploy", "");
    if (File.Exists(dst))
      File.Delete(dst);
    File.Copy(src, dst);
  }

  public static void SetReadOnly(FileInfo info, bool ReadOnly)
  {
    if (!info.Exists)
      return;
    FileAttributes attributes = info.Attributes;
    if (!ReadOnly && (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
      attributes ^= FileAttributes.ReadOnly;
    if (ReadOnly && (attributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
      attributes |= FileAttributes.ReadOnly;
    info.Attributes = attributes;
  }

  public static void SetReadOnly(string file, bool ReadOnly)
  {
    if (!File.Exists(file))
      return;
    FileAttributes attributes = File.GetAttributes(file);
    if (!ReadOnly && (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
      attributes ^= FileAttributes.ReadOnly;
    if (ReadOnly && (attributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
      attributes |= FileAttributes.ReadOnly;
    File.SetAttributes(file, attributes);
  }

  public static object ConcatProperties(object from, object to)
  {
    if (from == null && to == null)
      return (object) null;
    if (from == null)
      return to;
    if (to == null)
      return from;
    foreach (PropertyInfo property1 in from.GetType().GetProperties())
    {
      object obj = property1.GetValue(from, (object[]) null);
      if (obj != null)
      {
        PropertyInfo property2 = to.GetType().GetProperty(property1.Name);
        if (!(property2 == (PropertyInfo) null))
        {
          try
          {
            property2.SetValue(to, obj, (object[]) null);
          }
          catch
          {
          }
        }
      }
    }
    return to;
  }

  public static object ConcatFields(object from, object to)
  {
    if (from == null && to == null)
      return (object) null;
    if (from == null)
      return to;
    if (to == null)
      return from;
    foreach (System.Reflection.FieldInfo field1 in from.GetType().GetFields())
    {
      object obj = field1.GetValue(from);
      if (obj != null)
      {
        System.Reflection.FieldInfo field2 = to.GetType().GetField(field1.Name);
        if (!(field2 == (System.Reflection.FieldInfo) null))
        {
          try
          {
            field2.SetValue(to, obj);
          }
          catch
          {
          }
        }
      }
    }
    return to;
  }

  public static void AddIfNotEmpty<T>(this List<T> list, IEnumerable<T> items)
  {
    if (items == null)
      return;
    list.AddIfNotEmpty<T>(items.ToArray<T>());
  }

  public static void AddIfNotEmpty<T>(this List<T> list, params T[] items)
  {
    if (items == null)
      return;
    foreach (T obj in items)
    {
      if ((object) obj != null)
        list.Add(obj);
    }
  }

  internal static List<T> AddRange<T>(this List<T> list, IEnumerable<T> items)
  {
    if (items != null && list != null)
      list.AddRange(items);
    return list;
  }

  internal static List<T> Add<T>(this List<T> list, T item)
  {
    if ((object) item != null && list != null)
    {
      // ISSUE: explicit non-virtual call
      __nonvirtual (list.Add(item));
    }
    return list;
  }

  public static TResult DoWith<TResult>(
    this TResult obj,
    System.Action<TResult> action,
    Func<TResult> initialisator = null)
    where TResult : class
  {
    if ((object) obj == null && initialisator != null)
      obj = initialisator();
    if ((object) obj != null && action != null)
      action(obj);
    return obj;
  }

  public static TResult DoWith<T, TResult>(
    this T obj,
    Func<T, TResult> action,
    Func<TResult> initialisator = null)
    where T : class
    where TResult : class
  {
    TResult result = default (TResult);
    if (action != null)
      result = action(obj);
    if ((object) result == null && initialisator != null)
      result = initialisator();
    return result;
  }

  public static void AddRange<T1, T2>(this Dictionary<T1, T2> dict, Dictionary<T1, T2> values)
  {
    if (values == null)
      return;
    foreach (T1 key in values.Keys)
      dict[key] = values[key];
  }
}
