// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeItemBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeItemBase
{
  public readonly string Address;

  public PXExchangeItemBase(string mailbox) => this.Address = mailbox;

  public override int GetHashCode() => this.Address.GetHashCode();

  public override bool Equals(object obj)
  {
    return obj is PXExchangeItemBase && string.Equals(this.Address, (obj as PXExchangeItemBase).Address);
  }
}
