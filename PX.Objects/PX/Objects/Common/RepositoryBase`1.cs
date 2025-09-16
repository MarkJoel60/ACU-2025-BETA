// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.RepositoryBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// A base class for repository objects capable of retrieving entities
/// from the database according to different restrictions. Optionally,
/// uses a <see cref="T:PX.Data.PXGraph" /> object to leverage Acumatica platform's
/// caching mechanisms.
/// </summary>
/// <typeparam name="TNode">The type of objects retrieved from the database.</typeparam>
public class RepositoryBase<TNode> where TNode : class, IBqlTable, new()
{
  protected readonly PXGraph _graph;
  protected string _itemName;

  public RepositoryBase(PXGraph graph)
  {
    this._graph = graph ?? new PXGraph();
    this._itemName = PXUIFieldAttribute.GetItemName(this._graph.Caches[typeof (TNode)]);
  }

  protected TNode ForceNotNull(TNode record, string valueDescription = null)
  {
    if ((object) record != null)
      return record;
    if (valueDescription == null)
      throw new PXException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) this._itemName
      });
    throw new PXException("{0} '{1}' cannot be found in the system.", new object[2]
    {
      (object) this._itemName,
      (object) valueDescription
    });
  }

  public PXResultset<TNode> Select<Where>(params object[] parameters) where Where : IBqlWhere, new()
  {
    return PXSelectBase<TNode, PXSelect<TNode, Where>.Config>.Select(this._graph, parameters);
  }

  public TNode SelectSingle<Where>(params object[] parameters) where Where : IBqlWhere, new()
  {
    return PXResultset<TNode>.op_Implicit(PXSelectBase<TNode, PXSelect<TNode, Where>.Config>.SelectWindowed(this._graph, 0, 1, parameters));
  }

  public TNode SelectSingle<Where, OrderBy>(params object[] parameters)
    where Where : IBqlWhere, new()
    where OrderBy : IBqlOrderBy, new()
  {
    return PXResultset<TNode>.op_Implicit(PXSelectBase<TNode, PXSelect<TNode, Where, OrderBy>.Config>.SelectWindowed(this._graph, 0, 1, parameters));
  }
}
