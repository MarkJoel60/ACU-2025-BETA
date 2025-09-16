// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BQL.IsSelfVoidingVoidApplication`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.BQL;

/// <summary>
/// A predicate that returns <c>true</c> if and only if the application record
/// is produced by the voiding process for one of the Accounts Receivable
/// self-voiding documents. Due to the fact that there is no separate voiding
/// document record for self-voiding documents, such applications should be
/// separately reported in <see cref="T:PX.Objects.AR.ARStatementProcess" />.
/// </summary>
public class IsSelfVoidingVoidApplication<TAdjust> : IBqlUnary, IBqlCreator, IBqlVerifier where TAdjust : ARAdjust
{
  private static Type _whereType;

  private static IBqlCreator Where
  {
    get
    {
      if (IsSelfVoidingVoidApplication<TAdjust>._whereType == (Type) null)
      {
        Dictionary<string, Type> dictionary = ((IEnumerable<Type>) typeof (TAdjust).GetNestedTypes()).ToDictionary<Type, string>((Func<Type, string>) (nestedTypes => nestedTypes.Name));
        IsSelfVoidingVoidApplication<TAdjust>._whereType = BqlCommand.Compose(new Type[14]
        {
          typeof (PX.Data.Where<,,>),
          dictionary["voidAdjNbr"],
          typeof (IsNotNull),
          typeof (And<,,>),
          dictionary["voided"],
          typeof (Equal<>),
          typeof (True),
          typeof (And<>),
          typeof (Where2<,>),
          typeof (IsSelfVoiding<>),
          dictionary["adjgDocType"],
          typeof (Or<>),
          typeof (IsSelfVoiding<>),
          dictionary["adjdDocType"]
        });
      }
      return (IBqlCreator) (Activator.CreateInstance(IsSelfVoidingVoidApplication<TAdjust>._whereType) as IBqlUnary);
    }
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return IsSelfVoidingVoidApplication<TAdjust>.Where.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ((IBqlVerifier) IsSelfVoidingVoidApplication<TAdjust>.Where).Verify(cache, item, pars, ref result, ref value);
  }

  public static bool Verify(TAdjust application)
  {
    if (!application.Voided.GetValueOrDefault() || !application.VoidAdjNbr.HasValue)
      return false;
    return ARDocType.IsSelfVoiding(application.AdjgDocType) || ARDocType.IsSelfVoiding(application.AdjdDocType);
  }
}
