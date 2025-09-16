// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.PaymentProcessing.PaymentConnectorCallbackService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;
using System.Web;

#nullable disable
namespace PX.Objects.CC.PaymentProcessing;

internal class PaymentConnectorCallbackService
{
  public const string CancelParamName = "__CLOSECCHFORM";
  public const string TransactionIdParamName = "__TRANID";

  public PaymentConnectorCallbackParams FromCCPaymentPanelCallback(
    bool includeCacheForSearching = false,
    Func<string, string> getFullKey = null)
  {
    PaymentConnectorCallbackParams connectorCallbackParams = new PaymentConnectorCallbackParams();
    bool result;
    if (bool.TryParse(this.GetStringFromRequestOrCache("__CLOSECCHFORM", getFullKey == null ? "__CLOSECCHFORM" : getFullKey("__CLOSECCHFORM"), includeCacheForSearching), out result))
      connectorCallbackParams.IsCancelled = new bool?(result);
    string fromRequestOrCache = this.GetStringFromRequestOrCache("__TRANID", getFullKey == null ? "__TRANID" : getFullKey("__TRANID"), includeCacheForSearching);
    if (!string.IsNullOrEmpty(fromRequestOrCache))
      connectorCallbackParams.TransactionId = fromRequestOrCache;
    return connectorCallbackParams;
  }

  public PaymentConnectorCallbackParams FromCommandArguments(string commandArguments)
  {
    PaymentConnectorCallbackParams connectorCallbackParams = new PaymentConnectorCallbackParams();
    if (!string.IsNullOrEmpty(commandArguments))
    {
      if (commandArguments == "__CLOSECCHFORM")
        connectorCallbackParams.IsCancelled = new bool?(true);
      else
        connectorCallbackParams.TransactionId = commandArguments;
    }
    return connectorCallbackParams;
  }

  private string GetStringFromRequestOrCache(
    string key,
    string fullNameKey,
    bool includeCacheForSearching = false)
  {
    HttpRequest request = HttpContext.Current?.Request;
    if (request != null)
      return request.Form.Get(key) ?? request.Headers.Get(key);
    return !includeCacheForSearching ? (string) null : PXContext.GetSlot<string>(fullNameKey);
  }
}
