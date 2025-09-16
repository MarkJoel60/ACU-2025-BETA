// Decompiled with JetBrains decompiler
// Type: PX.SM.IWikiMaintGraph
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.SM;

/// <exclude />
internal interface IWikiMaintGraph
{
  void OnWikiCreated(WikiDescriptor rec);

  void OnWikiUpdated(WikiDescriptor rec);

  void OnWikiDeleted(WikiDescriptor rec);

  Type PageType { get; }
}
