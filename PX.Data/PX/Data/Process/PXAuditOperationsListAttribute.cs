// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.PXAuditOperationsListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Process;

internal class PXAuditOperationsListAttribute : PXStringListAttribute
{
  internal const string Insert = "I";
  internal const string Update = "U";
  internal const string Delete = "D";
  internal const string Archive = "A";
  internal const string Extract = "E";

  public PXAuditOperationsListAttribute()
    : base(new string[5]{ "I", "U", "D", "A", "E" }, new string[5]
    {
      "Created",
      "Modified",
      "Deleted",
      "Archived",
      "Extracted"
    })
  {
  }

  internal static AuditOperation GetOperation(string code)
  {
    switch (code)
    {
      case "I":
        return AuditOperation.Insert;
      case "U":
        return AuditOperation.Update;
      case "D":
        return AuditOperation.Delete;
      case "A":
        return AuditOperation.Archive;
      case "E":
        return AuditOperation.Extract;
      default:
        throw new Exception();
    }
  }
}
