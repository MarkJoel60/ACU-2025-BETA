// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.CustomerRLS
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA.Light;

/// <summary>
/// AR-specific business account data related to customer payment methods,
/// statement cycles, and credit verification rules.
/// </summary>
/// <remarks>This is defind to be used in access control (Row Level Secutity)</remarks>
[PXHidden]
public class CustomerRLS : PX.Objects.AR.Customer
{
  /// <summary>The identifier of the business account.</summary>
  [PXDBIdentity(BqlTable = typeof (PX.Objects.AR.Customer))]
  public new virtual int? BAccountID { get; set; }

  /// <summary>
  /// The group mask of the customer. The value of the field
  /// is used for the purposes of access control.
  /// </summary>
  [PXDBBinary(BqlTable = typeof (PX.Objects.AR.Customer))]
  public new virtual 
  #nullable disable
  byte[] GroupMask { get; set; }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerRLS.bAccountID>
  {
  }

  public new abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CustomerRLS.groupMask>
  {
  }
}
