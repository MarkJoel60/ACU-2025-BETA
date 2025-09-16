// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Repositories.ICCDisplayMaskService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;

#nullable disable
namespace PX.Objects.AR.Repositories;

public interface ICCDisplayMaskService
{
  string UseDisplayMaskForCardNumber(string aID, string aDisplayMask);

  string UseAdjustedDisplayMaskForCardNumber(string aID, string aDisplayMask);

  string UseDefaultMaskForCardNumber(
    string cardNbr,
    string cardType,
    MeansOfPayment? meansOfPayment);
}
