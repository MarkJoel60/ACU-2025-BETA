// Decompiled with JetBrains decompiler
// Type: PX.Data.FilterListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Maintenance.GI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>
/// Displays a list of shared filters for the defined screen.
/// </summary>
public class FilterListAttribute : 
  PXCustomSelectorAttribute,
  IPXFieldDefaultingSubscriber,
  IPXFieldUpdatingSubscriber
{
  private readonly System.Type _screenIdentity;
  internal static readonly Guid MinFilterValue = new Guid(0, (short) 0, (short) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 16 /*0x10*/);
  public static readonly FilterHeader AllRecordsFilter = new FilterHeader()
  {
    FilterID = new Guid?(Guid.Empty),
    FilterName = "All Records",
    FilterOrder = new long?(-1L)
  };

  public bool ShowOnlyShared { get; private set; } = true;

  public bool IsSiteMapIdentityScreenID { get; set; } = true;

  public bool IsSiteMapIdentityGIDesignID { get; set; }

  public FilterListAttribute(System.Type siteMapIdentity)
    : base(FilterListAttribute.GetSearchBqlCommand(siteMapIdentity))
  {
    if (siteMapIdentity == (System.Type) null)
      throw new ArgumentNullException(nameof (siteMapIdentity));
    this._screenIdentity = typeof (IBqlField).IsAssignableFrom(siteMapIdentity) ? siteMapIdentity : throw new ArgumentException(PXLocalizer.LocalizeFormat("The type {0} must inherit the PX.Data.IBqlField interface.", (object) nameof (siteMapIdentity)));
    this.SubstituteKey = typeof (FilterHeader.filterName);
  }

  private static System.Type GetSearchBqlCommand(System.Type siteMapIdentity)
  {
    return BqlCommand.Compose(typeof (Search<,>), typeof (FilterHeader.filterID), BqlCommand.Compose(typeof (Where<,>), typeof (FilterHeader.screenID), typeof (Equal<>), typeof (Optional<>), siteMapIdentity));
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.SetAltered(this._FieldName, true);
  }

  public virtual IEnumerable GetRecords()
  {
    List<FilterHeader> records = new List<FilterHeader>()
    {
      new FilterHeader()
      {
        FilterID = FilterListAttribute.AllRecordsFilter.FilterID,
        FilterName = PXMessages.LocalizeNoPrefix(FilterListAttribute.AllRecordsFilter.FilterName),
        FilterOrder = FilterListAttribute.AllRecordsFilter.FilterOrder
      }
    };
    PXCache cach = this._Graph.Caches[this._screenIdentity.DeclaringType];
    object obj = cach.GetValue(cach.Current, this._screenIdentity.Name);
    string screenID = (string) null;
    if (obj != null)
    {
      if (this.IsSiteMapIdentityScreenID)
        screenID = (string) obj;
      else if (this.IsSiteMapIdentityGIDesignID)
      {
        Guid result;
        if (Guid.TryParse(obj.ToString(), out result))
        {
          GenericInquiryDesigner instance = PXGraph.CreateInstance<GenericInquiryDesigner>();
          instance.CurrentDesign.Current = (GIDesign) instance.Designs.Search<GIDesign.designID>((object) result);
          if (instance.CurrentDesign.Current != null)
            screenID = instance.CurrentDesign.Current.SitemapScreenID;
        }
      }
      else
      {
        Guid result;
        if (Guid.TryParse(obj.ToString(), out result))
        {
          PXSiteMapNode siteMapNodeFromKey = PXSiteMap.Provider.FindSiteMapNodeFromKey(result);
          if (siteMapNodeFromKey != null)
            screenID = siteMapNodeFromKey.ScreenID;
        }
      }
    }
    if (screenID != null)
    {
      IEnumerable<FilterHeader> source = FilterHeader.Definition.Get().Where<FilterHeader>((Func<FilterHeader, bool>) (f => string.Equals(f.ScreenID, screenID, StringComparison.OrdinalIgnoreCase)));
      foreach (FilterHeader filterHeader in !this.ShowOnlyShared ? source.Where<FilterHeader>((Func<FilterHeader, bool>) (f =>
      {
        bool? isShared = f.IsShared;
        bool flag = true;
        return isShared.GetValueOrDefault() == flag & isShared.HasValue || string.Equals(f.UserName, this._Graph.Accessinfo.UserName, StringComparison.OrdinalIgnoreCase);
      })) : source.Where<FilterHeader>((Func<FilterHeader, bool>) (f =>
      {
        bool? isShared = f.IsShared;
        bool flag = true;
        return isShared.GetValueOrDefault() == flag & isShared.HasValue;
      })))
        records.Add(filterHeader);
    }
    records.Sort((Comparison<FilterHeader>) ((f1, f2) => f1.FilterOrder.Value.CompareTo((object) f2.FilterOrder)));
    return (IEnumerable) records;
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.NewValue != null)
      return;
    e.NewValue = (object) FilterListAttribute.AllRecordsFilter.FilterID;
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row != null && e.ReturnValue == null)
      e.ReturnValue = (object) PXMessages.LocalizeNoPrefix(FilterListAttribute.AllRecordsFilter.FilterName);
    base.FieldSelecting(sender, e);
  }

  public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    Guid? newValue = e.NewValue as Guid?;
    Guid? filterId = FilterListAttribute.AllRecordsFilter.FilterID;
    if ((newValue.HasValue == filterId.HasValue ? (newValue.HasValue ? (newValue.GetValueOrDefault() == filterId.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      return;
    e.NewValue = (object) null;
  }

  public static void SetShowOnlyShared<TField>(PXCache cache, object data, bool sharedOnly) where TField : IBqlField
  {
    FilterListAttribute.SetShowOnlyShared(cache, data, typeof (TField).Name, sharedOnly);
  }

  public static void SetShowOnlyShared(PXCache cache, object data, string name, bool sharedOnly)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (FilterListAttribute filterListAttribute in cache.GetAttributesOfType<FilterListAttribute>(data, name))
      filterListAttribute.ShowOnlyShared = sharedOnly;
  }
}
