// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INNotificationSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

public class INNotificationSource
{
  public const 
  #nullable disable
  string None = "None";

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INNotificationSource.none>
  {
    public none()
      : base("None")
    {
    }
  }
}
