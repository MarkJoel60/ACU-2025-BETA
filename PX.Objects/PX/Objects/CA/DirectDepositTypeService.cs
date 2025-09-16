// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.DirectDepositTypeService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class DirectDepositTypeService
{
  private IEnumerable<IDirectDepositType> _directDepositTypes;

  public DirectDepositTypeService(IEnumerable<IDirectDepositType> directDepositTypes)
  {
    this._directDepositTypes = directDepositTypes;
  }

  public IEnumerable<DirectDepositType> GetDirectDepositTypes()
  {
    foreach (IDirectDepositType directDepositType in this._directDepositTypes)
    {
      if (directDepositType.IsActive())
        yield return directDepositType.GetDirectDepositType();
    }
  }

  public IEnumerable<PaymentMethodDetail> GetDefaults(string code)
  {
    foreach (IDirectDepositType directDepositType in this._directDepositTypes)
    {
      if (directDepositType.IsActive() && directDepositType.GetDirectDepositType().Code == code)
        return directDepositType.GetDefaults();
    }
    return Enumerable.Empty<PaymentMethodDetail>();
  }

  public void SetPaymentMethodDefaults(PXCache cache)
  {
    PaymentMethod current = (PaymentMethod) cache.Current;
    foreach (IDirectDepositType directDepositType in this._directDepositTypes)
    {
      if (directDepositType.IsActive() && directDepositType.GetDirectDepositType().Code == current.DirectDepositFileFormat)
      {
        directDepositType.SetPaymentMethodDefaults(cache);
        break;
      }
    }
  }

  public bool AllowChangeExportMethod(PXCache cache)
  {
    PaymentMethod current = (PaymentMethod) cache.Current;
    foreach (IDirectDepositType directDepositType in this._directDepositTypes)
    {
      if (directDepositType.IsActive() && directDepositType.GetDirectDepositType().Code == current.DirectDepositFileFormat)
        return directDepositType.AllowChangeExportMethod();
    }
    return true;
  }
}
