// Decompiled with JetBrains decompiler
// Type: PX.Common.Tools
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Xml.Serialization;

#nullable enable
namespace PX.Common;

public static class Tools
{
  private static readonly GregorianCalendar \u0002 = new GregorianCalendar(GregorianCalendarTypes.Localized);
  private static readonly HashSet<Type> \u000E = new HashSet<Type>()
  {
    typeof (byte),
    typeof (byte?),
    typeof (sbyte),
    typeof (sbyte?),
    typeof (short),
    typeof (short?),
    typeof (ushort),
    typeof (ushort?),
    typeof (int),
    typeof (int?),
    typeof (uint),
    typeof (uint?),
    typeof (long),
    typeof (long?),
    typeof (ulong),
    typeof (ulong?),
    typeof (float),
    typeof (float?),
    typeof (double),
    typeof (double?),
    typeof (bool),
    typeof (bool?),
    typeof (Decimal),
    typeof (Decimal?),
    typeof (DateTime),
    typeof (DateTime?),
    typeof (Guid),
    typeof (Guid?),
    typeof (char),
    typeof (char?),
    typeof (string)
  };

  public static TOut[]? ToArray<TOut>(this IEnumerable? coll) => MainTools.ToArray<TOut>(coll);

  public static string Serialize(this object? obj)
  {
    if (obj == null)
      return string.Empty;
    Type type = obj.GetType();
    using (MemoryStream memoryStream = new MemoryStream())
    {
      new XmlSerializer(type).Serialize((Stream) memoryStream, obj);
      string str = Encoding.UTF8.GetString(memoryStream.ToArray());
      return type.FullName + "$$" + str;
    }
  }

  public static object? Deserialize(this string? data)
  {
    if (Str.IsNullOrEmpty(data))
      return (object) null;
    int length = data.IndexOf("$$");
    if (length <= 0 || length >= data.Length - "$$".Length)
      return (object) null;
    string typeName = data.Substring(0, length);
    string s = data.Substring(length + "$$".Length);
    using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
      return new XmlSerializer(PXBuildManager.GetType(typeName, true)).Deserialize((Stream) memoryStream);
  }

  public static T Apply<T>(this T target, Action<T> action)
  {
    action(target);
    return target;
  }

  public static void Call<TInput>(this TInput? o, Action<TInput> evaluator)
  {
    if (object.Equals((object) o, (object) default (TInput)))
      return;
    evaluator(o);
  }

  [return: NotNullIfNotNull("o")]
  public static TResult? With<TInput, TResult>(this TInput? o, Func<TInput, TResult> evaluator)
  {
    return object.Equals((object) o, (object) default (TInput)) ? default (TResult) : evaluator(o);
  }

  public static TResult? With<TInput, TResult>(this TInput? o, Func<TInput, TResult> evaluator) where TInput : struct
  {
    return !o.HasValue ? default (TResult) : evaluator(o.Value);
  }

  public static TResult Return<TInput, TResult>(
    this TInput? o,
    Func<TInput, TResult> evaluator,
    TResult failureValue)
  {
    return object.Equals((object) o, (object) default (TInput)) ? failureValue : evaluator(o);
  }

  public static TResult Return<TInput, TResult>(
    this TInput? o,
    Func<TInput, TResult> evaluator,
    Func<TResult> alternateEvaluator)
  {
    return object.Equals((object) o, (object) default (TInput)) ? alternateEvaluator() : evaluator(o);
  }

  public static bool GenericIsAssignableFrom(this Type baseType, Type c)
  {
    Tools.\u000E obj = new Tools.\u000E();
    obj.\u0002 = baseType;
    ExceptionExtensions.ThrowOnNull<Type>(obj.\u0002, nameof (baseType), (string) null);
    Type type1 = ExceptionExtensions.CheckIfNull<Type>(c, nameof (c), (string) null);
    if (obj.\u0002.IsGenericType)
      obj.\u0002 = obj.\u0002.GetGenericTypeDefinition();
    if (obj.\u0002.IsInterface)
      return ((IEnumerable<Type>) c.GetInterfaces()).Any<Type>(new Func<Type, bool>(obj.\u0002));
    Type type2 = typeof (object);
    while (type1 != type2 && type1 != (Type) null && type1.BaseType != (Type) null)
    {
      type1 = type1.BaseType;
      if (type1.IsGenericType)
        type1 = type1.GetGenericTypeDefinition();
      if (type1 == obj.\u0002)
        return true;
    }
    return false;
  }

