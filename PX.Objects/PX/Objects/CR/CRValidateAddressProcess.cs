// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRValidateAddressProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CR;

[Serializable]
public class CRValidateAddressProcess : PXGraph<
#nullable disable
CRValidateAddressProcess>
{
  protected static readonly System.Type[] AddressFieldsToValidate = new System.Type[6]
  {
    typeof (CRValidateAddressProcess.BAccountAddress.addressLine1),
    typeof (CRValidateAddressProcess.BAccountAddress.addressLine2),
    typeof (CRValidateAddressProcess.BAccountAddress.city),
    typeof (CRValidateAddressProcess.BAccountAddress.state),
    typeof (CRValidateAddressProcess.BAccountAddress.countryID),
    typeof (CRValidateAddressProcess.BAccountAddress.postalCode)
  };
  [PXCacheName("Filter")]
  public PXFilter<CRValidateAddressProcess.ValidateAddressFilter> Filter;
  [PXCacheName("Address")]
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<CRValidateAddressProcess.BAccountAddress, CRValidateAddressProcess.ValidateAddressFilter, Where2<Where<Current<CRValidateAddressProcess.ValidateAddressFilter.country>, IsNull, Or<Current<CRValidateAddressProcess.ValidateAddressFilter.country>, Equal<CRValidateAddressProcess.BAccountAddress.countryID>>>, And2<Where<Current<CRValidateAddressProcess.ValidateAddressFilter.bAccountType>, IsNull, Or<Current<CRValidateAddressProcess.ValidateAddressFilter.bAccountType>, Equal<CRValidateAddressProcess.BAccountAddress.type>>>, And<Where<Current<CRValidateAddressProcess.ValidateAddressFilter.bAccountStatus>, IsNull, Or<Current<CRValidateAddressProcess.ValidateAddressFilter.bAccountStatus>, Equal<CRValidateAddressProcess.BAccountAddress.status>>>>>>, OrderBy<Asc<CRValidateAddressProcess.BAccountAddress.acctCD>>> AddressList;
  public PXCancel<CRValidateAddressProcess.ValidateAddressFilter> Cancel;

  public CRValidateAddressProcess()
  {
    ((PXProcessing<CRValidateAddressProcess.BAccountAddress>) this.AddressList).SetProcessCaption(PXMessages.LocalizeNoPrefix("Validate"));
    ((PXProcessing<CRValidateAddressProcess.BAccountAddress>) this.AddressList).SetProcessAllCaption(PXMessages.LocalizeNoPrefix("Validate All"));
    ((PXGraph) this).Actions.Move("Process", nameof (Cancel));
  }

  protected virtual void ValidateAddressFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRValidateAddressProcess.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new CRValidateAddressProcess.\u003C\u003Ec__DisplayClass7_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.filter = e.Row as CRValidateAddressProcess.ValidateAddressFilter;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass70.filter == null)
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<CRValidateAddressProcess.BAccountAddress>) this.AddressList).SetProcessDelegate<CRValidateAddressProcess>(new PXProcessingBase<CRValidateAddressProcess.BAccountAddress>.ProcessItemDelegate<CRValidateAddressProcess>((object) cDisplayClass70, __methodptr(\u003CValidateAddressFilter_RowSelected\u003Eb__0)));
    PXUIFieldAttribute.SetWarning<CRValidateAddressProcess.ValidateAddressFilter.country>(((PXSelectBase) this.Filter).Cache, (object) null, PXSelectBase<PX.Objects.CS.Country, PXSelectReadonly<PX.Objects.CS.Country, Where<PX.Objects.CS.Country.addressValidatorPluginID, IsNotNull>>.Config>.Select((PXGraph) this, Array.Empty<object>()).Count == 0 ? "No country is configured for address validation." : (string) null);
  }

  public virtual void ProcessAddress(
    PXGraph graph,
    CRValidateAddressProcess.ValidateAddressFilter filter,
    CRValidateAddressProcess.BAccountAddress address)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRValidateAddressProcess.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new CRValidateAddressProcess.\u003C\u003Ec__DisplayClass8_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass80.warnings = new List<string>();
    bool valueOrDefault = ((bool?) filter?.IsOverride).GetValueOrDefault();
    foreach (System.Type type in CRValidateAddressProcess.AddressFieldsToValidate)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRValidateAddressProcess.\u003C\u003Ec__DisplayClass8_1 cDisplayClass81 = new CRValidateAddressProcess.\u003C\u003Ec__DisplayClass8_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass81.CS\u0024\u003C\u003E8__locals1 = cDisplayClass80;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass81.field = type;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      graph.ExceptionHandling.AddHandler(typeof (CRValidateAddressProcess.BAccountAddress), cDisplayClass81.field.Name, new PXExceptionHandling((object) cDisplayClass81, __methodptr(\u003CProcessAddress\u003Eb__0)));
    }
    try
    {
      PXProcessing<CRValidateAddressProcess.BAccountAddress>.SetCurrentItem((object) address);
      if (this.ValidateAddress(graph, address, valueOrDefault))
      {
        PXProcessing<CRValidateAddressProcess.BAccountAddress>.SetProcessed();
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        PXProcessing<CRValidateAddressProcess.BAccountAddress>.SetWarning(PXAddressValidator.FormatWarningMessage(cDisplayClass80.warnings));
      }
    }
    catch (PXException ex)
    {
      PXProcessing<CRValidateAddressProcess.BAccountAddress>.SetError((Exception) ex);
    }
    finally
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.warnings.Clear();
      graph.Clear();
    }
  }

  protected virtual bool ValidateAddress(
    PXGraph graph,
    CRValidateAddressProcess.BAccountAddress address,
    bool isOverride)
  {
    if (graph == null || address == null)
      return false;
    if (address.IsValidated.GetValueOrDefault())
      return true;
    PXCache cach = graph.Caches[typeof (CRValidateAddressProcess.BAccountAddress)];
    CRValidateAddressProcess.BAccountAddress aAddress = !isOverride ? address : cach.Insert(cach.CreateCopy((object) address)) as CRValidateAddressProcess.BAccountAddress;
    try
    {
      if (PXAddressValidator.Validate<CRValidateAddressProcess.BAccountAddress>(graph, aAddress, true, isOverride, isOverride))
      {
        aAddress.IsValidated = new bool?(true);
        cach.Update((object) aAddress);
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          cach.Persist((object) aAddress, (PXDBOperation) 1);
          transactionScope.Complete(graph);
        }
        return true;
      }
    }
    finally
    {
      cach.Remove((object) aAddress);
    }
    return false;
  }

  [PXHidden]
  [Serializable]
  public class ValidateAddressFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXBool]
    [PXUIField(DisplayName = "Override Automatically")]
    public virtual bool? IsOverride { get; set; }

    [PXString(2, InputMask = ">??")]
    [PXUIField(DisplayName = "Country")]
    [PXSelector(typeof (Search<PX.Objects.CS.Country.countryID>), new System.Type[] {typeof (PX.Objects.CS.Country.countryID), typeof (PX.Objects.CS.Country.description), typeof (PX.Objects.CS.Country.addressValidatorPluginID)}, DescriptionField = typeof (PX.Objects.CS.Country.description))]
    public virtual string Country { get; set; }

    [PXString(2, IsFixed = true)]
    [PXUIField(DisplayName = "Customer/Vendor Type")]
    [BAccountType.List]
    public virtual string BAccountType { get; set; }

    [PXString(1, IsFixed = true)]
    [PXUIField(DisplayName = "Customer/Vendor Status")]
    [CustomerStatus.BusinessAccountNonCustomerList]
    public virtual string BAccountStatus { get; set; }

    public abstract class isOverride : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CRValidateAddressProcess.ValidateAddressFilter.isOverride>
    {
    }

    public abstract class country : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.ValidateAddressFilter.country>
    {
    }

    public abstract class bAccountType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.ValidateAddressFilter.bAccountType>
    {
    }

    public abstract class bAccountStatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.ValidateAddressFilter.bAccountStatus>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (Select2<Address, InnerJoin<BAccount, On<BAccount.bAccountID, Equal<Address.bAccountID>>>, Where<Address.isValidated, NotEqual<True>>>), new System.Type[] {typeof (Address)}, Persistent = true)]
  public class BAccountAddress : 
    PXBqlTable,
    IBqlTable,
    IBqlTableSystemDataStorage,
    IAddressBase,
    IValidatedAddress
  {
    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected { get; set; }

    [PXDBIdentity(IsKey = true, BqlField = typeof (Address.addressID))]
    [PXUIField(DisplayName = "Address ID")]
    public virtual int? AddressID { get; set; }

    [PXDimensionSelector("BIZACCT", typeof (Search<BAccount.acctCD, Where<Match<Current<AccessInfo.userName>>>>))]
    [PXDBString(30, IsUnicode = true, BqlField = typeof (BAccount.acctCD))]
    [PXUIField(DisplayName = "Customer/Vendor")]
    public virtual string AcctCD { get; set; }

    [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (BAccount.acctName))]
    [PXUIField(DisplayName = "Name")]
    public virtual string AcctName { get; set; }

    [PXDBString(1, IsFixed = true, BqlField = typeof (BAccount.status))]
    [PXUIField(DisplayName = "Status")]
    [CustomerStatus.BusinessAccountNonCustomerList]
    public virtual string Status { get; set; }

    [PXDBString(2, IsFixed = true, BqlField = typeof (BAccount.type))]
    [PXUIField(DisplayName = "Type")]
    [BAccountType.List]
    public virtual string Type { get; set; }

    [PXDBString(50, IsUnicode = true, BqlField = typeof (Address.addressLine1))]
    [PXUIField(DisplayName = "Address Line 1")]
    public virtual string AddressLine1 { get; set; }

    [PXDBString(50, IsUnicode = true, BqlField = typeof (Address.addressLine2))]
    [PXUIField(DisplayName = "Address Line 2")]
    public virtual string AddressLine2 { get; set; }

    [PXDBString(50, IsUnicode = true, BqlField = typeof (Address.addressLine3))]
    [PXUIField(DisplayName = "Address Line 3")]
    public virtual string AddressLine3 { get; set; }

    [PXDBString(50, IsUnicode = true, BqlField = typeof (Address.city))]
    [PXUIField(DisplayName = "City")]
    public virtual string City { get; set; }

    [PXDBString(2, IsFixed = true, BqlField = typeof (Address.countryID))]
    [PXUIField(DisplayName = "Country")]
    public virtual string CountryID { get; set; }

    [PXDBString(50, IsUnicode = true, BqlField = typeof (Address.state))]
    [PXUIField(DisplayName = "State")]
    public virtual string State { get; set; }

    [PXDBString(20, BqlField = typeof (Address.postalCode))]
    [PXUIField(DisplayName = "Postal Code")]
    public virtual string PostalCode { get; set; }

    [PXDBBool(BqlField = typeof (Address.isValidated))]
    [PXUIField(DisplayName = "Validated")]
    public virtual bool? IsValidated { get; set; }

    [PXDBInt(BqlField = typeof (Address.revisionID))]
    [AddressRevisionID]
    public virtual int? RevisionID { get; set; }

    [PXNote(BqlField = typeof (Address.noteID))]
    public virtual Guid? NoteID { get; set; }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.selected>
    {
    }

    public abstract class addressID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.addressID>
    {
    }

    public abstract class acctCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.acctCD>
    {
    }

    public abstract class acctName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.acctName>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.status>
    {
    }

    public abstract class type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.type>
    {
    }

    public abstract class addressLine1 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.addressLine1>
    {
    }

    public abstract class addressLine2 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.addressLine2>
    {
    }

    public abstract class addressLine3 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.addressLine3>
    {
    }

    public abstract class city : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.city>
    {
    }

    public abstract class countryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.countryID>
    {
    }

    public abstract class state : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.state>
    {
    }

    public abstract class postalCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.postalCode>
    {
    }

    public abstract class isValidated : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.isValidated>
    {
    }

    public abstract class revisionID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.revisionID>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      CRValidateAddressProcess.BAccountAddress.noteID>
    {
    }
  }
}
