// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRBaseMassProcess`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.MassProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.CR;

public abstract class CRBaseMassProcess<TGraph, TPrimary> : PXGraph<TGraph>
  where TGraph : PXGraph, IMassProcess<TPrimary>, new()
  where TPrimary : class, IBqlTable, new()
{
  public PXCancel<TPrimary> Cancel;
  private readonly PXPrimaryGraphCollection primaryGraph;
  private PXProcessing<TPrimary> _items;

  protected CRBaseMassProcess()
  {
    this.primaryGraph = new PXPrimaryGraphCollection((PXGraph) this);
    if (this.ProcessingDataMember == null)
      throw new PXException("{0} is not processing graph", new object[1]
      {
        (object) typeof (TGraph).FullName
      });
    foreach (System.Type table in ((PXSelectBase) this.ProcessingDataMember).View.BqlSelect.GetTables())
      PXDBAttributeAttribute.Activate(((PXGraph) this).Caches[table]);
    // ISSUE: method pointer
    ((PXProcessingBase<TPrimary>) this.ProcessingDataMember).SetParametersDelegate(new PXProcessingBase<TPrimary>.ParametersDelegate((object) this, __methodptr(\u003C\u002Ector\u003Eb__2_0)));
    PXUIFieldAttribute.SetDisplayName<BAccount.acctCD>(((PXGraph) this).Caches[typeof (BAccount)], "Business Account");
  }

  protected virtual PXGraph GetPrimaryGraph(TPrimary item) => this.primaryGraph[(IBqlTable) item];

  public PXProcessing<TPrimary> ProcessingDataMember
  {
    get
    {
      return this._items ?? (this._items = ((IEnumerable<FieldInfo>) typeof (TGraph).GetFields(BindingFlags.Instance | BindingFlags.Public)).Where<FieldInfo>((Func<FieldInfo, bool>) (field => typeof (PXProcessing<TPrimary>).IsAssignableFrom(field.FieldType))).Select<FieldInfo, PXProcessing<TPrimary>>((Func<FieldInfo, PXProcessing<TPrimary>>) (field => (PXProcessing<TPrimary>) field.GetValue((object) this))).FirstOrDefault<PXProcessing<TPrimary>>());
    }
  }

  public abstract void ProccessItem(PXGraph graph, TPrimary item);

  protected virtual bool AskParameters() => true;
}
