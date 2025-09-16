// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BQL.IsSelfVoiding`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.BQL;

/// <summary>
/// A predicate that returns <c>true</c> if and only if the document type
/// represented by the generic type passed corresponds to one of the
/// Accounts Receivable self-voiding documents, which do not create a
/// separate voiding document: instead, all their applications are reversed.
/// </summary>
public class IsSelfVoiding<TDocTypeField> : IBqlUnary, IBqlCreator, IBqlVerifier where TDocTypeField : IBqlField
{
  private static IBqlCreator Where
  {
    get
    {
      return (IBqlCreator) new PX.Data.Where<TDocTypeField, Equal<ARDocType.smallBalanceWO>, Or<TDocTypeField, Equal<ARDocType.smallCreditWO>>>();
    }
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return IsSelfVoiding<TDocTypeField>.Where.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ((IBqlVerifier) IsSelfVoiding<TDocTypeField>.Where).Verify(cache, item, pars, ref result, ref value);
  }
}
