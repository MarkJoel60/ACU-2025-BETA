// Decompiled with JetBrains decompiler
// Type: PX.Data.ILInstruction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Reflection.Emit;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class ILInstruction
{
  private object _Operand;
  public int Pos;
  public OpCode Code;
  internal Func<object> PendingOperand;

  public object Operand
  {
    get
    {
      if (this.PendingOperand != null)
      {
        this._Operand = this.PendingOperand();
        this.PendingOperand = (Func<object>) null;
      }
      return this._Operand;
    }
    set
    {
      this._Operand = value;
      this.PendingOperand = (Func<object>) null;
    }
  }

  public string Name => this.Code.Name;
}
