// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PXRSACryptStringWithMaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// This as a specialized version of PXRSACryptStringAttribute<br />
/// which allows to define entry mask dynamically. Works identically to the PXDBStringWithMask <br />
/// and PXRSACryptString attributes - namely providing run-time entry mask definition for the crypted strings.<br />
/// </summary>
/// <example>
/// <code>
/// [PXRSACryptStringWithMaskAttribute(1028,
///     typeof(Search&lt;PaymentMethodDetail.entryMask,
///         Where&lt;PaymentMethodDetail.paymentMethodID, Equal&lt;Current&lt;CustomerPaymentMethodDetail.paymentMethodID&gt;&gt;,
/// 		And&lt;PaymentMethodDetail.detailID, Equal&lt;Current&lt;CustomerPaymentMethodDetail.detailID&gt;&gt;&gt;&gt;&gt;),
/// 	IsUnicode = true)]
/// </code>
/// </example>
public class PXRSACryptStringWithMaskAttribute : 
  PXRSACryptStringAttribute,
  IPXFieldSelectingSubscriber
{
  protected Type _MaskSearch;
  protected Type _SourceType;
  protected string _SourceField;
  protected BqlCommand _Select;

  /// <summary>
  /// Calls the default constructor of the PXDBString
  /// </summary>
  /// <param name="length">Length of the string in the database. Passed to PXDBString.</param>
  /// <param name="aMaskSearch">Must be a IBqlSearch type returning a valid mask expression</param>
  public PXRSACryptStringWithMaskAttribute(int length, Type aMaskSearch)
    : base(length)
  {
    this.AssignMaskSearch(aMaskSearch);
  }

  /// <summary>Calls the default constructor of the PXDBString</summary>
  /// <param name="aMaskSearch">Must be a IBqlSearch type returning a valid mask expression</param>
  public PXRSACryptStringWithMaskAttribute(Type aMaskSearch) => this.AssignMaskSearch(aMaskSearch);

  protected virtual void AssignMaskSearch(Type aMaskSearch)
  {
    if (aMaskSearch == (Type) null)
      throw new PXArgumentException("type", "The argument cannot be null.");
    this._MaskSearch = typeof (IBqlSearch).IsAssignableFrom(aMaskSearch) ? aMaskSearch : throw new PXArgumentException(nameof (aMaskSearch), "The MaskField value is not valid.");
    this._Select = BqlCommand.CreateInstance(new Type[1]
    {
      aMaskSearch
    });
    this._SourceType = BqlCommand.GetItemType(((IBqlSearch) this._Select).GetField());
    this._SourceField = ((IBqlSearch) this._Select).GetField().Name;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      string mask = this.FindMask(sender, e.Row);
      if (!string.IsNullOrEmpty(mask))
      {
        if (!string.IsNullOrEmpty((string) e.ReturnValue))
        {
          ((PXDBCryptStringAttribute) this).ViewAsString = mask.Replace("#", "0").Replace("-", "").Replace("/", "");
          ((PXDBCryptStringAttribute) this).FieldSelecting(sender, e);
          if (!(e.ReturnState is PXStringState))
            return;
          e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(((PXDBStringAttribute) this)._Length), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(((PXDBFieldAttribute) this)._IsKey), new int?(), mask, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
        }
        else
          e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(((PXDBStringAttribute) this)._Length), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(((PXDBFieldAttribute) this)._IsKey), new int?(), mask, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
      }
      else
        ((PXDBCryptStringAttribute) this).FieldSelecting(sender, e);
    }
    else
      ((PXDBCryptStringAttribute) this).FieldSelecting(sender, e);
  }

  protected virtual string FindMask(PXCache sender, object row)
  {
    if (this._Select == null)
      return (string) null;
    object obj = sender.Graph.TypedViews.GetView(this._Select, false).SelectSingleBound(new object[1]
    {
      row
    }, Array.Empty<object>());
    if (obj != null && obj is PXResult)
      obj = ((PXResult) obj)[this._SourceType];
    return (string) sender.Graph.Caches[this._SourceType].GetValue(obj, this._SourceField == null ? ((PXEventSubscriberAttribute) this)._FieldName : this._SourceField);
  }
}
