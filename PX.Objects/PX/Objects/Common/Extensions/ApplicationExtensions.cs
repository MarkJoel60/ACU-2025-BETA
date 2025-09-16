// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.ApplicationExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common.Abstractions;

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class ApplicationExtensions
{
  /// <summary>
  /// Returns <c>true</c> if the specified document is on the adjusting side of the
  /// specified application, <c>false</c> otherwise. For example, this method will
  /// return <c>true</c> if you call it on an application of a payment to an invoice,
  /// and specify the payment as the argument.
  /// </summary>
  public static bool IsOutgoingApplicationFor(
    this IDocumentAdjustment application,
    IDocumentKey document)
  {
    return document.DocType == application.AdjgDocType && document.RefNbr == application.AdjgRefNbr;
  }

  /// <summary>
  /// Returns <c>true</c> if the specified document is on the adjusted side of the
  /// specified application, <c>false</c> otherwise. For example, this method will
  /// return <c>true</c> if you call it on an application of a payment to an invoice,
  /// and specify the invoice as the argument.
  /// </summary>
  public static bool IsIncomingApplicationFor(
    this IDocumentAdjustment application,
    IDocumentKey document)
  {
    return document.DocType == application.AdjdDocType && document.RefNbr == application.AdjdRefNbr;
  }

  /// <summary>
  /// Returns <c>true</c> if and only if the calling application corresponds to the
  /// specified document (on either the adjusting or the adjusted side).
  /// </summary>
  public static bool IsApplicationFor(this IDocumentAdjustment application, IDocumentKey document)
  {
    return application.IsOutgoingApplicationFor(document) || application.IsIncomingApplicationFor(document);
  }
}
