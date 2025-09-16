// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.Interfaces.ITerminalProcessingWrapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers.Interfaces;

public interface ITerminalProcessingWrapper
{
  POSTerminalData GetTerminal(string terminalID);

  IEnumerable<POSTerminalData> GetTerminals();
}
