// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.DirectDepositTypeListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public class DirectDepositTypeListAttribute : PXStringListAttribute
{
  [InjectDependency]
  protected DirectDepositTypeService DirectDepositService { get; set; }

  public virtual void CacheAttached(PXCache sender)
  {
    IEnumerable<DirectDepositType> directDepositTypes = this.DirectDepositService?.GetDirectDepositTypes() ?? (IEnumerable<DirectDepositType>) new List<DirectDepositType>();
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    foreach (DirectDepositType directDepositType in directDepositTypes)
    {
      stringList1.Add(directDepositType.Code);
      stringList2.Add(directDepositType.Description);
    }
    this._AllowedValues = stringList1.ToArray();
    this._AllowedLabels = stringList2.ToArray();
    base.CacheAttached(sender);
  }
}
