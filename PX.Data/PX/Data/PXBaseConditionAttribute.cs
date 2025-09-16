// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBaseConditionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXBaseConditionAttribute : PXBaseFormulaBasedAttribute
{
  public virtual System.Type Condition
  {
    get => this.Formula;
    set => this.Formula = value;
  }

  public PXBaseConditionAttribute()
  {
  }

  public PXBaseConditionAttribute(System.Type conditionType)
    : base(conditionType)
  {
  }

  protected internal static bool GetConditionResult(PXCache sender, object row, System.Type conditionType)
  {
    bool? formulaResult = PXBaseFormulaBasedAttribute.GetFormulaResult<bool?>(sender, row, conditionType);
    bool flag = true;
    return formulaResult.GetValueOrDefault() == flag & formulaResult.HasValue;
  }

  protected static System.Type GetCondition<AttrType, Field>(PXCache sender, object row)
    where AttrType : PXBaseConditionAttribute
    where Field : IBqlField
  {
    return sender.GetAttributesReadonly<Field>().OfType<AttrType>().Select<AttrType, System.Type>((Func<AttrType, System.Type>) (attr => attr.Formula)).FirstOrDefault<System.Type>();
  }

  protected static System.Type GetCondition<AttrType>(PXCache sender, object row, string fieldName) where AttrType : PXBaseConditionAttribute
  {
    return sender.GetAttributes(row, fieldName).OfType<AttrType>().Select<AttrType, System.Type>((Func<AttrType, System.Type>) (attr => attr.Formula)).FirstOrDefault<System.Type>();
  }
}
