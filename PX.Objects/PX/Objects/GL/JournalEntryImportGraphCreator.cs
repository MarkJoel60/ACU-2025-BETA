// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntryImportGraphCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

public class JournalEntryImportGraphCreator : IDocGraphCreator
{
  public virtual PXGraph Create(GLTran tran) => this.Create(tran.TranType, tran.RefNbr, new int?());

  public virtual PXGraph Create(string aTranType, string aRefNbr, int? referenceID)
  {
    JournalEntryImport instance = PXGraph.CreateInstance<JournalEntryImport>();
    ((PXSelectBase<GLTrialBalanceImportMap>) instance.Map).Current = PXResultset<GLTrialBalanceImportMap>.op_Implicit(((PXSelectBase<GLTrialBalanceImportMap>) instance.Map).Search<GLTrialBalanceImportMap.number>((object) aRefNbr, Array.Empty<object>()));
    return (PXGraph) instance;
  }
}
