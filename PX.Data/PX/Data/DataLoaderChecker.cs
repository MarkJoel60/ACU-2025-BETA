// Decompiled with JetBrains decompiler
// Type: PX.Data.DataLoaderChecker
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Model;
using PX.DbServices.Model.ImportExport;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.Data;

public class DataLoaderChecker(PointDbmsBase point, ExportTemplate relations) : DataLoaderBase(point, relations)
{
  public bool DoHaveParent(YaqlCondition filter)
  {
    try
    {
      int? parent = CompanyHeader.getParent(new int?(PXDatabase.Provider.getCompanyID(this.Relations.RootTable, out companySetting _)) ?? 1, this.hierarchy);
      return this.point.GetTable(this.Relations.RootTable, FileMode.Open).ReadRows(Yaql.and(filter, this.getCompanyRestriction(parent, this.Relations.RootTable, true)), (Dictionary<string, object>) null, (IEnumerable<YaqlScalar>) null).Count<object[]>() > 0;
    }
    catch (Exception ex)
    {
      return false;
    }
  }
}
