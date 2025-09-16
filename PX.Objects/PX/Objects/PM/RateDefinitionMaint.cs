// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RateDefinitionMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class RateDefinitionMaint : PXGraph<
#nullable disable
RateDefinitionMaint>
{
  public PXSetup<PMSetup> Setup;
  public PXFilter<RateDefinitionMaint.PMRateDefinitionFilter> Filter;
  public PXSelect<PMRateDefinition, Where<PMRateDefinition.rateTypeID, Equal<Current<RateDefinitionMaint.PMRateDefinitionFilter.rateTypeID>>, And<PMRateDefinition.rateTableID, Equal<Current<RateDefinitionMaint.PMRateDefinitionFilter.rateTableID>>>>, OrderBy<Asc<PMRateDefinition.sequence>>> RateDefinitions;
  public PXSave<RateDefinitionMaint.PMRateDefinitionFilter> Save;
  public PXCancel<RateDefinitionMaint.PMRateDefinitionFilter> Cancel;
  public PXAction<RateDefinitionMaint.PMRateDefinitionFilter> viewRate;

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public void ViewRate()
  {
    if (((PXSelectBase<PMRateDefinition>) this.RateDefinitions).Current != null)
    {
      ((PXAction) this.Save).Press();
      RateMaint instance = PXGraph.CreateInstance<RateMaint>();
      ((PXSelectBase<PMRateSequence>) instance.RateSequence).Current = PXResultset<PMRateSequence>.op_Implicit(PXSelectBase<PMRateSequence, PXSelect<PMRateSequence, Where<PMRateSequence.rateTableID, Equal<Current<PMRateDefinition.rateTableID>>, And<PMRateSequence.rateTypeID, Equal<Current<PMRateDefinition.rateTypeID>>, And<PMRateSequence.sequence, Equal<Current<PMRateDefinition.sequence>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (((PXSelectBase<PMRateSequence>) instance.RateSequence).Current == null)
      {
        PMRateSequence pmRateSequence = ((PXSelectBase<PMRateSequence>) instance.RateSequence).Insert();
        ((PXSelectBase<PMRateSequence>) instance.RateSequence).SetValueExt<PMRateSequence.rateTableID>(pmRateSequence, (object) ((PXSelectBase<PMRateDefinition>) this.RateDefinitions).Current.RateTableID);
        ((PXSelectBase<PMRateSequence>) instance.RateSequence).SetValueExt<PMRateSequence.rateTypeID>(pmRateSequence, (object) ((PXSelectBase<PMRateDefinition>) this.RateDefinitions).Current.RateTypeID);
        ((PXSelectBase<PMRateSequence>) instance.RateSequence).SetValueExt<PMRateSequence.sequence>(pmRateSequence, (object) ((PXSelectBase<PMRateDefinition>) this.RateDefinitions).Current.Sequence.ToString());
      }
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Rate Maintenance - View Rates");
    }
  }

  protected virtual void PMRateDefinition_RateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PMRateDefinition))
      return;
    e.NewValue = (object) ((PXSelectBase<RateDefinitionMaint.PMRateDefinitionFilter>) this.Filter).Current.RateTypeID;
  }

  protected virtual void PMRateDefinition_RateTableID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PMRateDefinition))
      return;
    e.NewValue = (object) ((PXSelectBase<RateDefinitionMaint.PMRateDefinitionFilter>) this.Filter).Current.RateTableID;
  }

  protected virtual void PMRateDefinition_Sequence_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PMRateDefinition))
      return;
    e.NewValue = (object) this.GetNextSequence();
  }

  private short GetNextSequence()
  {
    PXSelect<PMRateDefinition, Where<PMRateDefinition.rateTypeID, Equal<Current<RateDefinitionMaint.PMRateDefinitionFilter.rateTypeID>>, And<PMRateDefinition.rateTableID, Equal<Current<RateDefinitionMaint.PMRateDefinitionFilter.rateTableID>>>>, OrderBy<Asc<PMRateDefinition.sequence>>> pxSelect = new PXSelect<PMRateDefinition, Where<PMRateDefinition.rateTypeID, Equal<Current<RateDefinitionMaint.PMRateDefinitionFilter.rateTypeID>>, And<PMRateDefinition.rateTableID, Equal<Current<RateDefinitionMaint.PMRateDefinitionFilter.rateTableID>>>>, OrderBy<Asc<PMRateDefinition.sequence>>>((PXGraph) this);
    short val2_1 = 1;
    short val2_2 = 0;
    object[] objArray = Array.Empty<object>();
    foreach (PXResult<PMRateDefinition> pxResult in ((PXSelectBase<PMRateDefinition>) pxSelect).Select(objArray))
    {
      PMRateDefinition pmRateDefinition = PXResult<PMRateDefinition>.op_Implicit(pxResult);
      ++val2_1;
      val2_2 = Math.Max(pmRateDefinition.Sequence.GetValueOrDefault(), val2_2);
    }
    return Math.Max((short) ((int) val2_2 + 1), val2_1);
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class PMRateDefinitionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _RateTableID;
    protected string _RateTypeID;

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Rate Table Code")]
    [PXSelector(typeof (PMRateTable.rateTableID), DescriptionField = typeof (PMRateTable.description))]
    public virtual string RateTableID
    {
      get => this._RateTableID;
      set => this._RateTableID = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXSelector(typeof (PMRateType.rateTypeID), DescriptionField = typeof (PMRateType.description))]
    [PXUIField(DisplayName = "Rate Type")]
    public virtual string RateTypeID
    {
      get => this._RateTypeID;
      set => this._RateTypeID = value;
    }

    public abstract class rateTableID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RateDefinitionMaint.PMRateDefinitionFilter.rateTableID>
    {
    }

    public abstract class rateTypeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RateDefinitionMaint.PMRateDefinitionFilter.rateTypeID>
    {
    }
  }
}
