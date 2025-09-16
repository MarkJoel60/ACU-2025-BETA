// Decompiled with JetBrains decompiler
// Type: PX.SM.IWikiPageMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.SM;

/// <exclude />
public interface IWikiPageMaint
{
  void InitNew(Guid wikiid);

  void InitNew(Guid? wikiid, Guid? parentid, string name);

  string GetFileUrl { get; set; }

  Guid CurrentAttachmentGuid { get; set; }
}
