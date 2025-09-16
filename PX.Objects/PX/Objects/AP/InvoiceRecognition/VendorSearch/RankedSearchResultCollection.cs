// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.VendorSearch.RankedSearchResultCollection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP.InvoiceRecognition.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.VendorSearch;

internal class RankedSearchResultCollection
{
  private readonly Dictionary<Guid?, RankedSearchResult> _resultsByNoteId = new Dictionary<Guid?, RankedSearchResult>();
  private readonly VendorSearchFeedbackBuilder _feedbackBuilder;

  public int Count => this._resultsByNoteId.Count;

  public RankedSearchResultCollection(VendorSearchFeedbackBuilder feedbackBuilder)
  {
    ExceptionExtensions.ThrowOnNull<VendorSearchFeedbackBuilder>(feedbackBuilder, nameof (feedbackBuilder), (string) null);
    this._feedbackBuilder = feedbackBuilder;
  }

  public RankedSearchResult Add(PXGraph graph, Vendor vendor, int? termIndex = null)
  {
    ExceptionExtensions.ThrowOnNull<Vendor>(vendor, nameof (vendor), (string) null);
    RankedSearchResult rankedSearchResult1;
    if (this._resultsByNoteId.TryGetValue(vendor.NoteID, out rankedSearchResult1))
    {
      rankedSearchResult1.IncreaseRank();
      return rankedSearchResult1;
    }
    RankedSearchResult rankedSearchResult2 = new RankedSearchResult(vendor, termIndex);
    this._resultsByNoteId.Add(vendor.NoteID, rankedSearchResult2);
    this._feedbackBuilder.AddCandidate(graph, rankedSearchResult2.Vendor.BAccountID.Value, rankedSearchResult2.Vendor.DefContactID, rankedSearchResult2.Vendor.PrimaryContactID, rankedSearchResult2.Vendor.NoteID, rankedSearchResult2.Candidate);
    return rankedSearchResult2;
  }

  public RankedSearchResult GetMaxRankResult()
  {
    return this._resultsByNoteId.Count == 0 ? (RankedSearchResult) null : this._resultsByNoteId.Values.Max<RankedSearchResult>();
  }
}
