// Decompiled with JetBrains decompiler
// Type: PX.Data.ErrorMessagesNoPrefix
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

[PXLocalizable]
public static class ErrorMessagesNoPrefix
{
  public const string NoPermissionToSendEmail = "The email cannot be sent because the account you signed in with does not have permission for using the email address specified in the system email account on the System Email Accounts (SM204002) form.";
  public const string NoPermissionToReceiveEmails = "Emails cannot be received because the account you signed in with does not have permission for using the email address specified in the system email account on the System Email Accounts (SM204002) form.";
  public const string StoreSeenMessageFlagFailed = "The email was not marked as read after it was received on the mail server.";
  public const string StoreUnseenMessageFlagFailed = "The email was not marked as unread after it was received on the mail server.";
  public const string StoreDeletedMessageFlagFailed = "After the email was received, it was not marked as deleted on the mail server.";
  public const string TimeoutDuringImapFetch = "An IMAP item could not be fetched because of a timeout error.";
  public const string EmailAccountCannotBeUsedForOAuth2 = "The {0} email account cannot be used for OAuth2 authentication. ";
  public const string MakePXBqlTabeTheMostBaseParent = "A data access class must derive from the PX.Data.PXBqlTable class.";
}
