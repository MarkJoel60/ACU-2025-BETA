// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXAddressValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.AddressValidator;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.CS;

public static class PXAddressValidator
{
  public static bool Validate<T>(PXGraph aGraph, T aAddress, bool aSynchronous) where T : IAddressBase, IValidatedAddress
  {
    return PXAddressValidator.Validate<T>(aGraph, aAddress, aSynchronous, false);
  }

  public static void Validate<T>(
    PXGraph aGraph,
    List<T> aAddresses,
    bool aSynchronous,
    bool updateToValidAddress)
    where T : IAddressBase, IValidatedAddress
  {
    foreach (T aAddress in aAddresses)
      PXAddressValidator.Validate<T>(aGraph, aAddress, aSynchronous, updateToValidAddress);
  }

  public static bool Validate<T>(
    PXGraph aGraph,
    T aAddress,
    bool aSynchronous,
    bool updateToValidAddress)
    where T : IAddressBase, IValidatedAddress
  {
    return PXAddressValidator.Validate<T>(aGraph, aAddress, aSynchronous, updateToValidAddress, false);
  }

  public static bool Validate<T>(
    PXGraph aGraph,
    T aAddress,
    bool aSynchronous,
    bool updateToValidAddress,
    bool forceOverride)
    where T : IAddressBase, IValidatedAddress
  {
    return PXAddressValidator.Validate<T>(aGraph, aAddress, aSynchronous, updateToValidAddress, forceOverride, out IList<(string, string, string)> _);
  }

