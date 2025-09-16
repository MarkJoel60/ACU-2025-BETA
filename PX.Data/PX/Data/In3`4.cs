// Decompiled with JetBrains decompiler
// Type: PX.Data.In3`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Checks if the specified field value matches any value in the list of constants defined by the operand. The list can contain from two to four constants. The condition is true if the field value is equal to a value from the list. Equivalent to <tt>(... OR ... OR ... OR ...)</tt> statement.</summary>
public class In3<Operand1, Operand2, Operand3, Operand4> : In3
  where Operand1 : IBqlOperand
  where Operand2 : IBqlOperand
  where Operand3 : IBqlOperand
  where Operand4 : IBqlOperand
{
  public In3()
    : base(typeof (Operand1), typeof (Operand2), typeof (Operand3), typeof (Operand4))
  {
  }
}
