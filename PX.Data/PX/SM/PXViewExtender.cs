// Decompiled with JetBrains decompiler
// Type: PX.SM.PXViewExtender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.SM;

public static class PXViewExtender
{
  public static void WhereAndCurrent<Filter>(this 
  #nullable disable
  PXView view, params string[] excluded) where Filter : IBqlTable
  {
    view.WhereAnd(PXViewExtender.WhereByType(typeof (Filter), view.Graph, view.BqlSelect, excluded));
  }

  public static void WhereAndCurrent<Filter>(this PXSelectBase select, params string[] excluded) where Filter : IBqlTable
  {
    select.View.WhereAndCurrent<Filter>(excluded);
  }

  private static System.Type WhereByType(
    System.Type Filter,
    PXGraph graph,
    BqlCommand command,
    string[] excluded)
  {
    PXCache filter = graph.Caches[Filter];
    System.Type type = typeof (Where<PXViewExtender.BitOn, Equal<PXViewExtender.BitOn>>);
    foreach (string field1 in filter.Fields.Where<string>((Func<string, bool>) (field => (excluded == null || !((IEnumerable<string>) excluded).Any<string>((Func<string, bool>) (e => string.Compare(e, field, true) == 0))) && !filter.Fields.Contains(field + "Wildcard"))))
    {
      foreach (System.Type table in command.GetTables())
      {
        PXCache cach = graph.Caches[table];
        bool flag = false;
        if (cach.Fields.Contains(field1))
        {
          System.Type bqlField1 = filter.GetBqlField(field1);
          System.Type bqlField2 = cach.GetBqlField(field1);
          if (bqlField1 != (System.Type) null && bqlField2 != (System.Type) null)
          {
            type = BqlCommand.Compose(typeof (Where2<,>), typeof (Where<,,>), typeof (Current<>), bqlField1, typeof (PX.Data.IsNull), typeof (Or<,>), bqlField2, typeof (Equal<>), typeof (Current<>), bqlField1, typeof (PX.Data.And<>), type);
            flag = true;
          }
        }
        string field2;
        if (field1.Length > 8 && field1.EndsWith("Wildcard") && cach.Fields.Contains(field2 = field1.Substring(0, field1.Length - 8)))
        {
          System.Type bqlField3 = filter.GetBqlField(field1);
          System.Type bqlField4 = cach.GetBqlField(field2);
          type = BqlCommand.Compose(typeof (Where2<,>), typeof (Where<,,>), typeof (Current<>), bqlField3, typeof (PX.Data.IsNull), typeof (Or<,>), bqlField4, typeof (Like<>), typeof (Current<>), bqlField3, typeof (PX.Data.And<>), type);
          flag = true;
        }
        if (flag)
          break;
      }
    }
    return type;
  }

  public class BitOn : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXViewExtender.BitOn>
  {
    public BitOn()
      : base(1)
    {
    }
  }
}
