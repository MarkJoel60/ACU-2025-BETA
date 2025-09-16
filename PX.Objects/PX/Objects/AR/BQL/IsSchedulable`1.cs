// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BQL.IsSchedulable`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.Common;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.BQL;

/// <summary>
/// A predicate that returns <c>true</c> if and only if the
/// <see cref="!:APRegister" /> descendant record can be added to
/// a recurring transaction schedule.
/// </summary>
/// <remarks>
/// Due to the absence of Exists&lt;&gt; BQL predicate, the BQL implementation
/// of the current predicate does not check for the presence of <see cref="!:GLVoucher" />
/// records referencing the document record. It should be done separately via a
/// left join and a null check. See <see cref="M:PX.Objects.AR.BQL.IsSchedulable`1.IsDocumentSchedulable(PX.Data.PXGraph,PX.Objects.AR.ARRegister)" /> for example
/// and details.
/// </remarks>
public class IsSchedulable<TRegister> : IBqlUnary, IBqlCreator, IBqlVerifier where TRegister : ARRegister
{
  private static Type _whereType;

  private static IBqlCreator Where
  {
    get
    {
      if (IsSchedulable<TRegister>._whereType == (Type) null)
      {
        Dictionary<string, Type> dictionary = ((IEnumerable<Type>) typeof (TRegister).GetNestedTypes()).ToDictionary<Type, string>((Func<Type, string>) (nestedTypes => nestedTypes.Name));
        IsSchedulable<TRegister>._whereType = BqlCommand.Compose(new Type[20]
        {
          typeof (PX.Data.Where<,,>),
          dictionary["released"],
          typeof (Equal<>),
          typeof (False),
          typeof (And<,,>),
          dictionary["hold"],
          typeof (Equal<>),
          typeof (False),
          typeof (And<,,>),
          dictionary["voided"],
          typeof (Equal<>),
          typeof (False),
          typeof (And<,,>),
          dictionary["origModule"],
          typeof (Equal<>),
          typeof (BatchModule.moduleAR),
          typeof (And<,>),
          dictionary["isMigratedRecord"],
          typeof (Equal<>),
          typeof (False)
        });
      }
      return (IBqlCreator) (Activator.CreateInstance(IsSchedulable<TRegister>._whereType) as IBqlUnary);
    }
  }

  public static bool IsDocumentSchedulable(PXGraph graph, ARRegister document)
  {
    return ((PXSelectBase<ARRegister>) new PXSelect<ARRegister, PX.Data.Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>, And<IsSchedulable<ARRegister>>>>>(graph)).Any<ARRegister>((object) document.DocType, (object) document.RefNbr);
  }

  public static void Ensure(PXGraph graph, ARRegister document)
  {
    if (!IsSchedulable<TRegister>.IsDocumentSchedulable(graph, document))
      throw new PXException("The document cannot be added to a schedule. Only balanced documents originated in the Accounts Receivable module can be added to a schedule.");
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return IsSchedulable<TRegister>.Where.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ((IBqlVerifier) IsSchedulable<TRegister>.Where).Verify(cache, item, pars, ref result, ref value);
  }
}
