// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.AutoNumberAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

#nullable disable
namespace PX.Objects.CS;

public class AutoNumberAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldDefaultingSubscriber,
  IPXFieldVerifyingSubscriber,
  IPXRowInsertingSubscriber,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber,
  IPXFieldSelectingSubscriber
{
  private Type _doctypeField;
  private string[] _doctypeValues;
  private Type[] _setupFields;
  private string[] _setupValues;
  private string _dateField;
  private Type _dateType;
  private string _numberingID;
  private DateTime? _dateTime;
  private string _emptyDateMessage;
  protected PXEventSubscriberAttribute.ObjectRef<bool?> _UserNumbering;
  protected PXEventSubscriberAttribute.ObjectRef<string> _NewSymbol;
  protected AutoNumberAttribute.NullNumberingMode NullMode;
  protected string NullString;
  protected string LastNbr;
  protected string WarnNbr;
  protected string NewNumber;
  protected string NotSetNumber;
  protected int? NumberingSEQ;
  protected object _KeyToAbort;

  public static string GetNewNumberSymbol<Field>(PXCache cache, object data) where Field : IBqlField
  {
    using (IEnumerator<AutoNumberAttribute> enumerator = cache.GetAttributesReadonly<Field>(data).OfType<AutoNumberAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        AutoNumberAttribute current = enumerator.Current;
        current.getfields(cache, data);
        return current.GetNewNumberSymbol();
      }
    }
    return string.Empty;
  }

  public static bool IsViewOnlyRecord<Field>(PXCache cache, object data) where Field : IBqlField
  {
    using (IEnumerator<AutoNumberAttribute> enumerator = cache.GetAttributesReadonly<Field>(data).OfType<AutoNumberAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        AutoNumberAttribute current = enumerator.Current;
        current.getfields(cache, data);
        string a = (string) cache.GetValue(data, current._FieldName);
        string newNumberSymbol = current.GetNewNumberSymbol();
        return current._numberingID == null && string.Equals(a, newNumberSymbol);
      }
    }
    return false;
  }

  public static void SetNumberingId<Field>(PXCache cache, string key, string value) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly<Field>())
    {
      int index;
      if (subscriberAttribute is AutoNumberAttribute && subscriberAttribute.AttributeLevel == 1 && ((AutoNumberAttribute) subscriberAttribute)._doctypeValues.Length != 0 && (index = Array.IndexOf<string>(((AutoNumberAttribute) subscriberAttribute)._doctypeValues, key)) >= 0)
        ((AutoNumberAttribute) subscriberAttribute)._setupValues[index] = value;
    }
  }

  public static void SetNumberingId<Field>(PXCache cache, string value) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly<Field>())
    {
      if (subscriberAttribute is AutoNumberAttribute && subscriberAttribute.AttributeLevel == 1 && ((AutoNumberAttribute) subscriberAttribute)._doctypeValues.Length == 0)
        ((AutoNumberAttribute) subscriberAttribute)._setupValues[0] = value;
    }
  }

  [PXInternalUseOnly]
  public static void SetUserNumbering<Field>(PXCache cache) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly<Field>())
    {
      if (subscriberAttribute is AutoNumberAttribute && subscriberAttribute.AttributeLevel == 1 && ((AutoNumberAttribute) subscriberAttribute)._doctypeValues.Length == 0)
        ((AutoNumberAttribute) subscriberAttribute).UserNumbering = new bool?(true);
    }
  }

  public static NumberingSequence GetNumberingSequence(
    string numberingID,
    int? branchID,
    DateTime? date)
  {
    if (numberingID == null || !date.HasValue)
      return (NumberingSequence) null;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<NumberingSequence>(new PXDataField[20]
    {
      (PXDataField) new PXDataField<NumberingSequence.endNbr>(),
      (PXDataField) new PXDataField<NumberingSequence.lastNbr>(),
      (PXDataField) new PXDataField<NumberingSequence.startNbr>(),
      (PXDataField) new PXDataField<NumberingSequence.warnNbr>(),
      (PXDataField) new PXDataField<NumberingSequence.nbrStep>(),
      (PXDataField) new PXDataField<NumberingSequence.numberingSEQ>(),
      (PXDataField) new PXDataField<NumberingSequence.nBranchID>(),
      (PXDataField) new PXDataField<NumberingSequence.startNbr>(),
      (PXDataField) new PXDataField<NumberingSequence.startDate>(),
      (PXDataField) new PXDataField<NumberingSequence.createdByID>(),
      (PXDataField) new PXDataField<NumberingSequence.createdByScreenID>(),
      (PXDataField) new PXDataField<NumberingSequence.createdDateTime>(),
      (PXDataField) new PXDataField<NumberingSequence.lastModifiedByID>(),
      (PXDataField) new PXDataField<NumberingSequence.lastModifiedByScreenID>(),
      (PXDataField) new PXDataField<NumberingSequence.lastModifiedDateTime>(),
      (PXDataField) new PXDataFieldValue<NumberingSequence.numberingID>((PXDbType) 22, new int?((int) byte.MaxValue), (object) numberingID),
      (PXDataField) new PXDataFieldValue<NumberingSequence.nBranchID>((PXDbType) 8, new int?(4), (object) branchID, (PXComp) 9),
      (PXDataField) new PXDataFieldValue<NumberingSequence.startDate>((PXDbType) 4, new int?(4), (object) date, (PXComp) 5),
      (PXDataField) new PXDataFieldOrder<NumberingSequence.nBranchID>(true),
      (PXDataField) new PXDataFieldOrder<NumberingSequence.startDate>(true)
    }))
    {
      if (pxDataRecord == null)
        return (NumberingSequence) null;
      return new NumberingSequence()
      {
        EndNbr = pxDataRecord.GetString(0),
        LastNbr = pxDataRecord.GetString(1) ?? pxDataRecord.GetString(2),
        WarnNbr = pxDataRecord.GetString(3),
        NbrStep = new int?(pxDataRecord.GetInt32(4).Value),
        NumberingSEQ = new int?(pxDataRecord.GetInt32(5).Value),
        NBranchID = pxDataRecord.GetInt32(6),
        StartNbr = pxDataRecord.GetString(7),
        StartDate = pxDataRecord.GetDateTime(8),
        CreatedByID = pxDataRecord.GetGuid(9),
        CreatedByScreenID = pxDataRecord.GetString(10),
        CreatedDateTime = pxDataRecord.GetDateTime(11),
        LastModifiedByID = pxDataRecord.GetGuid(12),
        LastModifiedByScreenID = pxDataRecord.GetString(13),
        LastModifiedDateTime = pxDataRecord.GetDateTime(14)
      };
    }
  }

  private void getfields(PXCache sender, object row)
  {
    Type type = (Type) null;
    string str1 = (string) null;
    BqlCommand bqlCommand = (BqlCommand) null;
    this._numberingID = (string) null;
    if (this._doctypeField != (Type) null)
    {
      string str2 = (string) sender.GetValue(row, this._doctypeField.Name);
      int index;
      if ((index = Array.IndexOf<string>(this._doctypeValues, str2)) >= 0 && this._setupValues[index] != null)
        this._numberingID = this._setupValues[index];
      else if (index >= 0 && this._setupFields[index] != (Type) null)
      {
        if (typeof (IBqlSearch).IsAssignableFrom(this._setupFields[index]))
        {
          bqlCommand = BqlCommand.CreateInstance(new Type[1]
          {
            this._setupFields[index]
          });
          type = BqlCommand.GetItemType(((IBqlSearch) bqlCommand).GetField());
          str1 = ((IBqlSearch) bqlCommand).GetField().Name;
        }
        else if (this._setupFields[index].IsNested && typeof (IBqlField).IsAssignableFrom(this._setupFields[index]))
        {
          str1 = this._setupFields[index].Name;
          type = BqlCommand.GetItemType(this._setupFields[index]);
        }
      }
    }
    else if ((this._numberingID = this._setupValues[0]) == null)
    {
      if (typeof (IBqlSearch).IsAssignableFrom(this._setupFields[0]))
      {
        bqlCommand = BqlCommand.CreateInstance(new Type[1]
        {
          this._setupFields[0]
        });
        type = BqlCommand.GetItemType(((IBqlSearch) bqlCommand).GetField());
        str1 = ((IBqlSearch) bqlCommand).GetField().Name;
      }
      else if (this._setupFields[0].IsNested && typeof (IBqlField).IsAssignableFrom(this._setupFields[0]))
      {
        str1 = this._setupFields[0].Name;
        type = BqlCommand.GetItemType(this._setupFields[0]);
      }
    }
    if (bqlCommand != null)
    {
      if (sender.Graph.Caches[type].Keys.Count == 0)
      {
        IList list = (IList) BqlCommand.Compose(new Type[2]
        {
          typeof (PXSetup<>),
          type
        }).GetMethod("Select", BindingFlags.Static | BindingFlags.Public).Invoke((object) null, new object[2]
        {
          (object) sender.Graph,
          (object) new object[0]
        });
        if (list != null && list.Count > 0)
          this._numberingID = (string) sender.Graph.Caches[type].GetValue((object) PXResult.Unwrap(list[0], type), str1);
      }
      else
      {
        PXView view = sender.Graph.TypedViews.GetView(bqlCommand, false);
        int num1 = -1;
        int num2 = 0;
        object[] objArray = new object[1]{ row };
        ref int local1 = ref num1;
        ref int local2 = ref num2;
        List<object> objectList = view.Select(objArray, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, 1, ref local2);
        if (objectList != null && objectList.Count > 0)
        {
          object obj = objectList[objectList.Count - 1];
          if (obj != null && obj is PXResult)
            obj = ((PXResult) obj)[type];
          this._numberingID = (string) sender.Graph.Caches[type].GetValue(obj, str1);
        }
      }
    }
    else if (type != (Type) null)
    {
      PXCache cach = sender.Graph.Caches[type];
      if (cach.Current != null && this._numberingID == null)
        this._numberingID = (string) cach.GetValue(cach.Current, str1);
    }
    PXCache cach1 = sender.Graph.Caches[this._dateType];
    if (sender.GetItemType() == this._dateType)
    {
      this._dateTime = (DateTime?) cach1.GetValue(row, this._dateField);
    }
    else
    {
      if (cach1.Current == null)
        return;
      this._dateTime = (DateTime?) cach1.GetValue(cach1.Current, this._dateField);
    }
  }

  public AutoNumberAttribute(
    Type doctypeField,
    Type dateField,
    string[] doctypeValues,
    Type[] setupFields)
  {
    this._dateField = dateField.Name;
    this._dateType = BqlCommand.GetItemType(dateField);
    this._doctypeField = doctypeField;
    this._doctypeValues = doctypeValues;
    this._setupFields = setupFields;
  }

  protected AutoNumberAttribute(
    Type doctypeField,
    Type dateField,
    params Tuple<string, Type>[] docTypesToSetups)
    : this(doctypeField, dateField, docTypesToSetups != null ? ((IEnumerable<Tuple<string, Type>>) docTypesToSetups).Select<Tuple<string, Type>, string>((Func<Tuple<string, Type>, string>) (t => t.Item1)).ToArray<string>() : (string[]) null, docTypesToSetups != null ? ((IEnumerable<Tuple<string, Type>>) docTypesToSetups).Select<Tuple<string, Type>, Type>((Func<Tuple<string, Type>, Type>) (t => t.Item2)).ToArray<Type>() : (Type[]) null)
  {
  }

  protected static AutoNumberAttribute.Pairer Pair(string docTypeValue)
  {
    return new AutoNumberAttribute.Pairer(docTypeValue);
  }

  public AutoNumberAttribute(Type setupField, Type dateField)
    : this((Type) null, dateField, new string[0], new Type[1]
    {
      setupField
    })
  {
  }

  public AutoNumberAttribute(Type setupField, Type dateField, string emptyDateMessage)
    : this((Type) null, dateField, new string[0], new Type[1]
    {
      setupField
    })
  {
    this._emptyDateMessage = emptyDateMessage;
  }

  protected bool? UserNumbering
  {
    get => this._UserNumbering.Value;
    set => this._UserNumbering.Value = value;
  }

  protected string NewSymbol
  {
    get => this._NewSymbol.Value;
    set => this._NewSymbol.Value = value;
  }

  public string this[string key]
  {
    get
    {
      string str1 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
      if (!PXDBLocalizableStringAttribute.IsEnabled)
        str1 = "";
      AutoNumberAttribute.Numberings slot = PXDatabase.GetSlot<AutoNumberAttribute.Numberings>(typeof (AutoNumberAttribute.Numberings).Name + str1, new Type[1]
      {
        typeof (Numbering)
      });
      if (slot == null)
        return (string) null;
      string str2;
      if (!slot.TryGetValue(key, out str2))
        throw new PXException("Numbering ID is null.");
      return str2;
    }
  }

  protected virtual string GetNewNumberSymbol() => this.GetNewNumberSymbol((string) null);

  protected virtual string GetNewNumberSymbol(string numberingID)
  {
    numberingID = numberingID ?? this._numberingID;
    if (numberingID != null)
    {
      bool? nullable = new bool?((this.NewSymbol = this[numberingID]) == null);
      this.UserNumbering = nullable;
      return nullable.GetValueOrDefault() ? (string) null : " " + this.NewSymbol;
    }
    return this.NullMode == AutoNumberAttribute.NullNumberingMode.UserNumbering ? this.NullString : " <SELECT>";
  }

  void IPXFieldDefaultingSubscriber.FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.Row != null)
      this.getfields(sender, e.Row);
    e.NewValue = (object) this.GetNewNumberSymbol();
  }

  void IPXFieldVerifyingSubscriber.FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (sender.Locate(e.Row) != null || sender.Graph is PXGenericInqGrph)
      return;
    string str = (string) sender.GetValue(e.Row, this._FieldOrdinal);
    if (this.UserNumbering.GetValueOrDefault() || str == null)
      return;
    e.NewValue = (object) str;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._UserNumbering = new PXEventSubscriberAttribute.ObjectRef<bool?>();
    this._NewSymbol = new PXEventSubscriberAttribute.ObjectRef<string>();
    this._setupValues = new string[this._setupFields.Length];
    sender.SetAutoNumber(this._FieldName);
    bool flag;
    if (!(flag = sender.Keys.IndexOf(this._FieldName) > 0))
    {
      foreach (PXEventSubscriberAttribute subscriberAttribute in sender.GetAttributesReadonly(this._FieldName))
      {
        if (subscriberAttribute is PXDBFieldAttribute)
        {
          if (flag = ((PXDBFieldAttribute) subscriberAttribute).IsKey)
            break;
        }
      }
    }
    if (!flag)
    {
      this.NullString = string.Empty;
      this.NullMode = AutoNumberAttribute.NullNumberingMode.UserNumbering;
    }
    else
    {
      PXGraph.RowSelectedEvents rowSelected = sender.Graph.RowSelected;
      Type itemType1 = sender.GetItemType();
      AutoNumberAttribute autoNumberAttribute1 = this;
      // ISSUE: virtual method pointer
      PXRowSelected pxRowSelected = new PXRowSelected((object) autoNumberAttribute1, __vmethodptr(autoNumberAttribute1, Parameter_RowSelected));
      rowSelected.AddHandler(itemType1, pxRowSelected);
      PXGraph.CommandPreparingEvents commandPreparing1 = sender.Graph.CommandPreparing;
      Type itemType2 = sender.GetItemType();
      string fieldName = this._FieldName;
      AutoNumberAttribute autoNumberAttribute2 = this;
      // ISSUE: virtual method pointer
      PXCommandPreparing commandPreparing2 = new PXCommandPreparing((object) autoNumberAttribute2, __vmethodptr(autoNumberAttribute2, Parameter_CommandPreparing));
      commandPreparing1.AddHandler(itemType2, fieldName, commandPreparing2);
    }
  }

  protected virtual void Parameter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || this.UserNumbering.HasValue && this.NewSymbol != null)
      return;
    this.getfields(sender, e.Row);
    this.GetNewNumberSymbol();
  }

  protected virtual void Parameter_CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 4096 /*0x1000*/) != 4096 /*0x1000*/ && ((e.Operation & 3) != null || (e.Operation & 124) == 16 /*0x10*/ || (e.Operation & 124) == 64 /*0x40*/ || e.Row != null) || !(e.Value is string str))
      return;
    bool? userNumbering = this.UserNumbering;
    bool flag = false;
    if (!(userNumbering.GetValueOrDefault() == flag & userNumbering.HasValue) || str.Length <= 1 || !string.Equals(str.Substring(1), this.NewSymbol))
      return;
    e.DataValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected static bool IS_SEPARATE_SCOPE => WebConfig.EnableAutoNumberingInSeparateConnection;

  public static string GetKeyToAbort(PXCache cache, object row, string fieldName)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(row, fieldName))
    {
      if (subscriberAttribute is AutoNumberAttribute && subscriberAttribute.AttributeLevel == 2)
        return (string) ((AutoNumberAttribute) subscriberAttribute)._KeyToAbort;
    }
    return (string) null;
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) != 2)
      return;
    this.getfields(sender, e.Row);
    if ((this.NotSetNumber = this.GetNewNumberSymbol()) == null && this.NullString == string.Empty)
      return;
    if ((this.NotSetNumber = this.GetNewNumberSymbol()) == this.NullString)
    {
      object obj = sender.GetValue(e.Row, this._FieldName);
      AutoNumberAttribute.Numberings slot = PXDatabase.GetSlot<AutoNumberAttribute.Numberings>(typeof (AutoNumberAttribute.Numberings).Name, new Type[1]
      {
        typeof (Numbering)
      });
      if (slot == null || obj == null)
        return;
      foreach (KeyValuePair<string, string> numbering in slot.GetNumberings())
      {
        if (numbering.Value == (string) obj || this.GetNewNumberSymbol(numbering.Key) == (string) obj)
          throw new PXException("Document cannot be saved. Document number ({0}) equals to the New Number Symbol of a Numbering Sequence. Please change document number.", new object[1]
          {
            (object) (string) obj
          });
      }
    }
    else
    {
      if (!this._dateTime.HasValue)
      {
        Exception exception;
        if (!string.IsNullOrEmpty(this._emptyDateMessage))
          exception = (Exception) new AutoNumberException(this._emptyDateMessage, Array.Empty<object>());
        else
          exception = (Exception) new AutoNumberException("'{0}' must have a value.", new object[1]
          {
            (object) (((PXFieldState) sender.GetStateExt(e.Row, this._dateField))?.DisplayName ?? this._dateField)
          });
        sender.RaiseExceptionHandling(this._dateField, e.Row, (object) null, exception);
        throw exception;
      }
      if (this._numberingID != null && this._dateTime.HasValue)
      {
        this.NewNumber = AutoNumberAttribute.GetNextNumber(sender, e.Row, this._numberingID, this._dateTime, this.NewNumber, out this.LastNbr, out this.WarnNbr, out this.NumberingSEQ);
        if (this.NewNumber.CompareTo(this.WarnNbr) >= 0)
          PXUIFieldAttribute.SetWarning(sender, e.Row, this._FieldName, "The numbering sequence is expiring.");
        this._KeyToAbort = sender.GetValue(e.Row, this._FieldName);
        sender.SetValue(e.Row, this._FieldName, (object) this.NewNumber);
      }
      else if (string.IsNullOrEmpty(this.NewNumber = (string) sender.GetValue(e.Row, this._FieldName)) || string.Equals(this.NewNumber, this.NotSetNumber))
        throw new AutoNumberException("Cannot generate the next number for the {0} sequence.", new object[1]
        {
          (object) this._numberingID
        });
    }
  }

  public static string GetNextNumber(
    PXCache sender,
    object data,
    string numberingID,
    DateTime? dateTime)
  {
    return AutoNumberAttribute.GetNextNumber(sender, data, numberingID, dateTime, (string) null, out string _, out string _, out int? _);
  }

  protected static string GetNextNumberInt(
    PXCache sender,
    object data,
    string numberingID,
    DateTime? dateTime,
    string lastAssigned,
    out string LastNbr,
    out string WarnNbr,
    out int? NumberingSEQ)
  {
    if (numberingID != null && dateTime.HasValue)
    {
      int? branchId = sender.Graph.Accessinfo.BranchID;
      if (data != null && sender.Fields.Contains("BranchID"))
      {
        object stateExt = sender.GetStateExt(data, "BranchID");
        if (stateExt is PXFieldState && ((PXFieldState) stateExt).Required.GetValueOrDefault())
          branchId = (int?) sender.GetValue(data, "BranchID");
      }
      NumberingSequence numberingSequence = AutoNumberAttribute.GetNumberingSequence(numberingID, branchId, dateTime);
      LastNbr = numberingSequence != null ? numberingSequence.LastNbr : throw new AutoNumberException("Cannot generate the next number for the {0} sequence.", new object[1]
      {
        (object) numberingID
      });
      WarnNbr = numberingSequence.WarnNbr;
      NumberingSEQ = numberingSequence.NumberingSEQ;
      string nextNumberInt = AutoNumberAttribute.NextNumber(LastNbr, numberingSequence.NbrStep.GetValueOrDefault());
      int? nbrStep;
      if (string.Equals(lastAssigned, nextNumberInt, StringComparison.InvariantCultureIgnoreCase))
      {
        string str = nextNumberInt;
        nbrStep = numberingSequence.NbrStep;
        int valueOrDefault = nbrStep.GetValueOrDefault();
        nextNumberInt = AutoNumberAttribute.NextNumber(str, valueOrDefault);
      }
      else if (lastAssigned != null && lastAssigned.CompareTo(nextNumberInt) > 0)
      {
        string str = lastAssigned;
        nbrStep = numberingSequence.NbrStep;
        int valueOrDefault = nbrStep.GetValueOrDefault();
        nextNumberInt = AutoNumberAttribute.NextNumber(str, valueOrDefault);
      }
      if (nextNumberInt.CompareTo(numberingSequence.EndNbr) >= 0)
        throw new PXException("Cannot generate the next number for the {0} sequence because it is expired.", new object[1]
        {
          (object) numberingID
        });
      try
      {
        if (LastNbr != numberingSequence.StartNbr)
        {
          if (!PXDatabase.Update<NumberingSequence>(new PXDataFieldParam[5]
          {
            (PXDataFieldParam) new PXDataFieldAssign<NumberingSequence.lastNbr>((object) nextNumberInt),
            (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.numberingID>((object) numberingID),
            (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.numberingSEQ>((object) NumberingSEQ),
            (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.lastNbr>((object) LastNbr),
            (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
          }))
          {
            PXDatabase.Update<NumberingSequence>(new PXDataFieldParam[3]
            {
              (PXDataFieldParam) new PXDataFieldAssign<NumberingSequence.nbrStep>((object) numberingSequence.NbrStep),
              (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.numberingID>((object) numberingID),
              (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.numberingSEQ>((object) NumberingSEQ)
            });
            using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<NumberingSequence>(new PXDataField[3]
            {
              (PXDataField) new PXDataField<NumberingSequence.lastNbr>(),
              (PXDataField) new PXDataFieldValue<NumberingSequence.numberingID>((object) numberingID),
              (PXDataField) new PXDataFieldValue<NumberingSequence.numberingSEQ>((object) NumberingSEQ)
            }))
            {
              if (pxDataRecord != null)
              {
                LastNbr = pxDataRecord.GetString(0);
                string str = LastNbr;
                nbrStep = numberingSequence.NbrStep;
                int valueOrDefault = nbrStep.GetValueOrDefault();
                nextNumberInt = AutoNumberAttribute.NextNumber(str, valueOrDefault);
                if (nextNumberInt.CompareTo(numberingSequence.EndNbr) >= 0)
                  throw new PXException("Cannot generate the next number for the {0} sequence because it is expired.", new object[1]
                  {
                    (object) numberingID
                  });
              }
            }
            PXDatabase.Update<NumberingSequence>(new PXDataFieldParam[3]
            {
              (PXDataFieldParam) new PXDataFieldAssign<NumberingSequence.lastNbr>((object) nextNumberInt),
              (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.numberingID>((object) numberingID),
              (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.numberingSEQ>((object) NumberingSEQ)
            });
          }
        }
        else
          PXDatabase.Update<NumberingSequence>(new PXDataFieldParam[4]
          {
            (PXDataFieldParam) new PXDataFieldAssign<NumberingSequence.lastNbr>((object) nextNumberInt),
            (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.numberingID>((object) numberingID),
            (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.numberingSEQ>((object) NumberingSEQ),
            (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
          });
      }
      catch (PXDbOperationSwitchRequiredException ex)
      {
        PXDatabase.Insert<NumberingSequence>(new PXDataFieldAssign[14]
        {
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.endNbr>((PXDbType) 22, new int?(15), (object) numberingSequence.EndNbr),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.lastNbr>((PXDbType) 22, new int?(15), (object) nextNumberInt),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.warnNbr>((PXDbType) 22, new int?(15), (object) numberingSequence.WarnNbr),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.nbrStep>((PXDbType) 8, new int?(4), (object) numberingSequence.NbrStep.GetValueOrDefault()),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.startNbr>((PXDbType) 22, new int?(15), (object) numberingSequence.StartNbr),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.startDate>((PXDbType) 4, (object) numberingSequence.StartDate),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.createdByID>((PXDbType) 14, new int?(16 /*0x10*/), (object) numberingSequence.CreatedByID),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.createdByScreenID>((PXDbType) 3, new int?(8), (object) numberingSequence.CreatedByScreenID),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.createdDateTime>((PXDbType) 4, new int?(8), (object) numberingSequence.CreatedDateTime),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.lastModifiedByID>((PXDbType) 14, new int?(16 /*0x10*/), (object) numberingSequence.LastModifiedByID),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.lastModifiedByScreenID>((PXDbType) 3, new int?(8), (object) numberingSequence.LastModifiedByScreenID),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.lastModifiedDateTime>((PXDbType) 4, new int?(8), (object) numberingSequence.LastModifiedDateTime),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.numberingID>((PXDbType) 22, new int?(10), (object) numberingID),
          (PXDataFieldAssign) new PXDataFieldAssign<NumberingSequence.nBranchID>((PXDbType) 8, new int?(4), (object) numberingSequence.NBranchID)
        });
      }
      return nextNumberInt;
    }
    LastNbr = (string) null;
    WarnNbr = (string) null;
    NumberingSEQ = new int?();
    return (string) null;
  }

  protected static string GetNextNumber(
    PXCache sender,
    object data,
    string numberingID,
    DateTime? dateTime,
    string lastAssigned,
    out string LastNbr,
    out string WarnNbr,
    out int? NumberingSEQ)
  {
    if (!AutoNumberAttribute.IS_SEPARATE_SCOPE)
      return AutoNumberAttribute.GetNextNumberInt(sender, data, numberingID, dateTime, lastAssigned, out LastNbr, out WarnNbr, out NumberingSEQ);
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        PXTransactionScope.SetSuppressWorkflow(true);
        string nextNumberInt = AutoNumberAttribute.GetNextNumberInt(sender, data, numberingID, dateTime, lastAssigned, out LastNbr, out WarnNbr, out NumberingSEQ);
        transactionScope.Complete();
        return nextNumberInt;
      }
    }
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if ((e.Operation & 3) == 2 && e.TranStatus == 2)
    {
      if (!this.UserNumbering.GetValueOrDefault())
      {
        try
        {
          if (e.Exception is PXLockViolationException exception)
          {
            if (exception.IsSameEntity(e.Row))
            {
              string str = sender.GetValue(e.Row, this._FieldOrdinal) as string;
              if (!string.IsNullOrEmpty(str))
              {
                if (str == this.NewNumber)
                {
                  PXDatabase.Update<NumberingSequence>(new PXDataFieldParam[4]
                  {
                    (PXDataFieldParam) new PXDataFieldAssign<NumberingSequence.lastNbr>((object) str),
                    (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.numberingID>((object) this._numberingID),
                    (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.numberingSEQ>((object) this.NumberingSEQ),
                    (PXDataFieldParam) new PXDataFieldRestrict<NumberingSequence.lastNbr>((object) this.LastNbr)
                  });
                  exception.Retry = true;
                }
              }
            }
          }
        }
        catch
        {
        }
        if (this._KeyToAbort != null)
          sender.SetValue(e.Row, this._FieldOrdinal, this._KeyToAbort);
      }
    }
    if (e.TranStatus == null)
      return;
    this._KeyToAbort = (object) null;
  }

  void IPXRowInsertingSubscriber.RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    string str = (string) sender.GetValue(e.Row, this._FieldOrdinal);
    if (this.UserNumbering.GetValueOrDefault() && str == null && sender.Graph.UnattendedMode && !e.ExternalCall)
      throw new PXException("Cannot generate the next number. Manual Numbering is activated for '{0}'", new object[1]
      {
        (object) this._numberingID
      });
  }

  void IPXFieldSelectingSubscriber.FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != 2 && !e.IsAltered)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), this._FieldName, new bool?(), new int?(-1), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
  }

  public static bool TryToGetNextNumber(string str, int count, out string nextNumber)
  {
    try
    {
      nextNumber = AutoNumberAttribute.NextNumber(str, count);
      return true;
    }
    catch (AutoNumberException ex)
    {
      nextNumber = (string) null;
      return false;
    }
  }

  public static bool TryToGetNextNumber(string str, out string nextNumber)
  {
    return AutoNumberAttribute.TryToGetNextNumber(str, 1, out nextNumber);
  }

  public static bool TryToGetNextNumber(string str)
  {
    return AutoNumberAttribute.TryToGetNextNumber(str, out string _);
  }

  public static bool CheckIfNumberEndsDigit(string str)
  {
    return !string.IsNullOrEmpty(str) && char.IsDigit(str.Last<char>());
  }

  public static string NextNumber(string str, short count)
  {
    return AutoNumberAttribute.NextNumber(str, (int) count);
  }

  public static string NextNumber(string str, int count)
  {
    if (str == null)
      throw new AutoNumberException();
    bool flag = true;
    int num1 = Math.Abs(count);
    int num2 = Math.Sign(count);
    StringBuilder stringBuilder = new StringBuilder();
    for (int length = str.Length; length > 0; --length)
    {
      string input = str.Substring(length - 1, 1);
      if (Regex.IsMatch(input, "[^0-9]"))
        flag = false;
      if (flag && Regex.IsMatch(input, "[0-9]"))
      {
        int int16_1 = (int) Convert.ToInt16(input);
        string str1 = Convert.ToString(num1);
        int int16_2 = (int) Convert.ToInt16(str1.Substring(str1.Length - 1, 1));
        int num3;
        if (num2 >= 0)
        {
          stringBuilder.Append((int16_1 + int16_2) % 10);
          num3 = num1 - int16_2 + (int16_1 + int16_2 - (int16_1 + int16_2) % 10);
        }
        else
        {
          stringBuilder.Append((10 + int16_1 - int16_2) % 10);
          num3 = num1 - int16_2 - (int16_1 - int16_2 - (10 + int16_1 - int16_2) % 10);
        }
        num1 = num3 / 10;
        if (num1 == 0)
          flag = false;
      }
      else
        stringBuilder.Append(input);
    }
    if (num1 != 0)
      throw new AutoNumberException();
    char[] charArray = stringBuilder.ToString().ToCharArray();
    Array.Reverse((Array) charArray);
    return new string(charArray);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2023 R2. Please use TryToGetNextNumber(string str) instead.")]
  public static bool CanNextNumber(string str)
  {
    try
    {
      AutoNumberAttribute.NextNumber(str, 1);
      return true;
    }
    catch (AutoNumberException ex)
    {
      return false;
    }
  }

  public static string NextNumber(string str) => AutoNumberAttribute.NextNumber(str, true);

  public static string NextNumber(string str, bool silent)
  {
    try
    {
      return AutoNumberAttribute.NextNumber(str, 1);
    }
    catch (AutoNumberException ex)
    {
      if (silent)
        return str;
      throw ex;
    }
  }

  protected struct Pairer(string docTypeValue)
  {
    private readonly string docTypeValue = docTypeValue;

    public Tuple<string, Type> To<TSetupField>() where TSetupField : IBqlField
    {
      return Tuple.Create<string, Type>(this.docTypeValue, typeof (TSetupField));
    }
  }

  public enum NullNumberingMode
  {
    ViewOnly,
    UserNumbering,
  }

  public class Numberings : IPrefetchable, IPXCompanyDependent
  {
    protected Dictionary<string, string> _items = new Dictionary<string, string>();

    void IPrefetchable.Prefetch()
    {
      this._items.Clear();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Numbering>(new PXDataField[3]
      {
        (PXDataField) new PXDataField<Numbering.numberingID>(),
        PXDBLocalizableStringAttribute.GetValueSelect("Numbering", "NewSymbol", false),
        (PXDataField) new PXDataField<Numbering.userNumbering>()
      }))
      {
        string key = pxDataRecord.GetString(0);
        string str = pxDataRecord.GetString(1);
        bool? boolean = pxDataRecord.GetBoolean(2);
        this._items[key] = boolean.GetValueOrDefault() ? (string) null : str;
      }
    }

    public bool TryGetValue(string key, out string value)
    {
      return this._items.TryGetValue(key, out value);
    }

    public ReadOnlyDictionary<string, string> GetNumberings()
    {
      return new ReadOnlyDictionary<string, string>((IDictionary<string, string>) this._items);
    }
  }
}
