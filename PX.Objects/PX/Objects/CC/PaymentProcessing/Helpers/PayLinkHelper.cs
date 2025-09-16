// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.PaymentProcessing.Helpers.PayLinkHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CC.PaymentProcessing.Helpers;

public static class PayLinkHelper
{
  public static bool PayLinkWasProcessed(CCPayLink payLink) => payLink.LinkStatus == "C";

  public static bool PayLinkCreated(CCPayLink payLink) => payLink.LinkStatus != "N";

  public static bool PayLinkOpen(CCPayLink payLink) => payLink.LinkStatus == "O";
}
