// Decompiled with JetBrains decompiler
// Type: PX.Data.ILReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class ILReader
{
  private BinaryReader stream;
  private OpCode[] singleByteOpCode;
  private OpCode[] doubleByteOpCode;
  private byte[] instrunctions;
  private List<ILInstruction> ilInstructions;
  public Module module;

  public ILReader() => this.CreateOpCodes();

  public static ILInstruction[] Parse(MethodInfo methodInfo)
  {
    MethodBody methodBody = methodInfo.GetMethodBody();
    if (methodBody == null)
      return new ILInstruction[0];
    return new ILReader() { module = methodInfo.Module }.ReadInstructions(methodBody.GetILAsByteArray()).ToArray();
  }

  private void CreateOpCodes()
  {
    this.singleByteOpCode = new OpCode[225];
    this.doubleByteOpCode = new OpCode[31 /*0x1F*/];
    foreach (System.Reflection.FieldInfo opCodeField in this.GetOpCodeFields())
    {
      OpCode opCode = (OpCode) opCodeField.GetValue((object) null);
      if (opCode.OpCodeType != OpCodeType.Nternal)
      {
        if (opCode.Size == 1)
          this.singleByteOpCode[(int) opCode.Value] = opCode;
        else
          this.doubleByteOpCode[(int) opCode.Value & (int) byte.MaxValue] = opCode;
      }
    }
  }

  public List<ILInstruction> ReadInstructions(byte[] body)
  {
    this.ilInstructions = new List<ILInstruction>();
    this.instrunctions = body;
    this.stream = new BinaryReader((Stream) new MemoryStream(this.instrunctions), Encoding.UTF8);
    while (this.stream.BaseStream.Position < this.stream.BaseStream.Length)
    {
      ILInstruction instruction = new ILInstruction()
      {
        Pos = (int) this.stream.BaseStream.Position
      };
      instruction.Code = this.ReadOpCode();
      this.ReadOperand(instruction);
      this.ilInstructions.Add(instruction);
    }
    return this.ilInstructions;
  }

  private void ReadOperand(ILInstruction instruction)
  {
    object obj = (object) null;
    switch (instruction.Code.OperandType)
    {
      case OperandType.InlineBrTarget:
        obj = (object) ((long) this.stream.ReadInt32() + this.stream.BaseStream.Position);
        goto case OperandType.InlineNone;
      case OperandType.InlineField:
      case OperandType.InlineMethod:
      case OperandType.InlineTok:
      case OperandType.InlineType:
        int op1 = this.stream.ReadInt32();
        instruction.PendingOperand = (Func<object>) (() =>
        {
          try
          {
            return (object) this.module.ResolveMember(op1);
          }
          catch
          {
          }
          return (object) null;
        });
        goto case OperandType.InlineNone;
      case OperandType.InlineI:
        obj = (object) this.stream.ReadInt32();
        goto case OperandType.InlineNone;
      case OperandType.InlineI8:
        obj = (object) this.stream.ReadInt64();
        goto case OperandType.InlineNone;
      case OperandType.InlineNone:
        if (obj == null)
          break;
        instruction.Operand = obj;
        break;
      case OperandType.InlineR:
        obj = (object) this.stream.ReadDouble();
        goto case OperandType.InlineNone;
      case OperandType.InlineSig:
        int op2 = this.stream.ReadInt32();
        instruction.PendingOperand = (Func<object>) (() =>
        {
          try
          {
            return (object) this.module.ResolveSignature(op2);
          }
          catch
          {
          }
          return (object) null;
        });
        goto case OperandType.InlineNone;
      case OperandType.InlineString:
        int op3 = this.stream.ReadInt32();
        instruction.PendingOperand = (Func<object>) (() =>
        {
          try
          {
            return (object) this.module.ResolveString(op3);
          }
          catch
          {
          }
          return (object) null;
        });
        goto case OperandType.InlineNone;
      case OperandType.InlineSwitch:
        int length = this.stream.ReadInt32();
        int[] numArray1 = new int[length];
        int[] numArray2 = new int[length];
        for (int index = 0; index < length; ++index)
          numArray2[index] = this.stream.ReadInt32();
        for (int index = 0; index < length; ++index)
          numArray1[index] = (int) this.stream.BaseStream.Position + numArray2[index];
        goto case OperandType.InlineNone;
      case OperandType.InlineVar:
        obj = (object) this.stream.ReadUInt16();
        goto case OperandType.InlineNone;
      case OperandType.ShortInlineBrTarget:
        obj = (object) ((long) this.stream.ReadByte() + this.stream.BaseStream.Position);
        goto case OperandType.InlineNone;
      case OperandType.ShortInlineI:
        obj = !(instruction.Code == OpCodes.Ldc_I4_S) ? (object) this.stream.ReadByte() : (object) (sbyte) this.stream.ReadByte();
        goto case OperandType.InlineNone;
      case OperandType.ShortInlineR:
        obj = (object) this.stream.ReadSingle();
        goto case OperandType.InlineNone;
      case OperandType.ShortInlineVar:
        obj = (object) this.stream.ReadByte();
        goto case OperandType.InlineNone;
      default:
        throw new NotSupportedException();
    }
  }

  private OpCode ReadOpCode()
  {
    byte index = this.stream.ReadByte();
    return index != (byte) 254 ? this.singleByteOpCode[(int) index] : this.doubleByteOpCode[(int) this.stream.ReadByte()];
  }

  private System.Reflection.FieldInfo[] GetOpCodeFields()
  {
    return typeof (OpCodes).GetFields(BindingFlags.Static | BindingFlags.Public);
  }
}
