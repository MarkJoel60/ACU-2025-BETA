// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ARInvoiceEntryEmulator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

public class ARInvoiceEntryEmulator : ARInvoiceEntry
{
  [ExcludeFromCodeCoverage]
  public override void Persist()
  {
  }

  [ExcludeFromCodeCoverage]
  public virtual int Persist(Type cacheType, PXDBOperation operation) => 1;
}
