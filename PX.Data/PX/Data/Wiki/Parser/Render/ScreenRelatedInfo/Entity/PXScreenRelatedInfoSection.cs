// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.Entity.PXScreenRelatedInfoSection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.Entity;

[PXInternalUseOnly]
public class PXScreenRelatedInfoSection
{
  public string Header { get; set; }

  public ICollection<PXScreenRelatedInfoLink> Links { get; private set; }

  public PXScreenRelatedInfoSection()
  {
    this.Links = (ICollection<PXScreenRelatedInfoLink>) new LinkedList<PXScreenRelatedInfoLink>();
  }
}
