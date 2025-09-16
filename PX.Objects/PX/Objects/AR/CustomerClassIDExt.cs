// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerClassIDExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Extension is need to force set <see cref="P:PX.Objects.AR.Customer.ClassID" /> even if there is some extension for <see cref="P:PX.Objects.CR.BAccount.ClassID" /> over <see cref="T:PX.Objects.CR.BAccount" />
/// which will override attributes even for <see cref="T:PX.Objects.AR.Customer" />.
/// </summary>
[PXInternalUseOnly]
public sealed class CustomerClassIDExt : PXCacheExtension<Customer>
{
  [PXString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  public string ClassID => this.Base.CustomerClassID;
}
