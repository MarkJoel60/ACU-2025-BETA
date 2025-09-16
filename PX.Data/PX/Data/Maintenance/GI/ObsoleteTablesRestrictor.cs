// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.ObsoleteTablesRestrictor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Reflection;

#nullable disable
namespace PX.Data.Maintenance.GI;

internal class ObsoleteTablesRestrictor : IPXSchemaTableRestrictor
{
  public bool IsAllowed(System.Type table)
  {
    if (table == (System.Type) null)
      throw new ArgumentNullException(nameof (table));
    return !Attribute.IsDefined((MemberInfo) table, typeof (ObsoleteAttribute), false);
  }
}
