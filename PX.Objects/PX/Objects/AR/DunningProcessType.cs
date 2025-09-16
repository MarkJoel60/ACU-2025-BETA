// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DunningProcessType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AR;

public class DunningProcessType
{
  public const int ProcessByCustomer = 0;
  public const int ProcessByDocument = 1;

  public class ListAttribute : PXIntListAttribute
  {
    public ListAttribute()
      : base(new int[2]{ 0, 1 }, new string[2]
      {
        "By Customer",
        "By Document"
      })
    {
    }
  }
}
