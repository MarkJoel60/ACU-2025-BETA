// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipLineSplitProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// Special projection for SOShipLineSplit records.
/// It returns both assigned and unassigned records in the scope of reports or generic inquiries,
/// but only one type of records depending on the passed parameter in the scope of other graphs.
/// </summary>
public class SOShipLineSplitProjectionAttribute : PXProjectionAttribute
{
  protected bool _isUnassignedValue;
  protected Type _customselect;

  public SOShipLineSplitProjectionAttribute(
    Type select,
    Type unassignedType,
    bool isUnassignedValue)
    : base(select)
  {
    this._isUnassignedValue = isUnassignedValue;
    Type[] genericArguments = select.GetGenericArguments();
    Type type;
    if (!this._isUnassignedValue)
      type = BqlCommand.Compose(new Type[5]
      {
        typeof (Select<,>),
        genericArguments[0],
        typeof (Where<,>),
        unassignedType,
        typeof (Equal<False>)
      });
    else
      type = BqlCommand.Compose(new Type[5]
      {
        typeof (Select<,>),
        genericArguments[0],
        typeof (Where<,>),
        unassignedType,
        typeof (Equal<True>)
      });
    this._customselect = type;
    this.Persistent = true;
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    return sender.Graph.GetType() == typeof (PXGraph) || sender.Graph.GetType() == typeof (PXGenericInqGrph) ? base.GetSelect(sender) : this._customselect;
  }
}