  public static bool HasDefaultConstructor(this Type t)
  {
    return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != (ConstructorInfo) null;
  }

  /// <summary>Converts a type instance to its code representation</summary>
  /// <remarks>
  /// <code>typeof(Dictionary&lt;Tuple&lt;int, string&gt;, List&lt;double&gt;&gt;).ToCodeString()</code> will return "Dictionary&lt;Tuple&lt;int, string&gt;, List&lt;double&gt;&gt;"
  ///     </remarks>
  public static string ToCodeString(this Type type, bool preserveFullNames = false)
  {
    Tools.\u0006 obj = new Tools.\u0006();
    obj.\u0002 = preserveFullNames;
    ExceptionExtensions.ThrowOnNull<Type>(type, nameof (type), (string) null);
    if (typeof (ITypeArrayOf<object>).IsAssignableFrom(type))
    {
      string str = typeof (TypeArray).With<Type, string>(new Func<Type, string>(obj.\u0002));
      Type[] source = TypeArray.CheckAndExtract<object>(type, (string) null);
      return source.Length == 0 ? str + ".Empty" : $"{str}<{string.Join(", ", ((IEnumerable<Type>) source).Select<Type, string>(new Func<Type, string>(obj.\u000E)))}>";
    }
    if (type.IsGenericType)
    {
      \u003C\u003Ef__AnonymousType0<Type, string[]>[] array = Enumerable.ToArray(type.\u000E().Reverse<Type>().Select(new Func<Type, \u003C\u003Ef__AnonymousType0<Type, string[]>>(new Tools.\u0008()
      {
        \u0002 = EnumerableExtensions.ToQueue<string>(((IEnumerable<Type>) type.GetGenericArguments()).Select<Type, string>(new Func<Type, string>(obj.\u0006)))
      }.\u0002)));
      string codeString;
      if (array[0].Type.IsGenericType)
      {
        codeString = obj.\u0002 ? array[0].Type.GetGenericSimpleFullName() : MainTools.GetGenericSimpleName(array[0].Type);
        if (((IEnumerable<string>) array[0].Args).Any<string>())
          codeString = $"{codeString}<{string.Join(", ", array[0].Args)}>";
      }
      else
        codeString = (array[0].Type.FullName ?? array[0].Type.Name).With<string, string>(new Func<string, string>(obj.\u000E));
      for (int index = 1; index < array.Length; ++index)
      {
        codeString = $"{codeString}.{MainTools.GetGenericSimpleName(array[index].Type)}";
        if (((IEnumerable<string>) array[index].Args).Any<string>())
          codeString = $"{codeString}<{string.Join(", ", array[index].Args)}>";
      }
      return codeString;
    }
    Type[] array1 = Enumerable.ToArray<Type>(type.\u000E().Reverse<Type>());
    string codeString1 = (array1[0].FullName ?? array1[0].Name).With<string, string>(new Func<string, string>(obj.\u0006));
    for (int index = 1; index < array1.Length; ++index)
      codeString1 = $"{codeString1}.{array1[index].Name}";
    return codeString1;
  }

  public static bool IsPrimitive<TType>() => typeof (TType).IsPrimitive();

  public static bool IsPrimitive(this Type type) => Tools.\u000E.Contains(type);

  public static string GetGenericSimpleFullName(this Type type)
  {
    return StringExtensions.FirstSegment(type.FullName ?? type.Name, '`');
  }

  public static Type? GetNestedType(
    this Type type,
    string typeName,
    BindingFlags flags,
    bool searchInBaseClasses)
  {
    if (!searchInBaseClasses)
      return type.GetNestedType(typeName, flags);
    for (; type != typeof (object); type = type.BaseType)
    {
      Type nestedType = type.GetNestedType(typeName, flags);
      if (nestedType != (Type) null)
        return nestedType;
    }
    return (Type) null;
  }

  /// <summary>
  /// Returns a number representing the length of the inheritance chain for the current type
  /// (count of base classes, including the current type itself).
  /// </summary>
  /// <remarks>Function will return 1 for <see cref="T:System.Object" /> type</remarks>
  /// <param name="type">Inspecting type</param>
  public static int GetInheritanceDepth(this Type type) => type.GetInheritanceChain().Count<Type>();

