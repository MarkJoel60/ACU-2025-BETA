// Decompiled with JetBrains decompiler
// Type: PX.Data.PXImagesListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Sets a drop-down list as the input control for a DAC field.
/// In this control, a user selects a value from a fixed set of strings.
/// Every possible value is accompanied by an image in the drop-down list.
/// </summary>
public class PXImagesListAttribute : PXStringListAttribute
{
  protected string[] _AllowedImages;

  public override bool IsLocalizable => true;

  public PXImagesListAttribute()
  {
  }

  /// <summary>
  /// Creates a drop-down list with images displayed for each item.
  /// </summary>
  /// <param name="allowedValues">Specifies possible values to select.</param>
  /// <param name="allowedLabels">Specifies the text labels of the list items.</param>
  /// <param name="allowedImages">Specifies the images for the list items as the members
  /// of the <tt>PX.Web.UI.Sprite</tt> class.</param>
  public PXImagesListAttribute(
    string[] allowedValues,
    string[] allowedLabels,
    string[] allowedImages)
    : base(allowedValues, allowedLabels)
  {
    if (allowedValues.Length != allowedImages.Length)
      throw new PXArgumentException(nameof (allowedImages), "The length of the values array is not equal to the length of labels array.");
    this._AllowedImages = allowedImages;
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnState is PXStringState returnState) || this._AllowedImages == null)
      return;
    returnState.AllowedImages = this._AllowedImages;
  }
}
