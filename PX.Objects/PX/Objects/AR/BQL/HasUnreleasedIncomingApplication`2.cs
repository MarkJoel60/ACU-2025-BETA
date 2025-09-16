// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BQL.HasUnreleasedIncomingApplication`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.BQL;

internal class HasUnreleasedIncomingApplication<TDocTypeField, TRefNbrField> : 
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TDocTypeField : IBqlOperand
  where TRefNbrField : IBqlOperand
{
  private static readonly IBqlCreator where = (IBqlCreator) new Where<ARAdjust.adjdDocType, Equal<TDocTypeField>, And<ARAdjust.adjdRefNbr, Equal<TRefNbrField>, And<ARAdjust.released, NotEqual<True>>>>();
  private static readonly IBqlCreator exist = (IBqlCreator) Activator.CreateInstance(typeof (Exists<>).MakeGenericType(HasUnreleasedIncomingApplication<TDocTypeField, TRefNbrField>.SelectType));

  private static Type SelectType
  {
    get
    {
      return typeof (PX.Data.Select<,>).MakeGenericType(typeof (ARAdjust), HasUnreleasedIncomingApplication<TDocTypeField, TRefNbrField>.where.GetType());
    }
  }

  public static BqlCommand Select
  {
    get
    {
      return (BqlCommand) Activator.CreateInstance(HasUnreleasedIncomingApplication<TDocTypeField, TRefNbrField>.SelectType);
    }
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return HasUnreleasedIncomingApplication<TDocTypeField, TRefNbrField>.exist.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ((IBqlVerifier) HasUnreleasedIncomingApplication<TDocTypeField, TRefNbrField>.exist).Verify(cache, item, pars, ref result, ref value);
  }
}
