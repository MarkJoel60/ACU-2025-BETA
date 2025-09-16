// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ILocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CR;

public interface ILocation
{
  int? LocationID { get; set; }

  bool? IsActive { get; set; }

  int? CARAccountID { get; set; }

  int? CARSubID { get; set; }

  int? CSalesAcctID { get; set; }

  int? CSalesSubID { get; set; }

  bool? IsARAccountSameAsMain { get; set; }

  int? VAPAccountID { get; set; }

  int? VAPSubID { get; set; }

  int? VExpenseAcctID { get; set; }

  int? VExpenseSubID { get; set; }

  bool? IsAPAccountSameAsMain { get; set; }

  int? CDiscountAcctID { get; set; }

  int? CDiscountSubID { get; set; }

  int? CFreightAcctID { get; set; }

  int? CFreightSubID { get; set; }
}
