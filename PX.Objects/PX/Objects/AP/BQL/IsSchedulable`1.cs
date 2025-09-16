// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.BQL.IsSchedulable`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.BQL;

public class IsSchedulable<TRegister> : IBqlUnary, IBqlCreator, IBqlVerifier where TRegister : PX.Objects.AP.APRegister
{
  private IBqlCreator where;

  public IsSchedulable()
  {
    Dictionary<string, System.Type> dictionary = ((IEnumerable<System.Type>) typeof (TRegister).GetNestedTypes()).ToDictionary<System.Type, string>((Func<System.Type, string>) (nestedTypes => nestedTypes.Name));
    System.Type type1 = dictionary["released"];
    System.Type type2 = dictionary["prebooked"];
    System.Type type3 = dictionary["hold"];
    System.Type type4 = dictionary["voided"];
    System.Type type5 = dictionary["rejected"];
    System.Type type6 = dictionary["origModule"];
    System.Type type7 = dictionary["isMigratedRecord"];
    System.Type type8 = dictionary["createdByScreenID"];
    System.Type type9 = dictionary["docType"];
    System.Type type10 = dictionary["refNbr"];
    System.Type type11 = dictionary["noteID"];
    this.where = (IBqlCreator) (Activator.CreateInstance(BqlCommand.Compose(typeof (Where<,,>), type1, typeof (Equal<>), typeof (False), typeof (And<,,>), type2, typeof (Equal<>), typeof (False), typeof (And<,,>), type3, typeof (Equal<>), typeof (False), typeof (And<,,>), type4, typeof (Equal<>), typeof (False), typeof (And<,,>), type5, typeof (Equal<>), typeof (False), typeof (And<,,>), type6, typeof (Equal<>), typeof (BatchModule.moduleAP), typeof (And<,,>), type7, typeof (Equal<>), typeof (False), typeof (And<>), typeof (Not<>), typeof (IsPOLinked<,>), type9, type10)) as IBqlUnary);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.where.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this.where.Verify(cache, item, pars, ref result, ref value);
  }

  public static bool IsDocumentSchedulable(PXGraph graph, PX.Objects.AP.APRegister document)
  {
    bool? result = new bool?();
    object obj = (object) null;
    new IsSchedulable<TRegister>().Verify(graph.Caches[typeof (TRegister)], (object) document, new List<object>(), ref result, ref obj);
    return result.GetValueOrDefault();
  }

  public static void Ensure(PXGraph graph, PX.Objects.AP.APRegister document)
  {
    if (!IsSchedulable<TRegister>.IsDocumentSchedulable(graph, document))
      throw new PXException("The document cannot be added to a schedule. Only balanced documents originated in the Accounts Payable module can be added to a schedule.");
  }
}
