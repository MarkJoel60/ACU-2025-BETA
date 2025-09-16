// Decompiled with JetBrains decompiler
// Type: PX.Data.In3`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class In3<Operand1, Operand2, Operand3> : In3
  where Operand1 : IBqlOperand
  where Operand2 : IBqlOperand
  where Operand3 : IBqlOperand
{
  public In3()
    : base(typeof (Operand1), typeof (Operand2), typeof (Operand3))
  {
  }
}
