// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.IGroupedCollection`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Scopes;

public interface IGroupedCollection<TEntity> : 
  ICollection<TEntity>,
  IEnumerable<TEntity>,
  IEnumerable
  where TEntity : class, IBqlTable, new()
{
  PXCache Cache { get; }

  void Insert(TEntity data);

  void InsertRange(TEntity group, IEnumerable<TEntity> data);

  void Update(TEntity oldData, TEntity newData);

  void Delete(TEntity data);

  IEnumerable<TEntity> GetItems(TEntity group);

  IEnumerable<TEntity> LoadItems(TEntity group);

  IGroupedCollection<TEntity> Clone();
}
