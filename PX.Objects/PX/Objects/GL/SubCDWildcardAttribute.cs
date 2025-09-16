// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SubCDWildcardAttribute
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
namespace PX.Objects.GL;

[PXDBString(30, IsUnicode = true, InputMask = "")]
[PXUIField]
public sealed class SubCDWildcardAttribute : PXDimensionWildcardAttribute
{
  private int _UIAttrIndex = -1;
  private const string _DimensionName = "SUBACCOUNT";

  private void Initialize()
  {
    this._UIAttrIndex = -1;
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes)
    {
      if (attribute is PXUIFieldAttribute)
        this._UIAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).IndexOf(attribute);
    }
  }

  public SubCDWildcardAttribute()
    : base("SUBACCOUNT", typeof (Sub.subCD))
  {
    (((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 2] as PXDimensionAttribute).ValidComboRequired = false;
    this.Initialize();
  }

  public string DisplayName
  {
    get
    {
      return this._UIAttrIndex != -1 ? ((PXUIFieldAttribute) ((PXAggregateAttribute) this)._Attributes[this._UIAttrIndex]).DisplayName : (string) null;
    }
    set
    {
      if (this._UIAttrIndex == -1)
        return;
      ((PXUIFieldAttribute) ((PXAggregateAttribute) this)._Attributes[this._UIAttrIndex]).DisplayName = value;
    }
  }

  public virtual void CacheAttached(PXCache sender)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
    {
      // ISSUE: method pointer
      sender.Graph.FieldDefaulting.AddHandler(sender.GetItemType(), ((PXEventSubscriberAttribute) this)._FieldName, new PXFieldDefaulting((object) this, __methodptr(FieldDefaulting)));
    }
    ((PXAggregateAttribute) this).CacheAttached(sender);
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel)
      return;
    e.NewValue = (object) this.GetDefaultSubID(sender, e.Row);
    ((CancelEventArgs) e).Cancel = true;
  }

  private string GetDefaultSubID(PXCache sender, object row)
  {
    if (this.Definitions.DefaultSubCD != null)
      return this.Definitions.DefaultSubCD;
    object defaultSubId = (object) "0";
    sender.RaiseFieldUpdating(((PXEventSubscriberAttribute) this)._FieldName, row, ref defaultSubId);
    return (string) defaultSubId;
  }

  private SubCDWildcardAttribute.Definition Definitions
  {
    get
    {
      SubCDWildcardAttribute.Definition definitions = PXContext.GetSlot<SubCDWildcardAttribute.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<SubCDWildcardAttribute.Definition>(PXDatabase.GetSlot<SubCDWildcardAttribute.Definition>(typeof (SubCDWildcardAttribute.Definition).FullName, new Type[1]
        {
          typeof (Sub)
        }));
      return definitions;
    }
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    private string _DefaultSubCD;

    public string DefaultSubCD => this._DefaultSubCD;

    public void Prefetch()
    {
      this._DefaultSubCD = (string) null;
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Sub>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<Sub.subCD>(),
        (PXDataField) new PXDataFieldOrder<Sub.subCD>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this._DefaultSubCD = pxDataRecord.GetString(0);
      }
    }
  }
}
