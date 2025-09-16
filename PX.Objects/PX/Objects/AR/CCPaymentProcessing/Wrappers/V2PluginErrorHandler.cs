// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.V2PluginErrorHandler
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using System;
using System.Net;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public static class V2PluginErrorHandler
{
  public static void ExecuteAndHandleError(Action pluginAction)
  {
    try
    {
      pluginAction();
    }
    catch (CCProcessingException ex)
    {
      throw new PXException(((Exception) ex).InnerException, "Credit card processing error. {0} : {1}", new object[2]
      {
        (object) CCError.CCErrorSource.ProcessingCenter,
        (object) ((Exception) ex).Message
      });
    }
    catch (WebException ex)
    {
      throw new PXException((Exception) ex, "Credit card processing error. {0} : {1}", new object[2]
      {
        (object) CCError.CCErrorSource.Network,
        (object) ex.Message
      });
    }
    catch (Exception ex)
    {
      throw new PXException(ex, "Credit card processing error. {0} : {1}", new object[2]
      {
        (object) CCError.CCErrorSource.Internal,
        (object) ex.Message
      });
    }
  }

  public static T ExecuteAndHandleError<T>(Func<T> pluginAction)
  {
    T obj = default (T);
    try
    {
      return pluginAction();
    }
    catch (CCProcessingException ex)
    {
      throw new PXException((Exception) ex, "Credit card processing error. {0} : {1}", new object[2]
      {
        (object) CCError.CCErrorSource.ProcessingCenter,
        (object) ((Exception) ex).Message
      });
    }
    catch (WebException ex)
    {
      throw new PXException((Exception) ex, "Credit card processing error. {0} : {1}", new object[2]
      {
        (object) CCError.CCErrorSource.Network,
        (object) ex.Message
      });
    }
    catch (Exception ex)
    {
      throw new PXException(ex, "Credit card processing error. {0} : {1}", new object[2]
      {
        (object) CCError.CCErrorSource.Internal,
        (object) ex.Message
      });
    }
  }
}
