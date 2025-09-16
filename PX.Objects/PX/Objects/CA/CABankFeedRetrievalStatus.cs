// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedRetrievalStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class CABankFeedRetrievalStatus
{
  public const string None = "N";
  public const string Success = "S";
  public const string Error = "E";
  public const string LoginFailed = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(CABankFeedRetrievalStatus.ListAttribute.GetStatuses)
    {
    }

    public static (string, string)[] GetStatuses
    {
      get
      {
        return new (string, string)[4]
        {
          ("N", "None"),
          ("E", "Error"),
          ("S", "Success"),
          ("L", "Login Failed")
        };
      }
    }
  }
}
