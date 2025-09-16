// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PXDimensionValueLookupModeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL;

/// <summary>Attribute suppress the dimension's lookup mode</summary>
public sealed class PXDimensionValueLookupModeAttribute : PXDimensionAttribute
{
  public DimensionLookupMode LookupMode { get; set; }

  public PXDimensionValueLookupModeAttribute(string dimension, DimensionLookupMode lookupMode)
    : base(dimension)
  {
    this.LookupMode = lookupMode;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (((PXEventSubscriberAttribute) this)._AttributeLevel != 2 && !e.IsAltered)
      return;
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object returnState = e.ReturnState;
    string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
    PXSegment[] pxSegmentArray = this._Definition == null || !this._Definition.Dimensions.ContainsKey(this._Dimension) ? new PXSegment[0] : this._Definition.Dimensions[this._Dimension];
    string str;
    if (e.ReturnState is PXFieldState && !string.IsNullOrEmpty(((PXFieldState) e.ReturnState).ViewName))
      str = (string) null;
    else if (this._Definition.LookupModes[this._Dimension] != 2)
      str = $"_{this._Dimension}_Segments_";
    else
      str = $"{sender.GetItemType().Name}_{((PXEventSubscriberAttribute) this)._FieldName}_{this._Dimension}_Segments_";
    DimensionLookupMode? nullable1 = new DimensionLookupMode?(this.LookupMode);
    bool? nullable2 = new bool?(this.ValidComboRequired);
    string wildcard = this._Wildcard;
    PXFieldState instance = PXSegmentedState.CreateInstance(returnState, fieldName, pxSegmentArray, str, nullable1, nullable2, wildcard);
    selectingEventArgs.ReturnState = (object) instance;
    ((PXStringState) e.ReturnState).IsUnicode = true;
    ((PXFieldState) e.ReturnState).DescriptionName = typeof (PXDimensionAttribute.SegmentValue.descr).Name;
  }
}