  public static bool Validate<T>(
    PXGraph aGraph,
    T aAddress,
    bool aSynchronous,
    bool updateToValidAddress,
    bool forceOverride,
    out IList<(string fieldName, string fieldValue, string warningMessage)> warnings)
    where T : IAddressBase, IValidatedAddress
  {
    warnings = (IList<(string, string, string)>) new List<(string, string, string)>();
    if (!AddressValidatorPluginMaint.IsActive(aGraph, aAddress.CountryID))
    {
      if (aSynchronous)
      {
        PXCache cach = aGraph.Caches[aAddress.GetType()];
        string str1 = "CountryID";
        string str2 = PXMessages.LocalizeFormatNoPrefix("The Address Verification Service (AVS) is not configured for the country '{0}'", new object[1]
        {
          (object) aAddress.CountryID
        });
        cach.RaiseExceptionHandling(str1, (object) aAddress, (object) aAddress.CountryID, (Exception) new PXSetPropertyException(str2, (PXErrorLevel) 2));
        warnings.Add((str1, aAddress.CountryID, str2));
        return false;
      }
      throw new PXException("The Address Verification Service (AVS) is not configured for the country '{0}'", new object[1]
      {
        (object) aAddress.CountryID
      });
    }
    bool flag = true;
    Country country = Country.PK.Find(aGraph, aAddress.CountryID);
    updateToValidAddress = updateToValidAddress && country.AutoOverrideAddress.GetValueOrDefault() | forceOverride;
    IAddressValidator addressValidator = AddressValidatorPluginMaint.CreateAddressValidator(aGraph, country.AddressValidatorPluginID);
    if (addressValidator != null)
    {
      PXCache cache = aGraph.Caches[aAddress.GetType()];
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      T validAddress = (T) cache.CreateCopy((object) aAddress);
      try
      {
        flag = addressValidator.ValidateAddress((IAddressBase) validAddress, dictionary);
      }
      catch
      {
        throw new PXException("An unknown error has happen during address validation");
      }
      if (flag)
      {
        \u003C\u003Ef__AnonymousType39<string, string, string>[] array = ((IEnumerable<string>) new string[5]
        {
          "AddressLine1",
          "AddressLine2",
          "City",
          "State",
          "PostalCode"
        }).Select(field => new
        {
          Field = field,
          OriginalValue = (string) cache.GetValue((object) (T) aAddress, field) ?? string.Empty,
          ValidValue = (string) cache.GetValue((object) validAddress, field) ?? string.Empty
        }).Where(x => string.Compare(x.OriginalValue.Trim(), x.ValidValue.Trim(), StringComparison.OrdinalIgnoreCase) != 0).ToArray();
        if (aSynchronous && !updateToValidAddress && array.Any())
        {
          HashSet<string> stringSet = new HashSet<string>()
          {
            "PostalCode"
          };
          foreach (var data in array)
          {
            string str = PXMessages.LocalizeFormatNoPrefix("AVS Returns: '{0}'", new object[1]
            {
              (object) data.ValidValue
            });
            cache.RaiseExceptionHandling(data.Field, (object) aAddress, (object) data.OriginalValue, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 2));
            warnings.Add((data.Field, data.OriginalValue, str));
            if (stringSet.Contains(data.Field))
              flag = false;
          }
        }
        if (flag)
        {
          T copy = (T) cache.CreateCopy((object) aAddress);
          Action<T> action = (Action<T>) null;
          if (aSynchronous & updateToValidAddress)
          {
            foreach (var data in array)
            {
              var m = data;
              string validValue = m.ValidValue == string.Empty ? (string) null : m.ValidValue;
              cache.SetValue((object) copy, m.Field, (object) validValue);
              string warning = PXMessages.LocalizeFormatNoPrefix("The previous '{0}' value has been replaced by Address Verification Service (AVS)", new object[1]
              {
                (object) m.OriginalValue
              });
              action += (Action<T>) (a => cache.RaiseExceptionHandling(m.Field, (object) a, (object) validValue, (Exception) new PXSetPropertyException(warning, (PXErrorLevel) 2)));
              warnings.Add((m.Field, validValue, warning));
            }
          }
          ref T local = ref copy;
          if ((object) default (T) == null)
          {
            T obj = local;
            local = ref obj;
          }
          bool? nullable = new bool?(true);
          local.IsValidated = nullable;
          aAddress = (T) cache.Update((object) copy);
          if (action != null)
            action(aAddress);
        }
      }
      else
      {
        string empty = string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        int num = 0;
        foreach (KeyValuePair<string, string> keyValuePair in dictionary)
        {
          string str = keyValuePair.Value;
          if (!aSynchronous)
          {
            if (num > 0)
              stringBuilder.Append(",");
            stringBuilder.AppendFormat("{0}:{1}", (object) keyValuePair.Key, (object) str);
            ++num;
          }
          else
          {
            object obj = cache.GetValue((object) aAddress, keyValuePair.Key);
            cache.RaiseExceptionHandling(keyValuePair.Key, (object) aAddress, obj, (Exception) new PXSetPropertyException(str));
            warnings.Add((keyValuePair.Key, (string) obj, str));
          }
        }
        if (!aSynchronous)
          throw new PXException(stringBuilder.ToString());
      }
    }
    return flag;
  }

  public static bool IsValidateRequired<T>(PXGraph aGraph, T aAddress) where T : IAddressBase
  {
    return !string.IsNullOrEmpty(aAddress?.CountryID) && Country.PK.Find(aGraph, aAddress.CountryID)?.AddressValidatorPluginID != null;
  }

  public static string FormatWarningMessage(List<string> warnings)
  {
    if (warnings == null)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < warnings.Count; ++index)
    {
      stringBuilder.AppendLine(warnings[index]);
      if (index < warnings.Count - 1)
        stringBuilder.AppendLine();
    }
    if (warnings.Count == 0)
      stringBuilder.Append(PXMessages.LocalizeNoPrefix("The address is invalid."));
    return stringBuilder.ToString();
  }

  public static void OnFieldException(
    PXCache sender,
    PXExceptionHandlingEventArgs e,
    Type field,
    ref List<string> warnings)
  {
    if (sender == null || e == null || field == (Type) null || warnings == null)
      return;
    PXSetPropertyException exception = e.Exception as PXSetPropertyException;
    if (e.Row == null || exception == null || string.IsNullOrWhiteSpace(((PXException) exception).MessageNoPrefix))
      return;
    warnings.Add(PXMessages.LocalizeFormatNoPrefix("Invalid {0} value: '{1}' {2}{3}", new object[4]
    {
      (object) PXUIFieldAttribute.GetDisplayName(sender, field.Name).ToLower(),
      sender.GetValue(e.Row, field.Name),
      (object) Environment.NewLine,
      (object) ((PXException) exception).MessageNoPrefix
    }));
  }
}
