// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.PXProcessingViewOf`1
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using System;

#nullable disable
namespace PX.Data.BQL.Fluent;

public class PXProcessingViewOf<TTable> : PXProcessing<TTable> where TTable : class, IBqlTable, new()
{
  public PXProcessingViewOf(PXGraph graph)
    : base(graph, (Delegate) null)
  {
  }

  public PXProcessingViewOf(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public class FilteredBy<TFilter> : PXFilteredProcessing<TTable, TFilter> where TFilter : class, IBqlTable, new()
  {
    public FilteredBy(PXGraph graph)
      : base(graph)
    {
    }

    public FilteredBy(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class BasedOn<TCommand> : PXProcessing<TTable> where TCommand : FbqlSelect<TCommand, TTable>, new()
  {
    protected virtual BqlCommand GetCommand() => new TCommand().Unwrap();

    public BasedOn(PXGraph graph)
      : base(graph, (Delegate) null)
    {
    }

    public BasedOn(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    public class FilteredBy<TFilter> : PXFilteredProcessing<TTable, TFilter> where TFilter : class, IBqlTable, new()
    {
      protected virtual BqlCommand GetCommand() => new TCommand().Unwrap();

      public FilteredBy(PXGraph graph)
        : base(graph)
      {
      }

      public FilteredBy(PXGraph graph, Delegate handler)
        : base(graph, handler)
      {
      }
    }
  }
}
