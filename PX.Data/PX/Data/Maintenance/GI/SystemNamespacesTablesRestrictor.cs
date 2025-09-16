// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.SystemNamespacesTablesRestrictor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Maintenance.GI;

internal class SystemNamespacesTablesRestrictor : IPXSchemaTableRestrictor
{
  public bool IsAllowed(System.Type table)
  {
    string str = !(table == (System.Type) null) ? table.FullName : throw new ArgumentNullException(nameof (table));
    return !str.StartsWith("Customization.") && !str.StartsWith("PX.Web.Controls.") && !str.StartsWith("PX.Translation.");
  }
}
