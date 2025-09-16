// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.CRBaseActivityMaint_CbAPI_LinkActionExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Collections;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

[PXInternalUseOnly]
public abstract class CRBaseActivityMaint_CbAPI_LinkActionExt<TGraph, TMain> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
{
  public PXAction<TMain> attachRefNote;

  [PXUIField(Visible = false)]
  [PXButton]
  public virtual IEnumerable AttachRefNote(PXAdapter adapter) => adapter.Get();
}
