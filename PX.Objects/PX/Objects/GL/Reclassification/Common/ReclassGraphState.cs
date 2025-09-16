// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Reclassification.Common.ReclassGraphState
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL.Reclassification.Common;

public class ReclassGraphState : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private int currentSplitLineNbr;

  public virtual ReclassScreenMode ReclassScreenMode { get; set; }

  public virtual string EditingBatchModule { get; set; }

  public virtual string EditingBatchNbr { get; set; }

  public virtual string EditingBatchMasterPeriodID { get; set; }

  public virtual string EditingBatchCuryID { get; set; }

  public virtual string OrigBatchModuleToReverse { get; set; }

  public virtual string OrigBatchNbrToReverse { get; set; }

  public virtual int CurrentSplitLineNbr => this.currentSplitLineNbr;

  public virtual void IncSplitLineNbr() => ++this.currentSplitLineNbr;

  public virtual HashSet<GLTranForReclassification> GLTranForReclassToDelete { get; set; }

  public Dictionary<GLTranKey, List<GLTranKey>> SplittingGroups { get; set; }

  public ReclassGraphState()
  {
    this.GLTranForReclassToDelete = new HashSet<GLTranForReclassification>();
    this.SplittingGroups = new Dictionary<GLTranKey, List<GLTranKey>>();
    this.currentSplitLineNbr = int.MinValue;
  }

  public void ClearSplittingGroups()
  {
    this.SplittingGroups = new Dictionary<GLTranKey, List<GLTranKey>>();
    this.currentSplitLineNbr = int.MinValue;
  }
}
