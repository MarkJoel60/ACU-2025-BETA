// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Group
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.PM;

public class Group
{
  private bool statReady;
  private bool hasMixedInventory;
  private bool hasMixedUOM;
  private bool hasMixedBAccount;
  private bool hasMixedBAccountLoc;
  private bool hasMixedDescription;
  private bool hasMixedAccountID;
  private bool hasMixedSubID;

  public System.Collections.Generic.List<PMTran> List { get; private set; }

  public Group() => this.List = new System.Collections.Generic.List<PMTran>();

  public bool HasMixedInventory
  {
    get
    {
      if (!this.statReady)
        this.InitStatistics();
      return this.hasMixedInventory;
    }
  }

  public bool HasMixedUOM
  {
    get
    {
      if (!this.statReady)
        this.InitStatistics();
      return this.hasMixedUOM;
    }
  }

  public bool HasMixedBAccount
  {
    get
    {
      if (!this.statReady)
        this.InitStatistics();
      return this.hasMixedBAccount;
    }
  }

  public bool HasMixedBAccountLoc
  {
    get
    {
      if (!this.statReady)
        this.InitStatistics();
      return this.hasMixedBAccountLoc;
    }
  }

  public bool HasMixedDescription
  {
    get
    {
      if (!this.statReady)
        this.InitStatistics();
      return this.hasMixedDescription;
    }
  }

  public bool HasMixedAccountID
  {
    get
    {
      if (!this.statReady)
        this.InitStatistics();
      return this.hasMixedAccountID;
    }
  }

  public bool HasMixedSubID
  {
    get
    {
      if (!this.statReady)
        this.InitStatistics();
      return this.hasMixedSubID;
    }
  }

  private void InitStatistics()
  {
    if (this.List.Count <= 0)
      return;
    int num1 = this.List[0].InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID;
    string uom = this.List[0].UOM;
    int? baccountId = this.List[0].BAccountID;
    int? locationId = this.List[0].LocationID;
    string invoicedDescription = this.List[0].InvoicedDescription;
    int? accountId = this.List[0].AccountID;
    int? subId = this.List[0].SubID;
    for (int index = 1; index < this.List.Count; ++index)
    {
      int num2 = num1;
      int? nullable1 = this.List[index].InventoryID;
      int valueOrDefault = nullable1.GetValueOrDefault();
      if (!(num2 == valueOrDefault & nullable1.HasValue))
        this.hasMixedInventory = true;
      if (string.IsNullOrEmpty(uom))
        uom = this.List[index].UOM;
      else if (!string.IsNullOrEmpty(this.List[index].UOM) && uom != this.List[index].UOM)
        this.hasMixedUOM = true;
      nullable1 = baccountId;
      int? nullable2 = this.List[index].BAccountID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        this.hasMixedBAccount = true;
      nullable2 = locationId;
      nullable1 = this.List[index].LocationID;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        this.hasMixedBAccountLoc = true;
      if (invoicedDescription != this.List[index].InvoicedDescription)
        this.hasMixedDescription = true;
      nullable1 = accountId;
      nullable2 = this.List[index].AccountID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        this.hasMixedAccountID = true;
      nullable2 = subId;
      nullable1 = this.List[index].SubID;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        this.hasMixedSubID = true;
    }
    this.statReady = true;
  }
}
