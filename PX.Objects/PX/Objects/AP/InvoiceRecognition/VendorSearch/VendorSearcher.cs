// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.VendorSearch.VendorSearcher
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DocumentRecognition;
using PX.Common;
using PX.Data;
using PX.Data.Search;
using PX.Objects.AP.InvoiceRecognition.Feedback;
using PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearch;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.VendorSearch;

internal class VendorSearcher : IVendorSearchService
{
  internal const string TermTypeAddress = "address";
  private readonly IVendorRepository _vendorRepository;
  private readonly IEntitySearchService _entitySearchService;
  private readonly VendorSearchFeedbackBuilder _feedbackBuilder;

  public VendorSearcher(
    IVendorRepository vendorRepository,
    IEntitySearchService entitySearchService,
    VendorSearchFeedbackBuilder feedbackBuilder)
  {
    ExceptionExtensions.ThrowOnNull<IVendorRepository>(vendorRepository, nameof (vendorRepository), (string) null);
    ExceptionExtensions.ThrowOnNull<IEntitySearchService>(entitySearchService, nameof (entitySearchService), (string) null);
    ExceptionExtensions.ThrowOnNull<VendorSearchFeedbackBuilder>(feedbackBuilder, nameof (feedbackBuilder), (string) null);
    this._vendorRepository = vendorRepository;
    this._entitySearchService = entitySearchService;
    this._feedbackBuilder = feedbackBuilder;
  }

  private IEnumerable<(string FullTextQuery, Guid[] Results)> SearchByTerm(FullTextTerm term)
  {
    if (string.IsNullOrWhiteSpace(term?.Text))
      return Enumerable.Empty<(string, Guid[])>();
    List<(string, Guid[])> valueTupleList = new List<(string, Guid[])>();
    string query = this.EscapeExactSearchQuery(term.Text);
    (string FullTextQuery1, Guid[] Results1) = this.FullTextSearch(query);
    valueTupleList.Add((FullTextQuery1, Results1));
    bool flag = "address".Equals(term.Type, StringComparison.OrdinalIgnoreCase);
    if (Results1.Length != 0 | flag)
      return (IEnumerable<(string, Guid[])>) valueTupleList;
    for (int startIndex = query.LastIndexOf(' '); startIndex > 0; startIndex = query.LastIndexOf(' '))
    {
      query = query.Remove(startIndex);
      (string FullTextQuery2, Guid[] Results2) = this.FullTextSearch(query);
      switch (Results2.Length)
      {
        case 0:
          valueTupleList.Add((FullTextQuery2, Results2));
          continue;
        case 1:
          valueTupleList.Add((FullTextQuery2, Results2));
          return (IEnumerable<(string, Guid[])>) valueTupleList;
        default:
          return (IEnumerable<(string, Guid[])>) valueTupleList;
      }
    }
    return (IEnumerable<(string, Guid[])>) valueTupleList;
  }

  private string EscapeExactSearchQuery(string query)
  {
    bool flag = false;
    string str = query;
    while (!flag && !string.IsNullOrEmpty(str))
    {
      str = str.Trim();
      if (str.StartsWith("\"") && str.EndsWith("\"") && str.Length > 1)
        str = str.Substring(1, str.Length - 2);
      else
        flag = true;
    }
    return str;
  }

  private (string FullTextQuery, Guid[] Results) FullTextSearch(string query)
  {
    List<EntitySearchResult> source = (List<EntitySearchResult>) null;
    try
    {
      source = this._entitySearchService.Search(query, 0, WebConfig.MaxFullTextSearchResultCount, typeof (Vendor).FullName);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
    }
    if (source == null)
      return (query, Array<Guid>.Empty);
    Guid[] array = source.Select<EntitySearchResult, Guid>((Func<EntitySearchResult, Guid>) (r => r.NoteID)).ToArray<Guid>();
    return (query, array);
  }

