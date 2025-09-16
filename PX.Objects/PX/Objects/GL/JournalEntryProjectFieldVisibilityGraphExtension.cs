// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntryProjectFieldVisibilityGraphExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.GL;

public class JournalEntryProjectFieldVisibilityGraphExtension : PXGraphExtension<JournalEntry>
{
  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2019 R2.")]
  protected virtual void _(PX.Data.Events.RowSelected<GLTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<Batch> e)
  {
    if (((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<Batch>>) e).Cache.Graph.UnattendedMode)
      return;
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.contractManagement>() || ProjectAttribute.IsPMVisible("GL");
    PXUIFieldAttribute.SetVisibility<GLTran.projectID>(((PXSelectBase) this.Base.GLTranModuleBatNbr).Cache, (object) null, flag ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<GLTran.projectID>(((PXSelectBase) this.Base.GLTranModuleBatNbr).Cache, (object) null, flag);
  }
}
