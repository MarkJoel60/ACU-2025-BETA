// Decompiled with JetBrains decompiler
// Type: PX.Data.SmartJoin`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class SmartJoin<Separator, Str1, Str2> : 
  BqlFormulaEvaluator<Separator, Str1, Str2>,
  IBqlOperand
  where Separator : IBqlOperand
  where Str1 : IBqlOperand
  where Str2 : IBqlOperand
{
  public override object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> pars)
  {
    string par1 = (string) pars[typeof (Separator)];
    string par2 = (string) pars[typeof (Str1)];
    string par3 = (string) pars[typeof (Str2)];
    string str1;
    if (!string.IsNullOrWhiteSpace(par2) && !string.IsNullOrWhiteSpace(par3))
      str1 = string.Join(par1, par2.Trim(), par3.Trim());
    else
      str1 = (par2 + par3).Trim();
    string str2 = str1;
    return !string.IsNullOrWhiteSpace(str2) ? (object) str2 : (object) null;
  }
}
