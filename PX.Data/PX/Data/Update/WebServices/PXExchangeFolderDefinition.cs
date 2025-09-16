// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeFolderDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.ExchangeService;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeFolderDefinition : PXExchangeItemBase
{
  public readonly string Name;
  public readonly DistinguishedFolderIdNameType Parent;
  public readonly string CachedID;
  public readonly object Tag;

  public PXExchangeFolderDefinition(
    string mailbox,
    DistinguishedFolderIdNameType parent,
    string name,
    string cached,
    object tag = null)
    : base(mailbox)
  {
    this.Name = name;
    this.Parent = parent;
    this.CachedID = cached;
    this.Tag = tag;
  }
}
