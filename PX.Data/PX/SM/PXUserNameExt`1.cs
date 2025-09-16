// Decompiled with JetBrains decompiler
// Type: PX.SM.PXUserNameExt`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class PXUserNameExt<Field> : BqlFormulaEvaluator<Field>, IBqlOperand where Field : IBqlField
{
  public override object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> parameters)
  {
    if (!(cache.GetStateExt<Field>(item) is PXFieldState stateExt) || string.IsNullOrEmpty(Convert.ToString(stateExt.Value)))
      return (object) null;
    return stateExt?.Value;
  }
}
