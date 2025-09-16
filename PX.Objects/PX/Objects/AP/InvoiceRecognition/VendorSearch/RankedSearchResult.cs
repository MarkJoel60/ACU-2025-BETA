// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.VendorSearch.RankedSearchResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearch;
using System;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.VendorSearch;

internal class RankedSearchResult : IComparable<RankedSearchResult>
{
  public Candidate Candidate { get; }

  public float Rank => this.Candidate.Score.Value;

  public int? TermIndex { get; }

  public Vendor Vendor { get; }

  public RankedSearchResult(Vendor vendor, int? termIndex)
  {
    ExceptionExtensions.ThrowOnNull<Vendor>(vendor, nameof (vendor), (string) null);
    this.Vendor = vendor;
    this.TermIndex = termIndex;
    this.Candidate = new Candidate()
    {
      Score = new float?(0.0f)
    };
  }

  public void IncreaseRank()
  {
    Candidate candidate = this.Candidate;
    float? score = candidate.Score;
    float num = 1f;
    candidate.Score = score.HasValue ? new float?(score.GetValueOrDefault() + num) : new float?();
  }

  public int CompareTo(RankedSearchResult other)
  {
    if (other == null || (double) this.Rank > (double) other.Rank)
      return 1;
    return (double) this.Rank < (double) other.Rank ? -1 : 0;
  }
}
