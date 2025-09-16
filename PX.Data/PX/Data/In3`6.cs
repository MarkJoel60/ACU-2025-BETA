// Decompiled with JetBrains decompiler
// Type: PX.Data.In3`6
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class In3<Operand1, Operand2, Operand3, Operand4, Operand5, Operand6> : In3
  where Operand1 : IBqlOperand
  where Operand2 : IBqlOperand
  where Operand3 : IBqlOperand
  where Operand4 : IBqlOperand
  where Operand5 : IBqlOperand
  where Operand6 : IBqlOperand
{
  public In3()
    : base(typeof (Operand1), typeof (Operand2), typeof (Operand3), typeof (Operand4), typeof (Operand5), typeof (Operand6))
  {
  }
}
