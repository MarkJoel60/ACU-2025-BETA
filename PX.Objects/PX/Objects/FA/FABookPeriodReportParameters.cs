// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookPeriodReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.DAC;

#nullable enable
namespace PX.Objects.FA;

public class FABookPeriodReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(false, typeof (Search2<PX.Objects.GL.DAC.Organization.organizationID, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>, And2<FeatureInstalled<FeaturesSet.branch>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>>))]
  public virtual int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (FABookPeriodReportParameters.organizationID), false, typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>, CrossJoin<FeaturesSet>>, Where<FeaturesSet.branch, Equal<True>, And<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.withoutBranches>, And<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>>), null)]
  public virtual int? BranchID { get; set; }

  [OrganizationTree(typeof (FABookPeriodReportParameters.organizationID), typeof (FABookPeriodReportParameters.branchID), null, false)]
  public virtual int? OrgBAccountID { get; set; }

  [PXDBInt]
  [GLBookDefault]
  [PXSelector(typeof (FABook.bookID), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXUIField(DisplayName = "Book")]
  public virtual int? BookID { get; set; }

  [PXDBString]
  [FABookPeriodSelector(null, null, null, typeof (FABookPeriodReportParameters.bookID), false, null, typeof (AccessInfo.businessDate), typeof (FABookPeriodReportParameters.branchID), null, typeof (FABookPeriodReportParameters.organizationID), null, ReportParametersFlag.Organization | ReportParametersFlag.Branch | ReportParametersFlag.Book)]
  public virtual 
  #nullable disable
  string CurrentFABookPeriodIDByOrganizationBranchBook { get; set; }

  [PXDBString]
  [FABookPeriodSelector(null, null, null, typeof (FABookPeriodReportParameters.bookID), false, null, typeof (AccessInfo.businessDate), null, null, typeof (FABookPeriodReportParameters.organizationID), null, ReportParametersFlag.Organization | ReportParametersFlag.Book)]
  public virtual string CurrentFABookPeriodIDByOrganizationBook { get; set; }

  [PXDBString]
  [FABookPeriodSelector(null, null, null, typeof (FABookPeriodReportParameters.bookID), false, null, typeof (AccessInfo.businessDate), typeof (FABookPeriodReportParameters.branchID), null, null, null, ReportParametersFlag.Branch | ReportParametersFlag.Book)]
  public virtual string CurrentFABookPeriodIDByBranchBook { get; set; }

  [PXDBString]
  [FABookPeriodSelector(null, null, null, typeof (FABookPeriodReportParameters.bookID), false, null, typeof (AccessInfo.businessDate), typeof (FABookPeriodReportParameters.branchID), null, null, null, ReportParametersFlag.BAccount | ReportParametersFlag.Book)]
  public virtual string CurrentFABookPeriodIDByBAccountBook { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FABookPeriodReportParameters.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookPeriodReportParameters.branchID>
  {
  }

  public abstract class orgBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FABookPeriodReportParameters.orgBAccountID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookPeriodReportParameters.bookID>
  {
  }

  public abstract class currentFABookPeriodIDByOrganizationBranchBook : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookPeriodReportParameters.currentFABookPeriodIDByOrganizationBranchBook>
  {
  }

  public abstract class currentFABookPeriodIDByOrganizationBook : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookPeriodReportParameters.currentFABookPeriodIDByOrganizationBook>
  {
  }

  public abstract class currentFABookPeriodIDByBranchBook : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookPeriodReportParameters.currentFABookPeriodIDByBranchBook>
  {
  }

  public abstract class currentFABookPeriodIDByBAccountBook : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookPeriodReportParameters.currentFABookPeriodIDByBAccountBook>
  {
  }
}
