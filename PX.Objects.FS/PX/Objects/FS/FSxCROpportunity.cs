// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxCROpportunity
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.FS;

public class FSxCROpportunity : PXCacheExtension<
#nullable disable
CROpportunity>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<CROpportunity.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField(DisplayName = "Service Order Type")]
  [PXDefault]
  [FSSelectorActiveSrvOrdType]
  [PXFormula(typeof (Default<FSxCROpportunity.sDEnabled>))]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<FSServiceOrder.refNbr, Where<FSServiceOrder.srvOrdType, Equal<Current<FSxCROpportunity.srvOrdType>>>>))]
  [PXDefault]
  public virtual string ServiceOrderRefNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Service Order")]
  public virtual bool? SDEnabled { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXDefault]
  public virtual bool? chkServiceManagement => new bool?(true);

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxCROpportunity.branchLocationID>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxCROpportunity.srvOrdType>
  {
  }

  public abstract class serviceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxCROpportunity.serviceOrderRefNbr>
  {
  }

  public abstract class sDEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxCROpportunity.sDEnabled>
  {
  }

  public abstract class ChkServiceManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxCROpportunity.ChkServiceManagement>
  {
  }
}
