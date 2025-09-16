// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXWikiSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.SM;
using System;
using System.Web.UI;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>
/// Defines settings for all wiki pages.
/// Absolute context is for external usages (emails, export, etc.),
/// relative context is for internal representaion (within our application).
/// </summary>
public class PXWikiSettings
{
  protected Page page;
  protected WikiReader reader;

  public PXWikiSettings(Page page)
    : this(page, (WikiReader) null)
  {
  }

  public PXWikiSettings(Page page, WikiReader reader)
  {
    this.page = page;
    this.reader = reader;
  }

  public PXDBContext Absolute => new PXDBContext((ISettings) new PXAbsoluteSettings(), this.reader);

  public PXDBContext Relative
  {
    get
    {
      if (ServiceLocator.IsLocationProviderSet && this.page != null)
      {
        ISettings settings = ServiceLocator.Current.GetInstance<Func<Page, ISettings>>(nameof (Relative))(this.page);
        if (settings != null)
          return new PXDBContext(settings, this.reader);
      }
      return this.Absolute;
    }
  }
}
