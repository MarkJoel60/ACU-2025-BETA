// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.IExpirationStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

internal interface IExpirationStorage
{
  bool IsExpired(object key);

  void ClearExpiration(object key);

  void UpdateExpiration(object key);
}