  /// <summary>
  /// Returns a sequence of base classes which are present in the inheritance chain of the current type,
  /// starting with the current type itself and ending with an <see cref="T:System.Object" /> type.
  /// </summary>
  /// <param name="type">Inspecting type</param>
  public static IEnumerable<Type> GetInheritanceChain(this Type type)
  {
    return ExceptionExtensions.CheckIfNull<Type>(type, nameof (type), (string) null).\u0002();
  }

  private static IEnumerable<Type> \u0002(this Type? _param0)
  {
    return (IEnumerable<Type>) new Tools.\u0005(-2)
    {
      \u0003 = _param0
    };
  }

  private static IEnumerable<Type> \u000E(this Type? _param0)
  {
    return (IEnumerable<Type>) new Tools.\u000F(-2)
    {
      \u0003 = _param0
    };
  }

  public static bool IsAnonymousType(this Type type)
  {
    return Attribute.IsDefined((MemberInfo) ExceptionExtensions.CheckIfNull<Type>(type, nameof (type), (string) null), typeof (CompilerGeneratedAttribute), false);
  }

  public static string? ToCapitalized(this string? text)
  {
    return !Str.IsNullOrEmpty(text) ? char.ToUpper(text[0]).ToString() + text.Substring(1) : text;
  }

  [Obsolete("Use HtmlEntensions.GetHtmlBodyContent instead")]
  public static string GetBody(string? value) => HtmlEntensions.GetHtmlBodyContent(value);

  [Obsolete("Use HtmlEntensions.MergeHtmls instead")]
  public static string? AppendToHtmlBody(string? html, string delimiter, string? value)
  {
    return HtmlEntensions.MergeHtmls(html, value);
  }

  [Obsolete("Use HtmlEntensions.RemoveHeader instead")]
  public static string RemoveHeader(string? html) => HtmlEntensions.RemoveHeader(html);

  [Obsolete("Use HtmlEntensions.ConvertHtmlFragmentToSimpleText instead")]
  public static string? ConvertHtmlFragmentToSimpleText(string? html)
  {
    return HtmlEntensions.ConvertHtmlFragmentToSimpleText(html);
  }

  [Obsolete("Use HtmlEntensions.ConvertHtmlToSimpleText instead")]
  public static string ConvertHtmlToSimpleText(string? html)
  {
    return HtmlEntensions.ConvertHtmlToSimpleText(html);
  }

  [Obsolete("Use HtmlEntensions.ConvertSimpleTextToHtml instead")]
  public static string ConvertSimpleTextToHtml(string? simpleText)
  {
    return HtmlEntensions.ConvertSimpleTextToHtml(simpleText);
  }

  public static string?[] Split(this string? s, char separator, int count)
  {
    return Str.IsNullOrEmpty(s) ? new string[1]{ s } : s.Split(new char[1]
    {
      separator
    }, count);
  }

  public static string Replace(
    this string str,
    string oldValue,
    string newValue,
    StringComparison comparison)
  {
    ExceptionExtensions.ThrowOnNull<string>(str, nameof (str), (string) null);
    ExceptionExtensions.ThrowOnNull<string>(oldValue, nameof (oldValue), (string) null);
    int num1;
    int num2 = num1 = 0;
    int startIndex = num1;
    int length = num1;
    string upper1 = str.ToUpper();
    string upper2 = oldValue.ToUpper();
    int val2 = str.Length / oldValue.Length * (newValue.Length - oldValue.Length);
    char[] chArray = new char[str.Length + Math.Max(0, val2)];
    int num3;
    for (; (num3 = upper1.IndexOf(upper2, startIndex, comparison)) != -1; startIndex = num3 + oldValue.Length)
    {
      for (int index = startIndex; index < num3; ++index)
        chArray[length++] = str[index];
      for (int index = 0; index < newValue.Length; ++index)
        chArray[length++] = newValue[index];
    }
    if (startIndex == 0)
      return str;
    for (int index = startIndex; index < str.Length; ++index)
      chArray[length++] = str[index];
    return new string(chArray, 0, length);
  }

  public static int LastIndex<T>(this T[] array) => array.Length - 1;

