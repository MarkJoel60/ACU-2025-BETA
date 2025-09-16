// Decompiled with JetBrains decompiler
// Type: System.Web.Compilation.PXBuildManager
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.DbServices.Utils;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace System.Web.Compilation;

public static class PXBuildManager
{
  private static readonly ConcurrentDictionary<string, Type> \u0006 = new ConcurrentDictionary<string, Type>((IEqualityComparer<string>) StringComparer.Ordinal);
  private static readonly ConcurrentDictionary<string, Type> \u0008 = new ConcurrentDictionary<string, Type>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  public static event Action<List<Type>> SortExtensions;

  internal static void SortExts(List<Type> _param0)
  {
    Action<List<Type>> action = PXBuildManager.\u0002;
    if (action == null)
      return;
    action(_param0);
  }

  public static void PartialSort(List<Type> list, Dictionary<Type, int> order)
  {
    PXBuildManager.\u000E obj = new PXBuildManager.\u000E();
    obj.\u0002 = order;
    // ISSUE: explicit non-virtual call
    if (list == null || __nonvirtual (list.Count) <= 1)
      return;
    Dictionary<Type, int> dictionary = obj.\u0002;
    // ISSUE: explicit non-virtual call
    if ((dictionary != null ? (__nonvirtual (dictionary.Count) > 1 ? 1 : 0) : 0) == 0)
      return;
    Type[] array = list.Where<Type>(new Func<Type, bool>(obj.\u0002)).OrderBy<Type, int>(new Func<Type, int>(obj.\u0002)).ToArray<Type>();
    if (array.Length == 0)
      return;
    int num = 0;
    for (int index = 0; index < list.Count; ++index)
    {
      if (obj.\u0002.ContainsKey(list[index]))
        list[index] = array[num++];
    }
  }

  public static event Func<string, Type> TypeGetter;

  public static Type GetType(string typeName, bool throwOnError)
  {
    return PXBuildManager.GetType(typeName, throwOnError, false);
  }

  public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
  {
    Type type = (ignoreCase ? PXBuildManager.\u0008 : PXBuildManager.\u0006).GetOrAdd<(string, bool)>(typeName, PXBuildManager.\u0002.\u000E ?? (PXBuildManager.\u0002.\u000E = new Func<string, (string, bool), Type>(PXBuildManager.\u0002.\u0002.\u0002)), (typeName, ignoreCase));
    if (type != (Type) null)
      return type;
    if (PXBuildManager.\u000E != null)
    {
      int length = typeName.IndexOf(',');
      string str = length < 0 ? typeName : typeName.Substring(0, length);
      type = PXBuildManager.\u000E(str);
      if (type == (Type) null)
      {
        if (typeName.Contains("["))
        {
          try
          {
            type = PXBuildManager.TypeParser.Parse(typeName).MakeType();
          }
          catch
          {
          }
        }
      }
    }
    return !throwOnError || !(type == (Type) null) ? type : throw new Exception("The type is not found: " + typeName);
  }

  internal static void ClearTypeCache()
  {
    PXBuildManager.\u0006.Clear();
    PXBuildManager.\u0008.Clear();
  }

  private static Type \u0002(string _param0, bool _param1)
  {
    try
    {
      return BuildManager.GetType(_param0, false, _param1);
    }
    catch
    {
      return (Type) null;
    }
  }

  public static bool IsRuntimAssembly(Assembly a) => ReflectionUtils.IsRuntimeAssembly(a);

  [Serializable]
  private sealed class \u0002
  {
    public static readonly PXBuildManager.\u0002 \u0002 = new PXBuildManager.\u0002();
    public static Func<string, (string, bool), Type> \u000E;

    internal Type \u0002(string _param1, (string, bool) _param2)
    {
      return PXBuildManager.\u0002(_param2.Item1, _param2.Item2);
    }
  }

  private sealed class \u000E
  {
    public Dictionary<Type, int> \u0002;

    internal bool \u0002(Type _param1) => this.\u0002.ContainsKey(_param1);

    internal int \u0002(Type _param1) => this.\u0002[_param1];
  }

  public class TypeParser
  {
    public string Name;
    public bool IsGeneric;
    public bool IsArray;
    public List<PXBuildManager.TypeParser.ArrayDimension> ArrayDimensions;
    public List<PXBuildManager.TypeParser> TypeArguments;

    public TypeParser()
    {
      this.Name = (string) null;
      this.IsGeneric = false;
      this.IsArray = false;
      this.ArrayDimensions = new List<PXBuildManager.TypeParser.ArrayDimension>();
      this.TypeArguments = new List<PXBuildManager.TypeParser>();
    }

    public override string ToString()
    {
      string str = this.Name;
      if (this.IsGeneric)
        str = $"{str}[{string.Join(",", this.TypeArguments.Select<PXBuildManager.TypeParser, string>(PXBuildManager.TypeParser.\u0002.\u000E ?? (PXBuildManager.TypeParser.\u0002.\u000E = new Func<PXBuildManager.TypeParser, string>(PXBuildManager.TypeParser.\u0002.\u0002.\u0002))))}]";
      foreach (PXBuildManager.TypeParser.ArrayDimension arrayDimension in this.ArrayDimensions)
        str += arrayDimension.ToString();
      return str;
    }

