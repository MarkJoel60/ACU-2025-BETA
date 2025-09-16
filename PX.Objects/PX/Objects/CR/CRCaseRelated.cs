// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseRelated
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR.Workflows;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
[Serializable]
public class CRCaseRelated : CRCase
{
  [PXDBString(1, IsFixed = true)]
  [CaseWorkflow.States.List(BqlField = typeof (CRCase.status))]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public override 
  #nullable disable
  string Status { get; set; }

  [Owner(typeof (CRCase.workgroupID), Enabled = false)]
  public override int? OwnerID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup", Enabled = false)]
  [PXCompanyTreeSelector]
  public override int? WorkgroupID { get; set; }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCaseRelated.status>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCaseRelated.ownerID>
  {
  }

  public new abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCaseRelated.workgroupID>
  {
  }
}
