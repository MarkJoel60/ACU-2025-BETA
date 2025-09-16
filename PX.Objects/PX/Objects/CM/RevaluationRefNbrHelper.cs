// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RevaluationRefNbrHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM;

public class RevaluationRefNbrHelper
{
  private Dictionary<string, string> _batchKeys;
  private string extRefNbrNumbering;

  public RevaluationRefNbrHelper(string aExtRefNbrNumbering)
  {
    this._batchKeys = new Dictionary<string, string>();
    this.extRefNbrNumbering = aExtRefNbrNumbering;
  }

  public void Subscribe(JournalEntry graph)
  {
    // ISSUE: method pointer
    ((PXGraph) graph).RowPersisting.AddHandler<GLTran>(new PXRowPersisting((object) this, __methodptr(OnRowPersisting)));
    // ISSUE: method pointer
    ((PXGraph) graph).RowInserting.AddHandler<GLTran>(new PXRowInserting((object) this, __methodptr(OnRowInserting)));
  }

  public void OnRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    GLTran row = (GLTran) e.Row;
    if (row == null || (e.Operation & 3) == 3)
      return;
    this.AssignRefNbr(sender, row, true);
  }

  public void OnRowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    GLTran row = (GLTran) e.Row;
    if (row == null)
      return;
    this.AssignRefNbr(sender, row, false);
  }

  /// <summary>
  /// Assigns the <see cref="P:PX.Objects.GL.GLTran.RefNbr" />.
  /// The method either generates a reference number based on the specified numbering sequence (<see cref="F:PX.Objects.CM.RevaluationRefNbrHelper.extRefNbrNumbering" />),
  /// or takes it from the BatchNbr-RefNbr dictionary (<see cref="F:PX.Objects.CM.RevaluationRefNbrHelper._batchKeys" />).
  /// If the number is generated it is also stored in the dictionary.
  /// </summary>
  /// <param name="sender">cache of type <see cref="T:PX.Objects.GL.GLTran" />. Will be used to get a cache of type <see cref="T:PX.Objects.GL.Batch" />.</param>
  /// <param name="tran">transaction, to which the RefNbr is assigned</param>
  /// <param name="generateIfNew">specifies whether a RefNbr must be generated based on the sequence if the RefNbr is not found for the current Batch</param>
  private void AssignRefNbr(PXCache sender, GLTran tran, bool generateIfNew)
  {
    Batch current = (Batch) sender.Graph.Caches[typeof (Batch)].Current;
    if (current == null || string.IsNullOrEmpty(current.BatchNbr))
      return;
    string batchNbr = current.BatchNbr;
    if (!string.IsNullOrEmpty(tran.RefNbr))
      return;
    string str = (string) null;
    if (!this._batchKeys.TryGetValue(batchNbr, out str) & generateIfNew)
    {
      str = AutoNumberAttribute.GetNextNumber(sender, (object) tran, this.extRefNbrNumbering, tran.TranDate);
      this._batchKeys.Add(batchNbr, str);
    }
    if (str != null)
      tran.RefNbr = str;
    PXDBDefaultAttribute.SetDefaultForInsert<GLTran.refNbr>(sender, (object) tran, false);
  }
}
