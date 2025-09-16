// Decompiled with JetBrains decompiler
// Type: PX.SM.CurrentCompanies
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class CurrentCompanies : 
  IBqlConstants,
  IBqlConstantsOf<IImplement<IBqlEquitable>>,
  IBqlConstantsOf<IImplement<IBqlCastableTo<IBqlInt>>>,
  IBqlConstantsOf<IImplement<IBqlCastableTo<IBqlString>>>
{
  public IEnumerable<object> GetValues(PXGraph graph)
  {
    if (graph == null)
      return (IEnumerable<object>) Array<object>.Empty;
    string[] values = PXAccess.GetCompanies();
    if (values == null || values.Length == 0)
      values = new string[1]
      {
        PXDatabase.Provider.DbCompanies[0]
      };
    return (IEnumerable<object>) values;
  }
}