  public static long Count(this IEnumerable coll)
  {
    IEnumerator enumerator = (IEnumerator) null;
    try
    {
      enumerator = coll.GetEnumerator();
      long num = 0;
      while (enumerator.MoveNext())
        ++num;
      return num;
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
  }

  public static bool ContainedBy(this string str, params string[] arr)
  {
    return arr != null && ((IEnumerable<string>) arr).Any<string>(new Func<string, bool>(new Tools.\u0003()
    {
      \u0002 = str
    }.\u0002));
  }

  public static string? RemoveFromStart(
    this string? s,
    string prefix,
    StringComparison comparisonType = StringComparison.Ordinal)
  {
    return Str.IsNullOrEmpty(s) || !s.StartsWith(prefix, comparisonType) ? s : s.Substring(prefix.Length, s.Length - prefix.Length);
  }

  public static string? RemoveFromEnd(
    this string? s,
    string suffix,
    StringComparison comparisonType = StringComparison.Ordinal)
  {
    return Str.IsNullOrEmpty(s) || !s.EndsWith(suffix, comparisonType) ? s : s.Substring(0, s.Length - suffix.Length);
  }

  public static IEnumerable GetResetableEnumerator(this IEnumerable obj)
  {
    return (IEnumerable) new ResetableEnumerableObject(obj);
  }

  public static IEnumerable<T> GetResetableEnumerator<T>(this IEnumerable<T> obj)
  {
    return (IEnumerable<T>) new ResetableEnumerableObject<T>(obj);
  }

  public static void QuickArraySort<T>(T[] array, IComparer<T> comparer)
  {
    ExceptionExtensions.ThrowOnNull<T[]>(array, nameof (array), (string) null);
    ExceptionExtensions.ThrowOnNull<IComparer<T>>(comparer, nameof (comparer), (string) null);
    if (array.Length < 2)
      return;
    long[] numArray1 = new long[2048 /*0x0800*/];
    long[] numArray2 = new long[2048 /*0x0800*/];
    long index1 = 1;
    numArray1[1] = 0L;
    numArray2[1] = (long) (array.Length - 1);
    do
    {
      long num1 = numArray1[index1];
      long num2 = numArray2[index1];
      --index1;
      do
      {
        long index2 = num1 + num2 >> 1;
        long index3 = num1;
        long index4 = num2;
        T obj1 = array[index2];
        do
        {
          while (comparer.Compare(array[index3], obj1) < 0)
            ++index3;
          while (comparer.Compare(obj1, array[index4]) < 0)
            --index4;
          if (index3 <= index4)
          {
            T obj2 = array[index3];
            array[index3] = array[index4];
            array[index4] = obj2;
            ++index3;
            --index4;
          }
        }
        while (index3 <= index4);
        if (index3 < index2)
        {
          if (index3 < num2)
          {
            ++index1;
            numArray1[index1] = index3;
            numArray2[index1] = num2;
          }
          num2 = index4;
        }
        else
        {
          if (index4 > num1)
          {
            ++index1;
            numArray1[index1] = num1;
            numArray2[index1] = index4;
          }
          num1 = index3;
        }
      }
      while (num1 < num2);
    }
    while (index1 != 0L);
  }

  /// <summary>ISO 8601</summary>
  public static int GetWeekNumber(DateTime date) => Tools.GetWeekNumber(date, DayOfWeek.Sunday);

  public static int GetWeekNumber(DateTime date, DayOfWeek firstDayOfWeek)
  {
    DateTime dateTime = new DateTime(date.Year, 12, 31 /*0x1F*/);
    int num = (7 + (Tools.\u0002.GetDayOfWeek(date) - firstDayOfWeek)) % 7;
    return date.AddDays((double) (-1 * num)).AddDays(3.0) > dateTime ? 1 : Tools.\u0002.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, firstDayOfWeek);
  }

  /// <summary>ISO 8601</summary>
  public static DateTime GetWeekStart(int year, int weekNumber)
  {
    return Tools.GetWeekStart(year, weekNumber, DayOfWeek.Sunday);
  }

