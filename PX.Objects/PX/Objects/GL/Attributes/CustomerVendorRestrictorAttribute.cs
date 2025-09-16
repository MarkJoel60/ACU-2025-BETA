// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.CustomerVendorRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.GL.Attributes;

public class CustomerVendorRestrictorAttribute : PXRestrictorAttribute
{
  public CustomerVendorRestrictorAttribute()
    : base(typeof (Where<BAccountR.type, NotEqual<BAccountType.branchType>, And<BAccountR.type, NotEqual<BAccountType.organizationType>, And<BAccountR.type, NotEqual<BAccountType.prospectType>>>>), "A vendor, customer, or employee business account can be specified.", Array.Empty<System.Type>())
  {
  }
}
