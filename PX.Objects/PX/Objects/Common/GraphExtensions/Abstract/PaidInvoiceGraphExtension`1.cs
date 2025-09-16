// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.PaidInvoiceGraphExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract;

public abstract class PaidInvoiceGraphExtension<TGraph> : 
  InvoiceBaseGraphExtension<TGraph, PaidInvoice, PaidInvoiceMapping>
  where TGraph : PXGraph
{
}
