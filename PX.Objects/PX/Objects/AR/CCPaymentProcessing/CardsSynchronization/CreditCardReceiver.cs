// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.CardsSynchronization.CreditCardReceiver
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.CardsSynchronization;

public class CreditCardReceiver
{
  private ICCProfileProcessor profileProcessor;

  public string CustomerProfileId { get; private set; }

  public List<CreditCardData> Result { get; private set; }

  public int AttempsCnt { get; set; } = 1;

  public Func<CreditCardData, bool> ProcessFilter { get; set; }

  public CreditCardReceiver(ICCProfileProcessor profileProcessor, string customerProfileId)
  {
    this.profileProcessor = profileProcessor;
    this.CustomerProfileId = customerProfileId;
  }

  public void DoAction()
  {
    try
    {
      --this.AttempsCnt;
      IEnumerable<CreditCardData> allPaymentProfiles = this.profileProcessor.GetAllPaymentProfiles(this.CustomerProfileId);
      this.Result = new List<CreditCardData>();
      foreach (CreditCardData creditCardData in allPaymentProfiles)
      {
        if (this.ProcessFilter != null)
        {
          if (this.ProcessFilter(creditCardData))
            this.Result.Add(creditCardData);
        }
        else
          this.Result.Add(creditCardData);
      }
    }
    catch (CCProcessingException ex)
    {
      if (this.AttempsCnt > 0)
      {
        Thread.Sleep(100);
        this.DoAction();
      }
      else
        throw;
    }
  }
}
