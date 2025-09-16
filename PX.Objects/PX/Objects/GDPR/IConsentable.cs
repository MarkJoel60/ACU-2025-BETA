// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.IConsentable
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.GDPR;

/// <summary>
/// Represents an entity that should have the consent of a person to proceed.
/// </summary>
/// <remarks>
/// The interface is used by the <see cref="P:PX.Objects.CS.FeaturesSet.GDPRCompliance">GDPR feature</see>.
/// </remarks>
public interface IConsentable
{
  /// <summary>
  /// Specifies whether the person has given the consent to process the personal data.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  bool? ConsentAgreement { get; set; }

  /// <summary>
  /// The date when the person has given the consent to process the personal data.
  /// </summary>
  /// <value>The consent date.</value>
  DateTime? ConsentDate { get; set; }

  /// <summary>
  /// The date when the consent given by the person will be revoked.
  /// If this box is empty, the consent will never be revoked.
  /// </summary>
  /// <value>The consent expiration date.</value>
  DateTime? ConsentExpirationDate { get; set; }
}
