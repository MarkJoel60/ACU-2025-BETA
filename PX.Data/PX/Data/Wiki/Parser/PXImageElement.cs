// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXImageElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents an image element in memory.</summary>
public class PXImageElement : PXElement, IWidthHeightSettable
{
  private string name = "";
  private ImageType type;
  private ImageLocation location;
  private int width = -1;
  private int height = -1;
  private string caption;
  private bool isClickable = true;
  private string navigateUrl;
  private string wikiText;
  private string props;
  private string internalLink;
  private bool isEmbedded;
  private bool hasEditLink = true;
  private int? fileRevision;

  /// <summary>Initializes a new instance of the PXImageElement</summary>
  /// <param name="name">Name of picture to be retrieved from storage.</param>
  public PXImageElement(string name) => this.name = name;

  /// <summary>Gets name of picture to be retrieved from storage.</summary>
  public string Name => this.name;

  /// <summary>Gets or sets image caption.</summary>
  public string Caption
  {
    get => this.caption;
    set => this.caption = HttpUtility.HtmlEncode(value);
  }

  /// <summary>Gets or sets image type.</summary>
  public ImageType Type
  {
    get => this.type;
    set => this.type = value;
  }

  /// <summary>Gets or sets image location on a page.</summary>
  public ImageLocation Location
  {
    get => this.location;
    set => this.location = value;
  }

  /// <summary>Gets or sets image width.</summary>
  public int Width
  {
    get => this.width;
    set => this.width = value;
  }

  /// <summary>Gets or sets image height.</summary>
  public int Height
  {
    get => this.height;
    set => this.height = value;
  }

  /// <summary>
  /// Gets or sets value indicating, whether user will be allowed to click on image in order to see its full size.
  /// </summary>
  public bool IsClickable
  {
    get => this.isClickable;
    set => this.isClickable = value;
  }

  /// <summary>
  /// Gets or sets an external url to which user will be navigated when clicking an image.
  /// </summary>
  public string NavigateUrl
  {
    get => this.navigateUrl;
    set => this.navigateUrl = value;
  }

  /// <summary>
  /// Gets or sets wiki representation of the current image which is used for WYSIWYG editor.
  /// </summary>
  public string WikiText
  {
    get => this.wikiText;
    set => this.wikiText = value;
  }

  /// <summary>
  /// Gets or sets name of article to navigate to when user clicks this image.
  /// </summary>
  public string InternalLink
  {
    get => this.internalLink;
    set => this.internalLink = value;
  }

  /// <summary>
  /// Gets or sets any additional HTML attributes for the current image.
  /// </summary>
  public string Props
  {
    get => this.props;
    set => this.props = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether this image should be displayed as embedded into page
  /// when its type is video.
  /// </summary>
  public bool IsEmbedded
  {
    get => this.isEmbedded;
    set => this.isEmbedded = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether image has "edit" link (when user has appropriate access rights).
  /// </summary>
  public bool HasEditLink
  {
    get => this.hasEditLink;
    set => this.hasEditLink = value;
  }

  /// <summary>Gets or sets number of file revision.</summary>
  public int? FileRevision
  {
    get => this.fileRevision;
    set => this.fileRevision = value;
  }
}
