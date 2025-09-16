// Decompiled with JetBrains decompiler
// Type: PX.Data.PXHeaderImageAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXHeaderImageAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  protected string _HeaderImage = "";

  public PXHeaderImageAttribute(string headerImage) => this._HeaderImage = headerImage;

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXHeaderImageState.CreateInstance(e.ReturnState, this.FieldName, this._HeaderImage);
  }
}
