// Decompiled with JetBrains decompiler
// Type: PX.Data.FeatureInstalled`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class FeatureInstalled<Field> : 
  BqlChainableConditionLite<FeatureInstalled<Field>>,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where Field : IBqlField
{
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) (result = new bool?(PXAccess.FeatureInstalled<Field>()));
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (info.BuildExpression)
      exp = SQLExpression.IsTrue(PXAccess.FeatureInstalled<Field>());
    return true;
  }
}
