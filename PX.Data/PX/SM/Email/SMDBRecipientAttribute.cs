// Decompiled with JetBrains decompiler
// Type: PX.SM.Email.SMDBRecipientAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM.Email;

/// <summary>
/// The attribute is used for email recipient fields, such as To, Cc, Bcc, From, Sender, or ReplyTo.
/// This attribute is needed to properly set the length of such fields.
/// </summary>
/// <remarks>
/// For single recipient fields, such as From or Sender, the field length is defined in the <see cref="F:PX.SM.Email.SMDBRecipientAttribute.SingleRecipientFieldLength" /> constant.
/// For multiple recipient fields, such as To, Cc, or Bcc, the field length is defined in the <see cref="F:PX.SM.Email.SMDBRecipientAttribute.MultipleRecipientsFieldLength" /> constant.
/// </remarks>
public class SMDBRecipientAttribute : PXDBStringAttribute
{
  /// <summary>
  /// The length of the field that contains a single recipient.
  /// </summary>
  public const int SingleRecipientFieldLength = 500;
  /// <summary>
  /// The length of the field that contains multiple recipients.
  /// </summary>
  public const int MultipleRecipientsFieldLength = 3000;

  /// <summary>
  /// Initialize a new instance of the <see cref="T:PX.SM.Email.SMDBRecipientAttribute" /> class.
  /// </summary>
  /// <param name="isMultiple">
  /// <see langword="true" /> if used for field that contains multiple recipients, such as To, Cc, Bcc.
  /// In that case <see cref="F:PX.SM.Email.SMDBRecipientAttribute.SingleRecipientFieldLength" /> is used as field length.
  /// Otherwise <see cref="F:PX.SM.Email.SMDBRecipientAttribute.MultipleRecipientsFieldLength" />.
  /// </param>
  public SMDBRecipientAttribute(bool isMultiple = false)
    : base(isMultiple ? 3000 : 500)
  {
    this.IsUnicode = true;
  }
}