  public static DateTime GetWeekStart(int year, int weekNumber, DayOfWeek firstDayOfWeek)
  {
    DateTime date = new DateTime(year, 1, 15);
    int weekNumber1 = Tools.GetWeekNumber(date, firstDayOfWeek);
    int num = 7 * (weekNumber - weekNumber1) - Tools.\u0002(date, firstDayOfWeek);
    return date.AddDays((double) num);
  }

  private static int \u0002(DateTime _param0, DayOfWeek _param1)
  {
    int num = _param0.DayOfWeek - _param1;
    if (num < 0)
      num += 7;
    return num;
  }

  public static Type? TryLoadType(string type)
  {
    Type type1 = (Type) null;
    try
    {
      Type type2 = PXBuildManager.GetType(type, false);
      if (Tools.\u0002(type2))
        type1 = type2;
    }
    catch (StackOverflowException ex)
    {
      throw;
    }
    catch (OutOfMemoryException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
    }
    return type1;
  }

  private static bool \u0002(Type _param0)
  {
    object[] objArray = _param0.With<Type, object[]>(Tools.\u0002.\u000E ?? (Tools.\u0002.\u000E = new Func<Type, object[]>(Tools.\u0002.\u0002.\u0002)));
    if (objArray != null)
    {
      foreach (RequiredTypesAttribute requiredTypesAttribute in objArray)
      {
        foreach (Type type in requiredTypesAttribute.Types)
        {
          if (!Tools.\u0002(type))
            return false;
        }
      }
    }
    return true;
  }

  public static string ZipToString(this string str) => Encoding.UTF8.GetString(str.Zip());

  public static byte[] Zip(this string str)
  {
    using (MemoryStream memoryStream1 = new MemoryStream(Encoding.UTF8.GetBytes(str)))
    {
      using (MemoryStream memoryStream2 = new MemoryStream())
      {
        using (GZipStream gzipStream = new GZipStream((Stream) memoryStream2, CompressionMode.Compress))
          Tools.\u0002((Stream) memoryStream1, (Stream) gzipStream);
        return memoryStream2.ToArray();
      }
    }
  }

  public static string Unzip(this byte[] bytes)
  {
    using (MemoryStream memoryStream1 = new MemoryStream(bytes))
    {
      using (MemoryStream memoryStream2 = new MemoryStream())
      {
        using (GZipStream gzipStream = new GZipStream((Stream) memoryStream1, CompressionMode.Decompress))
          Tools.\u0002((Stream) gzipStream, (Stream) memoryStream2);
        return Encoding.UTF8.GetString(memoryStream2.ToArray());
      }
    }
  }

  public static string UnzipFromString(this string src) => Encoding.UTF8.GetBytes(src).Unzip();

  private static void \u0002(Stream _param0, Stream _param1)
  {
    byte[] buffer = new byte[4096 /*0x1000*/];
    int count;
    while ((count = _param0.Read(buffer, 0, buffer.Length)) != 0)
      _param1.Write(buffer, 0, count);
  }

  public static string AppendUrlParam(this string url, string name, string value)
  {
    ExceptionExtensions.ThrowOnNull<string>(url, nameof (url), (string) null);
    ExceptionExtensions.ThrowOnNull<string>(name, nameof (name), (string) null);
    ExceptionExtensions.ThrowOnNull<string>(value, nameof (value), (string) null);
    char ch = url.Contains<char>('?') ? '&' : '?';
    return $"{url}{ch.ToString()}{name}={Uri.EscapeUriString(value)}";
  }

  public static bool TryAddHeader(this HttpResponse response, string name, string value)
  {
    try
    {
      response.AddHeader(name, value);
      return true;
    }
    catch (HttpException ex)
    {
    }
    return false;
  }

  /// <summary>Suppress expression type</summary>
  /// <typeparam name="T">Expression original type</typeparam>
  /// <param name="expression">Expression whose type should be suppressed</param>
  public static void ToVoid<T>(this T expression)
  {
  }

  /// <summary>Logical implication (lazy)</summary>
  /// <param name="antecedent">Antecedent</param>
  /// <param name="consequent">Consequent</param>
  /// <returns>
  /// <c>false</c> if <paramref name="antecedent" /> is <c>true</c>
  ///     and <paramref name="consequent" /> is <c>false</c>, otherwise - <c>true</c></returns>
  public static bool Implies(this bool antecedent, Func<bool> consequent)
  {
    return !antecedent || consequent();
  }

