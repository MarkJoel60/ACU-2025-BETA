// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.GenericInquiryReferenceInfoProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Maintenance.GI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.GenericInquiry;

internal class GenericInquiryReferenceInfoProvider : IGenericInquiryReferenceInfoProvider
{
  private readonly IGenericInquiryDescriptionProvider _genericInquiryDescriptionProvider;
  private const string SlotKey = "GenericInquiry$ReferencesInfo";

  public GenericInquiryReferenceInfoProvider(
    IGenericInquiryDescriptionProvider genericInquiryDescriptionProvider)
  {
    this._genericInquiryDescriptionProvider = genericInquiryDescriptionProvider;
  }

  public bool HasReferenceTo(Guid designIdToCheckForReferences, Guid designIdToLookFor)
  {
    return this.Cache.HasReferenceTo(designIdToCheckForReferences, designIdToLookFor);
  }

  public IEnumerable<(string Name, Guid designId)> GetReferencesTo(Guid designIdToLookFor)
  {
    foreach ((Guid designIdToCheckForReferences, string str) in (IEnumerable<(Guid, string)>) this.Cache.AllInquiries)
    {
      if (designIdToCheckForReferences != designIdToLookFor && this.Cache.HasReferenceTo(designIdToCheckForReferences, designIdToLookFor))
        yield return (str, designIdToCheckForReferences);
    }
  }

  public IEnumerable<(string Name, Guid designId)> GetReferencesFrom(Guid designIdToLookFor)
  {
    foreach ((Guid designIdToLookFor1, string str) in (IEnumerable<(Guid, string)>) this.Cache.AllInquiries)
    {
      if (designIdToLookFor1 != designIdToLookFor && this.Cache.HasReferenceTo(designIdToLookFor, designIdToLookFor1))
        yield return (str, designIdToLookFor1);
    }
  }

  private GenericInquiryReferenceInfoProvider.ReferenceProvider Cache
  {
    get
    {
      return PXContext.GetSlot<GenericInquiryReferenceInfoProvider.ReferenceProvider>("GenericInquiry$ReferencesInfo") ?? PXContext.SetSlot<GenericInquiryReferenceInfoProvider.ReferenceProvider>("GenericInquiry$ReferencesInfo", PXDatabase.GetSlot<GenericInquiryReferenceInfoProvider.ReferenceProvider, IGenericInquiryDescriptionProvider>("GenericInquiry$ReferencesInfo", this._genericInquiryDescriptionProvider, GenericInquiryReferenceInfoProvider.ReferenceProvider.UsedTables));
    }
  }

  internal class ReferenceProvider : 
    IPrefetchable<IGenericInquiryDescriptionProvider>,
    IPXCompanyDependent
  {
    public static readonly System.Type[] UsedTables = new System.Type[2]
    {
      typeof (GITable),
      typeof (GIDesign)
    };
    private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, bool>> _referencesInfo = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, bool>>();
    private IGenericInquiryDescriptionProvider _descriptionProvider;

    public IList<(Guid, string)> AllInquiries { get; private set; }

    public bool HasReferenceTo(Guid designIdToCheckForReferences, Guid designIdToLookFor)
    {
      return designIdToCheckForReferences == designIdToLookFor || this._referencesInfo.GetOrAdd(designIdToCheckForReferences, (Func<Guid, ConcurrentDictionary<Guid, bool>>) (_ => new ConcurrentDictionary<Guid, bool>())).GetOrAdd<(Guid, IGenericInquiryDescriptionProvider, Func<GIDescription, Guid, bool>)>(designIdToLookFor, (Func<Guid, (Guid, IGenericInquiryDescriptionProvider, Func<GIDescription, Guid, bool>), bool>) ((toLookFor, parameters) =>
      {
        GIDescription giDescription = parameters.Item2.Get(parameters.Item1);
        return giDescription != null && parameters.Item3(giDescription, toLookFor);
      }), (designIdToCheckForReferences, this._descriptionProvider, new Func<GIDescription, Guid, bool>(this.HasReferenceTo)));
    }

    private bool HasReferenceTo(GIDescription description, Guid designIdToLookFor)
    {
      Guid result;
      foreach (Guid designId in description.Tables.Where<GITable>((Func<GITable, bool>) (x =>
      {
        int? type = x.Type;
        int num = 1;
        return type.GetValueOrDefault() == num & type.HasValue;
      })).Select<GITable, Guid?>((Func<GITable, Guid?>) (x => !Guid.TryParse(x.Name, out result) ? new Guid?() : new Guid?(result))).Where<Guid?>((Func<Guid?, bool>) (x => x.HasValue)).Select<Guid?, Guid>((Func<Guid?, Guid>) (x => x.Value)))
      {
        if (designId == designIdToLookFor)
          return true;
        GIDescription description1 = this._descriptionProvider.Get(designId);
        if (description1 != null && this.HasReferenceTo(description1, designIdToLookFor))
          return true;
      }
      return false;
    }

    public void Prefetch(
      IGenericInquiryDescriptionProvider descriptionProvider)
    {
      this._descriptionProvider = descriptionProvider;
      this.AllInquiries = (IList<(Guid, string)>) PXDatabase.SelectRecords<GIDesign>().Select<GIDesign, (Guid, string)>((Func<GIDesign, (Guid, string)>) (x => (x.DesignID.GetValueOrDefault(), x.Name))).ToList<(Guid, string)>();
    }
  }
}
