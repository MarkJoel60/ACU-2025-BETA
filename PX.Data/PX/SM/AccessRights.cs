// Decompiled with JetBrains decompiler
// Type: PX.SM.AccessRights
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class AccessRights : Access, IPXAuditSource
{
  string IPXAuditSource.GetMainView() => "Roles";

  IEnumerable<Type> IPXAuditSource.GetAuditedTables()
  {
    yield return typeof (PX.SM.Roles);
    yield return typeof (RolesInGraph);
    yield return typeof (RolesInCache);
    yield return typeof (RolesInMember);
  }
}
