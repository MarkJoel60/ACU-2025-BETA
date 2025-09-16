// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlCommon
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class BqlCommon
{
  public static IBqlCreator createOperand<Operand1>(this IBqlCreator op)
  {
    op = (object) Activator.CreateInstance<Operand1>() as IBqlCreator;
    return op != null ? op : throw new PXArgumentException(typeof (Operand1).Name, "'{0}' either has to be a class field or has to expose the IBqlCreator interface.");
  }

  public static IBqlCreator createOperand(this IBqlCreator op, System.Type tOperand)
  {
    op = Activator.CreateInstance(tOperand) as IBqlCreator;
    return op != null ? op : throw new PXArgumentException(tOperand.Name, "'{0}' either has to be a class field or has to expose the IBqlCreator interface.");
  }
}
