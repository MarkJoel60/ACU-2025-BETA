// Decompiled with JetBrains decompiler
// Type: PX.Data.PXVariantAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;
using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>byte[]</tt> type that is not
/// mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field is not bound to a table column.</remarks>
/// <example>
/// The code belows the definition of a data field in the DAC.
/// <code>
/// [PXVariant]
/// [PXUIField(Visible = false)]
/// public object TargetTable { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXVariantAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber
{
  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null || e.ReturnValue == null || !(e.ReturnValue.GetType() == typeof (byte[])))
      return;
    e.ReturnValue = PXVariantAttribute.GetValue((byte[]) e.ReturnValue);
  }

  /// <exclude />
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
    TypeCode typeCode = System.Type.GetTypeCode(e.NewValue.GetType());
    using (MemoryStream output = new MemoryStream())
    {
      using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output, Encoding.Unicode))
      {
        binaryWriter.Write((byte) typeCode);
        switch (typeCode)
        {
          case TypeCode.Boolean:
            binaryWriter.Write((bool) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.Char:
            binaryWriter.Write((char) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.SByte:
            binaryWriter.Write((sbyte) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.Byte:
            binaryWriter.Write((byte) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.Int16:
            binaryWriter.Write((short) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.UInt16:
            binaryWriter.Write((ushort) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.Int32:
            binaryWriter.Write((int) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.UInt32:
            binaryWriter.Write((uint) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.Int64:
            binaryWriter.Write((long) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.UInt64:
            binaryWriter.Write((ulong) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.Single:
            binaryWriter.Write((float) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.Double:
            binaryWriter.Write((double) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.Decimal:
            binaryWriter.Write((Decimal) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.DateTime:
            binaryWriter.Write(((System.DateTime) e.NewValue).Ticks);
            e.NewValue = (object) output.ToArray();
            break;
          case TypeCode.String:
            binaryWriter.Write((string) e.NewValue);
            e.NewValue = (object) output.ToArray();
            break;
          default:
            throw new PXException("The value cannot be converted to a byte array.");
        }
      }
    }
  }
}
