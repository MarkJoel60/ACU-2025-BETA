// Decompiled with JetBrains decompiler
// Type: PX.Data.MassProcess.PXMassProcessBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.MassProcess;

/// <exclude />
public abstract class PXMassProcessBase<TGraph, TPrimary> : PXGraph<TGraph>
  where TGraph : PXGraph, IMassProcess<TPrimary>, new()
  where TPrimary : class, IBqlTable, new()
{
  public PXCancel<TPrimary> Cancel;
  private readonly PXPrimaryGraphCollection primaryGraph;
  private PXProcessing<TPrimary> _items;

  protected PXMassProcessBase()
  {
    this.primaryGraph = new PXPrimaryGraphCollection((PXGraph) this);
    if (this.ProcessingDataMember == null)
      throw new PXException($"{typeof (TGraph).FullName} is not processing graph");
    this.ProcessingDataMember.SetParametersDelegate((PXProcessingBase<TPrimary>.ParametersDelegate) (_param1 =>
    {
      bool flag = this.AskParameters();
      this.Unload();
      TGraph process;
      using (new PXPreserveScope())
        process = PXGraph.CreateInstance<TGraph>();
      this.ProcessingDataMember.SetProcessDelegate((PXProcessingBase<TPrimary>.ProcessItemDelegate) (item =>
      {
        PXGraph graph = this.primaryGraph[(IBqlTable) item];
        if (graph == null)
          throw new PXException("A graph type cannot be determined from the specified article type.");
        process.ProccessItem(graph, item);
        graph.Actions.PressSave();
      }));
      return flag;
    }));
  }

  public PXProcessing<TPrimary> ProcessingDataMember
  {
    get
    {
      return this._items ?? (this._items = ((IEnumerable<System.Reflection.FieldInfo>) typeof (TGraph).GetFields(BindingFlags.Instance | BindingFlags.Public)).Where<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (field => typeof (PXProcessing<TPrimary>).IsAssignableFrom(field.FieldType))).Select<System.Reflection.FieldInfo, PXProcessing<TPrimary>>((Func<System.Reflection.FieldInfo, PXProcessing<TPrimary>>) (field => (PXProcessing<TPrimary>) field.GetValue((object) this))).FirstOrDefault<PXProcessing<TPrimary>>());
    }
  }

  public abstract void ProccessItem(PXGraph graph, TPrimary item);

  protected virtual bool AskParameters() => true;
}
