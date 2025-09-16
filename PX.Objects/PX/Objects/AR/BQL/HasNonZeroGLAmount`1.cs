// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BQL.HasNonZeroGLAmount`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.BQL;

public class HasNonZeroGLAmount<TAdjustment> : IBqlUnary, IBqlCreator, IBqlVerifier where TAdjustment : ARAdjust
{
  private static IBqlCreator _where;

  private static IBqlCreator Where
  {
    get
    {
      if (HasNonZeroGLAmount<TAdjustment>._where != null)
        return HasNonZeroGLAmount<TAdjustment>._where;
      Dictionary<string, Type> dictionary = ((IEnumerable<Type>) typeof (TAdjustment).GetNestedTypes()).ToDictionary<Type, string>((Func<Type, string>) (nestedType => nestedType.Name));
      HasNonZeroGLAmount<TAdjustment>._where = (IBqlCreator) (Activator.CreateInstance(BqlCommand.Compose(new Type[28]
      {
        typeof (PX.Data.Where<,,>),
        dictionary["adjWOAmt"],
        typeof (NotEqual<>),
        typeof (decimal0),
        typeof (Or<,,>),
        dictionary["curyAdjgWOAmt"],
        typeof (NotEqual<>),
        typeof (decimal0),
        typeof (Or<,,>),
        dictionary["curyAdjdWOAmt"],
        typeof (NotEqual<>),
        typeof (decimal0),
        typeof (Or<,,>),
        dictionary["adjDiscAmt"],
        typeof (NotEqual<>),
        typeof (decimal0),
        typeof (Or<,,>),
        dictionary["curyAdjgDiscAmt"],
        typeof (NotEqual<>),
        typeof (decimal0),
        typeof (Or<,,>),
        dictionary["curyAdjdDiscAmt"],
        typeof (NotEqual<>),
        typeof (decimal0),
        typeof (Or<,>),
        dictionary["rGOLAmt"],
        typeof (NotEqual<>),
        typeof (decimal0)
      })) as IBqlUnary);
      return HasNonZeroGLAmount<TAdjustment>._where;
    }
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return HasNonZeroGLAmount<TAdjustment>.Where.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ((IBqlVerifier) HasNonZeroGLAmount<TAdjustment>.Where).Verify(cache, item, pars, ref result, ref value);
  }
}
