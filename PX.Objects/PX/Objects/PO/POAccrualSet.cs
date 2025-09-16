// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualSet
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

public class POAccrualSet : HashSet<PX.Objects.AP.APTran>
{
  private PX.Objects.AP.APTran _apTran;

  public POAccrualSet()
    : base((IEqualityComparer<PX.Objects.AP.APTran>) new POAccrualComparer())
  {
  }

  public POAccrualSet(IEqualityComparer<PX.Objects.AP.APTran> comparer)
    : base(comparer)
  {
  }

  public POAccrualSet(IEnumerable<PX.Objects.AP.APTran> trans, IEqualityComparer<PX.Objects.AP.APTran> comparer)
    : base(trans, comparer)
  {
  }

  public bool Contains(POLineRS line)
  {
    if (this._apTran == null)
      this._apTran = new PX.Objects.AP.APTran();
    this._apTran.POOrderType = line.OrderType;
    this._apTran.PONbr = line.OrderNbr;
    this._apTran.POLineNbr = line.LineNbr;
    this._apTran.POAccrualType = line.POAccrualType;
    this._apTran.POAccrualRefNoteID = line.OrderNoteID;
    this._apTran.POAccrualLineNbr = line.LineNbr;
    return this.Contains(this._apTran);
  }

  public bool Contains(LinkLineOrder line)
  {
    if (line.POAccrualType != "O")
      throw new PXArgumentException(nameof (line));
    if (this._apTran == null)
      this._apTran = new PX.Objects.AP.APTran();
    this._apTran.POOrderType = line.OrderType;
    this._apTran.PONbr = line.OrderNbr;
    this._apTran.POLineNbr = line.OrderLineNbr;
    this._apTran.POAccrualType = line.POAccrualType;
    this._apTran.POAccrualRefNoteID = line.OrderNoteID;
    this._apTran.POAccrualLineNbr = line.OrderLineNbr;
    return this.Contains(this._apTran);
  }
}
