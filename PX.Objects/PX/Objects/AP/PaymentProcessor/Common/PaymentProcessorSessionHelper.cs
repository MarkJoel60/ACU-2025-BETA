// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.Common.PaymentProcessorSessionHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.PaymentProcessor.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.PaymentProcessor.Common;

public static class PaymentProcessorSessionHelper
{
  public const string SessionKey = "PaymentProcessorUserSessionStore";

  public static void SetUserSessionStore(APExternalPaymentProcessor processor)
  {
    UserSessionStore? nullable = PXContext.Session["PaymentProcessorUserSessionStore"] as UserSessionStore?;
    if (!nullable.HasValue)
      nullable = new UserSessionStore?(new UserSessionStore());
    processor.UserSessionStore = nullable;
  }

  public static void SaveUserSessionStore(UserSessionStore store)
  {
    if (PXLongOperation.IsLongOperationContext())
      PXLongOperation.SetCustomInfo((object) new PaymentProcessorSessionUpdater(store));
    else
      PaymentProcessorSessionHelper.SaveUserSessionStoreInSession(store);
  }

  public static void SaveUserSessionStoreInSession(UserSessionStore sessionStore)
  {
    PXContext.Session.SetValueType("PaymentProcessorUserSessionStore", (ValueType) (object) sessionStore);
  }

  public static UserSessionStore AddOrUpdateUserSessionIdInStore(
    UserSessionStore store,
    string extOrganizationId,
    string userSessionId)
  {
    if (((UserSessionStore) ref store).UserSessionIdForOrganization == null)
      ((UserSessionStore) ref store).UserSessionIdForOrganization = new Dictionary<string, string>();
    Dictionary<string, string> idForOrganization = ((UserSessionStore) ref store).UserSessionIdForOrganization;
    if (idForOrganization.ContainsKey(extOrganizationId))
      idForOrganization[extOrganizationId] = userSessionId;
    else
      idForOrganization.Add(extOrganizationId, userSessionId);
    return store;
  }
}
