// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXAbsoluteSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System.Collections.Generic;
using System.Configuration;

#nullable disable
namespace PX.Data.Wiki.Parser;

public sealed class PXAbsoluteSettings : ISettings
{
  private readonly string _editLinkText;
  private readonly string _closeLinkText;
  private string _rootPath;
  private string _GetCSSUrl;
  private IDictionary<string, string> _processedExtImages;
  private string _playFlashIconUrl;

  public PXAbsoluteSettings()
  {
    this._editLinkText = "Edit";
    this._closeLinkText = "Close";
  }

  public bool NamedLinks => false;

  public string EditLinkText => this._editLinkText;

  public string CloseLinkText => this._closeLinkText;

  public string HintImageUrl => this.RootPath + "/App_Themes/Default/Images/Wiki//info.png";

  public string WarnImageUrl => this.RootPath + "/App_Themes/Default/Images/Wiki//Warn.png";

  public bool IsHtml { get; set; }

  public string DefaultStylesPath
  {
    get
    {
      string str1 = PXExecutionContext.Current.Request.ApplicationPath;
      if (!string.IsNullOrEmpty(str1))
        str1 = str1.TrimEnd('/') + "/";
      string str2 = ConfigurationManager.AppSettings["DefaultWikiCSS"];
      if (!string.IsNullOrEmpty(str2))
        str2 = str2.TrimStart('/');
      return str1 + str2;
    }
  }

  public string GetCSSUrl
  {
    get
    {
      return string.IsNullOrEmpty(this._GetCSSUrl) ? this.RootPath + "/App_Themes/GetCSS.aspx" : this._GetCSSUrl;
    }
    set => this._GetCSSUrl = value;
  }

  public string ArticleShowUrl => this.RootPath + "/Wiki/ShowWiki.aspx";

  public string GetFileUrl => this.RootPath + "/Frames/GetFile.ashx";

  public string FileEditUrl => this.RootPath + "/Pages/SM/SM202510.aspx";

  public string MagnifyImageUrl => string.Empty;

  public bool FilesDirectAccess => false;

  public string PlayFlashIconUrl
  {
    get => this._playFlashIconUrl;
    set => this._playFlashIconUrl = value;
  }

  public string GetRSSUrl => this.RootPath + "/Frames/GetRSS.ashx";

  public string RSSImageUrl => this.RootPath + "/App_Themes/Default/Images/Wiki/rss.gif";

  public string RootUrl => this.RootPath;

  public string ExternalRootUrl => this.RootPath;

  public string SearchLink
  {
    get => string.Empty;
    set
    {
    }
  }

  public string SearchUnknownLink
  {
    get => string.Empty;
    set
    {
    }
  }

  public string DefaultExtensionImage
  {
    get => this.RootPath + "/App_Themes/Default/Images/Wiki/binary.gif";
  }

  public IDictionary<string, string> ExtensionsImages
  {
    get
    {
      if (this._processedExtImages == null)
      {
        this._processedExtImages = (IDictionary<string, string>) new Dictionary<string, string>();
        string rootPath = this.RootPath;
        foreach (UploadAllowedFileTypes allowedFileType in SitePolicy.AllowedFileTypes)
        {
          if (!string.IsNullOrEmpty(allowedFileType.IconUrl))
            this._processedExtImages.Add(allowedFileType.FileExt.ToLower(), rootPath + allowedFileType.IconUrl.Replace("~", "/"));
        }
      }
      return this._processedExtImages;
    }
  }

  private string RootPath
  {
    get
    {
      if (this._rootPath == null)
      {
        PXExecutionContext.RequestInfo request = PXExecutionContext.Current.Request;
        this._rootPath = $"{request.Scheme}://{request.Authority}{(request.ApplicationPath != "/" ? (object) request.ApplicationPath : (object) string.Empty)}";
      }
      return this._rootPath;
    }
  }
}
