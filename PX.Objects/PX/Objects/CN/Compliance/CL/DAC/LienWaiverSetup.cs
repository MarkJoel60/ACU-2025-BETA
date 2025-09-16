// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CN.Common.DAC;
using PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiver;
using PX.Objects.CN.Compliance.CL.Graphs;
using System;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.DAC;

/// <summary>
/// Represents the tenant-level compliance preferences record that
/// contains settings for configuring the generation of lien waivers.
/// The single record of this type is created and edited on the Compliance Preferences (CL301000) form
/// (which corresponds to the <see cref="T:PX.Objects.CN.Compliance.CL.Graphs.ComplianceDocumentSetupMaint" /> graph).
/// </summary>
[PXCacheName("Compliance Preferences")]
[PXPrimaryGraph(typeof (ComplianceDocumentSetupMaint))]
public class LienWaiverSetup : BaseCache, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Warn Users During AP Bill Entry")]
  public virtual bool? ShouldWarnOnBillEntry { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Warn Users During Bill Selection for Payment")]
  public virtual bool? ShouldWarnOnPayment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Prevent AP Bill Payment")]
  public virtual bool? ShouldStopPayments { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatically Generate Lien Waivers")]
  public virtual bool? ShouldGenerateConditional { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatically Generate Lien Waivers")]
  public virtual bool? ShouldGenerateUnconditional { get; set; }

  [PXDBBool]
  [PXUIEnabled(typeof (Where<BqlOperand<LienWaiverSetup.shouldGenerateConditional, IBqlBool>.IsEqual<True>>))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Generate for AP Documents Not Linked to Commitments")]
  public virtual bool? GenerateWithoutCommitmentConditional { get; set; }

  [PXDBBool]
  [PXUIEnabled(typeof (Where<BqlOperand<LienWaiverSetup.shouldGenerateUnconditional, IBqlBool>.IsEqual<True>>))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Generate for AP Documents Not Linked to Commitments")]
  public virtual bool? GenerateWithoutCommitmentUnconditional { get; set; }

  [PXDBString]
  [PXDefault("Paying AP Bill")]
  [LienWaiverGenerationEvent.List]
  [PXUIField(DisplayName = "Generate Lien Waivers on", Enabled = false)]
  public virtual 
  #nullable disable
  string GenerationEventConditional { get; set; }

  [PXDBString]
  [PXDefault("Paying AP Bill")]
  [LienWaiverGenerationEvent.List]
  [PXUIField(DisplayName = "Generate Lien Waivers on", Enabled = false)]
  public virtual string GenerationEventUnconditional { get; set; }

  [PXDBString]
  [PXUIEnabled(typeof (Where<BqlOperand<LienWaiverSetup.shouldGenerateConditional, IBqlBool>.IsEqual<True>>))]
  [PXDefault("Posting Period End Date")]
  [LienWaiverThroughDateSource.List]
  [PXUIField(DisplayName = "Through Date")]
  public virtual string ThroughDateSourceConditional { get; set; }

  [PXDBString]
  [PXUIEnabled(typeof (Where<BqlOperand<LienWaiverSetup.shouldGenerateUnconditional, IBqlBool>.IsEqual<True>>))]
  [PXDefault("AP Check Date")]
  [LienWaiverThroughDateSource.List]
  [PXUIField(DisplayName = "Through Date")]
  public virtual string ThroughDateSourceUnconditional { get; set; }

  [PXDBString(10)]
  [PXUIEnabled(typeof (Where<BqlOperand<LienWaiverSetup.shouldGenerateConditional, IBqlBool>.IsEqual<True>>))]
  [PXDefault("CP")]
  [LienWaiverGroupBySource.List]
  [PXUIField(DisplayName = "Calculate Amount By")]
  public virtual string GroupByConditional { get; set; }

  [PXDBString(10)]
  [PXUIEnabled(typeof (Where<BqlOperand<LienWaiverSetup.shouldGenerateUnconditional, IBqlBool>.IsEqual<True>>))]
  [PXDefault("CP")]
  [LienWaiverGroupBySource.List]
  [PXUIField(DisplayName = "Calculate Amount By")]
  public virtual string GroupByUnconditional { get; set; }

  public abstract class shouldWarnOnBillEntry : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LienWaiverSetup.shouldWarnOnBillEntry>
  {
  }

  public abstract class shouldWarnOnPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LienWaiverSetup.shouldWarnOnPayment>
  {
  }

  public abstract class shouldStopPayments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LienWaiverSetup.shouldStopPayments>
  {
  }

  public abstract class shouldGenerateConditional : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LienWaiverSetup.shouldGenerateConditional>
  {
  }

  public abstract class shouldGenerateUnconditional : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LienWaiverSetup.shouldGenerateUnconditional>
  {
  }

  public abstract class generateWithoutCommitmentConditional : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LienWaiverSetup.generateWithoutCommitmentConditional>
  {
  }

  public abstract class generateWithoutCommitmentUnconditional : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LienWaiverSetup.generateWithoutCommitmentUnconditional>
  {
  }

  public abstract class generationEventConditional : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverSetup.generationEventConditional>
  {
  }

  public abstract class generationEventUnconditional : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverSetup.generationEventUnconditional>
  {
  }

  public abstract class throughDateSourceConditional : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverSetup.throughDateSourceConditional>
  {
  }

  public abstract class throughDateSourceUnconditional : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverSetup.throughDateSourceUnconditional>
  {
  }

  public abstract class groupByConditional : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverSetup.groupByConditional>
  {
  }

  public abstract class groupByUnconditional : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverSetup.groupByUnconditional>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LienWaiverSetup.noteID>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  LienWaiverSetup.tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LienWaiverSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LienWaiverSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LienWaiverSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LienWaiverSetup.lastModifiedDateTime>
  {
  }
}
