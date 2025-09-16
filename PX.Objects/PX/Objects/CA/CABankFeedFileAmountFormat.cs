// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedFileAmountFormat
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class CABankFeedFileAmountFormat
{
  public const string SameColumn = "S";
  public const string DifferentColumns = "D";
  public const string DependsOnParameter = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(CABankFeedFileAmountFormat.ListAttribute.GetTypes)
    {
    }

    public static (string, string)[] GetTypes
    {
      get
      {
        return new (string, string)[3]
        {
          ("S", "Debit/Credit in Same Column"),
          ("D", "Debit and Credit in Different Columns"),
          ("P", "Debit/Credit Property in Separate Column")
        };
      }
    }
  }
}
