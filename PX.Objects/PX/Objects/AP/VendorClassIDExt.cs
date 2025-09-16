// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorClassIDExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// Extension is need to force set <see cref="P:PX.Objects.AP.Vendor.ClassID" /> even if there is some extension for <see cref="P:PX.Objects.CR.BAccount.ClassID" /> over <see cref="T:PX.Objects.CR.BAccount" />
/// which will override attributes even for <see cref="T:PX.Objects.AP.Vendor" />.
/// </summary>
[PXInternalUseOnly]
public sealed class VendorClassIDExt : PXCacheExtension<Vendor>
{
  [PXString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Class ID", Visibility = PXUIVisibility.Invisible)]
  public string ClassID => this.Base.VendorClassID;
}
