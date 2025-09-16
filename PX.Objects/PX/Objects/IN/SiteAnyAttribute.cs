// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.SiteAnyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// Represents Site Field
/// The Selector will return all sites.
/// </summary>
[PXDBInt]
[PXUIField]
public class SiteAnyAttribute : PXEntityAttribute
{
  public const 
  #nullable disable
  string DimensionName = "INSITE";
  public bool SetDefaultValue = true;
  protected Type _whereType;

  public SiteAnyAttribute()
    : this(false)
  {
  }

  public SiteAnyAttribute(bool allowTransit)
    : this(typeof (Where<Match<Current<AccessInfo.userName>>>), false, allowTransit)
  {
  }

  public SiteAnyAttribute(Type WhereType, bool allowTransit)
    : this(WhereType, true, allowTransit)
  {
  }

  public SiteAnyAttribute(Type WhereType, bool validateAccess, bool allowTransit)
  {
    if (!(WhereType != (Type) null))
      return;
    this._whereType = WhereType;
    List<Type> typeList = new List<Type>();
    if (validateAccess)
    {
      typeList.Add(typeof (Search<,>));
      typeList.Add(typeof (INSite.siteID));
      typeList.Add(typeof (Where2<,>));
      typeList.Add(typeof (Match<>));
      typeList.Add(typeof (Current<AccessInfo.userName>));
      if (allowTransit)
      {
        typeList.Add(typeof (And<>));
      }
      else
      {
        typeList.Add(typeof (And<,,>));
        typeList.Add(typeof (INSite.siteID));
        typeList.Add(typeof (NotEqual<SiteAnyAttribute.transitSiteID>));
        typeList.Add(typeof (And<>));
      }
      typeList.Add(this._whereType);
    }
    else
    {
      typeList.Add(typeof (Search<,>));
      typeList.Add(typeof (INSite.siteID));
      if (!allowTransit)
      {
        typeList.Add(typeof (Where2<,>));
        typeList.Add(typeof (Where<,>));
        typeList.Add(typeof (INSite.siteID));
        typeList.Add(typeof (NotEqual<SiteAnyAttribute.transitSiteID>));
        typeList.Add(typeof (And<>));
      }
      typeList.Add(this._whereType);
    }
    Type type1 = BqlCommand.Compose(typeList.ToArray());
    PXAggregateAttribute.AggregatedAttributesCollection attributes = ((PXAggregateAttribute) this)._Attributes;
    Type type2 = type1;
    Type type3 = typeof (INSite.siteCD);
    Type[] typeArray = new Type[2]
    {
      typeof (INSite.siteCD),
      typeof (INSite.descr)
    };
    PXDimensionSelectorAttribute selectorAttribute1;
    PXDimensionSelectorAttribute selectorAttribute2 = selectorAttribute1 = new PXDimensionSelectorAttribute("INSITE", type2, type3, typeArray);
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
    selectorAttribute2.CacheGlobal = true;
    selectorAttribute2.DescriptionField = typeof (INSite.descr);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    if (this.SetDefaultValue && !PXAccess.FeatureInstalled<FeaturesSet.warehouse>() && PXAccess.FeatureInstalled<FeaturesSet.inventory>() && sender.Graph.GetType() != typeof (PXGraph))
    {
      if (!this.Definitions.DefaultSiteID.HasValue)
        ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).ValidComboRequired = false;
      PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
      Type itemType = sender.GetItemType();
      string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
      SiteAnyAttribute siteAnyAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) siteAnyAttribute, __vmethodptr(siteAnyAttribute, Feature_FieldDefaulting));
      fieldDefaulting.AddHandler(itemType, fieldName, pxFieldDefaulting);
    }
    ((PXAggregateAttribute) this).CacheAttached(sender);
  }

  public virtual void Feature_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel)
      return;
    if (!this.Definitions.DefaultSiteID.HasValue)
    {
      object obj = (object) "MAIN";
      sender.RaiseFieldUpdating(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
      e.NewValue = obj;
    }
    else
      e.NewValue = (object) this.Definitions.DefaultSiteID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual SiteAnyAttribute.Definition Definitions
  {
    get
    {
      SiteAnyAttribute.Definition definitions = PXContext.GetSlot<SiteAnyAttribute.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<SiteAnyAttribute.Definition>(PXDatabase.GetSlot<SiteAnyAttribute.Definition>("INSite.Definition", new Type[2]
        {
          typeof (INSite),
          typeof (INSetup)
        }));
      return definitions;
    }
  }

  public virtual SiteAnyAttribute.Definition SiteDefinitions => this.Definitions;

  /// <summary>The ID of the in-transit warehouse.</summary>
  public class transitSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Operand<
    #nullable disable
    SiteAnyAttribute.transitSiteID>,
    IBqlCreator,
    IBqlVerifier
  {
    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      value = (object) this.Value;
    }

    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      PXMutableCollection.AddMutableItem((IBqlCreator) this);
      exp = (SQLExpression) new SQLConst((object) this.Value);
      return true;
    }

    public virtual int Value
    {
      get
      {
        SiteAnyAttribute.Definition definition = PXContext.GetSlot<SiteAnyAttribute.Definition>();
        if (definition == null)
          definition = PXContext.SetSlot<SiteAnyAttribute.Definition>(PXDatabase.GetSlot<SiteAnyAttribute.Definition>("INSite.Definition", new Type[2]
          {
            typeof (INSite),
            typeof (INSetup)
          }));
        return ((int?) definition?.TransitSiteID).GetValueOrDefault();
      }
    }
  }

  public class dimensionName : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SiteAnyAttribute.dimensionName>
  {
    public dimensionName()
      : base("INSITE")
    {
    }
  }

  public class Definition : IPrefetchable, IPXCompanyDependent
  {
    private int? _DefaultSiteID;
    private int? _TransitSiteID;

    public int? DefaultSiteID => this._DefaultSiteID;

    public int? TransitSiteID => this._TransitSiteID;

    public void Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<INSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<INSetup.transitSiteID>()
      }))
      {
        this._TransitSiteID = new int?(-1);
        if (pxDataRecord != null)
          this._TransitSiteID = pxDataRecord.GetInt32(0);
      }
      List<PXDataField> pxDataFieldList = new List<PXDataField>();
      pxDataFieldList.Add((PXDataField) new PXDataField<INSite.siteID>());
      if (this._TransitSiteID.HasValue)
        pxDataFieldList.Add((PXDataField) new PXDataFieldValue("SiteID", (PXDbType) 8, new int?(4), (object) this._TransitSiteID, (PXComp) 1));
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue<INSite.active>((object) true));
      pxDataFieldList.Add((PXDataField) new PXDataFieldOrder<INSite.siteID>());
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<INSite>(pxDataFieldList.ToArray()))
      {
        this._DefaultSiteID = new int?();
        if (pxDataRecord == null)
          return;
        this._DefaultSiteID = pxDataRecord.GetInt32(0);
      }
    }
  }
}
