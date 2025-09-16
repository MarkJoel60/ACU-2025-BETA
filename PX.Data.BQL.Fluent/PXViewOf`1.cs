// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.PXViewOf`1
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <summary>
/// Defines a data view for retrieving a particular data set from
/// the database and provides the interface to the cache for inserting,
/// updating, and deleting the data records.
/// </summary>
/// <summary>A fluent BQL data view.</summary>
/// <typeparam name="TTable">A DAC type.</typeparam>
public class PXViewOf<TTable> : PXSelect<TTable> where TTable : class, IBqlTable, new()
{
  public PXViewOf(PXGraph graph)
    : base(graph)
  {
  }

  public PXViewOf(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  /// <summary>Enforce readonly-mode for current view</summary>
  public class Readonly : PXSelectReadonly<TTable>
  {
    public Readonly(PXGraph graph)
      : base(graph)
    {
    }

    public Readonly(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  /// <summary>
  /// Defines a select command (<see cref="T:PX.Data.BQL.Fluent.IFbqlSelect`1" />) which receives data.
  /// </summary>
  /// <typeparam name="TCommand"><see cref="T:PX.Data.BQL.Fluent.IFbqlSelect`1" /> which receives data.</typeparam>
  public class BasedOn<TCommand> : PXSelectBase<TTable, PXViewOf<TTable>.BasedOn<TCommand>.Config> where TCommand : FbqlSelect<TCommand, TTable>, new()
  {
    public BasedOn(PXGraph graph)
      : base(graph)
    {
    }

    public BasedOn(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    public class Config : 
      PXSelectBase<TTable, PXViewOf<TTable>.BasedOn<TCommand>.Config>.IViewConfig,
      IViewConfigBase
    {
      private static readonly TCommand CommandPrototype = new TCommand();

      public BqlCommand GetCommand() => new TCommand().Unwrap();

      public bool IsReadOnly
      {
        get => PXViewOf<TTable>.BasedOn<TCommand>.Config.CommandPrototype.HasAggregateBlock;
      }
    }

    /// <summary>
    /// The class that implements the functionality that allows users to drag rows, cut and paste rows, and insert new rows in the middle of the grid.
    /// </summary>
    /// <typeparam name="TPrimaryTable">The primary DAC in the graph.</typeparam>
    /// <typeparam name="TSortedTable">The DAC of the grid rows. Must be the same as <tt>TTable</tt></typeparam>
    /// <remarks>This class can be used in a graph for the grid's view.</remarks>
    [PXDynamicButton(new string[] {"PasteLine", "ResetOrder"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (ActionsMessages))]
    public class WithDragAndDrop<TPrimaryTable, TSortedTable> : 
      PXOrderedSelectBase<TPrimaryTable, TSortedTable>
      where TPrimaryTable : class, IBqlTable, new()
      where TSortedTable : class, TTable, IBqlTable, ISortOrder, new()
    {
      /// <exclude />
      public WithDragAndDrop(PXGraph graph)
      {
        ((PXSelectBase) this)._Graph = graph;
        this.Initialize();
        ((PXSelectBase) this).View = new PXView(graph, false, PXSelectBase<TTable, PXViewOf<TTable>.BasedOn<TCommand>.Config>.GetCommand());
      }

      /// <exclude />
      public WithDragAndDrop(PXGraph graph, Delegate handler)
      {
        ((PXSelectBase) this)._Graph = graph;
        this.Initialize();
        ((PXSelectBase) this).View = new PXView(graph, false, PXSelectBase<TTable, PXViewOf<TTable>.BasedOn<TCommand>.Config>.GetCommand(), handler);
      }

      /// <summary>
      /// The class that dynamically generates command names based on TSortedTable
      /// </summary>
      public class WithOwnActions : PXOrderedSelectBase<TPrimaryTable, TSortedTable>
      {
        /// <exclude />
        public string PasteLineCommand = "PasteLine_" + typeof (TSortedTable).Name;
        /// <exclude />
        public string ResetOrderCommand = "ResetOrder_" + typeof (TSortedTable).Name;

        /// <exclude />
        public WithOwnActions(PXGraph graph)
        {
          ((PXSelectBase) this)._Graph = graph;
          this.Initialize();
          ((PXSelectBase) this).View = new PXView(graph, false, PXSelectBase<TTable, PXViewOf<TTable>.BasedOn<TCommand>.Config>.GetCommand());
        }

        /// <exclude />
        public WithOwnActions(PXGraph graph, Delegate handler)
        {
          ((PXSelectBase) this)._Graph = graph;
          this.Initialize();
          ((PXSelectBase) this).View = new PXView(graph, false, PXSelectBase<TTable, PXViewOf<TTable>.BasedOn<TCommand>.Config>.GetCommand(), handler);
        }

        protected virtual void AddActions(PXGraph graph)
        {
          PXGraph pxGraph1 = graph;
          string pasteLineCommand1 = this.PasteLineCommand;
          string pasteLineCommand2 = this.PasteLineCommand;
          PXViewOf<TTable>.BasedOn<TCommand>.WithDragAndDrop<TPrimaryTable, TSortedTable>.WithOwnActions withOwnActions1 = this;
          // ISSUE: virtual method pointer
          PXButtonDelegate pxButtonDelegate1 = new PXButtonDelegate((object) withOwnActions1, __vmethodptr(withOwnActions1, PasteLine));
          this.AddAction(pxGraph1, pasteLineCommand1, pasteLineCommand2, pxButtonDelegate1, (List<PXEventSubscriberAttribute>) null);
          PXGraph pxGraph2 = graph;
          string resetOrderCommand1 = this.ResetOrderCommand;
          string resetOrderCommand2 = this.ResetOrderCommand;
          PXViewOf<TTable>.BasedOn<TCommand>.WithDragAndDrop<TPrimaryTable, TSortedTable>.WithOwnActions withOwnActions2 = this;
          // ISSUE: virtual method pointer
          PXButtonDelegate pxButtonDelegate2 = new PXButtonDelegate((object) withOwnActions2, __vmethodptr(withOwnActions2, ResetOrder));
          this.AddAction(pxGraph2, resetOrderCommand1, resetOrderCommand2, pxButtonDelegate2, (List<PXEventSubscriberAttribute>) null);
        }
      }
    }

    /// <summary>Enforce readonly-mode for current view</summary>
    public class ReadOnly : PXSelectBase<TTable, PXViewOf<TTable>.BasedOn<TCommand>.ReadOnly.Config>
    {
      public ReadOnly(PXGraph graph)
        : base(graph)
      {
      }

      public ReadOnly(PXGraph graph, Delegate handler)
        : base(graph, handler)
      {
      }

      public class Config : 
        PXSelectBase<TTable, PXViewOf<TTable>.BasedOn<TCommand>.ReadOnly.Config>.IViewConfig,
        IViewConfigBase
      {
        public BqlCommand GetCommand() => new TCommand().Unwrap();

        public bool IsReadOnly => true;
      }
    }
  }
}
