// Decompiled with JetBrains decompiler
// Type: PX.Data.IAuditInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public interface IAuditInfo
{
  Guid? CreatedByID { get; set; }

  string CreatedByScreenID { get; set; }

  System.DateTime? CreatedDateTime { get; set; }

  Guid? LastModifiedByID { get; set; }

  string LastModifiedByScreenID { get; set; }

  System.DateTime? LastModifiedDateTime { get; set; }
}
