// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLineSplitProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// Special projection for POReceiptLineSplit records.
/// It returns both assigned and unassigned records in the scope of reports or generic inquiries,
/// but only one type of records depending on the passed parameter in the scope of other graphs.
/// </summary>
public class POReceiptLineSplitProjectionAttribute : PXProjectionAttribute
{
  protected readonly Type CustomSelect;

  public POReceiptLineSplitProjectionAttribute(
    Type select,
    Type unassignedType,
    bool isUnassignedValue)
    : base(select)
  {
    this.CustomSelect = BqlCommand.CreateInstance(new Type[1]
    {
      select
    }).WhereAnd(BqlCommand.Compose(new Type[4]
    {
      typeof (Where<,>),
      unassignedType,
      typeof (Equal<>),
      isUnassignedValue ? typeof (True) : typeof (False)
    })).GetSelectType();
    this.Persistent = true;
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    return EnumerableExtensions.IsIn<Type>(sender.Graph.GetType(), typeof (PXGraph), typeof (PXGenericInqGrph)) ? base.GetSelect(sender) : this.CustomSelect;
  }
}
