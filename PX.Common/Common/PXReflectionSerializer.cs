// Decompiled with JetBrains decompiler
// Type: PX.Common.PXReflectionSerializer
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Serialization;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

#nullable disable
namespace PX.Common;

public class PXReflectionSerializer : ReflectionSerializer
{
  private static readonly Dictionary<Type, PXReflectionSerializer.\u000E\u2009> \u0002 = new Dictionary<Type, PXReflectionSerializer.\u000E\u2009>((IEqualityComparer<Type>) new ReflectionSerializer.ObjectComparer<Type>());
  private static readonly ReaderWriterLock \u000E = new ReaderWriterLock();
  public static Dictionary<object, object> Refs = new Dictionary<object, object>((IEqualityComparer<object>) new ReflectionSerializer.ObjectComparer<object>());

  private static MethodInfo \u0002<T>(Expression<Action<T>> _param0)
  {
    return ((MethodCallExpression) _param0.Body).Method;
  }

  private static FieldInfo \u000E<T>(Expression<Func<T, object>> _param0)
  {
    return (FieldInfo) ((MemberExpression) _param0.Body).Member;
  }

  public static int GetStableHash(string s)
  {
    if (s == null)
      return 0;
    int stableHash = 17;
    foreach (char ch in s)
      stableHash = stableHash * 23 + (int) ch;
    return stableHash;
  }

