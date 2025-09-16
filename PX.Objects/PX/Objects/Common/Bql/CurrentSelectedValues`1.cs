// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Bql.CurrentSelectedValues`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Bql;

public class CurrentSelectedValues<TField> : 
  IBqlConstantsOf<IImplement<IBqlEquitable>>,
  IBqlConstants,
  IBqlConstantsOf<IImplement<IBqlCastableTo<IBqlString>>>
  where TField : IBqlField
{
  private const string MultiSelectSeparator = ",";

  public IEnumerable<object> GetValues(PXGraph graph)
  {
    if (graph == null)
      return (IEnumerable<object>) Array<string>.Empty;
    PXCache cach = graph.Caches[BqlCommand.GetItemType<TField>()];
    string str = (string) cach.GetValue<TField>(cach.Current);
    if (string.IsNullOrEmpty(str))
      return (IEnumerable<object>) Array<string>.Empty;
    return (IEnumerable<object>) str.Split(new string[1]
    {
      ","
    }, StringSplitOptions.RemoveEmptyEntries);
  }
}
