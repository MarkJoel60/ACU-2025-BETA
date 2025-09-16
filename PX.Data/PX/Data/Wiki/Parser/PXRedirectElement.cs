// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXRedirectElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents a redirect element in memory.</summary>
internal class PXRedirectElement : PXElement
{
  private PXLinkElement link;

  public PXRedirectElement(PXLinkElement link) => this.link = link;

  public PXLinkElement RedirectLink => this.link;
}
