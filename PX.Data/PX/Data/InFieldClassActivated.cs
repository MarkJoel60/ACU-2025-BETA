// Decompiled with JetBrains decompiler
// Type: PX.Data.InFieldClassActivated
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class InFieldClassActivated : IBqlComparison, IBqlCreator, IBqlVerifier
{
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(true);
  }

  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    string[] fieldClassRestricted = PXAccess.Provider.FieldClassRestricted;
    if (fieldClassRestricted == null || fieldClassRestricted.Length == 0)
    {
      if (info.BuildExpression)
        exp = exp.IsNotNull();
      return flag;
    }
    if (info.BuildExpression)
    {
      SQLExpression exp1 = (SQLExpression) null;
      for (int index = 0; index < fieldClassRestricted.Length; ++index)
        exp1 = index != 0 ? exp1.Seq((SQLExpression) new SQLConst((object) fieldClassRestricted[index])) : (SQLExpression) new SQLConst((object) fieldClassRestricted[index]);
      exp = exp.NotIn(exp1);
    }
    if (info.Fields != null && info.Fields is BqlCommand.EqualityList fields)
      fields.NonStrict = true;
    return flag;
  }
}
