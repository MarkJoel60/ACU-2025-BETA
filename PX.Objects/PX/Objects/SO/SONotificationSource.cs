// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SONotificationSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO;

public class SONotificationSource
{
  public const 
  #nullable disable
  string Customer = "Customer";

  public class customer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SONotificationSource.customer>
  {
    public customer()
      : base("Customer")
    {
    }
  }
}
