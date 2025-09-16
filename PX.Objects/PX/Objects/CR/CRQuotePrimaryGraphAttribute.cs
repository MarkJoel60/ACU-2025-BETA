// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRQuotePrimaryGraphAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CR;

public sealed class CRQuotePrimaryGraphAttribute : CRCacheIndependentPrimaryGraphListAttribute
{
  public CRQuotePrimaryGraphAttribute()
    : base(new System.Type[4]
    {
      typeof (PMQuoteMaint),
      typeof (PMQuoteMaint),
      typeof (QuoteMaint),
      typeof (QuoteMaint)
    }, new System.Type[4]
    {
      typeof (Select<PMQuote, Where<PMQuote.quoteID, Equal<Current<CRQuote.quoteID>>>>),
      typeof (Select<PMQuote, Where<PMQuote.quoteID, Equal<Current<CRQuote.noteID>>>>),
      typeof (Select<CRQuote, Where<CRQuote.quoteID, Equal<Current<CRQuote.quoteID>>, And<CRQuote.quoteType, Equal<CRQuoteTypeAttribute.distribution>>>>),
      typeof (Select<CRQuote, Where<CRQuote.quoteID, Equal<Current<CRQuote.noteID>>>>)
    })
  {
  }

  protected override void OnAccessDenied(System.Type graphType)
  {
    throw new AccessViolationException(Messages.FormNoAccessRightsMessage(graphType));
  }
}
