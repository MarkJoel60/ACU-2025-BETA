// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.PaymentProcessing.PaymentConnectorCallbackParams
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CC.PaymentProcessing;

public class PaymentConnectorCallbackParams
{
  public bool? IsCancelled { get; set; }

  public string TransactionId { get; set; }
}
