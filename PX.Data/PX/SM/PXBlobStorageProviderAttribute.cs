// Decompiled with JetBrains decompiler
// Type: PX.SM.PXBlobStorageProviderAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.SM;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class PXBlobStorageProviderAttribute : Attribute
{
  public readonly Type Provider;
  public readonly string Title;

  public PXBlobStorageProviderAttribute(Type provider, string title)
  {
    this.Provider = provider;
    this.Title = title;
  }
}
