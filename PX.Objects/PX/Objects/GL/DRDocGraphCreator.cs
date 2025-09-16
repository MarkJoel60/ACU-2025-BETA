// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DRDocGraphCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.DR;
using System;

#nullable disable
namespace PX.Objects.GL;

public class DRDocGraphCreator : IDocGraphCreator
{
  public virtual PXGraph Create(GLTran tran) => this.Create(tran.TranType, tran.RefNbr, new int?());

  public virtual PXGraph Create(string aTranType, string aRefNbr, int? referenceID)
  {
    DraftScheduleMaint instance = PXGraph.CreateInstance<DraftScheduleMaint>();
    ((PXSelectBase<DRSchedule>) instance.Schedule).Current = PXResultset<DRSchedule>.op_Implicit(((PXSelectBase<DRSchedule>) instance.Schedule).Search<DRSchedule.scheduleNbr>((object) aRefNbr, Array.Empty<object>()));
    return (PXGraph) instance;
  }
}
