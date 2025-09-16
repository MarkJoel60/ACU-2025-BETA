// Decompiled with JetBrains decompiler
// Type: PX.Data.DeletedRecordsTracking.DeleteTranInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable enable
namespace PX.Data.DeletedRecordsTracking;

internal class DeleteTranInfo
{
  public required string TableName { get; set; }

  public required string DacName { get; set; }

  public Guid NoteId { get; set; }

  public int CompanyId { get; set; }

  public override bool Equals(object obj)
  {
    return obj is DeleteTranInfo deleteTranInfo && this.TableName == deleteTranInfo.TableName && this.DacName == deleteTranInfo.DacName && this.NoteId == deleteTranInfo.NoteId && this.CompanyId == this.CompanyId;
  }

  public override int GetHashCode()
  {
    return this.TableName.GetHashCode() ^ this.DacName.GetHashCode() ^ this.NoteId.GetHashCode() ^ this.CompanyId;
  }
}
