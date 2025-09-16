// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBVariantAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;
using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>byte[]</tt> type to the database
/// column of a variant type.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</remarks>
/// <example>
/// <code>
/// [PXDBVariant]
/// [PXUIField(DisplayName = "Value")]
/// public virtual byte[] Value { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBVariantAttribute : 
  PXDBBinaryAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber
{
  /// <summary>Initializes a new instance of the attribute
  /// with default parameters.</summary>
  public PXDBVariantAttribute()
  {
  }

  /// <summary>Initializes a new instance with the given maximum
  /// length.</summary>
  /// <param name="length">The maximum length of the value.</param>
  public PXDBVariantAttribute(int length)
    : base(length)
  {
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null || e.ReturnValue == null || !(e.ReturnValue.GetType() == typeof (byte[])))
      return;
    e.ReturnValue = PXDBVariantAttribute.GetValue((byte[]) e.ReturnValue);
  }

  /// <summary>
  /// Gets a typed value from an array of bytes. The first byte
  /// in the array is converted to <tt>Typecode</tt> to determine
  /// the data type of the value.
  /// </summary>
  /// <param name="val">The array of bytes to convert to a typed value.</param>
  /// <returns>The typed value.</returns>
  public static object GetValue(byte[] val)
  {
    if (val.Length == 0)
      return (object) null;
    using (MemoryStream input = new MemoryStream(val, 1, val.Length - 1))
    {
      using (BinaryReader binaryReader = new BinaryReader((Stream) input, Encoding.Unicode))
      {
        switch (val[0])
        {
          case 3:
            return (object) binaryReader.ReadBoolean();
          case 4:
            return (object) binaryReader.ReadChar();
          case 5:
            return (object) binaryReader.ReadSByte();
          case 6:
            return (object) binaryReader.ReadByte();
          case 7:
            return (object) binaryReader.ReadInt16();
          case 8:
            return (object) binaryReader.ReadUInt16();
          case 9:
            return (object) binaryReader.ReadInt32();
          case 10:
            return (object) binaryReader.ReadUInt32();
          case 11:
            return (object) binaryReader.ReadInt64();
          case 12:
            return (object) binaryReader.ReadUInt64();
          case 13:
            return (object) binaryReader.ReadSingle();
          case 14:
            return (object) binaryReader.ReadDouble();
          case 15:
            return (object) binaryReader.ReadDecimal();
          case 16 /*0x10*/:
            return (object) new System.DateTime(binaryReader.ReadInt64());
          case 18:
            return (object) binaryReader.ReadString();
          default:
            throw new PXException("The value cannot be restored from a byte array.");
        }
      }
    }
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null || !(e.NewValue.GetType() != typeof (byte[])))
      return;
    e.NewValue = (object) PXDBVariantAttribute.SetValue(e.NewValue);
  }

  /// <summary>Convert a typed value to an array of bytes.</summary>
  /// <param name="value">The data value to convert.</param>
  /// <returns>The array of bytes representing the provided value.</returns>
  public static byte[] SetValue(object value)
  {
    TypeCode typeCode = System.Type.GetTypeCode(value.GetType());
    using (MemoryStream output = new MemoryStream())
    {
      using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output, Encoding.Unicode))
      {
        binaryWriter.Write((byte) typeCode);
        switch (typeCode)
        {
          case TypeCode.Boolean:
            binaryWriter.Write((bool) value);
            return output.ToArray();
          case TypeCode.Char:
            binaryWriter.Write((char) value);
            return output.ToArray();
          case TypeCode.SByte:
            binaryWriter.Write((sbyte) value);
            return output.ToArray();
          case TypeCode.Byte:
            binaryWriter.Write((byte) value);
            return output.ToArray();
          case TypeCode.Int16:
            binaryWriter.Write((short) value);
            return output.ToArray();
          case TypeCode.UInt16:
            binaryWriter.Write((ushort) value);
            return output.ToArray();
          case TypeCode.Int32:
            binaryWriter.Write((int) value);
            return output.ToArray();
          case TypeCode.UInt32:
            binaryWriter.Write((uint) value);
            return output.ToArray();
          case TypeCode.Int64:
            binaryWriter.Write((long) value);
            return output.ToArray();
          case TypeCode.UInt64:
            binaryWriter.Write((ulong) value);
            return output.ToArray();
          case TypeCode.Single:
            binaryWriter.Write((float) value);
            return output.ToArray();
          case TypeCode.Double:
            binaryWriter.Write((double) value);
            return output.ToArray();
          case TypeCode.Decimal:
            binaryWriter.Write((Decimal) value);
            return output.ToArray();
          case TypeCode.DateTime:
            binaryWriter.Write(((System.DateTime) value).Ticks);
            return output.ToArray();
          case TypeCode.String:
            binaryWriter.Write((string) value);
            return output.ToArray();
          default:
            throw new PXException("The value cannot be converted to a byte array.");
        }
      }
    }
  }
}
