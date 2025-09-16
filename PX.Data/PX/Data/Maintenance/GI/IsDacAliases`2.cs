// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.IsDacAliases`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Maintenance.GI;

internal class IsDacAliases<ParentAlias, ChildAlias> : BqlFormulaEvaluator<ParentAlias, ChildAlias>
  where ParentAlias : IBqlOperand
  where ChildAlias : IBqlOperand
{
  public override object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> pars)
  {
    string par1 = (string) pars[typeof (ParentAlias)];
    string par2 = (string) pars[typeof (ChildAlias)];
    int? tableType1 = this.GetTableType(cache, par1);
    bool flag = !tableType1.HasValue || tableType1.GetValueOrDefault() == 0;
    if (flag)
    {
      int? tableType2 = this.GetTableType(cache, par2);
      flag = !tableType2.HasValue || tableType2.GetValueOrDefault() == 0;
    }
    return (object) flag;
  }

  private int? GetTableType(PXCache cache, string alias)
  {
    if (!(cache?.Graph is GenericInquiryDesigner graph))
      return new int?();
    return ((GITable) graph.Tables.Search<GITable.alias>((object) alias))?.Type;
  }
}