    public string FormatForDisplay(int indent = 0)
    {
      PXBuildManager.TypeParser.\u000E obj = new PXBuildManager.TypeParser.\u000E();
      obj.\u0002 = indent;
      string str1 = new string(' ', obj.\u0002);
      string str2 = $"{str1}Name: {this.Name}\r\n{str1}IsGeneric: {this.IsGeneric.ToString()}\r\n{str1}ArraySpec: {string.Join("", this.ArrayDimensions.Select<PXBuildManager.TypeParser.ArrayDimension, string>(PXBuildManager.TypeParser.\u0002.\u0006 ?? (PXBuildManager.TypeParser.\u0002.\u0006 = new Func<PXBuildManager.TypeParser.ArrayDimension, string>(PXBuildManager.TypeParser.\u0002.\u0002.\u0002))))}\r\n";
      if (this.IsGeneric)
        str2 = $"{str2}{str1}GenericParameters: {{\r\n{string.Join(str1 + "},{\r\n", this.TypeArguments.Select<PXBuildManager.TypeParser, string>(new Func<PXBuildManager.TypeParser, string>(obj.\u0002)))}{str1}}}\r\n";
      return str2;
    }

    public static PXBuildManager.TypeParser Parse(string name)
    {
      int num = 0;
      return PXBuildManager.TypeParser.\u0002(name, ref num, out bool _);
    }

    private static PXBuildManager.TypeParser \u0002(
      string _param0,
      ref int _param1,
      out bool _param2)
    {
      StringBuilder stringBuilder = new StringBuilder();
      PXBuildManager.TypeParser typeParser = new PXBuildManager.TypeParser();
      _param2 = true;
      bool flag1 = _param1 == 0;
      bool flag2 = true;
label_31:
      while (_param1 < _param0.Length)
      {
        char ch = _param0[_param1++];
        switch (ch)
        {
          case ',':
            if (flag1)
            {
              while (true)
              {
                if (_param1 < _param0.Length && _param0[_param1] != ']')
                  ++_param1;
                else
                  goto label_31;
              }
            }
            else
            {
              if (typeParser.Name == null)
                typeParser.Name = stringBuilder.ToString();
              _param2 = false;
              return typeParser;
            }
          case '[':
            if (flag2)
            {
              flag1 = true;
              continue;
            }
            typeParser.IsArray = true;
            PXBuildManager.TypeParser.ArrayDimension arrayDimension = new PXBuildManager.TypeParser.ArrayDimension();
            typeParser.ArrayDimensions.Add(arrayDimension);
            while (_param1 < _param0.Length)
            {
              switch (_param0[_param1++])
              {
                case ',':
                  ++arrayDimension.Dimensions;
                  continue;
                case ']':
                  goto label_31;
                default:
                  throw new Exception("Parse error");
              }
            }
            throw new Exception("Parse error");
          case ']':
            if (typeParser.Name == null)
              typeParser.Name = stringBuilder.ToString();
            if (flag1)
            {
              flag1 = false;
              continue;
            }
            _param2 = true;
            return typeParser;
          case '`':
            typeParser.IsGeneric = true;
            stringBuilder.Append(ch);
            while (_param1 < _param0.Length && _param0[_param1] != '[')
              stringBuilder.Append(_param0[_param1++]);
            ++_param1;
            bool flag3 = false;
            while (!flag3)
              typeParser.TypeArguments.Add(PXBuildManager.TypeParser.\u0002(_param0, ref _param1, out flag3));
            if (_param0[_param1 - 1] != ']')
              throw new Exception("Parse error");
            continue;
          default:
            if (!flag2 || ch != ' ')
            {
              flag2 = false;
              stringBuilder.Append(ch);
              continue;
            }
            continue;
        }
      }
      if (typeParser.Name == null)
        typeParser.Name = stringBuilder.ToString();
      return typeParser;
    }

    public Type MakeType()
    {
      Type type = BuildManager.GetType(this.Name, false, true);
      if (type == (Type) null && PXBuildManager.\u000E != null)
        type = PXBuildManager.\u000E(this.Name);
      if (type != (Type) null && this.IsGeneric)
      {
        Type[] array = this.TypeArguments.Select<PXBuildManager.TypeParser, Type>(PXBuildManager.TypeParser.\u0002.\u0008 ?? (PXBuildManager.TypeParser.\u0002.\u0008 = new Func<PXBuildManager.TypeParser, Type>(PXBuildManager.TypeParser.\u0002.\u0002.\u0002))).ToArray<Type>();
        type = type.MakeGenericType(array);
      }
      if (type != (Type) null && this.IsArray)
      {
        foreach (PXBuildManager.TypeParser.ArrayDimension arrayDimension in this.ArrayDimensions)
          type = type.MakeArrayType(arrayDimension.Dimensions);
      }
      return type;
    }

    [Serializable]
    private sealed class \u0002
    {
      public static readonly PXBuildManager.TypeParser.\u0002 \u0002 = new PXBuildManager.TypeParser.\u0002();
      public static Func<PXBuildManager.TypeParser, string> \u000E;
      public static Func<PXBuildManager.TypeParser.ArrayDimension, string> \u0006;
      public static Func<PXBuildManager.TypeParser, Type> \u0008;

      internal string \u0002(PXBuildManager.TypeParser _param1) => _param1.ToString();

      internal string \u0002(PXBuildManager.TypeParser.ArrayDimension _param1)
      {
        return _param1.ToString();
      }

      internal Type \u0002(PXBuildManager.TypeParser _param1) => _param1.MakeType();
    }

    private sealed class \u000E
    {
      public int \u0002;

      internal string \u0002(PXBuildManager.TypeParser _param1)
      {
        return _param1.FormatForDisplay(this.\u0002 + 4);
      }
    }

    public class ArrayDimension
    {
      public int Dimensions;

      public ArrayDimension() => this.Dimensions = 1;

      public override string ToString() => $"[{new string(',', this.Dimensions - 1)}]";
    }
  }
}
