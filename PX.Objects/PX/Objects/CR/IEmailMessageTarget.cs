// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.IEmailMessageTarget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// Represents an entity that provides the email address and display name
/// to an <see cref="T:PX.Objects.CR.SMEmail">email</see> when the latter is opened on the form that contains this entity.
/// <see cref="P:PX.Objects.CR.SMEmail.MailTo">SMEmail.MailTo</see> will be filled with the email address representation
/// made by the <see cref="M:PX.Data.PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(System.String,System.String)" /> method
/// on the basis of <see cref="P:PX.Objects.CR.IEmailMessageTarget.Address" /> and <see cref="P:PX.Objects.CR.IEmailMessageTarget.DisplayName" />.
/// </summary>
public interface IEmailMessageTarget
{
  /// <summary>Gets the display name of the entity.</summary>
  /// <value>
  /// The display name. Can be <see langword="null" />.
  /// </value>
  string DisplayName { get; }

  /// <summary>Gets the email address of the entity.</summary>
  /// <value>
  /// The email address. If it is <see langword="null" />,
  /// the <see cref="P:PX.Objects.CR.SMEmail.MailTo" /> property will not be defaulted.
  /// </value>
  string Address { get; }
}
