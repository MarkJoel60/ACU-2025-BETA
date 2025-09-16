// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearchFeedbackBuilder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Search;
using PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearch;
using PX.Objects.AP.InvoiceRecognition.VendorSearch;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.Feedback;

internal class VendorSearchFeedbackBuilder
{
  private readonly IContactRepository _contactRepository;
  private readonly IEntitySearchService _entitySearchService;
  private readonly List<PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearch.Search> _searches = new List<PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearch.Search>();
  private readonly Dictionary<string, Candidate> _candidates = new Dictionary<string, Candidate>();
  private Found _winner;

  public VendorSearchFeedbackBuilder(
    IContactRepository contactRepository,
    IEntitySearchService entitySearchService)
  {
    ExceptionExtensions.ThrowOnNull<IContactRepository>(contactRepository, nameof (contactRepository), (string) null);
    ExceptionExtensions.ThrowOnNull<IEntitySearchService>(entitySearchService, nameof (entitySearchService), (string) null);
    this._contactRepository = contactRepository;
    this._entitySearchService = entitySearchService;
  }

  public void Clear()
  {
    this._searches.Clear();
    this._candidates.Clear();
    this._winner = (Found) null;
  }

  public void AddFullTextSearch(string query, List<Found> results)
  {
    this.AddSearch(SearchType.FullText, query, results);
  }

  public void AddEmailSearch(string email, List<Found> results)
  {
    this.AddSearch(SearchType.Email, email, results);
  }

  public void AddDomainSearch(string domain, List<Found> results)
  {
    this.AddSearch(SearchType.EmailDomain, domain, results);
  }

  private void AddSearch(SearchType type, string input, List<Found> found)
  {
    ExceptionExtensions.ThrowOnNull<string>(input, nameof (input), (string) null);
    this._searches.Add(new PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearch.Search()
    {
      Type = type,
      Input = input,
      Found = found
    });
  }

  public void AddWinner(int baccountId, float score)
  {
    if (this._winner != null)
      throw new PXInvalidOperationException();
    string key = baccountId.ToString();
    this._winner = this._candidates.ContainsKey(key) ? new Found()
    {
      Id = key,
      Score = new float?(score)
    } : throw new PXArgumentException(nameof (baccountId));
  }

  public void AddCandidate(
    PXGraph graph,
    int baccountId,
    int? defContactId,
    int? primaryContactID,
    Guid? noteId,
    Candidate candidate)
  {
    ExceptionExtensions.ThrowOnNull<Candidate>(candidate, nameof (candidate), (string) null);
    string key = baccountId.ToString();
    if (this._candidates.ContainsKey(key))
      return;
    PX.Objects.CR.Contact accountContact = defContactId.HasValue ? this._contactRepository.GetAccountContact(graph, baccountId, defContactId.Value) : (PX.Objects.CR.Contact) null;
    PX.Objects.CR.Contact primaryContact = primaryContactID.HasValue ? this._contactRepository.GetPrimaryContact(graph, baccountId, primaryContactID.Value) : (PX.Objects.CR.Contact) null;
    List<string> otherContactEmails = this._contactRepository.GetOtherContactEmails(graph, baccountId, accountContact, primaryContact);
    candidate.Term = noteId.HasValue ? this._entitySearchService.GetSearchIndexContent(noteId.Value) : (string) null;
    candidate.Emails = new Emails()
    {
      Account = accountContact?.EMail,
      Primary = primaryContact?.EMail,
      Contacts = otherContactEmails
    };
    this._candidates.Add(key, candidate);
  }

  public VendorSearchFeedback ToVendorSearchFeedback()
  {
    return new VendorSearchFeedback()
    {
      Searches = new List<PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearch.Search>((IEnumerable<PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearch.Search>) this._searches),
      Candidates = new Dictionary<string, Candidate>((IDictionary<string, Candidate>) this._candidates),
      Winner = this._winner
    };
  }
}
