// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.IAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public interface IAddress : IAddressBase, IValidatedAddress, INotable
{
  int? AddressID { get; set; }

  int? BAccountID { get; set; }

  int? BAccountAddressID { get; set; }

  int? RevisionID { get; set; }

  bool? IsDefaultAddress { get; set; }
}
