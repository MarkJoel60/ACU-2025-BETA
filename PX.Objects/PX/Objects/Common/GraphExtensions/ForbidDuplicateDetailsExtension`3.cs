// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.ForbidDuplicateDetailsExtension`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.GraphExtensions;

public abstract class ForbidDuplicateDetailsExtension<TGraph, TDocument, TDetail> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, new()
  where TDetail : class, IBqlTable, new()
{
  private PXCache<TDocument> _documentCache;
  private PXCache<TDetail> _detailsCache;

  protected PXCache<TDocument> DocumentCache
  {
    get
    {
      return this._documentCache ?? (this._documentCache = GraphHelper.Caches<TDocument>((PXGraph) this.Base));
    }
  }

  protected PXCache<TDetail> DetailsCache
  {
    get
    {
      return this._detailsCache ?? (this._detailsCache = GraphHelper.Caches<TDetail>((PXGraph) this.Base));
    }
  }

  protected abstract IEnumerable<Type> GetDetailUniqueFields();

  protected abstract void RaiseDuplicateError(TDetail duplicate);

  protected virtual bool ForbidDuplicates(TDocument document)
  {
    return (object) document != null && ((PXCache) this.DetailsCache).IsDirty && EnumerableExtensions.IsNotIn<PXEntryStatus>(this.DocumentCache.GetStatus(document), (PXEntryStatus) 3, (PXEntryStatus) 4);
  }

  protected virtual PXSelect<TDetail> InitializeDuplicatesLoadQuery(TDetail detail)
  {
    PXSelect<TDetail> query = new PXSelect<TDetail>((PXGraph) this.Base);
    List<Type> list = ((PXCache) this.DetailsCache).BqlKeys.ToList<Type>();
    foreach (Type detailUniqueField in this.GetDetailUniqueFields())
    {
      if (list.Contains(detailUniqueField))
        list.Remove(detailUniqueField);
      if (((PXCache) this.DetailsCache).GetValue((object) detail, detailUniqueField.Name) == null)
        andIsNull(detailUniqueField);
      else
        andEqualCurrent(detailUniqueField);
    }
    Type type1 = (Type) null;
    foreach (Type type2 in list)
    {
      Type type3 = BqlCommand.Compose(new Type[5]
      {
        typeof (Where<,>),
        type2,
        typeof (NotEqual<>),
        typeof (Current<>),
        type2
      });
      Type type4;
      if (!(type1 == (Type) null))
        type4 = BqlCommand.Compose(new Type[4]
        {
          typeof (Where2<,>),
          type1,
          typeof (Or<>),
          type3
        });
      else
        type4 = type3;
      type1 = type4;
    }
    if (type1 != (Type) null)
      ((PXSelectBase<TDetail>) query).WhereAnd(type1);
    return query;

    void andIsNull(Type field)
    {
      ((PXSelectBase<TDetail>) query).WhereAnd(BqlCommand.Compose(new Type[3]
      {
        typeof (Where<,>),
        field,
        typeof (IsNull)
      }));
    }

    void andEqualCurrent(Type field)
    {
      ((PXSelectBase<TDetail>) query).WhereAnd(BqlCommand.Compose(new Type[5]
      {
        typeof (Where<,>),
        field,
        typeof (Equal<>),
        typeof (Current<>),
        field
      }));
    }
  }

  protected virtual TDetail[] LoadDetails(TDocument document)
  {
    return PXParentAttribute.SelectChildren((PXCache) this.DetailsCache, (object) document, typeof (TDocument)).OfType<TDetail>().ToArray<TDetail>();
  }

  public virtual void CheckForDuplicates()
  {
    TDocument current = (TDocument) ((PXCache) this.DocumentCache).Current;
    if (!this.ForbidDuplicates(current))
      return;
    if (!NonGenericIEnumerableExtensions.Any_(((PXCache) this.DetailsCache).Deleted))
    {
      TDetail[] array1 = ((PXCache) this.DetailsCache).Inserted.OfType<TDetail>().ToArray<TDetail>();
      TDetail[] array2 = ((PXCache) this.DetailsCache).Updated.OfType<TDetail>().ToArray<TDetail>();
      if (((IEnumerable<TDetail>) array1).Any<TDetail>() && !((IEnumerable<TDetail>) array2).Any<TDetail>() && array1.Length <= 5 || ((IEnumerable<TDetail>) array2).Any<TDetail>() && !((IEnumerable<TDetail>) array1).Any<TDetail>() && array2.Length == 1)
      {
        TDetail[] array3 = ((IEnumerable<TDetail>) array1).Concat<TDetail>((IEnumerable<TDetail>) array2).ToArray<TDetail>();
        if (this.CheckForDuplicates(array3) || this.DocumentCache.GetStatus(current) == 2)
          return;
        foreach (TDetail detail in array3)
          this.CheckForDuplicateOnDB(detail);
        return;
      }
    }
    this.CheckForDuplicates(this.LoadDetails(current));
  }

  public virtual bool CheckForDuplicates(TDetail[] details)
  {
    if (((IEnumerable<TDetail>) details).Count<TDetail>() <= 1)
      return false;
    IEqualityComparer<TDetail> comparer = (IEqualityComparer<TDetail>) new FieldSubsetEqualityComparer<TDetail>((PXCache) this.DetailsCache, this.GetDetailUniqueFields());
    IEnumerable<TDetail> source = ((IEnumerable<TDetail>) details).GroupBy<TDetail, TDetail>((Func<TDetail, TDetail>) (detail => detail), comparer).Where<IGrouping<TDetail, TDetail>>((Func<IGrouping<TDetail, TDetail>, bool>) (duplicatesGroup => duplicatesGroup.HasAtLeastTwoItems<TDetail>())).Flatten<TDetail, TDetail>();
    foreach (TDetail duplicate in source)
      this.RaiseDuplicateError(duplicate);
    return source.Any<TDetail>();
  }

  public virtual bool CheckForDuplicateOnDB(TDetail detail)
  {
    if ((object) (TDetail) ((PXSelectBase) this.InitializeDuplicatesLoadQuery(detail)).View.SelectSingleBound((object[]) new TDetail[1]
    {
      detail
    }, Array.Empty<object>()) == null)
      return false;
    this.RaiseDuplicateError(detail);
    return true;
  }
}
