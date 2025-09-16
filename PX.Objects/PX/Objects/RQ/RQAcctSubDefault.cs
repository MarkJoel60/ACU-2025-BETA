// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.RQ;

/// <summary>Helper. Used to define expence sub. in request.</summary>
public class RQAcctSubDefault
{
  /// <summary>Mask for inventory item.</summary>
  public const string MaskItem = "I";
  /// <summary>Mask for requester.</summary>
  public const string MaskRequester = "R";
  /// <summary>Mask for department.</summary>
  public const string MaskDepartment = "D";
  /// <summary>Mask for request class.</summary>
  public const string MaskClass = "Q";

  /// <summary>List of allowed source to compune sub. in request.</summary>
  public class ClassListAttribute : PXCustomStringListAttribute
  {
    public ClassListAttribute()
      : base(new string[4]{ "Q", "D", "I", "R" }, new string[4]
      {
        "Request Class",
        "Department",
        "Inventory Item",
        "Requester"
      })
    {
    }
  }
}
