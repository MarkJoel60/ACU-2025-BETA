// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.FbqlSelect`2
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using System;

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <summary>Includes the classes for fluent BQL data views.</summary>
/// <typeparam name="TSelf">The type of the data view.</typeparam>
/// <typeparam name="TTable">The DAC type.</typeparam>
public abstract class FbqlSelect<TSelf, TTable> : 
  FbqlCommand,
  IFbqlSelect<TTable>,
  IBqlSelect<TTable>,
  IBqlSelect
  where TSelf : FbqlSelect<TSelf, TTable>, new()
  where TTable : class, IBqlTable, new()
{
  internal FbqlSelect(FbqlParseResult parseResult)
    : base(BqlCommand.CreateInstance(new Type[1]
    {
      parseResult.BqlCommand
    }))
  {
    this.HasJoinBlock = parseResult.HasJoin;
    this.HasWhereBlock = parseResult.HasWhere;
    this.HasAggregateBlock = parseResult.HasAggregate;
    this.HasOrderBlock = parseResult.HasOrder;
  }

  public BqlCommand AddNewJoin(Type newJoin)
  {
    return ((IBqlSelect) this.InnerBqlCommand).AddNewJoin(newJoin);
  }

  public IBqlOrderBy GetOrderBy() => ((IBqlSelect) this.InnerBqlCommand).GetOrderBy();

  public bool HasJoinBlock { get; }

  public bool HasWhereBlock { get; }

  public bool HasAggregateBlock { get; }

  public bool HasOrderBlock { get; }

  /// <summary>
  /// Defines a data view for retrieving a particular data set from
  /// the database and provides the interface to the cache for inserting,
  /// updating, and deleting the data records.
  /// </summary>
  public class View : PXViewOf<TTable>.BasedOn<TSelf>
  {
    public View(PXGraph graph)
      : base(graph)
    {
    }

    public View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  /// <summary>
  /// <para>Defines a special data view used on processing webpages, which are intended for mass processing of data records.</para>
  /// <para>The <tt>ProcessingView</tt> type is used to define the data view in a graph bound to a processing webpage. A data view of this type includes
  /// definitions of two actions, <tt>Process</tt> and <tt>ProcessAll</tt>, which are added to the graph and are used to invoke the processing. You should set the
  /// processing method by invoking one of the <tt>SetProcessDelegate</tt> methods in the constructor of the graph.</para>
  /// </summary>
  /// <example>
  /// The code below shows definition of the graph that contains the
  /// processing data view.
  /// <code title="" description="" lang="CS">
  /// public class ARPaymentsProcessing : PXGraph&lt;ARPaymentsProcessing&gt;
  /// {
  ///   // Definition of the data view to process
  ///   public SelectFrom&lt;ARPaymentInfo&gt;.ProcessingView ARDocumentList;
  ///   // The constructor of the graph
  ///   public ARPaymentsAutoProcessing()
  ///   {
  ///     // Specifying the field to mark data records for processing
  ///     ARDocumentList.SetSelected&lt;ARPaymentInfo.selected&gt;();
  ///     // Setting the processing method
  ///     ARDocumentList.SetProcessDelegate(Process);
  ///   }
  ///   // The processing method (must be static)
  ///   public static void Process(List&lt;ARPaymentInfo&gt; products)
  ///   {
  ///     ...
  ///   }
  /// ...
  /// }</code></example>
  public class ProcessingView : PXProcessingViewOf<TTable>.BasedOn<TSelf>
  {
    public ProcessingView(PXGraph graph)
      : base(graph)
    {
    }

    public ProcessingView(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class SearchFor<TField> : SearchFor<TField>.In<TSelf> where TField : IBqlField
  {
  }
}
