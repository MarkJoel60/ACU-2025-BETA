// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.SubItemRawExtAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.IN;

[PXDBString(30, IsUnicode = true, InputMask = "")]
[PXUIField]
public class SubItemRawExtAttribute : PXEntityAttribute
{
  public const string DimensionName = "INSUBITEM";

  public SubItemRawExtAttribute()
  {
  }

  public SubItemRawExtAttribute(Type inventoryItem)
    : this()
  {
    if (!(inventoryItem != (Type) null))
      return;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("INSUBITEM", BqlCommand.Compose(new Type[15]
    {
      typeof (Search<,>),
      typeof (INSubItem.subItemCD),
      typeof (Where2<,>),
      typeof (Match<>),
      typeof (Current<AccessInfo.userName>),
      typeof (And<>),
      typeof (Where<,,>),
      typeof (Optional<>),
      inventoryItem,
      typeof (IsNull),
      typeof (Or<>),
      typeof (Where<>),
      typeof (Match<>),
      typeof (Optional<>),
      inventoryItem
    }))
    {
      ValidComboRequired = false
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.subItem>())
    {
      ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1]).ValidComboRequired = false;
      ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1]).SetSegmentDelegate((Delegate) null);
      PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
      Type itemType = sender.GetItemType();
      string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
      SubItemRawExtAttribute itemRawExtAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) itemRawExtAttribute, __vmethodptr(itemRawExtAttribute, FieldDefaulting));
      fieldDefaulting.AddHandler(itemType, fieldName, pxFieldDefaulting);
    }
    ((PXAggregateAttribute) this).CacheAttached(sender);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Definitions.DefaultSubItemCD;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (!(typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber)))
      return;
    subscribers.Clear();
  }

  protected virtual SubItemRawExtAttribute.Definition Definitions
  {
    get
    {
      SubItemRawExtAttribute.Definition definitions = PXContext.GetSlot<SubItemRawExtAttribute.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<SubItemRawExtAttribute.Definition>(PXDatabase.GetSlot<SubItemRawExtAttribute.Definition>("INSubItem.DefinitionCD", new Type[1]
        {
          typeof (INSubItem)
        }));
      return definitions;
    }
  }

  protected class Definition : IPrefetchable, IPXCompanyDependent
  {
    private string _DefaultSubItemCD;

    public string DefaultSubItemCD => this._DefaultSubItemCD;

    public void Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<INSubItem>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<INSubItem.subItemCD>(),
        (PXDataField) new PXDataFieldOrder<INSubItem.subItemID>()
      }))
      {
        this._DefaultSubItemCD = (string) null;
        if (pxDataRecord == null)
          return;
        this._DefaultSubItemCD = pxDataRecord.GetString(0);
      }
    }
  }
}