  /// <summary>Logical implication (eager)</summary>
  /// <param name="antecedent">Antecedent</param>
  /// <param name="consequent">Consequent</param>
  /// <returns>
  /// <c>false</c> if <paramref name="antecedent" /> is <c>true</c>
  ///     and <paramref name="consequent" /> is <c>false</c>, otherwise - <c>true</c></returns>
  public static bool Implies(this bool antecedent, bool consequent) => !antecedent | consequent;

  public static string ToInvariantString(this object obj)
  {
    switch (obj)
    {
      case string invariantString:
        return invariantString;
      case IFormattable formattable:
        return formattable.ToString((string) null, (IFormatProvider) CultureInfo.InvariantCulture);
      case IConvertible convertible:
        return convertible.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      case null:
        throw new ArgumentNullException(nameof (obj));
      default:
        using (new PXInvariantCultureScope())
          return obj.ToString();
    }
  }

  public static void Init<T>(this Lazy<T> lazy)
  {
    T obj = lazy.Value;
  }

  [DebuggerStepThrough]
  public static TValue? AsNullable<TValue>(this TValue val) where TValue : struct
  {
    return new TValue?(val);
  }

  public static T Max<T>(T first, T second) where T : IComparable<T>
  {
    return Comparer<T>.Default.Compare(first, second) <= 0 ? second : first;
  }

  public static T Max<T>(T first, T second, T third, params T[] others) where T : IComparable<T>
  {
    return ((IEnumerable<T>) new T[3]
    {
      first,
      second,
      third
    }).Concat<T>((IEnumerable<T>) others).Max<T>();
  }

  public static T Min<T>(T first, T second) where T : IComparable<T>
  {
    return Comparer<T>.Default.Compare(first, second) >= 0 ? second : first;
  }

  public static T Min<T>(T first, T second, T third, params T[] others) where T : IComparable<T>
  {
    return ((IEnumerable<T>) new T[3]
    {
      first,
      second,
      third
    }).Concat<T>((IEnumerable<T>) others).Min<T>();
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly 
    #nullable disable
    Tools.\u0002 \u0002 = new Tools.\u0002();
    public static Func<Type, object[]> \u000E;

    internal object[] \u0002(Type _param1)
    {
      return _param1.GetCustomAttributes(typeof (RequiredTypesAttribute), true);
    }
  }

  private sealed class \u0003
  {
    public string \u0002;

    internal bool \u0002(string _param1)
    {
      return string.Equals(_param1, this.\u0002, StringComparison.OrdinalIgnoreCase);
    }
  }

