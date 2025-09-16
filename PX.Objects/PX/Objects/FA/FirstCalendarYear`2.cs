// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FirstCalendarYear`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class FirstCalendarYear<BookIDValue, OrganizationIDValue> : 
  BqlFormulaEvaluator<BookIDValue, OrganizationIDValue>,
  IBqlOperand
  where BookIDValue : IBqlOperand
  where OrganizationIDValue : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> parameters)
  {
    int? parameter1 = (int?) parameters[typeof (BookIDValue)];
    int? parameter2 = (int?) parameters[typeof (OrganizationIDValue)];
    FABookYear faBookYear = GraphHelper.RowCast<FABookYear>((IEnumerable) PXSelectBase<FABookYear, PXSelect<FABookYear, Where<FABookYear.bookID, Equal<Required<FABook.bookID>>, And<FABookYear.organizationID, Equal<Required<FABookYear.organizationID>>>>, OrderBy<Asc<FABookYear.year>>>.Config>.SelectSingleBound(cache.Graph, new object[0], new object[2]
    {
      (object) parameter1,
      (object) parameter2
    })).FirstOrDefault<FABookYear>();
    return faBookYear == null ? (object) null : (object) faBookYear.Year;
  }
}
