// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.StorageServiceStub
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.Data.Services;

/// <exclude />
public class StorageServiceStub : IStorageService
{
  public IEnumerable<Assembly> LoadCustomStorageProviderAssemblies()
  {
    return (IEnumerable<Assembly>) new Assembly[0];
  }
}
