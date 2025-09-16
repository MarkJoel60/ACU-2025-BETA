// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.PONotificationSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PO;

public class PONotificationSource
{
  public const 
  #nullable disable
  string Vendor = "Vendor";

  public class vendor : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PONotificationSource.vendor>
  {
    public vendor()
      : base("Vendor")
    {
    }
  }
}
