// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.EntryStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class EntryStatus : 
  BqlType<IBqlEntryStatus, PXEntryStatus>.Operand<EntryStatus>,
  IBqlCreator,
  IBqlVerifier
{
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) (PXEntryStatus) (cache.InternalCurrent != null ? (object) cache.GetStatus(cache.InternalCurrent) : (object) 0);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return true;
  }

  public sealed class inserted : 
    BqlType<IBqlEntryStatus, PXEntryStatus>.Constant<EntryStatus.inserted>
  {
    public inserted()
      : base((PXEntryStatus) 2)
    {
    }
  }

  public sealed class updated : BqlType<IBqlEntryStatus, PXEntryStatus>.Constant<EntryStatus.updated>
  {
    public updated()
      : base((PXEntryStatus) 1)
    {
    }
  }

  public sealed class deleted : BqlType<IBqlEntryStatus, PXEntryStatus>.Constant<EntryStatus.deleted>
  {
    public deleted()
      : base((PXEntryStatus) 3)
    {
    }
  }

  public sealed class notchanged : 
    BqlType<IBqlEntryStatus, PXEntryStatus>.Constant<EntryStatus.notchanged>
  {
    public notchanged()
      : base((PXEntryStatus) 0)
    {
    }
  }
}
