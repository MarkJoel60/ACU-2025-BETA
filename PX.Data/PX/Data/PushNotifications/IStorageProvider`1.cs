// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.IStorageProvider`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.PushNotifications;

public interface IStorageProvider<T> : IStorageInfoProvider, IDisposable where T : class
{
  void SaveValue(Guid key, T value);

  bool TryGetValue(Guid key, out T value);

  void RemoveValue(Guid key);

  void Purge();
}
