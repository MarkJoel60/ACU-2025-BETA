// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.EmailFactorSenderBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.EP;
using PX.Data.SQLTree;
using PX.Data.Wiki.Parser;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.MultiFactorAuth;

internal class EmailFactorSenderBase
{
  protected readonly INotificationSender _sender;

  public EmailFactorSenderBase(INotificationSender sender) => this._sender = sender;

  protected IEnumerable<(int CompanyId, Guid userId, string email)> SelectUserEmails(
    IEnumerable<(int companyId, Guid userId, int twoFactorLevel)> userIds)
  {
    ParameterExpression parameterExpression = Expression.Parameter(typeof (EmailFactorSenderBase.UserWithCompany));
    BinaryExpression binaryExpression = (BinaryExpression) null;
    foreach ((int num, Guid guid, int _) in userIds.Where<(int, Guid, int)>((Func<(int, Guid, int), bool>) (c => c.twoFactorLevel > 0)))
    {
      BinaryExpression right = Expression.AndAlso((Expression) Expression.Equal((Expression) Expression.PropertyOrField((Expression) parameterExpression, "PKID"), (Expression) Expression.Constant((object) guid, typeof (Guid?))), (Expression) Expression.Equal((Expression) Expression.PropertyOrField((Expression) parameterExpression, "CompanyID"), (Expression) Expression.Constant((object) num)));
      binaryExpression = binaryExpression == null ? right : Expression.OrElse((Expression) binaryExpression, (Expression) right);
    }
    if (binaryExpression == null)
      return Enumerable.Empty<(int, Guid, string)>();
    return (IEnumerable<(int, Guid, string)>) PXDatabase.Select<EmailFactorSenderBase.UserWithCompany>().Where<EmailFactorSenderBase.UserWithCompany>(Expression.Lambda<Func<EmailFactorSenderBase.UserWithCompany, bool>>((Expression) binaryExpression, parameterExpression)).Select(c => new
    {
      CompanyId = c.CompanyID,
      UserId = c.PKID,
      Email = c.Email
    }).SkipRestrictions().AsEnumerable().Select(c => (c.CompanyId, c.UserId.Value, c.Email)).ToArray<(int, Guid, string)>();
  }

  protected bool SendWithTemplate<TemplateID>(Guid userId, string email, string code) where TemplateID : IBqlField
  {
    try
    {
      Access instance = PXGraph.CreateInstance<Access>();
      instance.UserList.Current = (Users) instance.UserList.Search<Users.pKID>((object) userId);
      email = email ?? instance.UserList.Current.Email;
      Notification notification = (Notification) PXSelectBase<Notification, PXSelectJoin<Notification, InnerJoin<PreferencesEmail, On<Notification.notificationID, Equal<TemplateID>>>>.Config>.Select((PXGraph) instance);
      if (instance.UserList.Current != null)
      {
        if (notification != null)
        {
          instance.UserList.Current.TwoFactorCode = code;
          string str1;
          string str2;
          using (!string.IsNullOrEmpty(notification.LocaleName) ? new PXCultureScope(new CultureInfo(notification.LocaleName)) : (PXCultureScope) null)
          {
            str1 = PXTemplateContentParser.Instance.Process(notification.Subject, (PXGraph) instance, typeof (Users), (object[]) null);
            str2 = PXTemplateContentParser.ScriptInstance.Process(notification.Body, (PXGraph) instance, typeof (Users), (object[]) null);
          }
          this._sender.NotifyAndDeliver(new EmailNotificationParameters()
          {
            To = email,
            Subject = str1,
            Body = str2
          });
          return true;
        }
      }
    }
    catch
    {
    }
    return false;
  }

  internal class UserWithCompany : Users
  {
    [PXDBInt]
    public int CompanyID { get; set; }

    public class companyId : IBqlField, IBqlOperand
    {
    }
  }
}
