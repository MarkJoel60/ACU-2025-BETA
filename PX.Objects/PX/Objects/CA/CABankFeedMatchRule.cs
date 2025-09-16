// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedMatchRule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CABankFeedMatchRule
{
  public const string Empty = "N";
  public const string StartsWith = "S";
  public const string Contains = "C";
  public const string EndsWith = "E";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute(bool allowEmptyValue)
    {
      string[] sourceArray1 = new string[3]{ "S", "C", "E" };
      string[] sourceArray2 = new string[3]
      {
        "Starts With",
        "Contains",
        "Ends With"
      };
      string[] destinationArray1 = (string[]) null;
      string[] destinationArray2 = (string[]) null;
      if (allowEmptyValue)
      {
        destinationArray1 = new string[4]
        {
          "N",
          null,
          null,
          null
        };
        destinationArray2 = new string[4]
        {
          " ",
          null,
          null,
          null
        };
        Array.Copy((Array) sourceArray1, 0, (Array) destinationArray1, 1, 3);
        Array.Copy((Array) sourceArray2, 0, (Array) destinationArray2, 1, 3);
      }
      if (destinationArray1 != null)
      {
        this._AllowedValues = destinationArray1;
        this._AllowedLabels = destinationArray2;
      }
      else
      {
        this._AllowedValues = sourceArray1;
        this._AllowedLabels = sourceArray2;
      }
      this._NeutralAllowedLabels = this._AllowedLabels;
    }
  }
}