  private static PXReflectionSerializer.\u000E\u2009 \u0002(Type _param0)
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXReflectionSerializer.\u000E);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      PXReflectionSerializer.\u000E\u2009 obj;
      if (PXReflectionSerializer.\u0002.TryGetValue(_param0, out obj))
        return obj;
      ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
      if (PXReflectionSerializer.\u0002.TryGetValue(_param0, out obj))
        return obj;
      obj = new PXReflectionSerializer.\u000E\u2009(_param0);
      PXReflectionSerializer.\u0002.Add(_param0, obj);
      return obj;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  public static void Serialize(Stream stream, object root)
  {
    PXReflectionSerializer.\u0006\u2009 obj = new PXReflectionSerializer.\u0006\u2009();
    PXReflectionSerializer.\u0002(obj, root);
    obj.\u0002(stream);
  }

  public static byte[] GetHashCode(object root)
  {
    PXReflectionSerializer.\u0006\u2009 obj = new PXReflectionSerializer.\u0006\u2009();
    PXReflectionSerializer.\u0002(obj, root);
    return obj.\u0002();
  }

  public static T Clone<T>(T item)
  {
    return (T) PXReflectionSerializer.Clone((Stream) new MemoryStream(), (object) item);
  }

  public static object Clone(Stream stream, object root)
  {
    PXReflectionSerializer.Refs.Clear();
    PXReflectionSerializer.\u0006\u2009 obj1 = new PXReflectionSerializer.\u0006\u2009();
    PXReflectionSerializer.\u0002(obj1, root);
    obj1.\u0002(stream);
    Enumerable.ToArray<KeyValuePair<Type, int>>(obj1.\u0002.OrderByDescending<KeyValuePair<Type, int>, int>(PXReflectionSerializer.\u0002.\u000E ?? (PXReflectionSerializer.\u0002.\u000E = new Func<KeyValuePair<Type, int>, int>(PXReflectionSerializer.\u0002.\u0002.\u0002))));
    stream.Position = 0L;
    object obj2 = new PXReflectionSerializer.\u0002\u2009(stream).\u0002();
    DeflateStream destination = new DeflateStream((Stream) new MemoryStream(), CompressionMode.Compress);
    stream.Position = 0L;
    stream.CopyTo((Stream) destination);
    destination.Close();
    return obj2;
  }

  /// <summary>
  /// Returns a sequence where each element was cloned by <see cref="M:PX.Common.PXReflectionSerializer.Clone``1(``0)" /> method from corresponding one of the source sequence
  /// </summary>
  public static IEnumerable<T> CloneSequence<T>(IEnumerable<T> source)
  {
    return (source != null ? source.Select<T, T>(PXReflectionSerializer.\u000E<T>.\u0002 ?? (PXReflectionSerializer.\u000E<T>.\u0002 = new Func<T, T>(PXReflectionSerializer.Clone<T>))) : (IEnumerable<T>) null) ?? Enumerable.Empty<T>();
  }

  public static object GetRef(object x) => PXReflectionSerializer.Refs[x];

  private static void \u0002(PXReflectionSerializer.\u0006\u2009 _param0, object _param1)
  {
    try
    {
      _param0.h(_param1, (PXReflectionSerializer.\u000E\u2009) null, (PXReflectionSerializer.\u000E\u2009) null);
    }
    catch (Exception ex)
    {
      int count = _param0.\u0003\u2009.Count;
      if (count == 0)
        throw;
      StringBuilder stringBuilder = new StringBuilder(count + 1);
      foreach (PXReflectionSerializer.\u000E\u2009 obj in _param0.\u0003\u2009)
        stringBuilder.AppendLine(obj.\u0002.FullName);
      stringBuilder.AppendLine(ex.Message);
      throw new PXSerializationException(stringBuilder.ToString());
    }
  }

  public static object Serialize(Stream stream, object root, bool check)
  {
    if (!check)
    {
      PXReflectionSerializer.Serialize(stream, root);
      return (object) null;
    }
    long position = stream.Position;
    stream.Position = position;
    PXReflectionSerializer.\u0006\u2009 obj = new PXReflectionSerializer.\u0006\u2009();
    PXReflectionSerializer.\u0002(obj, root);
    obj.\u0002(stream);
    stream.Position = 0L;
    object b = new PXReflectionSerializer.\u0002\u2009(stream)
    {
      \u000F = obj
    }.\u0002();
    new PXObjectComparer().Compare(root, b, (PXObjectComparer.StackFrame) null);
    return b;
  }

  public static object Deserialize(Stream stream)
  {
    PXReflectionSerializer.\u0002\u2009 obj1 = new PXReflectionSerializer.\u0002\u2009(stream);
    try
    {
      return obj1.\u0002();
    }
    catch (Exception ex)
    {
      int count = obj1.\u000E\u2009.Count;
      if (count == 0)
        throw;
      StringBuilder stringBuilder = new StringBuilder(count + 1);
      foreach (PXReflectionSerializer.\u000E\u2009 obj2 in obj1.\u000E\u2009)
        stringBuilder.AppendLine(obj2.\u0002.FullName);
      stringBuilder.AppendLine("Original exception:" + ex?.ToString());
      throw new PXSerializationException(stringBuilder.ToString());
    }
  }

  internal static MemProfilerResult GetSize(object _param0)
  {
    using (PXReflectionSerializer.\u0005 obj1 = new PXReflectionSerializer.\u0005())
    {
      PXReflectionSerializer.\u0006\u2009 obj2 = new PXReflectionSerializer.\u0006\u2009(true);
      PXReflectionSerializer.\u0002(obj2, _param0);
      obj2.\u0002((Stream) obj1);
      return new MemProfilerResult(obj2.\u0002().Values.ToArray<MemProfilerTypeInfo>(), obj1.Length);
    }
  }

  public static void TestSize()
  {
    PXReflectionSerializer.GetSize((object) new Dictionary<int, int>()
    {
      {
        2,
        3
      },
      {
        4,
        5
      }
    });
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly PXReflectionSerializer.\u0002 \u0002 = new PXReflectionSerializer.\u0002();
    public static Func<KeyValuePair<Type, int>, int> \u000E;

    internal int \u0002(KeyValuePair<Type, int> _param1) => _param1.Value;
  }

  private sealed class \u0002\u2009
  {
    public object[] \u0002;
    private readonly Stream \u000E;
    private readonly BinaryReaderEx \u0006;
    public readonly BinaryReaderEx h;
    private readonly PXReflectionSerializer.\u000E\u2009[] \u0008;
    private readonly List<IDeserializationCallback> \u0003 = new List<IDeserializationCallback>();
    public PXReflectionSerializer.\u0006\u2009 \u000F;
    private int \u0005;
    public readonly StreamingContext \u0002\u2009;
    internal Stack<PXReflectionSerializer.\u000E\u2009> \u000E\u2009 = new Stack<PXReflectionSerializer.\u000E\u2009>();
    public PXReflectionSerializer.LogEntry \u0006\u2009;
    private int \u0008\u2009;

    public \u0002\u2009(Stream _param1)
    {
      BinaryReaderEx binaryReaderEx = new BinaryReaderEx(_param1);
      ushort length = binaryReaderEx.ReadUInt16();
      this.\u0008 = new PXReflectionSerializer.\u000E\u2009[(int) length];
      for (int index = 0; index < (int) length; ++index)
      {
        Type type = binaryReaderEx.ReadTypeString();
        int num1 = binaryReaderEx.ReadInt32();
        PXReflectionSerializer.\u000E\u2009 obj = PXReflectionSerializer.\u0002(type);
        int num2 = obj.\u0002();
        if (num1 != num2)
          throw new PXSerializationException($"Distinct Type hash: {type.FullName}, old hash:{num1} new hash:{num2}");
        this.\u0008[index] = obj;
      }
      this.\u0002 = new object[binaryReaderEx.ReadInt32()];
      int count1 = binaryReaderEx.ReadInt32();
      this.\u000E = (Stream) new MemoryStream(binaryReaderEx.ReadBytes(count1));
      int count2 = binaryReaderEx.ReadInt32();
      this.h = new BinaryReaderEx((Stream) new MemoryStream(binaryReaderEx.ReadBytes(count2)));
      int count3 = binaryReaderEx.ReadInt32();
      this.\u0006 = new BinaryReaderEx((Stream) new MemoryStream(binaryReaderEx.ReadBytes(count3)));
      this.h.BitReader.Bytes = binaryReaderEx.ReadByteArray();
    }

    public object \u0002()
    {
      object obj = this.h((PXReflectionSerializer.\u000E\u2009) null);
      foreach (IDeserializationCallback deserializationCallback in this.\u0003)
      {
        try
        {
          deserializationCallback.OnDeserialization((object) null);
        }
        catch (ArgumentException ex)
        {
          if (!(deserializationCallback is IDictionary))
            throw;
          PXFirstChanceExceptionLoggerProxy.LogMessage("Error deserializing type " + deserializationCallback.GetType().FullName);
          foreach (object key in (IEnumerable) ((IDictionary) deserializationCallback).Keys)
          {
            if (key != null)
            {
              if (key is WeakReference)
                PXFirstChanceExceptionLoggerProxy.LogMessage($"Entry {(((WeakReference) key).Target ?? key).ToString()}: {((IDictionary) deserializationCallback)[key]?.ToString()}");
              else
                PXFirstChanceExceptionLoggerProxy.LogMessage($"Entry {key.ToString()}: {((IDictionary) deserializationCallback)[key]?.ToString()}");
            }
            else
              PXFirstChanceExceptionLoggerProxy.LogMessage("Entry with null key found");
          }
          throw;
        }
      }
      return obj;
    }

    public object h(PXReflectionSerializer.\u000E\u2009 _param1)
    {
      PXReflectionSerializer.LogEntry logEntry = (PXReflectionSerializer.LogEntry) null;
      if (this.\u000F != null)
      {
        logEntry = this.\u000F.\u000E[this.\u0005];
        this.\u0006\u2009 = logEntry;
        if ((long) logEntry.StartPos != this.h.BaseStream.Position)
          throw new Exception("Bad Position");
      }
      ++this.\u0005;
      if (_param1 != null && _param1.\u000E\u2007)
        _param1 = (PXReflectionSerializer.\u000E\u2009) null;
      int num1 = _param1 == null ? 1 : (_param1.\u000E ? 1 : 0);
      PXReflectionSerializer.\u000E\u2009 obj1 = _param1;
      if (num1 != 0)
      {
        int num2 = this.\u0006.ReadInt32();
        if (num2 == 0)
          return (object) null;
        if (num2 >= 2)
          return PXReflectionSerializer.\u0003.\u0002(this.\u0002[num2 - 2]);
        if (num2 < 0)
          obj1 = this.\u0008[-num2 - 1];
        else if (num2 != 1)
          throw new Exception("Bad index");
      }
      if (logEntry != null && logEntry.Item != null && logEntry.Item.GetType() != obj1.\u0002 && logEntry.Item.GetType() != obj1.\u0003\u2006)
        throw new Exception("Type");
      if (obj1.\u0006\u2000 != null && !this.h.ReadBoolean())
      {
        object instance = Activator.CreateInstance(obj1.\u0002);
        this.\u000E(obj1, instance);
        return instance;
      }
      if (this.h.ReadBoolean())
      {
        ReaderWriterLock readerWriterLock = new ReaderWriterLock();
        this.\u000E(obj1, (object) readerWriterLock);
        return (object) readerWriterLock;
      }
      this.\u000E\u2009.Push(obj1);
      object obj2 = obj1.\u000E\u2004(this);
      if (obj2 is PXSerializationSurrogate serializationSurrogate)
      {
        obj2 = serializationSurrogate.RestoreObject();
        ((PXReflectionSerializer.\u0003) serializationSurrogate.ForwardItem).\u0002(obj2);
      }
      if (obj1.\u0006\u2006)
        this.\u000E(obj1, obj2);
      this.\u000E\u2009.Pop();
      return obj2;
    }

    public object \u0002(PXReflectionSerializer.\u000E\u2009 _param1)
    {
      PXReflectionSerializer.\u0002\u2009.\u0002 obj1 = new PXReflectionSerializer.\u0002\u2009.\u0002();
      obj1.\u0002 = FormatterServices.GetUninitializedObject(_param1.\u0002);
      this.\u000E(_param1, obj1.\u0002);
      if (_param1.\u000F\u2000 != null)
        _param1.\u000F\u2000(obj1.\u0002, this.\u0002\u2009);
      foreach (PXReflectionSerializer.\u0006 obj2 in _param1.\u0002())
      {
        int num = obj2.\u0008.\u000F\u2006 ? 1 : 0;
        PXReflectionSerializer.\u000E\u2009 obj3 = (PXReflectionSerializer.\u000E\u2009) null;
        if (num != 0)
          obj3 = obj2.\u0008;
        object obj4 = this.h(obj3);
        if (!(obj4 is PXReflectionSerializer.\u0003 obj5))
          obj2.\u0002(obj1.\u0002, obj4);
        else
          obj5.\u0002(new Action<object>(new PXReflectionSerializer.\u0002\u2009.\u000E()
          {
            \u000E = obj1,
            \u0002 = obj2
          }.\u0002));
      }
      this.\u0002(_param1, obj1.\u0002);
      return obj1.\u0002;
    }

    public object \u000E(PXReflectionSerializer.\u000E\u2009 _param1)
    {
      object uninitializedObject = FormatterServices.GetUninitializedObject(_param1.\u0002);
      this.\u000E(_param1, uninitializedObject);
      if (_param1.\u000F\u2000 != null)
        _param1.\u000F\u2000(uninitializedObject, this.\u0002\u2009);
      foreach (PXReflectionSerializer.\u0008 obj1 in _param1.\u0002())
      {
        object obj2;
        if (obj1.\u0006)
        {
          obj2 = obj1.\u0008.Invoke((object) this.h, (object[]) null);
          if (obj1.\u000E.\u0005\u2006)
            obj2 = Enum.ToObject(obj1.\u000E.\u0002, obj2);
        }
        else
          obj2 = this.h(obj1.\u000E.\u000F\u2006 ? obj1.\u000E : (PXReflectionSerializer.\u000E\u2009) null);
        if (obj1.\u0002.Length == 1)
        {
          TypedReference typedReference = __makeref (uninitializedObject);
          obj1.\u0002[0].SetValueDirect(typedReference, obj2);
        }
        else
        {
          TypedReference typedReference = TypedReference.MakeTypedReference(uninitializedObject, obj1.\u000F.\u0002);
          obj1.\u0002[obj1.\u0002.Length - 1].SetValueDirect(typedReference, obj2);
        }
      }
      this.\u0002(_param1, uninitializedObject);
      return uninitializedObject;
    }

    public object \u0006(PXReflectionSerializer.\u000E\u2009 _param1)
    {
      return this.h.ReadBoolean() ? (object) null : this.h(PXReflectionSerializer.\u0002(_param1.\u0003\u2006));
    }

    public object \u0008(PXReflectionSerializer.\u000E\u2009 _param1)
    {
      int length = this.h.ReadInt32();
      PXReflectionSerializer.\u000E\u2009 obj1 = PXReflectionSerializer.\u0002(_param1.\u000E\u2000);
      Array instance = Array.CreateInstance(obj1.\u0002, length);
      this.\u000E(_param1, (object) instance);
      if (!obj1.\u000F\u2006)
        obj1 = (PXReflectionSerializer.\u000E\u2009) null;
      for (int index = 0; index < length; ++index)
      {
        object obj2 = this.h(obj1);
        instance.SetValue(obj2, index);
      }
      return (object) instance;
    }

    public object \u0003(PXReflectionSerializer.\u000E\u2009 _param1)
    {
      int length = this.h.ReadInt32();
      PXReflectionSerializer.\u000E\u2009 obj1 = PXReflectionSerializer.\u0002(_param1.\u000E\u2000);
      Array instance = Array.CreateInstance(obj1.\u0002, length);
      if (!obj1.\u000F\u2006)
        obj1 = (PXReflectionSerializer.\u000E\u2009) null;
      for (int index = 0; index < length; ++index)
      {
        object obj2 = this.h(obj1);
        instance.SetValue(obj2, index);
      }
      return (object) instance;
    }

    public object \u000F(PXReflectionSerializer.\u000E\u2009 _param1)
    {
      SerializationInfo serializationInfo = new SerializationInfo(_param1.\u0002, (IFormatterConverter) new FormatterConverter());
      StreamingContext streamingContext = new StreamingContext();
      PXReflectionSerializer.\u0003 obj1 = new PXReflectionSerializer.\u0003()
      {
        \u0006 = _param1
      };
      this.\u000E(_param1, (object) obj1);
      int num = this.h.ReadInt32();
      for (int index = 0; index < num; ++index)
      {
        string name = this.h.ReadString();
        object obj2 = this.h((PXReflectionSerializer.\u000E\u2009) null);
        serializationInfo.AddValue(name, obj2);
      }
      object obj3 = _param1.\u0002\u2006(serializationInfo, streamingContext);
      obj1.\u0002(obj3);
      if (obj3 is IDeserializationCallback deserializationCallback)
        this.\u0003.Add(deserializationCallback);
      return obj3;
    }

    public object \u0005(PXReflectionSerializer.\u000E\u2009 _param1)
    {
      if (this.h.ReadBoolean())
      {
        object instance = Activator.CreateInstance(_param1.\u0002);
        this.\u000E(_param1, instance);
        return instance;
      }
      object obj = PXReflectionSerializer.\u000E\u2009.\u0008\u2009.GetOrAdd(_param1.\u0002, new XmlSerializer(_param1.\u0002)).Deserialize((TextReader) new StringReader(this.h.ReadString()));
      this.\u000E(_param1, obj);
      return obj;
    }

    public MemberInfo \u0002()
    {
      Type type = this.h.ReadTypeString();
      int metadataToken = this.h.ReadInt32();
      MemberInfo memberInfo = type.Module.ResolveMember(metadataToken);
      if (!(memberInfo.DeclaringType != type))
        return memberInfo;
      throw new PXSerializationException("Can't resolve the method.");
    }

    private Delegate \u0002()
    {
      Type type = this.h.ReadTypeString();
      MethodInfo method = (MethodInfo) this.\u0002();
      object firstArgument = this.h((PXReflectionSerializer.\u000E\u2009) null);
      try
      {
        return firstArgument == null ? Delegate.CreateDelegate(type, method) : Delegate.CreateDelegate(type, firstArgument, method);
      }
      catch (Exception ex)
      {
        throw new PXSerializationException(ex.Message);
      }
    }

    public Delegate \u0002(PXReflectionSerializer.\u000E\u2009 _param1)
    {
      PXReflectionSerializer.\u0003 obj = new PXReflectionSerializer.\u0003()
      {
        \u0006 = _param1
      };
      this.\u000E(_param1, (object) obj);
      int length = this.h.ReadInt32();
      Delegate[] delegateArray = new Delegate[length];
      for (int index = 0; index < length; ++index)
        delegateArray[index] = this.\u0002();
      Delegate @delegate = length == 1 ? delegateArray[0] : Delegate.Combine(delegateArray);
      obj.\u0002((object) @delegate);
      return @delegate;
    }

    public void \u0002(PXReflectionSerializer.\u000E\u2009 _param1, object _param2)
    {
      if (_param1.\u0005\u2000 == null)
        return;
      _param1.\u0005\u2000(_param2, this.\u0002\u2009);
    }

    public void \u000E(PXReflectionSerializer.\u000E\u2009 _param1, object _param2)
    {
      if (_param2 == null)
        throw new Exception("result is null");
      if (_param2 is PXSerializationSurrogate serializationSurrogate)
      {
        serializationSurrogate.ForwardItem = (object) new PXReflectionSerializer.\u0003()
        {
          \u0006 = _param1
        };
        _param2 = serializationSurrogate.ForwardItem;
      }
      if (!_param1.\u000E)
        return;
      if (_param2 is IDeserializationCallback deserializationCallback)
        this.\u0003.Add(deserializationCallback);
      this.\u0002[this.\u0008\u2009] = _param2;
      if (_param2 is PXReflectionSerializer.\u0003 obj1)
        obj1.\u0008 = this.\u0008\u2009;
      if (this.\u000F != null)
      {
        object obj2 = this.\u000F.\u0002\u2009[this.\u0008\u2009];
        if (obj1 != null)
        {
          if (!(obj1.\u0006.\u0002 != obj2.GetType()))
            return;
          throw new Exception("distinct type");
        }
        if (obj2.GetType() != _param2.GetType())
          throw new Exception("distinct type");
      }
      ++this.\u0008\u2009;
    }

    private sealed class \u0002
    {
      public object \u0002;
    }

    private sealed class \u000E
    {
      public PXReflectionSerializer.\u0006 \u0002;
      public PXReflectionSerializer.\u0002\u2009.\u0002 \u000E;

      internal void \u0002(object _param1) => this.\u0002.\u0002(this.\u000E.\u0002, _param1);
    }
  }

  private sealed class \u0003
  {
    private Action<object> \u0002;
    private object \u000E;
    public PXReflectionSerializer.\u000E\u2009 \u0006;
    public int \u0008;

    public void \u0002(Action<object> _param1)
    {
      Action<object> action = this.\u0002;
      Action<object> comparand;
      do
      {
        comparand = action;
        action = Interlocked.CompareExchange<Action<object>>(ref this.\u0002, comparand + _param1, comparand);
      }
      while (action != comparand);
    }

    public void \u000E(Action<object> _param1)
    {
      Action<object> action = this.\u0002;
      Action<object> comparand;
      do
      {
        comparand = action;
        action = Interlocked.CompareExchange<Action<object>>(ref this.\u0002, comparand - _param1, comparand);
      }
      while (action != comparand);
    }

    public object \u0002() => this.\u000E;

    public void \u0002(object _param1)
    {
      this.\u000E = _param1;
      if (this.\u0002 == null)
        return;
      this.\u0002(_param1);
    }

    public static object \u0002(object _param0)
    {
      return !(_param0 is PXReflectionSerializer.\u0003 obj) ? _param0 : obj.\u0002();
    }
  }

  private sealed class \u0005 : MemoryStream
  {
    private long \u0002;

    public override bool CanRead => false;

    public override bool CanSeek => false;

    public override bool CanWrite => true;

    public override long Length => this.\u0002;

    public override long Position
    {
      get => this.\u0002;
      set
      {
      }
    }

    public override void Flush()
    {
    }

    public override int Read(byte[] _param1, int _param2, int _param3) => 0;

    public override long Seek(long _param1, SeekOrigin _param2) => 0;

    public override void SetLength(long _param1) => this.\u0002 = _param1;

    public override void Write(byte[] _param1, int _param2, int _param3)
    {
      this.\u0002 += (long) _param3;
    }

    public override void WriteByte(byte _param1) => ++this.\u0002;
  }

  private sealed class \u0006
  {
    public Action<object, object> \u0002;
    public Func<object, object> \u000E;
    public string \u0006;
    public PXReflectionSerializer.\u000E\u2009 \u0008;
    public FieldInfo \u0003;
  }

  private sealed class \u0006\u2009
  {
    public Dictionary<Type, int> \u0002 = new Dictionary<Type, int>();
    public List<PXReflectionSerializer.LogEntry> \u000E;
    public List<PXReflectionSerializer.LogEntry> \u0006 = new List<PXReflectionSerializer.LogEntry>();
    public readonly Dictionary<object, int> \u0008 = new Dictionary<object, int>((IEqualityComparer<object>) new ReflectionSerializer.ObjectComparer<object>());
    private readonly Stream \u0003;
    private readonly BinaryWriterEx \u000F;
    public readonly BinaryWriterEx h;
    private readonly List<MemoryStream> \u0005 = new List<MemoryStream>();
    public ArrayList \u0002\u2009 = new ArrayList();
    public StreamingContext \u000E\u2009;
    private readonly bool \u0006\u2009;
    private int \u0008\u2009;
    internal Stack<PXReflectionSerializer.\u000E\u2009> \u0003\u2009 = new Stack<PXReflectionSerializer.\u000E\u2009>();
    private PXReflectionSerializer.\u0006\u2009.\u0006 \u000F\u2009;
    private Dictionary<short, MemProfilerTypeInfo> \u0005\u2009;
    private readonly Dictionary<PXReflectionSerializer.\u000E\u2009, ushort> \u0002\u2006 = new Dictionary<PXReflectionSerializer.\u000E\u2009, ushort>();

    public \u0006\u2009(bool _param1 = false)
    {
      this.\u0006\u2009 = _param1;
      MemoryStream memoryStream1 = this.\u0006\u2009 ? (MemoryStream) new PXReflectionSerializer.\u0005() : new MemoryStream();
      this.\u0005.Add(memoryStream1);
      this.\u0003 = (Stream) memoryStream1;
      MemoryStream memoryStream2 = this.\u0006\u2009 ? (MemoryStream) new PXReflectionSerializer.\u0005() : new MemoryStream();
      this.\u0005.Add(memoryStream2);
      this.h = new BinaryWriterEx((Stream) memoryStream2);
      MemoryStream memoryStream3 = this.\u0006\u2009 ? (MemoryStream) new PXReflectionSerializer.\u0005() : new MemoryStream();
      this.\u0005.Add(memoryStream3);
      this.\u000F = new BinaryWriterEx((Stream) memoryStream3);
      if (!this.\u0006\u2009)
        return;
      this.\u0002(new Dictionary<short, MemProfilerTypeInfo>());
    }

    public void \u0002(Stream _param1)
    {
      BinaryWriterEx binaryWriterEx = new BinaryWriterEx(_param1);
      List<PXReflectionSerializer.\u000E\u2009> list = EnumerableExtensions.OrderBy<KeyValuePair<PXReflectionSerializer.\u000E\u2009, ushort>, ushort>((IEnumerable<KeyValuePair<PXReflectionSerializer.\u000E\u2009, ushort>>) this.\u0002\u2006, PXReflectionSerializer.\u0006\u2009.\u0002.\u000E ?? (PXReflectionSerializer.\u0006\u2009.\u0002.\u000E = new Func<KeyValuePair<PXReflectionSerializer.\u000E\u2009, ushort>, ushort>(PXReflectionSerializer.\u0006\u2009.\u0002.\u0002.\u0002)), Array.Empty<ushort>()).Select<KeyValuePair<PXReflectionSerializer.\u000E\u2009, ushort>, PXReflectionSerializer.\u000E\u2009>(PXReflectionSerializer.\u0006\u2009.\u0002.\u0006 ?? (PXReflectionSerializer.\u0006\u2009.\u0002.\u0006 = new Func<KeyValuePair<PXReflectionSerializer.\u000E\u2009, ushort>, PXReflectionSerializer.\u000E\u2009>(PXReflectionSerializer.\u0006\u2009.\u0002.\u0002.\u0002))).ToList<PXReflectionSerializer.\u000E\u2009>();
      binaryWriterEx.WriteUInt16((ushort) list.Count);
      foreach (PXReflectionSerializer.\u000E\u2009 obj in list)
      {
        binaryWriterEx.WriteTypeString(obj.\u0002);
        binaryWriterEx.WriteInt32(obj.\u0002());
      }
      ((BinaryWriter) binaryWriterEx).Write(this.\u0008\u2009);
      foreach (MemoryStream memoryStream in this.\u0005)
      {
        int length = (int) memoryStream.Length;
        ((BinaryWriter) binaryWriterEx).Write(length);
        memoryStream.Position = 0L;
        byte[] buffer = memoryStream.GetBuffer();
        ((BinaryWriter) binaryWriterEx).BaseStream.Write(buffer, 0, length);
      }
      binaryWriterEx.WriteByteArray(this.h.BitWriter.Bytes.ToArray());
    }

    public byte[] \u0002()
    {
      SHA1Managed shA1Managed = new SHA1Managed();
      foreach (MemoryStream memoryStream in this.\u0005)
      {
        int length = (int) memoryStream.Length;
        if (length != 0)
        {
          memoryStream.Position = 0L;
          byte[] buffer = memoryStream.GetBuffer();
          shA1Managed.TransformBlock(buffer, 0, length, buffer, 0);
        }
      }
      byte[] array = this.h.BitWriter.Bytes.ToArray();
      shA1Managed.TransformFinalBlock(array, 0, array.Length);
      return shA1Managed.Hash;
    }

    private void \u0002(int _param1) => this.\u000F.WriteInt32(_param1);

    internal Dictionary<short, MemProfilerTypeInfo> \u0002() => this.\u0005\u2009;

    private void \u0002(Dictionary<short, MemProfilerTypeInfo> _param1)
    {
      this.\u0005\u2009 = _param1;
    }

    public void h(
      object _param1,
      PXReflectionSerializer.\u000E\u2009 _param2,
      PXReflectionSerializer.\u000E\u2009 _param3)
    {
      PXReflectionSerializer.\u0006\u2009.\u000E obj = new PXReflectionSerializer.\u0006\u2009.\u000E();
      obj.\u0002 = _param2;
      using (this.\u0006\u2009 ? new PXReflectionSerializer.\u0006\u2009.\u0006(this, new Func<PXReflectionSerializer.\u000E\u2009>(obj.\u0002), (IDictionary<short, MemProfilerTypeInfo>) this.\u0002()) : (PXReflectionSerializer.\u0006\u2009.\u0006) null)
      {
        if (this.\u000E != null)
          this.\u000E.Add(new PXReflectionSerializer.LogEntry()
          {
            Item = _param1,
            StartPos = (int) ((BinaryWriter) this.h).BaseStream.Position
          });
        if ((obj.\u0002 == null ? 1 : (obj.\u0002.\u000E ? 1 : 0)) != 0)
        {
          if (_param1 == null)
          {
            this.\u0002(0);
            return;
          }
          if ((obj.\u0002 == null || !obj.\u0002.\u000E ? (!(_param1 is ValueType) ? 1 : 0) : 1) != 0)
          {
            int num;
            if (this.\u0008.TryGetValue(_param1, out num))
            {
              this.\u0002(num);
              return;
            }
            this.\u0008.Add(_param1, 2 + this.\u0008.Count);
            if (this.\u000E != null)
              this.\u0002\u2009.Add(_param1);
            ++this.\u0008\u2009;
          }
          if (obj.\u0002 == null || obj.\u0002.\u000E\u2007)
          {
            Type type = _param1.GetType();
            obj.\u0002 = _param3 == null || !(_param3.\u0002 == type) ? PXReflectionSerializer.\u0002(type) : _param3;
            if (obj.\u0002.\u000E\u2007)
            {
              obj.\u0002 = obj.\u0002.\u0006\u2007;
              PXSerializationSurrogate instance = (PXSerializationSurrogate) Activator.CreateInstance(obj.\u0002.\u0002);
              instance.SaveObjectData(_param1);
              _param1 = (object) instance;
            }
            ushort count;
            if (!this.\u0002\u2006.TryGetValue(obj.\u0002, out count))
            {
              count = (ushort) this.\u0002\u2006.Count;
              this.\u0002\u2006.Add(obj.\u0002, count);
            }
            this.\u0002(-((int) count + 1));
          }
          else
            this.\u0002(1);
        }
        if (obj.\u0002.\u0006\u2000 != null)
        {
          bool flag = obj.\u0002.\u0006\u2000(_param1);
          ((BinaryWriter) this.h).Write(flag);
          if (!flag)
            return;
        }
        if (_param1 is IPXWeakCollection)
          ((IPXWeakCollection) _param1).Shrink((IDictionary) this.\u0008);
        if (obj.\u0002.\u0002\u2004 == null)
          throw new Exception("Cannot serialize an object of the type: " + obj.\u0002.\u0002.FullName);
        bool flag1 = _param1 is ReaderWriterLock;
        ((BinaryWriter) this.h).Write(flag1);
        if (flag1)
          return;
        this.\u0003\u2009.Push(obj.\u0002);
        obj.\u0002.\u0002\u2004(this, _param1);
        this.\u0003\u2009.Pop();
      }
    }

    public void \u0002(object _param1, PXReflectionSerializer.\u000E\u2009 _param2)
    {
      if (_param2.\u0008\u2000 != null)
        _param2.\u0008\u2000(_param1, this.\u000E\u2009);
      foreach (PXReflectionSerializer.\u0006 obj1 in _param2.\u0002())
      {
        object obj2 = obj1.\u000E(_param1);
        int num = obj1.\u0008.\u000F\u2006 ? 1 : 0;
        PXReflectionSerializer.\u000E\u2009 obj3 = (PXReflectionSerializer.\u000E\u2009) null;
        if (num != 0)
          obj3 = obj1.\u0008;
        this.h(obj2, obj3, obj1.\u0008);
      }
      if (_param2.\u0003\u2000 == null)
        return;
      _param2.\u0003\u2000(_param1, this.\u000E\u2009);
    }

    public void \u000E(object _param1, PXReflectionSerializer.\u000E\u2009 _param2)
    {
      if (_param2.\u0008\u2000 != null)
        _param2.\u0008\u2000(_param1, this.\u000E\u2009);
      foreach (PXReflectionSerializer.\u0008 obj1 in _param2.\u0002())
      {
        object obj2 = _param1;
        foreach (FieldInfo fieldInfo in obj1.\u0002)
          obj2 = fieldInfo.GetValue(obj2);
        if (obj1.\u0006)
        {
          obj1.\u0003.Invoke((object) this.h, new object[1]
          {
            obj2
          });
        }
        else
        {
          PXReflectionSerializer.\u000E\u2009 obj3 = obj1.\u000E.\u000F\u2006 ? obj1.\u000E : (PXReflectionSerializer.\u000E\u2009) null;
          this.h(obj2, obj3, obj1.\u000E);
        }
      }
      if (_param2.\u0003\u2000 == null)
        return;
      _param2.\u0003\u2000(_param1, this.\u000E\u2009);
    }

    public void \u0006(object _param1, PXReflectionSerializer.\u000E\u2009 _param2)
    {
      XmlSerializer orAdd = PXReflectionSerializer.\u000E\u2009.\u0008\u2009.GetOrAdd(_param2.\u0002, new XmlSerializer(_param2.\u0002));
      StringWriter stringWriter1 = new StringWriter();
      StringWriter stringWriter2 = stringWriter1;
      object o = _param1;
      orAdd.Serialize((TextWriter) stringWriter2, o);
      stringWriter1.Close();
      string str = stringWriter1.ToString();
      bool flag = str.EndsWith("/>");
      ((BinaryWriter) this.h).Write(flag);
      if (flag)
        return;
      ((BinaryWriter) this.h).Write(str);
    }

    public void \u0008(object _param1, PXReflectionSerializer.\u000E\u2009 _param2)
    {
      ISerializable serializable = (ISerializable) _param1;
      SerializationInfo serializationInfo = new SerializationInfo(_param2.\u0002, (IFormatterConverter) new FormatterConverter());
      StreamingContext streamingContext = new StreamingContext();
      SerializationInfo info = serializationInfo;
      StreamingContext context = streamingContext;
      serializable.GetObjectData(info, context);
      List<SerializationEntry> serializationEntryList = new List<SerializationEntry>();
      foreach (SerializationEntry serializationEntry in serializationInfo)
        serializationEntryList.Add(serializationEntry);
      this.h.WriteInt32(serializationEntryList.Count);
      foreach (SerializationEntry serializationEntry in serializationEntryList)
      {
        ((BinaryWriter) this.h).Write(serializationEntry.Name);
        this.h(serializationEntry.Value, (PXReflectionSerializer.\u000E\u2009) null, (PXReflectionSerializer.\u000E\u2009) null);
      }
    }

    public void \u0003(object _param1, PXReflectionSerializer.\u000E\u2009 _param2)
    {
      Array array = (Array) _param1;
      this.h.WriteInt32(array.Length);
      Type type = _param2.\u000E\u2000;
      PXReflectionSerializer.\u000E\u2009 obj1 = (PXReflectionSerializer.\u000E\u2009) null;
      PXReflectionSerializer.\u000E\u2009 obj2 = PXReflectionSerializer.\u0002(type);
      if (type.IsSealed)
        obj1 = obj2;
      foreach (object obj3 in array)
        this.h(obj3, obj1, obj2);
    }

    public void \u000F(object _param1, PXReflectionSerializer.\u000E\u2009 _param2)
    {
      bool flag = _param1 == null;
      ((BinaryWriter) this.h).Write(flag);
      if (flag)
        return;
      this.h(_param1, PXReflectionSerializer.\u0002(_param2.\u0003\u2006), (PXReflectionSerializer.\u000E\u2009) null);
    }

    public void \u0002(MemberInfo _param1)
    {
      this.h.WriteTypeString(_param1.DeclaringType);
      this.h.WriteInt32(_param1.MetadataToken);
    }

    private void \u0002(Delegate _param1)
    {
      this.h.WriteTypeString(_param1.GetType());
      this.\u0002((MemberInfo) _param1.Method);
      this.h(_param1.Target, (PXReflectionSerializer.\u000E\u2009) null, (PXReflectionSerializer.\u000E\u2009) null);
    }

    public void \u0002(MulticastDelegate _param1)
    {
      Delegate[] invocationList = _param1.GetInvocationList();
      this.h.WriteInt32(invocationList.Length);
      foreach (Delegate @delegate in invocationList)
        this.\u0002(@delegate);
    }

    [Serializable]
    private sealed class \u0002
    {
      public static readonly PXReflectionSerializer.\u0006\u2009.\u0002 \u0002 = new PXReflectionSerializer.\u0006\u2009.\u0002();
      public static Func<KeyValuePair<PXReflectionSerializer.\u000E\u2009, ushort>, ushort> \u000E;
      public static Func<KeyValuePair<PXReflectionSerializer.\u000E\u2009, ushort>, PXReflectionSerializer.\u000E\u2009> \u0006;

      internal ushort \u0002(
        KeyValuePair<PXReflectionSerializer.\u000E\u2009, ushort> _param1)
      {
        return _param1.Value;
      }

      internal PXReflectionSerializer.\u000E\u2009 \u0002(
        KeyValuePair<PXReflectionSerializer.\u000E\u2009, ushort> _param1)
      {
        return _param1.Key;
      }
    }

    private sealed class \u0006 : IDisposable
    {
      private readonly PXReflectionSerializer.\u0006\u2009 \u0002;
      private readonly Func<PXReflectionSerializer.\u000E\u2009> \u000E;
      private readonly IDictionary<short, MemProfilerTypeInfo> \u0006;
      private readonly PXReflectionSerializer.\u0006\u2009.\u0006 \u0008;
      private long \u0003;
      private long \u000F;
      private long \u0005;

      internal \u0006(
        PXReflectionSerializer.\u0006\u2009 _param1,
        Func<PXReflectionSerializer.\u000E\u2009> _param2,
        IDictionary<short, MemProfilerTypeInfo> _param3)
      {
        this.\u0002 = _param1;
        this.\u000E = _param2;
        this.\u0006 = _param3;
        this.\u0003 = ((BinaryWriter) this.\u0002.h).BaseStream.Position;
        this.\u0008 = this.\u0002.\u000F\u2009;
        this.\u0002.\u000F\u2009 = this;
      }

      public void Dispose()
      {
        this.\u000F = ((BinaryWriter) this.\u0002.h).BaseStream.Position - this.\u0003;
        this.\u0008?.\u0002(this);
        long num = this.\u000F - this.\u0005;
        Func<PXReflectionSerializer.\u000E\u2009> func = this.\u000E;
        PXReflectionSerializer.\u000E\u2009 obj = func != null ? func() : (PXReflectionSerializer.\u000E\u2009) null;
        short key = obj != null ? obj.\u0002() : (short) -1;
        if (key != (short) -1)
        {
          MemProfilerTypeInfo profilerTypeInfo;
          if (this.\u0006.TryGetValue(key, out profilerTypeInfo))
          {
            ((MemProfilerTypeInfo) ref profilerTypeInfo).Append(num, this.\u000F, 1);
            this.\u0006[key] = profilerTypeInfo;
          }
          else
            this.\u0006.Add(key, new MemProfilerTypeInfo(obj.\u0002.ToString(), num, this.\u000F, 1));
        }
        this.\u0002.\u000F\u2009 = this.\u0008;
      }

      private void \u0002(PXReflectionSerializer.\u0006\u2009.\u0006 _param1)
      {
        this.\u0005 += _param1.\u000F;
      }
    }

    private sealed class \u000E
    {
      public PXReflectionSerializer.\u000E\u2009 \u0002;

      internal PXReflectionSerializer.\u000E\u2009 \u0002() => this.\u0002;
    }
  }

  private sealed class \u0008
  {
    public FieldInfo[] \u0002;
    public PXReflectionSerializer.\u000E\u2009 \u000E;
    public bool \u0006;
    public MethodInfo \u0008;
    public MethodInfo \u0003;
    public PXReflectionSerializer.\u0008 \u000F;
    public int \u0005;
  }

  private static class \u000E<\u0002>
  {
    public static Func<\u0002, \u0002> \u0002;
  }

  private sealed class \u000E\u2009
  {
    public readonly Type \u0002;
    public readonly bool \u000E;
    private PXReflectionSerializer.\u0006[] \u0006;
    private int? \u0008;
    private PXReflectionSerializer.\u0008[] \u0003;
    private PXReflectionSerializer.\u0008[] \u000F;
    private int \u0005;
    private static readonly List<PXReflectionSerializer.\u000E\u2009> \u0002\u2009 = new List<PXReflectionSerializer.\u000E\u2009>();
    private static object \u000E\u2009 = new object();
    private short \u0006\u2009;
    internal static ConcurrentDictionary<Type, XmlSerializer> \u0008\u2009 = new ConcurrentDictionary<Type, XmlSerializer>();
    private bool \u0003\u2009;
    public bool \u000F\u2009;
    public bool \u0005\u2009;
    public Func<SerializationInfo, StreamingContext, object> \u0002\u2006;
    public bool \u000E\u2006;
    public bool \u0006\u2006;
    public bool \u0008\u2006;
    public Type \u0003\u2006;
    public bool \u000F\u2006;
    public bool \u0005\u2006;
    public Type \u0002\u2000;
    public readonly Type \u000E\u2000;
    public Func<object, bool> \u0006\u2000;
    public Action<object, StreamingContext> \u0008\u2000;
    public Action<object, StreamingContext> \u0003\u2000;
    public Action<object, StreamingContext> \u000F\u2000;
    public Action<object, StreamingContext> \u0005\u2000;
    public Action<PXReflectionSerializer.\u0006\u2009, object> \u0002\u2004;
    public Func<PXReflectionSerializer.\u0002\u2009, object> \u000E\u2004;
    private Action<PXReflectionSerializer.\u0006\u2009, object> \u0006\u2004;
    private Func<PXReflectionSerializer.\u0002\u2009, object> \u0008\u2004;
    private bool \u0003\u2004;
    private string \u000F\u2004;
    private static readonly List<PXReflectionSerializer.\u0008> \u0005\u2004 = new List<PXReflectionSerializer.\u0008>();
    private Action<PXReflectionSerializer.\u000F, object> \u0002\u2007;
    public bool \u000E\u2007;
    public PXReflectionSerializer.\u000E\u2009 \u0006\u2007;

    public \u000E\u2009(Type _param1)
    {
      PXReflectionSerializer.\u000E\u2009.\u000E obj1 = new PXReflectionSerializer.\u000E\u2009.\u000E();
      obj1.\u000E = _param1;
      // ISSUE: explicit constructor call
      base.\u002Ector();
      obj1.\u0002 = this;
      PXReflectionSerializer.\u000E\u2009.\u0006 obj2 = new PXReflectionSerializer.\u000E\u2009.\u0006();
      obj2.\u000E = obj1;
      this.\u0002 = obj2.\u000E.\u000E;
      this.\u000E = !obj2.\u000E.\u000E.IsValueType;
      this.\u000F\u2006 = obj2.\u000E.\u000E.IsSealed;
      this.\u000E\u2000 = this.\u0002.GetElementType();
      this.\u0003\u2006 = Nullable.GetUnderlyingType(this.\u0002);
      if (this.\u0003\u2006 != (Type) null)
      {
        this.\u0008\u2006 = true;
        this.\u000E\u2004 = new Func<PXReflectionSerializer.\u0002\u2009, object>(obj2.\u000E.\u0002);
        this.\u0002\u2004 = new Action<PXReflectionSerializer.\u0006\u2009, object>(obj2.\u000E.\u0002);
      }
      else
      {
        this.\u0005\u2006 = typeof (Enum).IsAssignableFrom(this.\u0002);
        if (this.\u0005\u2006)
          this.\u0002\u2000 = Enum.GetUnderlyingType(this.\u0002);
        obj2.\u0002 = typeof (BinaryWriterEx).GetMethod("Write", BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding, (Binder) null, new Type[1]
        {
          obj2.\u000E.\u000E
        }, (ParameterModifier[]) null);
        this.\u0006\u2006 = obj2.\u0002 != (MethodInfo) null;
        if (this.\u0006\u2006)
        {
          PXReflectionSerializer.\u000E\u2009.\u0008 obj3 = new PXReflectionSerializer.\u000E\u2009.\u0008();
          if (obj2.\u000E.\u000E == typeof (char[]))
          {
            this.\u0002\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u000F ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u000F = new Action<PXReflectionSerializer.\u0006\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0002));
            this.\u000E\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u0005 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0005 = new Func<PXReflectionSerializer.\u0002\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0002));
          }
          else if (obj2.\u000E.\u000E == typeof (byte[]))
          {
            this.\u0002\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u0002\u2009 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0002\u2009 = new Action<PXReflectionSerializer.\u0006\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u000E));
            this.\u000E\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u000E\u2009 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u000E\u2009 = new Func<PXReflectionSerializer.\u0002\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u000E));
          }
          else if (obj2.\u000E.\u000E == typeof (string))
          {
            this.\u0002\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u0006\u2009 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0006\u2009 = new Action<PXReflectionSerializer.\u0006\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0006));
            this.\u000E\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u0008\u2009 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0008\u2009 = new Func<PXReflectionSerializer.\u0002\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0006));
          }
          else
          {
            obj3.\u0002 = ((IEnumerable<MethodInfo>) typeof (BinaryReaderEx).GetMethods()).FirstOrDefault<MethodInfo>(new Func<MethodInfo, bool>(obj2.\u000E.\u0002));
            this.\u000E\u2004 = !(obj3.\u0002 == (MethodInfo) null) ? new Func<PXReflectionSerializer.\u0002\u2009, object>(obj3.\u0002) : throw new Exception("Can't find the read method " + obj2.\u000E.\u000E.FullName);
            this.\u0002\u2004 = new Action<PXReflectionSerializer.\u0006\u2009, object>(obj2.\u0002);
          }
        }
        else if (typeof (Type).IsAssignableFrom(this.\u0002))
        {
          this.\u0006\u2006 = true;
          this.\u000E\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u0003\u2009 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0003\u2009 = new Func<PXReflectionSerializer.\u0002\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0008));
          this.\u0002\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u000F\u2009 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u000F\u2009 = new Action<PXReflectionSerializer.\u0006\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0008));
        }
        else if (typeof (MemberInfo).IsAssignableFrom(this.\u0002))
        {
          this.\u0006\u2006 = true;
          this.\u000E\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u0005\u2009 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0005\u2009 = new Func<PXReflectionSerializer.\u0002\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0003));
          this.\u0002\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u0002\u2006 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0002\u2006 = new Action<PXReflectionSerializer.\u0006\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0003));
        }
        else if (this.\u0002 == typeof (DBNull))
        {
          this.\u0006\u2006 = true;
          this.\u000E\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u000E\u2006 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u000E\u2006 = new Func<PXReflectionSerializer.\u0002\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u000F));
          this.\u0002\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u0006\u2006 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0006\u2006 = new Action<PXReflectionSerializer.\u0006\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u000F));
        }
        else if (typeof (MulticastDelegate).IsAssignableFrom(this.\u0002))
        {
          this.\u0003\u2004 = true;
          this.\u0008\u2006 = true;
          this.\u000E\u2004 = new Func<PXReflectionSerializer.\u0002\u2009, object>(obj2.\u000E.\u000E);
          this.\u0002\u2004 = PXReflectionSerializer.\u000E\u2009.\u0002.\u0008\u2006 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0008\u2006 = new Action<PXReflectionSerializer.\u0006\u2009, object>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0005));
        }
        else
        {
          this.\u000E\u2006 = obj2.\u000E.\u000E.IsArray;
          if (this.\u000E\u2006)
          {
            this.\u000E\u2004 = new Func<PXReflectionSerializer.\u0002\u2009, object>(obj2.\u000E.\u0006);
            this.\u0002\u2004 = new Action<PXReflectionSerializer.\u0006\u2009, object>(obj2.\u000E.\u000E);
          }
          else
          {
            PXReflectionSerializer.\u000E\u2009.\u0003 obj4 = new PXReflectionSerializer.\u000E\u2009.\u0003();
            obj4.\u0002 = this.\u0002.GetMethod("ShouldSerialize", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (obj4.\u0002 != (MethodInfo) null && obj4.\u0002.GetParameters().Length == 0)
              this.\u0006\u2000 = new Func<object, bool>(obj4.\u0002);
            this.\u000F\u2009 = typeof (IXmlSerializable).IsAssignableFrom(this.\u0002) && !PXSurrogateSelector.BaseIgnoreSerializable(this.\u0002);
            if (this.\u000F\u2009)
            {
              this.\u000E\u2004 = new Func<PXReflectionSerializer.\u0002\u2009, object>(obj2.\u000E.\u0008);
              this.\u0002\u2004 = new Action<PXReflectionSerializer.\u0006\u2009, object>(obj2.\u000E.\u0006);
            }
            else
            {
              PXSerializationSurrogateAttribute customAttribute = (PXSerializationSurrogateAttribute) Attribute.GetCustomAttribute((MemberInfo) this.\u0002, typeof (PXSerializationSurrogateAttribute));
              this.\u000E\u2007 = customAttribute != null;
              if (this.\u000E\u2007)
              {
                this.\u0006\u2007 = PXReflectionSerializer.\u0002(customAttribute.SurrogateType);
              }
              else
              {
                this.\u0005\u2009 = typeof (ISerializable).IsAssignableFrom(this.\u0002);
                if (this.\u0005\u2009)
                {
                  PXReflectionSerializer.\u000E\u2009.\u000F obj5 = new PXReflectionSerializer.\u000E\u2009.\u000F();
                  obj5.\u000E = obj2;
                  this.\u0003\u2004 = true;
                  this.\u000E\u2004 = new Func<PXReflectionSerializer.\u0002\u2009, object>(obj5.\u000E.\u000E.\u0003);
                  obj5.\u0002 = obj5.\u000E.\u000E.\u000E.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, new Type[2]
                  {
                    typeof (SerializationInfo),
                    typeof (StreamingContext)
                  }, (ParameterModifier[]) null);
                  bool flag = false;
                  if (obj5.\u0002 == (ConstructorInfo) null)
                  {
                    for (Type baseType = obj5.\u000E.\u000E.\u000E.BaseType; baseType != (Type) null && baseType != typeof (object); baseType = baseType.BaseType)
                    {
                      if (baseType.FullName.Equals("System.Exception"))
                      {
                        flag = true;
                        break;
                      }
                    }
                    if (flag)
                    {
                      for (Type baseType = obj5.\u000E.\u000E.\u000E.BaseType; baseType != (Type) null && baseType != typeof (object); baseType = baseType.BaseType)
                      {
                        obj5.\u0002 = baseType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, new Type[2]
                        {
                          typeof (SerializationInfo),
                          typeof (StreamingContext)
                        }, (ParameterModifier[]) null);
                        if (obj5.\u0002 != (ConstructorInfo) null)
                          break;
                      }
                    }
                  }
                  if (obj5.\u0002 == (ConstructorInfo) null)
                  {
                    this.\u0002\u2004 = new Action<PXReflectionSerializer.\u0006\u2009, object>(obj5.\u000E.\u000E.\u0008);
                  }
                  else
                  {
                    this.\u0002\u2006 = !flag ? new Func<SerializationInfo, StreamingContext, object>(obj5.\u000E) : new Func<SerializationInfo, StreamingContext, object>(obj5.\u0002);
                    this.\u0002\u2004 = new Action<PXReflectionSerializer.\u0006\u2009, object>(obj5.\u000E.\u000E.\u0003);
                  }
                }
                else
                {
                  Type type1;
                  string name;
                  if (PXReflectionSerializer.\u000E\u2009.\u0002(this.\u0002, out type1, out name))
                  {
                    PXReflectionSerializer.\u000E\u2009.\u0005 obj6 = new PXReflectionSerializer.\u000E\u2009.\u0005();
                    obj6.\u000E = obj2;
                    Type genericTypeArgument = this.\u0002.GenericTypeArguments[0];
                    this.\u000E\u2000 = genericTypeArgument;
                    Type type2 = (Type) null;
                    if (this.\u0002.GenericTypeArguments.Length > 1)
                    {
                      type2 = this.\u0002.GenericTypeArguments[1];
                      this.\u000E\u2000 = typeof (KeyValuePair<,>).MakeGenericType(genericTypeArgument, type2);
                    }
                    MethodInfo methodInfo = ((IEnumerable<MethodInfo>) type1.Assembly.GetType(name).GetMethods()).FirstOrDefault<MethodInfo>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0003\u2006 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0003\u2006 = new Func<MethodInfo, bool>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0002)));
                    if (type2 == (Type) null)
                      obj6.\u0002 = methodInfo.MakeGenericMethod(genericTypeArgument);
                    else
                      obj6.\u0002 = methodInfo.MakeGenericMethod(genericTypeArgument, type2);
                    this.\u000E\u2004 = new Func<PXReflectionSerializer.\u0002\u2009, object>(obj6.\u0002);
                    this.\u0002\u2004 = new Action<PXReflectionSerializer.\u0006\u2009, object>(obj6.\u000E.\u000E.\u000F);
                  }
                  else
                  {
                    this.\u0008\u2006 = true;
                    bool flag = this.\u0002.IsInterface || obj2.\u000E.\u000E.IsAbstract;
                    if (!this.\u0008\u2006 || flag)
                      return;
                    List<Action<object, StreamingContext>> actionList1 = new List<Action<object, StreamingContext>>();
                    List<Action<object, StreamingContext>> actionList2 = new List<Action<object, StreamingContext>>();
                    List<Action<object, StreamingContext>> actionList3 = new List<Action<object, StreamingContext>>();
                    List<Action<object, StreamingContext>> actionList4 = new List<Action<object, StreamingContext>>();
                    for (Type baseType = obj2.\u000E.\u000E; baseType != (Type) null; baseType = baseType.BaseType)
                    {
                      foreach (MethodInfo method in baseType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                      {
                        PXReflectionSerializer.\u000E\u2009.\u0002\u2009 obj7 = new PXReflectionSerializer.\u000E\u2009.\u0002\u2009();
                        obj7.\u0002 = method;
                        if (Attribute.IsDefined((MemberInfo) method, typeof (OnSerializingAttribute)))
                          actionList1.Add(new Action<object, StreamingContext>(obj7.\u0002));
                        if (Attribute.IsDefined((MemberInfo) method, typeof (OnSerializedAttribute)))
                          actionList2.Add(new Action<object, StreamingContext>(obj7.\u000E));
                        if (Attribute.IsDefined((MemberInfo) method, typeof (OnDeserializingAttribute)))
                          actionList3.Add(new Action<object, StreamingContext>(obj7.\u0006));
                        if (Attribute.IsDefined((MemberInfo) method, typeof (OnDeserializedAttribute)))
                          actionList4.Add(new Action<object, StreamingContext>(obj7.\u0008));
                      }
                    }
                    for (int index = actionList1.Count - 1; index >= 0; --index)
                      this.\u0008\u2000 += actionList1[index];
                    for (int index = actionList2.Count - 1; index >= 0; --index)
                      this.\u0003\u2000 += actionList2[index];
                    for (int index = actionList3.Count - 1; index >= 0; --index)
                      this.\u000F\u2000 += actionList3[index];
                    for (int index = actionList4.Count - 1; index >= 0; --index)
                      this.\u0005\u2000 += actionList4[index];
                    this.\u0003\u2009 = this.\u0002.IsValueType && !this.\u0002.IsEnum;
                    this.\u000E\u2004 = !this.\u0002.IsValueType || this.\u0003\u2009 ? this.\u0002() : new Func<PXReflectionSerializer.\u0002\u2009, object>(obj2.\u000E.\u000F);
                    this.\u0002\u2004 = new Action<PXReflectionSerializer.\u0006\u2009, object>(obj2.\u000E.\u0005);
                  }
                }
              }
            }
          }
        }
      }
    }

    public PXReflectionSerializer.\u0006[] \u0002()
    {
      if (this.\u0006 == null)
      {
        if (this.\u0002.IsAbstract || this.\u0002.IsInterface)
          return (PXReflectionSerializer.\u0006[]) null;
        this.\u0006 = PXReflectionSerializer.\u000E\u2009.\u0002(this.\u0002).ToArray();
      }
      return this.\u0006;
    }

    public int \u0002()
    {
      if (!this.\u0008.HasValue)
      {
        int num = 17;
        foreach (PXReflectionSerializer.\u0008 obj in this.\u0002())
          num = num * 23 + PXReflectionSerializer.GetStableHash(obj.\u000E.\u0002.FullName);
        if (this.\u0002.Name == "NVPair" && num != -1149573453)
          throw new Exception($"Invalid hash for NVPair: {num} {string.Join(",", ((IEnumerable<PXReflectionSerializer.\u0008>) this.\u0002()).Select<PXReflectionSerializer.\u0008, string>(PXReflectionSerializer.\u000E\u2009.\u0002.\u000E ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u000E = new Func<PXReflectionSerializer.\u0008, string>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0002))))}");
        this.\u0008 = new int?(num);
      }
      return this.\u0008.Value;
    }

    public PXReflectionSerializer.\u0008[] \u0002()
    {
      if (this.\u0003 == null)
      {
        if (this.\u0002.IsAbstract || this.\u0002.IsInterface)
          return (PXReflectionSerializer.\u0008[]) null;
        List<PXReflectionSerializer.\u0008> objList = new List<PXReflectionSerializer.\u0008>();
        PXReflectionSerializer.\u000E\u2009.\u0002(objList, new PXReflectionSerializer.\u0008()
        {
          \u0002 = new FieldInfo[0],
          \u000E = this
        });
        for (int index = 0; index < objList.Count; ++index)
          objList[index].\u0005 = index;
        this.\u0003 = Enumerable.ToArray<PXReflectionSerializer.\u0008>(EnumerableExtensions.OrderBy<PXReflectionSerializer.\u0008, bool>((IEnumerable<PXReflectionSerializer.\u0008>) objList, PXReflectionSerializer.\u000E\u2009.\u0002.\u0006 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0006 = new Func<PXReflectionSerializer.\u0008, bool>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0002)), Array.Empty<bool>()).ThenBy<PXReflectionSerializer.\u0008, string>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0008 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0008 = new Func<PXReflectionSerializer.\u0008, string>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u000E))).ThenBy<PXReflectionSerializer.\u0008, int>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0003 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u0003 = new Func<PXReflectionSerializer.\u0008, int>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0002))));
      }
      return this.\u0003;
    }

    private PXReflectionSerializer.\u0008[] \u000E()
    {
      if (this.\u000F == null)
      {
        List<PXReflectionSerializer.\u0008> objList = new List<PXReflectionSerializer.\u0008>();
        this.\u0002(objList, new PXReflectionSerializer.\u0008()
        {
          \u0002 = new FieldInfo[0],
          \u000E = this
        });
        this.\u000F = objList.ToArray();
      }
      return this.\u000F;
    }

    public int \u000E()
    {
      if (this.\u0005 < 0)
      {
        if (this.\u0002.IsArray)
        {
          this.\u0005 = 0;
        }
        else
        {
          try
          {
            this.\u0005 = Marshal.SizeOf(this.\u0002);
          }
          catch
          {
            this.\u0005 = Marshal.ReadInt32(this.\u0002.TypeHandle.Value, 4);
          }
        }
      }
      return this.\u0005;
    }

    private void \u0002(
      List<PXReflectionSerializer.\u0008> _param1,
      PXReflectionSerializer.\u0008 _param2)
    {
      Type type1 = _param2.\u000E.\u0002;
      if (type1.IsInterface || type1.IsPrimitive)
        return;
      for (Type type2 = type1; type2 != typeof (object); type2 = type2.BaseType)
      {
        foreach (FieldInfo field in type2.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
          if (!field.FieldType.IsPrimitive)
          {
            PXReflectionSerializer.\u0008 obj = new PXReflectionSerializer.\u0008()
            {
              \u000F = _param2,
              \u0002 = Enumerable.ToArray<FieldInfo>(((IEnumerable<FieldInfo>) _param2.\u0002).Concat<FieldInfo>((IEnumerable<FieldInfo>) new FieldInfo[1]
              {
                field
              })),
              \u000E = PXReflectionSerializer.\u0002(field.FieldType)
            };
            if (field.FieldType.IsValueType)
              this.\u0002(_param1, obj);
            else
              _param1.Add(obj);
          }
        }
      }
    }

    private static PXReflectionSerializer.\u000E\u2009 h(short _param0)
    {
      return PXReflectionSerializer.\u000E\u2009.\u0002\u2009[(int) _param0];
    }

    public short \u0002()
    {
      if (this.\u0006\u2009 == (short) -1)
      {
        lock (PXReflectionSerializer.\u000E\u2009.\u000E\u2009)
        {
          if (this.\u0006\u2009 == (short) -1)
          {
            this.\u0006\u2009 = (short) PXReflectionSerializer.\u000E\u2009.\u0002\u2009.Count;
            PXReflectionSerializer.\u000E\u2009.\u0002\u2009.Add(this);
          }
        }
      }
      return this.\u0006\u2009;
    }

    public bool \u0002() => this.\u0003\u2006 != (Type) null;

    public static bool \u0002(Type _param0, out Type _param1, out string _param2)
    {
      _param1 = (Type) null;
      _param2 = (string) null;
      if (!_param0.IsGenericType)
        return false;
      _param1 = _param0.GetGenericTypeDefinition();
      string fullName = _param1.FullName;
      if (fullName != null)
      {
        switch (fullName.Length)
        {
          case 44:
            switch (fullName[30])
            {
              case 'I':
                if (fullName == "System.Collections.Immutable.IImmutableSet`1")
                  goto label_25;
                goto label_28;
              case 'm':
                if (fullName == "System.Collections.Immutable.ImmutableList`1")
                  break;
                goto label_28;
              default:
                goto label_28;
            }
            break;
          case 45:
            switch (fullName[38])
            {
              case 'A':
                if (fullName == "System.Collections.Immutable.ImmutableArray`1")
                {
                  _param2 = "System.Collections.Immutable.ImmutableArray";
                  goto label_29;
                }
                goto label_28;
              case 'Q':
                if (fullName == "System.Collections.Immutable.ImmutableQueue`1")
                  goto label_23;
                goto label_28;
              case 'S':
                if (fullName == "System.Collections.Immutable.ImmutableStack`1")
                  goto label_22;
                goto label_28;
              case 'e':
                if (fullName == "System.Collections.Immutable.IImmutableList`1")
                  break;
                goto label_28;
              default:
                goto label_28;
            }
            break;
          case 46:
            switch (fullName[39])
            {
              case 'Q':
                if (fullName == "System.Collections.Immutable.IImmutableQueue`1")
                  goto label_23;
                goto label_28;
              case 'S':
                if (fullName == "System.Collections.Immutable.IImmutableStack`1")
                  goto label_22;
                goto label_28;
              default:
                goto label_28;
            }
          case 47:
            if (fullName == "System.Collections.Immutable.ImmutableHashSet`1")
              goto label_25;
            goto label_28;
          case 49:
            if (fullName == "System.Collections.Immutable.ImmutableSortedSet`1")
            {
              _param2 = "System.Collections.Immutable.ImmutableSortedSet";
              goto label_29;
            }
            goto label_28;
          case 50:
            if (fullName == "System.Collections.Immutable.ImmutableDictionary`2")
              goto label_26;
            goto label_28;
          case 51:
            if (fullName == "System.Collections.Immutable.IImmutableDictionary`2")
              goto label_26;
            goto label_28;
          case 56:
            if (fullName == "System.Collections.Immutable.ImmutableSortedDictionary`2")
            {
              _param2 = "System.Collections.Immutable.ImmutableSortedDictionary";
              goto label_29;
            }
            goto label_28;
          default:
            goto label_28;
        }
        _param2 = "System.Collections.Immutable.ImmutableList";
        goto label_29;
label_22:
        _param2 = "System.Collections.Immutable.ImmutableStack";
        goto label_29;
label_23:
        _param2 = "System.Collections.Immutable.ImmutableQueue";
        goto label_29;
label_25:
        _param2 = "System.Collections.Immutable.ImmutableHashSet";
        goto label_29;
label_26:
        _param2 = "System.Collections.Immutable.ImmutableDictionary";
label_29:
        return true;
      }
label_28:
      return false;
    }

    private static List<PXReflectionSerializer.\u0006> \u0002(Type _param0)
    {
      List<PXReflectionSerializer.\u0006> objList = new List<PXReflectionSerializer.\u0006>();
      bool flag = PXSurrogateSelector.BaseSerializable(_param0);
      for (Type type = _param0; type != typeof (object) && (!flag || PXSurrogateSelector.BaseSerializable(type)); type = type.BaseType)
      {
        foreach (FieldInfo fieldInfo in Enumerable.ToArray<FieldInfo>(EnumerableExtensions.OrderBy<FieldInfo, string>((IEnumerable<FieldInfo>) type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic), PXReflectionSerializer.\u000E\u2009.\u0002.\u000F\u2006 ?? (PXReflectionSerializer.\u000E\u2009.\u0002.\u000F\u2006 = new Func<FieldInfo, string>(PXReflectionSerializer.\u000E\u2009.\u0002.\u0002.\u0002)), Array.Empty<string>())))
        {
          PXReflectionSerializer.\u000E\u2009.\u000E\u2009 obj1 = new PXReflectionSerializer.\u000E\u2009.\u000E\u2009();
          if (!fieldInfo.IsNotSerialized)
          {
            obj1.\u0002 = fieldInfo;
            PXReflectionSerializer.\u0006 obj2 = new PXReflectionSerializer.\u0006()
            {
              \u0006 = fieldInfo.Name,
              \u0008 = PXReflectionSerializer.\u0002(fieldInfo.FieldType),
              \u0003 = fieldInfo,
              \u000E = new Func<object, object>(obj1.\u0002),
              \u0002 = new Action<object, object>(obj1.\u0002)
            };
            objList.Add(obj2);
          }
        }
      }
      return objList;
    }

    private static void \u0002(
      List<PXReflectionSerializer.\u0008> _param0,
      PXReflectionSerializer.\u0008 _param1)
    {
      foreach (PXReflectionSerializer.\u0006 obj1 in _param1.\u000E.\u0002())
      {
        Type type = obj1.\u0008.\u0002;
        PXReflectionSerializer.\u0008 obj2 = new PXReflectionSerializer.\u0008()
        {
          \u000E = obj1.\u0008,
          \u0002 = Enumerable.ToArray<FieldInfo>(((IEnumerable<FieldInfo>) _param1.\u0002).Concat<FieldInfo>((IEnumerable<FieldInfo>) new FieldInfo[1]
          {
            obj1.\u0003
          })),
          \u000F = _param1
        };
        PXReflectionSerializer.\u000E\u2009.\u0002(obj2);
        if ((!type.IsValueType || obj2.\u0006 ? 1 : (obj2.\u000E.\u0002() ? 1 : 0)) != 0)
        {
          foreach (FieldInfo fieldInfo in _param1.\u0002)
          {
            if (fieldInfo.IsInitOnly)
              throw new Exception($"initonly {fieldInfo.DeclaringType?.ToString()}::{fieldInfo.Name}");
          }
          _param0.Add(obj2);
        }
        else
          PXReflectionSerializer.\u000E\u2009.\u0002(_param0, obj2);
      }
    }

    private static void \u0002(PXReflectionSerializer.\u0008 _param0)
    {
      Type type1 = _param0.\u000E.\u0002;
      if (!type1.IsValueType)
        return;
      if (_param0.\u000E.\u0002())
      {
        _param0.\u0008 = typeof (BinaryReaderEx).GetMethod($"Read{_param0.\u000E.\u0003\u2006.Name}Nullable");
        if (!(_param0.\u0008 != (MethodInfo) null))
          return;
        _param0.\u0006 = true;
        _param0.\u0003 = typeof (BinaryWriterEx).GetMethod("WriteNullable", BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding, (Binder) null, new Type[1]
        {
          type1
        }, (ParameterModifier[]) null);
        if (_param0.\u0003 == (MethodInfo) null)
          throw new Exception("No Writer nullable" + type1.FullName);
      }
      else if (_param0.\u000E.\u0005\u2006)
      {
        Type type2 = _param0.\u000E.\u0002\u2000;
        _param0.\u0008 = typeof (BinaryReaderEx).GetMethod("Read" + type2.Name);
        if (!(_param0.\u0008 != (MethodInfo) null))
          return;
        _param0.\u0006 = true;
        _param0.\u0003 = typeof (BinaryWriterEx).GetMethod("Write", BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding, (Binder) null, new Type[1]
        {
          type2
        }, (ParameterModifier[]) null);
        if (_param0.\u0003 == (MethodInfo) null)
          throw new Exception("No Writer enum" + type2.FullName);
      }
      else
      {
        _param0.\u0008 = typeof (BinaryReaderEx).GetMethod("Read" + type1.Name);
        if (!(_param0.\u0008 != (MethodInfo) null))
          return;
        _param0.\u0006 = true;
        _param0.\u0003 = typeof (BinaryWriterEx).GetMethod("Write", BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding, (Binder) null, new Type[1]
        {
          type1
        }, (ParameterModifier[]) null);
        if (_param0.\u0003 == (MethodInfo) null)
          throw new Exception("No Writer" + type1.FullName);
      }
    }

    private static object h(object _param0, int _param1, object _param2)
    {
      PXReflectionSerializer.\u0008 obj = PXReflectionSerializer.\u000E\u2009.\u0005\u2004[_param1];
      FieldInfo[] source = obj.\u0002;
      if (source.Length == 1)
      {
        TypedReference typedReference = __makeref (_param0);
        source[0].SetValueDirect(typedReference, _param2);
        return _param0;
      }
      ((IEnumerable<FieldInfo>) source).Last<FieldInfo>().SetValueDirect(TypedReference.MakeTypedReference(_param0, obj.\u000F.\u0002), _param2);
      return _param0;
    }

    private static void h(object _param0, Action<object> _param1)
    {
      if (!(_param0 is PXReflectionSerializer.\u0003 obj))
        _param1(_param0);
      else
        obj.\u0002(_param1);
    }

    private Func<PXReflectionSerializer.\u0002\u2009, object> \u0002()
    {
      return new Func<PXReflectionSerializer.\u0002\u2009, object>(this.\u0002);
    }

    private Func<PXReflectionSerializer.\u0002\u2009, object> \u000E()
    {
      PXReflectionSerializer.\u000E\u2009.\u0006\u2009 obj1 = new PXReflectionSerializer.\u000E\u2009.\u0006\u2009();
      obj1.\u0002 = this;
      PXReflectionSerializer.\u0008[] objArray = this.\u0002();
      ParameterExpression instance = Expression.Parameter(typeof (PXReflectionSerializer.\u0002\u2009), "reader");
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof (object), "item");
      ParameterExpression left = Expression.Variable(this.\u0002, "converted");
      ParameterExpression parameterExpression2 = Expression.Variable(typeof (object), "next");
      List<Expression> expressionList = new List<Expression>();
      expressionList.Add((Expression) Expression.Assign((Expression) left, (Expression) Expression.Convert((Expression) parameterExpression1, this.\u0002)));
      foreach (PXReflectionSerializer.\u0008 obj2 in objArray)
      {
        Expression expression1 = (Expression) left;
        foreach (FieldInfo field in obj2.\u0002)
          expression1 = (Expression) Expression.Field(expression1, field);
        Expression expression2;
        if (obj2.\u0006)
        {
          MethodInfo method = obj2.\u0008;
          expression2 = (Expression) Expression.Call((Expression) Expression.Field((Expression) instance, PXReflectionSerializer.\u000E<PXReflectionSerializer.\u0002\u2009>((Expression<Func<PXReflectionSerializer.\u0002\u2009, object>>) (r => r.h))), method);
          if (obj2.\u000E.\u0005\u2006)
            expression2 = (Expression) Expression.Convert(expression2, obj2.\u000E.\u0002);
        }
        else
        {
          MethodInfo method = PXReflectionSerializer.\u0002<PXReflectionSerializer.\u0002\u2009>((Expression<Action<PXReflectionSerializer.\u0002\u2009>>) (r => r.h(default (PXReflectionSerializer.\u000E\u2009))));
          if (obj2.\u000E.\u000F\u2006)
          {
            short num = obj2.\u000E.\u0002();
            MethodCallExpression methodCallExpression = Expression.Call(PXReflectionSerializer.\u0002<object>((Expression<Action<object>>) (_ => PXReflectionSerializer.\u000E\u2009.h((short) 0))), (Expression) Expression.Constant((object) num));
            expression2 = (Expression) Expression.Call((Expression) instance, method, (Expression) methodCallExpression);
          }
          else
            expression2 = (Expression) Expression.Call((Expression) instance, method, (Expression) Expression.Constant((object) null, typeof (PXReflectionSerializer.\u000E\u2009)));
        }
        if (((IEnumerable<FieldInfo>) obj2.\u0002).Last<FieldInfo>().IsInitOnly)
        {
          int count;
          lock (((ICollection) PXReflectionSerializer.\u000E\u2009.\u0005\u2004).SyncRoot)
          {
            count = PXReflectionSerializer.\u000E\u2009.\u0005\u2004.Count;
            PXReflectionSerializer.\u000E\u2009.\u0005\u2004.Add(obj2);
          }
          MethodInfo method = PXReflectionSerializer.\u0002<object>((Expression<Action<object>>) (_ => PXReflectionSerializer.\u000E\u2009.h(default (object), 0, default (object))));
          if (this.\u0003\u2009)
            expressionList.Add((Expression) Expression.Assign((Expression) left, (Expression) Expression.Convert((Expression) Expression.Call(method, (Expression) Expression.Convert((Expression) left, typeof (object)), (Expression) Expression.Constant((object) count), (Expression) Expression.Convert(expression2, typeof (object))), this.\u0002)));
          else
            expressionList.Add((Expression) Expression.Call(method, (Expression) Expression.Convert((Expression) left, typeof (object)), (Expression) Expression.Constant((object) count), (Expression) Expression.Convert(expression2, typeof (object))));
        }
        else if (obj2.\u000E.\u0003\u2004)
        {
          MethodInfo method = PXReflectionSerializer.\u0002<object>((Expression<Action<object>>) (_ => PXReflectionSerializer.\u000E\u2009.h(default (object), default (Action<object>))));
          expressionList.Add((Expression) Expression.Call(method, expression2, (Expression) (obj => Expression.Assign(expression1, (Expression) Expression.Convert(obj, obj2.\u000E.\u0002)))));
        }
        else
          expressionList.Add((Expression) Expression.Assign(expression1, (Expression) Expression.Convert(expression2, obj2.\u000E.\u0002)));
      }
      expressionList.Add((Expression) Expression.Convert((Expression) left, typeof (object)));
      BlockExpression body = Expression.Block((IEnumerable<ParameterExpression>) new ParameterExpression[2]
      {
        left,
        parameterExpression2
      }, (IEnumerable<Expression>) expressionList);
      obj1.\u000E = Expression.Lambda<Func<PXReflectionSerializer.\u0002\u2009, object, object>>((Expression) body, instance, parameterExpression1).Compile();
      this.\u000F\u2004 = body.ToString();
      return new Func<PXReflectionSerializer.\u0002\u2009, object>(obj1.\u0002);
    }

    public static MethodInfo \u0002(Type _param0)
    {
      return typeof (BinaryReader).GetMethod("Read" + _param0.Name);
    }

    public static MethodInfo \u000E(Type _param0)
    {
      return typeof (BinaryWriter).GetMethod("Write", BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding, (Binder) null, new Type[1]
      {
        _param0
      }, (ParameterModifier[]) null);
    }

    public Action<PXReflectionSerializer.\u000F, object> \u0002()
    {
      if (this.\u0002\u2007 == null)
        this.\u0002\u2007 = this.\u000E();
      return this.\u0002\u2007;
    }

    private Action<PXReflectionSerializer.\u000F, object> \u000E()
    {
      PXReflectionSerializer.\u0008[] objArray = this.\u000E();
      ParameterExpression instance = Expression.Parameter(typeof (PXReflectionSerializer.\u000F), "writer");
      ParameterExpression parameterExpression = Expression.Parameter(typeof (object), "item");
      ParameterExpression left = Expression.Variable(this.\u0002, "converted");
      List<Expression> expressionList = new List<Expression>();
      expressionList.Add((Expression) Expression.Assign((Expression) left, (Expression) Expression.Convert((Expression) parameterExpression, this.\u0002)));
      foreach (PXReflectionSerializer.\u0008 obj in objArray)
      {
        Expression expression1 = (Expression) left;
        foreach (FieldInfo field in obj.\u0002)
          expression1 = (Expression) Expression.Field(expression1, field);
        MethodInfo method = PXReflectionSerializer.\u0002<PXReflectionSerializer.\u000F>((Expression<Action<PXReflectionSerializer.\u000F>>) (_ => _.h(default (object), default (PXReflectionSerializer.\u000E\u2009))));
        short num = obj.\u000E.\u0002();
        MethodCallExpression methodCallExpression = Expression.Call(PXReflectionSerializer.\u0002<object>((Expression<Action<object>>) (_ => PXReflectionSerializer.\u000E\u2009.h((short) 0))), (Expression) Expression.Constant((object) num));
        Expression expression2 = !obj.\u000E.\u000F\u2006 ? (Expression) Expression.Call((Expression) instance, method, (Expression) Expression.Convert(expression1, typeof (object)), (Expression) Expression.Constant((object) null, typeof (PXReflectionSerializer.\u000E\u2009))) : (Expression) Expression.Call((Expression) instance, method, (Expression) Expression.Convert(expression1, typeof (object)), (Expression) methodCallExpression);
        expressionList.Add(expression2);
      }
      return ((Expression<Action<PXReflectionSerializer.\u000F, object>>) ((parameterExpression1, parameterExpression2) => Expression.Block((IEnumerable<ParameterExpression>) new ParameterExpression[1]
      {
        left
      }, (IEnumerable<Expression>) expressionList))).Compile();
    }

    private Action<PXReflectionSerializer.\u0006\u2009, object> \u0002()
    {
      PXReflectionSerializer.\u000E\u2009.\u0008\u2009 obj1 = new PXReflectionSerializer.\u000E\u2009.\u0008\u2009();
      obj1.\u0002 = this;
      PXReflectionSerializer.\u0008[] objArray = this.\u0002();
      ParameterExpression instance = Expression.Parameter(typeof (PXReflectionSerializer.\u0006\u2009), "writer");
      ParameterExpression parameterExpression = Expression.Parameter(typeof (object), "item");
      ParameterExpression left = Expression.Variable(this.\u0002, "converted");
      List<Expression> expressionList = new List<Expression>();
      expressionList.Add((Expression) Expression.Assign((Expression) left, (Expression) Expression.Convert((Expression) parameterExpression, this.\u0002)));
      foreach (PXReflectionSerializer.\u0008 obj2 in objArray)
      {
        Expression expression1 = (Expression) left;
        foreach (FieldInfo field in obj2.\u0002)
          expression1 = (Expression) Expression.Field(expression1, field);
        if (obj2.\u0006)
        {
          if (obj2.\u000E.\u0005\u2006)
            expression1 = (Expression) Expression.Convert(expression1, obj2.\u000E.\u0002\u2000);
          MethodInfo method = obj2.\u0003;
          Expression expression2 = (Expression) Expression.Call((Expression) Expression.Field((Expression) instance, PXReflectionSerializer.\u000E<PXReflectionSerializer.\u0006\u2009>((Expression<Func<PXReflectionSerializer.\u0006\u2009, object>>) (w => w.h))), method, expression1);
          expressionList.Add(expression2);
        }
        else
        {
          MethodInfo method = PXReflectionSerializer.\u0002<PXReflectionSerializer.\u0006\u2009>((Expression<Action<PXReflectionSerializer.\u0006\u2009>>) (_ => _.h(default (object), default (PXReflectionSerializer.\u000E\u2009), default (PXReflectionSerializer.\u000E\u2009))));
          short num = obj2.\u000E.\u0002();
          MethodCallExpression methodCallExpression = Expression.Call(PXReflectionSerializer.\u0002<object>((Expression<Action<object>>) (_ => PXReflectionSerializer.\u000E\u2009.h((short) 0))), (Expression) Expression.Constant((object) num));
          Expression expression3 = !obj2.\u000E.\u000F\u2006 ? (Expression) Expression.Call((Expression) instance, method, (Expression) Expression.Convert(expression1, typeof (object)), (Expression) Expression.Constant((object) null, typeof (PXReflectionSerializer.\u000E\u2009)), (Expression) methodCallExpression) : (Expression) Expression.Call((Expression) instance, method, (Expression) Expression.Convert(expression1, typeof (object)), (Expression) methodCallExpression, (Expression) Expression.Constant((object) null, typeof (PXReflectionSerializer.\u000E\u2009)));
          expressionList.Add(expression3);
        }
      }
      BlockExpression body = Expression.Block((IEnumerable<ParameterExpression>) new ParameterExpression[1]
      {
        left
      }, (IEnumerable<Expression>) expressionList);
      obj1.\u000E = Expression.Lambda<Action<PXReflectionSerializer.\u0006\u2009, object>>((Expression) body, instance, parameterExpression).Compile();
      return new Action<PXReflectionSerializer.\u0006\u2009, object>(obj1.\u0002);
    }

    private object \u0002(PXReflectionSerializer.\u0002\u2009 _param1)
    {
      if (this.\u0008\u2004 == null)
        this.\u0008\u2004 = this.\u000E();
      return this.\u0008\u2004(_param1);
    }

    [Serializable]
    private sealed class \u0002
    {
      public static readonly PXReflectionSerializer.\u000E\u2009.\u0002 \u0002 = new PXReflectionSerializer.\u000E\u2009.\u0002();
      public static Func<PXReflectionSerializer.\u0008, string> \u000E;
      public static Func<PXReflectionSerializer.\u0008, bool> \u0006;
      public static Func<PXReflectionSerializer.\u0008, string> \u0008;
      public static Func<PXReflectionSerializer.\u0008, int> \u0003;
      public static Action<PXReflectionSerializer.\u0006\u2009, object> \u000F;
      public static Func<PXReflectionSerializer.\u0002\u2009, object> \u0005;
      public static Action<PXReflectionSerializer.\u0006\u2009, object> \u0002\u2009;
      public static Func<PXReflectionSerializer.\u0002\u2009, object> \u000E\u2009;
      public static Action<PXReflectionSerializer.\u0006\u2009, object> \u0006\u2009;
      public static Func<PXReflectionSerializer.\u0002\u2009, object> \u0008\u2009;
      public static Func<PXReflectionSerializer.\u0002\u2009, object> \u0003\u2009;
      public static Action<PXReflectionSerializer.\u0006\u2009, object> \u000F\u2009;
      public static Func<PXReflectionSerializer.\u0002\u2009, object> \u0005\u2009;
      public static Action<PXReflectionSerializer.\u0006\u2009, object> \u0002\u2006;
      public static Func<PXReflectionSerializer.\u0002\u2009, object> \u000E\u2006;
      public static Action<PXReflectionSerializer.\u0006\u2009, object> \u0006\u2006;
      public static Action<PXReflectionSerializer.\u0006\u2009, object> \u0008\u2006;
      public static Func<MethodInfo, bool> \u0003\u2006;
      public static Func<FieldInfo, string> \u000F\u2006;

      internal string \u0002(PXReflectionSerializer.\u0008 _param1)
      {
        return _param1.\u000E.\u0002.FullName;
      }

      internal bool \u0002(PXReflectionSerializer.\u0008 _param1) => _param1.\u0006;

      internal string \u000E(PXReflectionSerializer.\u0008 _param1) => _param1.\u000E.\u0002.Name;

      internal int \u0002(PXReflectionSerializer.\u0008 _param1) => _param1.\u0005;

      internal void \u0002(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        char[] chars = (char[]) _param2;
        _param1.h.WriteInt32(chars.Length);
        ((BinaryWriter) _param1.h).Write(chars);
      }

      internal object \u0002(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return (object) _param1.h.ReadChars(_param1.h.ReadInt32());
      }

      internal void \u000E(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        _param1.h.WriteByteArray((byte[]) _param2);
      }

      internal object \u000E(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return (object) _param1.h.ReadByteArray();
      }

      internal void \u0006(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        ((BinaryWriter) _param1.h).Write((string) _param2);
      }

      internal object \u0006(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return (object) _param1.h.ReadString();
      }

      internal object \u0008(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return (object) _param1.h.ReadTypeString();
      }

      internal void \u0008(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        _param1.h.WriteTypeString((Type) _param2);
      }

      internal object \u0003(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return (object) _param1.\u0002();
      }

      internal void \u0003(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        _param1.\u0002((MemberInfo) _param2);
      }

      internal object \u000F(PXReflectionSerializer.\u0002\u2009 _param1) => (object) DBNull.Value;

      internal void \u000F(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
      }

      internal void \u0005(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        _param1.\u0002((MulticastDelegate) _param2);
      }

      internal bool \u0002(MethodInfo _param1)
      {
        return _param1.Name == "CreateRange" && _param1.GetParameters().Length == 1;
      }

      internal string \u0002(FieldInfo _param1) => _param1.Name;
    }

    private sealed class \u0002\u2009
    {
      public MethodInfo \u0002;

      internal void \u0002(object _param1, StreamingContext _param2)
      {
        this.\u0002.Invoke(_param1, new object[1]
        {
          (object) _param2
        });
      }

      internal void \u000E(object _param1, StreamingContext _param2)
      {
        this.\u0002.Invoke(_param1, new object[1]
        {
          (object) _param2
        });
      }

      internal void \u0006(object _param1, StreamingContext _param2)
      {
        this.\u0002.Invoke(_param1, new object[1]
        {
          (object) _param2
        });
      }

      internal void \u0008(object _param1, StreamingContext _param2)
      {
        this.\u0002.Invoke(_param1, new object[1]
        {
          (object) _param2
        });
      }
    }

    private sealed class \u0003
    {
      public MethodInfo \u0002;

      internal bool \u0002(object _param1) => (bool) this.\u0002.Invoke(_param1, (object[]) null);
    }

    private sealed class \u0005
    {
      public MethodInfo \u0002;
      public PXReflectionSerializer.\u000E\u2009.\u0006 \u000E;

      internal object \u0002(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        PXReflectionSerializer.\u0003 obj1 = new PXReflectionSerializer.\u0003()
        {
          \u0006 = this.\u000E.\u000E.\u0002
        };
        _param1.\u000E(this.\u000E.\u000E.\u0002, (object) obj1);
        object obj2 = this.\u0002.Invoke((object) null, new object[1]
        {
          _param1.\u0003(this.\u000E.\u000E.\u0002)
        });
        obj1.\u0002(obj2);
        return obj2;
      }
    }

    private sealed class \u0006
    {
      public MethodInfo \u0002;
      public PXReflectionSerializer.\u000E\u2009.\u000E \u000E;

      internal void \u0002(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        this.\u0002.Invoke((object) _param1.h, new object[1]
        {
          _param2
        });
      }
    }

    private sealed class \u0006\u2009
    {
      public PXReflectionSerializer.\u000E\u2009 \u0002;
      public Func<PXReflectionSerializer.\u0002\u2009, object, object> \u000E;

      internal object \u0002(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        PXReflectionSerializer.LogEntry logEntry = _param1.\u0006\u2009;
        object uninitializedObject = FormatterServices.GetUninitializedObject(this.\u0002.\u0002);
        _param1.\u000E(this.\u0002, uninitializedObject);
        if (this.\u0002.\u000F\u2000 != null)
          this.\u0002.\u000F\u2000(uninitializedObject, _param1.\u0002\u2009);
        object obj = this.\u000E(_param1, uninitializedObject);
        _param1.\u0002(this.\u0002, obj);
        return obj;
      }
    }

    private sealed class \u0008
    {
      public MethodInfo \u0002;

      internal object \u0002(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return this.\u0002.Invoke((object) _param1.h, (object[]) null);
      }
    }

    private sealed class \u0008\u2009
    {
      public PXReflectionSerializer.\u000E\u2009 \u0002;
      public Action<PXReflectionSerializer.\u0006\u2009, object> \u000E;

      internal void \u0002(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        PXReflectionSerializer.\u000E\u2009 obj = this.\u0002;
        if (obj.\u0008\u2000 != null)
          obj.\u0008\u2000(_param2, _param1.\u000E\u2009);
        this.\u000E(_param1, _param2);
        if (obj.\u0003\u2000 == null)
          return;
        obj.\u0003\u2000(_param2, _param1.\u000E\u2009);
      }
    }

    private sealed class \u000E
    {
      public PXReflectionSerializer.\u000E\u2009 \u0002;
      public Type \u000E;

      internal object \u0002(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return _param1.\u0006(this.\u0002);
      }

      internal void \u0002(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        _param1.\u000F(_param2, this.\u0002);
      }

      internal bool \u0002(MethodInfo _param1)
      {
        return _param1.Name == "Read" + this.\u000E.Name && _param1.ReturnType == this.\u000E;
      }

      internal object \u000E(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return (object) _param1.\u0002(this.\u0002);
      }

      internal object \u0006(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return _param1.\u0008(this.\u0002);
      }

      internal void \u000E(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        _param1.\u0003(_param2, this.\u0002);
      }

      internal object \u0008(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return _param1.\u0005(this.\u0002);
      }

      internal void \u0006(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        _param1.\u0006(_param2, this.\u0002);
      }

      internal object \u0003(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return _param1.\u000F(this.\u0002);
      }

      internal void \u0008(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        throw new Exception($"Cannot find the serialization constructor {this.\u000E.FullName}(SerializationInfo info, StreamingContext context) ");
      }

      internal void \u0003(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        _param1.\u0008(_param2, this.\u0002);
      }

      internal void \u000F(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        Array instance = Array.CreateInstance(this.\u0002.\u000E\u2000, ((ICollection) _param2).Count);
        IEnumerable enumerable = (IEnumerable) _param2;
        int num = 0;
        foreach (object obj in enumerable)
          instance.SetValue(obj, num++);
        _param1.\u0003((object) instance, this.\u0002);
      }

      internal object \u000F(PXReflectionSerializer.\u0002\u2009 _param1)
      {
        return _param1.\u000E(this.\u0002);
      }

      internal void \u0005(PXReflectionSerializer.\u0006\u2009 _param1, object _param2)
      {
        if (this.\u0002.\u0006\u2004 == null)
          this.\u0002.\u0006\u2004 = this.\u0002.\u0002();
        this.\u0002.\u0006\u2004(_param1, _param2);
      }
    }

    private sealed class \u000E\u2009
    {
      public FieldInfo \u0002;

      internal object \u0002(object _param1) => this.\u0002.GetValue(_param1);

      internal void \u0002(object _param1, object _param2)
      {
        this.\u0002.SetValueDirect(__makeref (_param1), _param2);
      }
    }

    private sealed class \u000F
    {
      public ConstructorInfo \u0002;
      public PXReflectionSerializer.\u000E\u2009.\u0006 \u000E;

      internal object \u0002(SerializationInfo _param1, StreamingContext _param2)
      {
        object uninitializedObject = FormatterServices.GetUninitializedObject(this.\u000E.\u000E.\u000E);
        this.\u0002.Invoke(uninitializedObject, new object[2]
        {
          (object) _param1,
          (object) _param2
        });
        return uninitializedObject;
      }

      internal object \u000E(SerializationInfo _param1, StreamingContext _param2)
      {
        return this.\u0002.Invoke(new object[2]
        {
          (object) _param1,
          (object) _param2
        });
      }
    }
  }

  private sealed class \u000F
  {
    public readonly Dictionary<object, int> \u0002 = new Dictionary<object, int>((IEqualityComparer<object>) new ReflectionSerializer.ObjectComparer<object>());

    public void h(object _param1, PXReflectionSerializer.\u000E\u2009 _param2)
    {
      if (_param1 == null || _param1 is ValueType || (object) (_param1 as Type) != null || this.\u0002.TryGetValue(_param1, out int _))
        return;
      if (_param2 == null)
        _param2 = PXReflectionSerializer.\u0002(_param1.GetType());
      int num1 = _param2.\u000E();
      if (num1 == 0)
      {
        Array array = (Array) _param1;
        Type elementType = _param2.\u0002.GetElementType();
        int num2 = 8;
        if (elementType.IsValueType)
          num2 = PXReflectionSerializer.\u0002(elementType).\u000E();
        num1 = num2 * array.Length + 12;
      }
      this.\u0002.Add(_param1, num1);
      if (_param2.\u000E\u2006)
      {
        Type elementType = _param2.\u0002.GetElementType();
        PXReflectionSerializer.\u000E\u2009 obj = PXReflectionSerializer.\u0002(elementType);
        if (elementType.IsPrimitive)
          return;
        foreach (object o in (IEnumerable) _param1)
        {
          if (o != null)
          {
            if (!obj.\u0002.IsInstanceOfType(o))
              obj = PXReflectionSerializer.\u0002(o.GetType());
            obj.\u0002()(this, o);
          }
        }
      }
      else
      {
        try
        {
          _param2.\u0002()(this, _param1);
        }
        catch (VerificationException ex)
        {
        }
        catch (RemotingException ex)
        {
        }
      }
    }
  }

  public class LogEntry
  {
    public object Item;
    public int StartPos;
    public int EndPos;
  }

  public class ObjectInfo
  {
    public int ID;
    public Type T;
    public const int NULL_REF = 0;
    public const int NEW_OBJ = 1;
    public const int FIRST_ELEM = 2;
  }
}
