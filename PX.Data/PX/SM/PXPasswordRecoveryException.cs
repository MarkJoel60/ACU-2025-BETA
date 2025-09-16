// Decompiled with JetBrains decompiler
// Type: PX.SM.PXPasswordRecoveryException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public class PXPasswordRecoveryException : PXException
{
  public PXPasswordRecoveryException.ErrorCode Reason { get; }

  public PXPasswordRecoveryException(PXPasswordRecoveryException.ErrorCode reason)
  {
    string message;
    switch (reason)
    {
      case PXPasswordRecoveryException.ErrorCode.PasswordRecoveryDisabled:
        message = "The password recovery feature is disabled for this account. Contact your system administrator.";
        break;
      case PXPasswordRecoveryException.ErrorCode.InactiveUserAccount:
        message = "The password recovery feature is disabled for this account. Contact your system administrator.";
        break;
      case PXPasswordRecoveryException.ErrorCode.NoEmailInUserAccount:
        message = "No email address is specified in your account.";
        break;
      case PXPasswordRecoveryException.ErrorCode.EmailSenderNotConfigured:
        message = "The email account is empty. Please define the Default Email Account in Email Preferences.";
        break;
      case PXPasswordRecoveryException.ErrorCode.PasswordRecoveryNotificationIsNotConfigured:
        message = "A notification for password recovery has not been configured.";
        break;
      case PXPasswordRecoveryException.ErrorCode.PasswordRecoveryNotificationNotFound:
        message = "A notification for password recovery has not been found.";
        break;
      default:
        throw new NotSupportedException();
    }
    // ISSUE: explicit constructor call
    base.\u002Ector(message);
    this.Reason = reason;
  }

  public PXPasswordRecoveryException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public enum ErrorCode
  {
    PasswordRecoveryDisabled,
    InactiveUserAccount,
    NoEmailInUserAccount,
    EmailSenderNotConfigured,
    PasswordRecoveryNotificationIsNotConfigured,
    PasswordRecoveryNotificationNotFound,
  }
}
