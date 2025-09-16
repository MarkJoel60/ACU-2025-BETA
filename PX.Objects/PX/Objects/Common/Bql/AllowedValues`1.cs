// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Bql.AllowedValues`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.Bql;

public class AllowedValues<TField> : 
  IBqlConstantsOf<IImplement<IBqlEquitable>>,
  IBqlConstants,
  IBqlConstantsOf<IImplement<IBqlCastableTo<IBqlString>>>
  where TField : IBqlField
{
  public IEnumerable<object> GetValues(PXGraph graph)
  {
    if (graph == null)
      return (IEnumerable<object>) Array<string>.Empty;
    PXCache cach = graph.Caches[BqlCommand.GetItemType<TField>()];
    return (IEnumerable<object>) this.GetAllowedValues(cach, cach.Current);
  }

  protected string[] GetAllowedValues(PXCache cache, object row)
  {
    PXStringListAttribute stringListAttribute = cache.GetAttributesReadonly<TField>(row).OfType<PXStringListAttribute>().FirstOrDefault<PXStringListAttribute>();
    return stringListAttribute == null ? Array<string>.Empty : stringListAttribute.GetAllowedValues(cache);
  }
}
