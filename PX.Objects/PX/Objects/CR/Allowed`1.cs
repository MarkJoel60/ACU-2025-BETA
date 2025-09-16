// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Allowed`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

[Obsolete]
public class Allowed<StringlistValue> : BqlFormulaEvaluator<StringlistValue>, ISwitch where StringlistValue : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> pars)
  {
    if (!cache.GetAttributesReadonly(item, this.OuterField.Name).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr => attr is PXStringListAttribute)))
      return (object) null;
    string val = (string) pars[typeof (StringlistValue)];
    return !new List<string>((IEnumerable<string>) ((PXStringState) cache.GetStateExt(item, this.OuterField.Name)).AllowedValues).Exists((Predicate<string>) (str => string.CompareOrdinal(str, val) == 0)) ? (object) null : (object) val;
  }

  public System.Type OuterField { get; set; }
}
