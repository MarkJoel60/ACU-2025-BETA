// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQViewExtender
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.BQLConstants;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.RQ;

public static class RQViewExtender
{
  public static void WhereAndCurrent<Filter>(this PXView view, params string[] excluded) where Filter : IBqlTable
  {
    view.WhereAnd(RQViewExtender.WhereByType(typeof (Filter), view.Graph, view.BqlSelect, excluded));
  }

  public static void WhereAndCurrent<Filter>(this PXSelectBase select, params string[] excluded) where Filter : IBqlTable
  {
    select.View.WhereAndCurrent<Filter>(excluded);
  }

  private static Type WhereByType(
    Type Filter,
    PXGraph graph,
    BqlCommand command,
    string[] excluded)
  {
    PXCache filter = graph.Caches[Filter];
    Type type = typeof (Where<BitOn, Equal<BitOn>>);
    foreach (string str1 in ((IEnumerable<string>) filter.Fields).Where<string>((Func<string, bool>) (field => (excluded == null || !((IEnumerable<string>) excluded).Any<string>((Func<string, bool>) (e => string.Compare(e, field, true) == 0))) && !filter.Fields.Contains(field + "Wildcard"))))
    {
      foreach (Type table in command.GetTables())
      {
        PXCache cach = graph.Caches[table];
        bool flag = false;
        if (cach.Fields.Contains(str1))
        {
          Type bqlField1 = filter.GetBqlField(str1);
          Type bqlField2 = cach.GetBqlField(str1);
          if (bqlField1 != (Type) null && bqlField2 != (Type) null)
          {
            type = BqlCommand.Compose(new Type[12]
            {
              typeof (Where2<,>),
              typeof (Where<,,>),
              typeof (Current<>),
              bqlField1,
              typeof (IsNull),
              typeof (Or<,>),
              bqlField2,
              typeof (Equal<>),
              typeof (Current<>),
              bqlField1,
              typeof (And<>),
              type
            });
            flag = true;
          }
        }
        string str2;
        if (str1.Length > 8 && str1.EndsWith("Wildcard") && cach.Fields.Contains(str2 = str1.Substring(0, str1.Length - 8)))
        {
          Type bqlField3 = filter.GetBqlField(str1);
          Type bqlField4 = cach.GetBqlField(str2);
          type = BqlCommand.Compose(new Type[12]
          {
            typeof (Where2<,>),
            typeof (Where<,,>),
            typeof (Current<>),
            bqlField3,
            typeof (IsNull),
            typeof (Or<,>),
            bqlField4,
            typeof (Like<>),
            typeof (Current<>),
            bqlField3,
            typeof (And<>),
            type
          });
          flag = true;
        }
        if (flag)
          break;
      }
    }
    return type;
  }
}
