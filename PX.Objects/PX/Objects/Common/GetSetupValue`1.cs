// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GetSetupValue`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

public class GetSetupValue<Field> : BqlFormulaEvaluator<Field>, IBqlOperand where Field : IBqlField
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    Type itemType = BqlCommand.GetItemType(typeof (Field));
    PXCache cach = cache.Graph.Caches[itemType];
    string field = cach.GetField(typeof (Field));
    object obj = new PXView(cache.Graph, true, BqlCommand.CreateInstance(new Type[2]
    {
      typeof (Select<>),
      itemType
    })).SelectSingle(Array.Empty<object>());
    return cach.GetValue(obj, field);
  }
}