  private sealed class \u0005 : 
    IEnumerable<Type>,
    IEnumerable,
    IEnumerator<Type>,
    IDisposable,
    IEnumerator
  {
    private int \u0002;
    private 
    #nullable enable
    Type \u000E;
    private int \u0006;
    private 
    #nullable disable
    Type \u0008;
    public Type \u0003;

    [DebuggerHidden]
    public \u0005(int _param1)
    {
      this.\u0002 = _param1;
      this.\u0006 = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.\u0005\u2009\u2009\u2009\u0002()
    {
    }

    bool IEnumerator.MoveNext()
    {
      switch (this.\u0002)
      {
        case 0:
          this.\u0002 = -1;
          break;
        case 1:
          this.\u0002 = -1;
          this.\u0008 = this.\u0008.BaseType;
          break;
        default:
          return false;
      }
      if (!(this.\u0008 != (Type) null))
        return false;
      this.\u000E = this.\u0008;
      this.\u0002 = 1;
      return true;
    }

    [DebuggerHidden]
    #nullable enable
    Type IEnumerator<
    #nullable disable
    Type>.\u0005\u2009\u2009\u2009\u0002() => this.\u000E;

    [DebuggerHidden]
    void IEnumerator.\u0005\u2009\u2009\u2009\u000E() => throw new NotSupportedException();

    [DebuggerHidden]
    object IEnumerator.\u0005\u2009\u2009\u2009\u0002() => (object) this.\u000E;

    [DebuggerHidden]
    IEnumerator<Type> IEnumerable<Type>.\u0005\u2009\u2009\u2009\u0002()
    {
      Tools.\u0005 obj;
      if (this.\u0002 == -2 && this.\u0006 == Environment.CurrentManagedThreadId)
      {
        this.\u0002 = 0;
        obj = this;
      }
      else
        obj = new Tools.\u0005(0);
      obj.\u0008 = this.\u0003;
      return (IEnumerator<Type>) obj;
    }

    [DebuggerHidden]
    IEnumerator IEnumerable.\u0005\u2009\u2009\u2009\u0002()
    {
      return (IEnumerator) this.\u0005\u2009\u2009\u2009\u0002();
    }
  }

  private sealed class \u0006
  {
    public bool \u0002;
    public Func<string, string> \u000E;

    internal string \u0002(Type _param1)
    {
      return (_param1.FullName ?? _param1.Name).With<string, string>(this.\u000E ?? (this.\u000E = new Func<string, string>(this.\u0002)));
    }

    internal string \u0002(string _param1)
    {
      return !this.\u0002 ? StringExtensions.LastSegment(_param1, '.') : _param1;
    }

    internal string \u000E(Type _param1) => _param1.ToCodeString(this.\u0002);

    internal string \u0006(Type _param1) => _param1.ToCodeString(this.\u0002);

    internal string \u000E(string _param1)
    {
      return !this.\u0002 ? StringExtensions.LastSegment(_param1, '.') : _param1;
    }

    internal string \u0006(string _param1)
    {
      return !this.\u0002 ? StringExtensions.LastSegment(_param1, '.') : _param1;
    }
  }

  private sealed class \u0008
  {
    public Queue<string> \u0002;

    internal \u003C\u003Ef__AnonymousType0<Type, string[]> \u0002(Type _param1)
    {
      return new
      {
        Type = _param1,
        Args = Enumerable.ToArray<string>(EnumerableExtensions.Dequeue<string>(this.\u0002, _param1.IsGenericType ? _param1.GetGenericArguments().Length : 0))
      };
    }
  }

  private sealed class \u000E
  {
    public Type \u0002;

    internal bool \u0002(Type _param1)
    {
      return this.\u0002.Name == _param1.Name && this.\u0002.Namespace == _param1.Namespace && this.\u0002.Assembly == _param1.Assembly;
    }
  }

  private sealed class \u000F : 
    IEnumerable<Type>,
    IEnumerable,
    IEnumerator<Type>,
    IDisposable,
    IEnumerator
  {
    private int \u0002;
    private 
    #nullable enable
    Type \u000E;
    private int \u0006;
    private 
    #nullable disable
    Type \u0008;
    public Type \u0003;

    [DebuggerHidden]
    public \u000F(int _param1)
    {
      this.\u0002 = _param1;
      this.\u0006 = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.\u000F\u2009\u2009\u2009\u0002()
    {
    }

    bool IEnumerator.MoveNext()
    {
      switch (this.\u0002)
      {
        case 0:
          this.\u0002 = -1;
          break;
        case 1:
          this.\u0002 = -1;
          this.\u0008 = this.\u0008.DeclaringType;
          break;
        default:
          return false;
      }
      if (!(this.\u0008 != (Type) null))
        return false;
      this.\u000E = this.\u0008;
      this.\u0002 = 1;
      return true;
    }

    [DebuggerHidden]
    #nullable enable
    Type IEnumerator<
    #nullable disable
    Type>.\u000F\u2009\u2009\u2009\u0002() => this.\u000E;

    [DebuggerHidden]
    void IEnumerator.\u000F\u2009\u2009\u2009\u000E() => throw new NotSupportedException();

    [DebuggerHidden]
    object IEnumerator.\u000F\u2009\u2009\u2009\u0002() => (object) this.\u000E;

    [DebuggerHidden]
    IEnumerator<Type> IEnumerable<Type>.\u000F\u2009\u2009\u2009\u0002()
    {
      Tools.\u000F obj;
      if (this.\u0002 == -2 && this.\u0006 == Environment.CurrentManagedThreadId)
      {
        this.\u0002 = 0;
        obj = this;
      }
      else
        obj = new Tools.\u000F(0);
      obj.\u0008 = this.\u0003;
      return (IEnumerator<Type>) obj;
    }

    [DebuggerHidden]
    IEnumerator IEnumerable.\u000F\u2009\u2009\u2009\u0002()
    {
      return (IEnumerator) this.\u000F\u2009\u2009\u2009\u0002();
    }
  }
}
