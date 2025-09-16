// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.Graphs.EPEventMaint.Extensions.EPEventMaint_AttendeeExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Export.Imc;
using PX.Objects.CR.Extensions;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#nullable enable
namespace PX.Objects.EP.Graphs.EPEventMaint.Extensions;

public class EPEventMaint_AttendeeExt : PXGraphExtension<
#nullable disable
PX.Objects.EP.EPEventMaint>
{
  public static readonly string DoubleNewLine = Environment.NewLine + Environment.NewLine;
  [PXViewDetailsButton(typeof (EPAttendee.contactID), typeof (SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<BqlField<EPAttendee.contactID, IBqlInt>.FromCurrent>>), WindowMode = PXRedirectHelper.WindowMode.New)]
  public FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Contact>.On<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  EPAttendee.contactID>>>>.Where<BqlOperand<
  #nullable enable
  EPAttendee.eventNoteID, IBqlGuid>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.CR.CRActivity.noteID, IBqlGuid>.FromCurrent>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  EPAttendee.isOwner, IBqlBool>.Desc>>, 
  #nullable disable
  EPAttendee>.View Attendees;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  EPAttendee.eventNoteID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.CR.CRActivity.noteID, IBqlGuid>.FromCurrent>>>>, 
  #nullable disable
  PX.Data.And<BqlOperand<
  #nullable enable
  EPAttendee.invitation, IBqlInt>.IsNotIn<
  #nullable disable
  PXInvitationStatusAttribute.notinvited, PXInvitationStatusAttribute.rejected>>>>.And<BqlOperand<
  #nullable enable
  EPAttendee.isOwner, IBqlBool>.IsNotEqual<
  #nullable disable
  PX.Data.True>>>, EPAttendee>.View InvitedAttendees;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  EPAttendee.eventNoteID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.CR.CRActivity.noteID, IBqlGuid>.FromCurrent>>>>, 
  #nullable disable
  PX.Data.And<BqlOperand<
  #nullable enable
  EPAttendee.invitation, IBqlInt>.IsEqual<
  #nullable disable
  PXInvitationStatusAttribute.notinvited>>>>.And<BqlOperand<
  #nullable enable
  EPAttendee.isOwner, IBqlBool>.IsNotEqual<
  #nullable disable
  PX.Data.True>>>, EPAttendee>.View NotInvitedAttendees;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  EPAttendee.contactID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  AccessInfo.contactID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  EPAttendee.eventNoteID, IBqlGuid>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.CR.CRActivity.noteID, IBqlGuid>.FromCurrent>>>, 
  #nullable disable
  EPAttendee>.View AttendeeForCurrentUser;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  EPAttendee.eventNoteID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.CR.CRActivity.noteID, IBqlGuid>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  EPAttendee.isOwner, IBqlBool>.IsNotEqual<
  #nullable disable
  PX.Data.True>>>, EPAttendee>.View AttendeesExceptOwner;
  [PXHidden]
  public FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  EPAttendee.eventNoteID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.CR.CRActivity.noteID, IBqlGuid>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  EPAttendee.isOwner, IBqlBool>.IsEqual<
  #nullable disable
  PX.Data.True>>>, EPAttendee>.View AttendeeForOwner;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View SendInvitationCancellationsToAttendeesAnswer;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View SendInvitationsToAttendeesAnswer;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View SendInvitationsToOnlyNotInvitedAttendeesAnswer;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View SendInvitationReschdulesToAttendeesAnswer;
  public PXAction<PX.Objects.CR.CRActivity> AcceptInvitation;
  public PXAction<PX.Objects.CR.CRActivity> RejectInvitation;
  public PXAction<PX.Objects.CR.CRActivity> SendInvitations;
  public PXAction<PX.Objects.CR.CRActivity> SendPersonalInvitation;

  [InjectDependency]
  public ICurrentUserInformationProvider CurrentUserInformationProvider { get; private set; }

  [PXOverride]
  public virtual IEnumerable cancelActivity(PXAdapter adapter, PXButtonDelegate del)
  {
    if (this.ConfirmToSendInvitationCancellationsToAttendees())
    {
      this.EnsureIsSavedWithoutSendingEmails();
      PX.Objects.EP.EPEventMaint graph = this.Base.CloneGraphState<PX.Objects.EP.EPEventMaint>();
      EPEventMaint_AttendeeExt ext = graph.GetExtension<EPEventMaint_AttendeeExt>();
      PX.Objects.CR.CRActivity current = this.Base.Events.Current;
      PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
      {
        ext.SendInvitationCancellationsToAttendees();
        ext.SetAllPrimaryAnswers(WebDialogResult.No);
        graph.CancelRow(current);
      }));
    }
    else
    {
      this.SetAllPrimaryAnswers(WebDialogResult.No);
      this.Base.CancelRow(this.Base.Events.Current);
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Accept the invitation to the event", DisplayOnMainToolbar = true, Category = "Management")]
  [PXUIField(DisplayName = "Accept", Visible = false, MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable acceptInvitation(PXAdapter adapter)
  {
    this.AcceptParticipation(true);
    return adapter.Get();
  }

  [PXButton(Tooltip = "Decline the invitation to the event", DisplayOnMainToolbar = true, Category = "Management")]
  [PXUIField(DisplayName = "Decline", Visible = false, MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable rejectInvitation(PXAdapter adapter)
  {
    this.AcceptParticipation(false);
    return adapter.Get();
  }

  protected virtual void AcceptParticipation(bool accept)
  {
    int num = accept ? 2 : 3;
    foreach (EPAttendee firstTableItem in this.AttendeeForCurrentUser.Select().FirstTableItems)
    {
      firstTableItem.Invitation = new int?(num);
      this.AttendeeForCurrentUser.Update(firstTableItem);
    }
    this.Base.Save.PressImpl();
  }

  [PXButton(Tooltip = "Send the invitations to all attendees by email")]
  [PXUIField(DisplayName = "Invite All", Visible = false, MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable sendInvitations(PXAdapter adapter)
  {
    List<EPAttendee> list1 = this.Attendees.Select().FirstTableItems.ToList<EPAttendee>();
    List<EPAttendee> list2 = this.NotInvitedAttendees.Select().FirstTableItems.ToList<EPAttendee>();
    List<EPAttendee> forSend;
    if (!list2.Any<EPAttendee>())
      forSend = this.SendInvitationsToAttendeesAnswer.WithAnswerForCbApi<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.Yes).WithAnswerForUnattendedMode<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.No).View.Ask((object) null, "Confirmation", "The invitations will be sent to all potential attendees, including those that were previously invited.", MessageButtons.YesNo, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
      {
        {
          WebDialogResult.Yes,
          "Confirm"
        },
        {
          WebDialogResult.No,
          "Cancel"
        }
      }, MessageIcon.None, true).IsPositive() ? list1 : (List<EPAttendee>) null;
    else if (list2.Count != list1.Count)
      forSend = this.SendInvitationsToOnlyNotInvitedAttendeesAnswer.WithAnswerForCbApi<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.Yes).WithAnswerForUnattendedMode<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.No).View.Ask((object) null, "Confirmation", "The invitations will be sent to only the potential attendees who have not been invited.", MessageButtons.YesNo, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
      {
        {
          WebDialogResult.Yes,
          "Confirm"
        },
        {
          WebDialogResult.No,
          "Cancel"
        }
      }, MessageIcon.None, true).IsPositive() ? list2 : list1;
    else
      forSend = list2;
    if (forSend != null && forSend.Any<EPAttendee>())
    {
      this.EnsureIsSavedWithoutSendingEmails();
      PX.Objects.EP.EPEventMaint graph = this.Base.CloneGraphState<PX.Objects.EP.EPEventMaint>();
      PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
      {
        graph.GetExtension<EPEventMaint_AttendeeExt>().SendEmails(EPEventMaint_AttendeeExt.NotificationTypes.Invitation, (IEnumerable<EPAttendee>) forSend);
        graph.Actions.PressSave();
      }));
    }
    this.SendInvitationsToAttendeesAnswer.View.ClearDialog();
    this.SendInvitationsToOnlyNotInvitedAttendeesAnswer.View.ClearDialog();
    this.Attendees.View.RequestRefresh();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Send the invitations to the selected attendees by email")]
  [PXUIField(DisplayName = "Invite", Visible = false, MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable sendPersonalInvitation(PXAdapter adapter)
  {
    PX.Objects.CR.Contact contact = this.Base.CurrentOwner.SelectSingle();
    if (contact != null)
    {
      int? contactId = contact.ContactID;
    }
    EPAttendee attendee = this.Attendees.Current;
    int num1;
    if (attendee != null)
    {
      int? invitation = attendee.Invitation;
      int num2 = 0;
      if (!(invitation.GetValueOrDefault() == num2 & invitation.HasValue))
        num1 = this.InvitedAttendees.WithAnswerForCbApi<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<EPAttendee.eventNoteID, Equal<BqlField<PX.Objects.CR.CRActivity.noteID, IBqlGuid>.FromCurrent>>>>, PX.Data.And<BqlOperand<EPAttendee.invitation, IBqlInt>.IsNotIn<PXInvitationStatusAttribute.notinvited, PXInvitationStatusAttribute.rejected>>>>.And<BqlOperand<EPAttendee.isOwner, IBqlBool>.IsNotEqual<PX.Data.True>>>, EPAttendee>.View>(WebDialogResult.Yes).WithAnswerForUnattendedMode<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<EPAttendee.eventNoteID, Equal<BqlField<PX.Objects.CR.CRActivity.noteID, IBqlGuid>.FromCurrent>>>>, PX.Data.And<BqlOperand<EPAttendee.invitation, IBqlInt>.IsNotIn<PXInvitationStatusAttribute.notinvited, PXInvitationStatusAttribute.rejected>>>>.And<BqlOperand<EPAttendee.isOwner, IBqlBool>.IsNotEqual<PX.Data.True>>>, EPAttendee>.View>(WebDialogResult.No).View.Ask((object) null, "Confirmation", "The invitation that has already been sent will be sent once again.", MessageButtons.YesNo, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
        {
          {
            WebDialogResult.Yes,
            "Confirm"
          },
          {
            WebDialogResult.No,
            "Cancel"
          }
        }, MessageIcon.None, true).IsPositive() ? 1 : 0;
      else
        num1 = 1;
    }
    else
      num1 = 0;
    if (num1 != 0)
    {
      this.EnsureIsSavedWithoutSendingEmails();
      PX.Objects.EP.EPEventMaint graph = this.Base.CloneGraphState<PX.Objects.EP.EPEventMaint>();
      EPEventMaint_AttendeeExt ext = graph.GetExtension<EPEventMaint_AttendeeExt>();
      PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
      {
        ext.SendEmail(EPEventMaint_AttendeeExt.NotificationTypes.Invitation, attendee);
        ext.SendInvitationsToAttendeesAnswer.View.ClearDialog();
        ext.SendInvitationCancellationsToAttendeesAnswer.View.ClearDialog();
        ext.SendInvitationReschdulesToAttendeesAnswer.View.ClearDialog();
        graph.Actions.PressSave();
      }));
    }
    this.Attendees.View.RequestRefresh();
    return adapter.Get();
  }

  [PXOverride]
  public virtual void Persist(System.Action del)
  {
    this.AssertAttendees();
    (bool sendCancellation1, bool sendReschedule1, bool sendInvitation1) = this.GetInvitationStates();
    List<EPAttendee> removedAttendees1 = this.GetRemovedAttendeesToSendInvitationCancellations().ToList<EPAttendee>();
    del();
    if (!(sendCancellation1 | sendReschedule1 | sendInvitation1) && !removedAttendees1.Any<EPAttendee>())
      return;
    if (PXLongOperation.IsLongOperationContext())
    {
      Send(this);
    }
    else
    {
      EPEventMaint_AttendeeExt ext = this.Base.CloneGraphState<PX.Objects.EP.EPEventMaint>().GetExtension<EPEventMaint_AttendeeExt>();
      PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() => Send(ext)));
    }
    List<EPAttendee> removedAttendees2;
    bool sendCancellation;
    bool sendReschedule;
    bool sendInvitation;

    void Send(EPEventMaint_AttendeeExt ext)
    {
      ext.SendInvitationCancellationsToRemovedAttendees((IEnumerable<EPAttendee>) removedAttendees2);
      if (sendCancellation)
        ext.SendInvitationCancellationsToAttendees();
      if (sendReschedule)
        ext.SendInvitationReschdulesToAttendees();
      if (!sendInvitation)
        return;
      ext.SendInvitationsToAttendees();
    }
  }

  [PXOverride]
  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows,
    ExecuteSelectDelegate executeSelect)
  {
    if (this.Base.IsCopyPasteContext && viewName == "Attendees")
      viewName = "AttendeesExceptOwner";
    return executeSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  public virtual void _(PX.Data.Events.RowSelected<PX.Objects.CR.CRActivity> e)
  {
    if (e.Row == null)
      return;
    bool isVisible = this.Base.IsCurrentUserOwnerOfEvent(e.Row);
    bool flag1 = this.Base.WasEventOriginallyEditable(e.Row);
    this.SendInvitations.SetVisible(isVisible);
    this.SendInvitations.SetEnabled(flag1 & isVisible);
    this.SendPersonalInvitation.SetVisible(isVisible);
    this.SendPersonalInvitation.SetEnabled(flag1 & isVisible);
    PXCache<EPAttendee> pxCache = this.Base.Caches<EPAttendee>();
    int num1;
    bool flag2 = (num1 = flag1 & isVisible ? 1 : 0) != 0;
    pxCache.AllowUpdate = num1 != 0;
    int num2;
    bool flag3 = (num2 = flag2 ? 1 : 0) != 0;
    pxCache.AllowInsert = num2 != 0;
    pxCache.AllowDelete = flag3;
    EPAttendee epAttendee = this.AttendeeForCurrentUser.SelectSingle();
    int? invitation;
    int num3;
    if (((isVisible ? 0 : (epAttendee != null ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
    {
      invitation = epAttendee.Invitation;
      num3 = invitation.GetValueOrDefault() != 5 ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag4 = num3 != 0;
    this.AcceptInvitation.SetVisible(!isVisible);
    this.RejectInvitation.SetVisible(!isVisible);
    PXAction<PX.Objects.CR.CRActivity> acceptInvitation = this.AcceptInvitation;
    int num4;
    if (flag4)
    {
      invitation = epAttendee.Invitation;
      num4 = invitation.GetValueOrDefault() != 2 ? 1 : 0;
    }
    else
      num4 = 0;
    acceptInvitation.SetEnabled(num4 != 0);
    PXAction<PX.Objects.CR.CRActivity> rejectInvitation = this.RejectInvitation;
    int num5;
    if (flag4)
    {
      invitation = epAttendee.Invitation;
      num5 = invitation.GetValueOrDefault() != 3 ? 1 : 0;
    }
    else
      num5 = 0;
    rejectInvitation.SetEnabled(num5 != 0);
  }

  public virtual void _(PX.Data.Events.RowInserted<PX.Objects.CR.CRActivity> e)
  {
    this.EnsureAttendeeForOwner(e.Row);
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.CR.CRActivity, PX.Objects.CR.CRActivity.ownerID> e)
  {
    if (object.Equals(e.NewValue, e.OldValue))
      return;
    this.EnsureAttendeeForOwner(e.Row);
  }

  public virtual void _(PX.Data.Events.RowSelected<EPAttendee> e)
  {
    e.Cache.AdjustUI((object) e.Row).ForAllFields((System.Action<PXUIFieldAttribute>) (a =>
    {
      if (e.Row == null)
        return;
      int? invitation = e.Row.Invitation;
      int num = 0;
      if (invitation.GetValueOrDefault() == num & invitation.HasValue)
      {
        bool? isOwner = e.Row.IsOwner;
        if (!isOwner.HasValue || !isOwner.GetValueOrDefault())
          return;
      }
      a.Enabled = false;
    }));
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<EPAttendee, EPAttendee.contactID> e)
  {
    if (e.NewValue == e.OldValue)
      return;
    if (e.NewValue is int newValue)
      e.Row.Email = PX.Objects.CR.Contact.PK.Find((PXGraph) this.Base, new int?(newValue))?.EMail;
    else
      e.Row.Email = (string) null;
  }

  public void SendEmail(EPEventMaint_AttendeeExt.NotificationTypes invite, EPAttendee attendee)
  {
    this.SendEmails(invite, (IEnumerable<EPAttendee>) new EPAttendee[1]
    {
      attendee
    });
  }

  public virtual void SendEmails(
    EPEventMaint_AttendeeExt.NotificationTypes invite,
    IEnumerable<EPAttendee> attendees)
  {
    PX.Objects.CR.CRActivity current = this.Base.Events.Current;
    (string name, byte[] numArray) = this.GetAttachmentForEvent(this.Base.Events.Current);
    foreach (EPAttendee attendee in attendees)
    {
      this.AssertAttendee(attendee);
      NotificationGenerator notificationGenerator1 = this.Base.Setup.Current.IsSimpleNotification.GetValueOrDefault() ? this.GetNotificationGeneratorForSimpleEmail(invite, current) : this.GetNotificationGeneratorForTemplate(invite, current);
      notificationGenerator1.MailAccountId = MailAccountManager.DefaultAnyMailAccountID;
      notificationGenerator1.To = attendee.Email;
      NotificationGenerator notificationGenerator2 = notificationGenerator1;
      string str;
      switch (invite)
      {
        case EPEventMaint_AttendeeExt.NotificationTypes.Invitation:
          str = PXMessages.LocalizeFormatNoPrefixNLA("Invitation to {0}", (object) current.Subject);
          break;
        case EPEventMaint_AttendeeExt.NotificationTypes.Reschedule:
          str = PXMessages.LocalizeFormatNoPrefixNLA("Rescheduling of {0}", (object) current.Subject);
          break;
        case EPEventMaint_AttendeeExt.NotificationTypes.Cancel:
          str = PXMessages.LocalizeFormatNoPrefixNLA("Cancel the invitation to {0}", (object) current.Subject);
          break;
        default:
          str = notificationGenerator1.Subject;
          break;
      }
      notificationGenerator2.Subject = str;
      notificationGenerator1.AddAttachment(name, numArray);
      notificationGenerator1.ParentNoteID = current.NoteID;
      try
      {
        notificationGenerator1.Send();
      }
      catch (Exception ex)
      {
        throw new PXException(ex, "The following error has occurred during the sending of the emails: {0}", new object[1]
        {
          (object) ex.Message
        });
      }
      EPAttendee epAttendee = attendee;
      int? nullable;
      switch (invite)
      {
        case EPEventMaint_AttendeeExt.NotificationTypes.Invitation:
          nullable = new int?(1);
          break;
        case EPEventMaint_AttendeeExt.NotificationTypes.Reschedule:
          nullable = new int?(4);
          break;
        case EPEventMaint_AttendeeExt.NotificationTypes.Cancel:
          nullable = new int?(5);
          break;
        default:
          nullable = attendee.Invitation;
          break;
      }
      epAttendee.Invitation = nullable;
      if (this.Attendees.Cache.GetStatus((object) attendee) != PXEntryStatus.Deleted)
      {
        this.Attendees.Update(attendee);
        this.Attendees.Cache.PersistUpdated((object) attendee);
      }
    }
  }

  public virtual void AssertAttendee(EPAttendee attendees)
  {
    if (string.IsNullOrEmpty(attendees.Email))
    {
      PXSetPropertyException<EPAttendee.email> propertyException = new PXSetPropertyException<EPAttendee.email>("The email address cannot be empty.");
      this.Attendees.Cache.RaiseExceptionHandling<EPAttendee.email>((object) attendees, (object) attendees.Email, (Exception) propertyException);
      throw propertyException;
    }
  }

  public virtual void AssertAttendees()
  {
    bool flag = false;
    foreach (EPAttendee attendees in this.Attendees.Cache.Cached)
    {
      if (EnumerableExtensions.IsIn<PXEntryStatus>(this.Attendees.Cache.GetStatus((object) attendees), PXEntryStatus.Inserted, PXEntryStatus.Updated, PXEntryStatus.Modified))
      {
        try
        {
          this.AssertAttendee(attendees);
        }
        catch (PXSetPropertyException<EPAttendee.email> ex)
        {
          flag = true;
        }
      }
    }
    if (flag)
      throw new PXInvalidOperationException("At least one email address of an attendee has not been found. The email address cannot be empty.");
  }

  public virtual string GetBodyForSimpleNotification(
    EPEventMaint_AttendeeExt.NotificationTypes invite,
    PX.Objects.CR.CRActivity @event,
    PX.Objects.CR.Contact owner)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    string str1 = string.Empty;
    switch (invite)
    {
      case EPEventMaint_AttendeeExt.NotificationTypes.Reschedule:
        stringBuilder1.Append(PXMessages.LocalizeNoPrefix("The event was rescheduled."));
        str1 = GetEventStringInfo("Start Date: {0} {1}", "Due Date: {0} {1}");
        break;
      case EPEventMaint_AttendeeExt.NotificationTypes.Cancel:
        stringBuilder1.Append(PXMessages.LocalizeNoPrefix("The event was canceled."));
        break;
      default:
        StringBuilder stringBuilder2 = stringBuilder1;
        string str2;
        if (owner == null)
          str2 = PXMessages.LocalizeNoPrefix("You are invited to the event.");
        else
          str2 = PXMessages.LocalizeFormatNoPrefixNLA("{0} invited you to the event.", (object) owner.DisplayName);
        stringBuilder2.Append(str2);
        str1 = GetEventStringInfo("New Start Date: {0} {1}", "New Due Date: {0} {1}");
        break;
    }
    stringBuilder1.Append(EPEventMaint_AttendeeExt.DoubleNewLine).Append(PXMessages.LocalizeFormatNoPrefixNLA("Subject: {0}", (object) @event.Subject.Trim())).Append(EPEventMaint_AttendeeExt.DoubleNewLine);
    if (!string.IsNullOrWhiteSpace(@event.Location))
      stringBuilder1.Append(PXMessages.LocalizeFormatNoPrefixNLA("Location: {0}", (object) @event.Location.Trim())).Append(EPEventMaint_AttendeeExt.DoubleNewLine);
    stringBuilder1.Append(str1);
    if (owner != null && this.Base.Setup.Current.AddContactInformation.GetValueOrDefault())
      stringBuilder1.Append(EPEventMaint_AttendeeExt.DoubleNewLine).Append(EPEventMaint_AttendeeExt.DoubleNewLine).Append(PXMessages.LocalizeNoPrefix("Contact Person:")).Append(PXMessages.LocalizeFormatNoPrefixNLA("Name: {0}", (object) owner.DisplayName)).Append(PXMessages.LocalizeFormatNoPrefixNLA("Email: {0}", (object) owner.EMail)).Append(PXMessages.LocalizeFormatNoPrefixNLA("Phone: {0}", (object) owner.Phone1));
    return stringBuilder1.ToString();

    string GetEventStringInfo(string startDateFormat, string endDateFormat)
    {
      if (!@event.StartDate.HasValue || !@event.EndDate.HasValue)
        return string.Empty;
      System.DateTime dateTime1 = @event.StartDate.Value;
      System.DateTime dateTime2 = @event.EndDate.Value;
      string str1 = string.IsNullOrEmpty(@event.TimeZone) ? LocaleInfo.GetTimeZone().DisplayName : PXTimeZoneInfo.FindSystemTimeZoneById(@event.TimeZone).DisplayName;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(PXMessages.LocalizeFormatNoPrefixNLA(startDateFormat, (object) dateTime1, (object) str1));
      stringBuilder.Append(EPEventMaint_AttendeeExt.DoubleNewLine);
      stringBuilder.Append(PXMessages.LocalizeFormatNoPrefixNLA(endDateFormat, (object) dateTime2, (object) str1));
      if (!string.IsNullOrEmpty(@event.Body))
      {
        string str2 = Tools.ConvertHtmlToSimpleText(@event.Body).Replace(Environment.NewLine, EPEventMaint_AttendeeExt.DoubleNewLine);
        stringBuilder.Append(EPEventMaint_AttendeeExt.DoubleNewLine);
        stringBuilder.Append(str2);
      }
      return stringBuilder.ToString();
    }
  }

  public virtual NotificationGenerator GetNotificationGeneratorForSimpleEmail(
    EPEventMaint_AttendeeExt.NotificationTypes invite,
    PX.Objects.CR.CRActivity @event)
  {
    return new NotificationGenerator((PXGraph) this.Base)
    {
      Body = this.GetBodyForSimpleNotification(invite, @event, this.Base.CurrentOwner.SelectSingle())
    };
  }

  public virtual NotificationGenerator GetNotificationGeneratorForTemplate(
    EPEventMaint_AttendeeExt.NotificationTypes invite,
    PX.Objects.CR.CRActivity @event)
  {
    EPSetup current = this.Base.Setup.Current;
    string str;
    switch (invite)
    {
      case EPEventMaint_AttendeeExt.NotificationTypes.Reschedule:
        str = "RescheduleTemplateID";
        break;
      case EPEventMaint_AttendeeExt.NotificationTypes.Cancel:
        str = "CancelInvitationTemplateID";
        break;
      default:
        str = "InvitationTemplateID";
        break;
    }
    string fieldName = str;
    if (this.Base.Setup.Cache.GetValue((object) current, fieldName) is int notificationId)
      return (NotificationGenerator) TemplateNotificationGenerator.Create((PXGraph) this.Base, (object) @event, notificationId);
    throw new PXInvalidOperationException("{0} is not configured on the Event Setup (EP204070) form.", new object[1]
    {
      (object) (this.Base.Setup.Cache.GetStateExt((object) null, fieldName) is PXFieldState stateExt ? stateExt.DisplayName : fieldName)
    });
  }

  public virtual (string name, byte[] data) GetAttachmentForEvent(PX.Objects.CR.CRActivity @event)
  {
    byte[] array;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      vEvent vevent = this.Base.VCalendarFactory.CreateVEvent((object) @event);
      vevent.Method = "REQUEST";
      vevent.Write((Stream) memoryStream);
      array = memoryStream.ToArray();
    }
    return ("event.ics", array);
  }

  public virtual bool ShouldCancelEvent()
  {
    return this.Base.WasCurrentEventOriginallyEditable() && this.Base.Events.Current?.UIStatus == "CL";
  }

  public virtual bool ConfirmToSendInvitationCancellationsToAttendees()
  {
    if (!this.Base.IsCurrentEventPersisted() || this.Base.IsCurrentEventInThePast() || !this.Base.WasCurrentEventOriginallyEditable() || !this.InvitedAttendees.Select().Any<PXResult<EPAttendee>>())
      return false;
    return this.SendInvitationCancellationsToAttendeesAnswer.WithAnswerForCbApi<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.Yes).WithAnswerForUnattendedMode<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.No).View.Ask((object) null, "Confirmation", "The invitations that have already been sent will be canceled. The cancellation emails will be sent to all potential attendees.", MessageButtons.YesNo, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
    {
      {
        WebDialogResult.Yes,
        "Confirm"
      },
      {
        WebDialogResult.No,
        "Cancel"
      }
    }, MessageIcon.None, true).IsPositive();
  }

  public virtual void SendInvitationCancellationsToAttendees()
  {
    using (new PXReadDeletedScope())
      this.SendEmails(EPEventMaint_AttendeeExt.NotificationTypes.Cancel, this.InvitedAttendees.Select().FirstTableItems);
  }

  public virtual IEnumerable<EPAttendee> GetRemovedAttendeesToSendInvitationCancellations()
  {
    EPEventMaint_AttendeeExt maintAttendeeExt = this;
    if (maintAttendeeExt.Base.IsCurrentEventEditable() && !maintAttendeeExt.Base.IsCurrentEventInThePast())
    {
      foreach (EPAttendee attendee in maintAttendeeExt.Attendees.Cache.Deleted.OfType<EPAttendee>().Where<EPAttendee>((Func<EPAttendee, bool>) (a => a.Email != null && EnumerableExtensions.IsIn<int?>(a.Invitation, new int?(1), new int?(2), new int?(4)))))
      {
        yield return attendee;
        maintAttendeeExt.Attendees.Cache.Remove((object) attendee);
      }
    }
  }

  public virtual void SendInvitationCancellationsToRemovedAttendees(
    IEnumerable<EPAttendee> removedAttendees)
  {
    using (new PXReadDeletedScope())
    {
      if (removedAttendees.Any<EPAttendee>())
        this.SendEmails(EPEventMaint_AttendeeExt.NotificationTypes.Cancel, removedAttendees);
    }
    foreach (EPAttendee removedAttendee in removedAttendees)
    {
      this.Attendees.Cache.ResetPersisted((object) removedAttendee);
      this.Attendees.Cache.PersistDeleted((object) removedAttendee);
    }
  }

  public virtual bool ConfirmToSendInvitationsToAttendees()
  {
    if (!this.Base.IsCurrentEventInThePast() && this.Base.IsCurrentEventEditable())
    {
      PXResultset<EPAttendee> pxResultset1 = this.Attendees.Select();
      PXResultset<EPAttendee> pxResultset2 = this.NotInvitedAttendees.Select();
      if (pxResultset1.Count > 0 && pxResultset2.Count > 0)
        return this.SendInvitationsToAttendeesAnswer.WithAnswerForCbApi<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.Yes).WithAnswerForUnattendedMode<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.No).View.Ask((object) null, "Confirmation", "At least one potential attendee has been selected. The invitations will be sent to all selected potential attendees.", MessageButtons.YesNo, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
        {
          {
            WebDialogResult.Yes,
            "Confirm"
          },
          {
            WebDialogResult.No,
            "Cancel"
          }
        }, MessageIcon.None, true).IsPositive();
    }
    return false;
  }

  public virtual void SendInvitationsToAttendees()
  {
    this.SendEmails(EPEventMaint_AttendeeExt.NotificationTypes.Invitation, this.NotInvitedAttendees.Select().FirstTableItems);
  }

  public virtual bool ConfirmToSendInvitationReschdulesToAttendees()
  {
    if (!this.Base.IsCurrentEventPersisted() || this.Base.IsCurrentEventInThePast() || !this.Base.IsCurrentEventEditable() || !this.InvitedAttendees.Select().Any<PXResult<EPAttendee>>())
      return false;
    return this.SendInvitationReschdulesToAttendeesAnswer.WithAnswerForCbApi<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.Yes).WithAnswerForUnattendedMode<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.No).View.Ask((object) null, "Confirmation", "The invited potential attendees will be notified about the new time of the event.", MessageButtons.YesNo, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
    {
      {
        WebDialogResult.Yes,
        "Confirm"
      },
      {
        WebDialogResult.No,
        "Cancel"
      }
    }, MessageIcon.None, true).IsPositive();
  }

  public virtual void SendInvitationReschdulesToAttendees()
  {
    this.SendEmails(EPEventMaint_AttendeeExt.NotificationTypes.Reschedule, this.InvitedAttendees.Select().FirstTableItems);
  }

  public virtual bool ShouldRescheduleEvent()
  {
    PX.Objects.CR.CRActivity current = this.Base.Events.Current;
    return (!(this.Base.Events.Cache.GetOriginal((object) current) is PX.Objects.CR.CRActivity original) ? 1 : (!this.Base.Events.Cache.ObjectsEqualBy<TypeArrayOf<IBqlField>.FilledWith<PX.Objects.CR.CRActivity.startDate, PX.Objects.CR.CRActivity.endDate, PX.Objects.CR.CRActivity.timeZone>>((object) original, (object) current) ? 1 : 0)) != 0 && !this.Base.IsEventInThePast(current) && this.Base.IsEventEditable(current);
  }

  public virtual (bool sendCancellation, bool sendReschedule, bool sendInvitation) GetInvitationStates()
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    if (this.Base.Events.Current == null)
    {
      List<PX.Objects.CR.CRActivity> list = this.Base.Events.Cache.Deleted.OfType<PX.Objects.CR.CRActivity>().ToList<PX.Objects.CR.CRActivity>();
      if (list.Count != 1)
        return (false, false, false);
      this.Base.Events.Current = list[0];
    }
    switch (this.Base.Events.Cache.GetStatus((object) this.Base.Events.Current))
    {
      case PXEntryStatus.Updated:
      case PXEntryStatus.Modified:
        if (this.ShouldCancelEvent())
        {
          if (this.ConfirmToSendInvitationCancellationsToAttendees())
          {
            flag1 = true;
            break;
          }
          break;
        }
        if (this.ShouldRescheduleEvent() && this.ConfirmToSendInvitationReschdulesToAttendees())
        {
          flag2 = true;
          goto case PXEntryStatus.Inserted;
        }
        goto case PXEntryStatus.Inserted;
      case PXEntryStatus.Inserted:
        if (this.ConfirmToSendInvitationsToAttendees())
        {
          flag3 = true;
          break;
        }
        break;
      case PXEntryStatus.Deleted:
        this.SendInvitationCancellationsToAttendeesAnswer.WithAnswerForMobile<FbqlSelect<SelectFromBase<EPAttendee, TypeArrayOf<IFbqlJoin>.Empty>, EPAttendee>.View>(WebDialogResult.Yes);
        if (this.ConfirmToSendInvitationCancellationsToAttendees())
        {
          flag1 = true;
          break;
        }
        break;
    }
    return (flag1, flag2, flag3);
  }

  public virtual void EnsureIsSavedWithoutSendingEmails()
  {
    if (!this.Base.IsDirty)
      return;
    this.SetAllPrimaryAnswers(WebDialogResult.No);
    this.Base.Actions.PressSave();
  }

  public virtual void SetAllPrimaryAnswers(WebDialogResult answer)
  {
    this.SendInvitationsToAttendeesAnswer.View.Answer = this.SendInvitationCancellationsToAttendeesAnswer.View.Answer = this.SendInvitationReschdulesToAttendeesAnswer.View.Answer = answer;
  }

  public virtual void EnsureAttendeeForOwner(PX.Objects.CR.CRActivity row)
  {
    if (row == null)
      return;
    using (new ReadOnlyScope(new PXCache[1]
    {
      (PXCache) this.Base.Caches<EPAttendee>()
    }))
    {
      bool flag = false;
      PXView view = this.AttendeeForOwner.View;
      object[] currents = (object[]) new PX.Objects.CR.CRActivity[1]
      {
        row
      };
      object[] objArray = Array.Empty<object>();
      foreach (EPAttendee epAttendee in view.SelectMultiBound(currents, objArray))
      {
        if (!flag)
        {
          int? nullable = row.OwnerID;
          if (nullable.HasValue)
          {
            nullable = epAttendee.ContactID;
            int? ownerId = row.OwnerID;
            if (!(nullable.GetValueOrDefault() == ownerId.GetValueOrDefault() & nullable.HasValue == ownerId.HasValue))
            {
              epAttendee.ContactID = row.OwnerID;
              this.AttendeeForOwner.Update(epAttendee);
            }
            flag = true;
            continue;
          }
        }
        this.AttendeeForOwner.Delete(epAttendee);
      }
      if (flag || row == null || !row.OwnerID.HasValue)
        return;
      this.AttendeeForCurrentUser.Insert(new EPAttendee()
      {
        ContactID = row.OwnerID,
        EventNoteID = row.NoteID,
        IsOwner = new bool?(true),
        Invitation = new int?(2)
      });
    }
  }

  public enum NotificationTypes
  {
    Invitation,
    Reschedule,
    Cancel,
  }
}
