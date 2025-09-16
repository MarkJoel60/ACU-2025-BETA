// Decompiled with JetBrains decompiler
// Type: PX.Common.BinaryReaderEx
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Web.Compilation;

#nullable disable
namespace PX.Common;

public class BinaryReaderEx(Stream s) : BinaryReader(s)
{
  public BitReader BitReader = new BitReader();
  private static readonly ConcurrentDictionary<string, Type> \u0002 = new ConcurrentDictionary<string, Type>();

  public Stream GetStream() => this.BaseStream;

  public Type ReadTypeString()
  {
    string str = this.ReadString();
    Type type;
    if (!BinaryReaderEx.\u0002.TryGetValue(str, out type))
    {
      type = PXBuildManager.GetType(str, false);
      if (type == (Type) null)
        throw new PXSerializationException("Cannot find the type: " + str);
      BinaryReaderEx.\u0002.TryAdd(str, type);
    }
    return type;
  }

  public DateTime ReadDateTime() => DateTime.FromBinary(this.ReadInt64());

  public Guid ReadGuid() => new Guid(this.ReadBytes(16 /*0x10*/));

  public short? ReadInt16Nullable()
  {
    return this.ReadBoolean() ? new short?() : new short?(this.ReadInt16());
  }

  public int? ReadInt32Nullable() => this.ReadBoolean() ? new int?() : new int?(this.ReadInt32());

  public long? ReadInt64Nullable()
  {
    return this.ReadBoolean() ? new long?() : new long?(this.ReadInt64());
  }

  public Decimal? ReadDecimalNullable()
  {
    return this.ReadBoolean() ? new Decimal?() : new Decimal?(this.ReadDecimal());
  }

  public Guid? ReadGuidNullable() => this.ReadBoolean() ? new Guid?() : new Guid?(this.ReadGuid());

  public DateTime? ReadDateTimeNullable()
  {
    return this.ReadBoolean() ? new DateTime?() : new DateTime?(this.ReadDateTime());
  }

  public bool? ReadBooleanNullable()
  {
    return this.ReadBoolean() ? new bool?() : new bool?(this.ReadBoolean());
  }

  public byte[] ReadByteArray() => this.ReadBytes(this.ReadInt32());

  public override bool ReadBoolean() => this.BitReader.Read();
}
