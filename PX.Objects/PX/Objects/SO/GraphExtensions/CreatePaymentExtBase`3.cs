// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.CreatePaymentExtBase`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.SO.Interfaces;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

public abstract class CreatePaymentExtBase<TGraph, TDocument, TAdjust> : 
  CreatePaymentExtBase<TGraph, TGraph, TDocument, TAdjust, TAdjust>
  where TGraph : PXGraph<TGraph, TDocument>, new()
  where TDocument : class, IBqlTable, ICreatePaymentDocument, new()
  where TAdjust : class, IBqlTable, ICreatePaymentAdjust, new()
{
}
