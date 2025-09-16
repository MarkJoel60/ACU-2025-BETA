// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Repositories.IdentityRepository`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common.Repositories;

/// <summary>
/// Repository providing basic methods for tables that have a unique ID field.
/// </summary>
public class IdentityRepository<TNode, TIdentityField>(PXGraph graph) : RepositoryBase<TNode>(graph)
  where TNode : class, IBqlTable, new()
  where TIdentityField : IBqlField
{
  public TNode FindByID(object id)
  {
    return this.SelectSingle<Where<TIdentityField, Equal<Required<TIdentityField>>>>(id);
  }

  public TNode GetByID(object id) => this.ForceNotNull(this.FindByID(id));
}
