// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorBusinessAccount_CU_PR_VCAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorBusinessAccount_CU_PR_VCAttribute : FSSelectorBusinessAccount_BaseAttribute
{
  public FSSelectorBusinessAccount_CU_PR_VCAttribute()
    : base("BIZACCT", typeof (Where2<Where<BAccountSelectorBase.type, Equal<BAccountType.prospectType>, And<PX.Objects.AR.Customer.bAccountID, IsNull>>, Or<Where<Where2<Where<BAccountSelectorBase.type, Equal<BAccountType.customerType>, Or<BAccountSelectorBase.type, Equal<BAccountType.combinedType>>>, And<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>))
  {
  }
}
