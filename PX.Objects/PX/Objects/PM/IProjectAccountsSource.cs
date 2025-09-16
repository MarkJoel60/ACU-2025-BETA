// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.IProjectAccountsSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.PM;

public interface IProjectAccountsSource
{
  int? ProjectID { get; set; }

  int? DefaultSalesAccountID { get; set; }

  int? DefaultSalesSubID { get; set; }

  int? DefaultExpenseAccountID { get; set; }

  int? DefaultExpenseSubID { get; set; }

  int? DefaultAccrualAccountID { get; set; }

  int? DefaultAccrualSubID { get; set; }
}
