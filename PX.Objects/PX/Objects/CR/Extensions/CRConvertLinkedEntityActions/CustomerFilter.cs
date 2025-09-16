// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRConvertLinkedEntityActions.CustomerFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CR.Extensions.CRCreateActions;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRConvertLinkedEntityActions;

/// <exclude />
[PXHidden]
[Serializable]
public class CustomerFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IClassIdFilter
{
  [PXDefault]
  [PXDimensionSelector("BIZACCT", typeof (Search<PX.Objects.CR.BAccount.acctCD>), typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXUIField(DisplayName = "Customer ID", Enabled = false)]
  public virtual 
  #nullable disable
  string AcctCD { get; set; }

  [PXString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Customer Class", Required = true)]
  [PXDefault(typeof (Search2<ARSetup.dfltCustomerClassID, InnerJoin<CustomerClass, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>, Where<CustomerClass.orgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>))]
  [PXSelector(typeof (Search<CustomerClass.customerClassID, Where<CustomerClass.orgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And<MatchUser>>>), CacheGlobal = true, DescriptionField = typeof (CustomerClass.descr))]
  public virtual string ClassID { get; set; }

  [PXDBEmail]
  [PXDefault]
  [PXUIField(DisplayName = "Customer Email", Required = false)]
  public virtual string Email { get; set; }

  [PXString]
  [PXUIField(DisplayName = "", IsReadOnly = true)]
  public virtual string WarningMessage { get; set; }

  public abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerFilter.acctCD>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerFilter.classID>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerFilter.email>
  {
  }

  public abstract class warningMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerFilter.warningMessage>
  {
  }
}
