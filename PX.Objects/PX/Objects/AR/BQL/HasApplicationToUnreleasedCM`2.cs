// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BQL.HasApplicationToUnreleasedCM`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.AR.Standalone;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.BQL;

internal class HasApplicationToUnreleasedCM<TDocTypeField, TRefNbrField> : 
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TDocTypeField : IBqlOperand
  where TRefNbrField : IBqlOperand
{
  public static readonly BqlCommand Select = (BqlCommand) new Select2<PX.Objects.AR.ARAdjust, InnerJoin<ARRegisterAlias, On<PX.Objects.AR.ARAdjust.adjdDocType, Equal<ARRegisterAlias.docType>, And<PX.Objects.AR.ARAdjust.adjdRefNbr, Equal<ARRegisterAlias.refNbr>>>>, Where<PX.Objects.AR.ARAdjust.adjgDocType, Equal<TDocTypeField>, And<PX.Objects.AR.ARAdjust.adjgRefNbr, Equal<TRefNbrField>, And<PX.Objects.AR.ARAdjust.adjdDocType, Equal<ARDocType.creditMemo>, And<ARRegisterAlias.released, NotEqual<True>>>>>>();

  private IBqlCreator exist
  {
    get
    {
      return (IBqlCreator) Activator.CreateInstance(typeof (Exists<>).MakeGenericType(HasApplicationToUnreleasedCM<TDocTypeField, TRefNbrField>.Select.GetType()));
    }
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.exist.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ((IBqlVerifier) this.exist).Verify(cache, item, pars, ref result, ref value);
  }
}
