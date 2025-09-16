// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRValidationProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CR;

public class CRValidationProcess : PXGraph<
#nullable disable
CRValidationProcess>
{
  public PXSetupSelect<CRSetup> Setup;
  public PXFilter<CRValidationProcess.ValidationFilter> Filter;
  [PXViewDetailsButton(typeof (CRValidationProcess.ValidationFilter), typeof (Select2<BAccount, InnerJoin<Contact, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>, Where<Contact.contactID, Equal<Current<Contact.contactID>>>>))]
  [PXViewDetailsButton(typeof (CRValidationProcess.ValidationFilter), typeof (Select<Contact, Where<Contact.contactID, Equal<Current<Contact.contactID>>>>))]
  public PXProcessingViewOf<ContactAccountLead>.BasedOn<SelectFromBase<ContactAccountLead, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  Contact.contactType, 
  #nullable disable
  Equal<ContactTypesAttribute.bAccountProperty>>>>, And<BqlOperand<
  #nullable enable
  ContactAccountLead.type, IBqlString>.IsNotIn<
  #nullable disable
  BAccountType.branchType, BAccountType.organizationType>>>, And<BqlOperand<
  #nullable enable
  ContactAccount.defContactID, IBqlInt>.IsEqual<
  #nullable disable
  Contact.contactID>>>>.And<BqlOperand<
  #nullable enable
  ContactAccountLead.accountStatus, IBqlString>.IsNotEqual<
  #nullable disable
  CustomerStatus.inactive>>>>>.Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  Contact.contactType, 
  #nullable disable
  In3<ContactTypesAttribute.lead, ContactTypesAttribute.person>>>>>.And<BqlOperand<
  #nullable enable
  Contact.isActive, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  CRValidationProcess.ValidationFilter.validationType>, 
  #nullable disable
  Equal<CRValidationProcess.ValidationFilter.validateAll>>>>>.Or<BqlOperand<
  #nullable enable
  Contact.duplicateStatus, IBqlString>.IsEqual<
  #nullable disable
  DuplicateStatusAttribute.notValidated>>>>>>.FilteredBy<CRValidationProcess.ValidationFilter> Contacts;
  public PXCancel<CRValidationProcess.ValidationFilter> Cancel;

  public CRValidationProcess()
  {
    ((PXGraph) this).Actions["Process"].SetVisible(false);
    ((PXAction) this.Cancel).SetVisible(false);
    ((PXGraph) this).Actions.Move("Process", nameof (Cancel));
    if (((PXSelectBase<CRSetup>) this.Setup).Current == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (CRValidation), new object[1]
      {
        (object) typeof (CRValidation).Name
      });
    ((PXProcessingBase<ContactAccountLead>) this.Contacts).ParallelProcessingOptions = (Action<PXParallelProcessingOptions>) (settings => settings.IsEnabled = true);
  }

  public virtual void _(
    PX.Data.Events.RowSelected<CRValidationProcess.ValidationFilter> e)
  {
    CRValidationProcess.ValidationFilter row = e.Row;
    // ISSUE: method pointer
    ((PXProcessingBase<ContactAccountLead>) this.Contacts).SetProcessDelegate(new PXProcessingBase<ContactAccountLead>.ProcessListDelegate((object) null, __methodptr(ProcessValidation)));
  }

  private static void ProcessValidation(List<ContactAccountLead> list)
  {
    PXCache cache = PXGraph.CreateInstance<PXGraph>().Caches[typeof (ContactAccountLead)];
    Dictionary<System.Type, PXGraph> graphs = new Dictionary<System.Type, PXGraph>();
    IEnumerable<(PXGraph, ContactAccountLead, object)> tuples = list.Select<ContactAccountLead, (PXGraph, ContactAccountLead, object)>((Func<ContactAccountLead, (PXGraph, ContactAccountLead, object)>) (i =>
    {
      object copy = cache.CreateCopy((object) i);
      System.Type key;
      PXPrimaryGraphAttribute.FindPrimaryGraph(cache, ref copy, ref key);
      PXGraph instance;
      if (!graphs.TryGetValue(key, out instance))
        graphs.Add(key, instance = PXGraph.CreateInstance(key));
      return (instance, i, copy);
    }));
    int num = 0;
    foreach ((PXGraph graph, ContactAccountLead contactAccountLead, object entity) in tuples)
    {
      PXProcessing.SetCurrentItem((object) contactAccountLead);
      graph.Views[graph.PrimaryView].Cache.Current = entity;
      try
      {
        object obj = PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<PXGraph, BAccount>.RunActionWithAppliedSearch(graph, entity, "CheckForDuplicates");
        Contact contact = PXResult.Unwrap<Contact>(obj) ?? (Contact) PXResult.Unwrap<CRLead>(obj);
        if (contact != null)
        {
          contactAccountLead.DuplicateFound = contact.DuplicateFound;
          contactAccountLead.DuplicateStatus = contact.DuplicateStatus;
          PXProcessing.SetProcessed<ContactAccountLead>();
        }
        else
          PXProcessing.SetError<ContactAccountLead>(num, "Duplicate validation for the record cannot be performed. Contact your system administrator.");
      }
      catch (Exception ex)
      {
        PXProcessing.SetError<ContactAccountLead>(num, ex);
      }
      graph.Clear();
      ++num;
    }
  }

  [PXHidden]
  public class ValidationFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public const int ValidateNewAndUpdated = 0;
    public const int ValidateAll = 1;

    [PXInt]
    [PXDefault(0)]
    [PXIntList(new int[] {0, 1}, new string[] {"Validate Only New and Updated Records", "Validate All Records"})]
    [PXUIField(DisplayName = "Validation Type")]
    public virtual int? ValidationType { get; set; }

    public class validateNewAndUpdated : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CRValidationProcess.ValidationFilter.validateNewAndUpdated>
    {
      public validateNewAndUpdated()
        : base(0)
      {
      }
    }

    public class validateAll : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CRValidationProcess.ValidationFilter.validateAll>
    {
      public validateAll()
        : base(1)
      {
      }
    }

    public abstract class validationType : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CRValidationProcess.ValidationFilter.validationType>
    {
    }
  }
}
