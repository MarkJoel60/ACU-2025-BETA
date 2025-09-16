// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.IDtoBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.UserRecords.DTO;

#nullable disable
namespace PX.Data.UserRecords;

/// <summary>Interface the builder of user record.</summary>
public interface IDtoBuilder
{
  /// <summary>Creates record DTO.</summary>
  /// <param name="userRecord">The user record.</param>
  /// <param name="screenID">Identifier for the screen.</param>
  /// <returns />
  UserRecordResult CreateRecordDTO(IUserRecord userRecord, string screenID);
}
