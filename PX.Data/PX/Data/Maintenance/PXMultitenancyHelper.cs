// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.PXMultitenancyHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Maintenance;

internal static class PXMultitenancyHelper
{
  public static void ExecuteOnAllDB(System.Action action)
  {
    IEnumerable<IPXMultiDatabaseUser> multiDatabaseUsers = (IEnumerable<IPXMultiDatabaseUser>) PXAccess.GetMultiDatabaseUsers();
    if (multiDatabaseUsers == null)
    {
      action();
    }
    else
    {
      HashSet<string> stringSet = new HashSet<string>();
      foreach (IPXMultiDatabaseUser multiDatabaseUser in multiDatabaseUsers)
      {
        string companyId = multiDatabaseUser.GetCompanyID();
        using (new PXLoginScope(string.IsNullOrEmpty(companyId) ? multiDatabaseUser.GetUserName() : $"{multiDatabaseUser.GetUserName()}@{companyId}", Array.Empty<string>()))
        {
          multiDatabaseUser.Initialize();
          string connectionString = PXAccess.GetConnectionString();
          if (!stringSet.Contains(connectionString))
          {
            stringSet.Add(connectionString);
            action();
          }
        }
      }
    }
  }
}
