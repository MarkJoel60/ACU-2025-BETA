// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.DistinguishedFolderIdNameType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public enum DistinguishedFolderIdNameType
{
  /// <remarks />
  calendar,
  /// <remarks />
  contacts,
  /// <remarks />
  deleteditems,
  /// <remarks />
  drafts,
  /// <remarks />
  inbox,
  /// <remarks />
  journal,
  /// <remarks />
  notes,
  /// <remarks />
  outbox,
  /// <remarks />
  sentitems,
  /// <remarks />
  tasks,
  /// <remarks />
  msgfolderroot,
  /// <remarks />
  publicfoldersroot,
  /// <remarks />
  root,
  /// <remarks />
  junkemail,
  /// <remarks />
  searchfolders,
  /// <remarks />
  voicemail,
  /// <remarks />
  recoverableitemsroot,
  /// <remarks />
  recoverableitemsdeletions,
  /// <remarks />
  recoverableitemsversions,
  /// <remarks />
  recoverableitemspurges,
  /// <remarks />
  archiveroot,
  /// <remarks />
  archivemsgfolderroot,
  /// <remarks />
  archivedeleteditems,
  /// <remarks />
  archiveinbox,
  /// <remarks />
  archiverecoverableitemsroot,
  /// <remarks />
  archiverecoverableitemsdeletions,
  /// <remarks />
  archiverecoverableitemsversions,
  /// <remarks />
  archiverecoverableitemspurges,
  /// <remarks />
  syncissues,
  /// <remarks />
  conflicts,
  /// <remarks />
  localfailures,
  /// <remarks />
  serverfailures,
  /// <remarks />
  recipientcache,
  /// <remarks />
  quickcontacts,
  /// <remarks />
  conversationhistory,
  /// <remarks />
  adminauditlogs,
  /// <remarks />
  todosearch,
  /// <remarks />
  mycontacts,
  /// <remarks />
  directory,
  /// <remarks />
  imcontactlist,
  /// <remarks />
  peopleconnect,
  /// <remarks />
  favorites,
}