  public VendorSearchResult FindVendor(
    PXGraph graph,
    string vendorName,
    IList<FullTextTerm> fullTextTerms,
    string email)
  {
    ExceptionExtensions.ThrowOnNull<PXGraph>(graph, nameof (graph), (string) null);
    if (!string.IsNullOrEmpty(vendorName))
    {
      int? vendorIdByVendorName = this._vendorRepository.GetActiveVendorIdByVendorName(graph, vendorName);
      if (vendorIdByVendorName.HasValue)
        return new VendorSearchResult(vendorIdByVendorName, new int?(), (VendorSearchFeedback) null);
    }
    this._feedbackBuilder.Clear();
    RankedSearchResultCollection searchResultCollection = new RankedSearchResultCollection(this._feedbackBuilder);
    if (fullTextTerms != null)
      this.SearchByTerms(graph, searchResultCollection, fullTextTerms);
    if (!string.IsNullOrWhiteSpace(email))
      this.SearchByEmail(graph, searchResultCollection, email);
    RankedSearchResult maxRankResult = searchResultCollection.GetMaxRankResult();
    if (maxRankResult == null)
      return new VendorSearchResult();
    this._feedbackBuilder.AddWinner(maxRankResult.Vendor.BAccountID.Value, maxRankResult.Rank);
    VendorSearchFeedback vendorSearchFeedback = this._feedbackBuilder.ToVendorSearchFeedback();
    return new VendorSearchResult(maxRankResult.Vendor.BAccountID, maxRankResult.TermIndex, vendorSearchFeedback);
  }

  private void SearchByTerms(
    PXGraph graph,
    RankedSearchResultCollection searchResultCollection,
    IList<FullTextTerm> fullTextTerms)
  {
    foreach (FullTextTerm term in fullTextTerms.Distinct<FullTextTerm>((IEqualityComparer<FullTextTerm>) new FullTextTermComparer()))
    {
      foreach ((string str, Guid[] Results) in this.SearchByTerm(term))
      {
        List<Found> results = new List<Found>();
        foreach (Guid noteId in Results)
        {
          Vendor activeVendorByNoteId = this._vendorRepository.GetActiveVendorByNoteId(graph, noteId);
          if (activeVendorByNoteId != null)
          {
            int num = fullTextTerms.IndexOf(term);
            RankedSearchResult rankedSearchResult = searchResultCollection.Add(graph, activeVendorByNoteId, new int?(num));
            Found found = new Found()
            {
              Id = activeVendorByNoteId.BAccountID.Value.ToString(),
              Score = new float?(rankedSearchResult.Rank)
            };
            results.Add(found);
          }
        }
        this._feedbackBuilder.AddFullTextSearch(str, results);
      }
    }
  }

  private void SearchByExactEmail(
    PXGraph graph,
    RankedSearchResultCollection searchResultCollection,
    string email,
    out bool anyFound)
  {
    IEnumerable<Vendor> vendorsByEmail = this._vendorRepository.GetVendorsByEmail(graph, email);
    List<Found> results = new List<Found>();
    foreach (Vendor vendor in vendorsByEmail)
    {
      RankedSearchResult rankedSearchResult = searchResultCollection.Add(graph, vendor);
      Found found = new Found()
      {
        Id = vendor.BAccountID.Value.ToString(),
        Score = new float?(rankedSearchResult.Rank)
      };
      results.Add(found);
    }
    this._feedbackBuilder.AddEmailSearch(email, results);
    anyFound = results.Count > 0;
  }

  private void SearchByEmailDomain(
    PXGraph graph,
    RankedSearchResultCollection searchResultCollection,
    string email)
  {
    string domain = email.Substring(email.IndexOf('@') + 1);
    if (string.IsNullOrEmpty(domain) || this._vendorRepository.IsExcludedDomain(domain))
      return;
    (string str, IEnumerable<Vendor> Results) = this._vendorRepository.GetVendorsByDomain(graph, domain);
    List<Found> results = new List<Found>();
    foreach (Vendor vendor1 in Results)
    {
      RankedSearchResultCollection resultCollection = searchResultCollection;
      PXGraph graph1 = graph;
      Vendor vendor2 = vendor1;
      int? nullable = new int?();
      int? termIndex = nullable;
      RankedSearchResult rankedSearchResult = resultCollection.Add(graph1, vendor2, termIndex);
      Found found1 = new Found();
      nullable = vendor1.BAccountID;
      found1.Id = nullable.Value.ToString();
      found1.Score = new float?(rankedSearchResult.Rank);
      Found found2 = found1;
      results.Add(found2);
    }
    this._feedbackBuilder.AddDomainSearch(str, results);
  }

  private void SearchByEmail(
    PXGraph graph,
    RankedSearchResultCollection searchResultCollection,
    string email)
  {
    bool anyFound;
    this.SearchByExactEmail(graph, searchResultCollection, email, out anyFound);
    if (anyFound)
      return;
    this.SearchByEmailDomain(graph, searchResultCollection, email);
  }
}
