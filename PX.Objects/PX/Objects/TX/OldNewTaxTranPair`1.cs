// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.OldNewTaxTranPair`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.TX;

internal class OldNewTaxTranPair<T> where T : TaxTran, new()
{
  public T OldTaxTran { get; }

  public T NewTaxTran { get; private set; }

  public OldNewTaxTranPair(T TaxTran)
  {
    this.OldTaxTran = TaxTran;
    T obj = new T();
    obj.TaxID = TaxTran.TaxID;
    this.NewTaxTran = obj;
  }

  public T InsertCurrentNewTaxTran(PXSelectBase<T> view)
  {
    return this.NewTaxTran = view.Insert(this.NewTaxTran);
  }
}
