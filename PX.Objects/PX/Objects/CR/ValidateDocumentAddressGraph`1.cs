// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ValidateDocumentAddressGraph`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class ValidateDocumentAddressGraph<TGraph> : PXGraph<TGraph> where TGraph : PXGraph, new()
{
  protected static readonly System.Type[] AddressFieldsToValidate = new System.Type[6]
  {
    typeof (UnvalidatedAddress.addressLine1),
    typeof (UnvalidatedAddress.addressLine2),
    typeof (UnvalidatedAddress.city),
    typeof (UnvalidatedAddress.state),
    typeof (UnvalidatedAddress.countryID),
    typeof (UnvalidatedAddress.postalCode)
  };
  [PXCacheName("Filter")]
  public PXFilter<ValidateDocumentAddressFilter> Filter;
  [PXCacheName("Address")]
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<UnvalidatedAddress, ValidateDocumentAddressFilter> DocumentAddresses;
  public PXCancel<ValidateDocumentAddressFilter> Cancel;
  public PXAction<UnvalidatedAddress> viewDocument;

  public virtual IEnumerable documentAddresses()
  {
    IEnumerable enumerable = (IEnumerable) new List<UnvalidatedAddress>();
    if (!string.IsNullOrEmpty(((PXSelectBase<ValidateDocumentAddressFilter>) this.Filter).Current?.DocumentType))
      enumerable = ((PXSelectBase) this.DocumentAddresses).Cache.Inserted.Count() > 0L ? this.GetCachedAddressRecords(((PXSelectBase<ValidateDocumentAddressFilter>) this.Filter).Current) : this.GetAddressRecords(((PXSelectBase<ValidateDocumentAddressFilter>) this.Filter).Current);
    return enumerable;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    UnvalidatedAddress current = ((PXSelectBase<UnvalidatedAddress>) this.DocumentAddresses).Current;
    if (current != null && !string.IsNullOrEmpty(current.DocumentNbr))
    {
      PXGraph docGraph = ValidateDocumentAddressGraph<TGraph>.GetDocGraph(current.Document);
      if (docGraph != null)
      {
        docGraph.Views[docGraph.PrimaryView].Cache.Current = (object) current.Document;
        if (docGraph.Views[docGraph.PrimaryView]?.Cache?.Current != null)
        {
          PXRedirectRequiredException requiredException = new PXRedirectRequiredException(docGraph, true, string.Empty);
          ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException;
        }
      }
    }
    return adapter.Get();
  }

  public ValidateDocumentAddressGraph()
  {
    ((PXProcessingBase<UnvalidatedAddress>) this.DocumentAddresses).SetSelected<UnvalidatedAddress.selected>();
    ((PXProcessing<UnvalidatedAddress>) this.DocumentAddresses).SetProcessCaption(PXMessages.LocalizeNoPrefix("Validate"));
    ((PXProcessing<UnvalidatedAddress>) this.DocumentAddresses).SetProcessAllCaption(PXMessages.LocalizeNoPrefix("Validate All"));
    ((PXGraph) this).Actions.Move("Process", nameof (Cancel));
  }

  protected virtual void _(
    Events.RowSelected<ValidateDocumentAddressFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ValidateDocumentAddressGraph<TGraph>.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new ValidateDocumentAddressGraph<TGraph>.\u003C\u003Ec__DisplayClass8_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass80.filter = e.Row;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass80.filter == null)
      return;
    PXUIFieldAttribute.SetWarning(((PXSelectBase) this.Filter).Cache, (object) null, "Country", PXSelectBase<PX.Objects.CS.Country, PXSelectReadonly<PX.Objects.CS.Country, Where<PX.Objects.CS.Country.addressValidatorPluginID, IsNotNull>>.Config>.Select((PXGraph) this, Array.Empty<object>()).Count == 0 ? "No country is configured for address validation." : (string) null);
    // ISSUE: method pointer
    ((PXProcessingBase<UnvalidatedAddress>) this.DocumentAddresses).SetProcessDelegate(new PXProcessingBase<UnvalidatedAddress>.ProcessListDelegate((object) cDisplayClass80, __methodptr(\u003C_\u003Eb__0)));
  }

  protected virtual void _(Events.RowUpdated<ValidateDocumentAddressFilter> e)
  {
    ValidateDocumentAddressFilter row = e.Row;
    ValidateDocumentAddressFilter oldRow = e.OldRow;
    if ((row.Country != oldRow.Country ? 1 : (row.DocumentType != oldRow.DocumentType ? 1 : 0)) == 0)
      return;
    ((PXSelectBase) this.DocumentAddresses).Cache.Clear();
  }

  public static void ProcessAddress(
    ValidateDocumentAddressFilter filter,
    List<UnvalidatedAddress> addresses)
  {
    bool valueOrDefault = ((bool?) filter?.IsOverride).GetValueOrDefault();
    PXGraph docGraph = ValidateDocumentAddressGraph<TGraph>.GetDocGraph(addresses[0].Document);
    foreach (UnvalidatedAddress address in addresses)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ValidateDocumentAddressGraph<TGraph>.\u003C\u003Ec__DisplayClass10_0 cDisplayClass100 = new ValidateDocumentAddressGraph<TGraph>.\u003C\u003Ec__DisplayClass10_0();
      PXGraph instance = (PXGraph) PXGraph.CreateInstance<ValidateDocumentAddressGraph<TGraph>>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.warnings = new List<string>();
      foreach (System.Type type in ValidateDocumentAddressGraph<TGraph>.AddressFieldsToValidate)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ValidateDocumentAddressGraph<TGraph>.\u003C\u003Ec__DisplayClass10_1 cDisplayClass101 = new ValidateDocumentAddressGraph<TGraph>.\u003C\u003Ec__DisplayClass10_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass101.CS\u0024\u003C\u003E8__locals1 = cDisplayClass100;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass101.field = type;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        instance.ExceptionHandling.AddHandler(typeof (UnvalidatedAddress), cDisplayClass101.field.Name, new PXExceptionHandling((object) cDisplayClass101, __methodptr(\u003CProcessAddress\u003Eb__0)));
      }
      try
      {
        PXProcessing<UnvalidatedAddress>.SetCurrentItem((object) address);
        if (ValidateDocumentAddressGraph<TGraph>.ValidateDocumentAddress(instance, address, valueOrDefault, docGraph))
        {
          PXProcessing<UnvalidatedAddress>.SetProcessed();
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          PXProcessing<UnvalidatedAddress>.SetWarning(PXAddressValidator.FormatWarningMessage(cDisplayClass100.warnings));
        }
      }
      catch (PXException ex)
      {
        PXProcessing<UnvalidatedAddress>.SetError((Exception) ex);
      }
    }
  }

  protected virtual IEnumerable GetCachedAddressRecords(ValidateDocumentAddressFilter filter)
  {
    foreach (UnvalidatedAddress cachedAddressRecord in ((PXSelectBase) this.DocumentAddresses).Cache.Inserted)
      yield return (object) cachedAddressRecord;
  }

  protected virtual IEnumerable GetAddressRecords(ValidateDocumentAddressFilter filter)
  {
    yield return (object) null;
  }

  protected static bool ValidateDocumentAddress(
    PXGraph processingScreenGraph,
    UnvalidatedAddress originalAddress,
    bool isOverride,
    PXGraph documentGraph)
  {
    if (processingScreenGraph == null || originalAddress == null)
      return false;
    if (originalAddress.Address.IsValidated.GetValueOrDefault())
      return true;
    PXCache cach1 = processingScreenGraph.Caches[originalAddress.Address.GetType()];
    IAddress address = !isOverride ? originalAddress.Address : (IAddress) cach1.Insert((object) originalAddress.Address);
    IList<(string fieldName, string fieldValue, string warningMessage)> warnings;
    if (PXAddressValidator.Validate<IAddress>(processingScreenGraph, address, true, isOverride, isOverride, out warnings))
    {
      address.IsValidated = new bool?(true);
      if (isOverride && warnings.Count > 0)
      {
        ValidateDocumentAddressGraph<TGraph>.SaveDocumentAddress(documentGraph, originalAddress.Document, address);
      }
      else
      {
        PXGraph instance = (PXGraph) PXGraph.CreateInstance<TGraph>();
        PXCache cach2 = instance.Caches[originalAddress.Address.GetType()];
        cach2.Update((object) address);
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          cach2.Persist((object) address, (PXDBOperation) 1);
          transactionScope.Complete(instance);
        }
      }
      return true;
    }
    PXCache cach3 = processingScreenGraph.Caches[typeof (UnvalidatedAddress)];
    foreach ((string fieldName, string fieldValue, string warningMessage) tuple in (IEnumerable<(string fieldName, string fieldValue, string warningMessage)>) warnings)
    {
      string warningMessage = tuple.warningMessage;
      cach3.RaiseExceptionHandling(tuple.fieldName, (object) originalAddress, (object) tuple.fieldValue, (Exception) new PXSetPropertyException(warningMessage, (PXErrorLevel) 2));
    }
    return false;
  }

  protected static PXGraph GetDocGraph(IBqlTable document)
  {
    System.Type primaryGraphType = new EntityHelper((PXGraph) PXGraph.CreateInstance<ValidateDocumentAddressGraph<TGraph>>()).GetPrimaryGraphType(document.GetType(), (object) document, false);
    return !(primaryGraphType != (System.Type) null) ? (PXGraph) null : PXGraph.CreateInstance(primaryGraphType);
  }

  protected static void SaveDocumentAddress(PXGraph docGraph, IBqlTable row, IAddress address)
  {
    docGraph.Views[docGraph.PrimaryView].Cache.Current = (object) row;
    docGraph.Caches[address.GetType()].Update((object) address);
    docGraph.Actions.PressSave();
    docGraph.Clear();
  }

  protected virtual UnvalidatedAddress ConvertToUnvalidatedAddress<TAddress>(
    TAddress address,
    IBqlTable document,
    string documentNbr,
    string documentType,
    string status)
    where TAddress : IAddress
  {
    return new UnvalidatedAddress()
    {
      DocumentNbr = documentNbr,
      DocumentType = documentType,
      AddressID = address.AddressID,
      Status = status,
      AddressLine1 = address.AddressLine1,
      AddressLine2 = address.AddressLine2,
      City = address.City,
      State = address.State,
      PostalCode = address.PostalCode,
      CountryID = address.CountryID,
      Address = (IAddress) address,
      Document = document
    };
  }
}
